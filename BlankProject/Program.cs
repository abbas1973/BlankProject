﻿using BLL.Interface;
using BLL;
using Filters;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AntiXssMiddleware.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
services.AddRazorPages();


#region DB Context
var connectionString = builder.Configuration.GetConnectionString("ApplicationContext");
services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString)/*, ServiceLifetime.Transient*/);
#endregion



#region گرفتن httpcontext در کلاس لایبرری ها
services.AddHttpContextAccessor();
#endregion




#region اضافه کردن سرویس کوکی
//services.Configure<CookiePolicyOptions>(options =>
//{
//    options.CheckConsentNeeded = context => true;
//    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
//});
#endregion


#region اضافه کردن سرویس سشن
services.AddDistributedMemoryCache();

services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(180);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "_session.cookie";
});
#endregion


#region اضافه کردن سرویس کش
//services.AddMemoryCache();

//services.AddResponseCaching(options =>
//{
//    options.UseCaseSensitivePaths = false;
//});
#endregion



#region فیلتر برای اکشن ها
// فیلتر بررسی لاگین ادمین در پنل ادمین
services.AddScoped<UserAuthorize>(_ => new UserAuthorize());
#endregion



#region نام کوکی و هدر AntiForgeryKey برای جلوگیری از حملات csrf
services.AddAntiforgery(options =>
{
    options.Cookie.Name = "_CSRF.cookie";
    options.HeaderName = "_CSRF_header";
});
#endregion






#region Dipendency Injection
// موجودیت های پایه
services.AddScoped<DbContext, ApplicationContext>();

#region DataTable
services.AddScoped<IDataTableManager, DataTableManager>();
#endregion


#region AuthSystem
services.AddScoped<IAuthManager, AuthManager>();
services.AddScoped<IMenuManager, MenuManager>();
services.AddScoped<IRoleManager, RoleManager>();
services.AddScoped<IRoleMenuManager, RoleMenuManager>();
services.AddScoped<IUserManager, UserManager>();
#endregion


#endregion




var app = builder.Build();

app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();


#region امنیت
// برای جلوگیری از حملات xss
app.UseAntiXssMiddleware();

#region تنظیم هدر برای جلوگیری از حملات
app.Use(async (context, next) =>
{
    // برای جلوگیری از iframe شدن صفحات سایت و براي مقابله در برابر حملات ClickJacking
    context.Response.Headers.Add("X-Frame-Options", "DENY");

    // جلوگیری از حملات xss
    context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");

    // جلوگیری از MIME-Sniffing و تغییر پسوند فایل ها
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

    context.Response.Headers.Remove("Server");
    await next();
});
#endregion 
#endregion



#region Exception Handler
//app.UseExceptionHandler("/error");
app.UseExceptionHandler(
          new ExceptionHandlerOptions()
          {
              AllowStatusCode404Response = true,
              ExceptionHandlingPath = "/error"
          }
      );
#endregion


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Index}/{id?}");


app.Run();