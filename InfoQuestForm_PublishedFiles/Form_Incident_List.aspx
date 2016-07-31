<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_Incident_List" CodeBehind="Form_Incident_List.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Incident List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Incident_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Incident_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Incident_List" AssociatedUpdatePanelID="UpdatePanel_Incident_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Incident_List" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td>
                <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
              </td>
              <td style="width: 25px"></td>
              <td class="Form_Header">
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
          <table class="Table" style="width: 1000px;">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_SearchHeading" runat="server" Text=""></asp:Label>
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
                    <td style="width: 175px;">
                      Facility Type
                    </td>
                    <td style="width: 875px;">
                      <asp:DropDownList ID="DropDownList_FacilityType" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Incident_FacilityType" DataTextField="Facility_Type_Lookup_Name" DataValueField="Facility_Type_Lookup_Id" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_FacilityType_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Facility Type</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Incident_FacilityType" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Facility
                    </td>
                    <td style="width: 875px;">
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Incident_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Facility_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Incident_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Report Number
                    </td>
                    <td style="width: 875px;">
                      <asp:TextBox ID="TextBox_ReportNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Unit Issued To
                    </td>
                    <td style="width: 875px;">
                      <asp:DropDownList ID="DropDownList_UnitToUnit" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Incident_UnitToUnit" DataTextField="Unit_Name" DataValueField="Unit_Id">
                        <asp:ListItem Value="">Select Unit</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Incident_UnitToUnit" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Form Status
                    </td>
                    <td style="width: 875px;">
                      <asp:DropDownList ID="DropDownList_Status" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Status</asp:ListItem>
                        <asp:ListItem Value="Pending Approval">Pending Approval</asp:ListItem>
                        <asp:ListItem Value="Approved">Approved</asp:ListItem>
                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Investigation Completed
                    </td>
                    <td style="width: 875px;">
                      <asp:DropDownList ID="DropDownList_InvestigationCompleted" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Form Status Date<br />
                      (yyyy/mm/dd)
                    </td>
                    <td style="width: 875px;">From&nbsp;
                      <asp:TextBox ID="TextBox_StatusDateFrom" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_StatusDateFrom" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                      <Ajax:CalendarExtender ID="CalendarExtender_StatusDateFrom" runat="server" CssClass="Calendar" TargetControlID="TextBox_StatusDateFrom" Format="yyyy/MM/dd" PopupButtonID="ImageButton_StatusDateFrom">
                      </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_StatusDateFrom" runat="server" TargetControlID="TextBox_StatusDateFrom" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                      </Ajax:TextBoxWatermarkExtender>
                      &nbsp;&nbsp;&nbsp;To&nbsp;
                      <asp:TextBox ID="TextBox_StatusDateTo" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_StatusDateTo" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                      <Ajax:CalendarExtender ID="CalendarExtender_StatusDateTo" runat="server" CssClass="Calendar" TargetControlID="TextBox_StatusDateTo" Format="yyyy/MM/dd" PopupButtonID="ImageButton_StatusDateTo">
                      </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_StatusDateTo" runat="server" TargetControlID="TextBox_StatusDateTo" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                      </Ajax:TextBoxWatermarkExtender>
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
          <table class="Table" style="width: 1000px;">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridHeading" runat="server" Text=""></asp:Label>
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
                      <asp:GridView ID="GridView_Incident_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_Incident_List" Width="1000px" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_Incident_List_PreRender" OnDataBound="GridView_Incident_List_DataBound" OnRowCreated="GridView_Incident_List_RowCreated">
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
                                <asp:DropDownList ID="DropDownList_PageSize" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_SelectedIndexChanged" OnDataBinding="DropDownList_PageSize_DataBinding">
                                  <asp:ListItem Value="20">20</asp:ListItem>
                                  <asp:ListItem Value="50">50</asp:ListItem>
                                  <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" OnUnload="ImageButton_First_Unload" />
                                <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" OnUnload="ImageButton_Prev_Unload" />
                              </td>
                              <td>Page
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_Page" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_SelectedIndexChanged" OnDataBinding="DropDownList_Page_DataBinding">
                                </asp:DropDownList>
                              </td>
                              <td>of
                          <%=GridView_Incident_List.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" OnUnload="ImageButton_Next_Unload"  />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" OnUnload="ImageButton_Last_Unload" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10" style="text-align:center;">
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Incident" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
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
                              <td style="text-align: center;">
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Incident" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("Incident_Id"), Eval("Facility_Id"), Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                          <asp:BoundField DataField="Incident_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="Incident_ReportNumber" />
                          <asp:BoundField DataField="Incident_UnitTo_Name" HeaderText="Unit Issued To" ReadOnly="True" SortExpression="Incident_UnitTo_Name" />
                          <asp:BoundField DataField="Incident_Status" HeaderText="Form Status" ReadOnly="True" SortExpression="Incident_Status" />
                          <asp:BoundField DataField="Incident_StatusDate" HeaderText="Date" ReadOnly="True" SortExpression="Incident_StatusDate" />
                          <asp:BoundField DataField="Incident_InvestigationCompleted" HeaderText="Investigation Completed" ReadOnly="True" SortExpression="Incident_InvestigationCompleted" />
                          <asp:BoundField DataField="Incident_InvestigationCompletedDate" HeaderText="Date" ReadOnly="True" SortExpression="Incident_InvestigationCompletedDate" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_Incident_List" runat="server" OnSelected="SqlDataSource_Incident_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <div>
            <asp:ImageButton runat="server" ID="ImageButton_BulkApproval" AlternateText="" BorderWidth="0px" Height="0px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
          </div>
          <div>
            &nbsp;
          </div>
          <table id="TableBulkApproval" class="Table" runat="server" style="width: 1000px;">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_BulkApprovalHeading" runat="server" Text=""></asp:Label>
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
                    <asp:Label ID="Label_TotalRecords_BulkApproval" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_Incident_BulkApproval" runat="server" AllowPaging="True" Width="1000px" DataSourceID="SqlDataSource_Incident_BulkApproval" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="100" OnPreRender="GridView_Incident_BulkApproval_PreRender" OnDataBound="GridView_Incident_BulkApproval_DataBound" OnRowCreated="GridView_Incident_BulkApproval_RowCreated">
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
                                <asp:DropDownList ID="DropDownList_PageSize" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_SelectedIndexChanged_BulkApproval" OnDataBinding="DropDownList_PageSize_DataBinding_BulkApproval">
                                  <asp:ListItem Value="20">20</asp:ListItem>
                                  <asp:ListItem Value="50">50</asp:ListItem>
                                  <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" OnUnload="ImageButton_First_Unload_BulkApproval" />
                                <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" OnUnload="ImageButton_Prev_Unload_BulkApproval" />
                              </td>
                              <td>Page
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_Page" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_SelectedIndexChanged_BulkApproval" OnDataBinding="DropDownList_Page_DataBinding_BulkApproval">
                                </asp:DropDownList>
                              </td>
                              <td>of
                          <%=GridView_Incident_BulkApproval.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" OnUnload="ImageButton_Next_Unload_BulkApproval" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" OnUnload="ImageButton_Last_Unload_BulkApproval" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10" style="text-align:center;">
                                <asp:Button ID="Button_ApproveAll" runat="server" Text="Approve All Incidents" CssClass="Controls_Button" OnClick="Button_ApproveAll_Click" />&nbsp;
                          <asp:Button ID="Button_Update" runat="server" Text="Update Incidents" CssClass="Controls_Button" OnClick="Button_Update_Click" />&nbsp;
                          <asp:Button ID="Button_Cancel" runat="server" Text="Cancel" CssClass="Controls_Button" OnClick="Button_Cancel_Click" />&nbsp;
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
                              <td>&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("Incident_Id"), Eval("Facility_Id"), Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                          <asp:BoundField DataField="Incident_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="Incident_ReportNumber" />
                          <asp:BoundField DataField="Incident_Date" HeaderText="Date of Issue" ReadOnly="True" SortExpression="Incident_Date" />
                          <asp:BoundField DataField="Incident_ReportingPerson" HeaderText="Originator" ReadOnly="True" SortExpression="Incident_ReportingPerson" />
                          <asp:BoundField DataField="Incident_UnitFrom_Name" HeaderText="Unit Reported By" ReadOnly="True" SortExpression="Incident_UnitFrom_Name" />
                          <asp:BoundField DataField="Incident_UnitTo_Name" HeaderText="Unit Issued To" ReadOnly="True" SortExpression="Incident_UnitTo_Name" />
                          <asp:BoundField DataField="Incident_Description" HeaderText="Description of Incident" ReadOnly="True" SortExpression="Incident_Description" />
                          <asp:TemplateField HeaderText="Form Status">
                            <ItemTemplate>
                              <asp:HiddenField ID="HiddenField_UpdateId" runat="server" Value='<%# Bind("Incident_Id") %>' />
                              <asp:DropDownList ID="DropDownList_UpdateStatus" runat="server" SelectedValue='<%# Bind("Incident_Status") %>' CssClass="Controls_DropDownList" OnSelectedIndexChanged="DropDownList_UpdateStatus_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                <asp:ListItem Value="Pending Approval">Pending Approval</asp:ListItem>
                                <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                              </asp:DropDownList>
                              <br />
                              <br />
                              <asp:Label ID="Label_UpdateStatusRejectedMessage" runat="server" Text="Rejection Reason is Required" Visible="false" CssClass="Controls_Validation"></asp:Label>
                              <br />
                              <asp:Label ID="Label_UpdateStatusRejectedLabel" runat="server" Text="Rejection Reason:" Visible="false"></asp:Label>
                              <br />
                              <asp:TextBox ID="TextBox_UpdateStatusRejectedReason" runat="server" Text='<%# Bind("Incident_StatusRejectedReason") %>' Rows="4" Visible="false" CssClass="Controls_TextBox" TextMode="MultiLine">
                              </asp:TextBox>
                            </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_Incident_BulkApproval" runat="server" OnSelected="SqlDataSource_Incident_BulkApproval_Selected"></asp:SqlDataSource>
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
