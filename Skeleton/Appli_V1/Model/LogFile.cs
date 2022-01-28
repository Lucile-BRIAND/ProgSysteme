using System;
using System.IO;
using Newtonsoft.Json;

namespace Appli_V1.Controllers
{
    class LogFile
    {
        //THIS CLASS IS A SINGLETON
        //
        //Private attributes
        private static LogFile logInstance = null; //default unique instance
        private StreamWriter file; //file object we could write in

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
        public void WriteLogMessage(string jobName, string sourcePath, string targetPath, int fileSize, double transferTime)
        {
            //Adding values to the json keys
            var jsonData = new
            {
                Name = jobName,
                FileSource = sourcePath,
                FileTarget = targetPath,
                FileSize = fileSize,
                FileTransferTime = transferTime,
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") //current timestamp in the proper format
            };

            //Reserializing the json file and writing
            string json = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
            json += "\n";
            File.AppendAllText("DailyLog.json", json); //creates the file if it doesn't exist + appends text in it
        }

    }
}