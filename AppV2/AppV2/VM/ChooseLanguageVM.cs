using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppV2.Models;

namespace AppV2.VM
{
    class ChooseLanguageVM
    {
        LanguageFile languageFile = LanguageFile.GetInstance;
        public LanguageFile SaveLanguage(int index)
        {
            switch (index) {
                case 0:
                    languageFile.InitLanguage("French");
                    return languageFile.ReadFile();

                case 1:
                    languageFile.InitLanguage("English");
                    return languageFile.ReadFile();
                default:
                    languageFile.InitLanguage("English");
                    return languageFile.ReadFile();
            }
            
        }
    }
}
