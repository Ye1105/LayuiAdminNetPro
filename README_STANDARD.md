<h1 align="center" >🐌 LayuiAdminNetPro </h1>  

<div align="center"> 
<p> 使用规范 </p>
</div>

## :zap: 模板

### 内容页基础模板

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="renderer" content="webkit">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <title>welcome</title>
    <link type="text/css" rel="stylesheet" href="~/layuiadmin/layui/css/layui.css" media="all" />
    <link type="text/css" rel="stylesheet" href="~/layuiadmin/style/admin.css" media="all">
</head>
<body>
    <div class="layui-fluid">
        <div class="layui-row layui-col-space15">
            <!--公告模块-->
            <div class="layui-col-md12">
                <code>管理列表</code>
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript" src="~/layuiadmin/layui/layui.js"></script>
<script type="text/javascript">
    layui.config({
        base: '../layuiadmin/' //静态资源所在路径
    }).extend({
        index: 'lib/index' //主入口模块
    }).use("index")
</script>
```

