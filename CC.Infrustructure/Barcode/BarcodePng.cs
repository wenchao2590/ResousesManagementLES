using Infrustructure.Utilities;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;

namespace Infrustructure.Barcode
{
    public class BarcodePng
    {
        public static void ConvertByteQrCodeToWriteableBitmap(string content, string fileName, int width, string filePath)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options.Height = writer.Options.Width = width;
            writer.Options.Margin = 0;//设置边框
            ZXing.Common.BitMatrix bm = writer.Encode(content);
            Bitmap bmap = writer.Write(bm);
            bmap.Save(filePath + @"\" + fileName, ImageFormat.Png);
        }

        public static void ConvertByteDataMatrixToWriteableBitmap(string content, string fileName, int width, string filePath)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.DATA_MATRIX;
            writer.Options.Height = writer.Options.Width = width;
            writer.Options.Margin = 0;//设置边框
            ZXing.Common.BitMatrix bm = writer.Encode(content);
            Bitmap bmap = writer.Write(bm);
            bmap.Save(filePath + @"\" + fileName, ImageFormat.Png);
        }

        public static void ConvertBytePdf417ToWriteableBitmap(string content, string fileName, int width, string filePath)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.PDF_417;
            writer.Options.Height = width / 4;
            writer.Options.Width = width;
            writer.Options.Margin = 0;//设置边框
            ZXing.Common.BitMatrix bm = writer.Encode(content);
            Bitmap bmap = writer.Write(bm);
            bmap.Save(filePath + @"\" + fileName, ImageFormat.Png);
        }

        /// <summary>
        /// Code128
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="filePath"></param>
        public static void GetCode128ToBitmap(string content, string fileName, int width, string filePath)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.CODE_128;
            writer.Options.Height = width / 4;
            writer.Options.Width = width;
            writer.Options.Margin = 0;//设置边框
            ZXing.Common.BitMatrix bm = writer.Encode(content);
            Bitmap bmap = writer.Write(bm);
            bmap.Save(filePath + @"\" + fileName, ImageFormat.Png);
        }

        public static void GetCode39ToBitmap(string content, string fileName, int width, string filePath)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.CODE_39;
            writer.Options.Height = width / 4;
            writer.Options.Width = width;
            writer.Options.Margin = 0;//设置边框
            ZXing.Common.BitMatrix bm = writer.Encode(content);
            Bitmap bmap = writer.Write(bm);
            bmap.Save(filePath + @"\" + fileName, ImageFormat.Png);
        }
    }
}
