﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using OfficeOpenXml;
using Infrustructure.Data.Integration;
using System.Linq;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;

namespace Infrustructure.Data
{
    [Obsolete("禁止使用，后期将会删除，使用ExcelDataReader,禁止使用DATATABLE!")]
    public class ExcelReader : IDisposable
    {
        static object _syncRoot = new object();
        #region Variables

        private bool _blnHeaders = true;
        private bool _blnKeepConnectionOpen;
        private bool _blnMixedData = true;
        private OleDbCommand _oleCmdSelect;
        private OleDbConnection _oleConn;
        private string _strExcelFilename;
        private string _strSheetName;
        private string _strSheetRange;

        #endregion

        public ExcelReader(string excelPath)
        {
            _strExcelFilename = excelPath;
        }

        #region properties

        public string ExcelFilename
        {
            get { return _strExcelFilename; }
            set { _strExcelFilename = value; }
        }

        public string SheetName
        {
            get { return _strSheetName; }
            set { _strSheetName = value; }
        }

        public string SheetRange
        {
            get { return _strSheetRange; }
            set
            {
                if (value.IndexOf(':') == -1)
                    throw new System.Exception("Invalid range length");
                _strSheetRange = value;
            }
        }

        public bool KeepConnectionOpen
        {
            get { return _blnKeepConnectionOpen; }
            set { _blnKeepConnectionOpen = value; }
        }

        public bool Headers
        {
            get { return _blnHeaders; }
            set { _blnHeaders = value; }
        }

        public bool MixedData
        {
            get { return _blnMixedData; }
            set { _blnMixedData = value; }
        }

        public string ColName(int intCol)
        {
            string sColName;
            if (intCol < 26)
                sColName = Convert.ToString(Convert.ToChar((Convert.ToByte('A') + intCol)));
            else
            {
                int intFirst = (intCol / 26);
                int intSecond = (intCol % 26);
                sColName = Convert.ToString(Convert.ToByte('A') + intFirst);
                sColName += Convert.ToString(Convert.ToByte('A') + intSecond);
            }
            return sColName;
        }

        public int ColNumber(string strCol)
        {
            strCol = strCol.ToUpper();
            int intColNumber;
            if (strCol.Length > 1)
            {
                intColNumber = Convert.ToInt16(Convert.ToByte(strCol[1]) - 65);
                intColNumber += Convert.ToInt16(Convert.ToByte(strCol[1]) - 64) * 26;
            }
            else
                intColNumber = Convert.ToInt16(Convert.ToByte(strCol[0]) - 65);
            return intColNumber;
        }

        public string[] GetExcelNames()
        {
            DataTable dt = null;

            try
            {
                if (_oleConn == null)
                    Open();

                // Get the data table containing the schema
                dt = _oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                    return null;

                string[] excelSheets = new string[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    string strSheetTableName = row["TABLE_NAME"].ToString();
                    excelSheets[i] = strSheetTableName.Substring(0, strSheetTableName.Length - 1);
                    i++;
                }


                return excelSheets;
            }
            catch
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (!KeepConnectionOpen)
                    Close();
                if (dt != null)
                    dt.Dispose();
            }
        }

        public string[] GetExcelTableNames()
        {
            DataTable dt = null;
            try
            {
                if (_oleConn == null)
                    Open();

                // Get the data table containing the schema
                dt = _oleConn.GetSchema("Tables", null);

                if (dt == null)
                    return null;

                List<string> excelSheets = new List<string>();

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    string strSheetTableName = row["TABLE_NAME"].ToString().Trim('\'');
                    if (strSheetTableName.Contains("<") ||
                        strSheetTableName.Contains(">"))
                        throw new InvalidDataException("Excel文件的表名不能包含'<'和'>'。");
                    if (strSheetTableName.EndsWith("$"))
                        excelSheets.Add(strSheetTableName.TrimEnd('$'));
                }


                return excelSheets.ToArray();
            }
            catch (InvalidDataException)
            {
                throw;
            }
            catch
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (!KeepConnectionOpen)
                    Close();
                if (dt != null)
                    dt.Dispose();
            }
        }

        public DataTable GetTableSchema(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentException("table name can not be empty!", "tableName");
            try
            {
                if (_oleConn == null)
                    Open();
                if (!tableName.EndsWith("$"))
                    tableName += "$";
                // Get the data table containing the schema
                return _oleConn.GetSchema("Columns", new string[]
				                                     	{
				                                     		null,
				                                     		null, tableName, null
				                                     	});
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (!KeepConnectionOpen)
                    Close();
            }
        }

        #endregion

        #region Excel Connection

        private string ExcelConnectionOptions()
        {
            string strOpts = "";
            if (MixedData)
                strOpts += "Imex=2;";
            if (Headers)
                strOpts += "HDR=Yes;";
            else
                strOpts += "HDR=No;";
            return strOpts;
        }


        private string ExcelConnection()
        {
            return string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties={1}Excel 12.0;HDR=Yes;{1}",
                                 _strExcelFilename,
                                 Convert.ToChar(34));
        }

        #endregion

        #region Open / Close

        public void Open()
        {
            Close();

            if (!File.Exists(_strExcelFilename))
                throw new FileNotFoundException("", _strExcelFilename);
            _oleConn = new OleDbConnection(ExcelConnection());
            _oleConn.Open();
        }

        public void Close()
        {
            if (_oleConn != null)
            {
                if (_oleConn.State != ConnectionState.Closed)
                    _oleConn.Close();
                _oleConn.Dispose();
                _oleConn = null;
            }
        }

        #endregion

        #region Command Select

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        private bool SetSheetQuerySelect()
        {
            if (_oleConn == null)
                throw new System.Exception("Connection is unassigned or closed.");

            if (_strSheetName.Length == 0)
                throw new System.Exception("Sheetname was not assigned.");

            _oleCmdSelect = new OleDbCommand(string.Format("SELECT * FROM [{0}${1}]", _strSheetName, _strSheetRange), _oleConn);

            return true;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_oleConn != null)
            {
                _oleConn.Dispose();
                _oleConn = null;
            }
            if (_oleCmdSelect != null)
            {
                _oleCmdSelect.Dispose();
                _oleCmdSelect = null;
            }
        }

        #endregion

        public DataTable GetTable()
        {
            return GetTable("Sheet1");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public DataTable GetTable(string strTableName)
        {
            if (strTableName.EndsWith("$"))
                strTableName.TrimEnd('$');
            _strSheetName = strTableName;
            //Open and query

            DataTable dt = new DataTable(strTableName);
            if (_oleConn == null)
                Open();
            if (_oleConn.State != ConnectionState.Open)
                throw new System.Exception("Connection cannot open error.");
            if (!SetSheetQuerySelect())
                return null;


            //Fill table
            OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
            oleAdapter.SelectCommand = _oleCmdSelect;
            oleAdapter.FillSchema(dt, SchemaType.Source);
            oleAdapter.Fill(dt);
            if (!Headers)
                if (_strSheetRange.IndexOf(":") > 0)
                {
                    string FirstCol = _strSheetRange.Substring(0, _strSheetRange.IndexOf(":") - 1);
                    int intCol = ColNumber(FirstCol);
                    for (int intI = 0; intI < dt.Columns.Count; intI++)
                    {
                        dt.Columns[intI].Caption = ColName(intCol + intI);
                    }
                }
            // remove the empty column
            for (int i = dt.Columns.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(dt.Columns[i].ColumnName))
                {
                    dt.Columns.RemoveAt(i);
                    i--;
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
            //Cannot delete rows in Excel workbook
            dt.DefaultView.AllowDelete = false;

            //Clean up
            _oleCmdSelect.Dispose();
            oleAdapter.Dispose();
            if (!KeepConnectionOpen) Close();
            return dt;
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static bool SaveData(string directory, DataTable dt, out string file, out string message)
        {
            file = string.Empty;
            if (!Directory.Exists(directory))
            {
                message = "can not found directory: " + directory;
                return false;
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                message = "can not found any data";
                return false;
            }

            message = string.Empty;

            lock (_syncRoot)
            {
                file = string.Concat(dt.TableName, "_", DateTime.Now.ToString("yyyyMMddHHmmssfff"), ".xml");
            }
            string filename = Path.Combine(directory, file);

            using (StreamWriter sw = File.CreateText(filename))
            {
                sw.Write(
                    @"<?xml version=""1.0""?>
<?mso-application progid=""Excel.Sheet""?>
<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""
 xmlns:o=""urn:schemas-microsoft-com:office:office""
 xmlns:x=""urn:schemas-microsoft-com:office:excel""
 xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""
 xmlns:html=""http://www.w3.org/TR/REC-html40"">
 <ExcelWorkbook xmlns=""urn:schemas-microsoft-com:office:excel"">
  <WindowHeight>9480</WindowHeight>
  <WindowWidth>15135</WindowWidth>
  <WindowTopX>120</WindowTopX>
  <WindowTopY>60</WindowTopY>
  <ProtectStructure>False</ProtectStructure>
  <ProtectWindows>False</ProtectWindows>
 </ExcelWorkbook>
 <Styles>
  <Style ss:ID=""Default"" ss:Name=""Normal"">
   <Alignment ss:Vertical=""Center""/>
   <Borders/>
   <Font ss:FontName=""宋体"" x:CharSet=""134"" ss:Size=""11"" ss:Color=""#000000""/>
   <Interior/>
   <NumberFormat/>
   <Protection/>
  </Style>
  <Style ss:ID=""s62"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Center"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""12"" ss:Color=""#000000""
    ss:Bold=""1""/>
   <Interior/>
  </Style>
  <Style ss:ID=""s64"">
   <Alignment ss:Vertical=""Bottom"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""12"" ss:Color=""#000000""/>
   <Interior/>
  </Style>
 </Styles>\n");

                sw.Write(" <Worksheet ss:Name=\"{0}\">", dt.TableName);

                int fieldCount = dt.Columns.Count;
                sw.Write(
                    "  <Table ss:ExpandedColumnCount=\"{0}\" ss:ExpandedRowCount=\"{1}\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"15.75\">\n",
                    fieldCount, dt.Rows.Count + 1);

                bool isStart = true;

                foreach (DataRow dr in dt.Rows)
                {
                    if (isStart)
                    {
                        sw.Write("   <Row>\n");
                        // write column name
                        for (int i = 0; i < fieldCount; i++)
                        {
                            sw.Write("    <Cell ss:StyleID=\"s62\"><Data ss:Type=\"String\">{0}</Data></Cell>\n",
                                     HttpUtility.HtmlEncode(dt.Columns[i].ColumnName));
                        }
                        sw.Write("   </Row>\n");

                        isStart = false;
                    }

                    sw.Write("   <Row>\n");
                    for (int i = 0; i < fieldCount; i++)
                    {
                        sw.Write("    <Cell ss:StyleID=\"s64\"><Data ss:Type=\"{0}\">{1}</Data></Cell>\n",
                                 GetExcelType(dt.Columns[i].DataType),
                                 (dr[i] == null || dr[i] == DBNull.Value) ? string.Empty : HttpUtility.HtmlEncode(dr[i].ToString()));
                    }
                    sw.Write("   </Row>\n");
                }
                sw.Write(
                    @"  </Table>
  <WorksheetOptions xmlns=""urn:schemas-microsoft-com:office:excel"">
   <PageSetup>
    <Header x:Margin=""0.3""/>
    <Footer x:Margin=""0.3""/>
    <PageMargins x:Bottom=""0.75"" x:Left=""0.7"" x:Right=""0.7"" x:Top=""0.75""/>
   </PageSetup>
   <Selected/>
   <Panes />
   <ProtectObjects>False</ProtectObjects>
   <ProtectScenarios>False</ProtectScenarios>
  </WorksheetOptions>
 </Worksheet>
</Workbook>");
                sw.Close();
            }
            return true;
        }

        private static string GetExcelType(Type dataType)
        {
            if (dataType == typeof(int) ||
                dataType == typeof(float) ||
                dataType == typeof(decimal))
                return "Number";
            return "String";
        }
    }

    /// <summary>
    /// Excel读取，支持2007以后的OpenXml格式的的Excel
    /// </summary>
    [DataContract]
    public class ExcelDataReader : IDisposable
    {
        /// <summary>
        /// ExcelPackage
        /// <seealso cref="http://epplus.codeplex.com"/>
        /// </summary>
        private ExcelPackage excelPackage = null;

        /// <summary>
        /// 行标记
        /// </summary>
        private int rowFlag = 0;

        /// <summary>
        /// 总行数
        /// </summary>
        private int totalRow = 0;

        /// <summary>
        /// 总列数
        /// </summary>
        private int totalColumn = 0;

        /// <summary>
        /// Worksheet
        /// </summary>
        private ExcelWorksheet worksheet = null;

        /// <summary>
        /// 标题行
        /// </summary>
        private int headerRow = 0;

        /// <summary>
        /// 标题和列数对应
        /// </summary>
        Dictionary<string, int> columnMapping = new Dictionary<string, int>();

        Dictionary<string, string> destinationSourceMapping = new Dictionary<string, string>();

        private string schemaPath = String.Empty;

        private DataSchema schema = null;

        List<string> requiredColumns = new List<string>();

        List<string> nonSpecialCharsColumns = new List<string>();

        Dictionary<string, int> maxLengthColumns = new Dictionary<string, int>();
        //列类型
        Dictionary<string, string> columnTypes = new Dictionary<string, string>();

        /// <summary>
        /// ExcelReader
        /// </summary>
        /// <param name="fileName">excel完整路径</param>
        public ExcelDataReader(string fileName)
        {
            excelPackage = new ExcelPackage(new FileInfo(fileName));

            worksheet = excelPackage.Workbook.Worksheets[1];
            totalRow = worksheet.Dimension.End.Row;
            totalColumn = worksheet.Dimension.End.Column;
            headerRow = 1;
            Init();
        }

        /// <summary>
        /// ExcelReader
        /// </summary>
        /// <param name="fileName">excel完整路径</param>
        /// <param name="worksheetName">worksheet名称</param>
        public ExcelDataReader(string fileName, string worksheetName)
            : this(fileName, worksheetName, true)
        {

        }

        /// <summary>
        /// ExcelReader
        /// </summary>
        /// <param name="fileName">excel完整路径</param>
        /// <param name="worksheetName">worksheet名称</param>
        /// <param name="firstRowAsHeader"></param>
        public ExcelDataReader(string fileName, string worksheetName, bool firstRowAsHeader)
            : this(fileName, worksheetName, firstRowAsHeader ? 1 : 0)
        {

        }

        /// <summary>
        /// ExcelReader
        /// </summary>
        /// <param name="fileName">excel路径</param>
        /// <param name="worksheetName">worksheet名称</param>
        /// <param name="headerRow">标题行位置</param>
        public ExcelDataReader(string fileName, string worksheetName, int headerRow)
            : this(fileName, worksheetName, headerRow, String.Empty)
        {

        }

        /// <summary>
        /// ExcelReader
        /// </summary>
        /// <param name="fileName">Excel文件完整路径</param>
        /// <param name="worksheetName">worksheet名称</param>
        /// <param name="headerRow">标题行位置</param>
        /// <param name="schemaPath">验证schema xml路径</param>
        public ExcelDataReader(string fileName, string worksheetName, int headerRow, string schemaPath)
        {
            excelPackage = new ExcelPackage(new FileInfo(fileName));
            try
            {
                worksheet = excelPackage.Workbook.Worksheets[worksheetName];
                if (worksheet.Dimension == null)
                {
                    throw new ArgumentException(String.Format("导入的 Excel [{0}] 内容为空", worksheet.Name));
                }
                totalRow = worksheet.Dimension.End.Row;
                totalColumn = worksheet.Dimension.End.Column;
                this.headerRow = headerRow;
                this.schemaPath = schemaPath;
            }
            catch
            {
                throw new ArgumentException("Excel文件格式不正确，请检查Excel内容");
            }

            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            if (headerRow > 0)
            {
                rowFlag = headerRow;
                for (int i = 1; i <= totalColumn; i++)
                {
                    string columnName = worksheet.Cells[headerRow, i].Text.ToLower();
                    if (!String.IsNullOrEmpty(columnName) && !columnMapping.ContainsKey(columnName))
                    {
                        columnMapping[columnName] = i;
                    }
                }
            }
            else
            {
                rowFlag = 0;
            }

            if (!String.IsNullOrEmpty(schemaPath))
            {
                schema = ValidationUtils.GetDataSchemaFromFile(schemaPath);
                foreach (var field in schema.Fields)
                {
                    destinationSourceMapping[field.Destination.ToLower()] = field.Source.ToLower();
                    if (!columnMapping.ContainsKey(field.Source.ToLower()))
                    {
                        throw new ArgumentException(String.Format("列 [{0}] 在Excel中不存在", field.Source));
                    }

                    var requireAttribute = field.GetAnyAttributeValue("required");
                    if (!String.IsNullOrEmpty(requireAttribute))
                    {
                        try
                        {
                            if (Convert.ToBoolean(requireAttribute))
                            {
                                requiredColumns.Add(field.Source.ToLower());
                            }
                        }
                        catch
                        {

                        }
                    }

                    var nonSpecialCharsAttribute = field.GetAnyAttributeValue("nonspecialchars");
                    if (!String.IsNullOrEmpty(nonSpecialCharsAttribute))
                    {
                        try
                        {
                            if (Convert.ToBoolean(nonSpecialCharsAttribute))
                            {
                                nonSpecialCharsColumns.Add(field.Source.ToLower());
                            }
                        }
                        catch
                        {

                        }
                    }

                    var maxlengthAttribute = field.GetAnyAttributeValue("maxlength");
                    if (!String.IsNullOrEmpty(maxlengthAttribute))
                    {
                        try
                        {
                            int maxLength = Convert.ToInt32(maxlengthAttribute);
                            maxLengthColumns[field.Source.ToLower()] = maxLength;
                        }
                        catch
                        {
                        }
                    }

                    var typeAttribute = field.Type;
                    if (!String.IsNullOrEmpty(typeAttribute))
                    {
                        try
                        {
                            columnTypes[field.Source.ToLower()] = typeAttribute;
                        }
                        catch
                        {
                        }
                    }
                }


            }

        }

        /// <summary>
        /// 读取下一行记录
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            rowFlag++;
            if (rowFlag > totalRow)
            {

                return false;
            }
            else
            {
                if (IsEmptyRow(rowFlag))
                {
                    return Read();
                }

                if (schema != null)
                {
                    //valid row
                    foreach (string column in requiredColumns)
                    {
                        if (String.IsNullOrEmpty(GetText(column)))
                        {
                            throw new ArgumentException(String.Format("第[{0}]行的列 [{1}] 必须含有值", rowFlag, column));
                        }

                    }

                    //valid row
                    foreach (string column in nonSpecialCharsColumns)
                    {
                        var text = GetText(column);
                        if (text != null && text.Length > 0)
                        {
                            Regex reg = new Regex(@"^[A-Za-z0-9_\s\u4e00-\u9fa5]*$");
                            Match ma = null;

                            ma = reg.Match(text);
                            if (!ma.Success)
                                throw new ArgumentException(String.Format("第[{0}]行的列 [{1}] 含有非法字符", rowFlag, column));
                        }
                    }

                    foreach (var column in maxLengthColumns.Keys)
                    {
                        var text = GetText(column);
                        if (text != null)
                        {
                            if (text.Length > maxLengthColumns[column])
                                throw new ArgumentException(String.Format("第[{0}]行的列 [{1}] 长度超出范围,最大为" + maxLengthColumns[column] + "位", rowFlag, column));
                        }
                    }


                    foreach (var column in columnTypes.Keys)
                    {
                        if (columnTypes[column].ToLower().IndexOf("int32") > 0
                            && columnTypes[column].ToLower().IndexOf("uint32") < 0)//检查整数
                        {
                            var text = GetText(column);
                            if (text != null && text.Length > 0)
                            {
                                Regex reg = new Regex("^[0-9]+$");
                                Match ma = null;

                                ma = reg.Match(text);
                                if (!ma.Success)
                                    throw new ArgumentException(String.Format("第[{0}]行的列 [{1}] 数据必须为0或正整数", rowFlag, column));
                            }
                        }
                        if (columnTypes[column].ToLower().IndexOf("uint32") > 0)//检查正整数
                        {
                            var text = GetText(column);
                            if (text != null && text.Length > 0)
                            {
                                Regex reg = new Regex("^[0-9]*[1-9][0-9]*$");
                                Match ma = null;

                                ma = reg.Match(text);
                                if (!ma.Success)
                                    throw new ArgumentException(String.Format("第[{0}]行的列 [{1}] 数据必须为正整数", rowFlag, column));
                            }
                        }
                    }
                }
                return true;
            }

        }

        private bool IsEmptyRow(int rowFlag)
        {
            bool result = true;
            for (int i = 1; i <= ColumnCount; i++)
            {
                if (worksheet.GetValue(rowFlag, i) != null)
                    return false;
            }
            return result;
        }

        /// <summary>
        /// 获取指定列的值
        /// </summary>
        /// <param name="column">列数，以1作为开始</param>
        /// <returns></returns>
        public object this[int column]
        {
            get { return worksheet.GetValue(rowFlag, column); }
        }

        /// <summary>
        /// 根据列名获取指定列的值
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public object this[string columnName]
        {
            get
            {
                var lower = columnName.ToLower();
                if (!columnMapping.ContainsKey(lower))
                {
                    if (!destinationSourceMapping.ContainsKey(lower))
                        throw new ArgumentOutOfRangeException(String.Format("获取列名:[{0}]不在Sheet内存在", columnName));
                    else
                    {
                        return worksheet.GetValue(rowFlag, columnMapping[destinationSourceMapping[lower]]);
                    }
                }
                return worksheet.GetValue(rowFlag, columnMapping[lower]);
            }
        }

        /// <summary>
        /// 根据指定列名获取当前记录中的字符串
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetText(string columnName)
        {
            var lower = columnName.ToLower();
            if (!columnMapping.ContainsKey(lower))
            {
                if (!destinationSourceMapping.ContainsKey(lower))
                    throw new ArgumentOutOfRangeException(String.Format("获取列名:[{0}]不在Sheet内存在", columnName));
                else
                {
                    string text1 = worksheet.Cells[rowFlag, columnMapping[destinationSourceMapping[lower]]].Text;
                    return text1.TrimStart().TrimEnd();
                }
            }
            //modified by zeno 2013-8-8 
            //because obtained the text of cells is empty,so need use their value property
            string text = string.Empty;

            if (worksheet.Cells[rowFlag, columnMapping[lower]].Value != null)
            {
                text = worksheet.Cells[rowFlag, columnMapping[lower]].Value.ToString();
            }

            return text.TrimStart().TrimEnd();
        }

        /// <summary>
        /// 根据指定列获取当前记录中的字符串
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public string GetText(int column)
        {
            return worksheet.Cells[rowFlag, column].Text;
        }

        /// <summary>
        /// 总列数
        /// </summary>        
        public int ColumnCount
        {
            get { return totalColumn; }
        }


        /// <summary>
        /// 总列数
        /// </summary>        
        public int RowCount
        {
            get { return totalRow; }
        }
        /// <summary>
        /// 获取所有列名
        /// </summary>
        /// <returns></returns>
        public List<string> GetHeaders()
        {
            return columnMapping.Keys.ToList();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ExcelDataReader()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (excelPackage != null)
                {
                    excelPackage.Dispose();
                }
            }
            // free native resources if there are any.

        }

    }

}