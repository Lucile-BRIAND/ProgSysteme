using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class LanguageStrategyView : IStrategyView
    {
        //Private attribute that contains the value written by the user
        private string language_Selected;

        //Function used to display the initial messages
        public void DisplayExistingData()
        {
            Console.WriteLine("Choisissez la langue désirée / Choose the language (1 ou/or 2)");
            Console.WriteLine("1 . Anglais/English");
            Console.WriteLine("2 . Français/French");

        }
        //Function used to collect the data written by the user
        public string CollectOptions()
        {
            this.language_Selected = Console.ReadLine();
            return language_Selected;
        }

        public void DisplayErrorMessage(string Error_Message)
        {
            Console.WriteLine(Error_Message);
        }

    }
}