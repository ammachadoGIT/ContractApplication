using System.Collections.Generic;
using System.Threading.Tasks;
using ContractApplication.Models;

namespace ContractApplication.Services.Interfaces
{
    public interface IContractService
    {
        IEnumerable<ContractDto> ListContracts();

        ContractDto GetContract(int idFrom, int idTo);

        Task CreateContract(ContractDto contractDto);

        Task DeleteContract(ContractDto contractDto);
    }
}
