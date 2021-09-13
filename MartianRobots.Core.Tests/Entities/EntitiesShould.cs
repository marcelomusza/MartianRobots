using MartianRobots.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MartianRobots.Core.Tests.Entities
{
    public class EntitiesShould
    {
        [Fact]
        public void Robot_Should_Instantiate()
        {
            //Arrange
            var test = new Robot();

            //Act
            test.Id = 1;
            test.Name = "Test";

            //Assert
            Assert.IsType<Robot>(test);
        }

        /*
        SIMILARLY TESTS TO BE CREATED FOR THE REST OF THE ENTITIES 
        AND NEEDED CORE FEATURES
        */
    }
}
