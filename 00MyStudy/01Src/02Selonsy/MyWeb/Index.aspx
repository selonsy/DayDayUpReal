<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MyWeb.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Selonsy</title>
    <script src="Assets/js/jquery-1.12.0.min.js"></script>    
    <script src="js/Common.js"></script>
    <script src="js/index.js"></script>
    <link href="Assets/css/bootstrap.min.css" rel="stylesheet" />
    <script src="Assets/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">        
        <div>
            <table style="text-align: center; margin: 0 auto">
                <tr style="text-align: left">
                    <td>
                        <button type="button" id="btnOk" class="btn btn-primary" onclick="debugger;myAjaxWeb('XmlHttpCommon.aspx','getString','',callBack)">一键测试</button>
                    </td>
                    <td></td>
                </tr>
                <tr style="height: 34px">
                    <td>
                        <span class="label label-primary" style="float: left">ESB站点数据库配置</span>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">服务器地址</span>
                            <input id="txtESBDataBaseServer" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                    </td>
                </tr>           
            </table>
        </div>
    </form>
</body>
</html>
