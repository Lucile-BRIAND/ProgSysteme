﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppV3.Models;
using Newtonsoft.Json;

namespace AppV3.VM
{
    public class MainVM : INotifyPropertyChanged, IDisposable
    {

        public event PropertyChangedEventHandler PropertyChanged;
        LanguageFile singletonLang = LanguageFile.GetInstance;
        FileExtentions fileExtentions = FileExtentions.GetInstance;
        public string jobSoftware { get; set; }
        public string chooseAction { get; set; }
        public string createBackup { get; set; }
        public string executeBackup { get; set; }
        public string removeBackup { get; set; }
        public string Language1 { get; set; }
        public string Language2 { get; set; }
        public string JobSoftwareLabel { get; set; }
        public string LanguageLabel { get; set; }
        public string LogFileExtensionLabel { get; set; }
        public string ButtonValidate { get; set; }
        public string ButtonMainMenu { get; set; }
        public string ButtonSettings { get; set; }
        public string ExtentionLabel { get; set; }
        public bool PNGvalue { get; set; }
        public bool JPGvalue { get; set; }
        public bool PDFvalue { get; set; }
        public bool TXTvalue { get; set; }
        public string MaximumFileSizeLabel { get; set; }



        public string jobSoftwareName;


        public MainVM getValues()
        {
            var values = new MainVM()
            {
                chooseAction = singletonLang.ReadFile().Main,
                createBackup = singletonLang.ReadFile().MainCreate,
                executeBackup = singletonLang.ReadFile().MainExecute,
                removeBackup = singletonLang.ReadFile().MainRemove,
                Language1 = "French",
                Language2 = "English",
                JobSoftwareLabel = singletonLang.ReadFile().JobSoftware,
                LanguageLabel = singletonLang.ReadFile().ChooseLang,
                LogFileExtensionLabel = singletonLang.ReadFile().LogFileExtensionLabel,
                ButtonValidate = singletonLang.ReadFile().ValidationJobSoftware,
                ButtonMainMenu = singletonLang.ReadFile().MainReturn,
                ButtonSettings = singletonLang.ReadFile().Settings,
                ExtentionLabel = singletonLang.ReadFile().ExtentionLabel,
                TXTvalue = fileExtentions.TXTvalue,
                PDFvalue = fileExtentions.PDFvalue,
                JPGvalue = fileExtentions.JPGvalue,
                PNGvalue = fileExtentions.PNGvalue,
                MaximumFileSizeLabel = singletonLang.ReadFile().MaximumFileSizeLabel
            };
            return values;

        }
        public string JobSoftwareName
        {
            get
            {
                return jobSoftwareName;
            }
            set
            {
                if (!string.Equals(jobSoftwareName, value))
                {
                    jobSoftwareName = value;
                    OnPropertyChanged("JobSoftwareName");
                }
            }
        }


        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose() { }
        public static List<JobModel> DisplayJobs()
        {
            string file = "Jobfile.json";
            List<JobModel> jobModelList = new List<JobModel>();
            var contentFile = System.IO.File.ReadAllText(file);
            jobModelList = JsonConvert.DeserializeObject<List<JobModel>>(contentFile);
            return jobModelList;
        }
    }
}
