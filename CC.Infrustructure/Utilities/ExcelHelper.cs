using System.Data.OleDb;

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;
using Infrustructure.Logging;
using Infrustructure.Utilities.Exception;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Linq;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;

namespace Infrustructure.Utilities
{
    /**/
    /// <summary>
    /// ExcelHelper ��ժҪ˵����
    /// </summary>
	[Obsolete("Please use 'Infrustructure.Customization.LESDataTransfer' instead.")]
    public class ExcelHelper
    {
        public ExcelHelper()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /**/
        /// <summary>
        /// ���ϴ�EXCEL�ļ���ȡDATASET
        /// </summary>
        /// <param name="Path">�ļ�����</param>
        /// <returns>����һ�����ݼ�</returns>
        public static DataSet UploadExcelToDataSet(FileUpload fileUpload, string filePath, out string fileName)
        {
            string errMsg = string.Empty;
            DataSet ds = null;
            try
            {
                if (!filePath.EndsWith("\\"))
                {
                    filePath += "\\";
                }
                fileName = fileUpload.FileName.Replace(".xls", "") + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
                //fileName = fileUpload.FileName.Replace(".csv", "") + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
                string fullFileName = filePath + fileName;
                fileUpload.SaveAs(fullFileName);

                ds = ExcelToDS(fullFileName);

            }
            catch
            {
                throw (new System.Exception("�ϴ�������Excel�ļ���������"));
            }

            return ds;
        }
        /**/
        /// <summary>
        /// ��ȡExcel�ĵ�
        /// </summary>
        /// <param name="Path">�ļ�����</param>
        /// <returns>����һ�����ݼ�</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";

            string[] sheetNames = GetExcelSheetNames(Path);
            if (sheetNames.Length == 0)
            {
                throw new MPSBusinessException("��ȡExcel�ļ�ʧ��, ��ȷ�ϸ�Excel����Sheet");
                //return null;
            }
            DataSet ds = new DataSet();
            foreach (var sheetName in sheetNames)
            {
                DataTable dt = new DataTable();

                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    try
                    {
                        conn.Open();
                        OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                        oleAdapter.SelectCommand = new OleDbCommand("select * from [" + sheetName + "]", conn);
                        oleAdapter.FillSchema(dt, SchemaType.Source);
                        oleAdapter.Fill(dt);
                    }
                    catch (System.Data.OleDb.OleDbException e)
                    {
                        Logger.LogError("YF.MES.CC.Utility", "ExcelToDS", "ExcelHelper", "��ȡExcel�ļ�ʧ��", e);
                        throw new MPSBusinessException("��ȡExcel�ļ�ʧ��, ��ȷ�ϸ�Excel���ݱ�������Ϊ'Sheet1'�Ĺ���ҳ��,�Ҵ�ҳû�ж���Ŀհ���", e);
                    }
                    catch (System.Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("��ȡExcel��������" + ex.Message);
                        return null;

                    }
                }

                // remove the empty rows
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    bool hasValue = false;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j] != null && dt.Rows[i][j] != DBNull.Value)
                            hasValue = true;
                    }
                    if (!hasValue)
                        dt.Rows[i].Delete();
                }
                dt.AcceptChanges();

                ds.Tables.Add(dt);
            }
            return ds;
        }

        /// <summary>
        /// ��ȡEXCEL��sheet�б�
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static String[] GetExcelSheetNames(string Path)
        {

            OleDbConnection objConn = null;

            System.Data.DataTable dt = null;

            try
            {

                String connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties=Excel 8.0;";

                objConn = new OleDbConnection(connString);

                objConn.Open();

                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);



                if (dt == null)
                {

                    return null;

                }

                String[] excelSheets = new String[dt.Rows.Count];

                int i = 0;

                foreach (DataRow row in dt.Rows)
                {

                    excelSheets[i] = row["TABLE_NAME"].ToString();

                    i++;

                }

                // Loop through all of the sheets if you want too...

                for (int j = 0; j < excelSheets.Length; j++)
                {

                    // Query each excel sheet.

                }

                return excelSheets;

            }

            catch (System.Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("��ȡExcel��������" + ex.Message);
                return null;

            }

            finally
            {

                // Clean up.

                if (objConn != null)
                {

                    objConn.Close();

                    objConn.Dispose();

                }

                if (dt != null)
                {

                    dt.Dispose();

                }

            }

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static void DataTableToExcel(System.Data.DataView dataview, string Path, Hashtable NameMap)
        {
            try
            {
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                System.Data.OleDb.OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                string strSql = string.Empty, strSql1 = string.Empty;
                int i, j;

                for (i = 0; i < dataview.Count; i++)
                {

                    strSql = "INSERT INTO [sheet1$] (";
                    strSql1 = ") values(";
                    for (j = 0; j < dataview.Table.Columns.Count; j++)
                    {
                        if (NameMap.ContainsKey(dataview.Table.Columns[j].ColumnName))
                        {
                            strSql += NameMap[dataview.Table.Columns[j].ColumnName] + ",";  //2414210
                            strSql1 += "'" + dataview[i][j].ToString() + "',";
                        }

                    }

                    try
                    {
                        if (strSql.EndsWith(","))
                            strSql = strSql.Substring(0, strSql.Length - 1);
                        if (strSql1.EndsWith(","))
                            strSql1 = strSql1.Substring(0, strSql1.Length - 1);

                        strSql1 = strSql1 + ")";
                        strSql = strSql + strSql1;

                        cmd.CommandText = strSql;
                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("д��Excel��������" + strSql + strSql1 + ex.Message);
                        throw new System.Exception(strSql + ex.Message);
                    }
                }
                conn.Close();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                System.Diagnostics.Debug.WriteLine("д��Excel��������" + ex.Message);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static void DataTableToExcel(System.Data.DataView dataview, string Path)
        {
            try
            {
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                try
                {
                    conn.Open();
                    System.Data.OleDb.OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;

                    string strSql = string.Empty, strSql1 = string.Empty;
                    int i, j;

                    for (i = 0; i < dataview.Count; i++)
                    {

                        strSql = "INSERT INTO [sheet1$] (";
                        strSql1 = ") values(";
                        for (j = 0; j < dataview.Table.Columns.Count; j++)
                        {
                            strSql += "[" + dataview.Table.Columns[j].ColumnName + "]" + ",";
                            strSql1 += "'" + dataview[i][j].ToString() + "',";
                        }
                        //        
                        try
                        {
                            if (strSql.EndsWith(","))
                                strSql = strSql.Substring(0, strSql.Length - 1);
                            if (strSql1.EndsWith(","))
                                strSql1 = strSql1.Substring(0, strSql1.Length - 1);
                            strSql1 = strSql1 + ")";
                            strSql = strSql + strSql1;
                            cmd.CommandText = strSql;
                            cmd.ExecuteNonQuery();
                        }
                        catch (System.Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("д��Excel��������" + strSql + ex.Message);
                            throw new System.Exception(strSql + ex.Message);
                        }
                    }
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                System.Diagnostics.Debug.WriteLine("д��Excel��������" + ex.Message);
            }
        }

        /**/
        /// <summary>
        /// д��Excel�ĵ�
        /// </summary>
        /// <param name="Path">�ļ�����</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public bool SaveFP2toExcel(string Path)
        {
            try
            {
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                System.Data.OleDb.OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE [sheet1$] SET ����='2005-01-01' WHERE ����='����'";
                cmd.ExecuteNonQuery();

                conn.Close();
                return true;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                System.Diagnostics.Debug.WriteLine("д��Excel��������" + ex.Message);
            }
            return false;
        }

        #region Ѧ�������ӣ���Ҫ���ڶ�̬�е�����Bom���ɣ���������Ȼ���뵽ϵͳ

        #region �Ѷ�̬���ɵ�DataTable���ݵ�����ָ��Ŀ¼�µ�Excel,���Լ�����������,
        /// <summary>
        /// �Ѷ�̬���ɵ�DataTable���ݵ�����ָ��Ŀ¼�µ�Excel,���Լ�����������
        /// ���ݵ���Datatable�����ݼ�����������Excel�в�֧��Union all�Ĳ��뷽����
        /// ������һ���ύһ�Σ����ܲ���̫�ã����ڲ���������
        /// </summary>
        /// <param name="dt">Ҫ���������Table</param>
        /// <param name="excelPath">�ļ�·����</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static string DyDataTableToExcel(System.Data.DataTable dt, string excelPath)
        {
            if (dt == null)
            {
                return "DataTable����Ϊ��";
            }

            int rows = dt.Rows.Count;
            int cols = dt.Columns.Count;
            StringBuilder sb;

            if (rows == 0)
            {
                return "û������";
            }
            string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelPath + ";Extended Properties=Excel 8.0;";
            sb = new StringBuilder();

            //���ɴ�����Ľű�
            dt.TableName = " Sheet1 ";
            sb.Append("CREATE TABLE ");
            sb.Append(dt.TableName + " ( ");

            for (int i = 0; i < cols; i++)
            {
                if (i < cols - 1)
                    sb.Append(string.Format("{0} varchar,", "[" + dt.Columns[i].ColumnName + "]"));
                else
                    sb.Append(string.Format("{0} varchar)", "[" + dt.Columns[i].ColumnName + "]"));
            }

            using (OleDbConnection objConn = new OleDbConnection(ConnectionString))
            {
                OleDbCommand objCmd = new OleDbCommand();
                objCmd.Connection = objConn;

                objCmd.CommandText = sb.ToString();

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                }
                catch (System.Exception e)
                {
                    return "��Excel�д�����ʧ�ܣ�������Ϣ��" + e.Message;
                }


                #region ���ɲ������ݽű�Insert ���
                sb.Remove(0, sb.Length);
                sb.Append("INSERT INTO ");
                sb.Append(dt.TableName + " ( ");

                for (int i = 0; i < cols; i++)
                {
                    if (i < cols - 1)
                        sb.Append("[" + dt.Columns[i].ColumnName + "],");
                    else
                        sb.Append("[" + dt.Columns[i].ColumnName + "]) ");
                }


                #endregion


                //�������붯����Command
                string InsertSQL = sb.ToString() + " values(";

                sb.Remove(0, sb.Length);
                //����DataTable�����ݲ����½���Excel�ļ���
                //int j = 0;
                foreach (DataRow row in dt.Rows)
                {
                    sb.Remove(0, sb.Length);
                    for (int i = 0; i < cols; i++)
                    {
                        if (i < cols - 1)
                            sb.Append("'" + row[i].ToString() + "',");
                        else
                            sb.Append("'" + row[i].ToString() + "') ");
                    }
                    objCmd.CommandText = InsertSQL + sb.ToString();
                    objCmd.ExecuteNonQuery();
                }


                return "�����ѳɹ�����Excel";
            }//end using
        }
        #endregion

        #endregion

    }

    /// <summary>
    /// NpoiHelper
    /// </summary>
    public class NpoiHelper
    {

        /// <summary>
        /// DataTable 2 Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columns"></param>
        /// <param name="dicselection">codeitem</param>
        /// <param name="sheetName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool TableToExcel(DataTable dt, Dictionary<string, string> columns, Dictionary<string, Dictionary<string, string>> dicselection, string sheetName, string fileName)
        {
            ///ͨ��������ļ�������չ��
            IWorkbook workbook;
            string fileExt = Path.GetExtension(fileName).ToLower();
            if (fileExt == ".xlsx")
            { workbook = new XSSFWorkbook(); }
            else if (fileExt == ".xls")
            { workbook = new HSSFWorkbook(); }
            else { workbook = null; }
            if (workbook == null) { return false; }
            ///����SHEET
            ISheet sheet = workbook.CreateSheet(sheetName);
            ///��ͷ�ָ�
            ICellStyle headCellStyle = workbook.CreateCellStyle();
            headCellStyle.FillForegroundColor = HSSFColor.SkyBlue.Index;
            headCellStyle.FillPattern = FillPattern.SolidForeground;

            ///��ͷ  
            IRow row = sheet.CreateRow(0);
            List<string> columnNames = new List<string>(columns.Keys);
            for (int i = 0; i < columnNames.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(columns[columnNames[i]]);
                cell.CellStyle = headCellStyle;

                ///��ȡCODE_ITEM
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                if (dicselection.TryGetValue(columnNames[i], out keyValuePairs))
                {
                    string rangeName = columns[columnNames[i]];
                    ISheet iSheet = workbook.CreateSheet(rangeName);
                    List<string> codeitems = new List<string>(keyValuePairs.Keys);
                    if (codeitems.Count == 0) continue;
                    for (var j = 0; j < codeitems.Count; j++)
                    {
                        IRow cells = iSheet.CreateRow(j);
                        ///DISPLAY
                        cells.CreateCell(0).SetCellValue(keyValuePairs[codeitems[j]]);
                        ///VALUE
                        cells.CreateCell(1).SetCellValue(codeitems[j]);
                    }
                    IName range = workbook.CreateName();
                    range.RefersToFormula = string.Format("{0}!$A$1:$A${1}", rangeName, codeitems.Count);
                    range.NameName = rangeName;

                    CellRangeAddressList regions = new CellRangeAddressList(1, 65535, i, i);
                    if (fileExt == ".xlsx")
                    {
                        XSSFDataValidationHelper dvHelper = new XSSFDataValidationHelper((XSSFSheet)sheet);
                        XSSFDataValidationConstraint dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateExplicitListConstraint(keyValuePairs.Values.ToArray());
                        XSSFDataValidation dataValidate = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, regions);
                        dataValidate.SuppressDropDownArrow = true;
                        dataValidate.ShowErrorBox = true;
                        sheet.AddValidationData(dataValidate);
                    }
                    if (fileExt == ".xls")
                    {
                        DVConstraint constraint = DVConstraint.CreateFormulaListConstraint(rangeName);
                        HSSFDataValidation dataValidate = new HSSFDataValidation(regions, constraint);
                        dataValidate.SuppressDropDownArrow = true;
                        dataValidate.ShowErrorBox = true;
                        sheet.AddValidationData(dataValidate);
                    }
                }
            }
            for (int i = 0; i < dicselection.Count; i++)
            {
                workbook.SetSheetHidden(i + 1, SheetState.Hidden);
            }
            ///����  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow rowData = sheet.CreateRow(i + 1);
                for (int j = 0; j < columnNames.Count; j++)
                {
                    string tableFieldName = columnNames[j];
                    if (tableFieldName.LastIndexOf(".") > 0)
                        tableFieldName = tableFieldName.Substring(tableFieldName.LastIndexOf(".") + 1);

                    ICell cell = rowData.CreateCell(j);
                    object objValue = dt.Rows[i][tableFieldName];
                    if (objValue == null)
                    {
                        cell.SetCellValue(string.Empty);
                        continue;
                    }
                    string valueString = objValue.ToString();
                    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                    if (dicselection.TryGetValue(columnNames[j], out keyValuePairs))
                    {
                        string codeDisplay = string.Empty;
                        if (keyValuePairs.TryGetValue(valueString, out codeDisplay))
                            cell.SetCellValue(codeDisplay);
                        else
                            cell.SetCellValue(valueString);
                    }
                    else
                        cell.SetCellValue(valueString);
                }
            }
            ///����Ӧ�п�
            for (int i = 0; i < columnNames.Count; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            ///תΪ�ֽ����� 
            byte[] buf;
            using (MemoryStream stream = new MemoryStream())
            {
                workbook.Write(stream);
                buf = stream.ToArray();
            }

            ///����ΪExcel�ļ�  
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
            return true;
        }
        /// <summary>
        /// ׷�����ݵ�EXCEL��
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columns"></param>
        /// <param name="sheetName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool AppendToExcel(DataTable dt, Dictionary<string, string> columns, string sheetName, string fileName)
        {
            ///
            IWorkbook workbook;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                ///�ļ���չ��
                string fileExt = Path.GetExtension(fileName).ToLower();
                ///XSSFWorkbook ����XLSX��ʽ��HSSFWorkbook ����XLS��ʽ
                if (fileExt == ".xlsx")
                { workbook = new XSSFWorkbook(fs); }
                else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); }
                else { workbook = null; }
                if (workbook == null) { return false; }

                ISheet sheet = workbook.GetSheet(sheetName);
                ///���û�и�SHEET,�򴴽�
                if (sheet == null)
                    sheet = workbook.CreateSheet(sheetName);
                ///��ͷ  
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                ///��ͷ�ָ�
                ICellStyle headCellStyle = workbook.CreateCellStyle();
                headCellStyle.FillForegroundColor = HSSFColor.SkyBlue.Index;
                headCellStyle.FillPattern = FillPattern.SolidForeground;
                List<string> columnNames = new List<string>(columns.Keys);
                if (header == null)
                {
                    header = sheet.CreateRow(0);
                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        ICell cell = header.CreateCell(i);
                        cell.SetCellValue(columns[columnNames[i]]);
                        cell.CellStyle = headCellStyle;
                    }
                }
                ///�����ʽ����
                if (columnNames.Count != header.LastCellNum)
                    return false;
                ///��ǰ��д�������к�
                int currentLastRow = sheet.LastRowNum;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow rowData = sheet.CreateRow(currentLastRow + i + 1);
                    for (int j = 0; j < columns.Count; j++)
                    {
                        string tableFieldName = columnNames[j];
                        if (tableFieldName.LastIndexOf(".") > 0)
                            tableFieldName = tableFieldName.Substring(tableFieldName.LastIndexOf(".") + 1);

                        ICell cell = rowData.CreateCell(j);
                        object objValue = dt.Rows[i][tableFieldName];
                        if (objValue == null)
                        {
                            cell.SetCellValue(string.Empty);
                            continue;
                        }
                        string valueString = objValue.ToString();
                        cell.SetCellValue(valueString);
                    }
                }
                ///����Ӧ�п�
                for (int i = 0; i < columnNames.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }

            ///תΪ�ֽ����� 
            byte[] buf;
            using (MemoryStream stream = new MemoryStream())
            {
                workbook.Write(stream);
                buf = stream.ToArray();
            }
            ///����ΪExcel�ļ�  
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
            return true;
        }
        /// <summary>
        /// Excel�����Datable������һ��SHEET
        /// </summary>
        /// <param name="file">����·��(�����ļ�������չ��)</param>
        /// <returns></returns>
        public static DataTable ExcelToTable(string file, Dictionary<string, string> columns)
        {
            ///
            DataTable dt = new DataTable();
            ///
            IWorkbook workbook;
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                ///�ļ���չ��
                string fileExt = Path.GetExtension(file).ToLower();
                ///XSSFWorkbook ����XLSX��ʽ��HSSFWorkbook ����XLS��ʽ
                if (fileExt == ".xlsx")
                { workbook = new XSSFWorkbook(fs); }
                else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); }
                else { workbook = null; }
                if (workbook == null) { return null; }
                ISheet sheet = workbook.GetSheetAt(0);
                ///���ݿ��ֶ�
                List<string> columnNames = new List<string>(columns.Keys);

                ///��ͷ  
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                ///�����ʽ����
                if (columnNames.Count != header.LastCellNum)
                    return null;
                ///CODE����
                Dictionary<string, Dictionary<string, string>> dicselection = new Dictionary<string, Dictionary<string, string>>();
                ///CODEҳǩINDEX
                int codeSheetIndex = 1;
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    dt.Columns.Add(new DataColumn(columnNames[i]));
                    if (string.IsNullOrEmpty(columns[columnNames[i]])) continue;
                    ///��ȡCODEҳǩ
                    ISheet iSheet = workbook.GetSheetAt(codeSheetIndex);
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    ///EXCEL�е�ROW��0ΪINDEX��ʼ
                    for (int j = iSheet.FirstRowNum; j <= iSheet.LastRowNum; j++)
                    {
                        IRow cells = iSheet.GetRow(j);
                        if (cells == null) break;
                        object objValue = GetValueType(cells.GetCell(1));
                        if (objValue == null) continue;
                        object objDisplay = GetValueType(cells.GetCell(0));
                        if (objDisplay == null) continue;
                        dic.Add(objValue.ToString(), objDisplay.ToString());
                    }
                    dicselection.Add(columns[columnNames[i]], dic);
                    codeSheetIndex++;
                }

                ///����  
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    IRow cells = sheet.GetRow(i);
                    if (cells == null) continue;
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnNames.Count; j++)
                    {
                        object objValue = GetValueType(cells.GetCell(j));
                        if (objValue == null)
                        {
                            dr[j] = null;
                            continue;
                        }
                        if (string.IsNullOrEmpty(columns[columnNames[j]]))
                        {
                            dr[j] = objValue;
                            continue;
                        }
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        if (dicselection.TryGetValue(columns[columnNames[j]], out dic))
                        {
                            dr[j] = dic.FirstOrDefault(d => d.Value == GetValueType(cells.GetCell(j)).ToString()).Key;
                        }
                        else
                            dr[j] = objValue;
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        /// <summary>
        /// ��ȡ��Ԫ������
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    if (DateUtil.IsCellDateFormatted(cell))
                        return cell.DateCellValue;
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }
    }
}