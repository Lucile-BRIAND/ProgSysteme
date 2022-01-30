using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class LanguageView 
    {
        // Private Attribute who contain the value writed by the user
        private string languageSelected;
        // Function used to display the initial messages
        public void DisplayLanguageMessages()
        {
            Console.WriteLine("Choisissez la langue désirée / Choose the language (1 ou/or 2)");
            Console.WriteLine("1 . Anglais/English");
            Console.WriteLine("2 . Français/French");

        }
        // Function used to collect the data writed by the user
        public string CollectLanguage()
        {
            this.languageSelected = Console.ReadLine();
            return languageSelected;
        }
        public void DisplayErrorMessage(string errorMessage) //Displays error message
        {
            Console.WriteLine(errorMessage);
        }

    }
}
