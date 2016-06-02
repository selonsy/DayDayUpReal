<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Web.EncodeAndDecode.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
    <script src="../Scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        function Ok() {
            $.ajax({
                type:"GET",
                url: "WebForm2.aspx",
                data: {
                    username1: escape("沈金龙"),
                    username2: encodeURI("沈金龙"),
                    username3: encodeURIComponent("沈金龙"),
                    username4: encodeURI(encodeURI("沈金龙"))
                },
                dataType: "text",
                success: function (data) {
                    $('#txtInput').val(data);
                }
            });
        }

        function myTestForEncodeAndDecode() {
            var username1=escape("沈金龙");
            var username2 = encodeURI("沈金龙");
            var username3 = encodeURIComponent("沈金龙");

            alert(username1);
            alert(username2);
            alert(username3);

            alert(unescape(username1));
            alert(decodeURI(username2));
            alert(decodeURIComponent(username3));
        }

    </script>
    
<body>
    <form id="form1" runat="server">
    <div>
        <input type="button" id="btnClick0" onclick="Ok()" value="异步编码解码">
        <input type="text" id="txtInput">
        <input type="button" id="btnClick1" onclick="myTestForEncodeAndDecode()" value="编码解码">
        <input type="button" id="btnClick2" onclick="niming()" value="匿名对象赋值">
    </div>
    </form>
</body>
</html>
