using System;
using System.Collections.Generic;
using Maerk.SortingSystem.Dtos;

namespace Maerk.SortingSystem.DataAccess.Repositories.Interfaces
{
    public interface ISortingJobRepository
    {
        IEnumerable<SortingJobDto> GetSortingJobs();

        SortingJobDto GetSortingJob(int SortingJobId);

        SortingJobDto CreateSortingJob(SortingJobDto sortingJob);

        SortingJobDto UpdateSortingJob(SortingJobDto sortingJob);
    }
}
