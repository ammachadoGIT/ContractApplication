using System.Collections.Generic;
using System.Threading.Tasks;
using ContractApplication.Models;
using ContractApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContractApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorsController : ControllerBase
    {
        private readonly IContractorService contractorService;

        public ContractorsController(IContractorService contractorService)
        {
            this.contractorService = contractorService;
        }

        [HttpGet]
        public IEnumerable<ContractorDto> ListContractors()
        {
            return this.contractorService.ListContractors();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractorById([FromRoute] int id)
        {
            var contractor = await this.contractorService.GetContractorByIdAsync(id);
            return contractor == null
                       ? (IActionResult)this.NotFound()
                       : this.Ok(contractor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContractor([FromBody] ContractorDto contractor)
        {
            var newContractor = await this.contractorService.CreateContractor(contractor);
            return this.CreatedAtAction(nameof(this.CreateContractor), newContractor);
        }

        [HttpGet("shortest-path")]
        public List<ContractorDto> GetShortestPath([FromQuery] int fromId, [FromQuery] int toId)
        {
            var contractors = this.contractorService.ListContractors();
            return this.contractorService.GetShortestPath(fromId, toId, contractors);
        }
    }
}