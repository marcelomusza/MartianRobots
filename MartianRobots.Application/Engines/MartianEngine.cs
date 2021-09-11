using MartianRobots.Application.DTOs;
using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Models;
using System;

namespace MartianRobots.Application.Engines
{
    public class MartianEngine : IMartianEngine
    {
        private ILogger logger;
        public MartianEngine(ILogger Logger)
        {
            logger = Logger;
        }

        public UserOutput OperateRobotsOnGrid(UserInput input)
        {
            try
            {
                logger.Log(ConsoleColor.Green, "Starting Robot Operations on Mars");

                //Starting Grid



                //Sample Output
                UserOutput output = new UserOutput();
                output.RobotResult.Add(new RobotOperations { Name = "Robot 1", Position = new Position { X = 1, Y = 1 }, Orientation = "E", Status = "" });
                output.RobotResult.Add(new RobotOperations { Name = "Robot 2", Position = new Position { X = 3, Y = 3 }, Orientation = "N", Status = "LOST" });
                output.RobotResult.Add(new RobotOperations { Name = "Robot 3", Position = new Position { X = 4, Y = 2 }, Orientation = "N", Status = "" });


                return output;
            }
            catch (Exception ex)
            {
                logger.Log(ConsoleColor.Red, $"Error occurred during Robots Operation process. Error Details: {ex.Message}");
                return null;
            }
            
        }
    }
}
