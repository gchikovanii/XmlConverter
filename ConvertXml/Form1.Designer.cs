namespace ConvertXml
{
    partial class XmlConverterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeViewTb = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.browseBtn = new System.Windows.Forms.Button();
            this.pathTb = new System.Windows.Forms.TextBox();
            this.importBtn = new System.Windows.Forms.Button();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // treeViewTb
            // 
            this.treeViewTb.Location = new System.Drawing.Point(6, 129);
            this.treeViewTb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.treeViewTb.Name = "treeViewTb";
            this.treeViewTb.Size = new System.Drawing.Size(454, 374);
            this.treeViewTb.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tree";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 56);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(358, 23);
            this.progressBar.TabIndex = 11;
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(372, 11);
            this.browseBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(88, 27);
            this.browseBtn.TabIndex = 10;
            this.browseBtn.Text = "Browse..";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // pathTb
            // 
            this.pathTb.Location = new System.Drawing.Point(6, 16);
            this.pathTb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pathTb.Name = "pathTb";
            this.pathTb.Size = new System.Drawing.Size(358, 21);
            this.pathTb.TabIndex = 9;
            // 
            // importBtn
            // 
            this.importBtn.Location = new System.Drawing.Point(372, 53);
            this.importBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.importBtn.Name = "importBtn";
            this.importBtn.Size = new System.Drawing.Size(88, 28);
            this.importBtn.TabIndex = 8;
            this.importBtn.Text = "Import";
            this.importBtn.UseVisualStyleBackColor = true;
            this.importBtn.Click += new System.EventHandler(this.importBtn_Click);
            // 
            // OFD
            // 
            this.OFD.FileName = "openFileDialog";
            // 
            // XmlConverterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 506);
            this.Controls.Add(this.treeViewTb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.pathTb);
            this.Controls.Add(this.importBtn);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "XmlConverterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XmlConverter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.TextBox pathTb;
        private System.Windows.Forms.Button importBtn;
        private System.Windows.Forms.OpenFileDialog OFD;
    }
}

