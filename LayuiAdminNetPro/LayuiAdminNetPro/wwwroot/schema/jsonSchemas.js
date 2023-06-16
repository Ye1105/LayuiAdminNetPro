/*
 * @Author: 15868707168@163.com 15868707168@163.com
 * @Date: 2023-06-05 11:44:20
 * @LastEditors: 15868707168@163.com 15868707168@163.com
 * @LastEditTime: 2023-06-05 13:45:31
 * @FilePath: \登录界面\js\JsonSchemas.js
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
 */

var jsonSchemas = {
    /*
     * @{key}   
     * @{res,data,msg}
     */
    getSchema: function (key) {
        if (!key) return { res: false, msg: "参数错误,请退出重新登陆" };
        let schemas = localStorage.getItem("JsonSchemas")
        if (schemas) {
            let data = JSON.parse(schemas)
            if (data.hasOwnProperty(key)) {
                return { res: true, data: data[key] }
            }
            else
                return { res: false, msg: "schema规则不存在,请退出重新登陆" }
        } else {
            this.setSchemas();
            //setTimeout(() => {
            //    this.getSchema(key);
            //}, 500);
            return { res: false, msg: "重载schemas列表，请重新提交" }
        }
    },
    setSchemas: function () {
        try {
            //添加 | 更新 schemas
            fetch('/schema/JsonSchemas.json', { mode: 'cors' })
                .then((response) => response.json())
                .then((json) => {
                    if (json != 'undefined' && json != "") {
                        console.log("创建schema")
                        localStorage.removeItem("JsonSchemas")
                        localStorage.setItem("JsonSchemas", JSON.stringify(json))
                    }
                })
        } catch (e) {
            console.log(e)
        }
    }
}
