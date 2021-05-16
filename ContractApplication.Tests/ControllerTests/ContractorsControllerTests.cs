using System.Linq;
using AutoFixture;
using ContractApplication.Controllers;
using ContractApplication.Models;
using ContractApplication.Services.Interfaces;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace ContractApplication.Tests.ControllerTests
{
    public class ContractorsControllerTests : TestsBase
    {
        private ContractorsController sut;
        private Mock<IContractorService> contractorServiceMock;

        private ContractorDto contractor;

        [SetUp]
        public new void Setup()
        {
            this.contractorServiceMock = new Mock<IContractorService>();
            this.sut = new ContractorsController(this.contractorServiceMock.Object);

            this.contractor = this.Fixture.Build<ContractorDto>().Create();
        }

        [Test]
        public void CreateContractor_ValidContractor_Success()
        {
            // Arrange
            
            this.contractorServiceMock.Setup(x => x.CreateContractor(this.contractor));

            // Act
            var result = this.sut.CreateContractor(this.contractor);

            // Assert
            this.contractorServiceMock.VerifyAll();

            result.ShouldNotBeNull();
        }

        [Test]
        public void GetContractorById_ValidId_Success()
        {
            // Arrange
            this.contractorServiceMock.Setup(x => x.GetContractorByIdAsync(this.contractor.Id))
                .ReturnsAsync(this.contractor);

            // Act
            var result = this.sut.GetContractorById(this.contractor.Id);

            // Assert
            this.contractorServiceMock.VerifyAll();

            result.ShouldNotBeNull();
        }

        [Test]
        public void ListContractors_ValidContractor_Success()
        {
            // Arrange
            var contractors = this.Fixture.CreateMany<ContractorDto>().ToList();
            this.contractorServiceMock.Setup(x => x.ListContractors()).Returns(contractors);

            // Act
            var result = this.sut.ListContractors();

            // Assert
            this.contractorServiceMock.VerifyAll();

            result.ShouldNotBeNull();
            result.Count().ShouldBe(contractors.Count);
        }
    }
}