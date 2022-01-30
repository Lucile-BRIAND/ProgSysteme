using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Appli_V1.Controllers
{
    class ChooseLanguageController 
    {
        // Language view initialization
        LanguageView langView = new LanguageView();
        // Collects the model's intance
        LanguageFile singletonLang = LanguageFile.GetInstance;
        // Attribute containing the value of the selected language
        public string languageSelected;
        //Function who checks if the language exist
        public void CheckRequirements()
        {

            if (this.languageSelected.Equals("1") | this.languageSelected.Equals("2"))
            {
                
            }else{
                langView.DisplayErrorMessage("Langue Saisie non valide / Language enter none valid");
                InitView();
                
            }
           
        }
        //Function who collects the data
        public void CollectExistingData()
        {
            
            if (this.languageSelected.Equals("1"))
            {
                singletonLang.InitLanguage("English");
                singletonLang.ReadFile();

            }else if(this.languageSelected.Equals("2"))
            {
                singletonLang.InitLanguage("French");
                singletonLang.ReadFile();
            }
        }
        //Function who allows to initialize the views and to collects the user's data
        public void InitView()
        {
            langView.DisplayLanguageMessages();
            this.languageSelected = langView.CollectLanguage();
        }
    }
}
