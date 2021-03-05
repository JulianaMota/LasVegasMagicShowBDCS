<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MagicianDetails.aspx.cs" Inherits="LasVegasMagicShowBDCS.MagicianDetails" %>
<%@ OutputCache Location="None" NoStore="true" VaryByParam="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Magicians Acts</title>
    <link href="Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="nav">
            <a href="#home">Home</a>
            <a href="#news">News</a>
            <a href="#contact">Contact</a>
            <a href="#about">About</a>
            <asp:Button ID="ButtonLogout" runat="server" OnClick="ButtonLogout_Click" Text="Logout" CssClass="btn-nav" />
        </div>
        <h1>Magician Details</h1>
        <div runat="server" id="divMage" class="loginInfo">
            <div>
                <asp:Label ID="MageName1" runat="server">Magicain Name:  </asp:Label>
                <asp:Label ID="MageName" runat="server"></asp:Label>
            </div>
            <div>
                <asp:Label ID="ArtistName1" runat="server">Artist Name:  </asp:Label>
                <asp:Label ID="ArtistName" runat="server"></asp:Label>
            </div>
        </div>
        <div class="divTable">
            <div class="divTableTitle">
                <h2>Acts</h2>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:Button ID="ButtonOpenCreate" runat="server" Text="Create an Act" OnClick="ButtonOpenCreate_Click" />
            </div>
            <asp:Repeater ID="RepeaterActs" runat="server" OnItemCommand="RepeaterActs_ItemCommand">
                <HeaderTemplate>
                    <table class="mytable">
                        <tr>
                            <td class="myheader">ActID</td>
                            <td class="myheader">Title</td>
                            <td class="myheader">Description</td>
                            <td class="myheader">Picture</td>
                            <td class="myheader">Duration(min)</td>
                            <td class="myheader">Edit</td>
                            <td class="myheader">Delete</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                
                    <tr>
                        <td class="myitem">
                            <asp:Label ID="ActID" runat="server" Text='<%# Eval("ActID") %>'></asp:Label>

                        </td>
                        <td class="myitem">
                            <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                        </td>
                        <td class="myitem">
                            <asp:Label ID="Desc" runat="server" Text='<%# Eval("Description") %>'></asp:Label>

                        </td>
                        <td class="myitem">
                            <img class="img" id="Img" src="Pictures/<%# Eval("Picture").ToString() == "" ? "default.jpg" : Eval("Picture")  %>" alt="act image" />
                            <asp:Label ID="Picture" runat="server" Text='<%# Eval("Picture") %>' Visible="false"></asp:Label>
                        </td>
                        <td class="myitem">
                            <asp:Label ID="Duration" runat="server" Text='<%# Convert.ToInt32(Eval("Duration")) / 60 %>'></asp:Label>

                        </td>
                        <td class="myitem">
                            <asp:LinkButton ID="LBEdit" runat="server" CommandArgument='<%#Eval("ActID") %>'  CommandName="Edit">Edit</asp:LinkButton>
                        </td>
                        <td class="myitem">
                           <asp:LinkButton ID="LBdelete" runat="server" CommandArgument='<%#Eval("ActID") %>' CommandName="Delete">Delete</asp:LinkButton>
                        </td>
                    </tr>
                    
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            
        </div>
            <div runat="server" id="divCreateUpadate" class="modal hide">
                <div class="modal-box">
                    <asp:Label ID="LabelID" runat="server" Text="Act ID"></asp:Label>
                    <asp:Label ID="LabelTitle" runat="server" Text="Title"></asp:Label>
                    <asp:TextBox ID="TextBoxTitle" runat="server"></asp:TextBox>
                    <asp:Label ID="LabelDesc" runat="server" Text="Description"></asp:Label>
                    <asp:TextBox ID="TextBoxDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="LabelImg" runat="server" Text="Image"></asp:Label>
                    <asp:FileUpload ID="FileUploadImg" runat="server" />
                    <div class="div-upload">
                        <asp:TextBox ID="TextBoxImage" runat="server"></asp:TextBox>
                        <asp:Button ID="ButtonUpload" runat="server" Text="Upload" OnClick="ButtonUpload_Click" />
                    </div>
                    <asp:Label ID="LabelFileinfo" runat="server" Text=""></asp:Label>
                    <asp:Label ID="LabelDuration" runat="server" Text="Duration(min)"></asp:Label>
                    <asp:TextBox ID="TextBoxDuration" runat="server" Width="75px"></asp:TextBox>

                    <asp:Button ID="ButtonUpdate" runat="server" OnClick="ButtonUpdate_Click" Text="Update Act" CssClass="btn-submit" />
                    <asp:Button ID="ButtonCreate" runat="server" OnClick="ButtonCreate_Click" Text="Create new Act" CssClass="btn-submit" />
                     <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" CssClass="btn-cancel" />
                    <asp:Label ID="LabelCU" runat="server"></asp:Label>
                </div>
            </div>
    </form>
</body>
</html>
