using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DTO
{
    public class UsersStation
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PortName { get; set; }
        public double TimeDifference { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string BasePort { get; set; }
        public string BasePortName { get; set; }
    }
}