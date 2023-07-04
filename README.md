
<h1 align="center" >🐌 LayuiAdminNetPro </h1>  

<div align="center"> 
<p> LayuiAdminNetPro  是一个 Iframe  版的 Web 后台解决方案，基于 <a target="_blank" href="http://layui.org.cn/layuiadmin/index.html#get" >Layui</a> 和 .NET , 支持 .NET 6.0 + 。</p>
</div>




<div align="center" style="color:gray"> 
    中文 
</div>


## :zap: 功能特性
+ :boom: 实现用户权限、菜单栏权限自定义配置  
+ ⛳ 实现 [JsonSchema](http://json-schema.org/) 参数校验配置  
+ :palm_tree: 实现 [RESTful API](https://restfulapi.cn/) 自定义路由配置  
+ :sparkles: 实现 [JSON Web Tokens](https://jwt.io/) 认证  
+ :whale: 实现 [ASP.NET Authorization](https://learn.microsoft.com/zh-cn/aspnet/core/security/authorization/policies?view=aspnetcore-6.0) 自定义策略  
+ :pencil: 实现 AOP 日志记录和异常捕捉  
+ :beers: 基于 [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)   
+ :newspaper: 基于 [Mysql](https://www.mysql.com/cn/) 数据库 ，项目后期解耦仓储层会支持 `SqlServer`  等数据库  

##  :ghost: 项目图解

![图解](https://luoqiublog2-1302273318.cos.ap-nanjing.myqcloud.com/github/ren.jpg)


##  :camera: 项目界面

+ [基础界面展示(登录界面、主界面、用户管理界面、角色管理界面、路由管理界面等等)](./README_IMAGES.md)


## 🔖 项目结构

> 项目基础结构目录。`Tips：后续随着项目优化可能会有小的改动`

```C#
 Project 
    ├── LayuiAdminNetPro                     //主项目程序
    |   ├── wwwroot                           	//静态资源（Layui、Layuiadmin、schema、common等等）  
    |   ├── Areas                           	//区域    
    |   |   ├── Api                                //接口模块
    |   |   |   ├── JsonSchemas                       //数据处理接口参数校验
    |   |   |   └── Controllers                       //数据处理接口                   
    |   |   └── View                               //视图模块   
    |   |       ├── Controllers                       //视图控制器
    |   |       └── Views                             //视图页面
    |   ├── Utilities                         	//公用类
    |   |   ├── Expansions                         //接口扩展
    |   |   ├── Filters                            //过滤器
    |   |   ├── Common                             //帮助类
    |   |   ├── AutoMapper                         //映射
    |   |   └── Autofac                            //依赖注入
    |   ├── appsettings.json                    //项目配置文件
    |   └── Program.cs                          //项目入口    
    ├── LayuiAdminNetCore                    //实体类库
    |   ├── AdminModels                         //数据库映射实体类
    |   ├── RequstModels                        //接口请求参数实体类【分页等】
    |   ├── DtoModels                           //AutoMap映射实体类
    |   ├── Appsettings                       	//项目配置映射实体类
    |   ├── AuthorizationModels               	//权限相关实体类
    |   ├── Constants                         	//常量
    |   ├── Databases                           //ORM
    |   |   └─EF                                  //EF上下文
    |   ├── Enums                               //枚举
    |   └── Pages                               //分页
    ├── LayuiAdminNetGate                    //权限系统【鉴权、授权、自定义策略扩展】
    |   ├── Handler                             //权限校验
    |   ├── IServices                           //权限业务接口
    |   └── Services                            //权限业务实现
    ├── LayuiAdminNetService                 //逻辑业务层
    |   ├── IServices                        	//逻辑业务接口
    |   └── Services                         	//逻辑业务实现
    └── LayuiAdminNetInfrastructure          //仓储层
        ├── IRepositoies                        //EF数据交互接口
        └── Repositoies                         //EF数据交互实现【数据持久化】
```

## 💻项目进度

> 项目基础框架基本搭建完成，可以 `Star` :star: 关注一下，:pray:谢谢。

+  [项目进度](./README_SCHEDULE.md)

## 📄项目规范

> `Clone` 项目后，可以在 `Apifox` 中查看项目中数据接口的请求参数、请求方式和请求规则 `JsonSchema` 等等

+  [Apifox API 在线文档](https://apifox.com/apidoc/shared-a1ef2dce-1084-4da5-8bdb-18aaec8dd93a)

## :file_folder:数据库文件

> 以 `.sql`  后缀的数据文件 

+  [Mysql 项目初始化数据文件](./LayuiAdminNetProSql.sql)

## :rainbow:免责说明

+ `Layuiadmin`  相关代码版权归 [Layui](https://www.ilayuis.com/) 官方所有；除此之外，其他代码来自开源项目或作者原创；请尊重版权、开源和原创。

## 💕联系作者

> 对项目有疑问、建议或期待的朋友可以加我好友。

- **wechat** ：`yejiancong1105`