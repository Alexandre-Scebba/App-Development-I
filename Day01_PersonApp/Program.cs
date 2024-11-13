using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace Day01_PersonApp
{
    class Program
    {
        // static field that holds a list of Person objects...
        static List<Person> people = new List<Person>();

        // constant for file path...
        private const string FilePath = @"C:\Users\punk_\source\repos\CSharpClassProjects\Day01_PersonApp\people.txt";

        static void Main()
        {
            ReadAllPeopleFromFile();

            while (true)
            {
                Console.WriteLine("\nWhat do you want to do?");
                Console.WriteLine("1. Add person info");
                Console.WriteLine("2. List all persons info");
                Console.WriteLine("3. Find a person by name");
                Console.WriteLine("4. Find all persons younger than a certain age");
                Console.WriteLine("0. Exit\n");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddPersonInfo();
                        break;
                    case "2":
                        ListAllPersonInfo();
                        break;
                    case "3":
                        FindPersonByName();
                        break;
                    case "4":
                        FindPersonYoungerThan();
                        break;
                    case "0":
                        SaveAllPeopleToFile();
                        Console.WriteLine("\nGoodbye!\n");
                        return;
                    default:
                        Console.WriteLine("\nInvalid choice. Try again.\n");
                        break;

                }
            }
        }

        static void AddPersonInfo()
        {
            try
            {
                Console.Write("\nEnter name: ");
                string name = Console.ReadLine();

                Console.Write("Enter age: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Enter city: ");
                string city = Console.ReadLine();

                Person person = new Person
                {
                    Name = name,
                    Age = age,
                    City = city
                };
                people.Add(person);
                Console.WriteLine("\nPerson added.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Age must be a valid number.");
            }
        }


        static void ListAllPersonInfo()
        {
            Console.WriteLine("\nListing all persons:\n");
            foreach (Person person in people)
            {
                Console.WriteLine(person);
            }
        }

        static void SaveAllPeopleToFile()
        {
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach (Person person in people)
                {
                    writer.WriteLine($"{person.Name};{person.Age};{person.City}");
                }
            }
        }

        static void ReadAllPeopleFromFile()
        {
            if (!File.Exists(FilePath)) return;

            foreach (var line in File.ReadLines(FilePath))
            {
                try
                {
                    // assumes each line is formatted: "Name;Age;City"...
                    var data = line.Split(';');
                    var person = new Person
                    {
                        Name = data[0],
                        Age = int.Parse(data[1]),
                        City = data[2]
                    };
                    people.Add(person);
                }
                catch (Exception)
                {
                    Console.WriteLine($"\nInvalid line in file: {line}. Skipping...");
                }
            }
        }

        static void FindPersonByName()
        {
            try
            {
                Console.Write("\nEnter the name you are searching for: ");
                string inputName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inputName))
                {
                    throw new ArgumentException("\nName cannot be empty.");
                }

                bool found = false;

                foreach (var person in people)
                {
                    // check if person's name contains input (ignore case)
                    if (person.Name.IndexOf(inputName, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Console.WriteLine(person);
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine($"No matches found for {inputName}.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message} ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }


        static void FindPersonYoungerThan()
            {
                try
                {
                    Console.Write("\nEnter upper age limit: ");

                    string inputAge = Console.ReadLine();

                    if (!int.TryParse(inputAge, out int maxAge))
                    {
                        throw new ArgumentException("\nAge must be a valid number.");
                    }

                    bool found = false;

                    foreach (var person in people)
                    {
                        if (person.Age < maxAge)
                        {
                            Console.WriteLine(person);
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine($"No persons found younger than {maxAge}");
                    }
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }
            }




    }
}
