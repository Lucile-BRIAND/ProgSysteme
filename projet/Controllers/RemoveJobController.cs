using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class RemoveJobController 
    {
        private string valueEnter;
        RemoveJobView removeJobView = new RemoveJobView();
        ExistingJob existingJob = new ExistingJob();
        LanguageFile singletonLang = LanguageFile.GetInstance;

        public void GetFileContent()
        {
            string allFile = existingJob.ReadFile(); //Gets file content
            removeJobView.DisplayMessage(allFile); //Shows file content

        }
        public void RemoveJob()
        {
            if (existingJob.RemoveExistingJobs(this.valueEnter)) //Removes and checks if job is removed 
            {
                removeJobView.DisplayMessage(singletonLang.ReadFile().Validation);//Displays validation message
            }
            else
            {
                removeJobView.DisplayMessage(singletonLang.ReadFile().ErrorExecute); //Shows error message
            }
        }
        public void InitView()
        {
            removeJobView.DisplayMessage(singletonLang.ReadFile().Delete); //Shows delete option message
            GetFileContent(); //Shows all backups 
            this.valueEnter = removeJobView.CollectJobName(); //Collects entry name
            RemoveJob(); //Removes the backup from the file
            MainController mainController = new MainController();
            mainController.MainMenu(); //Returns to the main menu 

        }
    }
}