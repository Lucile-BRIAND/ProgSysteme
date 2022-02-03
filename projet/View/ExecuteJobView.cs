using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class ExecuteJobView
    {
        private string choiceSelected;
        public void DisplayMessage(string message) //Displays execution's menu messages (Question, validation, error) 
        {
            Console.WriteLine(message);
        }
        public string CollectName() //Collects backup's name 
        {
            this.choiceSelected = Console.ReadLine();
            return choiceSelected;
        }

    }
}
