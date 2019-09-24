using System;
using System.Collections.Generic;
using System.Diagnostics;
using Maerk.SortingSystem.Common.Extensions;
using Maerk.SortingSystem.DataAccess.Repositories.Interfaces;
using Maerk.SortingSystem.Dtos;
using Maerk.SortingSystem.Worker.Exceptions;
using Maerk.SortingSystem.Worker.Interfaces;
using Microsoft.Extensions.Logging;

namespace Maerk.SortingSystem.Worker
{    
    public class WorkerService : IWorkerService
    {
        private readonly ILogger _logger;        
        private readonly ISortingJobRepository _sortingJobRepository;

        public WorkerService(ILogger logger, ISortingJobRepository sortingJobRepository)
        {
            _logger = logger;
            _sortingJobRepository = sortingJobRepository;
        }

        public void ProcessSortingJob(SortingJobDto sortingJob)
        {
            sortingJob.TimeStamp = DateTime.UtcNow.ToUnixTimeSeconds();

            var stopwatch = Stopwatch.StartNew();

            var sortedSequence = SortSequence(sortingJob.Input);

            stopwatch.Stop();
                        
            sortingJob.Output = sortedSequence;
            sortingJob.Status = Status.Completed.ToString();
            sortingJob.Duration = stopwatch.Elapsed.TotalMilliseconds;
            
            UpdateSortingJob(sortingJob);
        }

        private IEnumerable<int> SortSequence(IEnumerable<int> sortingJobInput)
        {
            var sortedSequence = MergeSort.MergeSort<int>.Sort(sortingJobInput);

            return sortedSequence;
        }

        private SortingJobDto UpdateSortingJob(SortingJobDto sortingJob)
        {
            ValidateUpdatingSortingJob(sortingJob);

            var updatedSearchingJob = _sortingJobRepository.UpdateSortingJob(sortingJob);

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

            var existingSortingJob = _sortingJobRepository.GetSortingJob(sortingJob.Id);

            if (existingSortingJob == null)
            {
                var errorMessage = $"SortingJob with Id {sortingJob.Id} has not been found, update failed!";

                _logger.LogError(errorMessage);

                throw new UpdateSortingJobException(errorMessage);
            }
        }      
    }  
}
