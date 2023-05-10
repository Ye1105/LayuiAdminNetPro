using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region MemoryCache

builder.Services.AddMemoryCache();

#endregion MemoryCache

#region mysql

//builder.Services.AddDbContext<H5Context>(options =>
//{
//    options.UseMySql(appSettings.ConnectionString, new MySqlServerVersion(new Version(8, 0, 27)));
//});

#endregion mysql

#region Cors

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt =>
     //opt.WithOrigins(appSettings.CrosAddress)
     opt.AllowAnyOrigin()
     .AllowAnyHeader()
     .AllowAnyMethod()
     .WithExposedHeaders("X-Pagination")
    );
});

#endregion Cors

#region 解决MVC视图中的中文被html编码的问题

builder.Services.Configure<WebEncoderOptions>(options =>
{
    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
});

#endregion 解决MVC视图中的中文被html编码的问题

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