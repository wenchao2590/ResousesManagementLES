using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.TOOLS.DATAJOIN
{
    public partial class DialogFields : Form
    {
        public DialogFields()
        {
            InitializeComponent();
        }
        private string tableName;
        private bool keyFlag;
        public List<SelectedField> selectedFields = new List<SelectedField>();
        public List<SelectedField> loadedFields = new List<SelectedField>();
        public DialogFields(string tableName, bool keyFlag)
        {
            InitializeComponent();
            this.tableName = tableName;
            this.keyFlag = keyFlag;
            dgFields.AutoGenerateColumns = false;
        }

        private void DialogFields_Load(object sender, EventArgs e)
        {
            lblTableName.Text = tableName;
            dgFields.MultiSelect = !keyFlag;
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
                ///+ "AND c.name not in ('ID','NID','FID','PARENT_FID','VALID_FLAG','CREATE_USER','CREATE_DATE','MODIFY_USER','MODIFY_DATE') "
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
---AND a.name not in ('ID','NID','FID','PARENT_FID','VALID_FLAG','CREATE_USER','CREATE_DATE','MODIFY_USER','MODIFY_DATE') 
order by DB_ID";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@TABLE_NAME", DbType.AnsiString, tableName);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dtFields = ds.Tables[0];
            foreach (DataRow dr in dtFields.Rows)
            {
                SelectedField info = new SelectedField();
                info.fieldName = dr["FIELD_NAME"].ToString();
                info.dataType = dr["DATA_TYPE"].ToString();
                info.maxLength = int.Parse(dr["LENGTH"].ToString());
                info.precision = int.Parse(dr["PRECISION"].ToString());
                info.descCn = dr["TITLE_CN"].ToString();
                info.descEn = dr["TITLE_EN"].ToString();
                info.keyFieldFlag = keyFlag;
                info.tableName = tableName;
                loadedFields.Add(info);
            }
            dgFields.DataSource = null;
            dgFields.DataSource = loadedFields;
            dgFields.Refresh();
            dgFields.ClearSelection();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgFields.SelectedRows)
            {
                SelectedField info = row.DataBoundItem as SelectedField;
                selectedFields.Add(info);
            }
            if (selectedFields.Count > 0)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13) return;
            dgFields.DataSource = null;
            if (!string.IsNullOrEmpty(txtSearch.Text))
                dgFields.DataSource = loadedFields.Where(d => d.fieldName.Contains(txtSearch.Text)).ToList();
            else
                dgFields.DataSource = loadedFields;
            dgFields.Refresh();
            dgFields.ClearSelection();
        }
    }
}
