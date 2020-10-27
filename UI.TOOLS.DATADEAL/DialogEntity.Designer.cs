namespace UI.TOOLS.DATADEAL
{
    partial class DialogEntity
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
            this.lblEntityName = new System.Windows.Forms.Label();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.txtTableNames = new System.Windows.Forms.TextBox();
            this.lblTableNames = new System.Windows.Forms.Label();
            this.lblEntityType = new System.Windows.Forms.Label();
            this.cmbEntityType = new System.Windows.Forms.ComboBox();
            this.txtDefaultSort = new System.Windows.Forms.TextBox();
            this.lblDefaultSort = new System.Windows.Forms.Label();
            this.lblExDefaultSort = new System.Windows.Forms.Label();
            this.txtParentField = new System.Windows.Forms.TextBox();
            this.lblParentField = new System.Windows.Forms.Label();
            this.lblExParentField = new System.Windows.Forms.Label();
            this.txtAuthConfig = new System.Windows.Forms.TextBox();
            this.lblAuthConfig = new System.Windows.Forms.Label();
            this.lblExAuthConfig = new System.Windows.Forms.Label();
            this.txtTabTitles = new System.Windows.Forms.TextBox();
            this.lblTabTitles = new System.Windows.Forms.Label();
            this.lblExTabTitles = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.lblComments = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblEntityName
            // 
            this.lblEntityName.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEntityName.Location = new System.Drawing.Point(10, 19);
            this.lblEntityName.Name = "lblEntityName";
            this.lblEntityName.Size = new System.Drawing.Size(100, 20);
            this.lblEntityName.TabIndex = 0;
            this.lblEntityName.Text = "数据模型名称";
            // 
            // txtEntityName
            // 
            this.txtEntityName.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtEntityName.Location = new System.Drawing.Point(120, 19);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(300, 27);
            this.txtEntityName.TabIndex = 1;
            // 
            // txtTableNames
            // 
            this.txtTableNames.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtTableNames.Location = new System.Drawing.Point(120, 89);
            this.txtTableNames.Name = "txtTableNames";
            this.txtTableNames.Size = new System.Drawing.Size(300, 27);
            this.txtTableNames.TabIndex = 3;
            // 
            // lblTableNames
            // 
            this.lblTableNames.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTableNames.Location = new System.Drawing.Point(10, 89);
            this.lblTableNames.Name = "lblTableNames";
            this.lblTableNames.Size = new System.Drawing.Size(100, 20);
            this.lblTableNames.TabIndex = 2;
            this.lblTableNames.Text = "设计表名";
            // 
            // lblEntityType
            // 
            this.lblEntityType.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEntityType.Location = new System.Drawing.Point(10, 122);
            this.lblEntityType.Name = "lblEntityType";
            this.lblEntityType.Size = new System.Drawing.Size(100, 20);
            this.lblEntityType.TabIndex = 4;
            this.lblEntityType.Text = "数据模型类型";
            // 
            // cmbEntityType
            // 
            this.cmbEntityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntityType.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.cmbEntityType.FormattingEnabled = true;
            this.cmbEntityType.Location = new System.Drawing.Point(120, 123);
            this.cmbEntityType.Name = "cmbEntityType";
            this.cmbEntityType.Size = new System.Drawing.Size(300, 27);
            this.cmbEntityType.TabIndex = 5;
            // 
            // txtDefaultSort
            // 
            this.txtDefaultSort.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtDefaultSort.Location = new System.Drawing.Point(120, 156);
            this.txtDefaultSort.Name = "txtDefaultSort";
            this.txtDefaultSort.Size = new System.Drawing.Size(650, 27);
            this.txtDefaultSort.TabIndex = 7;
            // 
            // lblDefaultSort
            // 
            this.lblDefaultSort.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDefaultSort.Location = new System.Drawing.Point(10, 156);
            this.lblDefaultSort.Name = "lblDefaultSort";
            this.lblDefaultSort.Size = new System.Drawing.Size(100, 20);
            this.lblDefaultSort.TabIndex = 6;
            this.lblDefaultSort.Text = "默认排序依据";
            // 
            // lblExDefaultSort
            // 
            this.lblExDefaultSort.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExDefaultSort.Location = new System.Drawing.Point(10, 189);
            this.lblExDefaultSort.Name = "lblExDefaultSort";
            this.lblExDefaultSort.Size = new System.Drawing.Size(760, 20);
            this.lblExDefaultSort.TabIndex = 8;
            this.lblExDefaultSort.Text = "示例：DISPLAY_ORDER-asc";
            // 
            // txtParentField
            // 
            this.txtParentField.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtParentField.Location = new System.Drawing.Point(120, 222);
            this.txtParentField.Name = "txtParentField";
            this.txtParentField.Size = new System.Drawing.Size(650, 27);
            this.txtParentField.TabIndex = 11;
            // 
            // lblParentField
            // 
            this.lblParentField.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblParentField.Location = new System.Drawing.Point(10, 222);
            this.lblParentField.Name = "lblParentField";
            this.lblParentField.Size = new System.Drawing.Size(100, 20);
            this.lblParentField.TabIndex = 10;
            this.lblParentField.Text = "模型配置";
            // 
            // lblExParentField
            // 
            this.lblExParentField.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExParentField.Location = new System.Drawing.Point(10, 255);
            this.lblExParentField.Name = "lblExParentField";
            this.lblExParentField.Size = new System.Drawing.Size(760, 60);
            this.lblExParentField.TabIndex = 12;
            this.lblExParentField.Text = "示例：Fid-SEARCH_FID-SearchModelCondition-TS_SYS_SEARCH_MODEL_CONDITION";
            // 
            // txtAuthConfig
            // 
            this.txtAuthConfig.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtAuthConfig.Location = new System.Drawing.Point(120, 321);
            this.txtAuthConfig.Name = "txtAuthConfig";
            this.txtAuthConfig.Size = new System.Drawing.Size(650, 27);
            this.txtAuthConfig.TabIndex = 17;
            // 
            // lblAuthConfig
            // 
            this.lblAuthConfig.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAuthConfig.Location = new System.Drawing.Point(10, 321);
            this.lblAuthConfig.Name = "lblAuthConfig";
            this.lblAuthConfig.Size = new System.Drawing.Size(100, 20);
            this.lblAuthConfig.TabIndex = 16;
            this.lblAuthConfig.Text = "数据权限配置";
            // 
            // lblExAuthConfig
            // 
            this.lblExAuthConfig.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExAuthConfig.Location = new System.Drawing.Point(10, 354);
            this.lblExAuthConfig.Name = "lblExAuthConfig";
            this.lblExAuthConfig.Size = new System.Drawing.Size(760, 60);
            this.lblExAuthConfig.TabIndex = 18;
            this.lblExAuthConfig.Text = "示例：";
            // 
            // txtTabTitles
            // 
            this.txtTabTitles.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtTabTitles.Location = new System.Drawing.Point(120, 420);
            this.txtTabTitles.Name = "txtTabTitles";
            this.txtTabTitles.Size = new System.Drawing.Size(650, 27);
            this.txtTabTitles.TabIndex = 23;
            // 
            // lblTabTitles
            // 
            this.lblTabTitles.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTabTitles.Location = new System.Drawing.Point(10, 420);
            this.lblTabTitles.Name = "lblTabTitles";
            this.lblTabTitles.Size = new System.Drawing.Size(100, 20);
            this.lblTabTitles.TabIndex = 22;
            this.lblTabTitles.Text = "选项卡配置";
            // 
            // lblExTabTitles
            // 
            this.lblExTabTitles.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExTabTitles.Location = new System.Drawing.Point(10, 453);
            this.lblExTabTitles.Name = "lblExTabTitles";
            this.lblExTabTitles.Size = new System.Drawing.Size(760, 20);
            this.lblExTabTitles.TabIndex = 24;
            this.lblExTabTitles.Text = "示例：BAS-基本-BAS|EXT-扩展-EXT，说明：代码-中文显示-英文显示|分割多个选项卡";
            // 
            // txtComments
            // 
            this.txtComments.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtComments.Location = new System.Drawing.Point(120, 479);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(650, 116);
            this.txtComments.TabIndex = 26;
            // 
            // lblComments
            // 
            this.lblComments.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblComments.Location = new System.Drawing.Point(10, 479);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(100, 20);
            this.lblComments.TabIndex = 25;
            this.lblComments.Text = "备注";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(570, 56);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 94);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtID.Location = new System.Drawing.Point(120, 54);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(300, 27);
            this.txtID.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 28;
            this.label1.Text = "数据模型ID";
            // 
            // DialogEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 643);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.lblExTabTitles);
            this.Controls.Add(this.txtTabTitles);
            this.Controls.Add(this.lblTabTitles);
            this.Controls.Add(this.lblExAuthConfig);
            this.Controls.Add(this.txtAuthConfig);
            this.Controls.Add(this.lblAuthConfig);
            this.Controls.Add(this.lblExParentField);
            this.Controls.Add(this.txtParentField);
            this.Controls.Add(this.lblParentField);
            this.Controls.Add(this.lblExDefaultSort);
            this.Controls.Add(this.txtDefaultSort);
            this.Controls.Add(this.lblDefaultSort);
            this.Controls.Add(this.cmbEntityType);
            this.Controls.Add(this.lblEntityType);
            this.Controls.Add(this.txtTableNames);
            this.Controls.Add(this.lblTableNames);
            this.Controls.Add(this.txtEntityName);
            this.Controls.Add(this.lblEntityName);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogEntity";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "实体属性更改";
            this.Load += new System.EventHandler(this.DialogEntity_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEntityName;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.TextBox txtTableNames;
        private System.Windows.Forms.Label lblTableNames;
        private System.Windows.Forms.Label lblEntityType;
        private System.Windows.Forms.ComboBox cmbEntityType;
        private System.Windows.Forms.TextBox txtDefaultSort;
        private System.Windows.Forms.Label lblExDefaultSort;
        private System.Windows.Forms.Label lblDefaultSort;
        private System.Windows.Forms.TextBox txtParentField;
        private System.Windows.Forms.Label lblParentField;
        private System.Windows.Forms.Label lblExParentField;
        private System.Windows.Forms.TextBox txtAuthConfig;
        private System.Windows.Forms.Label lblAuthConfig;
        private System.Windows.Forms.Label lblExAuthConfig;
        private System.Windows.Forms.TextBox txtTabTitles;
        private System.Windows.Forms.Label lblTabTitles;
        private System.Windows.Forms.Label lblExTabTitles;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label1;
    }
}