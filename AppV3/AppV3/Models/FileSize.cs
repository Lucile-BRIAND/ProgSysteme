using System;

namespace AppV3.Models
{
    public class FileSize //SINGLETON
    {
        //Instance null by default 
        private static FileSize fileSizeInstance = null;
        public int FileMaxSize { get; set; }
        public bool FileIsTransfering { get; set; }

        //Private constructor 
        private FileSize() { }

        //Initialize the unique instance
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
