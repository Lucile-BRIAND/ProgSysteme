﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace AppV3.Models
{
    class LogFile
    {
        //THIS CLASS IS A SINGLETON
        //
        //Private attributes
        private static LogFile logInstance = null; //default unique instance

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
        public void WriteLogMessage(string jobName, string sourcePath, string targetPath, int nbFile, long fileSize, int timeCryptoSoft, int timeExecuteBackup, string format)
        {
            
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
                    Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") //current timestamp in the proper format
                };

                //Reserializing the json file and writing
                string dataLogSerialized = JsonConvert.SerializeObject(dataLog, Newtonsoft.Json.Formatting.Indented);
                dataLogSerialized += "\n";
                File.AppendAllText("DailyLog.json", dataLogSerialized); //creates the file if it doesn't exist + appends text in it
            }
            else if (format == "xml")
            {
                if (!File.Exists("DailyLog.xml"))
                {
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    xmlWriterSettings.NewLineOnAttributes = true;
                    using (XmlWriter xmlWriter = XmlWriter.Create("DailyLog.xml", xmlWriterSettings))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("DailyLogs");

                        xmlWriter.WriteStartElement("Job");
                        xmlWriter.WriteElementString("JobName", jobName);
                        xmlWriter.WriteElementString("SourcePath", sourcePath);
                        xmlWriter.WriteElementString("TargetPath", targetPath);
                        xmlWriter.WriteElementString("FileSize", fileSize.ToString());
                        xmlWriter.WriteElementString("FileTransferTime", timeExecuteBackupFormated);
                        xmlWriter.WriteElementString("CryptTime", timeCryptoSoftFormated);
                        xmlWriter.WriteElementString("Date", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
                        xmlWriter.Flush();
                        xmlWriter.Close();
                    }
                }
                else
                {
                    XDocument xDocument = XDocument.Load("DailyLog.xml");
                    XElement root = xDocument.Element("DailyLogs");
                    IEnumerable<XElement> rows = root.Descendants("Job");
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
           
        }

    }
}