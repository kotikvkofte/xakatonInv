using Model1.DataFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1
{
    public partial class Inventorys
    {
        private static List<Inventory> CurrentInventory = new List<Inventory>();

        public static void SetCurrentList(List<Inventory> ik)
        {
            CurrentInventory = ik;
        }

        /// <summary>
        /// возвращает количество текущих элементов листа
        /// (для счетчика)
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentInventoyNumb()
        {
            
            if (CurrentInventory == null)
            {
                return 0;
            }
            else
            {
                return CurrentInventory.Count();
            }
        }
        /// <summary>
        /// Добавляет объект в лист CurrentInventory
        /// </summary>
        /// <param name="inventory">Объект инвентаря</param>
        public static void AddToCurrentInventoy(Inventory inventory)
        {
            if (CurrentInventory == null)
            {
                CurrentInventory.Add(inventory);
            }
            if (CurrentInventory.FirstOrDefault(x => x == inventory) == null)
            {
                CurrentInventory.Add(inventory);
            }
        }
        /// <summary>
        /// Возвращает неотсканированные элементы CurrentInventory
        /// </summary>
        /// <returns></returns>
        public static List<Inventory> GetResultInventoryList()
        {
            return DataBaseActions.GetAllInventoryList().Except(CurrentInventory).ToList();
        }
        /// <summary>
        /// возвращает CurrentInventory
        /// </summary>
        /// <returns></returns>
        public static List<Inventory> GetCurrentInventoryList()
        {
            return CurrentInventory.ToList();
        }
    }
}
