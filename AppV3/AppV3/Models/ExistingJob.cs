using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace AppV3.Models
{
    class ExistingJob
    {
        public string file = "Jobfile.json"; //File where we stock backups 

        public ExistingJob() //Constructor 
        {

        }

        public string ReadFile() //Put the read file in a string
        {
            var contentFile = System.IO.File.ReadAllText(file);
            return contentFile;
        }

        public bool WriteExistingJobs(JobModel list) //Write the backup in the file 
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
            System.IO.File.WriteAllText(file, JsonConvert.SerializeObject(jobModelList, Formatting.Indented)); //Replace the file with the new one  
            return true;
        }

        public bool RemoveExistingJobs(string jobName) //Remove the backup corresponding to the name parameter 
        {
            var contentFile = System.IO.File.ReadAllText(file);
            List<JobModel> jobModelList = new List<JobModel>();
            try
            {
                jobModelList = JsonConvert.DeserializeObject<List<JobModel>>(contentFile); //Insert file content into object list 
            }
            catch
            {

            }

            foreach (JobModel jobObject in jobModelList) //Get objects in the object list 
            {
                if (jobObject.jobName == jobName)
                {
                    var index = jobModelList.IndexOf(jobObject);
                    jobModelList.RemoveAt(index);
                    System.IO.File.WriteAllText(file, JsonConvert.SerializeObject(jobModelList, Formatting.Indented)); //Replace the file with the new one   
                    break;
                }
            }
            return true;
        }
    }
}


