using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class CreateJobStrategy : IStrategyController
    {
        private int language;
        private string value_enter;
        CreateJobStrategyView createJobStrategyView = new CreateJobStrategyView();
        LanguageFile Singleton_Lang = LanguageFile.GetInstance;
        List<string> Recuperated_List = new List<string>();
        public void CheckRequirements()
        {
            ExistingJob existingJob = new ExistingJob();
            //Instanciate the new object to collect backup's requirement in his attributes
            jobModel newJob = new jobModel();

            // Set up the backup's requirement
            newJob.jobName = this.Recuperated_List[0];
            newJob.jobType = this.Recuperated_List[1];
            newJob.sourcePath = this.Recuperated_List[2];
            newJob.targetPath = this.Recuperated_List[3];

            //Call the Model function to Writes the new backup in the file 
            if(existingJob.WriteExistingJobs(newJob))
            {
                createJobStrategyView.Confirmation_Message(Singleton_Lang.ReadFile().Validation);
            }else
            {
                createJobStrategyView.DisplayError(Singleton_Lang.ReadFile().Error_Execute);
            }


        }
        public void CollectExistingData()
        {
            
        }
        public void InitView()
        {
            // Creation of the list Containing needed for a job
            List<string> CreateValues = new List<string>(4);
            CreateValues.Add(Singleton_Lang.ReadFile().Create_Name);
            CreateValues.Add(Singleton_Lang.ReadFile().Create_Type);
            CreateValues.Add(Singleton_Lang.ReadFile().Create_Source);
            CreateValues.Add(Singleton_Lang.ReadFile().Create_Target);
            // 
            // Send to the view the list of the create values
            this.Recuperated_List = createJobStrategyView.DisplayExistingData(CreateValues);
            if (Recuperated_List[1].Equals("1"))
            {
                Recuperated_List[1]=Singleton_Lang.ReadFile().Type_0;
                CheckRequirements();
            }
            else if(Recuperated_List[1].Equals("2"))
            {
                Recuperated_List[1] = Singleton_Lang.ReadFile().Type_1;
                CheckRequirements();
            }
            else
            {
                createJobStrategyView.DisplayError(Singleton_Lang.ReadFile().Error_Execute);
                InitView();
            }
            
        }
    }
}
