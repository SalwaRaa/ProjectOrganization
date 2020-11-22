using System;
using System.Collections.Generic;
using System.Text;
using Library;
using System.Linq;
using static Library.InputValidation;
using System.Collections;


namespace Employee
{
    
    class Program
    {
        static void Main(string[] args)
        {
            var employeeUI = new EmployeeUI();
            employeeUI.Run();

        }
    }
}
