namespace UI.Client
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabWorkCell = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LabProdLine = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LabEmployeeName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LabRole = new System.Windows.Forms.Label();
            this.CmbRoleList = new System.Windows.Forms.ComboBox();
            this.LabMessage = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PalMenuList = new System.Windows.Forms.Panel();
            this.BtnDown = new System.Windows.Forms.Button();
            this.pnlDown = new System.Windows.Forms.Panel();
            this.BtnCellChoose = new System.Windows.Forms.Button();
            this.BtnOn = new System.Windows.Forms.Button();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.TabConMain = new System.Windows.Forms.TabControl();
            this.tpHome = new System.Windows.Forms.TabPage();
            this.TopPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlDown.SuspendLayout();
            this.TabConMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopPanel.BackgroundImage")));
            this.TopPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TopPanel.Controls.Add(this.panel1);
            this.TopPanel.Controls.Add(this.LabMessage);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(862, 75);
            this.TopPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.LabWorkCell);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.LabProdLine);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.LabEmployeeName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.LabRole);
            this.panel1.Controls.Add(this.CmbRoleList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(660, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 73);
            this.panel1.TabIndex = 3;
            // 
            // LabWorkCell
            // 
            this.LabWorkCell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabWorkCell.AutoSize = true;
            this.LabWorkCell.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabWorkCell.ForeColor = System.Drawing.Color.White;
            this.LabWorkCell.Location = new System.Drawing.Point(62, 18);
            this.LabWorkCell.Name = "LabWorkCell";
            this.LabWorkCell.Size = new System.Drawing.Size(43, 17);
            this.LabWorkCell.TabIndex = 16;
            this.LabWorkCell.Text = "label4";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "当前工位";
            // 
            // LabProdLine
            // 
            this.LabProdLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabProdLine.AutoSize = true;
            this.LabProdLine.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabProdLine.ForeColor = System.Drawing.Color.White;
            this.LabProdLine.Location = new System.Drawing.Point(62, 2);
            this.LabProdLine.Name = "LabProdLine";
            this.LabProdLine.Size = new System.Drawing.Size(43, 17);
            this.LabProdLine.TabIndex = 14;
            this.LabProdLine.Text = "label4";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "当前产线";
            // 
            // LabEmployeeName
            // 
            this.LabEmployeeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabEmployeeName.AutoSize = true;
            this.LabEmployeeName.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabEmployeeName.ForeColor = System.Drawing.Color.White;
            this.LabEmployeeName.Location = new System.Drawing.Point(62, 34);
            this.LabEmployeeName.Name = "LabEmployeeName";
            this.LabEmployeeName.Size = new System.Drawing.Size(43, 17);
            this.LabEmployeeName.TabIndex = 7;
            this.LabEmployeeName.Text = "label4";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "用  户 名";
            // 
            // LabRole
            // 
            this.LabRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabRole.AutoSize = true;
            this.LabRole.BackColor = System.Drawing.Color.Transparent;
            this.LabRole.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabRole.ForeColor = System.Drawing.Color.White;
            this.LabRole.Location = new System.Drawing.Point(3, 52);
            this.LabRole.Name = "LabRole";
            this.LabRole.Size = new System.Drawing.Size(56, 17);
            this.LabRole.TabIndex = 3;
            this.LabRole.Text = "当前角色";
            // 
            // CmbRoleList
            // 
            this.CmbRoleList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbRoleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbRoleList.FormattingEnabled = true;
            this.CmbRoleList.Location = new System.Drawing.Point(62, 51);
            this.CmbRoleList.Name = "CmbRoleList";
            this.CmbRoleList.Size = new System.Drawing.Size(135, 20);
            this.CmbRoleList.TabIndex = 2;
            this.CmbRoleList.SelectionChangeCommitted += new System.EventHandler(this.CmbRoleList_SelectionChangeCommitted);
            // 
            // LabMessage
            // 
            this.LabMessage.AutoSize = true;
            this.LabMessage.BackColor = System.Drawing.Color.Transparent;
            this.LabMessage.Font = new System.Drawing.Font("SimSun", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabMessage.ForeColor = System.Drawing.Color.Blue;
            this.LabMessage.Location = new System.Drawing.Point(240, 21);
            this.LabMessage.Name = "LabMessage";
            this.LabMessage.Size = new System.Drawing.Size(0, 19);
            this.LabMessage.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(0, 60);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(141, 60);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "退出登陆";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitter1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 75);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(862, 10);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.PalMenuList);
            this.panel3.Controls.Add(this.BtnDown);
            this.panel3.Controls.Add(this.pnlDown);
            this.panel3.Controls.Add(this.BtnOn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 85);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(141, 468);
            this.panel3.TabIndex = 4;
            // 
            // PalMenuList
            // 
            this.PalMenuList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PalMenuList.Location = new System.Drawing.Point(0, 60);
            this.PalMenuList.Name = "PalMenuList";
            this.PalMenuList.Size = new System.Drawing.Size(141, 226);
            this.PalMenuList.TabIndex = 10;
            // 
            // BtnDown
            // 
            this.BtnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnDown.BackgroundImage")));
            this.BtnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDown.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnDown.ForeColor = System.Drawing.Color.White;
            this.BtnDown.Location = new System.Drawing.Point(0, 286);
            this.BtnDown.Name = "BtnDown";
            this.BtnDown.Size = new System.Drawing.Size(141, 60);
            this.BtnDown.TabIndex = 9;
            this.BtnDown.Text = "下一页";
            this.BtnDown.UseVisualStyleBackColor = true;
            this.BtnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // pnlDown
            // 
            this.pnlDown.Controls.Add(this.btnExit);
            this.pnlDown.Controls.Add(this.BtnCellChoose);
            this.pnlDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDown.Location = new System.Drawing.Point(0, 346);
            this.pnlDown.Name = "pnlDown";
            this.pnlDown.Size = new System.Drawing.Size(141, 122);
            this.pnlDown.TabIndex = 4;
            // 
            // BtnCellChoose
            // 
            this.BtnCellChoose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnCellChoose.BackgroundImage")));
            this.BtnCellChoose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCellChoose.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnCellChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCellChoose.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnCellChoose.ForeColor = System.Drawing.Color.White;
            this.BtnCellChoose.Location = new System.Drawing.Point(0, 0);
            this.BtnCellChoose.Name = "BtnCellChoose";
            this.BtnCellChoose.Size = new System.Drawing.Size(141, 60);
            this.BtnCellChoose.TabIndex = 2;
            this.BtnCellChoose.Text = "选择工位";
            this.BtnCellChoose.UseVisualStyleBackColor = true;
            this.BtnCellChoose.Click += new System.EventHandler(this.BtnCellChoose_Click);
            // 
            // BtnOn
            // 
            this.BtnOn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnOn.BackgroundImage")));
            this.BtnOn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnOn.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOn.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnOn.ForeColor = System.Drawing.Color.White;
            this.BtnOn.Location = new System.Drawing.Point(0, 0);
            this.BtnOn.Name = "BtnOn";
            this.BtnOn.Size = new System.Drawing.Size(141, 60);
            this.BtnOn.TabIndex = 0;
            this.BtnOn.Text = "上一页";
            this.BtnOn.UseVisualStyleBackColor = true;
            this.BtnOn.Click += new System.EventHandler(this.BtnOn_Click);
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitter2.Location = new System.Drawing.Point(141, 85);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(11, 468);
            this.splitter2.TabIndex = 8;
            this.splitter2.TabStop = false;
            // 
            // TabConMain
            // 
            this.TabConMain.Controls.Add(this.tpHome);
            this.TabConMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabConMain.Location = new System.Drawing.Point(152, 85);
            this.TabConMain.Name = "TabConMain";
            this.TabConMain.SelectedIndex = 0;
            this.TabConMain.Size = new System.Drawing.Size(710, 468);
            this.TabConMain.TabIndex = 9;
            // 
            // tpHome
            // 
            this.tpHome.Location = new System.Drawing.Point(4, 22);
            this.tpHome.Name = "tpHome";
            this.tpHome.Size = new System.Drawing.Size(702, 442);
            this.tpHome.TabIndex = 0;
            this.tpHome.Text = "首页";
            this.tpHome.UseVisualStyleBackColor = true;
            this.tpHome.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TabConMain_MouseDoubleClick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(862, 553);
            this.Controls.Add(this.TabConMain);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.TopPanel);
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.Text = "制造执行系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.pnlDown.ResumeLayout(false);
            this.TabConMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BtnCellChoose;
        private System.Windows.Forms.Button BtnOn;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Label LabMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LabRole;
        private System.Windows.Forms.ComboBox CmbRoleList;
        private System.Windows.Forms.Label LabEmployeeName;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label LabWorkCell;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LabProdLine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlDown;
        private System.Windows.Forms.Panel PalMenuList;
        private System.Windows.Forms.Button BtnDown;
        private System.Windows.Forms.TabControl TabConMain;
        private System.Windows.Forms.TabPage tpHome;
    }
}

