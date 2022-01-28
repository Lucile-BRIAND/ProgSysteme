using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class CreateJobStrategy : IStrategyController
    {
        private int language;
        //Initialization of language view
        CreateJobStrategyView createJobStrategyView = new CreateJobStrategyView();

        public void CheckRequirements()
        {
            ExistingJob existingJob = new ExistingJob();
            string contentFile = existingJob.ReadFile();
            jobModel newJob = new jobModel();
            newJob.jobName = "Name";
            newJob.jobType = "complete";
            newJob.sourcePath = "C:/Users/lu-ro/Desktop/Test/Source";
            newJob.targetPath = "C:/Users/lu-ro/Desktop/Test/Destination";

            bool verif = existingJob.WriteExistingJobs(newJob);

        }
        public void CollectExistingData()
        {
            
        }
    }
}