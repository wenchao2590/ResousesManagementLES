namespace LES_PDA
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pic_close = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageSize = new System.Drawing.Size(100, 45);
            this.imageList1.Images.Clear();
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource6"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource7"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource8"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource9"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource10"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource11"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource12"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource13"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource14"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource15"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource16"))));
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(263, 35);
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(310, 295);
            this.ControlBox = false;
            this.Controls.Add(this.pic_close);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "菜单选项";
            this.Load += new System.EventHandler(this.Main_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pic_close;


    }
}