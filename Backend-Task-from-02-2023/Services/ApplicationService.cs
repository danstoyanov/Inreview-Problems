using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

using Backend.Model;
using Backend.Interfaces;

namespace Backend.Services
{
    public class ApplicationService : IMovieService, ISalaryService
    {
        private List<MovieStar> movieStars;
        private string netSalary;

        public ApplicationService()
        {
            this.movieStars = new List<MovieStar>();
            this.netSalary = string.Empty;
        }

        public void Run()
        {
            while (true)
            {
                var userChoice = DisplayMenu();

                switch (userChoice)
                {
                    case 1:
                        GetAllStars();
                        PrintAllStars();
                        break;
                    case 2:
                        ReadSalary();
                        PrintSalary();
                        break;
                    case 3:
                        Console.WriteLine("Have a nice day !");
                        return;
                    default:
                        Console.WriteLine("ERROR !!!");
                        break;
                }
            }
        }

        private int DisplayMenu()
        {
            var menu = new StringBuilder();
            menu.AppendLine("Main Menu");
            menu.AppendLine("_________");
            menu.AppendLine("");
            menu.AppendLine("1. View Movie Stars List");
            menu.AppendLine("2. Calculate Net Salary");
            menu.AppendLine("3. Exit");

            Console.WriteLine(menu.ToString().Trim());

            var input = Console.ReadLine();

            if (!int.TryParse(input, out _))
            {
                Console.WriteLine("Invalid input!!");
                return 0;
            }
            else
            {
                var inputChoice = int.Parse(input);
                return inputChoice;
            }
        }

        public void GetAllStars()
        {
            var inputData = File.ReadAllText("input.txt");
            dynamic data = JsonConvert.DeserializeObject(inputData);

            foreach (var movieStar in data)
            {
                movieStars.Add(new MovieStar()
                {
                    Name = movieStar.Name,
                    Surname = movieStar.Surname,
                    Email = movieStar.Email,
                    Sex = movieStar.Sex,
                    Nationality = movieStar.Nationality,
                    DateOfBirth = DateTime.Parse(movieStar.dateOfBirth.ToString())
                });
            }
        }

        public void PrintAllStars()
        {
            var allStars = new StringBuilder();

            foreach (var movieStar in movieStars)
            {
                allStars.AppendLine($"{movieStar.Name} {movieStar.Sex}");
                allStars.AppendLine($"{movieStar.Sex}");
                allStars.AppendLine($"{movieStar.Nationality}");
                allStars.AppendLine($"{DateTime.UtcNow.Year - movieStar.DateOfBirth.Year} years old");
                allStars.AppendLine(" ");
            }

            Console.WriteLine(allStars);
        }

        public void ReadSalary()
        {
            Console.WriteLine();
            Console.WriteLine("Write your name...");
            var PersonName = Console.ReadLine();

            Console.WriteLine("Write your salary...");
            var inputSalary = Console.ReadLine();
            Console.WriteLine();

            if (!decimal.TryParse(inputSalary, out decimal salary))
            {
                throw new Exception("Invalid salary input!");
            }

            var grossSalary = decimal.Parse(inputSalary);

            netSalary = CalculateSalary(PersonName, grossSalary);
        }

        public string CalculateSalary(string PersonName, decimal grossSalary)
        {
            var result = string.Empty;

            decimal taxableIncome = grossSalary - 1000m;

            if (taxableIncome <= 0)
            {
                result = $"{PersonName} net salary is: {grossSalary}";
                return result;
            }
            else
            {
                decimal incomeTax = 0.1m * taxableIncome;
                decimal socialTax = 0.15m * taxableIncome;

                if (socialTax > 2000)
                {
                    socialTax = 3000 - 0.15m * 1000;
                }

                decimal totalSum = incomeTax + socialTax;
                decimal netSalaty = grossSalary - totalSum;

                result = $"{PersonName} net salary is: {netSalaty}";
                return result;
            }
        }

        public void PrintSalary()
        {
            Console.WriteLine(netSalary);
        }
    }
}