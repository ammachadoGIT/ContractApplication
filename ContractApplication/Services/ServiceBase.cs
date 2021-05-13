using AutoMapper;
using ContractApplication.Profiles;

namespace ContractApplication.Services
{
    public abstract class ServiceBase
    {
        protected ServiceBase()
        {
            this.Mapper = new MapperConfiguration(c => c.AddProfile<ApplicationProfile>()).CreateMapper();
        }

        protected IMapper Mapper { get; set; }
    }
}
