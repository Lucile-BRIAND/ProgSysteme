using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV2.Models
{
        public class JobModel    //Class with all backup's attributes 
        {
            public string jobName { get; set; }
            public string jobType { get; set; }
            public string sourcePath { get; set; }
            public string targetPath { get; set; }
        }
    
}
