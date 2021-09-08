using Model1;
using Model1.DataFiles;
using System.Windows;
using System.Linq;
using System.Windows.Input;

namespace bd
{
    /// <summary>
    /// Логика взаимодействия для AddLocationWindow.xaml
    /// </summary>
    public partial class AddLocationWindow : Window
    {
        public AddLocationWindow()
        {
            InitializeComponent();
            Keyboard.Focus(LocationTxb);
            LocList.ItemsSource = DataBaseActions.GetLocationsList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (SaveBtnChecker)
                {
                    Locations locations = OdbConnectHelper.entObj.Locations.FirstOrDefault(x => x.Id == SelectedId);
                    locations.Location = LocationTxb.Text;
                    OdbConnectHelper.entObj.SaveChanges();
                    LocationTxb.Text = "";
                    AddLocBtn.Content = "Добавить";
                    SaveBtnChecker = false;
                    SelectedId = 0;
                    LocList.ItemsSource = DataBaseActions.GetLocationsList();
                    MessageBox.Show("Изменения сохранены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (LocationTxb.Text != null && SaveBtnChecker == false)
                {
                    if (LocationTxb.Text == "")
                    {
                        MessageBox.Show("Введите место", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    var CheckLoc = OdbConnectHelper.entObj.Locations.FirstOrDefault(x => x.Location == LocationTxb.Text.ToLower());
                    if (CheckLoc != null)
                    {
                        MessageBox.Show("Такое место уже имеется в списке", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    Locations location = new Locations()
                    {
                        Location = LocationTxb.Text.ToLower()
                    };
                    DataBaseActions.AddLocation(location);
                    OdbConnectHelper.entObj.SaveChanges();
                    //MessageBox.Show("Новое место успешно добавлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    LocList.ItemsSource = DataBaseActions.GetLocationsList();
                    LocationTxb.Text = "";

                    Keyboard.Focus(LocationTxb);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Критическая ошибка приложения" + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        bool SaveBtnChecker = false;
        int SelectedId;
        private void ContextMenuEditBtn_Click(object sender, RoutedEventArgs e)
        {
            Locations locs = LocList.SelectedItem as Locations;
            LocationTxb.Text = locs.Location;
            AddLocBtn.Content = "Сохранить";
            SelectedId = locs.Id;
            SaveBtnChecker = true;
            LocList.ItemsSource = DataBaseActions.GetLocationsList();
        }

        private void ContextMenuDelBtn_Click(object sender, RoutedEventArgs e)
        {
            Locations locs = LocList.SelectedItem as Locations;
            var Result = MessageBox.Show("Вы действительно хотите удалить выбранный отдел?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                OdbConnectHelper.entObj.Locations.Remove(locs);
                OdbConnectHelper.entObj.SaveChanges();
                LocList.ItemsSource = DataBaseActions.GetLocationsList();
                MessageBox.Show("Отдел удален");
            }
            else if (Result == MessageBoxResult.No)
            {
                return;
            }
        }
    }
}
