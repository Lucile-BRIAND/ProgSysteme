using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class MainController
    {
        private string CollectChoice;

        // The MainController class uses different Controller to interact properly with the user
        // Here we instanciate the Controller Class object so that we could later call the appropriate method
        ChooseLanguageStrategy chooseLanguageStrategy = new ChooseLanguageStrategy();
        ExecuteJobStrategy executeJobStrategy = new ExecuteJobStrategy();
        RemoveJobStrategy removeJobStrategy = new RemoveJobStrategy();
        CreateJobStrategy createJobStrategy = new CreateJobStrategy();
        MainView mainView = new MainView();
        LanguageFile Singleton_Lang = LanguageFile.GetInstance;

        // NEED TO BE COMMENT
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

            // Verification of the entered value
            CheckRequirements();
        }
        public void Init_Main_Labels()
        {
            // NEED TO BE COMMENT
            First_Main = Singleton_Lang.ReadFile().Main_0;
            Second_Main = Singleton_Lang.ReadFile().Main_1;
            Third_Main = Singleton_Lang.ReadFile().Main_2;
        }
        public void CheckRequirements()
        {
            // Here we check if the user's task correspond to one of the expected entry
            if (this.CollectChoice.Equals("1") | this.CollectChoice.Equals("2") | this.CollectChoice.Equals("3"))
            {
                CallOfControllers();
            }
            // Else we display an error message to the user
            else
            {
                langView.DisplayErrorMessage(Singleton_Lang.ReadFile().Error_Main);
                MainMenu();
                this.CollectChoice = mainView.CollectOptions();
            }

        }
        public void CallOfControllers()
        {
            // The CallOfControllers method permit the interraction between the MainController, the user, and the different view to display
            // For each value, the function call the corresponding object with InitView method that initialize each process
            if (this.CollectChoice.Equals("1"))
            {
                createJobStrategy.InitView();
            }
            else if (this.CollectChoice.Equals("2"))
            {
                executeJobStrategy.InitView();
            }
            else if (this.CollectChoice.Equals("3"))
            {
                removeJobStrategy.InitView();
            }
        }

    }
}