<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotLogin.aspx.cs" Inherits="LasVegasMagicShowBDCS.NotLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Not Login Page</title>
    <link href="Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="formBack" runat="server">
        <div>
            <asp:Label ID="LabelMessage" runat="server" Text="You are not allowed to visit this page! Login First!"></asp:Label>
        </div>
        <asp:Button ID="ButtonBack" runat="server" OnClick="ButtonBack_Click" Text="Back" />
    </form>
</body>
</html>
