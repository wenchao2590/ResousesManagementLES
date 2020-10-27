namespace UI.TOOLS.DATADEAL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuComboTorV = new System.Windows.Forms.ToolStripComboBox();
            this.menuTextTableName = new System.Windows.Forms.ToolStripTextBox();
            this.menuBtnLoadTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnCreateEntity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnCreateField = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnCreateFieldAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnCreateFieldSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.menuComboMould = new System.Windows.Forms.ToolStripComboBox();
            this.menuBtnMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnSearchLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnSearchCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnBllFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnBllCreateInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnInsertSql = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnInsertSys = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnDeleteSql = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnDeleteEntity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnImageIconCss = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnGetExcelData = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnInsertEntityname = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnDeleteEntityName = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnDbCreateTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnDbDropTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBtnPublishSql = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTableName = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusEntity = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusMenu = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSearch = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.dgvFields = new System.Windows.Forms.DataGridView();
            this.cDB_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFIELD_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDATA_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cLENGTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPRECISION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescEn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLIST = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colForm = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSearch = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colExcel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuComboTorV,
            this.menuTextTableName,
            this.menuBtnLoadTable,
            this.menuBtnCreateEntity,
            this.menuBtnCreateField,
            this.menuComboMould,
            this.menuBtnMenu,
            this.menuBtnSearch,
            this.menuBtnExport});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1301, 40);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuComboTorV
            // 
            this.menuComboTorV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.menuComboTorV.Items.AddRange(new object[] {
            "Table",
            "View"});
            this.menuComboTorV.Name = "menuComboTorV";
            this.menuComboTorV.Size = new System.Drawing.Size(121, 36);
            this.menuComboTorV.SelectedIndexChanged += new System.EventHandler(this.menuComboTorV_SelectedIndexChanged);
            // 
            // menuTextTableName
            // 
            this.menuTextTableName.Name = "menuTextTableName";
            this.menuTextTableName.Size = new System.Drawing.Size(300, 36);
            this.menuTextTableName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.menuTextTableName_KeyPress);
            // 
            // menuBtnLoadTable
            // 
            this.menuBtnLoadTable.BackColor = System.Drawing.Color.White;
            this.menuBtnLoadTable.Image = ((System.Drawing.Image)(resources.GetObject("menuBtnLoadTable.Image")));
            this.menuBtnLoadTable.Name = "menuBtnLoadTable";
            this.menuBtnLoadTable.Size = new System.Drawing.Size(107, 36);
            this.menuBtnLoadTable.Text = "TABLE";
            this.menuBtnLoadTable.Click += new System.EventHandler(this.menuBtnLoadTable_Click);
            // 
            // menuBtnCreateEntity
            // 
            this.menuBtnCreateEntity.BackColor = System.Drawing.Color.White;
            this.menuBtnCreateEntity.Image = ((System.Drawing.Image)(resources.GetObject("menuBtnCreateEntity.Image")));
            this.menuBtnCreateEntity.Name = "menuBtnCreateEntity";
            this.menuBtnCreateEntity.Size = new System.Drawing.Size(115, 36);
            this.menuBtnCreateEntity.Text = "ENTITY";
            this.menuBtnCreateEntity.Click += new System.EventHandler(this.menuBtnCreateEntity_Click);
            // 
            // menuBtnCreateField
            // 
            this.menuBtnCreateField.BackColor = System.Drawing.Color.White;
            this.menuBtnCreateField.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBtnCreateFieldAll,
            this.menuBtnCreateFieldSelected});
            this.menuBtnCreateField.Image = ((System.Drawing.Image)(resources.GetObject("menuBtnCreateField.Image")));
            this.menuBtnCreateField.Name = "menuBtnCreateField";
            this.menuBtnCreateField.Size = new System.Drawing.Size(102, 36);
            this.menuBtnCreateField.Text = "FIELD";
            // 
            // menuBtnCreateFieldAll
            // 
            this.menuBtnCreateFieldAll.Name = "menuBtnCreateFieldAll";
            this.menuBtnCreateFieldAll.Size = new System.Drawing.Size(177, 30);
            this.menuBtnCreateFieldAll.Text = "ALL";
            this.menuBtnCreateFieldAll.Click += new System.EventHandler(this.menuBtnCreateFieldAll_Click);
            // 
            // menuBtnCreateFieldSelected
            // 
            this.menuBtnCreateFieldSelected.Name = "menuBtnCreateFieldSelected";
            this.menuBtnCreateFieldSelected.Size = new System.Drawing.Size(177, 30);
            this.menuBtnCreateFieldSelected.Text = "SELECTED";
            this.menuBtnCreateFieldSelected.Click += new System.EventHandler(this.menuBtnCreateFieldSelected_Click);
            // 
            // menuComboMould
            // 
            this.menuComboMould.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.menuComboMould.Name = "menuComboMould";
            this.menuComboMould.Size = new System.Drawing.Size(121, 36);
            // 
            // menuBtnMenu
            // 
            this.menuBtnMenu.Image = ((System.Drawing.Image)(resources.GetObject("menuBtnMenu.Image")));
            this.menuBtnMenu.Name = "menuBtnMenu";
            this.menuBtnMenu.Size = new System.Drawing.Size(110, 36);
            this.menuBtnMenu.Text = "MENU";
            this.menuBtnMenu.Click += new System.EventHandler(this.menuBtnMenu_Click);
            // 
            // menuBtnSearch
            // 
            this.menuBtnSearch.BackColor = System.Drawing.Color.White;
            this.menuBtnSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBtnSearchLoad,
            this.menuBtnSearchCreate});
            this.menuBtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("menuBtnSearch.Image")));
            this.menuBtnSearch.Name = "menuBtnSearch";
            this.menuBtnSearch.Size = new System.Drawing.Size(125, 36);
            this.menuBtnSearch.Text = "SEARCH";
            // 
            // menuBtnSearchLoad
            // 
            this.menuBtnSearchLoad.Name = "menuBtnSearchLoad";
            this.menuBtnSearchLoad.Size = new System.Drawing.Size(198, 30);
            this.menuBtnSearchLoad.Text = "CREATE";
            this.menuBtnSearchLoad.Click += new System.EventHandler(this.menuBtnSearchLoad_Click);
            // 
            // menuBtnSearchCreate
            // 
            this.menuBtnSearchCreate.Name = "menuBtnSearchCreate";
            this.menuBtnSearchCreate.Size = new System.Drawing.Size(198, 30);
            this.menuBtnSearchCreate.Text = "CONDITION";
            this.menuBtnSearchCreate.Click += new System.EventHandler(this.menuBtnSearchCreate_Click);
            // 
            // menuBtnExport
            // 
            this.menuBtnExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBtnBllFile,
            this.menuBtnBllCreateInfo,
            this.menuBtnInsertSql,
            this.menuBtnInsertSys,
            this.menuBtnDeleteSql,
            this.menuBtnDeleteEntity,
            this.menuBtnImageIconCss,
            this.menuBtnGetExcelData,
            this.menuBtnInsertEntityname,
            this.menuBtnDeleteEntityName,
            this.menuBtnDbCreateTable,
            this.menuBtnDbDropTable,
            this.menuBtnPublishSql});
            this.menuBtnExport.Image = ((System.Drawing.Image)(resources.GetObject("menuBtnExport.Image")));
            this.menuBtnExport.Name = "menuBtnExport";
            this.menuBtnExport.Size = new System.Drawing.Size(124, 36);
            this.menuBtnExport.Text = "EXPORT";
            // 
            // menuBtnBllFile
            // 
            this.menuBtnBllFile.Name = "menuBtnBllFile";
            this.menuBtnBllFile.Size = new System.Drawing.Size(276, 30);
            this.menuBtnBllFile.Text = "BLL.FILE";
            this.menuBtnBllFile.Click += new System.EventHandler(this.menuBtnBllFile_Click);
            // 
            // menuBtnBllCreateInfo
            // 
            this.menuBtnBllCreateInfo.Name = "menuBtnBllCreateInfo";
            this.menuBtnBllCreateInfo.Size = new System.Drawing.Size(276, 30);
            this.menuBtnBllCreateInfo.Text = "BLL.CREATE.INFO";
            this.menuBtnBllCreateInfo.Click += new System.EventHandler(this.menuBtnBllCreateInfo_Click);
            // 
            // menuBtnInsertSql
            // 
            this.menuBtnInsertSql.Name = "menuBtnInsertSql";
            this.menuBtnInsertSql.Size = new System.Drawing.Size(276, 30);
            this.menuBtnInsertSql.Text = "INSERT.SQL";
            this.menuBtnInsertSql.Click += new System.EventHandler(this.menuBtnInsertSql_Click);
            // 
            // menuBtnInsertSys
            // 
            this.menuBtnInsertSys.Name = "menuBtnInsertSys";
            this.menuBtnInsertSys.Size = new System.Drawing.Size(276, 30);
            this.menuBtnInsertSys.Text = "INSERT.ENTITY";
            this.menuBtnInsertSys.Click += new System.EventHandler(this.menuBtnInsertSys_Click);
            // 
            // menuBtnDeleteSql
            // 
            this.menuBtnDeleteSql.Name = "menuBtnDeleteSql";
            this.menuBtnDeleteSql.Size = new System.Drawing.Size(276, 30);
            this.menuBtnDeleteSql.Text = "INSERT.SYS";
            this.menuBtnDeleteSql.Click += new System.EventHandler(this.menuBtnDeleteSql_Click);
            // 
            // menuBtnDeleteEntity
            // 
            this.menuBtnDeleteEntity.Name = "menuBtnDeleteEntity";
            this.menuBtnDeleteEntity.Size = new System.Drawing.Size(276, 30);
            this.menuBtnDeleteEntity.Text = "DELETE.ENTITY";
            this.menuBtnDeleteEntity.Click += new System.EventHandler(this.menuBtnDeleteEntity_Click);
            // 
            // menuBtnImageIconCss
            // 
            this.menuBtnImageIconCss.Name = "menuBtnImageIconCss";
            this.menuBtnImageIconCss.Size = new System.Drawing.Size(276, 30);
            this.menuBtnImageIconCss.Text = "IMAGE.ICON.CSS";
            this.menuBtnImageIconCss.Click += new System.EventHandler(this.menuBtnImageIconCss_Click);
            // 
            // menuBtnGetExcelData
            // 
            this.menuBtnGetExcelData.Name = "menuBtnGetExcelData";
            this.menuBtnGetExcelData.Size = new System.Drawing.Size(276, 30);
            this.menuBtnGetExcelData.Text = "GET.EXCEL.DATA";
            this.menuBtnGetExcelData.Click += new System.EventHandler(this.menuBtnGetExcelData_Click);
            // 
            // menuBtnInsertEntityname
            // 
            this.menuBtnInsertEntityname.Name = "menuBtnInsertEntityname";
            this.menuBtnInsertEntityname.Size = new System.Drawing.Size(276, 30);
            this.menuBtnInsertEntityname.Text = "INSERT.ENTITYNAME";
            this.menuBtnInsertEntityname.Click += new System.EventHandler(this.menuBtnInsertEntityname_Click);
            // 
            // menuBtnDeleteEntityName
            // 
            this.menuBtnDeleteEntityName.Name = "menuBtnDeleteEntityName";
            this.menuBtnDeleteEntityName.Size = new System.Drawing.Size(276, 30);
            this.menuBtnDeleteEntityName.Text = "DELETE.ENTITYNAME";
            this.menuBtnDeleteEntityName.Click += new System.EventHandler(this.menuBtnDeleteEntityName_Click);
            // 
            // menuBtnDbCreateTable
            // 
            this.menuBtnDbCreateTable.Name = "menuBtnDbCreateTable";
            this.menuBtnDbCreateTable.Size = new System.Drawing.Size(276, 30);
            this.menuBtnDbCreateTable.Text = "DB_CREATE_TABLE";
            this.menuBtnDbCreateTable.Click += new System.EventHandler(this.menuBtnDbCreateTable_Click);
            // 
            // menuBtnDbDropTable
            // 
            this.menuBtnDbDropTable.Name = "menuBtnDbDropTable";
            this.menuBtnDbDropTable.Size = new System.Drawing.Size(276, 30);
            this.menuBtnDbDropTable.Text = "DB_DROP_TABLE";
            this.menuBtnDbDropTable.Click += new System.EventHandler(this.menuBtnDbDropTable_Click);
            // 
            // menuBtnPublishSql
            // 
            this.menuBtnPublishSql.Name = "menuBtnPublishSql";
            this.menuBtnPublishSql.Size = new System.Drawing.Size(276, 30);
            this.menuBtnPublishSql.Text = "PUBLISH_SQL";
            this.menuBtnPublishSql.Click += new System.EventHandler(this.menuBtnPublishSql_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTableName,
            this.statusEntity,
            this.statusMenu,
            this.statusSearch,
            this.toolStripProgressBar,
            this.lblMessage});
            this.statusStrip.Location = new System.Drawing.Point(0, 667);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1301, 30);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(56, 25);
            this.lblMessage.Text = "MSG";
            // 
            // lblTableName
            // 
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(0, 25);
            // 
            // statusEntity
            // 
            this.statusEntity.Name = "statusEntity";
            this.statusEntity.Size = new System.Drawing.Size(71, 25);
            this.statusEntity.Text = "ENTITY";
            // 
            // statusMenu
            // 
            this.statusMenu.Name = "statusMenu";
            this.statusMenu.Size = new System.Drawing.Size(66, 25);
            this.statusMenu.Text = "MENU";
            // 
            // statusSearch
            // 
            this.statusSearch.Name = "statusSearch";
            this.statusSearch.Size = new System.Drawing.Size(81, 25);
            this.statusSearch.Text = "SEARCH";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(400, 24);
            // 
            // dgvFields
            // 
            this.dgvFields.AllowUserToAddRows = false;
            this.dgvFields.AllowUserToDeleteRows = false;
            this.dgvFields.AllowUserToResizeColumns = false;
            this.dgvFields.AllowUserToResizeRows = false;
            this.dgvFields.BackgroundColor = System.Drawing.Color.White;
            this.dgvFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDB_ID,
            this.cFIELD_NAME,
            this.cDATA_TYPE,
            this.cLENGTH,
            this.cPRECISION,
            this.cDESC,
            this.colDescEn,
            this.colEntity,
            this.colLIST,
            this.colForm,
            this.colSearch,
            this.colExcel});
            this.dgvFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFields.Location = new System.Drawing.Point(0, 40);
            this.dgvFields.Name = "dgvFields";
            this.dgvFields.RowHeadersVisible = false;
            this.dgvFields.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvFields.RowTemplate.Height = 33;
            this.dgvFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFields.Size = new System.Drawing.Size(1301, 627);
            this.dgvFields.TabIndex = 2;
            this.dgvFields.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFields_CellContentDoubleClick);
            this.dgvFields.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvFields_DataBindingComplete);
            // 
            // cDB_ID
            // 
            this.cDB_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cDB_ID.DataPropertyName = "dbId";
            this.cDB_ID.HeaderText = "DB_ID";
            this.cDB_ID.Name = "cDB_ID";
            this.cDB_ID.ReadOnly = true;
            this.cDB_ID.Width = 98;
            // 
            // cFIELD_NAME
            // 
            this.cFIELD_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cFIELD_NAME.DataPropertyName = "fieldName";
            this.cFIELD_NAME.HeaderText = "FIELD_NAME";
            this.cFIELD_NAME.Name = "cFIELD_NAME";
            this.cFIELD_NAME.ReadOnly = true;
            this.cFIELD_NAME.Width = 158;
            // 
            // cDATA_TYPE
            // 
            this.cDATA_TYPE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cDATA_TYPE.DataPropertyName = "dataType";
            this.cDATA_TYPE.HeaderText = "DATA_TYPE";
            this.cDATA_TYPE.Name = "cDATA_TYPE";
            this.cDATA_TYPE.ReadOnly = true;
            this.cDATA_TYPE.Width = 146;
            // 
            // cLENGTH
            // 
            this.cLENGTH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cLENGTH.DataPropertyName = "maxLength";
            this.cLENGTH.HeaderText = "LENGTH";
            this.cLENGTH.Name = "cLENGTH";
            this.cLENGTH.ReadOnly = true;
            this.cLENGTH.Width = 117;
            // 
            // cPRECISION
            // 
            this.cPRECISION.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cPRECISION.DataPropertyName = "precision";
            this.cPRECISION.HeaderText = "PRECISION";
            this.cPRECISION.Name = "cPRECISION";
            this.cPRECISION.ReadOnly = true;
            this.cPRECISION.Width = 141;
            // 
            // cDESC
            // 
            this.cDESC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cDESC.DataPropertyName = "description";
            this.cDESC.HeaderText = "DESC";
            this.cDESC.Name = "cDESC";
            this.cDESC.ReadOnly = true;
            // 
            // colDescEn
            // 
            this.colDescEn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDescEn.DataPropertyName = "description_en";
            this.colDescEn.HeaderText = "DESC_EN";
            this.colDescEn.Name = "colDescEn";
            this.colDescEn.ReadOnly = true;
            this.colDescEn.Width = 125;
            // 
            // colEntity
            // 
            this.colEntity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colEntity.HeaderText = "ENTITY";
            this.colEntity.Name = "colEntity";
            this.colEntity.ReadOnly = true;
            this.colEntity.Width = 107;
            // 
            // colLIST
            // 
            this.colLIST.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colLIST.HeaderText = "LIST";
            this.colLIST.Name = "colLIST";
            this.colLIST.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colLIST.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colLIST.Width = 80;
            // 
            // colForm
            // 
            this.colForm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colForm.HeaderText = "FORM";
            this.colForm.Name = "colForm";
            this.colForm.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colForm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colForm.Width = 101;
            // 
            // colSearch
            // 
            this.colSearch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colSearch.HeaderText = "SEARCH";
            this.colSearch.Name = "colSearch";
            this.colSearch.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSearch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colSearch.Width = 117;
            // 
            // colExcel
            // 
            this.colExcel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colExcel.HeaderText = "EXCEL";
            this.colExcel.Name = "colExcel";
            this.colExcel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colExcel.Width = 99;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 697);
            this.Controls.Add(this.dgvFields);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据初始化";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripTextBox menuTextTableName;
        private System.Windows.Forms.ToolStripMenuItem menuBtnLoadTable;
        private System.Windows.Forms.DataGridView dgvFields;
        private System.Windows.Forms.ToolStripMenuItem menuBtnCreateEntity;
        private System.Windows.Forms.ToolStripMenuItem menuBtnCreateField;
        private System.Windows.Forms.ToolStripMenuItem menuBtnCreateFieldAll;
        private System.Windows.Forms.ToolStripMenuItem menuBtnCreateFieldSelected;
        private System.Windows.Forms.ToolStripMenuItem menuBtnSearch;
        private System.Windows.Forms.ToolStripStatusLabel lblMessage;
        private System.Windows.Forms.ToolStripMenuItem menuBtnSearchLoad;
        private System.Windows.Forms.ToolStripMenuItem menuBtnSearchCreate;
        private System.Windows.Forms.ToolStripMenuItem menuBtnMenu;
        private System.Windows.Forms.ToolStripStatusLabel lblTableName;
        private System.Windows.Forms.ToolStripMenuItem menuBtnExport;
        private System.Windows.Forms.ToolStripMenuItem menuBtnBllFile;
        private System.Windows.Forms.ToolStripMenuItem menuBtnInsertSql;
        private System.Windows.Forms.ToolStripComboBox menuComboMould;
        private System.Windows.Forms.ToolStripComboBox menuComboTorV;
        private System.Windows.Forms.ToolStripStatusLabel statusEntity;
        private System.Windows.Forms.ToolStripStatusLabel statusMenu;
        private System.Windows.Forms.ToolStripStatusLabel statusSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDB_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFIELD_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDATA_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLENGTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPRECISION;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescEn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntity;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colLIST;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colForm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSearch;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colExcel;
        private System.Windows.Forms.ToolStripMenuItem menuBtnInsertSys;
        private System.Windows.Forms.ToolStripMenuItem menuBtnDeleteSql;
        private System.Windows.Forms.ToolStripMenuItem menuBtnDeleteEntity;
        private System.Windows.Forms.ToolStripMenuItem menuBtnImageIconCss;
        private System.Windows.Forms.ToolStripMenuItem menuBtnGetExcelData;
        private System.Windows.Forms.ToolStripMenuItem menuBtnInsertEntityname;
        private System.Windows.Forms.ToolStripMenuItem menuBtnDeleteEntityName;
        private System.Windows.Forms.ToolStripMenuItem menuBtnDbCreateTable;
        private System.Windows.Forms.ToolStripMenuItem menuBtnDbDropTable;
        private System.Windows.Forms.ToolStripMenuItem menuBtnPublishSql;
        private System.Windows.Forms.ToolStripMenuItem menuBtnBllCreateInfo;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
    }
}

