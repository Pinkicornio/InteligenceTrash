using System;
using System.Collections.Generic;
using System.Text;

namespace PapeleraInteligente
{
    class StorageItem
    {
        int id;
        int stock;
        string name;
        string brand;

        public StorageItem(int id, int stock, string name, string brand)
        {
            this.id = id;
            this.stock = stock;
            this.name = name;
            this.brand = brand;
        }

        public StorageItem()
        {

        }

        public int Id { get => id; set => id = value; }
        public int Stock { get => stock; set => stock = value; }
        public string Name { get => name; set => name = value; }
        public string Brand { get => brand; set => brand = value; }
    }
}
