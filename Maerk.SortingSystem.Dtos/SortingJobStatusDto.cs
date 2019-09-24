using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maerk.SortingSystem.Dtos
{
    public class SortingJobStatusDto
    {
        public int Id { get; set; }

        public string Status { get; set; }
    }
}
