using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Appli_V1.Controllers
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

        public bool WriteExistingJobs(jobModel iList) //Writes the backup in the file 
        {
            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }

            var contentFile = System.IO.File.ReadAllText(file);
            List<jobModel> jobModelList = new List<jobModel>();
            try
            {
                jobModelList = JsonConvert.DeserializeObject<List<jobModel>>(contentFile);
            }
            catch
            {

            }

            if (jobModelList == null)
            {
                jobModelList = new List<jobModel>();
            }

            jobModelList.Add(iList);
            System.IO.File.WriteAllText(file, JsonConvert.SerializeObject(jobModelList, Formatting.Indented)); //Replaces the file with the new one  
            return true;
        }

        public bool RemoveExistingJobs(string name) //Removes the backup corresponding to the name parameter 
        {
            var contentFile = System.IO.File.ReadAllText(file);
            List<jobModel> jobModelList = new List<jobModel>();
            try
            {
                jobModelList = JsonConvert.DeserializeObject<List<jobModel>>(contentFile); //Inserts file content into object list 
            }
            catch
            {

            }

            foreach (jobModel jobObject in jobModelList) //Gets objects in the object list 
            {
                if (jobObject.jobName == name)
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
