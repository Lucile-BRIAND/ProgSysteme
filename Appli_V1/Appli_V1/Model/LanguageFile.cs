﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Appli_V1.Controllers
{
    class LanguageFile
    {
        //Instance null by default 
        private static LanguageFile languageInstance  = null ;
        // Private Attribute who contains the value writed by the user
        private string language;
        
        // Differents attributes used for the JSON reading
        public string Main { get; set; }
        public string Execute { get; set; }
        public string Delete { get; set; }
        public string Create_Name { get; set; }
        public string Create_Type { get; set; }
        public string Create_Source { get; set; }
        public string Create_Target { get; set; }
        public string Error_Main { get; set; }
        public string Error_Execute { get; set; }
        public string Validation { get; set; }
        public string Main_0 { get; set; }
        public string Main_1 { get; set; }
        public string Main_2 { get; set; }
        public string Type_0 { get; set; }
        public string Type_1 { get; set; }


        //private constructor 
        private LanguageFile() { }
        // Initializes the unique instance
        public static LanguageFile GetInstance
        {
            get
            {
                if (languageInstance == null)
                {
                    languageInstance = new LanguageFile();
                }
                return languageInstance;
            }

        }
        // Allows the initialisation of the language 
        public void InitLanguage(String language)
        {
            this.language = language;
            
        }
        // Function used to read the JSON file containing menu's messages
        public LanguageFile ReadFile()
        {
            
            StreamReader streamreader = new StreamReader("../../../Languages/" + this.language + "_Lang.json");
            string jsonread = streamreader.ReadToEnd();
            LanguageFile data = JsonConvert.DeserializeObject<LanguageFile>(jsonread);
            return data;
            
        }

    }
}
