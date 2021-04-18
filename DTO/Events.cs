using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DTO
{
    public class Events
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        [NotMapped]
        public Station Station { get; set; }
        public string EventType { get; set; }
        public DateTime DateTime { get; set; }
        public double Height { get; set; }
        public bool IsApproimateTime { get; set; }
        public bool IsApproximateHeight { get; set; }
        public bool Filtered { get; set; }
        public DateTime Date { get; set; }
    }
}
