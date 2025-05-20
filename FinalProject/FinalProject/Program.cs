using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Data;
using Service.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var conString = builder.Configuration.GetConnectionString("DefaultDatabase") ??
     throw new InvalidOperationException("Connection string 'DefaultDatabase'" +
    " not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conString));


builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddRepositoryLayer();
//builder.Services.AddServiceLayer();

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

app.Run();
