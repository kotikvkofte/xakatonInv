using Model1.DataFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model1
{
    public partial class DataBaseActions
    {
        public static Inventory FindBarCode(string code) => OdbConnectHelper.entObj.Inventory.FirstOrDefault(x => x.inventory_code == code);

        public static List<Locations> GetLocationsList() => OdbConnectHelper.entObj.Locations.ToList();
        public static List<Workplaces> GetWorkplacesList() => OdbConnectHelper.entObj.Workplaces.ToList();
        public static List<Inventory> GetAllInventoryList() => OdbConnectHelper.entObj.Inventory.ToList();
        public static List<Responsible_Persons> GetAllRespPersonsList() => OdbConnectHelper.entObj.Responsible_Persons.ToList();

        public static void AddLocation(Locations location)
        {
            OdbConnectHelper.entObj.Locations.Add(location);
        }
        public static void AddInventory(Inventory inventory)
        {
            OdbConnectHelper.entObj.Inventory.Add(inventory);
        }
        public static void AddRespPerson(Responsible_Persons person)
        {
            OdbConnectHelper.entObj.Responsible_Persons.Add(person);
        }
        public static List<Inventory> GetFiltredInventoryList(int SelectLocation, int SelectWorkPlace, string SearchName) => OdbConnectHelper.entObj.Inventory.Where(x => x.Workplaces.IdLocation == SelectLocation && x.Workplaces.Id == SelectWorkPlace && x.Name.StartsWith(SearchName)).ToList();
        public static List<Inventory> GetFiltredInventoryList(int SelectLocation, int SelectWorkPlace) => OdbConnectHelper.entObj.Inventory.Where(x => x.Workplaces.IdLocation == SelectLocation && x.Workplaces.Id == SelectWorkPlace).ToList();
        public static List<Inventory> GetFiltredInventoryList(string SearchName) => OdbConnectHelper.entObj.Inventory.Where(x => x.Name.StartsWith(SearchName)).ToList();

    }
}
