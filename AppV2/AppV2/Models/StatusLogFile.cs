using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace AppV2.Models
{
    class StatusLogFile //: FileAbstractClass
    {
        //THIS CLASS IS A SINGLETON
        //
        //Private attributes
        private static StatusLogFile statusInstance = null;

        //The private constructor is only accessible from this class
        private StatusLogFile()
        {
        }

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
        public void WriteStatusLogMessage(string jobName, string jobType, string sourcePath, string targetPath, string state, int totalFilesToCopy, long totalFilesSize, int nbFilesLeftToDo, long fileSizeLeftToCopy)
        {
            //Adding values to the json keys
            var dataLog = new
            {
                Name = jobName,
                Type = jobType,
                SourcePath = sourcePath,
                TargetPath = targetPath,
                State = state,
                TotalFilesToCopy = totalFilesToCopy,
                TotalFilesSize = totalFilesSize,
                NbFilesLeftToDo = nbFilesLeftToDo,
                FileSizeLeftToCopy = fileSizeLeftToCopy
            };

            //Reserializing the json file and writing
            string dataLogSerialized = JsonConvert.SerializeObject(dataLog, Formatting.Indented);
            dataLogSerialized += "\n";
            //File.WriteAllText("StatusLogFile.json", json); //creates the file if it doesn't exist + overwrite all the text in it
            File.AppendAllText("StatusLogFile.json", dataLogSerialized); //creates the file if it doesn't exist + appends text in it

        }

    }
}
