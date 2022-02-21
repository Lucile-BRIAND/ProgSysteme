using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppV3.Models;

namespace AppV3.VM
{
    class ChooseLanguageVM
    {
        LanguageFile singletonLang = LanguageFile.GetInstance;
        public LanguageFile SaveLanguage(int index)
        {
            switch (index) {
                case 0:
                    singletonLang.InitLanguage("French");
                    return singletonLang.ReadFile();

                case 1:
                    singletonLang.InitLanguage("English");
                    return singletonLang.ReadFile();
                default:
                    singletonLang.InitLanguage("English");
                    return singletonLang.ReadFile();
            }
            
        }
    }
}
