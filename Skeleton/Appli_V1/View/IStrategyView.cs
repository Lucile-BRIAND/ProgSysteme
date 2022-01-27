using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    interface IStrategyView
    {
        public void DisplayExistingData();
        public List<int> CollectDataRequirements();
        public void Error();
        public void Validation();
        
    }
}
