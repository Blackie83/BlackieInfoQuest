<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.InfoQuest_Print" CodeBehind="InfoQuest_Print.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Print</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body style="margin: 2px;">
  <form id="form_Print" runat="server">
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Print" runat="server" ScriptMode="Release">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Print" AssociatedUpdatePanelID="UpdatePanel_Print">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Print" runat="server">
        <ContentTemplate>
          <table style="width: 724px;">
            <tr>
              <td style="padding: 0px;">
                <Header:HeaderText ID="HeaderText_Page" runat="server" />
              </td>
            </tr>
          </table>
          <table id="TableNavigation_Form_ClinicalPracticeAudit" runat="server" style="width: 730px;" visible="false">
            <tr>
              <td style="background-color: #003768; padding: 6px 20px 6px 20px;">
                <asp:LinkButton ID="LinkButton_Navigation_Form_ClinicalPracticeAudit" runat="server" OnClick="LinkButton_Navigation_Form_ClinicalPracticeAudit_Click" Text="Audit" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_ClinicalPracticeAudit_All" runat="server" OnClick="LinkButton_Navigation_Form_ClinicalPracticeAudit_All_Click" Text="All Audits" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
              </td>
            </tr>
          </table>
          <table id="TableNavigation_Form_BundleCompliance" runat="server" style="width: 730px;" visible="false">
            <tr>
              <td style="background-color: #003768; padding: 6px 20px 6px 20px;">
                <asp:LinkButton ID="LinkButton_Navigation_Form_BundleCompliance" runat="server" OnClick="LinkButton_Navigation_Form_BundleCompliance_Click" Text="Bundle" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_BundleCompliance_All" runat="server" OnClick="LinkButton_Navigation_Form_BundleCompliance_All_Click" Text="All Bundles" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
              </td>
            </tr>
          </table>
          <table id="TableNavigation_Form_RehabBundleCompliance" runat="server" style="width: 730px;" visible="false">
            <tr>
              <td style="background-color: #003768; padding: 6px 20px 6px 20px;">
                <asp:LinkButton ID="LinkButton_Navigation_Form_RehabBundleCompliance" runat="server" OnClick="LinkButton_Navigation_Form_RehabBundleCompliance_Click" Text="Bundle" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_RehabBundleCompliance_All" runat="server" OnClick="LinkButton_Navigation_Form_RehabBundleCompliance_All_Click" Text="All Bundles" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
              </td>
            </tr>
          </table>
          <table id="TableNavigation_Form_MedicationBundleCompliance" runat="server" style="width: 730px;" visible="false">
            <tr>
              <td style="background-color: #003768; padding: 6px 20px 6px 20px;">
                <asp:LinkButton ID="LinkButton_Navigation_Form_MedicationBundleCompliance" runat="server" OnClick="LinkButton_Navigation_Form_MedicationBundleCompliance_Click" Text="Bundle" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_MedicationBundleCompliance_All" runat="server" OnClick="LinkButton_Navigation_Form_MedicationBundleCompliance_All_Click" Text="All Bundles" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
              </td>
            </tr>
          </table>
          <table id="TableNavigation_Form_AntimicrobialStewardshipIntervention" runat="server" style="width: 730px;" visible="false">
            <tr>
              <td style="background-color: #003768; padding: 6px 20px 6px 20px;">
                <asp:LinkButton ID="LinkButton_Navigation_Form_AntimicrobialStewardshipIntervention" runat="server" OnClick="LinkButton_Navigation_Form_AntimicrobialStewardshipIntervention_Click" Text="Intervention" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_AntimicrobialStewardshipIntervention_All" runat="server" OnClick="LinkButton_Navigation_Form_AntimicrobialStewardshipIntervention_All_Click" Text="All Interventions" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
              </td>
            </tr>
          </table>
          <table id="TableNavigation_Form_PharmacyClinicalMetrics_TherapeuticIntervention" runat="server" style="width: 730px;" visible="false">
            <tr>
              <td style="background-color: #003768; padding: 6px 20px 6px 20px;">
                <asp:LinkButton ID="LinkButton_Navigation_Form_PharmacyClinicalMetrics_TherapeuticIntervention" runat="server" OnClick="LinkButton_Navigation_Form_PharmacyClinicalMetrics_TherapeuticIntervention_Click" Text="Intervention" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_PharmacyClinicalMetrics_TherapeuticIntervention_All" runat="server" OnClick="LinkButton_Navigation_Form_PharmacyClinicalMetrics_TherapeuticIntervention_All_Click" Text="All Interventions" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
              </td>
            </tr>
          </table>
          <table id="TableNavigation_Form_PharmacyClinicalMetrics_PharmacistTime" runat="server" style="width: 730px;" visible="false">
            <tr>
              <td style="background-color: #003768; padding: 6px 20px 6px 20px;">
                <asp:LinkButton ID="LinkButton_Navigation_Form_PharmacyClinicalMetrics_PharmacistTime" runat="server" OnClick="LinkButton_Navigation_Form_PharmacyClinicalMetrics_PharmacistTime_Click" Text="Intervention" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_PharmacyClinicalMetrics_PharmacistTime_All" runat="server" OnClick="LinkButton_Navigation_Form_PharmacyClinicalMetrics_PharmacistTime_All_Click" Text="All Interventions" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
              </td>
            </tr>
          </table>
          <table id="TableNavigation_Form_HAI" runat="server" style="width: 730px;" visible="false">
            <tr>
              <td style="background-color: #003768; padding: 6px 20px 6px 20px;">
                <asp:LinkButton ID="LinkButton_Navigation_Form_HAI_All" runat="server" OnClick="LinkButton_Navigation_Form_HAI_All_Click" Text="All" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_HAI_PatientSection" runat="server" OnClick="LinkButton_Navigation_Form_HAI_PatientSection_Click" Text="Patient Section" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_HAI_PatientSection_Extra" runat="server" OnClick="LinkButton_Navigation_Form_HAI_PatientSection_Extra_Click" Text="Patient Section Extra" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_HAI_PatientSite" runat="server" OnClick="LinkButton_Navigation_Form_HAI_PatientSite_Click" Text="Patient Site" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_HAI_InvestigationSection" runat="server" OnClick="LinkButton_Navigation_Form_HAI_InvestigationSection_Click" Text="Investigation Section" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton>
              </td>
            </tr>
          </table>
          <table id="TableNavigation_Form_IPS" runat="server" style="width: 730px;" visible="false">
            <tr>
              <td style="background-color: #003768; padding: 6px 20px 6px 20px;">
                <asp:LinkButton ID="LinkButton_Navigation_Form_IPS" runat="server" OnClick="LinkButton_Navigation_Form_IPS_Click" Text="All" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_IPS_InfectionDetail" runat="server" OnClick="LinkButton_Navigation_Form_IPS_InfectionDetail_Click" Text="Infection Detail" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_IPS_InfectionDetail_Extra" runat="server" OnClick="LinkButton_Navigation_Form_IPS_InfectionDetail_Extra_Click" Text="Infection Detail Extra" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton><span style="padding: 10px;"></span>
                <asp:LinkButton ID="LinkButton_Navigation_Form_IPS_Investigation" runat="server" OnClick="LinkButton_Navigation_Form_IPS_Investigation_Click" Text="Investigation" ForeColor="White" Font-Names="Arial, Verdana" Font-Size="12px" Font-Bold="true" Font-Underline="false"></asp:LinkButton>
              </td>
            </tr>
          </table>
          <table id="TableParameters_Form_HAI" runat="server" style="width: 730px;" visible="false">
            <tr>
              <td style="padding: 0px;">
                <table class="Table_Body">
                  <tr>
                    <td colspan="3">Select HAI Site to display printing page for
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 50px;">HAI Site
                    </td>
                    <td style="width: 566px;">
                      <asp:DropDownList ID="DropDownList_Parameter_Form_HAI_Site" runat="server" DataSourceID="SqlDataSource_Parameter_Form_HAI_Site" DataTextField="iSiteNumber" DataValueField="iSiteNumber" CssClass="Controls_DropDownList">
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Parameter_Form_HAI_Site" runat="server"></asp:SqlDataSource>
                    </td>
                    <td style="width: 100px;">
                      <asp:Button ID="Button_Parameter_Form_HAI_Site" runat="server" Text="View HAI Site" CssClass="Controls_Button" OnClick="Button_Parameter_Form_HAI_Site_Click" />
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <table id="TableParameters_Form_IPS" runat="server" style="width: 730px;" visible="false">
            <tr>
              <td style="padding: 0px;">
                <table class="Table_Body">
                  <tr>
                    <td colspan="3">Select Infection to display printing page for
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 50px;">Infection
                    </td>
                    <td style="width: 566px;">
                      <asp:DropDownList ID="DropDownList_Parameter_Form_IPS_Infection" runat="server" DataSourceID="SqlDataSource_Parameter_Form_IPS_Infection" DataTextField="IPS_Infection_ReportNumber" DataValueField="IPS_Infection_Id" CssClass="Controls_DropDownList" OnDataBound="DropDownList_Parameter_Form_IPS_Infection_DataBound">
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Parameter_Form_IPS_Infection" runat="server"></asp:SqlDataSource>
                    </td>
                    <td style="width: 100px;">
                      <asp:Button ID="Button_Parameter_Form_IPS_Infection" runat="server" Text="View Infection" CssClass="Controls_Button" OnClick="Button_Parameter_Form_IPS_Infection_Click" />
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <table id="TablePrint" runat="server" style="width: 728px;">
            <tr>
              <td style="padding: 0px;">
                <rsweb:ReportViewer ID="ReportViewer_Print" runat="server" Width="100%" Height="500px" Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" BorderWidth="1px" BorderColor="#dfdfdf" BorderStyle="Solid">
                </rsweb:ReportViewer>
              </td>
            </tr>
          </table>
          <table style="width: 724px;">
            <tr>
              <td style="padding: 0px;">
                <Footer:FooterText ID="FooterText_Page" runat="server" />
              </td>
            </tr>
          </table>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </form>
</body>
</html>
