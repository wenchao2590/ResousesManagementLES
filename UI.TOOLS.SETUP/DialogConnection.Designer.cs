namespace UI.TOOLS.SETUP
{
    partial class DialogConnection
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
            this.txtInitialCatalog = new System.Windows.Forms.TextBox();
            this.lblInitialCatalog = new System.Windows.Forms.Label();
            this.lblDataSource = new System.Windows.Forms.Label();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnYes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtInitialCatalog
            // 
            this.txtInitialCatalog.Location = new System.Drawing.Point(197, 15);
            this.txtInitialCatalog.Name = "txtInitialCatalog";
            this.txtInitialCatalog.Size = new System.Drawing.Size(298, 26);
            this.txtInitialCatalog.TabIndex = 0;
            // 
            // lblInitialCatalog
            // 
            this.lblInitialCatalog.AutoSize = true;
            this.lblInitialCatalog.Location = new System.Drawing.Point(12, 18);
            this.lblInitialCatalog.Name = "lblInitialCatalog";
            this.lblInitialCatalog.Size = new System.Drawing.Size(169, 20);
            this.lblInitialCatalog.TabIndex = 1;
            this.lblInitialCatalog.Text = "InitialCatalog,数据库名";
            // 
            // lblDataSource
            // 
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Location = new System.Drawing.Point(12, 50);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(163, 20);
            this.lblDataSource.TabIndex = 3;
            this.lblDataSource.Text = "DataSource,连接地址";
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(197, 47);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(298, 26);
            this.txtDataSource.TabIndex = 2;
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Location = new System.Drawing.Point(12, 82);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(112, 20);
            this.lblUserID.TabIndex = 5;
            this.lblUserID.Text = "UserID,用户名";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(197, 79);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(298, 26);
            this.txtUserID.TabIndex = 4;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 114);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(114, 20);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password,密码";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(197, 111);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(298, 26);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnYes
            // 
            this.btnYes.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnYes.Location = new System.Drawing.Point(363, 145);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(132, 38);
            this.btnYes.TabIndex = 8;
            this.btnYes.Text = "确定";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // DialogConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 193);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblUserID);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.lblDataSource);
            this.Controls.Add(this.txtDataSource);
            this.Controls.Add(this.lblInitialCatalog);
            this.Controls.Add(this.txtInitialCatalog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据库连接配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblInitialCatalog;
        private System.Windows.Forms.Label lblDataSource;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnYes;
        public System.Windows.Forms.TextBox txtInitialCatalog;
        public System.Windows.Forms.TextBox txtDataSource;
        public System.Windows.Forms.TextBox txtUserID;
        public System.Windows.Forms.TextBox txtPassword;
    }
}