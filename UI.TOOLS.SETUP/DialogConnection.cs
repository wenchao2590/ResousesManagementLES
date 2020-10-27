using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.TOOLS.SETUP
{
    public partial class DialogConnection : Form
    {
        public DialogConnection()
        {
            InitializeComponent();
        }

        public DialogConnection(string initialCatalog, string dataSource, string userID, string password)
        {
            InitializeComponent();
            txtInitialCatalog.Text = initialCatalog;
            txtDataSource.Text = dataSource;
            txtUserID.Text = userID;
            txtPassword.Text = password;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInitialCatalog.Text)) return;
            if (string.IsNullOrEmpty(txtDataSource.Text)) return;
            if (string.IsNullOrEmpty(txtUserID.Text)) return;
            if (string.IsNullOrEmpty(txtPassword.Text)) return;
            this.DialogResult = DialogResult.Yes;
        }
    }
}
