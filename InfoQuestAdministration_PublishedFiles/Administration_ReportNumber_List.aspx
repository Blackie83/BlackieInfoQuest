<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_ReportNumber_List" CodeBehind="Administration_ReportNumber_List.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Report Number List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_ReportNumber_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_ReportNumber_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_ReportNumber_List" AssociatedUpdatePanelID="UpdatePanel_ReportNumber_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_ReportNumber_List" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>Search Report Numbers
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td colspan="2">
                      <asp:Label ID="Label_SearchErrorMessage" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td>Facility
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_FacilityId" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Facility_Id" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Facility_Id" runat="server" SelectCommand="SELECT Facility_Id , Facility_FacilityDisplayName FROM vAdministration_Facility_All ORDER BY Facility_FacilityDisplayName"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Form
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_FormId" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Form_Id" DataTextField="Form_Name" DataValueField="Form_Id">
                        <asp:ListItem Value="">Select Form</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Form_Id" runat="server" SelectCommand="SELECT Form_Id , Form_Name FROM vAdministration_Form_All ORDER BY Form_Name"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Financial Year
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_FinancialYear" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_ReportNumber_FinancialYear" DataTextField="Date_Year" DataValueField="Date_Year">
                        <asp:ListItem Value="">Select Year</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_ReportNumber_FinancialYear" runat="server" SelectCommand="SELECT DISTINCT Date_Year FROM Administration_Date WHERE Date_Year >= '2008' AND Date_Year <= ( SELECT	CASE WHEN MONTH(GETDATE()) IN ('1','2','3','4','5','6','7','8','9') THEN CAST((YEAR(GETDATE()) + 0) AS NVARCHAR(10)) WHEN MONTH(GETDATE()) IN ('10','11','12') THEN CAST((YEAR(GETDATE()) + 1) AS NVARCHAR(10)) END AS FinancialYear ) ORDER BY Date_Year DESC"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Generated Number
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_GeneratedNumber" runat="server" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Generated By
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_GeneratedBy" runat="server" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Date<br />
                      (yyyy/mm/dd)
                    </td>
                    <td>From&nbsp;
                    <asp:TextBox ID="TextBox_GeneratedDateFrom" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_GeneratedDateFrom" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                    <Ajax:CalendarExtender ID="CalendarExtender_GeneratedDateFrom" runat="server" CssClass="Calendar" TargetControlID="TextBox_GeneratedDateFrom" Format="yyyy/MM/dd" PopupButtonID="ImageButton_GeneratedDateFrom">
                    </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_GeneratedDateFrom" runat="server" TargetControlID="TextBox_GeneratedDateFrom" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                      </Ajax:TextBoxWatermarkExtender>
                      &nbsp;&nbsp;&nbsp;To&nbsp;
                    <asp:TextBox ID="TextBox_GeneratedDateTo" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_GeneratedDateTo" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                    <Ajax:CalendarExtender ID="CalendarExtender_GeneratedDateTo" runat="server" CssClass="Calendar" TargetControlID="TextBox_GeneratedDateTo" Format="yyyy/MM/dd" PopupButtonID="ImageButton_GeneratedDateTo">
                    </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_GeneratedDateTo" runat="server" TargetControlID="TextBox_GeneratedDateTo" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                      </Ajax:TextBoxWatermarkExtender>
                    </td>
                  </tr>
                  <tr>
                    <td>Is Active
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_IsActive" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                        <asp:ListItem Value="No">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Footer">
                  <tr>
                    <td>
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
          <table id="TableSearch" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>List of Report Numbers
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>Total Records:
                    <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_ReportNumber_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_ReportNumber_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_ReportNumber_List_PreRender" OnDataBound="GridView_ReportNumber_List_DataBound" OnRowCreated="GridView_ReportNumber_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
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
                                <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />
                                <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />
                              </td>
                              <td>Page
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_Page" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_SelectedIndexChanged">
                                </asp:DropDownList>
                              </td>
                              <td>of
                              <%=GridView_ReportNumber_List.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <RowStyle CssClass="GridView_RowStyle" />
                        <FooterStyle CssClass="GridView_FooterStyle" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td>No records
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>&nbsp;
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="text-align: center;">&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="ReportNumber_Id" HeaderText="Id" ReadOnly="True" SortExpression="ReportNumber_Id" />
                          <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                          <asp:BoundField DataField="Form_Name" HeaderText="Form" ReadOnly="True" SortExpression="Form_Name" />
                          <asp:BoundField DataField="ReportNumber_FinancialYear" HeaderText="Year" ReadOnly="True" SortExpression="ReportNumber_FinancialYear" />
                          <asp:BoundField DataField="ReportNumber_GeneratedNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="ReportNumber_GeneratedNumber" />
                          <asp:BoundField DataField="ReportNumber_GeneratedBy" HeaderText="By" ReadOnly="True" SortExpression="ReportNumber_GeneratedBy" />
                          <asp:BoundField DataField="ReportNumber_GeneratedDate" HeaderText="Date" ReadOnly="True" SortExpression="ReportNumber_GeneratedDate" />
                          <asp:BoundField DataField="ReportNumber_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="ReportNumber_GeneratedBy" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_ReportNumber_List" runat="server" SelectCommand="spAdministration_Get_ReportNumber_List" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnSelected="SqlDataSource_ReportNumber_List_Selected">
                        <SelectParameters>
                          <asp:QueryStringParameter Name="FacilityId" QueryStringField="s_Facility_Id" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="FormId" QueryStringField="s_Form_Id" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="FinancialYear" QueryStringField="s_ReportNumber_FinancialYear" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="GeneratedNumber" QueryStringField="s_ReportNumber_GeneratedNumber" Type="DateTime" DefaultValue="" />
                          <asp:QueryStringParameter Name="GeneratedBy" QueryStringField="s_ReportNumber_GeneratedBy" Type="DateTime" DefaultValue="" />
                          <asp:QueryStringParameter Name="GeneratedDateFrom" QueryStringField="s_ReportNumber_GeneratedDateFrom" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="GeneratedDateTo" QueryStringField="s_ReportNumber_GeneratedDateTo" Type="DateTime" DefaultValue="" />
                          <asp:QueryStringParameter Name="IsActive" QueryStringField="s_ReportNumber_IsActive" Type="DateTime" DefaultValue="" />
                        </SelectParameters>
                      </asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
