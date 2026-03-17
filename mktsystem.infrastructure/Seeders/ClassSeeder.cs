using mktsystem.domain.Entities;
using mktsystem.domain.Seeders;
using mktsystem.infrastructure.Persistence;

namespace mktsystem.infrastructure.Seeders
{
    public class ClassSeeder(MktSystemDbContext dbContext) : IClassSeeder
    {
        public async Task Seed()
        {
            if (!dbContext.Classes.Any())
            {
                var classes = new List<Classes>
                {
                    new Classes { Name = "Class A", MonthlyFee = 100 },
                    new Classes { Name = "Class B", MonthlyFee = 120 },
                    new Classes { Name = "Class C", MonthlyFee = 150 }
                };

                await dbContext.Classes.AddRangeAsync(classes);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}