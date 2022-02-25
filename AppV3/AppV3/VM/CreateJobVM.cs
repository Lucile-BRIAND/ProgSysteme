using System;
using AppV3.Models;

namespace AppV3.VM
{
    public class CreateJobVM
    {
        public string name { get; set; }
        public string mainMenu { get; set; }
        public string executeBackup { get; set; }
        public string createJob { get; set; }
        public string target { get; set; }
        public string source { get; set; }
        public string type { get; set; }
        public string type1 { get; set; }
        public string type2 { get; set; }

        public LanguageFile singletonLang = LanguageFile.GetInstance;

        public CreateJobVM getValues()
        {
            //Gets the selected language for the CreateJobView elements
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
            //Saves the entered data into a JobModel
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
            //Displays all jobs
             return System.IO.File.ReadAllText("Jobfile.json");
        }
    }

}
