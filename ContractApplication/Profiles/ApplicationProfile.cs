using AutoMapper;
using ContractApplication.Models;

namespace ContractApplication.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            this.CreateMap<ContractorDto, Contractor>().ReverseMap();
            this.CreateMap<ContractDto, Contract>()
                .ForMember(dest => dest.Contractor1, opt => opt.Ignore())
                .ForMember(dest => dest.Contractor2, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Contractor1Name, opt => opt.MapFrom(src => src.Contractor1.Name))
                .ForMember(dest => dest.Contractor2Name, opt => opt.MapFrom(src => src.Contractor2.Name));
        }
    }
}
