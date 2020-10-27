namespace UI.TOOLS.SETUP
{
    partial class FormSetup
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem01 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem05 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem06 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem02 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem04 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.panelFunction = new System.Windows.Forms.Panel();
            this.treeViewFunction = new System.Windows.Forms.TreeView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.panelMessage = new System.Windows.Forms.Panel();
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.toolStripMenuItem21 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem22 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem23 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.panelFunction.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem01,
            this.toolStripMenuItem11,
            this.toolStripMenuItem21});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(778, 32);
            this.menuStrip.TabIndex = 0;
            // 
            // toolStripMenuItem01
            // 
            this.toolStripMenuItem01.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem05,
            this.toolStripMenuItem06,
            this.toolStripMenuItem02,
            this.toolStripMenuItem04});
            this.toolStripMenuItem01.Name = "toolStripMenuItem01";
            this.toolStripMenuItem01.Size = new System.Drawing.Size(58, 28);
            this.toolStripMenuItem01.Text = "安装";
            // 
            // toolStripMenuItem05
            // 
            this.toolStripMenuItem05.Name = "toolStripMenuItem05";
            this.toolStripMenuItem05.Size = new System.Drawing.Size(252, 30);
            this.toolStripMenuItem05.Text = "新建数据库";
            this.toolStripMenuItem05.Click += new System.EventHandler(this.toolStripMenuItem05_Click);
            // 
            // toolStripMenuItem06
            // 
            this.toolStripMenuItem06.Name = "toolStripMenuItem06";
            this.toolStripMenuItem06.Size = new System.Drawing.Size(252, 30);
            this.toolStripMenuItem06.Text = "创建系统表";
            this.toolStripMenuItem06.Click += new System.EventHandler(this.toolStripMenuItem06_Click);
            // 
            // toolStripMenuItem02
            // 
            this.toolStripMenuItem02.Name = "toolStripMenuItem02";
            this.toolStripMenuItem02.Size = new System.Drawing.Size(252, 30);
            this.toolStripMenuItem02.Text = "一键配置";
            this.toolStripMenuItem02.Click += new System.EventHandler(this.toolStripMenuItem02_Click);
            // 
            // toolStripMenuItem04
            // 
            this.toolStripMenuItem04.Name = "toolStripMenuItem04";
            this.toolStripMenuItem04.Size = new System.Drawing.Size(252, 30);
            this.toolStripMenuItem04.Text = "一键安装";
            this.toolStripMenuItem04.Click += new System.EventHandler(this.toolStripMenuItem04_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem12,
            this.toolStripMenuItem14});
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(58, 28);
            this.toolStripMenuItem11.Text = "刷新";
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(252, 30);
            this.toolStripMenuItem12.Text = "刷新管理员权限";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.toolStripMenuItem12_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(252, 30);
            this.toolStripMenuItem14.Text = "刷新数据模型错误";
            this.toolStripMenuItem14.Click += new System.EventHandler(this.toolStripMenuItem14_Click);
            // 
            // panelFunction
            // 
            this.panelFunction.Controls.Add(this.treeViewFunction);
            this.panelFunction.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFunction.Location = new System.Drawing.Point(0, 32);
            this.panelFunction.Name = "panelFunction";
            this.panelFunction.Size = new System.Drawing.Size(252, 512);
            this.panelFunction.TabIndex = 1;
            // 
            // treeViewFunction
            // 
            this.treeViewFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFunction.FullRowSelect = true;
            this.treeViewFunction.Location = new System.Drawing.Point(0, 0);
            this.treeViewFunction.Name = "treeViewFunction";
            this.treeViewFunction.Size = new System.Drawing.Size(252, 512);
            this.treeViewFunction.TabIndex = 0;
            this.treeViewFunction.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFunction_AfterCheck);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(252, 514);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(526, 30);
            this.statusStrip.TabIndex = 2;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(300, 24);
            this.toolStripProgressBar.Step = 1;
            // 
            // panelMessage
            // 
            this.panelMessage.Controls.Add(this.richTextBoxMessage);
            this.panelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMessage.Location = new System.Drawing.Point(252, 32);
            this.panelMessage.Name = "panelMessage";
            this.panelMessage.Size = new System.Drawing.Size(526, 482);
            this.panelMessage.TabIndex = 3;
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxMessage.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.Size = new System.Drawing.Size(526, 482);
            this.richTextBoxMessage.TabIndex = 0;
            this.richTextBoxMessage.Text = "";
            // 
            // toolStripMenuItem21
            // 
            this.toolStripMenuItem21.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem22,
            this.toolStripMenuItem23});
            this.toolStripMenuItem21.Name = "toolStripMenuItem21";
            this.toolStripMenuItem21.Size = new System.Drawing.Size(58, 28);
            this.toolStripMenuItem21.Text = "卸载";
            // 
            // toolStripMenuItem22
            // 
            this.toolStripMenuItem22.Name = "toolStripMenuItem22";
            this.toolStripMenuItem22.Size = new System.Drawing.Size(252, 30);
            this.toolStripMenuItem22.Text = "功能卸载";
            this.toolStripMenuItem22.Click += new System.EventHandler(this.toolStripMenuItem22_Click);
            // 
            // toolStripMenuItem23
            // 
            this.toolStripMenuItem23.Name = "toolStripMenuItem23";
            this.toolStripMenuItem23.Size = new System.Drawing.Size(252, 30);
            this.toolStripMenuItem23.Text = "数据结构删除";
            this.toolStripMenuItem23.Click += new System.EventHandler(this.toolStripMenuItem23_Click);
            // 
            // FormSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 544);
            this.Controls.Add(this.panelMessage);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panelFunction);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LES.系统安装";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelFunction.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelMessage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem01;
        private System.Windows.Forms.Panel panelFunction;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Panel panelMessage;
        private System.Windows.Forms.TreeView treeViewFunction;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem04;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem05;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem06;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem02;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem21;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem22;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem23;
    }
}

