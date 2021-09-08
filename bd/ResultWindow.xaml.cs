using Model1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Printing;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using Model1.DataFiles;

namespace bd
{
    /// <summary>
    /// Логика взаимодействия для ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow()
        {
            InitializeComponent();

            ResultGrid.ItemsSource = Inventorys.GetResultInventoryList();
            ResultGrid2.ItemsSource = Inventorys.GetCurrentInventoryList();

        }

        private void PrintMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            /*PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(ResultGrid, "Распечатываем элемент Canvas");
            }
            Save();*/

            ExcelHelper.ListToExcel(Inventorys.GetResultInventoryList());
        }
        
        private void GetContac(out string res)
        {
            res = "";
            foreach (Inventory k in Inventorys.GetResultInventoryList())
            {
                string r = k.Workplaces != null ? k.Workplaces.Place : "-";
                string s = k.Workplaces != null ? k.Workplaces.Locations.Location : "-";
                res += $"{k.inventory_code}\t{k.Name}\t{k.Price}\t{k.Amount}\t{s}\t{r}\n";
            }

        }
        private void Save()
        {
            GetContac(out string res);
            Word.Application ap = new Word.Application();
            Word.Document document = ap.Documents.Open($"{Environment.CurrentDirectory}\\ResultGrid.docx", ReadOnly: false);
            Clipboard.SetText(res);
            document.Paragraphs.Add().Range.Paste();
            document.Close(Word.WdSaveOptions.wdSaveChanges);
        }

        
        private void PDFBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintClassHelper.Print(Inventorys.GetResultInventoryList(), "Неотсканированный инвентарь ");
        }

        private void PrintMenuBtn2_Click(object sender, RoutedEventArgs e)
        {
            ExcelHelper.ListToExcel(Inventorys.GetCurrentInventoryList());
        }

        private void PDFBtn2_Click(object sender, RoutedEventArgs e)
        {
            PrintClassHelper.Print(Inventorys.GetCurrentInventoryList(), "Отсканированный инвентарь ");
        }
    }
}
