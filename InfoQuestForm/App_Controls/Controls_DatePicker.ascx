<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Controls_DatePicker.ascx.cs" Inherits="InfoQuestForm.Controls_DatePicker" %>
<table>
  <tr>
    <td>
      <asp:DropDownList ID="DropDownList_Year" runat="server" CssClass="Controls_DropDownList">
        <asp:ListItem Value="">Select Year</asp:ListItem>
        <asp:ListItem Value="2015">2015</asp:ListItem>
        <asp:ListItem Value="2016">2016</asp:ListItem>
        <asp:ListItem Value="2017">2017</asp:ListItem>
        <asp:ListItem Value="2018">2018</asp:ListItem>
        <asp:ListItem Value="2019">2019</asp:ListItem>
        <asp:ListItem Value="2020">2020</asp:ListItem>
      </asp:DropDownList>
    </td>
    <td> / </td>
    <td>
      <asp:DropDownList ID="DropDownList_Month" runat="server" CssClass="Controls_DropDownList">
        <asp:ListItem Value="">Select Month</asp:ListItem>
        <asp:ListItem Value="01">01</asp:ListItem>
        <asp:ListItem Value="02">02</asp:ListItem>
        <asp:ListItem Value="03">03</asp:ListItem>
        <asp:ListItem Value="04">04</asp:ListItem>
        <asp:ListItem Value="05">05</asp:ListItem>
        <asp:ListItem Value="06">06</asp:ListItem>
        <asp:ListItem Value="07">07</asp:ListItem>
        <asp:ListItem Value="08">08</asp:ListItem>
        <asp:ListItem Value="09">09</asp:ListItem>
        <asp:ListItem Value="10">10</asp:ListItem>
        <asp:ListItem Value="11">11</asp:ListItem>
        <asp:ListItem Value="12">12</asp:ListItem>
      </asp:DropDownList>
    </td>
    <td> / </td>
    <td>
      <asp:DropDownList ID="DropDownList_Day" runat="server" CssClass="Controls_DropDownList">
        <asp:ListItem Value="">Select Day</asp:ListItem>
        <asp:ListItem Value="01">01</asp:ListItem>
        <asp:ListItem Value="02">02</asp:ListItem>
        <asp:ListItem Value="03">03</asp:ListItem>
        <asp:ListItem Value="04">04</asp:ListItem>
        <asp:ListItem Value="05">05</asp:ListItem>
        <asp:ListItem Value="06">06</asp:ListItem>
        <asp:ListItem Value="07">07</asp:ListItem>
        <asp:ListItem Value="08">08</asp:ListItem>
        <asp:ListItem Value="09">09</asp:ListItem>
        <asp:ListItem Value="10">10</asp:ListItem>
        <asp:ListItem Value="19">19</asp:ListItem>
        <asp:ListItem Value="20">20</asp:ListItem>
        <asp:ListItem Value="21">21</asp:ListItem>
        <asp:ListItem Value="22">22</asp:ListItem>
        <asp:ListItem Value="23">23</asp:ListItem>
        <asp:ListItem Value="24">24</asp:ListItem>
        <asp:ListItem Value="25">25</asp:ListItem>
      </asp:DropDownList>
    </td>
  </tr>
</table>

