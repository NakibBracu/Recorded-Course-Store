using Autofac;
using Autofac.Extensions.DependencyInjection;
using log4net;
using Microsoft.AspNetCore.Identity;
using RCS.Data.DataAccessServiceConfigurations;
using RCS.Services;
using RCS.Services.Services;
using RCS.UI;
using RCS.UI.Utilities.Email;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var connectionString = builder.Configuration.GetConnectionString("RCSConnectionString");

builder.Services.ConfigureDataAccessServices(connectionString)
    .RegisterServiceLayers();

// Add services to the container.
builder.Services.AddControllersWithViews();



//Enable Session
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.LoginPath = "/Account/SignIn";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

//HttpContextAccessor added
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Log4net 
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

//Autofac Configured
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new WebModule());
});

//Smtp config
builder.Services.Configure<Smtp>(builder.Configuration.GetSection("Smtp"));

var log = LogManager.GetLogger(typeof(Program));

try
{
    var app = builder.Build();
    //await app.Services.GetService<ISeedService>()!.DataSeed();


    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSession();

    app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    log.Info("Application is starting");
    app.Run();

}
catch (Exception ex)
{
    log.Fatal($"Application can not start.\n{ex}");
}
