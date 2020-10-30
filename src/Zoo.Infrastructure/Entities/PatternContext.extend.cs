namespace Zoo.Infrastructure.Entities
{
    using Microsoft.EntityFrameworkCore;

    using Store;

    public partial class PatternContext : IDbContext
    {
        protected PatternContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}