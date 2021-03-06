using System;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;

namespace AppV3.Models
{
    public class LanguageFile : INotifyPropertyChanged //SINGLETON
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Instance null by default 
        private static LanguageFile languageInstance = null;

        //Private attribute who contains the value writed by the user
        private string language = "English";
        public string mainCreate;

        //Different attributes used for the JSON reading
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
        public string startConnection { get; set; }
        public string ValidationJobSoftware { get; set; }
        public string ValidationRemoveJob { get; set; }
        public string ValidationCreateJob { get; set; }
        public string ChooseLang { get; set; }
        public string LogFileExtensionLabel { get; set; }
        public string Settings { get; set; }
        public string ExtentionLabel { get; set; }
        public string MaximumFileSizeLabel { get; set; }
        public string showBar { get; set; }
        public string acceptRequest { get; set; }
        public string resumeBackup { get; set; }
        public string MainReturn { get; set; }
        public string stopBackup { get; set; }
        public string pauseBackup { get; set; }
        public string ExtentionToPrioritizeLabel { get; set; }

        //private constructor 
        private LanguageFile() { }

        //Initialize the unique instance
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
