//+----------------------------------------------------------------------  
//| 功能：公用ajax调用函数(jQuery)
//| 说明： 
//| 参数：
//| url:,type:,data:,async:,asyncCall:;
//| 返回值：
//| 创建人：沈金龙
//| 创建时间：2016-1-29 17:26:42
//+---------------------------------------------------------------------
function myAjax(url, type, data, async, asyncCall) {
    var result;
    $.ajax({
        type: type,
        url: url,
        data: data,
        async: async,
        dataType: "text",
        //服务器返回的数据，描述状态的字符串
        success: function (data, textStatus) {
            //回调函数            
            if (asyncCall != undefined)
                asyncCall(data, textStatus);
            if (async == false)
                result = data;
        },
        //XMLHttpRequest对象、错误信息、捕获的错误对象
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myAjaxError(XMLHttpRequest, textStatus, errorThrown);
        }
    });
    //回调函数调用在返回值之前
    return result;
}

//原生异步调用函数
function myAjaxXml() {
    //toodoo
}

//+----------------------------------------------------------------------  
//| 功能：公用ajax错误处理函数(jQuery)
//| 说明： 
//| 参数：
//| xmlHttpRequest：XMLHttpRequest对象
//| textStatus：错误信息
//| errorThrown：捕获的错误对象
//| 返回值：
//| 创建人：沈金龙
//| 创建时间：2016-1-29 17:26:42
//+---------------------------------------------------------------------
function myAjaxError(xmlHttpRequest, textStatus, errorThrown){
    alert("这是ajax调用失败的错误处理函数！");
}





/*--------------------shenjl Add On 2016-01-29 合法性校验 Begin --------------------*/

//检查Url是否合法
function checkUrl(value) {
    if (value != "") {
        var reg = new RegExp("^((https|http)://)(([0-9]{1,3}.){3}([0-9]{1,3})|([0-9a-z_!~*'()-]+.)*([0-9a-z][0-9a-z-]{0,61})?[0-9a-z].[a-z]{2,6})(:[0-9]{1,5})?((/?)|(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$");
        return reg.test(value);
    }
    return true;
}

//检查Email是否合法
function checkEmail(value) {
    if (value != "") {
        var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
        return reg.test(value)
    }
    return true;
}

//检查Mobile是否合法
function checkMobile(value) {
    if (value != "") {
        var reg = /^1[0-9]{10}$/;
        return reg.test(value)
    }
    return true;
}

/*--------------------shenjl Add On 2016-01-29 合法性校验 End   --------------------*/


