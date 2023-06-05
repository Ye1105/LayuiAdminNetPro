﻿/*
 * @Author: 15868707168@163.com 15868707168@163.com
 * @Date: 2023-06-05 11:44:20
 * @LastEditors: 15868707168@163.com 15868707168@163.com
 * @LastEditTime: 2023-06-05 13:45:31
 * @FilePath: \登录界面\js\JsonSchemas.js
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
 */

var jsonSchemas = {
    /*
     * {key} require  
     */
    getSchema: function (key) {

        if (!localStorage.getItem("JsonSchemas"))
            this.setSchemas();

        if (!key) return "";
        let schemas = localStorage.getItem("JsonSchemas")
        if (schemas) {
            let data = JSON.parse(schemas)
            if (data.hasOwnProperty(key)) {
                return data[key]
            }
            else
                return ""
        }
    },
    setSchemas: function () {
        try {
            //添加 | 更新 schemas
            fetch('./schema/JsonSchemas.json', { mode: 'cors' })
                .then((response) => response.json())
                .then((json) => {
                    if (json != 'undefined' && json != "") {
                        localStorage.removeItem("JsonSchemas")
                        localStorage.setItem("JsonSchemas", JSON.stringify(json))
                    }
                })
        } catch (e) {
            console.log(e)
        }
    }
}