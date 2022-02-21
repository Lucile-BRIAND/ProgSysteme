using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Threading;

namespace AppV3.Models
{
    class LogFile //SINGLETON
    {
        //Private attributes
        private static LogFile logInstance = null; //default unique instance
        private string format;
        private string JobSoftware;
        private Semaphore semaphore = new Semaphore(1,1);

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

        // Init and Get functions -> Format LogFile
        public void InitFormat(string Format)
        {
            this.format = Format;
            Trace.WriteLine(format);
        }
        public string GetFormat()
        {
            return format;
        }

        // Init and Get functions -> JobSoftware
        public void InitJobSoftware(string jobSoftware)
        {
            this.JobSoftware = jobSoftware;
            Trace.WriteLine(JobSoftware);
        }
        public string GetJobSoftawre()
        {
            return JobSoftware;
        }

        //Writing content in the log file
        public void WriteLogMessage(string jobName, string sourcePath, string targetPath, int nbFile, long fileSize, int timeCryptoSoft, int timeExecuteBackup, string format)
        {
            //Blocks until the current thread is ended
            semaphore.WaitOne();

            //Formats several parameters
            string timeCryptoSoftFormated= timeCryptoSoft.ToString() + " ms";
            string timeExecuteBackupFormated = timeExecuteBackup.ToString() + " ms";
            string fileSizeFormated = fileSize.ToString() + " B";

            if (format == "json")
            {
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
                    Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") //Current timestamp in the proper format
                };

                //Reserializing the json file and writing
                string dataLogSerialized = JsonConvert.SerializeObject(dataLog, Newtonsoft.Json.Formatting.Indented);
                dataLogSerialized += "\n";
                File.AppendAllText("DailyLog.json", dataLogSerialized); //Creates the file if it doesn't exist + appends text in it
            }
            else if (format == "xml")
            {
                if (!File.Exists("DailyLog.xml")) //If the file doesn't exist
                {
                    //Creates the file and the associated settings
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    xmlWriterSettings.NewLineOnAttributes = true;

                    //Writes into the file
                    using (XmlWriter xmlWriter = XmlWriter.Create("DailyLog.xml", xmlWriterSettings))
                    {
                        //Start node
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("DailyLogs");

                        //Middle pieces of information
                        xmlWriter.WriteStartElement("Job");
                        xmlWriter.WriteElementString("JobName", jobName);
                        xmlWriter.WriteElementString("SourcePath", sourcePath);
                        xmlWriter.WriteElementString("TargetPath", targetPath);
                        xmlWriter.WriteElementString("FileSize", fileSize.ToString());
                        xmlWriter.WriteElementString("FileTransferTime", timeExecuteBackupFormated);
                        xmlWriter.WriteElementString("CryptTime", timeCryptoSoftFormated);
                        xmlWriter.WriteElementString("Date", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
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
                    XDocument xDocument = XDocument.Load("DailyLog.xml");
                    XElement root = xDocument.Element("DailyLogs");
                    IEnumerable<XElement> rows = root.Descendants("Job"); //New middle node
                    XElement firstRow = rows.First();
                    firstRow.AddBeforeSelf(
                       new XElement("Job",
                       new XElement("JobName", jobName),
                       new XElement("SourcePath", sourcePath),
                       new XElement("TargetPath", targetPath),
                       new XElement("FileSize", fileSize.ToString()),
                       new XElement("FileTransferTime", timeExecuteBackupFormated),
                       new XElement("CryptTime", timeCryptoSoftFormated),
                       new XElement("Date", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))));
                    xDocument.Save("DailyLog.xml");
                }
            }

            //Releases the semaphore
            semaphore.Release();
        }

    }
}
