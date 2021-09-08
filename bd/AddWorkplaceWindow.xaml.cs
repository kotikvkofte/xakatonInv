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
    /// Логика взаимодействия для AddWorkplaceWindow.xaml
    /// </summary>
    public partial class AddWorkplaceWindow : Window
    {
        public AddWorkplaceWindow()
        {
            InitializeComponent();
            WorkplaceList.ItemsSource = DataBaseActions.GetWorkplacesList();
            LocationCmb.DisplayMemberPath = "Location";
            LocationCmb.SelectedValuePath = "Id";
            LocationCmb.ItemsSource = DataBaseActions.GetLocationsList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SaveBtnChecker ==  true)
            {
                Workplaces workplaces = OdbConnectHelper.entObj.Workplaces.FirstOrDefault(x => x.Id == SelectedId);
                workplaces.IdLocation = Convert.ToInt32(LocationCmb.SelectedValue);
                workplaces.Place = WorkplaceTxb.Text;
                OdbConnectHelper.entObj.SaveChanges();
                LocationCmb.SelectedItem = null;
                WorkplaceTxb.Text = "";
                AddBtn.Content = "Добавить";
                SaveBtnChecker = false;
                SelectedId = 0;
                WorkplaceList.ItemsSource = DataBaseActions.GetWorkplacesList();
                MessageBox.Show("Изменения сохранены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (LocationCmb.SelectedItem != null && WorkplaceTxb.Text != "" && WorkplaceTxb.Text != null && SaveBtnChecker == false)
            {
                string workplc = WorkplaceTxb.Text.FirstCharToUpper();
                if (OdbConnectHelper.entObj.Workplaces.FirstOrDefault(x => x.Place == workplc) == null)
                {
                    int SelectedValue = Convert.ToInt32(LocationCmb.SelectedValue);
                    Workplaces workplaces = new Workplaces()
                    {
                        Place = workplc,
                        IdLocation = SelectedValue
                    };
                    OdbConnectHelper.entObj.Workplaces.Add(workplaces);
                    OdbConnectHelper.entObj.SaveChanges();
                    WorkplaceList.ItemsSource = DataBaseActions.GetWorkplacesList();
                }
                else
                {
                    MessageBox.Show("Такое рабочее место уже есть", "Внимание", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля","Внимание", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void ViewToTxbCmb()
        {
            Workplaces wp = WorkplaceList.SelectedItem as Workplaces;
            LocationCmb.SelectedItem = wp.Locations;
            WorkplaceTxb.Text = wp.Place;
        }
        bool SaveBtnChecker = false;
        int SelectedId;
        private void ContextMenuBtn1_Click(object sender, RoutedEventArgs e)
        {
            Workplaces wp = WorkplaceList.SelectedItem as Workplaces;
            LocationCmb.SelectedItem = wp.Locations;
            WorkplaceTxb.Text = wp.Place;
            AddBtn.Content = "Сохранить";
            SelectedId = wp.Id;
            SaveBtnChecker = true;
            WorkplaceList.ItemsSource = DataBaseActions.GetWorkplacesList();
        }

        private void ContextMenuDelBtn_Click(object sender, RoutedEventArgs e)
        {
            Workplaces wp = WorkplaceList.SelectedItem as Workplaces;
            var Result = MessageBox.Show("Вы действительно хотите удалить выбранное рабочее место?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                OdbConnectHelper.entObj.Workplaces.Remove(wp);
                OdbConnectHelper.entObj.SaveChanges();
                WorkplaceList.ItemsSource = DataBaseActions.GetWorkplacesList();
                MessageBox.Show("Рабочее место удалено");
            }
            else if (Result == MessageBoxResult.No)
            {
                return;
            }
        }
    }
}
