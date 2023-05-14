<h1 align="center" >🐌 LayuiAdminNetPro </h1>  

<div align="center"> 
<p> LayuiAdminNetPro  是一个 Iframe  版的 Web 后台解决方案，基于 <a target="_blank" href="http://layui.org.cn/layuiadmin/index.html#get" >Layuiadmin</a> 和 .NET , 支持 .NET 6.0 + 。</p>
</div>



<div align="center" style="color:gray"> 
    中文 
</div>


## :zap: 功能特性
+ :boom: 实现自定义用户权限配置  
+ :palm_tree: 实现 [RESTful API](https://restfulapi.cn/) 自定义路由配置  
+ :sparkles: 实现 [JSON Web Tokens](https://jwt.io/) 认证  
+ :whale: 实现 [ASP.NET Authorization](https://learn.microsoft.com/zh-cn/aspnet/core/security/authorization/policies?view=aspnetcore-6.0) 自定义策略  
+ :pencil: 实现 AOP 捕捉异常和日志记录  
+ :beers: 基于 [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)   
+ :newspaper: 基于 [Mysql](https://www.mysql.com/cn/) 数据库 ，项目后期解耦仓储层会支持 `SqlServer`  等数据库  

## 🔖 项目结构

> 项目结构目录，后续随着项目重构可能会增加。

```C#
Project 
  └───LayuiAdminNetPro                       //主项目程序
    │    ├─wwwroot                           	//静态资源、Layuiadmin资源等                
    │    ├─Controllers                       	//控制器
    │    ├─Models                            	//视图模型
    │    ├─Utilities                         	//公用类
    │    │	 ├─Middlewares                          //中间件
    │    │	 ├─Filters                              //过滤器   
    │    │	 └─Autofac                              //Autofac 依赖注入
    │    └─Views                             	//视图
    ├─LayuiAdminNetCore                      //实体类库
    │    ├─AdminModels                        	//数据库映射实体类
    │    ├─AdminPages                        	//接口参数类
    │    ├─Appsettings                       	//项目配置映射实体类
    │    ├─AuthorizationModels               	//权限相关类
    │    ├─Constants                         	//常量
    │    ├─Databases                            //ORM
    │    │	 └─EF                                   //EF上下文
    │    ├─Enums                                //枚举
    │    └─Pages                                //分页
    ├─LayuiAdminNetGate                     //权限系统【鉴权、授权、自定义策略扩展】
    │    ├─Handler                              //权限校验
    │    ├─IServices                            //权限业务接口
    │    └─Services                             //权限业务实现
    ├─LayuiAdminNetServer                   //逻辑业务层
    │    ├─IServices                        	//逻辑业务接口
    │    └─Services                         	//逻辑业务实现
    └─LayuiAdminNetInfrastructure           //仓储层
         ├─IRepositoies                         //EF数据交互接口
         └─Repositoies                          //EF数据交互实现【数据持久化】
```

## 💻项目进度

> 项目正在重构中，请勿克隆，可以 :star: 关注一下，:pray:谢谢。

+  [项目进度](./README_SCHEDULE.md)

## 📄项目规范

> 可以在 `apifox` 中查看和调试接口

+  [Apifox API 文档](https://apifox.com/apidoc/shared-a1ef2dce-1084-4da5-8bdb-18aaec8dd93a)

## :rainbow:免责说明

+ `Layuiadmin`  相关代码版权归 [Layui](https://www.ilayuis.com/) 官方所有；除此之外，其他代码来自开源项目或作者原创；请尊重版权、开源和原创。
