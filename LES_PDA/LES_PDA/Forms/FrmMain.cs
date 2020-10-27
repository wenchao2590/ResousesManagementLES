using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LES_PDA.Tools;
using System.Reflection;
//using LES_PDA.NewCodeText;
using LES_PDA.Forms;

namespace LES_PDA
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// int  下标  string==Resource资源值
        /// </summary>
        Dictionary<int, string> ResourceIndex = new Dictionary<int, string>();
        private void inUser()
        {
            if (Program.mAppUser == null) { MessageBox.Show("请登录！"); return; }
            if (Program.mAppUser.mUserName != "")
            {
                if (Program.mAppUser.mResource.Count <= 0)
                {
                    MessageBox.Show("该用户没有任何权限！\r\n请通知管理员添加权限。");
                }
                int y = 35;
                int x = 5;
                int jg = 8;
                int count = 1;
                foreach (string s in Program.mAppUser.mResource)
                {
                    PictureBox PicBox = new PictureBox();
                    PicBox.Width = 100;//图标的宽度
                    PicBox.Height = 45;//图标的高度
                    Point p = new Point();
                    p.X = x;//X坐标
                    p.Y = y;
                    PicBox.Location = p;
                    x = x + PicBox.Width + 5;//x坐标
                    if (count % 3 == 0)//表示三列
                    {
                        x = 5;
                        y = y + PicBox.Height + jg;//y坐标
                    }
                    string[] str = s.Split('$');
                    PicBox.Image = imageList1.Images[IndexPermission(str[1])];
                    PicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    PicBox.Tag = str[1];
                    PicBox.Name = str[1];
                    PicBox.Click += new EventHandler(PicBox_Click);
                    this.Controls.Add(PicBox);
                    ResourceIndex.Add(count - 1, str[1]);
                    count++;
                }
            }
        }

        /// <summary>
        /// 对应的图片下标
        /// </summary>
        /// <param name="Name">资源值</param>
        /// <returns></returns>
        private int IndexPermission(string Name)
        {
            //int Index=12;
           // int Index = 13;
            int Index = 15;
            switch (Name)
            {
                case "Receipt":
                    Index = 0;
                    break;
                case "ExternalExamination":
                    Index = 1;
                    break;
                case "Picking":
                    Index = 2;
                    break;
                case "Added":
                    Index = 3;
                    break;
                case "StockFreeze":
                    Index = 4;
                    break;
                case "StockMovement":
                    Index = 5;
                    break;
                case "QualityStatusChange":
                    Index = 6;
                    break;
                case "Stock":
                    Index = 7;
                    break;
                case "KanBanScan":
                    Index = 8;
                    break;
                case "StockUNFreeze":
                    Index = 9;
                    break;
                case "TranFor":
                    Index = 10;
                    break;
                case "SortQuery":
                    Index = 11;
                    break;
                case "CyProduct":
                    Index = 13;
                    break;
                case "ASNWholeInwhole":
                    Index = 14;
                    break;
                case "StampingFinishedProductStorage":
                    Index = 15;
                    break;
                case "Cyckjh":
                    Index = 16;
                    break;
                default:
                    Index = 12;
                    break;
            }
            return Index;
        }

        void PicBox_Click(object sender, EventArgs e)
        {
            PictureBox btn = (PictureBox)sender;
            string btnTag = btn.Tag.ToString();
            if (btnTag != "") { Openbtn(btnTag); }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            inUser();

            FormControls.ColorButton(this);
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string btnTag = btn.Tag.ToString();
            if (btnTag != "") { Openbtn(btnTag); }
        }

        private void Openbtn(string btnTag)
        {
            try
            {
                Type clsType = Type.GetType("LES_PDA.OpenForm");
                MethodInfo info = clsType.GetMethod(btnTag);
                object obj = Activator.CreateInstance(clsType);
                info.Invoke(obj, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("提示\r\n无法打开该功能，请联系管理员：\r\n");
            }
        }
        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D0:
                    Resource(9);
                    break;
                case Keys.D1:
                    Resource(6);
                    break;
                case Keys.D2:
                    Resource(7);
                    break;
                case Keys.D3:
                    Resource(8);
                    break;
                case Keys.D4:
                    Resource(3);
                    break;
                case Keys.D5:
                    Resource(4);
                    break;
                case Keys.D6:
                    Resource(5);
                    break;
                case Keys.D7:
                    Resource(0);
                    break;
                case Keys.D8:
                    Resource(1);
                    break;
                case Keys.D9:
                    Resource(2);
                    break;
                case Keys.F1:
                    Resource(10);
                    break;
                case Keys.F2:
                    Resource(11);
                    break;
                case Keys.F3:
                    Resource(12);
                    break;
                case Keys.F5:
                    Resource(13);
                    break;
                case Keys.F6:
                    Resource(14);
                    break;
                case Keys.M:
                    Resource(15);
                    break;
                case Keys.Escape:
                    this.Close();//直接退出
                    break;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 返回资源值
        /// </summary>
        /// <param name="Index">数组下标</param>
        /// <returns></returns>
        void Resource(int Index)
        {
            string Res = "";
            try
            {
                Res = ResourceIndex[Index];
                Openbtn(Res);
            }
            catch { }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}