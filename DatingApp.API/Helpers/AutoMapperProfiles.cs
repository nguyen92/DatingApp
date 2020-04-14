
using System.Linq;
using AutoMapper;
using DatingApp.API.DTOS;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
     public AutoMapperProfiles()
     {
         CreateMap<User, UserForListDto>().ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault().Url));
         CreateMap<User, UserForDetailDto>().ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x =>x.IsMain).Url));
         CreateMap<Photo, PhotosForDto>();
         
         CreateMap<UserForUpdateDto, User>();
         CreateMap<PhotoForCreationDto, Photo>();
        
        
     }

    }
}