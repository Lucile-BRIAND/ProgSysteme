using AppV3.Models;

namespace AppV3.VM
{
    class RemoveJobVM
    {
        public string removeBackup { get; set; }
        public string mainMenu { get; set; }

        MainVM mainVM = new MainVM();
        LanguageFile singletonLang = LanguageFile.GetInstance;

        public RemoveJobVM getValues()
        {
            var values = new RemoveJobVM()
            {
                removeBackup = singletonLang.ReadFile().ValidationRemoveJob,
                mainMenu = singletonLang.ReadFile().MainReturn
            };

            return values;

        }
        public static void RemoveJob(string jobname)
        {
            //Deletes a job from the list
            ExistingJob existingJob = new ExistingJob();
            existingJob.RemoveExistingJobs(jobname);
        }

    }
}
