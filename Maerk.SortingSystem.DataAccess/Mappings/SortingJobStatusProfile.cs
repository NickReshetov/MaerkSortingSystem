using AutoMapper;
using Maerk.SortingSystem.DataAccess.EntityFramework.Entities;
using Maerk.SortingSystem.Dtos;

namespace Maerk.SortingSystem.DataAccess.Mappings
{
    public class SortingJobStatusProfile : Profile
    {
        public SortingJobStatusProfile()
        {
            CreateMap<SortingJob, SortingJobStatusDto>().ReverseMap();

            CreateMap<SortingJobDto, SortingJobStatusDto>().ReverseMap();
        }
    }
}
