using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnyxScheduling.Auth;
using OnyxScheduling.Data.Repositories;
using OnyxScheduling.Interfaces;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using OnyxScheduling.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnyxScheduling.Controllers;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
// Quest PDF License
QuestPDF.Settings.License = LicenseType.Community;

// For Entity Framework
builder.Services.AddDbContext<AuthDataContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

// For Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AuthDataContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
    };
});

// Adding Repositories 
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IInvoiceInvoiceItemRepository, InvoiceInvoice_ItemRepository>();
builder.Services.AddScoped<IJobsRepository, JobsRepository>();
builder.Services.AddScoped<IJobInvoiceItemRepository, JobsInvoiceItemRepository>();
builder.Services.AddScoped<PdfService>();

builder.Services.AddControllers();
   /* .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // To keep the property names as-is
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault;
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.JsonSerializerOptions.DictionaryKeyPolicy = null;

        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;


        options.JsonSerializerOptions.Converters.Add(new MyCustomDateTimeConverter()); // Add your custom date converter

    });*/


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();