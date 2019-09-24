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
        public async Task<SortingJobDto> CreateSortingJobAsync([FromBody]IEnumerable<int> sortableSequence)
        {
            var sortingJobDto = await _sortingJobService.CreateSortingJobAsync(sortableSequence);

            return sortingJobDto;
        }

        [HttpGet("executions")]
        public IEnumerable<SortingJobDto> GetSortingJobs()
        {
            var sortingJobDtos = _sortingJobService.GetSortingJobs();

            return sortingJobDtos;
        }

        [HttpGet("executions/{sortingJobId:int}")]
        public async Task<SortingJobDto> GetSortingJobAsync(int sortingJobId)
        {
            var sortingJobDto = await _sortingJobService.GetSortingJobAsync(sortingJobId);

            return sortingJobDto;
        }
    }
}
