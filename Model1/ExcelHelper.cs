using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application = Microsoft.Office.Interop.Excel.Application;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using Model1.DataFiles;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows;

namespace Model1
{
    public partial class ExcelHelper
    {
        private static Application application;
        private static Excel.Workbook workBook;
        private static Excel.Worksheet worksheet;

        public static void ListToExcel(List<Inventory> list)
        {
            // Открываем приложение
            application = new Application
            {
                DisplayAlerts = false
            };

            // Файл шаблона
            const string template = "template.xlsx";

            // Открываем книгу
            workBook = application.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, template));

            // Получаем активную таблицу
            worksheet = workBook.ActiveSheet as Excel.Worksheet;

            //Заполняем таблицу значениями свойств из List'а
            int i = 2;
            foreach (Inventory item in list)
            {
                worksheet.Range[$"A{i}"].Value = item.Name;
                worksheet.Range[$"B{i}"].Value = item.inventory_code;
                worksheet.Range[$"C{i}"].Value = item.Price;
                worksheet.Range[$"D{i}"].Value = item.Amount;

                if (item.Workplaces.Locations != null)
                    worksheet.Range[$"E{i}"].Value = item.Workplaces.Locations.Location;
                else
                    worksheet.Range[$"E{i}"].Value = "-";

                if (item.Workplaces != null)
                    worksheet.Range[$"F{i}"].Value = item.Workplaces.Place;
                else
                    worksheet.Range[$"F{i}"].Value = "-";
                if (item.Responsible_Persons != null)
                    worksheet.Range[$"G{i}"].Value = item.Responsible_Persons.Name;
                else
                    worksheet.Range[$"G{i}"].Value = "-";

                i++;
            }
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "xls files (*.xls)|*.txt|All files (*.*)|*.*";
            saveFile.FilterIndex = 2;
            saveFile.DefaultExt = ".xlsx";
            if (saveFile.ShowDialog() == true)
            {
                workBook.SaveAs(saveFile.FileName);
                CloseExcel();
            }
        }

        private static void CloseExcel()
        {
            if (application != null)
            {
                int excelProcessId = -1;
                GetWindowThreadProcessId(application.Hwnd, ref excelProcessId);
                Marshal.ReleaseComObject(worksheet);
                workBook.Close();
                Marshal.ReleaseComObject(workBook);
                application.Quit();
                Marshal.ReleaseComObject(application);

                application = null;
                // Прибиваем висящий процесс
                try
                {
                    Process process = Process.GetProcessById(excelProcessId);
                    process.Kill();
                }
                finally { }
            }
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, ref int lpdwProcessId);

        /// <summary>
        /// Метод перевода эл-в массива к общей форме и удаление дубликатов
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private static string[] StringArrayToDistinctStringArray(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                string s = arr[i];
                if (arr[i] == null)
                {
                    s = "-";
                }
                if (arr[i] != "-")
                {
                    s = arr[i].FirstCharToUpper();
                }
                arr[i] = s;
            }
            return arr.Distinct().ToArray();
        }

        /// <summary>
        /// Возвращает строковый массив с большой буквы, остальной текст с маленькой
        /// </summary>
        private static string[] StringArrayFirstCharToUpper(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].FirstCharToUpper();
            }
            return arr;
        }

        public static /*List<Inventory>*/ void FromExcelToList()
        {
            // Открываем приложение
            application = new Application
            {
                DisplayAlerts = false
            };

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "xls files (*.xls)|*.xls|All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                // Открываем выбранную книгу
                workBook = application.Workbooks.Open(dlg.FileName);
                // Получить первый рабочий лист.
                worksheet = (Excel.Worksheet)workBook.Sheets[1];
            }
            else
            {
                //return new List<Inventory>();
            }

            List<Inventory> invList = new List<Inventory>();

            try
            {
                //Берем значения каждого столбца
                //Excel.Range usedColumn = worksheet.UsedRange.Columns[1];
                System.Array NamesSysArr = (System.Array)worksheet.UsedRange.Columns[1].Cells.Value2;
                System.Array NumSysArr = (System.Array)worksheet.UsedRange.Columns[2].Cells.Value2;
                System.Array PriceSysArr = (System.Array)worksheet.UsedRange.Columns[3].Cells.Value2;
                System.Array AmountSysArr = (System.Array)worksheet.UsedRange.Columns[4].Cells.Value2;
                System.Array LocationSysArr = (System.Array)worksheet.UsedRange.Columns[5].Cells.Value2;
                System.Array WorkplaceSysArr = (System.Array)worksheet.UsedRange.Columns[6].Cells.Value2;
                System.Array RespPersonSysArr = (System.Array)worksheet.UsedRange.Columns[7].Cells.Value2;

                //Переделываем в стандартны типовой массив
                string[] NamesArray = NamesSysArr.OfType<object>().Select(o => o.ToString()).ToArray();
                string[] NumArray = NumSysArr.OfType<object>().Select(o => o.ToString()).ToArray();
                float[] PriceArray = PriceSysArr.OfType<object>().Select(o => (float)Convert.ToDouble(o)).ToArray();
                int[] AmountArray = AmountSysArr.OfType<object>().Select(o => Convert.ToInt32(o)).ToArray();
                string[] LocationArray = LocationSysArr.OfType<object>().Select(o => o.ToString()).ToArray();
                string[] WorkplaceArray = WorkplaceSysArr.OfType<object>().Select(o => o.ToString()).ToArray();
                string[] RespPersonArray = RespPersonSysArr.OfType<object>().Select(o => o.ToString()).ToArray();


                try
                {
                    //Удаление прошлых данных из табилц
                    if (OdbConnectHelper.entObj.Inventory.FirstOrDefault() != null)
                        OdbConnectHelper.entObj.Inventory.RemoveRange(OdbConnectHelper.entObj.Inventory);
                    if (OdbConnectHelper.entObj.Workplaces.FirstOrDefault() != null)
                        OdbConnectHelper.entObj.Workplaces.RemoveRange(OdbConnectHelper.entObj.Workplaces);
                    if (OdbConnectHelper.entObj.Locations.FirstOrDefault() != null)
                        OdbConnectHelper.entObj.Locations.RemoveRange(OdbConnectHelper.entObj.Locations);
                    if (OdbConnectHelper.entObj.Responsible_Persons.FirstOrDefault() != null)
                        OdbConnectHelper.entObj.Responsible_Persons.RemoveRange(OdbConnectHelper.entObj.Responsible_Persons);
                    //Заполнение БД
                    //Таблица Locations
                    string[] DistinctLocationArray = StringArrayToDistinctStringArray(LocationArray);
                    for (int i = 0; i < DistinctLocationArray.Length; i++)
                    {
                        Locations loc = new Locations
                        {
                            Location = DistinctLocationArray[i]
                        };
                        OdbConnectHelper.entObj.Locations.Add(loc);
                    }
                    OdbConnectHelper.entObj.SaveChanges();

                    //Таблица Workplaces
                    string[] DistinctWorkplaceArray = StringArrayToDistinctStringArray(WorkplaceArray);
                    for (int i = 0; i < DistinctWorkplaceArray.Length; i++)
                    {
                        Workplaces wrk = new Workplaces
                        {
                            Place = DistinctWorkplaceArray[i]
                        };
                        OdbConnectHelper.entObj.Workplaces.Add(wrk);
                    }
                    OdbConnectHelper.entObj.SaveChanges();



                    for (int i = 0; i < LocationArray.Length; i++)
                    {
                        string b = WorkplaceArray[i].FirstCharToUpper();
                        var FirstWrk = OdbConnectHelper.entObj.Workplaces.FirstOrDefault(x => x.Place == b);
                        if (FirstWrk.IdLocation == null)
                        {
                            string a = LocationArray[i].FirstCharToUpper();
                            var FirstLoc = OdbConnectHelper.entObj.Locations.FirstOrDefault(x => x.Location == a);

                            FirstWrk.IdLocation = FirstLoc.Id;
                        }
                    }
                    OdbConnectHelper.entObj.SaveChanges();

                    //Таблица Responsible_Persons
                    string[] DistinctRespPersonArray = StringArrayToDistinctStringArray(RespPersonArray);
                    for (int i = 0; i < DistinctRespPersonArray.Length; i++)
                    {
                        Responsible_Persons responsible = new Responsible_Persons
                        {
                            Name = DistinctRespPersonArray[i]
                        };
                        OdbConnectHelper.entObj.Responsible_Persons.Add(responsible);
                    }
                    OdbConnectHelper.entObj.SaveChanges();

                    //Заполнение БД
                    for (int i = 0; i < NamesArray.Length; i++)
                    {
                        string plc = WorkplaceArray[i].FirstCharToUpper();
                        var k = OdbConnectHelper.entObj.Workplaces.FirstOrDefault(x => x.Place == plc);
                        string prs = RespPersonArray[i].FirstCharToUpper();
                        var p = OdbConnectHelper.entObj.Responsible_Persons.FirstOrDefault(x => x.Name == prs);
                        Inventory inv = new Inventory
                        {
                            Name = NamesArray[i],
                            inventory_code = NumArray[i],
                            Price = PriceArray[i],
                            Amount = AmountArray[i],
                            IdWorkplace = k.Id,
                            IdPerson = p.Id
                        };
                        OdbConnectHelper.entObj.Inventory.Add(inv);
                    }
                    CloseExcel();
                    OdbConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Все успешно сработало");

                }
                catch (Exception ex)
                {
                    CloseExcel();
                    MessageBox.Show("Критическая ошибка приложения" + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            catch (Exception ex)
            {
                CloseExcel();
                MessageBox.Show("Критическая ошибка приложения" + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void ReadExcelFile()
        {
            // Открываем приложение
            application = new Application
            {
                DisplayAlerts = false
            };

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "xls files (*.xls)|*.xls|All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                // Открываем выбранную книгу
                workBook = application.Workbooks.Open(dlg.FileName);
                // Получить первый рабочий лист.
                worksheet = (Excel.Worksheet)workBook.Sheets[1];
            }
            else
            {
                return;
            }

            //Создаем лист для приема данных из Excel
            List<Inventory> invList = new List<Inventory>();

            try
            {
                //Берем значения каждого столбца
                //Excel.Range usedColumn = worksheet.UsedRange.Columns[1];
                System.Array NamesSysArr = (System.Array)worksheet.UsedRange.Columns[1].Cells.Value2;
                System.Array NumSysArr = (System.Array)worksheet.UsedRange.Columns[2].Cells.Value2;
                System.Array PriceSysArr = (System.Array)worksheet.UsedRange.Columns[3].Cells.Value2;
                System.Array AmountSysArr = (System.Array)worksheet.UsedRange.Columns[4].Cells.Value2;
                System.Array LocationSysArr = (System.Array)worksheet.UsedRange.Columns[5].Cells.Value2;
                System.Array WorkplaceSysArr = (System.Array)worksheet.UsedRange.Columns[6].Cells.Value2;
                System.Array RespPersonSysArr = (System.Array)worksheet.UsedRange.Columns[7].Cells.Value2;

                //Переделываем в стандартны типовой массив
                string[] NamesArray = NamesSysArr.OfType<object>().Select(o => o.ToString()).ToArray();
                string[] NumArray = NumSysArr.OfType<object>().Select(o => o.ToString()).ToArray();
                float[] PriceArray = PriceSysArr.OfType<object>().Select(o => (float)Convert.ToDouble(o)).ToArray();
                int[] AmountArray = AmountSysArr.OfType<object>().Select(o => Convert.ToInt32(o)).ToArray();
                string[] LocationArray = LocationSysArr.OfType<object>().Select(o => o.ToString()).ToArray();
                string[] WorkplaceArray = WorkplaceSysArr.OfType<object>().Select(o => o.ToString()).ToArray();
                string[] RespPersonArray = RespPersonSysArr.OfType<object>().Select(o => o.ToString()).ToArray();


                try
                {
                    //Удаление прошлых данных из табилц
                    if (OdbConnectHelper.entObj.Inventory.FirstOrDefault() != null)
                        OdbConnectHelper.entObj.Inventory.RemoveRange(OdbConnectHelper.entObj.Inventory);
                    if (OdbConnectHelper.entObj.Workplaces.FirstOrDefault() != null)
                        OdbConnectHelper.entObj.Workplaces.RemoveRange(OdbConnectHelper.entObj.Workplaces);
                    if (OdbConnectHelper.entObj.Locations.FirstOrDefault() != null)
                        OdbConnectHelper.entObj.Locations.RemoveRange(OdbConnectHelper.entObj.Locations);
                    if (OdbConnectHelper.entObj.Responsible_Persons.FirstOrDefault() != null)
                        OdbConnectHelper.entObj.Responsible_Persons.RemoveRange(OdbConnectHelper.entObj.Responsible_Persons);
                    //Заполнение БД
                    //Таблица Locations
                    string[] DistinctLocationArray = StringArrayToDistinctStringArray(LocationArray);
                    for (int i = 0; i < DistinctLocationArray.Length; i++)
                    {
                        Locations loc = new Locations
                        {
                            Location = DistinctLocationArray[i]
                        };
                        OdbConnectHelper.entObj.Locations.Add(loc);
                    }
                    OdbConnectHelper.entObj.SaveChanges();

                    //Таблица Workplaces
                    string[] DistinctWorkplaceArray = StringArrayToDistinctStringArray(WorkplaceArray); 
                    for (int i = 0; i < DistinctWorkplaceArray.Length; i++)
                    {
                        Workplaces wrk = new Workplaces
                        {
                            Place = DistinctWorkplaceArray[i]
                        };
                        OdbConnectHelper.entObj.Workplaces.Add(wrk);
                    }
                    OdbConnectHelper.entObj.SaveChanges();

                    

                    for (int i = 0; i < LocationArray.Length; i++)
                    {
                        string b = WorkplaceArray[i].FirstCharToUpper();
                        var FirstWrk = OdbConnectHelper.entObj.Workplaces.FirstOrDefault(x => x.Place == b);
                        if (FirstWrk.IdLocation == null)
                        {
                            string a = LocationArray[i].FirstCharToUpper();
                            var FirstLoc = OdbConnectHelper.entObj.Locations.FirstOrDefault(x => x.Location == a);

                            FirstWrk.IdLocation = FirstLoc.Id;
                        }
                    }
                    OdbConnectHelper.entObj.SaveChanges();

                    //Таблица Responsible_Persons
                    string[] DistinctRespPersonArray = StringArrayToDistinctStringArray(RespPersonArray);
                    for (int i = 0; i < DistinctRespPersonArray.Length; i++)
                    {
                        Responsible_Persons responsible = new Responsible_Persons
                        {
                            Name = DistinctRespPersonArray[i]
                        };
                        OdbConnectHelper.entObj.Responsible_Persons.Add(responsible);
                    }
                    OdbConnectHelper.entObj.SaveChanges();

                    //Заполнение БД
                    for (int i = 0; i < NamesArray.Length; i++)
                    {
                        string plc = WorkplaceArray[i].FirstCharToUpper();
                        var k = OdbConnectHelper.entObj.Workplaces.FirstOrDefault(x => x.Place == plc);
                        string prs = RespPersonArray[i].FirstCharToUpper();
                        var p = OdbConnectHelper.entObj.Responsible_Persons.FirstOrDefault(x => x.Name == prs);
                        Inventory inv = new Inventory
                        {
                            Name = NamesArray[i],
                            inventory_code = NumArray[i],
                            Price = PriceArray[i],
                            Amount = AmountArray[i],
                            IdWorkplace = k.Id,
                            IdPerson = p.Id
                        };
                        OdbConnectHelper.entObj.Inventory.Add(inv);
                    }
                    CloseExcel();
                    OdbConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Все успешно сработало");
                }
                catch (Exception ex)
                {
                    CloseExcel();
                    MessageBox.Show("Критическая ошибка приложения" + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        
            catch (Exception ex)
            {
                CloseExcel();
                MessageBox.Show("Критическая ошибка приложения" + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
