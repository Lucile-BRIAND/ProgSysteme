using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class ExecuteStrategyView : IStrategyView
    {
        private string choice_selected;
        public void DisplayExistingData(string initial_message)
        {
            Console.WriteLine(initial_message);
        }
        public string CollectOptions()
        {
            this.choice_selected = Console.ReadLine();
            return choice_selected;
        }

    }
}
