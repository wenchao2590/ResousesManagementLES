namespace UI.TOOLS.DATAJOIN
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
            this.dgTables = new System.Windows.Forms.DataGridView();
            this.btnMainTable = new System.Windows.Forms.Button();
            this.btnJoinTables = new System.Windows.Forms.Button();
            this.btnDelTable = new System.Windows.Forms.Button();
            this.dgSelectedField = new System.Windows.Forms.DataGridView();
            this.btnKeyField = new System.Windows.Forms.Button();
            this.btnFields = new System.Windows.Forms.Button();
            this.btnDelField = new System.Windows.Forms.Button();
            this.btnCreateDm = new System.Windows.Forms.Button();
            this.btnCreateDal = new System.Windows.Forms.Button();
            this.btnCreateBll = new System.Windows.Forms.Button();
            this.btnCreateEntity = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.colTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbNameSpace = new System.Windows.Forms.ComboBox();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.colTableName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescCn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSelectedField)).BeginInit();
            this.SuspendLayout();
            // 
            // dgTables
            // 
            this.dgTables.AllowUserToAddRows = false;
            this.dgTables.AllowUserToDeleteRows = false;
            this.dgTables.AllowUserToResizeColumns = false;
            this.dgTables.AllowUserToResizeRows = false;
            this.dgTables.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTableName});
            this.dgTables.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgTables.Location = new System.Drawing.Point(0, 0);
            this.dgTables.MultiSelect = false;
            this.dgTables.Name = "dgTables";
            this.dgTables.RowHeadersVisible = false;
            this.dgTables.RowTemplate.Height = 33;
            this.dgTables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTables.Size = new System.Drawing.Size(200, 737);
            this.dgTables.TabIndex = 0;
            this.dgTables.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgTables_DataBindingComplete);
            // 
            // btnMainTable
            // 
            this.btnMainTable.BackColor = System.Drawing.SystemColors.Window;
            this.btnMainTable.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainTable.Location = new System.Drawing.Point(206, 12);
            this.btnMainTable.Name = "btnMainTable";
            this.btnMainTable.Size = new System.Drawing.Size(180, 50);
            this.btnMainTable.TabIndex = 1;
            this.btnMainTable.Text = "选择主表";
            this.btnMainTable.UseVisualStyleBackColor = false;
            this.btnMainTable.Click += new System.EventHandler(this.btnMainTable_Click);
            // 
            // btnJoinTables
            // 
            this.btnJoinTables.BackColor = System.Drawing.SystemColors.Window;
            this.btnJoinTables.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoinTables.Location = new System.Drawing.Point(206, 68);
            this.btnJoinTables.Name = "btnJoinTables";
            this.btnJoinTables.Size = new System.Drawing.Size(180, 50);
            this.btnJoinTables.TabIndex = 2;
            this.btnJoinTables.Text = "选择关联表";
            this.btnJoinTables.UseVisualStyleBackColor = false;
            this.btnJoinTables.Click += new System.EventHandler(this.btnJoinTables_Click);
            // 
            // btnDelTable
            // 
            this.btnDelTable.BackColor = System.Drawing.SystemColors.Window;
            this.btnDelTable.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelTable.Location = new System.Drawing.Point(206, 124);
            this.btnDelTable.Name = "btnDelTable";
            this.btnDelTable.Size = new System.Drawing.Size(180, 50);
            this.btnDelTable.TabIndex = 3;
            this.btnDelTable.Text = "删除已选表";
            this.btnDelTable.UseVisualStyleBackColor = false;
            this.btnDelTable.Click += new System.EventHandler(this.btnDelTable_Click);
            // 
            // dgSelectedField
            // 
            this.dgSelectedField.AllowUserToAddRows = false;
            this.dgSelectedField.AllowUserToDeleteRows = false;
            this.dgSelectedField.AllowUserToResizeColumns = false;
            this.dgSelectedField.AllowUserToResizeRows = false;
            this.dgSelectedField.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgSelectedField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSelectedField.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTableName1,
            this.colFieldName,
            this.colDescCn});
            this.dgSelectedField.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgSelectedField.Location = new System.Drawing.Point(392, 0);
            this.dgSelectedField.Name = "dgSelectedField";
            this.dgSelectedField.RowHeadersVisible = false;
            this.dgSelectedField.RowTemplate.Height = 33;
            this.dgSelectedField.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSelectedField.Size = new System.Drawing.Size(606, 737);
            this.dgSelectedField.TabIndex = 4;
            this.dgSelectedField.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgSelectedField_DataBindingComplete);
            // 
            // btnKeyField
            // 
            this.btnKeyField.BackColor = System.Drawing.SystemColors.Window;
            this.btnKeyField.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeyField.Location = new System.Drawing.Point(206, 180);
            this.btnKeyField.Name = "btnKeyField";
            this.btnKeyField.Size = new System.Drawing.Size(180, 50);
            this.btnKeyField.TabIndex = 5;
            this.btnKeyField.Text = "选择主键";
            this.btnKeyField.UseVisualStyleBackColor = false;
            this.btnKeyField.Click += new System.EventHandler(this.btnKeyField_Click);
            // 
            // btnFields
            // 
            this.btnFields.BackColor = System.Drawing.SystemColors.Window;
            this.btnFields.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFields.Location = new System.Drawing.Point(206, 236);
            this.btnFields.Name = "btnFields";
            this.btnFields.Size = new System.Drawing.Size(180, 50);
            this.btnFields.TabIndex = 6;
            this.btnFields.Text = "选择字段";
            this.btnFields.UseVisualStyleBackColor = false;
            this.btnFields.Click += new System.EventHandler(this.btnFields_Click);
            // 
            // btnDelField
            // 
            this.btnDelField.BackColor = System.Drawing.SystemColors.Window;
            this.btnDelField.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelField.Location = new System.Drawing.Point(206, 292);
            this.btnDelField.Name = "btnDelField";
            this.btnDelField.Size = new System.Drawing.Size(180, 50);
            this.btnDelField.TabIndex = 7;
            this.btnDelField.Text = "删除字段";
            this.btnDelField.UseVisualStyleBackColor = false;
            this.btnDelField.Click += new System.EventHandler(this.btnDelField_Click);
            // 
            // btnCreateDm
            // 
            this.btnCreateDm.BackColor = System.Drawing.SystemColors.Window;
            this.btnCreateDm.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateDm.Location = new System.Drawing.Point(206, 348);
            this.btnCreateDm.Name = "btnCreateDm";
            this.btnCreateDm.Size = new System.Drawing.Size(180, 50);
            this.btnCreateDm.TabIndex = 8;
            this.btnCreateDm.Text = "创建DM";
            this.btnCreateDm.UseVisualStyleBackColor = false;
            this.btnCreateDm.Click += new System.EventHandler(this.btnCreateDm_Click);
            // 
            // btnCreateDal
            // 
            this.btnCreateDal.BackColor = System.Drawing.SystemColors.Window;
            this.btnCreateDal.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateDal.Location = new System.Drawing.Point(206, 404);
            this.btnCreateDal.Name = "btnCreateDal";
            this.btnCreateDal.Size = new System.Drawing.Size(180, 50);
            this.btnCreateDal.TabIndex = 9;
            this.btnCreateDal.Text = "创建DAL";
            this.btnCreateDal.UseVisualStyleBackColor = false;
            this.btnCreateDal.Click += new System.EventHandler(this.btnCreateDal_Click);
            // 
            // btnCreateBll
            // 
            this.btnCreateBll.BackColor = System.Drawing.SystemColors.Window;
            this.btnCreateBll.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateBll.Location = new System.Drawing.Point(206, 460);
            this.btnCreateBll.Name = "btnCreateBll";
            this.btnCreateBll.Size = new System.Drawing.Size(180, 50);
            this.btnCreateBll.TabIndex = 10;
            this.btnCreateBll.Text = "创建BLL";
            this.btnCreateBll.UseVisualStyleBackColor = false;
            // 
            // btnCreateEntity
            // 
            this.btnCreateEntity.BackColor = System.Drawing.SystemColors.Window;
            this.btnCreateEntity.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateEntity.Location = new System.Drawing.Point(206, 516);
            this.btnCreateEntity.Name = "btnCreateEntity";
            this.btnCreateEntity.Size = new System.Drawing.Size(180, 50);
            this.btnCreateEntity.TabIndex = 11;
            this.btnCreateEntity.Text = "创建数据模型";
            this.btnCreateEntity.UseVisualStyleBackColor = false;
            this.btnCreateEntity.Click += new System.EventHandler(this.btnCreateEntity_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.Window;
            this.btnClear.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(206, 572);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(180, 50);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // colTableName
            // 
            this.colTableName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTableName.DataPropertyName = "tableName";
            this.colTableName.HeaderText = "TABLE_NAME";
            this.colTableName.Name = "colTableName";
            this.colTableName.ReadOnly = true;
            // 
            // cbNameSpace
            // 
            this.cbNameSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNameSpace.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.875F, System.Drawing.FontStyle.Bold);
            this.cbNameSpace.FormattingEnabled = true;
            this.cbNameSpace.Location = new System.Drawing.Point(206, 689);
            this.cbNameSpace.Name = "cbNameSpace";
            this.cbNameSpace.Size = new System.Drawing.Size(179, 36);
            this.cbNameSpace.TabIndex = 13;
            // 
            // txtEntityName
            // 
            this.txtEntityName.Location = new System.Drawing.Point(207, 640);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(178, 31);
            this.txtEntityName.TabIndex = 14;
            // 
            // colTableName1
            // 
            this.colTableName1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTableName1.DataPropertyName = "tableName";
            this.colTableName1.HeaderText = "TABLE_NAME";
            this.colTableName1.Name = "colTableName1";
            this.colTableName1.ReadOnly = true;
            this.colTableName1.Width = 197;
            // 
            // colFieldName
            // 
            this.colFieldName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colFieldName.DataPropertyName = "entityFieldName";
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
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 737);
            this.Controls.Add(this.txtEntityName);
            this.Controls.Add(this.cbNameSpace);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCreateEntity);
            this.Controls.Add(this.btnCreateBll);
            this.Controls.Add(this.btnCreateDal);
            this.Controls.Add(this.btnCreateDm);
            this.Controls.Add(this.btnDelField);
            this.Controls.Add(this.btnFields);
            this.Controls.Add(this.btnKeyField);
            this.Controls.Add(this.dgSelectedField);
            this.Controls.Add(this.btnDelTable);
            this.Controls.Add(this.btnJoinTables);
            this.Controls.Add(this.btnMainTable);
            this.Controls.Add(this.dgTables);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DATAJOIN";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSelectedField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgTables;
        private System.Windows.Forms.Button btnMainTable;
        private System.Windows.Forms.Button btnJoinTables;
        private System.Windows.Forms.Button btnDelTable;
        private System.Windows.Forms.DataGridView dgSelectedField;
        private System.Windows.Forms.Button btnKeyField;
        private System.Windows.Forms.Button btnFields;
        private System.Windows.Forms.Button btnDelField;
        private System.Windows.Forms.Button btnCreateDm;
        private System.Windows.Forms.Button btnCreateDal;
        private System.Windows.Forms.Button btnCreateBll;
        private System.Windows.Forms.Button btnCreateEntity;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTableName;
        private System.Windows.Forms.ComboBox cbNameSpace;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTableName1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescCn;
    }
}

