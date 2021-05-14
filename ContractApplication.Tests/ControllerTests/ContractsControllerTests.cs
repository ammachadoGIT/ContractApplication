using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using ContractApplication.Controllers;
using ContractApplication.Exceptions;
using ContractApplication.Models;
using ContractApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace ContractApplication.Tests.ControllerTests
{
    public class ContractsControllerTests : TestsBase
    {
        private ContractsController sut;
        private Mock<IContractService> contractServiceMock;

        [SetUp]
        public new void Setup()
        {
            this.contractServiceMock = new Mock<IContractService>();
            this.sut = new ContractsController(this.contractServiceMock.Object);
        }

        [Test]
        public async Task CreateContract_ValidContract_Success()
        {
            // Arrange
            var contractDto = this.Fixture.Create<ContractDto>();
            this.contractServiceMock.Setup(x => x.CreateContract(contractDto)).Returns(Task.CompletedTask);

            // Act
            await this.sut.CreateContract(contractDto);

            // Assert
            this.contractServiceMock.VerifyAll();
        }

        [Test]
        public void CreateContract_DuplicateContractExceptionThrown_ExceptionThrown()
        {
            // Arrange
            var contractorId = this.Fixture.Create<int>();
            var contract = new ContractDto { Contractor1Id = contractorId, Contractor2Id = contractorId };

            this.contractServiceMock.Setup(x => x.CreateContract(contract)).Throws(new DuplicateContractException());

            // Act
            Should.ThrowAsync<DuplicateContractException>(this.sut.CreateContract(contract));

            // Assert
            this.contractServiceMock.VerifyAll();
        }

        [Test]
        public void CreateContract_SelfContractExceptionThrown_ExceptionThrown()
        {
            // Arrange
            var contractorId = this.Fixture.Create<int>();
            var contract = new ContractDto { Contractor1Id = contractorId, Contractor2Id = contractorId };

            this.contractServiceMock.Setup(x => x.CreateContract(contract)).Throws(new SelfContractException());

            // Act
            Should.ThrowAsync<SelfContractException>(this.sut.CreateContract(contract));

            // Assert
            this.contractServiceMock.VerifyAll();
        }

        [Test]
        public void ListContracts_ServiceReturnsData_Success()
        {
            // Arrange
            var contracts = this.Fixture.CreateMany<ContractDto>().ToList();
            this.contractServiceMock.Setup(x => x.ListContracts()).Returns(contracts);

            // Act
            var results = this.sut.ListContracts();

            // Assert
            this.contractServiceMock.VerifyAll();
            results.ShouldNotBeNull();
            results.Count().ShouldBe(contracts.Count);
        }

        [Test]
        public async Task DeleteContract_ServicePerformsDeletion_Success()
        {
            // Arrange
            var contractDto = this.Fixture.Create<ContractDto>();
            this.contractServiceMock.Setup(x => x.DeleteContract(contractDto)).Returns(Task.CompletedTask);

            // Act
            var result = await this.sut.DeleteContract(contractDto);

            // Assert
            this.contractServiceMock.VerifyAll();
            result.ShouldBeOfType<NoContentResult>();
        }
    }
}