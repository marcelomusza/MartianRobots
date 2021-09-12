using MartianRobots.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Application.DTOs
{
    public class Robot
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public string Orientation { get; set; }
        public string Instructions { get; set; }
        public string Status { get; set; }
    }
}
