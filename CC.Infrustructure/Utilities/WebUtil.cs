#region Declaim

//---------------------------------------------------------------------------
// Name:		WebUtil.cs
// Function:	TODO: describe
// Author:		
// Date:    	
//---------------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

using System.Web.UI;
using System.Text.RegularExpressions;
using System.Collections;
using OfficeOpenXml.Style;
using Infrustructure.Customization;
using OfficeOpenXml;
using System.Drawing;
using Infrustructure.BaseClass;
using System.Linq;

#endregion

namespace Infrustructure.Utilities
{
    public static class WebUtil
    {
        #region const
        const string RTPMS_HEAD = @"��������,�������,�������,������,���,���ϵ���,��Ӧ������,��Ӧ�� DUNS,������,��װ���,�������,��װ����,ʵ�ʽ���ʱ��,���ݱ�ʶ,��������ʱ��,WM���";
        #endregion

        static Regex LinkParser = new Regex(@"<a[^>]+href=\s*(?:'(?<href>[^']+)'|""(?<href>[^""]+)""|(?<href>[^>\s]+))\s*[^>]*>(?<text>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static bool Export(GridView gridView, ref string filePath, out string message)
        {
            return Export2CSV(true, gridView, false, ref filePath, out message);
        }

        public static bool Export(GridView gridView, bool allowInvisible, ref string filePath, out string message)
        {
            return Export2CSV(true, gridView, allowInvisible, ref filePath, out message);
        }

        public static bool Export(IList infos, ref string filePath, out string message)
        {
            return Export2CSV(infos, ref filePath, out message);
        }

        public static bool Export(bool allowEmpty, GridView gridView, ref string filePath, out string message)
        {
            //return Export2CSV(allowEmpty, gridView, false, ref filePath, out message);
            return Export2Excel(allowEmpty, gridView, false, ref filePath, out message);
        }

        public static bool Export(bool allowEmpty, GridView gridView, bool allowInvisible, ref string filePath, out string message)
        {
            return Export2CSV(allowEmpty, gridView, allowInvisible, ref filePath, out message);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static bool Export2CSV(bool allowEmpty, GridView gridView, bool allowInvisible, ref string filePath, out string message)
        {
            message = string.Empty;
            if (gridView == null)
            {
                message = "û�ҵ��б����";
                return false;
            }
            if (gridView.DataSource == null && string.IsNullOrEmpty(gridView.DataSourceID))
            {
                message = "���ȵ����ѯ��ť����ִ�е�������";
                return false;
            }

            bool originalPaging = gridView.AllowPaging;
            int originalPageSize = gridView.PageSize;
            int originalPageIndex = gridView.PageIndex;

            gridView.AllowPaging = true;
            int count = (gridView.PageCount == 0) ? 1 : gridView.PageCount;
            gridView.PageSize = gridView.PageSize * count;
            // in case originally the gridview is not allow paging so may be lost the rows data.
            if (gridView.PageSize < gridView.Rows.Count)
                gridView.PageSize = gridView.Rows.Count;
            gridView.PageIndex = 0;

            if (gridView.PageSize > 5000)
            {
                message = "����������5000, Ϊ����ϵͳ����, ����С���ݷ�Χ�ٵ���";
                gridView.AllowPaging = originalPaging;
                gridView.PageSize = originalPageSize;
                gridView.PageIndex = originalPageIndex;

                return false;
            }

            try
            {
                gridView.DataBind();
            }
            catch (System.Exception ex)
            {
                message = "���ݰ�ʧ��. ��ϸ��Ϣ:" + ex.Message;
                return false;
            }

            gridView.AllowPaging = originalPaging;
            gridView.PageSize = originalPageSize;
            gridView.PageIndex = originalPageIndex;

            if (!allowEmpty && gridView.Rows.Count == 0)
            {
                message = "û����ؼ�¼";
                return false;
            }

            filePath = string.Format("{0}_{1}.csv", filePath, DateTime.Now.ToString("yyyyMMddHHmmssFFF"));
            string fullpath = Path.Combine(GetExportFolder(), filePath);
            using (StreamWriter writer = new StreamWriter(new FileStream(fullpath, FileMode.Create), Encoding.Unicode))
            {
                List<int> skipIndexes;
                List<int> templateIndexes;
                // write column header
                WriteCSVHeaderLine(gridView, writer, allowInvisible, out skipIndexes, out templateIndexes);

                // write content
                foreach (GridViewRow item in gridView.Rows)
                {
                    WriteCSVContentLine(item, writer, skipIndexes, templateIndexes);
                }

                writer.Close();
            }
            filePath = GetDownloadPath(fullpath);
            return true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static bool Export2CSV(IList infos, ref string filePath, out string message)
        {
            message = string.Empty;
            if (infos == null)
            {
                message = "û�ҵ��б����";
                return false;
            }
            if (infos.Count <= 0)
            {
                message = "�赼����¼Ϊ��";
                return false;
            }

            filePath = string.Format("{0}.csv", filePath);
            string fullpath = Path.Combine(GetExportFolder(), filePath);
            using (StreamWriter writer = new StreamWriter(new FileStream(fullpath, FileMode.Create), Encoding.Unicode))
            {
                // write csv
                WriteCSVLine(infos, writer);

                writer.Close();
            }
            filePath = GetDownloadPath(fullpath);
            return true;
        }

        public static string GetExportFolder()
        {
            string root = HttpContext.Current.Server.MapPath("~/");
            string tmpFolder = Path.Combine(root, "_tmpFiles");
            if (!Directory.Exists(tmpFolder))
                Directory.CreateDirectory(tmpFolder);
            return tmpFolder;
        }

        public static string GetDownloadPath(string fullpath)
        {
            //string filePath = "/" + fullpath.Substring(HttpContext.Current.Server.MapPath("~/").Length).Replace('\\', '/');
            string filePath = "../" + fullpath.Substring(HttpContext.Current.Server.MapPath("~/").Length).Replace('\\', '/');
            return filePath;
        }



        private static void WriteCSVLine(IList infos, TextWriter writer)
        {
            //int i = 0;
            string head = string.Empty;
            string contents = string.Empty;

            //write head
            writer.Write(RTPMS_HEAD);
            writer.WriteLine();

            foreach (object ob in infos)
            {
                contents = string.Empty;
                System.Reflection.PropertyInfo[] pis = ob.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo pi in pis)
                {
                    //if(i==0)
                    //    head += string.Format("{0},", pi.Name);
                    contents += string.Format("{0},", ob.GetType().GetProperty(pi.Name).GetValue(ob, null).ToString());
                }
                //if (i == 0)
                //{
                //    writer.Write(head.TrimEnd(','));
                //    writer.WriteLine();
                //}

                writer.Write(contents.TrimEnd(','));
                writer.WriteLine();

                //i++;
            }
        }


        private static void WriteCSVHeaderLine(GridView gridView, TextWriter writer, bool allowInvisible, out List<int> skipIndexes, out List<int> templateIndexes)
        {
            skipIndexes = new List<int>();
            templateIndexes = new List<int>();
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                if (!gridView.Columns[i].Visible && !allowInvisible)
                {
                    skipIndexes.Add(i);
                    continue;
                }
                string text = StringUtil.GetDelimitedReadyString(gridView.Columns[i].HeaderText.Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                if (i == gridView.Columns.Count - 1)
                    text = text.TrimEnd('\t');

                if (string.IsNullOrEmpty(text) ||
                    text == "\t" ||
                    text == "�޸�" ||
                    text == "�޸�\t" ||
                    text == "ȡ��" ||
                    text == "ȡ��\t" ||
                    text == "ɾ��" ||
                    text == "ɾ��\t" ||
                    text == "�鿴" ||
                    text == "�鿴\t" ||
                #region Added by ���Ѿ��������ݴ�����в�������
 text == "���ݴ���" ||
                    text == "���ݴ���\t" ||
                #endregion
 text == "����" ||
                    text == "����\t" ||
                        text == "���" ||
                        text == "���\t" ||
                        text == "�༭" ||
                        text == "�༭\t" ||
                        text == "����" ||
                        text == "����\t" ||
                        text == "����" ||
                        text == "����\t" ||
                        text == "�ֹ��嵥" ||
                        text == "�ֹ��嵥\t" ||
                        text == "��ϸ" ||
                        text == "��ϸ\t")
                    skipIndexes.Add(i);
                else
                {
                    if (gridView.Columns[i] is TemplateField)
                        templateIndexes.Add(i);
                    if (!string.IsNullOrEmpty(text) && text != "\t")
                        text = HttpUtility.HtmlDecode(text);
                    writer.Write(text);
                }
            }
            writer.WriteLine();
        }

        private static void WriteCSVContentLine(GridViewRow row, TextWriter writer, List<int> skipIndexes, List<int> templateIndexes)
        {
            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (skipIndexes.Contains(i))
                    continue;
                string text = "\t";
                if (templateIndexes.Contains(i))
                {
                    foreach (Control con in row.Cells[i].Controls)
                    {
                        if (con is HyperLink)
                        {
                            // hyperlink
                            text = ((HyperLink)con).Text.Trim();
                            text = StringUtil.GetDelimitedReadyString(text.Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                            break;
                        }
                        if (con is Label)
                        {
                            text = StringUtil.GetDelimitedReadyString(((Label)con).Text.Trim().Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                            break;
                        }
                        if (con is DataBoundLiteralControl)
                        {
                            // asp:TemplateField
                            DataBoundLiteralControl con1 = (DataBoundLiteralControl)con;

                            text = con1.Text.Trim().Trim('\r', '\n');
                            MatchCollection mc = LinkParser.Matches(text);
                            if (mc.Count > 0)
                            {
                                text = mc[0].Groups["text"].Value;
                            }
                            text = StringUtil.GetDelimitedReadyString(text.Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                            break;
                        }
                    }
                }
                else
                    text = StringUtil.GetDelimitedReadyString(row.Cells[i].Text.Trim().Trim('\r', '\n').Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');

                if (!string.IsNullOrEmpty(text) && text != "\t")
                    text = HttpUtility.HtmlDecode(text);
                writer.Write(text);
            }

            writer.WriteLine();
        }


        public static bool GetImportedDataTable(FileUpload fuFileUpload, string uploadFolder, out string message,
                                                out DataTable dt)
        {
            return GetImportedDataTable(fuFileUpload, uploadFolder, out message, out dt, new Hashtable());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fuFileUpload"></param>
        /// <param name="uploadFolder"></param>
        /// <param name="message"></param>
        /// <param name="dt"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        //[Obsolete("�������ã�Excel�ĵط���������")]
        public static bool GetImportedDataTable(FileUpload fuFileUpload, string uploadFolder, out string message,
                                                out DataTable dt, Hashtable param)
        {
            dt = new DataTable();
            if (fuFileUpload.PostedFile == null || string.IsNullOrEmpty(fuFileUpload.PostedFile.FileName))
            {
                message = "��ѡ���ϴ��ļ�";
                return false;
            }

            const int maxLength = 1024 * 1024 * 4; //���4M�������ڴ�ռ��̫����,��Ҫ���û��Լ�����ļ�
            if (fuFileUpload.FileBytes.Length <= 0 || fuFileUpload.FileBytes.Length > maxLength)
            {
                message = "�ϴ��ļ���СӦ��4M��";
                return false;
            }

            string mime = fuFileUpload.PostedFile.ContentType;
            //text/csv
            if (!mime.Equals("text/csv", StringComparison.InvariantCultureIgnoreCase) &&
                !mime.Equals("application/vnd.ms-excel", StringComparison.InvariantCultureIgnoreCase) &&
                !fuFileUpload.PostedFile.FileName.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase))
            {
                message = string.Format("�ϴ��ļ��ĸ�ʽ'{0}'�޷�ʶ��, �����ļ����ڱ�ʹ��.", mime);
                return false;
            }

            string filename = fuFileUpload.PostedFile.FileName;
            filename = filename.Substring(filename.LastIndexOf('\\') + 1);


            //���ϴ����ļ����浽����������
            string tempFileName = Path.Combine(uploadFolder, "temp_" + DateTime.Now.ToFileTime() + filename);
            fuFileUpload.SaveAs(tempFileName);

            if (tempFileName.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase))
                return new LESDataTransfer().ImportCSVData(tempFileName, out dt, out message, param);
            //try
            //{
            //    return new LESDataTransfer().ImportExcelData(tempFileName, "", out dt, out message, param);
            //}
            //catch (InvalidDataException)
            //{
            //    // it is vary possibly that csv data but filed end with xls. so retry in parsing with csv format
            return new LESDataTransfer().ImportCSVData(tempFileName, out dt, out message, param);
            //}
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static bool Export2Excel(bool allowEmpty, GridView gridView, bool allowInvisible, ref string filePath, out string message)
        {
            message = string.Empty;
            if (gridView == null)
            {
                message = "û�ҵ��б����";
                return false;
            }
            if (gridView.DataSource == null && string.IsNullOrEmpty(gridView.DataSourceID))
            {
                message = "���ȵ����ѯ��ť����ִ�е�������";
                return false;
            }

            bool originalPaging = gridView.AllowPaging;
            int originalPageSize = gridView.PageSize;
            int originalPageIndex = gridView.PageIndex;

            gridView.AllowPaging = true;
            int count = (gridView.PageCount == 0) ? 1 : gridView.PageCount;
            gridView.PageSize = gridView.PageSize * count;
            // in case originally the gridview is not allow paging so may be lost the rows data.
            if (gridView.PageSize < gridView.Rows.Count)
                gridView.PageSize = gridView.Rows.Count;
            gridView.PageIndex = 0;

            if (gridView.PageSize > 7000)
            {
                message = "����������7000, Ϊ����ϵͳ����, ����С���ݷ�Χ�ٵ���";
                gridView.AllowPaging = originalPaging;
                gridView.PageSize = originalPageSize;
                gridView.PageIndex = originalPageIndex;

                return false;
            }

            try
            {
                gridView.DataBind();
            }
            catch (System.Exception ex)
            {
                message = "���ݰ�ʧ��. ��ϸ��Ϣ:" + ex.Message;
                return false;
            }

            gridView.AllowPaging = originalPaging;
            gridView.PageSize = originalPageSize;
            gridView.PageIndex = originalPageIndex;

            if (!allowEmpty && gridView.Rows.Count == 0)
            {
                message = "û����ؼ�¼";
                return false;
            }

            filePath = string.Format("{0}_{1}.xlsx", filePath, DateTime.Now.ToString("yyyyMMddHHmmssFFF"));
            string fullpath = Path.Combine(GetExportFolder(), filePath);
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullpath)))
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                int rowFlag = 1;
                int columnFlag = 1;

                //write header
                var skipIndexes = new List<int>();
                var templateIndexes = new List<int>();
                #region HEADER
                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    if (!gridView.Columns[i].Visible && !allowInvisible)
                    {
                        skipIndexes.Add(i);
                        continue;
                    }
                    string text = StringUtil.GetDelimitedReadyString(gridView.Columns[i].HeaderText.Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                    if (i == gridView.Columns.Count - 1)
                        text = text.TrimEnd('\t');

                    if (string.IsNullOrEmpty(text) ||
                        text == "\t" ||
                        text == "�޸�" ||
                        text == "�޸�\t" ||
                        text == "ȡ��" ||
                        text == "ȡ��\t" ||
                        text == "ɾ��" ||
                        text == "ɾ��\t" ||
                        text == "�鿴" ||
                        text == "�鿴\t" ||
                    #region Added by ���Ѿ��������ݴ�����в�������
 text == "���ݴ���" ||
                        text == "���ݴ���\t" ||
                    #endregion
 text == "����" ||
                        text == "����\t" ||
                        text == "���" ||
                        text == "���\t" ||
                        text == "�༭" ||
                        text == "�༭\t" ||
                        text == "����" ||
                        text == "����\t" ||
                        text == "����" ||
                        text == "����\t" ||
                        text == "�ֹ��嵥" ||
                        text == "�ֹ��嵥\t" ||
                        text == "��ϸ" ||
                        text == "��ϸ\t")
                        skipIndexes.Add(i);
                    else
                    {
                        if (gridView.Columns[i] is TemplateField)
                            templateIndexes.Add(i);
                        if (!string.IsNullOrEmpty(text) && text != "\t")
                            text = HttpUtility.HtmlDecode(text);
                        worksheet.Cells[rowFlag, columnFlag].Value = text;

                        columnFlag++;
                    }
                }
                #endregion

                var columnCount = columnFlag - 1;
                rowFlag = 2;

                #region content
                foreach (GridViewRow row in gridView.Rows)
                {
                    columnFlag = 1;
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (skipIndexes.Contains(i))
                            continue;
                        string text = "\t";
                        if (templateIndexes.Contains(i))
                        {
                            if (row.Cells[i].Controls.Count > 0)
                            {
                                text = getControlText(row.Cells[i].Controls);
                            }
                            else
                            {
                                text = StringUtil.GetDelimitedReadyString(row.Cells[i].Text.Trim().Trim('\r', '\n').Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                            }
                        }
                        else
                        {
                            text = StringUtil.GetDelimitedReadyString(row.Cells[i].Text.Trim().Trim('\r', '\n').Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                            //text =
                            //    text.Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").
                            //        Replace("<br/>", "\\n").Replace("<br />", "\\n");
                        }
                        if (!string.IsNullOrEmpty(text) && text != "\t")
                            text = HttpUtility.HtmlDecode(text);

                        worksheet.Cells[rowFlag, columnFlag].Value = text;
                        worksheet.Cells[rowFlag, columnFlag].Style.Font.Color.SetColor(row.Cells[i].ForeColor);
                        //worksheet.Cells[rowFlag, columnFlag].Style.Numberformat.Format = "@";
                        columnFlag++;
                    }
                    rowFlag++;
                }
                #endregion
                //worksheet.Cells["A1:" + worksheet.Cells[1, columnCount].Address].AutoFilter = true;

                for (int i = 1; i <= columnCount; i++)
                {
                    worksheet.Column(i).AutoFit();
                }
                //worksheet.Cells.Style.Numberformat.Format = "@";
                //format
                using (ExcelRange r = worksheet.Cells["A1:" + worksheet.Cells[1, columnCount].Address])
                {
                    r.AutoFilter = true;
                    r.Style.Font.SetFromFont(new Font("΢���ź�", 10, FontStyle.Bold));
                    r.Style.Font.Color.SetColor(Color.Black);
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                }
                using (var range = worksheet.Cells["A2:" + worksheet.Cells[rowFlag, columnCount].Address])
                {
                    range.Style.Font.SetFromFont(new Font("΢���ź�", 10, FontStyle.Regular));
                }
                excelPackage.Save();
            }

            //filePath = GetDownloadPath(fullpath);
            return true;
        }

        public static bool Export2ExcelFromDataTable(bool allowEmpty, DataTable dataTable, ref string filePath, out string message)
        {
            message = string.Empty;
            if (dataTable == null)
            {
                message = "û�ҵ��б����";
                return false;
            }

            if (dataTable.Rows.Count > 7000)
            {
                message = "����������7000, Ϊ����ϵͳ����, ����С���ݷ�Χ�ٵ���";
                return false;
            }

            if (!allowEmpty && dataTable.Rows.Count == 0)
            {
                message = "û����ؼ�¼";
                return false;
            }

            filePath = string.Format("{0}_{1}.xlsx", filePath, DateTime.Now.ToString("yyyyMMddHHmmssFFF"));
            string fullpath = Path.Combine(GetExportFolder(), filePath);
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(fullpath)))
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                int rowFlag = 1;
                int columnFlag = 1;

                //write header
                var skipIndexes = new List<int>();
                var templateIndexes = new List<int>();
                #region HEADER
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string text = StringUtil.GetDelimitedReadyString(dataTable.Columns[i].ColumnName.Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                    if (i == dataTable.Columns.Count - 1)
                        text = text.TrimEnd('\t');

                    if (string.IsNullOrEmpty(text) ||
                        text == "\t" ||
                        text == "�޸�" ||
                        text == "�޸�\t" ||
                        text == "ȡ��" ||
                        text == "ȡ��\t" ||
                        text == "ɾ��" ||
                        text == "ɾ��\t" ||
                        text == "�鿴" ||
                        text == "�鿴\t" ||
                    #region Added by ���Ѿ��������ݴ�����в�������
 text == "���ݴ���" ||
                        text == "���ݴ���\t" ||
                    #endregion
 text == "����" ||
                        text == "����\t" ||
                        text == "���" ||
                        text == "���\t" ||
                        text == "�༭" ||
                        text == "�༭\t" ||
                        text == "����" ||
                        text == "����\t" ||
                        text == "����" ||
                        text == "����\t" ||
                        text == "�ֹ��嵥" ||
                        text == "�ֹ��嵥\t" ||
                        text == "��ϸ" ||
                        text == "��ϸ\t")
                        skipIndexes.Add(i);
                    else
                    {
                        if (!string.IsNullOrEmpty(text) && text != "\t")
                            text = HttpUtility.HtmlDecode(text);
                        worksheet.Cells[rowFlag, columnFlag].Value = text;

                        columnFlag++;
                    }
                }
                #endregion

                var columnCount = columnFlag - 1;
                rowFlag = 2;

                #region content
                foreach (DataRow row in dataTable.Rows)
                {
                    columnFlag = 1;
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        if (skipIndexes.Contains(i))
                            continue;
                        string text = "\t";
                        text = StringUtil.GetDelimitedReadyString(row.ItemArray[i].ToString().Trim().Trim('\r', '\n').Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                        if (!string.IsNullOrEmpty(text) && text != "\t")
                            text = HttpUtility.HtmlDecode(text);

                        worksheet.Cells[rowFlag, columnFlag].Value = text;
                        //worksheet.Cells[rowFlag, columnFlag].Style.Font.Color.SetColor(row.Cells[i].ForeColor);
                        //worksheet.Cells[rowFlag, columnFlag].Style.Numberformat.Format = "@";
                        columnFlag++;
                    }
                    rowFlag++;
                }
                #endregion
                //worksheet.Cells["A1:" + worksheet.Cells[1, columnCount].Address].AutoFilter = true;

                for (int i = 1; i <= columnCount; i++)
                {
                    worksheet.Column(i).AutoFit();
                }
                //worksheet.Cells.Style.Numberformat.Format = "@";
                //format
                using (ExcelRange r = worksheet.Cells["A1:" + worksheet.Cells[1, columnCount].Address])
                {
                    r.AutoFilter = true;
                    r.Style.Font.SetFromFont(new Font("΢���ź�", 10, FontStyle.Bold));
                    r.Style.Font.Color.SetColor(Color.Black);
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                }
                using (var range = worksheet.Cells["A2:" + worksheet.Cells[rowFlag, columnCount].Address])
                {
                    range.Style.Font.SetFromFont(new Font("΢���ź�", 10, FontStyle.Regular));
                }
                excelPackage.Save();
            }

            //filePath = GetDownloadPath(fullpath);
            return true;
        }

        private static string getControlText(ControlCollection controls)
        {
            string text = "\t";
            foreach (Control con in controls)
            {
                if (con is System.Web.UI.HtmlControls.HtmlAnchor)
                {
                    // hyperlink
                    text = ((System.Web.UI.HtmlControls.HtmlAnchor)con).InnerText.Trim();
                    text = StringUtil.GetDelimitedReadyString(text.Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                    break;
                }
                if (con is HyperLink)
                {
                    // hyperlink
                    text = ((HyperLink)con).Text.Trim();
                    text = StringUtil.GetDelimitedReadyString(text.Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                    break;
                }
                if (con is LinkButton)
                {
                    var lbtn = con as LinkButton;
                    if (lbtn != null)
                    {
                        if (lbtn.HasControls())
                        {
                            text = getControlText(lbtn.Controls);
                            text = StringUtil.GetDelimitedReadyString(text.Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                            break;
                        }
                        else
                        {
                            text = StringUtil.GetDelimitedReadyString(lbtn.Text.Trim().Trim('\r', '\n').Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                            break;
                        }
                    }
                }
                if (con is Label)
                {
                    text = StringUtil.GetDelimitedReadyString(((Label)con).Text.Trim().Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                    break;
                }

                if (con is TextBox)
                {
                    text = StringUtil.GetDelimitedReadyString(((TextBox)con).Text.Trim().Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").Replace("<br/>", "\\n").Replace("<br />", "\\n"), '\t');
                    break;
                }
                if (con is DataBoundLiteralControl)
                {
                    // asp:TemplateField
                    DataBoundLiteralControl con1 = (DataBoundLiteralControl)con;

                    text = con1.Text.Trim().Trim('\r', '\n');
                    MatchCollection mc = LinkParser.Matches(text);
                    if (mc.Count > 0)
                    {
                        text = mc[0].Groups["text"].Value;
                    }
                    text = StringUtil.GetDelimitedReadyString(text.Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "").Replace("<br/>", "").Replace("<br />", ""), ' ');
                    text = text.Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("\"", "");
                    //text =
                    //    text.Replace("&nbsp;", "").Replace("&quot;", "\"").Replace("<br>", "\\n").
                    //        Replace("<br/>", "\\n").Replace("<br />", "\\n");
                    break;
                }
            }
            return text;
        }

        /// <summary>
        /// ����û�����URL��Ȩ��
        /// </summary>
        public static void CheckUserRequestURL(IUser user, HttpResponse sender)
        {
            // ��ȡUrl
            string url = HttpContext.Current.Request.Url.ToString();

            string url1 = url;

            url = url
                    .Replace("/WM/", "/WarehouseManagement/")
                    .Replace("/SYS/", "/SystemManagement/")
                    .Replace("/CI/", "/CustomerInterface/")
                    .Replace("/PS/", "/ProductionSchedule/")
                    .Replace("/PC/", "/ProductionControl/")
                    .Replace("/MDM/", "/MasterData/")
                    .Trim();

            //����URL�����һ��/��λ��
            int start = url.LastIndexOf('/');

            //����URL�У�λ��                 
            int end = url.IndexOf('?');
            string requestPage = null;

            //�õ��������ҳ��
            requestPage = url.Substring(start + 1, end >= 0 && end > start ? end - start - 1 : url.Length - start - 1);
            requestPage = requestPage.ToLower();

            ///ȥ��ĩβ�Ĳ���
            if (end != -1)
            {
                url = url.Substring(0, url.IndexOf('?'));
                url1 = url1.Substring(0, url1.IndexOf('?'));
            }

            //���������Դ�Ƿ���Ȩ�޷���
            if (user.CanAccessMenuURLs.Any(n => url.IndexOf(n) >= 0))
                return;

            if (user.CanAccessMenuURLs.Any(n => url1.IndexOf(n) >= 0))
                return;

            sender.Redirect("~/UnauthorizedInfo.aspx");
        }
    }
}