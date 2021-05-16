using System.Collections.Generic;
using System.Threading.Tasks;
using ContractApplication.Models;

namespace ContractApplication.Services.Interfaces
{
    public interface IContractService
    {
        IEnumerable<ContractDto> ListContracts();

        Task CreateContract(ContractDto contractDto);

        Task DeleteContract(ContractDto contractDto);

        Task<ContractDto> GetContractByIdsAsync(int id1, int id2);
    }
}
