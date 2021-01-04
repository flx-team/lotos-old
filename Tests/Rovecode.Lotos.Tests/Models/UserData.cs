using System;
using Rovecode.Lotos.Models;

namespace Rovecode.Lotos.Tests.Models
{
    public record UserData : StorageData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public decimal Phone { get; set; }
    }
}
