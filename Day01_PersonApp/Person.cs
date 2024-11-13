using System;

namespace Day01_PersonApp
{
    public class Person
    {
        private string name;
        private int age;
        private string city;

        public string Name
        {
            get => name;
            set
            {
                if (value.Length < 2 || value.Length > 100 || value.Contains(";"))
                    throw new ArgumentException("Name must be 2-100 characters & cannot contain semicolons. ");
                name = value;
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (value < 0 || value > 150)
                    throw new ArgumentException("Age must be between 0-150.");
                age = value;
            }
        }

        public string City
        {
            get => city;
            set
            {
                if (value.Length < 2 || value.Length > 100 || value.Contains(";"))
                    throw new ArgumentException("City must be 2-100 characters & cannot contain semicolons.");
                city = value;
            }
        }

        public override string ToString()
        {
            return $"{Name} is {Age} from {City}";
        }



    }
}