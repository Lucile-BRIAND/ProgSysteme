using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_V1.Controllers
{
    class MainController 
    {
       
        ExecuteJobController executeJobController = new ExecuteJobController();
        RemoveJobController removeJobController = new RemoveJobController();
        CreateJobController createJobController = new CreateJobController();
        MainView mainView = new MainView();
        LanguageFile singletonLang = LanguageFile.GetInstance;

        private string firstMain;
        private string secondMain;
        private string thirdMain;
        private string collectChoice;
        public void MainMenu()
        {
            // Initializes the name of the three labels
            InitMenu();
            // Shows the initial message
            mainView.DisplayMenu(singletonLang.ReadFile().Main);
            // Shows the differents possibilities of the Application
            mainView.DisplayMenu("1. " + firstMain);
            mainView.DisplayMenu("2. " + secondMain);
            mainView.DisplayMenu("3. " + thirdMain);
            // Initializes the private attribute with selected value
            this.collectChoice = mainView.CollectChoice();
            //Input verification
            CheckUserEntry();
        }
        public void InitMenu() 
        {
            firstMain = singletonLang.ReadFile().Main0;
            secondMain = singletonLang.ReadFile().Main1;
            thirdMain = singletonLang.ReadFile().Main2; 
        }
        public void CheckUserEntry() //Checks the user's entry 
        {

            if (this.collectChoice.Equals("1") | this.collectChoice.Equals("2") | this.collectChoice.Equals("3"))
            {
                CallControllers();
            }
            else
            {
                mainView.DisplayMenu(singletonLang.ReadFile().ErrorMain);
                MainMenu();
                this.collectChoice = mainView.CollectChoice();
            }

        }
        public void CallControllers() //Call the controller that the user has choosen 
        {
            
            if(this.collectChoice.Equals("1"))
            {
                createJobController.InitView();
            }
            else if(this.collectChoice.Equals("2"))
            {
                executeJobController.SaveChoice();
            }
            else if(this.collectChoice.Equals("3"))
            {
                removeJobController.InitView();
            }
        }

    }
}
