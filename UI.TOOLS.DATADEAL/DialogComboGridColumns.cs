namespace UI.TOOLS.DATADEAL
{
    using System;
    using System.Windows.Forms;
    public partial class DialogComboGridColumns : Form
    {
        public DialogComboGridColumns()
        {
            InitializeComponent();
        }

        private string columns;
        /// <summary>
        /// 
        /// </summary>
        public string Columns
        {
            get
            {
                return columns;
            }

            set
            {
                columns = value;
            }
        }

        private void btnAddColumn_Click(object sender, EventArgs e)
        {
            try
            {
                columns = GetColumn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private string GetColumn()
        {
            string sql = string.Empty;
            ///对象属性
            if (string.IsNullOrEmpty(txtField.Text.Trim()))
                throw new Exception("对象属性为必填项");
            sql += txtField.Text.Trim();
            ///中文列标题
            if (string.IsNullOrEmpty(txtTitleCn.Text.Trim()))
                throw new Exception("中文列标题为必填项");
            sql += "-" + txtTitleCn.Text.Trim();
            ///列宽度
            if (!string.IsNullOrEmpty(txtWidth.Text.Trim()))
                sql += "-" + txtWidth.Text.Trim();
            ///英文列标题
            if (!string.IsNullOrEmpty(txtTitleEn.Text.Trim()))
            {
                if (string.IsNullOrEmpty(txtWidth.Text.Trim()))
                    sql += "-0-" + txtTitleEn.Text.Trim();
                else
                    sql += "-" + txtTitleEn.Text.Trim();
            }
            return "⊙" + sql;
        }
    }
}
