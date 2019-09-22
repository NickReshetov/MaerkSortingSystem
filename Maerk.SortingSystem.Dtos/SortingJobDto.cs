using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maerk.SortingSystem.Dtos
{
    public class SortingJobDto
    {
        public int Id { get; set; }

        public long TimeStamp { get; set; }

        public long Duration { get; set; }

        public string Status { get; set; }

        public IEnumerable<int> Input { get; set; }

        public IEnumerable<int> Output { get; set; }
    }
}
