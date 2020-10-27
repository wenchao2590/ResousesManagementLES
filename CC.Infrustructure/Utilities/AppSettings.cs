//+-------------------------------------------------------------------+
//+ Name: AppSettings	  
//+ Function:    ����LES_Monitor.exe.config�ļ�
//+ Author:       xiaoxiongliu
//+ Date:           2008-7-19     
//+-------------------------------------------------------------------+
//+ Change History:
//+ Date            Who       		Chages Made        Comments
//+-------------------------------------------------------------------+
//+  2008-7-19            xiaoxiongliu       Init Created
//+-------------------------------------------------------------------+
//+-------------------------------------------------------------------+
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Data ;

using System.Configuration;
using System.IO;
using Infrustructure.Logging;
namespace Infrustructure.Utilities
{
    public class APInfo
    {
        private string apName;
        public string ApName
        {
            get
            {
                return this.apName;
            }
            set
            {
                this.apName = value;
            }
        }

        private string apAddress;
        public string ApAddress
        {
            get
            {
                return this.apAddress;
            }
            set
            {
                this.apAddress = value;
            }
        }

        private string alarmTime;
        public string AlarmTime
        {
            get
            {
                return this.alarmTime;
            }
            set
            {
                this.alarmTime = value;
            }
        }

        private string alarmStatus;
        public string AlarmStatus
        {
            get
            {
                return this.alarmStatus;
            }
            set
            {
                this.alarmStatus = value;
            }
        }

        public string lastFileLocation;
        public string LastFileLocation
        {
            get
            {
                return this.lastFileLocation;
            }
            set
            {
                this.lastFileLocation = value;
            }
        }
        public string lastCheckTime;
        public string LastCheckTime
        {
            get
            {
                return this.lastCheckTime;
            }
            set
            {
                this.lastCheckTime = value;
            }
        }

    }

    public class AppSettings
    {
        const string elementName = "APInfo";

        //��ȡ���ݿ�������ַ���
        public static string GetDatabaseConnString(string DatabaseName)
        {
            //try
            //{

                //ʵ����XML�ĵ�
                XmlDocument Doc = new XmlDocument();
                //ʵ����������
                string filepath = Application.ExecutablePath + ".config";
                //����app.config�ļ�
                Doc.Load(filepath);
                //������Ӧ�ڵ�ֵ
                return  Doc.SelectSingleNode("/configuration/connectionStrings/add[@name='" + DatabaseName + "']").Attributes["connectionString"].Value;
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }
        //�趨���ݿ�������ַ���
        public static void SetDatabaseConnString(string DatabaseName, string DatabaseConnString)
        {
            //try
            //{ 
                //ʵ����XML�ĵ�
                XmlDocument Doc=new XmlDocument(); 
                //ʵ����������
                string filepath = Application.ExecutablePath + ".config";
                //����app.config�ļ�
                Doc.Load(filepath);
                //������Ӧ�ڵ�ֵ
                Doc.SelectSingleNode("/configuration/connectionStrings/add[@name='" + DatabaseName + "']").Attributes["connectionString"].Value = DatabaseConnString;
                //��������
                Doc.Save(filepath);

                Doc.Load(filepath); 
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        
        }
        //��ȡValueֵ 
        public static string GetConfigString(string key)
        {
            //try
            //{

                //ʵ����XML�ĵ�
                XmlDocument Doc = new XmlDocument();
                //ʵ����������
                string filepath = Application.ExecutablePath + ".config";
                //����app.config�ļ�
                Doc.Load(filepath);
                //������Ӧ�ڵ�ֵ
                return Doc.SelectSingleNode("/configuration/appSettings/add[@key='" + key + "']").Attributes["value"].Value;
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }
        //д���� 
        public static void SetValue(string AppKey, string AppValue)
        {
            //try
            //{
                XmlDocument xDoc = new XmlDocument();
                //��ȡ��ִ���ļ���·�������� 
                xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
                XmlNode xNode;
                XmlElement xElem1;
                XmlElement xElem2;
                //��ȡappSettings�ڵ�
                xNode = xDoc.SelectSingleNode("//appSettings");

                //��ȡappSettings�ڵ��µ�Ԫ�أ���Ԫ�ش������޸�Ԫ�ص�ֵ�����򴴽�Ԫ�ز��趨ֵ
                xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
                if (xElem1 != null)
                    xElem1.SetAttribute("value", AppValue);
                else
                {
                    xElem2 = xDoc.CreateElement("add");
                    xElem2.SetAttribute("key", AppKey);
                    xElem2.SetAttribute("value", AppValue);
                    xNode.AppendChild(xElem2);
                }
                xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");//����config�ļ�
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }

        #region  д��xml
        //����fileName:�ļ���;ElementName:Ԫ����;AttributeName:������;AttValue:����ֵ
        public static void WriteXml(string direcoryName, string fileName, string ElementName, string AttributeName, string AttValue)
        {

             WriteXml(direcoryName,fileName, ElementName, AttributeName, AttValue, "");               

        }
        #endregion

        #region  д��xml
        //����fileName:�ļ���;ElementName:Ԫ����;AttributeName:������;AttValue:����ֵ
        public static void WriteXml(string direcoryName, string fileName, string ElementName, string AttributeName, string AttValue, string str)
        {
            //��ȡҪ�����ļ���·��
            string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + direcoryName + "\\" + fileName + ".xml";
            try
            {               
                //�ж��ļ��Ƿ����
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //��ȡ��ִ���ļ���·�������� 
                    xDoc.Load(fileInfo);
                    XmlNode xNode;
                    XmlElement xElem1;
                    XmlElement xElem2;
                    xNode = xDoc.SelectSingleNode("//root");

                    xElem1 = (XmlElement)xNode.SelectSingleNode("//" + ElementName+"[@"+AttributeName+"]");
                    //��ȡroot�ڵ��µ�Ԫ�أ���Ԫ�ش������޸�Ԫ�ص�ֵ�����򴴽�Ԫ�ز��趨ֵ
                    if (xElem1 != null)
                        xElem1.SetAttribute(AttributeName,AttValue);
                    else
                    {
                        xElem2 = xDoc.CreateElement(ElementName);
                        XmlNode xmlAttribute = xDoc.CreateNode(XmlNodeType.Attribute, AttributeName, "");
                        xmlAttribute.Value = AttValue;
                        xElem2.Attributes.SetNamedItem(xmlAttribute);
                        xNode.AppendChild(xElem2); 
                    }
                    xDoc.Save(fileInfo);//�����ļ�
                }
                else
                {
                    CreateXml(fileInfo, ElementName, AttributeName, AttValue);//�ļ�������ʱ�������ļ�
                }
            }
            catch (System.Exception ex)
            {
                try
                {
                    //���ļ��Ѿ����ڣ������ݲ���ȷ����ɾ�����ļ������������ɸ��ļ�
                    Logger.LogError("CC.Utility", "WriteXml", "AppSettings", "��¼xml��־���󣬸��ļ��Ѿ����ڣ������ݲ���ȷ��", ex);
                    File.Delete(fileInfo);
                    CreateXml(fileInfo, ElementName, AttributeName, AttValue);//�ļ�������ʱ�������ļ�
                }
                catch (System.Exception ex1)
                {
                    Logger.LogError("CC.Utility", "WriteXml", "AppSettings", "����xml��־�ļ�����", ex1);
                }
            }
        }
        #endregion

        //��xml
        //����fileName:�ļ���;ElementName:Ԫ����;AttributeName:������;
        public static string xReadXml(string direcoryName, string fileName, string ElementName, string AttributeName)
        {
            string strAttValue = string.Empty;

            //��ȡ��ִ���ļ���·�������� 
            string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + direcoryName + "\\" + fileName + ".xml";
            //�ж��ļ��Ƿ����
            if (File.Exists(fileInfo))
            {
                XmlDocument xDoc = new XmlDocument();
                //��ȡ��ִ���ļ���·�������� 
                xDoc.Load(fileInfo);
                XmlNode xNode;
                XmlElement xElem1;
                xNode = xDoc.SelectSingleNode("//root");
                //xDoc .ReadNode(XmlReader XmlReader\

                //��ȡRoot�ڵ��µ�Ԫ��
                xElem1 = (XmlElement)xNode.SelectSingleNode("//" + ElementName + "[@" + AttributeName + "]");
                   
                strAttValue = xElem1.Attributes[AttributeName].Value;
            }                

            return strAttValue;
        }

        #region ��xml
        //����fileName:�ļ���;ElementName:Ԫ����;AttributeName:������;
        public static string ReadXml(string direcoryName, string fileName, string ElementName, string AttributeName)
        {
            //try
            //{
                return ReadXml(direcoryName,fileName, ElementName, AttributeName, "");     
            //}
            //catch(System.Exception ex)
            //{
            //    throw ex;
            //}
        }
        #endregion

        #region ��xml
        //����fileName:�ļ���;ElementName:Ԫ����;AttributeName:������;AttValue:����ֵ
        public static string ReadXml(string direcoryName,string fileName, string ElementName, string AttributeName,string AttValue)
        {
            string strAttValue = string.Empty;
            //try
            //{
                //��ȡ��ִ���ļ���·�������� 
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + direcoryName + "\\" + fileName + ".xml";
                //�ж��ļ��Ƿ����
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //��ȡ��ִ���ļ���·�������� 
                    xDoc.Load(fileInfo);
                    XmlNode xNode;
                    XmlElement xElem1;
                    XmlElement xElem2;
                    xNode = xDoc.SelectSingleNode("//root");

                    //��ȡRoot�ڵ��µ�Ԫ��
                    xElem1 = (XmlElement)xNode.SelectSingleNode("//" + ElementName+"[@"+AttributeName+"]");  

                    //Ԫ�ش������ȡԪ�ص�ֵ�����򴴽���Ԫ�ز��趨��ʼֵ
                    if(xElem1==null)  
                    {
                        xElem2 = xDoc.CreateElement(ElementName);
                        XmlNode xmlAttribute = xDoc.CreateNode(XmlNodeType.Attribute, AttributeName, "");
                        xmlAttribute.Value = AttValue;
                        xElem2.Attributes.SetNamedItem(xmlAttribute);
                        xNode.AppendChild(xElem2);
                        xElem1 = xElem2;
                        xDoc.Save(fileInfo);
                    }
                    strAttValue = xElem1.Attributes[AttributeName].Value;

                    xDoc = null;
                }
                else
                {
                    CreateXml(fileInfo, ElementName, AttributeName, AttValue);//�ļ�������ʱ�������ļ�
                    strAttValue = AttValue;
                }
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
            return strAttValue;
        }
        

        //��ȡ������Ϣ
        //����fileName:�ļ���;ElementName:Ԫ����;AttributeName:������;
        public static Hashtable ReadXml(string direcoryName, string fileName, string ElementName)
        {
            Hashtable hs = new Hashtable();
            //try
            //{               
                XmlDocument xDoc = new XmlDocument();
                //��ȡ��ִ���ļ���·�������� 
                xDoc.Load(System.Windows.Forms.Application.StartupPath + "\\" + direcoryName + "\\" + fileName + ".xml");
                XmlNodeList xmlnList = xDoc.SelectNodes("//root//plant//" + ElementName);
                if (xmlnList.Count > 0)
                {
                    for (int i = 0; i < xmlnList.Count; i++)
                    {
                        hs.Add(xmlnList[i].Attributes["id"].Value.ToString().ToLower(), xmlnList[i].Attributes["pmcZoneID"].Value.ToString().ToLower());
                    }                
                }
                return hs;
            //}
            //catch(System.Exception ex)
            //{
            //    throw ex;
            //}
        }

        // ��XML�ļ��л�ȡ���ݲ���DataSet���ͷ���
        //���� direcoryName��Ŀ¼���ƣ�fileName���ļ���
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static DataSet GetDataSetFromXml(string direcoryName, string fileName)
        {

                DataSet ds = new DataSet();
                //���ļ��л�ȡ����
                ds.ReadXml(System.Windows.Forms.Application.StartupPath + "\\" + direcoryName + "\\" + fileName + ".xml");
                return ds;

        }

        //����datatable���ݲ�����hashtable
        //����dt��Ҫ���˵����ݱ�Column�����Ǹ���Ϊ������filterValue����������
        public static Hashtable GetHashtableFromDatatable(DataTable dt, string Column, string filterValue)
        {
            Hashtable hs = new Hashtable();
            //try
            //{
                //��������
                DataRow[] rows = dt.Select(Column + "='" + filterValue + "'");
                //�����˵�����д��Hashtable
                if (rows.Length > 0)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        hs.Add(rows[i]["id"].ToString().ToLower(), rows[i]["pmcZoneID"].ToString().ToLower());
                    }
                }
                return hs;

            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }

        //����datatable���ݲ�����hashtable
        //����dt��Ҫ���˵����ݱ�Column�����Ǹ���Ϊ������filterValue����������
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static DataTable GetDatatableFromDatatable(DataTable dt, string Column, string filterValue)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("LESWorkShopID");
            dataTable.Columns.Add("PMCWorkShopID");
            dataTable.Columns.Add("DeleteWorkShop");

                //��������
                DataRow[] rows = dt.Select(Column + "='" + filterValue + "'");
                //�����˵�����д��Hashtable
                if (rows.Length > 0)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow row = dataTable.NewRow();
                        row["LESWorkShopID"] = rows[i]["id"].ToString().ToLower();
                        row["PMCWorkShopID"] = rows[i]["pmcZoneID"].ToString().ToLower();
                        dataTable.Rows.Add(row);
                    }
                }
                return dataTable;


        }

        //��ȡ������Ϣ
        //����fileName:�ļ���;ElementName:Ԫ����;AttributeName:������
        public static string ReadPlantXml(string direcoryName, string fileName, string ElementName, string AttributeName)
        {
            //try
            //{
                XmlDocument xDoc = new XmlDocument();
                //��ȡ��ִ���ļ���·�������� 
                xDoc.Load(System.Windows.Forms.Application.StartupPath + "\\" + direcoryName + "\\" + fileName + ".xml");
                XmlNode xNode;
                xNode = xDoc.SelectSingleNode("//" + ElementName);
                return xNode.Attributes[AttributeName].Value;                
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }
        #endregion

        #region ����XML�ļ�
        //����fileName:�ļ���;ElementName:Ԫ����;AttributeName:������;AttValue:����ֵ
        private static void CreateXml(string fileName, string ElementName, string AttributeName, string AttValue)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration xn = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xDoc.AppendChild(xn);
            XmlNode root = xDoc.CreateElement("root");
            xDoc.AppendChild(root);
            XmlNode xmln = xDoc.CreateElement(ElementName);
            XmlNode xmlNode = xDoc.CreateNode(XmlNodeType.Attribute, AttributeName, "");
            xmlNode.Value = AttValue;
            xmln.Attributes.SetNamedItem(xmlNode);
            root.AppendChild(xmln);
            xDoc.Save(fileName); 
        }

        //����fileName:�ļ���
        private static void CreateXml(string fileName)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration xn = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xDoc.AppendChild(xn);
            XmlNode root = xDoc.CreateElement("root");
            xDoc.AppendChild(root);            
            xDoc.Save(fileName);
        }
        #endregion
        #region ����cconfigĿ¼
        public static void createConfig(string directoyName)
        {
            //Ŀ¼config������ʱ������Ŀ¼
            string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoyName;
            //try
            //{
                if (!Directory.Exists(fileInfo))
                {
                    Directory.CreateDirectory(fileInfo);//����configĿ¼
                }
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }
        #endregion

        //�������õ������ַ���
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        public static void  SaveConnStr(string directoryName, string fileName, DataGridView dgv, string ElementName,out string returnInfo)
        {
            try
            {
                string dgvName = dgv.Name;
                //��ȡҪ�����ļ���·��
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoryName + "\\" + fileName + ".xml";
                //�ж��ļ��Ƿ����
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //��ȡ��ִ���ļ���·�������� 
                    xDoc.Load(fileInfo);
                    XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + ElementName);
                    //�����õ�ֵ���浽XML�ļ�
                    if (dgv.Rows.Count > 0 && xmlnList.Count > 0)
                    {
                        foreach (XmlNode node in xmlnList)
                        {
                            foreach (DataGridViewRow row in dgv.Rows)
                            {
                                if (dgvName == "m_dgvMAS")
                                {
                                    if (node.Attributes["id"].Value == row.Cells["plant"].Value.ToString())
                                    {
                                        node.Attributes["masConnStr"].Value = row.Cells["masConnStr"].Value.ToString();
                                        break;
                                    }
                                }
                                if (dgvName == "m_dgvPMC")
                                {
                                    if (node.Attributes["id"].Value == row.Cells["pmcplant"].Value.ToString())
                                    {
                                        node.Attributes["pmcConnStr"].Value = row.Cells["pmcConnStr"].Value.ToString();
                                        break;
                                    }
                                }
                            }
                        }                        
                    }
                    //�����ļ�
                    xDoc.Save(fileInfo);
                    returnInfo = "����ɹ�";
                }
                else
                {
                    returnInfo = fileInfo+"������";
                }
            }
            catch (System.Exception ex)
            {
                returnInfo = "����ʧ��";
                throw ex;
            }
        }


        //�������õ������ַ���
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        public static void SaveConnStr(string directoryName, string fileName,  string ElementName, string plantNo,string connStr,string moduleNm,out string returnInfo)
        {
            try
            {
                //��ȡҪ�����ļ���·��
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoryName + "\\" + fileName + ".xml";
                //�ж��ļ��Ƿ����
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //��ȡ��ִ���ļ���·�������� 
                    xDoc.Load(fileInfo);
                    XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + ElementName);
                    //�����õ�ֵ���浽XML�ļ�
                    if ( xmlnList.Count > 0)
                    {
                        foreach (XmlNode node in xmlnList)
                        {
                            if (moduleNm == "MAS")
                            {
                                if (node.Attributes["id"].Value == plantNo)
                                {
                                    node.Attributes["masConnStr"].Value = connStr;
                                    break;
                                }
                            }
                            if (moduleNm == "PMC")
                            {
                                if (node.Attributes["id"].Value == plantNo)
                                {
                                    node.Attributes["pmcConnStr"].Value = connStr;
                                    break;
                                }
                            }
                        }
                    }
                    //�����ļ�
                    xDoc.Save(fileInfo);
                    returnInfo = "����ɹ�";
                }
                else
                {
                    returnInfo = fileInfo + "������";
                }
            }
            catch (System.Exception ex)
            {
                returnInfo = "����ʧ��";
                throw ex;
            }
        }

        //�Ƴ��ڵ�
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        public static void RemoveNode(string directoryName, string fileName, string ElementName, string plantNo, string moduleNm, out string returnInfo)
        {
            try
            {
                //��ȡҪ�����ļ���·��
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoryName + "\\" + fileName + ".xml";
                //�ж��ļ��Ƿ����
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //��ȡ��ִ���ļ���·�������� 
                    xDoc.Load(fileInfo);
                    XmlNode rootNode = xDoc.SelectSingleNode("//root");
                    XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + ElementName);
                    //�����õ�ֵ���浽XML�ļ�
                    if (xmlnList.Count > 0)
                    {
                        foreach (XmlNode node in xmlnList)
                        {
                            if (moduleNm == "MAS")
                            {
                                if (node.Attributes["id"].Value == plantNo)
                                {
                                    rootNode.RemoveChild(node);
                                    break;
                                }
                            }
                            if (moduleNm == "PMC")
                            {
                                if (node.Attributes["id"].Value == plantNo)
                                {
                                    rootNode.RemoveChild(node);
                                    break;
                                }
                            }
                        }
                    }
                    //�����ļ�
                    xDoc.Save(fileInfo);
                    returnInfo = "ɾ���ɹ�";
                }
                else
                {
                    returnInfo = fileInfo + "������";
                }
            }
            catch (System.Exception ex)
            {
                returnInfo = "ɾ��ʧ��";
                throw ex;
            }
        }

        //������ӵ��½ڵ�
        public static void AddAndSaveConnStr(string directoryName, string fileName, string ElementName, string AttributeName1, string AttributeName2, string value1, string value2, out string returnInfo)
        {
            try
            {
                AppSettings app = new AppSettings();
                bool IsExist = app.isExistNode(directoryName, fileName, ElementName, AttributeName1, value1, out returnInfo);
                if (!IsExist && returnInfo =="")
                {
                    //��ȡҪ�����ļ���·��
                    string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoryName + "\\" + fileName + ".xml";
                    //�ж��ļ��Ƿ����
                    if (File.Exists(fileInfo))
                    {
                        XmlDocument xDoc = new XmlDocument();
                        //��ȡ��ִ���ļ���·�������� 
                        xDoc.Load(fileInfo);
                        XmlNode xNode;
                        XmlElement xElem;
                        xNode = xDoc.SelectSingleNode("//root");
                        xElem = xDoc.CreateElement(ElementName);
                        XmlNode xmlAttribute1 = xDoc.CreateNode(XmlNodeType.Attribute, AttributeName1, "");
                        xmlAttribute1.Value = value1;
                        xElem.Attributes.SetNamedItem(xmlAttribute1);
                        XmlNode xmlAttribute2 = xDoc.CreateNode(XmlNodeType.Attribute, AttributeName2, "");
                        xmlAttribute2.Value = value2;
                        xElem.Attributes.SetNamedItem(xmlAttribute2);
                        xNode.AppendChild(xElem);
                        //�����ļ�
                        xDoc.Save(fileInfo);
                        returnInfo = "����ɹ�";
                    }
                    else
                    {
                        returnInfo = fileInfo + "������";
                    }
                }
            }
            catch 
            {
                returnInfo = "����ʧ��";
                //throw ex;
            }
        }
        //��⹤���Ƿ����
        private   bool isExistNode(string directoryName, string fileName, string ElementName, string AttributeName, string value, out string returnInfo)       
        {
            bool isExist = false;
            string strInfo = string.Empty;
          
                //��ȡҪ�����ļ���·��
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoryName + "\\" + fileName + ".xml";
                //�ж��ļ��Ƿ����
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //��ȡ��ִ���ļ���·�������� 
                    xDoc.Load(fileInfo);
                    XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + ElementName);
                    //���ڵ��Ƿ����
                    foreach (XmlNode node in xmlnList)
                    {
                        if (node.Attributes[AttributeName].Value ==value)
                        {
                            isExist = true;
                            strInfo = "��������" + value + "�������ļ���" + fileInfo + "���Ѵ���";
                            break;
                        }
                    }                  
                    
                }
                else
                {
                    strInfo = fileInfo + "������";
                }
                returnInfo = strInfo;
                return isExist;

        }

        //���湤���ĳ���
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        public static void SaveWorkShop(string directoryName, string fileName, string ElementName, string AttributeName, string value, DataGridView dgv, out string returnInfo)
        {
            try
            {
                //��ȡҪ�����ļ���·��
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoryName + "\\" + fileName + ".xml";
                //�ж��ļ��Ƿ����
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //��ȡ��ִ���ļ���·�������� 
                    xDoc.Load(fileInfo);
                    XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + ElementName);
                    //�����õ�ֵ���浽XML�ļ�
                    if (dgv.Rows.Count > 0 && xmlnList.Count > 0)
                    {
                        foreach (XmlNode node in xmlnList)
                        {
                            if (node.Attributes[AttributeName].Value == value)
                            {
                                string connStr =node.Attributes["pmcConnStr"].Value;
                                node.RemoveAll();

                                XmlNode xmlMAttribute1 = xDoc.CreateNode(XmlNodeType.Attribute, "id", "");
                                xmlMAttribute1.Value = value;
                                node.Attributes.SetNamedItem(xmlMAttribute1);
                                XmlNode xmlMAttribute2 = xDoc.CreateNode(XmlNodeType.Attribute, "pmcConnStr", "");
                                xmlMAttribute2.Value = connStr;
                                node.Attributes.SetNamedItem(xmlMAttribute2);

                                foreach (DataGridViewRow row in dgv.Rows)
                                {
                                    if (row != null)
                                    {
                                        if (row.Cells["LESWorkShopID"].Value !=null && row.Cells["LESWorkShopID"].Value.ToString() != "" && row.Cells["PMCWorkShopID"].Value.ToString() != "")
                                        {
                                            XmlElement xElem;
                                            xElem = xDoc.CreateElement("workshop");
                                            XmlNode xmlAttribute1 = xDoc.CreateNode(XmlNodeType.Attribute, "id", "");
                                            xmlAttribute1.Value = row.Cells["LESWorkShopID"].Value.ToString();
                                            xElem.Attributes.SetNamedItem(xmlAttribute1);
                                            XmlNode xmlAttribute2 = xDoc.CreateNode(XmlNodeType.Attribute, "pmcZoneID", "");
                                            xmlAttribute2.Value = row.Cells["PMCWorkShopID"].Value.ToString();
                                            xElem.Attributes.SetNamedItem(xmlAttribute2);
                                            node.AppendChild(xElem);
                                        }
                                    }
                                }
                                break;
                            }                           
                        }
                    }
                    //�����ļ�
                    xDoc.Save(fileInfo);
                    returnInfo = "����ɹ�";
                }
                else
                {
                    returnInfo = fileInfo + "������";
                }
            }
            catch (System.Exception ex)
            {
                returnInfo = "����ʧ��";
                throw ex;
            }
        }

        #region AP��ؽڵ����
        /// <summary>
        /// ��Ҫ�������Ϣ��AP���ơ�·��������ʱ�䡢�Ƿ񱨾����ϴμ���ļ���λ�á��ϴμ��ʱ���
        /// </summary>
        /// <param name="apInfo">AP���������Ϣ</param>
        /// <returns>������</returns>
        public bool SaveAPNote(APInfo apInfo)
        {
            try
            {
                
                //��ȡҪ�����ļ���·��
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + AppConst.directoryAppConfig + "\\" + AppConst.APConfig + ".xml";
                //�ж��ļ��Ƿ����
                if (!File.Exists(fileInfo))
                {
                    //�����ļ�
                    CreateXml(fileInfo);
                }

                XmlDocument xDoc = new XmlDocument();
                //��ȡXML����
                xDoc.Load(fileInfo);
                XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + elementName);
                XmlNode rootNode = xDoc.SelectSingleNode("//root");

                int rowIndex = 0;
                int maxIndex = 0;
                //�Ƴ��Ѿ����ڵĽڵ�
                foreach (XmlNode node in xmlnList)
                {
                    if (node.Attributes["APName"].Value.ToUpper() == apInfo.ApName.ToUpper())
                    {
                        //���ڸýڵ㣬��ȡ������ϴ��ļ�λ�ú��ϴ��ļ����ʱ��
                        if (string.IsNullOrEmpty(apInfo.LastFileLocation))
                        {
                            apInfo.LastFileLocation = node.Attributes["LastFileLocation"].Value;
                            apInfo.LastCheckTime = node.Attributes["LastCheckTime"].Value;
                        }
                        rowIndex = int.Parse(node.Attributes["RowIndex"].Value);

                        //�Ƴ��ýڵ�
                        rootNode.RemoveChild(node);
                        break;
                    }
                    else
                    {
                        int index = int.Parse(node.Attributes["RowIndex"].Value);
                        if (index > maxIndex)
                        {
                            maxIndex = index;
                        }
                    }
                }

                if (rowIndex == 0)
                {
                    rowIndex = maxIndex + 1;
                }

                #region ����ڵ�
                XmlNode newNode = xDoc.CreateElement(elementName);

                XmlNode xmlMAttribute1 = xDoc.CreateNode(XmlNodeType.Attribute, "APName", "");
                xmlMAttribute1.Value = apInfo.ApName;
                newNode.Attributes.SetNamedItem(xmlMAttribute1);
                XmlNode xmlMAttribute2 = xDoc.CreateNode(XmlNodeType.Attribute, "APAddress", "");
                xmlMAttribute2.Value = apInfo.ApAddress;
                newNode.Attributes.SetNamedItem(xmlMAttribute2);
                XmlNode xmlMAttribute3 = xDoc.CreateNode(XmlNodeType.Attribute, "AlarmTime", "");
                xmlMAttribute3.Value = apInfo.AlarmTime;
                newNode.Attributes.SetNamedItem(xmlMAttribute3);
                XmlNode xmlMAttribute4 = xDoc.CreateNode(XmlNodeType.Attribute, "AlarmStatus", "");
                xmlMAttribute4.Value = apInfo.AlarmStatus;
                newNode.Attributes.SetNamedItem(xmlMAttribute4);
                XmlNode xmlMAttribute5 = xDoc.CreateNode(XmlNodeType.Attribute, "LastFileLocation", "");
                xmlMAttribute5.Value = apInfo.LastFileLocation;
                newNode.Attributes.SetNamedItem(xmlMAttribute5);
                XmlNode xmlMAttribute6 = xDoc.CreateNode(XmlNodeType.Attribute, "LastCheckTime", "");
                xmlMAttribute6.Value = apInfo.LastCheckTime;
                newNode.Attributes.SetNamedItem(xmlMAttribute6);
                XmlNode xmlMAttribute7 = xDoc.CreateNode(XmlNodeType.Attribute, "RowIndex", "");
                xmlMAttribute7.Value = rowIndex.ToString();
                newNode.Attributes.SetNamedItem(xmlMAttribute7);


                rootNode.AppendChild(newNode);
                #endregion

                //�����ļ�
                xDoc.Save(fileInfo);
                xDoc = null;         
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ����AP�����������ƻ�ȡAP�������
        /// </summary>
        /// <param name="apName">AP����������</param>
        /// <returns>AP��������AP�������</returns>
        public APInfo GetApInfo(string apName)
        {
            APInfo apInfo = null;
            try
            {
                //��ȡҪ�����ļ���·��
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + AppConst.directoryAppConfig + "\\" + AppConst.APConfig + ".xml";
                //�ж��ļ��Ƿ����
                if (!File.Exists(fileInfo))
                {
                    return null;
                }
                
                XmlDocument xDoc = new XmlDocument();
                //��ȡXML����
                xDoc.Load(fileInfo);
                XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + elementName);

                foreach (XmlNode node in xmlnList)
                {
                    if (node.Attributes["APName"].Value.ToUpper() == apName.ToUpper())
                    {
                        apInfo = new APInfo();
                        apInfo.ApName = node.Attributes["APName"].Value;
                        apInfo.ApAddress = node.Attributes["APAddress"].Value;
                        apInfo.AlarmTime = node.Attributes["AlarmTime"].Value;
                        apInfo.AlarmStatus = node.Attributes["AlarmStatus"].Value;
                        apInfo.LastFileLocation = node.Attributes["LastFileLocation"].Value;
                        apInfo.LastCheckTime = node.Attributes["LastCheckTime"].Value;                        
                        break;
                    }
                }

                xDoc = null;
            }
            catch
            {
                return null;
            }

            return apInfo;
        }

        /// <summary>
        /// ����AP������������ɾ��AP�������
        /// </summary>
        /// <param name="apName">AP����������</param>
        /// <returns>ִ�����</returns>
        public bool DeleteApInfo(string apName)
        {
            try
            {
                //��ȡҪ�����ļ���·��
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + AppConst.directoryAppConfig + "\\" + AppConst.APConfig + ".xml";
                //�ж��ļ��Ƿ����
                if (!File.Exists(fileInfo))
                {
                    return false;
                }

                XmlDocument xDoc = new XmlDocument();
                //��ȡXML����
                xDoc.Load(fileInfo);
                XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + elementName);
                XmlNode rootNode = xDoc.SelectSingleNode("//root");

                //�Ƴ��Ѿ����ڵĽڵ�
                foreach (XmlNode node in xmlnList)
                {
                    if (node.Attributes["APName"].Value.ToUpper() == apName.ToUpper())
                    {
                        //�Ƴ��ýڵ�
                        rootNode.RemoveChild(node);
                        break;
                    }
                }

                //�����ļ�
                xDoc.Save(fileInfo);
                xDoc = null;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ��ȡAP�������������Ϣ
        /// </summary>
        /// <returns>����AP���������Ϣ����</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public DataView GetApInfos()
        {      
            DataTable dt = new DataTable();
            try
            {
                //��ȡҪ�����ļ���·��
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + AppConst.directoryAppConfig + "\\" + AppConst.APConfig + ".xml";
                //�ж��ļ��Ƿ����
                if (!File.Exists(fileInfo))
                {
                    return null;
                }

                XmlDocument xDoc = new XmlDocument();
                //��ȡXML����
                xDoc.Load(fileInfo);
                XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + elementName);
                
                dt.Columns.Add("APName");
                dt.Columns.Add("APAddress");
                dt.Columns.Add("AlarmTime");
                dt.Columns.Add("AlarmStatus");

                
                dt.Columns.Add("ApLinkStatus");
                dt.Columns.Add("ApTryConnect");                
                dt.Columns.Add("ApEdit");
                dt.Columns.Add("ApDelete");

                dt.Columns.Add("RowIndex");

                foreach (XmlNode node in xmlnList)
                {
                    DataRow dr = dt.NewRow();
                    dr["ApName"] = node.Attributes["APName"].Value;
                    dr["ApAddress"] = node.Attributes["APAddress"].Value;
                    dr["AlarmTime"] = node.Attributes["AlarmTime"].Value;
                    dr["AlarmStatus"] = node.Attributes["AlarmStatus"].Value == "1" ? "���" : "�����";

                    dr["ApDelete"] = "ɾ��";
                    dr["ApEdit"] = "�༭";
                    dr["ApLinkStatus"] = "";
                    dr["ApTryConnect"] = "��������";

                    dr["RowIndex"] = node.Attributes["RowIndex"].Value;

                    dt.Rows.Add(dr);
                }

                xDoc = null;
            }
            catch
            {
                return null;
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = dt.DefaultView;
                dv.Sort = "RowIndex";
                return dv;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡWeb�������������Ϣ
        /// </summary>
        /// <returns>����Web���������Ϣ����</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public DataView GetWebMonitorInfos()
        {
            DataTable dt = new DataTable();
            try
            {
                //��ȡҪ�����ļ���·��
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + AppConst.directoryAppConfig + "\\" + System.Configuration.ConfigurationManager.AppSettings["WebMonitorConfig"].ToString() + ".xml";
                //�ж��ļ��Ƿ����
                if (!File.Exists(fileInfo))
                {
                    return null;
                }

                XmlDocument xDoc = new XmlDocument();
                //��ȡXML����
                xDoc.Load(fileInfo);
                XmlNodeList xmlnList = xDoc.SelectNodes("//root//WebInfo");

                dt.Columns.Add("WebName");
                dt.Columns.Add("WebAddress");
                dt.Columns.Add("AlarmStatus");
                
                dt.Columns.Add("LinkStatus");
                dt.Columns.Add("TryConnect");

                dt.Columns.Add("RowIndex", typeof(int));

                foreach (XmlNode node in xmlnList)
                {
                    DataRow dr = dt.NewRow();
                    dr["WebName"] = node.Attributes["WebName"].Value;
                    dr["WebAddress"] = node.Attributes["WebAddress"].Value;
                    dr["AlarmStatus"] = node.Attributes["AlarmStatus"].Value == "1" ? "���" : "�����";

                    dr["LinkStatus"] = "";
                    dr["TryConnect"] = "��������";

                    dr["RowIndex"] = node.Attributes["RowIndex"].Value;

                    dt.Rows.Add(dr);
                }

                xDoc = null;
            }
            catch
            {
                return null;
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = dt.DefaultView;
                dv.Sort = "RowIndex";
                return dv;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡAP�������������Ϣ
        /// </summary>
        /// <returns>����AP���������Ϣ����</returns>
        public List<APInfo> GetApInfoLists()
        {
            List<APInfo> apInfos = new List<APInfo>();
            try
            {
                //��ȡҪ�����ļ���·��
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + AppConst.directoryAppConfig + "\\" + AppConst.APConfig + ".xml";
                //�ж��ļ��Ƿ����
                if (!File.Exists(fileInfo))
                {
                    return null;
                }

                XmlDocument xDoc = new XmlDocument();
                //��ȡXML����
                xDoc.Load(fileInfo);
                XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + elementName);


                foreach (XmlNode node in xmlnList)
                {
                    APInfo apInfo = new APInfo();
                    apInfo.ApName = node.Attributes["APName"].Value;
                    apInfo.ApAddress = node.Attributes["APAddress"].Value;
                    apInfo.AlarmTime = node.Attributes["AlarmTime"].Value;
                    apInfo.AlarmStatus = node.Attributes["AlarmStatus"].Value;
                    apInfo.LastFileLocation = node.Attributes["LastFileLocation"].Value;
                    apInfo.LastCheckTime = node.Attributes["LastCheckTime"].Value;
                    apInfos.Add(apInfo);
                }

                xDoc = null;
            }
            catch
            {
                return null;
            }

            return apInfos;
        }
        #endregion
    }
}
