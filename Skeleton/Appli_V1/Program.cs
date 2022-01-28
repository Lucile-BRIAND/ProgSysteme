using System;
using System.Net.Http;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Appli_V1.Controllers
{
    class Program : MainController
    {
        /*public class Employee
        {
            public string Main { get; set; }

        }*/

        MainController mainController = new MainController();

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            /*StreamReader streamreader = new StreamReader("../../../Languages/English_Lang.json");
            string jsonread = streamreader.ReadToEnd();
            Employee employee = JsonConvert.DeserializeObject<Employee>(jsonread);
            Console.WriteLine(employee.Main);*/

            ChooseLanguageStrategy Premierepage = new ChooseLanguageStrategy();
            Premierepage.InitView();
            Premierepage.CheckRequirements();
            Premierepage.CollectExistingData();
            MainController Second_Page = new MainController();
            Second_Page.MainMenu();


        }


    }
}