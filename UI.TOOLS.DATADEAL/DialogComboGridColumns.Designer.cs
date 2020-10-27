namespace UI.TOOLS.DATADEAL
{
    partial class DialogComboGridColumns
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
            this.txtField = new System.Windows.Forms.TextBox();
            this.lblField = new System.Windows.Forms.Label();
            this.txtTitleCn = new System.Windows.Forms.TextBox();
            this.lblTitleCn = new System.Windows.Forms.Label();
            this.txtTitleEn = new System.Windows.Forms.TextBox();
            this.lblTitleEn = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.btnAddColumn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtField
            // 
            this.txtField.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtField.Location = new System.Drawing.Point(140, 10);
            this.txtField.Name = "txtField";
            this.txtField.Size = new System.Drawing.Size(150, 27);
            this.txtField.TabIndex = 7;
            // 
            // lblField
            // 
            this.lblField.BackColor = System.Drawing.Color.White;
            this.lblField.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblField.ForeColor = System.Drawing.Color.Red;
            this.lblField.Location = new System.Drawing.Point(12, 13);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(120, 20);
            this.lblField.TabIndex = 6;
            this.lblField.Text = "对象属性";
            // 
            // txtTitleCn
            // 
            this.txtTitleCn.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtTitleCn.Location = new System.Drawing.Point(140, 43);
            this.txtTitleCn.Name = "txtTitleCn";
            this.txtTitleCn.Size = new System.Drawing.Size(150, 27);
            this.txtTitleCn.TabIndex = 9;
            // 
            // lblTitleCn
            // 
            this.lblTitleCn.BackColor = System.Drawing.Color.White;
            this.lblTitleCn.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleCn.ForeColor = System.Drawing.Color.Red;
            this.lblTitleCn.Location = new System.Drawing.Point(12, 46);
            this.lblTitleCn.Name = "lblTitleCn";
            this.lblTitleCn.Size = new System.Drawing.Size(120, 20);
            this.lblTitleCn.TabIndex = 8;
            this.lblTitleCn.Text = "中文列标题";
            // 
            // txtTitleEn
            // 
            this.txtTitleEn.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtTitleEn.Location = new System.Drawing.Point(140, 76);
            this.txtTitleEn.Name = "txtTitleEn";
            this.txtTitleEn.Size = new System.Drawing.Size(150, 27);
            this.txtTitleEn.TabIndex = 11;
            // 
            // lblTitleEn
            // 
            this.lblTitleEn.BackColor = System.Drawing.Color.White;
            this.lblTitleEn.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleEn.ForeColor = System.Drawing.Color.Black;
            this.lblTitleEn.Location = new System.Drawing.Point(12, 79);
            this.lblTitleEn.Name = "lblTitleEn";
            this.lblTitleEn.Size = new System.Drawing.Size(120, 20);
            this.lblTitleEn.TabIndex = 10;
            this.lblTitleEn.Text = "英文列标题";
            // 
            // txtWidth
            // 
            this.txtWidth.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtWidth.Location = new System.Drawing.Point(140, 109);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(150, 27);
            this.txtWidth.TabIndex = 13;
            // 
            // lblWidth
            // 
            this.lblWidth.BackColor = System.Drawing.Color.White;
            this.lblWidth.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWidth.ForeColor = System.Drawing.Color.Black;
            this.lblWidth.Location = new System.Drawing.Point(12, 112);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(120, 20);
            this.lblWidth.TabIndex = 12;
            this.lblWidth.Text = "列宽度";
            // 
            // btnAddColumn
            // 
            this.btnAddColumn.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddColumn.Location = new System.Drawing.Point(190, 142);
            this.btnAddColumn.Name = "btnAddColumn";
            this.btnAddColumn.Size = new System.Drawing.Size(100, 50);
            this.btnAddColumn.TabIndex = 39;
            this.btnAddColumn.Text = "添加列";
            this.btnAddColumn.UseVisualStyleBackColor = true;
            this.btnAddColumn.Click += new System.EventHandler(this.btnAddColumn_Click);
            // 
            // DialogComboGridColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(304, 201);
            this.Controls.Add(this.btnAddColumn);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.txtTitleEn);
            this.Controls.Add(this.lblTitleEn);
            this.Controls.Add(this.txtTitleCn);
            this.Controls.Add(this.lblTitleCn);
            this.Controls.Add(this.txtField);
            this.Controls.Add(this.lblField);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogComboGridColumns";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "COMBO.GRID.COLUMN配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtField;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.TextBox txtTitleCn;
        private System.Windows.Forms.Label lblTitleCn;
        private System.Windows.Forms.TextBox txtTitleEn;
        private System.Windows.Forms.Label lblTitleEn;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Button btnAddColumn;
    }
}