<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebServiceTest.aspx.cs" Inherits="Web.WebServiceTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div> 
        第一个数a:      
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br/>
        第二个数b：
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br/>
        <asp:Button ID="Button1" runat="server" Text="调用" OnClick="Button1_Click" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
