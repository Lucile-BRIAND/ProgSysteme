using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Appli_V1.Controllers
{
    class ExecuteJobStrategy : IStrategyController
    {
        private int language;
        private string[] jobRequirements;
        private string source;
        private string destination;
        private string name;
        private string type;

        public ExecuteJobStrategy()
        {

        }
        // ExecuteStrategyView executeStrategyView = new ExecuteStrategyView();
        public void CheckRequirements()
        {
            string[] jobRequirements = new string[] {"test", "differential", "C:/Users/danyk/Documents/CESI/PROSIT/PROG SYS/test/Source", "C:/Users/danyk/Documents/CESI/PROSIT/PROG SYS/test/Destination" };

            name = jobRequirements[0];
            type = jobRequirements[1];
            source = jobRequirements[2];
            destination = jobRequirements[3];
            int nbfile = 0;
            if (type == "complete")
            {
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(source, destination));
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
                {
                    nbfile++;
                    File.Copy(newPath, newPath.Replace(source, destination), true);
                }

            }
            else if(type == "differential")
            {
                string[] originalFiles = Directory.GetFiles(source, "*", SearchOption.AllDirectories);

                Array.ForEach(originalFiles, (originalFileLocation) =>
                {
                    FileInfo originalFile = new FileInfo(originalFileLocation);
                    FileInfo destFile = new FileInfo(originalFileLocation.Replace(source, destination));

                    if (destFile.Exists)
                    {
                        if (originalFile.Length > destFile.Length)
                        {
                            originalFile.CopyTo(destFile.FullName, true);
                            nbfile++;
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(destFile.DirectoryName);
                        originalFile.CopyTo(destFile.FullName, false);
                        nbfile++;
                    }
                });
            }
            else
            {
                //error()
            }
           // int nbFichiers = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories).Length;
            LogFile lf = LogFile.GetInstance;
            lf.WriteLogMessage(name, source, destination, nbfile, 2);
        }
        public void CollectExistingData()
        {
            
        }
    }
}
