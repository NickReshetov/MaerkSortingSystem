using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Maerk.SortingSystem.DataAccess.Repositories.Interfaces;
using Maerk.SortingSystem.Dtos;
using Maerk.SortingSystem.Services.Exceptions;
using Maerk.SortingSystem.Services.Extensions;
using Maerk.SortingSystem.Services.Interfaces;
using Maerk.SortingSystem.Worker.Interfaces;
using Microsoft.Extensions.Logging;

namespace Maerk.SortingSystem.Services
{
    public class SortingJobService : ISortingJobService
    {
        private readonly ISortingJobRepository _sortingJobRepository;
        private readonly IWorkerService _workerService;
        private readonly ILogger _logger;

        public SortingJobService(ISortingJobRepository sortingJobRepository, IWorkerService workerService, ILogger logger)
        {
            _logger = logger;
            _sortingJobRepository = sortingJobRepository;
            _workerService = workerService;
        }

        public async Task<SortingJobDto> CreateSortingJobAsync(IEnumerable<int> sortableSequence)
        {
            var enumeratedSortableSequence = ValidateCreatingSortingJob(sortableSequence);

            var sortingJobDto = new SortingJobDto
            {
                Input = enumeratedSortableSequence,
                Status = Status.Pending.ToString()
            };

            var createdSortingJob = await _sortingJobRepository.CreateSortingJobAsync(sortingJobDto);
            
            //Should be replaced by sending a message to message queue
            SendSortingJobToProcess(createdSortingJob);

            return createdSortingJob;
        }

        private void SendSortingJobToProcess(SortingJobDto createdSortingJob)
        {
            var clonedCreatedSortingJob = createdSortingJob.Clone();

            var backgroundThread = new Thread(async () => await _workerService.ProcessSortingJobAsync(clonedCreatedSortingJob))
            {
                IsBackground = true
            };

            backgroundThread.Start();
        }

        public IEnumerable<SortingJobDto> GetSortingJobs()
        {
            var sortingJobs = _sortingJobRepository
                ?.GetSortingJobs()
                ?.ToList();

            ValidateHaveGottenSortingJobs(sortingJobs);

            return sortingJobs;
        }

        public async Task<SortingJobDto> GetSortingJobAsync(int sortingJobId)
        {
            ValidateGettingSortingJob(sortingJobId);

            var sortingJob = await _sortingJobRepository.GetSortingJobAsync(sortingJobId);

            ValidateHaveGottenSortingJob(sortingJobId, sortingJob);

            return sortingJob;
        }
        
        private IEnumerable<int> ValidateCreatingSortingJob(IEnumerable<int> sortableSequence)
        {
            List<int> enumeratedSortableSequence;

            try
            {
                enumeratedSortableSequence = sortableSequence.ToList();
            }
            catch (NullReferenceException)
            {
                var errorMessage = "Input array of elements is null!";

                _logger.LogError(errorMessage);

                throw new CreateSortingJobException(errorMessage);
            }

            if (!enumeratedSortableSequence.Any())
            {
                var errorMessage = "Input array of elements is empty!";

                _logger.LogError(errorMessage);

                throw new CreateSortingJobException(errorMessage);
            }

            return enumeratedSortableSequence;
        }

        private void ValidateHaveGottenSortingJobs(IEnumerable<SortingJobDto> sortingJobs)
        {
            if (sortingJobs == null)
            {
                var errorMessage = "SortingJobs collection is null!";

                _logger.LogError(errorMessage);

                throw new GetSortingJobsException(errorMessage);
            }
        }

        private void ValidateGettingSortingJob(int sortingJobId)
        {
            if (sortingJobId <= 0)
            {
                var errorMessage = $"SortingJobId is {sortingJobId}, but should be positive and greater than zero!";

                _logger.LogError(errorMessage);

                throw new GetSortingJobException(errorMessage);
            }
        }

        private void ValidateHaveGottenSortingJob(int sortingJobId, SortingJobDto sortingJob)
        {            
            if (sortingJob == null)
            {
                var errorMessage = $"SortingJob with Id {sortingJobId} has not been found, update failed!";

                _logger.LogError(errorMessage);

                throw new GetSortingJobException(errorMessage);
            }
        }
    }  
}
