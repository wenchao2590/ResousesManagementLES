//-------------------------------------------------------------------
//版权所有：版权所有(C) 2006，Microsoft(China) Co.,LTD
//系统名称：GMCC-ADC
//文件名称：
//模块名称：
//模块编号：
//作　　者：
//完成日期：
//功能说明：
//-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

using System.Security.Cryptography;
using System.IO;

namespace Infrustructure.Utilities
{
    /// <summary>
    /// 类名称   ：CryptTools
    /// 类说明   ：加解密算法
    /// 作者     ：
    /// 完成日期 ：
    /// </summary>
	public static class CryptTools
    {
        /// <summary>
        /// 方法说明　：加密方法
        /// 作者    　： 
        /// 完成日期　：
        /// </summary>
        /// <param name="content">需要加密的明文内容</param>
        /// <param name="secret">加密密钥</param>
        /// <returns>返回加密后密文字符串</returns>
        public static string Encrypt(string content, string secret)
        {
            if ((content == null) || (secret == null) || (content.Length == 0) || (secret.Length == 0))
                throw new ArgumentNullException("Invalid Argument");

            byte[] key = GetKey(secret);
            byte[] contentByte = Encoding.Unicode.GetBytes(content);
            using(MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(contentByte, 0, contentByte.Length);

                byte[] contentCryptByte = Crypt(memoryStream.ToArray(), key);

                return Encoding.ASCII.GetString(Base64Encode(contentCryptByte));
            }

        }

        /// <summary>
        /// 方法说明　：解密方法
        /// 作者    　： 
        /// 完成日期　：
        /// </summary>
        /// <param name="content">需要解密的密文内容</param>
        /// <param name="secret">解密密钥</param>
        /// <returns>返回解密后明文字符串</returns>
        public static string Decrypt(string content, string secret)
        {
            if ((content == null) || (secret == null) || (content.Length == 0) || (secret.Length == 0))
                throw new ArgumentNullException("Invalid Argument");

            byte[] Key = GetKey(secret);

            byte[] CryByte = Base64Decode(Encoding.ASCII.GetBytes(content));
            byte[] DecByte = Decrypt(CryByte, Key);

            byte[] RealDecByte;
            string RealDecStr;
            
            RealDecByte = DecByte;
            byte[] Prefix = new byte[CryptConstants.Operation.UnicodeReversePrefix.Length];
            Array.Copy(RealDecByte, Prefix, 2);

            if (CompareByteArrays(CryptConstants.Operation.UnicodeReversePrefix, Prefix))
            {
                byte SwitchTemp = 0;
                for (int i = 0; i < RealDecByte.Length - 1; i = i + 2)
                {
                    SwitchTemp = RealDecByte[i];
                    RealDecByte[i] = RealDecByte[i + 1];
                    RealDecByte[i + 1] = SwitchTemp;
                }
            }

            RealDecStr = Encoding.Unicode.GetString(RealDecByte);
            return RealDecStr;
        }
    
        //使用TripleDES加密
        public static byte[] Crypt(byte[] source, byte[] key)
        {
            if ((source.Length == 0) || (source == null) || (key == null) || (key.Length == 0))
            {
                throw new ArgumentException("Invalid Argument");
            }

            TripleDESCryptoServiceProvider dsp = null;
            try
            {
                dsp = new TripleDESCryptoServiceProvider();
                dsp.Mode = CipherMode.ECB;

                ICryptoTransform des = dsp.CreateEncryptor(key, null);

                return des.TransformFinalBlock(source, 0, source.Length);    
            }
            finally
            {
                if(dsp!=null)
                {
                    dsp.Dispose();
                }
            }
        
        }

        public static byte[] Decrypt(byte[] source, byte[] key)
        {
            if ((source.Length == 0) || (source == null) || (key == null) || (key.Length == 0))
            {
                throw new ArgumentNullException("Invalid Argument");
            }

            TripleDESCryptoServiceProvider dsp = null;
            try
            {


                dsp = new TripleDESCryptoServiceProvider();
                dsp.Mode = CipherMode.ECB;

                ICryptoTransform des = dsp.CreateDecryptor(key, null);

                byte[] ret = new byte[source.Length + 8];

                int num;
                num = des.TransformBlock(source, 0, source.Length, ret, 0);

                ret = des.TransformFinalBlock(source, 0, source.Length);
                ret = des.TransformFinalBlock(source, 0, source.Length);
                num = ret.Length;

                byte[] RealByte = new byte[num];
                Array.Copy(ret, RealByte, num);
                ret = RealByte;
                return ret;
            }finally
            {
                if(dsp!=null)
                {
                    dsp.Dispose();
                }
            }
        }

        //原始base64编码
        public static byte[] Base64Encode(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");
            ToBase64Transform tb64 = null;
            MemoryStream stm = null;

            try
            {
                tb64 = new ToBase64Transform();
                stm = new MemoryStream();
                int pos = 0;
                byte[] buff;

                while (pos + 3 < source.Length)
                {
                    buff = tb64.TransformFinalBlock(source, pos, 3);
                    stm.Write(buff, 0, buff.Length);
                    pos += 3;
                }

                buff = tb64.TransformFinalBlock(source, pos, source.Length - pos);
                stm.Write(buff, 0, buff.Length);

                return stm.ToArray();

            }
            finally
            {
                if(tb64!=null)
                {
                    tb64.Dispose();
                }
                if(stm!=null)
                {
                    stm.Dispose();
                }
            }
        }

        //原始base64解码
        public static byte[] Base64Decode(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");
            FromBase64Transform fb64 = null;
            MemoryStream stm = null;
            try
            {
                fb64 = new FromBase64Transform();
                stm = new MemoryStream();
                int pos = 0;
                byte[] buff;

                while (pos + 4 < source.Length)
                {
                    buff = fb64.TransformFinalBlock(source, pos, 4);
                    stm.Write(buff, 0, buff.Length);
                    pos += 4;
                }

                buff = fb64.TransformFinalBlock(source, pos, source.Length - pos);
                stm.Write(buff, 0, buff.Length);
                return stm.ToArray();

            }
            finally
            {
                if(fb64!=null)
                {
                    fb64.Dispose();
                }
                if(stm!=null)
                {
                    stm.Dispose();
                }
            }

        }

        public static byte[] GetKey(string secret)
        {
            if ((secret == null) || (secret.Length == 0))
                throw new ArgumentException("Secret is not valid");

            byte[] temp;

            ASCIIEncoding ae = new ASCIIEncoding();
            temp = Hash(ae.GetBytes(secret));

            byte[] ret = new byte[CryptConstants.Operation.KeySize];

            int i;

            if (temp.Length < CryptConstants.Operation.KeySize)
            {
                System.Array.Copy(temp, 0, ret, 0, temp.Length);
                for (i = temp.Length; i < CryptConstants.Operation.KeySize; i++)
                {
                    ret[i] = 0;
                }
            }
            else
                System.Array.Copy(temp, 0, ret, 0, CryptConstants.Operation.KeySize);

            return ret;
        }

        //比较两个byte数组是否相同
        public static bool CompareByteArrays(byte[] source, byte[] dest)
        {
            if ((source == null) || (dest == null))
                throw new ArgumentException("source or dest is not valid");

            bool ret = true;

            if (source.Length != dest.Length)
                return false;
            else
                if (source.Length == 0)
                    return true;

            for (int i = 0; i < source.Length; i++)
                if (source[i] != dest[i])
                {
                    ret = false;
                    break;
                }
            return ret;
        }

        //使用md5计算散列
        public static byte[] Hash(byte[] source)
        {
            if ((source == null) || (source.Length == 0))
                throw new ArgumentException("source is not valid");

            using (MD5 m = MD5.Create())
            {
                return m.ComputeHash(source);
            }
        }

        /// <summary>
        /// 对传入的明文密码进行Hash加密,密码不能为中文
        /// </summary>
        /// <param name="oriPassword">需要加密的明文密码</param>
        /// <returns>经过Hash加密的密码</returns>
        public static string HashPassword(string oriPassword)
        {
            if (string.IsNullOrEmpty(oriPassword))
                throw new ArgumentException("oriPassword is valid");

            ASCIIEncoding acii = new ASCIIEncoding();
            byte[] hashedBytes = Hash(acii.GetBytes(oriPassword));

            StringBuilder sb = new StringBuilder(30);
            foreach (byte b in hashedBytes)
            {
                sb.AppendFormat("{0:X2}",b);
            }
            return sb.ToString();
            
        }

        /// <summary>
        /// 将一个字符串进行base64编码後返回
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string EncodeBase64(string code_type,string code)
        {
               string encode = "";
               byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
               try
               {
                encode = Convert.ToBase64String(bytes);
               }
               catch
               {
                encode = code;
               }
               return encode;
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DecodeBase64(string code_type, string code)
          {
                   string decode = "";
                   byte[] bytes = Convert.FromBase64String(code);
                   try
                   {
                    decode = Encoding.GetEncoding(code_type).GetString(bytes);
                   }
                   catch
                   {
                    decode = code;
                   }
                   return decode;
          }



    }
    /// <summary>
    /// 类名称   ：CryptConstants
    /// 类说明   ：加解密算法常量.
    /// 作者     ：
    /// 完成日期 ：
    /// </summary>
    public class CryptConstants
    {
        public const string PassKey = "Password1@Foton";
        public struct Operation
        {
            public static readonly int KeySize = 24;
            public static readonly byte[] UnicodeOrderPrefix   = new byte[2] { 0xFF, 0xFE };
            public static readonly byte[] UnicodeReversePrefix = new byte[2] { 0xFE, 0xFF };
        }
    }
}
