using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Maerk.SortingSystem.DataAccess.Repositories.Interfaces;
using Maerk.SortingSystem.Dtos;
using Maerk.SortingSystem.Services.Exceptions;
using Maerk.SortingSystem.Services.Extensions;
using Maerk.SortingSystem.Services.Interfaces;
using Maerk.SortingSystem.Worker.Interfaces;

namespace Maerk.SortingSystem.Services
{
    public class SortingJobService : ISortingJobService
    {
        private readonly ISortingJobRepository _sortingJobRepository;
        private readonly IWorkerService _workerService;

        public SortingJobService(ISortingJobRepository sortingJobRepository, IWorkerService workerService)
        {
            _sortingJobRepository = sortingJobRepository;
            _workerService = workerService;
        }

        public SortingJobDto CreateSortingJob(IEnumerable<int> sortableSequence)
        {
            var enumeratedSortableSequence = ValidateCreatingSortingJob(sortableSequence);

            var sortingJobDto = new SortingJobDto
            {
                Input = enumeratedSortableSequence,
                Status = Status.Pending.ToString()
            };

            var createdSortingJob = _sortingJobRepository.CreateSortingJob(sortingJobDto);

            SendSortingJobToProcess(createdSortingJob);

            return createdSortingJob;
        }

        private void SendSortingJobToProcess(SortingJobDto createdSortingJob)
        {
            var clonedCreatedSortingJob = createdSortingJob.Clone();

            var backgroundThread = new Thread(() => _workerService.ProcessSortingJob(clonedCreatedSortingJob))
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

        public SortingJobDto GetSortingJob(int sortingJobId)
        {
            ValidateGettingSortingJob(sortingJobId);

            var sortingJob = _sortingJobRepository.GetSortingJob(sortingJobId);

            return sortingJob;
        }
        
        private IEnumerable<int> ValidateCreatingSortingJob(IEnumerable<int> sortableSequence)
        {
            List<int> enumeratedSortableSequence;

            try
            {
                enumeratedSortableSequence = sortableSequence.ToList();
            }
            catch (Exception ex)
            {
                throw new CreateSortingJobException("Input array of elements is null!");
            }
            
            if (!enumeratedSortableSequence.Any())
                throw new CreateSortingJobException("Input array of elements is empty!");

            return enumeratedSortableSequence;
        }

        private static void ValidateHaveGottenSortingJobs(IEnumerable<SortingJobDto> sortingJobs)
        {
            if (sortingJobs == null)
                throw new GetSortingJobsException("SortingJobs collection is null!");
        }

        private void ValidateGettingSortingJob(int sortingJobId)
        {
            if (sortingJobId <= 0)
                throw new GetSortingJobException($"SortingJobId is {sortingJobId}, but should be positive and greater than zero!");
        }                
    }  
}
