using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            var path = Console.ReadLine();

            if(!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                try
                {
                    //Read Input Json and Serialize in JSON Object
                    var queryString = File.ReadAllText(path);
                    Queries queryInput = JsonConvert.DeserializeObject<Queries>(queryString);

                    //Instantiate Query Builder
                    QueryBuilder sqlBuilder = new SQLQueryBuilder(queryInput);
                    sqlBuilder.Construct(Utility.GetEnumerableOfType<IOperator>().ToList());
                    //Display Results
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
