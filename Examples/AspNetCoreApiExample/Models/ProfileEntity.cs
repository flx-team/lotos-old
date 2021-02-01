using System;
using Rovecode.Lotos.Entities;

namespace AspNetCoreApiExample.Models
{
    public sealed class ProfileEntity : StorageEntity<ProfileEntity>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int Phone { get; set; }

        public void IncrementPhone()
        {
            Phone++;
        }
    }
}
