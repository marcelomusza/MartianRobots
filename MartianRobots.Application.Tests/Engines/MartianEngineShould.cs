using AutoMapper;
using MartianRobots.Application.DTOs;
using MartianRobots.Application.Engines;
using MartianRobots.Application.Interfaces;
using MartianRobots.Core.Entities;
using MartianRobots.Core.Interfaces;
using MartianRobots.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MartianRobots.Application.Tests.Engines
{
    public class MartianEngineShould
    {
        private readonly Mock<IUnitOfWork> uowMock;
        private readonly Mock<ILogger> loggerMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly MartianEngine sut;
        private readonly MartianRobotsDBContext context;

        public MartianEngineShould()
        {
            uowMock = new Mock<IUnitOfWork>();
            loggerMock = new Mock<ILogger>();
            mapperMock = new Mock<IMapper>();
            sut = new MartianEngine(loggerMock.Object, uowMock.Object, mapperMock.Object);
        }

        private static DbContextOptions<MartianRobotsDBContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<MartianRobotsDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString(), new InMemoryDatabaseRoot())
                .Options;

            return builder;
        }

        [Fact]
        public void MartianEngine_Input_RobotSampleOne_ReturnOutput()
        {

            using (var context = new MartianRobotsDBContext(CreateNewContextOptions()))
            {
                //Arrange
                var userInput = new UserInputDTO();
                var userOutput = new RobotDTO { 
                        Name = "Test Robot",
                        Position = new PositionDTO { X = 1, Y = 1 },
                        Orientation = 'E'
                    };
                var rm = new RobotMovements();

                userInput.GridCoordinates = new GridCoordinatesDTO { X = 5, Y = 3 };
                userInput.RobotOperations = new List<RobotDTO>();
                userInput.RobotOperations.Add(
                    new RobotDTO
                    {
                        Name = "Test Robot",
                        Instructions = "RFRFRFRF",
                        Orientation = 'E',
                        Position = new PositionDTO { X = 1, Y = 1 }
                    }
                );                

                rm.Orientation = 'E';
                rm.Position = new Position { X = 1, Y = 1 };
                rm.Robot = new Robot { Name="Test Robot"};
                rm.GridCoordinates = new GridCoordinate { X = 5, Y = 3 };

                uowMock.Setup(x => x.RobotMovements.GetRobotByName("Test Robot")).Returns((RobotMovements)null);
                uowMock.Setup(x => x.RobotMovements.Add(rm));
                SeedInMemoryDatabase(rm, context);

                //Act
                var result = sut.OperateRobotsOnGrid(userInput);
                var resultingRobot = result.RobotResult.Find(x => x.Name.Equals("Test Robot"));

                //Assert 
                Assert.Equal(userOutput.Name, resultingRobot.Name);
            }

        }

        /*
        SIMILARLY TESTS TO BE CREATED FOR THE REST OF THE ROBOT OPERATIONS AND COMBINATIONS
        */


        private void SeedInMemoryDatabase(RobotMovements rm, MartianRobotsDBContext context)
        {
            context.Add(rm);
            context.SaveChanges();
        }


    }
}
