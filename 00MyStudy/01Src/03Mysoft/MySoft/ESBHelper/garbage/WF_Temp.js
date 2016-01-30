/**********************************begin 通道函数使用的myEscape、escapeUrl方法不需要UTF-8，否则导致ERP303之前版本调用时会乱码 ******************************************************************/

//通过XMLHTTP通道异步取数
//参数：sFile		-	页面地址
//		sType		-	业务类别
//		sGUID		-	业务GUID
//		sText		-	其它参数
//		args		-	大容量业务参数
//		func		-   异步执行的函数名，即最终处理XmlHTTP返回数据的函数，其形式如： abc(sText)
//		extra		-   扩展参数
function GetDataFromXmlHTTPAsync(sFile, sType, sGUID, sText, args, func, extra) {
    var rdNum = Math.random();
    var url;
    if (sFile.search(/\?/g) > 0)
        url = sFile + "&ywtype=" + myEscapeForWF(sType) + "&ywonlyflag=" + myEscapeForWF(sGUID) + "&ywtxt=" + myEscapeForWF(sText) + "&rdnum=" + rdNum;
    else
        url = sFile + "?ywtype=" + myEscapeForWF(sType) + "&ywonlyflag=" + myEscapeForWF(sGUID) + "&ywtxt=" + myEscapeForWF(sText) + "&rdnum=" + rdNum;

    myXmlHttpForWF.callByAsync(url, args, func, extra);
}


//通过XMLHTTP通道不刷新页面从后台取数
//参数：sFile		-	打开文件
//		sType		-	业务对象
//		sGUID		-	业务GUID
//		sText		-	其它参数
//		args		-	大容量业务参数
function GetDataFromXMLHTTP(sFile, sType, sGUID, sText, args) {
    var rdNum = Math.random();
    var url;
    if (sFile.search(/\?/g) > 0)
        url = sFile + "&ywtype=" + myEscapeForWF(sType) + "&ywonlyflag=" + myEscapeForWF(sGUID) + "&ywtxt=" + myEscapeForWF(sText) + "&rdnum=" + rdNum;
    else
        url = sFile + "?ywtype=" + myEscapeForWF(sType) + "&ywonlyflag=" + myEscapeForWF(sGUID) + "&ywtxt=" + myEscapeForWF(sText) + "&rdnum=" + rdNum;

    return myXmlHttpForWF.getTextBySync(url, args);
}

//XMLHTTP通道公用函数 (请统一使用此公用函数)
//参数： url  通道页面的路径及参数
//       postData  POST到后端的数据。用于传递大量数据
//       asycCall  异步回调函数。在onreadystatechange时被调用。
function MyXMLHTTPRequestForWF(url, postData, asycCall, beforeSendCallback) {
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
        };
    }
    //发送请求
    if (postData == undefined || postData == null || postData == "") {
        oHTTP.open("GET", escapeUrlForWF(url), isAsyc);
        setXMLHttpRequestHeader(oHTTP); //设置请求头，以便处理异常时和普通请求区分处理
        if (typeof (beforeSendCallback) == "function") {
            beforeSendCallback(oHTTP);
        }
        oHTTP.send();
    }
    else {
        oHTTP.open("POST", escapeUrlForWF(url), isAsyc);
        setXMLHttpRequestHeader(oHTTP);
        if (typeof (beforeSendCallback) == "function") {
            beforeSendCallback(oHTTP);
        }
        //oHTTP.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        oHTTP.send(postData);
    }
    //处理同步请求结果
    if (!isAsyc) {
        //校验数据通道的返回值，如果是Global.asax.vb异常捕获后的返回值，则重定向，并直接抛出异常。
        handleResponseText(oHTTP.responseText);
        return oHTTP;
    }
}


var myXmlHttpForWF = new function () {
    this.createXMLHttp = function () {
        if (window.XMLHttpRequest) {
            var xmlObj = new XMLHttpRequest();
        }
        else {
            var MSXML = ['MSXML2.XMLHTTP.6.0', 'MSXML2.XMLHTTP.3.0', 'Microsoft.XMLHTTP'];
            for (var n = 0; n < MSXML.length; n++) {
                try {
                    var xmlObj = new ActiveXObject(MSXML[n]);
                    break;
                }
                catch (e) { logException(e); }
            }
        }
        return xmlObj;
    }

    var WF_XMLHTTP_PATH = "/myworkflow/wf_xmlhttp.aspx";
    var PUB_XMLHTTP_PATH = "/myworkflow/pub_xmlhttp.aspx";

    //异步调用
    this.callByAsync = function (url, data, callback, extra) {
        var XMLHTTP_Async = function () { };

        if (typeof (callback) == "function") {
            XMLHTTP_Async = function (objXMLHttp) {
                if (objXMLHttp.readyState == 4) {
                    if (objXMLHttp.status == 200 || objXMLHttp.status == 304) {
                        if (objXMLHttp.responseText == "服务器超时") {
                            window.navigate(escapeUrlForWF("/ErrPage.aspx?errid=001"));
                            return "-1";
                        }
                        if (url.toLowerCase().indexOf(WF_XMLHTTP_PATH.toLowerCase()) == 0 ||
                            url.toLowerCase().indexOf(PUB_XMLHTTP_PATH.toLowerCase()) == 0) {

                            //如果请求的页面时工作里引擎通道页面，并且没有标识就跳转错误页面返回-1
                            if (objXMLHttp.getResponseHeader("Is_WorkFlow_Xml_Http_Page") != "1") {
                                window.navigate(escapeUrlForWF("/ErrPage.aspx?errid=001"));
                                return "-1";
                            }
                        }
                        if (extra != null) {
                            callback(objXMLHttp.responseText, extra);
                        }
                        else {
                            callback(objXMLHttp.responseText);
                        }
                    }
                }
            }
        }

        MyXMLHTTPRequestForWF(url, data, XMLHTTP_Async);
    }

    //同步调用
    this.getTextBySync = function (url, data) {
        var objXMLHttp = null;

        objXMLHttp = MyXMLHTTPRequestForWF(url, data, null);

        if (objXMLHttp.readyState == 4 && objXMLHttp.status == 200) {
            var s = objXMLHttp.responseText;
            if (s == "服务器超时") {
                window.navigate(escapeUrlForWF("/ErrPage.aspx?errid=001"));
                return "-1";
            }
            if (url.toLowerCase().indexOf(WF_XMLHTTP_PATH.toLowerCase()) == 0 ||
                url.toLowerCase().indexOf(PUB_XMLHTTP_PATH.toLowerCase()) == 0) {

                //如果请求的页面时工作里引擎通道页面，并且没有标识就跳转错误页面返回-1
                if (objXMLHttp.getResponseHeader("Is_WorkFlow_Xml_Http_Page") != "1") {
                    window.navigate(escapeUrlForWF("/ErrPage.aspx?errid=001"));
                    return "-1";
                }
            }
            return s;
        }
        else {
            alert("操作失败！");
            return "-1";
        }
    }
}



function myEscapeForWF(s, b) {
    if (s == undefined) return "undefined";
    if (s == null) return "null";
    if (s === "") return "";

    s = escape(s);

    if (b === true) {
        s = s.replace(/\+/g, "%u002B");
    }

    s = s.replace(/%([0-9A-Fa-f]{2})/g, "%u00$1");

    return s;
}
//功能：分析整个URL找出里面未编码的参数使用myEscape进行编码
function escapeUrlForWF(s) {
    if (s == null || typeof (s) == "undefined" || s == "") {
        return s;
    }
    if (s && s.href) {  //处理替换location对象的问题
        s = s.href;
    }

    var pos = s.indexOf("?");
    if (pos < 0) return s;
    var url = s.substring(0, pos + 1);
    var str = s.substring(pos + 1);
    var reg = new RegExp("[^\u0000-\u00ff]|[\u00a8\u00b1\u00d7\u00f7\u00b0\u00a4\u00a7\u00b7\u002b]", "g");
    var matches = str.match(reg);
    if (matches != null) {
        for (var i = 0; i < matches.length; i++) {
            var c = matches[i];
            str = str.replace(c, myEscapeForWF(c));
        }
    }
    return url + str;
}

/**********************************end 通道函数使用的myEscape、escapeUrl方法不需要UTF-8，否则导致ERP303之前版本调用时会乱码 ******************************************************************/

/*--------------------------------------------------业务相关函数-----------------------------------------------------------------*/
//功能：加保护
//参数 pt  
//		修订保护 0 
//		批注保护 1 
//		窗体域保护   2
//		只读保护 3  (只有2003以上版本才支持)
//参数 noreset
//有关Protect 方法的第二个参数 使用true 还是 false:
//如果值为 False，则将窗体域重新设置为其默认值。如果值为 True，而指定文档又是处于保护状态，则保留当前窗体域的值。
//如果 Type 不是 wdAllowOnlyFormFields，则忽略 NoReset 参数。 
//所以
//在发起的时候加文档保护需要参数 false
//在审批的时候加文档保护需要参数 true
function SetDocProtection(wordobj, pt, noreset) {
    if (typeof (isMysoftProtected) != "undefined" && isMysoftProtected == 0) {
        if (noreset == undefined) noreset = true;
        var oDocument = wordobj.ActiveDocument;

        if (oDocument.ProtectionType > -1) {
            oDocument.Unprotect("MYsoft2006WORD");
            if (oDocument.ProtectionType == 2) oDocument.TrackRevisions = false;
        }

        if (pt == 2) oDocument.TrackRevisions = true;

        if (pt == 3 && parseFloat(oDocument.Application.Version) < 11.0) pt = 2;	//2003以下版本没有只读保护

        oDocument.Protect(pt, noreset, "MYsoft2006WORD");
    }
}

//功能：格式化数字
// number 数字 
// pattern 
//    alert(formatNumber(0,'')); 
//    alert(formatNumber(12432.21,'#,###')); 
//    alert(formatNumber(12432.21,'#,###.000#')); 
//    alert(formatNumber(12432,'#,###.00')); 
//    alert(formatNumber('12432.415','#,###.0#')); 
function formatNumber(number, pattern) {
    number = number.toString().replace(/,/ig, "");
    var minus;
    if (!isNaN(number) && number < 0) {
        minus = "-";
        number = 0 - number;
    }
    else {
        minus = "";
    }

    var str = number.toString();

    if (str.length == 0) return "";
    var strInt;
    var strFloat;
    var formatInt;
    var formatFloat;
    if (/\./g.test(pattern)) {
        formatInt = pattern.split('.')[0];
        formatFloat = pattern.split('.')[1];
    } else {
        formatInt = pattern;
        formatFloat = null;
    }
    if (/\./g.test(str)) {
        if (formatFloat != null) {
            var tempFloat = calcDoubleFix(Math.round(calcDoubleFix(parseFloat('0.' + str.split('.')[1]), Math.pow(10, formatFloat.length), '*')), Math.pow(10, formatFloat.length), '/');
            strInt = (calcDoubleFix(Math.floor(number), Math.floor(tempFloat), '+')).toString();
            strFloat = /\./g.test(tempFloat.toString()) ? tempFloat.toString().split('.')[1] : '0';
        } else {
            strInt = Math.round(number).toString();
            strFloat = '0';
        }
    } else {
        strInt = str;
        strFloat = '0';
    }
    if (formatInt != null) {
        var outputInt = '';
        var zero = formatInt.match(/0*$/)[0].length;
        var comma = null;
        if (/,/g.test(formatInt)) {
            comma = formatInt.match(/,[^,]*/)[0].length - 1;
        }
        var newReg = new RegExp('(\\d{' + comma + '})', 'g');
        if (strInt.length < zero) {
            outputInt = new Array(zero + 1).join('0') + strInt;
            outputInt = outputInt.substr(outputInt.length - zero, zero)
        } else {
            outputInt = strInt;
        }
        var
        outputInt = outputInt.substr(0, outputInt.length % comma) + outputInt.substring(outputInt.length % comma).replace(newReg, (comma != null ? ',' : '') + '$1')
        outputInt = outputInt.replace(/^,/, '');
        strInt = outputInt;
    }
    if (formatFloat != null) {
        var outputFloat = '';
        var zero = formatFloat.match(/^0*/)[0].length;
        if (strFloat.length < zero) {
            outputFloat = strFloat + new Array(zero + 1).join('0');
            //outputFloat        = outputFloat.substring(0,formatFloat.length);
            var outputFloat1 = outputFloat.substring(0, zero);
            var outputFloat2 = outputFloat.substring(zero, formatFloat.length);
            outputFloat = outputFloat1 + outputFloat2.replace(/0*$/, '');
        } else {
            outputFloat = strFloat.substring(0, formatFloat.lengtgetDomainDataTypeFromBOh);
        }
        strFloat = outputFloat;
    } else {
        if (pattern != '' || (pattern == '' && strFloat == '0')) {
            strFloat = '';
        }
    }
    return minus + strInt + (strFloat == '' ? '' : '.' + strFloat);
}

//得到所有HTMLdomain对象
function getHTMLDomainField(objContext) {
    var inputlist, selectlist, textarealist;
    var objlist = new Array();
    var i, j;

    inputlist = objContext.getElementsByTagName("INPUT");
    selectlist = objContext.getElementsByTagName("SELECT");
    textarealist = objContext.getElementsByTagName("TEXTAREA");
    linklist = objContext.getElementsByTagName("BUTTON");

    for (i = 0, j = inputlist.length; i < j; i++) {
        objlist.push(inputlist[i]);
    }

    for (i = 0, j = selectlist.length; i < j; i++) {
        objlist.push(selectlist[i]);
    }

    for (i = 0, j = textarealist.length; i < j; i++) {
        objlist.push(textarealist[i]);
    }
    var linkojb = "";
    for (i = 0, j = linklist.length; i < j; i++) {
        if (linklist[i].dm_name) {
            //只有第一个链接管用，其他的隐藏不显示
            if (linkojb == "") {
                objlist.push(linklist[i]);
                linkojb = linklist[i].dm_name;
            } else {
                linklist[i].style.display = "none";
            }
        }
    }
    return objlist;
}


//功能：转换流程状态名称
//参数：流程状态(枚举的数字)
function ConvertProcessStatus(processStatus) {
    var s = "";

    switch (processStatus) {
        case "-3":
            s = "草稿";
            break;
        case "-2":
            s = "已作废";
            break;
        case "-1":
            s = "已终止";
            break;
        case "0":
            s = "处理中";
            break;
        case "1":
            s = "已通过";
            break;
        case "2":
            s = "已归档";
            break;
    }

    return s;
}

//功能：选择流程模版
//参数：业务类型
//返回：LookupItems()
function selectProcessModule(sBusinessType) {
    var sFile = "/MyWorkflow/WF_SelectProcessModule.aspx"
    var parms = "businesstype=" + myEscapeForWF(sBusinessType);;
    var sRtn = openMyDlg("选择流程模版", sFile, parms, "", 700, 500);

    return sRtn;
}

//功能：业务模块发起流程
//参数：sBusinessGUID	-- 业务GUID
//		sBusinessType	-- 业务类型
//		bCheckBusiness	-- 是否需要校验流程实例存在与否，默认校验
//      upfiles			-- 在发起前准备的附件  格式：显示文件名,存放于upfiles目录下的物理文件名;	
//      processTitle    -- 流程标题
//      processNo       -- 流程编号
//      moduleGUID      -- 流程模板GUID
//attachmentDocTypes    -- 附件文档类型
function initiateBusinessProcess(sBusinessGUID, sBusinessType, bCheckBusiness, upfiles, processTitle, processNo, moduleGUID, attachmentDocTypes) {
    if (bCheckBusiness == undefined) bCheckBusiness = true;
    if (upfiles == undefined) upfiles = "";
    if (attachmentDocTypes == undefined) attachmentDocTypes = "";

    var xml = GetDataFromXMLHTTP("/MyWorkflow/WF_XMLHTTP.aspx", "PROCESSENTITYISEXISTS", sBusinessGUID, sBusinessType, "");
    if (xml == "-1") return false;

    var Dom = XMLDOM();

    Dom.loadXML(xml);

    if (Dom.parseError.errorCode != 0) return false;

    if (bCheckBusiness) //需要校验流程实例已经存在
    {
        if (Dom.selectSingleNode("/Xml/Entity") != null) {

            var ownername = Dom.selectSingleNode("/Xml/Entity").getAttribute("ownername");
            if (Dom.selectSingleNode("/Xml/Entity").attributes.getNamedItem("existsdraft").value == "true") {
                if (Dom.selectSingleNode("/Xml/Entity").attributes.getNamedItem("enable").value == "0") {
                    alert("该业务已经有[" + ownername + "]保存的流程草稿，不能重复发起！");
                    return false;
                }

                sFile = "/MyWorkflow/WF_ProcessInitiate_Form_Transfer.aspx?mode=2&opentype=bizsys&oid=" + myEscapeForWF(Dom.selectSingleNode("/Xml/Entity").attributes.getNamedItem("processguid").value);
                openInitApprovalFullWin(sFile);
                return true;
            }

            if (Dom.selectSingleNode("/Xml/Entity").attributes.getNamedItem("exists").value == "true") {
                //如果是重新发起步骤,且当前用户是发起人,打开待办界面,否则打开查看界面
                if (Dom.selectSingleNode("/Xml/Entity").attributes.getNamedItem("steptype").value == "0") {
                    if (Dom.selectSingleNode("/Xml/Entity").attributes.getNamedItem("enable").value == "0" ||
		               Dom.selectSingleNode("/Xml/Entity").attributes.getNamedItem("nodeguid").value == "") {
                        sFile = "/MyWorkflow/WF_ProcessInitiate_Form_Transfer.aspx?mode=3&oid=" + myEscapeForWF(Dom.selectSingleNode("/Xml/Entity").attributes.getNamedItem("processguid").value);
                    }
                    else {
                        sFile = "/MyWorkflow/WF_ProcessHandle_Form_Transfer.aspx?mode=2&oid=" + myEscapeForWF(Dom.selectSingleNode("/Xml/Entity").attributes.getNamedItem("nodeguid").value);
                    }
                    openInitApprovalFullWin(sFile);
                    return true;
                }

                alert("该业务已经有[" + ownername + "]发起的流程，不能重复发起流程！");
                return false;
            }
        }
    }

    //业务子系统没有传递模板GUID,则默认为空串
    var moduleGUID = moduleGUID == undefined ? "" : moduleGUID;
    if (moduleGUID == "") {
        if (Dom.selectNodes("//Module").length == 0) {
            alert("没有对应的流程模板可选择，请先定义流程模板！");
            return false;
        }

        if (Dom.selectNodes("//Module").length == 1) {
            moduleGUID = Dom.selectSingleNode("/Xml/Modules/Module").attributes.getNamedItem("guid").value;
        }
        else {
            var ls = selectProcessModule(sBusinessType);
            if (ls != undefined && ls.items.length > 0) {
                moduleGUID = ls.items[0].id;
            }
        }
    } else {    //2012-08-01 by SunF：如果业务系统直接传递过来了指定的流程模板，则需要校验该模板是否合法（如：是否启用）
        var strResult = GetDataFromXMLHTTP("/MyWorkflow/WF_XMLHTTP.aspx", "ProcessModuleCheck", moduleGUID, "", "");
        var xmlDom = XMLDOM();
        xmlDom.loadXML(strResult);

        if (xmlDom.documentElement.attributes.getNamedItem("result").text == "false")		//校验不通过
        {
            alert(xmlDom.documentElement.attributes.getNamedItem("errormessage").text);
            return false;
        }
    }

    if (moduleGUID.length > 0) {
        sFile = "/MyWorkflow/WF_ProcessInitiate_Form_Transfer.aspx?mode=1&opentype=bizsys&processguid=" + myEscapeForWF(moduleGUID) + "&businessGUID=" + myEscapeForWF(sBusinessGUID) + "&upfiles=" + myEscapeForWF(upfiles) + "&attachmentDocTypes=" + myEscapeForWF(attachmentDocTypes);
        if (processTitle && processTitle != "") sFile += "&processTitle=" + myEscapeForWF(processTitle);
        if (processNo && processNo) sFile += "&processNo=" + myEscapeForWF(processNo);
        openInitApprovalFullWin(sFile);
        return true;
    }

}

//功能：业务模块查看/审批流程
function openBuisinessProcess(sProcessGUID) {
    if (sProcessGUID == "") return false;

    var xml = GetDataFromXMLHTTP("/MyWorkflow/WF_XMLHTTP.aspx", "BusinessProcessHandle", "", sProcessGUID, "")
    if (xml == "-1") return false;

    var Dom = XMLDOM();

    Dom.loadXML(xml);

    if (Dom.parseError.errorCode != 0) return false;

    var status = Dom.documentElement.attributes.getNamedItem("status").value;
    var url = "";

    /*
		Draft = -3              ' 草稿
		Cancel = -2             ' 已作废
		NoPass = -1             ' 已终止
		Active = 0              ' 处理中 ,注意如果涉及被打回步骤，流程状态仍然属于在办
		Pass = 1                ' 已通过
		Book = 2                ' 已归档
	*/
    switch (status) {
        case "0":
        case "1":
            var nodes = Dom.selectNodes("//Node");
            if (nodes.length == 1) {
                url = "/MyWorkflow/WF_ProcessHandle_Form_Transfer.aspx?mode=2&oid=" + nodes[0].attributes.getNamedItem("guid").value;
            }
            else if (nodes.length > 1) {
                var filter = "";
                for (var i = 0; i < nodes.length; i++) {
                    filter += "'" + nodes[i].attributes.getNamedItem("guid").value + "'" + (i == nodes.length - 1 ? "" : ",");
                }

                var sRtn = openMyDlg("选择待办节点", "/MyWorkflow/WF_SelectProcessNode.aspx", "", filter, 700, 500);
                if (sRtn != undefined) {
                    url = "/MyWorkflow/WF_ProcessHandle_Form_Transfer.aspx?mode=2&oid=" + sRtn;
                }
                else {
                    return false;
                }
            }
            else {
                url = "/MyWorkflow/WF_ProcessInitiate_Form_Transfer.aspx?mode=3&oid=" + sProcessGUID;
            }

            break;

        case "-1":
        case "-2":
        case "2":
            url = "/MyWorkflow/WF_ProcessInitiate_Form_Transfer.aspx?mode=3&oid=" + sProcessGUID;
            break;

        case "-3":
            if (Dom.selectSingleNode("/Process/Node").attributes.getNamedItem("IsOwner").value == "true") {
                url = "/MyWorkflow/WF_ProcessInitiate_Form_Transfer.aspx?mode=2&oid=" + sProcessGUID;
            }
            else {
                url = "/MyWorkflow/WF_ProcessInitiate_Form_Transfer.aspx?mode=3&oid=" + sProcessGUID;
            }
            break;

        default:
            alert("流程尚未发起！");
            return false;
            break;
    }

    if (url.length > 0) {
        openInitApprovalFullWin(url + "&opentype=bizsys");
        return true;
    }
    else {
        return false;
    }
}


function openBusinessObjectInterfaceTest(businessTypeName, businessGuid) {
    var sFile = "/MyWorkflow/WF_BusinessObjectInterfaceTest.aspx?businessTypeName=" + myEscapeForWF(businessTypeName) + "&businessGuid=" + businessGuid;
    openInitApprovalFullWin(sFile);
}


// 功能：新增流程模板
function openAddProcessModule(documentType, processKindGuid) {
    if (documentType == undefined || documentType == "") {
        alert("流程模板文档类型不能为空！");
    }
    if (processKindGuid == undefined || processKindGuid == "") {
        alert("流程分类不能为空！");
    }

    var sPath;
    //工作流站点地址
    var sWfSite = GetWorkflowSiteUrl();

    sPath = sWfSite + "/MyWorkflowManagement/WF_ProcessDefined_Form.aspx?mode=1&processKindGUID=" + processKindGuid + "&documentType=" + documentType;

    switch (documentType) {
        case 0:
            //业务表单
            sPath += "&xml=" + myEscapeForWF("/MyWorkflowManagement/WF_ProcessDefined_Form.xml");
            break;

        case 1:
            //普通文档
            sPath += "&xml=" + myEscapeForWF("/MyWorkflowManagement/WF_ProcessDefined_Form_Doc.xml");
            break;
        case 2:
            //扩展表单
            sPath += "&xml=" + myEscapeForWF("/MyWorkflowManagement/WF_ProcessDefined_Form_Extend.xml");
            break;
    }

    openMyFullWin(sPath);

}

//查看流程模板
function openViewProcessModule(processGuid) {
    openProcessModule(processGuid, 3);
}

//编辑流程模板
function openModifyProcessModule(processGuid) {
    openProcessModule(processGuid, 2);
}
//业务系统查看流程模板函数
//processGUID:流程模板GUID
//mode: 2编辑；3 查看
function openProcessModule(processGuid, mode) {
    if (processGuid == "") { return; }

    //获取流程定义GUID
    var processDefinitionGUID = GetDataFromXMLHTTP("/MyWorkflow/WF_XmlHTTP.aspx", "GetProcessDefinitionGUID", processGuid);
    //processDefinitionGUID为空，表明流程定义不存在
    if (processDefinitionGUID == "") { return; }


    //获取流程模板类别
    var documentType = GetDataFromXMLHTTP("/MyWorkflow/WF_XmlHTTP.aspx", "GetProcessModuledocumentType", processGuid);
    //documentType为空，表明流程模板不存在
    if (documentType == "") { return; }

    //工作流站点地址
    var sWfSite = GetWorkflowSiteUrl();

    var sPath = sWfSite + "/MyWorkflowManagement/WF_ProcessDefined_Form.aspx?mode=" + mode + "&oid=" + myEscapeForWF(processDefinitionGUID);
    //表单类型
    switch (parseInt(documentType)) {
        case 0:
            //业务表单
            sPath += "&xml=" + myEscapeForWF("/MyWorkflowManagement/WF_ProcessDefined_Form.xml");
            break;
        case 1:
            //普通文档
            sPath += "&xml=" + myEscapeForWF("/MyWorkflowManagement/WF_ProcessDefined_Form_Doc.xml");
            break;

        case 2:
            //扩展表单
            sPath += "&xml=" + myEscapeForWF("/MyWorkflowManagement/WF_ProcessDefined_Form_Extend.xml");
            break;
    }

    openMyFullWin(sPath);

}



//跨域调用列表刷新afterworkflow函数
//约定，arg必须是字符串
function callOpenerAfterWorkflowFunction(processGuid) {

    try {

        opener.afterWorkflow(processGuid);

    } catch (e) {
        var frame = getTempFrame();
        frame.contentWindow.name = processGuid;

        var siteUrl = GetWorkflowSiteUrl();
        var crossDomainJsCallerUrl = siteUrl + "/MyWorkflow/CrossDomainCallAfterWorkflow.html";
        frame.contentWindow.location = crossDomainJsCallerUrl;
    }
}

function getTempFrame() {
    var frame = document.createElement("iframe");
    frame.style.width = 0;
    frame.style.height = 0;
    frame.style.display = "none";
    document.body.appendChild(frame);

    return frame;
}


//获取工作流站点地址
function GetWorkflowSiteUrl() {
    return GetDataFromXMLHTTP("/MyWorkflow/WF_XmlHTTP.aspx", "GetWorkflowSiteUrl");
}



// 功能：以 open 方式打开全屏窗口，只用于发起，审批页面
// 主要增加了 scrollbars=yes 参数，解决由于缺少参数，页面窗口无滚动条，导致只能使用body元素滚动条的，position定位出现bug的问题
function openInitApprovalFullWin(sPath, sName) {
    var iX = window.screen.availWidth - 10;
    var iY = window.screen.availHeight - 50;
    if (!sName) sName = "";
    try {
        return window.open(escapeUrlForWF(sPath), sName, "left=0,top=0,scrollbars=yes,width=" + iX + ",height=" + iY + ",status=1,resizable=1");
    }
    catch (e) { logException(e); }
}




/*--------------------------------------------------业务相关函数-----------------------------------------------------------------*/









/*--------------------------------------------------公共对象、函数-----------------------------------------------------------------*/




//XMLDOM
function XMLDOM() {
    var progIDs = ["Msxml2.DOMDocument.6.0", "Msxml2.DOMDocument.3.0", "Microsoft.XMLDOM"]; // MSXML5.0, MSXML4.0 and Msxml2.DOMDocument all have issues - be careful when using.

    for (var i = 0; i < progIDs.length; i++) {
        try {
            var myDom = new ActiveXObject(progIDs[i]);
            myDom.async = false;
            return myDom;
        }
        catch (e) { logException(e); }
    }

    return null;
}


//XSLT转换公用函数
//参数：sxml		-	XML内容
//		sXslURL		-	Xslt地址	
function XmlToHTML(sXml, sXslURL) {
    var sRtn;
    var xmlDom = XMLDOM();
    var xslDom = XMLDOM();

    xmlDom.loadXML(sXml);
    if (xmlDom.parseError.errorCode != 0) return "";

    xslDom.load(sXslURL);
    if (xslDom.parseError.errorCode != 0) return "";

    sRtn = xmlDom.documentElement.transformNode(xslDom);

    delete (xslDom);
    delete (xmlDom);

    return sRtn;
}

//字符串替换函数
function myReplace(expression, find, replacewith) {
    var r = new RegExp(find, "ig");

    return expression.replace(r, replacewith);
}



/*--------------------------------------------------公共对象、函数-----------------------------------------------------------------*/


/*--------------------------------------------------lookup 函数-----------------------------------------------------------------*/
//功能：选择域
//参数：
//      param			-- 参数值 
//      paramtype		-- 参数类型：GUID/XML  (目前采用的都是第二种方式)
//      selecttype		-- 枚举如下
//                          0 单选 业务域
//                          1 复选 业务域+空白域
//                          2 单选 业务域+空白域 
//                          3 复选 业务域，去除重复域Group
//                          4 复选 业务域+空白域，去除重复域Group
//                          5 单选 业务域+空白域，去除重复域Group
//                          6 单选 只显示文本区
//                          7 复选 只显修改数据链接
//                          8 复选 业务域+空白域，去除超链接域
//      defaultdomain	-- 默认选中的域串：以","分隔 
function OpenSelectDomain(param, paramtype, selecttype, defaultdomain) {
    if (paramtype == undefined) paramtype = "GUID";
    if (selecttype == undefined) selecttype = "0";
    if (defaultdomain == undefined) defaultdomain = "";

    var sTitle, sURL, sParams, sArgs, iX, iY;

    sTitle = "选择域";
    if (selecttype == "7") {
        sTitle = "选择可修改业务数据链接";
    }
    sURL = "/MyWorkflowManagement/WF_SelectDomain.aspx";
    sArgs = "";
    sParams = "paramtype=" + myEscapeForWF(paramtype);
    sParams += "&selecttype=" + myEscapeForWF(selecttype);

    if (paramtype == "GUID") {
        sParams = "&businessTypeGUID=" + myEscapeForWF(param);
        sParams += "&defaultdomain=" + myEscapeForWF(defaultdomain);
    }
    else	//参数是业务对象XML
    {
        sParams += "&defaultdomain=";

        if (param != undefined && param.xml) {
            var node = param.createElement("DefaultDomain");
            node.text = defaultdomain;
            param.documentElement.appendChild(node);
        }

        sArgs = param;
    }

    iX = 350;
    iY = 520;

    var sReturn = openMyDlg(sTitle, sURL, sParams, sArgs, iX, iY);

    if (sReturn != undefined)
        return sReturn;
    else
        return null;
}

function OpenSelectBusinessLink(strBusinessObjectGUID, strBusinessLink, strSelectType) {
    //1.设置LookUp加载数据格式
    var domainXmlDom = new ActiveXObject("Microsoft.XMLDOM");
    domainXmlDom.loadXML("<BusinessType><Item></Item></BusinessType>");
    //2.设置默认勾选值
    var xnBusinessLink = domainXmlDom.createElement("DefaultBusinessLink");
    xnBusinessLink.text = strBusinessLink;
    domainXmlDom.documentElement.appendChild(xnBusinessLink);
    //3.设置显示状态（单选|多选）
    var xnSelectType = domainXmlDom.createElement("DefaultSelectType");
    xnSelectType.text = strSelectType;
    domainXmlDom.documentElement.appendChild(xnSelectType);
    //4.LookUp
    var sTitle = "业务链接";
    var sFile = "/MyWorkflowManagement/SelectBusinessLink.aspx?BusinessObjectGUID=" + strBusinessObjectGUID;
    var sUrl = "/FrameTemp0.aspx";
    var sParams = "title=" + myEscapeForWF(sTitle) + "&filename=" + myEscapeForWF(sFile) + "&selecttype=" + myEscapeForWF(strSelectType);
    var sArgs = domainXmlDom;
    var sWidth = "360px";
    var sHeight = "500px";
    return openMyDlg(sTitle, sUrl, sParams, sArgs, sWidth, sHeight);
}
//功能：选择用户
function OpenSelectMultiUser(o, isOnlyShowUserBUs) {

    var url;
    var sOids, sNames;
    var sHeight, sWidth;
    var objValue = document.getElementById(o.valueid);
    var objName = document.getElementById(o.textid);
    if (objValue == undefined) {
        sOids = "";
    }
    else {
        sOids = objValue.value;
    }

    if (objName == undefined) {
        sNames = "";
    }
    else {
        sNames = objName.value;
    }

    if (!isOnlyShowUserBUs) {
        isOnlyShowUserBUs = false;
    }

    if (o.ismultiselect == "true") {
        sHeight = "528px";
        sWidth = "720px";
        url = "/MyWorkflow/Interface/Pub/selectUserMulti.aspx?class=user&browse=0&rowslimit=" + o.rowslimit + "&isOnlyShowUserBUs=" + isOnlyShowUserBUs.toString() + "&rdm=" + Math.random();
    }
    else {
        sHeight = "500px";
        sWidth = "520px";
        url = "/MyWorkflow/Interface/Pub/SelectUserSingle.aspx?class=user&browse=0&isOnlyShowUserBUs=" + isOnlyShowUserBUs.toString() + "&rdm=" + Math.random();
    }

    var args = new LookupArgsClass();
    var item;
    var arrValue = sOids.split(";");
    var arrName = sNames.split(";");
    var arrItem = new Array();
    if (arrValue.length == arrValue.length) {
        for (i = 0; i < arrValue.length; i++) {
            item = document.createElement("span");
            item.oid = arrValue[i];
            item.otype = "";
            item.innerHTML = "<span><img class='lui' src='/_imgs/ico_16_16.gif'>" + arrName[i] + "</span>";
            arrItem[i] = item;
        }
    }


    args.items = arrItem;

    var lookupItems = window.showModalDialog(escapeUrlForWF(url), args, "dialogWidth:" + sWidth + ";dialogHeight:" + sHeight);
    var sText = "";
    var sValue = "";
    var sType = "";

    if (lookupItems != null && o.textid != null && o.valueid != null) {
        if (lookupItems.items.length > 0) {
            for (var i = 0; i < lookupItems.items.length; i++) {
                sText += (sText == "" ? "" : ";") + lookupItems.items[i].name;
                sValue += (sValue == "" ? "" : ";") + lookupItems.items[i].id;
                sType += (sType == "" ? "5" : ";5");
            }
        }

        document.all(o.textid).value = sText;
        document.all(o.valueid).value = sValue;
    }
}
/*--------------------------------------------------lookup 函数-----------------------------------------------------------------*/





/*--------------------------------------------------Html表单函数-----------------------------------------------------------------*/
//数据表单前端绑定
//objContext, 表单容器
//domainEditList 可编辑列表,注意
//domainNoVisibleList 不可见列表
//domainXML
//loadDefault:是否取默认值  false -- 不取 / true -- 取
function DataBindForm(objContext, domainEditList, domainNoVisibleList, domainXML, loadDefault) {
    try {
        if (loadDefault == undefined) loadDefault = false;	//默认不取默认值 

        var inputlist, selectlist, textarealist;
        var objlist = new Array();
        var i, m, j;
        var xmlDom = XMLDOM();

        var bFocus = false;

        if (domainXML && domainXML.length > 0) {
            xmlDom.loadXML(domainXML);

            if (xmlDom.parseError.errorCode != 0)
                xmlDom = null;
        }
        else {
            xmlDom = null;
        }

        if (xmlDom == null) return false;

        var i, j;
        var objlist;
        objlist = getHTMLDomainField(objContext);

        //是否取默认值  填充要 初始化时
        if (loadDefault) {

            //初始化时，原有的："[发起人部门名称]", "[流程分类]""[本人姓名]", "[当前公司名称]", "[本人]", "[当前公司]","[今天]", "[现在]"
            var sKeyWords = "";
            //收集 初始化时 的关键字
            for (i = 0; i < objlist.length; i++) {
                if (objlist[i].dm_defaultvalue) {
                    if (objlist[i].dm_defaultvalue.indexOf("[") > -1 && objlist[i].dm_defaultvalue.indexOf("]") > -1) {
                        var mykey = objlist[i].dm_defaultvalue.substring(objlist[i].dm_defaultvalue.indexOf("["), objlist[i].dm_defaultvalue.indexOf("]") + 1);
                        if ("[本人姓名], [当前公司名称], [本人], [当前公司], [今天], [现在]".indexOf(mykey) > -1) {
                            //初始化时处理关键字数组
                            sKeyWords += (sKeyWords.length > 0 ? "," : "") + mykey;
                        }

                    }
                }
            }
            if (sKeyWords.length > 0) {
                var keyWordDom = XMLDOM();

                keyWordDom.async = false;
                //YiQQ:2014.03.12
                keyWordDom.loadXML(WorkflowPublicProxy.GetKeyWordsXml(sKeyWords));
            }
            var initDept = ""; //发起人部门
            var strprocesskind = ""; //流程分类
            try {
                initDept = GetDataFromXMLHTTP("/MyWorkflow/WF_XmlHTTP.aspx", "GetUserDepartment", "", "", "");
                strprocesskind = appForm.ProcessKindName.getValue();
            } catch (e) { }
            for (i = 0; i < objlist.length; i++) {
                if (objlist[i].dm_defaultvalue && objlist[i].dm_name) {
                    var sDefaultValue, sKeyWord;

                    sDefaultValue = objlist[i].dm_defaultvalue;

                    if (sDefaultValue.indexOf("[") > -1 && sDefaultValue.indexOf("]") > -1)	//含关键字
                    {
                        try {
                            sKeyWord = sDefaultValue.substring(sDefaultValue.indexOf("["), sDefaultValue.indexOf("]") + 1);
                            if ("[发起人部门名称]" == sKeyWord) {
                                sDefaultValue = sDefaultValue.replace(sKeyWord, initDept);
                            } else if ("[流程分类]" == sKeyWord) {
                                sDefaultValue = sDefaultValue.replace(sKeyWord, strprocesskind);
                            } else if ("[本人姓名], [当前公司名称], [本人], [当前公司], [今天], [现在]".indexOf(sKeyWord) > -1) {
                                sDefaultValue = sDefaultValue.replace(sKeyWord, keyWordDom.selectSingleNode("//KeyWord[@Name='" + sKeyWord + "']").attributes.getNamedItem("Value").value);
                                if ("[本人], [当前公司]".indexOf(sKeyWord) > -1) {
                                    sDefaultValue = sDefaultValue.toUpperCase();
                                }
                            } else {
                                sDefaultValue = "";
                            }
                        }
                        catch (e) {
                            logException(e);
                            sDefaultValue = "";
                        }
                    }

                    //修复默认值不覆盖域的值
                    var curDomain = xmlDom.selectSingleNode("//Domain[@name='" + objlist[i].dm_name + "']");
                    if (curDomain) {
                        if (curDomain.text == "") curDomain.text = sDefaultValue;
                    }
                    else {
                        objlist[i].value = sDefaultValue;
                    }
                }

            }
        }

        domainEditList = ',' + domainEditList + ",";
        domainNoVisibleList = ',' + domainNoVisibleList + ",";

        //2 绑定处理

        //2.1可重复表格域（动态表单域）
        tables = objContext.getElementsByTagName("TABLE");
        for (var i = 0; i < tables.length; i++) {
            if (tables[i].group_name && tables[i].group_name.length > 0) {
                var group = xmlDom.selectSingleNode("/BusinessType/Item/Group[@name='" + tables[i].group_name + "']");
                if (group) {
                    var arrItems = group.selectNodes("./Item");

                    var dynamicRow = tables[i].rows[1];

                    for (var j = 0; j < arrItems.length; j++) {
                        var objRow;
                        var lastRow;
                        if (j == 0) {
                            objRow = dynamicRow;
                        }
                        else {
                            objRow = dynamicRow.cloneNode(true);
                            //dynamicRow.parentElement.appendChild(objRow);
                            lastRow.insertAdjacentElement("AfterEnd", objRow);
                        }

                        var dynamicList = getHTMLDomainField(objRow);

                        var canEditRow = "false";

                        //标示可重复表格的行Index
                        objRow.dm_rowIndex = j + 1;
                        //控件的行Index
                        for (var k = 0; k < dynamicList.length; k++) {
                            dynamicList[k].id = dynamicList[k].dm_name + "_" + (j + 1);
                            dynamicList[k].dm_rowIndex = j + 1;
                            dynamicList[k].dm_dynamic = "1";
                            dynamicList[k].dm_group = tables[i].group_name;

                            if (domainEditList.indexOf(',' + dynamicList[k].dm_name + ',') > -1) {
                                canEditRow = "true";
                            }
                        }

                        //标示行是否可编辑
                        objRow.canEdit = canEditRow;

                        //绑定行数据 
                        bindDomain(dynamicList, arrItems[j], domainNoVisibleList, domainEditList);

                        lastRow = objRow;
                    }

                    tables[i]._originalRow = tables[i].rows[1].cloneNode(true);
                }
            }
        }

        //2.2普通域
        bindDomain(objlist, xmlDom.selectSingleNode("/BusinessType/Item"), domainNoVisibleList, domainEditList);

        //添加样式
        if (!objContext.styleSheets.length) objContext.createStyleSheet();

        with (objContext.styleSheets(objContext.styleSheets.length - 1)) {
            addRule("input", "border:0px;");
        }

        //禁止右键
        //objContext.oncontextmenu = function() {return false;}
        objContext.onselect = function () { return true; }
        objContext.onselectstart = function () { return true; }
    }
    catch (e) {
        logException(e);
        alert("流程模板定义有误，请联系流程创建人！");
        return false;
    }

    return true;
}

function bindDomain(objlist, domainNodes, domainNoVisibleList, domainEditList) {
    for (i = 0; i < objlist.length; i++) {
        var domainNode = domainNodes.selectSingleNode("Domain[@name='" + objlist[i].dm_name + "']")
        if (domainNode) {
            domainvalue = domainNode.text;

            switch (objlist[i].tagName) {
                case 'INPUT':

                    if (!objlist[i].dm_displaytype) objlist[i].dm_displaytype = "text";
                    switch (objlist[i].dm_displaytype.toLowerCase()) {
                        case "textarea":
                            objlist[i].value = "";
                            if (GetLen(domainvalue) > Number(objlist[i].dm_len)) {
                                objlist[i].textarea = substr(domainvalue, Number(objlist[i].dm_len))
                            } else {
                                objlist[i].textarea = domainvalue;
                            }
                            break;
                        case "number":
                            objlist[i].value = formatNumber(domainvalue, getPattern(objlist[i].acc, objlist[i].grp))
                            break;

                        case "datetime":
                            objlist[i].value = formatDate(domainvalue, objlist[i].dm_time == "1");
                            break;

                        case "select":
                            objlist[i].returnValue = "";
                            objlist[i].value = "";
                            var arrTemp = objlist[i].dm_dropdownoptions.split("|");
                            var arrTemp2;
                            for (var j = 0; j < arrTemp.length; j++) {
                                arrTemp2 = arrTemp[j].split(",");
                                if (domainvalue == arrTemp2[0]) {
                                    objlist[i].returnValue = domainvalue;
                                    objlist[i].value = arrTemp2[1];
                                    break;
                                }
                            }
                            break;

                        default:
                            objlist[i].value = domainvalue;
                            break;
                    }

                    break;
            }
        }
        //过滤修改业务数据链接
        if (objlist[i].tagName == "BUTTON") {
            if (appForm.ModifyBusinessDataDomain && appForm.ModifyBusinessDataDomain.getValue() != "") {
                objlist[i].disabled = false;
                objlist[i].style.display = "";

            } else {
                objlist[i].style.display = "none";
            }
        } else {
            if (domainNoVisibleList.indexOf(',' + objlist[i].dm_name + ',') > -1) {
                //不可见域不显示
                objlist[i].style.display = "none";
            }

            //不可编辑的disabled,可编辑的enabled
            if (domainEditList.indexOf(',' + objlist[i].dm_name + ',') > -1) {
                objlist[i].disabled = false;
            }
            else {
                objlist[i].disabled = true;
            }
        }
    }
}

//功能：检验表单必填项
//参数 objContext 
//参数 editdomainlist
//返回值 true/false
function CheckRequired(objContext, editdomainlist) {
    if (editdomainlist.length == 0) return true;

    var objlist;
    var sName;
    var isReq = 0;
    var sValue;

    editdomainlist = ',' + editdomainlist + ",";
    objlist = getHTMLDomainField(objContext);

    for (var i = 0; i < objlist.length ; i++) {
        sName = objlist[i].dm_name;
        isReq = objlist[i].dm_req;
        if (objlist[i].tagName == "INPUT" && objlist[i].className == "selectBox") {
            sName = objlist[i].parentElement.parentElement.parentElement.parentElement.parentElement.dm_name;
            isReq = objlist[i].parentElement.parentElement.parentElement.parentElement.parentElement.dm_req;
        }

        if (editdomainlist.indexOf(',' + sName + ',') > -1 && isReq == "1") {
            sValue = getDomainValue(objlist[i]);
            if (Trim(sValue).length == 0) {
                try {
                    if (objlist[i].dm_name && objlist[i].dm_displaytype == "radio") {
                        alert("表单域“" + sName + "”的值不允许为空，请在表单中选择！");
                        //如果是 单选框 使第一个选项获取光标
                        var sradio = objContext.getElementById(objlist[i].dm_name + "_0");
                        if (sradio) sradio.focus();
                    } else {
                        alert("表单域“" + sName + "”的值不允许为空，请在表单中填写！");
                        objlist[i].focus();
                    }
                } catch (e) { logException(e); }
                return false;
            }

        }
    }

    return true;
}

//获取表单域控件的值
function getDomainValue(domain) {
    var domainValue = "";

    switch (domain.tagName) {
        case 'INPUT':

            if (domain.returnValue && domain.dm_displaytype != "datetime") {
                domainValue = domain.returnValue;
            }
            else if (domain.dm_displaytype == "number" || domain.dm_displaytype == "sum" || domain.dm_type == "decimal" || domain.dm_type == "money") {
                domainValue = domain.value.replace(/,/g, "");
            }
            else if (domain.dm_displaytype == "textarea") { //Added By FUYL on 2012-10-09 文本区隐藏时的取textarea.
                domainValue = domain.textarea;
            }
            else {
                domainValue = domain.value;
            }

            break;
        case 'SELECT':
            domainValue = domain.options(domain.selectedIndex).value;
            break;
        case 'TEXTAREA':
            domainValue = domain.innerText;
            break;
    }

    return domainValue;
}


//功能：从文档中取值到domainXML中
//参数 objContext 
//参数 domainxml
//返回值 已经被插入值的domainxml
function SetHTMLDomainXMLValue(objContext, domainxml, editdomainlist) {
    var domainDom = XMLDOM();

    domainDom.loadXML(domainxml);

    if (domainDom.parseError.errorCode != 0) return "";

    tables = objContext.getElementsByTagName("TABLE");
    for (var i = 0, tblen = tables.length; i < tblen; i++) {
        if (tables[i].group_name && tables[i].group_name.length > 0) {
            var item = null;
            var group = domainDom.selectSingleNode("//Group[@name='" + tables[i].group_name + "']");
            if (group != null) {
                item = group.selectSingleNode("Item")
                group.parentNode.removeChild(group);
            }
            group = domainDom.createElement("Group");
            group.setAttribute("name", tables[i].group_name);
            //行
            for (var j = 0, rowlen = tables[i].rows.length; j < rowlen; j++) {
                var oTr = tables[i].rows[j];
                if (oTr.dm_rowIndex != undefined && oTr.dm_rowIndex != "") {
                    groupitem = item.cloneNode(true);
                    groupitem.setAttribute("rowIndex", j);
                    //列
                    for (var k = 0, domainlen = groupitem.childNodes.length; k < domainlen; k++) {
                        var domainControl, domainValue, domainName = groupitem.childNodes[k].getAttribute("name"), domainType = groupitem.childNodes[k].getAttribute("displaytype");
                        //修复可重复域中值重复的问题
                        //						if (domainType=="datetime" || domainType=="textarea") 
                        //						    domainControl = oTr.all(domainName);
                        //						else
                        //						    domainControl = oTr.all(domainName +"_"+ oTr.dm_rowIndex);

                        //将之前按照显示类型判断的条件修改为直接按name取值
                        domainControl = oTr.all(domainName);
                        if (!domainControl) continue;

                        if (domainType == "select" && domainControl.tagName != "INPUT") domainControl = domainControl.getElementsByTagName("Input")[0];
                        if (domainControl == null) continue;

                        groupitem.selectSingleNode("Domain[@name='" + domainName + "']").text = "";
                        //用cdata存储
                        var cdata = domainDom.createCDATASection(getDomainValue(domainControl));
                        groupitem.selectSingleNode("Domain[@name='" + domainName + "']").appendChild(cdata);

                    }
                    //XML中添加行
                    group.appendChild(groupitem);
                }
            }
            domainDom.selectSingleNode("/BusinessType/Item").appendChild(group);
        }
    }

    //普通域
    var i;
    var objlist;
    var sName;

    objlist = getHTMLDomainField(objContext);
    editdomainlist = ',' + editdomainlist + ",";

    for (i = 0; i < objlist.length; i++) {
        if (objlist[i].dm_dynamic == undefined || objlist[i].dm_dynamic != "1") {
            sName = objlist[i].dm_name;
            if (objlist[i].tagName == "INPUT" && objlist[i].className == "selectBox")
                sName = objlist[i].parentElement.parentElement.parentElement.parentElement.parentElement.dm_name;

            var domainNode = domainDom.documentElement.selectSingleNode("/BusinessType/Item/Domain[@name='" + sName + "']")
            if (domainNode) {
                domainNode.text = "";
                //用Cdata存储保留空格
                var cdata = domainDom.createCDATASection(getDomainValue(objlist[i]));
                domainNode.appendChild(cdata);
            }
        }
    }
    return domainDom.xml;
}


//取选择的单选框显示值
function getRadioSelectText(domain) {
    var arrTemp = domain.dm_dropdownoptions.split("|");
    for (var j = 0; j < arrTemp.length; j++) {
        var arrTemp2 = arrTemp[j].split(",");
        if (arrTemp2.length == 2 && domain.value == arrTemp2[0]) {
            return arrTemp2[1];
        }
    }
    return "";
}
//设置单选框的显示值
function setRadioSelectText(domain) {
    var radios = domain.parentElement.getElementsByTagName("INPUT");
    for (var i = 0; i < radios.length; i++) {
        if (radios[i].type == "radio" && radios[i].name == domain.name && radios[i].value == domain.value) {
            radios[i].checked = true;
            return;
        }
    }
}
////表单域应用样式 -  单选和复选
function applyRadioAndCheckStyle(objContext) {
    var sTemp = "";
    switch (objContext.dm_displaytype) {
        case "radio":       //单选框
            //设置单选选项
            var arrTemp = objContext.dm_dropdownoptions.split("|");
            var arrTemp2;
            //添加空选项
            var isChecked = false; //是否已选中,如果已选中,再次匹配也不会选中(确保只选中一个)
            for (var j = 0; j < arrTemp.length; j++) {
                arrTemp2 = arrTemp[j].split(",");
                if (arrTemp2.length == 2) {
                    sTemp += '<span style="' + objContext.style.cssText + ';width:auto;"><input type="radio" id="' + objContext.id + '_' + j + '" name="' + objContext.id + '_name"';
                    sTemp += ' value="' + HtmlEncode(arrTemp2[0]) + '" ' + (objContext.disabled ? 'disabled' : '');
                    if (objContext.value == arrTemp2[0] && !isChecked) {
                        sTemp += ' checked ';
                        isChecked = true;
                    }

                    sTemp += ' onclick="document.getElementById(\'' + objContext.id + '\').value=this.value;RecalcAll();"/><label for="' + objContext.id + '_' + j + '">' + HtmlEncode(arrTemp2[1]) + '</label></span>';
                }
            }
            if (!isChecked) {
                //没有匹配的值,清空Input的值.
                objContext.value = "";
            }
            objContext.style.display = "none";
            sTemp = objContext.outerHTML + sTemp;
            break;
        case "checkbox":       //复选框
            //设置单选选项
            //添加空选项
            sTemp = '<span style="' + objContext.style.cssText + ';width:auto;"><input type="checkbox" id="' + objContext.id + '_0" name="' + objContext.name + '" ';
            sTemp += ' title="' + HtmlEncode(objContext.name) + '"';
            if (objContext.value == "1") {
                sTemp += " checked ";
            }
            sTemp += (objContext.disabled ? 'disabled' : '') + ' onclick="document.getElementById(\'' + objContext.id + '\').value=this.checked?1:0;RecalcAll();"/><label for="' + objContext.id + '_0">' + objContext.name + '</label></span>';

            objContext.style.display = "none";
            sTemp = objContext.outerHTML + sTemp;
            break;
    }
    return sTemp;
}

//表单域应用样式
function applyFormFieldStyle(objContext) {
    try {
        var objList;
        var sTemp;

        objList = getHTMLDomainField(objContext);
        for (var i = 0; i < objList.length; i++) {
            //不可见域不处理
            if (objList[i].style.display == "none") continue;

            if (objList[i].disabled)		//不可编辑的ＩＮＰＵＴ只需转换TextArea / Hidden
            {
                switch (objList[i].dm_displaytype) {
                    case "hidden":
                        objList[i].style.display = "none";
                        break;

                    case "textarea":
                        sTemp = '<textarea name="' + objList[i].name + '"';
                        sTemp += ' maxlength="' + objList[i].dm_len + '"';
                        sTemp += ' style="border:0px;overflow:visible;' + objList[i].style.cssText + '"';
                        sTemp += ' readonly';

                        for (var j = 0; j < objList[i].attributes.length; j++) {
                            if (objList[i].attributes[j].name.substring(0, 3) == "dm_") {
                                sTemp += " " + objList[i].attributes[j].name;
                                sTemp += '="' + HtmlEncode(objList[i].attributes[j].value) + '"';
                            }
                        }
                        sTemp += ' title="' + HtmlEncode(objList[i].textarea) + '"';
                        sTemp += '>' + HtmlEncode(objList[i].textarea) + '</textarea>';
                        objList[i].outerHTML = sTemp;
                        break;
                    case "radio":
                    case "checkbox":
                        objList[i].outerHTML = applyRadioAndCheckStyle(objList[i]);
                        break;
                    case "link":
                        sTemp = '<a href=\"javascript:void(0);\" onclick=\"parent.linkDomainClick(\'' + objList[i].dm_linkurl + '\');" style="' + objList[i].style.cssText + ';" >' + (objList[i].dm_isuser == "0" ? HtmlEncode(objList[i].value) : objList[i].dm_displaytext) + '</a>';
                        objList[i].style.display = "none";
                        objList[i].outerHTML = objList[i].outerHTML + sTemp;
                        break;
                    default:
                        objList[i].style.borderWidth = "0";
                        objList[i].readOnly = true;
                        objList[i].disabled = false;

                        if (objList[i].dm_type == "decimal" || objList[i].dm_type == "money") objList[i].style.textAlign = "right";
                }

                continue;
            }
            else	//可编辑的ＩＮＰＵＴ进行ｈｔｃ转换
            {
                switch (objList[i].tagName) {
                    case "INPUT":
                        switch (objList[i].dm_displaytype) {
                            case "datetime":
                                //日期
                                sTemp = '<table cellpadding="0" cellspacing="0"';
                                sTemp += ' style="TABLE-LAYOUT:fixed;display:inline;width:' + objList[i].style.width + ';" >';
                                sTemp += '<COLGROUP><col><col width="40"></COLGROUP>';
                                sTemp += '<tr><td><input type="text" mapType="dtm" time="';
                                sTemp += (objList[i].dm_time == "1") ? "1" : "0";
                                sTemp += '" class="dtm" maxlength="';
                                sTemp += (objList[i].dm_time == "1") ? "16" : "10";
                                sTemp += '"';
                                sTemp += ' style="' + objList[i].style.cssText + ';width:100%;"';
                                sTemp += ' name="' + objList[i].name + '"';
                                sTemp += ' value="' + (objList[i].dm_time == "1" ? objList[i].value : objList[i].value.split(" ")[0]) + '"';
                                sTemp += ' returnValue="' + (objList[i].dm_time == "1" ? objList[i].value : objList[i].value.split(" ")[0]) + '"';
                                if (objList[i].disabled) sTemp += ' readonly';
                                for (var j = 0; j < objList[i].attributes.length; j++) {
                                    if (objList[i].attributes[j].name.substring(0, 3) == "dm_") {
                                        sTemp += " " + objList[i].attributes[j].name;
                                        sTemp += '="' + HtmlEncode(objList[i].attributes[j].value) + '"';
                                    }
                                }
                                sTemp += ' onreturnvaluechange="try{RecalcAll()}catch(e){event.returnValue=false;showError(e);logException(e)}"'; 	//计算表达式
                                sTemp += '></td><td style="PADDING-LEFT:4px"><img class="dtm" src="/_imgs/btn_off_cal.gif"></td></tr></table>';
                                objList[i].outerHTML = sTemp;
                                break;

                            case "number":
                                if (!objList[i].acc) objList[i].acc = 2;
                                objList[i].className = "num";
                                if (!objList[i].min) objList[i].min = 0;
                                if (!objList[i].max) objList[i].max = 99999999;
                                objList[i].returnValue = objList[i].value;
                                objList[i].defaultValue = objList[i].value;
                                objList[i].readOnly = objList[i].disabled;
                                objList[i].onreturnvaluechange = function () { try { RecalcAll() } catch (e) { event.returnValue = false; showError(e); logException(e) } };
                                break;

                            case "text":
                                objList[i].className = "txt";
                                objList[i].maxLength = (objList[i].dm_len) ? objList[i].dm_len : "50";
                                objList[i].readOnly = objList[i].disabled;
                                objList[i].onchange = RecalcAll;
                                objList[i].forbiddenchars = ",@&<>'";
                                break;

                            case "textarea": 	//文本区
                                sTemp = '<textarea name="' + objList[i].name + '"';
                                if (objList[i].dm_len) {
                                    sTemp += ' maxlength="' + objList[i].dm_len + '"';
                                }
                                else {
                                    sTemp += ' maxlength="50"';
                                }
                                sTemp += ' style="' + objList[i].style.cssText + ';"';
                                sTemp += ' forbiddenchars=",@&amp;&lt;&gt;\'"';
                                if (objList[i].disabled) sTemp += ' readonly';
                                for (var j = 0; j < objList[i].attributes.length; j++) {
                                    if (objList[i].attributes[j].name.substring(0, 3) == "dm_") {
                                        sTemp += " " + objList[i].attributes[j].name;
                                        sTemp += '="' + HtmlEncode(objList[i].attributes[j].value) + '"';
                                    }
                                }
                                sTemp += ' onchange="RecalcAll()"'; 	//计算表达式
                                sTemp += '>' + HtmlEncode(objList[i].textarea) + '</textarea>';
                                objList[i].outerHTML = sTemp;
                                break;

                            case "select": 		//下拉类型
                                sTemp = '<span class="selectBox" typename="mapType"';
                                sTemp += ' name="' + objList[i].name + '"';
                                sTemp += ' id="' + objList[i].id + '"';
                                sTemp += ' value="' + HtmlEncode(objList[i].returnValue) + '"';
                                sTemp += ' style="' + objList[i].style.cssText + '"';
                                if (objList[i].disabled) sTemp += ' disabled';
                                for (var j = 0; j < objList[i].attributes.length; j++) {
                                    if (objList[i].attributes[j].name.substring(0, 3) == "dm_") {
                                        sTemp += " " + objList[i].attributes[j].name;
                                        sTemp += '="' + HtmlEncode(objList[i].attributes[j].value) + '"';
                                    }
                                }
                                sTemp += ' changeHandler="RecalcAll()"'; 	//计算表达式
                                sTemp += '>';

                                sTemp += '<table id="_' + objList[i].id + '_Table" cellspacing="0" cellpadding="2" style="display:none;">';
                                //设置下拉选项
                                var arrTemp = objList[i].dm_dropdownoptions.split("|");
                                var arrTemp2;
                                //添加空选项
                                sTemp += '<tr height="18"><td nowrap val="">&nbsp;</td></tr>';
                                for (var j = 0; j < arrTemp.length; j++) {
                                    arrTemp2 = arrTemp[j].split(",");
                                    if (arrTemp2.length == 2) {
                                        sTemp += '<tr height="18"><td nowrap val="' + HtmlEncode(arrTemp2[0]) + '">' + HtmlEncode(arrTemp2[1]) + '</td></tr>';
                                    }
                                }
                                sTemp += '</table></span>'

                                objList[i].outerHTML = sTemp;
                                break;
                            case "radio":
                            case "checkbox":
                                objList[i].outerHTML = applyRadioAndCheckStyle(objList[i]);
                                break;
                            case "assistant_r": 	//辅助录入(只读)
                                sTemp = '<table cellpadding="0" cellspacing="0" border="0" width="' + objList[i].clientWidth + '"';
                                sTemp += ' style="TABLE-LAYOUT:fixed;display:inline;"';
                                sTemp += '><col><col width="25" align="right"><tr>';
                                sTemp += '<td><input type="text" style="' + objList[i].style.cssText + ';width:100%;" disabled class="txt" onkeydown="event.returnValue=false"';
                                sTemp += ' name="' + objList[i].name + '"';
                                sTemp += ' value="' + HtmlEncode(objList[i].value) + '"';
                                for (var j = 0; j < objList[i].attributes.length; j++) {
                                    if (objList[i].attributes[j].name.substring(0, 3) == "dm_") {
                                        sTemp += " " + objList[i].attributes[j].name;
                                        sTemp += '="' + HtmlEncode(objList[i].attributes[j].value) + '"';
                                    }
                                }
                                sTemp += '></td>';
                                sTemp += '<td align="right"><img style="cursor:hand" src="/_imgs/btn_off_lookup.gif" onclick="try{assistant(this);}catch(e){	logException(e);	}"></td>';
                                sTemp += '</tr></table>';
                                objList[i].outerHTML = sTemp;
                                break;

                            case "assistant_w": 	//辅助录入(可写)
                                sTemp = '<table cellpadding="0" cellspacing="0" border="0" width="' + objList[i].clientWidth + '"';
                                sTemp += ' style="TABLE-LAYOUT:fixed;display:inline;"';
                                sTemp += '><col><col width="25" align="right"><tr>';
                                sTemp += '<td><input type="text" style="' + objList[i].style.cssText + ';width:100%;" class="txt"';
                                sTemp += ' name="' + objList[i].name + '"';
                                sTemp += ' value="' + HtmlEncode(objList[i].value) + '"';
                                for (var j = 0; j < objList[i].attributes.length; j++) {
                                    if (objList[i].attributes[j].name.substring(0, 3) == "dm_") {
                                        sTemp += " " + objList[i].attributes[j].name;
                                        sTemp += '="' + HtmlEncode(objList[i].attributes[j].value) + '"';
                                    }
                                }
                                sTemp += ' onchange="RecalcAll()"'; 	//计算表达式
                                sTemp += '></td>';
                                sTemp += '<td align="right"><img style="cursor:hand" src="/_imgs/btn_off_lookup.gif" onclick="try{assistant(this);}catch(e){	logException(e);	}"></td>';
                                sTemp += '</tr></table>';
                                objList[i].outerHTML = sTemp;
                                break;

                            case "hidden": 		//隐藏
                                objList[i].style.display = "none";
                                break;

                            case "calc": 		//计算域
                            case "sum": 			//合计
                                objList[i].className = "txt";
                                objList[i].maxLength = 2147483647; 	//不限长度
                                objList[i].readOnly = true;

                                if (objList[i].dm_type == "decimal" || objList[i].dm_type == "money") objList[i].style.textAlign = "right";

                                break;
                            case "lookup": 		//Lookup 查找类型

                                break;

                            default: 			// 默认为文本类型
                                objList[i].className = "txt";
                                objList[i].maxLength = (objList[i].dm_len) ? objList[i].dm_len : 50;
                                objList[i].readOnly = objList[i].disabled;
                        }
                        break;

                    case "SELECT":
                        break;

                    case "TEXTAREA":
                        break;

                    case "BUTTON":
                        sTemp = "<a href=\"javascript:void(0);\" onclick=\"parent.openModifyUrl( '" + objList[i].dm_name + "', '', " + objList[i].dm_configWidth + ", " + objList[i].dm_configHeight + ");\" title=\"" + objList[i].dm_name + "\" style=\"color:blue\"><u>" + objList[i].dm_name + "</u></a>";
                        objList[i].outerHTML = sTemp;
                        break;
                }
            }
        }

        //应用样式文件
        if (objContext.tagName != "TR")
            objContext.createStyleSheet("/MyWorkflow/MapForWF/css/form.css");


        //重新计算 异步。等待加载htc加载完毕
        setTimeout(loadRecalAll, 1);
    }
    catch (e) {
        logException(e);
        alert("流程模板定义有误，请联系流程创建人！");
        return false;
    }

    return true;
}
//等待 htc 加载完毕后在执行计算
function loadRecalAll() {
    var objWindow, objContext;
    if (window.name == "appIframe_HtmlForm") {
        objWindow = window;
        objContext = document;
    }
    else {
        objWindow = parent.document.frames("appIframe_HtmlForm");
        objContext = parent.document.frames("appIframe_HtmlForm").document;
    }

    var spanlist = objContext.getElementsByTagName("SPAN");
    for (var i = 0; i < spanlist.length; i++) {
        if (spanlist[i].className == "selectBox" && spanlist[i].isInitialized != "1") {
            setTimeout(loadRecalAll, 1);
            return;
        }
    }
    for (var i = 0; i < spanlist.length; i++) {
        if (spanlist[i].className == "selectBox") {
            //给活动的下拉框赋样式
            var sInputList = spanlist[i].getElementsByTagName("INPUT");
            for (var j = 0; j < sInputList.length; j++) {
                if (sInputList[j].name == spanlist[i].name) {
                    sInputList[j].style.cssText = spanlist[i].style.cssText;
                    spanlist[i].style.width = sInputList[j].style.width;
                    sInputList[j].style.width = "100%";
                    break;
                }
            }
        }
    }

    RecalcAll();
}
//重新计算所有计算域的值
function RecalcAll() {
    var objWindow, objContext;
    if (window.name == "appIframe_HtmlForm") {
        objWindow = window;
        objContext = document;
    }
    else {
        objWindow = parent.document.frames("appIframe_HtmlForm");
        objContext = parent.document.frames("appIframe_HtmlForm").document;
    }


    var objList = getHTMLDomainField(objContext);

    //将计算域和合计域(平均值)放入数组中，数组对象包括域名称、域对象、域中引用域名称数组
    var allRecalcDomainList = [];
    for (var i = 0; i < objList.length; i++) {
        if (objList[i].dm_displaytype == "calc" || objList[i].dm_displaytype == "sum" || objList[i].dm_displaytype == "avg") {
            var o = new Object()
            o.key = objList[i].dm_name;
            o.type = objList[i].dm_displaytype;
            o.control = objList[i];
            o.callBackDomainNames = getCallBackDomains(o);
            allRecalcDomainList.push(o);
        }
    }
    while (allRecalcDomainList.length > 0) {
        var haveHandler = false;
        //1.对数组元素进行检查
        for (var j = 0; j < allRecalcDomainList.length; j++) {
            //1）检查引用的域是否需要计算：
            var callBackDomainIsFinish = true;
            for (var k = 0; k < allRecalcDomainList[j].callBackDomainNames.length; k++) {
                if (allRecalcDomainList.indexOf(allRecalcDomainList[j].callBackDomainNames[k]) > -1) {
                    callBackDomainIsFinish = false;
                    break;
                }
            }
            //2）引用的域不用再计算，则对该域进行计算，并从数组中删除
            if (callBackDomainIsFinish) {
                haveHandler = true; //标识进行过处理
                if (allRecalcDomainList[j].type == "calc") {
                    RecalcDomainSingle(allRecalcDomainList[j].control, objList);
                }
                else {
                    RecalcSumSingle(allRecalcDomainList[j].control, objList);
                }
                allRecalcDomainList.splice(j, 1);
                j--; //元素删除后，计算相应减1
            }
        }
        //2.如果所有元素检查完了也不能处理，但数组中还有域，说明引用存在闭环，提示定义错误
        if (!haveHandler) {
            alert("计算域和合计域的引用存在闭环，请检查域设置！");
            return;
        }
        //3.如果数组中有域，从第1点开始递归进行
    }
}



//获取域所依赖的其他域
function getCallBackDomains(domainObj) {
    var options = domainObj.control.dm_dropdownoptions;
    var callBackDomains = [];
    if (domainObj.type == "sum" || domainObj.type == "avg") {
        callBackDomains.push(options);
        return callBackDomains;
    } else {
        var iStart, iEnd;
        iEnd = -1;
        for (; ;) {
            iStart = options.indexOf("{", iEnd); 	// "{" 位置
            if (iStart < 0) {
                break;
            }
            iEnd = options.indexOf("}", iStart); 	// "}" 位置
            if (iEnd < 0) {
                break;
            }
            callBackDomains.push(options.substring(iStart + 1, iEnd)); 	// 一对 "{}" 中的字符串，为表单域名称
        }
        return callBackDomains;
    }
}


//计算单个计算域
function RecalcDomainSingle(domainObj, objList) {
    var objWindow, objContext;
    if (window.name == "appIframe_HtmlForm") {
        objWindow = window;
        objContext = document;
    }
    else {
        objWindow = parent.document.frames("appIframe_HtmlForm");
        objContext = parent.document.frames("appIframe_HtmlForm").document;
    }
    var i, j;
    var sExp; 	//表达式
    var s;
    var sField, sValue;
    var iStart, iEnd;
    if (domainObj.dm_displaytype == "calc")		//计算域
    {
        sExp = domainObj.dm_dropdownoptions; 	//表达式

        s = "";
        iEnd = -1;
        for (; ;) {
            iStart = sExp.indexOf("{", iEnd); 	// "{" 位置
            if (iStart < 0) {
                s += sExp.substr(iEnd + 1);
                break;
            }
            s += sExp.substring(iEnd + 1, iStart);
            iEnd = sExp.indexOf("}", iStart); 	// "}" 位置
            if (iEnd < 0) {
                s += sExp.substr(iStart);
                break;
            }
            sField = sExp.substring(iStart + 1, iEnd); 	// 一对 "{}" 中的字符串，为表单域名称
            sValue = "";

            for (j = 0; j < objList.length; j++) {
                if (objList[j].name == sField && (objList[j].dm_dynamic == undefined || objList[j].dm_dynamic != "1")) {
                    // 用表单域的值替换关键字
                    //sValue = (objList[j].returnValue!=undefined)?objList[j].returnValue:objList[j].value;
                    //2009-09-03 By SunF：计算“数字型”表单域时，需要根据其小数位数进行截断处理，同“合计域”的小数位数处理保持一致
                    //sValue = (objList[j].dm_displaytype == "number" ? objList[j].value.replace(/,/g, "") : objList[j].value);
                    if (domainObj.dm_type == "decimal" || domainObj.dm_type == "money") {
                        if (objWindow.event != null && "returnvaluechange" == objWindow.event.type && objWindow.event.srcElement === objList[j]) {
                            sValue = objWindow.event.value.replace(/,/g, "");
                        }
                        else {
                            sValue = objList[j].value.replace(/,/g, "");
                        }
                    }
                    else if (objList[j].dm_displaytype == "radio") {
                        sValue = getRadioSelectText(objList[j]);
                    } else {
                        sValue = objList[j].value;
                    }
                    break;
                }
            }

            //动态表格的计算域，按行计算
            if (domainObj.dm_dynamic != undefined && domainObj.dm_dynamic == "1") {
                var tables = objContext.getElementsByTagName("TABLE");

                for (var j = 0; j < tables.length; j++) {
                    if (tables[j].group_name && tables[j].group_name == domainObj.dm_group) {
                        var oTr = getTrByIndex(tables[j], domainObj.dm_rowIndex)
                        if (oTr != undefined) {
                            var dynamicList = getHTMLDomainField(oTr);

                            for (k = 0; k < dynamicList.length; k++) {
                                if (dynamicList[k].name == sField) {
                                    //2009-08-27 By SunF：经过讨论，还是维持原来的处理模式，下拉框中仍然取“文本”，主要是考虑“计算”域要在界面上展示，如果取“背后的值”，不太容易让人理解

                                    //2009-09-03 By SunF：计算“数字型”表单域时，需要根据其小数位数进行截断处理，同“合计域”的小数位数处理保持一致
                                    //sValue = (dynamicList[k].dm_displaytype=="number"?dynamicList[k].value.replace(/,/g,""):dynamicList[k].value);
                                    if (dynamicList[k].dm_displaytype == "number") {
                                        if (objWindow.event != null && "returnvaluechange" == objWindow.event.type && objWindow.event.srcElement === dynamicList[k]) {
                                            sValue = objWindow.event.value.replace(/,/g, "");
                                        }
                                        else {
                                            sValue = dynamicList[k].value.replace(/,/g, "");
                                        }
                                    }
                                    else if (dynamicList[k].dm_displaytype == "radio") {
                                        sValue = getRadioSelectText(dynamicList[k]);
                                    } else {
                                        sValue = dynamicList[k].value;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

            }

            //2009-08-28 By SunF：预处理数据：数字 - 空则默认为 0；其它 - 如果isNaN()，则需要在两端添加两个英文""符号
            if ((domainObj.dm_type == "decimal" || domainObj.dm_type == "money") && Trim(sValue) == "") sValue = 0;

            //Modified By FUYL on 2012-10-10 处理“\"”转义字符文本
            if ((domainObj.dm_type == "text" && Trim(sValue) == "") || isNaN(sValue))
                sValue = "\"" + sValue.replace(/\\/g, "\\\\").replace(/"/g, "\\\"") + "\"";

            s += sValue;

        }

        try {
            //计算表达式
            var v = eval(s);
            if (s) {
                //domainObj.value = (isNaN(v)?0.00:v);
                switch (typeof (v)) {
                    case "number":
                        domainObj.value = (isNaN(v) || v == Infinity || v == -Infinity ? 0.00 : v);
                        break;
                    case "string":
                        domainObj.value = v == undefined ? "" : v;
                        break;
                    default:
                        domainObj.value = v == undefined ? "" : v;
                        break;
                }
            }
        }
        catch (e) { logException(e); }
    }
}


function getTrByIndex(table, rowIndex) {
    for (var i = 0; i < table.rows.length; i++) {
        if (table.rows[i].dm_rowIndex != undefined && table.rows[i].dm_rowIndex == rowIndex) {
            return table.rows[i];
        }
    }
}

//计算单个合计值
function RecalcSumSingle(domainObj, objList) {
    var objWindow;
    if (window.name == "appIframe_HtmlForm")
        objWindow = window;
    else
        objWindow = parent.document.frames("appIframe_HtmlForm");

    var s, sValue;

    if (domainObj.dm_displaytype == "sum")		//计算域
    {
        var sField = domainObj.dm_dropdownoptions;
        if (sField == undefined) sField = "";
        s = "";
        if (sField != "") {
            for (j = 0; j < objList.length; j++) {
                if (objList[j].name == sField) {
                    // 用表单域的值替换关键字
                    //合计时，如果是被合计域值变更，则使用截断后的值进行计算。
                    if (objWindow.event != null && "returnvaluechange" == objWindow.event.type && objWindow.event.srcElement === objList[j]) {
                        sValue = objWindow.event.value.replace(/,/g, "");
                    }
                    else {
                        sValue = objList[j].value.replace(/,/g, "");
                    }
                    if (sValue == "") sValue = 0;
                    s += (s == "" ? "" : "+") + (isNaN(sValue) ? 0 : sValue);
                }
            }
        }

        try {
            //计算表达式
            var v = eval(s);

            if (s) domainObj.value = formatNumber(eval(s), getPattern(domainObj.acc, domainObj.grp));
        }
        catch (e) { logException(e); }

    }

}

//辅助录入
function assistant(obj) {
    var objInput;
    try {
        objInput = obj.parentElement.previousSibling.children[0];
    }
    catch (e) {
        logException(e);
        return;
    }

    var objContext;
    if (window.name == "appIframe_HtmlForm")
        objContext = document;
    else
        objContext = parent.document.frames("appIframe_HtmlForm").document;

    var objList;

    //动态表格的辅助录入域
    if (objInput.dm_dynamic != undefined && objInput.dm_dynamic == "1") {
        var tables = objContext.getElementsByTagName("TABLE");
        for (var j = 0; j < tables.length; j++) {
            if (tables[j].group_name && tables[j].group_name == objInput.dm_group) {
                var oTr = getTrByIndex(tables[j], objInput.dm_rowIndex)
                if (oTr != undefined) {
                    objList = getHTMLDomainField(oTr);
                }
            }
        }
    }
    else {
        objList = getHTMLDomainField(objContext);
    }

    var sXML = objInput.dm_dropdownoptions;
    if (sXML == "" || sXML == undefined) return;
    var xmlDom = XMLDOM();
    xmlDom.async = false;
    xmlDom.load(sXML);
    var xmlNode;
    var i;

    xmlNode = xmlDom.selectNodes("/page/controls/control[@id='appGrid']");
    if (xmlNode.length == 0)
        sFileName = "/WebEditor/dialog/Tree.aspx";
    else
        sFileName = "/WebEditor/dialog/Grid.aspx";
    var sParams = "xml=" + myEscapeForWF(sXML);
    var width = objInput.dm_configWidth;
    var height = objInput.dm_configHeight;
    if (width == "" || width == undefined) width = "640px";
    if (height == "" || height == undefined) height = "480px";
    sURL = "../FrameTemp0.aspx?title=" + myEscapeForWF("辅助录入") + "&filename=" + myEscapeForWF(sFileName) + "&rnd=" + Math.random() + "&param=" + sParams;
    var objReturn = window.showModalDialog(escapeUrlForWF(sURL), "", "dialogWidth:" + width + ";dialogHeight:" + height + ";help:no;resizable:yes;status:yes;scroll:no");

    if (!objReturn) return;
    for (i = 0; i < objReturn.values.length; i++) {
        for (j = 0; j < objList.length; j++) {
            if (objReturn.values[i].name == objList[j].dm_name) {
                try {
                    objList[j].setValue(objReturn.values[i].value);
                }
                catch (e) {
                    logException(e);
                    objList[j].value = objReturn.values[i].value;
                }
                //break;
            }
            else if (objList[j].className == "selectBox") {
                if (objReturn.values[i].name == objList[j].parentElement.parentElement.parentElement.parentElement.parentElement.dm_name) {
                    objList[j].setValue(objReturn.values[i].value);
                    //break;
                }
            }
        }
    }

    RecalcAll();		//重新计算表达式
}

//返回格式化字符串
function getPattern(acc, grp) {
    if (acc == undefined) acc = 2;
    if (grp == undefined) grp = "true";  //2009-09-04 By SunF：数字型默认应该显示“千分位”，同平台处理保持一致

    grp += "";

    var pattern;
    if (grp.toLowerCase() == "true") {
        pattern = "#,###";
    }
    else {
        pattern = "####";
    }

    acc = parseInt(acc);
    if (isNaN(acc)) {
        acc = 0;
    }

    if (acc > 0) {
        pattern += ".";

        for (var i = 0; i < acc; i++) {
            pattern += "0"
        }
    }

    return pattern;
}



//格式化日期字符串
function formatDate(sDate, bWithTime) {
    var r;
    var y, M, d, H, m, s;
    var D;
    var matches;

    // 判断是否 yyyy-MM-dd HH:mm:ss 格式
    r = new RegExp("^(\\d{4})-(\\d{1,2})-(\\d{1,2})\\s+(\\d{1,2}):(\\d{1,2}):(\\d{1,2})$", "ig");
    matches = r.exec(sDate);

    //匹配成功
    if (matches != null) {
        y = parseInt(RegExp.$1, 10);
        M = parseInt(RegExp.$2, 10) - 1;
        d = parseInt(RegExp.$3, 10);
        H = parseInt(RegExp.$4, 10);
        m = parseInt(RegExp.$5, 10);
        s = parseInt(RegExp.$6, 10);

        D = new Date(y, M, d, H, m, s);

        return FormatDate(D, bWithTime);
    }

    // 判断是否 yyyy-MM-dd 格式
    r = new RegExp("^(\\d{4})-(\\d{1,2})-(\\d{1,2})$", "ig");
    matches = r.exec(sDate);

    //匹配成功
    if (matches != null) {
        y = parseInt(RegExp.$1, 10);
        M = parseInt(RegExp.$2, 10) - 1;
        d = parseInt(RegExp.$3, 10);

        D = new Date(y, M, d, 0, 0, 0);

        return FormatDate(D, bWithTime);
    }

    //非日期格式，直接返回原字符串
    return sDate;
}




/*--------------------------------------------------Html表单函数-----------------------------------------------------------------*/




function findPosLeft(obj) {
    var curleft = 0;

    if (obj.offsetParent) {
        while (obj.offsetParent) {
            curleft += obj.offsetLeft;
            obj = obj.offsetParent;
        }
    } else if (obj.x) curleft += obj.x;
    return curleft;
}

function findPosTop(obj) {
    var curtop = 0;

    if (obj.offsetParent) {
        while (obj.offsetParent) {
            curtop += obj.offsetTop;
            obj = obj.offsetParent;
        }
    } else if (obj.y) curtop += obj.y;
    return curtop;
}
function XmlEncode(s) {
    s = s.replace(/&/g, "＆").replace(/</g, "＜").replace(/>/g, "＞").replace(/\'/g, "＇").replace(/\{/g, "｛").replace(/\}/g, "｝");
    return s.replace(/\"/g, "＂").replace(/\@/g, "＠").replace(/\,/g, "，");
}
function getDocExtendName(filename) {
    var pos = filename.lastIndexOf(".");
    if (pos < 0) return "";
    return filename.substring(pos + 1, filename.length).toUpperCase();
}



function substr(str, len) {
    if (!str || !len) { return ''; }
    //预期计数：中文2字节，英文1字节    
    var a = 0;
    //循环计数     
    var i = 0;
    //临时字串     
    var temp = new StringBuilder();
    for (i = 0; i < str.length; i++) {
        if (str.charCodeAt(i) > 255 || str.charCodeAt(i) == 10) {//按照预期计数增加2             
            a += 2;
        } else {
            a++;
        }
        //如果增加计数后长度大于限定长度，就直接返回临时字符串         
        if (a > len) { return temp.toString(); }
        //将当前内容加到临时字符串         
        temp.append(str.charAt(i));
    }
    //如果全部是单字节字符，就直接返回源字符串     
    return str;
}
//功能：设置appForm中的控件是否必填
function SetControlReq(obj, bValue) {
    if (!obj) return;
    if (obj.className.indexOf("dtm") >= 0 || obj.className.indexOf("lu") >= 0 || (obj.parentElement.nextSibling && obj.parentElement.nextSibling.innerHTML.indexOf("<IMG") != -1 && obj.parentElement.nextSibling.children[0].className == "")) {
        //className=dtm、lu(lookup)、txt(含图标控件)
        var objTd = obj.parentElement.parentElement.parentElement.parentElement.parentElement.previousSibling;
    }
    else {
        if (obj.className.indexOf("selectBox") >= 0) {
            //className=selectBox(下拉列表)
            var objTd = obj.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.previousSibling;
        }
        else {
            //className=txt(文本框)
            var objTd = obj.parentElement.previousSibling;
        }
    }
    if (!objTd) return;

    if (bValue) {
        objTd.className = "req";
        obj.req = "1";
    }
    else {
        objTd.className = "";
        obj.req = "0";
    }
}

//将编码规则中的#$转成对应的[],~转空
function codingShow(coding) {
    if (coding.length == 0) return "";
    //允许录入%，删除%的处理.replace(/\%/g,"[")
    var sReturn = coding.replace(/~/g, "").replace(/#/g, "[").replace(/\$/g, "[");
    var arr = sReturn.match(/\[[^\[^|\]]*\[/g);
    //BUG 48645 sunfx 2009-9-4 END
    if (arr) {
        for (var i = 0; i < arr.length; i++) {
            sReturn = sReturn.replace(arr[i], arr[i].replace(/\[$/g, "]"));
        }
    }
    return sReturn;
}

//移除事件
function deleteEvent(html) {

    html = html.replace(/onmouseover/ig, "E");
    html = html.replace(/onmousemove/ig, "E");
    html = html.replace(/onmouseout/ig, "E");
    html = html.replace(/onselectstart/ig, "E");
    html = html.replace(/onblur/ig, "E");
    html = html.replace(/onkeydown/ig, "E");
    html = html.replace(/onchange/ig, "E");
    //html = html.replace(/onclick/ig,"E");
    html = html.replace(/disabled/ig, "E");
    html = html.replace(/oncontextmenu/ig, "E");
    html = html.replace(/onfocus/ig, "E");
    html = html.replace(/href/ig, "E");
    //html = html.replace(/iframe/ig,"if");
    html = html.replace(/class/ig, "c");
    html = html.replace(/onreturnvaluechange/ig, "c");
    return html;

}

//取得发起或审批界面的风格类型，左右返回true/上下返回false
function getInterfaceType() {
    if (location.href.indexOf("_Form_lr.aspx?") > 0)
        return true;
    else
        return false;
}

//动态调整发起、审批界面框架的高度，宽度    
function dyniframesize(iframeid) {
    var dyniframe = null;
    var iframehide = "yes";

    if (iframeid == undefined) {
        alert("参数 iframeid 未赋值");
        return;
    }

    //判断表单是否加载完成，未完成则延迟调整
    dyniframe = $id(iframeid);
    if (!verifyAllFramesComplete(dyniframe)) {
        setTimeout("dyniframesize('" + iframeid + "')", 10);
        return;
    }

    var height;
    //固定高度
    if ($id("ExtendFormUrl")) {
        height = $id("ExtendFormUrl").value.match(/height=\d*/gi);
    }

    if (height != null) {
        height = height[0].match(/\d*$/);
        if (height != null) {
            $id("appIframe_HtmlForm").parentElement.style.height = height[0] + "px";
            $id("appIframe_HtmlForm").style.height = height[0] + "px";
        }
    } else {
        //动态高度
        if ($id) {
            dyniframe = $id(iframeid);
            if (dyniframe && !window.opera) {
                dyniframe.style.display = "block"
                if (dyniframe.contentDocument && dyniframe.contentDocument.body.offsetHeight) {
                    height = dyniframe.contentDocument.body.offsetHeight;

                    dyniframe.parentElement.style.height = height;
                    dyniframe.height = height;
                }
                else if (dyniframe.Document && dyniframe.Document.body.scrollHeight) {
                    height = dyniframe.Document.body.scrollHeight + "px";
                    dyniframe.parentElement.style.height = height;
                    dyniframe.style.height = height;
                }
            }
        }
        if ((document.all || $) && iframehide == "no") {
            var tempobj = document.all ? document.all[iframeid] : $id(iframeid);
            tempobj.style.display = "block";
        }
    }

    //动态宽度
    if ($id) {
        dyniframe = $id(iframeid);
        if (dyniframe && !window.opera) {
            dyniframe.style.display = "block"
            if (dyniframe.contentDocument && dyniframe.contentDocument.body.offsetWidth) {
                dyniframe.width = dyniframe.contentDocument.body.offsetWidth;
            }
            else if (dyniframe.Document && dyniframe.Document.body.scrollWidth) {
                dyniframe.style.width = dyniframe.Document.body.scrollWidth + "px";
            }
        }
    }
    if ((document.all || $) && iframehide == "no") {
        var tempobj = document.all ? document.all[iframeid] : $id(iframeid);
        tempobj.style.display = "block";
    }
}


function DomReady(fn) {
    var d = window.document, done = false,
    // only fire once
    init = function () {
        if (!done) {
            done = true;
            fn();
        }
    };
    // polling for no errors
    (function () {
        try {
            // throws errors until after ondocumentready
            d.documentElement.doScroll('left');
        } catch (e) {
            logException(e);
            setTimeout(arguments.callee, 0);
            return;
        }
        // no errors, fire
        init();
    })();
    // trying to always fire before onload
    d.onreadystatechange = function () {
        if (d.readyState == 'complete') {
            d.onreadystatechange = null;
            init();
        }
    };
}

/// 添加DomainXML中的流程属性
/// domain DomainXML字符串
/// data 需要传递的数据对象
function AddProcessInfoToDomainXML(domain, data) {
    var domainXML = XMLDOM();
    try {
        domainXML.loadXML(domain);
        domainXML.documentElement.setAttribute("HandleType", data.HandleType);
        domainXML.documentElement.setAttribute("ProcessStatus", data.ProcessStatus);
        domainXML.documentElement.setAttribute("NodeStatus", data.NodeStatus);
        domainXML.documentElement.setAttribute("ProcessGUID", data.ProcessGUID);
        domainXML.documentElement.setAttribute("IsHistory", data.IsHistory);
    } catch (e) { logException(e); alert("流程模板未使用业务对象或业务对象定义有误！"); return ""; }
    return domainXML.xml;
}

/// 删除DomainXML中的流程属性
/// domain DomainXML字符串
function removeProcessInfoFromDomainXML(domain) {
    var domainXML = XMLDOM();
    domainXML.loadXML(domain);
    if (domainXML.documentElement.getAttribute("HandleType") != null) {
        domainXML.documentElement.removeAttribute("HandleType");
    }
    if (domainXML.documentElement.getAttribute("NodeStatus") != null) {
        domainXML.documentElement.removeAttribute("NodeStatus");
    }
    if (domainXML.documentElement.getAttribute("ProcessGUID") != null) {
        domainXML.documentElement.removeAttribute("ProcessGUID");
    }
    if (domainXML.documentElement.getAttribute("IsHistory") != null) {
        domainXML.documentElement.removeAttribute("IsHistory");
    }
    return domainXML.xml;
}


var arrayIncludedFiles = [];
//引入JS文件
//fileName：
function IncludeJsFile(target, fileName) {
    if (target == null) return false;
    //避免同一个文件多次引入
    if (arrayIncludedFiles.indexOf(fileName) == -1) {
        //document.writeln("<script type=\"text/javascript\" src='"+fileName+"'></script>");
        var head = target.getElementsByTagName("head")[0];
        var script = target.createElement("script");
        script.src = escapeUrlForWF(fileName);

        arrayIncludedFiles[arrayIncludedFiles.length] = { key: fileName, value: false };
        var done = false;

        // Attach handlers for all browsers
        script.onload = script.onreadystatechange = function () {
            if (!done && (!this.readyState || this.readyState == "loaded" || this.readyState == "complete")) {
                done = true;

                success(fileName);

                // Handle memory leak in IE
                script.onload = script.onreadystatechange = null;
                head.removeChild(script);
            }
        };

        head.appendChild(script);
    }
}
function success(key) {
    var element = arrayIncludedFiles.getElement(key);
    if (element) element.value = true;
}

//根据业务对象对应系统，动态注册js，JS路径取自Workflow.config的script属性值。如：<Group id="0101" name="销售系统" script="/Slxt/Slxt_WF.js">
function LoadBusinessTypesJS() {
    var BusinessScript;
    BusinessScript = appForm.BusinessScript.getValue();
    //动态注册JS
    if (BusinessScript != "") {
        LoadJs(BusinessScript);
    }
}

//动态注册JS文件
function LoadJs(JsScr) {
    var oHead = document.getElementsByTagName('HEAD').item(0);
    var oScript = document.createElement("script");
    oScript.type = "text/javascript";
    oScript.src = escapeUrlForWF(JsScr);
    oHead.appendChild(oScript);
}
//功能：记录Word文档操作日志信息（主要记录文档大小）
//参数 DocGuid：文档GUID  
//     operType：文档操作类型
//     DocType ：文档类型，附件文档为“FJ”,保存在p_Documents中，其余文件保存在myWorkflowDocument中
function SaveWordLog(DocGuid, operType, DocType) {
    if (DocGuid == "") { return; }
    if (operType == undefined) { operType = ""; }
    if (DocType == undefined) { DocType = ""; }
    var sFile = "/MyWorkflow/WF_XmlHTTP.aspx";
    var strReturn = GetDataFromXMLHTTP(sFile, "SaveWordLog", DocGuid, operType, DocType);
}





//数据丢失检测相关
//记录日志
function WriteLog(functionName, exception, data) {
    GetDataFromXMLHTTP("/MyWorkflow/WF_XmlHTTP.aspx", "WriteLog", functionName, exception, data == undefined ? "" : data);
}

//校验所有的Iframe是否都已加载完成
function verifyAllFramesComplete(parentFrame) {
    if (parentFrame == null || parentFrame == undefined) { return true; }
    if (parentFrame.document.readyState != "complete") { return false; }
    var subFrames = parentFrame.document.frames;
    for (var i = 0; i < subFrames.length; i++) {
        if (!verifyAllFramesComplete(subFrames[i])) { return false; }
    }
    return true;
}





//启用右键菜单
function contextmenu(win) {
    var e = win ? win.event : event;
    var d = win ? win.document : document;
    if (!e || !e.srcElement) {
        return false;
    }
    var s = e.srcElement.tagName;
    if (s && !e.srcElement.disabled && (s == "INPUT" || s == "TEXTAREA" || d.selection.createRange().text.length > 0)) {
        e.returnValue = true;
        return true;
    }
    else {
        e.returnValue = false;
        return false;
    }
}






// 超链接域打开的配置的地址。
function linkDomainClick(linkUrl) {
    if (Trim(linkUrl).length == 0) {
        alert("未配置链接地址！");
        return;
    }
    try {
        var objContext;
        if (window.name == "appIframe_HtmlForm") {
            objContext = document;
        }
        else {
            objContext = parent.document.frames("appIframe_HtmlForm").document;
        }

        var inputlist = objContext.getElementsByTagName("INPUT");
        for (var i = 0; i < inputlist.length; i++) {
            if (inputlist[i].dm_name) {
                linkUrl = linkUrl.replace("{" + inputlist[i].dm_name + "}", myEscapeForWF(inputlist[i].dm_displaytype == "textarea" ? inputlist[i].textarea : inputlist[i].value));
            }
        }
    } catch (e) {
    }
    window.open(escapeUrlForWF(linkUrl));
}

//设置XMLHttp请求标识
function setXMLHttpRequestHeader(oHTTP) {
    oHTTP.setRequestHeader("X-Requested-With", "XMLHttpRequest");
}