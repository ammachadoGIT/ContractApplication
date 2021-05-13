using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public ICollection<Contract> ContractFrom { get; set; }

        [JsonIgnore]
        public ICollection<Contract> ContractTo { get; set; }

        public void AssignHealthStatus()
        {
            var randomNumber = RandomGenerator.NextDouble();

            var redChance = 0.2;
            var yellowChance = 0.2;

            // TODO: this could be parametrized to be more flexible if the probability for each status changes.
            this.HealthStatus = randomNumber < redChance ? HealthStatus.Red :
                                randomNumber < redChance + yellowChance ? HealthStatus.Yellow : HealthStatus.Green;
        }
    }
}
