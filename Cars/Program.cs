using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cars
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            #region multiple sorting and ordering.
            var sort = cars.OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name);                   // Secondary Sort
                                                                    // if it is three another or Terciary Sort ThenBy Linq Sort operator.
            #endregion

            #region Query Syntax for above Extension method syntax
            var qry =
                from car in cars
                orderby car.Combined ascending, car.Name ascending
                select car;
            #endregion

            #region Projecting data with Select
            var select =
                from car in cars
                where car.Manufacturer == "BMW" && car.Year == 2006
                orderby car.Combined descending, car.Name descending
                select new
                {
                    car.Manufacturer,
                    car.Name,
                    car.Combined
                };
            var resultSelect = cars.Select(c => new
            {
                c.Manufacturer,
                c.Name,
                c.Combined
            });
            //foreach (var car  in select)
            //{
            //    Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined} ");
            //}
            #endregion Projecting data with Select

            #region Joining data with Query Syntax
            var query =
                from car in cars
                join manufacturer in manufacturers
                        on car.Manufacturer equals manufacturer.Name
                orderby car.Combined descending, car.Name ascending
                select new                                              // it produces and IEnumerable of Anonymous types.
                {
                    manufacturer.Headquarters,
                    car.Name,
                    car.Combined
                };

            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            //}
            #endregion

            #region second version of above query syntax using Extension methods Syntax.
            var query2 =
                cars.Join(manufacturers,
                            c => c.Manufacturer,
                            m => m.Name, (c, m) => new
                            {
                                m.Headquarters,
                                c.Name,
                                c.Combined
                            })
                    .OrderByDescending(c => c.Combined)
                    .ThenBy(c => c.Name);
            foreach (var car in query2.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }                 
            #endregion
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                .Where(l => l.Length > 1)
                .Select(l =>                           // project with the .select operator each line into the manufacturer.
                {
                    var columns = l.Split(',');
                    return new Manufacturer
                    {
                        Name = columns[0],
                        Headquarters = columns[1],
                        Year = int.Parse(columns[2])
                    };
                });
            return query.ToList();
        }

        private static List<Car> ProcessFile(string path)
        {
            // Option 1
            //return
            //    File.ReadAllLines(path)
            //        .Skip(1)
            //        .Where(line => line.Length > 1)
            //        .Select(Car.ParseFromCsv)                            //.Select(TransformToCar) instead of having a static method inside of my program, I could delegate that responsibility to the car's class itself. Let's call a method on the car's class
            //        .ToList();

            // Option 2 Query Syntax
            //var query =
            //    from line in File.ReadAllLines(path).Skip(1)
            //    where line.Length > 1
            //    select Car.ParseFromCsv(line);                          // Projection
            // I just have to have a ToList operation, unfortunately there's no key for ToList, so I can either wrap this query in Parenthesis at the end.
            // or store this in a variable  var query = Query Expression, 
            //return query.ToList();                                      // and then return the result of that query, and put it into a List, return Query.ToList();


            // Option 3 with a Custom Linq Operator using Extension Method.
            var query =
                File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToCar();

            return query.ToList();
        }

    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}