using AutoMapper;
using ContractApplication.Models;

namespace ContractApplication.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            this.CreateMap<ContractorDto, Contractor>().ReverseMap();
            this.CreateMap<ContractDto, Contract>().ReverseMap();
        }
    }
}
