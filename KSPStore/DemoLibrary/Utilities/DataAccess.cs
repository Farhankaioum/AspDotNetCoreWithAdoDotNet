using System;
using System.Collections.Generic;
using System.Text;

namespace DemoLibrary.Utilities
{
    public class DataAccess
    {
        public void LoadData()
        {
            Console.WriteLine("Loading Data");
        }

        public void SaveData(string name)
        {
            Console.WriteLine($"Saving {name}");
        }
    }
}
