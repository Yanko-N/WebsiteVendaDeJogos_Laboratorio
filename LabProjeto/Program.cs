using LabProjeto.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(10);
    option.Cookie.Name = ".LabProject.Session";
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<RoleManager<IdentityRole>>();

var app = builder.Build();
//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhjVFpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jSH5SdEVmWHpddXNSQQ==;Mgo+DSMBPh8sVXJ0S0J+XE9HflRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31Td0ViW39bcXFWQGFfVg==;ORg4AjUWIQA/Gnt2VVhkQlFadVdJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkRjWH1edXVQRWZeVEM=;ODUzODkwQDMyMzAyZTM0MmUzMGc5ZVFMVUh3MTJsRjdQeHBONGNTdm5QTmVia3BPZ1l6RTlkcVJ1RHlQQk09;ODUzODkxQDMyMzAyZTM0MmUzMGdqT3VTdVdsYWJST1NJb0duemhCMFdsYmpIU2Z5bjM2clNWOUhROUkxbUE9;NRAiBiAaIQQuGjN/V0Z+WE9EaFxKVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdUViWX5ccHFUQ2NUUER2;ODUzODkzQDMyMzAyZTM0MmUzMGd2QWJGRm5vZU1QRitWRGIvdWJrRTdMck5kSElUREpkdTcvSi9ZNzVXZDg9;ODUzODk0QDMyMzAyZTM0MmUzMGRVWUlUYzErYmRvYkNZVnl4R2p4Nlp6WVF4UUlJS0IxeTVlUk5oYUFrQTQ9;Mgo+DSMBMAY9C3t2VVhkQlFadVdJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkRjWH1edXVQRWlUV0E=;ODUzODk2QDMyMzAyZTM0MmUzMFpnQTV0SG1DdkhXM1VxWXZGV2gxKyttRVp0L2ZBV1RIL1JVZkc3TlRpRzA9;ODUzODk3QDMyMzAyZTM0MmUzMEp0ZXZYMEZyMEtnR250R1BKZ1hJU1JoS1hlMzRvd1R4Q1dIWkp4bWIxZ0k9;ODUzODk4QDMyMzAyZTM0MmUzMGd2QWJGRm5vZU1QRitWRGIvdWJrRTdMck5kSElUREpkdTcvSi9ZNzVXZDg9");


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    SeedRoles.Seed(roleManager);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    name: "default",
    pattern: "{controller=JogoModels}/{action=HomeScreen}/{id?}");
app.MapRazorPages();

app.Run();
