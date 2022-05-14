using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ConvertXml
{
    public partial class XmlConverterForm : Form
    {
        // Getting connection string from App.config file
        string ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        public XmlConverterForm()
        {
            InitializeComponent();
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Title = "Upload your xml file:",
                Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*",
                DefaultExt = "*.xml"
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = fileDialog.FileName;
                importBtn.Enabled = true;
                pathTb.Text = filePath;
                LoadXmlTree(filePath);
            }
        }
        #region Load Xml Tree
        //Load Xml Tree Logic
        private void LoadXmlTree(string filepath)
        {
            try
            {
                string xmlFile = File.ReadAllText(filepath, Encoding.UTF8);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlFile);
                treeViewTb.Nodes.Clear();
                treeViewTb.Nodes.Add(new TreeNode(xmlDocument.DocumentElement.Name));
                TreeNode tNode = new TreeNode();
                tNode = treeViewTb.Nodes[0];
                this.AddNode(xmlDocument.DocumentElement, tNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddNode(XmlNode node, TreeNode treeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i;
            if (node.HasChildNodes)
            {
                nodeList = node.ChildNodes;

                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = node.ChildNodes[i];
                    treeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = treeNode.Nodes[i];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                treeNode.Text = (node.OuterXml).Trim();
            }
        }
        #endregion

        private void importBtn_Click(object sender, EventArgs e)
        {
            string XMlFile = pathTb.Text;
            if (File.Exists(XMlFile))
            {
                //Converting xml file to database and then create query!
                DataTable dataTable = CreateDataTableXML(XMlFile);
                if (dataTable.Columns.Count == 0)
                    dataTable.ReadXml(XMlFile);
                string querry = CreateTableQuery(dataTable);
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                //Open connection, check if database already exists if not create it!
                SqlCommand cmd = new SqlCommand("IF OBJECT_ID('dbo." + dataTable.TableName + "', 'U') IS NOT NULL DROP TABLE dbo." + dataTable.TableName + ";", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(querry, con);
                int check = cmd.ExecuteNonQuery();
                // Using SqlBulk , for details you can read it here https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlbulkcopy?view=dotnet-plat-ext-6.0 
                if (check != 0)
                {
                    using (var bulkCopy = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                    {
                        // my DataTable column names match my SQL Column names, so I simply made this loop. However if your column names don't match, just pass in which datatable name matches the SQL column name in Column Mappings
                        foreach (DataColumn col in dataTable.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }

                        bulkCopy.BulkCopyTimeout = 600;
                        bulkCopy.DestinationTableName = dataTable.TableName;
                        bulkCopy.WriteToServer(dataTable);
                    }

                    MessageBox.Show("Table created and inserted into database successfully!");
                }
                con.Close();
            }

        }

        //Get Table name as it is in xml file!

        public string GetTableName(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            string TableName = fileInfo.Name.Replace(fileInfo.Extension, "");

            return TableName;
        }

        //Table creation queryy!

        public string CreateTableQuery(DataTable table)
        {
            string sqlCommand = "CREATE TABLE " + table.TableName + "(";
            progressBar.Maximum = table.Columns.Count;
            progressBar.Value = 0;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                sqlCommand += "[" + table.Columns[i].ColumnName + "]";
                string columnType = table.Columns[i].DataType.ToString();
                switch (columnType)
                {
                    case "System.Int32":
                        sqlCommand += " int ";
                        break;
                    case "System.Int64":
                        sqlCommand += " bigint ";
                        break;
                    case "System.Int16":
                        sqlCommand += " smallint";
                        break;
                    case "System.Byte":
                        sqlCommand += " tinyint";
                        break;
                    case "System.Decimal":
                        sqlCommand += " decimal ";
                        break;
                    case "System.DateTime":
                        sqlCommand += " datetime ";
                        break;
                    case "System.String":
                    default:
                        sqlCommand += string.Format(" nvarchar({0}) ", table.Columns[i].MaxLength == -1 ? "max" : table.Columns[i].MaxLength.ToString());
                        break;
                }
                if (table.Columns[i].AutoIncrement)
                    sqlCommand += " IDENTITY(" + table.Columns[i].AutoIncrementSeed.ToString() + "," + table.Columns[i].AutoIncrementStep.ToString() + ") ";
                if (!table.Columns[i].AllowDBNull)
                    sqlCommand += " NOT NULL ";
                sqlCommand += ",";

                InProgress();
            }
            return sqlCommand.Substring(0, sqlCommand.Length - 1) + "\n)";
        }

        // Converting Xml File To Database
        public DataTable CreateDataTableXML(string XmlFile)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(XmlFile);

            DataTable dataTable = new DataTable();

            try
            {
                dataTable.TableName = GetTableName(XmlFile);
                XmlNode node = doc.DocumentElement.ChildNodes.Cast<XmlNode>().ToList()[0];
                progressBar.Maximum = node.ChildNodes.Count;
                progressBar.Value = 0;
                foreach (XmlNode columna in node.ChildNodes)
                {
                    dataTable.Columns.Add(columna.Name, typeof(String));
                    InProgress();
                }

                XmlNode xNode = doc.DocumentElement;
                progressBar.Maximum = node.ChildNodes.Count;
                progressBar.Value = 0;
                foreach (XmlNode Node in xNode.ChildNodes)
                {
                    List<string> xmlList = Node.ChildNodes.Cast<XmlNode>().ToList().Select(i => i.InnerText).ToList();
                    dataTable.Rows.Add(xmlList.ToArray());
                    InProgress();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dataTable;
        }

        //Display progress in progressbar

        public void InProgress()
        {
            if (progressBar.Value < progressBar.Maximum)
            {
                progressBar.Value++;
                int percent = (int)(((double)progressBar.Value / (double)progressBar.Maximum) * 100);
                progressBar.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(progressBar.Width / 2 - 10, progressBar.Height / 2 - 7));

                Application.DoEvents();
            }
        }
    }
}
