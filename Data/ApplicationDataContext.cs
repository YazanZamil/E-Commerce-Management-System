using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Project.Models;

namespace Project.Data
{
    public class ApplicationDataContext : IdentityDbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options)
            : base(options)
        {
        }
        public DbSet<Item> Item { get; set; } = default!;
        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<ShoppingBasket> ShoppingBasket { get; set; } = default!;

        
    }
}
