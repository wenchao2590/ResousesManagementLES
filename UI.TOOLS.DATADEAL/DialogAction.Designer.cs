namespace UI.TOOLS.DATADEAL
{
    partial class DialogAction
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
            this.ckAdd = new System.Windows.Forms.CheckBox();
            this.ckEdit = new System.Windows.Forms.CheckBox();
            this.ckDel = new System.Windows.Forms.CheckBox();
            this.ckSearch = new System.Windows.Forms.CheckBox();
            this.ckSave = new System.Windows.Forms.CheckBox();
            this.btnYes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ckAdd
            // 
            this.ckAdd.AutoSize = true;
            this.ckAdd.Location = new System.Drawing.Point(22, 19);
            this.ckAdd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ckAdd.Name = "ckAdd";
            this.ckAdd.Size = new System.Drawing.Size(72, 30);
            this.ckAdd.TabIndex = 0;
            this.ckAdd.Text = "添加";
            this.ckAdd.UseVisualStyleBackColor = true;
            // 
            // ckEdit
            // 
            this.ckEdit.AutoSize = true;
            this.ckEdit.Location = new System.Drawing.Point(22, 50);
            this.ckEdit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ckEdit.Name = "ckEdit";
            this.ckEdit.Size = new System.Drawing.Size(72, 30);
            this.ckEdit.TabIndex = 1;
            this.ckEdit.Text = "修改";
            this.ckEdit.UseVisualStyleBackColor = true;
            // 
            // ckDel
            // 
            this.ckDel.AutoSize = true;
            this.ckDel.Location = new System.Drawing.Point(22, 81);
            this.ckDel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ckDel.Name = "ckDel";
            this.ckDel.Size = new System.Drawing.Size(72, 30);
            this.ckDel.TabIndex = 2;
            this.ckDel.Text = "删除";
            this.ckDel.UseVisualStyleBackColor = true;
            // 
            // ckSearch
            // 
            this.ckSearch.AutoSize = true;
            this.ckSearch.Location = new System.Drawing.Point(22, 112);
            this.ckSearch.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ckSearch.Name = "ckSearch";
            this.ckSearch.Size = new System.Drawing.Size(72, 30);
            this.ckSearch.TabIndex = 3;
            this.ckSearch.Text = "查询";
            this.ckSearch.UseVisualStyleBackColor = true;
            // 
            // ckSave
            // 
            this.ckSave.AutoSize = true;
            this.ckSave.Location = new System.Drawing.Point(111, 19);
            this.ckSave.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ckSave.Name = "ckSave";
            this.ckSave.Size = new System.Drawing.Size(72, 30);
            this.ckSave.TabIndex = 4;
            this.ckSave.Text = "保存";
            this.ckSave.UseVisualStyleBackColor = true;
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(272, 199);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(100, 50);
            this.btnYes.TabIndex = 5;
            this.btnYes.Text = "确定";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // DialogAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.ckSave);
            this.Controls.Add(this.ckSearch);
            this.Controls.Add(this.ckDel);
            this.Controls.Add(this.ckEdit);
            this.Controls.Add(this.ckAdd);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogAction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "动作选择";
            this.Load += new System.EventHandler(this.DialogAction_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckAdd;
        private System.Windows.Forms.CheckBox ckEdit;
        private System.Windows.Forms.CheckBox ckDel;
        private System.Windows.Forms.CheckBox ckSearch;
        private System.Windows.Forms.CheckBox ckSave;
        private System.Windows.Forms.Button btnYes;
    }
}