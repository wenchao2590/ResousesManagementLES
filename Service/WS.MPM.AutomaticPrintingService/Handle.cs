using System.Collections.Generic;
using System.Linq;
using System.Text;
using DM.LES;
using BLL.LES;
using System.Transactions;
using BLL.SYS;
using System;
using DM.SYS;
using System.Threading;
using System.Data;
using System.IO;
using System.Web;

namespace WS.MPM.AutomaticPrintingService
{
    /// <summary>
    /// Handle
    /// </summary>
    public class Handle
    {
        #region 全局变量
        /// <summary>
        /// 登录用户
        /// </summary>
        // private string loginUser = "CreateWindowTimeService";

        private static string sourceId = System.Configuration.ConfigurationManager.AppSettings["sourceId"];//获取资源ID
        private static string topSize = System.Configuration.ConfigurationManager.AppSettings["topSize"];//获取每次操作数量
        private static string printerName = string.Empty;
        private static string paperSize = string.Empty;
        #endregion

        #region Handler
        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
            GetInfo();

            string runsheetFileName = @"D:\LES20180509\LES\CODE\UI.WEB\TEMPLATE\BFDA\BFDATwdPullOrder.html";
            using (HTMLPrinter htmlPrinter = new HTMLPrinter(printerName, 100, paperSize, true))
            {
                htmlPrinter.Print(runsheetFileName, false);
            }
            #region 修改已打印任务

            #endregion
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void GetInfo()
        {
            try
            {
                //根据源ID，获取对应数据信息
                string sql_TL_SYS_AUTO_PRINT_TASK = @"SELECT TOP {0} [ID],[FID],[SHEET_FID],[PULL_MODEL],[PART_BOX_CODE],[PRINT_CONFIG_CODE],[PRINT_FILEPATH],[STATUS],[PRINT_TIME],[VALID_FLAG],
[SOURCE_ID],[CREATE_DATE],[CREATE_USER],[MODIFY_DATE],[MODIFY_USER] FROM [dbo].[TL_SYS_AUTO_PRINT_TASK]  WHERE [STATUS]=10 AND [SOURCE_ID]={1} ORDER BY [ID] ASC";
                List<AutoPrintTaskInfo> autoprinttaskinfos = new AutoPrintTaskBLL().GetList(String.Format(sql_TL_SYS_AUTO_PRINT_TASK, topSize, sourceId));
                if (autoprinttaskinfos.Count < 1)
                    return;
                ///获取基本配置信息
                string sql_TM_AUTO_PRINTER_CONFIG = @"SELECT [ID] ,[FID],[VALID_FLAG],[CREATE_USER],[CREATE_DATE],[MODIFY_USER],[MODIFY_DATE]
      ,[COMMENTS],[PULL_MODEL],[PART_BOX_CODE],[PRINT_CONFIG_CODE],[PRINTER_NAME] ,[PRINTER_IPADDRESS],[IS_AUTO_PRINTER],[PAPER_SIZE],[PRINT_DIRECTION]
  FROM [dbo].[TM_AUTO_PRINTER_CONFIG]";
                List<PrinterConfigInfo> printerconfiginfos = new PrinterConfigBLL().GetList(sql_TM_AUTO_PRINTER_CONFIG);
                if (printerconfiginfos.Count < 1)
                    return;
                ///加载打印机配置信息
                string sql_TS_SYS_PRINT_CONFIG = @"SELECT [ID],[FID],[PRINT_CONFIG_CODE],[PRINT_CONFIG_NAME],[PRINT_TEMPLATE_FILENAME],[PRINT_TEMPLATE_URL],[TEMPLATE_FILE_TYPE]
      ,[PRINT_COPIES],[PRINTER_NAME],[LAST_UPLOADFILE_TIME],[STATUS],[COMMENTS],[VALID_FLAG],[CREATE_USER],[CREATE_DATE],[MODIFY_USER],[MODIFY_DATE] FROM [dbo].[TS_SYS_PRINT_CONFIG]";
                List<PrintConfigInfo> printconfiginfos = new PrintConfigBLL().GetList(sql_TS_SYS_PRINT_CONFIG);

                ///循环匹配打印机设置
                foreach (var item in autoprinttaskinfos)
                {
                    string pullModel = item.PullModel.GetValueOrDefault().ToString();
                    string partBoxCode = item.PartBoxCode;
                    if (string.IsNullOrEmpty(pullModel) || string.IsNullOrEmpty(partBoxCode))
                        continue;
                    //开始匹配，如果不为NULL，则表示匹配成功
                    var selectlist = printerconfiginfos.FirstOrDefault(a => a.PullModel == int.Parse(pullModel) && a.PartBoxCode == partBoxCode);
                    if (selectlist == null)
                        continue;
                    string printConfigCode = item["PRINT_CONFIG_CODE"].ToString();
                    //通过匹配到的PRINT_CONFIG_CODE 在printconfiginfos集合中匹配，如果匹配成功，则获取打印信息；
                    var listPathRout = printconfiginfos.FirstOrDefault(a => a.PrintConfigCode == printConfigCode && a.Status == 20);
                    if (listPathRout == null)
                        continue;
                    //Thread.Sleep(100);
                    string printTemplateFilename = listPathRout.PrintTemplateFilename;//获取模板名称
                    string printTemplateUrl = listPathRout.PrintTemplateUrl;//获取路径

                    ///根据打印配置代码①从打印配置TS_SYS_PRINT_CONFIG中获取对应的配置信息，如未能获取有效数据则提示<打印配置信息错误>，并记录系统日志
                    PrintConfigInfo info = new PrintConfigBLL().GetInfoByCode(printConfigCode);
                    if (info == null)
                        throw new Exception("MC:0x00000731");///打印配置信息错误

                    ///根据打印配置中的打印模板路径④及打印模板文件名称③获取服务端中的两个文件
                    ///一个为打印模板文件名称③，另一个为同名的xml字段匹配配置文件
                    string templateFileType = "html";
                    switch (info.TemplateFileType.GetValueOrDefault())
                    {
                        case (int)TemplateFileTypeConstants.xlsx: templateFileType = "xlsx"; break;
                        case (int)TemplateFileTypeConstants.xls: templateFileType = "xls"; break;
                        default: templateFileType = "html"; break;
                    }
                    ///根据打印文件路径名称在服务端获取相应的文件，如未能成功获取则提示<打印文件错误>，并记录系统日志TL_SYS_OPERATION_LOG
                    if (!info.PrintTemplateUrl.StartsWith("/"))
                        info.PrintTemplateUrl = "/" + info.PrintTemplateUrl;
                    string templateFileName = HttpContext.Current.Server.MapPath(info.PrintTemplateUrl) + "/" + info.PrintTemplateFilename + "." + templateFileType;
                    if (!File.Exists(templateFileName))
                        throw new Exception("MC:0x00000061");///打印模板文件不存在

                    string configFileName = HttpContext.Current.Server.MapPath(info.PrintTemplateUrl) + "/" + info.PrintTemplateFilename + ".xml";
                    if (!File.Exists(configFileName))
                        throw new Exception("MC:0x00000067");///打印配置文件不存在

                    ///
                    string tempPrintFilePathConfig = "/TEMP/PRINTFILES";
                    ///
                    string tempPrintFilePath = HttpContext.Current.Server.MapPath(tempPrintFilePathConfig);
                    ///文件夹是否存在
                    if (!Directory.Exists(tempPrintFilePath))
                        Directory.CreateDirectory(tempPrintFilePath);

                    List<string> listfiles = new PrintBLL().CreatePrintFiles(templateFileName, configFileName, templateFileType, GetPrintDataInfos("A", item["SHEET_FID"].ToString()), tempPrintFilePath, tempPrintFilePathConfig);
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取打印数据
        /// </summary>
        /// <param name="source_id"></param>
        /// <returns></returns>
        private DataSet GetPrintDataInfos(string source_id, string sheetId)
        {
            DataSet ds = new DataSet();
            switch (source_id)
            {
                case "A":
                    ds = GetSource();
                    break;
                case "B":
                    ds = GetSource1();
                    break;
                default:
                    break;
            }
            return ds;
        }

        private DataSet GetSource()
        {
            #region 获取TWD拉动单打印数据源
            string sqlStr = @"
SELECT TOP 100 XA.[ID] as TASK_ID, XC.[ITEM_NAME] as ORDER_TYPE, XB.*
FROM[dbo].[TL_SYS_AUTO_PRINT_TASK] as XA (Nolock)
left join[LES].[TT_MPM_TWD_PULL_ORDER] as XB (Nolock)on XA.[SHEET_FID] = XB.[FID]
left join TS_SYS_CODE_ITEM XC with(nolock) on XC.[ITEM_VALUE] = XB.[ORDER_TYPE] and XC.[CODE_FID] = N'4afa543d-4455-4e54-868e-f36474e21cf6' and XC.[VALID_FLAG] = 1
WHERE XA.[STATUS]= 10 AND XA.[SOURCE_ID]= 10 and XA.[PULL_MODEL] = 10
ORDER BY XA.[ID] ASC;
select XA.* from LES.TT_MPM_TWD_PULL_ORDER_DETAIL as XA (Nolock)
where XA.[ORDER_FID]  in (
SELECT TOP 100 XB.[SHEET_FID] from [dbo].[TL_SYS_AUTO_PRINT_TASK] as XB (Nolock)
WHERE XB.[STATUS]= 10 AND XB.[SOURCE_ID]= 10 and XB.[PULL_MODEL] = 10
ORDER BY XA.[ID] ASC
);";
            #endregion
            return BLL.SYS.CommonBLL.ExecuteDataSetBySql(sqlStr);
        }
        private DataSet GetSource1()
        {
            return null;
        }
        private DataSet GetSource2()
        {
            return null;
        }
    }
}