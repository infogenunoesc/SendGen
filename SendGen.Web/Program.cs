using Microsoft.EntityFrameworkCore;
using SendGen.Domain.SendGenDomains.Data;
using SendGen.Repository.OpaSuiteRepositories;
using SendGen.Repository.SendGenRepositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<SendGenContexto>(options =>
    options.UseSqlServer(connectionString));

// SendGen
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
// OpaSuite
builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
