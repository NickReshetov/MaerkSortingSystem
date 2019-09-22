using Maerk.SortingSystem.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Maerk.SortingSystem.DataAccess.EntityFramework
{
    public class MaerskSortingSystemDbContext : DbContext
    {
        public MaerskSortingSystemDbContext(DbContextOptions<MaerskSortingSystemDbContext> options) : base(options)
        {
        }
        
        public DbSet<SortingJob> SortingJobs { get; set; }
    }
}
