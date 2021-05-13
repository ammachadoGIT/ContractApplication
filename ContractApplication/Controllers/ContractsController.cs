using System.Collections.Generic;
using System.Threading.Tasks;
using ContractApplication.Exceptions;
using ContractApplication.Models;
using ContractApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContractApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService contractService;

        public ContractsController(IContractService contractService)
        {
            this.contractService = contractService;
        }

        [HttpGet]
        public IEnumerable<ContractDto> ListContracts()
        {
            return this.contractService.ListContracts();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContract([FromBody] ContractDto contractDto)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                await this.contractService.CreateContract(contractDto);
                return this.CreatedAtAction(nameof(this.CreateContract), contractDto);
            }
            catch (SelfContractException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (DuplicateContractException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContract(ContractDto contractDto)
        {
            await this.contractService.DeleteContract(contractDto);
            return this.NoContent();
        }
    }
}