using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class MainView
    {
        private string choice_selected;
        public void DisplayOptions(string initial_message) //Displays homepage's messages (menu and error)
        {
            Console.WriteLine(initial_message);
        }
        public string CollectOptions() //Collects the controller that the user want to call 
        {
            this.choice_selected = Console.ReadLine();
            return choice_selected;
        }
    }
}
