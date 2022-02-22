using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using AppV3.Models;
using System.Diagnostics;
using System.Threading;

namespace AppV3.VM
{
    class ExecuteJobVM
    {
        public string executeBackup { get; set; }
        public string mainMenu { get; set; }
        public string JobSoftwareLabel { get; set; }
        public string executeAllJobsButton { get; set; }

        FileExtentions fileExtentions = FileExtentions.GetInstance;
        FileSize fileSize = FileSize.GetInstance;
        StatusLogFile slf = StatusLogFile.GetInstance;

        public static int timeCryptoSoft;
        public static int timeExecuteBackup;
        public string JobSoftwareNameTextBox;
        private string format;

        private static object _lock;

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
        public void InitJobSoftwareName(string jobSoftware)
        {
            JobSoftwareNameTextBox = jobSoftware;
            Trace.WriteLine(JobSoftwareNameTextBox);
            lf.InitJobSoftware(this.JobSoftwareNameTextBox);
        }
        public void InitFormat(string Format)
        {
            this.format = Format;
            Trace.WriteLine(format);
            lf.InitFormat(this.format);
        }
        public void CallCryptoSoft(string path, int startCryptTime)
        {
            Process P = new Process();
            P.StartInfo.FileName = "D:/Documents/CESI/FISA A3 21-22/3 - PROG SYSTEME/Projet/ProgSysteme/CryptoSoft/CryptoSoft/bin/Debug/netcoreapp3.1/CryptoSoft.exe";
            P.StartInfo.Arguments = path;

            if (fileExtentions.extentions.Count != 0)
            {
                for (int i = 0; i < fileExtentions.extentions.Count; i++)
                {
                    P.StartInfo.Arguments += " " + fileExtentions.extentions[i];
                }
            }

            Trace.WriteLine("Args : " + P.StartInfo.Arguments);
            P.Start();

            timeCryptoSoft += DateTime.Now.Millisecond - startCryptTime;
        }

        public void ExecuteBackup(string name, string type, string source, string destination)
        {
            _lock = new object();

            this.format = lf.GetFormat();
            Trace.WriteLine(this.format);

            this.JobSoftwareNameTextBox = lf.GetJobSoftawre();
            Trace.WriteLine(this.JobSoftwareNameTextBox);
            if (this.format == null | JobSoftwareNameTextBox == null)
            {
                this.format = "json";
                this.JobSoftwareNameTextBox = "";
            }
            Trace.WriteLine(format);
            Process[] myProcesses = Process.GetProcessesByName(JobSoftwareNameTextBox);

            if (myProcesses.Length != 0)
            {
                return;
            }
            int startTranferTime = DateTime.Now.Millisecond;

            int nbfile = 0; //number of files that have been copied
            long totalfileSize = 0;
            long fileSizeLeftToCopy = 0;
            if (type == "Complete" | type == "Complète")
            {
                int totalNbFileComplete = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories).Length; //total number of files in the save

                foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
                {
                    FileInfo doc = new FileInfo(newPath);
                    totalfileSize += doc.Length;
                }
                //Appends the text in the status log file  => state 0 : initialization
                slf.WriteStatusLogMessage(name, type, source, destination, "STARTING", totalNbFileComplete, totalfileSize, totalNbFileComplete - nbfile, totalfileSize - fileSizeLeftToCopy, format);

                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(source, destination));
                }

                //Copies all the files & replaces any file with the same name
                foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
                {
                    FileInfo doc = new FileInfo(newPath);
                    if(doc.Length <= fileSize.FileMaxSize) //If the file size is under the maximum set size
                    {
                        fileSizeLeftToCopy += doc.Length;
                        nbfile++;

                        CompleteCopyMethod(newPath, source, destination, name, type, totalNbFileComplete, totalfileSize, nbfile, fileSizeLeftToCopy);
                    }

                    else if (doc.Length > fileSize.FileMaxSize && !fileSize.FileIsTransfering) //If the file is bigger but no other big file is currently transfering
                    {
                        lock (_lock) //LOCK: set the transfer status as occupied
                        {
                            fileSize.FileIsTransfering = true;
                        }

                        Thread.Sleep(5000); //DEBUG
                        fileSizeLeftToCopy += doc.Length;
                        nbfile++;

                        CompleteCopyMethod(newPath, source, destination, name, type, totalNbFileComplete, totalfileSize, nbfile, fileSizeLeftToCopy);

                        lock (_lock) //LOCK: reset the transfer status as free to use
                        {
                            fileSize.FileIsTransfering = false;
                        }
                    }

                    else if (doc.Length > fileSize.FileMaxSize && fileSize.FileIsTransfering) //If the file is bigger and another big file is currently tranfering
                    {
                        while (fileSize.FileIsTransfering) //wait until the transfer status is free
                        {
                            Thread.Sleep(20);
                        }

                        lock (_lock) //LOCK: set the transfer status as occupied
                        {
                            fileSize.FileIsTransfering = true;
                        }

                        fileSizeLeftToCopy += doc.Length;
                        nbfile++;

                        CompleteCopyMethod(newPath, source, destination, name, type, totalNbFileComplete, totalfileSize, nbfile, fileSizeLeftToCopy);

                        lock (_lock) //LOCK: reset the transfer status as free to use
                        {
                            fileSize.FileIsTransfering = false;
                        }
                    }
                    
                }

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

                    if (destFile.Exists) //If the file already exists in the destination
                    {
                        if (originalFile.Length > destFile.Length) //If the source file is different from the destination file
                        {
                            if (originalFile.Length <= fileSize.FileMaxSize) //If the file size is under the maximum set size
                            {
                                nbfile++;
                                fileSizeLeftToCopy += originalFile.Length;

                                DifferentialCopyMethod(originalFile, destFile, name, type, source, destination, totalNbFileDifferential, totalfileSize, nbfile, fileSizeLeftToCopy);

                            }

                            else if (originalFile.Length > fileSize.FileMaxSize && !fileSize.FileIsTransfering) //If the file is bigger but no other big file is currently transfering
                            {
                                lock (_lock) //LOCK: set the transfer status as occupied
                                {
                                    fileSize.FileIsTransfering = true;
                                }

                                nbfile++;
                                fileSizeLeftToCopy += originalFile.Length;

                                DifferentialCopyMethod(originalFile, destFile, name, type, source, destination, totalNbFileDifferential, totalfileSize, nbfile, fileSizeLeftToCopy);


                                lock (_lock) //LOCK: reset the transfer status as free to use
                                {
                                    fileSize.FileIsTransfering = false;
                                }
                            }

                            else if (originalFile.Length > fileSize.FileMaxSize && fileSize.FileIsTransfering) //If the file is bigger and another big file is currently tranfering
                            {
                                while (fileSize.FileIsTransfering) //wait until the transfer status is free
                                {
                                    Thread.Sleep(20);
                                }

                                lock (_lock) //LOCK: set the transfer status as occupied
                                {
                                    fileSize.FileIsTransfering = true;
                                }

                                nbfile++;
                                fileSizeLeftToCopy += originalFile.Length;

                                DifferentialCopyMethod(originalFile, destFile, name, type, source, destination, totalNbFileDifferential, totalfileSize, nbfile, fileSizeLeftToCopy);


                                lock (_lock) //LOCK: reset the transfer status as free to use
                                {
                                    fileSize.FileIsTransfering = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        //Creates the associate directory
                        Directory.CreateDirectory(destFile.DirectoryName);

                        if (originalFile.Length <= fileSize.FileMaxSize) //If the file size is under the maximum set size
                        {
                            nbfile++;
                            fileSizeLeftToCopy += originalFile.Length;

                            DifferentialCopyMethod(originalFile, destFile, name, type, source, destination, totalNbFileDifferential, totalfileSize, nbfile, fileSizeLeftToCopy);


                        }

                        else if (originalFile.Length > fileSize.FileMaxSize && !fileSize.FileIsTransfering) //If the file is bigger but no other big file is currently transfering
                        {
                            lock (_lock) //LOCK: set the transfer status as occupied
                            {
                                fileSize.FileIsTransfering = true;
                            }

                            Thread.Sleep(5000); //DEBUG
                            nbfile++;
                            fileSizeLeftToCopy += originalFile.Length;

                            DifferentialCopyMethod(originalFile, destFile, name, type, source, destination, totalNbFileDifferential, totalfileSize, nbfile, fileSizeLeftToCopy);


                            lock (_lock) //LOCK: reset the transfer status as free to use
                            {
                                fileSize.FileIsTransfering = false;
                            }
                        }

                        else if (originalFile.Length > fileSize.FileMaxSize && fileSize.FileIsTransfering) //If the file is bigger and another big file is currently tranfering
                        {
                            while (fileSize.FileIsTransfering) //wait until the transfer status is free
                            {
                                Thread.Sleep(20);
                            }

                            lock (_lock) //LOCK: set the transfer status as occupied
                            {
                                fileSize.FileIsTransfering = true;
                            }

                            nbfile++;
                            fileSizeLeftToCopy += originalFile.Length;

                            DifferentialCopyMethod(originalFile, destFile, name, type, source, destination, totalNbFileDifferential, totalfileSize, nbfile, fileSizeLeftToCopy);


                            lock (_lock) //LOCK: reset the transfer status as free to use
                            {
                                fileSize.FileIsTransfering = false;
                            }
                        }
                    }
                });

                CallCryptoSoft(destination, DateTime.Now.Millisecond);
                timeExecuteBackup += DateTime.Now.Millisecond - startTranferTime;
                lf.WriteLogMessage(name, source, destination, nbfile, totalfileSize, timeCryptoSoft, timeExecuteBackup, format);
            }

        }
        public void ExecutAllBackup()
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
                ExecuteBackup(name, type, source, destination);
            }
        }

        public void CompleteCopyMethod(string newPath, string source, string destination, string name, string type, int totalNbFileComplete, long totalfileSize, int nbfile, long fileSizeLeftToCopy)
        {
            File.Copy(newPath, newPath.Replace(source, destination), true);
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

        public void DifferentialCopyMethod(FileInfo originalFile, FileInfo destFile, string name, string type, string source, string destination, int totalNbFileDifferential, long totalfileSize, int nbfile, long fileSizeLeftToCopy)
        {
            //Copies the file
            originalFile.CopyTo(destFile.FullName, true);

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

        public void GetFileExtentions(bool valueTXT, bool valuePDF, bool valueJPG, bool valuePNG)
        {
            Trace.WriteLine("-------------------VALUES : " + valueTXT + valuePDF + valueJPG + valuePNG);

            fileExtentions.extentions.Clear(); //Clear the previous list of extentions

            if (valueTXT) //if true
            {
                fileExtentions.extentions.Add("*.txt");
                fileExtentions.TXTvalue = true;
            }
            else
            {
                fileExtentions.TXTvalue = false;
            }

            if (valuePDF) //if true
            {
                fileExtentions.extentions.Add("*.pdf");
                fileExtentions.PDFvalue = true;
            }
            else
            {
                fileExtentions.PDFvalue = false;
            }

            if (valueJPG) //if true
            {
                fileExtentions.extentions.Add("*.jpg");
                fileExtentions.JPGvalue = true;
            }
            else
            {
                fileExtentions.JPGvalue = false;
            }

            if (valuePNG) //if true
            {
                fileExtentions.extentions.Add("*.png");
                fileExtentions.PNGvalue = true;
            }
            else
            {
                fileExtentions.PNGvalue = false;
            }

        }
    }
}
