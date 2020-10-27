//+-------------------------------------------------------------------+
//+ Name: AppSettings	  
//+ Function:    操作LES_Monitor.exe.config文件
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

        //获取数据库的连接字符串
        public static string GetDatabaseConnString(string DatabaseName)
        {
            //try
            //{

                //实例化XML文档
                XmlDocument Doc = new XmlDocument();
                //实例化程序域
                string filepath = Application.ExecutablePath + ".config";
                //加载app.config文件
                Doc.Load(filepath);
                //设置相应节点值
                return  Doc.SelectSingleNode("/configuration/connectionStrings/add[@name='" + DatabaseName + "']").Attributes["connectionString"].Value;
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }
        //设定数据库的连接字符串
        public static void SetDatabaseConnString(string DatabaseName, string DatabaseConnString)
        {
            //try
            //{ 
                //实例化XML文档
                XmlDocument Doc=new XmlDocument(); 
                //实例化程序域
                string filepath = Application.ExecutablePath + ".config";
                //加载app.config文件
                Doc.Load(filepath);
                //设置相应节点值
                Doc.SelectSingleNode("/configuration/connectionStrings/add[@name='" + DatabaseName + "']").Attributes["connectionString"].Value = DatabaseConnString;
                //保存设置
                Doc.Save(filepath);

                Doc.Load(filepath); 
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        
        }
        //读取Value值 
        public static string GetConfigString(string key)
        {
            //try
            //{

                //实例化XML文档
                XmlDocument Doc = new XmlDocument();
                //实例化程序域
                string filepath = Application.ExecutablePath + ".config";
                //加载app.config文件
                Doc.Load(filepath);
                //设置相应节点值
                return Doc.SelectSingleNode("/configuration/appSettings/add[@key='" + key + "']").Attributes["value"].Value;
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }
        //写操作 
        public static void SetValue(string AppKey, string AppValue)
        {
            //try
            //{
                XmlDocument xDoc = new XmlDocument();
                //获取可执行文件的路径和名称 
                xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
                XmlNode xNode;
                XmlElement xElem1;
                XmlElement xElem2;
                //获取appSettings节点
                xNode = xDoc.SelectSingleNode("//appSettings");

                //获取appSettings节点下的元素，如元素存在则修改元素的值，否则创建元素并设定值
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
                xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");//保存config文件
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }

        #region  写入xml
        //参数fileName:文件名;ElementName:元素名;AttributeName:属性名;AttValue:属性值
        public static void WriteXml(string direcoryName, string fileName, string ElementName, string AttributeName, string AttValue)
        {

             WriteXml(direcoryName,fileName, ElementName, AttributeName, AttValue, "");               

        }
        #endregion

        #region  写入xml
        //参数fileName:文件名;ElementName:元素名;AttributeName:属性名;AttValue:属性值
        public static void WriteXml(string direcoryName, string fileName, string ElementName, string AttributeName, string AttValue, string str)
        {
            //获取要操作文件的路径
            string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + direcoryName + "\\" + fileName + ".xml";
            try
            {               
                //判断文件是否存在
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //获取可执行文件的路径和名称 
                    xDoc.Load(fileInfo);
                    XmlNode xNode;
                    XmlElement xElem1;
                    XmlElement xElem2;
                    xNode = xDoc.SelectSingleNode("//root");

                    xElem1 = (XmlElement)xNode.SelectSingleNode("//" + ElementName+"[@"+AttributeName+"]");
                    //获取root节点下的元素，如元素存在则修改元素的值，否则创建元素并设定值
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
                    xDoc.Save(fileInfo);//保存文件
                }
                else
                {
                    CreateXml(fileInfo, ElementName, AttributeName, AttValue);//文件不存在时创建该文件
                }
            }
            catch (System.Exception ex)
            {
                try
                {
                    //该文件已经存在，但内容不正确，先删除该文件，再重新生成该文件
                    Logger.LogError("CC.Utility", "WriteXml", "AppSettings", "记录xml日志错误，该文件已经存在，但内容不正确。", ex);
                    File.Delete(fileInfo);
                    CreateXml(fileInfo, ElementName, AttributeName, AttValue);//文件不存在时创建该文件
                }
                catch (System.Exception ex1)
                {
                    Logger.LogError("CC.Utility", "WriteXml", "AppSettings", "操作xml日志文件错误。", ex1);
                }
            }
        }
        #endregion

        //读xml
        //参数fileName:文件名;ElementName:元素名;AttributeName:属性名;
        public static string xReadXml(string direcoryName, string fileName, string ElementName, string AttributeName)
        {
            string strAttValue = string.Empty;

            //获取可执行文件的路径和名称 
            string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + direcoryName + "\\" + fileName + ".xml";
            //判断文件是否存在
            if (File.Exists(fileInfo))
            {
                XmlDocument xDoc = new XmlDocument();
                //获取可执行文件的路径和名称 
                xDoc.Load(fileInfo);
                XmlNode xNode;
                XmlElement xElem1;
                xNode = xDoc.SelectSingleNode("//root");
                //xDoc .ReadNode(XmlReader XmlReader\

                //获取Root节点下的元素
                xElem1 = (XmlElement)xNode.SelectSingleNode("//" + ElementName + "[@" + AttributeName + "]");
                   
                strAttValue = xElem1.Attributes[AttributeName].Value;
            }                

            return strAttValue;
        }

        #region 读xml
        //参数fileName:文件名;ElementName:元素名;AttributeName:属性名;
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

        #region 读xml
        //参数fileName:文件名;ElementName:元素名;AttributeName:属性名;AttValue:属性值
        public static string ReadXml(string direcoryName,string fileName, string ElementName, string AttributeName,string AttValue)
        {
            string strAttValue = string.Empty;
            //try
            //{
                //获取可执行文件的路径和名称 
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + direcoryName + "\\" + fileName + ".xml";
                //判断文件是否存在
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //获取可执行文件的路径和名称 
                    xDoc.Load(fileInfo);
                    XmlNode xNode;
                    XmlElement xElem1;
                    XmlElement xElem2;
                    xNode = xDoc.SelectSingleNode("//root");

                    //获取Root节点下的元素
                    xElem1 = (XmlElement)xNode.SelectSingleNode("//" + ElementName+"[@"+AttributeName+"]");  

                    //元素存在则获取元素的值，否则创建该元素并设定初始值
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
                    CreateXml(fileInfo, ElementName, AttributeName, AttValue);//文件不存在时创建该文件
                    strAttValue = AttValue;
                }
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
            return strAttValue;
        }
        

        //读取车间信息
        //参数fileName:文件名;ElementName:元素名;AttributeName:属性名;
        public static Hashtable ReadXml(string direcoryName, string fileName, string ElementName)
        {
            Hashtable hs = new Hashtable();
            //try
            //{               
                XmlDocument xDoc = new XmlDocument();
                //获取可执行文件的路径和名称 
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

        // 从XML文件中获取数据并以DataSet类型返回
        //参数 direcoryName：目录名称；fileName：文件名
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static DataSet GetDataSetFromXml(string direcoryName, string fileName)
        {

                DataSet ds = new DataSet();
                //从文件中获取数据
                ds.ReadXml(System.Windows.Forms.Application.StartupPath + "\\" + direcoryName + "\\" + fileName + ".xml");
                return ds;

        }

        //过滤datatable数据并返回hashtable
        //参数dt：要过滤的数据表；Column：以那个列为条件；filterValue：过滤条件
        public static Hashtable GetHashtableFromDatatable(DataTable dt, string Column, string filterValue)
        {
            Hashtable hs = new Hashtable();
            //try
            //{
                //过滤数据
                DataRow[] rows = dt.Select(Column + "='" + filterValue + "'");
                //将过滤的数据写到Hashtable
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

        //过滤datatable数据并返回hashtable
        //参数dt：要过滤的数据表；Column：以那个列为条件；filterValue：过滤条件
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static DataTable GetDatatableFromDatatable(DataTable dt, string Column, string filterValue)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("LESWorkShopID");
            dataTable.Columns.Add("PMCWorkShopID");
            dataTable.Columns.Add("DeleteWorkShop");

                //过滤数据
                DataRow[] rows = dt.Select(Column + "='" + filterValue + "'");
                //将过滤的数据写到Hashtable
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

        //读取工厂信息
        //参数fileName:文件名;ElementName:元素名;AttributeName:属性名
        public static string ReadPlantXml(string direcoryName, string fileName, string ElementName, string AttributeName)
        {
            //try
            //{
                XmlDocument xDoc = new XmlDocument();
                //获取可执行文件的路径和名称 
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

        #region 创建XML文件
        //参数fileName:文件名;ElementName:元素名;AttributeName:属性名;AttValue:属性值
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

        //参数fileName:文件名
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
        #region 创建cconfig目录
        public static void createConfig(string directoyName)
        {
            //目录config不存在时创建该目录
            string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoyName;
            //try
            //{
                if (!Directory.Exists(fileInfo))
                {
                    Directory.CreateDirectory(fileInfo);//创建config目录
                }
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }
        #endregion

        //保存设置的连接字符串
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        public static void  SaveConnStr(string directoryName, string fileName, DataGridView dgv, string ElementName,out string returnInfo)
        {
            try
            {
                string dgvName = dgv.Name;
                //获取要操作文件的路径
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoryName + "\\" + fileName + ".xml";
                //判断文件是否存在
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //获取可执行文件的路径和名称 
                    xDoc.Load(fileInfo);
                    XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + ElementName);
                    //将设置的值保存到XML文件
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
                    //保存文件
                    xDoc.Save(fileInfo);
                    returnInfo = "保存成功";
                }
                else
                {
                    returnInfo = fileInfo+"不存在";
                }
            }
            catch (System.Exception ex)
            {
                returnInfo = "保存失败";
                throw ex;
            }
        }


        //保存设置的连接字符串
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        public static void SaveConnStr(string directoryName, string fileName,  string ElementName, string plantNo,string connStr,string moduleNm,out string returnInfo)
        {
            try
            {
                //获取要操作文件的路径
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoryName + "\\" + fileName + ".xml";
                //判断文件是否存在
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //获取可执行文件的路径和名称 
                    xDoc.Load(fileInfo);
                    XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + ElementName);
                    //将设置的值保存到XML文件
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
                    //保存文件
                    xDoc.Save(fileInfo);
                    returnInfo = "保存成功";
                }
                else
                {
                    returnInfo = fileInfo + "不存在";
                }
            }
            catch (System.Exception ex)
            {
                returnInfo = "保存失败";
                throw ex;
            }
        }

        //移出节点
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        public static void RemoveNode(string directoryName, string fileName, string ElementName, string plantNo, string moduleNm, out string returnInfo)
        {
            try
            {
                //获取要操作文件的路径
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoryName + "\\" + fileName + ".xml";
                //判断文件是否存在
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //获取可执行文件的路径和名称 
                    xDoc.Load(fileInfo);
                    XmlNode rootNode = xDoc.SelectSingleNode("//root");
                    XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + ElementName);
                    //将设置的值保存到XML文件
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
                    //保存文件
                    xDoc.Save(fileInfo);
                    returnInfo = "删除成功";
                }
                else
                {
                    returnInfo = fileInfo + "不存在";
                }
            }
            catch (System.Exception ex)
            {
                returnInfo = "删除失败";
                throw ex;
            }
        }

        //保存添加的新节点
        public static void AddAndSaveConnStr(string directoryName, string fileName, string ElementName, string AttributeName1, string AttributeName2, string value1, string value2, out string returnInfo)
        {
            try
            {
                AppSettings app = new AppSettings();
                bool IsExist = app.isExistNode(directoryName, fileName, ElementName, AttributeName1, value1, out returnInfo);
                if (!IsExist && returnInfo =="")
                {
                    //获取要操作文件的路径
                    string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoryName + "\\" + fileName + ".xml";
                    //判断文件是否存在
                    if (File.Exists(fileInfo))
                    {
                        XmlDocument xDoc = new XmlDocument();
                        //获取可执行文件的路径和名称 
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
                        //保存文件
                        xDoc.Save(fileInfo);
                        returnInfo = "保存成功";
                    }
                    else
                    {
                        returnInfo = fileInfo + "不存在";
                    }
                }
            }
            catch 
            {
                returnInfo = "保存失败";
                //throw ex;
            }
        }
        //检测工厂是否存在
        private   bool isExistNode(string directoryName, string fileName, string ElementName, string AttributeName, string value, out string returnInfo)       
        {
            bool isExist = false;
            string strInfo = string.Empty;
          
                //获取要操作文件的路径
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoryName + "\\" + fileName + ".xml";
                //判断文件是否存在
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //获取可执行文件的路径和名称 
                    xDoc.Load(fileInfo);
                    XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + ElementName);
                    //检测节点是否存在
                    foreach (XmlNode node in xmlnList)
                    {
                        if (node.Attributes[AttributeName].Value ==value)
                        {
                            isExist = true;
                            strInfo = "【工厂】" + value + "在配置文件【" + fileInfo + "】已存在";
                            break;
                        }
                    }                  
                    
                }
                else
                {
                    strInfo = fileInfo + "不存在";
                }
                returnInfo = strInfo;
                return isExist;

        }

        //保存工厂的车间
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        public static void SaveWorkShop(string directoryName, string fileName, string ElementName, string AttributeName, string value, DataGridView dgv, out string returnInfo)
        {
            try
            {
                //获取要操作文件的路径
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + directoryName + "\\" + fileName + ".xml";
                //判断文件是否存在
                if (File.Exists(fileInfo))
                {
                    XmlDocument xDoc = new XmlDocument();
                    //获取可执行文件的路径和名称 
                    xDoc.Load(fileInfo);
                    XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + ElementName);
                    //将设置的值保存到XML文件
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
                    //保存文件
                    xDoc.Save(fileInfo);
                    returnInfo = "保存成功";
                }
                else
                {
                    returnInfo = fileInfo + "不存在";
                }
            }
            catch (System.Exception ex)
            {
                returnInfo = "保存失败";
                throw ex;
            }
        }

        #region AP监控节点操作
        /// <summary>
        /// 需要保存的信息：AP名称、路径、报警时间、是否报警、上次检测文件的位置、上次检测时间等
        /// </summary>
        /// <param name="apInfo">AP监控设置信息</param>
        /// <returns>保存结果</returns>
        public bool SaveAPNote(APInfo apInfo)
        {
            try
            {
                
                //获取要操作文件的路径
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + AppConst.directoryAppConfig + "\\" + AppConst.APConfig + ".xml";
                //判断文件是否存在
                if (!File.Exists(fileInfo))
                {
                    //创建文件
                    CreateXml(fileInfo);
                }

                XmlDocument xDoc = new XmlDocument();
                //获取XML内容
                xDoc.Load(fileInfo);
                XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + elementName);
                XmlNode rootNode = xDoc.SelectSingleNode("//root");

                int rowIndex = 0;
                int maxIndex = 0;
                //移除已经存在的节点
                foreach (XmlNode node in xmlnList)
                {
                    if (node.Attributes["APName"].Value.ToUpper() == apInfo.ApName.ToUpper())
                    {
                        //存在该节点，则取保存的上次文件位置和上次文件检测时间
                        if (string.IsNullOrEmpty(apInfo.LastFileLocation))
                        {
                            apInfo.LastFileLocation = node.Attributes["LastFileLocation"].Value;
                            apInfo.LastCheckTime = node.Attributes["LastCheckTime"].Value;
                        }
                        rowIndex = int.Parse(node.Attributes["RowIndex"].Value);

                        //移除该节点
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

                #region 保存节点
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

                //保存文件
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
        /// 根据AP服务器的名称获取AP监控设置
        /// </summary>
        /// <param name="apName">AP服务器名称</param>
        /// <returns>AP服务器的AP监控设置</returns>
        public APInfo GetApInfo(string apName)
        {
            APInfo apInfo = null;
            try
            {
                //获取要操作文件的路径
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + AppConst.directoryAppConfig + "\\" + AppConst.APConfig + ".xml";
                //判断文件是否存在
                if (!File.Exists(fileInfo))
                {
                    return null;
                }
                
                XmlDocument xDoc = new XmlDocument();
                //获取XML内容
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
        /// 根据AP服务器的名称删除AP监控设置
        /// </summary>
        /// <param name="apName">AP服务器名称</param>
        /// <returns>执行情况</returns>
        public bool DeleteApInfo(string apName)
        {
            try
            {
                //获取要操作文件的路径
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + AppConst.directoryAppConfig + "\\" + AppConst.APConfig + ".xml";
                //判断文件是否存在
                if (!File.Exists(fileInfo))
                {
                    return false;
                }

                XmlDocument xDoc = new XmlDocument();
                //获取XML内容
                xDoc.Load(fileInfo);
                XmlNodeList xmlnList = xDoc.SelectNodes("//root//" + elementName);
                XmlNode rootNode = xDoc.SelectSingleNode("//root");

                //移除已经存在的节点
                foreach (XmlNode node in xmlnList)
                {
                    if (node.Attributes["APName"].Value.ToUpper() == apName.ToUpper())
                    {
                        //移除该节点
                        rootNode.RemoveChild(node);
                        break;
                    }
                }

                //保存文件
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
        /// 获取AP监控设置所有信息
        /// </summary>
        /// <returns>返回AP监控设置信息队列</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public DataView GetApInfos()
        {      
            DataTable dt = new DataTable();
            try
            {
                //获取要操作文件的路径
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + AppConst.directoryAppConfig + "\\" + AppConst.APConfig + ".xml";
                //判断文件是否存在
                if (!File.Exists(fileInfo))
                {
                    return null;
                }

                XmlDocument xDoc = new XmlDocument();
                //获取XML内容
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
                    dr["AlarmStatus"] = node.Attributes["AlarmStatus"].Value == "1" ? "监控" : "不监控";

                    dr["ApDelete"] = "删除";
                    dr["ApEdit"] = "编辑";
                    dr["ApLinkStatus"] = "";
                    dr["ApTryConnect"] = "测试连接";

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
        /// 获取Web监控设置所有信息
        /// </summary>
        /// <returns>返回Web监控设置信息队列</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public DataView GetWebMonitorInfos()
        {
            DataTable dt = new DataTable();
            try
            {
                //获取要操作文件的路径
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + AppConst.directoryAppConfig + "\\" + System.Configuration.ConfigurationManager.AppSettings["WebMonitorConfig"].ToString() + ".xml";
                //判断文件是否存在
                if (!File.Exists(fileInfo))
                {
                    return null;
                }

                XmlDocument xDoc = new XmlDocument();
                //获取XML内容
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
                    dr["AlarmStatus"] = node.Attributes["AlarmStatus"].Value == "1" ? "监控" : "不监控";

                    dr["LinkStatus"] = "";
                    dr["TryConnect"] = "测试连接";

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
        /// 获取AP监控设置所有信息
        /// </summary>
        /// <returns>返回AP监控设置信息队列</returns>
        public List<APInfo> GetApInfoLists()
        {
            List<APInfo> apInfos = new List<APInfo>();
            try
            {
                //获取要操作文件的路径
                string fileInfo = System.Windows.Forms.Application.StartupPath + "\\" + AppConst.directoryAppConfig + "\\" + AppConst.APConfig + ".xml";
                //判断文件是否存在
                if (!File.Exists(fileInfo))
                {
                    return null;
                }

                XmlDocument xDoc = new XmlDocument();
                //获取XML内容
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
