// ReSharper disable RedundantUsingDirective.Global
// ReSharper disable CommentTypo

global using CategoryDto = Noted.Data.DataDto.CategoryDto;
global using NoteDto = Noted.Data.DataDto.NoteDto;
global using UserDto = Noted.Data.DataDto.UserDto;
global using TokenDto = Noted.Data.DataDto.TokenDto;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.HostedServices;
using Noted.Repositories;
using Noted.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(@"logs\log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try {
    Log.Information("Starting Noted");

    var builder = WebApplication.CreateBuilder(args);

    // Add logging
    builder.Logging.AddSerilog();

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    //get object SQLServer from appsettings.json
    var sqlServer = builder.Configuration.GetSection("SQLServer").Get<SqlServerDto>();

    //add dbcontext to services
    builder.Services.AddDbContext<NotedContext>(options => options.UseSqlServer(sqlServer?.ToString()));

    Log.Debug("SQLServer: {SqlServer}", sqlServer?.ToString());

    //add repositories to services
    builder.Services.AddScoped<INoteRepository, NoteRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ITokenRepository, TokenRepository>();

    //get salt from appsettings.json
    var salt = builder.Configuration.GetValue<string>("Salt");
    
    Log.Debug("Salt: {Salt}", salt);
    
    //add services to services
    builder.Services.AddScoped<ILoginService, LoginService>();
    builder.Services.AddScoped<IHashingService>(_ => new HashingService(salt ?? string.Empty));
    builder.Services.AddScoped<IDataService, DataService>();

    //add hosted services to services (background tasks) (singletons)
    builder.Services.AddHostedService<OldTokensCleaner>();

    //add swagger to services
    builder.Services.AddSwaggerGen(options => {
        options.SwaggerDoc("v1", new() { Title = "Noted", Version = "v1" });
        
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    var app = builder.Build();

    //check if Noted database exists and create it if not
    using (var scope = app.Services.CreateScope()) {
        var services = scope.ServiceProvider;
        try {
            var context = services.GetRequiredService<NotedContext>();
            context.Database.EnsureCreated();
        }
        catch (Exception e) {
            Log.Fatal(e, "An error occurred while tried to create the database");
        }
    }

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment()) {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    else {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Noted v1"));
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    app.MapFallbackToFile("index.html");

    app.Run();
}
catch (Exception e) {
    Log.Fatal(e, "Host terminated unexpectedly");
}
finally {
    Log.CloseAndFlush();
}