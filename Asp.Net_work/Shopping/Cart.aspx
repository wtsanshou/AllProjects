<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">

  
    
    <asp:Label ID="Label1" runat="server" 
        style="z-index: 1; left: 243px; top: 159px; position: absolute" 
        Text="Your shopping cart:"></asp:Label>
    <asp:TextBox ID="SQ" runat="server" 
        style="z-index: 1; left: 247px; top: 203px; position: absolute; height: 194px; width: 224px"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" 
        style="z-index: 1; left: 546px; top: 248px; position: absolute" Text="Button" />
    <asp:Button ID="Button2" runat="server" 
        style="z-index: 1; left: 548px; top: 317px; position: absolute" Text="Button" />
    <asp:Button ID="Button3" runat="server" 
        style="z-index: 1; left: 250px; top: 447px; position: absolute" Text="Button" />
    <asp:Button ID="Button4" runat="server" 
        style="z-index: 1; left: 395px; top: 444px; position: absolute" Text="Button" />
   

</asp:Content>
