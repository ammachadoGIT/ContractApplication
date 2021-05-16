using System;
using System.Collections.Generic;
using System.Linq;
using ContractApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumsController : ControllerBase
    {
        [HttpGet("health-status")]
        public IEnumerable<EnumDescription> ListHealthStatus()
        {
            return Enum.GetValues(typeof(HealthStatus)).Cast<HealthStatus>()
                .Select(e => new EnumDescription { Id = (int)e, Name = e.ToString() });
        }

        [HttpGet("contractor-type")]
        public IEnumerable<EnumDescription> ListContractorType()
        {
            return Enum.GetValues(typeof(ContractorType)).Cast<ContractorType>()
                .Select(e => new EnumDescription { Id = (int)e, Name = e.ToString() });
        }
    }
}