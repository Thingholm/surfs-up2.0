using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Models;


namespace Web.Data 
{
    public class AppDbContext : IdentityDbContext<User>  
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    }
}