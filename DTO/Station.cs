using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DTO
{
    public class Station
    {
        public string Type { get; set; }
        public Geometry Geometry { get; set; }
        public Properties Properties { get; set; }
        public List<Events> Events { set; get; }
    }

    public class Geometry
    {
        public string Type { get; set; }
        public List<double> Coordinates { get; set; }
    }

    public class Properties
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool ContinuousHeightsAvailable { get; set; }
        public string Footnote { get; set; }
    }
}
