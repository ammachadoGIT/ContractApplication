using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContractApplication.Models
{
    public class Contractor
    {
        private static readonly Random RandomGenerator = new Random();

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

        public ICollection<Contract> ContractFrom { get; set; }

        public ICollection<Contract> ContractTo { get; set; }

        public void AssignHealthStatus()
        {
            var randomNumber = RandomGenerator.NextDouble();

            // TODO: this could be parametrized to be more flexible if the probability for each status changes.
            const double RedChance = 0.2;
            const double YellowChance = 0.2;

            this.HealthStatus = randomNumber < RedChance ? HealthStatus.Red :
                                randomNumber < RedChance + YellowChance ? HealthStatus.Yellow : HealthStatus.Green;
        }
    }
}
