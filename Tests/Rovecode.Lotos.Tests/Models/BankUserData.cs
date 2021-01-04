using System;
namespace Rovecode.Lotos.Tests.Models
{
    public record BankUserData : UserData
    {
        public double Balance { get; set; }
    }
}
