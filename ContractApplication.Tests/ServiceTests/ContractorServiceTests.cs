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
    public class ContractorServiceTests : ServiceTestsBase
    {
        public ContractorServiceTests()
            : base(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite("Filename=ContractApplicationTests.db")
                .Options)
        {
        }

        [Test]
        public async Task ListAndCreateContractor_ContextReturnsData_Success()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                var sut = new ContractorService(context);
                var results = sut.ListContractors();

                results.ShouldNotBeNull();
                results.Count().ShouldBe(this.Contractors.Count());
            }

            using (var context = new ApplicationDbContext(ContextOptions))
            {
                var newContractor = this.Fixture.Build<ContractorDto>()
                    .Without(c => c.Id)
                    .Without(c => c.ContractFrom)
                    .Without(c => c.ContractTo)
                    .Create();

                var sut = new ContractorService(context);
                var result = await sut.CreateContractor(newContractor);

                result.ShouldNotBeNull();
                result.Id.ShouldBeGreaterThan(0);
                result.Name.ShouldBe(newContractor.Name);
            }

            using (var context = new ApplicationDbContext(ContextOptions))
            {
                var sut = new ContractorService(context);
                var results = sut.ListContractors();

                results.ShouldNotBeNull();
                results.Count().ShouldBe(this.Contractors.Count() + 1);
            }
        }

        [Test]
        public async Task GetContractorByIdAsync_ContextReturnsData_Success()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                var sut = new ContractorService(context);
                var id = this.Contractors.First().Id;
                var result = await sut.GetContractorByIdAsync(id);

                result.ShouldNotBeNull();
                result.Id.ShouldBe(this.Contractors.First().Id);
                result.Name.ShouldBe(this.Contractors.First().Name);
            }
        }
    }
}
