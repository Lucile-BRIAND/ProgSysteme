using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class MainController 
    {
       
        ExecuteJobStrategy executeJobStrategy = new ExecuteJobStrategy();
        RemoveJobStrategy removeJobStrategy = new RemoveJobStrategy();
        CreateJobStrategy createJobStrategy = new CreateJobStrategy();
        MainView mainView = new MainView();
        LanguageFile Singleton_Lang = LanguageFile.GetInstance;

        private string First_Main;
        private string Second_Main;
        private string Third_Main;
        private string CollectChoice;
        public void MainMenu()
        {
            // Initializes the name of the three labels
            Init_Main_Labels();
            // Shows the initial message
            mainView.DisplayOptions(Singleton_Lang.ReadFile().Main);
            // Shows the differents possibilities of the Application
            mainView.DisplayOptions("1. " + First_Main);
            mainView.DisplayOptions("2. " + Second_Main);
            mainView.DisplayOptions("3. " + Third_Main);
            // Initializes the private attribute with selected value
            this.CollectChoice = mainView.CollectOptions();
            //Input verification
            CheckRequirements();
        }
        public void Init_Main_Labels() 
        {
            First_Main = Singleton_Lang.ReadFile().Main_0;
            Second_Main = Singleton_Lang.ReadFile().Main_1;
            Third_Main = Singleton_Lang.ReadFile().Main_2; 
        }
        public void CheckRequirements() //Collect the user's entry 
        {

            if (this.CollectChoice.Equals("1") | this.CollectChoice.Equals("2") | this.CollectChoice.Equals("3"))
            {
                CallOfControllers();
            }
            else
            {
                mainView.DisplayOptions(Singleton_Lang.ReadFile().Error_Main);
                MainMenu();
                this.CollectChoice = mainView.CollectOptions();
            }

        }
        public void CallOfControllers() //Call the controller that the user has choosen 
        {
            
            if(this.CollectChoice.Equals("1"))
            {
                createJobStrategy.InitView();
            }
            else if(this.CollectChoice.Equals("2"))
            {
                executeJobStrategy.InitView();
            }
            else if(this.CollectChoice.Equals("3"))
            {
                removeJobStrategy.InitView();
            }
        }

    }
}
