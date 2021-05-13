using System.ComponentModel.DataAnnotations;

namespace ContractApplication.Models
{
    public class ContractorDto
    {
        [Required]
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

        [Required]
        [EnumDataType(typeof(HealthStatus))]
        public HealthStatus HealthStatus { get; set; }
    }
}
