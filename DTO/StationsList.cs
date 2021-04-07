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
        /// <summary>
        /// List with all Features, assigned to a station
        /// </summary>
        public virtual List<Station> Features { get; set; }
    }
}
