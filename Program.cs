using DataValidatore;
using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.csv");
        List<Employee> validEmployees = new List<Employee>();

        List<string> errors = new List<string>();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(','); 

            Employee employee = new Employee();

            if (!int.TryParse(data[0], out int id))
            {
                errors.Add($"Line {i + 1}: ID must be a valid number");
                continue;
            }

            employee.ID = id;

            employee.Name = data[1];

            if (!int.TryParse(data[2], out int age))
            {
                errors.Add($"Line {i + 1}: Age must be a valid number");
                continue;
            }

            employee.Age = age;

            if (!decimal.TryParse(data[3], out decimal salary))
            {
                errors.Add($"Line {i + 1}: Salary must be a valid number");
                continue;
            }

            employee.Salary = salary;

            bool isValid = true;//validation flag

            if(string.IsNullOrWhiteSpace(employee.Name))
            {
                errors.Add($"Line {i + 1}: Name is missing");
                isValid = false;
            }
            if(employee.Age < 18 || employee.Age > 100)
            {
                errors.Add($"Line {i + 1}: Age must be between 18 and 100");
                isValid = false;
            }
            if(isValid)
            {
                validEmployees.Add(employee);
            }

        }
        List<string> outputLines = new List<string>();

        outputLines.Add("ID,Name,Age,Salary");

        foreach (Employee employee in validEmployees)
        {
            outputLines.Add(
                $"{employee.ID},{employee.Name},{employee.Age},{employee.Salary}");
        }

        File.WriteAllLines("output.csv", outputLines);
        File.WriteAllLines("errors.txt", errors);
        Console.WriteLine("Validation Complete");
        foreach (string error in errors)
        {
            Console.WriteLine(error);
        }
    }
}