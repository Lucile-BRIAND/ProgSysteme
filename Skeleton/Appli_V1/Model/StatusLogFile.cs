using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Appli_V1.Controllers
{
    class StatusLogFile : FileAbstractClass
    {
        private StreamWriter file;
        private static StatusLogFile statusInstance = null;

        private StatusLogFile()
        {

        }

        public static StatusLogFile GetInstance
        {
            get {
                if (statusInstance == null)
                {
                    statusInstance = new StatusLogFile();
                }
                return statusInstance;
            }
        }

        public  void InitFile()
        {


        }
        public  void CloseFile()
        {

        }

        public void WriteStatusLogMessage()
        {

        }

    }
}
