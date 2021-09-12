using MartianRobots.Application.DTOs;
using MartianRobots.Application.Helpers;
using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Models;
using MartianRobots.Core.Interfaces;
using System;

namespace MartianRobots.Application.Engines
{
    public class MartianEngine : IMartianEngine
    {
        private ILogger logger;
        private IUnitOfWork uow;
        public MartianEngine(ILogger Logger, IUnitOfWork UnitOfWork)
        {
            logger = Logger;
            uow = UnitOfWork;
        }

        public UserOutput OperateRobotsOnGrid(UserInput input)
        {
            try
            {
                logger.Log(ConsoleColor.Green, "Starting Robot Operations on Mars");

                //Starting and Initializing the Mars Field
                var marsField = new GridCell[input.GridCoordinates.X + 1, input.GridCoordinates.Y + 1];
                marsField = MartianHelpers.InitializeMarsField(marsField);

                //Inserting Robots in their initial positions
                InsertRobotsInTheField(marsField, input);



                //Sample Output
                UserOutput output = new UserOutput();
                output.RobotResult.Add(new Robot { Name = "Robot 1", Position = new Position { X = 1, Y = 1 }, Orientation = "E", Status = "" });
                output.RobotResult.Add(new Robot { Name = "Robot 2", Position = new Position { X = 3, Y = 3 }, Orientation = "N", Status = "LOST" });
                output.RobotResult.Add(new Robot { Name = "Robot 3", Position = new Position { X = 4, Y = 2 }, Orientation = "N", Status = "" });


                return output;
            }
            catch (Exception ex)
            {
                logger.Log(ConsoleColor.Red, $"Error occurred during Robots Operation process. Error Details: {ex.Message}");
                return null;
            }
            
        }

        private void InsertRobotsInTheField(GridCell[,] marsField, UserInput input)
        {
            foreach (var robot in input.RobotOperations)
            {
                if(robot.Position.X <= marsField.GetLength(0) && robot.Position.Y <= marsField.GetLength(1))
                {
                    marsField[robot.Position.X, robot.Position.Y].Robot.Name = robot.Name;
                    marsField[robot.Position.X, robot.Position.Y].Robot.Orientation = robot.Orientation;
                    marsField[robot.Position.X, robot.Position.Y].Robot.Instructions = robot.Instructions;
                    marsField[robot.Position.X, robot.Position.Y].Robot.Position = robot.Position;
                }                
            }
        }
    }
}
