using System;

namespace Nviromon.Models
{
    public class Readings
    {
        public Guid Id { get; set; }

        public Guid deviceId { get; set; }
        
        public DateTime timestamp { get; set; }

        public int temperature { get; set; }

        public int humidity { get; set; }

        public int uvA { get; set; }
    }
}
