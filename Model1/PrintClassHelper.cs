using Microsoft.Win32;
using Model1.DataFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Word = Microsoft.Office.Interop.Word;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Drawing;

namespace Model1
{
    public partial class PrintClassHelper
    {
        /// <summary>
        /// Метод добавления изображения в word
        /// </summary>

        public void AddCurrentImgToPrint(BitmapImage bitmap)
        {
            Word.Application ap = new Word.Application();
            Word.Document document;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "doc files (*.doc)|*.doc|All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                // Открываем выбранный документ
                document = ap.Documents.Open(dlg.FileName, ReadOnly: false);

                int k = document.Paragraphs.Count;
                document.Paragraphs.Add().Range.InsertAfter("\n");
                Clipboard.SetImage(bitmap);
                document.Paragraphs.Add().Range.Paste();
                document.Close(Word.WdSaveOptions.wdSaveChanges);
            }
            else
            {
                return;
            }
        }
        public int p;
        public static void GetContac(out string res)
        {
            int o = 0;
            res = "";
            foreach (Inventory k in Inventorys.GetResultInventoryList())
            {
                string r = k.Workplaces != null ? k.Workplaces.Place : "-";
                string s = k.Workplaces != null ? k.Workplaces.Locations.Location : "-";
                res += $"{k.inventory_code}\t{k.Name}\t{k.Price}\t{k.Amount}\t{s}\t{r}\n";
                o++;
            }

        }

        private static DataTable dt = new DataTable();
        private static DataSet dataSet = new DataSet();

        public static void ConvertToDatatable(List<Inventory> list)
        {

            try
            {
                dt.Columns.Add("Наименование");
                dt.Columns.Add("Инвентарный номер");
                dt.Columns.Add("Цена");
                dt.Columns.Add("Количество");
                dt.Columns.Add("Рабочее место");
                dt.Columns.Add("Месторасположение");
                dt.Columns.Add("Ответственное лицо");
                foreach (var item in list)
                {
                    var row = dt.NewRow();

                    row["Наименование"] = item.Name;
                    row["Инвентарный номер"] = item.inventory_code;
                    row["Цена"] = item.Price;
                    row["Количество"] = item.Amount;

                    if (item.Workplaces.Place != null)
                        row["Рабочее место"] = item.Workplaces.Place;
                    else
                        row["Рабочее место"] = "-";

                    if (item.Workplaces.Locations != null)
                        row["Месторасположение"] = item.Workplaces.Locations.Location;
                    else
                        row["Месторасположение"] = "-";
                    if (item.Responsible_Persons != null)
                        row["Ответственное лицо"] = item.Responsible_Persons.Name;
                    else
                        row["Ответственное лицо"] = "-";

                    
                    
                    
                    dt.Rows.Add(row);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Bitmap BitmImgToBitm(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        private static byte[] CreateImage(int fontSize, int width, int height, int i, List<Inventory> list)
        {
            using (var b = BitmImgToBitm(GetBitmap1(list)[i]))
            {
                using (var g = Graphics.FromImage(b))
                {
                    using (var br = new SolidBrush(System.Drawing.Color.Black))
                    {
                        using (var ms = new System.IO.MemoryStream())
                        {
                            b.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            return ms.ToArray();
                        }
                    }
                }
            }
        }

        public static void PrintAga(List<Inventory> list)
        {
            int col = 3; 
            iTextSharp.text.Document doc = new iTextSharp.text.Document();

            PdfWriter.GetInstance(doc, new FileStream($"C:\\Users\\" + Environment.UserName + "\\Desktop\\Список " + col + ".pdf", FileMode.Create));
            col += col + 1;
            doc.Open();
            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            
            for (int i = 0; i < list.Count; i++)
            {
                doc.Add(new Paragraph(Convert.ToString(GetInventorysNames1(list)[i]), font));

                var tif = iTextSharp.text.Image.GetInstance(CreateImage(10,200,70, i, list));

                tif.ScalePercent(80,70);

                doc.Add(tif);
            }
            doc.Close();
        }

        public static void Print(List<Inventory> list,string a)
        {
            
            try
            {
                ConvertToDatatable(list);
                var gg = dt.Copy();
                dataSet.Tables.Add(gg);
                //Creates an empty PDF document instance
                iTextSharp.text.Document doc = new iTextSharp.text.Document();
                int col = 0;
                //Создаем объект записи пдф-документа в файл
                try
                {
                    col++;
                    PdfWriter.GetInstance(doc, new FileStream($"C:\\Users\\" + Environment.UserName + "\\Desktop\\Инвенторизация " + col + ".pdf", FileMode.Create));
                    doc.Open();

                }
                catch (Exception)
                {

                }
                //Открываем документ

                //Определение шрифта необходимо для сохранения кириллического текста
                //Иначе мы не увидим кириллический текст
                //Если мы работаем только с англоязычными текстами, то шрифт можно не указывать
                string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
                var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                var font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                //Обход по всем таблицам датасета (хотя в данном случае мы можем опустить
                //Так как в нашей бд только одна таблица)
                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    //Создаем объект таблицы и передаем в нее число столбцов таблицы из нашего датасета
                    PdfPTable table = new PdfPTable(dataSet.Tables[i].Columns.Count);

                    //Добавим в таблицу общий заголовок
                    PdfPCell cell = new PdfPCell(new Phrase(a+ DateTime.Now + (i + 1), font));

                    cell.Colspan = dataSet.Tables[i].Columns.Count;
                    cell.HorizontalAlignment = 1;
                    //Убираем границу первой ячейки, чтобы балы как заголовок
                    cell.Border = 0;
                    table.AddCell(cell);

                    //Сначала добавляем заголовки таблицы
                    for (int j = 0; j < dataSet.Tables[i].Columns.Count; j++)
                    {
                        cell = new PdfPCell(new Phrase(new Phrase(dataSet.Tables[i].Columns[j].ColumnName, font)));
                        //Фоновый цвет (необязательно, просто сделаем по красивее)
                        cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                        table.AddCell(cell);
                    }

                    //Добавляем все остальные ячейки
                    for (int j = 0; j < dataSet.Tables[i].Rows.Count; j++)
                    {
                        for (int k = 0; k < dataSet.Tables[i].Columns.Count; k++)
                        {
                            table.AddCell(new Phrase(dataSet.Tables[i].Rows[j][k].ToString(), font));
                        }
                    }
                    //Добавляем таблицу в документ
                    doc.Add(table);
                    doc.Close();
                    
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void ClearDataTable()
        {
            dt.Clear();
        }

        public static DataTable NameList = new DataTable();
        public static DataTable BitmapList = new DataTable();
        public static DataTable ResultList = new DataTable();
        public static DataSet ResultSet = new DataSet();
        public static void SumNameBit()
        {
            NameList.Columns.Add("Name");
            foreach (var item in GetInventorysNames())
            {
                var row = NameList.NewRow();

                row["Name"] = item;
                NameList.Rows.Add(row);
            }

            BitmapList.Columns.Add("Image");
            foreach (var item in GetBitmap())
            {
                var row = BitmapList.NewRow();
                row["Image"] = item;
                BitmapList.Rows.Add(row);
            }

            ResultList.Columns.Add("Name");
            ResultList.Columns.Add("Bitmap");
            for (int i = 0; i < GetInventorysNames().Count; i++)
            {
                var row = ResultList.NewRow();
                row["Name"] = GetInventorysNames()[i];
                row["Bitmap"] = GetBitmap()[i];
                ResultList.Rows.Add(row);
            }   
        }

        public static List<string> GetInventorysNames()
        {
            List<string> NamesInventorysList = new List<string>();
            foreach (var item in DataBaseActions.GetAllInventoryList())
            {
                NamesInventorysList.Add(Convert.ToString(item.Name));
            }
            return NamesInventorysList;
        }
        public static List<BitmapImage> GetBitmap()
        {
            List<BitmapImage> BitmapList = new List<BitmapImage>();
            BitmapImage bitmap = new BitmapImage();
            System.Drawing.Image img = null;
            foreach (var item in DataBaseActions.GetAllInventoryList())
            {
                BarCodeGenerator.GetBarcode(70, 200, BarcodeLib.TYPE.CODE128, item.inventory_code, out img);
                bitmap.BeginInit();
                bitmap.StreamSource = BarCodeGenerator.MSBarCode(img);
                bitmap.EndInit();
                BitmapList.Add(bitmap);
                bitmap = new BitmapImage();
            }
            return BitmapList;
        }

        public static List<string> GetInventorysNames1(List<Inventory> list)
        {
            List<string> NamesInventorysList = new List<string>();
            foreach (var item in list)
            {
                NamesInventorysList.Add(Convert.ToString(item.Name));
            }
            return NamesInventorysList;
        }

        public static List<BitmapImage> GetBitmap1(List<Inventory> list)
        {
            List<BitmapImage> BitmapList = new List<BitmapImage>();
            BitmapImage bitmap = new BitmapImage();
            System.Drawing.Image img = null;
            foreach (var item in list)
            {
                BarCodeGenerator.GetBarcode(70, 200, BarcodeLib.TYPE.CODE128, item.inventory_code, out img);
                bitmap.BeginInit();
                bitmap.StreamSource = BarCodeGenerator.MSBarCode(img);
                bitmap.EndInit();
                BitmapList.Add(bitmap);
                bitmap = new BitmapImage();
            }
            return BitmapList;
        }
    }
}
