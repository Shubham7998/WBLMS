using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WBLMS.Data;
using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Repositories;
using WBLMS.Services;
using WBLMS.Utilities;
using WBLMS.Models;
using Coravel;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//var connectionString = configuration.GetConnectionString("connectionStringHemantOffice");
//var connectionString = configuration.GetConnectionString("connectionstringshubhamhome");
var connectionString = configuration.GetConnectionString("connectionStringShubhamOffice");

builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

var emailSettings = builder.Services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

builder.Services.AddDbContext<WBLMSDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScheduler();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();

app.Services.UseScheduler(scheduler =>
{
    scheduler.ScheduleAsync(async () =>
    {
        using(var serviceScope = app.Services.CreateScope())
        {
            var services = serviceScope.ServiceProvider;

            var emailService = services.GetRequiredService<IEmailService>();
            var emailModel = new EmailModel("hemant.patel@wonderbiz.in", "Reset Password", EmailBody.WelcomeEmail("shubham.patil@wonderbiz.in"));

            emailService.SendEmail(emailModel);
        //await emailService.SendWelcomeEmailAsync("example@example.com");
        }

        await Console.Out.WriteLineAsync("Scheduled email sent.");
    }).EverySeconds(1); // Schedule the task to run daily at 12 PM
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseStaticFiles();
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

