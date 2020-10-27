using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using LES_PDA.RPdaQualityChange;

namespace LES_PDA.Forms
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            login();
        }

        private void login()
        {
            //string message = "";
            //string user = txbuser.Text;
            //string password = txbPassword.Text;
            //if (user == "")
            //{
            //    MessageBox.Show("用户名不能为空！");
            //    return;
            //}
            //if (password == "")
            //{
            //    MessageBox.Show("密码不能为空！");
            //    return;
            //}
            ////调用web 传入用户名，密码
            //Program.mAppUser = AuthorityUser.login(user, password, out message);
            //if (message != "") { MessageBox.Show("登陆失败：\r\n" + message); return; }
            //if (Program.mAppUser == null) { MessageBox.Show("登陆失败：\r\n" + message); return; }
            //if (Program.mAppUser.Status == 0)
            //{
            //    Program.mAppUser = null;
            //    MessageBox.Show("登录失败：\r\n" + message);
            //    return;
            //}

            ////保存USER
            //string Userstr = user;
            //if (ckbOK.Checked)
            //{
            //    Userstr += "$" + password;
            //}

            //Program.mAppUser.MPassWord = password;
            //Log.WriteTxt(Log.logType.USER, Userstr);

            //Main main = new Main();
            //main.ShowDialog();

        }

        /// <summary>
        /// 电脑运行请注释、、、pda运行请执行
        /// </summary>
        private void PointLocation()
        {
            Point pt = new Point();
            pt.X = txbuser.Location.X;
            pt.Y = txbuser.Location.Y + 13;
            txbuser.Location = pt;

            pt.X = txbPassword.Location.X;
            pt.Y = txbPassword.Location.Y + 15;
            txbPassword.Location = pt;

            pt.X = ckbOK.Location.X;
            pt.Y = ckbOK.Location.Y + 15;
            ckbOK.Location = pt;

            pt.X = pictureBox2.Location.X;
            pt.Y = pictureBox2.Location.Y + 20;
            pictureBox2.Location = pt;

            pt.X = pictureBox3.Location.X;
            pt.Y = pictureBox3.Location.Y + 20;
            pictureBox3.Location = pt;
        }

        private void lodin_Load(object sender, EventArgs e)
        {
           // PointLocation();
            
            //读取用户
            string USERstr = Log.RadUSER();
            if (USERstr == null) { return; }
            string[] users = USERstr.Split('$');
            txbuser.Text = users[0].Trim();
            if (users.Length > 1)
            {
                txbPassword.Text = users[1].Trim();
                ckbOK.Checked = true;
            }
        }

        private void lodin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }
    }
}