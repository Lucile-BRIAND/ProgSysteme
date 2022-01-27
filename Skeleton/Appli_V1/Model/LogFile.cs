using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json; //no use for now because the NuGet package is not downloaded

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
                    logInstance = new LogFile(); //creating a list
                }
                return logInstance;
            }

        }

        //Writing content in the log file
        public void WriteLogMessage(string jobName, string sourcePath, string targetPath, int fileSize)
        {
            //Adding values to the json keys
            var jsonData = new
            {
                Name = jobName,
                FileSource = sourcePath,
                FileTarget = targetPath,
                FileSize = fileSize,
                FileTransferTime = 0, //waiting to find the proper method
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") //current timestamp
            };

            //Reserializing the json file and writing
            string json = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
            json += "\n";
            File.AppendAllText("DailyLog.json", json); //creates the file if it doesn't exist, and append text in it
        }

    }
}
