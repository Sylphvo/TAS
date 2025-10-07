using Microsoft.EntityFrameworkCore;
using TAS.Data;

var builder = WebApplication.CreateBuilder(args);

// Connection string từ appsettings.json -> ConnectionStrings:Default
var cs = builder.Configuration.GetConnectionString("Default")
		 ?? throw new InvalidOperationException("Missing ConnectionStrings:Default");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
	opt.UseSqlServer(cs));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
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
