using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV3.Models
{
    class FileSize
    {
        //Instance null by default 
        private static FileSize fileSizeInstance = null;
        public int FileMaxSize { get; set; }
        public bool FileIsTransfering { get; set; }

        //private constructor 
        private FileSize() { }

        // Initializes the unique instance
        public static FileSize GetInstance
        {
            get
            {
                if (fileSizeInstance == null)
                {
                    fileSizeInstance = new FileSize();
                }
                return fileSizeInstance;
            }

        }
    }
}
