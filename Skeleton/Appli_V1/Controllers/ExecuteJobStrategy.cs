using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace Appli_V1.Controllers
{
    class ExecuteJobStrategy //: IStrategyController
    {
        // Initialize private class attribute
        private int language;
        private string[] jobRequirements;
        private string source;
        private string destination;
        private string name;
        private string type;
        private string value_enter;

        // The ExecuteJobStrategy class uses specific View class to interact properly with the user
        // Here we instanciate the View Class object so that we could later call the appropriate method
        ExecuteStrategyView executeStrategyView = new ExecuteStrategyView();

        // To match with the user's demand, we collect his chosen language
        LanguageFile Singleton_Lang = LanguageFile.GetInstance;

        // Instanciate object that we need to use below
        jobModel jobmodel = new jobModel();
        ExistingJob existingJob = new ExistingJob();

        // /!\ VERIFY THE USE OF THIS METHOD BELOW !
        public ExecuteJobStrategy()
        {

        }

        public void CheckRequirements()
        {
            // In order to execute a backup / job task we need to consider existing job that has already been created
            var contentFile = System.IO.File.ReadAllText(existingJob.file);
            var jobModelList = JsonConvert.DeserializeObject<List<jobModel>>(contentFile);
            foreach (jobModel attribute in jobModelList)
            {
                // Here we browse existingJob JSON file to pick up every data corresponding to the user's selected job
                if (attribute.jobName == this.value_enter)
                {
                    name = attribute.jobName;
                    type = attribute.jobType;
                    source = attribute.sourcePath;
                    destination = attribute.targetPath;
                }
            }

            int nbfile = 0; // Number of files that have been copied
            StatusLogFile file1 = StatusLogFile.GetInstance;

            // Here we will chose the right loop, corresponding to the backup's type (complete) and execute the task
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
            // Here we will chose the right loop, corresponding to the backup's type (differential) and execute the task
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
                // If there's no refferenced type of save : we display an error message
            }

            LogFile lf = LogFile.GetInstance;
            lf.WriteLogMessage(name, source, destination, nbfile, 2);
        }

        // /!\ VERIFY THE USE OF THIS METHOD BELOW !
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