<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_RenalClinicalOutcomes_List.aspx.cs" Inherits="InfoQuestForm.Form_RenalClinicalOutcomes_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Renal Clinical Outcomes List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_RenalClinicalOutcomes_List.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_RenalClinicalOutcomes_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_RenalClinicalOutcomes_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_RenalClinicalOutcomes_List" AssociatedUpdatePanelID="UpdatePanel_RenalClinicalOutcomes_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_RenalClinicalOutcomes_List" runat="server">
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
                    <td>Facility
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_RenalClinicalOutcomes_Facility" DataTextField="Facility" DataValueField="Facility">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_RenalClinicalOutcomes_Facility" runat="server"></asp:SqlDataSource>
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
          <div id="DivList" runat="server" style="height: 40px;">
            &nbsp;
          </div>
          <table id="TableList" runat="server" class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridHeading_List" runat="server" Text=""></asp:Label>
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
                    <asp:Label ID="Label_TotalRecords_List" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_RenalClinicalOutcomes_List" runat="server" Width="1000px" AutoGenerateColumns="False" DataSourceID="SqlDataSource_RenalClinicalOutcomes_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="100" OnPreRender="GridView_RenalClinicalOutcomes_List_PreRender" OnDataBound="GridView_RenalClinicalOutcomes_List_DataBound" OnRowCreated="GridView_RenalClinicalOutcomes_List_RowCreated">
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
                              <%=GridView_RenalClinicalOutcomes_List.PageCount%>
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
                                &nbsp;
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
                                &nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="RenalClinicalOutcomes_Facility" HeaderText="Facility" ReadOnly="True" SortExpression="RenalClinicalOutcomes_Facility" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_CaptureMonth" HeaderText="CaptureMonth" ReadOnly="True" SortExpression="RenalClinicalOutcomes_CaptureMonth" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_CaptureYear" HeaderText="CaptureYear" ReadOnly="True" SortExpression="RenalClinicalOutcomes_CaptureYear" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_TreatingDoctorName" HeaderText="TreatingDoctorName" ReadOnly="True" SortExpression="RenalClinicalOutcomes_TreatingDoctorName" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_TreatingDoctorEmail" HeaderText="TreatingDoctorEmail" ReadOnly="True" SortExpression="RenalClinicalOutcomes_TreatingDoctorEmail" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_PatientName" HeaderText="PatientName" ReadOnly="True" SortExpression="RenalClinicalOutcomes_PatientName" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_PatientSurname" HeaderText="PatientSurname" ReadOnly="True" SortExpression="RenalClinicalOutcomes_PatientSurname" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_MedicalAid" HeaderText="MedicalAid" ReadOnly="True" SortExpression="RenalClinicalOutcomes_MedicalAid" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_MedicalAidNumber" HeaderText="MedicalAidNumber" ReadOnly="True" SortExpression="RenalClinicalOutcomes_MedicalAidNumber" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_DateOfBirth" HeaderText="DateOfBirth" ReadOnly="True" SortExpression="RenalClinicalOutcomes_DateOfBirth" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_InfoQuestUploadUser" HeaderText="UploadUser" ReadOnly="True" SortExpression="RenalClinicalOutcomes_InfoQuestUploadUser" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_InfoQuestUploadFrom" HeaderText="UploadFrom" ReadOnly="True" SortExpression="RenalClinicalOutcomes_InfoQuestUploadFrom" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_InfoQuestUploadDate" HeaderText="UploadDate" ReadOnly="True" SortExpression="RenalClinicalOutcomes_InfoQuestUploadDate" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_RenalClinicalOutcomes_List" runat="server" OnSelected="SqlDataSource_RenalClinicalOutcomes_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivUpload" runat="server" style="height: 40px;">
            &nbsp;
          </div>
          <table id="TableUpload" runat="server" class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridHeading_Upload" runat="server" Text=""></asp:Label>
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
                    <asp:Label ID="Label_TotalRecords_Upload" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_RenalClinicalOutcomes_Upload" runat="server" Width="1000px" AutoGenerateColumns="False" DataSourceID="SqlDataSource_RenalClinicalOutcomes_Upload" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_RenalClinicalOutcomes_Upload_PreRender" OnDataBound="GridView_RenalClinicalOutcomes_Upload_DataBound" OnRowCreated="GridView_RenalClinicalOutcomes_Upload_RowCreated">
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
                                <asp:DropDownList ID="DropDownList_PageSize_Upload" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_Upload_SelectedIndexChanged">
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
                                <asp:DropDownList ID="DropDownList_Page_Upload" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_Upload_SelectedIndexChanged">
                                </asp:DropDownList>
                              </td>
                              <td>of
                              <%=GridView_RenalClinicalOutcomes_Upload.PageCount%>
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
                              <asp:LinkButton ID="LinkButton_RetrieveDatabaseFile_Upload" runat="server" OnClick="RetrieveDatabaseFile_Upload" Text="Download" CommandArgument='<%# Eval("RenalClinicalOutcomes_FileUpload_Id") + ";" + Eval("RenalClinicalOutcomes_FileUpload_Data") %>' OnDataBinding="LinkButton_RetrieveDatabaseFile_Upload_DataBinding"></asp:LinkButton>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="RenalClinicalOutcomes_FileUpload_Id" HeaderText="Id" ReadOnly="True" SortExpression="RenalClinicalOutcomes_FileUpload_Id" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_FileUpload_FileName" HeaderText="FileName" ReadOnly="True" SortExpression="RenalClinicalOutcomes_FileUpload_FileName" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_FileUpload_ZipFileName" HeaderText="ZipFileName" ReadOnly="True" SortExpression="RenalClinicalOutcomes_FileUpload_ZipFileName" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_FileUpload_ContentType" HeaderText="ContentType" ReadOnly="True" SortExpression="RenalClinicalOutcomes_FileUpload_ContentType" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_FileUpload_Records" HeaderText="Records" ReadOnly="True" SortExpression="RenalClinicalOutcomes_FileUpload_Records" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_FileUpload_CurrentDate" HeaderText="CurrentDate" ReadOnly="True" SortExpression="RenalClinicalOutcomes_FileUpload_CurrentDate" />
                          <asp:BoundField DataField="RenalClinicalOutcomes_FileUpload_From" HeaderText="From" ReadOnly="True" SortExpression="RenalClinicalOutcomes_FileUpload_From" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_RenalClinicalOutcomes_Upload" runat="server" OnSelected="SqlDataSource_RenalClinicalOutcomes_Upload_Selected"></asp:SqlDataSource>
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
