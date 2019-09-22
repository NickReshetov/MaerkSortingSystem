using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Maerk.SortingSystem.DataAccess.EntityFramework;
using Maerk.SortingSystem.DataAccess.EntityFramework.Entities;
using Maerk.SortingSystem.DataAccess.Repositories.Interfaces;
using Maerk.SortingSystem.Dtos;

namespace Maerk.SortingSystem.DataAccess.Repositories
{
    public class SortingJobRepository : ISortingJobRepository
    {
        private readonly MaerskSortingSystemDbContext _context = new MaerskSortingSystemDbContextFactory().CreateDbContext();
        private readonly IMapper _mapper;

        public SortingJobRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public SortingJobDto CreateSortingJob(SortingJobDto sortingJob)
        {
            var sortingJobEntity = _mapper.Map<SortingJob>(sortingJob);

            _context.SortingJobs.Add(sortingJobEntity);

            _context.SaveChanges();

            var sortingJobDto = _mapper.Map<SortingJobDto>(sortingJobEntity);

            return sortingJobDto;
        }

        public IEnumerable<SortingJobDto> GetSortingJobs()
        {
            var sortingJobEntities = _context.SortingJobs;

            var sortingJobDtos = _mapper.Map<IEnumerable<SortingJobDto>>(sortingJobEntities);

            return sortingJobDtos;
        }

        public SortingJobDto GetSortingJob(int sortingJobId)
        {
            var sortingJobEntity = GetSortingJobById(sortingJobId);

            var sortingJobDto = _mapper.Map<SortingJobDto>(sortingJobEntity);

            return sortingJobDto;
        }

        private SortingJob GetSortingJobById(int sortingJobId)
        {
            var sortingJobEntity = _context.SortingJobs
                .SingleOrDefault(c => c.Id == sortingJobId);

            return sortingJobEntity;
        }

        

        public SortingJobDto UpdateSortingJob(SortingJobDto sortingJob)
        {
            var existingSortingJob = GetSortingJobById(sortingJob.Id);

            var sortingJobEntity = _mapper.Map(sortingJob, existingSortingJob);

            _context.SortingJobs.Update(sortingJobEntity);

            _context.SaveChanges();

            var updatedSortingJobDto = _mapper.Map<SortingJobDto>(sortingJobEntity);

            return updatedSortingJobDto;
        }
    }
}
