﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Appli_V1.Controllers
{
    class LanguageFile : FileAbstractClass
    {
        // Initialisation of the instace 
        private static LanguageFile languageInstance  = null ;
        // Private Attribute who contain the value writed by the user
        private string language;
        
        // Differents attributes used for the JSON reading
        public string Main { get; set; }
        public string Execute { get; set; }
        public string Delete { get; set; }
        public string Create_Name { get; set; }
        public string Create_Source { get; set; }
        public string Create_Target { get; set; }
        
        
        
       // Creation of a private constructor 
        private LanguageFile() { }
        // Initialise the unique instance
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
        // Allow the initialisation of the language 
        public void InitLanguage(String language)
        {
            this.language = language;
            
        }
        // Function used to read the JSON file containing the differents messages of the menus
        public LanguageFile ReadFile()
        {

            StreamReader streamreader = new StreamReader("../../../Languages/" + language + "_Lang.json");
            string jsonread = streamreader.ReadToEnd();
            LanguageFile data = JsonConvert.DeserializeObject<LanguageFile>(jsonread);
            Console.WriteLine(data.Main);
            return data;
            
        }
    }
}
