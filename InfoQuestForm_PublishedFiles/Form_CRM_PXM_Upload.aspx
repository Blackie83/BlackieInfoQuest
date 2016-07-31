<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_CRM_PXM_Upload.aspx.cs" Inherits="InfoQuestForm.Form_CRM_PXM_Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Customer Relationship Management - PXM Upload</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_CRM_PXM_Upload.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_CRM_PXM_Upload" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_CRM_PXM_Upload" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_CRM_PXM_Upload" AssociatedUpdatePanelID="UpdatePanel_CRM_PXM_Upload">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_CRM_PXM_Upload" runat="server">
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
                    <td colspan="2">
                      <asp:Label ID="Label_SearchErrorMessage" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td id="SearchCurrentDate">Current Date (yyyy/mm/dd)
                    </td>
                    <td>From&nbsp;
                      <asp:TextBox ID="TextBox_CurrentDateFrom" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_CurrentDateFrom" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                      <Ajax:CalendarExtender ID="CalendarExtender_CurrentDateFrom" runat="server" CssClass="Calendar" TargetControlID="TextBox_CurrentDateFrom" Format="yyyy/MM/dd" PopupButtonID="ImageButton_CurrentDateFrom">
                      </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_CurrentDateFrom" runat="server" TargetControlID="TextBox_CurrentDateFrom" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                      </Ajax:TextBoxWatermarkExtender>
                      &nbsp;&nbsp;&nbsp;To&nbsp;
                      <asp:TextBox ID="TextBox_CurrentDateTo" runat="server" Width="75px" CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_CurrentDateTo" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                      <Ajax:CalendarExtender ID="CalendarExtender_CurrentDateTo" runat="server" CssClass="Calendar" TargetControlID="TextBox_CurrentDateTo" Format="yyyy/MM/dd" PopupButtonID="ImageButton_CurrentDateTo">
                      </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_CurrentDateTo" runat="server" TargetControlID="TextBox_CurrentDateTo" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
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
          <div id="DivEvent" runat="server" style="height: 40px;">
            &nbsp;
          </div>
          <table id="TableEvent" runat="server" class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridHeading_Event" runat="server" Text=""></asp:Label>
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
                    <asp:Label ID="Label_TotalRecords_Event" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_CRM_Event_FileUploaded" runat="server" Width="1000px" AutoGenerateColumns="False" DataSourceID="SqlDataSource_CRM_Event_FileUploaded" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_CRM_Event_FileUploaded_PreRender" OnDataBound="GridView_CRM_Event_FileUploaded_DataBound" OnRowCreated="GridView_CRM_Event_FileUploaded_RowCreated">
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
                                <asp:DropDownList ID="DropDownList_PageSize_Event" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_Event_SelectedIndexChanged">
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
                                <asp:DropDownList ID="DropDownList_Page_Event" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_Event_SelectedIndexChanged">
                                </asp:DropDownList>
                              </td>
                              <td>of
                              <%=GridView_CRM_Event_FileUploaded.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10">&nbsp;
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
                              <td style="text-align: center;">&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:LinkButton ID="LinkButton_RetrieveDatabaseFile_Event" runat="server" OnClick="RetrieveDatabaseFile_Event" Text="Download" CommandArgument='<%# Eval("PXM_PDCH_Event_FileCreated_Id") + ";" + Eval("PXM_PDCH_Event_FileCreated_Data") %>' OnDataBinding="LinkButton_RetrieveDatabaseFile_Event_DataBinding"></asp:LinkButton>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="PXM_PDCH_Event_FileCreated_Id" HeaderText="Id" ReadOnly="True" SortExpression="PXM_PDCH_Event_FileCreated_Id" />
                          <asp:BoundField DataField="PXM_PDCH_Event_FileCreated_Path" HeaderText="Path" ReadOnly="True" SortExpression="PXM_PDCH_Event_FileCreated_Path" />
                          <asp:BoundField DataField="PXM_PDCH_Event_FileCreated_CurrentDate" HeaderText="CurrentDate" ReadOnly="True" SortExpression="PXM_PDCH_Event_FileCreated_CurrentDate" />
                          <asp:BoundField DataField="PXM_PDCH_Event_FileCreated_StartDate" HeaderText="Start Date" ReadOnly="True" SortExpression="PXM_PDCH_Event_FileCreated_StartDate" />
                          <asp:BoundField DataField="PXM_PDCH_Event_FileCreated_EndDate" HeaderText="End Date" ReadOnly="True" SortExpression="PXM_PDCH_Event_FileCreated_EndDate" />
                          <asp:BoundField DataField="PXM_PDCH_Event_FileCreated_Facility_Id" HeaderText="Facility" ReadOnly="True" SortExpression="PXM_PDCH_Event_FileCreated_Facility_Id" />
                          <asp:BoundField DataField="PXM_PDCH_Event_FileCreated_ZipFileName" HeaderText="ZipFileName" ReadOnly="True" SortExpression="PXM_PDCH_Event_FileCreated_ZipFileName" />
                          <asp:BoundField DataField="PXM_PDCH_Event_FileCreated_ContentType" HeaderText="ContentType" ReadOnly="True" SortExpression="PXM_PDCH_Event_FileCreated_ContentType" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_Event_FileUploaded" runat="server" OnSelected="SqlDataSource_CRM_Event_FileUploaded_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivEscalation" runat="server" style="height: 40px;">
            &nbsp;
          </div>
          <table id="TableEscalation" runat="server" class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridHeading_Escalation" runat="server" Text=""></asp:Label>
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
                    <asp:Label ID="Label_TotalRecords_Escalation" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_CRM_Escalation_FileUploaded" runat="server" Width="1000px" AutoGenerateColumns="False" DataSourceID="SqlDataSource_CRM_Escalation_FileUploaded" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_CRM_Escalation_FileUploaded_PreRender" OnDataBound="GridView_CRM_Escalation_FileUploaded_DataBound" OnRowCreated="GridView_CRM_Escalation_FileUploaded_RowCreated">
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
                                <asp:DropDownList ID="DropDownList_PageSize_Escalation" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_Escalation_SelectedIndexChanged">
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
                                <asp:DropDownList ID="DropDownList_Page_Escalation" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_Escalation_SelectedIndexChanged">
                                </asp:DropDownList>
                              </td>
                              <td>of
                              <%=GridView_CRM_Escalation_FileUploaded.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10">&nbsp;
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
                              <td style="text-align: center;">&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:LinkButton ID="LinkButton_RetrieveDatabaseFile_Escalation" runat="server" OnClick="RetrieveDatabaseFile_Escalation" Text="Download" CommandArgument='<%# Eval("PXM_PDCH_Escalation_FileUploaded_Id") + ";" + Eval("PXM_PDCH_Escalation_FileUploaded_Data") %>' OnDataBinding="LinkButton_RetrieveDatabaseFile_Escalation_DataBinding"></asp:LinkButton>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="PXM_PDCH_Escalation_FileUploaded_Id" HeaderText="Id" ReadOnly="True" SortExpression="PXM_PDCH_Escalation_FileUploaded_Id" />
                          <asp:BoundField DataField="PXM_PDCH_Escalation_FileUploaded_FileName" HeaderText="FileName" ReadOnly="True" SortExpression="PXM_PDCH_Escalation_FileUploaded_FileName" />
                          <asp:BoundField DataField="PXM_PDCH_Escalation_FileUploaded_ZipFileName" HeaderText="ZipFileName" ReadOnly="True" SortExpression="PXM_PDCH_Escalation_FileUploaded_ZipFileName" />
                          <asp:BoundField DataField="PXM_PDCH_Escalation_FileUploaded_ContentType" HeaderText="ContentType" ReadOnly="True" SortExpression="PXM_PDCH_Escalation_FileUploaded_ContentType" />
                          <asp:BoundField DataField="PXM_PDCH_Escalation_FileUploaded_Records" HeaderText="Records" ReadOnly="True" SortExpression="PXM_PDCH_Escalation_FileUploaded_Records" />
                          <asp:BoundField DataField="PXM_PDCH_Escalation_FileUploaded_CurrentDate" HeaderText="CurrentDate" ReadOnly="True" SortExpression="PXM_PDCH_Escalation_FileUploaded_CurrentDate" />
                          <asp:BoundField DataField="PXM_PDCH_Escalation_FileUploaded_From" HeaderText="From" ReadOnly="True" SortExpression="PXM_PDCH_Escalation_FileUploaded_From" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_Escalation_FileUploaded" runat="server" OnSelected="SqlDataSource_CRM_Escalation_FileUploaded_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivReport" runat="server" style="height: 40px;">
            &nbsp;
          </div>
          <table id="TableReport" runat="server" class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridHeading_Report" runat="server" Text=""></asp:Label>
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
                    <asp:Label ID="Label_TotalRecords_Report" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_CRM_Report_FileUploaded" runat="server" Width="1000px" AutoGenerateColumns="False" DataSourceID="SqlDataSource_CRM_Report_FileUploaded" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_CRM_Report_FileUploaded_PreRender" OnDataBound="GridView_CRM_Report_FileUploaded_DataBound" OnRowCreated="GridView_CRM_Report_FileUploaded_RowCreated">
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
                                <asp:DropDownList ID="DropDownList_PageSize_Report" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_Report_SelectedIndexChanged">
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
                                <asp:DropDownList ID="DropDownList_Page_Report" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_Report_SelectedIndexChanged">
                                </asp:DropDownList>
                              </td>
                              <td>of
                              <%=GridView_CRM_Report_FileUploaded.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10">&nbsp;
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
                              <td style="text-align: center;">&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:LinkButton ID="LinkButton_RetrieveDatabaseFile_Report" runat="server" OnClick="RetrieveDatabaseFile_Report" Text="Download" CommandArgument='<%# Eval("PXM_PDCH_Report_FileUploaded_Id") + ";" + Eval("PXM_PDCH_Report_FileUploaded_Data") %>' OnDataBinding="LinkButton_RetrieveDatabaseFile_Report_DataBinding"></asp:LinkButton>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="PXM_PDCH_Report_FileUploaded_Id" HeaderText="Id" ReadOnly="True" SortExpression="PXM_PDCH_Report_FileUploaded_Id" />
                          <asp:BoundField DataField="PXM_PDCH_Report_FileUploaded_FileName" HeaderText="FileName" ReadOnly="True" SortExpression="PXM_PDCH_Report_FileUploaded_FileName" />
                          <asp:BoundField DataField="PXM_PDCH_Report_FileUploaded_ZipFileName" HeaderText="ZipFileName" ReadOnly="True" SortExpression="PXM_PDCH_Report_FileUploaded_ZipFileName" />
                          <asp:BoundField DataField="PXM_PDCH_Report_FileUploaded_ContentType" HeaderText="ContentType" ReadOnly="True" SortExpression="PXM_PDCH_Report_FileUploaded_ContentType" />
                          <asp:BoundField DataField="PXM_PDCH_Report_FileUploaded_Records" HeaderText="Records" ReadOnly="True" SortExpression="PXM_PDCH_Report_FileUploaded_Records" />
                          <asp:BoundField DataField="PXM_PDCH_Report_FileUploaded_CurrentDate" HeaderText="CurrentDate" ReadOnly="True" SortExpression="PXM_PDCH_Report_FileUploaded_CurrentDate" />
                          <asp:BoundField DataField="PXM_PDCH_Report_FileUploaded_From" HeaderText="From" ReadOnly="True" SortExpression="PXM_PDCH_Report_FileUploaded_From" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_Report_FileUploaded" runat="server" OnSelected="SqlDataSource_CRM_Report_FileUploaded_Selected"></asp:SqlDataSource>
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
