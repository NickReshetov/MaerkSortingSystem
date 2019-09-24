using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Maerk.SortingSystem.Common.Extensions;
using Maerk.SortingSystem.DataAccess.Repositories.Interfaces;
using Maerk.SortingSystem.Dtos;
using Maerk.SortingSystem.Worker.Exceptions;
using Maerk.SortingSystem.Worker.Interfaces;
using Microsoft.Extensions.Logging;

namespace Maerk.SortingSystem.Worker
{    
    public class Worker : IWorker
    {
        private readonly ILogger _logger;        
        private readonly ISortingJobRepository _sortingJobRepository;

        public Worker(ILogger logger, ISortingJobRepository sortingJobRepository)
        {
            _logger = logger;
            _sortingJobRepository = sortingJobRepository;
        }

        public async Task ProcessSortingJobAsync(SortingJobDto sortingJob)
        {
            sortingJob.TimeStamp = DateTime.UtcNow.ToUnixTimeSeconds();

            var stopwatch = Stopwatch.StartNew();

            var sortedSequence = SortSequence(sortingJob.Input);

            stopwatch.Stop();
                        
            sortingJob.Output = sortedSequence;
            sortingJob.Status = Status.Completed.ToString();
            sortingJob.Duration = stopwatch.Elapsed.TotalMilliseconds;
            
            await UpdateSortingJobAsync(sortingJob);
        }

        private IEnumerable<int> SortSequence(IEnumerable<int> sortingJobInput)
        {
            var sortedSequence = MergeSort.MergeSort<int>.Sort(sortingJobInput);

            return sortedSequence;
        }

        private async Task<SortingJobDto> UpdateSortingJobAsync(SortingJobDto sortingJob)
        {
            ValidateUpdatingSortingJob(sortingJob);

            var updatedSearchingJob = await _sortingJobRepository.UpdateSortingJobAsync(sortingJob);

            return updatedSearchingJob;
        }

        private void ValidateUpdatingSortingJob(SortingJobDto sortingJob)
        {
            if (sortingJob == null)
            {
                var errorMessage = "SortingJob is null, update failed!";

                _logger.LogError(errorMessage);

                throw new UpdateSortingJobException(errorMessage);
            }

            var existingSortingJob = _sortingJobRepository.GetSortingJobAsync(sortingJob.Id);

            if (existingSortingJob == null)
            {
                var errorMessage = $"SortingJob with Id {sortingJob.Id} has not been found, update failed!";

                _logger.LogError(errorMessage);

                throw new UpdateSortingJobException(errorMessage);
            }
        }      
    }  
}
