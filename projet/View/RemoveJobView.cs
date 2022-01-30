using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class RemoveJobView
    {
        private string choiceSelected;
        public void DisplayMessage(string message) //Displays remove's menu messages (Question, validation, error)
        {
            Console.WriteLine(message);
        }
        public string CollectJobName() //Collects backup's name 
        {
            this.choiceSelected = Console.ReadLine();
            return choiceSelected;
        }
    }
}
