<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_IPS_BedHistory.aspx.cs" Inherits="InfoQuestForm.Form_IPS_BedHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Infection Prevention Surveillance - Bed History</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_IPS_BedHistory.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_IPS_BedHistory" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_IPS_BedHistory" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_IPS_BedHistory" AssociatedUpdatePanelID="UpdatePanel_IPS_BedHistory">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_IPS_BedHistory" runat="server">
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
          <table id="TableInfo" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_InfoHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="width: 90px">Facility:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Visit Number:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Surname, Name:
                    </td>
                    <td style="width: 150px">
                      <asp:Label ID="Label_IName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 90px">Report Number:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IInfectionReportNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Category:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IInfectionCategoryName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Type:
                    </td>
                    <td style="width: 150px">
                      <asp:Label ID="Label_IInfectionTypeName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 90px">Infection:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IInfectionCompleted" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Specimen:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_ISpecimen" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">HAI Investigation:
                    </td>
                    <td style="width: 150px">
                      <asp:Label ID="Label_IHAI" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Footer">
                  <tr>
                    <td style="text-align: center;">
                      <asp:Button ID="Button_InfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_InfectionHome_OnClick" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div style="height: 40px; width: 900px; text-align: center;">
            &nbsp;
          </div>
          <table id="TableBedHistory" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_BedHistoryHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenTotalRecords" runat="server" Text="" Visible="false" />
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="padding: 0px;">
                      <table class="GridView_PagerStyle">
                        <tr>
                          <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_TopTotalRecords" runat="server" Text=""></asp:Label></td>
                          <td style="width: 800px; text-align: center;">
                            <asp:Button ID="Button_TopUpdate" runat="server" Text="Update Bed History" CssClass="Controls_Button" OnClick="Button_Update_OnClick" />
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_IPS_BedHistory" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource_IPS_BedHistory" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_BedHistory_PreRender" OnRowCreated="GridView_IPS_BedHistory_RowCreated" OnRowDataBound="GridView_IPS_BedHistory_RowDataBound">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_Update" runat="server" Text="Update Bed History" CssClass="Controls_Button" OnClick="Button_Update_OnClick" />
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
                              <td colspan="2">No Bed History
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="" ItemStyle-Width="60px">
                            <ItemTemplate>
                              <asp:CheckBox ID="CheckBox_Selected" runat="server" />
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                              <asp:Label ID="Label_Department" runat="server" Text='<%# Bind("Department") %>' Width="210px"></asp:Label>
                              <asp:HiddenField ID="HiddenField_Department" runat="server" Value='<%# Bind("Department") %>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Room">
                            <ItemTemplate>
                              <asp:Label ID="Label_Room" runat="server" Text='<%# Bind("Room") %>' Width="210px"></asp:Label>
                              <asp:HiddenField ID="HiddenField_Room" runat="server" Value='<%# Bind("Room") %>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Bed">
                            <ItemTemplate>
                              <asp:Label ID="Label_Bed" runat="server" Text='<%# Bind("Bed") %>' Width="210px"></asp:Label>
                              <asp:HiddenField ID="HiddenField_Bed" runat="server" Value='<%# Bind("Bed") %>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                              <asp:Label ID="Label_Date" runat="server" Text='<%# Bind("Date") %>' Width="210px"></asp:Label>
                              <asp:HiddenField ID="HiddenField_Date" runat="server" Value='<%# Bind("Date") %>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:ObjectDataSource ID="ObjectDataSource_IPS_BedHistory" runat="server" OnSelected="ObjectDataSource_IPS_BedHistory_Selected"></asp:ObjectDataSource>
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
