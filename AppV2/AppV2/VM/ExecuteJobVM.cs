using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppV2;
using AppV2.Models;
using System.Diagnostics;
using System.Threading;

namespace AppV2.VM
{
    class ExecuteJobVM : MainVM
    {
        //public string jobSoftwareName;
        public string executeBackup { get; set; }

        public string mainMenu { get; set; }

        

        LanguageFile gt = LanguageFile.GetInstance;
        LogFile lf = LogFile.GetInstance;
        public ExecuteJobVM getValues()
        {
            var values = new ExecuteJobVM()
            {
                executeBackup = gt.ReadFile().Execute,
                mainMenu = gt.ReadFile().MainReturn
            };

            return values;

        }
        /*
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
        */
        public void ExecuteBackup(string name, string type, string source, string destination, string JobSoftwareNameTextBox)
        {
            Thread.Sleep(10000);
            Process[] myProcesses = Process.GetProcessesByName(JobSoftwareNameTextBox);

            if (myProcesses.Length != 0)
            {
                return;
            }

            int nbfile = 0; //number of files that have been copied
            StatusLogFile file1 = StatusLogFile.GetInstance;


            if (type == "Complete" | type == "Complète")
            {
                int totalNbFileComplete = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories).Length; //total number of files in the save

                //Appends the text in the status log file  => state 0 : initialization
                file1.WriteStatusLogMessage(name, type, source, destination, "ACTIVE", totalNbFileComplete, 1000, totalNbFileComplete - nbfile);

                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(source, destination));
                }

                //Copies all the files & replaces any file with the same name
                foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
                {
                    nbfile++;
                    File.Copy(newPath, newPath.Replace(source, destination), true);

                    //Appends the text in the status log file
                    file1.WriteStatusLogMessage(name, type, source, destination, "ACTIVE", totalNbFileComplete, 1000, totalNbFileComplete - nbfile);
                }
                lf.WriteLogMessage(name, source, destination, nbfile, 2);

            }
            else if (type == "Differential" | type == "Differentielle")
            {
                string[] originalFiles = Directory.GetFiles(source, "*", SearchOption.AllDirectories);
                int totalNbFileDifferential = 0; //number of files that will be copied

                //FOREACH : counts the number of files that we have to copy
                Array.ForEach(originalFiles, (originalFileLocation) =>
                {
                    FileInfo originalFile = new FileInfo(originalFileLocation);
                    FileInfo destFile = new FileInfo(originalFileLocation.Replace(source, destination));

                    if (destFile.Exists)
                    {
                        if (originalFile.Length > destFile.Length)
                        {
                            totalNbFileDifferential++;
                        }
                    }
                    else
                    {
                        totalNbFileDifferential++;
                    }
                });

                //Appends the text in the status log file => state 0 : initialization
                if (totalNbFileDifferential != 0)
                {
                    file1.WriteStatusLogMessage(name, type, source, destination, "ACTIVE", totalNbFileDifferential, 1000, totalNbFileDifferential - nbfile);
                }
                //FOREACH : copies the files
                Array.ForEach(originalFiles, (originalFileLocation) =>
                {
                    FileInfo originalFile = new FileInfo(originalFileLocation);
                    FileInfo destFile = new FileInfo(originalFileLocation.Replace(source, destination));

                    if (destFile.Exists)
                    {
                        if (originalFile.Length > destFile.Length)
                        {
                            originalFile.CopyTo(destFile.FullName, true);
                            nbfile++;

                            //Appends the text in the status log file
                            file1.WriteStatusLogMessage(name, type, source, destination, "ACTIVE", totalNbFileDifferential, 1000, totalNbFileDifferential - nbfile);
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(destFile.DirectoryName);
                        originalFile.CopyTo(destFile.FullName, false);
                        nbfile++;

                        //Appends the text in the status log file
                        file1.WriteStatusLogMessage(name, type, source, destination, "ACTIVE", totalNbFileDifferential, 1000, totalNbFileDifferential - nbfile);
                    }
                });
                lf.WriteLogMessage(name, source, destination, nbfile, 2);
            }
        }
        public void ExecutAllBackup(string JobSoftwareNameTextBox)
        {
            //Read the JSON file containing the job's data
            var contentFile = System.IO.File.ReadAllText("Jobfile.json");
            var jobModelList = JsonConvert.DeserializeObject<List<JobModel>>(contentFile);
            


            foreach (JobModel attribute in jobModelList)
            {
            
                string name = attribute.jobName;
                string type = attribute.jobType;
                string source = attribute.sourcePath;
                string destination = attribute.targetPath;
                ExecuteBackup(name, type, source, destination, JobSoftwareNameTextBox);
            }
        }
    }
}
