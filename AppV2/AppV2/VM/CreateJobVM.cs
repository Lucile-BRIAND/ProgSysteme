using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppV2.Models;

namespace AppV2.VM
{
    class CreateJobVM : MainVM
    {
        public string jobName;
        private string jobType;
        private string sourcePath;
        private string targetPath;


        public string JobName
        {
            get
            {
                return jobName;
            }
            set
            {
                if (!string.Equals(jobName, value))
                {
                    jobName = value;
                    OnPropertyChanged("JobName");
                }
            }
        }
        public string JobType
        {
            get
            {
                return jobType;
            }
            set
            {
                if (!string.Equals(jobType, value))
                {
                    jobType = value;
                    OnPropertyChanged("JobType");
                }
            }
        }
        public string SourcePath
        {
            get
            {
                return sourcePath;
            }
            set
            {
                if (!string.Equals(SourcePath, value))
                {
                    sourcePath = value;
                    OnPropertyChanged("SourcePath");
                }
            }
        }
        public string TargetPath
        {
            get
            {
                return targetPath;
            }
            set
            {
                if (!string.Equals(TargetPath, value))
                {
                    targetPath = value;
                    OnPropertyChanged("TargetPath");
                }
            }
        }
        public void SaveJob(string jobname, string jobtype, string sourcepath, string targetpath)
        {
            JobModel userData = new JobModel();
            userData.jobName = jobname;
            userData.jobType = jobtype;
            userData.sourcePath = sourcepath;
            userData.targetPath = targetpath;
            ExistingJob existingJob = new ExistingJob();
            existingJob.WriteExistingJobs(userData);
        }
        public string DisplayJob()
        {
             return System.IO.File.ReadAllText("Jobfile.json");
        }
    }

}
