using CounselingService.API;
using CounselingService.API.DbContexts;
using CounselingService.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Microsoft.OpenApi.Extensions;
using System.Reflection;
using Microsoft.OpenApi.Models;

Log.Logger = new LoggerConfiguration().
    MinimumLevel.Debug().
    WriteTo.Console().
    WriteTo.File("logs/counselingapi.txt", rollingInterval: RollingInterval.Day).
    CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFuillPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlCommentsFuillPath);

    setupAction.AddSecurityDefinition("CounselingServiceApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });
    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "CounselingServiceApiBearerAuth"}
            }, new List<string>() }
    });
});

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

builder.Services.AddSingleton<CounselingDataStore>();

builder.Services.AddDbContext<CounselingInfoContext>(dbContextOptions => dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:CounselingInfoDBCOnnectionString"]));

builder.Services.AddScoped<ICounselingInfoRepository, CounselingInfoRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
);

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("MustBeAGamblingCounselor", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("counseling", "gambling");

        });
    }
);

builder.Services.AddApiVersioning( setupAction =>
{
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    setupAction.ReportApiVersions = true;
}).AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();


app.Run();
