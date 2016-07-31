<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_IPS_VisitDiagnosis.aspx.cs" Inherits="InfoQuestForm.Form_IPS_VisitDiagnosis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Infection Prevention Surveillance - Visit Diagnosis</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_IPS_VisitDiagnosis.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_IPS_VisitDiagnosis" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_IPS_VisitDiagnosis" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_IPS_VisitDiagnosis" AssociatedUpdatePanelID="UpdatePanel_IPS_VisitDiagnosis">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_IPS_VisitDiagnosis" runat="server">
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
          <table id="TableVisitDiagnosis" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_VisitDiagnosisHeading" runat="server" Text=""></asp:Label>
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
                            <asp:Button ID="Button_TopUpdate" runat="server" Text="Update Visit Diagnosis" CssClass="Controls_Button" OnClick="Button_Update_OnClick" />
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
                      <asp:GridView ID="GridView_IPS_VisitDiagnosis" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource_IPS_VisitDiagnosis" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_VisitDiagnosis_PreRender" OnRowCreated="GridView_IPS_VisitDiagnosis_RowCreated" OnRowDataBound="GridView_IPS_VisitDiagnosis_RowDataBound">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_Update" runat="server" Text="Update Visit Diagnosis" CssClass="Controls_Button" OnClick="Button_Update_OnClick" />
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
                              <td colspan="2">No Visit Diagnosis
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
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:CheckBox ID="CheckBox_Selected" runat="server" />
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                              <asp:Label ID="Label_CodeType" runat="server" Text='<%# Bind("CodeType") %>' Width="90px"></asp:Label>
                              <asp:HiddenField ID="HiddenField_CodeType" runat="server" Value='<%# Bind("CodeType") %>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                              <asp:Label ID="Label_Code" runat="server" Text='<%# Bind("Code") %>' Width="90px"></asp:Label>
                              <asp:HiddenField ID="HiddenField_Code" runat="server" Value='<%# Bind("Code") %>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                              <asp:Label ID="Label_CodeDescription" runat="server" Text='<%# Bind("CodeDescription") %>' Width="640px"></asp:Label>
                              <asp:HiddenField ID="HiddenField_CodeDescription" runat="server" Value='<%# Bind("CodeDescription") %>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:ObjectDataSource ID="ObjectDataSource_IPS_VisitDiagnosis" runat="server" OnSelected="ObjectDataSource_IPS_VisitDiagnosis_Selected"></asp:ObjectDataSource>
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
