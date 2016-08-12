using System;
using System.Collections.Generic;
using System.Linq;

namespace Features
{
    public class Program
    {
        static void Main(string[] args)
        {

            // Sample Query Syntax
            string[] cities = { "Boston", "Los Angeles", "Seattle", "London", "Hyderabad" };

            //IEnumerable<string> filteredCities = from city in cities
            //                                     where city.StartsWith("L") && city.Length < 15
            //                                     orderby city
            //                                     select city;
            //or
            //var filteredCities = from city in cities
            //                                     where city.StartsWith("L") && city.Length < 15
            //                                     orderby city
            //                                     select city;


            // this collection is an array of Employee
            //Employee[] developers = new Employee[]        // implicit Typing replace only on local variables.
            var developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 2, Name = "Chris" }
            };

            // this collection is a List of Employee
            //List<Employee> sales = new List<Employee>()
            var sales = new List<Employee>()                    // Implicit Typing.
            {
                new Employee { Id = 3, Name = "Alex" }
            };

            Console.WriteLine(developers.Count());
            foreach (var item in developers)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
