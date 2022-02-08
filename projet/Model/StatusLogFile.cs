using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;


namespace Appli_V1.Controllers
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
        public void WriteStatusLogMessage(string jobName, string jobType, string sourcePath, string targetPath, string state, int totalFilesToCopy, int totalFilesSize, int nbFilesLeftToDo, string format)
        {
            if(format == "json")
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
                };

                //Reserializing the json file and writing
                string dataLogSerialized = JsonConvert.SerializeObject(dataLog, Newtonsoft.Json.Formatting.Indented);
                dataLogSerialized += "\n";
                //File.WriteAllText("StatusLogFile.json", json); //creates the file if it doesn't exist + overwrite all the text in it
                File.AppendAllText("StatusLogFile.json", dataLogSerialized); //creates the file if it doesn't exist + appends text in it
            }
            else if (format == "xml")
            {
                //XML document creation
                if (!File.Exists("StatusLogFile.xml"))
                {
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    xmlWriterSettings.NewLineOnAttributes = true;
                    using (XmlWriter xmlWriter = XmlWriter.Create("StatusLogFile.xml", xmlWriterSettings))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("StatusLogFiles");

                        xmlWriter.WriteStartElement("Job");
                        xmlWriter.WriteElementString("JobName", jobName);
                        xmlWriter.WriteElementString("JobType", jobType);
                        xmlWriter.WriteElementString("SourcePath", sourcePath);
                        xmlWriter.WriteElementString("TargetPath", targetPath);
                        xmlWriter.WriteElementString("State", state);
                        xmlWriter.WriteElementString("TotalFilesToCopy", totalFilesToCopy.ToString());
                        xmlWriter.WriteElementString("TotalFilesSize", totalFilesSize.ToString());
                        xmlWriter.WriteElementString("NbFilesLeftToDo", nbFilesLeftToDo.ToString());
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
                        xmlWriter.Flush();
                        xmlWriter.Close();
                    }
                }
                else
                {
                    XDocument xDocument = XDocument.Load("StatusLogFile.xml");
                    XElement root = xDocument.Element("StatusLogFiles");
                    IEnumerable<XElement> rows = root.Descendants("Job");
                    XElement firstRow = rows.First();
                    firstRow.AddBeforeSelf(
                       new XElement("Job",
                       new XElement("JobName", jobName),
                       new XElement("JobType", jobType),
                       new XElement("SourcePath", sourcePath),
                       new XElement("TargetPath", targetPath),
                       new XElement("State", state),
                       new XElement("TotalFilesToCopy", totalFilesToCopy.ToString()),
                       new XElement("TotalFilesSize", totalFilesSize.ToString()),
                       new XElement("NbFilesLeftToDo", nbFilesLeftToDo.ToString())));
                    xDocument.Save("StatusLogFile.xml");
                }
            }

        }

    }

}