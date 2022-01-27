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
        public void MainMenu()
        {

        }
        public void CallController()
        {

        }
        public void CallLanguage()
        {

        }
    }
}
