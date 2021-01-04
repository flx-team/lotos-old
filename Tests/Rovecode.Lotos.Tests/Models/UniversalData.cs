using System;
using Rovecode.Lotos.Models;

namespace Rovecode.Lotos.Tests.Models
{
    public record UniversalData : StorageData
    {
        public UserData OtherObj { get; set; }

        public int Int { get; set; }

        public double Double { get; set; }

        public string String { get; set; }

        public double? NullDouble { get; set; }
    }
}
