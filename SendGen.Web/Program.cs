using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SendGen.Domain.SendGenDomains.Data;
using SendGen.Repository.OpaSuiteRepositories;
using SendGen.Repository.SendGenRepositories;
using SendGen.Web.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);









var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<SendGenContexto>(options =>
    options.UseSqlServer(connectionString));



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<ApplicationDbContext>();


// SendGen
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
// OpaSuite
builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();
//Utilidades
builder.Services.AddScoped<IUtilitiesRepository, UtilitiesRepository>();






// Add services to the container.
#if DEBUG
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
//#else
//builder.Services.AddControllersWithViews();
#endif



builder.Services.ConfigureApplicationCookie(options =>
{
	options.ExpireTimeSpan = TimeSpan.FromHours(48);
	options.SlidingExpiration = true;

	options.Cookie.HttpOnly = true;
});
var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("pt-BR") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
	DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
	SupportedCultures = supportedCultures,
	SupportedUICultures = supportedCultures,
});
var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
