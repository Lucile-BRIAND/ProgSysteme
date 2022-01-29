using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class CreateJobStrategy 
    {
        CreateJobStrategyView createJobStrategyView = new CreateJobStrategyView();
        LanguageFile Singleton_Lang = LanguageFile.GetInstance;
        List<string> Recuperated_List = new List<string>();
        public void CheckRequirements()
        {
            ExistingJob existingJob = new ExistingJob();
            //Instanciate the new object to collect backup's requirement in his attributes
            jobModel newJob = new jobModel();

            //Set up the backup's requirement
            newJob.jobName = this.Recuperated_List[0];
            newJob.jobType = this.Recuperated_List[1];
            newJob.sourcePath = this.Recuperated_List[2];
            newJob.targetPath = this.Recuperated_List[3];

            //Calls the Model function to writes the new backup in the file 
            if(existingJob.WriteExistingJobs(newJob))
            {
                createJobStrategyView.DisplayExistingData(Singleton_Lang.ReadFile().Validation);
            }else
            {
                createJobStrategyView.DisplayExistingData(Singleton_Lang.ReadFile().Error_Execute);
            }


        }
        public void CollectExistingData()
        {
            
        }
        public void InitView()
        {
            MainController mainController = new MainController();
            // Creation of the list Containing user's entry 
            List<string> CreateValues = new List<string>(4);
            CreateValues.Add(Singleton_Lang.ReadFile().Create_Name);
            CreateValues.Add(Singleton_Lang.ReadFile().Create_Type);
            CreateValues.Add(Singleton_Lang.ReadFile().Create_Source);
            CreateValues.Add(Singleton_Lang.ReadFile().Create_Target);
            
            this.Recuperated_List = createJobStrategyView.CollectOptions(CreateValues);
            if (Recuperated_List[1].Equals("1")) //Reads user's data when he chooses the differential type 
            {
                Recuperated_List[1]=Singleton_Lang.ReadFile().Type_0;
                CheckRequirements();
                mainController.MainMenu();
            }
            else if(Recuperated_List[1].Equals("2")) //Reads user's data when he chooses the complete type 
            {
                Recuperated_List[1] = Singleton_Lang.ReadFile().Type_1;
                CheckRequirements();
                mainController.MainMenu();
            }
            else
            {
                createJobStrategyView.DisplayExistingData(Singleton_Lang.ReadFile().Error_Execute); //Throws an error to the user if he enters something other than 1 or 2
                InitView();
            }
            
        }
    }
}
