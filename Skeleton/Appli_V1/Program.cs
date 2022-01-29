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

        // /!\ VERIFY THE USE OF THIS CLASS BELLOW !
        public class Employee
        {
            public string Main { get; set; }

        }

        // /!\ VERIFY THE USE OF THIS OBJECT BELLOW !
        MainController mainController = new MainController();
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            /*StreamReader streamreader = new StreamReader("../../../Languages/English_Lang.json");
            string jsonread = streamreader.ReadToEnd();
            Employee employee = JsonConvert.DeserializeObject<Employee>(jsonread);
            Console.WriteLine(employee.Main);*/


            // Instanciate object to launch the first page, this object will call the InitView to ask the user which language he wants to use.
            // Then our Controller will collect the user's choice

            ChooseLanguageStrategy First_Page = new ChooseLanguageStrategy();
            First_Page.InitView();
            First_Page.CheckRequirements();
            First_Page.CollectExistingData();

            // Now the user's chosen language is set, we can now enter our menu
            // we instanciate an object from the MainController Class to display the main menu, calling the corresponding method
            MainController Second_Page = new MainController();
            Second_Page.MainMenu();

        }

    }
}