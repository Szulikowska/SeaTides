using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DTO
{
    public class Station
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Type { get; set; }
        [ForeignKey("Geometry")]
        [JsonIgnore]
        public int GeometryId { get; set; }
        public Geometry Geometry { get; set; }
        [ForeignKey("Properties")]
        [JsonIgnore]
        public int PropertiesId { get; set; }
        public Properties Properties { get; set; }
        [JsonIgnore]
        public List<Events> Events { set; get; }
    }

    public class Geometry
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Type { get; set; }
        [NotMapped]
        public List<double> Coordinates { get; set; }
        [JsonIgnore]
        public double X 
        {
            get => Coordinates[0];
            set
            {
                X = Coordinates[0];
            }
        }
        [JsonIgnore]
        public double Y
        {
            get => Coordinates[1];
            set
            {
                Y = Coordinates[1];
            }
        }
    }


    public class Properties
    {
        [JsonIgnore]
        [Key]
        public int PropId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool ContinuousHeightsAvailable { get; set; }
        public string Footnote { get; set; }
    }
}
