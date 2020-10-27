namespace UI.Client.Templete
{
    partial class FrmTemplete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTemplete));
            this.BtnPanel = new System.Windows.Forms.Panel();
            this.PalAction = new System.Windows.Forms.Panel();
            this.gbClose = new System.Windows.Forms.GroupBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.BtnPanel.SuspendLayout();
            this.gbClose.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnPanel
            // 
            this.BtnPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnPanel.BackgroundImage")));
            this.BtnPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnPanel.Controls.Add(this.PalAction);
            this.BtnPanel.Controls.Add(this.gbClose);
            this.BtnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnPanel.Location = new System.Drawing.Point(0, 281);
            this.BtnPanel.Name = "BtnPanel";
            this.BtnPanel.Size = new System.Drawing.Size(543, 100);
            this.BtnPanel.TabIndex = 10;
            // 
            // PalAction
            // 
            this.PalAction.BackColor = System.Drawing.Color.Transparent;
            this.PalAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PalAction.Location = new System.Drawing.Point(0, 0);
            this.PalAction.Name = "PalAction";
            this.PalAction.Size = new System.Drawing.Size(433, 100);
            this.PalAction.TabIndex = 1;
            // 
            // gbClose
            // 
            this.gbClose.BackColor = System.Drawing.Color.Transparent;
            this.gbClose.Controls.Add(this.BtnClose);
            this.gbClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbClose.Location = new System.Drawing.Point(433, 0);
            this.gbClose.Name = "gbClose";
            this.gbClose.Size = new System.Drawing.Size(110, 100);
            this.gbClose.TabIndex = 0;
            this.gbClose.TabStop = false;
            this.gbClose.Text = "F12";
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnClose.BackgroundImage")));
            this.BtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnClose.ForeColor = System.Drawing.Color.White;
            this.BtnClose.Location = new System.Drawing.Point(3, 17);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(104, 80);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.Text = "关闭";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // pnlMessage
            // 
            this.pnlMessage.Controls.Add(this.lblMessage);
            this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMessage.Location = new System.Drawing.Point(0, 231);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Size = new System.Drawing.Size(543, 50);
            this.pnlMessage.TabIndex = 11;
            this.pnlMessage.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.Location = new System.Drawing.Point(0, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(543, 50);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "MESSAGE";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmTemplete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(543, 381);
            this.Controls.Add(this.pnlMessage);
            this.Controls.Add(this.BtnPanel);
            this.Name = "FrmTemplete";
            this.Text = "Templete";
            this.BtnPanel.ResumeLayout(false);
            this.gbClose.ResumeLayout(false);
            this.pnlMessage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BtnPanel;
        private System.Windows.Forms.Panel PalAction;
        private System.Windows.Forms.GroupBox gbClose;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Panel pnlMessage;
        private System.Windows.Forms.Label lblMessage;
    }
}