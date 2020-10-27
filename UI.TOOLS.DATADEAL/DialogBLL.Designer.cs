namespace UI.TOOLS.DATADEAL
{
    partial class DialogBLL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogBLL));
            this.rbLong = new System.Windows.Forms.RadioButton();
            this.rbInt = new System.Windows.Forms.RadioButton();
            this.gbKeyType = new System.Windows.Forms.GroupBox();
            this.gbKeyField = new System.Windows.Forms.GroupBox();
            this.rbId = new System.Windows.Forms.RadioButton();
            this.rbNid = new System.Windows.Forms.RadioButton();
            this.ckGetListByPage = new System.Windows.Forms.CheckBox();
            this.ckGetInfo = new System.Windows.Forms.CheckBox();
            this.ckAdd = new System.Windows.Forms.CheckBox();
            this.ckUpdate = new System.Windows.Forms.CheckBox();
            this.ckUpdateInfo = new System.Windows.Forms.CheckBox();
            this.ckDelete = new System.Windows.Forms.CheckBox();
            this.ckLogicDelete = new System.Windows.Forms.CheckBox();
            this.ckGetCounts = new System.Windows.Forms.CheckBox();
            this.btnYes = new System.Windows.Forms.Button();
            this.ckGetList = new System.Windows.Forms.CheckBox();
            this.ckInterfaceCreateInfo = new System.Windows.Forms.CheckBox();
            this.gbKeyType.SuspendLayout();
            this.gbKeyField.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbLong
            // 
            this.rbLong.AutoSize = true;
            this.rbLong.Checked = true;
            this.rbLong.Location = new System.Drawing.Point(6, 22);
            this.rbLong.Name = "rbLong";
            this.rbLong.Size = new System.Drawing.Size(78, 29);
            this.rbLong.TabIndex = 0;
            this.rbLong.TabStop = true;
            this.rbLong.Text = "long";
            this.rbLong.UseVisualStyleBackColor = true;
            // 
            // rbInt
            // 
            this.rbInt.AutoSize = true;
            this.rbInt.Location = new System.Drawing.Point(6, 49);
            this.rbInt.Name = "rbInt";
            this.rbInt.Size = new System.Drawing.Size(61, 29);
            this.rbInt.TabIndex = 1;
            this.rbInt.Text = "int";
            this.rbInt.UseVisualStyleBackColor = true;
            // 
            // gbKeyType
            // 
            this.gbKeyType.Controls.Add(this.rbLong);
            this.gbKeyType.Controls.Add(this.rbInt);
            this.gbKeyType.Location = new System.Drawing.Point(9, 2);
            this.gbKeyType.Name = "gbKeyType";
            this.gbKeyType.Size = new System.Drawing.Size(79, 84);
            this.gbKeyType.TabIndex = 2;
            this.gbKeyType.TabStop = false;
            this.gbKeyType.Text = "主键类型";
            // 
            // gbKeyField
            // 
            this.gbKeyField.Controls.Add(this.rbId);
            this.gbKeyField.Controls.Add(this.rbNid);
            this.gbKeyField.Location = new System.Drawing.Point(94, 2);
            this.gbKeyField.Name = "gbKeyField";
            this.gbKeyField.Size = new System.Drawing.Size(79, 84);
            this.gbKeyField.TabIndex = 3;
            this.gbKeyField.TabStop = false;
            this.gbKeyField.Text = "主键字段";
            // 
            // rbId
            // 
            this.rbId.AutoSize = true;
            this.rbId.Checked = true;
            this.rbId.Location = new System.Drawing.Point(6, 22);
            this.rbId.Name = "rbId";
            this.rbId.Size = new System.Drawing.Size(57, 29);
            this.rbId.TabIndex = 0;
            this.rbId.TabStop = true;
            this.rbId.Text = "ID";
            this.rbId.UseVisualStyleBackColor = true;
            // 
            // rbNid
            // 
            this.rbNid.AutoSize = true;
            this.rbNid.Location = new System.Drawing.Point(6, 49);
            this.rbNid.Name = "rbNid";
            this.rbNid.Size = new System.Drawing.Size(72, 29);
            this.rbNid.TabIndex = 1;
            this.rbNid.Text = "NID";
            this.rbNid.UseVisualStyleBackColor = true;
            // 
            // ckGetListByPage
            // 
            this.ckGetListByPage.AutoSize = true;
            this.ckGetListByPage.Checked = true;
            this.ckGetListByPage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckGetListByPage.Location = new System.Drawing.Point(9, 93);
            this.ckGetListByPage.Name = "ckGetListByPage";
            this.ckGetListByPage.Size = new System.Drawing.Size(166, 29);
            this.ckGetListByPage.TabIndex = 4;
            this.ckGetListByPage.Text = "GetListByPage";
            this.ckGetListByPage.UseVisualStyleBackColor = true;
            // 
            // ckGetInfo
            // 
            this.ckGetInfo.AutoSize = true;
            this.ckGetInfo.Checked = true;
            this.ckGetInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckGetInfo.Location = new System.Drawing.Point(9, 120);
            this.ckGetInfo.Name = "ckGetInfo";
            this.ckGetInfo.Size = new System.Drawing.Size(127, 29);
            this.ckGetInfo.TabIndex = 5;
            this.ckGetInfo.Text = "SelectInfo";
            this.ckGetInfo.UseVisualStyleBackColor = true;
            // 
            // ckAdd
            // 
            this.ckAdd.AutoSize = true;
            this.ckAdd.Checked = true;
            this.ckAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckAdd.Location = new System.Drawing.Point(9, 147);
            this.ckAdd.Name = "ckAdd";
            this.ckAdd.Size = new System.Drawing.Size(127, 29);
            this.ckAdd.TabIndex = 6;
            this.ckAdd.Text = "InsertInfo";
            this.ckAdd.UseVisualStyleBackColor = true;
            // 
            // ckUpdate
            // 
            this.ckUpdate.AutoSize = true;
            this.ckUpdate.Location = new System.Drawing.Point(9, 174);
            this.ckUpdate.Name = "ckUpdate";
            this.ckUpdate.Size = new System.Drawing.Size(140, 29);
            this.ckUpdate.TabIndex = 7;
            this.ckUpdate.Text = "UpdateInfo";
            this.ckUpdate.UseVisualStyleBackColor = true;
            // 
            // ckUpdateInfo
            // 
            this.ckUpdateInfo.AutoSize = true;
            this.ckUpdateInfo.Checked = true;
            this.ckUpdateInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckUpdateInfo.Location = new System.Drawing.Point(9, 201);
            this.ckUpdateInfo.Name = "ckUpdateInfo";
            this.ckUpdateInfo.Size = new System.Drawing.Size(213, 29);
            this.ckUpdateInfo.TabIndex = 8;
            this.ckUpdateInfo.Text = "UpdateInfoByFields";
            this.ckUpdateInfo.UseVisualStyleBackColor = true;
            // 
            // ckDelete
            // 
            this.ckDelete.AutoSize = true;
            this.ckDelete.Location = new System.Drawing.Point(9, 228);
            this.ckDelete.Name = "ckDelete";
            this.ckDelete.Size = new System.Drawing.Size(131, 29);
            this.ckDelete.TabIndex = 9;
            this.ckDelete.Text = "DeleteInfo";
            this.ckDelete.UseVisualStyleBackColor = true;
            // 
            // ckLogicDelete
            // 
            this.ckLogicDelete.AutoSize = true;
            this.ckLogicDelete.Checked = true;
            this.ckLogicDelete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckLogicDelete.Location = new System.Drawing.Point(9, 255);
            this.ckLogicDelete.Name = "ckLogicDelete";
            this.ckLogicDelete.Size = new System.Drawing.Size(142, 29);
            this.ckLogicDelete.TabIndex = 10;
            this.ckLogicDelete.Text = "LogicDelete";
            this.ckLogicDelete.UseVisualStyleBackColor = true;
            // 
            // ckGetCounts
            // 
            this.ckGetCounts.AutoSize = true;
            this.ckGetCounts.Location = new System.Drawing.Point(9, 282);
            this.ckGetCounts.Name = "ckGetCounts";
            this.ckGetCounts.Size = new System.Drawing.Size(133, 29);
            this.ckGetCounts.TabIndex = 11;
            this.ckGetCounts.Text = "GetCounts";
            this.ckGetCounts.UseVisualStyleBackColor = true;
            // 
            // btnYes
            // 
            this.btnYes.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnYes.Image = ((System.Drawing.Image)(resources.GetObject("btnYes.Image")));
            this.btnYes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnYes.Location = new System.Drawing.Point(8, 349);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(378, 50);
            this.btnYes.TabIndex = 12;
            this.btnYes.Text = "确定";
            this.btnYes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // ckGetList
            // 
            this.ckGetList.AutoSize = true;
            this.ckGetList.Checked = true;
            this.ckGetList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckGetList.Location = new System.Drawing.Point(9, 310);
            this.ckGetList.Name = "ckGetList";
            this.ckGetList.Size = new System.Drawing.Size(100, 29);
            this.ckGetList.TabIndex = 13;
            this.ckGetList.Text = "GetList";
            this.ckGetList.UseVisualStyleBackColor = true;
            // 
            // ckInterfaceCreateInfo
            // 
            this.ckInterfaceCreateInfo.AutoSize = true;
            this.ckInterfaceCreateInfo.Checked = true;
            this.ckInterfaceCreateInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckInterfaceCreateInfo.Location = new System.Drawing.Point(238, 12);
            this.ckInterfaceCreateInfo.Name = "ckInterfaceCreateInfo";
            this.ckInterfaceCreateInfo.Size = new System.Drawing.Size(132, 29);
            this.ckInterfaceCreateInfo.TabIndex = 14;
            this.ckInterfaceCreateInfo.Text = "CreateInfo";
            this.ckInterfaceCreateInfo.UseVisualStyleBackColor = true;
            // 
            // DialogBLL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 411);
            this.Controls.Add(this.ckInterfaceCreateInfo);
            this.Controls.Add(this.ckGetList);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.ckGetCounts);
            this.Controls.Add(this.ckLogicDelete);
            this.Controls.Add(this.ckDelete);
            this.Controls.Add(this.ckUpdateInfo);
            this.Controls.Add(this.ckUpdate);
            this.Controls.Add(this.ckAdd);
            this.Controls.Add(this.ckGetInfo);
            this.Controls.Add(this.ckGetListByPage);
            this.Controls.Add(this.gbKeyField);
            this.Controls.Add(this.gbKeyType);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogBLL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "业务逻辑层配置";
            this.gbKeyType.ResumeLayout(false);
            this.gbKeyType.PerformLayout();
            this.gbKeyField.ResumeLayout(false);
            this.gbKeyField.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbLong;
        private System.Windows.Forms.GroupBox gbKeyType;
        private System.Windows.Forms.GroupBox gbKeyField;
        private System.Windows.Forms.RadioButton rbId;
        public System.Windows.Forms.CheckBox ckUpdateInfo;
        public System.Windows.Forms.CheckBox ckDelete;
        public System.Windows.Forms.CheckBox ckLogicDelete;
        public System.Windows.Forms.CheckBox ckGetCounts;
        public System.Windows.Forms.CheckBox ckGetListByPage;
        public System.Windows.Forms.CheckBox ckGetInfo;
        public System.Windows.Forms.CheckBox ckAdd;
        public System.Windows.Forms.CheckBox ckUpdate;
        private System.Windows.Forms.Button btnYes;
        public System.Windows.Forms.RadioButton rbInt;
        public System.Windows.Forms.RadioButton rbNid;
        public System.Windows.Forms.CheckBox ckGetList;
        public System.Windows.Forms.CheckBox ckInterfaceCreateInfo;
    }
}