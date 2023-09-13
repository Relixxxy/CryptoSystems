using CryptoSystems.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoSystems.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<LaboratoryWork> LaboratoryWorks { get; set; }
}
