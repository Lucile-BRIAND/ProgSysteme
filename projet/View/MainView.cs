using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class MainView
    {
        private string choiceSelected;
        public void DisplayMenu(string message) //Displays homepage's messages (menu and error)
        {
            Console.WriteLine(message);
        }
        public string CollectChoice() //Collects the choice that the user want to call 
        {
            this.choiceSelected = Console.ReadLine();
            return choiceSelected;
        }
    }
}
