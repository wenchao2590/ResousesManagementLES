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
    public partial class DialogSqlExport : Form
    {
        public DialogSqlExport()
        {
            InitializeComponent();
        }

        public DialogSqlExport(string sqlStr)
        {
            InitializeComponent();
            this.sqlStr = sqlStr;
        }
        private string sqlStr;
        private void DialogSqlExport_Load(object sender, EventArgs e)
        {
            rtb.Text = sqlStr;
        }
    }
}
