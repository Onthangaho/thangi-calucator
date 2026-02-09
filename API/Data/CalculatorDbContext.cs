using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



using API.Auth;
using Microsoft.EntityFrameworkCore;
using thangi_calucator;
using Microsoft.AspNetCore.Identity;

public class CalculatorDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
       
       /* this class is responsible for managing the database connection and providing access to the Calculations table so that we can perform CRUD operations on it. 
       It inherits from DbContext, which is a base class provided by Entity Framework Core for working with databases in a .NET application. And*/

    public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options) : base(options)
    {
        
    }

    public DbSet<Calculation> Calculations { get; set; } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure the Calculation entity
        modelBuilder.Entity<Calculation>().HasKey(c => c.Id); // Set Id as the primary key
        
    }
        
}