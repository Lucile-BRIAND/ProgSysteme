using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class ExecuteStrategyView : IStrategyView
    {
        public void DisplayExistingData()
        {

        }
        public List<int> CollectDataRequirements()
        {
            List<int> lst = new List<int>(5);
            lst.Add(1);
            lst.Add(2);
            return lst;
        }
        public void Error()
        {

        }
        public void Validation()
        {

        }
    }
}
