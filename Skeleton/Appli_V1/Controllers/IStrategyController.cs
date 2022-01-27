using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    interface IStrategyController
    {
        public void CheckRequirements();
        public void CollectExistingData();
    }
}
