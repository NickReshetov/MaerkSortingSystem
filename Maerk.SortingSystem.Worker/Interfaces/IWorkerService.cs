using Maerk.SortingSystem.Dtos;

namespace Maerk.SortingSystem.Worker.Interfaces
{
    public interface IWorkerService
    {
        void ProcessSortingJob(SortingJobDto sortingJob);
    }
}