
function GetDataFromXMLHTTP(sFile, strType, strGUID, strTxt, strPostStream) {
    var rdNum = Math.random();
    var oHTTP = window.XMLHttpRequest ? new window.XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");
    var strTmp = "";

    if (sFile.indexOf("?") == -1) {
        strTmp = "?";
    }
    else {
        strTmp = "&";
    }

    var sUrl = sFile + strTmp + "ywtype=" + escape(strType) + "&ywonlyflag=" + escape(strGUID) + "&ywtxt=" + escape(strTxt) + "&rdnum=" + rdNum;

    if (strPostStream == undefined) {
        oHTTP.open("GET", sUrl, false);
        oHTTP.send();
    }
    else {
        oHTTP.open("POST", sUrl, false);
        oHTTP.send(strPostStream);
    }

    var bSuccess = handleXMLErr(oHTTP.responseXML);
    if (bSuccess) {
        if (oHTTP.responseText == "服务器超时") window.navigate("/ErrPage.aspx?errid=001");
        return oHTTP.responseText;
    }
    else {
        alert("取数失败，请与系统管理员联系！");
        return "-1";
    }
}

function MyXMLHTTPRequest(url, postData, asycCall) {
    if (url == undefined || url == null || url == "") {
        throw new Error("请求失败：请求文件路径为空！");
    }
    var isAsyc = (typeof (asycCall) == "function" ? true : false);

    //将会话状态信息传入通道页面 modified by xq at 2011.12.21
    var sMySessionState = "";
    if (document.all["___MYSESSIONSTATE"]) sMySessionState = "MySessionState=" + document.all["___MYSESSIONSTATE"].value;
    if (sMySessionState != "") url = url + (url.indexOf("?") < 0 ? "?" : "&") + sMySessionState;

    var rdNum = Math.random();
    var oHTTP = new ActiveXObject("Microsoft.XMLHTTP");
    if (url.toLowerCase().indexOf("rdnum") < 0) url = url + (url.indexOf("?") < 0 ? "?" : "&") + "rdnum=" + rdNum;

    //绑定异步回调函数
    if (isAsyc) {
        oHTTP.onreadystatechange = function () {
            if (oHTTP.readyState == 4) {
                handleResponseText(oHTTP.responseText);
            }
            asycCall(oHTTP);
        }
    }

    //发送请求
    if (postData == undefined || postData == null || postData == "") {
        oHTTP.open("GET", url, isAsyc);
        setXMLHttpRequestHeader(oHTTP); //设置请求头，以便处理异常时和普通请求区分处理
        oHTTP.send();
    }
    else {
        oHTTP.open("POST", url, isAsyc);
        setXMLHttpRequestHeader(oHTTP)
        oHTTP.send(postData);
    }

    //处理同步请求结果
    if (!isAsyc) {
        //校验数据通道的返回值，如果是Global.asax.vb异常捕获后的返回值，则重定向，并直接抛出异常。
        handleResponseText(oHTTP.responseText);
        return oHTTP;
    }
}

function handleXMLErr(xml, bContinue) {
    if (bContinue == null) bContinue = false;
    if (xml.parseError.errorCode != 0) {
        //alert("XML Parser Error: " + xml.parseError.reason);
        alert("XML解析错误: " + xml.parseError.reason);
        if (!bContinue) {
            return ERROR_STOP;
        }
        else {
            return ERROR_CONTINUE;
        }
    }
    var node = xml.selectSingleNode("/error");
    if (node) {
        if (!bContinue) {
            //openStdDlg("/_common/error/dlg_error.aspx?hresult=" + node.selectSingleNode("number").text, null, 400, 200);
            alert("发生运行时错误！");
            return ERROR_STOP;
        }
        else {
            return ERROR_CONTINUE;
        }
    }
    return ERROR_NONE;
}

function handleResponseText(strResponseText) {
    var msg = "", errorid = "", sParams = "";
    try {
        var xmlDom = new ActiveXObject("Microsoft.XMLDOM");
        xmlDom.loadXML(strResponseText);

        //只处理从Global.asax.vb返回的异常信息
        var attr = xmlDom.documentElement.attributes;
        if (attr.getNamedItem("xtype") && attr.getNamedItem("result") && attr.getNamedItem("xtype").text == "XMLHttpRequest" && attr.getNamedItem("result").text == "false") {
            msg = attr.getNamedItem("errormessage").text;
            errorid = attr.getNamedItem("errorid").text;
        }
        else {
            return;
        }
    }
    catch (e) {
        return;
    }

    if (errorid == "") {
        sParams = "errmessage=" + escape(msg);
    }
    else {
        sParams = "errid=" + errorid;
    }

    openMyDlg("错误提示", "/ErrPage.aspx", sParams, null, 400, 190);

    throw new Error("数据通道调用出现异常：" + msg);
}
