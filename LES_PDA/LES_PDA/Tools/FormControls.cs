using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LES_PDA.Tools
{
    public class FormControls
    {

        /// <summary>
        /// 空箱卡号长度 L
        /// </summary>
        public static int CodeLength = 10;

        /// <summary>
        /// 发动机长度 BSE
        /// </summary>
        public static int EnginLength = 15;


        /// <summary>
        /// 箱号
        /// </summary>
        public static int NOLength = 3;
        /// <summary>
        /// 更改传入当前窗体的按钮
        /// </summary>
        /// <param name="cl">当前窗体</param>
        public static void ColorButton(Form Forms)
        {
            try
            {
                Forms.BackColor = Color.LightSteelBlue;//窗体背景色

                Forms.MaximizeBox = false;
                Forms.MinimizeBox = false;
                Forms.ControlBox = false;
                //Forms.WindowState = FormWindowState.Maximized;
                Forms.Width = 325;
                Forms.Height = 300;
                foreach (Control ctrl in Forms.Controls)
                {
                    if (ctrl is TextBox || ctrl is ComboBox) { continue; }
                    else
                    {
                        ctrl.BackColor = Color.LightSteelBlue;//透明
                    }
                    if (ctrl is Button)//按钮
                    {
                        ctrl.BackColor = Color.RoyalBlue;//改成你默认的颜色
                        ctrl.ForeColor = Color.Black;//改变字体颜色
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ColorButton:" + ex.Message);
            }
        }
        
    }
}
