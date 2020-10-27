using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.TOOLS.DATADEAL
{
    public partial class DialogTables : Form
    {
        /// <summary>
        /// Table or View
        /// 1.Table
        /// 2.View
        /// </summary>
        private int torv = 1;
        public DialogTables()
        {
            InitializeComponent();
        }
        public DialogTables(int torv)
        {
            this.torv = torv;
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

        private void DialogTables_Load(object sender, EventArgs e)
        {
            string sql = "select name from sys.tables order by name";
            if (torv == 2)
                sql = "select name from sys.views order by name";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            List<string> tableNames = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                tableNames.Add(dr["name"].ToString());
            }
            cbList.DataSource = tableNames;
        }

        private void cbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableName = cbList.SelectedValue.ToString();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
