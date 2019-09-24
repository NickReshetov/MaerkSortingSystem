using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maerk.SortingSystem.DataAccess.EntityFramework;
using Maerk.SortingSystem.DataAccess.EntityFramework.Entities;
using Maerk.SortingSystem.DataAccess.Repositories.Interfaces;
using Maerk.SortingSystem.Dtos;
using Microsoft.EntityFrameworkCore;

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

        public async Task<SortingJobDto> CreateSortingJobAsync(SortingJobDto sortingJob)
        {
            var sortingJobEntity = _mapper.Map<SortingJob>(sortingJob);

            _context.SortingJobs.Add(sortingJobEntity);

            await _context.SaveChangesAsync();

            var sortingJobDto = _mapper.Map<SortingJobDto>(sortingJobEntity);

            return sortingJobDto;
        }

        public IEnumerable<SortingJobDto> GetSortingJobs()
        {
            var sortingJobEntities = _context.SortingJobs;

            var sortingJobDtos = _mapper.Map<IEnumerable<SortingJobDto>>(sortingJobEntities);

            return sortingJobDtos;
        }

        public async Task<SortingJobDto> GetSortingJobAsync(int sortingJobId)
        {
            var sortingJobEntity = await GetSortingJobByIdAsync(sortingJobId);

            var sortingJobDto = _mapper.Map<SortingJobDto>(sortingJobEntity);

            return sortingJobDto;
        }

        private async Task<SortingJob> GetSortingJobByIdAsync(int sortingJobId)
        {
            var sortingJobEntity = await _context.SortingJobs
                .SingleOrDefaultAsync(c => c.Id == sortingJobId);

            return sortingJobEntity;
        }

        

        public async Task<SortingJobDto> UpdateSortingJobAsync(SortingJobDto sortingJob)
        {
            var existingSortingJob = await GetSortingJobByIdAsync(sortingJob.Id);

            var sortingJobEntity = _mapper.Map(sortingJob, existingSortingJob);

            _context.SortingJobs.Update(sortingJobEntity);

            await _context.SaveChangesAsync();

            var updatedSortingJobDto = _mapper.Map<SortingJobDto>(sortingJobEntity);

            return updatedSortingJobDto;
        }
    }
}
