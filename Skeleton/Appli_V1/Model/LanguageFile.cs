using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Appli_V1.Controllers
{
    class LanguageFile : FileAbstractClass
    {
        private StreamWriter file;
        private static LanguageFile languageInstance  = null ;

        public override void InitFile()
        {
            this.file = new StreamWriter("TT");
        }
        public void StatusLanguageFile()
        {

        }
        public static LanguageFile GetInstance
        {
            get
            {
                if (languageInstance == null)
                {
                    languageInstance = new LanguageFile();
                }
                return languageInstance;
            }

        }
        public override void CloseFile()
        {

        }
    }
}
