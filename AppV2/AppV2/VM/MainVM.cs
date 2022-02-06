using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppV2.Models;
using Newtonsoft.Json;

namespace AppV2.VM
{
    public class MainVM : INotifyPropertyChanged, IDisposable
    {

        public List<JobModel> dataList = new List<JobModel>();
        public event PropertyChangedEventHandler PropertyChanged;
        LanguageFile gt = LanguageFile.GetInstance;
        public string jobSoftware { get; set; }
        public string chooseAction { get; set; }
        public string createBackup { get; set; }
        public string executeBackup { get; set; }
        public string removeBackup { get; set; }
        public string Language1 { get; set; }
        public string Language2 { get; set; }
        public string jobSoftwareName;
        

        public  MainVM getValues()
        {
            var values = new MainVM()
            {
                chooseAction = gt.ReadFile().Main,
                createBackup = gt.ReadFile().MainCreate,
                executeBackup = gt.ReadFile().MainExecute,
                removeBackup = gt.ReadFile().MainRemove,
                Language1 = "French",
                Language2 = "English"

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
            List<JobModel> jobmodelList = new List<JobModel>();
            var contentFile = System.IO.File.ReadAllText(file);
            jobmodelList = JsonConvert.DeserializeObject<List<JobModel>>(contentFile);
            return jobmodelList;
        }
    }
}
