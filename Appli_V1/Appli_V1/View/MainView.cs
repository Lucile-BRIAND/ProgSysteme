using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class MainView
    {
        private string choice_selected;
        public void DisplayOptions(string initial_message)
        {
            Console.WriteLine(initial_message);
        }
        public string CollectOptions()
        {
            this.choice_selected = Console.ReadLine();
            return choice_selected;
        }
        public void DisplayErrorMessage(string Error_Message)
        {
            Console.WriteLine(Error_Message);
        }
    }
}
