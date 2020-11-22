using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library
{
    public class PeopleRepository
    {
        private string _filePath = "person.csv";

        public PeopleRepository()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _filePath = Path.Combine(appDataFolder, "person.csv");

            if (!File.Exists(_filePath))
            {
                using (File.Create(_filePath)) ;

                //skapar en admin med dess värden
                People admin = new People("Admin", "Adam", "Adminstreet", "123", true);
                List<People> peoples = new List<People>();
                peoples.Add(admin);
                SaveToFile(peoples);
            }
        }
        
        public List<People> ReadFromCSVFile()
        {
          List<People> peoples = new List<People>();

            using (var reader = File.OpenText(_filePath))
            {
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    var splitLines = line.Split(',');
                    People people = new People(splitLines[0],splitLines[1], splitLines[2], splitLines[3], bool.Parse(splitLines[4]));

                    peoples.Add(people);
                }
                return peoples;
            }
        }
        
        public void SaveToFile(List<People> peoples)
        {
            using (var fs = File.CreateText(_filePath)) 
            {
                    foreach (var item in peoples)
                    {
                        fs.WriteLine($"{item.Id},{item.Name},{item.Adress},{item.Password},{item.IsAdmin}");
                    }

                Console.WriteLine("The user has been saved in the organization. Press 'Enter' to return to main menu.");
                while (Console.ReadKey(false).Key != ConsoleKey.Enter)
                {
                    return;
                }
            }
        }
       
    }
}





