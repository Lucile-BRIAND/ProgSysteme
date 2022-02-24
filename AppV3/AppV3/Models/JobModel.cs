using System;

namespace AppV3.Models
{
    public class JobModel    //Class with all backup's attributes 
    {
        public string jobName { get; set; }
        public string jobType { get; set; }
        public string sourcePath { get; set; }
        public string targetPath { get; set; }
    }
}
