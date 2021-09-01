using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Model1
{
    public class BarCodeGenerator
    {
        public static void GetBarcode(int height, int width, BarcodeLib.TYPE type, string code, out System.Drawing.Image image)
        {
            try
            {
                image = null;

                BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                b.BarWidth = 2;
                b.BackColor = System.Drawing.Color.White;
                b.ForeColor = System.Drawing.Color.Black;
                b.IncludeLabel = true;
                b.Alignment = BarcodeLib.AlignmentPositions.CENTER;
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


        public static MemoryStream MSBarCode(System.Drawing.Image img)
        {
            if (img != null)
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                byte[] buffer = ms.GetBuffer();

                MemoryStream bufferPasser = new MemoryStream(buffer);
                return bufferPasser;
            }
            return null;
        }
    }
}
