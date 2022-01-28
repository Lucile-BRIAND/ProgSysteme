using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class MainController 
    {
        private string CollectChoice;
        ChooseLanguageStrategy chooseLanguageStrategy = new ChooseLanguageStrategy();
        ExecuteJobStrategy executeJobStrategy = new ExecuteJobStrategy();
        RemoveJobStrategy removeJobStrategy = new RemoveJobStrategy();
        CreateJobStrategy createJobStrategy = new CreateJobStrategy();
        MainView mainView = new MainView();
        LanguageFile Singleton_Lang = LanguageFile.GetInstance;

        private string First_Main;
        private string Second_Main;
        private string Third_Main;
        LanguageStrategyView langView = new LanguageStrategyView();

        

        public void MainMenu()
        {
            // Initialise the name of the three labels
            Init_Main_Labels();
            // Show's the initial message
            mainView.DisplayOptions(Singleton_Lang.ReadFile().Main);
            // Show the differents possibilities of the Application
            mainView.DisplayOptions("1. " + First_Main);
            mainView.DisplayOptions("2. " + Second_Main);
            mainView.DisplayOptions("3. " + Third_Main);
            // Initialise the private attribute with selected value
            this.CollectChoice = mainView.CollectOptions();
            // Verification of the value enter
            CheckRequirements();
        }
        public void Init_Main_Labels()
        {
            First_Main = Singleton_Lang.ReadFile().Main_0;
            Second_Main = Singleton_Lang.ReadFile().Main_1;
            Third_Main = Singleton_Lang.ReadFile().Main_2;
        }
        public void CheckRequirements()
        {

            if (this.CollectChoice.Equals("1") | this.CollectChoice.Equals("2") | this.CollectChoice.Equals("3"))
            {
                CallOfControllers();
            }
            else
            {
                langView.DisplayErrorMessage(Singleton_Lang.ReadFile().Error_Main);
                MainMenu();
                this.CollectChoice = mainView.CollectOptions();
            }

        }
        public void CallOfControllers()
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
