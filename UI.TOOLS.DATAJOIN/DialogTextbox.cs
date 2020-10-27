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
    public partial class DialogTextbox : Form
    {
        public DialogTextbox()
        {
            InitializeComponent();
        }
        public DialogTextbox(string title)
        {
            InitializeComponent();
            this.Text = title;
        }
        public string content { get; set; }
        private void txtTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13) return;
            content = txtTextbox.Text.Trim();
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
