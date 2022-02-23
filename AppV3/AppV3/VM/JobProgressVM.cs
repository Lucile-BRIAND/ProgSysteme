using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AppV3.Models;

namespace AppV3.VM
{
    public class JobProgressVM
    {
        private int jobProgressPercentage;
        public string fileJson = "StatusLogFile.json";
        public string fileXml = "StatusLogFile.xml";
        
        JobProgressInformation jobProgressInformation = new JobProgressInformation();
        JobProgressVM jobProgressVM = new JobProgressVM();
        public void ReadLogFile()
        {
            var contentFile = System.IO.File.ReadAllText(fileJson);
            jobProgressVM = JsonConvert.DeserializeObject<JobProgressVM>(contentFile);            
        }

        public void DisplayLogFile()
        {            
        }
    }
}
