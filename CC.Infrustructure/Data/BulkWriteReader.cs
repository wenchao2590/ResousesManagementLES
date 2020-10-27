using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Globalization;

using Microsoft.Practices.EnterpriseLibrary.Data;

using Infrustructure.Data;

namespace Infrustructure.Data
{
    /// <summary>
    /// 批量写入基类
    /// </summary>
    public abstract class BulkWriteReader : IDataReader
    {
        protected ExcelDataReader reader;

        protected Dictionary<string, TableColumn> columns = new Dictionary<string, TableColumn>();

        protected string SCHEMA_SQL =
            "select name from sys.columns(nolock) where object_id=(select object_id from sys.objects(nolock) where name=@tableName) order by column_id";
            //"select  name   from   syscolumns   where   id=object_id('MES.TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD')";
 
        /// <summary>
        /// 字段顺序和名称对应
        /// </summary>
        protected Dictionary<int,string> orderName = new Dictionary<int, string>();
        
        /// <summary>
        /// 每一行字段名对应的值
        /// </summary>
        protected NameValuePair values = new NameValuePair();


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BulkWriteReader(string tempTable,string maintainTable)
        {
            Initialize(tempTable, maintainTable);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BulkWriteReader(string tableName)
        {
            Initialize(tableName);
        }

 
          /// <summary>
        /// 初始化
        /// <remarks>
        /// 表信息读取来自于配置文件数据库，如果需要写入其他库,请重载该方法
        /// </remarks>
        /// </summary>
        /// <param name="tableName"></param>
        protected virtual void Initialize(string tableName, string maintainTable)
        {
            var tableNameWithoutSchema = tableName;
            if (tableNameWithoutSchema.IndexOf('.') >= 0)
            {
                tableNameWithoutSchema = tableNameWithoutSchema.Split('.')[1];
            }
            tableNameWithoutSchema = tableNameWithoutSchema.Replace("[", "").Replace("]", "");

            var dataBase = DatabaseFactory.CreateDatabase();
            var command = dataBase.GetSqlStringCommand(SCHEMA_SQL);

            dataBase.AddInParameter(command, "@tableName", DbType.String, tableNameWithoutSchema);

            using(var reader = dataBase.ExecuteReader(command))
            {
                int i = 0;
                orderName.Clear();
                while (reader.Read())
                {
                    orderName[i] = reader[0].ToString().ToLower();
                    i++;
                }
            }

            TableColumnDAL dao = new TableColumnDAL();

            this.columns = dao.GetColumns("LES", maintainTable);
        }
        /// <summary>
        /// 初始化
        /// <remarks>
        /// 表信息读取来自于配置文件数据库，如果需要写入其他库,请重载该方法
        /// </remarks>
        /// </summary>
        /// <param name="tableName"></param>
        protected virtual void Initialize(string tableName)
        {
            var tableNameWithoutSchema = tableName;
            if (tableNameWithoutSchema.IndexOf('.') >= 0)
            {
                tableNameWithoutSchema = tableNameWithoutSchema.Split('.')[1];
            }
            tableNameWithoutSchema = tableNameWithoutSchema.Replace("[", "").Replace("]", "");

            var dataBase = DatabaseFactory.CreateDatabase();
            var command = dataBase.GetSqlStringCommand(SCHEMA_SQL);

            dataBase.AddInParameter(command, "@tableName", DbType.String, tableNameWithoutSchema);

            using(var reader = dataBase.ExecuteReader(command))
            {
                int i = 0;
                orderName.Clear();
                while (reader.Read())
                {
                    orderName[i] = reader[0].ToString().ToLower();
                    i++;
                }
            }

            TableColumnDAL dao = new TableColumnDAL();

            this.columns = dao.GetColumns("LES", tableNameWithoutSchema);
        }

        /// <summary>
        /// 获取一行数据
        /// </summary>
        /// <param name="rowValues"></param>
        protected abstract void GetRowValues(ref NameValuePair rowValues);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetErrorMessage()
        {
            StringBuilder errorMsg = new StringBuilder();
              
            //多线程处理时会有问题
            foreach (var t in this.columns)
            {
                string errorMessage = string.Empty;

                if (t.Value.Type == 61)
                {
                    errorMessage = this.FieldChecking(this.reader[t.Value.ColumnCname] == null ? string.Empty : this.reader[t.Value.ColumnCname].ToString(), t.Value);
                }
                else
                {
                    errorMessage = this.FieldChecking(this.reader.GetText(t.Value.ColumnCname), t.Value);
                }

                errorMsg.Append(errorMessage); 
            }

            return errorMsg.ToString();
 
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public int Depth
        {
            get { return 0; }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public  bool Read()
        {
            return RowRead();
        }

        protected abstract bool RowRead();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            
        }

        public int FieldCount
        {
            get { return orderName.Count; }
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            var columnName = orderName[i];
            //System.Diagnostics.Debug.WriteLine(columnName);
            return values[columnName];
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        public object this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        public object this[int i]
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        protected virtual object ConvertToDateTime(object dateTime)
        {
            if (null == dateTime)
            {
                return DBNull.Value;
            }

            if (string.IsNullOrEmpty(dateTime.ToString().Trim()))
            {
                return DBNull.Value;
            }

            if ("\t" == dateTime.ToString())
            {
                return DBNull.Value;
            }

            if (dateTime.ToString() == "00000000")
            {
                return DBNull.Value;
            }

            if (dateTime.ToString() == "99990101")
            {
                return DBNull.Value;
            }

            try
            {
                var dt = Convert.ToDateTime(dateTime);

                return dt;
            }
            catch
            {
            }

            try
            {

                DateTime dt = DateTime.ParseExact(dateTime.ToString(), "yyyyMMdd", DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);

                //System.Diagnostics.Debug.WriteLine(dt);

                return dt;
            }
            catch
            {
                return DBNull.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        protected virtual bool DateTimeChecking(string dateTime)
        {
            if ("\t" == dateTime)
            {
                return true;
            }

            if (null == dateTime)
            {
                return true;
            }

            if (string.IsNullOrEmpty(dateTime.ToString().Trim()))
            {
                return true;
            }


            if (dateTime.ToString() == "00000000")
            {
                return true;
            }

            if (dateTime.ToString() == "99990101")
            {
                return true;
            }

            try
            {
                var dt = Convert.ToDateTime(dateTime);

                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dateTime.ToString(), "yyyyMMdd", DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);

                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="int32"></param>
        /// <returns></returns>
        protected bool Int32Checking(string int32)
        {
            //string pattern = @"^\d*$"; // @"^\d*$";  

            //if (!Regex.IsMatch(int32, pattern))
            //{
            //    return false;
            //}

            try
            {
                Convert.ToInt32(int32);

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected object ConvertToInt32(object int32)
        {
            if (null == int32)
            {
                return DBNull.Value;
            }
            if (string.IsNullOrEmpty(int32.ToString().Trim()))
            {
                return DBNull.Value;
            }

            try
            {
                return Convert.ToInt32(int32);
            }
            catch
            {
                return DBNull.Value;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trueFalse"></param>
        /// <returns></returns>
        protected bool TrueFalseChecking(string trueFalse)
        {
            //string pattern = @"^\d*$"; // @"^\d*$";  

            //if (!Regex.IsMatch(int32, pattern))
            //{
            //    return false;
            //}

            List<string> trueFalseList = new List<string>();

            trueFalseList.Add("TRUE");
            trueFalseList.Add("FALSE");
            trueFalseList.Add("是");
            trueFalseList.Add("否");
            trueFalseList.Add("1");
            trueFalseList.Add("0");

            return trueFalseList.Contains(trueFalse.ToUpper());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trueFalse"></param>
        /// <returns></returns>
        protected object ConvertToBoolean(object trueFalse)
        {
            //string pattern = @"^\d*$"; // @"^\d*$";  

            //if (!Regex.IsMatch(int32, pattern))
            //{
            //    return false;
            //}

            if (null == trueFalse)
            {
                return DBNull.Value;
            }
            if (string.IsNullOrEmpty(trueFalse.ToString().Trim()))
            {
                return DBNull.Value;
            }

            Dictionary<string, bool> trueFalseList = new Dictionary<string, bool>();

            trueFalseList.Add("TRUE", true);
            trueFalseList.Add("FALSE", false);
            trueFalseList.Add("是", true);
            trueFalseList.Add("否", false);
            trueFalseList.Add("1", true);
            trueFalseList.Add("0", false);

            if (trueFalseList.ContainsKey(trueFalse.ToString().ToUpper()))
            {
                return trueFalseList[trueFalse.ToString().ToUpper()];
            }

            return DBNull.Value;
        }

        /// <summary>
        /// 字段名、值对应
        /// </summary>
        protected class NameValuePair 
        {
            private Dictionary<string , object > pair = new Dictionary<string, object>();

            public void Clear()
            {
                pair.Clear();
            }

            public object this[string name]
            {
                get { 
                    var lowerName = name.ToLower();
                    if(pair.ContainsKey(lowerName))
                    {
                        return pair[lowerName];
                    }
                    return null;
                }
                set
                {
                    var lowerName = name.ToLower();
                    pair[lowerName] = value;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        protected string FieldChecking(string text, TableColumn column)
        {
            StringBuilder errorMessage = new StringBuilder();

            //System.Diagnostics.Debug.WriteLine(text);

            //System.Diagnostics.Debug.WriteLine(column.ColumnCname + ":" + column.MaxLength);

            if (text.Length > column.MaxLength)
            {
                errorMessage.Append(string.Format("{0}字段长度超长;", column.ColumnCname));
            }
            else if (!column.IsNullable && string.IsNullOrEmpty(text))
            {
                throw new System.Exception(string.Format("{0}字段为必填项.",column.ColumnCname));
                //errorMessage.Append(string.Format("{0}字段为空;", column.ColumnCname));
            }
            else if (column.Type == 56 && !string.IsNullOrEmpty(text))
            {
                //数字
                if (!this.Int32Checking(text))
                {
                    errorMessage.Append(string.Format("{0}字段不为数字类型;", column.ColumnCname));
                }
            }
            else if (column.Type == 61 && !string.IsNullOrEmpty(text))
            {
                //时间
                if (!this.DateTimeChecking(text))
                {
                    errorMessage.Append(string.Format("{0}字段不为时间类型;", column.ColumnCname));
                }
            }
            else if (column.Type == 104 && !string.IsNullOrEmpty(text))
            {
                //是否
                if (!this.TrueFalseChecking(text))
                {
                    errorMessage.Append(string.Format("{0}字段不为是否类型;", column.ColumnCname));
                }
            }
            return errorMessage.ToString();

        }
    }
}
