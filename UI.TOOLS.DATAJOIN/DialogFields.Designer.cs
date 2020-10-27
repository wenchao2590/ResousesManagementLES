namespace UI.TOOLS.DATAJOIN
{
    partial class DialogFields
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
            this.dgFields = new System.Windows.Forms.DataGridView();
            this.lblTableName = new System.Windows.Forms.Label();
            this.btnYes = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.colFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescCn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescEn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgFields)).BeginInit();
            this.SuspendLayout();
            // 
            // dgFields
            // 
            this.dgFields.AllowUserToAddRows = false;
            this.dgFields.AllowUserToDeleteRows = false;
            this.dgFields.AllowUserToResizeColumns = false;
            this.dgFields.AllowUserToResizeRows = false;
            this.dgFields.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFieldName,
            this.colDescCn,
            this.colDescEn});
            this.dgFields.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgFields.Location = new System.Drawing.Point(0, 75);
            this.dgFields.Name = "dgFields";
            this.dgFields.RowHeadersVisible = false;
            this.dgFields.RowTemplate.Height = 33;
            this.dgFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFields.Size = new System.Drawing.Size(930, 417);
            this.dgFields.TabIndex = 0;
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(12, 9);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(152, 25);
            this.lblTableName.TabIndex = 1;
            this.lblTableName.Text = "TABLE_NAME";
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.SystemColors.Window;
            this.btnYes.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.Location = new System.Drawing.Point(818, 9);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(100, 60);
            this.btnYes.TabIndex = 2;
            this.btnYes.Text = "确定";
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.875F, System.Drawing.FontStyle.Bold);
            this.txtSearch.Location = new System.Drawing.Point(456, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(356, 44);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // colFieldName
            // 
            this.colFieldName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colFieldName.DataPropertyName = "fieldName";
            this.colFieldName.HeaderText = "FIELD_NAME";
            this.colFieldName.Name = "colFieldName";
            this.colFieldName.ReadOnly = true;
            this.colFieldName.Width = 189;
            // 
            // colDescCn
            // 
            this.colDescCn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescCn.DataPropertyName = "descCn";
            this.colDescCn.HeaderText = "DESC_CN";
            this.colDescCn.Name = "colDescCn";
            this.colDescCn.ReadOnly = true;
            // 
            // colDescEn
            // 
            this.colDescEn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDescEn.DataPropertyName = "descEn";
            this.colDescEn.HeaderText = "DESC_EN";
            this.colDescEn.Name = "colDescEn";
            this.colDescEn.ReadOnly = true;
            this.colDescEn.Width = 156;
            // 
            // DialogFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 492);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.lblTableName);
            this.Controls.Add(this.dgFields);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogFields";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择字段";
            this.Load += new System.EventHandler(this.DialogFields_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgFields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgFields;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescCn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescEn;
    }
}