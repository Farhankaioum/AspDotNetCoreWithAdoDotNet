using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSPStore.Models;

namespace KSPStore.ViewModel.Employee
{
    public class ReadTwoTableDataConcurrently
    {
        public List<KSPStore.Models.Employee> Employees { get; set; }
        public List<KSPStore.DataProvider.NewClass> NewClasses { get; set; }
    }
}
