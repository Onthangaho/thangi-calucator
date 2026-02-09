using Microsoft.Extensions.Options;
using thangi_calucator;
using thangi_calucator.Persistence;
using Microsoft.AspNetCore.Identity;    
using Microsoft.EntityFrameworkCore;
using API.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

var dataDirectory = Path.Combine(
    builder.Environment.ContentRootPath, "Data");

builder.Services.AddSingleton<ICalculationStore>(
    new FileCalculationStore(dataDirectory)

);
builder.Services.AddScoped<CalculatorService>();
// Add services to the container.
builder.Services.AddDbContext<CalculatorDbContext>(options=> 
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddEntityFrameworkStores<CalculatorDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddControllers();// tells ASP.NET that this app will use controllers as entry points

builder.Services.AddSwaggerGen();

/* Register the CalculatorService as a singleton so that it can be injected into controllers and other services throughout the application. 
This allows us to use the same instance of the CalculatorService across the entire application,
 which can help improve performance and reduce memory usage. By registering it as a singleton*/
builder.Services.AddSingleton<CalculatorService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
    var jwt  =builder.Configuration.GetSection("Jwt");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer=true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,

        ValidIssuer=jwt["Issuer"],
        ValidAudience=jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]))

    };
});



var app = builder.Build();
// Seed the database with an initial admin user and role so that we have a user to log in with and test the authentication and authorization features of the application.
using(var scope = app.Services.CreateAsyncScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


     // Call the SeedAsync method to seed the database with the initial admin user and role
    await IdentitySeeder.SeedAsync(userManager,roleManager);
}
// Add the exception handling middleware so that it can catch exceptions from all controllers
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();




app.Run();


