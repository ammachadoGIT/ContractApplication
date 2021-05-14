using Newtonsoft.Json;

namespace ContractApplication.Models
{
    public class ContractDto
    {
        public int Contractor1Id { get; set; }

        public int Contractor2Id { get; set; }

        [JsonIgnore]
        public ContractorDto Contractor1 { get; set; }

        [JsonIgnore]
        public ContractorDto Contractor2 { get; set; }

        public bool IsSelfContract() => this.Contractor1Id == this.Contractor2Id;
    }
}