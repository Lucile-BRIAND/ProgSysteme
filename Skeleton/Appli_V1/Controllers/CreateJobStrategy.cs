using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class CreateJobStrategy : IStrategyController
    {
        private string value_enter;
        //Initialization of language view
        CreateJobStrategyView createJobStrategyView = new CreateJobStrategyView();

         // Instanciate new object to work between ExistingJob Model and CreateJobStrategy Controller
         ExistingJob existingJob = new ExistingJob();

        public void CheckRequirements()
        {
            //Instanciate the new object to collect backup's requirement in his attributes
            jobModel newJob = new jobModel();

            // Set up the backup's requirement
            newJob.jobName = "Name";
            newJob.jobType = "complete";
            newJob.sourcePath = "C:/Users/lu-ro/Desktop/Test/Source";
            newJob.targetPath = "C:/Users/lu-ro/Desktop/Test/Destination";

            //Call the Model function to Writes the new backup in the file 
            this.value_enter = existingJob.WriteExistingJobs(newJob);

        }
        public void CollectExistingData()
        {
            if(existingJob.WriteExistingJobs(this.value_enter)) //Check if job is added 
            {
                    //DisplayValidation
            }
            else
            {
               createJobStrategyView.DisplayErrorMessage(Singleton_Lang.ReadFile().Error_Create); //Show error message
            }
        }

        public void InitView()
        {
            createJobStrategyView.DisplayExistingData(Singleton_Lang.ReadFile().Create); //Show added option message
            CheckRequirements(); //Show all backups 
            this.value_enter = createJobStrategyView.CollectOptions(); //Collect entry name 
         
        }
    }
}