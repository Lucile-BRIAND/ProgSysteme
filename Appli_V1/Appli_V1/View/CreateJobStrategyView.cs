using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class CreateJobStrategyView
    {
        public List<string> DisplayExistingData(List<string> Create_List)  //Collect all backup's data 
        {
            int Counter_A =0;
            List<string> CreateValues = new List<string>(4);
            while (Counter_A<=3)
            {
                Console.WriteLine(Create_List[Counter_A]);
                CreateValues.Add(Console.ReadLine());
                Counter_A++;
            }
            return CreateValues;
        }
        public void DisplayExistingData(string message) //Displays CreateBackup's menu messages (validation, error)
        {
            Console.WriteLine(message);
        }

    }
}
