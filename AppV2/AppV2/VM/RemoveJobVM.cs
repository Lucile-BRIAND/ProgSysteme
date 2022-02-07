using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppV2.Models;

namespace AppV2.VM
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
            ExistingJob existingJob = new ExistingJob();
            existingJob.RemoveExistingJobs(jobname);
        }

    }
}
