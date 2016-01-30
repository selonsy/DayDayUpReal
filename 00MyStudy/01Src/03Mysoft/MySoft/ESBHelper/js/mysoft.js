//全局变量定义
//service url
//var strTestLConnectionURL = "ESBHelper.Services.ESBProviderService.CheckDBConnection";

//枚举定义
var SiteType = {
    ESBSite: 99,
    Trunk: 0,
    Branch1: 1,
    Branch2: 2
};

//初始化
$(document).ready(function () {
    //注册点击事件   
    $("#btnTestEsbDBInfo").click(function () {
        checkDBConnection(SiteType.ESBSite);
    });
    $("#btnTestERPDBInfoForTrunk").click(function () {
        checkDBConnection(SiteType.Trunk);
    });
    $("#btnTestERPDBInfoForBranch1").click(function () {
        checkDBConnection(SiteType.Branch1);
    });
    $("#btnCreateForTrunk").click(function () {
        CreateSQL(SiteType.Trunk);
    });
    $("#btnCreateForBranch1").click(function () {
        CreateSQL(SiteType.Branch1);
    });

    //赋值测试
    TestDataForCheckDBConnection();

    //// 初始化轮播
    //$(".start-slide").click(function () {
    //    $("#myCarousel").carousel('cycle');
    //});

    $("#btnForZhuangBi").click(function () {
        alert("咋地，逼都让你装完了，还特么想上天呐！");
    });
});

//测试模块
function TestDataForCheckDBConnection() {
    //ESB站点
    $("#txtESBDataBaseServer").val('shenjl');
    $("#txtESBDataBaseName").val('TestDB_ESB');
    $("#txtESBDataBasePort").val('1433');
    $("#txtESBDataBaseUser").val('sa');
    $("#txtESBDataBasePassword").val('95938');

    //主站点
    $("#txtERPDataBaseServerForTrunk").val('10.5.10.75\\SQL2005_SH');
    $("#txtERPDataBaseNameForTrunk").val('dotnet_erp304_shshangshi');
    $("#txtERPDataBasePortForTrunk").val('1433');
    $("#txtERPDataBaseUserNameForTrunk").val('sa');
    $("#txtERPDataBasePasswordForTrunk").val('Mysoft95938');
    $("#txtERPProviderNameForTrunk").val('SHSS304');
    $("#txtERPDisplayNameForTrunk").val('上海上实ERP304');
    $("#txtERPIsNewErpForTrunk").val('1');
    $("#txtERPSysSignForTrunk").val('shss304');
    $("#txtERPDomainForTrunk").val('http://localhost:8001');

    //辅站点1
    $("#txtERPDataBaseServerForBranch1").val('10.5.10.75\\SQL2005_SH');
    $("#txtERPDataBaseNameForBranch1").val('erp307sp4_shanghai_shss_销售费用');
    $("#txtERPDataBasePortForBranch1").val('1433');
    $("#txtERPDataBaseUserNameForBranch1").val('sa');
    $("#txtERPDataBasePasswordForBranch1").val('Mysoft95938');
    $("#txtERPProviderNameForBranch1").val('SHSS307');
    $("#txtERPDisplayNameForBranch1").val('上海上实ERP307');
    $("#txtERPIsNewErpForBranch1").val('1');
    $("#txtERPSysSignForBranch1").val('shss307');
    $("#txtERPDomainForBranch1").val('http://localhost:8002');
}

//+----------------------------------------------------------------------  
//| 功能：生成SQL语句 
//| 说明：
//| 参数：
//| 返回值：
//| 创建人：沈金龙
//| 创建时间：2016-1-22 11:13:36
//+----------------------------------------------------------------------
function CreateSQL(type) {
    var obj = {
        dbEsbName: $("#txtESBDataBaseName").val(),
        dbServer: type == SiteType.Trunk ? $("#txtERPDataBaseServerForTrunk").val() : $("#txtERPDataBaseServerForBranch1").val(),
        dbName: type == SiteType.Trunk ? $("#txtERPDataBaseNameForTrunk").val() : $("#txtERPDataBaseNameForBranch1").val(),
        dbPort: type == SiteType.Trunk ? $("#txtERPDataBasePortForTrunk").val() : $("#txtERPDataBasePortForBranch1").val(),
        dbUserName: type == SiteType.Trunk ? $("#txtERPDataBaseUserNameForTrunk").val() : $("#txtERPDataBaseUserNameForBranch1").val(),
        dbPassword: type == SiteType.Trunk ? $("#txtERPDataBasePasswordForTrunk").val() : $("#txtERPDataBasePasswordForBranch1").val(),
        providerName: type == SiteType.Trunk ? $("#txtERPProviderNameForTrunk").val() : $("#txtERPProviderNameForBranch1").val(),
        displayName: type == SiteType.Trunk ? $("#txtERPDisplayNameForTrunk").val() : $("#txtERPDisplayNameForBranch1").val(),
        isNewErp: type == SiteType.Trunk ? $("#txtERPIsNewErpForTrunk").val() : $("#txtERPIsNewErpForBranch1").val(),
        sysSign: type == SiteType.Trunk ? $("#txtERPSysSignForTrunk").val() : $("#txtERPSysSignForBranch1").val(),
        dbDomain: type == SiteType.Trunk ? $("#txtERPDomainForTrunk").val() : $("#txtERPDomainForBranch1").val()
    };

    if (!VerfiyInput(obj) == true) return;

    var result = myAjax("XmlHttpCommon.aspx?ywtype=CreateSQL", "POST", obj, false, CreateSQLAsyncCall);

    if (result == "false")
        alert(obj.displayName + "SQL语句生成失败！");
    if (result == "true") {
        alert(obj.displayName + "SQL语句生成成功！");
        //控制按钮显示
        if (type == SiteType.Trunk) {
            $("#btnCreateForTrunk").hide();
            $("#btnDownLoadForTrunk").show();
            //添加下载事件
            $("#btnDownLoadForTrunk").click(function () {
                alert("就不给下！你打我啊~~");
            });
        }
        else {
            $("#btnCreateForBranch1").hide();
            $("#btnDownLoadForBranch1").show();
            //添加下载事件
            $("#btnDownLoadForBranch1").click(function () {
                alert("就不给下！你打我啊~~");
            });
        }
    }
}

function CreateSQLAsyncCall(data, textStatus) {
    //alert(data+"+"+textStatus);
}

//+----------------------------------------------------------------------  
//| 功能：测试数据库链接公用函数   
//| 说明：
//| 参数：
//| 返回值：
//| 创建人：沈金龙
//| 创建时间：2016-1-22 11:13:36
//+----------------------------------------------------------------------

function collectParamForDBConnectionTest(type) {
    var obj;
    var dbServer, dbName, dbPort, dbUserName, dbPassword;
    if (type == SiteType.ESBSite) {
        dbServer = $("#txtESBDataBaseServer").val();
        dbName = $("#txtESBDataBaseName").val();
        dbPort = $("#txtESBDataBasePort").val();
        dbUserName = $("#txtESBDataBaseUser").val();
        dbPassword = $("#txtESBDataBasePassword").val();
    } else if (type == SiteType.Trunk) {
        dbServer = $("#txtERPDataBaseServerForTrunk").val();
        dbName = $("#txtERPDataBaseNameForTrunk").val();
        dbPort = $("#txtERPDataBasePortForTrunk").val();
        dbUserName = $("#txtERPDataBaseUserNameForTrunk").val();
        dbPassword = $("#txtERPDataBasePasswordForTrunk").val();
    } else if (type == SiteType.Branch1) {
        dbServer = $("#txtERPDataBaseServerForBranch1").val();
        dbName = $("#txtERPDataBaseNameForBranch1").val();
        dbPort = $("#txtERPDataBasePortForBranch1").val();
        dbUserName = $("#txtERPDataBaseUserNameForBranch1").val();
        dbPassword = $("#txtERPDataBasePasswordForBranch1").val();
    } else {

    }

    var obj = {
        dbServer: dbServer,
        dbName: dbName,
        dbPort: dbPort,
        dbUserName: dbUserName,
        dbPassword: dbPassword
    };

    //buuug:是否需要在此处验证参数对象obj

    return obj;
}

function checkDBConnection(type) {

    var obj = collectParamForDBConnectionTest(type);

    if (!VerfiyInput(obj) == true) return;

    $.ajax({
        type: "POST",
        url: "XmlHttpCommon.aspx?ywtype=CheckDBConnection",
        data: obj,
        dataType: "text",
        success: function (data) {
            if (!data || data == "false")
                alert("数据库连接测试失败！");
            if (data == "true")
                alert("数据库连接测试成功！");
        }
    });
};

/*--------------------shenjl Add On 2016-1-29 15:42:47 公用函数 Begin --------------------*/

//+----------------------------------------------------------------------  
//| 功能：校验输入是否正确  
//| 说明：
//| 参数：
//| 返回值：
//| 创建人：沈金龙
//| 创建时间：2016-1-22 11:13:36
//+----------------------------------------------------------------------
function VerfiyInput(obj) {
    if (!$.trim(obj.dbServer) && obj.dbServer != undefined) {
        alert("请输入服务器地址！");
        return;
    }
    if (!$.trim(obj.dbName) && obj.dbName != undefined) {
        alert("请输入数据库名称！");
        return;
    }
    if (!$.trim(obj.dbUserName) && obj.dbUserName != undefined) {
        alert("请输入数据库账号！");
        return;
    }
    if (!$.trim(obj.dbPassword) && obj.dbPassword != undefined) {
        alert("请输入数据库密码！");
        return;
    }
    if (!$.trim(obj.providerName) && obj.providerName != undefined) {
        alert("请输入系统编码！");
        return;
    }
    if (!$.trim(obj.displayName) && obj.displayName != undefined) {
        alert("请输入系统名称！");
        return;
    }
    if (!$.trim(obj.isNewErp) && obj.isNewErp != undefined) {
        alert("请选择是否新工作流！");
        return;
    }
    if (!$.trim(obj.sysSign) && obj.sysSign != undefined) {
        alert("请输入系统标识！");
        return;
    }
    if (!$.trim(obj.dbDomain) && obj.dbDomain != undefined) {
        alert("请输入站点地址！");
        return;
    }

    return true;
}

/*--------------------shenjl Add On 2016-1-29 15:42:47 公用函数 End-----------------------*/

