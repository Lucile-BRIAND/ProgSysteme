﻿using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace Appli_V1.Controllers
{
    class ExecuteJobStrategy //: IStrategyController
    {
        private int language;
        private string[] jobRequirements;
        private string source;
        private string destination;
        private string name;
        private string type;
        private string value_enter;
        ExecuteStrategyView executeStrategyView = new ExecuteStrategyView();
        LanguageFile Singleton_Lang = LanguageFile.GetInstance;
        jobModel jobmodel = new jobModel();
        ExistingJob existingJob = new ExistingJob();
        public ExecuteJobStrategy()
        {

        }

        public void CheckRequirements()
        {

            var contentFile = System.IO.File.ReadAllText(existingJob.file);
            var jobModelList = JsonConvert.DeserializeObject<List<jobModel>>(contentFile);
            foreach(jobModel attribute in jobModelList)
            {
                if (attribute.jobName == this.value_enter)
                {
                    name = attribute.jobName;
                    type = attribute.jobType;
                    source = attribute.sourcePath;
                    destination = attribute.targetPath;
                }
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
                // Send Validation Message
                executeStrategyView.DisplayExistingData(Singleton_Lang.ReadFile().Validation);

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
                else
                {
                    //no files to copy : error()
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
                // Send Validation Message
                executeStrategyView.DisplayExistingData(Singleton_Lang.ReadFile().Validation);
            }
            else
            {
                //no refferenced type of save : error()
            }

            LogFile lf = LogFile.GetInstance;
            lf.WriteLogMessage(name, source, destination, nbfile, 2);
        }

        public void CollectExistingData()
        {

        }

        public void InitView()
        {
            executeStrategyView.DisplayExistingData(Singleton_Lang.ReadFile().Execute);
            this.value_enter = executeStrategyView.CollectOptions();
            CheckRequirements();
        }

    }
}