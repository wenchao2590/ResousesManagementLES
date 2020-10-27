using BLL.SYS;
using DM.SYS;
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
    public partial class DialogMenu : Form
    {
        public DialogMenu()
        {
            InitializeComponent();
        }
        private Guid parentMenuFid;

        public Guid ParentMenuFid
        {
            get
            {
                return parentMenuFid;
            }

            set
            {
                parentMenuFid = value;
            }
        }

        private List<MenuInfo> list = new List<MenuInfo>();
        private int menuIndex;

        private void DialogMenu_Load(object sender, EventArgs e)
        {
            int dataCount;
            list = new MenuBLL().GetListByPage("and [VALID_FLAG] = 1 "
                + "and ([PARENT_MENU_FID] = '" + Guid.Empty + "' or  [PARENT_MENU_FID] is null)"
                , "[DISPLAY_ORDER]"
                , 1
                , int.MaxValue
                , out dataCount);
            LoadMenuToView();
            #region 功能
            ckAdd.Checked = true;
            ckDel.Checked = true;
            ckEdit.Checked = true;
            ckForm.Checked = true;
            ckSave.Checked = true;
            ckSearch.Checked = true;
            #endregion
        }

        private void LoadMenuToView()
        {
            for (int i = 1; i <= 4; i++)
            {
                Button btn = this.Controls.Find("btnMenu" + i, true)[0] as Button;
                btn.Text = string.Empty;
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
                btn.Click -= Btn_Click;
                btn.Tag = null;
                if (list.Count < menuIndex + i) continue;
                MenuInfo info = list[menuIndex + i - 1];
                btn.Text = info.MenuName + "-" + info.MenuNameCn;
                btn.Tag = info;
                btn.Click += Btn_Click;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Tag == null) return;
            MenuInfo info = btn.Tag as MenuInfo;
            parentMenuFid = info.Fid.GetValueOrDefault();
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (menuIndex == 0) return;
            menuIndex -= 4;
            LoadMenuToView();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (list.Count <= menuIndex + 4) return;
            menuIndex += 4;
            LoadMenuToView();
        }
    }
}
