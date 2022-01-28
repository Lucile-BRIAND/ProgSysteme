using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class CreateJobStrategyView
    {
        private string choice_selected;
        public List<string>  DisplayExistingData(List<string> Create_List)
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
        public void DisplayError(string error_message)
        {
            Console.WriteLine(error_message);
        }
        public void Confirmation_Message(string validation_message)
        {
            Console.WriteLine(validation_message);
        }

    }
}
