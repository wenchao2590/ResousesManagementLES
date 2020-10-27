using BLL.SYS;
using DM.SYS;
using Infrustructure.BaseClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UI.Client
{
    public partial class FrmMain : Form
    {
        private Hashtable MenuListHT = new Hashtable();//Key：FormName Value：FormList
        private int DataTableCount = 0;//记录当前菜单加载项
        private int MaxDataTableCount = 0;//记录最大菜单加载项
        private int MainMenuMaxCount = int.Parse(ConfigurationManager.AppSettings["MainMenuMaxCount"].ToString());
        private string IsMaxWindows = ConfigurationManager.AppSettings["IsMaxWindows"].ToString();
        //public List<Thread> threadList = new List<Thread>();
        public FrmMain()
        {
            InitializeComponent();
        }
        private IUser users;
        public FrmMain(IUser user)
        {
            users = user;
            InitializeComponent();

            if (IsMaxWindows.ToUpper() == "TRUE")
                this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //this.KeyDown += FrmMain_KeyDown;
            //this.LabLoginName.Text = user.UserLoginName;
            RightTxtBoxBind(user);

        }

        private void RightTxtBoxBind(IUser user)
        {
            this.LabProdLine.Text = user.ProdLineCode;
            //this.LabWorkShop.Text = user.WorkShopCode;
            this.LabEmployeeName.Text = user.EmployeeName;
            this.LabWorkCell.Text = user.WorkCellCode;
        }

        /// <summary>
        /// 菜单绑定方法
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        private void LeftMenuBind(int startIndex, int endIndex)
        {
            if (CmbRoleList.SelectedValue == null) return;
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FrmMain));
            Guid roleFid = Guid.Parse(CmbRoleList.SelectedValue.ToString());
            List<MenuInfo> menus = new MenuBLL().GetClientMenusByRoleFid(roleFid);
            MaxDataTableCount = menus.Count;
            int top = 3;
            int left = 0;
            int a = 1;
            try
            {
                while (startIndex != endIndex)
                {
                    MenuInfo menuinfo = menus[startIndex];
                    Button btn = new Button();
                    btn.Name = menuinfo.Id.ToString();
                    btn.Text = menuinfo.MenuNameCn;
                    btn.Left = left;
                    btn.Height = 60;
                    btn.Width = 140;
                    btn.BackgroundImage = ((Image)(resources.GetObject("btnExit.BackgroundImage")));
                    btn.ForeColor = Color.White;
                    btn.Font = new Font("Microsoft YaHei", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Tag = menuinfo.LinkUrl;
                    btn.Top = a * (btn.Height + top) - btn.Height;
                    btn.Click += new EventHandler(btn_click);
                    PalMenuList.Controls.Add(btn);
                    a++; startIndex++;
                }
            }
            catch
            { }
            finally
            {
                DataTableCount = endIndex;
            }
        }
        /// <summary>
        /// 菜单按钮加载多任务方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string MenuLinkNameSpace = b.Tag.ToString().Substring(0, b.Tag.ToString().IndexOf(','));
            string MenuLinkName = b.Tag.ToString().Substring(b.Tag.ToString().IndexOf(',') + 1);

            if (MenuListHT.ContainsValue(b.Text))
            {
                return;
            }
            else
            {
                MenuListHT.Add(MenuListHT.Count + 1, b.Text);
            }
            this.IsMdiContainer = true;  //如果你要加入一个窗口放在tabpage里，原窗口要设置成mdi父窗口
            TabPage page1 = new TabPage(); //建立新的tabpage
            page1.Text = b.Text;
            page1.Tag = MenuListHT.Count;
            page1.ForeColor = Color.SteelBlue;
            users.CurrentRoleFid = new Guid(CmbRoleList.SelectedValue.ToString());
            users.MenuName = b.Tag.ToString();
            TabConMain_MouseDoubleClick("no", null);
            Form f = (Form)Assembly.Load(MenuLinkNameSpace).CreateInstance(MenuLinkName, true, System.Reflection.BindingFlags.Default, null, new Object[] { this.users }, null, null);//object 传参为 登陆用户及当前选择的角色信息
            TabConMain.TabPages.Add(page1);   //把tabpage加入到tabcontrol里
            f.MdiParent = this;   //新窗口的父窗口是原窗口
            f.TopLevel = false;
            f.Parent = page1;
            f.BackColor = Color.White;
            f.Dock = DockStyle.Fill;
            f.FormBorderStyle = FormBorderStyle.None;
            f.FormClosed += new FormClosedEventHandler(ChileForm_FormClosed);
            f.Show();
            TabConMain.SelectedTab = page1;


            //ActionBind(MenuLinkName,f);
        }
        /// <summary>
        /// 子菜单关闭时将tabpage关闭并删除HT中记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChileForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                TabPage page = TabConMain.SelectedTab;
                TabConMain.TabPages.Remove(page);
                MenuListHT.Remove(page.Tag);
                this.TopPanel.Visible = true;
                this.panel3.Visible = true;
            }
            catch { }
        }
        /// <summary>
        /// 退出  -弃用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("退出程序?"
               , "提示"
               , MessageBoxButtons.YesNo
               , MessageBoxIcon.Question
               , MessageBoxDefaultButton.Button1)
               == System.Windows.Forms.DialogResult.Yes)
            {
                TabPage page = TabConMain.SelectedTab;
                TabConMain.TabPages.Remove(page);
                MenuListHT.Remove(page.Text);

            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            CmbRoleList.DataSource = new RoleBLL().GetRolesByUser(users.UserFid);
            CmbRoleList.DisplayMember = "Comments";
            CmbRoleList.ValueMember = "FId";
            LeftMenuBind(0, MainMenuMaxCount);
        }
        /// <summary>
        /// 上一页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOn_Click(object sender, EventArgs e)
        {
            if (DataTableCount != 0)
            {
                if (DataTableCount >= MainMenuMaxCount)
                {
                    PalMenuList.Controls.Clear();
                    LeftMenuBind(DataTableCount - (MainMenuMaxCount * 2) > 0 ? DataTableCount - (MainMenuMaxCount * 2) : 0, DataTableCount - MainMenuMaxCount < MainMenuMaxCount ? MainMenuMaxCount : DataTableCount - MainMenuMaxCount);
                }
            }
        }
        /// <summary>
        /// 下一页按钮 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (MaxDataTableCount > DataTableCount)
            {
                PalMenuList.Controls.Clear();
                LeftMenuBind(DataTableCount, MaxDataTableCount - MainMenuMaxCount > DataTableCount ? DataTableCount + MainMenuMaxCount : MaxDataTableCount);
            }
        }
        private void BtnCellChoose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("重新选择工位将关闭当前已打开的选项卡，是否确认？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearALL();


            }
        }



        /// <summary>
        /// 清空方法
        /// </summary>
        private void ClearALL(int ClearMenu = 0)
        {
            this.MaxDataTableCount = 0;
            this.DataTableCount = 0;
            foreach (TabPage a in TabConMain.TabPages)
            {
                if (a.Text != "首页")
                    this.TabConMain.TabPages.Remove(a);
            }
            MenuListHT.Clear();
            this.PalMenuList.Controls.Clear();
            // this.PalActionBtnList.Controls.Clear();
        }

        private void TabConMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (TopPanel.Visible == false && sender.ToString() != "no")
            {
                this.TopPanel.Visible = true;
                this.panel3.Visible = true;
            }
            else
            {
                this.TopPanel.Visible = false;
                this.panel3.Visible = false;
            }
        }
        /// <summary>
        /// 角色改变-需要清空页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbRoleList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (MessageBox.Show("切换角色后将关闭当前打开的项，是否确认？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ClearALL();
                LeftMenuBind(0, MainMenuMaxCount);
            }
            else
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
