using System;
using System.Collections.Generic;
using Library;
using System.Linq;


namespace Admin
{
    class AdminUI
    {
        private readonly List<People> peoples = new List<People>();

        public void Run()
        {
            var pr = new PeopleRepository();
            peoples.AddRange(pr.ReadFromCSVFile());
            LogIn();
            AdminMenu();
        }

        private void AdminMenu()
        {
            var pr = new PeopleRepository();
           
            Console.Clear();
            Console.WriteLine("Please choose your command by typing a number ranging between 0 - 5");
            Console.WriteLine("1): Create a user account for a new employee");
            Console.WriteLine("2): Print all user accounts in the organization");
            Console.WriteLine("3): Remove an active user account");
            Console.WriteLine("4): Change access status to Employee/Admin");
            Console.WriteLine("5): Save user in the organization");
            Console.WriteLine("0): Exit the app");

            var command = InputValidation.OnlyNumbers(Console.ReadLine());
            switch (command)
            {
                case "1":
                    AddUser();
                    break;
                case "2":
                    ListUsers();
                    break;
                case "3":
                    RemoveUser();
                    break;
                case "4":
                    ChangeStatus();
                    break;
                case "5":
                    pr.SaveToFile(peoples);
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }
        }

        private void AddUser()
        {
            string id, name, adress, password;
            bool existance = true;

            Console.WriteLine("Please enter a user ID name");
            id = Console.ReadLine();
            id = InputValidation.NoSpecialCharacters(id);

            foreach (People item in peoples)
            {
                if (id == item.Id)
                {
                    existance = false;
                    Console.WriteLine("The user ID name has already been taken");
                    ReturnToMenu();
                }
            }

            Console.WriteLine("Please enter the user's full name");
            name = Console.ReadLine();
            name = InputValidation.OnlyLettersWhiteSpace(name);

            Console.WriteLine("Please enter the user's residential information");
            adress = Console.ReadLine();
            adress = InputValidation.OnlyNumberLetters(adress);

            Console.WriteLine("Please enter a password for the user");
            password = Console.ReadLine();
            password = InputValidation.NoWhiteSpace(password);

            Console.WriteLine("Should the user have Admin access? Please enter 'True' for YES and 'False' for NO");
            bool admin = bool.Parse(Console.ReadLine());
            
            if (existance == true)
            {
                peoples.Add(new People(id, name, adress, password, admin));
            }

            Console.WriteLine("Do you want to create another user? (Y/N)");
            var creatAnotherUser = 'Y';
            creatAnotherUser = char.ToUpper(Console.ReadKey(true).KeyChar);

            if (creatAnotherUser == 'Y')
            {
               Console.Clear();
               AddUser();
            }
          else if (creatAnotherUser == 'N')
            {
                ReturnToMenu();
            }
          else
          {
              throw new SystemException("Invalid input");
          }
          
       }

        private void ListUsers()
        {
            int count = 1;

            Console.WriteLine($"--All active users in the organization--");
            foreach (People item in peoples)
            {
                Console.WriteLine(string.Join(", ", $" {count} ID: " + item.Id + " ||" + " Name: " + item.Name + " ||" + " Adress: " + item.Adress + " ||" + " Password: " + item.Password + " ||" + " Status:" + People.PeopleIsAdmin(item.IsAdmin)));
                count++;
            }
            ReturnToMenu();
        }

        private void RemoveUser()
        {
            ListUsers();
            if (peoples.Count > 0)
            {
                Console.WriteLine("Enter the number of the user you want to remove");

                bool OnlyNumbers = int.TryParse(Console.ReadLine(), out int userInput);
                Console.WriteLine(userInput);
                while (userInput < 1 || userInput > peoples.Count || !OnlyNumbers)
                {
                    Console.WriteLine("Invalid number. Write a number that is assigned to a task");
                    OnlyNumbers = int.TryParse(Console.ReadLine(), out userInput);
                }

                Console.WriteLine("Are you sure you want to remove this user? Y/N");
                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    peoples.RemoveAt(userInput - 1);

                    Console.WriteLine("The user has been removed from the organization.");
                    ReturnToMenu();
                }
                else
                    {
                    Console.WriteLine("The task has not been removed.");
                    ReturnToMenu();
                    }
            }
         }

        private void ChangeStatus()
        {
            string id;
            bool existens = false;
            Console.WriteLine("Write the name of the user ID");
            id = Console.ReadLine();
            id = InputValidation.NoSpecialCharacters(id);

            Console.WriteLine("Enter 'True' for admin access and 'False' for employee access");
            var status = InputValidation.OnlyLetters(Console.ReadLine());

            while (status == null)
            {
                Console.WriteLine("Letters are only valid input");
            }

            for (int i = 0; i < peoples.Count; i++)
            {
                if (string.Equals(peoples[i].Id, id, StringComparison.CurrentCultureIgnoreCase))
                { 
                    peoples[i].IsAdmin = bool.Parse(status);
                    existens = false;
                    if (status == "True")
                    {
                        existens = true;
                    }

                    if (existens != false)
                    {
                        Console.WriteLine("Status has changed to: Is an Admin");
                    }
                    else
                    {
                        Console.WriteLine("Status has changed to: Not an Admin");
                    }
                    ReturnToMenu();
                }
            }

            if (!existens)
            {
                Console.WriteLine("The user that has been entered can not be found");
                ReturnToMenu();
            }
        }

        private bool AdminLogIn(string id, string password)
        {
            bool logIn = false;
            foreach (var person in peoples)
            {
                if (person.Id == id && person.Password == password && person.IsAdmin)
                {
                    logIn = true;
                }
            }
            return logIn;
        }

        private void LogIn()
        {
            bool logIn = false;
            while (!logIn)
            {
                Console.Clear();
                Console.WriteLine("--Admin Application--");
                Console.WriteLine("Please enter your ID: ");
                var tmpId = Console.ReadLine();
                Console.WriteLine("Please enter your password: ");
                var tmpPassword = Console.ReadLine();
                logIn = AdminLogIn(tmpId, tmpPassword);

                if (logIn)
                {
                    Console.WriteLine("Logging in. Please press any key to continue to the 'Main menu'");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("ID or password is incorrect. Press any key to try again");
                    Console.ReadKey();
                }
            }
        }

        private void ReturnToMenu()
        {
            Console.WriteLine("Press 'Enter' to return to Main Menue");
            while (Console.ReadKey(false).Key == ConsoleKey.Enter)
            {
                AdminMenu();
            }
        }
    }
}