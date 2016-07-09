//版本号：1.0.0.0 2016-1-31 15:42:34   

//+----------------------------------------------------------------------  
//| 功能：公用ajax调用函数(jQuery)
//| 说明：支持是否异步
//| 参数：url:页面地址,type:数据传递方式,data:要Post的数据,async:是否异步,asyncCall:回调函数;
//| 返回值：如果是同步调用，则返回后台返回的值
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

//+----------------------------------------------------------------------  
//| 功能：公用ajax调用函数(jQuery)
//| 说明：
//| 参数：
//| url:页面地址,
//| method:处理函数,需要有[WebMethod]的标识,
//| data:参数列表,一定要是json格式的字符串,
//| asyncCall:回调函数;
//| 返回值：
//| 创建人：沈金龙
//| 创建时间：2016-1-29 17:26:42
//+---------------------------------------------------------------------
function myAjaxWeb(url, method, data, asyncCall) {
    var xurl = url + "/" + method;
    var xdata = data == undefined ? "" : data;
    $.ajax({
        type: "Post", //WebMethod方法只接受post类型的请求           
        url: xurl,    //方法所在页面和方法名
        data: xdata,   //一定要是json格式的字符串
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            //返回的数据用data.d获取内容   
            if (asyncCall != undefined)
                asyncCall(data.d, textStatus);
        },
        //XMLHttpRequest对象、错误信息、捕获的错误对象
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myAjaxError(XMLHttpRequest, textStatus, errorThrown);
        }
    });
}

//+----------------------------------------------------------------------  
//| 功能：原生异步调用函数
//| 说明：
//| 参数：
//| url:页面地址,
//| method:处理函数,需要有[WebMethod]的标识,
//| data:参数列表,一定要是json格式的字符串,
//| asyncCall:回调函数;
//| 返回值：
//| 创建人：沈金龙
//| 创建时间：2016-1-31 15:55:21
//+---------------------------------------------------------------------
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
    var msg = "status:" + xmlHttpRequest.status + "\n";
    msg += "Info:" + xmlHttpRequest.statusText + "\n";
    msg += "ResponseText:" + xmlHttpRequest.responseText + "\n";
    alert(msg);
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


