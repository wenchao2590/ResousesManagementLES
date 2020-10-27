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
    public partial class DialogEntityField : Form
    {
        public DialogEntityField()
        {
            InitializeComponent();
        }

        public DialogEntityField(EntityFieldInfo _entityfieldinfo)
        {
            InitializeComponent();
            entityfieldinfo = _entityfieldinfo;
        }

        private EntityFieldInfo entityfieldinfo;

        private void DialogEntityField_Load(object sender, EventArgs e)
        {
            LoadDataType();
            LoadControlType();
            LoadTabTitle();
            LoadEditReadonly();
            LoadEntityFieldInfo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (entityfieldinfo == null) return;
            ///数据校验
            try
            {
                ValidData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ///
            try
            {
                if (!new EntityFieldBLL().UpdateInfo("[DISPLAY_NAME_CN] = '" + txtDisplayNameCn.Text.Trim() + "'"
                    + ",[DISPLAY_NAME_EN] = '" + txtDisplayNameEn.Text.Trim() + "'"
                    + ",[DISPLAY_ORDER] = " + txtDisplayOrder.Text.Trim() + ""
                    + ",[DATA_TYPE] = " + (cmbDataType.SelectedValue == null ? "NULL" : cmbDataType.SelectedValue.ToString()) + ""
                    + ",[CONTROL_TYPE] = " + (cmbControlType.SelectedValue == null ? "NULL" : cmbControlType.SelectedValue.ToString()) + ""
                    + ",[DEFAULT_VALUE] = '" + txtDefaultValue.Text.Trim() + "'"
                    + ",[TAB_TITLE_CODE] = '" + (cmbTabTitleCode.SelectedValue == null ? "NULL" : cmbTabTitleCode.SelectedValue.ToString()) + "'"
                    + ",[NULLENABLE] = " + (cbNullenable.Checked ? 1 : 0) + ""
                    + ",[EDITABLE] = " + (cbEditable.Checked ? 1 : 0) + ""
                    + ",[EDIT_DISPLAY_WIDTH] = '" + txtEditDisplayWidth.Text.Trim() + "'"
                    + ",[LISTABLE] = " + (cbListable.Checked ? 1 : 0) + ""
                    + ",[LIST_DISPLAY_WIDTH] = '" + txtListDisplayWidth.Text.Trim() + "'"
                    + ",[EDIT_READONLY] = " + (cmbEditReadonly.SelectedValue == null ? "NULL" : cmbEditReadonly.SelectedValue.ToString()) + ""
                    + ",[DATA_LENGTH] = " + (string.IsNullOrEmpty(txtDataLength.Text.Trim()) ? "NULL" : txtDataLength.Text.Trim()) + ""
                    + ",[PRECISION] = " + (string.IsNullOrEmpty(txtPrecision.Text.Trim()) ? "NULL" : txtPrecision.Text.Trim()) + ""
                    + ",[MIN_VALUE] = " + (string.IsNullOrEmpty(txtMinValue.Text.Trim()) ? "NULL" : txtMinValue.Text.Trim()) + ""
                    + ",[MAX_VALUE] = " + (string.IsNullOrEmpty(txtMaxValue.Text.Trim()) ? "NULL" : txtMaxValue.Text.Trim()) + ""
                    + ",[SORTABLE] = " + (cbSortable.Checked ? 1 : 0) + ""
                    + ",[REGEX] = '" + txtRegex.Text.Trim() + "'"
                    + ",[ERROR_MSG] = '" + txtErrorMsg.Text.Trim() + "'"
                    + ",[EXTEND1] = '" + txtExtend1.Text.Trim() + "'"
                    + ",[EXTEND2] = '" + txtExtend2.Text.Trim() + "'"
                    + ",[EXTEND3] = '" + txtExtend3.Text.Trim() + "'"
                    + ",[MODIFY_DATE] = GETDATE()"
                    + ",[EXPORT_EXCEL_FLAG] = " + (cbExportExcelFlag.Checked ? 1 : 0) + ""
                    + ",[EXPORT_EXCEL_ORDER] = " + (string.IsNullOrEmpty(txtExportExcelOrder.Text.Trim()) ? "NULL" : txtExportExcelOrder.Text.Trim()) + " "
                    , entityfieldinfo.Id))
                {
                    MessageBox.Show("保存实体属性配置失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        private void LoadEntityFieldInfo()
        {
            if (entityfieldinfo == null) return;
            ///属性名
            txtFieldName.Text = entityfieldinfo.FieldName;
            txtFieldName.Enabled = false;
            ///字段名
            txtTableFieldName.Text = entityfieldinfo.TableFieldName;
            txtTableFieldName.Enabled = false;
            ///中文名称
            txtDisplayNameCn.Text = entityfieldinfo.DisplayNameCn;
            ///英文名称
            txtDisplayNameEn.Text = entityfieldinfo.DisplayNameEn;
            ///顺序
            txtDisplayOrder.Text = entityfieldinfo.DisplayOrder.ToString();
            ///数据类型
            if (entityfieldinfo.DataType.GetValueOrDefault() > 0)
                cmbDataType.SelectedValue = entityfieldinfo.DataType.GetValueOrDefault();
            ///控件类型
            if (entityfieldinfo.ControlType.GetValueOrDefault() > 0)
                cmbControlType.SelectedValue = entityfieldinfo.ControlType.GetValueOrDefault();
            ///默认值
            txtDefaultValue.Text = entityfieldinfo.DefaultValue;
            ///选项卡代码
            cmbTabTitleCode.SelectedValue = (string.IsNullOrEmpty(entityfieldinfo.TabTitleCode) ? null : entityfieldinfo.TabTitleCode);
            ///是否允许为空
            cbNullenable.Checked = entityfieldinfo.Nullenable.GetValueOrDefault();
            ///编辑显示
            cbEditable.Checked = entityfieldinfo.Editable.GetValueOrDefault();
            ///编辑显示宽度
            txtEditDisplayWidth.Text = entityfieldinfo.EditDisplayWidth.ToString();
            ///列表显示
            cbListable.Checked = entityfieldinfo.Listable.GetValueOrDefault();
            ///列表显示宽度
            txtListDisplayWidth.Text = entityfieldinfo.ListDisplayWidth.ToString();
            ///编辑只读
            cmbEditReadonly.SelectedValue = (entityfieldinfo.EditReadonly == null ? 10 : entityfieldinfo.EditReadonly.GetValueOrDefault());
            ///数据长度
            txtDataLength.Text = entityfieldinfo.DataLength == null ? "" : entityfieldinfo.DataLength.GetValueOrDefault().ToString();
            ///精度
            txtPrecision.Text = entityfieldinfo.Precision == null ? "" : entityfieldinfo.Precision.GetValueOrDefault().ToString();
            ///最小值
            txtMinValue.Text = entityfieldinfo.MinValue == null ? "" : entityfieldinfo.MinValue.GetValueOrDefault().ToString();
            ///最大值
            txtMaxValue.Text = entityfieldinfo.MaxValue == null ? "" : entityfieldinfo.MaxValue.GetValueOrDefault().ToString();
            ///是否允许排序
            cbSortable.Checked = entityfieldinfo.Sortable.GetValueOrDefault();
            ///正则表达式
            txtRegex.Text = entityfieldinfo.Regex;
            ///报错信息
            txtErrorMsg.Text = entityfieldinfo.ErrorMsg;
            ///扩展1
            txtExtend1.Text = entityfieldinfo.Extend1;
            ///扩展2
            txtExtend2.Text = entityfieldinfo.Extend2;
            ///扩展3
            txtExtend3.Text = entityfieldinfo.Extend3;
            ///是否导出字段
            cbExportExcelFlag.Checked = entityfieldinfo.ExportExcelFlag.GetValueOrDefault();
            ///导出字段顺序
            txtExportExcelOrder.Text = entityfieldinfo.ExportExcelOrder == null ? "" : entityfieldinfo.ExportExcelOrder.GetValueOrDefault().ToString();
        }
        /// <summary>
        /// DATA_TYPE
        /// </summary>
        private void LoadDataType()
        {
            List<CodeItemDatasourceInfo> items = new CodeBLL().GetDataSource("DATA_TYPE");
            cmbDataType.DisplayMember = "ItemDisplay";
            cmbDataType.ValueMember = "ItemValue";
            cmbDataType.DataSource = items;
        }
        /// <summary>
        /// CONTROL_TYPE
        /// </summary>
        private void LoadControlType()
        {
            List<CodeItemDatasourceInfo> items = new CodeBLL().GetDataSource("CONTROL_TYPE");
            cmbControlType.DisplayMember = "ItemDisplay";
            cmbControlType.ValueMember = "ItemValue";
            cmbControlType.DataSource = items;
        }
        /// <summary>
        /// TAB_TITLE
        /// </summary>
        private void LoadTabTitle()
        {
            List<StringValueDatasourceInfo> items = new List<StringValueDatasourceInfo>();
            EntityInfo entityinfo = new EntityBLL().GetInfo(entityfieldinfo.EntityFid.GetValueOrDefault());
            if (entityinfo == null) return;
            if (string.IsNullOrEmpty(entityinfo.TabTitles)) return;
            string[] tabtitles = entityinfo.TabTitles.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var tabtitle in tabtitles)
            {
                string[] titletab = tabtitle.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                StringValueDatasourceInfo item = new StringValueDatasourceInfo();
                item.StringValue = titletab[0];
                item.ItemDisplay = titletab[1] + "[" + titletab[2] + "]";
                items.Add(item);
            }
            cmbTabTitleCode.DisplayMember = "ItemDisplay";
            cmbTabTitleCode.ValueMember = "StringValue";
            cmbTabTitleCode.DataSource = items;
        }
        /// <summary>
        /// READONLY_TYPE
        /// </summary>
        private void LoadEditReadonly()
        {
            List<CodeItemDatasourceInfo> items = new CodeBLL().GetDataSource("READONLY_TYPE");
            cmbEditReadonly.DisplayMember = "ItemDisplay";
            cmbEditReadonly.ValueMember = "ItemValue";
            cmbEditReadonly.DataSource = items;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExtend3Create_Click(object sender, EventArgs e)
        {
            DialogComboExtend dialog = new DialogComboExtend(txtExtend3.Text.Trim());
            if (dialog.ShowDialog(this) == DialogResult.Yes)
            {
                txtExtend3.Text = dialog.Extend;
            }
            dialog.Dispose();
        }
        /// <summary>
        /// 数据校验
        /// </summary>
        private void ValidData()
        {
            if (!string.IsNullOrEmpty(txtExtend1.Text.Trim()) && cmbControlType.SelectedValue.ToString() == "80")
            {
                string codeName = txtExtend1.Text.Trim();
                if (new CodeBLL().GetCounts("and [CODE_NAME] = '" + codeName + "' and [VALID_FLAG] = 1") == 0)
                    throw new Exception("系统代码" + codeName + "不存在");
            }
        }
    }
}
