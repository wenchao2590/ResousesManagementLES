using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using System.Linq;

namespace UI.TOOLS.DATAJOIN
{
    public partial class DialogTables : Form
    {
        public DialogTables()
        {
            InitializeComponent();
        }
        private string tableName;

        public string TableName
        {
            get
            {
                return tableName;
            }

            set
            {
                tableName = value;
            }
        }

        private List<string> tableNames = new List<string>();
        private void DialogTables_Load(object sender, EventArgs e)
        {
            string sql = "select name from sys.tables order by name";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                tableNames.Add(dr["name"].ToString());
            }
            cbList.DataSource = null;
            cbList.DataSource = tableNames;
            cbList.Refresh();
        }

        private void cbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbList.SelectedValue == null) return;
            tableName = cbList.SelectedValue.ToString();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13) return;
            cbList.DataSource = null;
            if (!string.IsNullOrEmpty(txtSearch.Text))
                cbList.DataSource = tableNames.Where(d => d.Contains(txtSearch.Text)).ToList();
            else
                cbList.DataSource = tableNames;
            cbList.Refresh();
        }
    }
}
