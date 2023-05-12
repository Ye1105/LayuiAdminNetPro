using Autofac;
using Autofac.Extensions.DependencyInjection;
using LayuiAdminNetCore.Appsettings;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetPro.Utilities.Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    //忽略循环引用
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    //小驼峰
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

#region MemoryCache

builder.Services.AddMemoryCache();

#endregion MemoryCache

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

#region Autofac IOC 容器

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); //通过工厂替换，把Autofac整合进来

//https://blog.csdn.net/xiaxiaoying2012/article/details/84617677
//http://niuyi.github.io/blog/2012/04/06/autofac-by-unit-test/
//https://www.cnblogs.com/gygtech/p/14491364.html

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    /* 通过继承 IDenpendency ，子类批量注入 */
    //Type basetype = typeof(IDenpendency);
    //containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
    //.Where(t => basetype.IsAssignableFrom(t) && t.IsClass)
    //.AsImplementedInterfaces().InstancePerLifetimeScope();

    //containerBuilder.RegisterGeneric(typeof(IList<>)).As(typeof(IList<>));

    /*  Middleware */
    var adminAssembly = Assembly.Load("LayuiAdminNetPro");
    containerBuilder.RegisterAssemblyTypes(adminAssembly).Where(t => t.Name.EndsWith("Handler")).AsImplementedInterfaces().InstancePerLifetimeScope();

    /* 权限配置注入 */
    var authorizeAssembly = Assembly.Load("LayuiAdminNetGate");
    containerBuilder.RegisterAssemblyTypes(authorizeAssembly).Where(t => t.IsClass && (t.Name.EndsWith("Service") || t.Name.EndsWith("Handler"))).AsImplementedInterfaces().InstancePerLifetimeScope();

    /* 业务逻辑注入 */
    var serverAssembly = Assembly.Load("LayuiAdminNetServer");
    containerBuilder.RegisterAssemblyTypes(serverAssembly).Where(t => t.IsClass && t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope(); //InstancePerLifetimeScope 保证对象生命周期基于请求

    /* 仓储配置注入 */
    var repositoryAssembly = Assembly.Load("LayuiAdminNetInfrastructure");
    containerBuilder.RegisterAssemblyTypes(repositoryAssembly).Where(t => t.IsClass && t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

    /* 注册每个控制器和抽象之间的关系 */
    var controllerBaseType = typeof(ControllerBase);
    containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
    .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
    .PropertiesAutowired(new CustomPropertySelector()); //注册属性注入
});

//支持控制器的实例让IOC容器来创建 -- autofac来创建
builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

#endregion Autofac IOC 容器

//获取 appsetings 中的配置信息
var appSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(option => appSection.Bind(option));
var appSettings = appSection.Get<AppSettings>();

/* JWT[Json web token] */
builder.Services
    // 配置授权服务，也就是具体的规则，已经对应的权限策略，比如公司不同权限的门禁卡
    .AddAuthorization(options =>
    {
        // 自定义基于策略的授权权限   [Authorize(Policy= Policys.Permission)]
        options.AddPolicy(Policys.Admin, policy => policy.Requirements.Add(new PermissionRequirement()));
    })
    // 配置认证服务
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    // 配置JWT
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents()
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies[".AspNetCore.Token"];
                return Task.CompletedTask;
            }
        };
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        //Token Validation Parameters。
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //是否验证发行人
            ValidateIssuer = true,
            ValidIssuer = appSettings!.JwtBearer!.Issuer,//发行人
                                                         //是否验证受众人
            ValidateAudience = true,
            ValidAudience = appSettings!.JwtBearer!.Audience,//受众人
                                                             //是否验证密钥
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(s: appSettings!.JwtBearer!.SecurityKey)),
            ValidateLifetime = true, //验证生命周期
            RequireExpirationTime = true, //过期时间
            ClockSkew = TimeSpan.Zero  //设置时钟偏斜为0
        };
    });

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  //JWT认证

app.UseAuthorization();   //JWT授权

app.UseCors("CorsPolicy");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();