using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppV3.VM;

namespace AppV3.Models
{
    class JobProgressPercentage
    {
        //Instance null by default 
        private static JobProgressPercentage jobProgressPercentageInstance = null;
        public double percentage;

        //private constructor 
        private JobProgressPercentage() { }

        // Initializes the unique instance
        public static JobProgressPercentage GetInstance
        {
            get
            {
                if (jobProgressPercentageInstance == null)
                {
                    jobProgressPercentageInstance = new JobProgressPercentage();
                }
                return jobProgressPercentageInstance;
            }

        }
    }
}
