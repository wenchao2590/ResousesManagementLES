using BLL.SYS;
using DM.SYS;
using Infrustructure.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UI.Client.Templete
{
    public partial class FrmTemplete : Form
    {
        protected IUser iUser;
        public Dictionary<int, Button> ActionListDic = new Dictionary<int, Button>();
        public FrmTemplete()
        {
            InitializeComponent();

        }

        public FrmTemplete(IUser user)
        {
            InitializeComponent();
            this.iUser = user;
            this.KeyDown += Templete_KeyDown;
            BtnBind();
        }


        protected void StopMessage()
        {
            //this.panel1.Visible = false;
        }
        protected void ShowMessage(string Msg, AppClass.AppConstants.MessageTypeStatus MessageType)
        {
            try
            {
                if (lblMessage.Text == Msg) return;
                this.Invoke(new EventHandler(delegate
                {
                    switch (MessageType)
                    {

                        case AppClass.AppConstants.MessageTypeStatus.Error:
                            this.lblMessage.Text = Msg;
                            this.pnlMessage.BackColor = Color.Red;
                            this.lblMessage.BackColor = Color.Red;
                            this.lblMessage.ForeColor = Color.White;
                            break;
                        case AppClass.AppConstants.MessageTypeStatus.Message:
                            this.lblMessage.Text = Msg;
                            this.pnlMessage.BackColor = Color.ForestGreen;
                            this.lblMessage.BackColor = Color.ForestGreen;
                            this.lblMessage.ForeColor = Color.White;
                            break;
                    }
                }));
            }
            catch { }
        }

        private void BtnBind()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FrmTemplete));
            List<ActionInfo> actions = new MenuActionBLL().GetActionByPageUrl(iUser.MenuName, iUser.CurrentRoleFid);
            int left = 5;
            try
            {
                int GBCount = 1;
                var GroupPoint = new Point(0, 2);
                foreach (ActionInfo actioninfo in actions)
                {
                    GroupBox ActionGB = new GroupBox();
                    ActionGB.Text = "F" + GBCount;
                    ActionGB.Left = left;
                    ActionGB.Location = GroupPoint;
                    ActionGB.Width = 110;
                    ActionGB.Height = 100;
                    ActionGB.BackColor = Color.Transparent;
                    Button ActionBtn = new Button();
                    ActionBtn.Text = actioninfo.ActionNameCn;
                    ActionBtn.Dock = DockStyle.Fill;
                    ActionBtn.BackgroundImage = ((Image)(resources.GetObject("BtnClose.BackgroundImage")));
                    ActionBtn.ForeColor = Color.White;
                    ActionBtn.Font = new Font("Microsoft YaHei", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
                    ActionBtn.BackgroundImageLayout = ImageLayout.Stretch;
                    ActionBtn.FlatStyle = FlatStyle.Flat;
                    var onlineMethod = this.GetType().GetMethod(string.Format("{0}Online", actioninfo.ActionName), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (onlineMethod == null)
                        continue;
                    ActionBtn.Click += (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), this, onlineMethod, false);
                    PalAction.Controls.Add(ActionGB);
                    ActionGB.Controls.Add(ActionBtn);
                    GBCount++;
                    GroupPoint.X += 5 + ActionGB.Width;
                    ActionListDic.Add(GBCount, ActionBtn);
                }
            }
            catch
            {

            }
        }

        private void Templete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                if (MessageBox.Show("关闭当前选项卡？"
                  , "提示"
                  , MessageBoxButtons.YesNo
                  , MessageBoxIcon.Question
                  , MessageBoxDefaultButton.Button1)
                  == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                    return;
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("关闭当前选项卡？"
                  , "提示"
                  , MessageBoxButtons.YesNo
                  , MessageBoxIcon.Question
                  , MessageBoxDefaultButton.Button1)
                  == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
                return;
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            lblMessage.Location = new Point(Convert.ToInt32(pnlMessage.Width - lblMessage.Width) / 2, Convert.ToInt32(pnlMessage.Height - lblMessage.Height) / 2);
        }
    }
}
