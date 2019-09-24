using System.Collections.Generic;
using System.Threading.Tasks;
using Maerk.SortingSystem.Dtos;

namespace Maerk.SortingSystem.Services.Interfaces
{
    public interface ISortingJobService
    {
        Task<SortingJobDto> CreateSortingJobAsync(IEnumerable<int> sortableSequence);

        Task<SortingJobDto> GetSortingJobAsync(int sortingJobId);

        IEnumerable<SortingJobDto> GetSortingJobs();                
    }
}