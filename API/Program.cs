using thangi_calucator;
using thangi_calucator.Persistence;

var builder = WebApplication.CreateBuilder(args);

var dataDirectory = Path.Combine(
    builder.Environment.ContentRootPath, "Data");

builder.Services.AddSingleton<ICalculationStore>(
    new FileCalculationStore(dataDirectory)

);
// Add services to the container.
builder.Services.AddControllers();// tells ASP.NET that this app will use controllers as entry points

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CalculatorService>();

var app = builder.Build();
// Add the exception handling middleware so that it can catch exceptions from all controllers
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


