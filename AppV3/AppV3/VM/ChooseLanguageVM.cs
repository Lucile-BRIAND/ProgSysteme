using System;
using AppV3.Models;

namespace AppV3.VM
{
    class ChooseLanguageVM
    {
        LanguageFile singletonLang = LanguageFile.GetInstance;

        //Saves the language choice in the language Singleton
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
