using System;

namespace Nviromon.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string username {get; set;}

        public byte[] passwordHash {get; set;}

        public byte[] passwordSalt { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime lastLogin { get; set; }
    }
}