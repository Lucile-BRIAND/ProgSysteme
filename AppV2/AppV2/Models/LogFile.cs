using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace AppV2.Models
{
    class LogFile
    {
        //THIS CLASS IS A SINGLETON
        //
        //Private attributes
        private static LogFile logInstance = null; //default unique instance

        //Private constructor, only accessible from this class
        private LogFile()
        {

        }

        //Getting the unique instance of this class
        public static LogFile GetInstance
        {
            get
            {
                if (logInstance == null)
                {
                    logInstance = new LogFile();
                }
                return logInstance;
            }

        }

        //Writing content in the log file
        public void WriteLogMessage(string jobName, string sourcePath, string targetPath, int nbFile, long fileSize, int timeCryptoSoft, int timeExecuteBackup)
        {
            string timeCryptoSoftFormated= timeCryptoSoft.ToString() + " ms";
            string timeExecuteBackupFormated = timeExecuteBackup.ToString() + " ms";
            string fileSizeFormated = fileSize.ToString() + " B";
            //Adding values to the json keys
            var dataLog = new
            {
                Name = jobName,
                FileSource = sourcePath,
                FileTarget = targetPath,
                NbFile = nbFile,
                FileSize = fileSizeFormated,
                FileTransferTime = timeExecuteBackupFormated,
                timeCryptoSoft = timeCryptoSoftFormated,
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") //current timestamp in the proper format
            };

            //Reserializing the json file and writing
            string dataLogSerialized = JsonConvert.SerializeObject(dataLog, Formatting.Indented);
            dataLogSerialized += "\n";
            File.AppendAllText("DailyLog.json", dataLogSerialized); //creates the file if it doesn't exist + appends text in it
        }

    }
}
