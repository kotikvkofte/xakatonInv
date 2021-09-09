using Model1;
using Model1.DataFiles;
using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Word = Microsoft.Office.Interop.Word;
namespace bd
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Inventory inventory;
        bool StartInventory = false;
        public MainWindow()
        {
            InitializeComponent();
            Keyboard.Focus(BarcodeTxb);
            OdbConnectHelper.entObj = new test1Entities();
            LocationCmb.DisplayMemberPath = "Location";
            LocationCmb.SelectedValuePath = "Id";
            LocationCmb.ItemsSource = DataBaseActions.GetLocationsList();
            WorkplaceCmb.DisplayMemberPath = "Place";
            WorkplaceCmb.SelectedValuePath = "Id";
            RespPerson.DisplayMemberPath = "Name";
            RespPerson.SelectedValuePath = "Id";
            RespPerson.ItemsSource = DataBaseActions.GetAllRespPersonsList();
            BarcodeTxb.Focus();
            UpdateCurrentInventory();
        }
        BitmapImage bitmap1;
        private void BarcodeTxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Drawing.Image img = null;
            
            if (BarcodeTxb.Text!="")
            {
                BarCodeGenerator.GetBarcode(70, 200, BarcodeLib.TYPE.CODE128, BarcodeTxb.Text, out img);

                if (img != null)
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = BarCodeGenerator.MSBarCode(img);
                    bitmap.EndInit();
                    ScanCodeImg.Source = bitmap;
                    bitmap1 = bitmap;
                    OutputFromDB();
                    SelectAllText();
                    Inventorys.AddToCurrentInventoy(inventory);
                    UpdateCurrentInventory();
                }
                else
                {
                    ClearFields();
                }
            }
            else
            {
                ClearFields();
            }
           
        }

        private void OutputFromDB()
        {
            LocationCmb.SelectedItem = "";
            inventory = DataBaseActions.FindBarCode(BarcodeTxb.Text);
            if (inventory != null)
            {
                NameTxt.Text = inventory.Name;
                PriceTxt.Text = inventory.Price.ToString();
                AmountTxt.Text = inventory.Amount.ToString();

                if (inventory.Workplaces == null)
                {
                    LocationCmb.SelectedItem = "";
                }
                else
                {
                    LocationCmb.SelectedItem = inventory.Workplaces.Locations;
                }
                
                WorkplaceCmb.SelectedItem = inventory.Workplaces;

                InputPanel.Visibility = Visibility.Hidden;
                MainPanel.Visibility = Visibility.Visible;
            }
            else
            {
                ClearFields();
            }
        }
        private void ClearFields()
        {
            NameTxt.Text = "";
            PriceTxt.Text = "";
            AmountTxt.Text = "";
            LocationCmb.SelectedItem = null;
            ScanCodeImg.Source = null;

            MainPanel.Visibility = Visibility.Hidden;
            InputPanel.Visibility = Visibility.Visible;

            if (BarcodeTxb.Text != "")
            {
                InputMsg.Text = "Код не найден";
            }
            else
            {
                InputMsg.Text = "Введите инвентарный номер";
            }
        }

        private void AddLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            AddLocationWindow locationWindow = new AddLocationWindow();

            locationWindow.ShowDialog();
            LocationCmb.ItemsSource = DataBaseActions.GetLocationsList();
            Keyboard.Focus(BarcodeTxb);
        }

        private void AddWorkplaceBtn_Click(object sender, RoutedEventArgs e)
        {
            AddWorkplaceWindow addWorkplace = new AddWorkplaceWindow();

            addWorkplace.ShowDialog();
            LocationCmb.ItemsSource = DataBaseActions.GetLocationsList();
            Keyboard.Focus(BarcodeTxb);
        }

        int SelectedValue1, SelectedValue2, SelectedValue3;

        private void LocationCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BarcodeTxb.Text == "")
            {
                return;
            }
            if (inventory != null)
            {
                try
                {
                    if (LocationCmb.SelectedItem != null)
                    {
                        WorkplaceCmb.SelectedItem = null;
                        SelectedValue1 = Convert.ToInt32(LocationCmb.SelectedValue);
                        WorkplaceCmb.ItemsSource = DataBaseActions.GetWorkplacesList().Where(x => x.IdLocation == SelectedValue1);
                        WorkplaceCmb.IsEnabled = true;
                        OdbConnectHelper.entObj.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Критическая ошибка приложения" + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void WorkplaceCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (WorkplaceCmb.SelectedItem != null)
                {
                    SelectedValue2 = Convert.ToInt32(WorkplaceCmb.SelectedValue);
                    inventory.IdWorkplace = SelectedValue2;
                    OdbConnectHelper.entObj.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критическая ошибка приложения" + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        private void RespPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BarcodeTxb.Text == "")
            {
                return;
            }
            if (inventory != null)
            {
                try
                {
                    if (RespPerson.SelectedItem != null)
                    {
                        SelectedValue3 = Convert.ToInt32(RespPerson.SelectedValue);
                        inventory.IdPerson = SelectedValue3;
                        OdbConnectHelper.entObj.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Критическая ошибка приложения" + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void SelectAllText()
        {
            if (SelectChkb.IsChecked == true)
            {
                BarcodeTxb.SelectAll();
            }
        }

        private void BarcodeTxb_GotFocus(object sender, RoutedEventArgs e)
        {
            SelectAllText();
        }

        private void SelectChkb_Checked(object sender, RoutedEventArgs e)
        {
            BarcodeTxb.SelectionStart = 0;
            BarcodeTxb.SelectionLength = BarcodeTxb.Text.Length;
            BarcodeTxb.Focus();
        }

        private void InventoryzationButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartInventory == false)
            {
                InventoryzationButton.Content = " Остановить ";
                InventoryzationContinueButton.Visibility = Visibility.Hidden;
                StartInventory = true;
                ChkbsInventoryPanel.Visibility = Visibility.Visible;
                InventoryTotalTxt.Text = DataBaseActions.GetAllInventoryList().Count().ToString();
            }
            else
            {
                InventoryzationButton.Content = " Начать инвентаризацию ";
                InventoryzationContinueButton.Visibility = Visibility.Visible;
                StartInventory = false;
                ChkbsInventoryPanel.Visibility = Visibility.Hidden;
                ResultWindow window = new ResultWindow();
                window.ShowDialog();
            }
            Keyboard.Focus(BarcodeTxb);
        }

        private void UpdateCurrentInventory()
        {
            if (StartInventory == true || ContinueInventarization == true)
            {
                InventoryCurrentTxt.Text = Inventorys.GetCurrentInventoyNumb().ToString();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Word.Application ap = new Word.Application();
            Word.Document document = ap.Documents.Open($"{Environment.CurrentDirectory}\\Doc1.docx", ReadOnly:false);

            int k = document.Paragraphs.Count;
            document.Paragraphs.Add().Range.InsertAfter("\n");
            Clipboard.SetImage(bitmap1);
            document.Paragraphs.Add().Range.Paste();
            document.Close(Word.WdSaveOptions.wdSaveChanges);
            Keyboard.Focus(BarcodeTxb);

        }

        private void SelectChkb_Unchecked(object sender, RoutedEventArgs e)
        {
            BarcodeTxb.SelectionStart = BarcodeTxb.Text.Length;
            BarcodeTxb.SelectionLength = BarcodeTxb.Text.Length;
            BarcodeTxb.Focus();
        }

        private void UpdateDBBtn_Click_1(object sender, RoutedEventArgs e)
        {
            ExcelHelper.ReadExcelFile();
        }

        private void AddRepPersonBtn_Click(object sender, RoutedEventArgs e)
        {
            AddRespPersonWindow addRespPerson = new AddRespPersonWindow();
            addRespPerson.ShowDialog();
            Keyboard.Focus(BarcodeTxb);
        }
        bool ContinueInventarization = false;
        private void InventoryzationContinueButton_Click(object sender, RoutedEventArgs e)
        {
            ContinueInv();
        }

        private void AddInventory_Click(object sender, RoutedEventArgs e)
        {
            AddInventoryWindow addInventoryWindow = new AddInventoryWindow(BarcodeTxb.Text);
            addInventoryWindow.ShowDialog();
            Keyboard.Focus(BarcodeTxb);
        }

        

        private void OpenListBtn_Click(object sender, RoutedEventArgs e)
        {
            ListWindow listWindow = new ListWindow();

            listWindow.ShowDialog();
            Keyboard.Focus(BarcodeTxb);
        }

        private void ContinueInv()
        {
            if (ContinueInventarization == false)
            {
                var lst = ExcelHelper.ExcelToList();//пшел на хуй:)
                if (lst == null)
                {
                    return;
                }
                Inventorys.SetCurrentList(lst);
                InventoryzationContinueButton.Content = " Остановить ";
                InventoryzationButton.Visibility = Visibility.Hidden;
                ContinueInventarization = true;
                ChkbsInventoryPanel.Visibility = Visibility.Visible;
                InventoryTotalTxt.Text = DataBaseActions.GetAllInventoryList().Count().ToString();
                InventoryCurrentTxt.Text = Inventorys.GetCurrentInventoyNumb().ToString();
            }
            else
            {
                InventoryzationButton.Content = " Продолжить инвентаризацию ";
                InventoryzationButton.Visibility = Visibility.Visible;
                ContinueInventarization = false;
                ChkbsInventoryPanel.Visibility = Visibility.Hidden;
                ResultWindow window = new ResultWindow();
                window.ShowDialog();
            }
            Keyboard.Focus(BarcodeTxb);
        }
    }
}
