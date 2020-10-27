using System;
using System.Data;
using System.Data.SqlClient;

namespace Infrustructure.Data.Integration
{
	internal class DatabaseStorage : StorageProviderBase
	{
		public DatabaseStorage(string providername) : base(providername)
		{
		}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal override void CreateDataSchema(IntegrationContext context)
		{
			string connectionstring = context.State[ContextState.DatabaseConnectionString] as string;
			if (string.IsNullOrEmpty(connectionstring))
				throw new ArgumentNullException(string.Format("argument [{0}] missed!", ContextState.DatabaseConnectionString));
			string tableName = context.State[ContextState.DatabaseTableName] as string;
			if (string.IsNullOrEmpty(tableName))
				throw new ArgumentNullException(string.Format("argument [{0}] missed!", ContextState.DatabaseTableName));

			DataTable dt = new DataTable(tableName);
			using (SqlConnection conn = new SqlConnection(connectionstring))
			{
				conn.Open();

				SqlCommand cmd2 = new SqlCommand(string.Format("SELECT * FROM {0} WHERE 1<>1;", tableName), conn);
				SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
				adapter.Fill(dt);
			}
			context.Schema = DataTableStorage.GetDataSchema(dt);
		}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal override void TransferData(IntegrationContext context)
		{
			string connectionstring = context.State[ContextState.DatabaseConnectionString] as string;
			if (string.IsNullOrEmpty(connectionstring))
				throw new ArgumentNullException(string.Format("argument [{0}] missed!", ContextState.DatabaseConnectionString));
			if (null == context.Schema)
				throw new System.Exception("data schema missed!");
			if (context.SuccessCount == 0)
				throw new System.Exception("data missed!");
			string tableName = context.State[ContextState.DatabaseTableName] as string;
			if (string.IsNullOrEmpty(tableName))
				throw new ArgumentNullException(string.Format("argument [{0}] missed!", ContextState.DatabaseTableName));
			bool isTruncate = (bool) context.State[ContextState.DatabaseTruncateTable];

			if (isTruncate)
				TruncateData(connectionstring, tableName);

			using (SqlConnection conn = new SqlConnection(connectionstring))
			using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
			{
				conn.Open();

				DataTable dt = new DataTable(tableName);
				SqlCommand cmd2 = new SqlCommand(string.Format("SELECT * FROM {0} WHERE 1<>1;", tableName), conn);
				SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
				adapter.Fill(dt);
				MappingColumns(bulkCopy, dt, context.Schema);
				DataTableStorage.FillDestinationDataItems(dt, context.Data);
				bulkCopy.DestinationTableName = tableName;
				bulkCopy.BatchSize = dt.Rows.Count;
				bulkCopy.WriteToServer(dt);
			}
		}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        protected virtual void TruncateData(string connectionstring, string tableName)
		{
			using (SqlConnection conn = new SqlConnection(connectionstring))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(string.Format("TRUNCATE TABLE {0};", tableName), conn);
				cmd.ExecuteNonQuery();
			}
		}

		private static void MappingColumns(SqlBulkCopy bulkCopy, DataTable dt, DataSchema schema)
		{
			foreach (DataColumn column in dt.Columns)
			{
				if (Array.Exists(schema.Fields, delegate(DataSchemaField field)
				                                	{
				                                		if (field.Skip)
				                                			return false;
				                                		else
				                                			return
				                                				field.Destination.Equals(column.ColumnName,
				                                				                         StringComparison.
				                                				                         	InvariantCultureIgnoreCase);
				                                	}))
				{
					SqlBulkCopyColumnMapping map = new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName);
					bulkCopy.ColumnMappings.Add(map);
				}
			}
		}
	}
}