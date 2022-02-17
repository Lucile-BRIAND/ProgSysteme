using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppV3;
using AppV3.Models;
using System.Diagnostics;
using System.Threading;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows; //oo

namespace AppV3.VM
{
    class ExecuteJobVM : Window
    {
        //public string jobSoftwareName;
        public string executeBackup { get; set; }

        public string mainMenu { get; set; }
        public string JobSoftwareLabel { get; set; }
        public string executeAllJobsButton { get; set; }
        public static byte[] ImageBytes;
        public static int timeCryptoSoft;
        public static int timeExecuteBackup;

        public delegate void ThreadDelegate(object jobToExecuteParameter);



        LanguageFile singletonLang = LanguageFile.GetInstance;
        LogFile lf = LogFile.GetInstance;
        public ExecuteJobVM getValues()
        {
            var values = new ExecuteJobVM()
            {
                executeBackup = singletonLang.ReadFile().Execute,
                mainMenu = singletonLang.ReadFile().MainReturn,
                JobSoftwareLabel = singletonLang.ReadFile().JobSoftware,
                executeAllJobsButton = singletonLang.ReadFile().Validation
            };

            return values;

        }
        public static void CallCryptoSoft(string path, int startCryptTime)
        {
            
            Process P = new Process();
            P.StartInfo.FileName = "D:/ProgSystemLocal/CryptoSoft/CryptoSoft/bin/Debug/netcoreapp3.1/CryptoSoft";
            P.StartInfo.Arguments = path;
            //int startCryptTime = DateTime.Now.Millisecond;
            P.Start();
            timeCryptoSoft += DateTime.Now.Millisecond - startCryptTime;
        }

        public void CreateThread(List<Thread> threadList, List<JobModel> jobListToExecute)
        {
            int i = 0;
            foreach(Thread t in threadList)
            {
                Trace.WriteLine("cc fils de chienne" + i);
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        t.Start(jobListToExecute[i]);
                    });
                i++;
            }
            MessageBox.Show("");

        }


        public void ExecuteBackup(string name, string type, string source, string destination, string JobSoftwareNameTextBox, string format)
        {
            //Thread.Sleep(10000);
            Process[] myProcesses = Process.GetProcessesByName(JobSoftwareNameTextBox);

            if (myProcesses.Length != 0)
            {
                return;
            }
            int startTranferTime = DateTime.Now.Millisecond;
            CallCryptoSoft(source, DateTime.Now.Millisecond);

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
                slf.WriteStatusLogMessage(name, type, source, destination, "STARTING", totalNbFileComplete, totalfileSize, totalNbFileComplete - nbfile, totalfileSize-fileSizeLeftToCopy, format);

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
                    Thread.Sleep(1000);
                    if (totalfileSize - fileSizeLeftToCopy == 0)
                    {
                        slf.WriteStatusLogMessage(name, type, source, destination, "END", totalNbFileComplete, totalfileSize, totalNbFileComplete - nbfile, totalfileSize - fileSizeLeftToCopy, format);
                    }
                    else
                    {
                        //Appends the text in the status log file
                        slf.WriteStatusLogMessage(name, type, source, destination, "ACTIVE", totalNbFileComplete, totalfileSize, totalNbFileComplete - nbfile, totalfileSize - fileSizeLeftToCopy, format);
                    }

                }
                CallCryptoSoft(source, DateTime.Now.Millisecond);
                CallCryptoSoft(destination, DateTime.Now.Millisecond);
                timeExecuteBackup += DateTime.Now.Millisecond - startTranferTime;
                lf.WriteLogMessage(name, source, destination, nbfile, totalfileSize, timeCryptoSoft, timeExecuteBackup, format);

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
                    slf.WriteStatusLogMessage(name, type, source, destination, "STARTING", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy, format);
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
                                slf.WriteStatusLogMessage(name, type, source, destination, "END", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy, format);
                            }
                            else
                            {
                                //Appends the text in the status log file
                                slf.WriteStatusLogMessage(name, type, source, destination, "ACTIVE", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy, format);
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
                            slf.WriteStatusLogMessage(name, type, source, destination, "END", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy, format);
                        }
                        else
                        {
                            //Appends the text in the status log file
                            slf.WriteStatusLogMessage(name, type, source, destination, "ACTIVE", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy, format);
                        }
                    }
                });
                CallCryptoSoft(source, DateTime.Now.Millisecond);
                CallCryptoSoft(destination, DateTime.Now.Millisecond);
                timeExecuteBackup += DateTime.Now.Millisecond - startTranferTime;
                lf.WriteLogMessage(name, source, destination, nbfile, totalfileSize, timeCryptoSoft, timeExecuteBackup, format);
            }

        }
        public void ExecutAllBackup(string JobSoftwareNameTextBox, string format)
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
                ExecuteBackup(name, type, source, destination, JobSoftwareNameTextBox, format);
            }
        }
    }
}
