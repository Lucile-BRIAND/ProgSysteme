using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;




namespace Appli_V1.Controllers
{
    class StatusLogFile : FileAbstractClass
    {
        private StreamWriter file;
        private static List<StatusLogFile> statusInstance = null;

        // new string
        private string mFileName;

        //the private constructor is only accessible from this class
        private StatusLogFile()
        {}

        //The unique function to use theStatusLogFile object
        public static StatusLogFile GetInstance
        {
            
            get {
                if (statusInstance == null)
                {
                    statusInstance = new List<StatusLogFile>();

                }
                return statusInstance;
            }
        }

        public void WriteStatusLogMessage( string jobName, string jobType, string sourcePath, string targetPath, string State, int TotalFilesToCopy, int TotalFilesSize, int NbFilesLeftToDo)
        {
            statusInstance.GetInstance;
            statusInstance.Add(new StatusLogFile()
            {
                Name = jobName,
                Type = jobType,
                sourcePath = sourcePath,
                targetPath = targetPath,
                state = State,
                totalFilesToCopy = TotalFilesToCopy,
                totalFilesSize = TotalFilesSize,
                nbFilesLeftToDo = NbFilesLeftToDo,
            });

            string json = JsonSerializer.Serialize(statusInstance);
            File.WriteAllText("StatusLogFile.json", json);

        }

        }

    }
}
