using System;
using System.Linq;
using ContractApplication.Controllers;
using ContractApplication.Models;
using NUnit.Framework;
using Shouldly;

namespace ContractApplication.Tests.ControllerTests
{
    public class EnumControllerTests : TestsBase
    {
        private EnumsController sut;

        [SetUp]
        public new void Setup()
        {
            this.sut = new EnumsController();
        }

        [Test]
        public void ListContractorType_ControllerReturnsData_Success()
        {
            // Act
            var results = this.sut.ListContractorType();

            // Assert
            results.ShouldNotBeNull();
            results.Count().ShouldBe(Enum.GetValues(typeof(ContractorType)).Length);
        }

        [Test]
        public void ListHealthStatus_ControllerReturnsData_Success()
        {
            // Act
            var results = this.sut.ListHealthStatus();

            // Assert
            results.ShouldNotBeNull();
            results.Count().ShouldBe(Enum.GetValues(typeof(HealthStatus)).Length);
        }
    }
}