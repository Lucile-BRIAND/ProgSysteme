using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppV2.Models;

namespace AppV2.VM
{
    class CreateJobVM
    {
        public string jobName;
        private string jobType;
        private string sourcePath;
        private string targetPath;
        public string name { get; set; }

        public string mainMenu { get; set; }
        public string executeBackup { get; set; }

        public string createJob { get; set; }
        public string target { get; set; }

        public string source { get; set; }
        public string type { get; set; }
        public string type1 { get; set; }
        public string type2 { get; set; }

        LanguageFile singletonLang = LanguageFile.GetInstance;
        public CreateJobVM getValues()
        {
            var values = new CreateJobVM()
            {
                executeBackup = singletonLang.ReadFile().Execute,
                mainMenu = singletonLang.ReadFile().MainReturn,
                name = singletonLang.ReadFile().ValidationCreateJob,
                target = singletonLang.ReadFile().CreateTarget,
                source = singletonLang.ReadFile().CreateSource,
                createJob = singletonLang.ReadFile().ValidationCreateJob,
                type = singletonLang.ReadFile().CreateType,
                type1 = singletonLang.ReadFile().Type0,
                type2 = singletonLang.ReadFile().Type1
            };

            return values;

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
