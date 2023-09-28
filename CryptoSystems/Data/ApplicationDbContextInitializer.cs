using CryptoSystems.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CryptoSystems.Data;

public class ApplicationDbContextInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.LaboratoryWorks.Any())
        {
            await context.LaboratoryWorks.AddRangeAsync(GetPreconfiguredLaboratoryWorks());
        }

        await context.SaveChangesAsync();
    }

    private static IEnumerable<LaboratoryWork> GetPreconfiguredLaboratoryWorks()
    {
        yield return new LaboratoryWork()
        {
            Id = 1,
            Name = "Stream encryption based on a linear feedback shift register",
            Description = "Laboratory work description",
            ControllerName = "LabOne",
            ActionName = "Index"
        };
    }
}
