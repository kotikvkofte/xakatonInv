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
    /// Логика взаимодействия для AddRespPersonWindow.xaml
    /// </summary>
    public partial class AddRespPersonWindow : Window
    {
        public AddRespPersonWindow()
        {
            InitializeComponent();
            PersonsList.ItemsSource = DataBaseActions.GetAllRespPersonsList();
        }

        private void AddRespPersonBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SaveBtnChecker)
                {
                    Responsible_Persons locations = OdbConnectHelper.entObj.Responsible_Persons.FirstOrDefault(x => x.Id == SelectedId);
                    locations.Name = RespPersonTxb.Text;
                    OdbConnectHelper.entObj.SaveChanges();
                    RespPersonTxb.Text = "";
                    AddRespPersonBtn.Content = "Добавить";
                    SaveBtnChecker = false;
                    SelectedId = 0;
                    MessageBox.Show("Изменения сохранены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    PersonsList.ItemsSource = DataBaseActions.GetAllRespPersonsList();
                    return;
                }
                if (RespPersonTxb.Text != null && SaveBtnChecker == false)
                {
                    if (RespPersonTxb.Text == "")
                    {
                        MessageBox.Show("Введите ответственное лицо", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    var CheckLoc = OdbConnectHelper.entObj.Responsible_Persons.FirstOrDefault(x => x.Name == RespPersonTxb.Text);
                    if (CheckLoc != null)
                    {
                        MessageBox.Show("Такое ответственное лицо уже имеется в списке", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    Responsible_Persons person = new Responsible_Persons()
                    {
                        Name = RespPersonTxb.Text
                    };
                    DataBaseActions.AddRespPerson(person);
                    OdbConnectHelper.entObj.SaveChanges();
                    //MessageBox.Show("Новое место успешно добавлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    PersonsList.ItemsSource = DataBaseActions.GetAllRespPersonsList();
                    RespPersonTxb.Text = "";

                    Keyboard.Focus(RespPersonTxb);
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
            Responsible_Persons pers = PersonsList.SelectedItem as Responsible_Persons;
            RespPersonTxb.Text = pers.Name;
            AddRespPersonBtn.Content = "Сохранить";
            SelectedId = pers.Id;
            SaveBtnChecker = true;
            PersonsList.ItemsSource = DataBaseActions.GetAllRespPersonsList();
        }

        private void ContextMenuDelBtn_Click(object sender, RoutedEventArgs e)
        {
            Responsible_Persons pers = PersonsList.SelectedItem as Responsible_Persons;
            var Result = MessageBox.Show("Вы действительно хотите удалить выбранное ответственное лицо?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                OdbConnectHelper.entObj.Responsible_Persons.Remove(pers);
                OdbConnectHelper.entObj.SaveChanges();
                PersonsList.ItemsSource = DataBaseActions.GetAllRespPersonsList();
                MessageBox.Show("Ответственное лицо удалено");
            }
            else if (Result == MessageBoxResult.No)
            {
                return;
            }
        }
    }
}
