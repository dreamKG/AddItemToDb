<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddItemToDB.aspx.cs" Inherits="AddItemToDB.Views.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="height: 270px">
    <form id="form1" runat="server">
    <div style="">
        <table style="margin-left:39%;margin-top:20%;width:22%">
            <tr >
                <th>时间</th>
                <th>数据</th>
                <th></th>
            </tr>
            <tr>
                <td>
                    昨天
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
        
    </form>
</body>
</html>
