using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using ContractApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractApplication.Tests
{
    public abstract class ServiceTestsBase : TestsBase
    {
        protected ServiceTestsBase(DbContextOptions<ApplicationDbContext> contextOptions)
        {
            this.Setup();

            this.ContextOptions = contextOptions;
            this.Seed();
        }

        protected IEnumerable<Contractor> Contractors { get; private set; }

        protected IEnumerable<Contract> Contracts { get; private set; }

        protected DbContextOptions<ApplicationDbContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new ApplicationDbContext(this.ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                this.Contractors = this.Fixture.Build<Contractor>()
                    .With(c => c.ContractFrom, (ICollection<Contract>)null)
                    .With(c => c.ContractTo, (ICollection<Contract>)null)
                    .Without(c => c.Id)
                    .CreateMany();

                this.Contracts = new List<Contract>
                                     {
                                         new Contract
                                             {
                                                 Contractor1Id = this.Contractors.First().Id,
                                                 Contractor1 = this.Contractors.First(),
                                                 Contractor2Id = this.Contractors.Last().Id,
                                                 Contractor2 = this.Contractors.Last()
                                             }
                                     };

                context.AddRange(this.Contractors);
                context.AddRange(this.Contracts);
                context.SaveChanges();
            }
        }
    }
}
