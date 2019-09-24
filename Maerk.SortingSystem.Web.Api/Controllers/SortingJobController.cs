using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<SortingJobStatusDto> CreateSortingJobAsync([FromBody]IEnumerable<int> sortableSequence)
        {
            var sortingJobStatusDto = await _sortingJobService.CreateSortingJobAsync(sortableSequence);

            return sortingJobStatusDto;
        }

        [HttpGet("executions")]
        public IEnumerable<SortingJobStatusDto> GetSortingJobs()
        {
            var sortingJobStatusDtos = _sortingJobService.GetSortingJobs();

            return sortingJobStatusDtos;
        }

        [HttpGet("executions/{sortingJobId:int}")]
        public async Task<SortingJobDto> GetSortingJobAsync(int sortingJobId)
        {
            var sortingJobDto = await _sortingJobService.GetSortingJobAsync(sortingJobId);

            return sortingJobDto;
        }
    }
}
