<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_Isidima_Next" CodeBehind="Form_Isidima_Next.aspx.cs" %>

<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Isidima Next</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Isidima_Next" runat="server" defaultbutton="Button_Search">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <table cellspacing="0" cellpadding="0" border="0">
        <tr>
          <td>
            <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Esidimeni/85_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
          </td>
          <td style="width: 25px"></td>
          <td style="color: #003768; font-size: 18px; font-weight: bold; padding-top: 18px; padding-bottom: 7px">
            <asp:Label ID="Label_Title" runat="server" Text=""></asp:Label>
          </td>
          <td style="width: 25px"></td>
          <td>&nbsp;
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <table cellspacing="0" cellpadding="0" border="0">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0" width="100%">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_SearchHeading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <table class="Record" cellspacing="0" cellpadding="0">
              <tr class="Controls">
                <td class="th">Facility
                </td>
                <td>
                  <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Isidima_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Facility_SelectedIndexChanged">
                    <asp:ListItem Value="">Select Facility</asp:ListItem>
                  </asp:DropDownList>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_Facility" runat="server"></asp:SqlDataSource>
                </td>
              </tr>
              <tr class="Controls">
                <td class="th">Patient Visit Number
                </td>
                <td>
                  <asp:TextBox ID="TextBox_PatientVisitNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                </td>
              </tr>
              <tr class="Controls">
                <td class="th">Ward
                </td>
                <td>
                  <asp:DropDownList ID="DropDownList_Ward" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Isidima_Next_Ward" DataTextField="Isidima_PI_PatientWard" DataValueField="Isidima_PI_PatientWard">
                    <asp:ListItem Value="">Select Ward</asp:ListItem>
                  </asp:DropDownList>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_Next_Ward" runat="server"></asp:SqlDataSource>
                </td>
              </tr>
              <tr class="Bottom">
                <td colspan="2" style="text-align: right;">
                  <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" />&nbsp;
                <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_Click" />&nbsp;
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <table cellspacing="0" cellpadding="0" border="0">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_GridHeading" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <table class="Record" cellspacing="0" cellpadding="0">
              <tr class="Row">
                <td>Total Records:
                <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label>
                </td>
              </tr>
              <tr>
                <td>
                  <asp:GridView ID="GridView_Isidima_Next" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_Isidima_Next" CssClass="Grid" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_Isidima_Next_PreRender" OnDataBound="GridView_Isidima_Next_DataBound" OnRowCreated="GridView_Isidima_Next_RowCreated">
                    <HeaderStyle CssClass="Caption" HorizontalAlign="Left" />
                    <AlternatingRowStyle CssClass="AltRow" />
                    <PagerTemplate>
                      <table cellpadding="0" cellspacing="0">
                        <tr>
                          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          </td>
                          <td>Records Per Page:
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_PageSize" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_SelectedIndexChanged">
                              <asp:ListItem Value="20">20</asp:ListItem>
                              <asp:ListItem Value="50">50</asp:ListItem>
                              <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList>
                          </td>
                          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          </td>
                          <td>
                            <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />&nbsp;
                          <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />&nbsp;
                          </td>
                          <td>Page
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_Page" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_SelectedIndexChanged">
                            </asp:DropDownList>
                          </td>
                          <td>of
                          <%=GridView_Isidima_Next.PageCount%>
                          </td>
                          <td>
                            <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />&nbsp;
                          <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />&nbsp;
                          </td>
                          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          </td>
                        </tr>
                        <tr>
                          <td colspan="10">
                            <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Isidima" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                          </td>
                        </tr>
                      </table>
                    </PagerTemplate>
                    <RowStyle CssClass="Row" />
                    <FooterStyle CssClass="Footer" />
                    <PagerStyle CssClass="Pager" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                      <table class="GridNoRecords" cellspacing="0" cellpadding="0">
                        <tr class="NoRecords">
                          <td>No records
                          </td>
                        </tr>
                        <tr class="Footer">
                          <td>&nbsp;
                          </td>
                        </tr>
                        <tr class="Footer">
                          <td style="text-align: center;">
                            <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Isidima" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                          </td>
                        </tr>
                      </table>
                    </EmptyDataTemplate>
                    <Columns>
                      <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                          <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("Facility_Id") , Eval("Isidima_PI_PatientVisitNumber")) %>' runat="server"></asp:HyperLink>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                      <asp:BoundField DataField="Isidima_PI_PatientVisitNumber" HeaderText="Patient Visit Number" ReadOnly="True" SortExpression="Isidima_PI_PatientVisitNumber" />
                      <asp:BoundField DataField="Isidima_PI_PatientName" HeaderText="Patient Name" ReadOnly="True" SortExpression="Isidima_PI_PatientName" />
                      <asp:BoundField DataField="Isidima_PI_PatientWard" HeaderText="Patient Ward" ReadOnly="True" SortExpression="Isidima_PI_PatientWard" />
                      <asp:BoundField DataField="Isidima_Category_PatientCategory_Name" HeaderText="Patient Category" ReadOnly="True" SortExpression="Isidima_Category_PatientCategory_Name" />
                      <asp:BoundField DataField="Current_Date" HeaderText="Current" ReadOnly="True" SortExpression="Current_Date" />
                      <asp:BoundField DataField="Next_Date" HeaderText="Next" ReadOnly="True" SortExpression="Next_Date" />
                      <asp:BoundField DataField="Days" HeaderText="Days Until Next" ReadOnly="True" SortExpression="Days" />
                    </Columns>
                  </asp:GridView>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_Next" runat="server" OnSelected="SqlDataSource_Isidima_Next_Selected"></asp:SqlDataSource>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
