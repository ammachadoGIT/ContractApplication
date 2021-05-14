using System.Collections.Generic;
using System.Threading.Tasks;
using ContractApplication.Models;

namespace ContractApplication.Services.Interfaces
{
    public interface IContractorService
    {
        IEnumerable<ContractorDto> ListContractors();

        Task<ContractorDto> GetContractorByIdAsync(int id);

        Task<ContractorDto> CreateContractor(ContractorDto contractorDto);

        List<int> GetShortestPath(int fromContractor, int toContractor, IEnumerable<ContractorDto> contractorDtos);
    }
}
