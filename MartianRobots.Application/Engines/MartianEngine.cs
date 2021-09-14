using AutoMapper;
using MartianRobots.Application.DTOs;
using MartianRobots.Application.Enums;
using MartianRobots.Application.Helpers;
using MartianRobots.Application.Interfaces;
using MartianRobots.Core.Entities;
using MartianRobots.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.Application.Engines
{
    public class MartianEngine : IMartianEngine
    {
        private ILogger logger;
        private IUnitOfWork uow;
        private IMapper mapper;

        private List<RobotScentDTO> robotScents = new List<RobotScentDTO>();
        private GridCoordinatesDTO marsGrid;

        public MartianEngine(ILogger Logger, IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            logger = Logger;
            uow = UnitOfWork;
            mapper = Mapper;
        }

        public UserOutputDTO OperateRobotsOnGrid(UserInputDTO input)
        {
            try
            {
                logger.Log(ConsoleColor.Green, "Starting Robot Operations on Mars");

                marsGrid = new GridCoordinatesDTO { X = input.GridCoordinates.X, Y = input.GridCoordinates.Y };

                //Inserting Robots in their initial positions
                InsertRobotsInTheGrid(input);

                //Move Robots Sequentially
                MoveRobotsInTheField(input);

                //Setting up Output
                var robots = input.RobotOperations;
                UserOutputDTO output = SetupOutput(robots);

                logger.Log(ConsoleColor.Green, "Robot Operations Completed successfully");

                return output;
            }
            catch (Exception ex)
            {
                logger.Log(ConsoleColor.Red, $"Error occurred during Robots Operation process. Error Details: {ex.Message}");
                return null;
            }
            
        }

        private UserOutputDTO SetupOutput(List<RobotDTO> robots)
        {
            UserOutputDTO output = new UserOutputDTO();
            foreach (var r in robots)
            {
                output.RobotResult.Add(
                    new RobotDTO { Name = r.Name, Position = r.Position, Orientation = r.Orientation, Status = r.Status }
                );
            }

            //Sample Output                
            //output.RobotResult.Add(new RobotDTO { Name = "Robot 1", Position = new PositionDTO { X = 1, Y = 1 }, Orientation = 'E', Status = "" });
            //output.RobotResult.Add(new RobotDTO { Name = "Robot 2", Position = new PositionDTO { X = 3, Y = 3 }, Orientation = 'N', Status = "LOST" });
            //output.RobotResult.Add(new RobotDTO { Name = "Robot 3", Position = new PositionDTO { X = 4, Y = 2 }, Orientation = 'N', Status = "" });

            return output;
        }

        private void MoveRobotsInTheField(UserInputDTO input)
        {

            foreach (var robot in input.RobotOperations)
            {
                var instructions = robot.Instructions.ToCharArray();

                //Moving the robot based on the Instructions provided by the User
                foreach (var inst in instructions)
                {
                    switch (inst)
                    {
                        case (char)Enumerations.Instructions.R:
                            TurnRobotOrientation(robot, inst);                            
                            break;
                        case (char)Enumerations.Instructions.L:
                            TurnRobotOrientation(robot, inst);
                            break;
                        case (char)Enumerations.Instructions.F:
                            MoveRobotToNextPosition(robot);
                            break;
                        default:
                            throw new Exception("Invalid Instruction, unable to proceed");
                    }

                    //Save New Position to the DB
                    SaveRobotPosition(robot);
                }
            }

            //Save DeadEnd Positions in DB for Statistic purposes
            if (robotScents.Any())
                SaveRobotScentsInDB(robotScents);
        }

        private void MoveRobotToNextPosition(RobotDTO robot)
        {

            var newPosition = new PositionDTO();

            switch (robot.Orientation)
            {
                case (char)Enumerations.Orientations.N:

                    newPosition = new PositionDTO { X = robot.Position.X, Y = robot.Position.Y + 1 };
                    ExecuteRobotMovement(robot, newPosition, (char)Enumerations.Orientations.N);

                    break;

                case (char)Enumerations.Orientations.E:

                    newPosition = new PositionDTO { X = robot.Position.X + 1, Y = robot.Position.Y };
                    ExecuteRobotMovement(robot, newPosition, (char)Enumerations.Orientations.E);

                    break;

                case (char)Enumerations.Orientations.S:

                    newPosition = new PositionDTO { X = robot.Position.X, Y = robot.Position.Y - 1 };
                    ExecuteRobotMovement(robot, newPosition, (char)Enumerations.Orientations.S);

                    break;

                case (char)Enumerations.Orientations.W:

                    newPosition = new PositionDTO { X = robot.Position.X - 1, Y = robot.Position.Y };
                    ExecuteRobotMovement(robot, newPosition, (char)Enumerations.Orientations.W);

                    break;
            }
        }

        private void ExecuteRobotMovement(RobotDTO robot, PositionDTO newPosition, char orientation)
        {            

            //Validate Robot Scents before making a move
            if (!robotScents.Any(x => x.X == newPosition.X && x.Y == newPosition.Y && x.Orientation == orientation))
            {
                //Robot is safe to move to the new Position   

                robot.Position = newPosition;
                robot.Instruction = (char)Enumerations.Instructions.F;
                
                if (robot.Position.X > marsGrid.X || robot.Position.Y > marsGrid.Y)
                {
                    //Robot crossed Grid limits
                    logger.Log(ConsoleColor.Red, $"Robot: {robot.Name} has crossed Grid Limits on Position X:{newPosition.X} Y:{newPosition.Y}... Robot is LOST");
                    robot.Status = "LOST";
                    var robotScent = new RobotScentDTO { X = newPosition.X, Y = newPosition.Y, Orientation = orientation };

                    SaveRobotScentsInMemory(robotScent);
                }
            }
            else
            {
                //Robot is about to reach an already registered Scent
                //Ignore Instruction and move on with the next      
                logger.Log(ConsoleColor.Yellow, $"Robot: {robot.Name} is about to reach a dead end on Position X:{newPosition.X} Y:{newPosition.Y}... Ignoring Instruction.");
            }
        }

        private void TurnRobotOrientation(RobotDTO robot, char inst)
        {
            switch (inst)
            {
                case (char)Enumerations.Instructions.R:

                    robot.Instruction = (char)Enumerations.Instructions.R;
                    switch (robot.Orientation)
                    {
                        case (char)Enumerations.Orientations.N:
                            robot.Orientation = (char)Enumerations.Orientations.E;
                            break;
                        case (char)Enumerations.Orientations.E:
                            robot.Orientation = (char)Enumerations.Orientations.S;
                            break;
                        case (char)Enumerations.Orientations.S:
                            robot.Orientation = (char)Enumerations.Orientations.W;
                            break;
                        case (char)Enumerations.Orientations.W:
                            robot.Orientation = (char)Enumerations.Orientations.N;
                            break;
                    }

                    break;
                case (char)Enumerations.Instructions.L:

                    robot.Instruction = (char)Enumerations.Instructions.L;
                    switch (robot.Orientation)
                    {
                        case (char)Enumerations.Orientations.N:
                            robot.Orientation = (char)Enumerations.Orientations.W;
                            break;
                        case (char)Enumerations.Orientations.E:
                            robot.Orientation = (char)Enumerations.Orientations.N;
                            break;
                        case (char)Enumerations.Orientations.S:
                            robot.Orientation = (char)Enumerations.Orientations.E;
                            break;
                        case (char)Enumerations.Orientations.W:
                            robot.Orientation = (char)Enumerations.Orientations.S;
                            break;
                    }

                    break;
            }


        }

        private void InsertRobotsInTheGrid(UserInputDTO input)
        {
            foreach (var robot in input.RobotOperations)
            {
                
                //Save Grid Coordinates for reference on each robot
                robot.GridCoordinates = input.GridCoordinates;

                //Save Initial Position to the DB
                SaveRobotPosition(robot);

            }
        }




        private void SaveRobotPosition(RobotDTO robotDTO)
        {
            var existingRobot = uow.RobotMovements.GetRobotByName(robotDTO.Name);

            if (existingRobot != null)
            {
                //Existing Robot -- To Insert new Positions and Instructions for an Existing Robot   

                var position = mapper.Map<PositionDTO, Position>(robotDTO.Position);

                RobotMovements rm = new RobotMovements();
                rm.Orientation = robotDTO.Orientation;
                rm.Instruction = robotDTO.Instruction;
                rm.RobotId = existingRobot.RobotId;                
                rm.GridCoordinatesId = existingRobot.GridCoordinatesId;
                rm.Dead = (robotDTO.Status != null) ? true : false;

                if (MartianHelpers.PositionIsEqualTo(existingRobot.Position, position))
                    rm.PositionId = existingRobot.PositionId;
                else
                    rm.Position = position;

                uow.RobotMovements.Add(rm);
            }
            else
            {
                //New Robot to Add into the Table

                var robot = mapper.Map<RobotDTO, Robot>(robotDTO);
                var position = mapper.Map<PositionDTO, Position>(robotDTO.Position);
                var gridCoords = mapper.Map<GridCoordinatesDTO, GridCoordinate>(robotDTO.GridCoordinates);

                RobotMovements rm = new RobotMovements();
                rm.Orientation = robotDTO.Orientation;
                rm.Position = position;
                rm.Robot = robot;
                rm.GridCoordinates = gridCoords;

                uow.RobotMovements.Add(rm);
            }           
            
            uow.Complete();

            logger.Log(ConsoleColor.Green, $"Robot: {robotDTO.Name} was inserted on Position X:{robotDTO.Position.X} Y:{robotDTO.Position.Y}, Orientation {robotDTO.Orientation}, Instruction {robotDTO.Instruction} and Status {robotDTO.Status}");
        }

        private void SaveRobotScentsInMemory(RobotScentDTO robotScent)
        {
            //Saving Robot Scent Position in Memory
            robotScents.Add(robotScent);
        }

        private void SaveRobotScentsInDB(List<RobotScentDTO> robotScents)
        {
            //Saving Robot Scent Position
            var robotScentsToSave = mapper.Map<List<RobotScentDTO>, List<RobotScent>>(robotScents);
            uow.DeadEnds.AddRange(robotScentsToSave);
            uow.Complete();
        }

        public List<RobotMovementsOutputDTO> GetRobotMovements()
        {
            try
            {
                var listOfRobotMovementsFromDB = uow.RobotMovements.GetListOfRobots();

                if (!listOfRobotMovementsFromDB.Any())
                    throw new Exception("Robot Movements not available");

                var robotMovementsOutput = mapper.Map<List<RobotMovements>, List<RobotMovementsOutputDTO>>(listOfRobotMovementsFromDB);

                return robotMovementsOutput;
            }
            catch (Exception ex)
            {
                logger.Log(ConsoleColor.Red, $"Error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
