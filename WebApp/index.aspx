<%@ Page Title="Login :: Customer Contact Management" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApp.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .errMsg {
            <%=display%>;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:Panel runat="server" DefaultButton="btnLogin" ID="loginDiv" ClientIDMode="Static">
        <div id="divErr">
            <p id="lblErrMsg" class="errMsg">The e-mail and/or password entered is invalid. Please try again.</p>
        </div>
        <p><span class="label">E-mail:</span> <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmail" Display="Dynamic" Font-Bold="true" ForeColor="Red" ValidationGroup="login"></asp:RequiredFieldValidator></p>
        <p><span class="label">Password:</span> <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="*" ControlToValidate="txtPassword" Display="Dynamic" Font-Bold="true" ForeColor="Red" ValidationGroup="login"></asp:RequiredFieldValidator></p>
        <p><span class="label">&nbsp;</span> <asp:Button ID="btnLogin" runat="server" Text="Login" ValidationGroup="login"  /></p>
    </asp:Panel>
</asp:Content>
