using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class CreateJobStrategyView : IStrategyView
    {
        private string choice_selected;

        public void DisplayExistingData(List<string> Liste)
        {

        }

        public string CollectOptions()
        {
            this.choice_selected = Console.ReadLine();
            return choice_selected;
        }
    }
}