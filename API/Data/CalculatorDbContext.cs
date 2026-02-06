using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



using API.Auth;
using Microsoft.EntityFrameworkCore;

public class CalculatorDbContext : IdentityDbContext<ApplicationUser>
{
       

    public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options) : base(options)
    {
        
    }
}