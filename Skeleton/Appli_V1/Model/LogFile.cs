using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Appli_V1.Controllers
{
    class LogFile : FileAbstractClass
    {
        //THIS CLASS IS A SINGLETON
        //
        //Private attributes
        private static List<LogFile> logInstance = null; //default unique instance
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
                    logInstance = new List<LogFile>(); //creating a list
                }
                return logInstance;
            }  
            
        }

        //Writing content in the log file
        public void WriteLogMessage(string jobName, string sourcePath, string targetPath, int fileSize)
        {
            //Adding values to the json keys
            logInstance.Add(new LogFile()
            {
                Name = jobName,
                FileSource = sourcePath,
                FileTarget = targetPath,
                FileSize = fileSize,
                FileTransferTime = null, //waiting to find the proper method
                Date = null //timestamp for the beginning of the job + waiting to find the proper method
            });

            //Reserializing the json file and writing
            string json = JsonSerializer.Serialize(logInstance);
            File.AppendAllText("DailyLog.json", json); //creates the file if it doesn't exist, and append text in it
        }

    }
}
