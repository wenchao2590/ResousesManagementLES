//-------------------------------------------------------------------
//版权所有：版权所有(C) 2006，Microsoft(China) Co.,LTD
//系统名称：GMCC-ADC
//文件名称：XmlWrapper
//模块名称：
//模块编号：
//作　　者：SuChuanyi
//完成日期：2011-1-24 21:32:11
//功能说明：
//-----------------------------------------------------------------
//修改记录：
//修改人：  
//修改时间：
//修改内容：
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Infrustructure.Utilities
{
    public enum LoadType
    {
        FromFile = 1,
        FromString = 2
    }

    public class XmlWrapper
    {
        #region 
        ///创建一个 XML document 内部变量
        public XmlDocument xmlDoc = new XmlDocument();
        /// <summary>
        /// 无参构造函数，构建一个只包含根结点的XML文档
        /// </summary>
        public XmlWrapper()
        {
            ClearDocument("ROOT");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootName"></param>
        public XmlWrapper(string rootName)
        {
            ClearDocument(rootName);
        }
        /// <summary>
        /// 构造函数，从XML文件或者XML字符串中读取信息构造一个XML文档
        /// </summary>
        /// <param name="source">XML字符串或者XML文件的路径</param>
        /// <param name="type">数据源的类型</param>
        public XmlWrapper(string source, LoadType type)
        {
            switch (type)
            {
                case LoadType.FromFile: LoadFromFile(source); break;
                case LoadType.FromString: LoadFromString(source); break;
            }
        }
        /// <summary>
        /// 创建一个只包含根结点的XML文档
        /// </summary>
        /// <param name="rootName">根结点名</param>
        public void ClearDocument(string rootName)
        {
            string newXML = string.Empty;
            ///创建XML的基本信息
            newXML = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            newXML = newXML + "<" + rootName + " />";
            ///加载XML字符串到XML文档对象
            xmlDoc.LoadXml(newXML);
        }
        /// <summary>
        /// 读入一个XML文件，并加载到XML文档对象
        /// </summary>
        /// <param name="url">XML文件路径</param>
        public void LoadFromFile(string url)
        {
            XmlTextReader xtr = null;
            try
            {
                ///将XML文件加载到XmlTextReader
                xtr = new XmlTextReader(url);
                ///从XmlTextReader中将XML信息加载到XML文档对象
                xmlDoc.Load(xtr);
            }
            finally
            {
                ///关闭 XMLTextReader
                if (xtr != null) xtr.Close();
            }
        }
        /// <summary>
        /// 读入XML字符串，并加载到XML文档对象
        /// </summary>
        /// <param name="xmlString">XML字符串</param>
        public void LoadFromString(string xmlString)
        {
            // 从字符串中将XML信息加载到XML文档对象
            xmlDoc.LoadXml(xmlString);
        }
        /// <summary>
        /// 根据节点路径转换为对象
        /// </summary>
        /// <param name="nodePath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object XmlToObject(string nodePath, Type type)
        {
            XmlNode xn = xmlDoc.SelectSingleNode(nodePath);

            if (xn == null) return null;
            XmlSerializer serializer = new XmlSerializer(type);
            using (StringReader reader = new StringReader(xn.OuterXml))
            {
                return serializer.Deserialize(reader);
            }
        }
        /// <summary>
        /// 根据节点记录转化为对象集合
        /// </summary>
        /// <param name="nodePath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<object> XmlToList(string nodePath, Type type)
        {
            List<object> ts = new List<object>();
            if (xmlDoc == null) return ts;
            foreach (XmlNode xn in xmlDoc.SelectNodes(nodePath))
            {
                XmlSerializer serializer = new XmlSerializer(type);
                using (StringReader reader = new StringReader(xn.OuterXml))
                {
                    ts.Add(serializer.Deserialize(reader));
                }
            }
            return ts;
        }
        #endregion

        public string ObjectToXmlByEncoding(object obj, Encoding encoding, bool namespaceFlag = true)
        {
            using (var writer = new StringWriterWithEncoding(encoding))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                if (namespaceFlag)
                    serializer.Serialize((TextWriter)writer, obj);
                else
                    serializer.Serialize((TextWriter)writer, obj, ns);
                return writer.ToString();
            }
        }


        public string ObjectToXml(object obj, bool namespaceFlag = true)
        {
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                if (namespaceFlag)
                    serializer.Serialize((TextWriter)writer, obj);
                else
                    serializer.Serialize((TextWriter)writer, obj, ns);
                return writer.ToString();
            }
        }
        public string GetXml(object obj)
        {
            if (obj == null) return string.Empty;
            MemoryStream Stream = new MemoryStream();
            //创建序列化对象   
            XmlSerializer xml = new XmlSerializer(obj.GetType());
            try
            {
                //序列化对象   
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();
            return str;
        }
        public object XmlToObject(string xml, object o)
        {
            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                return serializer.Deserialize(reader);
            }
        }

        public object XmlToObject(object o)
        {
            using (StringReader reader = new StringReader(xmlDoc.InnerXml))
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                return serializer.Deserialize(reader);
            }
        }





        /// <summary>
        /// 将XML文档内容以单行字符串形式返回
        /// </summary>
        public override string ToString()
        {
            // 创建 SringBuilder 和 StringWriter 对象
            StringBuilder builder = new StringBuilder(xmlDoc.InnerXml);
            //StringWriter writer = new StringWriter(builder);

            // 保存 XML 到 StringWriter/StringBuilder
            //xmlDoc.Save(writer);

            // 去除多余的转义符
            builder = builder.Replace("\r", "");
            builder = builder.Replace("\n", "");
            builder = builder.Replace("\t", "");
            builder = builder.Replace("  ", "");

            // 返回结果
            return builder.ToString();
        }







        /// <summary>
        /// 保存XML内容到一个文件中
        /// </summary>
        /// <param name="url">文件路径</param>
        public void SaveToFile(string url)
        {
            XmlTextWriter xtw = null;

            try
            {
                // 将 XML 文件加载到XmlTextWriter
                xtw = new XmlTextWriter(url, null);

                // 保存 XML
                xmlDoc.Save(xtw);
            }
            finally
            {
                // 关闭 XmlTextWriter
                if (xtw != null)
                    xtw.Close();
            }
        }

        /// <summary>
        /// 检验某节点是否存在.
        /// </summary>
        /// <param name="nodePattern">节点的XPath</param>
        /// <returns>返回 true 表示存在, false 则是不存在.</returns>
        public bool VerifyNode(string nodePattern)
        {
            return (xmlDoc.SelectSingleNode(nodePattern) != null);
        }

        /// <summary>
        /// 在某父节点下添加一个新的节点.
        /// </summary>
        /// <param name="parentPattern">父节点的XPath</param>
        /// <param name="nodeName">新节点名</param>
        public void AddNode(string parentPattern, string nodeName)
        {
            // 选择父节点
            XmlNode parentNode = xmlDoc.SelectSingleNode(parentPattern);

            // 创建新的字节点
            XmlElement childElement = xmlDoc.CreateElement(nodeName);

            // 将刚创建的字节点添加为父节点的最后一个元素
            parentNode.AppendChild(childElement);
        }

        public void AddNode(string parentPattern, string nodeName, string nodeValue)
        {
            // 选择父节点
            XmlNode parentNode = xmlDoc.SelectSingleNode(parentPattern);

            // 创建新的字节点
            XmlElement childElement = xmlDoc.CreateElement(nodeName);
            childElement.InnerText = nodeValue;

            // 将刚创建的字节点添加为父节点的最后一个元素
            parentNode.AppendChild(childElement);
        }

        /// <summary>
        /// 在某父节点下添加一个新的节点，并且该节点包含一个属性.
        /// </summary>
        /// <param name="parentPattern">父节点的XPath</param>
        /// <param name="nodeName">新节点名</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        public void AddNode(string parentPattern, string nodeName, string attributeName, string attributeValue)
        {
            // 选择父节点
            XmlNode parentNode = xmlDoc.SelectSingleNode(parentPattern);

            // 创建新的字节点
            XmlElement childElement = xmlDoc.CreateElement(nodeName);

            // 给新节点添加属性
            childElement.SetAttribute(attributeName, attributeValue);

            // 将刚创建的字节点添加为父节点的最后一个元素
            parentNode.AppendChild(childElement);
        }

        /// <summary>
        /// 删除指定节点.
        /// </summary>
        /// <param name="nodePattern">节点的XPath</param>
        public void DeleteNode(string nodePattern)
        {
            // 选择要删除的节点
            XmlNode deleteNode = xmlDoc.SelectSingleNode(nodePattern);

            if (deleteNode != null)
            {
                // 选择该节点的父节点
                XmlNode parentNode = deleteNode.ParentNode;

                // 从父节点中删除该节点已经该节点的所有子节点
                parentNode.RemoveChild(deleteNode);

            }
        }

        /// <summary>
        /// 根据条件删除或保留节点，如果该节点没出现过，则新增节点
        /// </summary>
        /// <param name="parentPattern">父节点的XPath</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="keep">条件</param>
        public void UpdateNode(string parentPattern, string nodeName, bool keep)
        {
            // 检查是该删掉节点还是保留节点
            if (keep == false)
            {
                // 删除节点
                DeleteNode(parentPattern + "/" + nodeName);
            }
            else
            {
                // 如果节点不存在，就新增
                if (VerifyNode(parentPattern + "/" + nodeName) == false)
                {
                    // 新增节点
                    AddNode(parentPattern, nodeName);
                }
            }
        }

        /// <summary>
        /// 获取指定节点的值(元素或属性).
        /// </summary>
        /// <param name="nodePattern">节点的XPath</param>
        /// <returns>如果节点没找到就返回空，否则就返回节点值</returns>				
        public string GetValue(string nodePattern)
        {
            string valueString = "";

            // 选择节点
            XmlNode targetNode = xmlDoc.SelectSingleNode(nodePattern);

            if (targetNode != null)
            {
                // 获取节点值
                valueString = targetNode.InnerText;
            }

            // 返回值
            return valueString;
        }

        /// <summary>
        /// 更新指定节点的值，如果无法找到节点，则新增一个节点.
        /// </summary>
        /// <param name="nodePattern">节点的XPath</param>
        /// <param name="valueString">节点的值</param>
        public void SetValue(string nodePattern, string valueString)
        {
            // 选择指定节点
            XmlNode targetNode = xmlDoc.SelectSingleNode(nodePattern);

            if (targetNode == null)
            {
                // 获取最有一个"/"的位置，为了将父节点跟节点分开
                int lastSlash = nodePattern.LastIndexOf("/");

                // 新怎节点
                AddNode(nodePattern.Substring(0, lastSlash), nodePattern.Substring(lastSlash + 1));

                // 选择节点
                targetNode = xmlDoc.SelectSingleNode(nodePattern);
            }

            // 更新节点值
            targetNode.InnerText = valueString;
        }

        /// <summary>
        /// 获取指定节点的属性值.
        /// </summary>
        /// <param name="nodePattern">指定节点的XPath</param>
        /// <param name="attributeName">属性名</param>
        /// <returns>属性值</returns>				
        public string GetAttribute(string nodePattern, string attributeName)
        {
            string attributeString = "";

            // 选择匹配的节点
            XmlNode matchNode = xmlDoc.SelectSingleNode(nodePattern);

            if (matchNode != null)
            {
                // 获取属性值
                try
                {
                    attributeString = matchNode.Attributes[attributeName].Value;
                }
                catch (NullReferenceException)
                {
                }
            }

            // 返回属性值
            return attributeString;
        }

        /// <summary>
        /// 给指定节点的指定属性设值.
        /// </summary>
        /// <param name="nodePattern">指定节点的 XPath</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        public void SetAttribute(string nodePattern, string attributeName, string attributeValue)
        {
            // 选择匹配的节点
            XmlNode matchNode = xmlDoc.SelectSingleNode(nodePattern);

            if (matchNode != null)
            {
                // 选择指定的属性
                XmlAttribute matchAttribute = matchNode.Attributes[attributeName];

                // 如果属性没找到，则新增属性
                if (matchAttribute == null)
                {
                    // 创建一个新的属性
                    XmlAttribute newAttribute = xmlDoc.CreateAttribute(attributeName);
                    newAttribute.Value = attributeValue;

                    // 把属性加到节点下
                    matchNode.Attributes.Append(newAttribute);

                }
                else
                {
                    // 给属性设值
                    matchAttribute.Value = attributeValue;
                }
            }
        }

        /// <summary>
        /// 获取匹配的一组节点的指定属性的值，并以指定的分隔符隔开。
        /// </summary>
        /// <param name="nodeGroupPattern">指定节点的XPath</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>用分隔符隔开的字符串</returns>			
        public string GetAttributeList(string nodeGroupPattern, string attributeName, char delimiter)
        {
            StringBuilder listString = new StringBuilder();

            // 选择匹配的节点列表
            XmlNodeList nodeList = xmlDoc.SelectNodes(nodeGroupPattern);

            // 遍历节点列表
            foreach (XmlNode node in nodeList)
            {
                listString.Append(node.Attributes[attributeName].Value + delimiter);
            }

            //去掉最后一个分隔符
            string listText = listString.ToString();
            int lastIndex = listText.LastIndexOf(delimiter);
            if (lastIndex != -1)
                listText = listText.Remove(lastIndex, 1);

            // 返回字符串
            return listText;
        }

        /// <summary>
        /// 获取匹配的一组节点的值，并以指定的分隔符隔开.
        /// </summary>
        /// <param name="nodeGroupPattern">指定节点的XPath</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>用分隔符隔开的字符串</returns>			
        public string GetValueList(string nodeGroupPattern, char delimiter)
        {
            StringBuilder listString = new StringBuilder();

            // 选择匹配的节点列表
            XmlNodeList nodeList = xmlDoc.SelectNodes(nodeGroupPattern);

            // 遍历节点列表
            foreach (XmlNode node in nodeList)
            {
                listString.Append(node.Name + delimiter);
            }

            //去掉最后一个分隔符
            string listText = listString.ToString();
            int lastIndex = listText.LastIndexOf(delimiter);
            if (lastIndex != -1)
                listText = listText.Remove(lastIndex, 1);

            // 返回字符串
            return listText;
        }

        /// <summary>
        /// 将指定节点复制到指定的父节点下.
        /// </summary>
        /// <param name="sourcePattern">需要复制的节点的XPath</param>
        /// <param name="destinationPattern">目的父节点的XPath</param>
        public void CopyNode(string sourcePattern, string destinationPattern)
        {
            // 选择复制源节点
            XmlNode sourceNode = xmlDoc.SelectSingleNode(sourcePattern);

            // 目的父节点
            XmlNode destinationNode = xmlDoc.SelectSingleNode(destinationPattern);

            // 克隆源节点到目的父节点下
            destinationNode.AppendChild(sourceNode.Clone());
        }

        /// <summary>
        /// XML序列化对象
        /// </summary>
        /// <param name="emp">object that would be converted into xml</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public string ObjectToXML(Object Instance)
        {
            //Instance = null;
            MemoryStream stream = null;
            TextWriter writer = null;
            try
            {
                stream = new MemoryStream(); // read xml in memory
                writer = new StreamWriter(stream, Encoding.UTF8);

                // get serialise object
                //XmlSerializer serializer = new XmlSerializer(typeof(Emp),"");
                XmlSerializer serializer = new XmlSerializer(Instance.GetType());

                XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
                xsn.Add(string.Empty, string.Empty);

                serializer.Serialize(writer, Instance, xsn); // read object
                int count = (int)stream.Length; // saves object in memory stream
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                // copy stream contents in byte array
                stream.Read(arr, 0, count);
                //UnicodeEncoding utf = new UnicodeEncoding(); // convert byte array to string
                UTF8Encoding utf = new UTF8Encoding();
                writer.Flush();
                stream.Flush();
                return utf.GetString(arr).Trim();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.ToString());
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
                if (writer != null)
                {
                    writer.Dispose();
                }
            }
        }


    }
}
