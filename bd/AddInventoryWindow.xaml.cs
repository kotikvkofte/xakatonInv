using Model1;
using Model1.DataFiles;
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

namespace bd
{
    /// <summary>
    /// Логика взаимодействия для AddInventoryWindow.xaml
    /// </summary>
    public partial class AddInventoryWindow : Window
    {
        string Code;
        public AddInventoryWindow(string InventoryCode)
        {
            InitializeComponent();
            Code = InventoryCode;
            if (Code == null)
            {
                Code = "";
            }
            CodeTxb.Text = Code;
            LocationCmb.DisplayMemberPath = "Location";
            LocationCmb.SelectedValuePath = "Id";
            LocationCmb.ItemsSource = DataBaseActions.GetLocationsList();
            WorkplaceCmb.DisplayMemberPath = "Place";
            WorkplaceCmb.SelectedValuePath = "Id";
        }

        private void AddInventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WorkplaceCmb.SelectedItem == null || LocationCmb.SelectedItem == null || AmountTxb.Text == "" || CodeTxb.Text == "" || NameTxb.Text == "" || PriceTxb.Text == "")
            {
                MessageBox.Show("Заполните все поля", "Уведомление",MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (DataBaseActions.GetAllInventoryList().FirstOrDefault(X => X.inventory_code == CodeTxb.Text) != null)
                {
                    MessageBox.Show("Такой инвентарный номер уже имеется в базе данных", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    int SelectWorkplaceId = Convert.ToInt32(WorkplaceCmb.SelectedValue);
                    Workplaces workplaces = OdbConnectHelper.entObj.Workplaces.FirstOrDefault(x => x.Id == SelectWorkplaceId);

                    Inventory inventory = new Inventory()
                    {
                        Amount = Convert.ToInt32(AmountTxb.Text),
                        inventory_code = CodeTxb.Text,
                        Name = NameTxb.Text.FirstCharToUpper(),
                        Price = Convert.ToInt32(PriceTxb.Text),
                        Workplaces = workplaces
                    };

                    try
                    {
                        DataBaseActions.AddInventory(inventory);
                        OdbConnectHelper.entObj.SaveChanges();
                        MessageBox.Show("Добавлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Критическая ошибка приложения" + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                
            }
        }

        private void LocationCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LocationCmb.SelectedItem != null)
            {
                int SelectedLocId = Convert.ToInt32(LocationCmb.SelectedValue);
                WorkplaceCmb.IsEnabled = true;
                WorkplaceCmb.ItemsSource = DataBaseActions.GetWorkplacesList().Where(x => x.IdLocation == SelectedLocId);
            }
        }
    }
}
