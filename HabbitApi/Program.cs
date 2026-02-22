using HabbitApi.Data;
using HabbitApi.Interface;
using HabbitApi.Mapper;
using HabbitApi.Repository;
using HabbitApi.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg => { }, typeof(HabitMappingProfile));


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IHabitRepository,HabitRepository>();
builder.Services.AddScoped<IHabitService, HabitService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Habbit API V1");
    c.RoutePrefix = string.Empty; 
});


app.UseHttpsRedirection();

app.Run();
