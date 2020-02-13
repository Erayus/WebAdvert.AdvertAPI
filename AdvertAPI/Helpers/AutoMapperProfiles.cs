using AdvertApi.Models;
using AdvertAPI.Dtos;
using AutoMapper;

namespace AdvertAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
         public AutoMapperProfiles()
        {
            CreateMap<AdvertForCreationDto, AdvertModel>().ReverseMap();
        }
    }
}