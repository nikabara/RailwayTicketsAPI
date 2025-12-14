using Application.Abstractions;
using Application.Services.AuthServices.Abstractions;
using Application.Services.AuthServices.Implementations;
using Application.Services.EntityServices.Abstractions;
using Application.Services.EntityServices.Implementations;
using Application.Services.ExternalServices.EmailSendingService.Abstractions;
using Application.Services.ExternalServices.EmailSendingService.Implementations;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;

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

builder.Services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();

builder.Services.AddScoped<IVerificationCodeService, VerificationCodeService>();

builder.Services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();

builder.Services.AddScoped<ISeatRepository, SeatRepository>();

builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();

builder.Services.AddScoped<ICreditCardService, CreditCardService>();

builder.Services.AddScoped<ICreditCardIssuerRepository, CreditCardIssuerRepository>();

builder.Services.AddScoped<IUserCreditCardRepository, UserCreditCardRepository>();

builder.Services.AddScoped<IUserCreditCardService, UserCreditCardService>();

builder.Services.AddScoped<ISeatRepository, SeatRepository>();

builder.Services.AddScoped<ISeatService, SeatService>();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddScoped<ITrainScheduleFilterService, TrainScheduleFilterService>();

builder.Services.AddScoped<IUserFilterRepository, UserFilterRepository>();

builder.Services.AddScoped<IUserFilterService, UserFilterService>();

#endregion

#region External service registrations

builder.Services.AddScoped<ISMTPEmailSender, SMTPEmailSender>();

#endregion

#region Auth services

builder.Services.AddScoped<IAuthService, AuthService>();

#endregion

#region Builder auth service configuration

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
        .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false
    }
);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Authorization, e.g. \"bearer {token}\"",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

#endregion

#region CORS origin pass
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
    policy =>
    {
        // 1. Allow your Angular origin (http://localhost:4200)
        policy.WithOrigins("http://localhost:4200")
              // 2. Allow all headers (like Content-Type, Authorization)
              .AllowAnyHeader()
              // 3. Allow all HTTP methods (POST, GET, etc.)
              .AllowAnyMethod();
    });
});
#endregion

var app = builder.Build();

app.UseCors("_myAllowSpecificOrigins");

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

// PROGRAM HAS ERRORS