using thangi_calucator;
using thangi_calucator.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();// tells ASP.NET that this app will use controllers as entry points

builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICalculationStore> (new FileCalculationStore("Data/calculation.json"));
builder.Services.AddSingleton<CalculatorService>();

var app = builder.Build();

app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();


app.Run();


