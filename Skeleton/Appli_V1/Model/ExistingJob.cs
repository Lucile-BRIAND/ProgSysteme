using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Appli_V1.Controllers
{
    class ExistingJob : FileAbstractClass
    {

        private StreamWriter file;
        private static ExistingJob jobInstance = null;
        private string sourcePath;
        private string targetPath;
        private string jobName;
        private string jobType;

        private ExistingJob()
        {

        }

        public static ExistingJob GetInstance
        {
            get
            {
                if (jobInstance == null)
                {
                    jobInstance = new ExistingJob();
                }
                return jobInstance;
            }
        }

        public override void InitFile()
        {


        }
        public override void CloseFile()
        {

        }

        public void WriteExistingJobs()
        {

        }
        public void RemoveExistingJobs()
        {

        }
    }
}
