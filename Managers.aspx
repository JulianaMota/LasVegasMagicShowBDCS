<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Managers.aspx.cs" Inherits="LasVegasMagicShowBDCS.Managers" %>
<%@ OutputCache Location="None" NoStore="true" VaryByParam="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Managers</title>
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
    <h1>Managers</h1>
    <div class="loginInfo">
        <div>
            <asp:Label ID="ManID1" runat="server">ID: </asp:Label>
            <asp:Label ID="ManID" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Label ID="ManNam1" runat="server">Manager Name: </asp:Label>
            <asp:Label ID="ManName" runat="server"></asp:Label>
        </div>
        <asp:Label ID="LabelErrMan" runat="server"></asp:Label>
    </div>
     <div class="divTable">  
        <div>
             <div class="divTableTitle">
                <h2>Porgram Table</h2>
                <asp:Label ID="LabelMessP" runat="server"></asp:Label>
            </div>
            <asp:GridView ID="GridViewPorgram" runat="server" OnSelectedIndexChanged="GridViewPorgram_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ButtonType="Button" HeaderText="Select" ShowHeader="True" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
            
        </div>
        <div>
            <h2>Acts Table</h2>
            <asp:GridView ID="GridViewActs" runat="server" OnSelectedIndexChanged="GridViewActs_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ButtonType="Button" HeaderText="Select" ShowHeader="True" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
         </div>
       </div>
        <div  runat="server" id="divModal" class="modal hide">
            <div class="modal-box">
                <h2 runat="server" id="actName"></h2>
                <div class="personDiv">
                    <h3 runat="server" id="actID1">ID:</h3>
                    <h3 runat="server" id="actID"></h3>
                </div>
                
                <div runat="server" id="divPForm" class="hide">
                    <asp:Button ID="ButtonChange" runat="server" Text="Change Sequence" OnClick="ButtonChange_Click" CssClass="btn-submit btn-middle"/>
                    <asp:Button ID="ButtonRemove" runat="server" Text="Remove Act" OnClick="ButtonRemove_Click" CssClass="btn-cancel btn-middle"/>
                </div>
                <div runat="server" id="divPAForm" class="hide">
                    <asp:Label ID="LabelSequence" runat="server" Text="Sequence number"></asp:Label>
                    <asp:TextBox ID="TextBoxSequence" runat="server"></asp:TextBox>
                    <asp:Button ID="ButtonAddAct" runat="server" Text="Add Act" OnClick="ButtonAddAct_Click" CssClass="btn-submit btn-middle" />
                    <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click" CssClass="btn-submit btn-middle" />
                </div>
                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" CssClass="btn-cancel btn-middle" />
                <asp:Label ID="LabelErrModal" runat="server"></asp:Label>
            </div>
        </div>
        
    </form>
</body>
</html>
