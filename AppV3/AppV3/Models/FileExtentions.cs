using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV3.Models
{
    class FileExtentions
    {
        //Instance null by default 
        private static FileExtentions extentionInstance = null;

        public bool PNGvalue { get; set; }
        public bool JPGvalue { get; set; }
        public bool PDFvalue { get; set; }
        public bool TXTvalue { get; set; }

        public List<string> extentions = new List<string>();

        //private constructor 
        private FileExtentions() { }

        // Initializes the unique instance
        public static FileExtentions GetInstance
        {
            get
            {
                if (extentionInstance == null)
                {
                    extentionInstance = new FileExtentions();
                }
                return extentionInstance;
            }

        }
    }
}
