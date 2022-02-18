using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppV3.Models
{
    class LanguageFile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Instance null by default 
        private static LanguageFile languageInstance = null;
        // Private Attribute who contains the value writed by the user
        private string language = "English";
        public string mainCreate;
        // Differents attributes used for the JSON reading
        public string Main { get; set; }
        public string Execute { get; set; }
        public string Delete { get; set; }
        public string CreateName { get; set; }
        public string CreateType { get; set; }
        public string CreateSource { get; set; }
        public string CreateTarget { get; set; }
        public string ErrorGrid { get; set; }
        public string ErrorExecute { get; set; }
        public string Validation { get; set; }
        public string MainCreate { get; set; }
        public string MainExecute { get; set; }
        public string MainRemove { get; set; }
        public string Type0 { get; set; }
        public string Type1 { get; set; }
        public string ExecuteType { get; set; }
        public string JobSoftware { get; set; }
        public string stopBackup { get; set; }
        public string pauseBackup { get; set; }
  

        public string ValidationJobSoftware { get; set; }
        public string ValidationRemoveJob { get; set; }
        public string ValidationCreateJob { get; set; }

        public string MainReturn { get; set; }

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
            
            StreamReader streamreader = new StreamReader("../../Languages/" + this.language + "_Lang.json");
                                                                
            string jsonRead = streamreader.ReadToEnd();
            LanguageFile messageList = JsonConvert.DeserializeObject<LanguageFile>(jsonRead);
            return messageList;

        }

    }
}
