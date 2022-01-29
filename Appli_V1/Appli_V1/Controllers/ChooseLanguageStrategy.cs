using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Appli_V1.Controllers
{
    class ChooseLanguageStrategy 
    {
        // Language view initialization
        LanguageStrategyView langView = new LanguageStrategyView();
        // Collects the model's intance
        LanguageFile Singleton_Lang = LanguageFile.GetInstance;
        // Attribute containing the value of the selected language
        public string language_Selected;
        //Function who checks if the language exist
        public void CheckRequirements()
        {

            if (this.language_Selected.Equals("1") | this.language_Selected.Equals("2"))
            {
                
            }else{
                langView.DisplayErrorMessage("Langue Saisie non valide / Language enter none valid");
                InitView();
                
            }
           
        }

        //Function who collects the data
        public void CollectExistingData()
        {
            
            if (this.language_Selected.Equals("1"))
            {
                Singleton_Lang.InitLanguage("English");
                Singleton_Lang.ReadFile();
            }else if(this.language_Selected.Equals("2"))
            {
                Singleton_Lang.InitLanguage("French");
                Singleton_Lang.ReadFile();
            }
        }
        //Function who allows to initialize the views and to collects the user's data
        public void InitView()
        {
            langView.DisplayExistingData();
            this.language_Selected = langView.CollectOptions();
        }
    }
}
