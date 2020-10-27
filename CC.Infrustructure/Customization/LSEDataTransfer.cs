using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using Infrustructure.Data;
using Infrustructure.Data.Integration;
using System.Reflection;

namespace Infrustructure.Customization
{
	public class LESDataTransfer
	{
		/// <summary>
		/// 将按字符串长度隔开的文本数据导入到数据库，约定：
		/// 1. 文本文件包含第一行头和最后一行尾，且不必导入
		/// 2. 文本文件不含列名，第二行起即为数据
		/// 3. 数据库连接取config文件中第一个连接串
		/// 4. 待导入表名即schema文件名
		/// 5. 导入前会清空表数据
		/// </summary>
		/// <param name="sourcePath">源数据文件的绝对物理路径</param>
		/// <param name="schemaPath">schema文件的绝对物理路径</param>
		/// <param name="message">如返回值为false, 则包含失败信息, 否则可忽略</param>
		/// <returns></returns>
		public bool TransferFixLengthFileToDatabase(string sourcePath, string schemaPath, out string message)
		{
			message = string.Empty;
			if (string.IsNullOrEmpty(sourcePath))
			{
				message = "数据文件路径为空";
				return false;
			}
			if (!File.Exists(sourcePath))
			{
				message = string.Format("数据文件路径[{0}]不存在", sourcePath);
				return false;
			}

			if (string.IsNullOrEmpty(schemaPath))
			{
				message = "schema文件路径为空";
				return false;
			}
			if (!File.Exists(schemaPath))
			{
				message = string.Format("schema文件路径[{0}]不存在", schemaPath);
				return false;
			}

			bool result;

			using (IntegrationEngine target = new IntegrationEngine())
			{
				FixedLengthFileStorage sourceProvider = new FixedLengthFileStorage("sourceProvider");
				target.Providers.Add(sourceProvider);
				DatabaseStorage destinationProvider = new DatabaseStorage("destinationProvider");
				target.Providers.Add(destinationProvider);

				IDictionary state3 = new Hashtable();
				state3["sourceProvider"] = IntegrationMode.GetSchema | IntegrationMode.GetData;
				state3["destinationProvider"] = IntegrationMode.TransferData;
				state3[ContextState.SourcePath] = sourcePath;
				state3[ContextState.SchemaFilePath] = schemaPath;
				state3[ContextState.FlatFileSkipFirstLine] = true;
				state3[ContextState.FlatFileSkipLastLine] = true;
				state3[ContextState.DatabaseConnectionString] = ConfigurationManager.ConnectionStrings[0].ConnectionString;
				state3[ContextState.DatabaseTableName] = GetTableNameFromSchemaFileName(schemaPath);
				state3[ContextState.DatabaseTruncateTable] = true;

				try
				{
					result = target.Run(state3);
				}
				catch (System.Exception ex)
				{
					result = false;
					message = ex.Message;
				}
			}
			return result;
		}

		/// <summary>
		/// 将按','隔开的文本数据导入到数据库，约定：
		/// 1. 文本文件包含第一行列名，且不必导入
		/// 2. 文本文件第二行起即为数据
		/// 3. 数据库连接取config文件中第一个连接串
		/// 4. 待导入表名即schema文件名
		/// 5. 导入前会清空表数据
		/// 6. 分隔符','在schema文件中定义
		/// </summary>
		/// <param name="sourcePath">源数据文件的绝对物理路径</param>
		/// <param name="schemaPath">schema文件的绝对物理路径</param>
		/// <param name="message">如返回值为false, 则包含失败信息, 否则可忽略</param>
		/// <returns></returns>
		public bool TransferCSVFileToDatabase(string sourcePath, string schemaPath, out string message)
		{
			message = string.Empty;
			if (string.IsNullOrEmpty(sourcePath))
			{
				message = "数据文件路径为空";
				return false;
			}
			if (!File.Exists(sourcePath))
			{
				message = string.Format("数据文件路径[{0}]不存在", sourcePath);
				return false;
			}

			if (string.IsNullOrEmpty(schemaPath))
			{
				message = "schema文件路径为空";
				return false;
			}
			if (!File.Exists(schemaPath))
			{
				message = string.Format("schema文件路径[{0}]不存在", schemaPath);
				return false;
			}

			bool result;

			using (IntegrationEngine target = new IntegrationEngine())
			{
				DelimitedFileStorage sourceProvider = new DelimitedFileStorage("sourceProvider");
				target.Providers.Add(sourceProvider);
				DatabaseStorage destinationProvider = new DatabaseStorage("destinationProvider");
				target.Providers.Add(destinationProvider);

				IDictionary state3 = new Hashtable();
				state3["sourceProvider"] = IntegrationMode.GetSchema | IntegrationMode.GetData;
				state3["destinationProvider"] = IntegrationMode.TransferData;
				state3[ContextState.SourcePath] = sourcePath;
				state3[ContextState.SchemaFilePath] = schemaPath;
				state3[ContextState.FlatFileSkipFirstLine] = true;
				state3[ContextState.FlatFileSkipLastLine] = false;
				state3[ContextState.DatabaseConnectionString] = ConfigurationManager.ConnectionStrings[0].ConnectionString;
				state3[ContextState.DatabaseTableName] = GetTableNameFromSchemaFileName(schemaPath);
				state3[ContextState.DatabaseTruncateTable] = true;

				try
				{
					result = target.Run(state3);
				}
				catch (System.Exception ex)
				{
					result = false;
					message = ex.Message;
				}
			}
			return result;
		}

		/// <summary>
		/// 将Excel数据导入数据库, 约定:
		/// 1. Excel文件包含第一行列名，且不必导入
		/// 2. 文本文件第二行起即为数据
		/// 3. 数据库连接取config文件中第一个连接串
		/// 4. 待导入表名即schema文件名
		/// 5. 先尝试读取Excel名为schema文件名的worksheet,失败则读取第一个worksheet的数据.
		/// 6. 导入前会清空表数据
		/// </summary>
		/// <param name="sourcePath">源数据文件的绝对物理路径</param>
		/// <param name="schemaPath">schema文件的绝对物理路径</param>
		/// <param name="message">如返回值为false, 则包含失败信息, 否则可忽略</param>
		/// <returns></returns>
        //public bool TransferExcelToDatabase(string sourcePath, string schemaPath, out string message)
        //{
        //    message = string.Empty;
        //    if (string.IsNullOrEmpty(sourcePath))
        //    {
        //        message = "数据文件路径为空";
        //        return false;
        //    }
        //    if (!File.Exists(sourcePath))
        //    {
        //        message = string.Format("数据文件路径[{0}]不存在", sourcePath);
        //        return false;
        //    }

        //    if (string.IsNullOrEmpty(schemaPath))
        //    {
        //        message = "schema文件路径为空";
        //        return false;
        //    }
        //    if (!File.Exists(schemaPath))
        //    {
        //        message = string.Format("schema文件路径[{0}]不存在", schemaPath);
        //        return false;
        //    }

        //    bool result;

        //    using (IntegrationEngine target = new IntegrationEngine())
        //    {
        //        ExcelStorage sourceProvider = new ExcelStorage("sourceProvider");
        //        target.Providers.Add(sourceProvider);
        //        DatabaseStorage destinationProvider = new DatabaseStorage("destinationProvider");
        //        target.Providers.Add(destinationProvider);

        //        IDictionary state3 = new Hashtable();
        //        state3["sourceProvider"] = IntegrationMode.GetSchema | IntegrationMode.GetData;
        //        state3["destinationProvider"] = IntegrationMode.TransferData;
        //        state3[ContextState.SourcePath] = sourcePath;
        //        state3[ContextState.SchemaFilePath] = schemaPath;
        //        state3[ContextState.DatabaseConnectionString] = ConfigurationManager.ConnectionStrings[1].ConnectionString;
        //        state3["tableName"] = state3[ContextState.DatabaseTableName] = GetTableNameFromSchemaFileName(schemaPath);
        //        state3[ContextState.DatabaseTruncateTable] = true;
        //        try
        //        {
        //            result = target.Run(state3);
        //        }
        //        catch (System.Exception ex)
        //        {
        //            message = ex.Message;
        //            return false;
        //        }
        //    }
        //    return result;
        //}

		

		

		/// <summary>
		/// 将内存数据导出到XML Speadsheet格式的Excel文件
		/// </summary>
		/// <param name="dt">待导出的数据</param>
		/// <param name="directory">待导出文件所在的目录绝对物理路径</param>
		/// <param name="schemaPath">schema文件的绝对物理路径</param>
		/// <param name="file">导出文件相对于<paramref name="directory"/>的相对路径</param>
		/// <param name="message">如返回值为false, 则包含失败信息, 否则可忽略</param>
		/// <returns></returns>
        //public bool TransferDataTableToExcel(DataTable dt, string directory, string schemaPath, out string file,
        //                                     out string message)
        //{
        //    file = string.Empty;
        //    message = string.Empty;
        //    if (dt == null || dt.Rows.Count == 0)
        //        message = "无可用的数据用来导出";
        //    if (string.IsNullOrEmpty(directory))
        //    {
        //        message = "目录路径为空";
        //        return false;
        //    }
        //    if (!Directory.Exists(directory))
        //    {
        //        message = string.Format("目录路径[{0}]不存在", directory);
        //        return false;
        //    }

        //    if (!string.IsNullOrEmpty(schemaPath) && !File.Exists(schemaPath))
        //    {
        //        message = string.Format("schema文件路径[{0}]不存在", schemaPath);
        //        return false;
        //    }

        //    bool result;

        //    using (IntegrationEngine target = new IntegrationEngine())
        //    {
        //        DataTableStorage sourceProvider = new DataTableStorage("sourceProvider");
        //        target.Providers.Add(sourceProvider);
        //        ExcelStorage destinationProvider = new ExcelStorage("destinationProvider");
        //        target.Providers.Add(destinationProvider);

        //        IDictionary state3 = new Hashtable();
        //        if (string.IsNullOrEmpty(schemaPath))
        //            state3["sourceProvider"] = IntegrationMode.CreateSchema | IntegrationMode.GetData;
        //        else
        //            state3["sourceProvider"] = IntegrationMode.GetSchema | IntegrationMode.GetData;
        //        state3["destinationProvider"] = IntegrationMode.TransferData;
        //        state3["datatable"] = dt;
        //        state3[ContextState.SchemaFilePath] = schemaPath;
        //        state3["desDirectory"] = directory;
        //        try
        //        {
        //            result = target.Run(state3);
        //            if (result)
        //                file = state3["desFile"] as string ?? string.Empty;
        //        }
        //        catch (System.Exception ex)
        //        {
        //            message = ex.Message;
        //            return false;
        //        }
        //    }
        //    return result;
        //}

		/// <summary>
		/// 将内存数据导出到CSV文件
		/// </summary>
		/// <param name="dt">待导出的数据</param>
		/// <param name="directory">待导出文件所在的目录绝对物理路径</param>
		/// <param name="schemaPath">schema文件的绝对物理路径</param>
		/// <param name="file">导出文件相对于<paramref name="directory"/>的相对路径</param>
		/// <param name="message">如返回值为false, 则包含失败信息, 否则可忽略</param>
		/// <returns></returns>
		public bool TransferDataTableToCSVFile(DataTable dt, string directory, string schemaPath, out string file,
		                                       out string message)
		{
			file = string.Empty;
			message = string.Empty;
            //if (dt == null || dt.Rows.Count == 0)
            //{
            //    message = "无可用的数据用来导出";
            //    return false;
            //}
			if (string.IsNullOrEmpty(directory))
			{
				message = "目录路径为空";
				return false;
			}
			if (!Directory.Exists(directory))
			{
				message = string.Format("目录路径[{0}]不存在", directory);
				return false;
			}

			if (!string.IsNullOrEmpty(schemaPath) && !File.Exists(schemaPath))
			{
				message = string.Format("schema文件路径[{0}]不存在", schemaPath);
				return false;
			}

			bool result;

			using (IntegrationEngine target = new IntegrationEngine())
			{
				DataTableStorage sourceProvider = new DataTableStorage("sourceProvider");
				target.Providers.Add(sourceProvider);
				DelimitedFileStorage destinationProvider = new DelimitedFileStorage("destinationProvider");
				target.Providers.Add(destinationProvider);

				IDictionary state3 = new Hashtable();
				if (string.IsNullOrEmpty(schemaPath))
					state3["sourceProvider"] = IntegrationMode.CreateSchema | IntegrationMode.GetData;
				else
					state3["sourceProvider"] = IntegrationMode.GetSchema | IntegrationMode.GetData;
				state3["destinationProvider"] = IntegrationMode.TransferData;
				state3["datatable"] = dt;
				state3[ContextState.SchemaFilePath] = schemaPath;
				state3["desDirectory"] = directory;
				if (string.IsNullOrEmpty(schemaPath))
					state3["desPrefix"] = dt.TableName;
				else
					state3["desPrefix"] = GetTableNameFromSchemaFileName(schemaPath);
				try
				{
					result = target.Run(state3);
					if (result)
						file = state3["desFile"] as string ?? string.Empty;
				}
				catch (System.Exception ex)
				{
					message = ex.Message;
					return false;
				}
			}
			return result;
		}

        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">字段类型</param>
        /// <returns>返回值DataColumn</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public DataColumn GenerateDataColumn(string columnName,Type dataType,bool allowDBNull)
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = columnName;
            dc.DataType = dataType;
            dc.AllowDBNull = allowDBNull;

            return dc;
        }

        
        /// <summary>
        /// 转换IList为DataTable
        /// </summary>
        /// <param name="infos">需要转换的IList</param>
        /// <param name="filterFields">需要过滤掉的字段</param>
        /// <returns>返回值DataTable</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public DataTable TransferIListToDataTable(IList infos,string[] filterFields)
        {
            if (infos == null)
                throw new System.Exception("Input IList is null!");

            IList<string> innerFilterFields = new List<string>(filterFields);
            
            DataTable dt = new DataTable();
            

            int rowIndex = 0;
            foreach (object row in infos)
            {
                int cellIndex=0;
                PropertyInfo[] pis = row.GetType().GetProperties();
                object[] rowContents = new object[pis.Length-filterFields.Length];

                foreach (PropertyInfo pi in pis)
                {
                    if (!innerFilterFields.Contains(pi.Name))
                    {
                        if (rowIndex == 0)
                        {
                            Type colType = pi.PropertyType;
                            if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                colType = colType.GetGenericArguments()[0];

                            dt.Columns.Add(this.GenerateDataColumn(pi.Name, colType, true));
                        }

                        rowContents[cellIndex] = row.GetType().GetProperty(pi.Name).GetValue(row, null);
                        cellIndex++;
                    }
                }

                dt.Rows.Add(rowContents);

                rowIndex++;
            }

            return dt;
        }

        //public bool ImportExcelData(string sourcePath, string tableName, out DataTable dt, out string message)
        //{
        //    return ImportExcelData(sourcePath, tableName, out dt, out message, new Hashtable());
        //}


        //public bool ImportExcelData(string sourcePath, string tableName, out DataTable dt,out string message, Hashtable param)
        //{
        //    dt = new DataTable();
        //    message = string.Empty;
        //    if (string.IsNullOrEmpty(sourcePath))
        //    {
        //        message = "数据文件路径为空";
        //        return false;
        //    }
        //    if (!File.Exists(sourcePath))
        //    {
        //        message = string.Format("数据文件路径[{0}]不存在", sourcePath);
        //        return false;
        //    }

        //    bool result;

        //    using (IntegrationEngine target = new IntegrationEngine())
        //    {
        //        ExcelStorage sourceProvider = new ExcelStorage("sourceProvider");
        //        target.Providers.Add(sourceProvider);
        //        DataTableStorage destinationProvider = new DataTableStorage("destinationProvider");
        //        target.Providers.Add(destinationProvider);

        //        IDictionary state3 = param;
        //        state3["sourceProvider"] = IntegrationMode.CreateSchema | IntegrationMode.GetData;
        //        state3["destinationProvider"] = IntegrationMode.TransferData;
        //        state3["tableName"] = tableName;
        //        state3[ContextState.SourcePath] = sourcePath;

        //        try
        //        {
        //            result = target.Run(state3);
        //            dt = state3["datatable"] as DataTable;
        //            if (dt == null || dt.Rows.Count == 0)
        //            {
        //                result = false;
        //                message = "没有读取到数据。";
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            result = false;
        //            message = ex.Message;
        //        }
        //    }
        //    return result;
        //}

        public bool ImportCSVData(string sourcePath, out DataTable dt, out string message)
        {
            return ImportCSVData(sourcePath, out dt, out message, new Hashtable());
        }

		public bool ImportCSVData(string sourcePath, out DataTable dt, out string message, Hashtable param)
		{
			dt = new DataTable();
			message = string.Empty;
			if (string.IsNullOrEmpty(sourcePath))
			{
				message = "数据文件路径为空";
				return false;
			}
			if (!File.Exists(sourcePath))
			{
				message = string.Format("数据文件路径[{0}]不存在", sourcePath);
				return false;
			}

			bool result;

			using (IntegrationEngine target = new IntegrationEngine())
			{
				DelimitedFileStorage sourceProvider = new DelimitedFileStorage("sourceProvider");
				target.Providers.Add(sourceProvider);
				DataTableStorage destinationProvider = new DataTableStorage("destinationProvider");
				target.Providers.Add(destinationProvider);

				IDictionary state3 = param;
				state3["sourceProvider"] = IntegrationMode.CreateSchema | IntegrationMode.GetData;
				state3["destinationProvider"] = IntegrationMode.TransferData;
				state3[ContextState.SourcePath] = sourcePath;
				state3[ContextState.FlatFileDelimiter] = ",";
				state3[ContextState.FlatFileSkipFirstLine] = true;
				state3[ContextState.FlatFileSkipLastLine] = false;

				try
				{
					result = target.Run(state3);
					dt = state3["datatable"] as DataTable;
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        result = false;
                        message = "没有读取到数据。";
                    }
				}
				catch (System.Exception ex)
				{
					result = false;
					message = ex.Message;
				}
			}
			return result;
		}

		public bool ExportData(DataTable dt, string directory, out string file,
		                                       out string message)
		{
			// TODO: left the choice to be CSV or EXCEL
			return TransferDataTableToCSVFile(dt, directory, null, out file,out message);
			//return TransferDataTableToExcel(dt, directory, null, out file, out message);
		}

		public bool ExportData<T>(BusinessObjectCollection<T> bocollection, string directory, out string file,
											   out string message) where T : BusinessObject
		{
			return TransferDataTableToCSVFile(bocollection.ToDataTable(), directory, null, out file, out message);
		}

		private static string GetTableNameFromSchemaFileName(string schemaPath)
		{
			int directoryIndex = schemaPath.LastIndexOf('\\') + 1;
			int extensionIndex = schemaPath.LastIndexOf('.');
			return schemaPath.Substring(directoryIndex, extensionIndex - directoryIndex);
		}
	}

	
}