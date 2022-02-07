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
        public string JobSoftwareLabel { get; set; }
        public static byte[] ImageBytes;
        public static int timeCryptoSoft;
        public static int timeExecuteBackup;




        LanguageFile singletonLang = LanguageFile.GetInstance;
        LogFile lf = LogFile.GetInstance;
        public ExecuteJobVM getValues()
        {
            var values = new ExecuteJobVM()
            {
                executeBackup = singletonLang.ReadFile().Execute,
                mainMenu = singletonLang.ReadFile().MainReturn,
                JobSoftwareLabel = singletonLang.ReadFile().JobSoftware
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
        public static void CryptoSoft(string path)
        {
            Int64 key = 0xA9A9;
            // Init the byte containing the file reading

            int startCryptTime = DateTime.Now.Millisecond;
            List<string> files = new List<string>();
            foreach (string newPath in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                files.Add(newPath);
            }
            foreach (string s in files)
            {
                // Read of the initial file
                ImageBytes = File.ReadAllBytes(s);
                // Encryptation or Decyptation

                for (int i = 0; i < ImageBytes.Length; i++)
                {
                    ImageBytes[i] = (byte)(ImageBytes[i] ^ (byte)key);
                }
                // Write of the cryptation and copy
                File.WriteAllBytes(s, ImageBytes);
            }

            timeCryptoSoft += DateTime.Now.Millisecond - startCryptTime;





        }
        public void ExecuteBackup(string name, string type, string source, string destination, string JobSoftwareNameTextBox)
        {
            //Thread.Sleep(10000);
            Process[] myProcesses = Process.GetProcessesByName(JobSoftwareNameTextBox);

            if (myProcesses.Length != 0)
            {
                return;
            }
            int startTranferTime = DateTime.Now.Millisecond;
            CryptoSoft(source);

            int nbfile = 0; //number of files that have been copied
            StatusLogFile slf = StatusLogFile.GetInstance;
            long totalfileSize = 0;
            long fileSizeLeftToCopy = 0;

            if (type == "Complete" | type == "Complète")
            {
                int totalNbFileComplete = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories).Length; //total number of files in the save

                foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
                {
                    totalfileSize += newPath.Length;
                }
                //Appends the text in the status log file  => state 0 : initialization
                slf.WriteStatusLogMessage(name, type, source, destination, "STARTING", totalNbFileComplete, totalfileSize, totalNbFileComplete - nbfile, totalfileSize-fileSizeLeftToCopy);

                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(source, destination));
                }

                //Copies all the files & replaces any file with the same name
                foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
                {
                   
                    fileSizeLeftToCopy += newPath.Length;

                    nbfile++;
                    File.Copy(newPath, newPath.Replace(source, destination), true);
                    if (totalfileSize - fileSizeLeftToCopy == 0)
                    {
                        slf.WriteStatusLogMessage(name, type, source, destination, "END", totalNbFileComplete, totalfileSize, totalNbFileComplete - nbfile, totalfileSize - fileSizeLeftToCopy);
                    }
                    else
                    {
                        //Appends the text in the status log file
                        slf.WriteStatusLogMessage(name, type, source, destination, "ACTIVE", totalNbFileComplete, totalfileSize, totalNbFileComplete - nbfile, totalfileSize - fileSizeLeftToCopy);
                    }

                }
                CryptoSoft(source);
                CryptoSoft(destination);
                timeExecuteBackup += DateTime.Now.Millisecond - startTranferTime;
                lf.WriteLogMessage(name, source, destination, nbfile, totalfileSize, timeCryptoSoft, timeExecuteBackup);

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
                            totalfileSize += originalFile.Length;
                            totalNbFileDifferential++;
                        }
                    }
                    else
                    {
                        totalfileSize += originalFile.Length;
                        totalNbFileDifferential++;
                    }
                });

                //Appends the text in the status log file => state 0 : initialization
                if (totalNbFileDifferential != 0)
                {
                    slf.WriteStatusLogMessage(name, type, source, destination, "STARTING", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy);
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
                            fileSizeLeftToCopy += originalFile.Length;

                            //Appends the text in the status log file
                            if (totalfileSize - fileSizeLeftToCopy == 0)
                            {
                                slf.WriteStatusLogMessage(name, type, source, destination, "END", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy);
                            }
                            else
                            {
                                //Appends the text in the status log file
                                slf.WriteStatusLogMessage(name, type, source, destination, "ACTIVE", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy);
                            }
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(destFile.DirectoryName);
                        originalFile.CopyTo(destFile.FullName, false);
                        nbfile++;
                        fileSizeLeftToCopy += originalFile.Length;

                        //Appends the text in the status log file
                        if (totalfileSize - fileSizeLeftToCopy == 0)
                        {
                            slf.WriteStatusLogMessage(name, type, source, destination, "END", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy);
                        }
                        else
                        {
                            //Appends the text in the status log file
                            slf.WriteStatusLogMessage(name, type, source, destination, "ACTIVE", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy);
                        }
                    }
                });
                CryptoSoft(source);
                CryptoSoft(destination);
                timeExecuteBackup += DateTime.Now.Millisecond - startTranferTime;
                lf.WriteLogMessage(name, source, destination, nbfile, totalfileSize, timeCryptoSoft, timeExecuteBackup);
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
