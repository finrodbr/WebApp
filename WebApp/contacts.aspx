<%@ Page Title="Customer List :: Customer Contact Management" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="contacts.aspx.cs" Inherits="WebApp.contacts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#body_txtFromDate").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#body_txtToDate").datepicker({
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager ID="sMgr" runat="server"></asp:ScriptManager>
    <div id="headerDiv">
        <div id="welcomeMessageDiv"><span class="welcomeMsg">Welcome, <%=userName%></span></div>
        <div id="menuDiv">
            <asp:Panel ID="pnlMenu" runat="server">
                <asp:Panel ID="pnlMnuAdmin" runat="server" CssClass="menu mnuAdmin">&nbsp;</asp:Panel>
                <asp:Panel ID="pnlMnuLogout" runat="server" CssClass="menu mnuLogout">
                    <asp:LinkButton ID="lnkLogout" runat="server" OnClick="lnkLogout_Click">Logout</asp:LinkButton>
                </asp:Panel>                
            </asp:Panel>
        </div>
    </div>
    <div id="bodyDiv">
        <div id="filtersDiv">
            <div class="row">
                <div class="left col1">
                    <span class="label">Name:</span> 
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </div>
                <div class="left col1">
                    <span class="label">Gender:</span> 
                    <asp:DropDownList ID="ddlGender" runat="server">
                        <asp:ListItem Text="All gender" Value="0"></asp:ListItem>
                        <asp:ListItem Text="F" Value="1"></asp:ListItem>
                        <asp:ListItem Text="M" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="left col2">
                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search" />
                </div>
                <div class="clear"></div>
            </div>
            <div class="row">
                <asp:UpdatePanel ID="updCityState" runat="server" ChildrenAsTriggers="true">
                    <ContentTemplate>
                <div class="left col1">
                    <span class="label">City:</span> 
                    <asp:DropDownList ID="ddlCity" ClientIDMode="Static" CssClass="fullWidth"  
                        runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="left col1">
                    <span class="label">Region:</span> 
                    <asp:DropDownList ID="ddlRegion" CssClass="fullWidth" Enabled="false" runat="server">
                        <asp:ListItem Text="Select a City" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="left col2">
                    <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" Text="Clear Fields" />
                </div>
                <div class="clear"></div>
            </div>
            <div class="row">
                <div class="left col3">
                    <span class="label">Last Purchase:</span>
                    <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                    <span class="until">until</span>
                    <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                </div>
                <div class="clear"></div>
            </div>
            <div class="row">
                <div class="left col1">
                    <span class="label">Classification:</span> 
                    <asp:DropDownList ID="ddlClassification" CssClass="fullWidth" runat="server"></asp:DropDownList>
                </div>
                <asp:Panel ID="pnlSeller" CssClass="left col1" runat="server">
                    <span class="label">Seller:</span> 
                    <asp:DropDownList ID="ddlSeller" CssClass="fullWidth" runat="server"></asp:DropDownList>
                </asp:Panel>
                <div class="left col2">
                    &nbsp;
                </div>
                <div class="clear"></div>
            </div>
        </div>
        <div id="gridDiv">
            <asp:GridView ID="grdContacts" CellPadding="10" CellSpacing="0" runat="server" 
                OnRowDataBound="grdContacts_RowDataBound" AllowSorting="true" OnSorting="grdContacts_Sorting"
                EmptyDataText="No contact found with those criteria">
                <Columns>
                    <asp:BoundField DataField="classificacaoNome" SortExpression="classificacaoNome" HeaderText="Classification" />
                    <asp:BoundField DataField="nome" SortExpression="nome" HeaderText="Name" />
                    <asp:BoundField DataField="phone" SortExpression="phone" HeaderText="Phone" />
                    <asp:BoundField DataField="sexoNome" SortExpression="sexoNome" HeaderText="Gender" />
                    <asp:BoundField DataField="cidadeNome" SortExpression="cidadeNome" HeaderText="City" />
                    <asp:BoundField DataField="regiaoNome" SortExpression="regiaoNome" HeaderText="Region" />
                    <asp:BoundField DataField="ultima_compra" DataFormatString="{0:d}" SortExpression="ultima_compra" HeaderText="Last Purchase" />
                    <asp:BoundField DataField="usuarioNome" SortExpression="usuarioNome" HeaderText="Seller" />
                </Columns>
                <SortedAscendingHeaderStyle CssClass="arrow-up" />
                <SortedDescendingHeaderStyle CssClass="arrow-down" />
                <RowStyle CssClass="trow1" />
                <AlternatingRowStyle CssClass="trow2" />
                <HeaderStyle CssClass="theader" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
