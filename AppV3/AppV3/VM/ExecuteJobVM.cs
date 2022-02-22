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


namespace AppV3.VM
{
    class ExecuteJobVM
    {
        //public string jobSoftwareName;
        public string executeBackup { get; set; }

        public string mainMenu { get; set; }
        public string JobSoftwareLabel { get; set; }
        public string executeAllJobsButton { get; set; }

        FileExtentions fileExtentions = FileExtentions.GetInstance;
        FileSize fileSize = FileSize.GetInstance;

        public static int timeCryptoSoft;
        public static int timeExecuteBackup;
        public string JobSoftwareNameTextBox;
        private string format;

        private static object _lock;

        public Semaphore semaphore = new Semaphore(1, 1);
        List<string> PrioritaryList = new List<string>();
        List<string> NonPriorityList = new List<string>();


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
            P.StartInfo.FileName = "C:/Users/Bruno/source/repos/CryptoSoft/CryptoSoft/bin/Debug/netcoreapp3.1/CryptoSoft";
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
        public void DefinePriority(JobModel source)
        {
            foreach (string newPath in Directory.GetFiles(source.sourcePath, "*.*", SearchOption.AllDirectories))
            {
                FileInfo doc = new FileInfo(newPath);
                //Trace.WriteLine(doc);
            }
        }
        public void CopyCompelte(string newPath, string source, string destination)
        {
            semaphore.WaitOne();
            File.Copy(newPath, newPath.Replace(source, destination), true);
            semaphore.Release();
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
            //Thread.Sleep(10000);
            Process[] myProcesses = Process.GetProcessesByName(JobSoftwareNameTextBox);

            if (myProcesses.Length != 0)
            {
                return;
            }
            int startTranferTime = DateTime.Now.Millisecond;
            //CallCryptoSoft(source, DateTime.Now.Millisecond);

            int nbfile = 0; //number of files that have been copied
            StatusLogFile slf = StatusLogFile.GetInstance;
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

                    if (fileExtentions.extensionToPrioritize.Contains(doc.Extension))
                    {
                        PrioritaryList.Add(newPath);

                    }
                    else if (!fileExtentions.extensionToPrioritize.Contains(doc.Extension))
                    {
                        NonPriorityList.Add(newPath);
                    }

                    foreach (string newPrioritaryPath in PrioritaryList)
                    {
                        //CopyCompelte(newPrioritaryPath, source, destination);
                    }
                    foreach (string newNonPrioritaryPath in NonPriorityList)
                    {
                        //CopyCompelte(newPrioritaryPath, source, destination);
                    }
                    if (doc.Length <= fileSize.FileMaxSize) //If the file size is under the maximum set size
                    {
                        fileSizeLeftToCopy += doc.Length;

                        nbfile++;


                    

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
                    else if (doc.Length > fileSize.FileMaxSize && !fileSize.FileIsTransfering) //If the file is bigger but no other big file is currently transfering
                    {
                        lock (_lock)
                        {
                            fileSize.FileIsTransfering = true;
                        }

                        Thread.Sleep(10000);
                        fileSizeLeftToCopy += doc.Length;

                        nbfile++;
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

                        lock (_lock)
                        {
                            fileSize.FileIsTransfering = false;
                        }
                    }
                    else if (doc.Length > fileSize.FileMaxSize && fileSize.FileIsTransfering) //If the file is bigger and another big file is currently tranfering
                    {
                        while (fileSize.FileIsTransfering)
                        {
                            Thread.Sleep(20);
                        }

                        lock (_lock)
                        {
                            fileSize.FileIsTransfering = true;
                        }

                        fileSizeLeftToCopy += doc.Length;

                        nbfile++;
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

                        lock (_lock)
                        {
                            fileSize.FileIsTransfering = false;
                        }
                    }
                    
                }
                //CallCryptoSoft(source, DateTime.Now.Millisecond);
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
                        if (originalFile.Length != destFile.Length)
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
                //CallCryptoSoft(source, DateTime.Now.Millisecond);
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
        public void GetFileExtentions(bool valueTXT, bool valuePDF, bool valueJPG, bool valuePNG)
        {
            Trace.WriteLine("-------------------VALUES : " + valueTXT + valuePDF + valueJPG + valuePNG);

            fileExtentions.extentions.Clear();

            if (valueTXT)
            {
                fileExtentions.extentions.Add("*.txt");
                fileExtentions.TXTvalue = true;
            }
            else
            {
                fileExtentions.TXTvalue = false;
            }

            if (valuePDF)
            {
                fileExtentions.extentions.Add("*.pdf");
                fileExtentions.PDFvalue = true;
            }
            else
            {
                fileExtentions.PDFvalue = false;
            }

            if (valueJPG)
            {
                fileExtentions.extentions.Add("*.jpg");
                fileExtentions.JPGvalue = true;
            }
            else
            {
                fileExtentions.JPGvalue = false;
            }

            if (valuePNG)
            {
                fileExtentions.extentions.Add("*.png");
                fileExtentions.PNGvalue = true;
            }
            else
            {
                fileExtentions.PNGvalue = false;
            }

        }
        public void GetExtensionToPrioritize(bool valueTXT, bool valuePDF, bool valueJPG, bool valuePNG)
        {
            Trace.WriteLine("-------------------VALUES : " + valueTXT + valuePDF + valueJPG + valuePNG);

            fileExtentions.extensionToPrioritize.Clear();

            if (valueTXT)
            {
                fileExtentions.extensionToPrioritize.Add(".txt");
                fileExtentions.Priority_TXTValue = true;
            }
            else
            {
                fileExtentions.Priority_TXTValue = false;
            }

            if (valuePDF)
            {
                fileExtentions.extensionToPrioritize.Add(".pdf");
                fileExtentions.Priority_PDFValue = true;
            }
            else
            {
                fileExtentions.Priority_PDFValue = false;
            }

            if (valueJPG)
            {
                fileExtentions.extensionToPrioritize.Add(".jpg");
                fileExtentions.Priority_JPGValue = true;
            }
            else
            {
                fileExtentions.Priority_JPGValue = false;
            }

            if (valuePNG)
            {
                fileExtentions.extensionToPrioritize.Add(".png");
                fileExtentions.Priority_PNGValue = true;
            }
            else
            {
                fileExtentions.Priority_PNGValue = false;
            }

        }
    }
}
