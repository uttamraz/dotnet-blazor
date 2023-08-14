using DotNetBlazor.Server.Context;
using DotNetBlazor.Server.Repository.ProfileRepository;
using DotNetBlazor.Server.Repository.RegistrationRepository;
using DotNetBlazor.Server.Services.AccountService;
using DotNetBlazor.Server.Services.ProfileService;
using DotNetBlazor.Server.Utility.Helpers;
using DotNetBlazor.Server.Utility.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net.Mime;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    //Validation Handler
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var result = new ValidationFailedResult(context.ModelState);
            result.ContentTypes.Add(MediaTypeNames.Application.Json);
            return result;
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotNetBlazor", Version = "v1" });
});


//User Manager Service
//builder.Services.AddIdentity<Users, IdentityRole>().AddEntityFrameworkStores<DbContext>();
builder.Services.AddSingleton<IContextHelper, ContextHelper>();
builder.Services.AddSingleton<IJwtUtils, JwtUtils>();
//Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

//Repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

//Adding DB Context with MSSQL
builder.Services.AddDbContext<BaseDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Connection"))
);

builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();


//configure logging first
builder.InitLog();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // migrate any database changes
    using var scope = app.Services.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
    dataContext.Database.Migrate();
}

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// Authorize Middleware
app.UseMiddleware<AuthorizeMiddleware>();

app.Run();
