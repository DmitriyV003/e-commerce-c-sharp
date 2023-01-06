using System.Net;
using core.Interfaces;
using core.Specifications;
using e_commerce.Errors;
using e_commerce.Helpers;
using infrastructure.Data;
using infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseSentry(o =>
{
    o.Dsn = "https://dedb94df589241748403262d751d50cc@o4504165013520384.ingest.sentry.io/4504165014896640";
    o.Debug = true;
    o.TracesSampleRate = 1.0;
});

// Add services to the container.

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddDbContext<AppIdentityDbContext>(x =>
{
    x.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection"));
});

builder.Services.AddDbContext<StoreContext>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var conf = ConfigurationOptions.Parse(
        builder.Configuration.GetConnectionString("Redis"), 
        true);

    return ConnectionMultiplexer.Connect(conf);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo(){Title = "E-Commerce", Version = "v1"});
    opts.EnableAnnotations();
});
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.Configure<ApiBehaviorOptions>(opts =>
{
    opts.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage)
            .ToArray();

        var res = new ApiValidationErrorResponse((int)HttpStatusCode.BadRequest, errors);

        return new BadRequestObjectResult(res);
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();
    try
    {
        var ctx = services.GetRequiredService<StoreContext>();
        await ctx.Database.MigrateAsync();
        logger.LogDebug("Migrating database");
        
    }
    catch (Exception e)
    {
        logger.LogError(e, "Error occured during migration");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseSentryTracing();
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapControllers();

app.Run();
