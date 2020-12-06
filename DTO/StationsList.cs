using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DTO
{
    public class StationsList
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual List<Station> Features { get; set; }
    }
}
