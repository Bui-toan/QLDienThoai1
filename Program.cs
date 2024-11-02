using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;
using QLDienThoai.Respository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("QldienThoaiContext");
builder.Services.AddDbContext<QldienThoaiContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<ITenRespository, TenRespository>();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<AppUser, IdentityRole>()
	.AddEntityFrameworkStores<QldienThoaiContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	// Password settings.
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	//options.Password.RequireNonAlphanumeric = true;
	//options.Password.RequireUppercase = true;
	options.Password.RequiredLength = 6;
	//options.Password.RequiredUniqueChars = 1;

	// Lockout settings.
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	options.Lockout.MaxFailedAccessAttempts = 5;
	options.Lockout.AllowedForNewUsers = true;

	// User settings.
	options.User.AllowedUserNameCharacters =
	"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
	options.User.RequireUniqueEmail = true;
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.IsEssential = true;
});
var app = builder.Build();
app.UseSession();
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

app.UseAuthentication(); // đăng nhập

app.UseAuthorization(); // kiểm tra quyền

app.MapControllerRoute(
	name: "Areas",
	pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
