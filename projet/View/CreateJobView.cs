using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class CreateJobView
    {
        public List<string> CollectRequirements(List<string> createList)  //Collects all backup's data 
        {
            int counter =0;
            List<string> createValues = new List<string>(4);
            while (counter<=3)
            {
                Console.WriteLine(createList[counter]);
                createValues.Add(Console.ReadLine());
                counter++;
            }
            return createValues;
        }
        public void DisplayMessage(string message) //Displays create's menu messages (validation, error)
        {
            Console.WriteLine(message);
        }

    }
}
