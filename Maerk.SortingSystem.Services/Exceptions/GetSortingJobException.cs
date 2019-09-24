using System;

namespace Maerk.SortingSystem.Services.Exceptions
{
    public class GetSortingJobException : Exception
    {
        public GetSortingJobException(string message) : base(message)
        {
        }
    }
}
