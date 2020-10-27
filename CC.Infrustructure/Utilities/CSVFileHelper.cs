using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Infrustructure.Utilities
{
    public class CSVFileHelper
    {
        public static DataTable UploadExcelToDataSet(FileUpload fileUpload, string filePath, out string fileName)
        {
            string errMsg = string.Empty;
            //DataSet ds = null;
            try
            {
                if (!filePath.EndsWith("\\"))
                {
                    filePath += "\\";
                }
                //fileName = fileUpload.FileName.Replace(".xls", "") + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
                fileName = fileUpload.FileName.Replace(".csv", "") + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
                string fullFileName = filePath + fileName;
                if (Directory.Exists(filePath) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(filePath);
                }

                fileUpload.SaveAs(fullFileName);
                DataTable dt=null;
                if (OpenCSVFile(ref dt, fullFileName))
                    return dt;
                return null;
                

            }
            catch
            {
                throw (new System.Exception("上传并保存Excel文件发生错误"));
               
            }

             
        }
        public static bool OpenCSVFile(ref DataTable mycsvdt, string filepath)
        {
            mycsvdt = new DataTable();
            string strpath = filepath; //csv文件的路径            
            try
            {
                int intColCount = 0;
                bool blnFlag = true;

                DataColumn mydc;
                DataRow mydr;

                string strline;
                string[] aryline;
                StreamReader mysr = new StreamReader(strpath, System.Text.Encoding.Default);

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new char[] { ',' });
                    
                    //给datatable加上列名
                    if (blnFlag)
                    {
                        blnFlag = false;
                        intColCount = aryline.Length; 
                        mydr = mycsvdt.NewRow();
                        for (int i = 0; i < intColCount; i++)
                        {
                            mydc = new DataColumn(aryline[i]);
                            mycsvdt.Columns.Add(mydc);
                        }
                        //mycsvdt.Rows.Add(mydr);
                        continue;
                    }
                    if (aryline.Length != intColCount)
                        continue;
                    //填充数据并加入到datatable中
                    mydr = mycsvdt.NewRow();
                    for (int i = 0; i < intColCount; i++)
                    {
                        mydr[i] = aryline[i];
                    }
                    mycsvdt.Rows.Add(mydr);
                }
                return true;

            }
           catch
            {
                return false;
            }
        }


    }
}
