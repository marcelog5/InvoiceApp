using InvoiceAppDomain.Data.Repository;
using InvoiceAppInfrastructure;
using InvoiceAppInfrastructure.Populate;
using InvoiceAppInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InvoiceDBContext>(
    options => options.UseSqlServer("Server=localhost;Database=InvoiceDataBase;User Id=sa;Password=gH9#kL$4p@3qW!;TrustServerCertificate=true;")
);

// Repository
builder.Services.AddTransient<IContractRepository, ContractRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<InvoiceDBContext>();
    InvoiceDBInitializer.Initialize(context);
}

app.Run();
