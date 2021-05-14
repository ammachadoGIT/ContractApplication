using System.Collections.Generic;
using System.Threading.Tasks;
using ContractApplication.Models;
using ContractApplication.Services.Interfaces;

namespace ContractApplication.Services
{
    public class ContractorService : ServiceBase, IContractorService
    {
        private readonly ApplicationDbContext context;

        public ContractorService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ContractorDto> ListContractors()
        {
            var contractors = this.context.Contractors;
            return this.Mapper.Map<IEnumerable<ContractorDto>>(contractors);
        }

        public async Task<ContractorDto> GetContractorByIdAsync(int id)
        {
            var contractor = await this.context.Contractors.FindAsync(id);
            return this.Mapper.Map<ContractorDto>(contractor);
        }

        public async Task<ContractorDto> CreateContractor(ContractorDto contractorDto)
        {
            var contractor = this.Mapper.Map<Contractor>(contractorDto);
            contractor.AssignHealthStatus();

            await this.context.Contractors.AddAsync(contractor);
            await this.context.SaveChangesAsync();

            return this.Mapper.Map<ContractorDto>(contractor);
        }
    }
}