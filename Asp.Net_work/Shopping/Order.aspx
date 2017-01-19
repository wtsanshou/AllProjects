<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">

    
        <asp:Label ID="Label1" runat="server" 
            style="z-index: 1; left: 248px; top: 160px; position: absolute" 
            Text="Please select a product:"></asp:Label>
        <asp:Label ID="Name" runat="server" 
            style="z-index: 1; left: 247px; top: 212px; position: absolute"></asp:Label>
        <asp:Label ID="ShortDes" runat="server" 
            style="z-index: 1; left: 245px; top: 260px; position: absolute; bottom: 243px"></asp:Label>
        <asp:Label ID="LongDes" runat="server" 
            style="z-index: 1; left: 243px; top: 304px; position: absolute"></asp:Label>
        <asp:Label ID="Price" runat="server" 
            style="z-index: 1; left: 241px; top: 350px; position: absolute; height: 7px; width: 40px"></asp:Label>
        <asp:Label ID="Label6" runat="server" 
            style="z-index: 1; left: 242px; top: 390px; position: absolute" 
            Text="Quantity:"></asp:Label>
        <asp:Button ID="AddCart" runat="server" onclick="AddCart_Click" 
            style="z-index: 1; left: 240px; top: 454px; position: absolute" 
            Text="Add to Cart" />
        <asp:Button ID="GoCart" runat="server" CausesValidation="False" 
            PostBackUrl="~/Cart.aspx" 
            style="z-index: 1; left: 463px; top: 459px; position: absolute" 
            Text="Go to Cart" />
        <asp:DropDownList ID="NameList" runat="server" AutoPostBack="True" 
            DataSourceID="AccessDataSource1" DataTextField="Name" 
            DataValueField="ProductID" 
            style="z-index: 1; left: 445px; top: 160px; position: absolute">
        </asp:DropDownList>
        <asp:Image ID="Image" runat="server" 
            style="z-index: 1; left: 582px; top: 130px; position: absolute; height: 319px; width: 274px" />
        <asp:TextBox ID="Quantity" runat="server" 
            style="z-index: 1; left: 419px; top: 397px; position: absolute"></asp:TextBox>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
            DataFile="~/App_Data/Halloween.mdb" 
            SelectCommand="SELECT [ProductID], [Name], [ShortDescription], [LongDescription], [ImageFile], [UnitPrice] FROM [Products]">
        </asp:AccessDataSource>
    
 </asp:Content>
