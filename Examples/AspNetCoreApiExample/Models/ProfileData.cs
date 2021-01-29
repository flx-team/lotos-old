using System;
using Rovecode.Lotos.Entities;

namespace AspNetCoreApiExample.Models
{
    public class ProfileEntity : StorageEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int Phone { get; set; }
    }
}
