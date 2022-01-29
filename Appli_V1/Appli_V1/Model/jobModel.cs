using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class jobModel    //Class with all backup's attributes 
    {
        public string jobName { get; set; }
        public string jobType { get; set; }
        public string sourcePath { get; set; }
        public string targetPath { get; set; }
    }
}