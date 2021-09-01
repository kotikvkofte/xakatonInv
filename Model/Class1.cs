using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Model
{
    public class BarCodeGenerator
    {
        public static void GetBarcode(int height, int width, BarcodeLib.TYPE type, string code, out System.Drawing.Image image)
        {
            try
            {
                image = null;

                BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                b.BackColor = System.Drawing.Color.White;
                b.ForeColor = System.Drawing.Color.Black;
                b.IncludeLabel = true;
                b.Alignment = BarcodeLib.AlignmentPositions.LEFT;
                b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
                b.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                System.Drawing.Font font = new System.Drawing.Font("verdana", 10f);
                b.LabelFont = font;
                b.Height = height;
                b.Width = width;

                image = b.Encode(type, code);

            }
            catch (Exception err)
            {
                err.ToString();
                image = null;
            }
        }


        public static BitmapImage AddBarCode(string msg)
        {
            System.Drawing.Image img;
            GetBarcode(200, 500, BarcodeLib.TYPE.CODE128B, msg, out img);

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            byte[] buffer = ms.GetBuffer();

            MemoryStream bufferPasser = new MemoryStream(buffer);

            BitmapImage bitmap = new BitmapImage();
            bitmap.StreamSource = bufferPasser;

            return bitmap;

        }
    }
}
