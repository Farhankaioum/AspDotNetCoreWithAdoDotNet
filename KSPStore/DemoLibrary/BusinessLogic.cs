using DemoLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoLibrary
{
    public class BusinessLogic
    {
        public void ProcessDate()
        {
            Logger logger = new Logger();
            DataAccess dataAccess = new DataAccess();

            logger.Log("Starting the processing data.");
            Console.WriteLine("Processing the data");
            dataAccess.LoadData();
            dataAccess.SaveData("ProcessInfo");
            logger.Log("Finished processing of the data.");

        }
       
    }
}
