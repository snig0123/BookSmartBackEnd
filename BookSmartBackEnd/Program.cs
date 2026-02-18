using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookSmartBackEnd.Authentication;
using BookSmartBackEnd.BusinessLogic;
using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEndDatabase;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var corsPolicy = "_corsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? [])
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                      });
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Setup Database
var conStrBuilder = new SqlConnectionStringBuilder(
    builder.Configuration.GetConnectionString("default"));
conStrBuilder.Password = builder.Configuration["DbPassword"];

builder.Services.AddDbContext<BookSmartContext>(options =>
    options.UseSqlServer(conStrBuilder.ConnectionString)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);

//Dependency Injection
builder.Services.AddScoped<IUserCreationService, UserCreationService>();
builder.Services.AddScoped<IUserBll, UserBll>();
builder.Services.AddScoped<IStaffBll, StaffBll>();
builder.Services.AddScoped<IServiceBll, ServiceBll>();
builder.Services.AddScoped<IScheduleBll, ScheduleBll>();
builder.Services.AddScoped<IAppointmentBll, AppointmentBll>();
builder.Services.AddScoped<IScheduleOverrideBll, ScheduleOverrideBll>();
builder.Services.AddScoped<IServiceScheduleBll, ServiceScheduleBll>();

//Singletons
builder.Services.AddSingleton<JwtHelper>();

//Authentication
byte[] key = Encoding.ASCII.GetBytes(builder.Configuration["jwtCert"] ?? string.Empty);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = "BookSmart",
        ValidateAudience = false
    };
    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["token"];
            return Task.CompletedTask;
        }
    };
});

// Set claims that can be used
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Staff", policy => policy.RequireClaim("Staff"));
    options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
});



WebApplication app = builder.Build();

//Create the database on first run and migrate if needed
//This code can be added later
//
//Use this in a migration when renaming a field or it will drop and recreate
//migrationBuilder.RenameColumn(
// name: "ColumnA",
// table: "MyTable",
// newName: "ColumnB");
//
/*using var scope = app.Services.CreateScope();
using var context = scope.ServiceProvider.GetRequiredService<BookSmartContext>();
context.Database.Migrate();*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();