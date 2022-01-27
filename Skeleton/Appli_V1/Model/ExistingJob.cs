using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace projet
{
    class ExistingJob 
    {
        
        private static ExistingJob jobInstance = null;
        private string jobName;
        private ExistingJob()
        {

        }

        public static ExistingJob GetInstance()
        {
                if (jobInstance == null)
                {
                    jobInstance = new ExistingJob();
                }
                return jobInstance;

        }
        public string ReadFile()
        {
            StreamReader reader = new StreamReader("Jobfile.json");
            string jsonString = reader.ReadToEnd();
            dynamic model = JsonConvert.DeserializeObject(jsonString);
            ExistingJob existingjob = new ExistingJob();
            existingjob.jobName = model.jobName;
            return existingjob.jobName;
        }

        public void WriteExistingJobs( string jobNamet, string jobTypet, string sourcePatht, string targetPatht)
        {
            var my_jsondata = new
            {
                jobName = jobNamet,
                jobType = jobTypet,
                sourcePath = sourcePatht,
                targetPath = targetPatht,
            };
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = System.Text.Json.JsonSerializer.Serialize(my_jsondata, options);
            File.AppendAllText("Jobfile.json", json);
        }
        public void RemoveExistingJobs(string jobName)
        {

        }
    }
}
