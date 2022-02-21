using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AppV3.VM;

namespace AppV3.Models
{
        class ExistingJob
        {
            public string file = "Jobfile.json"; //File where we stock backups 
            public ExistingJob() //Constructor 
            {

            }


            public string ReadFile() //Gets a string of the file
            {
                var contentFile = System.IO.File.ReadAllText(file);
                return contentFile;
            }

            public bool WriteExistingJobs(JobModel list) //Writes the backup in the file 
            {
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                }

                var contentFile = System.IO.File.ReadAllText(file);
                List<JobModel> jobModelList = new List<JobModel>();
                try
                {
                    jobModelList = JsonConvert.DeserializeObject<List<JobModel>>(contentFile);
                }
                catch
                {

                }

                if (jobModelList == null)
                {
                    jobModelList = new List<JobModel>();
                }

                jobModelList.Add(list);
                System.IO.File.WriteAllText(file, JsonConvert.SerializeObject(jobModelList, Formatting.Indented)); //Replaces the file with the new one  
                return true;
            }

            public bool RemoveExistingJobs(string jobName) //Removes the backup corresponding to the name parameter 
            {
                var contentFile = System.IO.File.ReadAllText(file);
                List<JobModel> jobModelList = new List<JobModel>();
                try
                {
                    jobModelList = JsonConvert.DeserializeObject<List<JobModel>>(contentFile); //Inserts file content into object list 
                }
                catch
                {

                }

                foreach (JobModel jobObject in jobModelList) //Gets objects in the object list 
                {
                    if (jobObject.jobName == jobName)
                    {
                        var index = jobModelList.IndexOf(jobObject);
                        jobModelList.RemoveAt(index);
                        System.IO.File.WriteAllText(file, JsonConvert.SerializeObject(jobModelList, Formatting.Indented)); //Replaces the file with the new one   
                        break;
                    }
                }
                return true;
            }
        }
    }


