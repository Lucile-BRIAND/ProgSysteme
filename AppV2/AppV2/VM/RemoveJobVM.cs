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
        public string name { get; set; }

        public static void RemoveJob(string jobname)
        {
            ExistingJob existingJob = new ExistingJob();
            existingJob.RemoveExistingJobs(jobname);
        }
    }
}
