using System;
using System.ComponentModel.DataAnnotations;

namespace Maerk.SortingSystem.DataAccess.EntityFramework.Entities
{
    public class SortingJob : BaseEntity
    {
        [Required]
        public DateTime TimeStamp { get; set; }

        public DateTimeOffset Duration { get; set; }

        [Required]
        //Generally should a dictionary table int the DB, but has been made string for the simplicity
        public string Status { get; set; }

        [Required]
        //Will contain arrays of numbers as JSON
        public string Input { get; set; }

        //Will contain arrays of numbers as JSON
        public string Output { get; set; }
    }
}
