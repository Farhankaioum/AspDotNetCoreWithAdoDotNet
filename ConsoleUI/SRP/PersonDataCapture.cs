using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    public class PersonDataCapture
    {
        public static Person Capture()
        {
            Person output = new Person();

            Console.Write("What is your first name: ");
            output.FirstName = Console.ReadLine();

            Console.Write("What is your last name: ");
            output.LastName = Console.ReadLine();

            return output;
        }
    }
}
