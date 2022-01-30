using System;
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
        public string CreateName { get; set; }
        public string CreateType { get; set; }
        public string CreateSource { get; set; }
        public string CreateTarget { get; set; }
        public string ErrorMain { get; set; }
        public string ErrorExecute { get; set; }
        public string Validation { get; set; }
        public string Main0 { get; set; }
        public string Main1 { get; set; }
        public string Main2 { get; set; }
        public string Type0 { get; set; }
        public string Type1 { get; set; }
        public string ExecuteType { get; set; }


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
        public void InitLanguage(string language)
        {
            this.language = language;
            
        }
        // Function used to read the JSON file containing menu's messages
        public LanguageFile ReadFile()
        {
            
            StreamReader streamreader = new StreamReader("../../../Languages/" + this.language + "_Lang.json");
            string jsonRead = streamreader.ReadToEnd();
            LanguageFile messageList = JsonConvert.DeserializeObject<LanguageFile>(jsonRead);
            return messageList;
            
        }

    }
}
