using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class RemoveJobStrategyView : IStrategyView
    {
        private string choice_selected;

        public void DisplayExistingData(string initial_message, string back_message)
        {
            Console.WriteLine(initial_message);
            Console.WriteLine(back_message);
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