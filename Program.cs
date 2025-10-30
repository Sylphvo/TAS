using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TAS.AttributeTargets;
using TAS.Data;
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
		o.ExpireTimeSpan = TimeSpan.FromDays(14);
		o.Cookie.HttpOnly = true;
		o.Cookie.SameSite = SameSiteMode.Lax;
		o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
	});

// Thêm Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Cấu hình supported cultures
var supportedCultures = new[]
{
	new CultureInfo("vi-VN"),
	new CultureInfo("en-US")
};
// Cấu hình RequestLocalizationOptions
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	options.DefaultRequestCulture = new RequestCulture("vi-VN");
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;
	// Cấu hình các provider để detect ngôn ngữ

	options.RequestCultureProviders = new List<IRequestCultureProvider>
	{
		new QueryStringRequestCultureProvider(), // ?culture=vi-VN
        new CookieRequestCultureProvider(),      // Cookie
        new AcceptLanguageHeaderRequestCultureProvider() // Header
    };
});
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
var cultures = new[] { new CultureInfo("vi-VN"), new CultureInfo("en-US") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
	DefaultRequestCulture = new RequestCulture("vi-VN"),// ngôn ngữ mặc định
	SupportedCultures = cultures,// định dạng ngày tháng, số, tiền tệ
	SupportedUICultures = cultures// ngôn ngữ hiển thị
});

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
