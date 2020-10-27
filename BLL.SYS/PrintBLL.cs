using DAL.SYS;
using DM.SYS;
using Infrustructure.Barcode;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL.SYS
{
    public class PrintBLL
    {
        AutoPrintTaskDAL dal = new AutoPrintTaskDAL();
        public bool CreateAutoPrintTask(string printConfigCode, string printFilepath)
        {
            AutoPrintTaskInfo info = new AutoPrintTaskInfo();
            info.PrintConfigCode = printConfigCode;
            info.PrintFilepath = printFilepath;
            info.Status = (int)PrintStateConstants.NOT_TO_PRINT;
            info.CreateDate = DateTime.Now;
            info.Fid = Guid.NewGuid();
            return dal.Add(info) > 0 ? true : false;
        }

        /// <summary>
        /// Task 90 打印文件生成函数
        /// TODO:仅实现了HTML格式的打印
        /// </summary>
        /// <param name="printConfigCode"></param>
        /// <returns></returns>
        public List<string> CreatePrintFiles(string templateFileName, string configFileName, string templateFileType, DataSet dataSet, string tempPrintFilePath, string tempPrintFilePathConfig)
        {
            StringBuilder sbTemplate = new StringBuilder();
            using (StreamReader sr = new StreamReader(templateFileName, Encoding.Default))
            {
                sbTemplate.Append(sr.ReadToEnd());
            }
            ///字段匹配配置文件格式：
            XmlWrapper xmlWrapper = new XmlWrapper(configFileName, LoadType.FromFile);
            ///主表配置信息
            PrintConfigXmlTableInfo printConfigXmlTableInfo = xmlWrapper.XmlToObject("/Table", typeof(PrintConfigXmlTableInfo)) as PrintConfigXmlTableInfo;
            if (string.IsNullOrEmpty(printConfigXmlTableInfo.AutoPrintFlag))
                sbTemplate = sbTemplate.Replace("$AutoPrintFlag$", string.Empty);
            else
            {
                if (printConfigXmlTableInfo.AutoPrintFlag.ToLower() == "true")
                    sbTemplate = sbTemplate.Replace("$AutoPrintFlag$", "<script>window.onload = function () {window.print();function colseAfterPrint() {if (p = document.execCommand('print')) window.close();else setTimeout('colseAfterPrint();', 1000);}colseAfterPrint();}</script>");
                else
                    sbTemplate = sbTemplate.Replace("$AutoPrintFlag$", string.Empty);
            }
            ///主表配置字段信息
            List<object> printConfigXmlFieldInfos = xmlWrapper.XmlToList("/Table/Field", typeof(PrintConfigXmlFieldInfo));
            ///明细行配置
            PrintConfigXmlDetailsInfo printConfigXmlDetailsInfo = xmlWrapper.XmlToObject("/Table/Details", typeof(PrintConfigXmlDetailsInfo)) as PrintConfigXmlDetailsInfo;
            ///明细行字段
            List<object> printConfigXmlDetailInfos = xmlWrapper.XmlToList("/Table/Details/Field", typeof(PrintConfigXmlFieldInfo));

            DataTable dataTable = dataSet.Tables[printConfigXmlTableInfo.TableName];
            if (dataTable == null && dataSet.Tables.Count > 0) dataTable = dataSet.Tables[0];
            List<string> printFiles = new List<string>();
            if (dataTable == null) return printFiles;

            foreach (DataRow dr in dataTable.Rows)
            {
                ///主表填充
                string printContent = FillPrintContent(sbTemplate.ToString(), printConfigXmlFieldInfos, dr, tempPrintFilePath, tempPrintFilePathConfig);
                if (printConfigXmlDetailsInfo == null)
                {
                    string saveFileName = printConfigXmlTableInfo.PrintFileName + "_" + DateTime.Now.Ticks + "." + templateFileType;
                    FileInfo fileInfo = new FileInfo(saveFileName);
                    File.WriteAllText(tempPrintFilePath + @"\" + saveFileName
                        , printContent
                        , Encoding.GetEncoding("GB2312"));
                    printFiles.Add(tempPrintFilePathConfig + "/" + saveFileName);
                    continue;
                }
                string[] relationFields = printConfigXmlDetailsInfo.RelationFields.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string conditions = string.Empty;
                for (int i = 0; i < relationFields.Length; i++)
                {
                    string[] relationField = relationFields[i].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                    if (relationField.Length != 2) continue;
                    conditions += "and " + relationField[0] + " = '" + dr[relationField[1]] + "'";
                }
                DataTable dtDetail = dataSet.Tables[printConfigXmlDetailsInfo.TableName];
                if (printConfigXmlDetailsInfo.TableName == printConfigXmlTableInfo.TableName) dtDetail = dataTable;
                if (dtDetail == null && dataSet.Tables.Count > 1) dtDetail = dataSet.Tables[1];
                if (dtDetail == null)
                {
                    string saveFileName = printConfigXmlTableInfo.PrintFileName + "_" + DateTime.Now.Ticks + "." + templateFileType;
                    FileInfo fileInfo = new FileInfo(saveFileName);
                    File.WriteAllText(tempPrintFilePath + @"\" + saveFileName
                        , printContent
                        , Encoding.GetEncoding("GB2312"));
                    printFiles.Add(tempPrintFilePathConfig + "/" + saveFileName);
                    continue;
                }
                ///明细数据
                DataRow[] detailTable;
                if (conditions.Length > 0)
                {
                    detailTable = dtDetail.Select(conditions.Substring(4));
                }
                else
                {
                    detailTable = new DataRow[dtDetail.Rows.Count];
                    for (int i = 0; i < dtDetail.Rows.Count; i++)
                    {
                        detailTable[i] = dtDetail.Rows[i];
                    }
                }

                ///每页最多几行明细
                int pageRowCnt = printConfigXmlDetailsInfo.MaxRow;

                int totalRowCnt = detailTable.Length;

                string detalRowTemplate = CreateHtmlDetailRowTemplate(sbTemplate.ToString());
                if (string.IsNullOrEmpty(detalRowTemplate))
                {
                    string saveFileName = printConfigXmlTableInfo.PrintFileName + "_" + DateTime.Now.Ticks + "." + templateFileType;
                    FileInfo fileInfo = new FileInfo(saveFileName);
                    File.WriteAllText(tempPrintFilePath + @"\" + saveFileName
                        , printContent
                        , Encoding.GetEncoding("GB2312"));
                    printFiles.Add(tempPrintFilePathConfig + "/" + saveFileName);
                    continue;
                }
                string emptyRowTemplate = CreateHtmlEmptyRowTemplate(detalRowTemplate);

                StringBuilder detailSb = new StringBuilder();
                ///当前页码
                int pageIndex = 0;
                while (totalRowCnt > 0)
                {
                    for (int i = 0; i < pageRowCnt; i++)
                    {
                        if (totalRowCnt > i)
                        {
                            ///
                            string detalRow = FillPrintContent(detalRowTemplate, printConfigXmlDetailInfos, detailTable[pageIndex * pageRowCnt + i], tempPrintFilePath, tempPrintFilePathConfig);
                            detailSb.Append(detalRow);
                            continue;
                        }
                        if (printConfigXmlDetailsInfo.FillEmpty)
                            detailSb.Append(emptyRowTemplate);
                    }
                    string saveFileName = printConfigXmlTableInfo.PrintFileName + "_" + pageIndex + "_" + DateTime.Now.Ticks + "." + templateFileType;
                    FileInfo fileInfo = new FileInfo(saveFileName);
                    File.WriteAllText(tempPrintFilePath + @"\" + saveFileName
                        , printContent.Replace(detalRowTemplate, detailSb.ToString())
                        , Encoding.GetEncoding("GB2312"));
                    printFiles.Add("../" + tempPrintFilePathConfig + "/" + saveFileName);
                    ///页码递增
                    pageIndex++;
                    ///
                    totalRowCnt -= pageRowCnt;
                    detailSb.Clear();
                    ///
                }
            }
            return printFiles;
        }
        /// <summary>
        /// 向模板填充打印内容
        /// </summary>
        /// <param name="templateSb"></param>
        /// <param name="drs"></param>
        /// <param name="rowsLabel"></param>
        /// <param name="isFillEmptyRows"></param>
        /// <param name="totalRowCount"></param>
        /// <param name="templateFilePath"></param>
        /// <param name="pageIndex"></param>
        private string FillPrintContent(string templateSb, List<object> printConfigXmlFieldInfos, DataRow dataRow, string tempPrintFilePath, string tempPrintFilePathConfig)
        {
            foreach (PrintConfigXmlFieldInfo field in printConfigXmlFieldInfos)
            {
                string stringFormat = field.StringFormat;
                object drValue = dataRow[field.FieldName];
                if (drValue == null) continue;
                string drValueString = drValue.ToString();
                if (string.IsNullOrEmpty(drValueString))
                {
                    templateSb = templateSb.Replace("$" + field.TemplateLabel + "$", "&nbsp;");
                    continue;
                }
                string fileName = string.Empty;
                int pngWidth = 200;
                ///需要字符格式化时
                if (!string.IsNullOrEmpty(stringFormat))
                {
                    switch (stringFormat.ToLower())
                    {
                        case "qrcode":
                            fileName = "QR_" + drValueString + "_" + DateTime.Now.Ticks + ".png";
                            BarcodePng.ConvertByteQrCodeToWriteableBitmap(drValueString, fileName, pngWidth, tempPrintFilePath);
                            drValueString = fileName;
                            break;
                        case "datamatrix":
                            fileName = "DATAMATRIX_" + drValueString + "_" + DateTime.Now.Ticks + ".png";
                            BarcodePng.ConvertByteDataMatrixToWriteableBitmap(drValueString, fileName, pngWidth, tempPrintFilePath);
                            drValueString = fileName;
                            break;
                        case "pdf417":
                            fileName = "PDF417_" + drValueString + "_" + DateTime.Now.Ticks + ".png";
                            BarcodePng.ConvertBytePdf417ToWriteableBitmap(drValueString, fileName, pngWidth, tempPrintFilePath);
                            drValueString = fileName;
                            break;
                        case "code39":
                            fileName = "CODE39_" + drValueString + "_" + DateTime.Now.Ticks + ".png";
                            BarcodePng.GetCode39ToBitmap(drValueString, fileName, pngWidth, tempPrintFilePath);
                            drValueString = fileName;
                            break;
                        case "code128":
                            fileName = "CODE128_" + drValueString + "_" + DateTime.Now.Ticks + ".png";
                            BarcodePng.GetCode128ToBitmap(drValueString, fileName, pngWidth, tempPrintFilePath);
                            drValueString = fileName;
                            break;
                    }
                    if (!string.IsNullOrEmpty(field.DataType))
                    {
                        switch (field.DataType.ToLower())
                        {
                            case "datetime": drValueString = DateTime.Parse(drValueString).ToString(stringFormat); break;
                            case "decimal": drValueString = decimal.Parse(drValueString).ToString(stringFormat); break;
                        }
                    }
                }
                templateSb = templateSb.Replace("$" + field.TemplateLabel + "$", drValueString);
            }
            return templateSb;
        }

        private string CreateHtmlDetailRowTemplate(string templateSb)
        {
            Regex reg = new Regex(@"(?is)<div\s+id=""details"">(?><div[^>]*>(?<o>)|</div>(?<-o>)|(?:(?!</?div\b).)*)*(?(o)(?!))</div>");
            Match m = reg.Match(templateSb);
            if (!m.Success) return string.Empty;
            int divIndex = m.Value.LastIndexOf("</div>");
            return m.Value.Substring(0, divIndex).Replace("<div id=\"details\">", string.Empty).Trim();
        }

        private string CreateHtmlEmptyRowTemplate(string detalRowTemplate)
        {
            int divIndex = 0;
            string emptyDetailTemplate = detalRowTemplate;
            while (divIndex > -1)
            {
                divIndex = emptyDetailTemplate.IndexOf("$");
                if (divIndex == -1) break;
                string prevString = emptyDetailTemplate.Substring(0, divIndex);
                string nextString = emptyDetailTemplate.Substring(divIndex + 1);
                divIndex = nextString.IndexOf("$");
                if (divIndex == -1) break;
                emptyDetailTemplate = prevString + "&nbsp;" + nextString.Substring(divIndex + 1);
            }
            return emptyDetailTemplate;
        }

        #region 新扩展方法
        public string CreatePrintFileByPageEntity(string templateFileName, string configFileName, string templateFileType, DataSet dataSet, string tempPrintFilePath, string tempPrintFilePathConfig)
        {
            StringBuilder sbTemplate = new StringBuilder();
            StringBuilder tempBuilder = new StringBuilder();
            using (StreamReader sr = new StreamReader(templateFileName, Encoding.Default))
            {
                sbTemplate.Append(sr.ReadToEnd());
            }
            ///字段匹配配置文件格式：
            XmlWrapper xmlWrapper = new XmlWrapper(configFileName, LoadType.FromFile);
            ///主表配置信息
            PrintConfigXmlTableInfo printConfigXmlTableInfo = xmlWrapper.XmlToObject("/Table", typeof(PrintConfigXmlTableInfo)) as PrintConfigXmlTableInfo;
            ///主表配置字段信息
            List<object> printConfigXmlFieldInfos = xmlWrapper.XmlToList("/Table/Field", typeof(PrintConfigXmlFieldInfo));
            ///明细行配置
            PrintConfigXmlDetailsInfo printConfigXmlDetailsInfo = xmlWrapper.XmlToObject("/Table/Details", typeof(PrintConfigXmlDetailsInfo)) as PrintConfigXmlDetailsInfo;
            ///明细行字段
            List<object> printConfigXmlDetailInfos = xmlWrapper.XmlToList("/Table/Details/Field", typeof(PrintConfigXmlFieldInfo));
            DataTable dataTable = dataSet.Tables[printConfigXmlTableInfo.TableName];
            if (dataTable == null && dataSet.Tables.Count > 0) dataTable = dataSet.Tables[0];
            string printFile = string.Empty;
            if (dataTable == null) return printFile;
            if (dataTable.Rows.Count == 0) return printFile;

            #region 变量初始化
            string saveFileName = string.Empty;
            StringBuilder bodymainBuilder = new StringBuilder();
            int IsEspecial = printConfigXmlTableInfo.IsEspecial;
            int MaxRow;
            bool FillEmpty = false;
            #endregion

            if (IsEspecial == 0)
            {
                MaxRow = printConfigXmlDetailsInfo.MaxRow;
                FillEmpty = printConfigXmlDetailsInfo.FillEmpty;

                string pagetemplate = string.Empty;

                foreach (DataRow dr in dataTable.Rows)
                {
                    #region 获取明细数据源根据配置关联字段
                    string conditions = string.Empty;
                    conditions = "1 = 1";
                    string[] relationFields = printConfigXmlDetailsInfo.RelationFields.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    string[] valueStr1 = relationFields[0].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] valueStr2 = relationFields[1].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < valueStr1.Length; i++)
                    {
                        conditions += "AND " + valueStr1[i] + " = '" + dr[valueStr2[i]].ToString() + "'";
                    }

                    DataRow[] dtDetail = dataSet.Tables[1].Select(conditions);
                    #endregion

                    int allrows = dtDetail.Length;//获取总行数
                    int allpages = (int)Math.Ceiling(decimal.Parse(allrows.ToString()) / decimal.Parse(MaxRow.ToString())); //获取总页数
                    int emptycount = allpages * MaxRow - allrows;//需要补空白行数
                    string mainStr = CreateHtmlMaindivTemplate(sbTemplate.ToString());
                    pagetemplate = sbTemplate.ToString().Replace(mainStr, "<!--replacement_markers_allpages-->");//整个页面填充标记
                    string detailsStr = CreateHtmlDetailsTemplate(sbTemplate.ToString());
                    string tabfirstStr = mainStr.Replace(detailsStr, "<!--replacement_markers_first-->");//传入主数据填充 <!--replacement_markers_first--> => detailsStr
                    string detailRowStr = CreateHtmlDetailTemplate(sbTemplate.ToString());//传入明细填充数据
                    string printContent = FillPrintContent(tabfirstStr, printConfigXmlFieldInfos, dr, tempPrintFilePath, tempPrintFilePathConfig);///主表填充
                    printContent = printContent.Replace(@"$PageCount$", allpages.ToString());
                    string tabsecondStr = detailsStr.Replace(detailRowStr, "<!--replacement_markers_second-->");// <!--replacement_markers_second--> => detailRowStr
                    if (allrows == 0)
                    {
                        string printContentnew = printContent.Replace(@"$PageThis$", "1");
                        if (FillEmpty)
                        {
                            for (int i = 0; i < MaxRow; i++)
                            {
                                string emptyRowTemplate = CreateHtmlEmptyRowTemplate(detailRowStr);
                                tempBuilder.AppendLine(emptyRowTemplate);
                            }
                        }
                        string alldetailesStr = tabsecondStr.Replace("<!--replacement_markers_second-->", tempBuilder.ToString());
                        string pagemiandivStr = printContentnew.Replace("<!--replacement_markers_first-->", alldetailesStr);
                        bodymainBuilder.AppendLine(pagemiandivStr);
                        tempBuilder.Clear();
                    }

                    int sIndex = 0;
                    int thispage = 1;
                    foreach (DataRow drDetails in dtDetail)
                    {
                        string rowTemplate = detailRowStr;
                        string detalRow = FillPrintContent(rowTemplate, printConfigXmlDetailInfos, drDetails, tempPrintFilePath, tempPrintFilePathConfig);
                        detalRow = detalRow.Replace("$RowId$", (sIndex + 1).ToString());
                        tempBuilder.AppendLine(detalRow);

                        if (allrows > 1)
                        {
                            if ((sIndex + 1) % MaxRow == 0)
                            {
                                string printContentnew = printContent.Replace(@"$PageThis$", thispage.ToString());
                                thispage = thispage + 1;
                                string alldetailesStr = tabsecondStr.Replace("<!--replacement_markers_second-->", tempBuilder.ToString());
                                string pagemiandivStr = printContentnew.Replace("<!--replacement_markers_first-->", alldetailesStr);
                                bodymainBuilder.AppendLine(pagemiandivStr);
                                bodymainBuilder.AppendLine("<div class=\"PageNext\"></div>");
                                tempBuilder.Clear();
                            }
                        }
                        else
                        {
                            string printContentnew = printContent.Replace(@"$PageThis$", thispage.ToString());
                            if (FillEmpty)
                            {
                                for (int i = 0; i < emptycount; i++)
                                {
                                    string emptyRowTemplate = CreateHtmlEmptyRowTemplate(detailRowStr);
                                    tempBuilder.AppendLine(emptyRowTemplate);
                                }
                            }
                            string alldetailesStr = tabsecondStr.Replace("<!--replacement_markers_second-->", tempBuilder.ToString());
                            string pagemiandivStr = printContentnew.Replace("<!--replacement_markers_first-->", alldetailesStr);
                            bodymainBuilder.AppendLine(pagemiandivStr);
                            tempBuilder.Clear();
                        }

                        sIndex++;
                    }
                    if (tempBuilder.Length > 0)
                    {
                        if (FillEmpty)
                        {
                            for (int i = 0; i < emptycount; i++)
                            {
                                string emptyRowTemplate = CreateHtmlEmptyRowTemplate(detailRowStr);
                                tempBuilder.AppendLine(emptyRowTemplate);
                            }
                        }

                        string alldetailesStr = tabsecondStr.Replace("<!--replacement_markers_second-->", tempBuilder.ToString());
                        printContent = printContent.Replace(@"$PageThis$", thispage.ToString());
                        string pagemiandivStr = printContent.Replace("<!--replacement_markers_first-->", alldetailesStr);
                        bodymainBuilder.AppendLine(pagemiandivStr);
                        bodymainBuilder.AppendLine("<div class=\"PageNext\"></div>");
                        tempBuilder.Clear();
                    }
                }
                pagetemplate = pagetemplate.Replace("<!--replacement_markers_allpages-->", bodymainBuilder.ToString());

                saveFileName = printConfigXmlTableInfo.PrintFileName + "_" + DateTime.Now.Ticks + "." + templateFileType;
                FileInfo fileInfo = new FileInfo(saveFileName);
                File.WriteAllText(tempPrintFilePath + @"\" + saveFileName
                    , pagetemplate.ToString()
                    , Encoding.GetEncoding("GB2312"));
                return "../" + tempPrintFilePathConfig + "/" + saveFileName;
            }
            else if (IsEspecial == 1)
            {
                #region 单Table实体支持一页打印多个
                FillEmpty = printConfigXmlDetailsInfo.FillEmpty;
                MaxRow = printConfigXmlDetailsInfo.MaxRow;

                int allrows = dataTable.Rows.Count;//获取总行数
                int allpages = (int)Math.Ceiling(Convert.ToDecimal((allrows / MaxRow).ToString())); //获取总页数

                int i = 0;
                foreach (DataRow dr in dataTable.Rows)
                {
                    string detalsTemplate = CreateHtmlDetailsTemplate(sbTemplate.ToString());
                    string detalRowTemplate = CreateHtmlDetailRowTemplate(sbTemplate.ToString());
                    string detalRow = FillPrintContent(detalRowTemplate, printConfigXmlDetailInfos, dataTable.Rows[i], tempPrintFilePath, tempPrintFilePathConfig);
                    detalRow = detalRow.Replace(@"$PageCount$", allrows.ToString());
                    detalRow = detalRow.Replace(@"$PageThis$", (i + 1).ToString());
                    tempBuilder.AppendLine(detalRow);

                    if (allrows > 1)
                    {
                        if ((i + 1) % MaxRow == 0)
                        {
                            detalsTemplate = detalsTemplate.Replace(detalRowTemplate, tempBuilder.ToString());
                            bodymainBuilder.AppendLine(detalsTemplate);
                            bodymainBuilder.AppendLine("<div class=\"PageNext\"></div>");
                            tempBuilder.Clear();
                        }
                    }
                    else
                    {
                        detalsTemplate = detalsTemplate.Replace(detalRowTemplate, tempBuilder.ToString());
                        bodymainBuilder.AppendLine(detalsTemplate);
                        tempBuilder.Clear();
                    }

                    i++;
                }
                if (tempBuilder.Length > 0)
                {
                    string detalsTemplate = CreateHtmlDetailsTemplate(sbTemplate.ToString());
                    string detalRowTemplate = CreateHtmlDetailRowTemplate(sbTemplate.ToString());
                    detalsTemplate = detalsTemplate.Replace(detalRowTemplate, tempBuilder.ToString());
                    bodymainBuilder.AppendLine(detalsTemplate);
                    tempBuilder.Clear();
                }
                sbTemplate = sbTemplate.Replace(CreateHtmlDetailsTemplate(sbTemplate.ToString()), bodymainBuilder.ToString());
                saveFileName = printConfigXmlTableInfo.PrintFileName + "_" + DateTime.Now.Ticks + "." + templateFileType;
                FileInfo fileInfo = new FileInfo(saveFileName);
                File.WriteAllText(tempPrintFilePath + @"\" + saveFileName
                    , sbTemplate.ToString()
                    , Encoding.GetEncoding("GB2312"));
                return "../" + tempPrintFilePathConfig + "/" + saveFileName;
                #endregion
            }
            return saveFileName;
        }

        private string CreateHtmlDetailsTemplate(string templateSb)
        {
            Regex reg = new Regex(@"(?is)<div\s+id=""details"">(?><div[^>]*>(?<o>)|</div>(?<-o>)|(?:(?!</?div\b).)*)*(?(o)(?!))</div>");
            Match m = reg.Match(templateSb);
            if (!m.Success) return string.Empty;
            return m.Value;
        }
        private string CreateHtmlMaindivTemplate(string templateSb)
        {
            Regex reg = new Regex(@"(?is)<div\s+id=""maindiv"">(?><div[^>]*>(?<o>)|</div>(?<-o>)|(?:(?!</?div\b).)*)*(?(o)(?!))</div>");
            Match m = reg.Match(templateSb);
            if (!m.Success) return string.Empty;
            return m.Value;
        }
        private string CreateHtmlDetailTemplate(string templateSb)
        {
            Regex reg = new Regex(@"(?is)<div\s+id=""detail"">(?><div[^>]*>(?<o>)|</div>(?<-o>)|(?:(?!</?div\b).)*)*(?(o)(?!))</div>");
            Match m = reg.Match(templateSb);
            if (!m.Success) return string.Empty;
            return m.Value;
        }
        #endregion
    }
}
