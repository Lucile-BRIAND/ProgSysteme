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
            if (!File.Exists(file)) //Creates a json file if Jobfile.json doesn't exists
            {
                File.Create(file).Close();
            }

            var contentFile = System.IO.File.ReadAllText(file); //Gets the content of the file 
            List<jobModel> jobModelList = new List<jobModel>();
            try  //Removes exception message 
            {
                jobModelList = JsonConvert.DeserializeObject<List<jobModel>>(contentFile); //Gets all objects from the file 
            }
            catch
            {

            }

            if (jobModelList == null)
            {
                jobModelList = new List<jobModel>(); //Creates the list object if nothing is in the file to allows using the "add" function
            }

            jobModelList.Add(iList);
            System.IO.File.WriteAllText(file, JsonConvert.SerializeObject(jobModelList)); //Replaces the file with the new one  
            return true;
        }

        public bool RemoveExistingJobs(string name) //Removes the backup corresponding to the name parameter 
        {
            bool verif = true;
            var contentFile = System.IO.File.ReadAllText(file);
            List<jobModel> jobModelList = new List<jobModel>();
            try //Removes exception message 
            {
                jobModelList = JsonConvert.DeserializeObject<List<jobModel>>(contentFile); //Inserts file content into object list 
            }
            catch
            {

            }
            try
            {
                foreach (jobModel jobObject in jobModelList) //Gets objects in the object list 
                {

                    if (jobObject.jobName == name) //Checks if a object with this name exist 
                    {
                        var index = jobModelList.IndexOf(jobObject);
                        jobModelList.RemoveAt(index);
                        System.IO.File.WriteAllText(file, JsonConvert.SerializeObject(jobModelList)); //Replaces the file with the new one   
                    }
                    else
                    {
                        verif = false;
                    }
                }
            }
            catch
            {
            }
            return verif;
        }
    }
}
