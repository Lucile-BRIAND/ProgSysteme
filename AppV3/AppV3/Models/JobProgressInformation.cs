using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV3.Models
{
    public class JobProgressInformation
    {
        public JobProgressInformation() { }
        public string Name { get; set; }
        public string Type { get; set; }

        public string SourcePath { get; set; }

        public string TargetPath { get; set; }

        public string State { get; set; }

        public int TotalFilesToCopy { get; set; }

        public long TotalFilesSize { get; set; }

        public int NbFilesLeftToDo { get; set; }

        public long FileSizeLeftToCopy { get; set; }
    }
}
