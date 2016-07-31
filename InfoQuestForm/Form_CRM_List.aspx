<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_CRM_List" CodeBehind="Form_CRM_List.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Customer Relationship Management List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_CRM_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_CRM_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_CRM_List" AssociatedUpdatePanelID="UpdatePanel_CRM_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_CRM_List" runat="server">
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
          <table class="Table">
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
                    <td colspan="4">
                      <asp:Label ID="Label_SearchErrorMessage" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      Facility Type
                    </td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_FacilityType" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_CRM_FacilityType" DataTextField="Facility_Type_Lookup_Name" DataValueField="Facility_Type_Lookup_Id" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_FacilityType_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Facility Type</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_FacilityType" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Facility
                    </td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_CRM_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Report Number
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_ReportNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>&nbsp;&nbsp;
                    </td>
                    <td>Originated At
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_OriginatedAt" runat="server" DataSourceID="SqlDataSource_CRM_OriginatedAt" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Originated At</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_OriginatedAt" runat="server"></asp:SqlDataSource>&nbsp;&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Type
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Type" runat="server" DataSourceID="SqlDataSource_CRM_Type" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Type</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_Type" runat="server"></asp:SqlDataSource>
                    </td>
                    <td>Received From
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_ReceivedFrom" runat="server" DataSourceID="SqlDataSource_CRM_ReceivedFrom" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select From</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_ReceivedFrom" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Patient Visit Number
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_PatientVisitNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                    <td>Patient or Customer Name
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_Name" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Form Status
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Status" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Status</asp:ListItem>
                        <asp:ListItem Value="Pending Approval">Pending Approval</asp:ListItem>
                        <asp:ListItem Value="Approved">Approved</asp:ListItem>
                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                    <td>Closed Out
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Closeout" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                        <asp:ListItem Value="No">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                  </tr>
                  <tr>
                    <td>Form Status Date<br />
                      (yyyy/mm/dd)
                    </td>
                    <td colspan="3">From&nbsp;
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
                  <tr>
                    <td>Routed
                    </td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_Route" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Route</asp:ListItem>
                        <asp:ListItem Value="No">No</asp:ListItem>
                        <asp:ListItem Value="Received">Received</asp:ListItem>
                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
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
          <div>
            &nbsp;
          </div>
          <div>
            &nbsp;
          </div>
          <table class="Table">
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
                      <asp:GridView ID="GridView_CRM_List" runat="server" Width="1000px" AutoGenerateColumns="False" DataSourceID="SqlDataSource_CRM_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="100" OnPreRender="GridView_CRM_List_PreRender" OnDataBound="GridView_CRM_List_DataBound" OnRowCreated="GridView_CRM_List_RowCreated">
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
                              <%=GridView_CRM_List.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10">
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Form" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />
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
                              <td></td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="text-align: center;">
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Form" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("CRM_Id"), Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                          <asp:BoundField DataField="CRM_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="CRM_ReportNumber" />
                          <asp:BoundField DataField="CRM_Type_Name" HeaderText="Type" ReadOnly="True" SortExpression="CRM_Type_Name" />
                          <asp:BoundField DataField="CRM_OriginatedAt_Name" HeaderText="Originated At" ReadOnly="True" SortExpression="CRM_OriginatedAt_Name" />
                          <asp:BoundField DataField="CRM_ReceivedFrom_Name" HeaderText="Received From" ReadOnly="True" SortExpression="CRM_ReceivedFrom_Name" />
                          <asp:BoundField DataField="CRM_CustomerName" HeaderText="Customer Name" ReadOnly="True" SortExpression="CRM_CustomerName" />
                          <asp:BoundField DataField="CRM_PatientVisitNumber" HeaderText="Visit Number" ReadOnly="True" SortExpression="CRM_PatientVisitNumber" />
                          <asp:BoundField DataField="CRM_PatientName" HeaderText="Patient Name" ReadOnly="True" SortExpression="CRM_PatientName" />
                          <asp:BoundField DataField="CRM_DateForwarded" HeaderText="Date Forwarded" ReadOnly="True" SortExpression="CRM_DateForwarded" />
                          <asp:BoundField DataField="CRM_Status" HeaderText="Form Status" ReadOnly="True" SortExpression="CRM_Status" />
                          <asp:BoundField DataField="CRM_StatusDate" HeaderText="Date" ReadOnly="True" SortExpression="CRM_StatusDate" />
                          <asp:BoundField DataField="Routed" HeaderText="Routed" ReadOnly="True" SortExpression="Routed" />
                          <asp:BoundField DataField="CRM_CloseOut" HeaderText="Closed Out" ReadOnly="True" SortExpression="CRM_CloseOut" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_List" runat="server" OnSelected="SqlDataSource_CRM_List_Selected"></asp:SqlDataSource>
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
          <table id="TableBulkApproval" class="Table" runat="server">
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
                      <asp:GridView ID="GridView_CRM_BulkApproval" runat="server" Width="1000px" AllowPaging="True" DataSourceID="SqlDataSource_CRM_BulkApproval" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="100" OnPreRender="GridView_CRM_BulkApproval_PreRender" OnDataBound="GridView_CRM_BulkApproval_DataBound" OnRowCreated="GridView_CRM_BulkApproval_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                              <td>Records Per Page:
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_PageSize" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_SelectedIndexChanged_BulkApproval">
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
                                <asp:DropDownList ID="DropDownList_Page" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_SelectedIndexChanged_BulkApproval">
                                </asp:DropDownList>
                              </td>
                              <td>of
                              <%=GridView_CRM_BulkApproval.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10">
                                <asp:Button ID="Button_ApproveAll" runat="server" Text="Approve All Forms" CssClass="Controls_Button" OnClick="Button_ApproveAll_Click" OnDataBinding="Button_ApproveAll_DataBinding" />&nbsp;
                              <asp:Button ID="Button_Update" runat="server" Text="Update Forms" CssClass="Controls_Button" OnClick="Button_Update_Click" OnDataBinding="Button_Update_DataBinding" />&nbsp;
                              <asp:Button ID="Button_Cancel" runat="server" Text="Cancel" CssClass="Controls_Button" OnClick="Button_Cancel_Click" OnDataBinding="Button_Cancel_DataBinding" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <RowStyle CssClass="GridView_RowStyle_TemplateField" />
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
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <table>
                                <tr>
                                  <td class="Table_TemplateField" colspan="9">
                                    <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label><asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label><asp:HiddenField ID="HiddenField_EditModifiedDate" runat="server" Value='<%# Bind("CRM_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" rowspan="4" style="width: 50px;">
                                    <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("CRM_Id"), Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink></td>
                                  <td class="Table_TemplateField" style="width: 200px;"><strong>Facility</strong></td>
                                  <td class="Table_TemplateField" style="width: 125px;"><strong>Report Number</strong></td>
                                  <td class="Table_TemplateField" style="width: 75px;"><strong>Type</strong></td>
                                  <td class="Table_TemplateField" style="width: 75px;"><strong>Originated At</strong></td>
                                  <td class="Table_TemplateField" style="width: 75px;"><strong>Received From</strong></td>
                                  <td class="Table_TemplateField" style="width: 100px;"><strong>Customer Name</strong></td>
                                  <td class="Table_TemplateField" style="width: 100px;"><strong>Patient Visit Number</strong></td>
                                  <td class="Table_TemplateField" style="width: 100px;"><strong>Patient Name</strong></td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 200px;">
                                    <asp:Label ID="Label_EditFacilityDisplayName" runat="server" Text='<%# Bind("Facility_FacilityDisplayName") %>' Width="185px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 125px;">
                                    <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("CRM_ReportNumber") %>' Width="110px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 75px;">
                                    <asp:Label ID="Label_EditTypeName" runat="server" Text='<%# Bind("CRM_Type_Name") %>' Width="60px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 75px;">
                                    <asp:Label ID="Label_EditOriginatedAtName" runat="server" Text='<%# Bind("CRM_OriginatedAt_Name") %>' Width="60px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 75px;">
                                    <asp:Label ID="Label_EditReceivedFromName" runat="server" Text='<%# Bind("CRM_ReceivedFrom_Name") %>' Width="60px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditCustomerName" runat="server" Text='<%# Bind("CRM_CustomerName") %>' Width="135px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 100px;">
                                    <asp:Label ID="Label_EditPatientVisitNumber" runat="server" Text='<%# Bind("CRM_PatientVisitNumber") %>' Width="85px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditPatientName" runat="server" Text='<%# Bind("CRM_PatientName") %>' Width="135px"></asp:Label></td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4" style="width: 475px;"><strong>Description</strong></td>
                                  <td class="Table_TemplateField" colspan="2" style="width: 225px;"><strong>Form Status</strong></td>
                                  <td class="Table_TemplateField" colspan="2" style="width: 250px;"><strong>
                                    <asp:Label ID="Label_UpdateStatusRejectedLabel" runat="server" Text="Rejection Reason:" Visible="false"></asp:Label></strong></td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4" style="width: 475px;">
                                    <asp:Label ID="Label_EditDescription" runat="server" Text='<%# Bind("CRM_Description") %>' Width="455px"></asp:Label><asp:HiddenField ID="HiddenField_EditCRMId" runat="server" Value='<%# Bind("CRM_Id") %>' />
                                  </td>
                                  <td class="Table_TemplateField" colspan="2" style="width: 225px;">
                                    <asp:DropDownList ID="DropDownList_EditStatus" runat="server" SelectedValue='<%# Bind("CRM_Status") %>' Width="200px" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditStatus_SelectedIndexChanged">
                                      <asp:ListItem Value="Pending Approval">Pending Approval</asp:ListItem>
                                      <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                      <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                    </asp:DropDownList><asp:HiddenField ID="HiddenField_EditStatus" runat="server" Value='<%# Bind("CRM_Status") %>' />
                                    <asp:HiddenField ID="HiddenField_EditStatusDate" runat="server" Value='<%# Bind("CRM_StatusDate") %>' />
                                  </td>
                                  <td class="Table_TemplateField" colspan="2" style="width: 250px;">
                                    <asp:TextBox ID="TextBox_EditStatusRejectedReason" runat="server" Visible="false" TextMode="MultiLine" Rows="3" Width="220px" Text='<%# Bind("CRM_StatusRejectedReason") %>' CssClass="Controls_TextBox"></asp:TextBox></td>
                                </tr>
                              </table>
                            </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_BulkApproval" runat="server" OnSelected="SqlDataSource_CRM_BulkApproval_Selected"></asp:SqlDataSource>
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
