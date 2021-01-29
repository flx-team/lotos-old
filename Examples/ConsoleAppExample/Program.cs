using System;
using System.Linq;
using Rovecode.Lotos.Factories;
using Rovecode.Lotos.Exceptions;
using System.Collections.Generic;
using Rovecode.Lotos.Containers;
using Rovecode.Lotos.Entities;

namespace ConsoleAppExample
{
    public class UserEntity : StorageEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var context = LotosConnectFactory.Connect("mongodb://localhost:27017,127.0.0.1:27018/?replicaSet=rs0", "horny");

            var container = context.CreateContainer();

            container.Sandbox(e =>
            {
                var users = e.GetStorage<UserEntity>();

                var user = users.Put(new()
                {
                    FirstName = "Roman",
                    LastName = "S",
                });

                throw new Exception();

                user.Value.LastName = "Suslikov";

                user.Push();
            });
        }
    }
}
