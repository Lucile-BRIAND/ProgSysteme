﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AppV2
{
    class JobModel    //Class with all backup's attributes 
    {
        public string jobName { get; set; }
        public string jobType { get; set; }
        public string sourcePath { get; set; }
        public string targetPath { get; set; }
    }
}