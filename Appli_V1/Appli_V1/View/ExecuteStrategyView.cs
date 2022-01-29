using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class ExecuteStrategyView
    {
        private string choice_selected;
        public void DisplayExistingData(string message) //Displays execution's menu messages (Question, validation, error) 
        {
            Console.WriteLine(message);
        }
        public string CollectOptions() //Collects backup's name 
        {
            this.choice_selected = Console.ReadLine();
            return choice_selected;
        }

    }
}
