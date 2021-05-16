using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ContractApplication.Models
{
    public class ContractorDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Address { get; set; }

        [Required]
        [MaxLength(16)]
        public string PhoneNumber { get; set; }

        [Required]
        [EnumDataType(typeof(ContractorType))]
        public ContractorType Type { get; set; }

        [EnumDataType(typeof(HealthStatus))]
        public HealthStatus? HealthStatus { get; set; }

        [JsonIgnore]
        public ICollection<ContractDto> ContractFrom { get; set; }

        [JsonIgnore]
        public ICollection<ContractDto> ContractTo { get; set; }
    }
}
