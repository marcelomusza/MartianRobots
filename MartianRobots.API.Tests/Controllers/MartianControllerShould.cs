using MartianRobots.API.Controllers;
using MartianRobots.Application.DTOs;
using MartianRobots.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MartianRobots.API.Tests.Controllers
{
    public class MartianControllerShould
    {
        private readonly Mock<IMartianEngine> martianEngineMock;
        private readonly MartianController sut;

        public MartianControllerShould()
        {
            martianEngineMock = new Mock<IMartianEngine>();
            sut = new MartianController(martianEngineMock.Object);
        }

        [Fact]
        public void MartianController_Input_ReturnOK()
        {
            //Arrange
            var userInput = new UserInputDTO();
            var userOutput = new UserOutputDTO();
            userInput.GridCoordinates = new GridCoordinatesDTO { X = 1, Y = 1 };
            userInput.RobotOperations = new List<RobotDTO>();
            userInput.RobotOperations.Add( new RobotDTO { Name = "Test Robot" } );
            userOutput.RobotResult = new List<RobotDTO>();
            userOutput.RobotResult.Add(new RobotDTO { Name = "Test Robot Output" });
            martianEngineMock.Setup(x => x.OperateRobotsOnGrid(userInput)).Returns(userOutput);

            //Act
            IActionResult result = sut.Input(userInput);

            //Assert 
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void MartianController_Input_ReturnBadRequest()
        {
            //Arrange
            var userInput = new UserInputDTO();
            UserOutputDTO userOutput = null;
            userInput.GridCoordinates = new GridCoordinatesDTO { X = 1, Y = 1 };
            userInput.RobotOperations = new List<RobotDTO>();
            userInput.RobotOperations.Add(new RobotDTO { Name = "Test Robot" });
            martianEngineMock.Setup(x => x.OperateRobotsOnGrid(userInput)).Returns(userOutput);

            //Act
            IActionResult result = sut.Input(userInput);

            //Assert 
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
