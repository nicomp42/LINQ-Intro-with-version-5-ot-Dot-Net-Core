/*
 * Bill Nicholson
 * nicholdw@ucmail.uc.edu
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using PizzaOrderNamespace;
using VINNamespace;

namespace LINQ_Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo05a();
        }
        /// <summary>
        /// Demonstrate using LINQ to query an array of Strings
        /// </summary>
        public static void Demo01()
        {
            // Data source
            string[] teams = { "Bengals", "Steelers", "Ravens", "Browns" };

            // LINQ Query , note the data type
            var myLINQQuery = from team in teams
                                              orderby team
                                              select team;

            // Query execution and evaluation of results
            foreach (String team in myLINQQuery) {Console.Write(team + " ");}
            Console.WriteLine();                    // Just to seperate result sets

            // LINQ Query , note the data type
            myLINQQuery = from team in teams
                          where team.Length > 6
                          orderby team
                          select team;

            // Query execution and evaluation of results
            foreach (String team in myLINQQuery) { Console.Write(team + " "); }
        }
        /// <summary>
        /// Demonstrate using LINQ to query a text file. Spoiler alert: we read it into an array.
        /// </summary>
        public static void Demo02()
        {
            // Data source is a text file
            //int[][] data = File.ReadAllLines("..\\..\\data.txt").Select(l => l.Split(' ').Select(int.Parse).ToArray()).ToArray();
            int[][] data = File.ReadAllLines(@"../../../data.txt").Select(l => l.Split(' ').Select(int.Parse).ToArray()).ToArray();

            // LINQ Query, note that the query is parsed at compile time. This is a good time to use the 'var' data type, then hover !
            var myLINQQuery = from row in data
                              select row;

            // Query execution
            foreach (int[] row in myLINQQuery)
            {
                Console.WriteLine(row[0] + ", " + row[1] + " ");
            }
        }
        /// <summary>
        /// Demonstrate using LINQ to query a JSON file.
        /// Adapted from https://markheath.net/post/linq-to-general-election-part-1-linq-to 
        /// </summary>
        public static void Demo03()
        {
            // This JSON download has a schema we can investigate
            string url = "https://tools.learningcontainer.com/sample-json.json";
            WebClient wc = new WebClient();
            string json = wc.DownloadString(url);
            JObject jo = JObject.Parse(json);         // Hover over JObject
            Console.WriteLine("JSON data...");
            Console.WriteLine(jo["firstName"].ToString());  // Case-sensitive!
            Console.WriteLine(jo["unknownkey"]);            // This fails quietly
        }
        /// <summary>
        /// Use an API key to access JSON data from openweathermap.org
        /// </summary>
        public static void Demo04()
        {
            String apiKey = @"407d3c698b542dff9b83708c98c93e30";
            string bataviaOHLatLong = "39.0770° N, 84.1769° W";
            // This JSON download has a schema we can investigate
            string currentWeatherURL = "https://api.openweathermap.org/data/2.5/onecall?lat=39.0770&lon=84.1769&exclude=&appid=" + apiKey;
            string url = "http://bulk.openweathermap.org/snapshot/weather_14.json.gz?appid=" + apiKey;
            Console.WriteLine(url);
            Console.WriteLine(currentWeatherURL);
            WebClient wc = new WebClient();
            string json = wc.DownloadString(currentWeatherURL);
            JObject jo = JObject.Parse(json);         // Hover over JObject
            Console.WriteLine("JSON data...");
            Console.WriteLine(jo["timezone"].ToString());  // Case-sensitive!
            Console.WriteLine(jo["current"]["temp"].ToString());
            Console.WriteLine(jo["current"]["weather"][0]["description"].ToString());

            // Select the structure for each ["hourly"] data point
            var myLINQQuery = from JoRow in jo["hourly"]
                              select JoRow;

            // Query execution
            foreach (var row in myLINQQuery)
            {
                //                Console.WriteLine(row);   // This yields everything in each ["hourly"][n] data structure
                Console.WriteLine(row["temp"]);
            }
        }
        /// <summary>
        /// Use .Net (Version 5) libraries to create a JSON representation of an object
        /// </summary>
        public static void Demo05()
        {
            PizzaOrder myPizzaOrder = new PizzaOrder();
            List<String> myToppings = new List<String>();
            myToppings.Add("Mushrooms");
            myToppings.Add("Green Olives");
            myPizzaOrder.toppings = myToppings;
            myPizzaOrder.pizzaSize = PizzaOrder.enumPizzaSize.large;
            myPizzaOrder.crust = "Thick";

            string jsonString = JsonSerializer.Serialize(myPizzaOrder);
            Console.WriteLine(jsonString);
        }
        /// <summary>
        /// Deserialize the JSON string created in Demo05
        /// </summary>
        public static void Demo05a()
        {
            String jsonString = "{\"pizzaSize\":4,\"crust\":\"Thick\",\"toppings\":[\"Mushrooms\",\"Green Olives\"]}";
            PizzaOrder po = new PizzaOrder();
            po = JsonSerializer.Deserialize<PizzaOrder>(jsonString);
            Console.WriteLine(po.ToString());

        }
        /// <summary>
        /// Read CSV file and query with LINQ
        /// </summary>
        public static void Demo06()
        {
            var vins = File.ReadAllLines(@"..\..\..\VINS.csv")
                .Skip(1)
                .Where(row => row.Length > 0)
                .Select(VIN.ParseRow).ToList();

            // Use LINQ to query
            var myLINQQuery = from vin in vins
                              where vin.manu == "Dodge"
                              select vin;

            // Query execution
            foreach (VIN vin in myLINQQuery) {
                Console.WriteLine(vin.licensePlate + ", " + vin.manu + " ");
            }

        }
        /// <summary>
        /// Filter a list of objects with LINQ
        /// Adapted from https://zetcode.com/csharp/filterlist/
        /// </summary>
        public static void Demo07()
        {
            var cars = new List<Car>
            {
                new ("Audi", 52642),
                new ("Mercedes", 57127),
                new ("Skoda", 9000),
                new ("Volvo", 29000),
                new ("Bentley", 350000),
                new ("Citroen", 21000),
                new ("Hummer", 41400),
                new ("Volkswagen", 21601)
            };

            foreach (var car in from car in cars
                                where car.Price > 9000 && car.Price < 50000
                                select new { car.Name, car.Price })
            {
                Console.WriteLine($"{car.Name} {car.Price}");
            }
        }
        public record Car(string Name, int Price);
    }
}
