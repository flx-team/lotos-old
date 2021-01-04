﻿using System;
using System.Linq;
using Rovecode.Lotos.Factories;
using Rovecode.Lotos.Models;
using Rovecode.Lotos.Exceptions;

namespace ConsoleAppExample
{
    record UserData : StorageData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Test
    {
        public Test(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }

        public string Name { get; }
        public DateTime Date { get; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var context = LotosConnectFactory.Connect("mongodb://localhost:27017", "LotosTest");

            var usersStorage = context.Get<UserData>();

            var user = usersStorage.Keep(new() { FirstName = "Roman", LastName = "S" });

            using (user)
            {
                user.Data.LastName += new Random().Next(0, 100).ToString();
            }

            // user.Burn();

            Console.WriteLine($"{user.Data.FirstName} / {user.Data.LastName}");
            Console.WriteLine();

            var users = usersStorage.SearchMany(e => e.FirstName == "Roman");

            foreach (var item in users)
            {
                Console.WriteLine($"{item.Data.FirstName} / {item.Data.LastName}");
            }

            if (users.Count() > 10)
            {
                usersStorage.BurnMany(e => e.FirstName == "Roman");
            }

            Console.ReadLine();
            Console.Clear();

            Main(args);
        }
    }
}