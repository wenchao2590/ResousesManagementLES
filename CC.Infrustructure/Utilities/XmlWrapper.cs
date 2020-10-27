//-------------------------------------------------------------------
//��Ȩ���У���Ȩ����(C) 2006��Microsoft(China) Co.,LTD
//ϵͳ���ƣ�GMCC-ADC
//�ļ����ƣ�XmlWrapper
//ģ�����ƣ�
//ģ���ţ�
//�������ߣ�SuChuanyi
//������ڣ�2011-1-24 21:32:11
//����˵����
//-----------------------------------------------------------------
//�޸ļ�¼��
//�޸��ˣ�  
//�޸�ʱ�䣺
//�޸����ݣ�
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
        ///����һ�� XML document �ڲ�����
        public XmlDocument xmlDoc = new XmlDocument();
        /// <summary>
        /// �޲ι��캯��������һ��ֻ����������XML�ĵ�
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
        /// ���캯������XML�ļ�����XML�ַ����ж�ȡ��Ϣ����һ��XML�ĵ�
        /// </summary>
        /// <param name="source">XML�ַ�������XML�ļ���·��</param>
        /// <param name="type">����Դ������</param>
        public XmlWrapper(string source, LoadType type)
        {
            switch (type)
            {
                case LoadType.FromFile: LoadFromFile(source); break;
                case LoadType.FromString: LoadFromString(source); break;
            }
        }
        /// <summary>
        /// ����һ��ֻ����������XML�ĵ�
        /// </summary>
        /// <param name="rootName">�������</param>
        public void ClearDocument(string rootName)
        {
            string newXML = string.Empty;
            ///����XML�Ļ�����Ϣ
            newXML = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            newXML = newXML + "<" + rootName + " />";
            ///����XML�ַ�����XML�ĵ�����
            xmlDoc.LoadXml(newXML);
        }
        /// <summary>
        /// ����һ��XML�ļ��������ص�XML�ĵ�����
        /// </summary>
        /// <param name="url">XML�ļ�·��</param>
        public void LoadFromFile(string url)
        {
            XmlTextReader xtr = null;
            try
            {
                ///��XML�ļ����ص�XmlTextReader
                xtr = new XmlTextReader(url);
                ///��XmlTextReader�н�XML��Ϣ���ص�XML�ĵ�����
                xmlDoc.Load(xtr);
            }
            finally
            {
                ///�ر� XMLTextReader
                if (xtr != null) xtr.Close();
            }
        }
        /// <summary>
        /// ����XML�ַ����������ص�XML�ĵ�����
        /// </summary>
        /// <param name="xmlString">XML�ַ���</param>
        public void LoadFromString(string xmlString)
        {
            // ���ַ����н�XML��Ϣ���ص�XML�ĵ�����
            xmlDoc.LoadXml(xmlString);
        }
        /// <summary>
        /// ���ݽڵ�·��ת��Ϊ����
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
        /// ���ݽڵ��¼ת��Ϊ���󼯺�
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
            //�������л�����   
            XmlSerializer xml = new XmlSerializer(obj.GetType());
            try
            {
                //���л�����   
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
        /// ��XML�ĵ������Ե����ַ�����ʽ����
        /// </summary>
        public override string ToString()
        {
            // ���� SringBuilder �� StringWriter ����
            StringBuilder builder = new StringBuilder(xmlDoc.InnerXml);
            //StringWriter writer = new StringWriter(builder);

            // ���� XML �� StringWriter/StringBuilder
            //xmlDoc.Save(writer);

            // ȥ�������ת���
            builder = builder.Replace("\r", "");
            builder = builder.Replace("\n", "");
            builder = builder.Replace("\t", "");
            builder = builder.Replace("  ", "");

            // ���ؽ��
            return builder.ToString();
        }







        /// <summary>
        /// ����XML���ݵ�һ���ļ���
        /// </summary>
        /// <param name="url">�ļ�·��</param>
        public void SaveToFile(string url)
        {
            XmlTextWriter xtw = null;

            try
            {
                // �� XML �ļ����ص�XmlTextWriter
                xtw = new XmlTextWriter(url, null);

                // ���� XML
                xmlDoc.Save(xtw);
            }
            finally
            {
                // �ر� XmlTextWriter
                if (xtw != null)
                    xtw.Close();
            }
        }

        /// <summary>
        /// ����ĳ�ڵ��Ƿ����.
        /// </summary>
        /// <param name="nodePattern">�ڵ��XPath</param>
        /// <returns>���� true ��ʾ����, false ���ǲ�����.</returns>
        public bool VerifyNode(string nodePattern)
        {
            return (xmlDoc.SelectSingleNode(nodePattern) != null);
        }

        /// <summary>
        /// ��ĳ���ڵ������һ���µĽڵ�.
        /// </summary>
        /// <param name="parentPattern">���ڵ��XPath</param>
        /// <param name="nodeName">�½ڵ���</param>
        public void AddNode(string parentPattern, string nodeName)
        {
            // ѡ�񸸽ڵ�
            XmlNode parentNode = xmlDoc.SelectSingleNode(parentPattern);

            // �����µ��ֽڵ�
            XmlElement childElement = xmlDoc.CreateElement(nodeName);

            // ���մ������ֽڵ����Ϊ���ڵ�����һ��Ԫ��
            parentNode.AppendChild(childElement);
        }

        public void AddNode(string parentPattern, string nodeName, string nodeValue)
        {
            // ѡ�񸸽ڵ�
            XmlNode parentNode = xmlDoc.SelectSingleNode(parentPattern);

            // �����µ��ֽڵ�
            XmlElement childElement = xmlDoc.CreateElement(nodeName);
            childElement.InnerText = nodeValue;

            // ���մ������ֽڵ����Ϊ���ڵ�����һ��Ԫ��
            parentNode.AppendChild(childElement);
        }

        /// <summary>
        /// ��ĳ���ڵ������һ���µĽڵ㣬���Ҹýڵ����һ������.
        /// </summary>
        /// <param name="parentPattern">���ڵ��XPath</param>
        /// <param name="nodeName">�½ڵ���</param>
        /// <param name="attributeName">������</param>
        /// <param name="attributeValue">����ֵ</param>
        public void AddNode(string parentPattern, string nodeName, string attributeName, string attributeValue)
        {
            // ѡ�񸸽ڵ�
            XmlNode parentNode = xmlDoc.SelectSingleNode(parentPattern);

            // �����µ��ֽڵ�
            XmlElement childElement = xmlDoc.CreateElement(nodeName);

            // ���½ڵ��������
            childElement.SetAttribute(attributeName, attributeValue);

            // ���մ������ֽڵ����Ϊ���ڵ�����һ��Ԫ��
            parentNode.AppendChild(childElement);
        }

        /// <summary>
        /// ɾ��ָ���ڵ�.
        /// </summary>
        /// <param name="nodePattern">�ڵ��XPath</param>
        public void DeleteNode(string nodePattern)
        {
            // ѡ��Ҫɾ���Ľڵ�
            XmlNode deleteNode = xmlDoc.SelectSingleNode(nodePattern);

            if (deleteNode != null)
            {
                // ѡ��ýڵ�ĸ��ڵ�
                XmlNode parentNode = deleteNode.ParentNode;

                // �Ӹ��ڵ���ɾ���ýڵ��Ѿ��ýڵ�������ӽڵ�
                parentNode.RemoveChild(deleteNode);

            }
        }

        /// <summary>
        /// ��������ɾ�������ڵ㣬����ýڵ�û���ֹ����������ڵ�
        /// </summary>
        /// <param name="parentPattern">���ڵ��XPath</param>
        /// <param name="nodeName">�ڵ���</param>
        /// <param name="keep">����</param>
        public void UpdateNode(string parentPattern, string nodeName, bool keep)
        {
            // ����Ǹ�ɾ���ڵ㻹�Ǳ����ڵ�
            if (keep == false)
            {
                // ɾ���ڵ�
                DeleteNode(parentPattern + "/" + nodeName);
            }
            else
            {
                // ����ڵ㲻���ڣ�������
                if (VerifyNode(parentPattern + "/" + nodeName) == false)
                {
                    // �����ڵ�
                    AddNode(parentPattern, nodeName);
                }
            }
        }

        /// <summary>
        /// ��ȡָ���ڵ��ֵ(Ԫ�ػ�����).
        /// </summary>
        /// <param name="nodePattern">�ڵ��XPath</param>
        /// <returns>����ڵ�û�ҵ��ͷ��ؿգ�����ͷ��ؽڵ�ֵ</returns>				
        public string GetValue(string nodePattern)
        {
            string valueString = "";

            // ѡ��ڵ�
            XmlNode targetNode = xmlDoc.SelectSingleNode(nodePattern);

            if (targetNode != null)
            {
                // ��ȡ�ڵ�ֵ
                valueString = targetNode.InnerText;
            }

            // ����ֵ
            return valueString;
        }

        /// <summary>
        /// ����ָ���ڵ��ֵ������޷��ҵ��ڵ㣬������һ���ڵ�.
        /// </summary>
        /// <param name="nodePattern">�ڵ��XPath</param>
        /// <param name="valueString">�ڵ��ֵ</param>
        public void SetValue(string nodePattern, string valueString)
        {
            // ѡ��ָ���ڵ�
            XmlNode targetNode = xmlDoc.SelectSingleNode(nodePattern);

            if (targetNode == null)
            {
                // ��ȡ����һ��"/"��λ�ã�Ϊ�˽����ڵ���ڵ�ֿ�
                int lastSlash = nodePattern.LastIndexOf("/");

                // �����ڵ�
                AddNode(nodePattern.Substring(0, lastSlash), nodePattern.Substring(lastSlash + 1));

                // ѡ��ڵ�
                targetNode = xmlDoc.SelectSingleNode(nodePattern);
            }

            // ���½ڵ�ֵ
            targetNode.InnerText = valueString;
        }

        /// <summary>
        /// ��ȡָ���ڵ������ֵ.
        /// </summary>
        /// <param name="nodePattern">ָ���ڵ��XPath</param>
        /// <param name="attributeName">������</param>
        /// <returns>����ֵ</returns>				
        public string GetAttribute(string nodePattern, string attributeName)
        {
            string attributeString = "";

            // ѡ��ƥ��Ľڵ�
            XmlNode matchNode = xmlDoc.SelectSingleNode(nodePattern);

            if (matchNode != null)
            {
                // ��ȡ����ֵ
                try
                {
                    attributeString = matchNode.Attributes[attributeName].Value;
                }
                catch (NullReferenceException)
                {
                }
            }

            // ��������ֵ
            return attributeString;
        }

        /// <summary>
        /// ��ָ���ڵ��ָ��������ֵ.
        /// </summary>
        /// <param name="nodePattern">ָ���ڵ�� XPath</param>
        /// <param name="attributeName">������</param>
        /// <param name="attributeValue">����ֵ</param>
        public void SetAttribute(string nodePattern, string attributeName, string attributeValue)
        {
            // ѡ��ƥ��Ľڵ�
            XmlNode matchNode = xmlDoc.SelectSingleNode(nodePattern);

            if (matchNode != null)
            {
                // ѡ��ָ��������
                XmlAttribute matchAttribute = matchNode.Attributes[attributeName];

                // �������û�ҵ�������������
                if (matchAttribute == null)
                {
                    // ����һ���µ�����
                    XmlAttribute newAttribute = xmlDoc.CreateAttribute(attributeName);
                    newAttribute.Value = attributeValue;

                    // �����Լӵ��ڵ���
                    matchNode.Attributes.Append(newAttribute);

                }
                else
                {
                    // ��������ֵ
                    matchAttribute.Value = attributeValue;
                }
            }
        }

        /// <summary>
        /// ��ȡƥ���һ��ڵ��ָ�����Ե�ֵ������ָ���ķָ���������
        /// </summary>
        /// <param name="nodeGroupPattern">ָ���ڵ��XPath</param>
        /// <param name="attributeName">������</param>
        /// <param name="delimiter">�ָ���</param>
        /// <returns>�÷ָ����������ַ���</returns>			
        public string GetAttributeList(string nodeGroupPattern, string attributeName, char delimiter)
        {
            StringBuilder listString = new StringBuilder();

            // ѡ��ƥ��Ľڵ��б�
            XmlNodeList nodeList = xmlDoc.SelectNodes(nodeGroupPattern);

            // �����ڵ��б�
            foreach (XmlNode node in nodeList)
            {
                listString.Append(node.Attributes[attributeName].Value + delimiter);
            }

            //ȥ�����һ���ָ���
            string listText = listString.ToString();
            int lastIndex = listText.LastIndexOf(delimiter);
            if (lastIndex != -1)
                listText = listText.Remove(lastIndex, 1);

            // �����ַ���
            return listText;
        }

        /// <summary>
        /// ��ȡƥ���һ��ڵ��ֵ������ָ���ķָ�������.
        /// </summary>
        /// <param name="nodeGroupPattern">ָ���ڵ��XPath</param>
        /// <param name="delimiter">�ָ���</param>
        /// <returns>�÷ָ����������ַ���</returns>			
        public string GetValueList(string nodeGroupPattern, char delimiter)
        {
            StringBuilder listString = new StringBuilder();

            // ѡ��ƥ��Ľڵ��б�
            XmlNodeList nodeList = xmlDoc.SelectNodes(nodeGroupPattern);

            // �����ڵ��б�
            foreach (XmlNode node in nodeList)
            {
                listString.Append(node.Name + delimiter);
            }

            //ȥ�����һ���ָ���
            string listText = listString.ToString();
            int lastIndex = listText.LastIndexOf(delimiter);
            if (lastIndex != -1)
                listText = listText.Remove(lastIndex, 1);

            // �����ַ���
            return listText;
        }

        /// <summary>
        /// ��ָ���ڵ㸴�Ƶ�ָ���ĸ��ڵ���.
        /// </summary>
        /// <param name="sourcePattern">��Ҫ���ƵĽڵ��XPath</param>
        /// <param name="destinationPattern">Ŀ�ĸ��ڵ��XPath</param>
        public void CopyNode(string sourcePattern, string destinationPattern)
        {
            // ѡ����Դ�ڵ�
            XmlNode sourceNode = xmlDoc.SelectSingleNode(sourcePattern);

            // Ŀ�ĸ��ڵ�
            XmlNode destinationNode = xmlDoc.SelectSingleNode(destinationPattern);

            // ��¡Դ�ڵ㵽Ŀ�ĸ��ڵ���
            destinationNode.AppendChild(sourceNode.Clone());
        }

        /// <summary>
        /// XML���л�����
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
