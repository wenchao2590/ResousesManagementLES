namespace UI.TOOLS.FIELDEXTEND
{
    partial class FrmMain
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
            this.lblControlType = new System.Windows.Forms.Label();
            this.cbControlType = new System.Windows.Forms.ComboBox();
            this.lblExtend1 = new System.Windows.Forms.Label();
            this.lblExtend2 = new System.Windows.Forms.Label();
            this.lblExtend3 = new System.Windows.Forms.Label();
            this.txtExtend1 = new System.Windows.Forms.TextBox();
            this.txtExtend2 = new System.Windows.Forms.TextBox();
            this.txtExtend3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblControlType
            // 
            this.lblControlType.AutoSize = true;
            this.lblControlType.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlType.Location = new System.Drawing.Point(13, 13);
            this.lblControlType.Name = "lblControlType";
            this.lblControlType.Size = new System.Drawing.Size(123, 36);
            this.lblControlType.TabIndex = 0;
            this.lblControlType.Text = "控件类型";
            // 
            // cbControlType
            // 
            this.cbControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbControlType.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.14286F, System.Drawing.FontStyle.Bold);
            this.cbControlType.FormattingEnabled = true;
            this.cbControlType.Location = new System.Drawing.Point(160, 10);
            this.cbControlType.Name = "cbControlType";
            this.cbControlType.Size = new System.Drawing.Size(300, 42);
            this.cbControlType.TabIndex = 1;
            // 
            // lblExtend1
            // 
            this.lblExtend1.AutoSize = true;
            this.lblExtend1.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtend1.Location = new System.Drawing.Point(13, 75);
            this.lblExtend1.Name = "lblExtend1";
            this.lblExtend1.Size = new System.Drawing.Size(142, 36);
            this.lblExtend1.TabIndex = 2;
            this.lblExtend1.Text = "EXTEND1";
            // 
            // lblExtend2
            // 
            this.lblExtend2.AutoSize = true;
            this.lblExtend2.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtend2.Location = new System.Drawing.Point(12, 206);
            this.lblExtend2.Name = "lblExtend2";
            this.lblExtend2.Size = new System.Drawing.Size(142, 36);
            this.lblExtend2.TabIndex = 3;
            this.lblExtend2.Text = "EXTEND2";
            // 
            // lblExtend3
            // 
            this.lblExtend3.AutoSize = true;
            this.lblExtend3.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtend3.Location = new System.Drawing.Point(12, 358);
            this.lblExtend3.Name = "lblExtend3";
            this.lblExtend3.Size = new System.Drawing.Size(142, 36);
            this.lblExtend3.TabIndex = 4;
            this.lblExtend3.Text = "EXTEND3";
            // 
            // txtExtend1
            // 
            this.txtExtend1.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.14286F, System.Drawing.FontStyle.Bold);
            this.txtExtend1.Location = new System.Drawing.Point(18, 114);
            this.txtExtend1.Name = "txtExtend1";
            this.txtExtend1.Size = new System.Drawing.Size(968, 41);
            this.txtExtend1.TabIndex = 5;
            // 
            // txtExtend2
            // 
            this.txtExtend2.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.14286F, System.Drawing.FontStyle.Bold);
            this.txtExtend2.Location = new System.Drawing.Point(18, 254);
            this.txtExtend2.Name = "txtExtend2";
            this.txtExtend2.Size = new System.Drawing.Size(968, 41);
            this.txtExtend2.TabIndex = 6;
            // 
            // txtExtend3
            // 
            this.txtExtend3.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.14286F, System.Drawing.FontStyle.Bold);
            this.txtExtend3.Location = new System.Drawing.Point(19, 397);
            this.txtExtend3.Multiline = true;
            this.txtExtend3.Name = "txtExtend3";
            this.txtExtend3.Size = new System.Drawing.Size(967, 165);
            this.txtExtend3.TabIndex = 7;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 704);
            this.Controls.Add(this.txtExtend3);
            this.Controls.Add(this.txtExtend2);
            this.Controls.Add(this.txtExtend1);
            this.Controls.Add(this.lblExtend3);
            this.Controls.Add(this.lblExtend2);
            this.Controls.Add(this.lblExtend1);
            this.Controls.Add(this.cbControlType);
            this.Controls.Add(this.lblControlType);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FIELD_EXTEND";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblControlType;
        private System.Windows.Forms.ComboBox cbControlType;
        private System.Windows.Forms.Label lblExtend1;
        private System.Windows.Forms.Label lblExtend2;
        private System.Windows.Forms.Label lblExtend3;
        private System.Windows.Forms.TextBox txtExtend1;
        private System.Windows.Forms.TextBox txtExtend2;
        private System.Windows.Forms.TextBox txtExtend3;
    }
}

