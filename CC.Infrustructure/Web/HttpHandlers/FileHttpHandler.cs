using System.Web;
using System.IO;

namespace Infrustructure.Web.HttpHandlers
{
    public class FileHttpHandler: IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string file = context.Request.RawUrl;
            string extension = file.Substring(file.LastIndexOf('.') + 1).ToLower();
            switch (extension)
            { 
                case "csv":
                case "xls":
                    file = context.Server.MapPath(file);
                    //string realFileName = file.Substring(file.LastIndexOf('\\') + 1);
                    //context.Response.Clear();
                    //context.Response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(realFileName));
                    ////context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                    //context.Response.ContentType = "application/octet-stream";
                    ////context.Response.ContentType = "application/vnd.ms-excel";
                    //context.Response.ContentEncoding = System.Text.Encoding.Unicode;
                    //context.Response.TransmitFile(file);
                    ////context.Response.Write(file);
                    //context.Response.Flush();
                    //context.Response.End();

                    // TODO: 如果文件超大?
                    using (FileStream fileStream = new FileStream(file, FileMode.Open))
                    {
                        long fileSize = fileStream.Length;
                        context.Response.Buffer = true;
                        context.Response.Clear();
                        context.Response.ContentType = "application/octet-stream";
                        context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + file + "\";");
                        context.Response.AddHeader("Content-Length", fileSize.ToString());
                        byte[] fileBuffer = new byte[fileSize];
                        fileStream.Read(fileBuffer, 0, (int)fileSize);
                        context.Response.BinaryWrite(fileBuffer);
                        context.Response.End();
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
