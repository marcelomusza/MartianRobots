using MartianRobots.Application.Interfaces;
using MartianRobots.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Application.Utilities
{
    public class Logger : ILogger
    {
        public void Log(ConsoleColor color, string message)
        {            
            Console.ForegroundColor = color;
            Console.WriteLine(DateTime.Now + " - " + message);
            Console.ResetColor();
        }
        

    }
}
