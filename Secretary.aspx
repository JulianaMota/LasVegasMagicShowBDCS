<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Secretary.aspx.cs" Inherits="LasVegasMagicShowBDCS.Secretaries" %>
<%@ OutputCache Location="None" NoStore="true" VaryByParam="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secretaries</title>
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
        <h1>Secretaries</h1>
        
        <div class="loginInfo">
            <div>
                <asp:Label ID="SecretaryID1" runat="server">Secretary ID: </asp:Label>
                <asp:Label ID="SecretaryID" runat="server"></asp:Label>
            </div>
            <div>
                <asp:Label ID="SecreName1" runat="server">Secretary Name: </asp:Label>
                <asp:Label ID="SecreName" runat="server"> </asp:Label>
            </div>
        </div>
        <div class="divTable">
            <div>
                <div class="divTableTitle">
                    <h2>Magicians List</h2>
                    <asp:Label ID="LabelMessMage" runat="server" Text=""></asp:Label>
                    <asp:Button ID="ButtonAddM" runat="server" Text="Add Magician" OnClick="ButtonAddM_Click" />
                </div>
                <asp:GridView ID="GridViewMagicians" runat="server" OnSelectedIndexChanged="GridViewMagicians_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ButtonType="Button" HeaderText="Select Magician" ShowSelectButton="True" SelectText="Setect"/>
                    </Columns>
                </asp:GridView>
            </div>

             <div class="divTableTitle">
                <h2>Secretaries List</h2>
                 <asp:Label ID="LabelMessSecre" runat="server" Text=""></asp:Label>
                 <asp:Button ID="ButtonAddS" runat="server" Text="Add Secretary" OnClick="ButtonAddS_Click" />
            </div>
            <asp:GridView ID="GridViewSecretaries" runat="server" OnSelectedIndexChanged="GridViewSecretaries_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ButtonType="Button" HeaderText="Select Secretary" ShowSelectButton="True" SelectText="Setect"/>
                </Columns>
            </asp:GridView>
            
            <div class="divTableTitle">
                <h2>Managers List</h2>
                <asp:Label ID="LabelMess" runat="server" Text=""></asp:Label>
                <asp:Button ID="ButtonAddMan" runat="server" Text="Add Manager" OnClick="ButtonAddMan_Click" />
            </div>
            <asp:GridView ID="GridViewManager" runat="server" OnSelectedIndexChanged="GridViewManager_SelectedIndexChanged">
                <Columns>
                   <asp:CommandField ButtonType="Button" HeaderText="Select Manager" ShowSelectButton="True" SelectText="Setect"/>
                </Columns>
            </asp:GridView>
            
            <div runat="server" id="divCU" class="modal hide">
                <div class="modal-box">
                <div>
                    <asp:Label ID="LabelName" runat="server" Text="Name"></asp:Label>
                    <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                    <asp:Label ID="LabelPass" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="TextBoxPass" runat="server"></asp:TextBox>
                </div>
                <div runat="server" id="mageFields" class="hide">
                    <asp:Label ID="LabelAName" runat="server" Text="Artist Name"></asp:Label>
                    <asp:TextBox ID="TextBoxAName" runat="server"></asp:TextBox>
                    <asp:Label ID="LabelManagerID" runat="server" Text="Manager ID"></asp:Label>
                    <asp:TextBox ID="TextBoxManID" runat="server" Width="57px"></asp:TextBox>
                </div>
                <div runat="server" id="secrFields" class="hide">
                    <asp:Label ID="LabelMID" runat="server" Text="Magician ID"></asp:Label>
                    <asp:TextBox ID="TextBoxMID" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Button ID="ButtonCreateM" runat="server" Text="Create Magician" OnClick="ButtonCreateM_Click" CssClass="btn-submit btn-middle" />
                    <asp:Button ID="ButtonCreateS" runat="server" Text="Create Secretary" OnClick="ButtonCreateS_Click" CssClass="btn-submit btn-middle" />
                    <asp:Button ID="ButtonCreateMan" runat="server" Text="Create Manager" OnClick="ButtonCreateMan_Click" CssClass="btn-submit btn-middle" />
                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" CssClass="btn-cancel btn-middle" />
                    <asp:Label ID="LabelInfo" runat="server"></asp:Label>
                </div>
                </div>
            </div>

             <div runat="server" id="divUD" class="modal hide">
                 <div class="modal-box">
                     <div>
                         <h2 runat="server" id="personName"></h2>
                         <div class="personDiv">
                            <p>ID: </p>
                            <p runat="server" id="personId"></p>
                         </div>                         
                         <asp:Button ID="ButtonUpdateSecre" runat="server" Text="Update Sercetary" OnClick="ButtonUpdateSecre_Click" CssClass="btn-submit btn-middle" />
                         <asp:Button ID="ButtonUpdateMan" runat="server" Text="Update Manager" OnClick="ButtonUpdateMan_Click" CssClass="btn-submit btn-middle" />
                         <asp:Button ID="ButtonUpdate" runat="server" Text="Update Magicain" OnClick="ButtonUpdate_Click" CssClass="btn-submit btn-middle" />
                         <asp:Button ID="ButtonDeleteMage" runat="server" Text="Delete Magician" OnClick="ButtonDeleteMage_Click" CssClass="btn-cancel btn-middle" />
                         <asp:Button ID="ButtonDeleteSecre" runat="server" Text="Delete Secretary" OnClick="ButtonDeleteSecre_Click" CssClass="btn-cancel btn-middle" />
                         <asp:Button ID="ButtonDeleteMan" runat="server" Text="Delete Manager" OnClick="ButtonDeleteMan_Click" CssClass="btn-cancel btn-middle" />
                     </div>
                    <div runat="server" id="divUpdate" class="hide">
                        <asp:Label ID="LabelUName" runat="server" Text="Name"></asp:Label>
                        <asp:TextBox ID="TextBoxUName" runat="server"></asp:TextBox>
                        <asp:Label ID="LabelUPass" runat="server" Text="Password"></asp:Label>
                        <asp:TextBox ID="TextBoxUPass" runat="server"></asp:TextBox>
            
                        <div runat="server" id="DivUmage" class="hide">
                            <asp:Label ID="LabelUAName" runat="server" Text="Artist Name"></asp:Label>
                            <asp:TextBox ID="TextBoxUAAName" runat="server"></asp:TextBox>
                            <asp:Label ID="LabelUManID" runat="server" Text="Manager ID"></asp:Label>
                            <asp:TextBox ID="TextBoxUManID" runat="server" Width="57px"></asp:TextBox>
                        </div>
                        <div runat="server" id="DivUSecre" class="hide">
                            <asp:Label ID="LabelUMID" runat="server" Text="Magician ID"></asp:Label>
                            <asp:TextBox ID="TextBoxUMID" runat="server"></asp:TextBox>
                        </div>
            
                        <asp:Button ID="ButtonUMage" runat="server" Text="Save" OnClick="ButtonUMage_Click" CssClass="btn-submit btn-middle hide" />
                        <asp:Button ID="ButtonUSecre" runat="server" Text="Save" OnClick="ButtonUSecre_Click" CssClass="btn-submit btn-middle hide" />
                        <asp:Button ID="ButtonUMan" runat="server" Text="Save" OnClick="ButtonUMan_Click" CssClass="btn-submit btn-middle hide" />
                        <asp:Label ID="LabelUInfo" runat="server"></asp:Label>
                    </div>
                     <asp:Button ID="ButtonCancelUD" runat="server" Text="Cancel" OnClick="ButtonCancelUD_Click" CssClass="btn-cancel btn-middle" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
