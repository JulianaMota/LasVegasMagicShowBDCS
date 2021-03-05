<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="LasVegasMagicShowBDCS.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Las vegas Magic Show</title>
    <link href="Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    
    <form id="form1" runat="server">
        <div class="nav">
            <a href="#home">Home</a>
            <a href="#news">News</a>
            <a href="#contact">Contact</a>
            <a href="#about">About</a>
            <asp:Button ID="ButtonOpenL" runat="server" Text="Sign In" OnClick="ButtonOpenL_Click" CssClass="btn-nav"/>
        </div>
        <h1>Las Vegas Magical Show</h1>
        <div id="divLogin" runat="server" class="modal hide">
            <div class="modal-box" >
                <h2>Sign In</h2>
                <asp:Label ID="LabelUsername" runat="server" Text="Username"></asp:Label>
                <asp:TextBox ID="TextBoxUserName" runat="server"></asp:TextBox>
                <asp:Label ID="LabelPass" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="TextBoxPass" runat="server"></asp:TextBox>
                <asp:Button ID="ButtonLogin" runat="server" OnClick="ButtonLogin_Click" Text="Login" CssClass="btn-submit" />
                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" CssClass="btn-cancel" />
                <asp:Label ID="LabelErrMessage" runat="server"></asp:Label>
            </div>
        </div>
        <div class="divTable">
        <h2>Program</h2>
            <asp:Label ID="LabelT" runat="server" Text="Total time of Program is:"></asp:Label>
            <asp:Label ID="LabelTotal" runat="server" Text=""></asp:Label>

            <asp:Repeater ID="RepeaterProgram" runat="server">
                            <HeaderTemplate>
                <table class="mytable">
                    <tr>
                        <td class="myheader">Sequence</td>
                        <td class="myheader">Title</td>  
                        <td class="myheader">Description</td>
                        <td class="myheader">Picture</td>
                        <td class="myheader">Duration</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                
                <tr>
                    <td class="myitem"><%# Eval("SequenceNum") %></td>
                    <td class="myitem"><%# Eval("Title") %></td>
                    <td class="myitem"><%# Eval("Description") %></td>
                    <td class="myitem"><img class="img" src="Pictures/<%# Eval("Picture").ToString() == "" ? "default.jpg" : Eval("Picture")  %>" alt="act image" /></td>
                    <td class="myitem"><%# Convert.ToInt32(Eval("Duration")) / 60 + " min" %></td>
                </tr>
                    
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
            </asp:Repeater>
            <asp:Label ID="LabelErrMess" runat="server"></asp:Label>
        </div> 
    </form>
</body>
</html>
