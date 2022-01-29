﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Appli_V1.Controllers
{
    class ChooseLanguageStrategy : IStrategyController
    {
        // Initialisation of language view
        LanguageStrategyView langView = new LanguageStrategyView();
        // Collect the instace of the model
        LanguageFile Singleton_Lang = LanguageFile.GetInstance;
        // Attribute containing the value of the language selected
        public string language_Selected;
        //Function who check if language exist
        public void CheckRequirements()
        {

            if (this.language_Selected.Equals("1") | this.language_Selected.Equals("2"))
            {
                
            }else{
                langView.DisplayErrorMessage("Langue Saisie non valide / Language enter none valid");
                InitView();
                
            }
           
        }

        //Function who collect the data
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
        // Function who call the initialise the view and collect the data
        public void InitView()
        {
            langView.DisplayExistingData();
            this.language_Selected = langView.CollectOptions();
        }
    }
}