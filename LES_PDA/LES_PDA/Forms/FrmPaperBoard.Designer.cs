namespace LES_PDA.Forms
{
    partial class FrmPaperBoard
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPaperBoard));
            this.pic_close = new System.Windows.Forms.PictureBox();
            this.pic_title = new System.Windows.Forms.PictureBox();
            this.lbl_KbCard_No = new System.Windows.Forms.Label();
            this.txt_KBCardNo = new System.Windows.Forms.TextBox();
            this.DG_List = new System.Windows.Forms.DataGrid();
            this.pic_submit = new System.Windows.Forms.PictureBox();
            this.pic_delete = new System.Windows.Forms.PictureBox();
            this.pic_clear = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // pic_close
            // 
            this.pic_close.BackColor = System.Drawing.Color.Transparent;
            this.pic_close.Image = ((System.Drawing.Image)(resources.GetObject("pic_close.Image")));
            this.pic_close.Location = new System.Drawing.Point(262, 0);
            this.pic_close.Name = "pic_close";
            this.pic_close.Size = new System.Drawing.Size(48, 35);
            this.pic_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // pic_title
            // 
            this.pic_title.Image = ((System.Drawing.Image)(resources.GetObject("pic_title.Image")));
            this.pic_title.Location = new System.Drawing.Point(0, 0);
            this.pic_title.Name = "pic_title";
            this.pic_title.Size = new System.Drawing.Size(263, 35);
            // 
            // lbl_KbCard_No
            // 
            this.lbl_KbCard_No.Location = new System.Drawing.Point(4, 47);
            this.lbl_KbCard_No.Name = "lbl_KbCard_No";
            this.lbl_KbCard_No.Size = new System.Drawing.Size(72, 20);
            this.lbl_KbCard_No.Text = "看板卡号:";
            // 
            // txt_KBCardNo
            // 
            this.txt_KBCardNo.Location = new System.Drawing.Point(76, 45);
            this.txt_KBCardNo.Name = "txt_KBCardNo";
            this.txt_KBCardNo.Size = new System.Drawing.Size(225, 23);
            this.txt_KBCardNo.TabIndex = 5;
            // 
            // DG_List
            // 
            this.DG_List.BackgroundColor = System.Drawing.Color.White;
            this.DG_List.GridLineColor = System.Drawing.Color.Black;
            this.DG_List.Location = new System.Drawing.Point(9, 82);
            this.DG_List.Name = "DG_List";
            this.DG_List.Size = new System.Drawing.Size(292, 158);
            this.DG_List.TabIndex = 6;
            // 
            // pic_submit
            // 
            this.pic_submit.Image = ((System.Drawing.Image)(resources.GetObject("pic_submit.Image")));
            this.pic_submit.Location = new System.Drawing.Point(19, 258);
            this.pic_submit.Name = "pic_submit";
            this.pic_submit.Size = new System.Drawing.Size(73, 26);
            this.pic_submit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_submit.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pic_delete
            // 
            this.pic_delete.Image = ((System.Drawing.Image)(resources.GetObject("pic_delete.Image")));
            this.pic_delete.Location = new System.Drawing.Point(118, 258);
            this.pic_delete.Name = "pic_delete";
            this.pic_delete.Size = new System.Drawing.Size(73, 26);
            this.pic_delete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // pic_clear
            // 
            this.pic_clear.Image = ((System.Drawing.Image)(resources.GetObject("pic_clear.Image")));
            this.pic_clear.Location = new System.Drawing.Point(220, 258);
            this.pic_clear.Name = "pic_clear";
            this.pic_clear.Size = new System.Drawing.Size(73, 26);
            this.pic_clear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // FrmPaperBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(310, 295);
            this.ControlBox = false;
            this.Controls.Add(this.pic_clear);
            this.Controls.Add(this.pic_delete);
            this.Controls.Add(this.pic_submit);
            this.Controls.Add(this.DG_List);
            this.Controls.Add(this.txt_KBCardNo);
            this.Controls.Add(this.lbl_KbCard_No);
            this.Controls.Add(this.pic_close);
            this.Controls.Add(this.pic_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPaperBoard";
            this.Text = "纸质看板扫描";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_close;
        private System.Windows.Forms.PictureBox pic_title;
        private System.Windows.Forms.Label lbl_KbCard_No;
        private System.Windows.Forms.TextBox txt_KBCardNo;
        private System.Windows.Forms.DataGrid DG_List;
        private System.Windows.Forms.PictureBox pic_submit;
        private System.Windows.Forms.PictureBox pic_delete;
        private System.Windows.Forms.PictureBox pic_clear;

    }
}