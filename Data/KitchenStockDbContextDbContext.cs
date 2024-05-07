using KitchenStock.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenStock.Data
{
    public class KitchenStockDbContext : DbContext
    {
        public KitchenStockDbContext(DbContextOptions<KitchenStockDbContext> options) : base(options) { }

        //Define the Database Sets
        public DbSet<ArticleModel> Articles { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<StockModel> Stock { get; set; }

        //Specify that the tables have triggers
        protected override void OnModelCreating(ModelBuilder aModelBuilder)
        {
            aModelBuilder.Entity<LocationModel>(e =>
            {
                e.ToTable("Locations", tb => tb.HasTrigger("trg_SetCreateDate_Locations"));
            });

            aModelBuilder.Entity<ArticleModel>(e =>
            {
                e.ToTable("Articles", tb => tb.HasTrigger("trg_SetCreateDate_Articles"));
            });

            aModelBuilder.Entity<StockModel>(e =>
            {
                e.ToTable("Stock", tb => tb.HasTrigger("trg_SetCreateDate_Stock"));
            });

            aModelBuilder.Entity<CategoryModel>(e =>
            {
                e.ToTable("Categories", tb => tb.HasTrigger("trg_SetCreateDate_Categories"));
            });
        }
    }
}
