using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContractApplication.Exceptions;
using ContractApplication.Models;
using ContractApplication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContractApplication.Services
{
    public class ContractService : ServiceBase, IContractService
    {
        private readonly ApplicationDbContext context;

        public ContractService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ContractDto> ListContracts()
        {
            var contracts = this.context.Contracts.Include(c => c.Contractor1).Include(c => c.Contractor2);
            return this.Mapper.Map<IEnumerable<ContractDto>>(contracts);
        }

        public ContractDto GetContract(int idFrom, int idTo)
        {
            var contract = this.context.Contracts.FindAsync(new Contract() { Contractor1Id = idFrom, Contractor2Id = idTo });
            return this.Mapper.Map<ContractDto>(contract);
        }

        public async Task CreateContract(ContractDto contractDto)
        {
            this.ValidateContractDto(contractDto);

            var contract = this.Mapper.Map<Contract>(contractDto);
            await this.context.Contracts.AddAsync(contract);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteContract(ContractDto contractDto)
        {
            if (!this.ContractExists(contractDto))
            {
                return;
            }

            var contract = this.Mapper.Map<Contract>(contractDto);
            this.context.Contracts.Remove(contract);
            await this.context.SaveChangesAsync();
        }

        private void ValidateContractDto(ContractDto contractDto)
        {
            if (contractDto.IsSelfContract())
            {
                throw new SelfContractException();
            }

            if (this.ContractExists(contractDto))
            {
                throw new DuplicateContractException();
            }
        }

        private bool ContractExists(ContractDto contractDto)
        {
            return this.context.Contracts.Any(e => e.Contractor1Id == contractDto.Contractor1Id && e.Contractor2Id == contractDto.Contractor2Id) ||
                   this.context.Contracts.Any(e => e.Contractor1Id == contractDto.Contractor2Id && e.Contractor2Id == contractDto.Contractor1Id);
        }
    }
}