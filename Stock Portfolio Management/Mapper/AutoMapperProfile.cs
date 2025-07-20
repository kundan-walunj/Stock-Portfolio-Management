using AutoMapper;
using Stock_Portfolio_Management.Model;

namespace Stock_Portfolio_Management.Mapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() { 
        CreateMap<Stock,StocksDto>().ReverseMap();  

        }
    }
}
