using System;
using System.Collections.Generic;
using System.Linq;
using Maerk.SortingSystem.DataAccess.Repositories.Interfaces;
using Maerk.SortingSystem.Dtos;
using Maerk.SortingSystem.Services.Exceptions;
using Maerk.SortingSystem.Services.Interfaces;

namespace Maerk.SortingSystem.Services
{
    public class SortingJobService : ISortingJobService
    {
        private readonly ISortingJobRepository _sortingJobRepository;

        public SortingJobService(ISortingJobRepository sortingJobRepository)
        {
            _sortingJobRepository = sortingJobRepository;
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

            return createdSortingJob;
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

        public SortingJobDto UpdateSortingJob(SortingJobDto sortingJob)
        {
            ValidateUpdatingSortingJob(sortingJob);

            var updatedSearchingJob = _sortingJobRepository.UpdateSortingJob(sortingJob);

            return updatedSearchingJob;
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

        private void ValidateUpdatingSortingJob(SortingJobDto sortingJob)
        {
            ValidateSortingJob(sortingJob);

            var existingSortingJob = _sortingJobRepository.GetSortingJob(sortingJob.Id);

            if (existingSortingJob == null)
                throw new UpdateSortingJobException($"SortingJob with Id {sortingJob.Id} has not been found, update failed!");
        }

        private static void ValidateSortingJob(SortingJobDto sortingJob)
        {
            if (sortingJob == null)
                throw new UpdateSortingJobException("SortingJob is null, update failed!");
        }
    }  
}
