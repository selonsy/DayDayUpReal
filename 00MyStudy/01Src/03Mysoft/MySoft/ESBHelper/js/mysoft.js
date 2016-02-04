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
    //测试按钮
    $("#btnTestEsbDBInfo").click(function () {
        checkDBConnection(SiteType.ESBSite);
    });
    $("#btnTestERPDBInfoForTrunk").click(function () {
        checkDBConnection(SiteType.Trunk);
    });
    $("#btnTestERPDBInfoForBranch1").click(function () {
        checkDBConnection(SiteType.Branch1);
    });

    //生成按钮
    $("#btnCreateForESB").click(function () {
        CreateSQL(SiteType.ESBSite);
    });
    $("#btnCreateForTrunk").click(function () {
        CreateSQL(SiteType.Trunk);
    });
    $("#btnCreateForBranch1").click(function () {
        CreateSQL(SiteType.Branch1);
    });

    //快捷操作按钮
    $("#btnFastTest").click(function () {
        //CreateSQL(SiteType.Branch1);
    });
    $("#btnFastCreate").click(function () {
        //CreateSQL(SiteType.Branch1);
    });
    $("#btnFastDownload").click(function () {
        //CreateSQL(SiteType.Branch1);
    });
    $("#btnForZhuangBi").click(function () {
        alert("咋地，逼都让你装完了，还特么想上天呐！");
    });
    $("#btnExample").click(function () {
        //测试语句
        TestDataForCheckDBConnection();
        $("#btnExample").hide();
        $("#btnClear").show();
    });
    $("#btnClear").click(function () {
        $("input[type='text']").val('');
        $('input[name="rdIsNewErpForTrunk"][value="1"]').attr("checked", true);
        $('input[name="rdIsNewErpForBranch"][value="1"]').attr("checked", true);
        $("#btnClear").hide();
        $("#btnExample").show();
    });
      
    //第三方控件初始化
    //// 初始化轮播
    //$(".start-slide").click(function () {
    //    $("#myCarousel").carousel('cycle');
    //});

    //onkeyup事件绑定
    bindOnKeyUp();
});

function bindOnKeyUp() {
    $("#txtERPDataBaseServer").keyup(function () {
        $("#txtERPDataBaseServerForBranch1").val($("#txtERPDataBaseServer").val());
    });
    $("#txtERPDataBasePort").keyup(function () {
        $("#txtERPDataBasePortForBranch1").val($("#txtERPDataBasePort").val());
    });
    $("#txtERPDataBaseUserName").keyup(function () {
        $("#txtERPDataBaseUserNameForBranch1").val($("#txtERPDataBaseUserName").val());
    });
    $("#txtERPDataBasePassword").keyup(function () {
        $("#txtERPDataBasePasswordForBranch1").val($("#txtERPDataBasePassword").val());
    });
}

//测试模块
function TestDataForCheckDBConnection() {
    //ESB站点
    $("#txtESBDataBaseServer").val('shenjl');
    $("#txtESBDataBaseName").val('TestDB_ESB');
    $("#txtESBDataBasePort").val('1433');
    $("#txtESBDataBaseUser").val('sa');
    $("#txtESBDataBasePassword").val('95938');

    //主站点
    $("#txtERPDataBaseServer").val('10.5.10.75\\SQL2008R2_SZ');
    $("#txtERPDataBasePort").val('1433');
    $("#txtERPDataBaseUserName").val('sa');
    $("#txtERPDataBasePassword").val('Mysoft95938');

    $("#txtERPDataBaseNameForTrunk").val('erp302_szzb_hngj');
    $("#txtERPProviderNameForTrunk").val('SZHNC302');
    $("#txtERPDisplayNameForTrunk").val('深圳华南城ERP302');
    $("#txtERPIsNewErpForTrunk").val('1');
    $('input[name="rdIsNewErpForTrunk"][value="0"]').attr("checked", true);
    $("#txtERPSysSignForTrunk").val('szhnc302');
    $("#txtERPDomainForTrunk").val('http://localhost:17302');

    //辅站点1
    $("#txtERPDataBaseServerForBranch1").val('10.5.10.75\\SQL2008R2_SZ');
    $("#txtERPDataBaseNameForBranch1").val('sydczl202sp2_szzb_hngj');
    $("#txtERPDataBasePortForBranch1").val('1433');
    $("#txtERPDataBaseUserNameForBranch1").val('sa');
    $("#txtERPDataBasePasswordForBranch1").val('Mysoft95938');
    $("#txtERPProviderNameForBranch1").val('SZHNC303');
    $("#txtERPDisplayNameForBranch1").val('深圳华南城SYDC202');
    $('input[name="rdIsNewErpForBranch"][value="0"]').attr("checked", true);
    $("#txtERPSysSignForBranch1").val('szhnc303');
    $("#txtERPDomainForBranch1").val('http://localhost:17303');
}

//+----------------------------------------------------------------------  
//| 功能：生成SQL语句 
//| 说明：
//| 参数：
//| 返回值：
//| 创建人：沈金龙
//| 创建时间：2016-1-22 11:13:36
//+----------------------------------------------------------------------
function collectParamForCreateESBSQL() {
    var obj = {
        createType:SiteType.ESBSite,
        dbEsbName: $("#txtESBDataBaseName").val(),
        dbServer: $("#txtERPDataBaseServer").val(),
        dbUserName: $("#txtERPDataBaseUserName").val(),
        dbPassword: $("#txtERPDataBasePassword").val(),
        dbPort: $("#txtERPDataBasePort").val(),

        dbName1: $("#txtERPDataBaseNameForTrunk").val(),
        providerName1: $("#txtERPProviderNameForTrunk").val(),
        displayName1: $("#txtERPDisplayNameForTrunk").val(),
        isNewErp1: $('input[name="rdIsNewErpForTrunk"]:checked').val(),
        sysSign1: $("#txtERPSysSignForTrunk").val(),
        dbDomain1: $("#txtERPDomainForTrunk").val(),

        dbName2: $("#txtERPDataBaseNameForBranch1").val(),
        providerName2: $("#txtERPProviderNameForBranch1").val(),
        displayName2: $("#txtERPDisplayNameForBranch1").val(),
        isNewErp2: $('input[name="rdIsNewErpForBranch"]:checked').val(),
        sysSign2: $("#txtERPSysSignForBranch1").val(),
        dbDomain2: $("#txtERPDomainForBranch1").val()
    };
    //buuug:个人觉得，验证应该在这里处理。。。收集参数就得给出一个没有问题的结果，有问题就应该这时候提出来。。

    //if (!VerfiyInput(obj) == true) return;
    //alert(obj.isNewErp1+'   '+obj.isNewErp2);
    return obj;
}


function collectParamForCreateERPTrunkSQL() {
    var obj = {
        createType: SiteType.Trunk,
        dbName1: $("#txtERPDataBaseNameForTrunk").val(),
        dbName2: $("#txtERPDataBaseNameForBranch1").val(),               
        sysSign1: $("#txtERPSysSignForTrunk").val(),
        sysSign2: $("#txtERPSysSignForBranch1").val(),
        isNewErp: $('input[name="rdIsNewErpForTrunk"]:checked').val()
    };
    //if (!VerfiyInput(obj) == true) return;
    return obj;
}

function collectParamForCreateERPBranch1SQL() {
    var obj = {
        createType: SiteType.Branch1,
        dbName1: $("#txtERPDataBaseNameForBranch1").val(),
        dbName2: $("#txtERPDataBaseNameForTrunk").val(),
        sysSign1: $("#txtERPSysSignForBranch1").val(),
        sysSign2: $("#txtERPSysSignForTrunk").val(),
        isNewErp: $('input[name="rdIsNewErpForBranch"]:checked').val()
    };
    //if (!VerfiyInput(obj) == true) return;
    return obj;
}

function CreateSQL(type) {
    var obj,result;
    if (type == SiteType.ESBSite) {
        obj = collectParamForCreateESBSQL();
    }
    else if (type == SiteType.Trunk) {
        obj = collectParamForCreateERPTrunkSQL();
    }
    else if (type == SiteType.Branch1) {
        obj = collectParamForCreateERPBranch1SQL();
    }
    
    try {
        result = myAjax("XmlHttpCommon.aspx?ywtype=CreateSQL", "post", obj, false, CreateSQLAsyncCall);
    }
    catch (e) {
        alert(e.message);
    }

    if (result == "false"||result=="")
        alert("SQL语句生成失败！");
    if (result == "true") {
        alert("SQL语句生成成功！");
        btnDisplayControl(type);
    }
}

function btnDisplayControl(type) {
    //控制按钮显示
    if (type == SiteType.ESBSite) {               
        $("#btnCreateForESB").hide();
        $("#btnDownLoadForESB").show();
        //添加下载事件
        $("#btnDownLoadForESB").click(function () {
            alert("就不给下！你打我啊~~");
        });
    }
    else if (type == SiteType.Trunk) {
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

function CreateSQLAsyncCall(data, textStatus) {
    //alert(data+"+"+textStatus);
}

function collectParamForCreateSQLTest(type) {
    var obj;
    var esbName = $("#txtESBDataBaseName");
    var dbServer,dbUserName, dbPassword,dbPort;
    var dbName,dbDomain, providerName, displayName, sysSign, isNewErp;
    
    if (type == SiteType.ESBSite) {
        dbServer = $("#txtESBDataBaseServer").val();
        dbName = $("#txtESBDataBaseName").val();
        dbPort = $("#txtESBDataBasePort").val();
        dbUserName = $("#txtESBDataBaseUser").val();
        dbPassword = $("#txtESBDataBasePassword").val();
    } else if (type == SiteType.Trunk) {
        dbName1 = $("#txtERPDataBaseNameForTrunk").val();
        isNewErp = $('#divIsNewErpForTrunk input[name="rdIsNewErpForTrunk"]:checked').val();
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
    }
    else {
        dbServer = $("#txtERPDataBaseServer").val();        
        dbPort = $("#txtERPDataBasePort").val();
        dbUserName = $("#txtERPDataBaseUserName").val();
        dbPassword = $("#txtERPDataBasePassword").val();      
        if (type == SiteType.Trunk) {
            dbName = $("#txtERPDataBaseNameForTrunk").val();
        }
        else if (type == SiteType.Branch1) {
           dbName = $("#txtERPDataBaseNameForBranch1").val();
        }
        else {
        }
    }
    var obj = {
        dbServer: dbServer,
        dbName: dbName,
        dbPort: dbPort,
        dbUserName: dbUserName,
        dbPassword: dbPassword
    };

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

