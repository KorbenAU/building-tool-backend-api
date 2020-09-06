using AutoMapper;

namespace Microservice.Business.Automapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        : this("MyProfile")
        {
        }

        protected AutoMapperProfileConfiguration(string profileName)
        : base(profileName)
        {
            // EXAMPLE:
            //CreateMap<[MyDbEntityName], [MyViewModelName]>();
            //CreateMap<[MyViewModelName], [MyDbEntityName]>();
        }
    }
}
