using System.Collections.Generic;
using Maerk.SortingSystem.Dtos;

namespace Maerk.SortingSystem.Services.Interfaces
{
    public interface ISortingJobService
    {
        SortingJobDto CreateSortingJob(IEnumerable<int> sortableSequence);

        SortingJobDto GetSortingJob(int sortingJobId);

        IEnumerable<SortingJobDto> GetSortingJobs();

        SortingJobDto UpdateSortingJob(SortingJobDto sortingJob);
        
    }
}