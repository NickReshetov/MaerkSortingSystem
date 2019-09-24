using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maerk.SortingSystem.Dtos;

namespace Maerk.SortingSystem.DataAccess.Repositories.Interfaces
{
    public interface ISortingJobRepository
    {
        IEnumerable<SortingJobDto> GetSortingJobs();

        Task<SortingJobDto> GetSortingJobAsync(int sortingJobId);

        Task<SortingJobDto> CreateSortingJobAsync(SortingJobDto sortingJob);

        Task<SortingJobDto> UpdateSortingJobAsync(SortingJobDto sortingJob);
    }
}
