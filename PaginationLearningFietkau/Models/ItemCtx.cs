using Microsoft.EntityFrameworkCore;

namespace PaginationLearningFietkau.Models
{
    public class ItemCtx : DbContext // this is the liaison from the application to the databasse
    {
        public ItemCtx(DbContextOptions<ItemCtx> options) : base (options) { }

        public DbSet<Perishable> Perishables { get; set; }
    }
}
