using System.Threading.Tasks;
using Maerk.SortingSystem.Dtos;

namespace Maerk.SortingSystem.Worker.Interfaces
{
    public interface IWorker
    {
        Task ProcessSortingJobAsync(SortingJobDto sortingJob);
    }
}