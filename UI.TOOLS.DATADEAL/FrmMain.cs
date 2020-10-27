namespace UI.TOOLS.DATADEAL
{
    using BLL.LES;
    using BLL.SYS;
    using DAL.SYS;
    using DM.SYS;
    using Infrustructure.Linq;
    using Infrustructure.Utilities;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Transactions;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using Excel = Microsoft.Office.Interop.Excel;
    public partial class FrmMain : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            dgvFields.AutoGenerateColumns = false;
        }
        #endregion

        #region Variable
        /// <summary>
        /// 数据表名称集合
        /// </summary>
        private List<string> tableNames = new List<string>();
        /// <summary>
        /// 数据视图名称集合
        /// </summary>
        private List<string> viewNames = new List<string>();
        /// <summary>
        /// 选中的表名
        /// </summary>
        private string selectedTableName = string.Empty;
        /// <summary>
        /// 数据模型
        /// </summary>
        private EntityInfo entityInfo = null;
        /// <summary>
        /// 菜单链接
        /// </summary>
        private MenuInfo menuInfo = null;
        /// <summary>
        /// 检索条件
        /// </summary>
        private SearchModelInfo searchInfo = null;
        /// <summary>
        /// 登录用户
        /// </summary>
        private string loginUser = "UI.TOOLS.DATADEAL";
        /// <summary>
        /// 模型字段
        /// </summary>
        private List<EntityFieldInfo> entityFieldList = new List<EntityFieldInfo>();
        /// <summary>
        /// 搜索字段
        /// </summary>
        private List<SearchModelConditionInfo> searchConditionList = new List<SearchModelConditionInfo>();
        /// <summary>
        /// 获取所有字段脚本
        /// @TABLE_NAME = 表名
        /// </summary>
        private const string sqlFieldsAll = @"SELECT c.column_id AS DB_ID
                ,c.name AS FIELD_NAME
                ,TYPE_NAME(c.user_type_id) AS DATA_TYPE
                ,c.max_length AS LENGTH
                ,COLUMNPROPERTY( c.OBJECT_ID , c.Name ,'PRECISION') AS PRECISION
                ,p.Value AS [TITLE_CN]
                ,c.name AS [TITLE_EN] 
				, c.is_nullable AS IsNullable
                FROM sys.columns c 
                LEFT JOIN sys.table_types t ON t.type_table_object_id = c.object_ID 
                LEFT JOIN sys.extended_properties p ON (p.major_id = c.OBJECT_ID OR p.major_id = t.user_type_id) AND p.minor_id = c.column_id AND p.name = 'MS_Description' 
                WHERE COALESCE( t.name, OBJECT_NAME(c.OBJECT_ID)) = @TABLE_NAME 
                ORDER BY c.column_id;";
        /// <summary>
        /// 除标准字段之外
        /// 获取所有字段脚本
        /// @TABLE_NAME = 表名
        /// </summary>
        private string sqlFieldsExceptStandard = @"SELECT c.column_id AS DB_ID
                ,c.name AS FIELD_NAME
                ,TYPE_NAME(c.user_type_id) AS DATA_TYPE
                ,c.max_length AS LENGTH
                ,COLUMNPROPERTY( c.OBJECT_ID , c.Name ,'PRECISION') AS PRECISION
                ,p.Value AS [TITLE_CN]
                ,c.name AS [TITLE_EN] 
                , c.is_nullable AS IsNullable 
                FROM sys.columns c 
                LEFT JOIN sys.table_types t ON t.type_table_object_id = c.object_ID 
                LEFT JOIN sys.extended_properties p ON (p.major_id = c.OBJECT_ID OR p.major_id = t.user_type_id) AND p.minor_id = c.column_id AND p.name = 'MS_Description' 
                WHERE COALESCE( t.name, OBJECT_NAME(c.OBJECT_ID)) =@TABLE_NAME 
                and c.name not in ('" + string.Join("','", standardFields.ToArray()) + "') and (p.Value not in ('自增主键')  or p.value is null) ORDER BY c.column_id;";
        /// <summary>
        /// 数据库标准结构字段
        /// </summary>
        private static List<string> standardFields = new List<string>() { "ID", "NID", "FID", "PARENT_FID", "VALID_FLAG", "MODIFY_USER", "MODIFY_DATE", "UPDATE_DATE", "UPDATE_USER", "CREATE_DATE", "CREATE_USER" };
        #endregion

        #region MESSAGE
        /// <summary>
        /// ShowMessage
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            Invoke(new EventHandler(delegate
            {
                MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + message
                    , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //lblMessage.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + message;
                //lblMessage.ForeColor = Color.White;
                //lblMessage.BackColor = Color.ForestGreen;
            }));
        }
        /// <summary>
        /// ErrorMessage
        /// </summary>
        /// <param name="message"></param>
        private void ErrorMessage(string message)
        {
            Invoke(new EventHandler(delegate
            {
                MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + message
                    , "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ////lblMessage.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + message;
                ////lblMessage.ForeColor = Color.White;
                ////lblMessage.BackColor = Color.Red;
            }));
        }
        #endregion

        #region Table
        /// <summary>
        /// Table Select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnLoadTable_Click(object sender, EventArgs e)
        {
            int torv = 1;
            if (menuBtnLoadTable.Text.ToLower() == "view") torv = 2;
            DialogTables dialog = new DialogTables(torv);
            if (dialog.ShowDialog(this) != DialogResult.Yes)
            {
                dialog.Dispose();
                return;
            }
            menuTextTableName.Text = dialog.TableName;
            dialog.Dispose();
            LoadFieldGrid(menuTextTableName.Text);
        }
        /// <summary>
        /// 加载数据库表名
        /// </summary>
        private void LoadTableNames()
        {
            string sql = "select name from sys.tables order by name";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            tableNames.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                tableNames.Add(dr["name"].ToString());
            }
        }
        /// <summary>
        /// 加载数据库视图名
        /// </summary>
        private void LoadViewNames()
        {
            string sql = "select name from sys.views order by name";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            viewNames.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                viewNames.Add(dr["name"].ToString());
            }
        }
        /// <summary>
        /// Text TableName KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuTextTableName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13) return;
            string tableviewname = string.Empty;
            if (menuComboTorV.SelectedIndex == 0)
                tableviewname = tableNames.FirstOrDefault(d => d.ToLower() == menuTextTableName.Text.ToLower());
            else
                tableviewname = viewNames.FirstOrDefault(d => d.ToLower() == menuTextTableName.Text.ToLower());
            menuTextTableName.Text = tableviewname;
            LoadFieldGrid(tableviewname);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        private void LoadFieldGrid(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                ErrorMessage("必须先填写正确的数据库表名");
                ClearForm();
                return;
            }
            ShowMessage(string.Empty);
            selectedTableName = tableName;
            string entityName = GetEntityNameByTableName(tableName);
            string getFieldsMethod = AppSettings.GetConfigString("GetFieldsMethod");
            string sql = "SELECT c.column_id AS DB_ID"
                + ",c.name AS FIELD_NAME"
                + ",TYPE_NAME(c.user_type_id) AS DATA_TYPE"
                + ",c.max_length AS LENGTH"
                + ",COLUMNPROPERTY( c.OBJECT_ID , c.Name ,'PRECISION') AS PRECISION"
                + ",p.Value AS [TITLE_CN]"
                + ",c.name AS [TITLE_EN] "
                + "FROM sys.columns c "
                + "LEFT JOIN sys.table_types t ON t.type_table_object_id = c.object_ID "
                + "LEFT JOIN sys.extended_properties p ON (p.major_id = c.OBJECT_ID OR p.major_id = t.user_type_id) AND p.minor_id = c.column_id AND p.name = 'MS_Description' "
                + "WHERE COALESCE( t.name, OBJECT_NAME(c.OBJECT_ID)) =@TABLE_NAME "
                + "and c.name not in ('ID','NID','FID','PARENT_FID','VALID_FLAG','MODIFY_USER','MODIFY_DATE','UPDATE_DATE','UPDATE_USER') "
                + "and (p.Value not in ('自增主键')  or p.value is null)"
                + "ORDER BY c.column_id;";
            if (getFieldsMethod.ToLower() == "kt")
                sql = @"select 
DB_ID = a.colorder,
FIELD_NAME = a.name, 
DATA_TYPE = k.name, 
--Tab_index = case when exists(SELECT name FROM sysindexes WHERE id = (select id from sysobjects where name=d.name) and indid<>'0' and name=a.name)  then '1' else '0' end, 
LENGTH = COL_LENGTH(d.name,a.name), 
PRECISION = COLUMNPROPERTY( OBJECT_ID(d.name),a.name,'PRECISION'), 
--Col_scale = isnull(COLUMNPROPERTY( OBJECT_ID(d.name),a.name,'Scale'),0), 
TITLE_CN = left(dbo.f_get_des_sql(d.name,a.name,'MS_Description'),case when CHARINDEX(',',dbo.f_get_des_sql(d.name,a.name,'MS_Description'))>0 then CHARINDEX(',',dbo.f_get_des_sql(d.name,a.name,'MS_Description'))-1 else 0 end  ), 
--Note = dbo.f_get_des_sql(d.name,a.name,'MS_Description'), 
TITLE_EN = left(dbo.f_get_des_sql(d.name,a.name,'US_Description'),case when CHARINDEX(',',dbo.f_get_des_sql(d.name,a.name,'US_Description'))>0 then CHARINDEX(',',dbo.f_get_des_sql(d.name,a.name,'US_Description'))-1 else 0 end  )
--,US_Des = dbo.f_get_des_sql(d.name,a.name,'US_Description'), 
--Empty = COLUMNPROPERTY( OBJECT_ID(d.name),a.name,'AllowsNull'), 
---Col_Deflt = replace(replace(convert(varchar(200),isnull(e.text,'')),'CREATE DEFAULT con_empty AS ',''),'CREATE DEFAULT con_zero AS ',''), 
---Endspace = case when A.status & 16 = 0 then '0' else '1' end, 
---tab_Key = case when a.name='nid'and exists(select id from dbo.sysobjects where id = object_id('PK_'+d.name)) then '1' else '0' end
FROM syscolumns a 
inner join sysobjects d on a.id=d.id and d.xtype='U' and  d.name<>'dtproperties' --U = 用户表 排出dtproperties
left join syscomments e on a.cdefault=e.id 
inner join systypes k on k.xtype = a.xtype 
WHERE d.name =@TABLE_NAME 
AND a.name not in ('ID','NID','FID','PARENT_FID','VALID_FLAG','CREATE_USER','CREATE_DATE','MODIFY_USER','MODIFY_DATE') 
order by DB_ID";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@TABLE_NAME", DbType.AnsiString, tableName);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dtFields = ds.Tables[0];
            #region Load Entity
            entityInfo = new EntityBLL().GetInfo(entityName, tableName);
            entityFieldList = new List<EntityFieldInfo>();
            if (entityInfo != null)
            {
                int dataCount;
                entityFieldList = new EntityFieldBLL().GetListByPage("and [VALID_FLAG] = 1 "
                    + "and [ENTITY_FID] = '" + entityInfo.Fid.GetValueOrDefault() + "'"
                    , "[DISPLAY_ORDER]"
                    , 1
                    , int.MaxValue
                    , out dataCount);
            }
            #endregion
            string linkUrl = GetLinkUrlByTableName(tableName);
            #region Load Menu
            menuInfo = new MenuBLL().GetInfo(linkUrl);
            #endregion
            #region Load Search
            searchInfo = new SearchModelBLL().GetInfo(entityName);
            int columnlength = 0;
            searchConditionList = new SearchModelBLL().GetSearchConditionsByName(entityName, out columnlength);
            #endregion

            List<TableField> tableFields = new List<TableField>();
            foreach (DataRow dr in dtFields.Rows)
            {
                TableField info = new TableField();
                info.dbId = int.Parse(dr["DB_ID"].ToString());
                info.fieldName = dr["FIELD_NAME"].ToString();
                info.dataType = dr["DATA_TYPE"].ToString();
                info.maxLength = int.Parse(dr["LENGTH"].ToString());
                info.precision = int.Parse(dr["PRECISION"].ToString());
                info.description = dr["TITLE_CN"].ToString();
                info.description_en = dr["TITLE_EN"].ToString();
                EntityFieldInfo entityfieldinfo = entityFieldList.FirstOrDefault(d => d.TableFieldName == info.fieldName);
                info.isEntity = entityfieldinfo == null ? false : true;
                info.isList = entityfieldinfo == null ? false : entityfieldinfo.Listable.GetValueOrDefault();
                info.isForm = entityfieldinfo == null ? false : entityfieldinfo.Editable.GetValueOrDefault();
                info.isExcel = entityfieldinfo == null ? false : entityfieldinfo.ExportExcelFlag.GetValueOrDefault();
                SearchModelConditionInfo conditioninfo = searchConditionList.FirstOrDefault(d => d.ColumnName == info.fieldName);
                info.isSearch = conditioninfo == null ? false : true;
                tableFields.Add(info);
            }
            dgvFields.DataSource = null;
            dgvFields.DataSource = tableFields;
            dgvFields.ClearSelection();
            LoadStatus();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private List<TableField> GetFieldsDataSource(string tableName, string sqlFields = sqlFieldsAll)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sqlFieldsAll);
            db.AddInParameter(cmd, "@TABLE_NAME", DbType.AnsiString, tableName);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dtFields = ds.Tables[0];
            List<TableField> tableFields = new List<TableField>();
            foreach (DataRow dr in dtFields.Rows)
            {
                TableField info = new TableField();
                info.dbId = int.Parse(dr["DB_ID"].ToString());
                info.fieldName = dr["FIELD_NAME"].ToString();
                info.dataType = dr["DATA_TYPE"].ToString();
                info.maxLength = int.Parse(dr["LENGTH"].ToString());
                info.precision = int.Parse(dr["PRECISION"].ToString());
                info.description = dr["TITLE_CN"].ToString();
                info.description_en = dr["TITLE_EN"].ToString();
                info.isNull = bool.Parse(dr["IsNullable"].ToString());
                EntityFieldInfo entityfieldinfo = entityFieldList.FirstOrDefault(d => d.TableFieldName == info.fieldName);
                info.isEntity = entityfieldinfo == null ? false : true;
                info.isList = entityfieldinfo == null ? false : entityfieldinfo.Listable.GetValueOrDefault();
                info.isForm = entityfieldinfo == null ? false : entityfieldinfo.Editable.GetValueOrDefault();
                info.isExcel = entityfieldinfo == null ? false : entityfieldinfo.ExportExcelFlag.GetValueOrDefault();
                SearchModelConditionInfo conditioninfo = searchConditionList.FirstOrDefault(d => d.ColumnName == info.fieldName);
                info.isSearch = conditioninfo == null ? false : true;
                tableFields.Add(info);
            }
            return tableFields;
        }
        /// <summary>
        /// 从表名获取实体名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string GetEntityNameByTableName(string tableName)
        {
            string[] tableSplitNames = tableName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (tableSplitNames.Length < 2 && !tableSplitNames[0].StartsWith("T"))
                return tableName.Substring(0, 1).ToUpper() + tableName.Substring(1).ToLower();
            string entityName = string.Empty;
            for (int i = 2; i < tableSplitNames.Length; i++)
            {
                entityName += tableSplitNames[i].Substring(0, 1).ToUpper() + tableSplitNames[i].Substring(1).ToLower();
            }
            return entityName;
        }
        /// <summary>
        /// 通过表名获取LINK_URL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string GetLinkUrlByTableName(string tableName)
        {
            string entityname = GetEntityNameByTableName(tableName);
            string[] tableSplitNames = tableName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            string selectedMouldName = menuComboMould.Text;
            if (tableSplitNames.Length > 2
                && tableSplitNames[1].ToLower() == selectedMouldName.ToLower()
                && tableSplitNames[0].StartsWith("T"))
                return "CommonList.aspx?" + entityname + "&" + tableName;
            string linkUrl = "CommonList.aspx?" + entityname + "&" + tableName + "&" + selectedMouldName;
            string keyFieldUrl = string.Empty;
            string keyLengthUrl = string.Empty;
            foreach (var dicItem in GetKeyFieldsByTableName(tableName, false))
            {
                keyFieldUrl += "|" + dicItem.Key;
                keyLengthUrl += "|" + dicItem.Value;
            }
            if (!string.IsNullOrEmpty(keyFieldUrl)) keyFieldUrl = "&" + keyFieldUrl.Substring(1);
            if (!string.IsNullOrEmpty(keyLengthUrl)) keyLengthUrl = "&" + keyLengthUrl.Substring(1);
            return linkUrl + keyFieldUrl + keyLengthUrl;
        }
        /// <summary>
        /// 根据表名获取其主键类型及字段名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private Dictionary<string, int> GetKeyFieldsByTableName(string tableName, bool isDatabaseFlag)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            string sql = @"With Entities AS
( 
SELECT 
	DB_NAME() AS [Database]
	,COALESCE( SCHEMA_NAME(T.Schema_id), OBJECT_SCHEMA_NAME(C.object_id)) AS Owner
	,COALESCE( T.Name, OBJECT_NAME(c.OBJECT_ID)) AS Parent
	,C.column_id AS OrdinalPosition	
	,C.name AS Name
	,TYPE_NAME(c.user_type_id) AS DataType
	,D.definition AS DefaultSetting
	, C.is_nullable AS IsNullable
	, C.max_length AS MaxLength
	, COLUMNPROPERTY( C.OBJECT_ID , C.Name ,'PRECISION') AS [Precision] -- 用于判断 NVARCHAR 实际长度的
	--,C.Object_id, P.major_id, P.minor_id
	,P.Value AS [DESC]
	,C.is_identity as IsIdentity	
	,C.column_id
	,C.object_id	
FROM 
	sys.columns C	
	LEFT JOIN sys.table_types T ON T.type_table_object_id = C.object_ID
	LEFT JOIN sys.default_constraints D ON C.object_id = D.parent_object_id AND D.parent_column_id = C.Column_id
	LEFT JOIN sys.extended_properties P ON (P.major_id = C.OBJECT_ID OR P.major_id = T.User_type_id ) AND P.minor_id = C.COLUMN_ID AND P.name = 'MS_Description'	
WHERE
	COALESCE( SCHEMA_NAME(T.Schema_id), OBJECT_SCHEMA_NAME(C.object_id)) in ('dbo','LES','GJS')	
)
SELECT E.*
	,isnull(IC.is_unique,0) as IsUnique
	,isnull(IC.is_primary_key,0) as IsPrimaryKey
FROM Entities E
LEFT JOIN
(SELECT a.object_Id, a.index_id,b.column_id,a.is_unique,a.is_primary_key FROM sys.indexes a, sys.index_columns b
	WHERE a.object_id=b.object_id AND a.is_primary_key =1
	AND a.index_id=b.index_id) IC
	ON E.object_id = IC.object_id AND E.column_id = IC.column_id
where Parent = @TABLE_NAME and is_primary_key = 1";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@TABLE_NAME", DbType.AnsiString, tableName);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dtFields = ds.Tables[0];
            foreach (DataRow dr in dtFields.Rows)
            {
                string dataType = dr["DataType"].ToString();
                string name = dr["Name"].ToString();
                int dataTypeInt = 0;
                switch (dataType)
                {
                    case "varchar":
                    case "nvarchar": dataTypeInt = 512; break;
                    case "int": dataTypeInt = 32; break;
                    case "bigint": dataTypeInt = 64; break;
                    case "uniqueidentifier": dataTypeInt = 36; break;
                }
                dic.Add(isDatabaseFlag ? name : GetPropNameByFieldName(name), dataTypeInt);
            }
            return dic;
        }

        /// <summary>
        /// Data Binding Complete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFields_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvFields.Rows)
            {
                TableField info = row.DataBoundItem as TableField;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = Color.White;
                    cell.Style.ForeColor = Color.Black;
                    if (cell.ColumnIndex == 7)
                    {
                        if (info.isEntity)
                        {
                            cell.Value = true;
                            cell.Style.BackColor = Color.ForestGreen;
                            cell.Style.ForeColor = Color.White;
                        }
                        else
                        {
                            cell.Value = false;
                        }
                    }
                    if (cell.ColumnIndex == 8)
                    {
                        if (info.isList)
                        {
                            cell.Value = true;
                            cell.Style.BackColor = Color.ForestGreen;
                            cell.Style.ForeColor = Color.White;
                        }
                        else
                        {
                            cell.Value = false;
                        }
                    }
                    if (cell.ColumnIndex == 9)
                    {
                        if (info.isForm)
                        {
                            cell.Value = true;
                            cell.Style.BackColor = Color.ForestGreen;
                            cell.Style.ForeColor = Color.White;
                        }
                        else
                        {
                            cell.Value = false;
                        }
                    }
                    if (cell.ColumnIndex == 10)
                    {
                        if (info.isSearch)
                        {
                            cell.Value = true;
                            cell.Style.BackColor = Color.ForestGreen;
                            cell.Style.ForeColor = Color.White;
                        }
                        else
                        {
                            cell.Value = false;
                        }
                    }
                    if (cell.ColumnIndex == 11)
                    {
                        if (info.isExcel)
                        {
                            cell.Value = true;
                            cell.Style.BackColor = Color.ForestGreen;
                            cell.Style.ForeColor = Color.White;
                        }
                        else
                        {
                            cell.Value = false;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 清空界面
        /// </summary>
        private void ClearForm()
        {
            entityInfo = null;
            menuInfo = null;
            searchInfo = null;
            dgvFields.DataSource = null;
            menuTextTableName.Text = string.Empty;
            selectedTableName = string.Empty;
            LoadStatus();
        }
        #endregion

        #region Entity
        /// <summary>
        /// Create or Load Entity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnCreateEntity_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedTableName))
            {
                ErrorMessage("必须先填写数据库表名");
                return;
            }
            ///
            string entityName = GetEntityNameByTableName(selectedTableName);
            DialogInput dialogInput = new DialogInput("请输入数据模型名称", entityName);
            if (dialogInput.ShowDialog(this) != DialogResult.Yes)
            {
                ErrorMessage("必须先键入数据模型名称");
                return;
            }
            entityName = dialogInput.InputMsg;
            dialogInput.Dispose();
            if (string.IsNullOrEmpty(entityName))
            {
                ErrorMessage("必须先键入数据模型名称");
                return;
            }
            entityInfo = new EntityBLL().GetInfo(entityName, string.Empty);
            if (entityInfo == null)
            {
                entityInfo = new EntityInfo();
                entityInfo.Fid = Guid.NewGuid();
                entityInfo.EntityName = entityName;
                entityInfo.TableNames = selectedTableName;

                List<StringValueDatasourceInfo> cditems = new List<StringValueDatasourceInfo>();
                foreach (var dicitem in GetKeyFieldsByTableName(selectedTableName, true))
                {
                    StringValueDatasourceInfo cditem = new StringValueDatasourceInfo();
                    cditem.ItemDisplay = dicitem.Key;
                    cditem.StringValue = dicitem.Key;
                    cditems.Add(cditem);
                }
                DialogCombobox dialogCombo = new DialogCombobox(cditems);
                if (dialogCombo.ShowDialog(this) != DialogResult.Yes) return;
                entityInfo.DefaultSort = dialogCombo.SelectedValue + "-asc";///KT默认为NID倒排序
                dialogCombo.Dispose();
                entityInfo.Comments = string.Empty;
                entityInfo.ValidFlag = true;
                entityInfo.CreateUser = loginUser;
                entityInfo.CreateDate = DateTime.Now;
                entityInfo.EntityType = 1;
                if (new EntityBLL().InsertInfo(entityInfo) == 0)
                {
                    ErrorMessage(selectedTableName + "已经生成实体类失败");
                    ClearForm();
                    return;
                }
            }
            entityInfo = new EntityBLL().GetInfo(entityName, this.menuTextTableName.ToString().Trim());
            ShowMessage(selectedTableName + "实体类数据读取成功");
            
            LoadStatus();

            DialogEntity dialog = new DialogEntity(entityInfo);
            dialog.ShowDialog(this);
            dialog.Dispose();
        }
        #endregion

        #region Field
        /// <summary>
        /// Create All Field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnCreateFieldAll_Click(object sender, EventArgs e)
        {
            if (entityInfo == null)
            {
                ErrorMessage("必须先读取实体类数据");
                return;
            }
            int dataCount = new EntityFieldBLL().GetCounts("and [VALID_FLAG] = 1 "
                + "and [ENTITY_FID] = '" + entityInfo.Fid.GetValueOrDefault() + "'");
            if (dataCount > 0)
            {
                ErrorMessage("已生成过对应的字段数据");
                return;
            }
            if (dgvFields.Rows.Count == 0)
            {
                ErrorMessage("必须先读取数据库表结构");
                return;
            }
            List<EntityFieldInfo> fieldlist = new List<EntityFieldInfo>();
            foreach (DataGridViewRow row in dgvFields.Rows)
            {
                TableField info = row.DataBoundItem as TableField;
                EntityFieldInfo fieldinfo = new EntityFieldInfo();
                fieldinfo.Fid = Guid.NewGuid();
                fieldinfo.EntityFid = entityInfo.Fid;
                fieldinfo.FieldName = GetPropNameByFieldName(info.fieldName);///对象的属性
                fieldinfo.TableFieldName = info.fieldName;///数据库字段
                fieldinfo.DisplayNameCn = GetFieldDescription(info.description);
                fieldinfo.DisplayNameEn = info.description_en;
                fieldinfo.DisplayOrder = info.dbId * 10;
                switch (row.Cells["cDATA_TYPE"].Value.ToString().ToLower())
                {
                    case "int": fieldinfo.DataType = 20; fieldinfo.ControlType = 70; break;
                    case "bit": fieldinfo.DataType = 30; fieldinfo.ControlType = 100; break;
                    case "datetime": fieldinfo.DataType = 40; fieldinfo.ControlType = 60; break;
                    case "date": fieldinfo.DataType = 50; fieldinfo.ControlType = 50; break;
                    case "numeric": fieldinfo.DataType = 60; fieldinfo.ControlType = 70; break;
                    default: fieldinfo.DataType = 10; fieldinfo.ControlType = 10; break;///string
                }
                fieldinfo.DataLength = info.maxLength;
                fieldinfo.Nullenable = true;
                DataGridViewCheckBoxCell checkForm = row.Cells["colForm"] as DataGridViewCheckBoxCell;
                fieldinfo.Editable = Convert.ToBoolean(checkForm.EditingCellFormattedValue);
                fieldinfo.EditDisplayWidth = "220";
                DataGridViewCheckBoxCell checkList = row.Cells["colLIST"] as DataGridViewCheckBoxCell;
                fieldinfo.Listable = Convert.ToBoolean(checkList.EditingCellFormattedValue);
                fieldinfo.ListDisplayWidth = "100";
                fieldinfo.Regex = string.Empty;
                fieldinfo.ValidFlag = true;
                fieldinfo.CreateDate = DateTime.Now;
                fieldinfo.CreateUser = loginUser;
                DataGridViewCheckBoxCell checkExcel = row.Cells["colExcel"] as DataGridViewCheckBoxCell;
                fieldinfo.ExportExcelFlag = Convert.ToBoolean(checkExcel.EditingCellFormattedValue);
                fieldlist.Add(fieldinfo);
            }
            using (var trans = new TransactionScope())
            {
                foreach (var fieldinfo in fieldlist)
                {
                    if (new EntityFieldBLL().InsertInfo(fieldinfo) == 0)
                    {
                        ErrorMessage("插入字段数据失败");
                        return;
                    }
                }
                trans.Complete();
            }
            entityFieldList.Clear();
            entityFieldList = fieldlist;
            ShowMessage("已成功生成" + entityInfo.EntityName + "所有字段对应的数据");
            LoadFieldGrid(entityInfo.TableNames);
        }
        /// <summary>
        /// Create Field Selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnCreateFieldSelected_Click(object sender, EventArgs e)
        {
            if (entityInfo == null)
            {
                ErrorMessage("请先获取实体类数据");
                return;
            }
            if (dgvFields.SelectedRows.Count == 0)
            {
                ErrorMessage("请先选择需要生成的字段");
                return;
            }
            List<TableField> list = new List<TableField>();
            foreach (DataGridViewRow row in dgvFields.SelectedRows)
            {
                TableField info = row.DataBoundItem as TableField;
                list.Add(info);
            }
            if (list.Count == 0)
            {
                ErrorMessage("请先选择需要生成的字段");
                return;
            }
            int dataCount = new EntityFieldBLL().GetCounts("and [VALID_FLAG] = 1 "
                + "and [ENTITY_FID] = '" + entityInfo.Fid.GetValueOrDefault() + "' "
                + "and [TABLE_FIELD_NAME] in ('" + string.Join("','", list.Select(d => d.fieldName).ToArray()) + "')");
            if (dataCount > 0)
            {
                ErrorMessage("已生成过对应的字段数据");
                return;
            }
            List<EntityFieldInfo> fieldlist = new List<EntityFieldInfo>();
            foreach (var info in list)
            {
                EntityFieldInfo fieldinfo = new EntityFieldInfo();
                fieldinfo.Fid = Guid.NewGuid();
                fieldinfo.EntityFid = entityInfo.Fid;
                fieldinfo.FieldName = GetPropNameByFieldName(info.fieldName);
                fieldinfo.TableFieldName = info.fieldName;
                fieldinfo.DisplayNameCn = GetFieldDescription(info.description);
                fieldinfo.DisplayNameEn = info.description_en;
                fieldinfo.DisplayOrder = info.dbId * 10;
                fieldinfo.DataType = 10;
                fieldinfo.ControlType = 10;
                fieldinfo.DataLength = info.maxLength;
                fieldinfo.Nullenable = true;
                fieldinfo.Editable = true;
                fieldinfo.EditDisplayWidth = "220";
                fieldinfo.Listable = true;
                fieldinfo.ListDisplayWidth = "100";
                fieldinfo.Regex = string.Empty;
                fieldinfo.ValidFlag = true;
                fieldinfo.CreateDate = DateTime.Now;
                fieldinfo.CreateUser = loginUser;
                fieldlist.Add(fieldinfo);
            }
            using (var trans = new TransactionScope())
            {
                foreach (var fieldinfo in fieldlist)
                {
                    if (new EntityFieldBLL().InsertInfo(fieldinfo) == 0)
                    {
                        ErrorMessage("插入字段数据失败");
                        return;
                    }
                }
                trans.Complete();
            }
            entityFieldList.Clear();
            entityFieldList = fieldlist;
            ShowMessage("已成功生成" + entityInfo.EntityName + "所有字段对应的数据");
            LoadFieldGrid(entityInfo.TableNames);
        }
        /// <summary>
        /// 根据数据库字段名称获取对象属性名称
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private string GetPropNameByFieldName(string fieldName)
        {
            string[] fieldnames = fieldName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            string propName = string.Empty;
            for (int i = 0; i < fieldnames.Length; i++)
            {
                propName += fieldnames[i].Substring(0, 1).ToUpper() + fieldnames[i].Substring(1).ToLower();
            }
            return propName;
        }
        /// <summary>
        /// 获取中文描述
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        private string GetFieldDescription(string description)
        {
            if (description == "")
            {
                return "";
            }
            string[] descs = description.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
            return descs[0];
        }
        #endregion

        #region Menu
        /// <summary>
        /// Menu Action Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnMenu_Click(object sender, EventArgs e)
        {
            #region IF
            if (entityInfo == null)
            {
                ErrorMessage("必须先加载实体类数据");
                return;
            }
            if (menuInfo != null)
            {
                ErrorMessage(entityInfo.EntityName + "已存在菜单项");
                return;
            }
            #endregion
            #region 确认功能模块
            DialogMenu dialog = new DialogMenu();
            if (dialog.ShowDialog(this) != DialogResult.Yes)
            {
                dialog.Dispose();
                return;
            }
            Guid parentMenuFid = dialog.ParentMenuFid;
            #endregion
            #region 确认菜单名称
            DialogInput dialog2 = new DialogInput("请输入菜单名称");
            if (dialog2.ShowDialog(this) != DialogResult.Yes)
            {
                dialog2.Dispose();
                return;
            }
            string menuNameCn = dialog2.InputMsg;
            dialog2.Dispose();
            #endregion
            #region 确定所选内容
            if (MessageBox.Show("您确定将[" + menuNameCn + "]作为菜单名称\r\n并将[" + menuComboMould.Text + "]作为命名空间吗?"
                , "提示"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1)
                == DialogResult.No)
            {
                return;
            }
            #endregion
            List<MenuInfo> menulist = new List<MenuInfo>();
            List<MenuActionInfo> actionlist = new List<MenuActionInfo>();
            #region List Menu
            MenuInfo listinfo = new MenuInfo();
            listinfo.Fid = Guid.NewGuid();
            listinfo.ParentMenuFid = parentMenuFid;
            listinfo.MenuName = entityInfo.EntityName;
            listinfo.DisplayOrder = 0;
            listinfo.ImageUrl = "icon-ok";
            listinfo.LinkUrl = GetLinkUrlByTableName(entityInfo.TableNames);
            listinfo.MenuType = 10;///功能菜单
            listinfo.NeedAuth = false;
            listinfo.ValidFlag = true;
            listinfo.CreateDate = DateTime.Now;
            listinfo.CreateUser = loginUser;
            listinfo.MenuNameCn = menuNameCn;
            menulist.Add(listinfo);
            #endregion
            #region Search Action
            if (dialog.ckSearch.Checked)
            {
                MenuActionInfo searchaction = new MenuActionInfo();
                searchaction.Fid = Guid.NewGuid();
                searchaction.MenuFid = listinfo.Fid;
                searchaction.ActionFid = Guid.Parse("059CB9A5-17D2-41C8-874F-D57611065490");
                searchaction.ClientJs = "search()";
                searchaction.ActionOrder = 90;
                searchaction.NeedAuth = false;
                searchaction.ValidFlag = true;
                searchaction.CreateDate = DateTime.Now;
                searchaction.CreateUser = loginUser;
                actionlist.Add(searchaction);
            }
            #endregion
            #region Add Action
            if (dialog.ckAdd.Checked)
            {
                MenuActionInfo addaction = new MenuActionInfo();
                addaction.Fid = Guid.NewGuid();
                addaction.MenuFid = listinfo.Fid;
                addaction.ActionFid = Guid.Parse("FD25066E-4EC9-4D75-9F6B-762139E3B435");
                addaction.ClientJs = "add()";
                addaction.ActionOrder = 10;
                addaction.NeedAuth = false;
                addaction.ValidFlag = true;
                addaction.CreateDate = DateTime.Now;
                addaction.CreateUser = loginUser;
                actionlist.Add(addaction);
            }
            #endregion
            #region Edit Action
            if (dialog.ckEdit.Checked)
            {
                MenuActionInfo editaction = new MenuActionInfo();
                editaction.Fid = Guid.NewGuid();
                editaction.MenuFid = listinfo.Fid;
                editaction.ActionFid = Guid.Parse("A3836DB1-E6D2-4BF0-8438-BED018794EE0");
                editaction.ClientJs = "editrows()";
                editaction.ActionOrder = 20;
                editaction.NeedAuth = false;
                editaction.ValidFlag = true;
                editaction.CreateDate = DateTime.Now;
                editaction.CreateUser = loginUser;
                actionlist.Add(editaction);
            }
            #endregion
            #region Del Action
            if (dialog.ckDel.Checked)
            {
                MenuActionInfo delaction = new MenuActionInfo();
                delaction.Fid = Guid.NewGuid();
                delaction.MenuFid = listinfo.Fid;
                delaction.ActionFid = Guid.Parse("CE7B9C8D-E9F2-48DB-ADBE-8089F574D0C0");
                delaction.ClientJs = "del()";
                delaction.ActionOrder = 30;
                delaction.NeedAuth = false;
                delaction.ValidFlag = true;
                delaction.CreateDate = DateTime.Now;
                delaction.CreateUser = loginUser;
                actionlist.Add(delaction);
            }
            #endregion
            #region Form Menu
            MenuInfo forminfo = null;
            if (dialog.ckForm.Checked)
            {
                forminfo = new MenuInfo();
                forminfo.Fid = Guid.NewGuid();
                forminfo.ParentMenuFid = listinfo.Fid;
                forminfo.MenuName = entityInfo.EntityName;
                forminfo.DisplayOrder = 10;
                forminfo.ImageUrl = "icon-blank";
                forminfo.LinkUrl = "CommonEdit.aspx?" + entityInfo.EntityName + "&createForm";
                forminfo.MenuType = 20;
                forminfo.NeedAuth = false;
                forminfo.ValidFlag = true;
                forminfo.CreateDate = DateTime.Now;
                forminfo.CreateUser = loginUser;
                forminfo.MenuNameCn = menuNameCn;
                forminfo.EditFormWidth = 800;
                forminfo.EditFormHeight = 600;
                menulist.Add(forminfo);
            }
            #endregion
            #region Save Action
            if (dialog.ckSave.Checked)
            {
                if (forminfo == null)
                {
                    ErrorMessage("创建保存功能时表单是必选项");
                    return;
                }
                MenuActionInfo saveaction = new MenuActionInfo();
                saveaction.Fid = Guid.NewGuid();
                saveaction.MenuFid = forminfo.Fid;
                saveaction.ActionFid = Guid.Parse("6C1C705D-6C1B-472B-94E9-34288F9ACF52");
                saveaction.ClientJs = "defaultSave()";
                saveaction.ActionOrder = 10;
                saveaction.NeedAuth = false;
                saveaction.ValidFlag = true;
                saveaction.CreateDate = DateTime.Now;
                saveaction.CreateUser = loginUser;
                actionlist.Add(saveaction);
            }
            #endregion

            using (var trans = new TransactionScope())
            {
                foreach (var menuinfo in menulist)
                {
                    if (new MenuBLL().InsertInfo(menuinfo) == 0)
                    {
                        ErrorMessage("添加菜单失败");
                        return;
                    }
                }
                foreach (var actioninfo in actionlist)
                {
                    if (new MenuActionBLL().InsertInfo(actioninfo) == 0)
                    {
                        ErrorMessage("添加功能失败");
                        return;
                    }
                }
                trans.Complete();
            }
            ShowMessage("添加菜单、功能成功");
            menuInfo = listinfo;
            LoadStatus();
            dialog.Dispose();
        }
        #endregion

        #region Search
        /// <summary>
        /// Search Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnSearchLoad_Click(object sender, EventArgs e)
        {
            if (entityInfo == null)
            {
                ErrorMessage("请首先创建数据模型");
                return;
            }
            if (menuInfo == null)
            {
                ErrorMessage("请首先创建菜单项");
                return;
            }
            if (searchInfo == null)
            {
                searchInfo = new SearchModelInfo();
                searchInfo.Fid = Guid.NewGuid();
                searchInfo.SearchName = entityInfo.EntityName;
                searchInfo.SearchType = 10;
                searchInfo.ColumnLength = 3;
                searchInfo.ValidFlag = true;
                searchInfo.CreateDate = DateTime.Now;
                searchInfo.CreateUser = loginUser;
                searchInfo.Id = new SearchModelBLL().InsertInfo(searchInfo);
                if (searchInfo.Id == 0)
                {
                    ErrorMessage("搜索模型添加失败");
                    searchInfo = null;
                    return;
                }
            }
            ShowMessage("成功获取搜索模型");
            LoadStatus();
        }
        /// <summary>
        /// Create Condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnSearchCreate_Click(object sender, EventArgs e)
        {
            if (entityInfo == null)
            {
                ErrorMessage("请首先创建数据模型");
                return;
            }
            if (searchInfo == null)
            {
                ErrorMessage("请先获取搜素模型数据");
                return;
            }
            foreach (DataGridViewRow row in dgvFields.Rows)
            {
                DataGridViewCheckBoxCell checkSearch = row.Cells["colSearch"] as DataGridViewCheckBoxCell;
                if (!Convert.ToBoolean(checkSearch.EditingCellFormattedValue)) continue;
                TableField info = row.DataBoundItem as TableField;
                int dataCount = new SearchModelConditionBLL().GetCounts("and [VALID_FLAG] = 1 "
                    + "and [SEARCH_FID] = '" + searchInfo.Fid.GetValueOrDefault() + "' "
                    + "and [COLUMN_NAME] = '" + info.fieldName + "'");
                if (dataCount > 0) continue;
                ///EntityFieldInfo
                EntityFieldInfo entityfieldinfo = new EntityFieldBLL().GetInfo(entityInfo.EntityName, GetPropNameByFieldName(info.fieldName));
                ///SearchModelConditionInfo
                SearchModelConditionInfo conditioninfo = new SearchModelConditionInfo();
                ///逻辑主键
                conditioninfo.Fid = Guid.NewGuid();
                ///外键
                conditioninfo.SearchFid = searchInfo.Fid;
                ///属性名
                conditioninfo.ControlId = GetPropNameByFieldName(info.fieldName);
                ///表
                conditioninfo.TableName = entityInfo.TableNames;
                ///字段
                conditioninfo.ColumnName = info.fieldName;
                ///匹配方式，默认为模糊匹配50
                conditioninfo.DatasearchType = "50";
                ///VALID_FLAG
                conditioninfo.ValidFlag = true;
                ///CREATE_DATE
                conditioninfo.CreateDate = DateTime.Now;
                ///CREATE_USER
                conditioninfo.CreateUser = loginUser;
                if (entityfieldinfo == null)
                {
                    ///默认控件类型为文本框
                    conditioninfo.ControlType = "10";
                    ///默认显示顺序为数据库字段顺序
                    conditioninfo.DisplayOrder = info.dbId * 10;
                    ///默认标题为数据库字段描述
                    conditioninfo.LabelText = GetFieldDescription(info.description);
                    ///默认数据类型为nvarchar
                    conditioninfo.ColumnType = "10";
                    ///默认正则表达式无
                    conditioninfo.RegexExpression = string.Empty;
                }
                else
                {
                    ///控件类型
                    conditioninfo.ControlType = entityfieldinfo.ControlType.GetValueOrDefault().ToString();
                    ///显示顺序
                    conditioninfo.DisplayOrder = entityfieldinfo.DisplayOrder;
                    ///默认值
                    conditioninfo.DefaultValue = entityfieldinfo.DefaultValue;
                    ///字段长度
                    conditioninfo.MaxLength = entityfieldinfo.DataLength;
                    ///显示标题
                    conditioninfo.LabelText = entityfieldinfo.DisplayNameCn;
                    ///数据类型
                    conditioninfo.ColumnType = entityfieldinfo.DataType.GetValueOrDefault().ToString();
                    ///正则表达式
                    conditioninfo.RegexExpression = entityfieldinfo.Regex;
                    ///系统代码
                    conditioninfo.CodeName = entityfieldinfo.Extend1;
                    ///COMBO配置
                    conditioninfo.ExtendField3 = entityfieldinfo.Extend3;
                    ///权限配置
                    conditioninfo.ExtendField2 = entityfieldinfo.Extend2;
                    ///正则校验错误信息
                    conditioninfo.ExtendField4 = entityfieldinfo.ErrorMsg;
                    ///数值最小值
                    conditioninfo.ExtendField5 = entityfieldinfo.MinValue.GetValueOrDefault().ToString();
                    ///数值最大值
                    conditioninfo.ExtendField6 = entityfieldinfo.MaxValue.GetValueOrDefault().ToString();
                    ///数值小数位数
                    conditioninfo.ExtendField7 = entityfieldinfo.Precision.GetValueOrDefault().ToString();
                }
                if (new SearchModelConditionBLL().InsertInfo(conditioninfo) == 0)
                {
                    ErrorMessage("生成检索条件数据失败");
                    return;
                }
            }
            ShowMessage("生成检索条件数据成功");
            LoadFieldGrid(entityInfo.TableNames);
        }
        #endregion

        #region Export
        /// <summary>
        /// BLL.cs导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnBllFile_Click(object sender, EventArgs e)
        {
            if (entityInfo == null)
            {
                ErrorMessage("请先生成数据模型");
                return;
            }
            string spaceName = menuComboMould.Text.ToUpper();
            string entityName = entityInfo.EntityName;
            DialogBLL dialogbll = new DialogBLL();
            if (dialogbll.ShowDialog(this) != DialogResult.Yes)
                return;
            Dictionary<string, int> dic = GetKeyFieldsByTableName(entityInfo.TableNames, false);
            string aloneKeyType = "bool";
            string keyFieldParamString = string.Empty;
            string keyFieldString = string.Empty;
            foreach (var dicitem in dic)
            {
                switch (dicitem.Value)
                {
                    case 512: keyFieldParamString += ",string " + dicitem.Key.Substring(0, 1).ToLower() + dicitem.Key.Substring(1) + ""; break;
                    case 32: keyFieldParamString += ",int " + dicitem.Key.Substring(0, 1).ToLower() + dicitem.Key.Substring(1) + ""; aloneKeyType = "int"; break;
                    case 64: keyFieldParamString += ",long " + dicitem.Key.Substring(0, 1).ToLower() + dicitem.Key.Substring(1) + ""; aloneKeyType = "long"; break;
                    case 36: keyFieldParamString += ",Guid " + dicitem.Key.Substring(0, 1).ToLower() + dicitem.Key.Substring(1) + ""; break;
                }
                keyFieldString += "," + dicitem.Key.Substring(0, 1).ToLower() + dicitem.Key.Substring(1) + "";
            }
            if (dic.Count > 1) aloneKeyType = "bool";

            StringBuilder sb = new StringBuilder();
            ///using
            sb.AppendLine("using DAL." + spaceName + ";");
            sb.AppendLine("using DM." + spaceName + ";");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("");
            ///namespace
            sb.AppendLine("namespace BLL." + spaceName + "");
            sb.AppendLine("{");
            sb.AppendLine("    public class " + entityName + "BLL");
            sb.AppendLine("    {");
            sb.AppendLine("        #region Common");
            sb.AppendLine("        " + entityName + "DAL dal = new " + entityName + "DAL();");
            if (dialogbll.ckGetListByPage.Checked)
            {
                ///分页获取集合
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// 分页获取集合");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        /// <param name=\"textWhere\">string 条件语句,无须where</param>");
                sb.AppendLine("        /// <param name=\"textOrder\">string 排序语句,无须order by</param>");
                sb.AppendLine("        /// <param name=\"pageIndex\">int 页码,从1开始</param>");
                sb.AppendLine("        /// <param name=\"pageRow\">int 每页行数</param>");
                sb.AppendLine("        /// <param name=\"dataCount\">out int 数据行数</param>");
                sb.AppendLine("        /// <returns>List<" + entityName + "Info></returns>");
                sb.AppendLine("        public List<" + entityName + "Info> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)");
                sb.AppendLine("        {");
                sb.AppendLine("            dataCount = dal.GetCounts(textWhere);");
                sb.AppendLine("            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);");
                sb.AppendLine("        }");
                sb.AppendLine("");
            }
            if (dialogbll.ckGetInfo.Checked)
            {
                ///获取单行
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// SelectInfo");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        /// <param name=\"id\">主键</param>");
                sb.AppendLine("        /// <returns></returns>");
                sb.AppendLine("        public " + entityName + "Info SelectInfo(" + keyFieldParamString.Substring(1) + ")");
                sb.AppendLine("        {");
                sb.AppendLine("            return dal.GetInfo(" + keyFieldString.Substring(1) + ");");
                sb.AppendLine("        }");
                sb.AppendLine("");
            }
            if (dialogbll.ckAdd.Checked)
            {
                ///新增
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// InsertInfo");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        /// <param name=\"info\">对象</param>");
                sb.AppendLine("        /// <returns></returns>");
                sb.AppendLine("        public " + aloneKeyType + " InsertInfo(" + entityName + "Info info)");
                sb.AppendLine("        {");
                sb.AppendLine("            return dal.Add(info);");
                sb.AppendLine("        }");
                sb.AppendLine("");
            }
            if (dialogbll.ckUpdate.Checked)
            {
                ///修改
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// UpdateInfo");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        /// <param name=\"info\">对象</param>");
                sb.AppendLine("        /// <returns></returns>");
                sb.AppendLine("        public bool UpdateInfo(" + entityName + "Info info)");
                sb.AppendLine("        {");
                sb.AppendLine("            return dal.Update(info) > 0 ? true : false;");
                sb.AppendLine("        }");
                sb.AppendLine("");
            }
            if (dialogbll.ckDelete.Checked)
            {
                ///物理删除
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// DeleteInfo");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        /// <returns></returns>");
                sb.AppendLine("        public bool DeleteInfo(" + keyFieldParamString.Substring(1) + ")");
                sb.AppendLine("        {");
                sb.AppendLine("            return dal.Delete(" + keyFieldString.Substring(1) + ") > 0 ? true : false;");
                sb.AppendLine("        }");
                sb.AppendLine("");
            }
            if (dialogbll.ckLogicDelete.Checked)
            {
                ///逻辑删除
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// LogicDeleteInfo");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        /// <param name=\"id\">主键</param>");
                sb.AppendLine("        /// <param name=\"loginUser\">用户</param>");
                sb.AppendLine("        /// <returns></returns>");
                sb.AppendLine("        public bool LogicDeleteInfo(" + keyFieldParamString.Substring(1) + ", string loginUser)");
                sb.AppendLine("        {");
                sb.AppendLine("            return dal.LogicDelete(" + keyFieldString.Substring(1) + ", loginUser) > 0 ? true : false;");
                sb.AppendLine("        }");
                sb.AppendLine("");
            }
            if (dialogbll.ckUpdateInfo.Checked)
            {
                ///修改部分字段
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// UpdateInfo");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        /// <param name=\"fields\">更新字段</param>");
                sb.AppendLine("        /// <returns></returns>");
                sb.AppendLine("        public bool UpdateInfo(string fields, " + keyFieldParamString.Substring(1) + ")");
                sb.AppendLine("        {");
                sb.AppendLine("            return dal.UpdateInfo(fields, " + keyFieldString.Substring(1) + ") > 0 ? true : false;");
                sb.AppendLine("        }");
                sb.AppendLine("");
            }
            if (dialogbll.ckGetCounts.Checked)
            {
                ///获取行数
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// GetCounts");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        /// <param name=\"textWhere\">string 条件语句,无须where</param>");
                sb.AppendLine("        /// <returns></returns>");
                sb.AppendLine("        public int GetCounts(string textWhere)");
                sb.AppendLine("        {");
                sb.AppendLine("            return dal.GetCounts(textWhere);");
                sb.AppendLine("        }");
                sb.AppendLine("");
            }
            if (dialogbll.ckGetList.Checked)
            {
                ///分页获取集合
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// 获取集合");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        /// <param name=\"textWhere\">string 条件语句,无须where</param>");
                sb.AppendLine("        /// <param name=\"textOrder\">string 排序语句,无须order by</param>");
                sb.AppendLine("        /// <returns>List<" + entityName + "Info></returns>");
                sb.AppendLine("        public List<" + entityName + "Info> GetList(string textWhere, string textOrder)");
                sb.AppendLine("        {");
                sb.AppendLine("            return dal.GetList(textWhere, textOrder);");
                sb.AppendLine("        }");
                sb.AppendLine("");
            }
            ///
            sb.AppendLine("        #endregion");
            sb.AppendLine("");
            sb.AppendLine("        #region Interface");
            if (dialogbll.ckInterfaceCreateInfo.Checked)
            {
                List<TableField> tableFields = GetFieldsDataSource(entityInfo.TableNames);
                ///分页获取集合
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// Create " + entityName + "Info");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        /// <param name=\"loginUser\"></param>");
                sb.AppendLine("        /// <returns>" + entityName + "Info</returns>");
                sb.AppendLine("        public static " + entityName + "Info Create" + entityName + "Info(string loginUser)");
                sb.AppendLine("        {");
                sb.AppendLine("            " + entityName + "Info info = new " + entityName + "Info();");
                foreach (var tableField in tableFields)
                {
                    sb.AppendLine("            ///" + tableField.fieldName + "");
                    switch (tableField.fieldName)
                    {
                        case "FID": sb.AppendLine("            info.Fid = Guid.NewGuid();"); break;
                        case "VALID_FLAG": sb.AppendLine("            info.ValidFlag = true;"); break;
                        case "CREATE_DATE": sb.AppendLine("            info.CreateDate = DateTime.Now;"); break;
                        case "CREATE_USER": sb.AppendLine("            info.CreateUser = loginUser;"); break;
                        default: sb.AppendLine("            info." + GetPropNameByFieldName(tableField.fieldName) + " = null;"); break;
                    }
                }
                sb.AppendLine("            return info;");
                sb.AppendLine("        }");
                sb.AppendLine("");
            }
            sb.AppendLine("        #endregion");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = entityName + "BLL.cs";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(dialog.FileName))
                    File.Create(dialog.FileName).Close();
                using (StreamWriter sw = new StreamWriter(dialog.FileName))
                {
                    sw.WriteLine(sb.ToString());
                    sw.Close();
                }
            }
        }
        /// <summary>
        /// 系统数据INSERT脚本导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnInsertSql_Click(object sender, EventArgs e)
        {
            if (entityInfo == null) return;
            ///表名
            string tableName = entityInfo.TableNames;
            ///架构
            string schema = "dbo";
            if (menuComboMould.Text.ToUpper() != "SYS")
                schema = menuComboMould.Text.ToUpper();
            if (dgvFields.DataSource == null) return;
            List<TableField> fields = dgvFields.DataSource as List<TableField>;
            if (fields.Count == 0) return;
            List<string> unFields = new List<string>();
            unFields.Add("ID");
            unFields.Add("FID");
            unFields.Add("VALID_FLAG");
            unFields.Add("CREATE_DATE");
            unFields.Add("CREATE_USER");
            unFields.Add("MODIFY_DATE");
            unFields.Add("MODIFY_USER");
            List<TableField> tableFields = fields.Where(d => !unFields.Contains(d.fieldName)).ToList();
            string valueSql = string.Empty;
            for (int i = 0; i < tableFields.Count; i++)
            {
                switch (tableFields[i].dataType)
                {
                    case "int":
                    case "decimal":
                    case "bit":
                    case "bigint":
                        valueSql += ",'+ISNULL(REPLACE(CONVERT(nvarchar(max), " + tableFields[i].fieldName + "),'''',''''''),'NULL')+'";
                        break;
                    default:
                        valueSql += ",'''+ISNULL(REPLACE(CONVERT(nvarchar(max), " + tableFields[i].fieldName + "),'''',''''''),'NULL')+'''";
                        break;
                }
            }
            string sql = "select 'insert into " + schema + ".[" + tableName + "] ([FID],[" + string.Join("],[", tableFields.Select(d => d.fieldName).ToArray()) + "],[VALID_FLAG],[CREATE_DATE],[CREATE_USER]) "
                + "values ('''+ISNULL(REPLACE(CONVERT(nvarchar(max), FID),'''',''''''),'NULL')+'''" + valueSql + ",1,GETDATE(),'''+ISNULL(REPLACE(CONVERT(nvarchar(max), CREATE_USER),'''',''''''),'NULL')+''');' as INSERT_SQL "
                + "from " + schema + ".[" + tableName + "] with(nolock) where [VALID_FLAG] = 1 order by [ID];";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            StringBuilder sb = new StringBuilder();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    string insertSql = DBConvert.GetString(dr, dr.GetOrdinal("INSERT_SQL"));
                    if (string.IsNullOrEmpty(insertSql)) continue;
                    sb.AppendLine(insertSql.Replace("'NULL'", "NULL"));
                }
            }
            DialogSqlExport dialog = new DialogSqlExport(sb.ToString());
            dialog.ShowDialog(this);
        }
        /// <summary>
        /// 获取数据模型所对应的交付脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnInsertSys_Click(object sender, EventArgs e)
        {
            if (entityInfo == null) return;
            int dataCnt = 0;
            SaveFileDialog dialog = null;
            ///数据模型
            EntityInfo entity = new EntityBLL().GetInfo(entityInfo.EntityName);
            if (entity == null)
            {
                MessageBox.Show("没有" + entityInfo.EntityName + "相关的数据模型配置"
                    , "错误"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error
                    , MessageBoxDefaultButton.Button1);
                return;
            }
            entity = BLL.SYS.CommonBLL.FieldNullToEmpty(entity) as EntityInfo;
            ///清空无效数据
            string sql = "--CLEAR\r\n"
                + "delete from dbo.[TS_SYS_ENTITY] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_ENTITY_FIELD] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_MENU] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_MENU_ACTION] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_ACTION] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_CODE] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_CODE_ITEM] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_SEARCH_MODEL] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_SEARCH_MODEL_CONDITION] where [VALID_FLAG] = 0;\r\n";
            #region TS_SYS_ENTITY
            sql += "--ENTITY:" + entity.EntityName + "\r\n";
            sql += "if not exists (select * from dbo.TS_SYS_ENTITY with(nolock) where [FID] = '" + entity.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)\r\n"
                + "insert into dbo.TS_SYS_ENTITY "
                + "([FID]"
                + ",[ENTITY_NAME]"
                + ",[TABLE_NAMES]"
                + ",[COMMENTS]"
                + ",[VALID_FLAG]"
                + ",[CREATE_USER]"
                + ",[CREATE_DATE]"
                + ",[ENTITY_TYPE]"
                + ",[KEY_FIELDS]"
                + ",[PARENT_FIELD]"
                + ",[DEFAULT_SORT]"
                + ",[AUTH_CONFIG]"
                + ",[DEFAULT_PAGESIZE]"
                + ",[TAB_TITLES]) "
                + "values "
                + "('" + entity.Fid.GetValueOrDefault() + "'"///FID
                + ",'" + entity.EntityName + "'"///ENTITY_NAME
                + ",'" + entity.TableNames + "'"///TABLE_NAMES
                + ",'" + entity.Comments + "'"///COMMENTS
                + "," + (entity.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                + ",'" + loginUser + "'"///CREATE_USER
                + ",GETDATE()"///CREATE_DATE
                + "," + entity.EntityType.GetValueOrDefault() + ""///ENTITY_TYPE
                + ",'" + entity.KeyFields + "'"///KEY_FIELDS
                + ",'" + entity.ParentField + "'"///PARENT_FIELD
                + ",'" + entity.DefaultSort + "'"///DEFAULT_SORT
                + ",'" + entity.AuthConfig + "'"///AUTH_CONFIG
                //+ "," + (entity.DefaultPagesize == null ? "NULL" : "" + entity.DefaultPagesize.GetValueOrDefault() + "") + ""///DEFAULT_PAGESIZE
                + ",'" + entity.TabTitles + "'"///TAB_TITLES
                + ");\r\n";
            #endregion

            List<EntityFieldInfo> entityfields = new EntityFieldBLL().GetListByPage("and [VALID_FLAG] = 1 and [ENTITY_FID] = '" + entity.Fid.GetValueOrDefault() + "' "
                , string.Empty, 1, int.MaxValue, out dataCnt);
            ///系统代码
            List<string> syscodes = new List<string>();
            foreach (var entityfield in entityfields)
            {
                EntityFieldInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(entityfield) as EntityFieldInfo;
                if (!string.IsNullOrEmpty(info.Extend1.Trim()) && !syscodes.Contains(info.Extend1.Trim()))
                    syscodes.Add(info.Extend1.Trim());
                #region TS_SYS_ENTITY_FIELD
                sql += "--ENTITY.FIELD:" + info.FieldName + "\r\n";
                sql += "if not exists (select * from dbo.TS_SYS_ENTITY_FIELD with(nolock) where [FID] = '" + info.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_ENTITY_FIELD "
                    + "([FID]"
                    + ",[ENTITY_FID]"
                    + ",[FIELD_NAME]"
                    + ",[TABLE_FIELD_NAME]"
                    + ",[DISPLAY_NAME_CN]"
                    + ",[DISPLAY_NAME_EN]"
                    + ",[DISPLAY_ORDER]"
                    + ",[DATA_TYPE]"
                    + ",[CONTROL_TYPE]"
                    + ",[DATA_LENGTH]"
                    + ",[PRECISION]"
                    + ",[DEFAULT_VALUE]"
                    + ",[NULLENABLE]"
                    + ",[REGEX]"
                    + ",[ERROR_MSG]"
                    + ",[MIN_VALUE]"
                    + ",[MAX_VALUE]"
                    + ",[EDITABLE]"
                    + ",[EDIT_DISPLAY_WIDTH]"
                    + ",[LISTABLE]"
                    + ",[LIST_DISPLAY_WIDTH]"
                    + ",[EXTEND1]"
                    + ",[EXTEND2]"
                    + ",[EXTEND3]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_USER]"
                    + ",[CREATE_DATE]"
                    + ",[EDIT_READONLY]"
                    + ",[TAB_TITLE_CODE]"
                    + ",[SORTABLE]"
                    + ",[EXPORT_EXCEL_FLAG]"
                    + ",[EXPORT_EXCEL_ORDER]"
                    + ",[TOOLTIP_HELPER_CN]"
                    + ",[TOOLTIP_HELPER_EN]) "
                    + "values "
                    + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + info.EntityFid.GetValueOrDefault() + "'"///ENTITY_FID
                    + ",'" + info.FieldName + "'"///FIELD_NAME
                    + ",'" + info.TableFieldName + "'"///TABLE_FIELD_NAME
                    + ",'" + info.DisplayNameCn + "'"///DISPLAY_NAME_CN
                    + ",'" + info.DisplayNameEn + "'"///DISPLAY_NAME_EN
                    + "," + info.DisplayOrder.GetValueOrDefault() + ""///DISPLAY_ORDER
                    + "," + info.DataType.GetValueOrDefault() + ""///DATA_TYPE
                    + "," + info.ControlType.GetValueOrDefault() + ""///CONTROL_TYPE
                    + "," + info.DataLength.GetValueOrDefault() + ""///DATA_LENGTH
                    + "," + info.Precision.GetValueOrDefault() + ""///PRECISION
                    + ",'" + info.DefaultValue + "'"///DEFAULT_VALUE
                    + "," + (info.Nullenable.GetValueOrDefault() ? "1" : "0") + ""///NULLENABLE
                    + ",'" + info.Regex + "'"///REGEX
                    + ",'" + info.ErrorMsg + "'"///ERROR_MSG
                    + "," + info.MinValue.GetValueOrDefault() + ""///MIN_VALUE
                    + "," + info.MaxValue.GetValueOrDefault() + ""///MAX_VALUE
                    + "," + (info.Editable.GetValueOrDefault() ? "1" : "0") + ""///EDITABLE
                    + ",'" + info.EditDisplayWidth + "'"///EDIT_DISPLAY_WIDTH
                    + "," + (info.Listable.GetValueOrDefault() ? "1" : "0") + ""///LISTABLE
                    + ",'" + info.ListDisplayWidth + "'"///LIST_DISPLAY_WIDTH
                    + ",'" + info.Extend1 + "'"///EXTEND1
                    + ",'" + info.Extend2 + "'"///EXTEND2
                    + ",'" + info.Extend3.Replace("'", "''") + "'"///EXTEND3
                    + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ",GETDATE()"///CREATE_DATE
                    + "," + info.EditReadonly.GetValueOrDefault() + ""///EDIT_READONLY
                    + ",'" + info.TabTitleCode + "'"///TAB_TITLE_CODE
                    + "," + (info.Sortable.GetValueOrDefault() ? "1" : "0") + ""///SORTABLE
                    + "," + (info.ExportExcelFlag.GetValueOrDefault() ? "1" : "0") + ""///EXPORT_EXCEL_FLAG
                    + "," + info.ExportExcelOrder.GetValueOrDefault() + ""///EXPORT_EXCEL_ORDER
                    + ",'" + info.TooltipHelperCn + "'"///TOOLTIP_HELPER_CN
                    + ",'" + info.TooltipHelperEn + "'"///TOOLTIP_HELPER_EN
                    + ");\r\n";
                #endregion
            }
            ///检索条件
            SearchModelInfo searchmodel = new SearchModelBLL().GetInfo(entityInfo.EntityName);
            if (searchmodel != null)
            {
                #region TS_SYS_SEARCH_MODEL
                searchmodel = BLL.SYS.CommonBLL.FieldNullToEmpty(searchmodel) as SearchModelInfo;
                sql += "--SEARCH:" + searchmodel.SearchName + "\r\n";
                sql += "if not exists (select * from dbo.TS_SYS_SEARCH_MODEL with(nolock) where [FID] = '" + searchmodel.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_SEARCH_MODEL "
                    + "([FID]"
                    + ",[SEARCH_NAME]"
                    + ",[SEARCH_TYPE]"
                    + ",[COLUMN_LENGTH]"
                    + ",[COMMENTS]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_USER]"
                    + ",[CREATE_DATE]) "
                    + "values "
                    + "('" + searchmodel.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + searchmodel.SearchName + "'"///SEARCH_NAME
                    + "," + searchmodel.SearchType.GetValueOrDefault() + ""///SEARCH_TYPE
                    + "," + searchmodel.ColumnLength.GetValueOrDefault() + ""///COLUMN_LENGTH
                    + ",'" + searchmodel.Comments + "'"///COMMENTS
                    + "," + (searchmodel.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ",GETDATE()"///CREATE_DATE
                    + ");\r\n";
                #endregion
                List<SearchModelConditionInfo> searchconditions
                    = new SearchModelConditionBLL().GetListByPage("and [VALID_FLAG] = 1 and [SEARCH_FID] = '" + searchmodel.Fid.GetValueOrDefault() + "'"
                    , string.Empty, 1, int.MaxValue, out dataCnt);
                foreach (var searchcondition in searchconditions)
                {
                    #region TS_SYS_SEARCH_MODEL_CONDITION
                    SearchModelConditionInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(searchcondition) as SearchModelConditionInfo;
                    sql += "--CONDITION:" + info.ControlId + "\r\n";
                    sql += "if not exists (select * from dbo.TS_SYS_SEARCH_MODEL_CONDITION with(nolock) where [FID] = '" + info.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)\r\n"
                        + "insert into dbo.TS_SYS_SEARCH_MODEL_CONDITION "
                        + "([FID]"
                        + ",[SEARCH_FID]"
                        + ",[CONTROL_ID]"
                        + ",[CONTROL_TYPE]"
                        + ",[DEFAULT_VALUE]"
                        + ",[DISPLAY_ORDER]"
                        + ",[MAX_LENGTH]"
                        + ",[LABEL_TEXT]"
                        + ",[TABLE_NAME]"
                        + ",[COLUMN_NAME]"
                        + ",[COLUMN_TYPE]"
                        + ",[REGEX_EXPRESSION]"
                        + ",[DATASEARCH_TYPE]"
                        + ",[CODE_NAME]"
                        + ",[EXTEND_FIELD1]"
                        + ",[EXTEND_FIELD2]"
                        + ",[EXTEND_FIELD3]"
                        + ",[EXTEND_FIELD4]"
                        + ",[EXTEND_FIELD5]"
                        + ",[EXTEND_FIELD6]"
                        + ",[EXTEND_FIELD7]"
                        + ",[EXTEND_FIELD8]"
                        + ",[EXTEND_FIELD9]"
                        + ",[EXTEND_FIELD10]"
                        + ",[VALID_FLAG]"
                        + ",[CREATE_USER]"
                        + ",[CREATE_DATE]) "
                        + "values "
                        + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                        + ",'" + info.SearchFid.GetValueOrDefault() + "'"///SEARCH_FID
                        + ",'" + info.ControlId + "'"///CONTROL_ID
                        + ",'" + info.ControlType + "'"///CONTROL_TYPE
                        + ",'" + info.DefaultValue + "'"///DEFAULT_VALUE
                        + "," + info.DisplayOrder.GetValueOrDefault() + ""///DISPLAY_ORDER
                        + "," + info.MaxLength.GetValueOrDefault() + ""///MAX_LENGTH
                        + ",'" + info.LabelText + "'"///LABEL_TEXT
                        + ",'" + info.TableName + "'"///TABLE_NAME
                        + ",'" + info.ColumnName + "'"///COLUMN_NAME
                        + ",'" + info.ColumnType + "'"///COLUMN_TYPE
                        + ",'" + info.RegexExpression + "'"///REGEX_EXPRESSION
                        + ",'" + info.DatasearchType + "'"///DATASEARCH_TYPE
                        + ",'" + info.CodeName + "'"///CODE_NAME
                        + ",'" + info.ExtendField1 + "'"///EXTEND_FIELD1
                        + ",'" + info.ExtendField2 + "'"///EXTEND_FIELD2
                        + ",'" + info.ExtendField3.Replace("'", "''") + "'"///EXTEND_FIELD3
                        + ",'" + info.ExtendField4 + "'"///EXTEND_FIELD4
                        + ",'" + info.ExtendField5 + "'"///EXTEND_FIELD5
                        + ",'" + info.ExtendField6 + "'"///EXTEND_FIELD6
                        + ",'" + info.ExtendField7 + "'"///EXTEND_FIELD7
                        + ",'" + info.ExtendField8 + "'"///EXTEND_FIELD8
                        + ",'" + info.ExtendField9 + "'"///EXTEND_FIELD9
                        + ",'" + info.ExtendField10 + "'"///EXTEND_FIELD10
                        + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                        + ",'" + loginUser + "'"///CREATE_USER
                        + ",GETDATE()"///CREATE_DATE
                        + ");\r\n";
                    #endregion
                }
            }
            ///系统代码
            List<CodeInfo> codes = new CodeBLL().GetListByPage("and [VALID_FLAG] = 1 and [CODE_NAME] in ('" + string.Join("','", syscodes.ToArray()) + "')"
                , string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var codeinfo in codes)
            {
                #region TS_SYS_CODE
                CodeInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(codeinfo) as CodeInfo;
                sql += "--CODE:" + info.CodeName + "|" + info.CodeNameCn + "\r\n";
                sql += "if not exists (select * from dbo.TS_SYS_CODE with(nolock) where [FID] = '" + info.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_CODE "
                    + "([FID]"
                    + ",[CODE_NAME]"
                    + ",[CODE_NAME_CN]"
                    + ",[COMMENTS]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_USER]"
                    + ",[CREATE_DATE]) "
                    + "values "
                    + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + info.CodeName + "'"///CODE_NAME
                    + ",'" + info.CodeNameCn + "'"///CODE_NAME_CN
                    + ",'" + info.Comments + "'"///COMMENTS
                    + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ",GETDATE()"///CREATE_DATE
                    + ");\r\n";
                #endregion
                List<CodeItemInfo> codeitems = new CodeItemBLL().GetListByPage("and [VALID_FLAG] = 1 and [CODE_FID] = '" + info.Fid.GetValueOrDefault() + "'"
                , string.Empty, 1, int.MaxValue, out dataCnt);
                foreach (var codeitem in codeitems)
                {
                    #region TS_SYS_CODE_ITEM
                    CodeItemInfo codeiteminfo = BLL.SYS.CommonBLL.FieldNullToEmpty(codeitem) as CodeItemInfo;
                    sql += "if not exists (select * from dbo.TS_SYS_CODE_ITEM with(nolock) where [FID] = '" + codeiteminfo.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_CODE_ITEM "
                    + "([FID]"
                    + ",[CODE_FID]"
                    + ",[ITEM_VALUE]"
                    + ",[ITEM_NAME]"
                    + ",[ITEM_NAME_EN]"
                    + ",[PARENT_FID]"
                    + ",[DISPLAY_ORDER]"
                    + ",[COMMENTS]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_USER]"
                    + ",[CREATE_DATE]) "
                    + "values "
                    + "('" + codeiteminfo.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + codeiteminfo.CodeFid.GetValueOrDefault() + "'"///CODE_FID
                    + "," + codeiteminfo.ItemValue.GetValueOrDefault() + ""///ITEM_VALUE
                    + ",'" + codeiteminfo.ItemName + "'"///ITEM_NAME
                    + ",'" + codeiteminfo.ItemNameEn + "'"///ITEM_NAME_EN
                    + ",NULL"///PARENT_FID
                    + "," + codeiteminfo.DisplayOrder.GetValueOrDefault() + ""///DISPLAY_ORDER
                    + ",'" + codeiteminfo.Comments + "'"///COMMENTS
                    + "," + (codeiteminfo.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ",GETDATE()"///CREATE_DATE
                    + ");\r\n";
                    #endregion
                }
            }
            ///仅生成数据模型脚本
            if (MessageBox.Show("是否继续生成菜单、按钮、条件配置?"
                , "提示"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                dialog = new SaveFileDialog();
                if (sender == null)
                    dialog.FileName = "CREATE_" + entityInfo.EntityName + ".sql";
                else
                    dialog.FileName = "CREATE_" + entityInfo.TableNames + ".sql";
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    if (!File.Exists(dialog.FileName))
                        File.Create(dialog.FileName).Close();
                    using (StreamWriter sw = new StreamWriter(dialog.FileName))
                    {
                        sw.WriteLine(sql);
                        sw.Close();
                    }
                }
                return;
            }
            ///菜单、按钮
            ///LIST菜单项
            List<MenuInfo> menus
                = new MenuBLL().GetListByPage("and [VALID_FLAG] = 1 and [MENU_NAME] = '" + entityInfo.EntityName + "' and [MENU_TYPE] = 10"
                , string.Empty, 1, int.MaxValue, out dataCnt);
            if (menus.Count == 0)
            {
                MessageBox.Show("没有" + entityInfo.EntityName + "对应的列表菜单项", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            MenuInfo menuinfo = menus.FirstOrDefault();
            ///选定所属一级菜单
            menus = new MenuBLL().GetListByPage("and [VALID_FLAG] = 1 "
                + "and [FID] = '" + menuinfo.ParentMenuFid.GetValueOrDefault() + "' "
                + "and ([PARENT_MENU_FID] is NULL or [PARENT_MENU_FID] = '" + Guid.Empty.ToString() + "')"
                , string.Empty, 1, int.MaxValue, out dataCnt);
            ///模块
            MenuInfo pMenuinfo = menus.FirstOrDefault();
            ///菜单
            #region TS_SYS_MENU
            pMenuinfo = BLL.SYS.CommonBLL.FieldNullToEmpty(pMenuinfo) as MenuInfo;
            sql += "--MOULD:" + pMenuinfo.MenuName + "|" + menuinfo.MenuNameCn + "\r\n";
            sql += "if not exists (select * from dbo.TS_SYS_MENU with(nolock) where [FID] = '" + pMenuinfo.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)\r\n"
                + "insert into dbo.TS_SYS_MENU "
                + "([FID]"
                + ",[MENU_NAME]"
                + ",[PARENT_MENU_FID]"
                + ",[DISPLAY_ORDER]"
                + ",[IMAGE_URL]"
                + ",[LINK_URL]"
                + ",[COMMENTS]"
                + ",[MENU_TYPE]"
                + ",[NEED_AUTH]"
                + ",[CREATE_USER]"
                + ",[CREATE_DATE]"
                + ",[VALID_FLAG]"
                + ",[MENU_NAME_CN]"
                + ",[FAVORITE_PIC]"
                + ",[EDIT_FORM_WIDTH]"
                + ",[EDIT_FORM_HEIGHT]) "
                + "values "
                + "('" + pMenuinfo.Fid.GetValueOrDefault() + "'"///FID
                + ",'" + pMenuinfo.MenuName + "'"///MENU_NAME
                + ",'" + pMenuinfo.ParentMenuFid.GetValueOrDefault() + "'"///PARENT_MENU_FID
                + "," + pMenuinfo.DisplayOrder.GetValueOrDefault() + ""///DISPLAY_ORDER
                + ",'" + pMenuinfo.ImageUrl + "'"///IMAGE_URL
                + ",'" + pMenuinfo.LinkUrl + "'"///LINK_URL
                + ",'" + pMenuinfo.Comments + "'"///COMMENTS
                + "," + pMenuinfo.MenuType.GetValueOrDefault() + ""///MENU_TYPE
                + "," + (pMenuinfo.NeedAuth.GetValueOrDefault() ? "1" : "0") + ""///NEED_AUTH
                + ",'" + loginUser + "'"///CREATE_USER
                + ",GETDATE()"///CREATE_DATE
                + "," + (pMenuinfo.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                + ",'" + pMenuinfo.MenuNameCn + "'"///MENU_NAME_CN
                + ",'" + pMenuinfo.FavoritePic + "'"///FAVORITE_PIC
                + "," + pMenuinfo.EditFormWidth.GetValueOrDefault() + ""///EDIT_FORM_WIDTH
                + "," + pMenuinfo.EditFormHeight.GetValueOrDefault() + ""///EDIT_FORM_HEIGHT
                + ");\r\n";

            menuinfo = BLL.SYS.CommonBLL.FieldNullToEmpty(menuinfo) as MenuInfo;
            sql += "--MENU.LIST:" + menuinfo.MenuName + "|" + menuinfo.MenuNameCn + "\r\n";
            sql += "if not exists (select * from dbo.TS_SYS_MENU with(nolock) where [FID] = '" + menuinfo.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)\r\n"
                + "insert into dbo.TS_SYS_MENU "
                + "([FID]"
                + ",[MENU_NAME]"
                + ",[PARENT_MENU_FID]"
                + ",[DISPLAY_ORDER]"
                + ",[IMAGE_URL]"
                + ",[LINK_URL]"
                + ",[COMMENTS]"
                + ",[MENU_TYPE]"
                + ",[NEED_AUTH]"
                + ",[CREATE_USER]"
                + ",[CREATE_DATE]"
                + ",[VALID_FLAG]"
                + ",[MENU_NAME_CN]"
                + ",[FAVORITE_PIC]"
                + ",[EDIT_FORM_WIDTH]"
                + ",[EDIT_FORM_HEIGHT]) "
                + "values "
                + "('" + menuinfo.Fid.GetValueOrDefault() + "'"///FID
                + ",'" + menuinfo.MenuName + "'"///MENU_NAME
                + ",'" + menuinfo.ParentMenuFid.GetValueOrDefault() + "'"///PARENT_MENU_FID
                + "," + menuinfo.DisplayOrder.GetValueOrDefault() + ""///DISPLAY_ORDER
                + ",'" + menuinfo.ImageUrl + "'"///IMAGE_URL
                + ",'" + menuinfo.LinkUrl + "'"///LINK_URL
                + ",'" + menuinfo.Comments + "'"///COMMENTS
                + "," + menuinfo.MenuType.GetValueOrDefault() + ""///MENU_TYPE
                + "," + (menuinfo.NeedAuth.GetValueOrDefault() ? "1" : "0") + ""///NEED_AUTH
                + ",'" + loginUser + "'"///CREATE_USER
                + ",GETDATE()"///CREATE_DATE
                + "," + (menuinfo.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                + ",'" + menuinfo.MenuNameCn + "'"///MENU_NAME_CN
                + ",'" + menuinfo.FavoritePic + "'"///FAVORITE_PIC
                + "," + menuinfo.EditFormWidth.GetValueOrDefault() + ""///EDIT_FORM_WIDTH
                + "," + menuinfo.EditFormHeight.GetValueOrDefault() + ""///EDIT_FORM_HEIGHT
                + ");\r\n";
            #endregion
            ///按钮
            List<Guid> actionFids = new List<Guid>();
            List<MenuActionInfo> menuactions = new MenuActionBLL().GetList("and [VALID_FLAG] = 1 and [MENU_FID] = '" + menuinfo.Fid.GetValueOrDefault() + "'", string.Empty);
            foreach (var menuaction in menuactions)
            {
                MenuActionInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(menuaction) as MenuActionInfo;
                actionFids.Add(info.ActionFid.GetValueOrDefault());
                #region TS_SYS_MENU_ACTION
                sql += "if not exists (select * from dbo.TS_SYS_MENU_ACTION with(nolock) where [FID] = '" + info.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.[TS_SYS_MENU_ACTION] "
                + "([FID]"
                + ",[MENU_FID]"
                + ",[ACTION_FID]"
                + ",[CLIENT_JS]"
                + ",[ACTION_ORDER]"
                + ",[DETAIL_FLAG]"
                + ",[VALID_FLAG]"
                + ",[CREATE_USER]"
                + ",[CREATE_DATE]"
                + ",[NEED_AUTH]) "
                + "values "
                + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                + ",'" + info.MenuFid.GetValueOrDefault() + "'"///MENU_FID
                + ",'" + info.ActionFid.GetValueOrDefault() + "'"///ACTION_FID
                + ",'" + info.ClientJs.Replace("\'", "\"") + "'"///CLIENT_JS
                + "," + info.ActionOrder.GetValueOrDefault() + ""///ACTION_ORDER
                + "," + (info.DetailFlag.GetValueOrDefault() ? "1" : "0") + ""///DETAIL_FLAG
                + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                + ",'" + loginUser + "'"///CREATE_USER
                + ",GETDATE()"///CREATE_DATE
                + "," + (info.NeedAuth.GetValueOrDefault() ? "1" : "0") + ""///NEED_AUTH
                + ");\r\n";
                #endregion
            }

            ///弹出窗体
            menus = new MenuBLL().GetListByPage("and [VALID_FLAG] = 1 and [PARENT_MENU_FID] = '" + menuinfo.Fid.GetValueOrDefault() + "' and [MENU_TYPE] = 20"
                , string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var menu in menus)
            {
                #region TS_SYS_MENU
                MenuInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(menu) as MenuInfo;
                sql += "--MENU.FORM:" + info.MenuName + "|" + info.MenuNameCn + "\r\n";
                sql += "if not exists (select * from dbo.TS_SYS_MENU with(nolock) where [FID] = '" + info.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_MENU "
                    + "([FID]"
                    + ",[MENU_NAME]"
                    + ",[PARENT_MENU_FID]"
                    + ",[DISPLAY_ORDER]"
                    + ",[IMAGE_URL]"
                    + ",[LINK_URL]"
                    + ",[COMMENTS]"
                    + ",[MENU_TYPE]"
                    + ",[NEED_AUTH]"
                    + ",[CREATE_USER]"
                    + ",[CREATE_DATE]"
                    + ",[VALID_FLAG]"
                    + ",[MENU_NAME_CN]"
                    + ",[FAVORITE_PIC]"
                    + ",[EDIT_FORM_WIDTH]"
                    + ",[EDIT_FORM_HEIGHT]) "
                    + "values "
                    + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                + ",'" + info.MenuName + "'"///MENU_NAME
                + ",'" + menuinfo.Fid.GetValueOrDefault() + "'"///PARENT_MENU_FID
                + "," + info.DisplayOrder.GetValueOrDefault() + ""///DISPLAY_ORDER
                + ",'" + info.ImageUrl + "'"///IMAGE_URL
                + ",'" + info.LinkUrl + "'"///LINK_URL
                + ",'" + info.Comments + "'"///COMMENTS
                + "," + info.MenuType.GetValueOrDefault() + ""///MENU_TYPE
                + "," + (info.NeedAuth.GetValueOrDefault() ? "1" : "0") + ""///NEED_AUTH
                + ",'" + loginUser + "'"///CREATE_USER
                + ",GETDATE()"///CREATE_DATE
                + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                + ",'" + info.MenuNameCn + "'"///MENU_NAME_CN
                + ",'" + info.FavoritePic + "'"///FAVORITE_PIC
                + "," + info.EditFormWidth.GetValueOrDefault() + ""///EDIT_FORM_WIDTH
                + "," + info.EditFormHeight.GetValueOrDefault() + ""///EDIT_FORM_HEIGHT
                + ");\r\n";
                #endregion
                menuactions = new MenuActionBLL().GetList("and [VALID_FLAG] = 1 and [MENU_FID] = '" + menu.Fid.GetValueOrDefault() + "'", string.Empty);
                foreach (var menuaction in menuactions)
                {
                    MenuActionInfo infosub = BLL.SYS.CommonBLL.FieldNullToEmpty(menuaction) as MenuActionInfo;
                    actionFids.Add(infosub.ActionFid.GetValueOrDefault());
                    #region TS_SYS_MENU_ACTION
                    sql += "if not exists (select * from dbo.TS_SYS_MENU_ACTION with(nolock) where [FID] = '" + infosub.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)\r\n"
                        + "insert into dbo.[TS_SYS_MENU_ACTION] "
                + "([FID]"
                + ",[MENU_FID]"
                + ",[ACTION_FID]"
                + ",[CLIENT_JS]"
                + ",[ACTION_ORDER]"
                + ",[DETAIL_FLAG]"
                + ",[VALID_FLAG]"
                + ",[CREATE_USER]"
                + ",[CREATE_DATE]"
                + ",[NEED_AUTH]) "
                + "values "
                + "('" + infosub.Fid.GetValueOrDefault() + "'"///FID
                + ",'" + infosub.MenuFid.GetValueOrDefault() + "'"///MENU_FID
                + ",'" + infosub.ActionFid.GetValueOrDefault() + "'"///ACTION_FID
                + ",'" + infosub.ClientJs.Replace("\'", "\"") + "'"///CLIENT_JS
                + "," + infosub.ActionOrder.GetValueOrDefault() + ""///ACTION_ORDER
                + "," + (infosub.DetailFlag.GetValueOrDefault() ? "1" : "0") + ""///DETAIL_FLAG
                + "," + (infosub.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                + ",'" + loginUser + "'"///CREATE_USER
                + ",GETDATE()"///CREATE_DATE
                + "," + (infosub.NeedAuth.GetValueOrDefault() ? "1" : "0") + ""///NEED_AUTH
                + ");\r\n";
                    #endregion
                }
            }
            ///按钮
            List<ActionInfo> actions = new ActionBLL().GetListByPage("and [VALID_FLAG] = 1 and [FID] in ('" + string.Join("','", actionFids.ToArray()) + "')"
                , string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var actioninfo in actions)
            {
                #region TS_SYS_ACTION
                ActionInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(actioninfo) as ActionInfo;
                sql += "--ACTION:" + info.ActionName + "|" + info.ActionNameCn + "\r\n";
                sql += "if not exists (select * from dbo.TS_SYS_ACTION with(nolock) where [FID] = '" + info.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.[TS_SYS_ACTION] "
                    + "([FID]"
                    + ",[ACTION_NAME]"
                    + ",[ACTION_NAME_CN]"
                    + ",[ACTION_TYPE]"
                    + ",[COMMENTS]"
                    + ",[ICON_URL]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_USER]"
                    + ",[CREATE_DATE]) "
                    + "values "
                    + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + info.ActionName + "'"///ACTION_NAME
                    + ",'" + info.ActionNameCn + "'"///ACTION_NAME_CN
                    + "," + info.ActionType.GetValueOrDefault() + ""///ACTION_TYPE
                    + ",'" + info.Comments.Replace("\'", "\"") + "'"///COMMENTS
                    + ",'" + info.IconUrl + "'"///ICON_URL
                    + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ",GETDATE()"///CREATE_DATE
                    + ");\r\n";
                #endregion
            }

            ///
            dialog = new SaveFileDialog();
            if (sender == null)
                dialog.FileName = "CREATE_" + entityInfo.EntityName + ".sql";
            else
                dialog.FileName = "CREATE_" + entityInfo.TableNames + ".sql";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(dialog.FileName))
                    File.Create(dialog.FileName).Close();
                using (StreamWriter sw = new StreamWriter(dialog.FileName))
                {
                    sw.WriteLine(sql);
                    sw.Close();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnDeleteSql_Click(object sender, EventArgs e)
        {
            int dataCnt = 0;
            string sql = "delete from dbo.TS_SYS_CONFIG where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.TS_SYS_HANDLER where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.TS_SYS_MESSAGE where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.TS_SYS_PROCESS_SCHEDULE where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.TS_SYS_SEQ_DEFINE where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.TS_SYS_SEQ_SECTION where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.TS_SYS_CODE where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.TS_SYS_CODE_ITEM where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.TS_SYS_PRINT_CONFIG where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.TS_SYS_INTERFACE_CONFIG where [VALID_FLAG] = 0;\r\n";
            ///CONFIG
            List<ConfigInfo> configs = new ConfigBLL().GetListByPage(string.Empty, string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var configinfo in configs)
            {
                #region TS_SYS_CONFIG
                ConfigInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(configinfo) as ConfigInfo;
                sql += "---CONFIG:" + info.Name + "\r\n";
                sql += "if not exists (select * from dbo.TS_SYS_CONFIG with(nolock) where [CODE] = '" + info.Code + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_CONFIG "
                    + "([FID]"
                    + ",[NAME]"
                    + ",[CODE]"
                    + ",[CONFIG_VALUE]"
                    + ",[COMMENTS]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_USER]"
                    + ",[CREATE_DATE]) "
                    + "values "
                    + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + info.Name + "'"///NAME
                    + ",'" + info.Code + "'"///CODE
                    + ",'" + info.ConfigValue + "'"///CONFIG_VALUE
                    + ",'" + info.Comments + "'"///COMMENTS
                    + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ",GETDATE()"///CREATE_DATE
                    + ");\r\n";
                #endregion
            }
            ///HANDLER
            List<HandlerInfo> handlers = new HandlerBLL().GetListByPage(string.Empty, string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var handlerinfo in handlers)
            {
                #region TS_SYS_HANDLER
                HandlerInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(handlerinfo) as HandlerInfo;
                sql += "---HANDLER:" + info.AjaxMethodName + "\r\n";
                sql += "if not exists (select * from dbo.TS_SYS_HANDLER with(nolock) where [AJAX_METHOD_NAME] = '" + info.AjaxMethodName + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_HANDLER "
                    + "([FID]"
                    + ",[AJAX_METHOD_NAME]"
                    + ",[ASSEMBLY_NAME]"
                    + ",[CLASS_NAME]"
                    + ",[SERVER_METHOD_NAME]"
                    + ",[STATUS]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_DATE]"
                    + ",[CREATE_USER]"
                    + ",[COMMENTS]) "
                    + "values "
                    + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + info.AjaxMethodName + "'"///AJAX_METHOD_NAME
                    + ",'" + info.AssemblyName + "'"///ASSEMBLY_NAME
                    + ",'" + info.ClassName + "'"///CLASS_NAME
                    + ",'" + info.ServerMethodName + "'"///SERVER_METHOD_NAME
                   
                    + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",GETDATE()"///CREATE_DATE
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ",'" + info.Comments + "'"///COMMENTS
                    + ");\r\n";
                #endregion
            }
            ///HELP
            ///IMAGE_SOURCE
            ///INTERFACE_CONFIG
            List<InterfaceConfigInfo> interfaceConfigInfos = new InterfaceConfigBLL().GetListByPage(string.Empty, string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var interfaceConfigInfo in interfaceConfigInfos)
            {
                #region TS_SYS_INTERFACE_CONFIG
                InterfaceConfigInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(interfaceConfigInfo) as InterfaceConfigInfo;
                sql += "---INTERFACE_CONFIG:" + info.InterfaceCode + "\r\n";
                sql += "if not exists (select * from dbo.TS_SYS_INTERFACE_CONFIG with(nolock) where [INTERFACE_CODE] = '" + info.InterfaceCode + "' and [VALID_FLAG] = 1)\r\n"
                    + @"insert into [dbo].[TS_SYS_INTERFACE_CONFIG]
           ([FID]
           ,[INTERFACE_CODE]
           ,[METHOD_NAME]
           ,[SYS_NAME]
           ,[SYS_METHOD_NAME]
           ,[CALL_URL]
           ,[USER_NAME]
           ,[PASS_WORD]
           ,[PARAM1]
           ,[PARAM2]
           ,[PARAM3]
           ,[VALID_FLAG],[CREATE_USER],[CREATE_DATE])
     values"
           + "(N'" + info.Fid.GetValueOrDefault() + "'"
           + ",N'" + info.InterfaceCode + "'"
           + ",N'" + info.MethrodName + "'"
           + ",N'" + info.SysName + "'"
           + ",N'" + info.MethrodName + "'"
           + ",N'" + info.CallUrl + "'"
           + ",N'" + info.UserName + "'"
           + ",N'" + info.PassWord + "'"
           + ",N'" + info.Param1 + "'"
           + ",N'" + info.Param2 + "'"
           + ",N'" + info.Param3 + "'"
           + ",1,N'" + loginUser + "',GETDATE());\r\n";
                #endregion
            }
            /// 
            ///MESSAGE
            List<MessageInfo> messages = new MessageBLL().GetListByPage(string.Empty, string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var messageinfo in messages)
            {
                #region TS_SYS_MESSAGE
                MessageInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(messageinfo) as MessageInfo;
                sql += "---MESSAGE:" + info.MessageCode + "\r\n";
                sql += "if not exists (select * from [dbo].[TS_SYS_MESSAGE] with(nolock) where [MESSAGE_CODE] = '" + info.MessageCode + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_MESSAGE "
                    + "([FID]"
                    + ",[MESSAGE_CODE]"
                    + ",[MESSAGE_TYPE]"
                    + ",[MESSAGE_CN]"
                    + ",[MESSAGE_EN]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_DATE]"
                    + ",[CREATE_USER]) "
                    + "values "
                    + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + info.MessageCode + "'"///MESSAGE_CODE
                    + "," + info.MessageType.GetValueOrDefault() + ""///MESSAGE_TYPE
                    + ",'" + info.MessageCn.Replace("\'", "\"") + "'"///MESSAGE_CN
                    + ",'" + info.MessageEn.Replace("\'", "\"") + "'"///MESSAGE_EN
                    + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",GETDATE()"///CREATE_DATE
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ");\r\n";
                #endregion
            }
            ///PROCESS_SCHEDULE
            List<ProcessScheduleInfo> processScheduleInfos = new ProcessScheduleBLL().GetListByPage("[LAST_RUN_STATUS] = " + (int)ProcessRunStatusConstants.Running + " ", string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var processScheduleInfo in processScheduleInfos)
            {
                #region TS_SYS_PROCESS_SCHEDULE
                ProcessScheduleInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(processScheduleInfo) as ProcessScheduleInfo;
                sql += "---PROCESS_SCHEDULE:" + info.ProcessCode + "\r\n";
                sql += "if not exists (select * from dbo.TS_SYS_PROCESS_SCHEDULE with(nolock) where [PROCESS_CODE] = '" + info.ProcessCode + "' and [VALID_FLAG] = 1)\r\n"
                    + @"insert into [dbo].[TS_SYS_PROCESS_SCHEDULE]
           ([FID]
           ,[PROCESS_CODE]
           ,[PROCESS_NAME]
           ,[LAST_RUN_STATUS]
           ,[RUN_INTERVAL]
           ,[CHECK_INTERVAL]
           ,[SYSTEM_PARAMETER1]
           ,[SYSTEM_PARAMETER2]
           ,[SYSTEM_PARAMETER3]
           ,[SYSTEM_PARAMETER4]
           ,[SYSTEM_PARAMETER5]
           ,[VALID_FLAG]
           ,[CREATE_DATE]
           ,[CREATE_USER])
     values "
            + "(N'" + info.Fid.GetValueOrDefault() + "'"
            + ",N'" + info.ProcessCode + "'"
            + ",N'" + info.ProcessName + "'"
            + "," + (int)ProcessRunStatusConstants.Init + ""
            + "," + info.RunInterval.GetValueOrDefault() + ""
            + "," + info.CheckInterval.GetValueOrDefault() + ""
            + ",N'" + info.SystemParameter1 + "'"
            + ",N'" + info.SystemParameter2 + "'"
            + ",N'" + info.SystemParameter3 + "'"
            + ",N'" + info.SystemParameter4 + "'"
            + ",N'" + info.SystemParameter5 + "'"
            + ",1,GETDATE(),N'" + loginUser + "');\r\n";
                #endregion
            }
            ///SEQ_DEFINE
            List<SeqDefineInfo> seqDefineInfos = new SeqDefineBLL().GetListByPage(string.Empty, string.Empty, 1, int.MaxValue, out dataCnt);
            List<SeqSectionInfo> seqSectionInfos = new List<SeqSectionInfo>();
            if (seqDefineInfos.Count > 0)
                seqSectionInfos = new SeqSectionBLL().GetListByPage("[DEFINE_FID] in ('" + string.Join("','", seqDefineInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var seqDefineInfo in seqDefineInfos)
            {
                #region TS_SYS_SEQ_DEFINE
                SeqDefineInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(seqDefineInfo) as SeqDefineInfo;
                sql += "---SEQ_DEFINE:" + info.SeqCode + "|" + info.SeqName + "\r\n";
                sql += "if not exists (select * from [dbo].[TS_SYS_SEQ_DEFINE] with(nolock) where [SEQ_CODE] = '" + info.SeqCode + "' and [VALID_FLAG] = 1)\r\n"
                    + @"insert into [dbo].[TS_SYS_SEQ_DEFINE]
           ([FID]
           ,[SEQ_CODE]
           ,[SEQ_NAME]
           ,[SECTION_NUM]
           ,[JOIN_CHAR]
           ,[VALID_FLAG]
           ,[CREATE_USER]
           ,[CREATE_DATE])
     values "
            + "(N'" + info.Fid.GetValueOrDefault() + "'"
            + ",N'" + info.SeqCode + "'"
            + ",N'" + info.SeqName + "'"
            + "," + info.SectionNum.GetValueOrDefault() + ""
            + ",N'" + info.JoinChar + "'"
            + ",1,N'" + loginUser + "',GETDATE());\r\n";
                #endregion
                foreach (var seqSectionInfo in seqSectionInfos.Where(d => d.DefineFid.GetValueOrDefault() == info.Fid.GetValueOrDefault()).ToList())
                {
                    #region TS_SYS_SEQ_SECTION
                    SeqSectionInfo sectionInfo = BLL.SYS.CommonBLL.FieldNullToEmpty(seqSectionInfo) as SeqSectionInfo;
                    sql += "---SEQ_SECTION:" + info.SeqCode + "|" + sectionInfo.SectionSeq + "\r\n";
                    sql += "if not exists (select * from [dbo].[TS_SYS_SEQ_SECTION] with(nolock) where [DEFINE_FID] = '" + info.Fid + "' and [SECTION_SEQ] = " + sectionInfo.SectionSeq.GetValueOrDefault() + " and [VALID_FLAG] = 1)\r\n"
                        + @"insert into [dbo].[TS_SYS_SEQ_SECTION]
           ([FID]
           ,[DEFINE_FID]
           ,[SEQ_CODE]
           ,[SECTION_SEQ]
           ,[IS_FIXED_LENGTH]
           ,[LENGTH]
           ,[FILL_TYPE]
           ,[FILL_CHAR]
           ,[DATA_GENERATE_TYPE]
           ,[STEP_LENGTH]
           ,[DEFAULT_VALUE]
           ,[MIN_VALUE]
           ,[MAX_VALUE]
           ,[IS_CYCLE]
           ,[IS_AUTOUP]
           ,[IS_SEED_VALUE]
           ,[VALID_FLAG]
           ,[CREATE_USER]
           ,[CREATE_DATE])
     values "
            + "(N'" + sectionInfo.Fid.GetValueOrDefault() + "'"
            + ",N'" + sectionInfo.DefineFid.GetValueOrDefault() + "'"
            + ",N'" + sectionInfo.SeqCode + "'"
            + "," + sectionInfo.SectionSeq.GetValueOrDefault() + ""
            + "," + (sectionInfo.IsFixedLength.GetValueOrDefault() ? "1" : "0") + ""
            + "," + sectionInfo.Length.GetValueOrDefault() + ""
            + "," + sectionInfo.FillType.GetValueOrDefault() + ""
            + ",N'" + sectionInfo.FillChar + "'"
            + "," + sectionInfo.DataGenerateType.GetValueOrDefault() + ""
            + "," + sectionInfo.StepLength.GetValueOrDefault() + ""
            + ",N'" + sectionInfo.DefaultValue + "'"
            + "," + sectionInfo.MinValue.GetValueOrDefault() + ""
            + "," + sectionInfo.MaxValue.GetValueOrDefault() + ""
            + "," + (sectionInfo.IsCycle.GetValueOrDefault() ? "1" : "0") + ""
            + "," + (sectionInfo.IsAutoup.GetValueOrDefault() ? "1" : "0") + ""
            + "," + (sectionInfo.IsSeedValue.GetValueOrDefault() ? "1" : "0") + ""
            + ",1,N'" + loginUser + "',GETDATE());\r\n";
                    #endregion
                }
            }
            ///CODE&ITEM
            ///未在界面配置中出现的CODE
            List<CodeInfo> codeInfos = new CodeBLL().GetListByPage("[CODE_NAME] not in (select [EXTEND1] from dbo.[TS_SYS_ENTITY_FIELD] with(nolock) where [VALID_FLAG] = 1 and LEN([EXTEND1])>0)"
                , string.Empty, 1, int.MaxValue, out dataCnt);
            List<CodeItemInfo> codeItemInfos = new List<CodeItemInfo>();
            if (codeInfos.Count > 0)
                codeItemInfos = new CodeItemBLL().GetListByPage("[CODE_FID] in ('" + string.Join("','", codeInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var codeInfo in codeInfos)
            {
                #region TS_SYS_CODE
                CodeInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(codeInfo) as CodeInfo;
                sql += "---CODE:" + info.CodeName + "\r\n";
                sql += "if not exists (select * from [dbo].[TS_SYS_CODE] with(nolock) where [CODE_NAME] = '" + info.CodeName + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_CODE "
                    + "([FID]"
                    + ",[CODE_NAME]"
                    + ",[CODE_NAME_CN]"
                    + ",[COMMENTS]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_DATE]"
                    + ",[CREATE_USER]) "
                    + "values "
                    + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + info.CodeName + "'"///CODE_NAME
                    + ",'" + info.CodeNameCn + "'"///CODE_NAME_CN
                    + ",'" + info.Comments.Replace("\'", "\"") + "'"///COMMENTS
                    + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",GETDATE()"///CREATE_DATE
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ");\r\n";
                #endregion
                foreach (var codeItemInfo in codeItemInfos.Where(d => d.CodeFid.GetValueOrDefault() == codeInfo.Fid.GetValueOrDefault()).ToList())
                {
                    #region TS_SYS_CODE_ITEM
                    CodeItemInfo itemInfo = BLL.SYS.CommonBLL.FieldNullToEmpty(codeItemInfo) as CodeItemInfo;
                    sql += "---CODE_ITEM:" + itemInfo.ItemValue + "|" + itemInfo.ItemName + "\r\n";
                    sql += "if not exists (select * from [dbo].[TS_SYS_CODE_ITEM] with(nolock) where [CODE_FID] = N'" + info.Fid.GetValueOrDefault() + "' and [ITEM_VALUE] = " + itemInfo.ItemValue.GetValueOrDefault() + " and [VALID_FLAG] = 1)\r\n"
                        + "insert into dbo.TS_SYS_CODE_ITEM "
                        + "([FID]"
                        + ",[CODE_FID]"
                        + ",[ITEM_VALUE]"
                        + ",[ITEM_NAME]"
                        + ",[ITEM_NAME_EN]"
                        + ",[DISPLAY_ORDER]"
                        + ",[COMMENTS]"
                        + ",[VALID_FLAG]"
                        + ",[CREATE_DATE]"
                        + ",[CREATE_USER]) "
                        + "values "
                        + "('" + itemInfo.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + itemInfo.CodeFid.GetValueOrDefault() + "'"///CODE_FID
                    + "," + itemInfo.ItemValue.GetValueOrDefault() + ""///ITEM_VALUE
                    + ",'" + itemInfo.ItemName + "'"///ITEM_NAME
                    + ",'" + itemInfo.ItemNameEn + "'"///ITEM_NAME_EN
                    + "," + itemInfo.DisplayOrder.GetValueOrDefault() + ""///DISPLAY_ORDER
                    + ",'" + itemInfo.Comments.Replace("\'", "\"") + "'"///COMMENTS
                    + "," + (itemInfo.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",GETDATE()"///CREATE_DATE
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ");\r\n";
                    #endregion
                }
            }
            ///PRINT_CONFIG
            List<PrintConfigInfo> printConfigInfos = new PrintConfigBLL().GetListByPage("[STATUS] = " + (int)BasicDataStatusConstants.Enable + " ", string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var printConfigInfo in printConfigInfos)
            {
                #region TS_SYS_PRINT_CONFIG
                PrintConfigInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(printConfigInfo) as PrintConfigInfo;
                sql += "---PRINT_CONFIG:" + info.PrintConfigCode + "|" + info.PrintConfigName + "\r\n";
                sql += "if not exists (select * from dbo.TS_SYS_PRINT_CONFIG with(nolock) where [PRINT_CONFIG_CODE] = '" + info.PrintConfigCode + "' and [VALID_FLAG] = 1)\r\n"
                    + @"insert into [dbo].[TS_SYS_PRINT_CONFIG]
           ([FID]
           ,[PRINT_CONFIG_CODE]
           ,[PRINT_CONFIG_NAME]
           ,[PRINT_TEMPLATE_FILENAME]
           ,[PRINT_TEMPLATE_URL]
           ,[TEMPLATE_FILE_TYPE]
           ,[PRINT_COPIES]
           ,[PRINTER_NAME]
           ,[LAST_UPLOADFILE_TIME]
           ,[STATUS]
           ,[COMMENTS]
           ,[VALID_FLAG]
           ,[CREATE_USER]
           ,[CREATE_DATE])
     values "
            + "(N'" + info.Fid.GetValueOrDefault() + "'"///FID
            + ",N'" + info.PrintConfigCode + "'"///PRINT_CONFIG_CODE
            + ",N'" + info.PrintConfigName + "'"///PRINT_CONFIG_NAME
            + ",N'" + info.PrintTemplateFilename + "'"///PRINT_TEMPLATE_FILENAME
            + ",N'" + info.PrintTemplateUrl + "'"///PRINT_TEMPLATE_URL
            + "," + info.TemplateFileType.GetValueOrDefault() + ""///TEMPLATE_FILE_TYPE
            + "," + info.PrintCopies.GetValueOrDefault() + ""///PRINT_COPIES
            + ",N'" + info.PrinterName + "'"///PRINTER_NAME
            + ",NULL"///LAST_UPLOADFILE_TIME
            + "," + (int)BasicDataStatusConstants.Created + ""///STATUS
            + ",N'" + info.Comments + "'"///COMMENTS
            + ",1,N'" + loginUser + "',GETDATE());\r\n";///VALID_FLAG,CREATE_USER,CREATE_DATE
                #endregion
            }
            ///
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "DB_SYS_INIT_CONFIG.sql";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(dialog.FileName))
                    File.Create(dialog.FileName).Close();
                using (StreamWriter sw = new StreamWriter(dialog.FileName))
                {
                    sw.WriteLine(sql);
                    sw.Close();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnDeleteEntity_Click(object sender, EventArgs e)
        {
            if (entityInfo == null) return;
            int dataCnt = 0;
            ///清空无效数据
            string sql = "--CLEAR\r\n"
                + "delete from dbo.[TS_SYS_ENTITY] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_ENTITY_FIELD] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_MENU] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_MENU_ACTION] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_ACTION] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_CODE] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_CODE_ITEM] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_SEARCH_MODEL] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_SEARCH_MODEL_CONDITION] where [VALID_FLAG] = 0;\r\n";
            SaveFileDialog dialog = null;
            ///数据模型
            EntityInfo entity = new EntityBLL().GetInfo(entityInfo.EntityName);
            if (entity == null)
            {
                MessageBox.Show("没有" + entityInfo.EntityName + "相关的数据模型配置"
                    , "错误"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error
                    , MessageBoxDefaultButton.Button1);
                return;
            }
            entity = BLL.SYS.CommonBLL.FieldNullToEmpty(entity) as EntityInfo;
            #region TS_SYS_ENTITY
            sql += "--ENTITY:" + entity.EntityName + "\r\n";
            sql += "delete from dbo.[TS_SYS_ENTITY] where [VALID_FLAG] = 1 and [FID] = '" + entity.Fid.GetValueOrDefault() + "';\r\n";
            sql += "delete from dbo.[TS_SYS_ENTITY_FIELD] where [VALID_FLAG] = 1 and [ENTITY_FID] = '" + entity.Fid.GetValueOrDefault() + "';\r\n";
            #endregion

            List<EntityFieldInfo> entityfields = new EntityFieldBLL().GetListByPage("[ENTITY_FID] = N'" + entity.Fid.GetValueOrDefault() + "' ", string.Empty, 1, int.MaxValue, out dataCnt);
            ///系统代码
            List<string> syscodes = new List<string>();
            foreach (var entityfield in entityfields)
            {
                EntityFieldInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(entityfield) as EntityFieldInfo;
                if (!string.IsNullOrEmpty(info.Extend1.Trim()) && !syscodes.Contains(info.Extend1.Trim()))
                    syscodes.Add(info.Extend1.Trim());
            }
            ///检索条件
            SearchModelInfo searchmodel = new SearchModelBLL().GetInfo(entityInfo.EntityName);
            if (searchmodel != null)
            {
                #region TS_SYS_SEARCH_MODEL
                searchmodel = BLL.SYS.CommonBLL.FieldNullToEmpty(searchmodel) as SearchModelInfo;
                sql += "--SEARCH:" + searchmodel.SearchName + "\r\n";
                sql += "delete from dbo.[TS_SYS_SEARCH_MODEL] where [VALID_FLAG] = 1 and [FID] = '" + searchmodel.Fid.GetValueOrDefault() + "';\r\n";
                sql += "delete from dbo.[TS_SYS_SEARCH_MODEL_CONDITION] where [VALID_FLAG] = 1 and [SEARCH_FID] = '" + searchmodel.Fid.GetValueOrDefault() + "';\r\n";
                #endregion
            }
            ///系统代码
            List<CodeInfo> codes = new CodeBLL().GetListByPage("[CODE_NAME] in ('" + string.Join("','", syscodes.ToArray()) + "')", string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var codeinfo in codes)
            {
                ///校验是否其它数据模型用到过这个CODE，有则不能删除
                dataCnt = new EntityFieldBLL().GetCounts("" +
                    "[EXTEND1] = N'" + codeinfo.CodeName + "' and " +
                    "[ENTITY_FID] <> N'" + entity.Fid.GetValueOrDefault() + "'");
                if (dataCnt > 0) continue;
                #region TS_SYS_CODE
                CodeInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(codeinfo) as CodeInfo;
                sql += "--CODE:" + info.CodeName + "|" + info.CodeNameCn + "\r\n";
                sql += "delete from dbo.[TS_SYS_CODE] where [VALID_FLAG] = 1 and [FID] = '" + info.Fid.GetValueOrDefault() + "';\r\n";
                sql += "delete from dbo.[TS_SYS_CODE_ITEM] where [VALID_FLAG] = 1 and [CODE_FID] = '" + info.Fid.GetValueOrDefault() + "';\r\n";
                #endregion
            }
            ///仅生成数据模型脚本
            if (MessageBox.Show("是否继续生成菜单、按钮、条件配置?"
                , "提示"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                dialog = new SaveFileDialog();
                if (sender == null)
                    dialog.FileName = "DELETE_" + entityInfo.EntityName + ".sql";
                else
                    dialog.FileName = "DELETE_" + entityInfo.TableNames + ".sql";
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    if (!File.Exists(dialog.FileName))
                        File.Create(dialog.FileName).Close();
                    using (StreamWriter sw = new StreamWriter(dialog.FileName))
                    {
                        sw.WriteLine(sql);
                        sw.Close();
                    }
                }
                return;
            }
            ///菜单、按钮
            ///LIST菜单项
            List<MenuInfo> menus = new MenuBLL().GetListByPage("" +
                "[MENU_NAME] = '" + entityInfo.EntityName + "' and " +
                "[MENU_TYPE] = " + (int)MenuTypeConstants.WebMenu + ""
                , string.Empty, 1, int.MaxValue, out dataCnt);
            if (menus.Count == 0)
            {
                MessageBox.Show("没有" + entityInfo.EntityName + "对应的列表菜单项", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            ///如果多个模块下有相同ENTITY_NAME的菜单，此处需要进行选择
            MenuInfo menuinfo = menus.FirstOrDefault();
            if (menus.Count > 1)
            {
                ///web module
                List<MenuInfo> menuInfos = new MenuBLL().GetListByPage("[MENU_TYPE] = " + (int)MenuTypeConstants.WebModule + "", string.Empty, 1, int.MaxValue, out dataCnt);

                List<GuidValueDatasourceInfo> guidValues = new List<GuidValueDatasourceInfo>();
                foreach (var menu in menus)
                {
                    MenuInfo menuInfo = menuInfos.FirstOrDefault(d => d.Fid.GetValueOrDefault() == menu.ParentMenuFid.GetValueOrDefault());
                    if (menuInfo == null) continue;
                    GuidValueDatasourceInfo guidValue = new GuidValueDatasourceInfo();
                    guidValue.GuidValue = menuInfo.Fid.GetValueOrDefault();
                    guidValue.StringDisplay = menuInfo.MenuNameCn;
                    guidValues.Add(guidValue);
                }
                DialogCombobox dialogCombobox = new DialogCombobox(guidValues);
                if (dialogCombobox.ShowDialog(this) != DialogResult.Yes)
                {
                    MessageBox.Show("没有选择菜单对应的模块", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                menuinfo = menus.FirstOrDefault(d => d.ParentMenuFid.GetValueOrDefault() == dialogCombobox.SelectedGuidValue);
                if (menuinfo == null)
                {
                    MessageBox.Show("没有选择菜单对应的模块", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            ///菜单
            #region TS_SYS_MENU
            menuinfo = BLL.SYS.CommonBLL.FieldNullToEmpty(menuinfo) as MenuInfo;
            sql += "--MENU.LIST:" + menuinfo.MenuName + "|" + menuinfo.MenuNameCn + "\r\n";
            sql += "delete from dbo.[TS_SYS_MENU] where [VALID_FLAG] = 1 and [FID] = '" + menuinfo.Fid.GetValueOrDefault() + "';\r\n";
            sql += "delete from dbo.[TS_SYS_MENU_ACTION] where [VALID_FLAG] = 1 and [MENU_FID] = '" + menuinfo.Fid.GetValueOrDefault() + "';\r\n";
            #endregion
            ///按钮
            List<Guid> actionFids = new List<Guid>();
            List<MenuActionInfo> menuactions = new MenuActionBLL().GetList("[MENU_FID] = N'" + menuinfo.Fid.GetValueOrDefault() + "'", string.Empty);
            foreach (var menuaction in menuactions)
            {
                MenuActionInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(menuaction) as MenuActionInfo;
                actionFids.Add(info.ActionFid.GetValueOrDefault());
            }

            ///弹出窗体
            menus = new MenuBLL().GetListByPage("" +
                "[PARENT_MENU_FID] = '" + menuinfo.Fid.GetValueOrDefault() + "' and " +
                "[MENU_TYPE] = " + (int)MenuTypeConstants.WebForm + ""
                , string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var menu in menus)
            {
                #region TS_SYS_MENU
                MenuInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(menu) as MenuInfo;
                sql += "--MENU.FORM:" + info.MenuName + "|" + info.MenuNameCn + "\r\n";
                sql += "delete from dbo.[TS_SYS_MENU] where [VALID_FLAG] = 1 and [FID] = '" + info.Fid.GetValueOrDefault() + "';\r\n";
                sql += "delete from dbo.[TS_SYS_MENU_ACTION] where [VALID_FLAG] = 1 and [MENU_FID] = '" + info.Fid.GetValueOrDefault() + "';\r\n";
                #endregion
                menuactions = new MenuActionBLL().GetList("[MENU_FID] = N'" + menu.Fid.GetValueOrDefault() + "'", string.Empty);
                foreach (var menuaction in menuactions)
                {
                    MenuActionInfo infosub = BLL.SYS.CommonBLL.FieldNullToEmpty(menuaction) as MenuActionInfo;
                    actionFids.Add(infosub.ActionFid.GetValueOrDefault());
                }
            }
            ///按钮
            List<ActionInfo> actions = new ActionBLL().GetListByPage("[FID] in ('" + string.Join("','", actionFids.ToArray()) + "')"
                , string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var actioninfo in actions)
            {
                string menuIds = (menus.Count == 0 ? string.Empty : ",'" + string.Join("','", menus.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "'");
                ///如果有其它界面使用这个ACTION，则不进行删除
                List<MenuActionInfo> menuactionlist
                    = new MenuActionBLL().GetList("" +
                    "[ACTION_FID] = '" + actioninfo.Fid + "' and " +
                    "[MENU_FID] not in ('" + menuinfo.Fid.GetValueOrDefault() + "'" + menuIds + ")", string.Empty);
                if (menuactionlist.Count > 0) continue;
                #region TS_SYS_ACTION
                ActionInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(actioninfo) as ActionInfo;
                sql += "--ACTION:" + info.ActionName + "|" + info.ActionNameCn + "\r\n";
                sql += "delete from dbo.[TS_SYS_ACTION] where [VALID_FLAG] = 1 and [FID] = '" + info.Fid.GetValueOrDefault() + "';\r\n";
                #endregion
            }

            ///
            dialog = new SaveFileDialog();
            if (sender == null)
                dialog.FileName = "DELETE_" + entityInfo.EntityName + ".sql";
            else
                dialog.FileName = "DELETE_" + entityInfo.TableNames + ".sql";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(dialog.FileName))
                    File.Create(dialog.FileName).Close();
                using (StreamWriter sw = new StreamWriter(dialog.FileName))
                {
                    sw.WriteLine(sql);
                    sw.Close();
                }
            }
        }

        #endregion

        /// <summary>
        /// 加载状态
        /// </summary>
        private void LoadStatus()
        {
            #region Entity Status
            if (entityInfo == null)
            {
                statusEntity.BackColor = Color.White;
                statusEntity.ForeColor = Color.Black;
            }
            else
            {
                statusEntity.BackColor = Color.ForestGreen;
                statusEntity.ForeColor = Color.White;
            }
            #endregion
            #region Menu Status
            if (menuInfo == null)
            {
                statusMenu.BackColor = Color.White;
                statusMenu.ForeColor = Color.Black;
            }
            else
            {
                statusMenu.BackColor = Color.ForestGreen;
                statusMenu.ForeColor = Color.White;
            }
            #endregion
            #region Search Status
            if (searchInfo == null)
            {
                statusSearch.BackColor = Color.White;
                statusSearch.ForeColor = Color.Black;
            }
            else
            {
                statusSearch.BackColor = Color.ForestGreen;
                statusSearch.ForeColor = Color.White;
            }
            #endregion
        }
        /// <summary>
        /// 加载模块
        /// </summary>
        private void LoadMoulds()
        {
            List<CodeItemDatasourceInfo> codeitems = new CodeBLL().GetDataSource("SYS_MOULD");
            foreach (var codeitem in codeitems)
            {
                menuComboMould.Items.Add(codeitem.ItemDisplay);
            }
            if (codeitems.Count == 0)
            {
                menuComboMould.Items.Add("SYS");
                menuComboMould.Items.Add("LES");
            }
            if (menuComboMould.Items.Count > 0)
                menuComboMould.SelectedIndex = 0;
        }
        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadTableNames();
            LoadViewNames();
            ///默认为Table
            menuComboTorV.SelectedIndex = 0;
            LoadMoulds();
        }
        /// <summary>
        /// T or V
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuComboTorV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (menuComboTorV.SelectedIndex == 0)
                menuBtnLoadTable.Text = "TABLE";
            else
                menuBtnLoadTable.Text = "VIEW";
        }
        /// <summary>
        /// 双击GRID数据行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFields_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ///实体类
            if (entityInfo == null)
            {
                ErrorMessage("必须先加载实体类数据");
                return;
            }
            TableField tablefieldinfo = dgvFields.Rows[e.RowIndex].DataBoundItem as TableField;
            ///获取实体类属性对象
            EntityFieldInfo entityfieldinfo = new EntityFieldBLL().GetInfo(entityInfo.EntityName, GetPropNameByFieldName(tablefieldinfo.fieldName));
            if (entityfieldinfo == null)
            {
                ErrorMessage(tablefieldinfo.fieldName + "尚未创建实体属性");
                return;
            }
            ///
            DialogEntityField dialog = new DialogEntityField(entityfieldinfo);
            dialog.ShowDialog(this);
            dialog.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnImageIconCss_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择图标文件所保存的路径";
            if (dialog.ShowDialog() != DialogResult.OK) return;
            string iconFoldPath = dialog.SelectedPath;
            int dataCnt = 0;
            List<ImageResourceInfo> imagelist
                = new ImageResourceBLL().GetListByPage("[IMAGE_TYPE] in (" + (int)ImageTypeConstants.ActionIcon + "," + (int)ImageTypeConstants.MenuIcon + ")"
                , string.Empty, 1, int.MaxValue, out dataCnt);
            StringBuilder iconCss = new StringBuilder();
            StringBuilder iconJson = new StringBuilder();
            iconJson.AppendLine("{");
            iconJson.AppendLine("  \"Icon_Item\": [");
            StringBuilder iconJsonEn = new StringBuilder();
            iconJsonEn.AppendLine("{");
            iconJsonEn.AppendLine("  \"Icon_Item\": [");
            foreach (ImageResourceInfo imageinfo in imagelist)
            {
                string imageTypeName = "ico";
                switch (imageinfo.ImageFileType.GetValueOrDefault())
                {
                    case (int)ImageFileTypeConstants.Gif: imageTypeName = "gif"; break;
                    case (int)ImageFileTypeConstants.Png: imageTypeName = "png"; break;
                    case (int)ImageFileTypeConstants.Jpg: imageTypeName = "jpg"; break;
                }
                using (FileStream fs = new FileStream(iconFoldPath + @"\" + imageinfo.ImageName + "." + imageTypeName, FileMode.Create))
                {
                    fs.Write(imageinfo.ImageResource, 0, imageinfo.ImageResource.Length);
                    fs.Close();
                }
                iconCss.AppendLine("." + imageinfo.CssTagName + " {");
                iconCss.AppendLine("    background: url('" + imageinfo.ImageUrl + "/" + imageinfo.ImageName + "." + imageTypeName + "') no-repeat center center;");
                iconCss.AppendLine("}");
                iconCss.AppendLine("");

                iconJson.AppendLine("    {");
                iconJson.AppendLine("      \"id\": \"" + imageinfo.ImageName + "\",");
                iconJson.AppendLine("      \"text\": \"" + imageinfo.ImageNameCn + "\",");
                iconJson.AppendLine("      \"iconCls\": \"" + imageinfo.CssTagName + "\",");
                iconJson.AppendLine("      \"iconImg\": \".." + imageinfo.ImageUrl + "/" + imageinfo.ImageName + "." + imageTypeName + "\"");
                iconJson.AppendLine("    },");

                iconJsonEn.AppendLine("    {");
                iconJsonEn.AppendLine("      \"id\": \"" + imageinfo.ImageName + "\",");
                iconJsonEn.AppendLine("      \"text\": \"" + imageinfo.ImageNameEn + "\",");
                iconJsonEn.AppendLine("      \"iconCls\": \"" + imageinfo.CssTagName + "\",");
                iconJsonEn.AppendLine("      \"iconImg\": \".." + imageinfo.ImageUrl + "/" + imageinfo.ImageName + "." + imageTypeName + "\"");
                iconJsonEn.AppendLine("    },");
            }
            iconJson = new StringBuilder(iconJson.ToString().Remove(iconJson.Length - 3));
            iconJson.AppendLine("");
            iconJson.AppendLine("  ]");
            iconJson.AppendLine("}");
            iconJsonEn = new StringBuilder(iconJsonEn.ToString().Remove(iconJsonEn.Length - 3));
            iconJsonEn.AppendLine("");
            iconJsonEn.AppendLine("  ]");
            iconJsonEn.AppendLine("}");
            SaveFileDialog dialogSave = new SaveFileDialog();
            dialogSave.FileName = "icon.css";
            if (dialogSave.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(dialogSave.FileName))
                    File.Create(dialogSave.FileName).Close();
                using (StreamWriter sw = new StreamWriter(dialogSave.FileName))
                {
                    sw.WriteLine(iconCss.ToString());
                    sw.Close();
                }
            }
            dialogSave = new SaveFileDialog();
            dialogSave.FileName = "iconCn.data.json";
            if (dialogSave.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(dialogSave.FileName))
                    File.Create(dialogSave.FileName).Close();
                using (StreamWriter sw = new StreamWriter(dialogSave.FileName))
                {
                    sw.WriteLine(iconJson.ToString());
                    sw.Close();
                }
            }
            dialogSave = new SaveFileDialog();
            dialogSave.FileName = "iconEn.data.json";
            if (dialogSave.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(dialogSave.FileName))
                    File.Create(dialogSave.FileName).Close();
                using (StreamWriter sw = new StreamWriter(dialogSave.FileName))
                {
                    sw.WriteLine(iconJsonEn.ToString());
                    sw.Close();
                }
            }
        }


        /// <summary>
        /// 生成数据结构文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnGetExcelData_Click(object sender, EventArgs e)
        {
            Dictionary<int, string> valuePairsRowNo = new Dictionary<int, string>();
            valuePairsRowNo.Add(1, "①");
            valuePairsRowNo.Add(2, "②");
            valuePairsRowNo.Add(3, "③");
            valuePairsRowNo.Add(4, "④");
            valuePairsRowNo.Add(5, "⑤");
            valuePairsRowNo.Add(6, "⑥");
            valuePairsRowNo.Add(7, "⑦");
            valuePairsRowNo.Add(8, "⑧");
            valuePairsRowNo.Add(9, "⑨");
            valuePairsRowNo.Add(10, "⑩");

            valuePairsRowNo.Add(11, "⑪");
            valuePairsRowNo.Add(12, "⑫");
            valuePairsRowNo.Add(13, "⑬");
            valuePairsRowNo.Add(14, "⑭");
            valuePairsRowNo.Add(15, "⑮");
            valuePairsRowNo.Add(16, "⑯");
            valuePairsRowNo.Add(17, "⑰");
            valuePairsRowNo.Add(18, "⑱");
            valuePairsRowNo.Add(19, "⑲");
            valuePairsRowNo.Add(20, "⑳");

            valuePairsRowNo.Add(21, "㉑");
            valuePairsRowNo.Add(22, "㉒");
            valuePairsRowNo.Add(23, "㉓");
            valuePairsRowNo.Add(24, "㉔");
            valuePairsRowNo.Add(25, "㉕");
            valuePairsRowNo.Add(26, "㉖");
            valuePairsRowNo.Add(27, "㉗");
            valuePairsRowNo.Add(28, "㉘");
            valuePairsRowNo.Add(29, "㉙");
            valuePairsRowNo.Add(30, "㉚");

            valuePairsRowNo.Add(31, "㉛");
            valuePairsRowNo.Add(32, "㉜");
            valuePairsRowNo.Add(33, "㉝");
            valuePairsRowNo.Add(34, "㉞");
            valuePairsRowNo.Add(35, "㉟");
            valuePairsRowNo.Add(36, "㊱");
            valuePairsRowNo.Add(37, "㊲");
            valuePairsRowNo.Add(38, "㊳");
            valuePairsRowNo.Add(39, "㊴");
            valuePairsRowNo.Add(40, "㊵");

            valuePairsRowNo.Add(41, "㊶");
            valuePairsRowNo.Add(42, "㊷");
            valuePairsRowNo.Add(43, "㊸");
            valuePairsRowNo.Add(44, "㊹");
            valuePairsRowNo.Add(45, "㊺");
            valuePairsRowNo.Add(46, "㊻");
            valuePairsRowNo.Add(47, "㊼");
            valuePairsRowNo.Add(48, "㊽");
            valuePairsRowNo.Add(49, "㊾");
            valuePairsRowNo.Add(50, "㊿");

            string configFileName = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择数据结构配置文件";
            openFileDialog.Filter = "数据结构配置文件(*.xml)|*.xml";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                configFileName = openFileDialog.FileName;
            }
            openFileDialog.Dispose();
            if (string.IsNullOrEmpty(configFileName)) return;
            XmlWrapper xmlWrapper = new XmlWrapper(configFileName, LoadType.FromFile);
            List<object> configs = xmlWrapper.XmlToList("/Entitys/Entity", typeof(ConfigEntity));
            ShowMessage("读取配置成功");
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择数据结构保存的目录";
            string selectedPath = string.Empty;
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                selectedPath = folderBrowserDialog.SelectedPath;
            }
            folderBrowserDialog.Dispose();
            if (string.IsNullOrEmpty(selectedPath)) return;
            ///已创建的数据库表名，防止配置文件中有表名重复的项
            List<string> createTableNames = new List<string>();
            ///表结构
            DataTable dataTable = new DataTable();
            ///数据结构
            Dictionary<string, string> columns = new Dictionary<string, string>();
            columns.Add("TableName", "表名");
            dataTable.Columns.Add("TableName");
            columns.Add("DisplayOrder", "序号");
            dataTable.Columns.Add("DisplayOrder");
            columns.Add("FieldName", "字段名");
            dataTable.Columns.Add("FieldName");
            columns.Add("FieldDescription", "字段描述");
            dataTable.Columns.Add("FieldDescription");
            columns.Add("DataType", "数据类型");
            dataTable.Columns.Add("DataType");
            columns.Add("DataLength", "字段长度");
            dataTable.Columns.Add("DataLength");
            columns.Add("NullFlag", "允许空");
            dataTable.Columns.Add("NullFlag");
            columns.Add("Comments", "备注");
            dataTable.Columns.Add("Comments");
            foreach (ConfigEntity config in configs)
            {
                if (string.IsNullOrEmpty(config.TableName)) continue;
                if (string.IsNullOrEmpty(config.Schema)) continue;
                if (createTableNames.Contains(config.TableName)) continue;
                if (string.IsNullOrEmpty(config.PathName)) continue;
                #region 获取表注释
                object objTableDescription = BLL.SYS.CommonBLL.ExecuteScalar("select VALUE from fn_listextendedproperty (NULL, 'schema', '" + config.Schema + "', 'table', '" + config.TableName + "', NULL, NULL)");
                if (config.DbCreateType == 20)
                    objTableDescription = BLL.SYS.CommonBLL.ExecuteScalar("select VALUE from fn_listextendedproperty ('MS_Description', 'schema', '" + config.Schema + "', 'view', '" + config.TableName + "', NULL, NULL)");
                string tableDescription = string.Empty;
                if (objTableDescription == null || objTableDescription == DBNull.Value)
                {
                    DialogInput dialog = new DialogInput("请输入【" + config.Schema + "." + config.TableName + "】的表描述信息");
                    if (dialog.ShowDialog(this) != DialogResult.Yes)
                    {
                        dialog.Dispose();
                        ErrorMessage("未输入【" + config.Schema + "." + config.TableName + "】的表描述");
                        return;
                    }
                    tableDescription = dialog.InputMsg;
                    dialog.Dispose();
                    ///添加注释
                    if (config.DbCreateType == 20)
                    {
                        BLL.SYS.CommonBLL.ExecuteNonQueryBySql("EXEC dbo.sp_addextendedproperty @name=N'MS_Description', @value=N'" + tableDescription + "' , @level0type=N'SCHEMA',@level0name=N'" + config.Schema + "', @level1type=N'view',@level1name=N'" + config.TableName + "'");
                    }
                    else
                    {
                        // string[] path_name111 = config.PathName.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        //int cnt1 = path_name111[0].Length;
                        // string sheetName11 = path_name111.Length > 1 ? config.PathName.Substring(cnt1 + 1) : config.PathName;
                        BLL.SYS.CommonBLL.ExecuteNonQueryBySql("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + tableDescription + "' , @level0type=N'SCHEMA',@level0name=N'" + config.Schema + "', @level1type=N'TABLE',@level1name=N'" + config.TableName + "'");
                        // continue;
                    }
                }
                else
                    tableDescription = objTableDescription.ToString();
                if (string.IsNullOrEmpty(tableDescription))
                {
                    ErrorMessage("未输入【" + config.Schema + "." + config.TableName + "】的表描述");
                    return;
                }
                ///替换数据库中的回车换行符
                tableDescription = tableDescription.Replace("\r\n", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
                #endregion

                ///获取表结构
                List<TableField> fields = GetFieldsDataSource(config.TableName);
                if (fields.Count == 0) continue;

                int rowNo = 0;
                dataTable.Rows.Clear();
                foreach (var field in fields)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["TableName"] = config.Schema + "." + config.TableName;
                    rowNo++;
                    if (valuePairsRowNo.TryGetValue(rowNo, out string strNo))
                        dataRow["DisplayOrder"] = strNo;
                    else
                        dataRow["DisplayOrder"] = rowNo;
                    dataRow["FieldName"] = field.fieldName;
                    dataRow["FieldDescription"] = field.description;
                    dataRow["DataType"] = field.dataType;
                    dataRow["DataLength"] = field.maxLength;
                    dataRow["NullFlag"] = field.isNull;
                    dataRow["Comments"] = string.Empty;
                    dataTable.Rows.Add(dataRow);
                }
                ///EXCEL页签名称
                string[] path_name = config.PathName.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                int cnt = path_name[0].Length;
                string sheetName = path_name.Length > 1 ? config.PathName.Substring(cnt + 1) : config.PathName;
                ///文件名称                
                string fileName = path_name[0];
                if (File.Exists(selectedPath + @"\" + fileName + ".xlsx"))
                    ///追加
                    NpoiHelper.AppendToExcel(dataTable, columns, tableDescription, selectedPath + @"\" + fileName + ".xlsx");
                else
                    NpoiHelper.TableToExcel(dataTable, columns, new Dictionary<string, Dictionary<string, string>>(), tableDescription, selectedPath + @"\" + fileName + ".xlsx");
                createTableNames.Add(config.TableName);
            }
            MessageBox.Show(
                    "生成成功",
                    "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
        }

        private void menuBtnInsertEntityname_Click(object sender, EventArgs e)
        {
            DialogInput dialog = new DialogInput("请输入实体名称");
            if (dialog.ShowDialog(this) != DialogResult.Yes)
            {
                dialog.Dispose();
                return;
            }
            string entityName = dialog.InputMsg;
            entityInfo = new EntityBLL().GetInfo(entityName);
            menuBtnInsertSys_Click(null, null);
            entityInfo = null;
        }

        private void menuBtnDeleteEntityName_Click(object sender, EventArgs e)
        {
            DialogInput dialog = new DialogInput("请输入实体名称");
            if (dialog.ShowDialog(this) != DialogResult.Yes)
            {
                dialog.Dispose();
                return;
            }
            string entityName = dialog.InputMsg;
            entityInfo = new EntityBLL().GetInfo(entityName);
            menuBtnDeleteEntity_Click(null, null);
            entityInfo = null;
        }
        /// <summary>
        /// 创建数据表脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnDbCreateTable_Click(object sender, EventArgs e)
        {
            if (entityInfo == null)
            {
                MessageBox.Show(
                    "请先选择数据库表或视图",
                    "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                return;
            }
            ///表名
            string tableName = entityInfo.TableNames;
            ///架构
            string schema = "dbo";
            if (menuComboMould.Text.ToUpper() != "SYS")
                schema = menuComboMould.Text.ToUpper();
            if (dgvFields.DataSource == null) return;

            object objTableDescription = BLL.SYS.CommonBLL.ExecuteScalar("select VALUE from fn_listextendedproperty (NULL, 'schema', '" + schema + "', 'table', '" + tableName + "', NULL, NULL)");
            string tableDescription = string.Empty;
            if (objTableDescription == null || objTableDescription == DBNull.Value)
            {
                DialogInput dialog = new DialogInput("请输入数据表的描述信息");
                if (dialog.ShowDialog(this) != DialogResult.Yes)
                {
                    dialog.Dispose();
                    return;
                }
                tableDescription = dialog.InputMsg;
                dialog.Dispose();
                ///添加注释
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + tableDescription + "' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "'");
            }
            else
                tableDescription = objTableDescription.ToString();
            if (string.IsNullOrEmpty(tableDescription)) return;

            List<TableField> fields = dgvFields.DataSource as List<TableField>;
            if (fields.Count == 0) return;
            List<string> unFields = new List<string>();
            unFields.Add("ID");
            unFields.Add("FID");
            unFields.Add("VALID_FLAG");
            unFields.Add("CREATE_DATE");
            unFields.Add("CREATE_USER");
            unFields.Add("MODIFY_DATE");
            unFields.Add("MODIFY_USER");
            List<TableField> tableFields = fields.Where(d => !unFields.Contains(d.fieldName)).OrderBy(d => d.dbId).ToList();

            StringBuilder sb = new StringBuilder();
            ///创建表
            sb.AppendLine("/****** [" + schema + "].[" + tableName + "] " + tableDescription + " Script Date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "******/");
            sb.AppendLine("SET ANSI_NULLS ON");
            sb.AppendLine("");
            sb.AppendLine("SET QUOTED_IDENTIFIER ON");
            sb.AppendLine("IF not exists(select * from sysobjects where NAME = '" + tableName + "' and TYPE = 'U')");
            sb.AppendLine("BEGIN");
            sb.AppendLine("CREATE TABLE [" + schema + "].[" + tableName + "](");
            sb.AppendLine("	[ID] [bigint] IDENTITY(1,1) NOT NULL,");
            sb.AppendLine("	[FID] [uniqueidentifier] NULL,");
            sb.AppendLine("	[VALID_FLAG] [bit] NOT NULL,");
            sb.AppendLine("	[CREATE_USER] [nvarchar](32) NOT NULL,");
            sb.AppendLine("	[CREATE_DATE] [datetime] NOT NULL,");
            sb.AppendLine("	[MODIFY_USER] [nvarchar](32) NULL,");
            sb.AppendLine("	[MODIFY_DATE] [datetime] NULL,");
            sb.AppendLine(" CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED ");
            sb.AppendLine("(");
            sb.AppendLine("	[ID] ASC");
            sb.AppendLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
            sb.AppendLine(") ON [PRIMARY]");
            sb.AppendLine("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据有效标记' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'VALID_FLAG'");
            sb.AppendLine("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'CREATE_USER'");
            sb.AppendLine("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'CREATE_DATE'");
            sb.AppendLine("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新用户' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'MODIFY_USER'");
            sb.AppendLine("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'MODIFY_DATE'");
            sb.AppendLine("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + tableDescription + "' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "'");
            sb.AppendLine("END");
            sb.AppendLine("");
            ///创建字段
            string missDescriptionFields = string.Empty;
            for (int i = 0; i < tableFields.Count; i++)
            {
                string fieldName = tableFields[i].fieldName;
                string fieldDescription = tableFields[i].description;
                ///没有注释，需要补充
                if (string.IsNullOrEmpty(fieldDescription))
                {
                    DialogInput dialog = new DialogInput("请输入" + fieldName + "的字段注释");
                    if (dialog.ShowDialog(this) != DialogResult.Yes)
                    {
                        dialog.Dispose();
                        return;
                    }
                    fieldDescription = dialog.InputMsg;
                    ///添加注释
                    string sql = "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + fieldDescription + "' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'" + fieldName + "'";
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sql);
                    dialog.Dispose();
                }
                string fieldDataType = tableFields[i].dataType;
                int fieldDataLength = tableFields[i].precision;
                switch (fieldDataType)
                {
                    case "int":
                    case "bit":
                    case "bigint":
                    case "uniqueidentifier":
                    case "image":
                    case "datetime": break;
                    case "decimal": fieldDataType += "(18,4)"; break;
                    case "nvarchar":
                        if (fieldDataLength > 0)
                            fieldDataType += "(" + fieldDataLength + ")";
                        else
                            fieldDataType += "(max)";
                        break;
                    default:
                        MessageBox.Show(
                        "未知的数据类型:" + fieldDataType,
                        "错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                        return;
                }
                sb.AppendLine("---" + fieldName + " " + fieldDescription + "");
                sb.AppendLine("IF not exists(select * from syscolumns where ID = object_id('" + schema + "." + tableName + "') and NAME = '" + fieldName + "')");
                sb.AppendLine("BEGIN");
                sb.AppendLine("	ALTER TABLE " + schema + "." + tableName + " ADD " + fieldName + " " + fieldDataType + " NULL");
                if (!string.IsNullOrEmpty(fieldDescription))
                    sb.AppendLine(" EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + fieldDescription + "' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'" + fieldName + "'");
                sb.AppendLine("END");
            }
            ///
            SaveFileDialog dialogSave = new SaveFileDialog();
            dialogSave.FileName = "DB_CREATE_" + entityInfo.TableNames + ".sql";
            if (dialogSave.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(dialogSave.FileName))
                    File.Create(dialogSave.FileName).Close();
                using (StreamWriter sw = new StreamWriter(dialogSave.FileName))
                {
                    sw.WriteLine(sb.ToString());
                    sw.Close();
                }
            }
        }
        /// <summary>
        /// 删除数据表脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnDbDropTable_Click(object sender, EventArgs e)
        {
            if (entityInfo == null)
            {
                MessageBox.Show(
                    "请先选择数据库表或视图",
                    "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                return;
            }
            ///表名
            string tableName = entityInfo.TableNames;
            ///架构
            string schema = "dbo";
            if (menuComboMould.Text.ToUpper() != "SYS")
                schema = menuComboMould.Text.ToUpper();
            if (dgvFields.DataSource == null) return;

            object objTableDescription = BLL.SYS.CommonBLL.ExecuteScalar("select VALUE from fn_listextendedproperty (NULL, 'schema', '" + schema + "', 'table', '" + tableName + "', NULL, NULL)");
            string tableDescription = string.Empty;
            if (objTableDescription == null || objTableDescription == DBNull.Value)
                tableDescription = string.Empty;
            else
                tableDescription = objTableDescription.ToString();
            if (string.IsNullOrEmpty(tableDescription)) return;

            List<TableField> fields = dgvFields.DataSource as List<TableField>;
            if (fields.Count == 0) return;
            List<string> unFields = new List<string>();
            unFields.Add("ID");
            unFields.Add("FID");
            unFields.Add("VALID_FLAG");
            unFields.Add("CREATE_DATE");
            unFields.Add("CREATE_USER");
            unFields.Add("MODIFY_DATE");
            unFields.Add("MODIFY_USER");
            List<TableField> tableFields = fields.Where(d => !unFields.Contains(d.fieldName)).OrderBy(d => d.dbId).ToList();

            StringBuilder sb = new StringBuilder();
            ///创建表
            sb.AppendLine("/****** [" + schema + "].[" + tableName + "] " + tableDescription + " Script Date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "******/");

            ///创建字段
            string missDescriptionFields = string.Empty;
            for (int i = 0; i < tableFields.Count; i++)
            {
                string fieldName = tableFields[i].fieldName;
                string fieldDescription = tableFields[i].description;
                string fieldDataType = tableFields[i].dataType;
                switch (fieldDataType)
                {
                    case "int":
                    case "bit":
                    case "bigint":
                    case "uniqueidentifier":
                    case "datetime":
                    case "decimal":
                    case "image":
                    case "nvarchar": break;
                    default:
                        MessageBox.Show(
                        "未知的数据类型:" + fieldDataType,
                        "错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                        return;
                }
                sb.AppendLine("---" + fieldName + " " + fieldDescription + "");
                sb.AppendLine("IF exists(select * from syscolumns where ID = object_id('" + schema + "." + tableName + "') and NAME = '" + fieldName + "')");
                sb.AppendLine("BEGIN");
                sb.AppendLine("  EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'" + fieldName + "'");
                sb.AppendLine("  ALTER TABLE " + schema + "." + tableName + " DROP COLUMN " + fieldName + "");
                sb.AppendLine("END");
            }
            sb.AppendLine("IF exists(select * from sysobjects where NAME = '" + tableName + "' and TYPE = 'U')");
            sb.AppendLine("BEGIN");
            sb.AppendLine("  EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'VALID_FLAG'");
            sb.AppendLine("  EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'CREATE_USER'");
            sb.AppendLine("  EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'CREATE_DATE'");
            sb.AppendLine("  EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'MODIFY_USER'");
            sb.AppendLine("  EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'MODIFY_DATE'");
            sb.AppendLine("  EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "'");
            sb.AppendLine("  DROP TABLE [" + schema + "].[" + tableName + "]");
            sb.AppendLine("END");
            sb.AppendLine("");
            ///
            SaveFileDialog dialogSave = new SaveFileDialog();
            dialogSave.FileName = "DB_DROP_" + entityInfo.TableNames + ".sql";
            if (dialogSave.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(dialogSave.FileName))
                    File.Create(dialogSave.FileName).Close();
                using (StreamWriter sw = new StreamWriter(dialogSave.FileName))
                {
                    sw.WriteLine(sb.ToString());
                    sw.Close();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void DoPublishSql(List<object> configs, string selectedPath)
        {
            Invoke(new Action(() =>
            {
                toolStripProgressBar.Maximum = configs.Count;
                toolStripProgressBar.Value = 0;
            }));
            foreach (ConfigEntity config in configs)
            {
                Invoke(new Action(() =>
                {
                    toolStripProgressBar.Value++;
                }));
                if (string.IsNullOrEmpty(config.Name))
                {
                    ErrorMessage("配置信息错误，模型名称不能为空");
                    return;
                }
                if (string.IsNullOrEmpty(config.PathName))
                {
                    ErrorMessage("配置信息错误，【" + config.Name + "】路径名称不能为空");
                    return;
                }
                ///获取模型
                EntityInfo entity = new EntityBLL().GetInfo(config.Name);
                if (entity == null)
                {
                    ErrorMessage("配置信息错误，【" + config.Name + "】数据模型不存在");
                    return;
                }
                ///数据模型配置
                string sql = CreateEntitySql(entity, config.MenuFlag);
                ///创建数据模型配置的脚本文件名
                string fileName = "CREATE_" + (string.IsNullOrEmpty(config.TableName) ? config.Name : config.TableName) + ".sql";
                if (!Directory.Exists(selectedPath + @"\" + config.PathName))
                    Directory.CreateDirectory(selectedPath + @"\" + config.PathName);
                ///文件路径
                fileName = selectedPath + @"\" + config.PathName + @"\" + fileName;
                if (!File.Exists(fileName))
                    File.Create(fileName).Close();
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(sql);
                    sw.Close();
                }
                ///删除模型配置
                sql = DeleteEntitySql(entity, config.MenuFlag);
                fileName = "DELETE_" + (string.IsNullOrEmpty(config.TableName) ? config.Name : config.TableName) + ".sql";
                fileName = selectedPath + @"\" + config.PathName + @"\" + fileName;
                if (!File.Exists(fileName))
                    File.Create(fileName).Close();
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(sql);
                    sw.Close();
                }
                ///不涉及表结构的情况下，继续执行下一个数据模型
                if (string.IsNullOrEmpty(config.TableName)) continue;
                if (string.IsNullOrEmpty(config.Schema))
                {
                    ErrorMessage("配置信息错误，【" + config.Name + "】数据架构不能为空");
                    return;
                }
                ///创建数据结构
                if (config.DbCreateType == 10)
                    sql = DbCreateTableSql(config.TableName, config.Schema);
                ///创建视图
                if (config.DbCreateType == 20)
                    sql = DbCreateViewSql(config.TableName, config.Schema);

                fileName = "DB_CREATE_" + config.TableName + ".sql";
                fileName = selectedPath + @"\" + config.PathName + @"\" + fileName;
                if (!File.Exists(fileName))
                    File.Create(fileName).Close();
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(sql);
                    sw.Close();
                }
                ///删除数据结构
                if (config.DbCreateType == 10)
                    sql = DbDropTableSql(config.TableName, config.Schema);
                ///删除视图
                if (config.DbCreateType == 20)
                    sql = DbDropViewSql(config.TableName, config.Schema);
                fileName = "DB_DROP_" + config.TableName + ".sql";
                fileName = selectedPath + @"\" + config.PathName + @"\" + fileName;
                if (!File.Exists(fileName))
                    File.Create(fileName).Close();
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(sql);
                    sw.Close();
                }
                Invoke(new Action(() =>
                {
                    lblMessage.Text = config.Name;
                    lblMessage.BackColor = Color.Green;
                    lblMessage.ForeColor = Color.White;
                }));
            }

            #region TS_SYS_CONFIG
            string sqlConfig = "delete from dbo.TS_SYS_CONFIG where [VALID_FLAG] = 0;\r\n";
            ///CONFIG
            List<ConfigInfo> configInfos = new ConfigBLL().GetListByPage(string.Empty, string.Empty, 1, int.MaxValue, out int dataCnt);
            foreach (var configInfo in configInfos)
            {
                #region TS_SYS_CONFIG
                ConfigInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(configInfo) as ConfigInfo;
                sqlConfig += "---CONFIG:" + info.Name + "\r\n";
                sqlConfig += "if not exists (select * from dbo.TS_SYS_CONFIG with(nolock) where [CODE] = '" + info.Code + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_CONFIG "
                    + "([FID]"
                    + ",[NAME]"
                    + ",[CODE]"
                    + ",[CONFIG_VALUE]"
                    + ",[COMMENTS]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_USER]"
                    + ",[CREATE_DATE]) "
                    + "values "
                    + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                        + ",'" + info.Name + "'"///NAME
                        + ",'" + info.Code + "'"///CODE
                        + ",'" + info.ConfigValue + "'"///CONFIG_VALUE
                        + ",'" + info.Comments + "'"///COMMENTS
                        + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                        + ",'" + loginUser + "'"///CREATE_USER
                        + ",GETDATE()"///CREATE_DATE
                        + ");\r\n";
                #endregion
            }
            string fileNameConfig = "DB_SYS_CONFIG.sql";
            if (!Directory.Exists(selectedPath + @"\系统配置"))
                Directory.CreateDirectory(selectedPath + @"\系统配置");
            ///文件路径
            fileNameConfig = selectedPath + @"\系统配置\" + fileNameConfig;
            if (!File.Exists(fileNameConfig))
                File.Create(fileNameConfig).Close();
            using (StreamWriter sw = new StreamWriter(fileNameConfig))
            {
                sw.WriteLine(sqlConfig);
                sw.Close();
            }
            #endregion

            #region TS_SYS_HANDLER
            sqlConfig = "delete from dbo.TS_SYS_HANDLER where [VALID_FLAG] = 0;\r\n";
            ///HANDLER
            List<HandlerInfo> handlers = new HandlerBLL().GetListByPage(string.Empty, string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var handlerinfo in handlers)
            {
                #region TS_SYS_HANDLER
                HandlerInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(handlerinfo) as HandlerInfo;
                sqlConfig += "---HANDLER:" + info.AjaxMethodName + "\r\n";
                sqlConfig += "if not exists (select * from dbo.TS_SYS_HANDLER with(nolock) where [AJAX_METHOD_NAME] = '" + info.AjaxMethodName + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_HANDLER "
                    + "([FID]"
                    + ",[AJAX_METHOD_NAME]"
                    + ",[ASSEMBLY_NAME]"
                    + ",[CLASS_NAME]"
                    + ",[SERVER_METHOD_NAME]"
                    + ",[STATUS]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_DATE]"
                    + ",[CREATE_USER]"
                    + ",[COMMENTS]) "
                    + "values "
                    + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + info.AjaxMethodName + "'"///AJAX_METHOD_NAME
                    + ",'" + info.AssemblyName + "'"///ASSEMBLY_NAME
                    + ",'" + info.ClassName + "'"///CLASS_NAME
                    + ",'" + info.ServerMethodName + "'"///SERVER_METHOD_NAME
                   
                    + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",GETDATE()"///CREATE_DATE
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ",'" + info.Comments + "'"///COMMENTS
                    + ");\r\n";
                #endregion
            }
            fileNameConfig = "DB_SYS_HANDLER.sql";
            if (!Directory.Exists(selectedPath + @"\系统配置"))
                Directory.CreateDirectory(selectedPath + @"\系统配置");
            ///文件路径
            fileNameConfig = selectedPath + @"\系统配置\" + fileNameConfig;
            if (!File.Exists(fileNameConfig))
                File.Create(fileNameConfig).Close();
            using (StreamWriter sw = new StreamWriter(fileNameConfig))
            {
                sw.WriteLine(sqlConfig);
                sw.Close();
            }
            #endregion

            #region TS_SYS_MESSAGE
            sqlConfig = "delete from dbo.TS_SYS_MESSAGE where [VALID_FLAG] = 0;\r\n";
            ///MESSAGE
            List<MessageInfo> messages = new MessageBLL().GetListByPage(string.Empty, string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var messageinfo in messages)
            {
                #region TS_SYS_MESSAGE
                MessageInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(messageinfo) as MessageInfo;
                sqlConfig += "---MESSAGE:" + info.MessageCode + "\r\n";
                sqlConfig += "if not exists (select * from [dbo].[TS_SYS_MESSAGE] with(nolock) where [MESSAGE_CODE] = '" + info.MessageCode + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_MESSAGE "
                    + "([FID]"
                    + ",[MESSAGE_CODE]"
                    + ",[MESSAGE_TYPE]"
                    + ",[MESSAGE_CN]"
                    + ",[MESSAGE_EN]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_DATE]"
                    + ",[CREATE_USER]) "
                    + "values "
                    + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + info.MessageCode + "'"///MESSAGE_CODE
                    + "," + info.MessageType.GetValueOrDefault() + ""///MESSAGE_TYPE
                    + ",'" + info.MessageCn.Replace("\'", "\"") + "'"///MESSAGE_CN
                    + ",'" + info.MessageEn.Replace("\'", "\"") + "'"///MESSAGE_EN
                    + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",GETDATE()"///CREATE_DATE
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ");\r\n";
                #endregion
            }
            fileNameConfig = "DB_SYS_MESSAGE.sql";
            if (!Directory.Exists(selectedPath + @"\系统配置"))
                Directory.CreateDirectory(selectedPath + @"\系统配置");
            ///文件路径
            fileNameConfig = selectedPath + @"\系统配置\" + fileNameConfig;
            if (!File.Exists(fileNameConfig))
                File.Create(fileNameConfig).Close();
            using (StreamWriter sw = new StreamWriter(fileNameConfig))
            {
                sw.WriteLine(sqlConfig);
                sw.Close();
            }
            #endregion

            #region TS_SYS_PROCESS_SCHEDULE
            sqlConfig = "delete from dbo.TS_SYS_PROCESS_SCHEDULE where [VALID_FLAG] = 0;\r\n";
            ///PROCESS_SCHEDULE
            List<ProcessScheduleInfo> processScheduleInfos = new ProcessScheduleBLL().GetListByPage("[LAST_RUN_STATUS] = " + (int)ProcessRunStatusConstants.Running + " ", string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var processScheduleInfo in processScheduleInfos)
            {
                #region TS_SYS_PROCESS_SCHEDULE
                ProcessScheduleInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(processScheduleInfo) as ProcessScheduleInfo;
                sqlConfig += "---PROCESS_SCHEDULE:" + info.ProcessCode + "\r\n";
                sqlConfig += "if not exists (select * from dbo.TS_SYS_PROCESS_SCHEDULE with(nolock) where [PROCESS_CODE] = '" + info.ProcessCode + "' and [VALID_FLAG] = 1)\r\n"
                    + @"insert into [dbo].[TS_SYS_PROCESS_SCHEDULE]
                               ([FID]
                               ,[PROCESS_CODE]
                               ,[PROCESS_NAME]
                               ,[LAST_RUN_STATUS]
                               ,[RUN_INTERVAL]
                               ,[CHECK_INTERVAL]
                               ,[SYSTEM_PARAMETER1]
                               ,[SYSTEM_PARAMETER2]
                               ,[SYSTEM_PARAMETER3]
                               ,[SYSTEM_PARAMETER4]
                               ,[SYSTEM_PARAMETER5]
                               ,[VALID_FLAG]
                               ,[CREATE_DATE]
                               ,[CREATE_USER])
                         values "
                                + "(N'" + info.Fid.GetValueOrDefault() + "'"
                                + ",N'" + info.ProcessCode + "'"
                                + ",N'" + info.ProcessName + "'"
                                + "," + (int)ProcessRunStatusConstants.Init + ""
                                + "," + info.RunInterval.GetValueOrDefault() + ""
                                + "," + info.CheckInterval.GetValueOrDefault() + ""
                                + ",N'" + info.SystemParameter1 + "'"
                                + ",N'" + info.SystemParameter2 + "'"
                                + ",N'" + info.SystemParameter3 + "'"
                                + ",N'" + info.SystemParameter4 + "'"
                                + ",N'" + info.SystemParameter5 + "'"
                                + ",1,GETDATE(),N'" + loginUser + "');\r\n";
                #endregion
            }
            fileNameConfig = "DB_SYS_PROCESS_SCHEDULE.sql";
            if (!Directory.Exists(selectedPath + @"\系统配置"))
                Directory.CreateDirectory(selectedPath + @"\系统配置");
            ///文件路径
            fileNameConfig = selectedPath + @"\系统配置\" + fileNameConfig;
            if (!File.Exists(fileNameConfig))
                File.Create(fileNameConfig).Close();
            using (StreamWriter sw = new StreamWriter(fileNameConfig))
            {
                sw.WriteLine(sqlConfig);
                sw.Close();
            }
            #endregion

            #region TS_SYS_SEQ_DEFINE
            sqlConfig = "delete from dbo.TS_SYS_SEQ_DEFINE where [VALID_FLAG] = 0;\r\n"
            + "delete from dbo.TS_SYS_SEQ_SECTION where [VALID_FLAG] = 0;\r\n";
            ///SEQ_DEFINE
            List<SeqDefineInfo> seqDefineInfos = new SeqDefineBLL().GetListByPage(string.Empty, string.Empty, 1, int.MaxValue, out dataCnt);
            List<SeqSectionInfo> seqSectionInfos = new List<SeqSectionInfo>();
            if (seqDefineInfos.Count > 0)
                seqSectionInfos = new SeqSectionBLL().GetListByPage("[DEFINE_FID] in ('" + string.Join("','", seqDefineInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var seqDefineInfo in seqDefineInfos)
            {
                #region TS_SYS_SEQ_DEFINE
                SeqDefineInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(seqDefineInfo) as SeqDefineInfo;
                sqlConfig += "---SEQ_DEFINE:" + info.SeqCode + "|" + info.SeqName + "\r\n";
                sqlConfig += "if not exists (select * from [dbo].[TS_SYS_SEQ_DEFINE] with(nolock) where [SEQ_CODE] = '" + info.SeqCode + "' and [VALID_FLAG] = 1)\r\n"
                    + @"insert into [dbo].[TS_SYS_SEQ_DEFINE]
                               ([FID]
                               ,[SEQ_CODE]
                               ,[SEQ_NAME]
                               ,[SECTION_NUM]
                               ,[JOIN_CHAR]
                               ,[VALID_FLAG]
                               ,[CREATE_USER]
                               ,[CREATE_DATE])
                         values "
                                + "(N'" + info.Fid.GetValueOrDefault() + "'"
                                + ",N'" + info.SeqCode + "'"
                                + ",N'" + info.SeqName + "'"
                                + "," + info.SectionNum.GetValueOrDefault() + ""
                                + ",N'" + info.JoinChar + "'"
                                + ",1,N'" + loginUser + "',GETDATE());\r\n";
                #endregion
                foreach (var seqSectionInfo in seqSectionInfos.Where(d => d.DefineFid.GetValueOrDefault() == info.Fid.GetValueOrDefault()).ToList())
                {
                    #region TS_SYS_SEQ_SECTION
                    SeqSectionInfo sectionInfo = BLL.SYS.CommonBLL.FieldNullToEmpty(seqSectionInfo) as SeqSectionInfo;
                    sqlConfig += "---SEQ_SECTION:" + info.SeqCode + "|" + sectionInfo.SectionSeq + "\r\n";
                    sqlConfig += "if not exists (select * from [dbo].[TS_SYS_SEQ_SECTION] with(nolock) where [DEFINE_FID] = '" + info.Fid + "' and [SECTION_SEQ] = " + sectionInfo.SectionSeq.GetValueOrDefault() + " and [VALID_FLAG] = 1)\r\n"
                        + @"insert into [dbo].[TS_SYS_SEQ_SECTION]
                                   ([FID]
                                   ,[DEFINE_FID]
                                   ,[SEQ_CODE]
                                   ,[SECTION_SEQ]
                                   ,[IS_FIXED_LENGTH]
                                   ,[LENGTH]
                                   ,[FILL_TYPE]
                                   ,[FILL_CHAR]
                                   ,[DATA_GENERATE_TYPE]
                                   ,[STEP_LENGTH]
                                   ,[DEFAULT_VALUE]
                                   ,[MIN_VALUE]
                                   ,[MAX_VALUE]
                                   ,[IS_CYCLE]
                                   ,[IS_AUTOUP]
                                   ,[IS_SEED_VALUE]
                                   ,[VALID_FLAG]
                                   ,[CREATE_USER]
                                   ,[CREATE_DATE])
                             values "
                                    + "(N'" + sectionInfo.Fid.GetValueOrDefault() + "'"
                                    + ",N'" + sectionInfo.DefineFid.GetValueOrDefault() + "'"
                                    + ",N'" + sectionInfo.SeqCode + "'"
                                    + "," + sectionInfo.SectionSeq.GetValueOrDefault() + ""
                                    + "," + (sectionInfo.IsFixedLength.GetValueOrDefault() ? "1" : "0") + ""
                                    + "," + sectionInfo.Length.GetValueOrDefault() + ""
                                    + "," + sectionInfo.FillType.GetValueOrDefault() + ""
                                    + ",N'" + sectionInfo.FillChar + "'"
                                    + "," + sectionInfo.DataGenerateType.GetValueOrDefault() + ""
                                    + "," + sectionInfo.StepLength.GetValueOrDefault() + ""
                                    + ",N'" + sectionInfo.DefaultValue + "'"
                                    + "," + sectionInfo.MinValue.GetValueOrDefault() + ""
                                    + "," + sectionInfo.MaxValue.GetValueOrDefault() + ""
                                    + "," + (sectionInfo.IsCycle.GetValueOrDefault() ? "1" : "0") + ""
                                    + "," + (sectionInfo.IsAutoup.GetValueOrDefault() ? "1" : "0") + ""
                                    + "," + (sectionInfo.IsSeedValue.GetValueOrDefault() ? "1" : "0") + ""
                                    + ",1,N'" + loginUser + "',GETDATE());\r\n";
                    #endregion
                }
            }
            fileNameConfig = "DB_SYS_SEQ_DEFINE.sql";
            if (!Directory.Exists(selectedPath + @"\系统配置"))
                Directory.CreateDirectory(selectedPath + @"\系统配置");
            ///文件路径
            fileNameConfig = selectedPath + @"\系统配置\" + fileNameConfig;
            if (!File.Exists(fileNameConfig))
                File.Create(fileNameConfig).Close();
            using (StreamWriter sw = new StreamWriter(fileNameConfig))
            {
                sw.WriteLine(sqlConfig);
                sw.Close();
            }
            #endregion

            #region TS_SYS_CODE
            sqlConfig = "delete from dbo.TS_SYS_CODE where [VALID_FLAG] = 0;\r\n"
            + "delete from dbo.TS_SYS_CODE_ITEM where [VALID_FLAG] = 0;\r\n";
            ///CODE&ITEM
            ///未在界面配置中出现的CODE
            List<CodeInfo> codeInfos = new CodeBLL().GetListByPage("[CODE_NAME] not in (select [EXTEND1] from dbo.[TS_SYS_ENTITY_FIELD] with(nolock) where [VALID_FLAG] = 1 and LEN([EXTEND1])>0)"
                , string.Empty, 1, int.MaxValue, out dataCnt);
            List<CodeItemInfo> codeItemInfos = new List<CodeItemInfo>();
            if (codeInfos.Count > 0)
                codeItemInfos = new CodeItemBLL().GetListByPage("[CODE_FID] in ('" + string.Join("','", codeInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var codeInfo in codeInfos)
            {
                #region TS_SYS_CODE
                CodeInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(codeInfo) as CodeInfo;
                sqlConfig += "---CODE:" + info.CodeName + "\r\n";
                sqlConfig += "if not exists (select * from [dbo].[TS_SYS_CODE] with(nolock) where [CODE_NAME] = '" + info.CodeName + "' and [VALID_FLAG] = 1)\r\n"
                    + "insert into dbo.TS_SYS_CODE "
                    + "([FID]"
                    + ",[CODE_NAME]"
                    + ",[CODE_NAME_CN]"
                    + ",[COMMENTS]"
                    + ",[VALID_FLAG]"
                    + ",[CREATE_DATE]"
                    + ",[CREATE_USER]) "
                    + "values "
                    + "('" + info.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + info.CodeName + "'"///CODE_NAME
                    + ",'" + info.CodeNameCn + "'"///CODE_NAME_CN
                    + ",'" + info.Comments.Replace("\'", "\"") + "'"///COMMENTS
                    + "," + (info.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",GETDATE()"///CREATE_DATE
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ");\r\n";
                #endregion
                foreach (var codeItemInfo in codeItemInfos.Where(d => d.CodeFid.GetValueOrDefault() == codeInfo.Fid.GetValueOrDefault()).ToList())
                {
                    #region TS_SYS_CODE_ITEM
                    CodeItemInfo itemInfo = BLL.SYS.CommonBLL.FieldNullToEmpty(codeItemInfo) as CodeItemInfo;
                    sqlConfig += "---CODE_ITEM:" + itemInfo.ItemValue + "|" + itemInfo.ItemName + "\r\n";
                    sqlConfig += "if not exists (select * from [dbo].[TS_SYS_CODE_ITEM] with(nolock) where [CODE_FID] = N'" + info.Fid.GetValueOrDefault() + "' and [ITEM_VALUE] = " + itemInfo.ItemValue.GetValueOrDefault() + " and [VALID_FLAG] = 1)\r\n"
                        + "insert into dbo.TS_SYS_CODE_ITEM "
                        + "([FID]"
                        + ",[CODE_FID]"
                        + ",[ITEM_VALUE]"
                        + ",[ITEM_NAME]"
                        + ",[ITEM_NAME_EN]"
                        + ",[DISPLAY_ORDER]"
                        + ",[COMMENTS]"
                        + ",[VALID_FLAG]"
                        + ",[CREATE_DATE]"
                        + ",[CREATE_USER]) "
                        + "values "
                        + "('" + itemInfo.Fid.GetValueOrDefault() + "'"///FID
                    + ",'" + itemInfo.CodeFid.GetValueOrDefault() + "'"///CODE_FID
                    + "," + itemInfo.ItemValue.GetValueOrDefault() + ""///ITEM_VALUE
                    + ",'" + itemInfo.ItemName + "'"///ITEM_NAME
                    + ",'" + itemInfo.ItemNameEn + "'"///ITEM_NAME_EN
                    + "," + itemInfo.DisplayOrder.GetValueOrDefault() + ""///DISPLAY_ORDER
                    + ",'" + itemInfo.Comments.Replace("\'", "\"") + "'"///COMMENTS
                    + "," + (itemInfo.ValidFlag.GetValueOrDefault() ? "1" : "0") + ""///VALID_FLAG
                    + ",GETDATE()"///CREATE_DATE
                    + ",'" + loginUser + "'"///CREATE_USER
                    + ");\r\n";
                    #endregion
                }
            }
            fileNameConfig = "DB_SYS_CODE.sql";
            if (!Directory.Exists(selectedPath + @"\系统配置"))
                Directory.CreateDirectory(selectedPath + @"\系统配置");
            ///文件路径
            fileNameConfig = selectedPath + @"\系统配置\" + fileNameConfig;
            if (!File.Exists(fileNameConfig))
                File.Create(fileNameConfig).Close();
            using (StreamWriter sw = new StreamWriter(fileNameConfig))
            {
                sw.WriteLine(sqlConfig);
                sw.Close();
            }
            #endregion

            #region TS_SYS_PRINT_CONFIG
            sqlConfig = "delete from dbo.TS_SYS_PRINT_CONFIG where [VALID_FLAG] = 0;\r\n";
            ///PRINT_CONFIG
            List<PrintConfigInfo> printConfigInfos = new PrintConfigBLL().GetListByPage("[STATUS] = " + (int)BasicDataStatusConstants.Enable + " ", string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var printConfigInfo in printConfigInfos)
            {
                #region TS_SYS_PRINT_CONFIG
                PrintConfigInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(printConfigInfo) as PrintConfigInfo;
                sqlConfig += "---PRINT_CONFIG:" + info.PrintConfigCode + "|" + info.PrintConfigName + "\r\n";
                sqlConfig += "if not exists (select * from dbo.TS_SYS_PRINT_CONFIG with(nolock) where [PRINT_CONFIG_CODE] = '" + info.PrintConfigCode + "' and [VALID_FLAG] = 1)\r\n"
                    + @"insert into [dbo].[TS_SYS_PRINT_CONFIG]
                               ([FID]
                               ,[PRINT_CONFIG_CODE]
                               ,[PRINT_CONFIG_NAME]
                               ,[PRINT_TEMPLATE_FILENAME]
                               ,[PRINT_TEMPLATE_URL]
                               ,[TEMPLATE_FILE_TYPE]
                               ,[PRINT_COPIES]
                               ,[PRINTER_NAME]
                               ,[LAST_UPLOADFILE_TIME]
                               ,[STATUS]
                               ,[COMMENTS]
                               ,[VALID_FLAG]
                               ,[CREATE_USER]
                               ,[CREATE_DATE])
                         values "
                                + "(N'" + info.Fid.GetValueOrDefault() + "'"///FID
                                + ",N'" + info.PrintConfigCode + "'"///PRINT_CONFIG_CODE
                                + ",N'" + info.PrintConfigName + "'"///PRINT_CONFIG_NAME
                                + ",N'" + info.PrintTemplateFilename + "'"///PRINT_TEMPLATE_FILENAME
                                + ",N'" + info.PrintTemplateUrl + "'"///PRINT_TEMPLATE_URL
                                + "," + info.TemplateFileType.GetValueOrDefault() + ""///TEMPLATE_FILE_TYPE
                                + "," + info.PrintCopies.GetValueOrDefault() + ""///PRINT_COPIES
                                + ",N'" + info.PrinterName + "'"///PRINTER_NAME
                                + ",NULL"///LAST_UPLOADFILE_TIME
                                + "," + (int)BasicDataStatusConstants.Created + ""///STATUS
                                + ",N'" + info.Comments + "'"///COMMENTS
                                + ",1,N'" + loginUser + "',GETDATE());\r\n";///VALID_FLAG,CREATE_USER,CREATE_DATE
                #endregion
            }
            fileNameConfig = "DB_SYS_PRINT_CONFIG.sql";
            if (!Directory.Exists(selectedPath + @"\系统配置"))
                Directory.CreateDirectory(selectedPath + @"\系统配置");
            ///文件路径
            fileNameConfig = selectedPath + @"\系统配置\" + fileNameConfig;
            if (!File.Exists(fileNameConfig))
                File.Create(fileNameConfig).Close();
            using (StreamWriter sw = new StreamWriter(fileNameConfig))
            {
                sw.WriteLine(sqlConfig);
                sw.Close();
            }
            #endregion

            #region TS_SYS_INTERFACE_CONFIG
            sqlConfig = "delete from dbo.TS_SYS_INTERFACE_CONFIG where [VALID_FLAG] = 0;\r\n";
            ///INTERFACE_CONFIG
            List<InterfaceConfigInfo> interfaceConfigInfos = new InterfaceConfigBLL().GetListByPage(string.Empty, string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var interfaceConfigInfo in interfaceConfigInfos)
            {
                #region TS_SYS_INTERFACE_CONFIG
                InterfaceConfigInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(interfaceConfigInfo) as InterfaceConfigInfo;
                sqlConfig += "---INTERFACE_CONFIG:" + info.InterfaceCode + "\r\n";
                sqlConfig += "if not exists (select * from dbo.TS_SYS_INTERFACE_CONFIG with(nolock) where [INTERFACE_CODE] = '" + info.InterfaceCode + "' and [VALID_FLAG] = 1)\r\n"
                    + @"insert into [dbo].[TS_SYS_INTERFACE_CONFIG]
                                ([FID]
                                ,[INTERFACE_CODE]
                                ,[METHOD_NAME]
                                ,[SYS_NAME]
                                ,[SYS_METHOD_NAME]
                                ,[CALL_URL]
                                ,[USER_NAME]
                                ,[PASS_WORD]
                                ,[PARAM1]
                                ,[PARAM2]
                                ,[PARAM3]
                                ,[VALID_FLAG],[CREATE_USER],[CREATE_DATE])
                            values"
                                + "(N'" + info.Fid.GetValueOrDefault() + "'"
                                + ",N'" + info.InterfaceCode + "'"
                                + ",N'" + info.MethrodName + "'"
                                + ",N'" + info.SysName + "'"
                                + ",N'" + info.MethrodName + "'"
                                + ",N'" + info.CallUrl + "'"
                                + ",N'" + info.UserName + "'"
                                + ",N'" + info.PassWord + "'"
                                + ",N'" + info.Param1 + "'"
                                + ",N'" + info.Param2 + "'"
                                + ",N'" + info.Param3 + "'"
                                + ",1,N'" + loginUser + "',GETDATE());\r\n";
                #endregion
            }
            fileNameConfig = "DB_SYS_INTERFACE_CONFIG.sql";
            if (!Directory.Exists(selectedPath + @"\系统配置"))
                Directory.CreateDirectory(selectedPath + @"\系统配置");
            ///文件路径
            fileNameConfig = selectedPath + @"\系统配置\" + fileNameConfig;
            if (!File.Exists(fileNameConfig))
                File.Create(fileNameConfig).Close();
            using (StreamWriter sw = new StreamWriter(fileNameConfig))
            {
                sw.WriteLine(sqlConfig);
                sw.Close();
            }
            #endregion

            ShowMessage("生成成功");
        }
        /// <summary>
        /// 批量生成发布脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnPublishSql_Click(object sender, EventArgs e)
        {
            string configFileName = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择部署配置文件";
            openFileDialog.Filter = "部署配置文件(*.xml)|*.xml";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                configFileName = openFileDialog.FileName;
            }
            openFileDialog.Dispose();
            if (string.IsNullOrEmpty(configFileName)) return;
            XmlWrapper xmlWrapper = new XmlWrapper(configFileName, LoadType.FromFile);
            List<object> configs = xmlWrapper.XmlToList("/Entitys/Entity", typeof(ConfigEntity));
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择脚本保存的目录";
            string selectedPath = string.Empty;
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                selectedPath = folderBrowserDialog.SelectedPath;
            }
            folderBrowserDialog.Dispose();
            if (string.IsNullOrEmpty(selectedPath)) return;

            ThreadPool.QueueUserWorkItem(state =>
            {
                try
                {
                    DoPublishSql(configs, selectedPath);
                }
                catch (ThreadAbortException)
                {
                }
            }, null);
        }

        #region Private
        /// <summary>
        /// CreateEntitySql
        /// </summary>
        /// <param name="entityInfo"></param>
        /// <param name="menuFlag"></param>
        /// <returns></returns>
        private string CreateEntitySql(EntityInfo entityInfo, bool menuFlag)
        {
            #region 清空无效数据
            StringBuilder @string = new StringBuilder();
            @string.AppendLine("--CLEAR");
            @string.AppendLine("delete from dbo.[TS_SYS_ENTITY] where [VALID_FLAG] = 0;");
            @string.AppendLine("delete from dbo.[TS_SYS_ENTITY_FIELD] where [VALID_FLAG] = 0;");
            @string.AppendLine("delete from dbo.[TS_SYS_MENU] where [VALID_FLAG] = 0;");
            @string.AppendLine("delete from dbo.[TS_SYS_MENU_ACTION] where [VALID_FLAG] = 0;");
            @string.AppendLine("delete from dbo.[TS_SYS_ACTION] where [VALID_FLAG] = 0;");
            @string.AppendLine("delete from dbo.[TS_SYS_CODE] where [VALID_FLAG] = 0;");
            @string.AppendLine("delete from dbo.[TS_SYS_CODE_ITEM] where [VALID_FLAG] = 0;");
            @string.AppendLine("delete from dbo.[TS_SYS_SEARCH_MODEL] where [VALID_FLAG] = 0;");
            @string.AppendLine("delete from dbo.[TS_SYS_SEARCH_MODEL_CONDITION] where [VALID_FLAG] = 0;");
            #endregion

            #region TS_SYS_ENTITY
            if (entityInfo == null) return string.Empty;
            @string.AppendLine("--ENTITY:" + entityInfo.EntityName);
            @string.AppendLine("if not exists (select * from dbo.TS_SYS_ENTITY with(nolock) where [FID] = N'" + entityInfo.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)");
            @string.AppendLine(EntityDAL.GetInsertSql(entityInfo));
            #endregion

            List<EntityFieldInfo> entityFieldInfos
                = new EntityFieldBLL().GetListByPage("[ENTITY_FID] = N'" + entityInfo.Fid.GetValueOrDefault() + "'"
                , string.Empty, 1, int.MaxValue, out int dataCnt);
            ///系统代码
            List<string> syscodes = new List<string>();
            foreach (var entityFieldInfo in entityFieldInfos)
            {
                ///数据模型用到的系统代码集合
                string syscode = string.IsNullOrEmpty(entityFieldInfo.Extend1) ? string.Empty : entityFieldInfo.Extend1.Trim();
                if (!string.IsNullOrEmpty(syscode) && !syscodes.Contains(syscode))
                    syscodes.Add(syscode);
                #region TS_SYS_ENTITY_FIELD
                @string.AppendLine("--ENTITY.FIELD:" + entityFieldInfo.FieldName);
                @string.AppendLine("if not exists (select * from dbo.TS_SYS_ENTITY_FIELD with(nolock) " +
                    "where [FID] = N'" + entityFieldInfo.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)");
                if (!string.IsNullOrEmpty(entityFieldInfo.Extend3))
                    entityFieldInfo.Extend3 = entityFieldInfo.Extend3.Replace("'", "''");
                @string.AppendLine(EntityFieldDAL.GetInsertSql(entityFieldInfo));
                #endregion
            }
            ///检索条件
            SearchModelInfo searchModelInfo = new SearchModelBLL().GetInfo(entityInfo.EntityName);
            if (searchModelInfo != null)
            {
                #region TS_SYS_SEARCH_MODEL
                @string.AppendLine("--SEARCH:" + searchModelInfo.SearchName);
                @string.AppendLine("if not exists (select * from dbo.TS_SYS_SEARCH_MODEL with(nolock) " +
                    "where [FID] = N'" + searchModelInfo.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)");
                @string.AppendLine(SearchModelDAL.GetInsertSql(searchModelInfo));
                #endregion
                List<SearchModelConditionInfo> searchModelConditionInfos
                    = new SearchModelConditionBLL().GetListByPage("[SEARCH_FID] = N'" + searchModelInfo.Fid.GetValueOrDefault() + "'"
                    , string.Empty, 1, int.MaxValue, out dataCnt);
                foreach (var searchModelConditionInfo in searchModelConditionInfos)
                {
                    #region TS_SYS_SEARCH_MODEL_CONDITION
                    @string.AppendLine("--CONDITION:" + searchModelConditionInfo.ControlId);
                    @string.AppendLine("if not exists (select * from dbo.TS_SYS_SEARCH_MODEL_CONDITION with(nolock) " +
                        "where [FID] = '" + searchModelConditionInfo.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)");
                    if (!string.IsNullOrEmpty(searchModelConditionInfo.ExtendField3))
                        searchModelConditionInfo.ExtendField3 = searchModelConditionInfo.ExtendField3.Replace("'", "''");
                    @string.AppendLine(SearchModelConditionDAL.GetInsertSql(searchModelConditionInfo));
                    #endregion
                }
            }
            ///系统代码
            List<CodeInfo> codeInfos
                = new CodeBLL().GetListByPage("[CODE_NAME] in ('" + string.Join("','", syscodes.ToArray()) + "')"
                , string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var codeInfo in codeInfos)
            {
                #region TS_SYS_CODE
                @string.AppendLine("--CODE:" + codeInfo.CodeName + "|" + codeInfo.CodeNameCn);
                @string.AppendLine("if not exists (select * from dbo.TS_SYS_CODE with(nolock) " +
                    "where [FID] = N'" + codeInfo.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)");
                @string.AppendLine(CodeDAL.GetInsertSql(codeInfo));
                #endregion
                List<CodeItemInfo> codeItemInfos
                    = new CodeItemBLL().GetListByPage("[CODE_FID] = N'" + codeInfo.Fid.GetValueOrDefault() + "'"
                    , string.Empty, 1, int.MaxValue, out dataCnt);
                foreach (var codeItemInfo in codeItemInfos)
                {
                    #region TS_SYS_CODE_ITEM
                    @string.AppendLine("if not exists (select * from dbo.TS_SYS_CODE_ITEM with(nolock) " +
                        "where [FID] = '" + codeItemInfo.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)");
                    @string.AppendLine(CodeItemDAL.GetInsertSql(codeItemInfo));
                    #endregion
                }
            }
            ///不涉及菜单项的对象到此为止
            if (!menuFlag)
                return @string.ToString();
            ///菜单、按钮
            ///LIST菜单项
            List<MenuInfo> menuInfos
                = new MenuBLL().GetListByPage("[MENU_NAME] = '" + entityInfo.EntityName + "' and [MENU_TYPE] = " + (int)MenuTypeConstants.WebMenu + ""
                , string.Empty, 1, int.MaxValue, out dataCnt);
            if (menuInfos.Count == 0)
                return @string.ToString();
            List<MenuInfo> menus = new List<MenuInfo>();
            foreach (var menuInfo in menuInfos)
            {
                menus.Add(menuInfo);
                ///弹出窗体
                menus.AddRange(new MenuBLL().GetListByPage("[PARENT_MENU_FID] = N'" + menuInfo.Fid.GetValueOrDefault() + "' and [MENU_TYPE] = " + (int)MenuTypeConstants.WebForm + ""
                    , string.Empty, 1, int.MaxValue, out dataCnt));
                ///模块
                MenuInfo pMenuInfo = new MenuBLL().GetInfo(menuInfo.ParentMenuFid.GetValueOrDefault());
                if (pMenuInfo == null) continue;
                if (pMenuInfo.MenuType.GetValueOrDefault() != (int)MenuTypeConstants.WebModule) continue;
                if (menus.Contains(pMenuInfo)) continue;
                menus.Add(pMenuInfo);
            }
            ///菜单
            foreach (var menu in menus)
            {
                #region TS_SYS_MENU
                @string.AppendLine("--MENU:" + menu.MenuName + "|" + menu.MenuNameCn);
                @string.AppendLine("if not exists (select * from dbo.TS_SYS_MENU with(nolock) where [FID] = '" + menu.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)");
                @string.AppendLine(MenuDAL.GetInsertSql(menu));
                #endregion
            }
            ///
            List<MenuActionInfo> menuActionInfos
                = new MenuActionBLL().GetList("[MENU_FID] in ('" + string.Join("','", menus.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            foreach (var menuActionInfo in menuActionInfos)
            {
                #region TS_SYS_MENU_ACTION
                @string.AppendLine("if not exists (select * from dbo.TS_SYS_MENU_ACTION with(nolock) " +
                    "where [FID] = N'" + menuActionInfo.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)");
                if (!string.IsNullOrEmpty(menuActionInfo.ClientJs))
                    menuActionInfo.ClientJs = menuActionInfo.ClientJs.Replace("'", "''");
                @string.AppendLine(MenuActionDAL.GetInsertSql(menuActionInfo));
                #endregion
            }
            ///按钮
            List<ActionInfo> actionInfos = new List<ActionInfo>();
            if (menuActionInfos.Count > 0)
                actionInfos = new ActionBLL().GetListByPage("[FID] in ('" + string.Join("','", menuActionInfos.Select(d => d.ActionFid.GetValueOrDefault()).ToArray()) + "')"
                    , string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var actionInfo in actionInfos)
            {
                #region TS_SYS_ACTION
                @string.AppendLine("--ACTION:" + actionInfo.ActionName + "|" + actionInfo.ActionNameCn);
                @string.AppendLine("if not exists (select * from dbo.TS_SYS_ACTION with(nolock) " +
                    "where [FID] = '" + actionInfo.Fid.GetValueOrDefault() + "' and [VALID_FLAG] = 1)");
                if (!string.IsNullOrEmpty(actionInfo.Comments))
                    actionInfo.Comments = actionInfo.Comments.Replace("'", "''");
                @string.AppendLine(ActionDAL.GetInsertSql(actionInfo));
                #endregion
            }
            return @string.ToString();
        }
        /// <summary>
        /// DeleteEntitySql
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="menuFlag"></param>
        /// <returns></returns>
        private string DeleteEntitySql(EntityInfo entity, bool menuFlag)
        {
            #region 清空无效数据
            string sql = "--CLEAR\r\n"
                + "delete from dbo.[TS_SYS_ENTITY] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_ENTITY_FIELD] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_MENU] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_MENU_ACTION] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_ACTION] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_CODE] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_CODE_ITEM] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_SEARCH_MODEL] where [VALID_FLAG] = 0;\r\n"
                + "delete from dbo.[TS_SYS_SEARCH_MODEL_CONDITION] where [VALID_FLAG] = 0;\r\n";
            #endregion

            #region TS_SYS_ENTITY
            sql += "--ENTITY:" + entity.EntityName + "\r\n";
            sql += "delete from dbo.[TS_SYS_ENTITY] where [VALID_FLAG] = 1 and [FID] = '" + entity.Fid.GetValueOrDefault() + "';\r\n";
            sql += "delete from dbo.[TS_SYS_ENTITY_FIELD] where [VALID_FLAG] = 1 and [ENTITY_FID] = '" + entity.Fid.GetValueOrDefault() + "';\r\n";
            #endregion

            List<EntityFieldInfo> entityfields = new EntityFieldBLL().GetListByPage("[ENTITY_FID] = N'" + entity.Fid.GetValueOrDefault() + "' ", string.Empty, 1, int.MaxValue, out int dataCnt);
            ///系统代码
            List<string> syscodes = new List<string>();
            foreach (var entityfield in entityfields)
            {
                EntityFieldInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(entityfield) as EntityFieldInfo;
                if (!string.IsNullOrEmpty(info.Extend1.Trim()) && !syscodes.Contains(info.Extend1.Trim()))
                    syscodes.Add(info.Extend1.Trim());
            }
            ///检索条件
            SearchModelInfo searchmodel = new SearchModelBLL().GetInfo(entity.EntityName);
            if (searchmodel != null)
            {
                #region TS_SYS_SEARCH_MODEL
                searchmodel = BLL.SYS.CommonBLL.FieldNullToEmpty(searchmodel) as SearchModelInfo;
                sql += "--SEARCH:" + searchmodel.SearchName + "\r\n";
                sql += "delete from dbo.[TS_SYS_SEARCH_MODEL] where [VALID_FLAG] = 1 and [FID] = '" + searchmodel.Fid.GetValueOrDefault() + "';\r\n";
                sql += "delete from dbo.[TS_SYS_SEARCH_MODEL_CONDITION] where [VALID_FLAG] = 1 and [SEARCH_FID] = '" + searchmodel.Fid.GetValueOrDefault() + "';\r\n";
                #endregion
            }
            ///系统代码
            List<CodeInfo> codes = new CodeBLL().GetListByPage("[CODE_NAME] in ('" + string.Join("','", syscodes.ToArray()) + "')", string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var codeinfo in codes)
            {
                ///校验是否其它数据模型用到过这个CODE，有则不能删除
                dataCnt = new EntityFieldBLL().GetCounts("" +
                    "[EXTEND1] = N'" + codeinfo.CodeName + "' and " +
                    "[ENTITY_FID] <> N'" + entity.Fid.GetValueOrDefault() + "'");
                if (dataCnt > 0) continue;
                #region TS_SYS_CODE
                CodeInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(codeinfo) as CodeInfo;
                sql += "--CODE:" + info.CodeName + "|" + info.CodeNameCn + "\r\n";
                sql += "delete from dbo.[TS_SYS_CODE] where [VALID_FLAG] = 1 and [FID] = '" + info.Fid.GetValueOrDefault() + "';\r\n";
                sql += "delete from dbo.[TS_SYS_CODE_ITEM] where [VALID_FLAG] = 1 and [CODE_FID] = '" + info.Fid.GetValueOrDefault() + "';\r\n";
                #endregion
            }
            ///不涉及菜单项的对象到此为止
            if (!menuFlag) return sql;
            ///菜单、按钮
            ///LIST菜单项
            List<MenuInfo> menus = new MenuBLL().GetListByPage("" +
                "[MENU_NAME] = '" + entity.EntityName + "' and " +
                "[MENU_TYPE] = " + (int)MenuTypeConstants.WebMenu + ""
                , string.Empty, 1, int.MaxValue, out dataCnt);
            if (menus.Count == 0)
                throw new Exception("没有" + entity.EntityName + "对应的列表菜单项");
            ///如果多个模块下有相同ENTITY_NAME的菜单，此处需要进行选择
            MenuInfo menuinfo = menus.FirstOrDefault();
            if (menus.Count > 1)
            {
                ///web module
                List<MenuInfo> menuInfos = new MenuBLL().GetListByPage("[MENU_TYPE] = " + (int)MenuTypeConstants.WebModule + "", string.Empty, 1, int.MaxValue, out dataCnt);

                List<GuidValueDatasourceInfo> guidValues = new List<GuidValueDatasourceInfo>();
                foreach (var menu in menus)
                {
                    MenuInfo menuInfo = menuInfos.FirstOrDefault(d => d.Fid.GetValueOrDefault() == menu.ParentMenuFid.GetValueOrDefault());
                    if (menuInfo == null) continue;
                    GuidValueDatasourceInfo guidValue = new GuidValueDatasourceInfo();
                    guidValue.GuidValue = menuInfo.Fid.GetValueOrDefault();
                    guidValue.StringDisplay = menuInfo.MenuNameCn;
                    guidValues.Add(guidValue);
                }
                DialogCombobox dialogCombobox = new DialogCombobox(guidValues);
                if (dialogCombobox.ShowDialog(this) != DialogResult.Yes)
                    throw new Exception("没有选择菜单对应的模块");
                menuinfo = menus.FirstOrDefault(d => d.ParentMenuFid.GetValueOrDefault() == dialogCombobox.SelectedGuidValue);
                if (menuinfo == null)
                    throw new Exception("没有选择菜单对应的模块");
            }

            ///菜单
            #region TS_SYS_MENU
            menuinfo = BLL.SYS.CommonBLL.FieldNullToEmpty(menuinfo) as MenuInfo;
            sql += "--MENU.LIST:" + menuinfo.MenuName + "|" + menuinfo.MenuNameCn + "\r\n";
            sql += "delete from dbo.[TS_SYS_MENU] where [VALID_FLAG] = 1 and [FID] = '" + menuinfo.Fid.GetValueOrDefault() + "';\r\n";
            sql += "delete from dbo.[TS_SYS_MENU_ACTION] where [VALID_FLAG] = 1 and [MENU_FID] = '" + menuinfo.Fid.GetValueOrDefault() + "';\r\n";
            #endregion
            ///按钮
            List<Guid> actionFids = new List<Guid>();
            List<MenuActionInfo> menuactions = new MenuActionBLL().GetList("[MENU_FID] = N'" + menuinfo.Fid.GetValueOrDefault() + "'", string.Empty);
            foreach (var menuaction in menuactions)
            {
                MenuActionInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(menuaction) as MenuActionInfo;
                actionFids.Add(info.ActionFid.GetValueOrDefault());
            }
            ///弹出窗体
            menus = new MenuBLL().GetListByPage("" +
                "[PARENT_MENU_FID] = '" + menuinfo.Fid.GetValueOrDefault() + "' and " +
                "[MENU_TYPE] = " + (int)MenuTypeConstants.WebForm + ""
                , string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var menu in menus)
            {
                #region TS_SYS_MENU
                MenuInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(menu) as MenuInfo;
                sql += "--MENU.FORM:" + info.MenuName + "|" + info.MenuNameCn + "\r\n";
                sql += "delete from dbo.[TS_SYS_MENU] where [VALID_FLAG] = 1 and [FID] = '" + info.Fid.GetValueOrDefault() + "';\r\n";
                sql += "delete from dbo.[TS_SYS_MENU_ACTION] where [VALID_FLAG] = 1 and [MENU_FID] = '" + info.Fid.GetValueOrDefault() + "';\r\n";
                #endregion
                menuactions = new MenuActionBLL().GetList("[MENU_FID] = N'" + menu.Fid.GetValueOrDefault() + "'", string.Empty);
                foreach (var menuaction in menuactions)
                {
                    MenuActionInfo infosub = BLL.SYS.CommonBLL.FieldNullToEmpty(menuaction) as MenuActionInfo;
                    actionFids.Add(infosub.ActionFid.GetValueOrDefault());
                }
            }
            ///按钮
            List<ActionInfo> actions = new ActionBLL().GetListByPage("[FID] in ('" + string.Join("','", actionFids.ToArray()) + "')"
                , string.Empty, 1, int.MaxValue, out dataCnt);
            foreach (var actioninfo in actions)
            {
                string menuIds = (menus.Count == 0 ? string.Empty : ",'" + string.Join("','", menus.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "'");
                ///如果有其它界面使用这个ACTION，则不进行删除
                List<MenuActionInfo> menuactionlist
                    = new MenuActionBLL().GetList("" +
                    "[ACTION_FID] = '" + actioninfo.Fid + "' and " +
                    "[MENU_FID] not in ('" + menuinfo.Fid.GetValueOrDefault() + "'" + menuIds + ")", string.Empty);
                if (menuactionlist.Count > 0) continue;
                #region TS_SYS_ACTION
                ActionInfo info = BLL.SYS.CommonBLL.FieldNullToEmpty(actioninfo) as ActionInfo;
                sql += "--ACTION:" + info.ActionName + "|" + info.ActionNameCn + "\r\n";
                sql += "delete from dbo.[TS_SYS_ACTION] where [VALID_FLAG] = 1 and [FID] = '" + info.Fid.GetValueOrDefault() + "';\r\n";
                #endregion
            }
            return sql;
        }
        /// <summary>
        /// DbCreateTableSql
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        private string DbCreateTableSql(string tableName, string schema)
        {
            object objTableDescription = BLL.SYS.CommonBLL.ExecuteScalar("select VALUE from fn_listextendedproperty (NULL, 'schema', '" + schema + "', 'table', '" + tableName + "', NULL, NULL)");
            string tableDescription = string.Empty;
            if (objTableDescription == null || objTableDescription == DBNull.Value)
            {
                DialogInput dialog = new DialogInput("请输入【" + schema + "." + tableName + "】的表描述信息");
                if (dialog.ShowDialog(this) != DialogResult.Yes)
                {
                    dialog.Dispose();
                    throw new Exception("未输入【" + schema + "." + tableName + "】的表描述");
                }
                tableDescription = dialog.InputMsg;
                dialog.Dispose();
                ///添加注释
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + tableDescription + "' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "'");
            }
            else
                tableDescription = objTableDescription.ToString();
            if (string.IsNullOrEmpty(tableDescription))
                throw new Exception("未输入【" + schema + "." + tableName + "】的表描述");

            List<TableField> fields = GetFieldsDataSource(tableName);
            if (fields.Count == 0)
                throw new Exception("【" + schema + "." + tableName + "】下没有任何有效字段");
            List<string> unFields = new List<string>();
            unFields.Add("ID");
            unFields.Add("FID");
            unFields.Add("VALID_FLAG");
            unFields.Add("CREATE_DATE");
            unFields.Add("CREATE_USER");
            unFields.Add("MODIFY_DATE");
            unFields.Add("MODIFY_USER");
            List<TableField> tableFields = fields.Where(d => !unFields.Contains(d.fieldName)).OrderBy(d => d.dbId).ToList();

            StringBuilder sb = new StringBuilder();
            ///创建表
            sb.AppendLine("/****** [" + schema + "].[" + tableName + "] " + tableDescription + " Script Date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "******/");
            sb.AppendLine("SET ANSI_NULLS ON");
            sb.AppendLine("");
            sb.AppendLine("SET QUOTED_IDENTIFIER ON");
            sb.AppendLine("IF not exists(select * from sysobjects where NAME = '" + tableName + "' and TYPE = 'U')");
            sb.AppendLine("BEGIN");
            sb.AppendLine("CREATE TABLE [" + schema + "].[" + tableName + "](");
            sb.AppendLine("	[ID] [bigint] IDENTITY(1,1) NOT NULL,");
            sb.AppendLine("	[FID] [uniqueidentifier] NULL,");
            sb.AppendLine("	[VALID_FLAG] [bit] NOT NULL,");
            sb.AppendLine("	[CREATE_USER] [nvarchar](32) NOT NULL,");
            sb.AppendLine("	[CREATE_DATE] [datetime] NOT NULL,");
            sb.AppendLine("	[MODIFY_USER] [nvarchar](32) NULL,");
            sb.AppendLine("	[MODIFY_DATE] [datetime] NULL,");
            sb.AppendLine(" CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED ");
            sb.AppendLine("(");
            sb.AppendLine("	[ID] ASC");
            sb.AppendLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
            sb.AppendLine(") ON [PRIMARY]");
            sb.AppendLine("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据有效标记' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'VALID_FLAG'");
            sb.AppendLine("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'CREATE_USER'");
            sb.AppendLine("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'CREATE_DATE'");
            sb.AppendLine("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新用户' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'MODIFY_USER'");
            sb.AppendLine("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'MODIFY_DATE'");
            sb.AppendLine("EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + tableDescription + "' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "'");
            sb.AppendLine("END");
            sb.AppendLine("");
            ///创建字段
            string missDescriptionFields = string.Empty;
            for (int i = 0; i < tableFields.Count; i++)
            {
                string fieldName = tableFields[i].fieldName;
                if (fieldName.StartsWith("20") || fieldName.StartsWith("19")) continue;
                string fieldDescription = tableFields[i].description.Replace("\r\n", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
                ///没有注释，需要补充
                if (string.IsNullOrEmpty(fieldDescription))
                {
                    DialogInput dialog = new DialogInput("请输入" + fieldName + "的字段注释");
                    if (dialog.ShowDialog(this) != DialogResult.Yes)
                    {
                        dialog.Dispose();
                        throw new Exception("未输入【" + schema + "." + tableName + "." + fieldName + "】的字段描述");
                    }
                    fieldDescription = dialog.InputMsg;
                    ///添加注释
                    string sql = "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + fieldDescription + "' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'" + fieldName + "'";
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sql);
                    dialog.Dispose();
                }
                string fieldDataType = tableFields[i].dataType;
                int fieldDataLength = tableFields[i].precision;
                switch (fieldDataType)
                {
                    case "int":
                    case "bit":
                    case "bigint":
                    case "uniqueidentifier":
                    case "image":
                    case "datetime": break;
                    case "decimal": fieldDataType += "(18,4)"; break;
                    case "nvarchar":
                        if (fieldDataLength > 0)
                            fieldDataType += "(" + fieldDataLength + ")";
                        else
                            fieldDataType += "(max)";
                        break;
                    default:
                        throw new Exception("【" + schema + "." + tableName + "." + fieldName + "】中出现了未知的数据类型【" + fieldDataType + "】");
                }
                fieldDescription = fieldDescription.Replace("\r\n", string.Empty);
                sb.AppendLine("---" + fieldName + " " + fieldDescription + "");
                sb.AppendLine("IF not exists(select * from syscolumns where ID = object_id('" + schema + "." + tableName + "') and NAME = '" + fieldName + "')");
                sb.AppendLine("BEGIN");
                sb.AppendLine("	ALTER TABLE " + schema + "." + tableName + " ADD " + fieldName + " " + fieldDataType + " NULL");
                if (!string.IsNullOrEmpty(fieldDescription))
                    sb.AppendLine(" EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + fieldDescription + "' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'" + fieldName + "'");
                sb.AppendLine("END");
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        private string DbCreateViewSql(string viewName, string schema)
        {
            DataTable dtViewSql = BLL.SYS.CommonBLL.ExecuteDataTableBySql("EXEC sys.sp_helptext '[" + schema + "].[" + viewName + "]'");
            if (dtViewSql.Rows.Count == 0) return string.Empty;
            StringBuilder @string = new StringBuilder();
            foreach (DataRow dr in dtViewSql.Rows)
            {
                string drViewSql = dr["Text"].ToString().Trim();
                if (drViewSql.StartsWith("CREATE VIEW "))
                    drViewSql = drViewSql.Replace("CREATE VIEW ", "ALTER VIEW ");
                if (!string.IsNullOrEmpty(drViewSql))
                    @string.AppendLine(drViewSql);
            }
            return @string.ToString();
        }
        /// <summary>
        /// DbDropTableSql
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        private string DbDropTableSql(string tableName, string schema)
        {
            object objTableDescription = BLL.SYS.CommonBLL.ExecuteScalar("select VALUE from fn_listextendedproperty (NULL, 'schema', '" + schema + "', 'table', '" + tableName + "', NULL, NULL)");
            string tableDescription = string.Empty;
            if (objTableDescription == null || objTableDescription == DBNull.Value)
                tableDescription = string.Empty;
            else
                tableDescription = objTableDescription.ToString();
            if (string.IsNullOrEmpty(tableDescription))
                throw new Exception("【" + schema + "." + tableName + "】没有表描述");

            List<TableField> fields = GetFieldsDataSource(tableName);
            if (fields.Count == 0)
                throw new Exception("【" + schema + "." + tableName + "】下没有任何有效字段");
            List<string> unFields = new List<string>();
            unFields.Add("ID");
            unFields.Add("FID");
            unFields.Add("VALID_FLAG");
            unFields.Add("CREATE_DATE");
            unFields.Add("CREATE_USER");
            unFields.Add("MODIFY_DATE");
            unFields.Add("MODIFY_USER");
            List<TableField> tableFields = fields.Where(d => !unFields.Contains(d.fieldName)).OrderBy(d => d.dbId).ToList();

            StringBuilder sb = new StringBuilder();
            ///删除表
            sb.AppendLine("/****** [" + schema + "].[" + tableName + "] " + tableDescription + " Script Date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "******/");
            ///删除字段
            string missDescriptionFields = string.Empty;
            for (int i = 0; i < tableFields.Count; i++)
            {
                string fieldName = tableFields[i].fieldName;
                if (fieldName.StartsWith("20") || fieldName.StartsWith("19")) continue;
                string fieldDescription = tableFields[i].description;
                fieldDescription = fieldDescription.Replace("\r\n", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
                string fieldDataType = tableFields[i].dataType;
                switch (fieldDataType)
                {
                    case "int":
                    case "bit":
                    case "bigint":
                    case "uniqueidentifier":
                    case "datetime":
                    case "decimal":
                    case "image":
                    case "nvarchar": break;
                    default:
                        throw new Exception("【" + schema + "." + tableName + "." + fieldName + "】中出现了未知的数据类型【" + fieldDataType + "】");
                }
                sb.AppendLine("---" + fieldName + " " + fieldDescription + "");
                sb.AppendLine("IF exists(select * from syscolumns where ID = object_id('" + schema + "." + tableName + "') and NAME = '" + fieldName + "')");
                sb.AppendLine("BEGIN");
                sb.AppendLine("  EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'" + fieldName + "'");
                sb.AppendLine("  ALTER TABLE " + schema + "." + tableName + " DROP COLUMN " + fieldName + "");
                sb.AppendLine("END");
            }
            sb.AppendLine("IF exists(select * from sysobjects where NAME = '" + tableName + "' and TYPE = 'U')");
            sb.AppendLine("BEGIN");
            sb.AppendLine("  IF exists(select * from syscolumns where ID = object_id('" + schema + "." + tableName + "') and NAME = 'VALID_FLAG')");
            sb.AppendLine("     EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'VALID_FLAG'");
            sb.AppendLine("  IF exists(select * from syscolumns where ID = object_id('" + schema + "." + tableName + "') and NAME = 'CREATE_USER')");
            sb.AppendLine("     EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'CREATE_USER'");
            sb.AppendLine("  IF exists(select * from syscolumns where ID = object_id('" + schema + "." + tableName + "') and NAME = 'CREATE_DATE')");
            sb.AppendLine("     EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'CREATE_DATE'");
            sb.AppendLine("  IF exists(select * from syscolumns where ID = object_id('" + schema + "." + tableName + "') and NAME = 'MODIFY_USER')");
            sb.AppendLine("     EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'MODIFY_USER'");
            sb.AppendLine("  IF exists(select * from syscolumns where ID = object_id('" + schema + "." + tableName + "') and NAME = 'MODIFY_DATE')");
            sb.AppendLine("     EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'MODIFY_DATE'");
            sb.AppendLine("  IF exists(select * from sysobjects where NAME = '" + tableName + "' and TYPE = 'U')");
            sb.AppendLine("     EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'" + schema + "', @level1type=N'TABLE',@level1name=N'" + tableName + "'");
            sb.AppendLine("  DROP TABLE [" + schema + "].[" + tableName + "]");
            sb.AppendLine("END");
            sb.AppendLine("");
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        private string DbDropViewSql(string viewName, string schema)
        {
            StringBuilder @string = new StringBuilder();
            @string.AppendLine("/****** [" + schema + "].[" + viewName + "] Script Date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "******/");
            @string.AppendLine("DROP VIEW [" + schema + "].[" + viewName + "]");
            return @string.ToString();
        }
        #endregion
        /// <summary>
        /// 生成BLL对象接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBtnBllCreateInfo_Click(object sender, EventArgs e)
        {
            DialogInput dialogInput = new DialogInput("请输入数据库表名");
            if (dialogInput.ShowDialog(this) != DialogResult.Yes)
            {
                dialogInput.Dispose();
                return;
            }
            string tableName = dialogInput.InputMsg;
            if (string.IsNullOrEmpty(tableName)) return;
            string entityName = GetEntityNameByTableName(tableName);
            StringBuilder @string = new StringBuilder();
            @string.AppendLine("        #region Interface");
            List<TableField> tableFields = GetFieldsDataSource(tableName);
            if (tableFields.Count == 0) return;
            @string.AppendLine("        /// <summary>");
            @string.AppendLine("        /// Create " + entityName + "Info");
            @string.AppendLine("        /// </summary>");
            @string.AppendLine("        /// <param name=\"loginUser\"></param>");
            @string.AppendLine("        /// <returns>" + entityName + "Info</returns>");
            @string.AppendLine("        public static " + entityName + "Info Create" + entityName + "Info(string loginUser)");
            @string.AppendLine("        {");
            @string.AppendLine("            " + entityName + "Info info = new " + entityName + "Info();");
            foreach (var tableField in tableFields)
            {
                @string.AppendLine("            ///" + tableField.fieldName + "," + tableField.description.Replace("\r\n", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty));
                switch (tableField.fieldName)
                {
                    case "ID": @string.AppendLine("            info.Id = 0;"); break;
                    case "FID": @string.AppendLine("            info.Fid = Guid.NewGuid();"); break;
                    case "VALID_FLAG": @string.AppendLine("            info.ValidFlag = true;"); break;
                    case "CREATE_DATE": @string.AppendLine("            info.CreateDate = DateTime.Now;"); break;
                    case "CREATE_USER": @string.AppendLine("            info.CreateUser = loginUser;"); break;
                    default: @string.AppendLine("            info." + GetPropNameByFieldName(tableField.fieldName) + " = null;"); break;
                }
            }
            @string.AppendLine("            return info;");
            @string.AppendLine("        }");
            @string.AppendLine("        #endregion");
            DialogSqlExport dialogSqlExport = new DialogSqlExport(@string.ToString());
            dialogSqlExport.ShowDialog(this);
        }
    }
    /// <summary>
    /// DV Entity
    /// </summary>
    public class TableField
    {
        public int dbId { get; set; }
        public string fieldName { get; set; }
        public string dataType { get; set; }
        public int maxLength { get; set; }
        public int precision { get; set; }
        public string description { get; set; }
        public string description_en { get; set; }
        public bool isEntity { get; set; }
        public bool isList { get; set; }
        public bool isForm { get; set; }
        public bool isSearch { get; set; }
        public bool isExcel { get; set; }

        public bool isNull { get; set; }
    }
    /// <summary>
    /// Config Entity
    /// </summary>
    [XmlRoot("Entity")]
    public class ConfigEntity
    {
        [XmlAttribute]
        public string Name;
        [XmlAttribute]
        public string TableName;
        [XmlAttribute]
        public bool MenuFlag;
        [XmlAttribute]
        public string PathName;
        [XmlAttribute]
        public string Schema;
        [XmlAttribute]
        public int DbCreateType;
    }
}
