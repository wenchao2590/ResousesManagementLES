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
    public partial class DialogInput : Form
    {
        public DialogInput()
        {
            InitializeComponent();
        }

        public DialogInput(string tipMsg)
        {
            InitializeComponent();
            this.Text = tipMsg;
        }

        public DialogInput(string tipMsg, string inputMsg)
        {
            InitializeComponent();
            this.Text = tipMsg;
            txtInput.Text = inputMsg;
        }

        private string inputMsg;

        public string InputMsg
        {
            get
            {
                return inputMsg;
            }

            set
            {
                inputMsg = value;
            }
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13) return;
            if (txtInput.Text.Trim().Length == 0) return;
            inputMsg = txtInput.Text.Trim();
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void DialogInput_Load(object sender, EventArgs e)
        {
            txtInput.Focus();
        }
    }
}
