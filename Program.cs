// Program.cs
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TAS.AttributeTargets;
using TAS.Data; // DbContext của bạn

var builder = WebApplication.CreateBuilder(args);

// lấy chuỗi kết nối: appsettings.ConnectionStrings.Default
var cs = builder.Configuration.GetConnectionString("Default")
		  ?? throw new InvalidOperationException("Missing ConnectionStrings:Default");

builder.Services.AddControllersWithViews(o =>
{
	o.Filters.Add(new RequireLoginAttribute()); // nhớ đặt [AllowAnonymous] cho Login/Register
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(o =>
	{
		o.LoginPath = "/Account/Login";
		o.LogoutPath = "/Account/Logout";
		o.AccessDeniedPath = "/Account/Denied";
		o.SlidingExpiration = true;
		o.ExpireTimeSpan = TimeSpan.FromDays(14);
		o.Cookie.HttpOnly = true;
		o.Cookie.SameSite = SameSiteMode.Lax;
		o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
	});
builder.Services.AddAuthorization();

// Đăng ký DI cho SQL Server + Dapper executor
builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<IDbExecutor, DbExecutor>();

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(cs));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
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
app.Run();
