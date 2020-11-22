using System;
using System.Text.RegularExpressions;

namespace Library
{
    public class InputValidation
    {
        //only letters and numbers
        public static string NoSpecialCharacters(string userInput)
        {
            string result = userInput;
            while (!Regex.IsMatch(result, @"^[a-zA-Z0-9]{5,12}$"))
            {
                Console.WriteLine("Needs to be 5-40 symbols long \nWhite spcae is not a valid character \nSpecial symbols is not a valid character");
                result = Console.ReadLine();
            }
            return result;
        }

        //only letters & white space
        public static string OnlyLettersWhiteSpace(string userInput) 
        {
            string result = userInput;
            while (!Regex.IsMatch(result, @"^(?!\s*$)[\p{L}\s]+$"))
            {
                Console.WriteLine("Only letters are valid inputs");
                result = Console.ReadLine();
            }
            return result;
        }

        //only letters/numbers & white space
        public static string OnlyNumberLetters(string userInput) 
        {
            string result = userInput;
            while (!Regex.IsMatch(result, @"[a-zA-Z0-9]$"))
            {
                Console.WriteLine("Only letter and numbers are valid inputs. \n Special symbols is not a valid character");
                result = Console.ReadLine();
            }
            return result;
        }

       //all execpt whitespace
        public static string NoWhiteSpace(string userInput)
        {
            string result = userInput;
            while (!Regex.IsMatch(result, @"[^\s]{5,12}$"))
            {
                Console.WriteLine("Password need to be 5-12 symbols long \nWhite spcae is not a valid character");
                result = Console.ReadLine();
            }
            return result;
        }

        public static string OnlyNumbers(string userInput)
        {
            string result = userInput;
            while (!Regex.IsMatch(result, @"[0-9]"))
            {
                Console.WriteLine("Only numbers ranging in the menu are valid inputs");
                result = Console.ReadLine();
            }
            return result;
        }

        public static string OnlyLetters(string userInput)
        {
            string result = userInput;
            while (!Regex.IsMatch(result, @"^[\p{L}]+$"))
            {
                Console.WriteLine("Only numbers ranging in the menu are valid inputs");
                result = Console.ReadLine();
            }
            return result;
        }

    }
}
