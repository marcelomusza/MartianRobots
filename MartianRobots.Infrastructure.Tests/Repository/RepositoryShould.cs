using MartianRobots.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MartianRobots.Infrastructure.Tests.Repository
{
    public class RepositoryShould
    {
        private readonly Mock<DbContext> dbContextMock;
        private readonly Mock<DbSet<Test>> dbSetMock;
        private readonly Repository<Test> sut;

        public RepositoryShould()
        {
            dbContextMock = new Mock<DbContext>();
            dbSetMock = new Mock<DbSet<Test>>();
            sut = new Repository<Test>(dbContextMock.Object);
        }

        [Fact]
        public void Repository_Should_Add()
        {
            //Arrange
            var testObject = new Test() { Name = "My Test" };
            dbContextMock.Setup(x => x.Set<Test>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<Test>()));

            //Act
            sut.Add(testObject);

            //Assert
            dbContextMock.Verify(x => x.Set<Test>());
            dbSetMock.Verify(x => x.Add(It.Is<Test>(y => y == testObject)));
        }

        /*
        SIMILARLY TESTS TO BE CREATED FOR THE REST OF THE CRUD OPERATIONS
        */
    }
}
