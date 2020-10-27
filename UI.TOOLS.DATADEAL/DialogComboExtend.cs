namespace UI.TOOLS.DATADEAL
{
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
    public partial class DialogComboExtend : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public DialogComboExtend()
        {
            InitializeComponent();
        }

        public DialogComboExtend(string _extend)
        {
            InitializeComponent();
            extend = _extend;
        }

        private string extend;
        /// <summary>
        /// 返回规则文本
        /// </summary>
        public string Extend
        {
            get
            {
                return extend;
            }

            set
            {
                extend = value;
            }
        }

        private void DialogComboExtend_Load(object sender, EventArgs e)
        {
            LoadControlType();

            lblExtend.Text = extend;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMainExtend_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            try
            {
                sql = GetMainExtend();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(extend))
            {
                lblExtend.Text = extend = sql;
                return;
            }
            if (MessageBox.Show("已编辑的扩展信息将被替换，是否继续?"
                , "提示"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button2)
                == DialogResult.Yes)
                lblExtend.Text = extend = sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGridExtend_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            try
            {
                sql = GetGridExtend();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(extend))
            {
                lblExtend.Text = extend = sql;
                return;
            }
            if (MessageBox.Show("已编辑的扩展信息将被替换，是否继续?"
                , "提示"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button2)
                == DialogResult.Yes)
                lblExtend.Text = extend = sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinkExtend_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            try
            {
                sql = GetLinkExtend();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(extend))
            {
                MessageBox.Show("请先添加主控件或表格控件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblExtend.Text = extend += sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGridColumns_Click(object sender, EventArgs e)
        {
            DialogComboGridColumns dialog = new DialogComboGridColumns();
            if (dialog.ShowDialog(this) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(txtGridColumns.Text.Trim()))
                    txtGridColumns.Text = dialog.Columns.Substring(1);
                else
                    txtGridColumns.Text += dialog.Columns;
            }
            dialog.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTreeExtend_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            try
            {
                sql = GetTreeExtend();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(extend))
            {
                lblExtend.Text = extend = sql;
                return;
            }
            if (MessageBox.Show("已编辑的扩展信息将被替换，是否继续?"
                , "提示"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button2)
                == DialogResult.Yes)
                lblExtend.Text = extend = sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        /// <summary>
        /// 加载控件类型
        /// </summary>
        private void LoadControlType()
        {
            List<CodeItemDatasourceInfo> items = new CodeBLL().GetDataSource("CONTROL_TYPE");
            cmbLinkControlType.DisplayMember = "ItemDisplay";
            cmbLinkControlType.ValueMember = "ItemValue";
            cmbLinkControlType.DataSource = items;
        }

        private string GetTreeExtend()
        {
            string sql = string.Empty;
            ///对象名,entityName
            if (string.IsNullOrEmpty(txtEntityName.Text.Trim()))
                throw new Exception("对象名为必填项");
            sql += ",对象名:" + txtEntityName.Text.Trim();
            ///绑定字段,idField
            if (string.IsNullOrEmpty(txtIdField.Text.Trim()))
                throw new Exception("绑定字段为必填项");
            sql += ",绑定字段:" + txtIdField.Text.Trim();
            ///显示字段,textField
            if (string.IsNullOrEmpty(txtTextField.Text.Trim()))
                throw new Exception("显示字段为必填项");
            sql += ",显示字段:" + txtTextField.Text.Trim();
            ///命名空间名,AN
            if (string.IsNullOrEmpty(txtAN.Text.Trim()))
                throw new Exception("命名空间名为必填项");
            sql += ",命名空间名:" + txtAN.Text.Trim();
            ///父节点字段,ParentFid
            if (string.IsNullOrEmpty(txtTreeParentId.Text.Trim()))
                throw new Exception("父节点字段为必填项");
            sql += ",父节点字段:" + txtTreeParentId.Text.Trim();
            ///函数名,ajaxMethod
            if (!string.IsNullOrEmpty(txtAjaxMethod.Text.Trim()))
                sql += ",函数名:" + txtAjaxMethod.Text.Trim();
            ///数据库条件,sqlFilter
            if (!string.IsNullOrEmpty(txtSqlFilter.Text.Trim()))
                sql += ",数据库条件:" + txtSqlFilter.Text.Trim();
            ///复杂过滤条件,complexFilter
            if (!string.IsNullOrEmpty(txtComplexFilter.Text.Trim()))
                sql += ",复杂过滤条件:" + txtComplexFilter.Text.Trim();
            ///数据库默认排序,sqlSort
            if (!string.IsNullOrEmpty(txtSqlSort.Text.Trim()))
                sql += ",数据库默认排序:" + txtSqlSort.Text.Trim();
            ///是否多选,isMultiple
            if (cbIsMultiple.Checked)
                sql += ",是否多选:true";
            return "sql^" + sql.Substring(1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetMainExtend()
        {
            string sql = string.Empty;
            ///对象名,entityName
            if (string.IsNullOrEmpty(txtEntityName.Text.Trim()))
                throw new Exception("对象名为必填项");
            sql += ",对象名:" + txtEntityName.Text.Trim();
            ///绑定字段,idField
            if (string.IsNullOrEmpty(txtIdField.Text.Trim()))
                throw new Exception("绑定字段为必填项");
            sql += ",绑定字段:" + txtIdField.Text.Trim();
            ///显示字段,textField
            if (string.IsNullOrEmpty(txtTextField.Text.Trim()))
                throw new Exception("显示字段为必填项");
            sql += ",显示字段:" + txtTextField.Text.Trim();
            ///命名空间名,AN
            if (string.IsNullOrEmpty(txtAN.Text.Trim()))
                throw new Exception("命名空间名为必填项");
            sql += ",命名空间名:" + txtAN.Text.Trim();
            ///函数名,ajaxMethod
            if (!string.IsNullOrEmpty(txtAjaxMethod.Text.Trim()))
                sql += ",函数名:" + txtAjaxMethod.Text.Trim();
            ///数据库条件,sqlFilter
            if (!string.IsNullOrEmpty(txtSqlFilter.Text.Trim()))
                sql += ",数据库条件:" + txtSqlFilter.Text.Trim();
            ///复杂过滤条件,complexFilter
            if (!string.IsNullOrEmpty(txtComplexFilter.Text.Trim()))
                sql += ",复杂过滤条件:" + txtComplexFilter.Text.Trim();
            ///数据库默认排序,sqlSort
            if (!string.IsNullOrEmpty(txtSqlSort.Text.Trim()))
                sql += ",数据库默认排序:" + txtSqlSort.Text.Trim();
            ///是否多选,isMultiple
            if (cbIsMultiple.Checked)
                sql += ",是否多选:true";
            return "sql^" + sql.Substring(1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetLinkExtend()
        {
            string sql = string.Empty;
            ///对象名,entityName
            if (string.IsNullOrEmpty(txtEntityName.Text.Trim()))
                throw new Exception("对象名为必填项");
            sql += ",对象名:" + txtEntityName.Text.Trim();
            ///控件类型,controlType
            if (string.IsNullOrEmpty(cmbLinkControlType.SelectedValue.ToString()))
                throw new Exception("控件类型为必选项");
            sql += ",控件类型:" + cmbLinkControlType.SelectedValue.ToString();
            ///属性名称,attributeName
            if (string.IsNullOrEmpty(txtLinkAttributeName.Text.Trim()))
                throw new Exception("属性名称为必填项");
            sql += ",属性名称:" + txtLinkAttributeName.Text.Trim();
            ///数据属性名称,dataAttribute
            if (string.IsNullOrEmpty(txtLinkDataAttributeName.Text.Trim()))
                throw new Exception("数据属性名称为必填项");
            sql += ",数据属性名称:" + txtLinkDataAttributeName.Text.Trim();
            ///字段名称,fieldName
            if (string.IsNullOrEmpty(txtLinkFieldName.Text.Trim()))
                throw new Exception("字段名称为必填项");
            sql += ",字段名称:" + txtLinkFieldName.Text.Trim();
            ///命名空间名,AN
            if (string.IsNullOrEmpty(txtAN.Text.Trim()))
                throw new Exception("命名空间名为必填项");
            sql += ",命名空间名:" + txtAN.Text.Trim();
            ///函数名,ajaxMethod
            if (!string.IsNullOrEmpty(txtAjaxMethod.Text.Trim()))
                sql += ",函数名:" + txtAjaxMethod.Text.Trim();
            ///数据库条件,sqlFilter
            if (!string.IsNullOrEmpty(txtSqlFilter.Text.Trim()))
                sql += ",数据库条件:" + txtSqlFilter.Text.Trim();
            ///复杂过滤条件,complexFilter
            if (!string.IsNullOrEmpty(txtComplexFilter.Text.Trim()))
                sql += ",复杂过滤条件:" + txtComplexFilter.Text.Trim();
            ///数据库默认排序,sqlSort
            if (!string.IsNullOrEmpty(txtSqlSort.Text.Trim()))
                sql += ",数据库默认排序:" + txtSqlSort.Text.Trim();
            return "|" + sql.Substring(1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetGridExtend()
        {
            string sql = string.Empty;
            ///对象名,entityName
            if (string.IsNullOrEmpty(txtEntityName.Text.Trim()))
                throw new Exception("对象名为必填项");
            sql += ",对象名:" + txtEntityName.Text.Trim();
            ///绑定字段,idField
            if (string.IsNullOrEmpty(txtIdField.Text.Trim()))
                throw new Exception("绑定字段为必填项");
            sql += ",绑定字段:" + txtIdField.Text.Trim();
            ///显示字段,textField
            if (string.IsNullOrEmpty(txtTextField.Text.Trim()))
                throw new Exception("显示字段为必填项");
            sql += ",显示字段:" + txtTextField.Text.Trim();
            ///命名空间名,AN
            if (string.IsNullOrEmpty(txtAN.Text.Trim()))
                throw new Exception("命名空间名为必填项");
            sql += ",命名空间名:" + txtAN.Text.Trim();

            ///列,columns
            if (string.IsNullOrEmpty(txtGridColumns.Text.Trim()))
                throw new Exception("列为必填项");
            sql += ",列:" + txtGridColumns.Text.Trim();
            ///排序名,sortName
            if (!string.IsNullOrEmpty(txtGridSortName.Text.Trim()))
                sql += ",排序名:" + txtGridSortName.Text.Trim();
            ///排序方式,sortOrder
            if (!string.IsNullOrEmpty(txtGridSortOrder.Text.Trim()))
                sql += ",排序方式:" + txtGridSortOrder.Text.Trim();
            ///前台过滤条件,comboFilter
            if (!string.IsNullOrEmpty(txtGridComboFilter.Text.Trim()))
                sql += ",前台过滤条件:" + txtGridComboFilter.Text.Trim();

            ///函数名,ajaxMethod
            if (!string.IsNullOrEmpty(txtAjaxMethod.Text.Trim()))
                sql += ",函数名:" + txtAjaxMethod.Text.Trim();
            ///数据库条件,sqlFilter
            if (!string.IsNullOrEmpty(txtSqlFilter.Text.Trim()))
                sql += ",数据库条件:" + txtSqlFilter.Text.Trim();
            ///复杂过滤条件,complexFilter
            if (!string.IsNullOrEmpty(txtComplexFilter.Text.Trim()))
                sql += ",复杂过滤条件:" + txtComplexFilter.Text.Trim();
            ///数据库默认排序,sqlSort
            if (!string.IsNullOrEmpty(txtSqlSort.Text.Trim()))
                sql += ",数据库默认排序:" + txtSqlSort.Text.Trim();
            ///是否多选,isMultiple
            if (cbIsMultiple.Checked)
                sql += ",是否多选:true";
            return "sql^" + sql.Substring(1);
        }

        private void ShowHelpDialog(string message)
        {
            MessageBox.Show(message, "示例", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblEntityName_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("对象名,entityName"
                + "\r\n示例：Plant");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblIdField_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("绑定字段,idField"
                + "\r\n示例：Plant");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTextField_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("显示字段,textField"
            + "\r\n示例：PlantName");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblAN_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("命名空间名,AN"
            + "\r\n示例：BLL.LES");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblAjaxMethod_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("函数名,ajaxMethod"
            + "\r\n示例：默认为 ajaxControl");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSqlFilter_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("数据库条件,sqlFilter"
            + "\r\n示例：and VALID_FLAG = 1");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblComplexFilter_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("复杂过滤条件,complexFilter"
            + "\r\n点击[复杂过滤]按钮，打开生成器");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSqlSort_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("数据库默认排序,sqlSort"
            + "\r\n示例：ID asc,CREATE_DATE desc");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblLinkAttributeName_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("属性名称,attributeName"
            + "\r\n示例：控件类型 + 属性名称 = 联动控件ID,20-AssemblyLine");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblLinkDataAttributeName_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("数据属性名称,dataAttributeName"
            + "\r\n示例：父控件数据关联属性,Plant");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblLinkFieldName_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("字段名称,fieldName"
            + "\r\n示例：联动控件获取数据关联条件字段,PLANT");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblGridSortName_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("排序名,sortName"
            + "\r\n示例：对象的属性默认为[idField],Plant");
        }
        /// <summary>
        /// 排序方式,sortOrder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblGridSortOrder_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("排序方式,sortOrder"
            + "\r\n示例：默认为 desc");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblGridColumns_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("列,columns"
            + "\r\n点击[列]按钮，打开生成器");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblGridComboFilter_DoubleClick(object sender, EventArgs e)
        {
            ShowHelpDialog("前台过滤条件,comboFilter"
            + "\r\n示例：也称输入过滤字段，ASSEMBLY_LINE-ASSEMBLY_LINE_NAME");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComplexFilter_Click(object sender, EventArgs e)
        {
            DialogComplexFilter dialog = new DialogComplexFilter();
            if (dialog.ShowDialog(this) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(txtComplexFilter.Text.Trim()))
                    txtComplexFilter.Text = dialog.ComplexFilter.Substring(1);
                else
                    txtComplexFilter.Text += dialog.ComplexFilter;
            }
            dialog.Dispose();
        }



    }
}
