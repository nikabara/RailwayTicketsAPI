using Application.Abstractions;
using Application.Services.Abstractions;
using Application.Services.Implementations;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region Swagger builder.Services

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region DbContext builder.Services

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.MigrationsAssembly("RailwayTicketsAPI");
        }
    )
);

#endregion

#region Service registration

builder.Services.AddScoped<ITrainService, TrainService>();

builder.Services.AddScoped<ITrainRepository, TrainRepository>();

builder.Services.AddScoped<ITrainScheduleRepository, TrainScheduleRepository>();

builder.Services.AddScoped<ITrainScheduleService, TrainScheduleService>();

builder.Services.AddScoped<IVagonRepository, VagonRepository>();

builder.Services.AddScoped<IVagonService, VagonService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
