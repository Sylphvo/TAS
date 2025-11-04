using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using TAS.AttributeTargets;
using TAS.Data;
using TAS.Helpers;
using TAS.TagHelpers;
using TAS.ViewModels; // DbContext của bạn

// Tạo builder
var builder = WebApplication.CreateBuilder(args);

// lấy chuỗi kết nối: appsettings.ConnectionStrings.Default
var cs = builder.Configuration.GetConnectionString("DefaultConnection");

// Đăng ký CommonDb
builder.Services.AddScoped<ConnectDbHelper>();

// Đăng ký RubberGardenModels
builder.Services.AddScoped<RubberGardenModels>();

builder.Services.AddIdentityCore<UserAccountIdentity>()
	.AddRoles<IdentityRole<Guid>>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddSignInManager()
	.AddDefaultTokenProviders();

builder.Services.AddAuthentication().AddIdentityCookies();

// Đăng ký MVC với filter RequireLogin toàn cục
builder.Services.AddControllersWithViews(o =>
{
	o.Filters.Add(new RequireLoginAttribute()); // nhớ đặt [AllowAnonymous] cho Login/Register
})
	.AddViewLocalization()  // Thêm hỗ trợ localization cho View
	.AddDataAnnotationsLocalization(); // Thêm hỗ trợ localization cho DataAnnotation

// Đăng ký Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(o =>
	{
		o.LoginPath = "/Account/Login";
		o.LogoutPath = "/Account/Logout";
		o.AccessDeniedPath = "/Account/Denied";
		o.SlidingExpiration = true;
		o.ExpireTimeSpan = TimeSpan.FromMinutes(30);
		o.Cookie.HttpOnly = true;
		o.Cookie.SameSite = SameSiteMode.Lax;
		o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
	});

// Thêm Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


// Cấu hình RequestLocalizationOptions
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	var supported = new[] { "vi", "en" };
	options.SetDefaultCulture("vi")
		.AddSupportedCultures(supported)
		.AddSupportedUICultures(supported);

	// Cấu hình các provider để detect ngôn ngữ

	options.RequestCultureProviders = new List<IRequestCultureProvider>
	{
        new CookieRequestCultureProvider(),      // Cookie
		new QueryStringRequestCultureProvider(), // ?culture=vi-VN
        new AcceptLanguageHeaderRequestCultureProvider() // Header
    };
});
// Đăng ký LanguageService
builder.Services.AddHttpContextAccessor();
// Đăng ký dịch vụ ngôn ngữ
builder.Services.AddScoped<ILanguageService, LanguageService>();


// Đăng ký Authorization
builder.Services.AddAuthorization();

// Đăng ký DI cho SQL Server + Dapper executor
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(cs));


// Đăng ký TagHelper
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");// custom error page
	app.UseHsts();// 30 days HSTS
}

// Sử dụng Localization
// QUAN TRỌNG: UseRequestLocalization phải đặt trước UseRouting
var opts = new RequestLocalizationOptions()
	.SetDefaultCulture("vi")
	.AddSupportedCultures("vi", "en")
	.AddSupportedUICultures("vi", "en");
app.UseRequestLocalization(opts);

// Middleware
app.UseHttpsRedirection();

// Phục vụ file tĩnh từ wwwroot
app.UseStaticFiles();

// Kích hoạt routing
app.UseRouting();

// Xác thực & ủy quyền
app.UseAuthentication();

// Ủy quyền
app.UseAuthorization();

// Định nghĩa route mặc định
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

// Chạy ứng dụng
app.Run();
