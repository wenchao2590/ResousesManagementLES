using BLL.SYS;
using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace UI.TOOLS.DATADEAL
{
    public partial class DialogEntity : Form
    {
        public DialogEntity()
        {
            InitializeComponent();
        }

        public DialogEntity(EntityInfo entityInfo)
        {
            InitializeComponent();
            info = entityInfo;
        }

        private EntityInfo info;
        /// <summary>
        /// 实体对象
        /// </summary>
        public EntityInfo Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }
        /// <summary>
        /// 加载模型类型选项
        /// </summary>
        private void LoadEntityType()
        {
            List<CodeItemDatasourceInfo> items = new CodeBLL().GetDataSource("ENTITY_TYPE");
            cmbEntityType.DisplayMember = "ItemDisplay";
            cmbEntityType.ValueMember = "ItemValue";
            cmbEntityType.DataSource = items;
        }
        /// <summary>
        /// 加载实体类
        /// </summary>
        private void LoadEntityInfo()
        {
            if (info == null) return;
            ///数据模型名称
            txtEntityName.Text = info.EntityName;
            txtEntityName.Enabled = false;
            ///设计表名
            txtTableNames.Text = info.TableNames;
            txtTableNames.Enabled = false;
            ///数据模型类型
            cmbEntityType.SelectedValue = info.EntityType.GetValueOrDefault();
            ///默认排序依据
            txtDefaultSort.Text = info.DefaultSort;
            ///模型配置
            txtParentField.Text = info.ParentField;
            ///数据权限配置
            txtAuthConfig.Text = info.AuthConfig;
            ///选项卡配置
            txtTabTitles.Text = info.TabTitles;
            ///备注
            txtComments.Text = info.Comments;
            txtID.Text = info.Id.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (info == null) return;
            try
            {
                using (var trans = new TransactionScope())
                {
                    if (!new EntityBLL().UpdateInfo("[ENTITY_TYPE] = " + cmbEntityType.SelectedValue.ToString() + ""
                        + ",[ENTITY_NAME] = '" + txtEntityName.Text.Trim() + "'"
                        + ",[DEFAULT_SORT] = '" + txtDefaultSort.Text.Trim() + "'"
                        + ",[PARENT_FIELD] = '" + txtParentField.Text.Trim() + "'"
                        + ",[AUTH_CONFIG] = '" + txtAuthConfig.Text.Trim() + "'"
                        + ",[TAB_TITLES] = '" + txtTabTitles.Text.Trim() + "'"
                        + ",[COMMENTS] = '" + txtComments.Text.Trim() + "'", info.Id))
                    {
                        MessageBox.Show("保存实体属性失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (!string.IsNullOrEmpty(txtTabTitles.Text))
                    {
                        string[] tabtitles = txtTabTitles.Text.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        string firstcode = tabtitles[0].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries)[0];
                        string sql = "update dbo.[TS_SYS_ENTITY_FIELD] "
                            + "set [TAB_TITLE_CODE] = '" + firstcode + "'"
                            + ",[MODIFY_DATE] = GETDATE() "
                            + "where LEN(ISNULL([TAB_TITLE_CODE],'')) = 0 "
                            + "and [ENTITY_FID] = '" + info.Fid.GetValueOrDefault() + "'"
                            + "and [VALID_FLAG] = 1;";
                        CommonDAL.ExecuteNonQueryBySql(sql);
                    }
                    trans.Complete();
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DialogEntity_Load(object sender, EventArgs e)
        {
            LoadEntityType();
            LoadEntityInfo();
        }
    }
}
