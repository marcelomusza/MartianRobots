using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Application.Interfaces
{
    public interface ILogger
    {
        void Log(ConsoleColor color, string text);
    }
}
