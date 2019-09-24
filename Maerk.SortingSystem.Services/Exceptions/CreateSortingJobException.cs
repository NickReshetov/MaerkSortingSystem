using System;

namespace Maerk.SortingSystem.Services.Exceptions
{
    public class CreateSortingJobException : Exception
    {
        public CreateSortingJobException(string message) : base(message)
        {
        }
    }
}
