using Library;
using System.Collections.Generic;


namespace Admin
{
    class Program
    {
        // skapar instans variabel ut av PeopleRepository, för att slippa skapa den flera gånger
        public static PeopleRepository peopleRepository = new PeopleRepository();

        //måste sparas i en gemensam plats för att ha tillgång till den annars kommer den skapa den o sedan kasta bort listan
        public static List<People> peoples = new List<People>();

        static void Main(string[] args)
        {
            var adminUI = new AdminUI();
            adminUI.Run();
        }
       
     }
 }




    

