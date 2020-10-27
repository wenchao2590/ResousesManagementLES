using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.ComponentModel;
using System.ServiceModel;
using System.Runtime.Serialization;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Infrustructure.Utilities;

namespace Infrustructure.Data
{

    public class TableColumnDAL
    {
        private const string TM_SYS_COLUMNS_SELECT_BY_TABLENAME =
   @"SELECT [ID]
          ,[DATABASE_SCHEMA]
          ,[TABLE_NAME]
          ,[COLUMN_NAME]
          ,[ORDER]
          ,[TYPE]
          ,[MAX_LENGTH]
          ,[DESIRED_LENGTH]
          ,[PRECISION]
          ,[SCALE]
          ,[IS_NULLABLE]
          ,[IS_IDENTITY]
          ,[COLUMN_CNAME]
      FROM [MES].[TM_SYS_COLUMNS]
	 WHERE DATABASE_SCHEMA=@DATABASE_SCHEMA AND TABLE_NAME=@TABLE_NAME ;";

        public Dictionary<string, TableColumn> GetColumns(string schema, string tableName)
        {
            Dictionary<string, TableColumn> lst = new Dictionary<string, TableColumn>();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(TM_SYS_COLUMNS_SELECT_BY_TABLENAME);
            db.AddInParameter(dbCommand, "@DATABASE_SCHEMA", DbType.String, schema);
            db.AddInParameter(dbCommand, "@TABLE_NAME", DbType.String, tableName);

            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    TableColumn info = CreateTableColumnInfo(dr);
                    lst.Add(info.ColumnName, info);
                }
            }

            return lst;
        }

        private TableColumn CreateTableColumnInfo(IDataReader rdr)
        {
            TableColumn info = new TableColumn();

            info.Id = DBConvert.GetInt32(rdr, 0);
            info.DatabaseSchema = DBConvert.GetString(rdr, 1);
            info.TableName = DBConvert.GetString(rdr, 2);
            info.ColumnName = DBConvert.GetString(rdr, 3);
            info.Order = DBConvert.GetInt32(rdr, 4);
            info.Type = DBConvert.GetInt32(rdr, 5);
            info.MaxLength = DBConvert.GetInt32(rdr, 6);
            info.DesiredLength = DBConvert.GetInt32Nullable(rdr, 7);
            info.Precision = DBConvert.GetInt32(rdr, 8);
            info.Scale = DBConvert.GetInt32(rdr, 9);
            info.IsNullable = DBConvert.GetBool(rdr, 10);
            info.IsIdentity = DBConvert.GetBool(rdr, 11);
            info.ColumnCname = DBConvert.GetString(rdr, 12);

            if (string.IsNullOrEmpty(info.ColumnCname))
            {
                //throw new ApplicationException(string.Format("导入配置列名{0}对应的中文名为空。",info.ColumnName));
            }

            return info;
        }

    }
}