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
        public virtual Geometry Geometry { get; set; }
        [ForeignKey("Properties")]
        [JsonIgnore]
        public int PropertiesId { get; set; }
        public virtual Properties Properties { get; set; }

        [JsonIgnore]
        public virtual List<Events> Events { set; get; }
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
            get
            {
                return Coordinates[0];
            }
            set
            {
                if (Coordinates == null)
                    Coordinates = new List<double>();
                Coordinates.Add(value);
            }
        }
        [JsonIgnore]
        public double Y
        {
            get
            {
                return Coordinates[1];
            }
            set
            {
                if (Coordinates == null)
                    Coordinates = new List<double>();
                Coordinates.Add(value);
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
