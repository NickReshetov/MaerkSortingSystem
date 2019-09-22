using System.Collections.Generic;
using Maerk.SortingSystem.Dtos;
using Maerk.SortingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Maerk.SortingSystem.Web.Api.Controllers
{
    [ApiController]
    [Route("api/mergesort")]
    public class SortingJobController : ControllerBase
    {
        private readonly ISortingJobService _sortingJobService;

        public SortingJobController(ISortingJobService sortingJobService)
        {
            _sortingJobService = sortingJobService;
        }

        [HttpPost]
        public SortingJobDto CreateSortingJob([FromBody]IEnumerable<int> sortableSequence)
        {
            var sortingJobDto = _sortingJobService.CreateSortingJob(sortableSequence);

            return sortingJobDto;
        }

        [HttpGet("executions")]
        public IEnumerable<SortingJobDto> GetSortingJobs()
        {
            var sortingJobDtos = _sortingJobService.GetSortingJobs();

            return sortingJobDtos;
        }

        [HttpGet("executions/{sortingJobId:int}")]
        public SortingJobDto GetSortingJob(int sortingJobId)
        {
            var sortingJobDto = _sortingJobService.GetSortingJob(sortingJobId);

            return sortingJobDto;
        }
    }
}
