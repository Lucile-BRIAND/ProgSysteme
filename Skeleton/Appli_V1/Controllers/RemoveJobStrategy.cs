using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class RemoveJobStrategy : IStrategyController
    {
        private int language;
        private string value_enter;
        RemoveJobStrategyView removeJobStrategyView = new RemoveJobStrategyView();
        ExistingJob existingJob = new ExistingJob();
        LanguageFile Singleton_Lang = LanguageFile.GetInstance;
        public void CheckRequirements()
        {
            string allFile = existingJob.ReadFile(); //Get file content
            removeJobStrategyView.DisplayExistingData(allFile); //Show file content

        }
        public void CollectExistingData()
        {
           if(existingJob.RemoveExistingJobs(this.value_enter)) //Check if job is removed 
            {
                    //DisplayValidation
            }
            else
            {
                removeJobStrategyView.DisplayErrorMessage(Singleton_Lang.ReadFile().Error_Remove); //Show error message
            }
        }
        public void InitView()
        {
            removeJobStrategyView.DisplayExistingData(Singleton_Lang.ReadFile().Delete); //Show delete option message
            CheckRequirements(); //Show all backups 
            this.value_enter = removeJobStrategyView.CollectOptions(); //Collect entry name 
         
        }
    }
}
