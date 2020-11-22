using Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee
{
    class EmployeeUI
    {
        private readonly List<People> peoples = new List<People>();
        public string userId;
        public void Run()
        {
            var pr = new PeopleRepository();
            peoples.AddRange(pr.ReadFromCSVFile());
            LogIn();
            EmployeeMenu();
        }

        private void EmployeeMenu()
        {
            var pr = new PeopleRepository();
            
            Console.Clear();
            Console.WriteLine("Please choose your command by typing a number ranging between 0 - 3");
            Console.WriteLine("1): Show profile information");
            Console.WriteLine("2): Edit your profile information");
            Console.WriteLine("3): Save user in the organization");
            Console.WriteLine("0): Exit the app");

            var command = InputValidation.OnlyNumbers(Console.ReadLine());
            switch (command)
            {
                case "1":
                    PrintOwnInfo(userId);
                    ReturnToMenu();
                    break;
                case "2":
                    userId = CheckEditUser(userId);
                    ReturnToMenu();
                    break;
                case "3":
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

       private void PrintOwnInfo(string id)
        {
            foreach (var item in peoples)
            {
                if (item.Id == id)
                {
                    Console.WriteLine($"ID name: {item.Id}");
                    Console.WriteLine($"Full name: {item.Name}");
                    Console.WriteLine($"Residential information: {item.Adress}");
                    Console.WriteLine($"Password: {item.Password}");
                    Console.WriteLine($"User access satus: {item.IsAdmin}");
                }
            }
    
        }

        private bool EmployeeLogIn(string id, string password)
        {
            bool logIn = false;
            foreach (var person in peoples)
            {
                if (person.Id == id && person.Password == password)
                {
                    logIn = true;
                }
                else
                {
                    logIn = false;
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
                Console.WriteLine("--Employee application--");
                Console.WriteLine("Please enter your ID: ");
                userId = Console.ReadLine();
                Console.WriteLine("Please enter your password: ");
                var tmpPassword = Console.ReadLine();
                logIn = EmployeeLogIn(userId, tmpPassword);

                if (logIn)
                {
                    Console.WriteLine("Logging in. Please press any key to continue to the menu");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("ID or password is incorrect. Press any key to try again");
                    Console.ReadKey();
                }
            }
        }

        private string CheckEditUser(string id)
        {
            string tmpid = id;
            Console.WriteLine("--Edit information menue--");
            Console.WriteLine("Your current profile");
            PrintOwnInfo(id);
            Console.WriteLine("Enter the information you want to edit \n 1: Id \n 2: Name \n 3: Residential information \n 4: Password");
            foreach (var item in peoples)
            {
                if (item.Id == id)
                {
                    string userInput = Console.ReadLine().ToLower();
                    switch (userInput)
                    {
                        case "1":
                            Console.WriteLine("Please enter the new ID name");
                            tmpid = InputValidation.NoSpecialCharacters(Console.ReadLine());
                            item.Id = tmpid;
                            break;
                        case "2":
                            Console.WriteLine("Please enter the new Name");
                            string tmpName = InputValidation.OnlyLettersWhiteSpace(Console.ReadLine());
                            item.Name = tmpName;
                            break;
                        case "3":
                            Console.WriteLine("Please enter the new Residential information");
                            string tmpAdress = InputValidation.OnlyLettersWhiteSpace(Console.ReadLine());
                            item.Adress = tmpAdress;
                            break;
                        case "4":
                            Console.WriteLine("Please enter the new password");
                            string tmpPassword = InputValidation.OnlyNumberLetters(Console.ReadLine());
                            item.Password = tmpPassword;
                            break;
                        default:
                            Console.WriteLine("Error");
                            break;
                            
                    } 
                }
              
            }
            return tmpid;  
        }

        public void ReturnToMenu()
        {
            Console.WriteLine("Press 'Enter' to return to Main Menue");
            while (Console.ReadKey(false).Key == ConsoleKey.Enter)
            {
                EmployeeMenu();
            }
        }
    }
}
