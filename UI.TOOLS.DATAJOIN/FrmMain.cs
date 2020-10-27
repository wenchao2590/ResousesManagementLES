using BLL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;

namespace UI.TOOLS.DATAJOIN
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            dgTables.AutoGenerateColumns = false;
            dgSelectedField.AutoGenerateColumns = false;
        }
        private List<SelectedTable> selectedTables = new List<SelectedTable>();
        private List<SelectedField> selectedFields = new List<SelectedField>();

        private void ShowMessage(string message)
        {
            MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + message
                , "提示"
                , MessageBoxButtons.OK
                , MessageBoxIcon.Information);
        }

        private void ErrorMessage(string message)
        {
            MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + message
                 , "错误"
                 , MessageBoxButtons.OK
                 , MessageBoxIcon.Error);
        }
        private void btnMainTable_Click(object sender, EventArgs e)
        {
            int cnt = selectedTables.Count(d => d.mainTableFlag);
            if (cnt > 0)
            {
                ErrorMessage("主表只能有一张");
                return;
            }
            DialogTables dialog = new DialogTables();
            if (dialog.ShowDialog(this) == DialogResult.Yes)
            {
                SelectedTable t = selectedTables.FirstOrDefault(d => d.tableName == dialog.TableName);
                if (t == null)
                {
                    t = new SelectedTable();
                    t.mainTableFlag = true;
                    t.tableName = dialog.TableName;
                    selectedTables.Add(t);
                }
            }
            dialog.Dispose();
            dgTables.DataSource = null;
            dgTables.DataSource = selectedTables;
            dgTables.Refresh();
            dgTables.ClearSelection();
        }

        private void btnJoinTables_Click(object sender, EventArgs e)
        {
            DialogTables dialog = new DialogTables();
            if (dialog.ShowDialog(this) == DialogResult.Yes)
            {
                SelectedTable t = selectedTables.FirstOrDefault(d => d.tableName == dialog.TableName);
                if (t == null)
                {
                    t = new SelectedTable();
                    t.mainTableFlag = false;
                    t.tableName = dialog.TableName;
                    selectedTables.Add(t);
                }
            }
            dialog.Dispose();
            dgTables.DataSource = null;
            dgTables.DataSource = selectedTables;
            dgTables.Refresh();
            dgTables.ClearSelection();
        }

        private void dgTables_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgTables.Rows)
            {
                SelectedTable t = row.DataBoundItem as SelectedTable;
                if (t.mainTableFlag)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.BackColor = Color.ForestGreen;
                        cell.Style.ForeColor = Color.White;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.BackColor = Color.White;
                        cell.Style.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void btnDelTable_Click(object sender, EventArgs e)
        {
            if (dgTables.SelectedRows.Count == 0) return;
            SelectedTable t = dgTables.SelectedRows[0].DataBoundItem as SelectedTable;
            if (MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n您确认要删除" + t.tableName + "的所有关联内容吗?"
                 , "提示"
                 , MessageBoxButtons.YesNoCancel
                 , MessageBoxIcon.Question
                 , MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;
            List<SelectedField> selectedFields1 = new List<SelectedField>();
            foreach (var info in selectedFields)
            {
                if (info.tableName == t.tableName) continue;
                selectedFields1.Add(info);
            }
            selectedFields = selectedFields1;
            List<SelectedTable> selectedTables1 = new List<SelectedTable>();
            foreach (var info in selectedTables)
            {
                if (info.tableName == t.tableName) continue;
                selectedTables1.Add(info);
            }
            selectedTables = selectedTables1;

            dgSelectedField.DataSource = null;
            dgSelectedField.DataSource = selectedFields;
            dgSelectedField.Refresh();
            dgSelectedField.ClearSelection();

            dgTables.DataSource = null;
            dgTables.DataSource = selectedTables;
            dgTables.Refresh();
            dgTables.ClearSelection();
        }

        private void btnKeyField_Click(object sender, EventArgs e)
        {
            int cnt = selectedFields.Count(d => d.keyFieldFlag);
            if (cnt > 0)
            {
                ErrorMessage("主键只能有一个");
                return;
            }
            if (dgTables.SelectedRows.Count == 0) return;
            SelectedTable t = dgTables.SelectedRows[0].DataBoundItem as SelectedTable;
            DialogFields dialog = new DialogFields(t.tableName, true);
            List<SelectedField> fields = new List<SelectedField>();
            if (dialog.ShowDialog(this) == DialogResult.Yes)
            {
                foreach (var info in dialog.selectedFields)
                {
                    cnt = selectedFields.Count(d => d.tableName == t.tableName && d.fieldName == info.fieldName);
                    if (cnt > 0) continue;
                    info.entityFieldName = info.fieldName;
                    cnt = selectedFields.Count(d => d.fieldName == info.fieldName);
                    while (cnt > 0)
                    {
                        DialogTextbox tdialog = new DialogTextbox("请输入字段别名");
                        if (tdialog.ShowDialog(this) == DialogResult.Yes)
                        {
                            info.entityFieldName = tdialog.content;
                        }
                        dialog.Dispose();
                        cnt = selectedFields.Count(d => d.entityFieldName == info.entityFieldName);
                        if (cnt > 0)
                            ErrorMessage("已有重复字段别名，请重新输入");
                        else
                        {
                            cnt = fields.Count(d => d.entityFieldName == info.entityFieldName);
                            if (cnt > 0)
                                ErrorMessage("已有重复字段别名，请重新输入");
                        }
                    }
                    fields.Add(info);
                }
            }
            foreach (var field in fields)
            {
                selectedFields.Add(field);
            }
            dialog.Dispose();
            dgSelectedField.DataSource = null;
            dgSelectedField.DataSource = selectedFields;
            dgSelectedField.Refresh();
            dgSelectedField.ClearSelection();
        }

        private void btnFields_Click(object sender, EventArgs e)
        {
            if (dgTables.SelectedRows.Count == 0) return;
            SelectedTable t = dgTables.SelectedRows[0].DataBoundItem as SelectedTable;
            DialogFields dialog = new DialogFields(t.tableName, false);
            List<SelectedField> fields = new List<SelectedField>();
            if (dialog.ShowDialog(this) == DialogResult.Yes)
            {
                foreach (var info in dialog.selectedFields)
                {
                    int cnt = selectedFields.Count(d => d.tableName == t.tableName && d.fieldName == info.fieldName);
                    if (cnt > 0) continue;
                    info.entityFieldName = info.fieldName;
                    cnt = selectedFields.Count(d => d.fieldName == info.fieldName);
                    while (cnt > 0)
                    {
                        DialogTextbox tdialog = new DialogTextbox("请输入字段别名");
                        if (tdialog.ShowDialog(this) == DialogResult.Yes)
                        {
                            info.entityFieldName = tdialog.content;
                        }
                        dialog.Dispose();
                        cnt = selectedFields.Count(d => d.entityFieldName == info.entityFieldName);
                        if (cnt > 0)
                            ErrorMessage("已有重复字段别名，请重新输入");
                        else
                        {
                            cnt = fields.Count(d => d.entityFieldName == info.entityFieldName);
                            if (cnt > 0)
                                ErrorMessage("已有重复字段别名，请重新输入");
                        }
                    }
                    fields.Add(info);
                }
            }
            foreach (var field in fields)
            {
                selectedFields.Add(field);
            }
            dialog.Dispose();
            dgSelectedField.DataSource = null;
            dgSelectedField.DataSource = selectedFields;
            dgSelectedField.Refresh();
            dgSelectedField.ClearSelection();
        }

        private void btnDelField_Click(object sender, EventArgs e)
        {
            if (dgTables.SelectedRows.Count == 0) return;
            if (MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n您确认要删除所选的内容吗?"
                    , "提示"
                    , MessageBoxButtons.YesNoCancel
                    , MessageBoxIcon.Question
                    , MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;
            foreach (DataGridViewRow row in dgSelectedField.SelectedRows)
            {
                SelectedField t = row.DataBoundItem as SelectedField;
                for (int i = 0; i < selectedFields.Count; i++)
                {
                    if (selectedFields[i].tableName != t.tableName) continue;
                    if (selectedFields[i].fieldName != t.fieldName) continue;
                    selectedFields.Remove(selectedFields[i]);
                    break;
                }
            }
            dgSelectedField.DataSource = null;
            dgSelectedField.DataSource = selectedFields;
            dgSelectedField.Refresh();
            dgSelectedField.ClearSelection();
        }

        private void dgSelectedField_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgSelectedField.Rows)
            {
                SelectedField t = row.DataBoundItem as SelectedField;
                if (t.keyFieldFlag)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.BackColor = Color.ForestGreen;
                        cell.Style.ForeColor = Color.White;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.BackColor = Color.White;
                        cell.Style.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void btnCreateDm_Click(object sender, EventArgs e)
        {
            string spaceName = cbNameSpace.Text.ToUpper();
            string entityName = txtEntityName.Text.Trim();
            if (string.IsNullOrEmpty(entityName))
            {
                DialogTextbox dialog = new DialogTextbox("请输入数据模型名称");
                if (dialog.ShowDialog(this) == DialogResult.Yes)
                {
                    entityName = dialog.content;
                    txtEntityName.Text = entityName;
                }
                dialog.Dispose();
            }
            if (string.IsNullOrEmpty(entityName)) return;
            if (MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n您确认要创建" + entityName + "的DM类文件吗?"
                , "提示"
                , MessageBoxButtons.YesNoCancel
                , MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;
            StringBuilder sb = new StringBuilder();
            ///using
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("");
            ///namespace
            sb.AppendLine("namespace DM." + spaceName + "");
            sb.AppendLine("{");
            sb.AppendLine("    public class " + entityName + "Info");
            sb.AppendLine("    {");
            foreach (var field in selectedFields)
            {
                string dataType = "string";
                switch (field.dataType)
                {
                    case "int": dataType = "int"; break;
                    case "numeric": dataType = "decimal"; break;
                }
                if (field.keyFieldFlag)
                    sb.AppendLine("        public " + dataType + " " + GetEntityNameByTableName(field.fieldName) + "{ get;set; }");
                else
                    sb.AppendLine("        public " + dataType + " " + GetEntityNameByTableName(field.tableName) + "_" + field.fieldName + "{ get;set; }");
            }
            ///
            sb.AppendLine("    }");
            sb.AppendLine("}");
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.FileName = entityName + "Info.cs";
            if (savedialog.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(savedialog.FileName))
                    File.Create(savedialog.FileName).Close();
                using (StreamWriter sw = new StreamWriter(savedialog.FileName))
                {
                    sw.WriteLine(sb.ToString());
                    sw.Close();
                }
            }
        }

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
        private string GetTableNameByEntityName(string entityName)
        {
            return entityName.Substring(0, 1).ToLower() + entityName.Substring(1);
        }

        private void btnCreateDal_Click(object sender, EventArgs e)
        {
            string spaceName = cbNameSpace.Text.ToUpper();
            string entityName = txtEntityName.Text.Trim();
            if (string.IsNullOrEmpty(entityName))
            {
                DialogTextbox dialog = new DialogTextbox("请输入数据模型名称");
                if (dialog.ShowDialog(this) == DialogResult.Yes)
                {
                    entityName = dialog.content;
                    txtEntityName.Text = entityName;
                }
                dialog.Dispose();
            }
            if (string.IsNullOrEmpty(entityName)) return;
            string joinStr = string.Empty;
            DialogMultextbox mdialog = new DialogMultextbox("请输入表关系语句");
            if (mdialog.ShowDialog(this) == DialogResult.Yes)
            {
                joinStr = mdialog.content;
            }
            mdialog.Dispose();
            if (MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n您确认要创建" + entityName + "的DAL类文件吗?"
                , "提示"
                , MessageBoxButtons.YesNoCancel
                , MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;
            StringBuilder sb = new StringBuilder();
            ///using
            sb.AppendLine("using DM." + spaceName + ";");
            sb.AppendLine("using Infrustructure.Utilities;");
            sb.AppendLine("using Microsoft.Practices.EnterpriseLibrary.Data;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using System.Data.Common;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("");
            ///namespace
            sb.AppendLine("namespace DAL." + spaceName + "");
            sb.AppendLine("{");
            sb.AppendLine("    public class " + entityName + "DAL");
            sb.AppendLine("    {");
            ///GetListByPage
            sb.AppendLine("        public List<" + entityName + "Info> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)");
            sb.AppendLine("        {");
            sb.AppendLine("            if (pageIndex <= 0) pageIndex = 1;");
            sb.AppendLine("            if (pageRow <= 0) pageRow = 10;");
            sb.AppendLine("            string whereText = string.Empty;");
            sb.AppendLine("             if (!string.IsNullOrEmpty(textWhere))");
            sb.AppendLine("            {");
            sb.AppendLine("                if (textWhere.Trim().StartsWith(\"and\", StringComparison.OrdinalIgnoreCase))");
            sb.AppendLine("                    whereText += \" where 1 = 1 \" + textWhere;");
            sb.AppendLine("                else");
            sb.AppendLine("                    whereText += \" where \" + textWhere;");
            sb.AppendLine("            }");
            sb.AppendLine("            else");
            sb.AppendLine("                whereText += \" where 1 = 1 \";");
            sb.AppendLine("            if (string.IsNullOrEmpty(textOrder))");
            SelectedField keyField = selectedFields.FirstOrDefault(d => d.keyFieldFlag);
            if (keyField == null) return;
            sb.AppendLine("                textOrder += \"dbo.[" + keyField.tableName + "].[" + keyField.entityFieldName + "] desc\";");
            sb.AppendLine("            string sql = \"select top \" + pageRow + \" * from \"");
            sb.AppendLine("                + \"(select row_number() over(order by \" + textOrder + \") as rownumber\"");
            SelectedTable mainTable = selectedTables.FirstOrDefault(d => d.mainTableFlag);
            if (mainTable == null) return;
            sb.AppendLine("                + \", \" + FieldSql + \" from dbo.[" + mainTable.tableName + "] with(nolock)\" + LeftJoinSql + \" \" + whereText + \") T \"");
            sb.AppendLine("                + \"where rownumber > \" + (pageIndex - 1) * pageRow + \" \";");
            sb.AppendLine("            Database db = DatabaseFactory.CreateDatabase();");
            sb.AppendLine("            DbCommand cmd = db.GetSqlStringCommand(sql);");
            sb.AppendLine("            List<" + entityName + "Info> list = new List<" + entityName + "Info>();");
            sb.AppendLine("            using (IDataReader dr = db.ExecuteReader(cmd))");
            sb.AppendLine("            {");
            sb.AppendLine("                while (dr.Read())");
            sb.AppendLine("                {");
            sb.AppendLine("                    " + entityName + "Info info = new " + entityName + "Info();");
            foreach (var table in selectedTables)
            {
                sb.AppendLine("                    Update" + GetEntityNameByTableName(table.tableName) + "Fields(info, dr);");
            }
            sb.AppendLine("                    list.Add(info);");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("            return list;");
            sb.AppendLine("        }");
            ///GetCounts
            sb.AppendLine("        public int GetCounts(string textWhere)");
            sb.AppendLine("        {");
            sb.AppendLine("            if (string.IsNullOrEmpty(textWhere))");
            sb.AppendLine("                textWhere = string.Empty;");
            sb.AppendLine("            else");
            sb.AppendLine("            {");
            sb.AppendLine("                if (!textWhere.Trim().StartsWith(\"and\", StringComparison.OrdinalIgnoreCase))");
            sb.AppendLine("                    textWhere = \" and \" + textWhere;");
            sb.AppendLine("            }");
            sb.AppendLine("            string sql = \"select count(1) from dbo.[" + mainTable.tableName + "] with(nolock)\" + LeftJoinSql + \" where 1 = 1 {0}; \";");
            sb.AppendLine("            Database db = DatabaseFactory.CreateDatabase();");
            sb.AppendLine("            DbCommand cmd = db.GetSqlStringCommand(string.Format(sql, textWhere));");
            sb.AppendLine("            return Convert.ToInt32(db.ExecuteScalar(cmd));");
            sb.AppendLine("        }");
            ///GetInfo
            string keyFieldType = "string";
            string dbType = "String";
            switch (keyField.dataType)
            {
                case "int": keyFieldType = "int"; dbType = "Int32"; break;
                case "numeric": keyFieldType = "decimal"; dbType = "Decimal"; break;
            }
            sb.AppendLine("        public " + entityName + "Info GetInfo(" + keyFieldType + " " + keyField.fieldName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            string sql = \"select \" + FieldSql + \" from dbo.[" + mainTable.tableName + "] with(nolock)\" + LeftJoinSql + \" where dbo.[" + keyField.tableName + "].[" + keyField.entityFieldName + "] = @" + keyField.fieldName + "; \";");
            sb.AppendLine("            Database db = DatabaseFactory.CreateDatabase();");
            sb.AppendLine("            DbCommand cmd = db.GetSqlStringCommand(sql);");
            sb.AppendLine("            db.AddInParameter(cmd, \"@" + keyField.entityFieldName + "\", DbType." + dbType + ", " + keyField.fieldName + ");");
            sb.AppendLine("            using (IDataReader dr = db.ExecuteReader(cmd))");
            sb.AppendLine("            {");
            sb.AppendLine("                if (dr.Read())");
            sb.AppendLine("                {");
            sb.AppendLine("                    " + entityName + "Info info = new " + entityName + "Info();");
            foreach (var table in selectedTables)
            {
                sb.AppendLine("                    Update" + GetEntityNameByTableName(table.tableName) + "Fields(info, dr);");
            }
            sb.AppendLine("                        return info;");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("            return null;");
            sb.AppendLine("        }");
            ///LeftJoinSql
            sb.AppendLine("        private string LeftJoinSql = \"" + joinStr + "\";");
            ///FieldSql
            sb.AppendLine("        private string FieldSql = string.Empty");
            bool firstFlag = true;
            foreach (var field in selectedFields)
            {
                sb.AppendLine("        + \"" + (firstFlag ? string.Empty : ",")
                    + "dbo.[" + field.tableName + "].[" + field.fieldName + "]"
                    + (field.fieldName == field.entityFieldName ? string.Empty : (" as " + field.entityFieldName)) + "\"");
                firstFlag = false;
            }
            sb.AppendLine("        ;");
            ///UpdateFields
            foreach (var table in selectedTables)
            {
                sb.AppendLine("        private void Update" + GetEntityNameByTableName(table.tableName) + "Fields(" + entityName + "Info info, IDataReader dr)");
                sb.AppendLine("        {");
                foreach (var field in selectedFields)
                {
                    string getDataType = "GetString";
                    switch (field.dataType)
                    {
                        case "int": getDataType = "GetInt32"; break;
                        case "numeric": getDataType = "GetDecimal"; break;
                    }
                    if (field.keyFieldFlag)
                        sb.AppendLine("            info." + GetEntityNameByTableName(field.fieldName) + " = DBConvert." + getDataType + "(dr, dr.GetOrdinal(\"" + field.entityFieldName + "\"));");
                    else
                        sb.AppendLine("            info." + GetEntityNameByTableName(field.tableName) + "_" + field.fieldName + " = DBConvert." + getDataType + "(dr, dr.GetOrdinal(\"" + field.entityFieldName + "\"));");
                }
                sb.AppendLine("        }");
            }
            ///
            ///
            sb.AppendLine("    }");
            sb.AppendLine("}");
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.FileName = entityName + "DAL.cs";
            if (savedialog.ShowDialog(this) == DialogResult.OK)
            {
                if (!File.Exists(savedialog.FileName))
                    File.Create(savedialog.FileName).Close();
                using (StreamWriter sw = new StreamWriter(savedialog.FileName))
                {
                    sw.WriteLine(sb.ToString());
                    sw.Close();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            selectedFields.Clear();
            selectedTables.Clear();
            txtEntityName.Text = string.Empty;

            dgSelectedField.DataSource = null;
            dgSelectedField.DataSource = selectedFields;
            dgSelectedField.Refresh();
            dgSelectedField.ClearSelection();

            dgTables.DataSource = null;
            dgTables.DataSource = selectedTables;
            dgTables.Refresh();
            dgTables.ClearSelection();
        }

        private void btnCreateEntity_Click(object sender, EventArgs e)
        {
            string spaceName = cbNameSpace.Text.ToUpper();
            string entityName = txtEntityName.Text.Trim();
            if (string.IsNullOrEmpty(entityName))
            {
                DialogTextbox dialog = new DialogTextbox("请输入数据模型名称");
                if (dialog.ShowDialog(this) == DialogResult.Yes)
                {
                    entityName = dialog.content;
                    txtEntityName.Text = entityName;
                }
                dialog.Dispose();
            }
            if (string.IsNullOrEmpty(entityName)) return;
            EntityInfo entityinfo = new EntityBLL().GetInfo(entityName, GetTableNameByEntityName(entityName));
            if (entityinfo != null)
            {
                ErrorMessage(entityName + "已存在");
                return;
            }
            entityinfo = new EntityInfo();
            entityinfo.Fid = Guid.NewGuid();
            entityinfo.EntityName = entityName;
            entityinfo.TableNames = GetTableNameByEntityName(entityName);
            SelectedField keyField = selectedFields.FirstOrDefault(d => d.keyFieldFlag);
            if (keyField == null) return;
            entityinfo.DefaultSort = "dbo.[" + keyField.tableName + "].[" + keyField.fieldName + "]-desc";///KT默认为NID倒排序
            entityinfo.Comments = string.Empty;
            entityinfo.ValidFlag = true;
            entityinfo.CreateUser = string.Empty;
            entityinfo.CreateDate = DateTime.Now;
            entityinfo.EntityType = 1;
            List<EntityFieldInfo> fieldlist = new List<EntityFieldInfo>();
            int cnt = 1;
            foreach (var field in selectedFields.Where(d => !d.keyFieldFlag))
            {
                EntityFieldInfo fieldinfo = new EntityFieldInfo();
                fieldinfo.Fid = Guid.NewGuid();
                fieldinfo.EntityFid = entityinfo.Fid;
                fieldinfo.FieldName = GetEntityNameByTableName(field.tableName) + "_" + field.fieldName;///对象的属性
                fieldinfo.TableFieldName = field.fieldName;///数据库字段
                fieldinfo.DisplayNameCn = field.descCn;
                fieldinfo.DisplayNameEn = field.descEn;
                fieldinfo.DisplayOrder = cnt * 10;
                fieldinfo.DataType = 10;///string
                fieldinfo.ControlType = 10;///textbox
                fieldinfo.DataLength = field.maxLength;
                fieldinfo.Nullenable = true;
                fieldinfo.Editable = true;
                fieldinfo.EditDisplayWidth = "200";
                fieldinfo.Listable = true;
                fieldinfo.ListDisplayWidth = "100";
                fieldinfo.Regex = string.Empty;
                fieldinfo.ValidFlag = true;
                fieldinfo.CreateDate = DateTime.Now;
                fieldinfo.CreateUser = string.Empty;
                fieldlist.Add(fieldinfo);
                cnt++;
            }
            using (var trans = new TransactionScope())
            {
                if (new EntityBLL().InsertInfo(entityinfo) == 0)
                {
                    ErrorMessage(entityName + "已经生成实体类失败");
                    return;
                }
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
            ShowMessage("数据模型添加成功");
            btnClear_Click(null, null);
        }

        private void LoadMoulds()
        {
            List<CodeItemDatasourceInfo> codeitems = new CodeBLL().GetDataSource("SYS_MOULD");
            foreach (var codeitem in codeitems)
            {
                cbNameSpace.Items.Add(codeitem.ItemDisplay);
            }
            if (cbNameSpace.Items.Count > 0)
                cbNameSpace.SelectedIndex = 0;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadMoulds();
        }
    }

    public class SelectedTable
    {
        public string tableName { get; set; }
        public bool mainTableFlag { get; set; }
    }

    public class SelectedField
    {
        public string tableName { get; set; }
        public bool keyFieldFlag { get; set; }
        public string fieldName { get; set; }
        public string descCn { get; set; }
        public string descEn { get; set; }
        public string dataType { get; set; }
        public int maxLength { get; set; }
        public int precision { get; set; }
        public string entityFieldName { get; set; }
    }
}
