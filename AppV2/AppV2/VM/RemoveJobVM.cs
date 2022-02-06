﻿using System;
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

        public string removeJobGrid { get; set; }

        MainVM mainVM = new MainVM();

        LanguageFile gt = LanguageFile.GetInstance;

        public RemoveJobVM getValues()
        {
            var values = new RemoveJobVM()
            {
                removeBackup = gt.ReadFile().ValidationRemoveJob,
                mainMenu = gt.ReadFile().MainReturn
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
