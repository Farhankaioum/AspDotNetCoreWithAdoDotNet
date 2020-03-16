using DemoLibrary;
using System;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            businessLogic.ProcessDate();

            #region //For Single Responsibility Principle
            //StandardMessages.WelcomeMessage();

            //Person user = PersonDataCapture.Capture();

            //bool isUserValid = PersonValidator.Validate(user);

            //if (isUserValid == false)
            //{
            //    StandardMessages.EndApplication();
            //    return; 
            //}

            //AccountGenerator.CreateAccount(user);

            //StandardMessages.EndApplication();

            #endregion

        }
    }
}
