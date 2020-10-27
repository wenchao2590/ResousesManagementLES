using BLL.SYS;
using System;
using System.Windows.Forms;

namespace UI.Client.Templete
{
    public partial class FrmLogin : Form
    {
        public Infrustructure.BaseClass.IUser User { get; private set; }
        Guid Workcell = Guid.Empty;
        public FrmLogin(Guid WorkCell)
        {
            Workcell = WorkCell;
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

            this.txtUserName.Text = "admin";
            this.txtPassWord.Text = "123456";
            this.BtnLogin.Select();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()) || string.IsNullOrEmpty(txtPassWord.Text.Trim()))
            {
                MessageBox.Show("用户名或密码不可为空");
                return;
            }
            String Msg = string.Empty;
            ///User = new UserBLL().Login(txtUserName.Text, txtPassWord.Text, this.cmbPlant.SelectedValue.ToString(), out Msg, Workcell,new Guid(cmbPlant.SelectedValue.ToString()));


            if (User == null)
            {
                this.txtUserName.Text = "";
                this.txtPassWord.Text = "";
                MessageBox.Show(Msg);
                return;
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Application.Exit();
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.BtnLogin_Click(null, null);
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPassWord.Select();
            }
        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLogin_Click(null, null);
            }
        }
    }
}
