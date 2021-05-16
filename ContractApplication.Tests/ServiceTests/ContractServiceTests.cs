using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using ContractApplication.Models;
using ContractApplication.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace ContractApplication.Tests.ServiceTests
{
    public class ContractServiceTests : ServiceTestsBase
    {
        public ContractServiceTests()
            : base(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite("Filename=ContractApplicationTests.db")
                .Options)   
        {
        }

        [Test]
        public async Task ListAndCreateAndDeleteContract_ContextReturnsData_Success()
        {
            var newContractDto = this.Fixture.Build<ContractDto>()
                .With(c => c.Contractor1Id, this.Contractors.ElementAt(1).Id)
                .With(c => c.Contractor2Id, this.Contractors.ElementAt(2).Id)
                .Create();

            // count initial number of contracts
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                var sut = new ContractService(context);
                var results = sut.ListContracts();

                results.ShouldNotBeNull();
                results.Count().ShouldBe(this.Contracts.Count());
            }

            // create a new contract
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                var sut = new ContractService(context);
                await sut.CreateContract(newContractDto);
            }

            // count number of contracts, it should be one more than the initial number
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                var sut = new ContractService(context);
                var results = sut.ListContracts();

                results.ShouldNotBeNull();
                results.Count().ShouldBe(this.Contracts.Count() + 1);
            }

            // delete one contract
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                var sut = new ContractService(context);
                await sut.DeleteContract(newContractDto);
            }

            // list contracts again, count should be the same as the initial number
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                var sut = new ContractService(context);
                var results = sut.ListContracts();

                results.Count().ShouldBe(this.Contracts.Count());
            }
        }
    }
}
