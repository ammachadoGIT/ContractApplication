namespace ContractApplication.Models
{
    public class Contract
    {
        public int Contractor1Id { get; set; }

        public int Contractor2Id { get; set; }

        public Contractor Contractor1 { get; set; }

        public Contractor Contractor2 { get; set; }

        public bool IsSelfContract() => this.Contractor1Id == this.Contractor2Id;
    }
}