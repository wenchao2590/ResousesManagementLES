namespace UI.TOOLS.DATAJOIN
{
    partial class DialogTextbox
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
            this.txtTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtTextbox
            // 
            this.txtTextbox.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.875F, System.Drawing.FontStyle.Bold);
            this.txtTextbox.Location = new System.Drawing.Point(12, 12);
            this.txtTextbox.Name = "txtTextbox";
            this.txtTextbox.Size = new System.Drawing.Size(300, 44);
            this.txtTextbox.TabIndex = 0;
            this.txtTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTextbox_KeyPress);
            // 
            // DialogTextbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 69);
            this.Controls.Add(this.txtTextbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogTextbox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTextbox;
    }
}