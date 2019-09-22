using System;

namespace Maerk.SortingSystem.Services.Exceptions
{
    public class GetSortingJobsException : Exception
    {
        public GetSortingJobsException(string message) : base(message)
        {
        }
    }
}
