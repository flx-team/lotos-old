using System;
using Rovecode.Lotos.Models;

namespace AspNetCoreApiExample.Models
{
    public record ProfileData : StorageData
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int Phone { get; set; }
    }
}
