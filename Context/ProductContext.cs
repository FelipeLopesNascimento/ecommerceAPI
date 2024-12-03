using Microsoft.EntityFrameworkCore;
using ecommerceAPI.models;

namespace ecommerceAPI.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        
    }
}