using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Events
    {
        public string EventType { get; set; }
        public DateTime DateTime { get; set; }
        public double Height { get; set; }
        public bool IsApproimateTime { get; set; }
        public bool IsApproximateHeight { get; set; }
        public bool Filtered { get; set; }
        public DateTime Date { get; set; }
    }
}
