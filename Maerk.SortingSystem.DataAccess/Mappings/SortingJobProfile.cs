using AutoMapper;
using Maerk.SortingSystem.Common.Extensions;
using Maerk.SortingSystem.DataAccess.EntityFramework.Entities;
using Maerk.SortingSystem.Dtos;
using Newtonsoft.Json;

namespace Maerk.SortingSystem.DataAccess.Mappings
{
    public class SortingJobProfile : Profile
    {
        public SortingJobProfile()
        {
            CreateMap<SortingJob, SortingJobDto>()
                .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(s => s.TimeStamp.ToUnixTimeSeconds()))                
                .ForMember(dest => dest.Input, opt => opt.MapFrom(s => JsonConvert.DeserializeObject(s.Input)))
                .ForMember(dest => dest.Output, opt => opt.MapFrom(s => JsonConvert.DeserializeObject(s.Output)));

            CreateMap<SortingJobDto,SortingJob>()
                .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(s => s.TimeStamp.ToDateTime()))                
                .ForMember(dest => dest.Input, opt => opt.MapFrom(s => JsonConvert.SerializeObject(s.Input)))
                .ForMember(dest => dest.Output, opt => opt.MapFrom(s => JsonConvert.SerializeObject(s.Output)));

        }
    }
}
