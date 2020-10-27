namespace UI.Client.Templete
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.PlantName = new System.Windows.Forms.Label();
            this.cmbPlant = new System.Windows.Forms.ComboBox();
            this.PlantLoginName = new System.Windows.Forms.Label();
            this.PlantPassWord = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cbRemPass = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 69);
            this.panel1.TabIndex = 0;
            // 
            // PlantName
            // 
            this.PlantName.AutoSize = true;
            this.PlantName.BackColor = System.Drawing.Color.Transparent;
            this.PlantName.Location = new System.Drawing.Point(53, 85);
            this.PlantName.Name = "PlantName";
            this.PlantName.Size = new System.Drawing.Size(29, 12);
            this.PlantName.TabIndex = 1;
            this.PlantName.Text = "工厂";
            this.PlantName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbPlant
            // 
            this.cmbPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlant.FormattingEnabled = true;
            this.cmbPlant.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmbPlant.Location = new System.Drawing.Point(55, 100);
            this.cmbPlant.Name = "cmbPlant";
            this.cmbPlant.Size = new System.Drawing.Size(229, 20);
            this.cmbPlant.TabIndex = 2;
            // 
            // PlantLoginName
            // 
            this.PlantLoginName.AutoSize = true;
            this.PlantLoginName.BackColor = System.Drawing.Color.Transparent;
            this.PlantLoginName.Location = new System.Drawing.Point(53, 123);
            this.PlantLoginName.Name = "PlantLoginName";
            this.PlantLoginName.Size = new System.Drawing.Size(41, 12);
            this.PlantLoginName.TabIndex = 3;
            this.PlantLoginName.Text = "用户名";
            this.PlantLoginName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlantPassWord
            // 
            this.PlantPassWord.AutoSize = true;
            this.PlantPassWord.BackColor = System.Drawing.Color.Transparent;
            this.PlantPassWord.Location = new System.Drawing.Point(53, 162);
            this.PlantPassWord.Name = "PlantPassWord";
            this.PlantPassWord.Size = new System.Drawing.Size(29, 12);
            this.PlantPassWord.TabIndex = 4;
            this.PlantPassWord.Text = "密码";
            this.PlantPassWord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(55, 138);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(229, 21);
            this.txtUserName.TabIndex = 5;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(55, 177);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Size = new System.Drawing.Size(229, 21);
            this.txtPassWord.TabIndex = 6;
            this.txtPassWord.UseSystemPasswordChar = true;
            this.txtPassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassWord_KeyDown);
            // 
            // BtnLogin
            // 
            this.BtnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnLogin.BackgroundImage")));
            this.BtnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLogin.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnLogin.ForeColor = System.Drawing.Color.White;
            this.BtnLogin.Location = new System.Drawing.Point(55, 228);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(100, 50);
            this.BtnLogin.TabIndex = 9;
            this.BtnLogin.Text = "登  录";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(184, 228);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 50);
            this.button2.TabIndex = 10;
            this.button2.Text = "取  消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbRemPass
            // 
            this.cbRemPass.AutoSize = true;
            this.cbRemPass.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRemPass.Location = new System.Drawing.Point(55, 205);
            this.cbRemPass.Name = "cbRemPass";
            this.cbRemPass.Size = new System.Drawing.Size(75, 21);
            this.cbRemPass.TabIndex = 11;
            this.cbRemPass.Text = "记住密码";
            this.cbRemPass.UseVisualStyleBackColor = true;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(331, 294);
            this.ControlBox = false;
            this.Controls.Add(this.cbRemPass);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.PlantPassWord);
            this.Controls.Add(this.PlantLoginName);
            this.Controls.Add(this.cmbPlant);
            this.Controls.Add(this.PlantName);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLogin_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label PlantName;
        private System.Windows.Forms.ComboBox cmbPlant;
        internal System.Windows.Forms.Label PlantLoginName;
        internal System.Windows.Forms.Label PlantPassWord;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox cbRemPass;
    }
}