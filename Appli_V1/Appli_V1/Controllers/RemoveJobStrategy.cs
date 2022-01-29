using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class RemoveJobStrategy 
    {
        private string value_enter;
        RemoveJobStrategyView removeJobStrategyView = new RemoveJobStrategyView();
        ExistingJob existingJob = new ExistingJob();
        LanguageFile Singleton_Lang = LanguageFile.GetInstance;

        public void CheckRequirements()
        {
            string allFile = existingJob.ReadFile(); //Gets file content
            removeJobStrategyView.DisplayExistingData(allFile); //Shows file content

        }
        public void CollectExistingData()
        {
            if (existingJob.RemoveExistingJobs(this.value_enter)) //Removes and checks if job is removed 
            {
                removeJobStrategyView.DisplayExistingData(Singleton_Lang.ReadFile().Validation);//Displays validation message
            }
            else
            {
                removeJobStrategyView.DisplayExistingData(Singleton_Lang.ReadFile().Error_Execute); //Shows error message
            }
        }
        public void InitView()
        {
            removeJobStrategyView.DisplayExistingData(Singleton_Lang.ReadFile().Delete); //Shows delete option message
            CheckRequirements(); //Shows all backups 
            this.value_enter = removeJobStrategyView.CollectOptions(); //Collects entry name
            CollectExistingData(); //Removes the backup from the file
            MainController mainController = new MainController();
            mainController.MainMenu(); //Returns to the main menu 

        }
    }
}