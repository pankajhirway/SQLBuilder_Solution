using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLBuilder.models;

namespace SQLBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Absolute Path to JSON: ");
            var path = Console.ReadLine();//"/Users/pankaj.hirway@avalara.com/Projects/SQLBuilder/SQLBuilder/input.json";

            if(!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                try
                {
                    var queryString = File.ReadAllText(path);
                    Queries queryInput = JsonConvert.DeserializeObject<Queries>(queryString);
                    SQLQueryConstructor queryConstructor = new SQLQueryConstructor();
                    QueryBuilder sqlBuilder = new SQLQueryBuilder(queryInput);
                    queryConstructor.Construct(sqlBuilder, SQLOperators.Operators);
                    Console.WriteLine(sqlBuilder.QueryString);
                    Console.ReadLine();
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                Console.Write("Please enter a valid path");
                Environment.Exit(1);
            }
        }
    }
}
