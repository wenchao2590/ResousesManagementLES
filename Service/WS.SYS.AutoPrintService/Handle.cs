using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Infrustructure.Utilities;
using BLL.SYS;
using DM.SYS;
using System.Data;
using System.Transactions;
using System.Runtime.InteropServices;

namespace WS.SYS.AutoPrintService
{
    class Handle
    {
        #region 全局变量
        /// <summary>
        /// 登录用户
        /// </summary>
        string loginUser = "AutoPrintService";
        int status = 0;
        //要排除ID的集合
        List<long> dealedIds = new List<long>();
        #endregion
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SetDefaultPrinter(string Name);
        #region Handler
        public void Handler()
        {
            while (status != 10)
            {
                #region 获取第一条数据
                string idswhere = string.Empty;
                //数据库执行语句
                StringBuilder sqlText = new StringBuilder();
                //集合大于0，排除
                if (dealedIds.Count > 0)
                    idswhere = "and [ID] not in (" + string.Join(",", dealedIds.ToArray()) + ")";
                AutoPrintTaskInfo autoPrintTaskInfo = new AutoPrintTaskBLL().GetTopInfo(idswhere);
                if (autoPrintTaskInfo == null)
                    throw new Exception("0x00000001");///TODO:没有未打印的任务

                dealedIds.Add(autoPrintTaskInfo.Id);
                #endregion

                #region 打印
                PrintConfigInfo printConfigInfo = new PrintConfigBLL().GetInfoByCode(autoPrintTaskInfo.PrintConfigCode);
                if (printConfigInfo == null)
                    throw new Exception("0x00000002");///TODO:没有配置信息

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                //不现实调用程序窗口,但是对于某些应用无效
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //采用操作系统自动识别的模式
                p.StartInfo.UseShellExecute = true;
                //要打印的文件路径
                p.StartInfo.FileName = printConfigInfo.PrintTemplateUrl;
                //指定执行的动作，是打印，即print，打开是 open
                p.StartInfo.Verb = "print";
                //将指定的打印机设为默认打印机
                SetDefaultPrinter(printConfigInfo.PrinterName);
                //打印数量
                for (int i = 0; i < printConfigInfo.PrintCopies; i++)
                {
                    //开始打印
                    p.Start();
                }
                ////等待十秒
                //p.WaitForExit(10000);
                #endregion

                //ü   记录打印时间到最后修改时间⑦，同时标记状态③为20.已打印
                sqlText.AppendFormat(@" update TL_SYS_AUTO_PRINT_TASK set [STATUS] = 20,[MODIFY_TIME]=getdate(),[MODIFY_USER]='{0}' where [ID]={1}", loginUser, autoPrintTaskInfo.Id);
                #region 执行
                using (TransactionScope trans = new TransactionScope())
                {
                    if (!BLL.LES.CommonBLL.ExecuteNonQueryBySql(sqlText.ToString()))
                        throw new Exception("0x00000003");///TODO:写入数据库失败
                    trans.Complete();
                }
                #endregion
            }
        }
        #endregion
    }
}