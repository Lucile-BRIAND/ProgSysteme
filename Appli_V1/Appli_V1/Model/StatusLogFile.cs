using System;
using System.IO;
using Newtonsoft.Json;


namespace Appli_V1.Controllers
{
    class StatusLogFile //: FileAbstractClass
    {
        //THIS CLASS IS A SINGLETON
        //
        //Private attributes
        private StreamWriter file;
        private static StatusLogFile statusInstance = null;

        //The private constructor is only accessible from this class
        private StatusLogFile()
        { }

        //The unique function to use theStatusLogFile object
        public static StatusLogFile GetInstance
        {
            get
            {
                if (statusInstance == null)
                {
                    statusInstance = new StatusLogFile();
                }
                return statusInstance;
            }
        }

        //Writing content in the log file
        public void WriteStatusLogMessage(string jobName, string jobType, string sourcePath, string targetPath, string State, int TotalFilesToCopy, int TotalFilesSize, int NbFilesLeftToDo)
        {
            //Adding values to the json keys
            var jsonData = new
            {
                Name = jobName,
                Type = jobType,
                sourcePath = sourcePath,
                targetPath = targetPath,
                state = State,
                totalFilesToCopy = TotalFilesToCopy,
                totalFilesSize = TotalFilesSize,
                nbFilesLeftToDo = NbFilesLeftToDo,
            };

            //Reserializing the json file and writing
            string json = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
            json += "\n";
            //File.WriteAllText("StatusLogFile.json", json); //creates the file if it doesn't exist + overwrite all the text in it
            File.AppendAllText("StatusLogFile.json", json); //creates the file if it doesn't exist + appends text in it

        }

    }

}