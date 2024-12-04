using Microsoft.EntityFrameworkCore;
using ecommerceAPI.Models;

namespace ecommerceAPI.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
