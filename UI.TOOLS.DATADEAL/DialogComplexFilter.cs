using BLL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.TOOLS.DATADEAL
{
    public partial class DialogComplexFilter : Form
    {
        public DialogComplexFilter()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        private void LoadType()
        {
            List<CodeItemDatasourceInfo> items = new CodeBLL().GetDataSource("COMPLEX_FILTER");
            cmbType.DisplayMember = "ItemDisplay";
            cmbType.ValueMember = "ItemValue";
            cmbType.DataSource = items;
        }
        /// <summary>
        /// 
        /// </summary>
        private void LoadControlType()
        {
            List<CodeItemDatasourceInfo> items = new CodeBLL().GetDataSource("CONTROL_TYPE");
            cmbControlType.DisplayMember = "ItemDisplay";
            cmbControlType.ValueMember = "ItemValue";
            cmbControlType.DataSource = items;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DialogComplexFilter_Load(object sender, EventArgs e)
        {
            LoadType();
            LoadControlType();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void ShowHelpDialog(string message)
        {
            MessageBox.Show(message, "示例", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetComplexFilter()
        {
            string sql = string.Empty;
            string tableFieldName = string.Empty;
            string complexFilterType = string.Empty;
            ///条件类型
            if (string.IsNullOrEmpty(cmbType.SelectedValue.ToString()))
                throw new Exception("类型为必选项");
            complexFilterType = cmbType.SelectedValue.ToString();
            ///中文列标题
            if (string.IsNullOrEmpty(txtTableFieldName.Text.Trim()))
                throw new Exception("字段为必填项");
            tableFieldName = txtTableFieldName.Text.Trim();
            switch (complexFilterType)
            {
                case "10":///控件条件
                          ///控件类型
                    if (string.IsNullOrEmpty(cmbControlType.SelectedValue.ToString()))
                        throw new Exception("控件类型为必选项");
                    sql += cmbControlType.SelectedValue.ToString();
                    ///属性
                    if (string.IsNullOrEmpty(txtFieldName.Text.Trim()))
                        throw new Exception("属性为必填项");
                    sql += "、" + txtFieldName.Text.Trim();
                    sql += "、" + tableFieldName;
                    ///属性A-属性B
                    if (!string.IsNullOrEmpty(txtFindField.Text.Trim()))
                        sql += "、" + txtFindField.Text.Trim();
                    break;
                case "20":///父对象条件
                          ///属性
                    if (string.IsNullOrEmpty(txtFieldName.Text.Trim()))
                        throw new Exception("属性为必填项");
                    sql += tableFieldName + "、" + txtFieldName.Text.Trim();
                    break;
                case "30":///固定条件
                          ///值
                    if (string.IsNullOrEmpty(txtFieldValue.Text.Trim()))
                        throw new Exception("值为必填项");
                    sql += tableFieldName + "、" + txtFieldValue.Text.Trim();
                    break;
                case "40":///权限条件

                    break;
            }
            if (string.IsNullOrEmpty(sql))
                return string.Empty;
            return "⊙" + complexFilterType + "、" + sql;
        }
        /// <summary>
        /// 帮助
        /// </summary>
        private string helpMessage
            = "控件条件：控件类型、属性(父控件.属性)、字段、属性A-属性B(可选)"
            + "\r\n【输出：and 字段 = '控件的值or作为属性A的样本查找属性B的值'】"
            + "\r\n父对象条件：字段、属性"
            + "\r\n【输出：and 字段 = '父对象.属性的值'】"
            + "\r\n固定条件：字段、值"
            + "\r\n【输出：and 字段 = '值'】"
            + "\r\n权限条件：有待改进，暂不使用";

        private string complexFilter;
        /// <summary>
        /// 
        /// </summary>
        public string ComplexFilter
        {
            get
            {
                return complexFilter;
            }

            set
            {
                complexFilter = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTableFieldName_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog(helpMessage);
        }

        private void btnAddComplexFilter_Click(object sender, EventArgs e)
        {
            try
            {
                complexFilter = GetComplexFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!string.IsNullOrEmpty(complexFilter))
                this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
