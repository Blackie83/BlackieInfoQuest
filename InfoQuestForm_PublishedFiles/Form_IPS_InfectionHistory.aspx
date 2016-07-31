<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_IPS_InfectionHistory.aspx.cs" Inherits="InfoQuestForm.Form_IPS_InfectionHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Infection Prevention Surveillance - Infection History</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_IPS_InfectionHistory" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_IPS_InfectionHistory" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_IPS_InfectionHistory" AssociatedUpdatePanelID="UpdatePanel_IPS_InfectionHistory">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_IPS_InfectionHistory" runat="server">
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
          <table id="TableInfo" class="Table" style="width: 750px;" runat="server">
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
                    <td style="width: 50px">Facility:
                    </td>
                    <td style="width: 200px">
                      <asp:Label ID="Label_IFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 100px">Visit Number:
                    </td>
                    <td style="width: 100px">
                      <asp:Label ID="Label_IVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 100px">Surname, Name:
                    </td>
                    <td style="width: 200px">
                      <asp:Label ID="Label_IName" runat="server" Text=""></asp:Label>&nbsp;
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
                      <asp:Button ID="Button_Close" runat="server" Text="Close Window" CssClass="Controls_Button" OnClick="Button_Close_OnClick" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div style="height: 40px; width: 750px; text-align: center;">
            &nbsp;
          </div>
          <table id="TableInfectionHistory" class="Table" style="width: 750px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_InfectionHistoryHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenInfectionHistoryTotalRecords" runat="server" Text="" Visible="false" />
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
                      <asp:GridView ID="GridView_IPS_InfectionHistory_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_InfectionHistory_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_InfectionHistory_List_PreRender" OnRowCreated="GridView_IPS_InfectionHistory_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_InfectionHistoryTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px;">&nbsp;
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
                              <td colspan="2">No Infection History Captured
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_InfectionHistoryTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <table>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <strong>Facility</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <strong>Visit Number</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <strong>Life Number</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <strong>Admission Date</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <strong>Discharge Date</strong>
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_Facility" runat="server" Text='<%# Bind("Facility_FacilityDisplayName") %>' Width="130px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_VisitNumber" runat="server" Text='<%# Bind("IPS_VisitInformation_VisitNumber") %>' Width="130px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_LifeNumber" runat="server" Text='<%# Bind("PatientInformation_LifeNumber") %>' Width="130px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_AdmissionDate" runat="server" Text='<%# Bind("IPS_VisitInformation_DateOfAdmission") %>' Width="130px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_DischargeDate" runat="server" Text='<%# Bind("IPS_VisitInformation_DateOfDischarge") %>' Width="130px"></asp:Label>&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <strong>Report Number</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <strong>Category</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <strong>Type</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <strong>Sub Type</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <strong>Sub Sub Type</strong>
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_ReportNumber" runat="server" Text='<%# Bind("IPS_Infection_ReportNumber") %>' Width="130px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_Category" runat="server" Text='<%# Bind("IPS_Infection_Category_Name") %>' Width="130px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_Type" runat="server" Text='<%# Bind("IPS_Infection_Type_Name") %>' Width="130px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_SubType" runat="server" Text='<%# Bind("IPS_Infection_SubType_Name") %>' Width="130px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_SubSubType" runat="server" Text='<%# Bind("IPS_Infection_SubSubType_Name") %>' Width="130px"></asp:Label>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_InfectionHistory_List" runat="server" OnSelected="SqlDataSource_IPS_InfectionHistory_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Footer">
                  <tr>
                    <td style="text-align: center;">&nbsp;
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
