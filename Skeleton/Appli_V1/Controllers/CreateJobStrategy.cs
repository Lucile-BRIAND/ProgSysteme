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
            //Instanciates new object to work between ExistingJob Model and CreateJobStrategy Controller
            ExistingJob existingJob = new ExistingJob();

            //Instanciates the new object to collect backup requirements in his attributes
            jobModel newJob = new jobModel();

            //Sets the backup requirements
            newJob.jobName = "Name";
            newJob.jobType = "complete";
            newJob.sourcePath = "C:/Users/lu-ro/Desktop/Test/Source";
            newJob.targetPath = "C:/Users/lu-ro/Desktop/Test/Destination";

            //Calls the Model function to Write the new backup in the file 
            bool verif = existingJob.WriteExistingJobs(newJob);

        }

        public void CollectExistingData()
        {

        }
    }
}