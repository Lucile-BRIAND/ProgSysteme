using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class CreateJobController 
    {
        CreateJobView createJobView = new CreateJobView();
        LanguageFile singletonLang = LanguageFile.GetInstance;
        List<string> recuperatedList = new List<string>();
        public void SaveJob()
        {
            ExistingJob existingJob = new ExistingJob();
            //Instanciate the new object to collect backup's requirement in his attributes
            JobModel newJob = new JobModel();

            //Set up the backup's requirement
            newJob.jobName = this.recuperatedList[0];
            newJob.jobType = this.recuperatedList[1];
            newJob.sourcePath = this.recuperatedList[2];
            newJob.targetPath = this.recuperatedList[3];

            //Calls the Model function to writes the new backup in the file 
            if(existingJob.WriteExistingJobs(newJob))
            {
                createJobView.DisplayMessage(singletonLang.ReadFile().Validation);
            }else
            {
                createJobView.DisplayMessage(singletonLang.ReadFile().ErrorExecute);
            }


        }
        public void InitView()
        {
            MainController mainController = new MainController();
            // Creation of the list Containing user's entry 
            List<string> CreateValues = new List<string>(4);
            CreateValues.Add(singletonLang.ReadFile().CreateName);
            CreateValues.Add(singletonLang.ReadFile().CreateType);
            CreateValues.Add(singletonLang.ReadFile().CreateSource);
            CreateValues.Add(singletonLang.ReadFile().CreateTarget);
            
            this.recuperatedList = createJobView.CollectRequirements(CreateValues);
            if (recuperatedList[1].Equals("1")) //Reads user's data when he chooses the differential type 
            {
                recuperatedList[1]=singletonLang.ReadFile().Type0;
                SaveJob();
                mainController.MainMenu();
            }
            else if(recuperatedList[1].Equals("2")) //Reads user's data when he chooses the complete type 
            {
                recuperatedList[1] = singletonLang.ReadFile().Type1;
                SaveJob();
                mainController.MainMenu();
            }
            else
            {
                createJobView.DisplayMessage(singletonLang.ReadFile().ErrorExecute); //Throws an error to the user if he enters something other than 1 or 2
                InitView();
            }
            
        }
    }
}
