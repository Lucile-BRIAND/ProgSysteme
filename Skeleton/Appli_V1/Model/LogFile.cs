using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Appli_V1.Controllers
{
    class LogFile : FileAbstractClass
    {
        private static LogFile logInstance = null;
        private StreamWriter file;
        
        private  LogFile()
        {

        }
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
        public void WriteLogMessage()
        {

        }
        public override void InitFile()
        {

        }
        public override void CloseFile()
        {

        }


    }
}
