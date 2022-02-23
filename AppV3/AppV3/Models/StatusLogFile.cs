using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Threading;

namespace AppV3.Models
{
    class StatusLogFile //SINGLETON
    {
        //Private attributes
        private static StatusLogFile statusInstance = null;
        private Semaphore semaphore = new Semaphore(1, 1);
        private List<JobModel> jobModelList = new List<JobModel>();

        public JobProgressInformation jobInfo = new JobProgressInformation();


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
        public void WriteStatusLogMessage(string jobName, string jobType, string sourcePath, string targetPath, string state, int totalFilesToCopy, long totalFilesSize, int nbFilesLeftToDo, long fileSizeLeftToCopy, string format)
        {
            //Blocks until the current thread is ended
            semaphore.WaitOne();

            //Formats several parameters
            string totalFilesSizeFormated = totalFilesSize.ToString() + " B";
            string fileSizeLeftToCopyFormated = fileSizeLeftToCopy.ToString() + " B";

            if (format == "json")
            {
                if (!File.Exists("StatusLogFile.json"))
                {
                    File.Create("StatusLogFile.json").Close();
                }
                var content = File.ReadAllText("StatusLogFile.json");

                JobModel jobModel = new JobModel();
                JobProgressInformation jobProgressInformation1 = new JobProgressInformation();
                jobModel.jobProgressInformation = jobProgressInformation1;

                jobModel.jobProgressInformation.Name = jobName;
                jobModel.jobProgressInformation.Type = jobType;
                jobModel.jobProgressInformation.SourcePath = sourcePath;
                jobModel.jobProgressInformation.TargetPath = targetPath;
                jobModel.jobProgressInformation.State = state;
                jobModel.jobProgressInformation.TotalFilesToCopy = totalFilesToCopy;
                jobModel.jobProgressInformation.TotalFilesSize = totalFilesSize;
                jobModel.jobProgressInformation.NbFilesLeftToDo = nbFilesLeftToDo;
                jobModel.jobProgressInformation.FileSizeLeftToCopy = fileSizeLeftToCopy;

                JobModel jobModelList = JsonConvert.DeserializeObject<JobModel>(content);
                List<JobModel> jobModelList1 = new List<JobModel>();
                jobModelList1.Add(jobModelList);

                //Adding values to the json keys
                //var dataLog = new
                //{
                //    Name = jobName,
                //    Type = jobType,
                //    SourcePath = sourcePath,
                //    TargetPath = targetPath,
                //    State = state,
                //    TotalFilesToCopy = totalFilesToCopy,
                //    TotalFilesSize = totalFilesSizeFormated,
                //    NbFilesLeftToDo = nbFilesLeftToDo,
                //    FileSizeLeftToCopy = fileSizeLeftToCopyFormated
                //};

                //Reserializing the json file and writing
                string dataLogSerialized = JsonConvert.SerializeObject(jobModelList1, Newtonsoft.Json.Formatting.Indented);

                //File.WriteAllText("StatusLogFile.json", json); //creates the file if it doesn't exist + overwrite all the text in it
                File.WriteAllText("StatusLogFile.json", dataLogSerialized); //creates the file if it doesn't exist + appends text in it
            }
            else if (format == "xml")
            {
                if (!File.Exists("StatusLogFile.xml")) //If the file doesn't exist
                {
                    //Creates the file and the associated settings
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    xmlWriterSettings.NewLineOnAttributes = true;

                    //Writes into the file
                    using (XmlWriter xmlWriter = XmlWriter.Create("StatusLogFile.xml", xmlWriterSettings))
                    {
                        //Start node
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("StatusLogFile");

                        //Middle pieces of information
                        xmlWriter.WriteStartElement("Job");
                        xmlWriter.WriteElementString("JobName", jobName);
                        xmlWriter.WriteElementString("Type", jobType);
                        xmlWriter.WriteElementString("SourcePath", sourcePath);
                        xmlWriter.WriteElementString("TargetPath", targetPath);
                        xmlWriter.WriteElementString("State", state);
                        xmlWriter.WriteElementString("TotalFilesToCopy", totalFilesToCopy.ToString());
                        xmlWriter.WriteElementString("TotalFilesSize", totalFilesSize.ToString() + " B");
                        xmlWriter.WriteElementString("NbFilesLeftToDo", nbFilesLeftToDo.ToString());
                        xmlWriter.WriteElementString("FileSizeLeftToCopy", fileSizeLeftToCopy.ToString() + " B");
                        xmlWriter.WriteEndElement();

                        //Ending node and closing
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
                        xmlWriter.Flush();
                        xmlWriter.Close();
                    }
                }
                else
                {
                    //Search for the existing file and writing
                    XDocument xDocument = XDocument.Load("StatusLogFile.xml");
                    XElement root = xDocument.Element("StatusLogFile");
                    IEnumerable<XElement> rows = root.Descendants("Job"); //New middle node
                    XElement firstRow = rows.First();
                    firstRow.AddBeforeSelf(
                       new XElement("Job",
                       new XElement("JobName", jobName),
                       new XElement("Type", jobType),
                       new XElement("SourcePath", sourcePath),
                       new XElement("TargetPath", targetPath),
                       new XElement("State", state),
                       new XElement("TotalFilesToCopy", totalFilesToCopy.ToString()),
                       new XElement("TotalFilesSize", totalFilesSize.ToString() + " B"),
                       new XElement("NbFilesLeftToDo", nbFilesLeftToDo.ToString()),
                       new XElement("FileSizeLeftToCopy", fileSizeLeftToCopy.ToString() + " B")));
                    xDocument.Save("StatusLogFile.xml");
                }
            }

            //Releases the semaphore
            semaphore.Release();
        }

    }
}
