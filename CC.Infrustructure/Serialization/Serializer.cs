using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;

namespace Infrustructure.Serialization
{
    public static class Serializer
    {
        /// <summary>
        /// 将对象序列化为json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String ObjectToJsonString(object obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            String strJson = "";
            //序列化
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                strJson = Encoding.UTF8.GetString(stream.ToArray());
            }

            return strJson;
        }

        /// <summary>
        /// 将json字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonStringToObject<T>(String jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)serializer.ReadObject(ms);
            }

        }

        public static T XmlToObject<T>(String xml)
        {
            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        public static object XmlToObject(String xml, object o)
        {
            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                return serializer.Deserialize(reader);
            }
        }

        public static String ObjectToXml(object obj)
        {
            using (StringWriter writer = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize((TextWriter)writer, obj);
                return writer.ToString();
            }
        }
    }
}
