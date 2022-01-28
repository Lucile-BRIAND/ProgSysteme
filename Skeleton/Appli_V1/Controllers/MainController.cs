using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class MainController
    {
        private int CollectChoice;
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
            Init_Main_Labels();
            //Shows the initial message
            mainView.DisplayOptions(Singleton_Lang.ReadFile().Main);
            //Shows the different possibilities of the application (main menu)
            mainView.DisplayOptions("1. " + First_Main);
            mainView.DisplayOptions("2. " + Second_Main);
            mainView.DisplayOptions("3. " + Third_Main);
            //Collects the entry of the user
            mainView.CollectOptions();
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
            if (this.First_Main.Equals("1") | this.Second_Main.Equals("2") | this.Third_Main.Equals("3"))
            {
                CallController();
            }
            else
            {
                langView.DisplayErrorMessage(Singleton_Lang.ReadFile().Error_Main);
                MainMenu();
                mainView.CollectOptions();
            }
        }

        public void CallController()
        {
            Console.Write("Oui pq pas"); //TEST
        }

    }
}