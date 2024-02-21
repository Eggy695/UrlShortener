using AutoMapper;
using Url = Entities.Models.UrlManagement;
using Shared.DataTransferObjects;

namespace UrlShortener
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Url, UrlManagmentDto>();
             
            CreateMap<UrlForShortUrlCreationDto, Url>();
        }
    }
}
