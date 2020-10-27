using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Infrustructure.Access
{
    /// <summary>
    /// Oledb connection extend methods
    /// </summary>
    public static class ExtandOleDbConnectionClass
    {
        /// <summary>
        /// Create OleDbCommand
        /// </summary>
        /// <param name="oleDbConn"></param>
        /// <returns></returns>
        public static OleDbCommand CreateCommand(this OleDbConnection oleDbConn)
        {
            return oleDbConn.CreateCommand();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oleDbConn"></param>
        /// <returns></returns>
        public static OleDbCommand CreateCommandByQueryString(this OleDbConnection oleDbConn, string commandString)
        {
            OleDbCommand command = new OleDbCommand(commandString, oleDbConn);
            command.CommandType = CommandType.Text;
            return command;
        }

        /// <summary>
        /// Create OldDb command By ProcedureName
        /// </summary>
        /// <param name="oleDbConn"></param>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public static OleDbCommand CreateCommandByProcedureName(this OleDbConnection oleDbConn, string procedureName)
        {
            OleDbCommand command = new OleDbCommand(procedureName, oleDbConn);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        /// <summary>
        /// Add Input Parameter
        /// </summary>
        /// <param name="oleDbConn"></param>
        /// <param name="command"></param>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        public static void AddInputParameter(this OleDbConnection oleDbConn, OleDbCommand command, string paramName, object value)
        {
            command.Parameters.AddWithValue(paramName, value);
        }

        /// <summary>
        /// Add Output Parameter
        /// </summary>
        /// <param name="oleDbConn"></param>
        /// <param name="command"></param>
        /// <param name="paramName"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        public static void AddOutputParameter(this OleDbConnection oleDbConn, OleDbCommand command, string paramName, OleDbType type, int size)
        {
            command.Parameters.Add(new OleDbParameter(paramName, type, size));
            command.Parameters[paramName].Direction = ParameterDirection.Output;
        }

        /// <summary>
        /// Add Return Parameter
        /// </summary>
        /// <param name="oleDbConn"></param>
        /// <param name="command"></param>
        /// <param name="paramName"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        public static void AddReturnParameter(this OleDbConnection oleDbConn, OleDbCommand command, string paramName, OleDbType type, int size)
        {
            command.Parameters.Add(new OleDbParameter(paramName, type));
            command.Parameters[paramName].Direction = ParameterDirection.ReturnValue;
        }

        /// <summary>
        /// Execute Scale
        /// </summary>
        /// <param name="oleDbConn"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static object ExecuteScale(this OleDbConnection oleDbConn, OleDbCommand command)
        {
            try
            {
                oleDbConn.Open();
                return command.ExecuteScalar();
            }
            finally
            {
                oleDbConn.Close();
            }
        }

        /// <summary>
        /// Execute no Query
        /// </summary>
        /// <param name="oleDbConn"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(this OleDbConnection oleDbConn, OleDbCommand command)
        {
            try
            {
                oleDbConn.Open();
                return command.ExecuteNonQuery();
            }
            finally
            {
                oleDbConn.Close();
            }
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <param name="oleDbConn"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static OleDbDataReader ExecuteReader(this OleDbConnection oleDbConn, OleDbCommand command)
        {
            try
            {
                oleDbConn.Open();
                return command.ExecuteReader();
            }
            finally
            {
                // oleDbConn.Close();
            }
        }

        /// <summary>
        /// Execute Table
        /// </summary>
        /// <param name="oleDbConn"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static DataTable ExecuteTable(this OleDbConnection oleDbConn, OleDbCommand command)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }
}
