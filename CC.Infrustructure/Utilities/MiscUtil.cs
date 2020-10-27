using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using Infrustructure.Data.Integration;
using System.ComponentModel;
using System.Globalization;
using System.Collections;

namespace Infrustructure.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// TODO: refactoring with Infrustructure.Data.Integration.ValidationUtils
    /// </remarks>
    public static class MiscUtil
    {
        public static string ResolveFilePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            // ����·��
            if (File.Exists(path))
                return path;

            // ���������·��

            // website
            if (System.Web.HttpContext.Current != null)
            {
                string path1 = System.Web.HttpContext.Current.Server.MapPath(path);
                if (File.Exists(path1))
                    return path1;
            }

            // winform
            string rootlocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            rootlocation = rootlocation.Substring(0, rootlocation.LastIndexOf('\\') + 1);
            string path2 = Path.Combine(rootlocation, path.TrimStart('\\'));
            if (File.Exists(path2))
                return path2;

            return string.Empty;
        }

        public static string ResolveFolderPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            // ����·��
            if (Directory.Exists(path))
                return path;

            // ���������·��

            // website
            if (System.Web.HttpContext.Current != null)
            {
                string path1 = System.Web.HttpContext.Current.Server.MapPath(path);
                if (Directory.Exists(path1))
                    return path1;
            }

            // winform
            string rootlocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            rootlocation = rootlocation.Substring(0, rootlocation.LastIndexOf('\\') + 1);
            string path2 = Path.Combine(rootlocation, path.TrimStart('\\'));
            if (Directory.Exists(path2))
                return path2;

            return string.Empty;
        }

        public static T Clone<T>(T oldObj) where T: class
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                bFormatter.Serialize(stream, oldObj);
                stream.Seek(0, SeekOrigin.Begin);
                T clone = (T)bFormatter.Deserialize(stream);
                return clone;
            }
        }

        public static void EnsureColumn(DataTable dt, string columnname)
        {
            if (!dt.Columns.Contains(columnname))
                throw new System.Exception(string.Format("�����������Ϊ'{0}'��������", columnname));
        }

        public static void EnsureColumns(DataTable dt, params string[] columnnames)
        {
            string names = string.Empty;
            foreach (string name in columnnames)
	        {
                if (!dt.Columns.Contains(name))
                {
                    names += name + ",";
                }
	        }
            if (!string.IsNullOrEmpty(names))
                throw new System.Exception(string.Format("�����������������:({0})", names.TrimEnd(',')));
        }

        public static void EnsureColumnLength(DataRow dr, int rowindex, string columnname, int length)
        {
            string colvalue = dr[columnname].ToString();
            if (colvalue.Length != length)
                throw new System.Exception(string.Format("��[{0}]�е�������'{1}'��ֵ[{2}]�ĳ��ȱ���Ϊ{3}���ַ�", rowindex, columnname, colvalue, length));
        }

        public static void EnsureColumnMaxLength(DataRow dr, int rowindex, string columnname, int maxlength)
        {
            string colvalue = dr[columnname].ToString();
            if (colvalue.Length > maxlength)
                throw new System.Exception(string.Format("��[{0}]�е�������'{1}'��ֵ[{2}]�ĳ��Ȳ��ܳ���{3}���ַ�", rowindex, columnname, colvalue, maxlength));
        }

        public static void EnsureColumnNotEmpty(DataRow dr, int rowindex, string columnname)
        {
            string colvalue = dr[columnname].ToString();
            if (colvalue.Length == 0)
                throw new System.Exception(string.Format("��[{0}]�е�������'{1}'��ֵ����Ϊ��", rowindex, columnname));
        }

        public static void EnsureColumnType<T>(DataRow dr, int rowindex, string columnname)
        {
            string colvalue = dr[columnname].ToString();
            Type targetType = typeof(T);
            if (colvalue.Length == 0 && ValidationUtils.IsTheTargetTypeAValueTypeDifferentFromString(targetType))
            {
                throw new System.Exception(string.Format("��[{0}]�е�������'{1}'��ֵ'{2}'����ת��Ϊ{3}", rowindex, columnname, colvalue, targetType));
            }

            object convertedValue = null;
            try
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(targetType);
                convertedValue = typeConverter.ConvertFromString(null, CultureInfo.CurrentCulture, colvalue);
            }
            catch
            {   
            }

            string typename;

            if (targetType == typeof(int))
                typename = "����";
            else if (targetType == typeof(bool))
                typename = "���ֵ";
            else if (targetType == typeof(decimal))
                typename = "С��";
            else if (targetType == typeof(DateTime))
                typename = "ʱ��";
            else
                typename = targetType.ToString();

            if(convertedValue == null)
                throw new System.Exception(string.Format("��[{0}]�е�������'{1}'��ֵ'{2}'����ת��Ϊ{3}", rowindex, columnname, colvalue, typename));
        }

        public static void EnsureColumnDomain<T>(DataRow dr, int rowindex, string columnname, params T[] domains)
        {
            EnsureColumnType<T>(dr, rowindex, columnname);
            bool match = false;
            T colvalue = (T)dr[columnname];
            string info = string.Empty;
            foreach (T element in domains)
            {
                if (element.Equals(colvalue))
                {
                    match = true;
                }
                info += string.Format("{0},",typeof(T));
            }
            if (!match)
                throw new System.Exception(string.Format("��[{0}]�е�������'{1}'��ֵ'{2}'����λ��({3})", rowindex, columnname, colvalue, info.TrimEnd(',')));
        }

        public static void EnsureColumnMinValue<T>(DataRow dr, int rowindex, string columnname, T minvalue) where T : IComparable
        {
            EnsureColumnType<T>(dr, rowindex, columnname);

            T colvalue = (T)dr[columnname];
            if(colvalue.CompareTo(minvalue)< 0)
                throw new System.Exception(string.Format("��[{0}]�е�������'{1}'��ֵ'{2}'���벻С��({3})", rowindex, columnname, colvalue, minvalue));
        }

        public static string EnsureDataTableQualify(DataTable dt, string schemapath)
        {
            return EnsureDataTableQualify(dt, schemapath, new Hashtable());
        }

        public static string EnsureDataTableQualify(DataTable dt, string schemapath, Hashtable param)
        {
            if (param == null)
                param = new Hashtable();
            try
            {
                IntegrationContext context = new IntegrationContext(param);
                context.Schema = ValidationUtils.GetDataSchemaFromFile(schemapath);
                if (context.Schema == null)
                    return string.Format("�޷�������[{0}]��schema", schemapath);
                DataTableStorage.GetDataFromSourceDataTable(dt, context);
                return context.Message;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
