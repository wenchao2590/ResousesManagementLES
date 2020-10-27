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
    public partial class DialogAction : Form
    {
        public DialogAction()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            addBtn = ckAdd.Checked;
            editBtn = ckEdit.Checked;
            delBtn = ckDel.Checked;
            searchBtn = ckSearch.Checked;
            saveBtn = ckSave.Checked;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void DialogAction_Load(object sender, EventArgs e)
        {
            ckAdd.Checked = true;
            ckEdit.Checked = true;
            ckDel.Checked = true;
            ckSearch.Checked = true;
            ckSave.Checked = true;
        }

        private bool addBtn;
        private bool editBtn;
        private bool delBtn;
        private bool searchBtn;
        private bool saveBtn;

        public bool AddBtn
        {
            get
            {
                return addBtn;
            }

            set
            {
                addBtn = value;
            }
        }

        public bool EditBtn
        {
            get
            {
                return editBtn;
            }

            set
            {
                editBtn = value;
            }
        }

        public bool DelBtn
        {
            get
            {
                return delBtn;
            }

            set
            {
                delBtn = value;
            }
        }

        public bool SearchBtn
        {
            get
            {
                return searchBtn;
            }

            set
            {
                searchBtn = value;
            }
        }

        public bool SaveBtn
        {
            get
            {
                return saveBtn;
            }

            set
            {
                saveBtn = value;
            }
        }
    }
}
