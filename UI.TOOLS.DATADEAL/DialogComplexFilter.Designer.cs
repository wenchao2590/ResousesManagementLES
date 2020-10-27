namespace UI.TOOLS.DATADEAL
{
    partial class DialogComplexFilter
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
            this.txtTableFieldName = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblTableFieldName = new System.Windows.Forms.Label();
            this.lblFieldValue = new System.Windows.Forms.Label();
            this.txtFieldValue = new System.Windows.Forms.TextBox();
            this.cmbControlType = new System.Windows.Forms.ComboBox();
            this.lblControlType = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.lblFindField = new System.Windows.Forms.Label();
            this.txtFindField = new System.Windows.Forms.TextBox();
            this.btnAddComplexFilter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTableFieldName
            // 
            this.txtTableFieldName.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtTableFieldName.Location = new System.Drawing.Point(170, 43);
            this.txtTableFieldName.Name = "txtTableFieldName";
            this.txtTableFieldName.Size = new System.Drawing.Size(282, 27);
            this.txtTableFieldName.TabIndex = 7;
            // 
            // lblType
            // 
            this.lblType.BackColor = System.Drawing.Color.White;
            this.lblType.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblType.ForeColor = System.Drawing.Color.Red;
            this.lblType.Location = new System.Drawing.Point(12, 13);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(150, 20);
            this.lblType.TabIndex = 6;
            this.lblType.Text = "条件类型[0]";
            this.lblType.DoubleClick += new System.EventHandler(this.lblTableFieldName_DoubleClick);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(170, 10);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(282, 27);
            this.cmbType.TabIndex = 24;
            // 
            // lblTableFieldName
            // 
            this.lblTableFieldName.BackColor = System.Drawing.Color.White;
            this.lblTableFieldName.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTableFieldName.ForeColor = System.Drawing.Color.Red;
            this.lblTableFieldName.Location = new System.Drawing.Point(12, 46);
            this.lblTableFieldName.Name = "lblTableFieldName";
            this.lblTableFieldName.Size = new System.Drawing.Size(150, 20);
            this.lblTableFieldName.TabIndex = 25;
            this.lblTableFieldName.Text = "字段[3.1][1.3][2.1]";
            this.lblTableFieldName.DoubleClick += new System.EventHandler(this.lblTableFieldName_DoubleClick);
            // 
            // lblFieldValue
            // 
            this.lblFieldValue.BackColor = System.Drawing.Color.White;
            this.lblFieldValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFieldValue.ForeColor = System.Drawing.Color.Black;
            this.lblFieldValue.Location = new System.Drawing.Point(12, 79);
            this.lblFieldValue.Name = "lblFieldValue";
            this.lblFieldValue.Size = new System.Drawing.Size(150, 20);
            this.lblFieldValue.TabIndex = 27;
            this.lblFieldValue.Text = "值[3.2]";
            this.lblFieldValue.DoubleClick += new System.EventHandler(this.lblTableFieldName_DoubleClick);
            // 
            // txtFieldValue
            // 
            this.txtFieldValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtFieldValue.Location = new System.Drawing.Point(170, 76);
            this.txtFieldValue.Name = "txtFieldValue";
            this.txtFieldValue.Size = new System.Drawing.Size(282, 27);
            this.txtFieldValue.TabIndex = 26;
            // 
            // cmbControlType
            // 
            this.cmbControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbControlType.FormattingEnabled = true;
            this.cmbControlType.Location = new System.Drawing.Point(170, 109);
            this.cmbControlType.Name = "cmbControlType";
            this.cmbControlType.Size = new System.Drawing.Size(282, 27);
            this.cmbControlType.TabIndex = 29;
            // 
            // lblControlType
            // 
            this.lblControlType.BackColor = System.Drawing.Color.White;
            this.lblControlType.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblControlType.ForeColor = System.Drawing.Color.Black;
            this.lblControlType.Location = new System.Drawing.Point(12, 113);
            this.lblControlType.Name = "lblControlType";
            this.lblControlType.Size = new System.Drawing.Size(150, 20);
            this.lblControlType.TabIndex = 28;
            this.lblControlType.Text = "控件类型[1.1]";
            this.lblControlType.DoubleClick += new System.EventHandler(this.lblTableFieldName_DoubleClick);
            // 
            // lblFieldName
            // 
            this.lblFieldName.BackColor = System.Drawing.Color.White;
            this.lblFieldName.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFieldName.ForeColor = System.Drawing.Color.Black;
            this.lblFieldName.Location = new System.Drawing.Point(12, 145);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(150, 20);
            this.lblFieldName.TabIndex = 31;
            this.lblFieldName.Text = "属性名[1.2][2.2]";
            this.lblFieldName.DoubleClick += new System.EventHandler(this.lblTableFieldName_DoubleClick);
            // 
            // txtFieldName
            // 
            this.txtFieldName.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtFieldName.Location = new System.Drawing.Point(170, 142);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(282, 27);
            this.txtFieldName.TabIndex = 30;
            // 
            // lblFindField
            // 
            this.lblFindField.BackColor = System.Drawing.Color.White;
            this.lblFindField.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFindField.ForeColor = System.Drawing.Color.Black;
            this.lblFindField.Location = new System.Drawing.Point(12, 178);
            this.lblFindField.Name = "lblFindField";
            this.lblFindField.Size = new System.Drawing.Size(150, 20);
            this.lblFindField.TabIndex = 33;
            this.lblFindField.Text = "属性A找属性B[1.4]";
            this.lblFindField.DoubleClick += new System.EventHandler(this.lblTableFieldName_DoubleClick);
            // 
            // txtFindField
            // 
            this.txtFindField.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtFindField.Location = new System.Drawing.Point(170, 175);
            this.txtFindField.Name = "txtFindField";
            this.txtFindField.Size = new System.Drawing.Size(282, 27);
            this.txtFindField.TabIndex = 32;
            // 
            // btnAddComplexFilter
            // 
            this.btnAddComplexFilter.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddComplexFilter.Location = new System.Drawing.Point(352, 219);
            this.btnAddComplexFilter.Name = "btnAddComplexFilter";
            this.btnAddComplexFilter.Size = new System.Drawing.Size(100, 50);
            this.btnAddComplexFilter.TabIndex = 39;
            this.btnAddComplexFilter.Text = "添加条件";
            this.btnAddComplexFilter.UseVisualStyleBackColor = true;
            this.btnAddComplexFilter.Click += new System.EventHandler(this.btnAddComplexFilter_Click);
            // 
            // DialogComplexFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this.btnAddComplexFilter);
            this.Controls.Add(this.lblFindField);
            this.Controls.Add(this.txtFindField);
            this.Controls.Add(this.lblFieldName);
            this.Controls.Add(this.txtFieldName);
            this.Controls.Add(this.cmbControlType);
            this.Controls.Add(this.lblControlType);
            this.Controls.Add(this.lblFieldValue);
            this.Controls.Add(this.txtFieldValue);
            this.Controls.Add(this.lblTableFieldName);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.txtTableFieldName);
            this.Controls.Add(this.lblType);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogComplexFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "COMBO复杂过滤条件";
            this.Load += new System.EventHandler(this.DialogComplexFilter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTableFieldName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblTableFieldName;
        private System.Windows.Forms.Label lblFieldValue;
        private System.Windows.Forms.TextBox txtFieldValue;
        private System.Windows.Forms.ComboBox cmbControlType;
        private System.Windows.Forms.Label lblControlType;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.Label lblFindField;
        private System.Windows.Forms.TextBox txtFindField;
        private System.Windows.Forms.Button btnAddComplexFilter;
    }
}