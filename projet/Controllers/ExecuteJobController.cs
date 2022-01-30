using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace Appli_V1.Controllers
{
    class ExecuteJobController //: IStrategyController
    {
        private string source;
        private string destination;
        private string name;
        private string type;
        private string valueEnter;
        private string typeEnter;
        ExecuteJobView executeStrategyView = new ExecuteJobView();
        LanguageFile singletonLang = LanguageFile.GetInstance;
        ExistingJob existingJob = new ExistingJob();
        LogFile lf = LogFile.GetInstance;

        public ExecuteJobController()
        {
        }

        public void ExecuteJobAndLogs()
        {
            if(name != null)
            {
                // Send Validation Message
                executeStrategyView.DisplayMessage(singletonLang.ReadFile().Validation);
            }
            else
            {
                // Send Error Message
                executeStrategyView.DisplayMessage(singletonLang.ReadFile().ErrorExecute);
                SaveChoice();
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
            else if (type == "Differential" | type== "Differentielle")
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
        public void SaveChoice()
        {
            //Read the JSON file containing the job's data
            var contentFile = System.IO.File.ReadAllText(existingJob.file);
            var jobModelList = JsonConvert.DeserializeObject<List<JobModel>>(contentFile);
            MainController mc = new MainController();
            // Show the message asking the user to write the type of the execution and collect her repsonce 
            executeStrategyView.DisplayMessage(singletonLang.ReadFile().ExecuteType);
            this.typeEnter = executeStrategyView.CollectName();
            // If the user want to execute one backup
            if(this.typeEnter.Equals("1"))
            {
                // Show message of the name's backup
                executeStrategyView.DisplayMessage(singletonLang.ReadFile().Execute);
                // Collect name's backup
                this.valueEnter = executeStrategyView.CollectName();
                // Collect the jobModel object corresponding
                foreach (JobModel attribute in jobModelList)
                {
                    if (attribute.jobName == this.valueEnter)
                    {
                        name = attribute.jobName;
                        type = attribute.jobType;
                        source = attribute.sourcePath;
                        destination = attribute.targetPath;
                        ExecuteJobAndLogs();
                    }
                }
                // Redirecton to main menu
                mc.MainMenu();

            }
            // If user want to execute all of the backups
            else if(this.typeEnter.Equals("2"))
            {
                // Collect the jobModel object corresponding
                foreach (JobModel attribute in jobModelList)
                {
                        name = attribute.jobName;
                        type = attribute.jobType;
                        source = attribute.sourcePath;
                        destination = attribute.targetPath;
                        ExecuteJobAndLogs();
                }
                // Redirection to main menu
                mc.MainMenu();
            }
            else
            {
                SaveChoice();

            }
            
        }

    }
}