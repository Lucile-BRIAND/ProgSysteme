using System;
using System.IO;
using System.Collections.Generic;
using AppV3.Models;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace AppV3.VM
{
    class ExecuteJobVM
    {
        public string executeBackup { get; set; }
        public string mainMenu { get; set; }
        public string JobSoftwareLabel { get; set; }
        public string executeAllJobsButton { get; set; }
        public string showBar { get; set; }
        public string acceptRequest { get; set; }
        public string resumeBackup { get; set; }
        public string stopBackup { get; set; }
        public string pauseBackup { get; set; }

        //Singletons instances
        public FileExtentions fileExtentions = FileExtentions.GetInstance;
        public FileSize fileSize = FileSize.GetInstance;
        public StatusLogFile slf = StatusLogFile.GetInstance;
        public LanguageFile singletonLang = LanguageFile.GetInstance;
        public LogFile lf = LogFile.GetInstance;
        public SocketManager socketManage = SocketManager.GetInstance;
        public SocketManagerBackupsName socketManageBackupsName = SocketManagerBackupsName.GetInstance;
        public JobProgressPercentage jobProgressPercentageInstance = JobProgressPercentage.GetInstance;

        public string JobSoftwareNameTextBox;
        private string format;
        public int indexPauseBackup;
        public int indexStopBackup;

        private static object _lock;

        
        public ExecuteJobVM getValues()
        {
            //Display the messages and elements in the correct language
            var values = new ExecuteJobVM()
            {
                executeBackup = singletonLang.ReadFile().Execute,
                mainMenu = singletonLang.ReadFile().MainReturn,
                JobSoftwareLabel = singletonLang.ReadFile().JobSoftware,
                executeAllJobsButton = singletonLang.ReadFile().Validation,
                showBar = singletonLang.ReadFile().showBar,
                acceptRequest = singletonLang.ReadFile().acceptRequest,
                resumeBackup = singletonLang.ReadFile().resumeBackup,
                stopBackup = singletonLang.ReadFile().stopBackup,
                pauseBackup = singletonLang.ReadFile().pauseBackup
            };

            return values;

        }
        public void InitJobSoftwareName(string jobSoftware)
        {
            //Initialize the selected job software
            JobSoftwareNameTextBox = jobSoftware;
            lf.InitJobSoftware(this.JobSoftwareNameTextBox);
        }
        public void InitJobPauseName(int PID, int index)
        {
            //Initialize the ID of the job in order to pause it
            lf.InitJobPauseSoftware(PID);
            indexPauseBackup = index;

        }
        public void InitJobStopName(int PID, int index)
        {
            //Initialize the ID of the job in order to stop it
            lf.InitJobStopSoftware(PID);
            indexStopBackup = index;

        }
        public void InitFormat(string Format)
        {
            //Initialize the selected log files format
            format = Format;
            lf.InitFormat(format);
        }
        public int CallCryptoSoft(string path, int startCryptTime)
        {
            //Call the external application CryptoSoft

            int timeCryptoSoft = 0; //total time of the encryption

            Process P = new Process();
            P.StartInfo.FileName = @"C:\Users\danyk\Documents\CESI\PROSIT\PROG SYS\Version3\VERSION3\CryptoSoft\CryptoSoft\bin\Debug\netcoreapp3.1\CryptoSoft";
            P.StartInfo.Arguments = path;

            if (fileExtentions.extentions.Count != 0)
            {
                for (int i = 0; i < fileExtentions.extentions.Count; i++)
                {
                    P.StartInfo.Arguments += " " + fileExtentions.extentions[i];
                }
            }
            //Trace.WriteLine("Args : " + P.StartInfo.Arguments);
            P.Start();
            timeCryptoSoft += DateTime.Now.Millisecond - startCryptTime;

            return timeCryptoSoft;
        }

        public void ExecuteBackup(string name, string type, string source, string destination, int index)
        {
            //Call the backups in order to execute them

            Byte[] buffer;
            buffer = Encoding.UTF8.GetBytes(name + " " + "index n°" + index);
            socketManageBackupsName.socket.Send(buffer);
           
            _lock = new object(); //object used to lock critical sections of code

            format = lf.GetFormat();

            JobSoftwareNameTextBox = lf.GetJobSoftware();
            if (format == null | JobSoftwareNameTextBox == null)
            {
                format = "json";
                JobSoftwareNameTextBox = "";
            }
            Process[] myProcesses = Process.GetProcessesByName(JobSoftwareNameTextBox);
            if (myProcesses.Length != 0)
            {
                return;
            }

            int startTranferTime = DateTime.Now.Millisecond; //timestamp for the start of the execution
            int nbfile = 0; //number of files that have been copied
            long totalfileSize = 0; //total size of the backup in Bytes
            long fileSizeLeftToCopy = 0; //size of the files that are waiting to be copied in Bytes
            int timeExecuteBackup = 0; //total time of the backup execution

            //Declare the list of prioritary/non-prioritary files
            List<string> PrioritaryListComplete = new List<string>();
            List<string> NonPrioritaryListComplete = new List<string>();

            //Make sure that the list of prioritary/non-prioritary files are empty
            PrioritaryListComplete.Clear();
            NonPrioritaryListComplete.Clear();

            //-----------------------
            //COMPLETE BACKUP
            if (type == "Complete" | type == "Complète")
            {
                int totalNbFileComplete = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories).Length; //total number of files in the save

                foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
                {
                    FileInfo doc = new FileInfo(newPath);
                    totalfileSize += doc.Length;

                    //Define if the files are prioritary or not
                    if (fileExtentions.extentionToPrioritize.Contains(doc.Extension))
                    {
                        PrioritaryListComplete.Add(newPath);
                    }
                    else if (!fileExtentions.extentionToPrioritize.Contains(doc.Extension))
                    {
                        NonPrioritaryListComplete.Add(newPath);
                    }
                }

                //Appends the text in the status log file  => state 0 : initialization
                slf.WriteStatusLogMessage(name, type, source, destination, "STARTING", totalNbFileComplete, totalfileSize, totalNbFileComplete - nbfile, totalfileSize - fileSizeLeftToCopy, format);

                //Now create all the directories
                foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(source, destination));
                }

                //Call copy method and the associated calls
                //      Prioritary files first:
                List<long> valuesList = new List<long>();
                valuesList = CallCompleteCopy(PrioritaryListComplete, source, destination, name, type, totalNbFileComplete, totalfileSize, nbfile, fileSizeLeftToCopy, index);
                nbfile = (int)valuesList[0]; //parameter actualization
                fileSizeLeftToCopy = valuesList[1]; //parameter actualization
                valuesList.Clear();

                Thread.Sleep(2000); //DEBUG: helps to see the difference between prioritary files and other files

                //      Then the non-prioritary files:
                valuesList = CallCompleteCopy(NonPrioritaryListComplete, source, destination, name, type, totalNbFileComplete, totalfileSize, nbfile, fileSizeLeftToCopy, index);
                nbfile += (int)valuesList[0]; //parameter actualization
                fileSizeLeftToCopy = valuesList[1]; //parameter actualization
                valuesList.Clear();

               
                //Check if files need to be encrypted
                int timeCryptoSoft = CallCryptoSoft(destination, DateTime.Now.Millisecond);
                timeExecuteBackup += DateTime.Now.Millisecond - startTranferTime;
                //Write into the DailyLog file
                lf.WriteLogMessage(name, source, destination, nbfile, totalfileSize, timeCryptoSoft, timeExecuteBackup, format);

            }

            //-----------------------
            //DIFFERENTIAL BACKUP
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
                        if (originalFile.Length != destFile.Length) //If the existing file in the destination is different from the original file
                        {
                            totalfileSize += originalFile.Length;
                            totalNbFileDifferential++;

                            //Define if the file is prioritary or not
                            if (fileExtentions.extentionToPrioritize.Contains(originalFile.Extension))
                            {
                                PrioritaryListComplete.Add(originalFileLocation);
                            }
                            else if (!fileExtentions.extentionToPrioritize.Contains(originalFile.Extension))
                            {
                                NonPrioritaryListComplete.Add(originalFileLocation);
                            }
                        }
                    }
                    else
                    {
                        totalfileSize += originalFile.Length;
                        totalNbFileDifferential++;

                        //Define if the file is prioritary or not
                        if (fileExtentions.extentionToPrioritize.Contains(originalFile.Extension))
                        {
                            PrioritaryListComplete.Add(originalFileLocation);
                        }
                        else if (!fileExtentions.extentionToPrioritize.Contains(originalFile.Extension))
                        {
                            NonPrioritaryListComplete.Add(originalFileLocation);
                        }
                    }
                });

                //Convert the files lists into files arrays
                string[] PrioritaryArrayDifferential = PrioritaryListComplete.ToArray();
                string[] NonPrioritaryArrayDifferential = NonPrioritaryListComplete.ToArray();

                //Appends the text in the status log file => state 0 : initialization
                if (totalNbFileDifferential != 0)
                {
                    slf.WriteStatusLogMessage(name, type, source, destination, "STARTING", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy, format);
                }

                //Call copy method and the associated calls
                //      Prioritary files first:
                List<long> valuesList = new List<long>();
                valuesList = CallDifferentialCopy(PrioritaryArrayDifferential, name, type, source, destination, totalNbFileDifferential, totalfileSize, nbfile, fileSizeLeftToCopy, index);
                nbfile = (int)valuesList[0]; //parameter actualization
                fileSizeLeftToCopy = valuesList[1]; //parameter actualization
                valuesList.Clear();

                Thread.Sleep(2000); //DEBUG: helps to see the difference between prioritary files and other files

                //      Then the non-prioritary files:
                valuesList = CallDifferentialCopy(NonPrioritaryArrayDifferential, name, type, source, destination, totalNbFileDifferential, totalfileSize, nbfile, fileSizeLeftToCopy, index);
                nbfile = (int)valuesList[0]; //parameter actualization
                fileSizeLeftToCopy = valuesList[1]; //parameter actualization
                valuesList.Clear();

                //Check if files need to be encrypted
                int timeCryptoSoft = CallCryptoSoft(destination, DateTime.Now.Millisecond);
                timeExecuteBackup += DateTime.Now.Millisecond - startTranferTime;
                //Write into the DailyLog file
                lf.WriteLogMessage(name, source, destination, nbfile, totalfileSize, timeCryptoSoft, timeExecuteBackup, format);

            }

        }

        public List<long> CallCompleteCopy(List<string> filesList, string source, string destination, string name, string type, int totalNbFileComplete, long totalfileSize, int nbfile, long fileSizeLeftToCopy, int index)
        {
            //Call the complete copy method according to conditions

            jobProgressPercentageInstance.percentage = 0;
            Byte[] buffer;
            //Copy all the files & replaces any file with the same name
            foreach (string newPath in filesList)
            {                
                jobProgressPercentageInstance.percentage = ((100 * fileSizeLeftToCopy) / totalfileSize);
                jobProgressPercentageInstance.percentage = Math.Round(jobProgressPercentageInstance.percentage);
                Trace.WriteLine(jobProgressPercentageInstance.percentage);
                buffer = Encoding.UTF8.GetBytes(jobProgressPercentageInstance.percentage.ToString());
                socketManage.socket.Send(buffer);

                Thread.Sleep(2000);
                int PIDPause = lf.GetJobPauseSoftware();
                int PIDStop = lf.GetJobStopSoftware();
                //PAUSE
                try
                {
                    Process myProcess = Process.GetProcessById(PIDPause);
                    Trace.WriteLine(PIDPause);
                    if (myProcess != null && indexPauseBackup == index)
                    {
                        myProcess.WaitForExit();
                    }
                }
                catch
                {

                }
                //STOP
                try
                {
                    Process myProcess2 = Process.GetProcessById(PIDStop);
                    Trace.WriteLine(PIDStop);
                    if (myProcess2 != null && indexStopBackup == index)
                    {
                        myProcess2.Kill();
                        break;
                    }
                }
                catch
                {

                }
                Process[] myProcesses = Process.GetProcessesByName(lf.GetJobSoftware());
                Trace.WriteLine("test" + lf.GetJobSoftware() + " " + myProcesses.Length);
                if (myProcesses.Length != 0)
                {
                    myProcesses[0].WaitForExit();
                }

                FileInfo doc = new FileInfo(newPath);
                if (doc.Length <= fileSize.FileMaxSize) //If the file size is under the maximum set size
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

                    Thread.Sleep(2000); //DEBUG
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
            jobProgressPercentageInstance.percentage = ((100 * fileSizeLeftToCopy) / totalfileSize);
            jobProgressPercentageInstance.percentage = Math.Round(jobProgressPercentageInstance.percentage);
            Trace.WriteLine(jobProgressPercentageInstance.percentage);
            buffer = Encoding.UTF8.GetBytes(jobProgressPercentageInstance.percentage.ToString());
            socketManage.socket.Send(buffer);

            //Add the necessary values in a list
            List<long> valuesList = new List<long>
            {
                nbfile,
                fileSizeLeftToCopy
            };

            return valuesList;
        }

        public List<long> CallDifferentialCopy(string[] filesArray, string name, string type, string source, string destination, int totalNbFileDifferential, long totalfileSize, int nbfile, long fileSizeLeftToCopy, int index)
        {
            //Call the differential copy method according to conditions

            //FOREACH : copy the files
            Array.ForEach(filesArray, (originalFileLocation) =>
            {
                Thread.Sleep(2000);
                int PIDPause = lf.GetJobPauseSoftware();
                int PIDStop = lf.GetJobStopSoftware();
                try
                {
                    Process myProcess = Process.GetProcessById(PIDPause);
                    Trace.WriteLine(PIDPause);
                    if (myProcess != null && indexPauseBackup == index)
                    {
                        myProcess.WaitForExit();
                    }
                }
                catch
                {

                }
                //STOP
                try
                {
                    Process myProcess2 = Process.GetProcessById(PIDStop);
                    Trace.WriteLine(PIDStop);
                    if (myProcess2 != null && indexStopBackup == index)
                    {
                        myProcess2.Kill();
                        return;
                    }
                }
                catch
                {

                }
                Process[] myProcesses = Process.GetProcessesByName(lf.GetJobSoftware());
                Trace.WriteLine("test" + lf.GetJobSoftware() + " " + myProcesses.Length);
                if (myProcesses.Length != 0)
                {
                    myProcesses[0].WaitForExit();
                }

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

                        Thread.Sleep(2000); //DEBUG
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

            //Add the necessary values in a list
            List<long> valuesList = new List<long>
            {
                nbfile,
                fileSizeLeftToCopy
            };

            return valuesList;
        }

        public void CompleteCopyMethod(string newPath, string source, string destination, string name, string type, int totalNbFileComplete, long totalfileSize, int nbfile, long fileSizeLeftToCopy)
        {
            //Copy the file in complete mode

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
            //Copy the file in differential mode

            //Copy the file
            originalFile.CopyTo(destFile.FullName, true);

            //Append the text in the status log file
            if (totalfileSize - fileSizeLeftToCopy == 0)
            {
                slf.WriteStatusLogMessage(name, type, source, destination, "END", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy, format);
            }
            else
            {
                //Append the text in the status log file
                slf.WriteStatusLogMessage(name, type, source, destination, "ACTIVE", totalNbFileDifferential, totalfileSize, totalNbFileDifferential - nbfile, totalfileSize - fileSizeLeftToCopy, format);
            }
        }

        public void GetExtentionsToEncrypt(bool valueTXT, bool valuePDF, bool valueJPG, bool valuePNG)
        {
            //Get the files' extentions to encrypt with the checkboxes

            //Trace.WriteLine("-------------------VALUES : " + valueTXT + valuePDF + valueJPG + valuePNG); //DEBUG

            fileExtentions.extentions.Clear(); //Clear the previous list of extentions

            if (valueTXT) //if checked
            {
                fileExtentions.extentions.Add("*.txt");
                fileExtentions.Encrypt_TXTValue = true;
            }
            else
            {
                fileExtentions.Encrypt_TXTValue = false;
            }

            if (valuePDF) //if checked
            {
                fileExtentions.extentions.Add("*.pdf");
                fileExtentions.Encrypt_PDFValue = true;
            }
            else
            {
                fileExtentions.Encrypt_PDFValue = false;
            }

            if (valueJPG)  //if checked
            {
                fileExtentions.extentions.Add("*.jpg");
                fileExtentions.Encrypt_JPGValue = true;
            }
            else
            {
                fileExtentions.Encrypt_JPGValue = false;
            }

            if (valuePNG)  //if checked
            {
                fileExtentions.extentions.Add("*.png");
                fileExtentions.Encrypt_PNGValue = true;
            }
            else
            {
                fileExtentions.Encrypt_PNGValue = false;
            }

        }

        public void GetExtentionsToPrioritize(bool valueTXT, bool valuePDF, bool valueJPG, bool valuePNG)
        {
            //Get the files' extentions to prioritize with the checkboxes

            //Trace.WriteLine("-------------------VALUES : " + valueTXT + valuePDF + valueJPG + valuePNG); //DEBUG

            fileExtentions.extentionToPrioritize.Clear(); //Clear the previous list of extentions

            if (valueTXT)  //if checked
            {
                fileExtentions.extentionToPrioritize.Add(".txt");
                fileExtentions.Priority_TXTValue = true;
            }
            else
            {
                fileExtentions.Priority_TXTValue = false;
            }

            if (valuePDF)  //if checked
            {
                fileExtentions.extentionToPrioritize.Add(".pdf");
                fileExtentions.Priority_PDFValue = true;
            }
            else
            {
                fileExtentions.Priority_PDFValue = false;
            }

            if (valueJPG)  //if checked
            {
                fileExtentions.extentionToPrioritize.Add(".jpg");
                fileExtentions.Priority_JPGValue = true;
            }
            else
            {
                fileExtentions.Priority_JPGValue = false;
            }

            if (valuePNG)  //if checked
            {
                fileExtentions.extentionToPrioritize.Add(".png");
                fileExtentions.Priority_PNGValue = true;
            }
            else
            {
                fileExtentions.Priority_PNGValue = false;
            }
        }
    }
}
