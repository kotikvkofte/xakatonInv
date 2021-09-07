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
    /// Логика взаимодействия для ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        public ListWindow()
        {
            InitializeComponent();
            LocationCmb.DisplayMemberPath = "Location";
            LocationCmb.SelectedValuePath = "Id";
            LocationCmb.ItemsSource = DataBaseActions.GetLocationsList();
            WorkplaceCmb.DisplayMemberPath = "Place";
            WorkplaceCmb.SelectedValuePath = "Id";
            WorkplaceCmb.ItemsSource = DataBaseActions.GetWorkplacesList();
            MainList.ItemsSource = DataBaseActions.GetAllInventoryList();
        }
        int SelectLoc, SelectWorkplace;

        private void LocationCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectLoc = Convert.ToInt32(LocationCmb.SelectedValue);
            WorkplaceCmb.ItemsSource = DataBaseActions.GetWorkplacesList().Where(x => x.IdLocation == SelectLoc);
            SearchInDB();
        }

        private void WorkplaceCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectWorkplace = Convert.ToInt32(WorkplaceCmb.SelectedValue);
            SearchInDB();
        }

        private void NameTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameTxb.Text != "")
            {
                NameTxb.Text = NameTxb.Text.FirstCharToUpper();
                NameTxb.Focus();
                NameTxb.SelectionStart = NameTxb.Text.Length;
            }
            SearchInDB();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            WorkplaceCmb.SelectedItem = null;
            LocationCmb.SelectedItem = null;
            NameTxb.Text = "";
            LocationCmb.ItemsSource = DataBaseActions.GetLocationsList();
            WorkplaceCmb.ItemsSource = DataBaseActions.GetWorkplacesList();
            MainList.ItemsSource = DataBaseActions.GetAllInventoryList();
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Inventory> inventories = (List<Inventory>)MainList.ItemsSource;
            PrintClassHelper.PrintAga(inventories);
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Inventory inventory = MainList.SelectedItem as Inventory;
            var Result = MessageBox.Show("Вы действительно хотите удалить выбранный инвентарь?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                OdbConnectHelper.entObj.Inventory.Remove(inventory);
                OdbConnectHelper.entObj.SaveChanges();
                SearchInDB();
                MessageBox.Show("Инвентарь удален");
            }
            else if (Result == MessageBoxResult.No)
            {
                return;
            }
        }

        private void SearchInDB()
        {
            MainList.ItemsSource = DataBaseActions.GetFiltredInventoryList(SelectLocation: SelectLoc, SelectWorkPlace: SelectWorkplace, SearchName: NameTxb.Text);
            if (LocationCmb.SelectedItem == null && WorkplaceCmb.SelectedItem == null && NameTxb.Text == "")
            {
                MainList.ItemsSource = DataBaseActions.GetAllInventoryList();
            }
            if (NameTxb.Text == "" && LocationCmb.SelectedItem != null && WorkplaceCmb.SelectedItem != null)
            {
                MainList.ItemsSource = DataBaseActions.GetFiltredInventoryList(SelectLoc, SelectWorkplace);
            }
            if (NameTxb.Text != "" && LocationCmb.SelectedItem == null && WorkplaceCmb.SelectedItem == null)
            {
                MainList.ItemsSource = DataBaseActions.GetFiltredInventoryList(NameTxb.Text);
            }
            if (NameTxb.Text == "" && LocationCmb.SelectedItem == null && WorkplaceCmb.SelectedItem != null)
            {
                MainList.ItemsSource = OdbConnectHelper.entObj.Inventory.Where(x => x.Workplaces.Id == SelectWorkplace).ToList();
            }
            if (NameTxb.Text == "" && LocationCmb.SelectedItem != null && WorkplaceCmb.SelectedItem == null)
            {
                MainList.ItemsSource = OdbConnectHelper.entObj.Inventory.Where(x => x.Workplaces.Locations.Id == SelectLoc).ToList();
            }
        }
    }
}
