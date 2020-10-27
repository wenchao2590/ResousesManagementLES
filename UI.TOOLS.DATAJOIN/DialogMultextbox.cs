using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.TOOLS.DATAJOIN
{
    public partial class DialogMultextbox : Form
    {
        public DialogMultextbox()
        {
            InitializeComponent();
        }
        public DialogMultextbox(string title)
        {
            InitializeComponent();
            this.Text = title;
        }
        public string content { get; set; }
        private void btnYes_Click(object sender, EventArgs e)
        {
            content = txtTextbox.Text;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
