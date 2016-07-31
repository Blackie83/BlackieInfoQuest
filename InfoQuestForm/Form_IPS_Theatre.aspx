<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_IPS_Theatre.aspx.cs" Inherits="InfoQuestForm.Form_IPS_Theatre" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Infection Prevention Surveillance - Theatre</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_IPS_Theatre.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_IPS_Theatre" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_IPS_Theatre" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_IPS_Theatre" AssociatedUpdatePanelID="UpdatePanel_IPS_Theatre">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_IPS_Theatre" runat="server">
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
          <table id="TableTheatre" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_TheatreHeading" runat="server" Text=""></asp:Label>
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
                            <asp:Button ID="Button_TopUpdate" runat="server" Text="Update Visit History" CssClass="Controls_Button" OnClick="Button_Update_OnClick" />
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
                      <asp:GridView ID="GridView_IPS_Theatre" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource_IPS_Theatre" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_Theatre_PreRender" OnRowCreated="GridView_IPS_Theatre_RowCreated" OnRowDataBound="GridView_IPS_Theatre_RowDataBound">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_Update" runat="server" Text="Update Visit History" CssClass="Controls_Button" OnClick="Button_Update_OnClick" />
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
                              <td colspan="2">No Visit History
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
                              <table class="Table_Body">
                                <tr>
                                  <td style="width: 50px;" class="Table_TemplateField" rowspan="8">
                                    <asp:CheckBox ID="CheckBox_Selected" runat="server" /></td>
                                  <td style="width: 170px;" class="Table_TemplateField"><strong>Facility</strong></td>
                                  <td style="width: 85px;" class="Table_TemplateField"><strong>Visit Number</strong></td>
                                  <td style="width: 85px;" class="Table_TemplateField"><strong>Type</strong></td>
                                  <td style="width: 170px;" class="Table_TemplateField"><strong>Admission Date</strong></td>
                                  <td style="width: 170px;" class="Table_TemplateField"><strong>Discharge Date</strong></td>
                                  <td style="width: 170px;" class="Table_TemplateField"><strong>Final Diagnosis</strong></td>
                                </tr>
                                <tr>
                                  <td style="width: 170px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_Facility" runat="server" Text='<%# GetFacilityName(Eval("FacilityCode")) %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_FacilityCode" runat="server" Value='<%# Bind("FacilityCode") %>' />
                                  </td>
                                  <td style="width: 85px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_VisitNumber" runat="server" Text='<%# Bind("VisitNumber") %>' Width="75px"></asp:Label><asp:HiddenField ID="HiddenField_VisitNumber" runat="server" Value='<%# Bind("VisitNumber") %>' />
                                  </td>
                                  <td style="width: 85px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_ServiceCategory" runat="server" Text='<%# GetVisitType(Eval("ServiceCategory"), Eval("VisitType")) %>' Width="75px"></asp:Label>
                                    <asp:HiddenField ID="HiddenField_ServiceCategory" runat="server" Value='<%# Bind("ServiceCategory") %>' />
                                    <asp:HiddenField ID="HiddenField_VisitType" runat="server" Value='<%# Bind("VisitType") %>' />
                                  </td>
                                  <td style="width: 170px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_AdmissionDate" runat="server" Text='<%# Bind("AdmissionDate") %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_AdmissionDate" runat="server" Value='<%# Bind("AdmissionDate") %>' />
                                  </td>
                                  <td style="width: 170px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_DischargeDate" runat="server" Text='<%# Bind("DischargeDate") %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_DischargeDate" runat="server" Value='<%# Bind("DischargeDate") %>' />
                                  </td>
                                  <td style="width: 170px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_FinalDiagnosis" runat="server" Text='<%# GetFinalDiagnosis(Eval("FinalDiagnosisCode"), Eval("FinalDiagnosisDescription")) %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_FinalDiagnosisCode" runat="server" Value='<%# Bind("FinalDiagnosisCode") %>' />
                                    <asp:HiddenField ID="HiddenField_FinalDiagnosisDescription" runat="server" Value='<%# Bind("FinalDiagnosisDescription") %>' />
                                  </td>
                                </tr>
                                <tr id="SurgicalRow1" runat="server">
                                  <td style="width: 170px;" class="Table_TemplateField"><strong>Theatre</strong></td>
                                  <td style="width: 170px;" class="Table_TemplateField" colspan="2"><strong>Theatre Time</strong></td>
                                  <td style="width: 170px;" class="Table_TemplateField"><strong>Procedure Date</strong></td>
                                  <td style="width: 170px;" class="Table_TemplateField"><strong>Procedure</strong></td>
                                  <td style="width: 170px;" class="Table_TemplateField"><strong>Theatre Invoice</strong></td>
                                </tr>
                                <tr id="SurgicalRow2" runat="server">
                                  <td style="width: 170px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_Theatre" runat="server" Text='<%# Bind("Theatre") %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_Theatre" runat="server" Value='<%# Bind("Theatre") %>' />
                                  </td>
                                  <td style="width: 170px;" class="Table_TemplateField" colspan="2">
                                    <asp:Label ID="Label_TheatreTime" runat="server" Text='<%# Bind("TheatreTime") %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_TheatreTime" runat="server" Value='<%# Bind("TheatreTime") %>' />
                                  </td>
                                  <td style="width: 170px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_ProcedureDate" runat="server" Text='<%# Bind("ProcedureDate") %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_ProcedureDate" runat="server" Value='<%# Bind("ProcedureDate") %>' />
                                  </td>
                                  <td style="width: 170px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_Procedure" runat="server" Text='<%# GetProcedure(Eval("ProcedureCode"), Eval("ProcedureDescription")) %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_ProcedureCode" runat="server" Value='<%# Bind("ProcedureCode") %>' />
                                    <asp:HiddenField ID="HiddenField_ProcedureDescription" runat="server" Value='<%# Bind("ProcedureDescription") %>' />
                                  </td>
                                  <td style="width: 170px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_TheatreInvoice" runat="server" Text='<%# Bind("TheatreInvoice") %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_TheatreInvoice" runat="server" Value='<%# Bind("TheatreInvoice") %>' />
                                  </td>
                                </tr>
                                <tr id="SurgicalRow3" runat="server">
                                  <td style="width: 170px;" class="Table_TemplateField"><strong>Surgeon</strong></td>
                                  <td style="width: 170px;" class="Table_TemplateField" colspan="2"><strong>Anaesthetist</strong></td>
                                  <td style="width: 170px;" class="Table_TemplateField"><strong>Assistant</strong></td>
                                  <td style="width: 170px;" class="Table_TemplateField"><strong>Wound Category</strong></td>
                                  <td style="width: 170px;" class="Table_TemplateField"><strong>Scrub Nurse</strong></td>
                                </tr>
                                <tr id="SurgicalRow4" runat="server">
                                  <td style="width: 170px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_Surgeon" runat="server" Text='<%# Bind("Surgeon") %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_Surgeon" runat="server" Value='<%# Bind("Surgeon") %>' />
                                  </td>
                                  <td style="width: 170px;" class="Table_TemplateField" colspan="2">
                                    <asp:Label ID="Label_Anaesthetist" runat="server" Text='<%# Bind("Anaesthetist") %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_Anaesthetist" runat="server" Value='<%# Bind("Anaesthetist") %>' />
                                  </td>
                                  <td style="width: 170px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_Assistant" runat="server" Text='<%# Bind("Assistant") %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_Assistant" runat="server" Value='<%# Bind("Assistant") %>' />
                                  </td>
                                  <td style="width: 170px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_WoundCategory" runat="server" Text='<%# Bind("WoundCategory") %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_WoundCategory" runat="server" Value='<%# Bind("WoundCategory") %>' />
                                  </td>
                                  <td style="width: 170px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_ScrubNurse" runat="server" Text='<%# Bind("ScrubNurse") %>' Width="160px"></asp:Label><asp:HiddenField ID="HiddenField_ScrubNurse" runat="server" Value='<%# Bind("ScrubNurse") %>' />
                                  </td>
                                </tr>
                                <tr id="InfectionHistory1" runat="server">
                                  <td style="width: 850px; text-align: center;" class="Table_TemplateField" colspan="6">
                                    <asp:Button ID="Button_VisitInfectionHistory" runat="server" Text="Visit Infection History" CssClass="Controls_Button" OnClick="Button_VisitInfectionHistory_OnClick" />&nbsp;
                                  </td>
                                </tr>
                                <tr id="InfectionHistory2" runat="server">
                                  <td style="width: 850px; text-align: center;" class="Table_TemplateField" colspan="6"><strong>Visit Infection History</strong>
                                  </td>
                                </tr>
                                <tr id="InfectionHistory3" runat="server">
                                  <td style="width: 850px; text-align: left; padding: 0px; border: none;" class="Table_TemplateField" colspan="6">
                                    <asp:GridView ID="GridView_IPS_Theatre_InfectionHistory_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_Theatre_InfectionHistory_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_Theatre_InfectionHistory_List_PreRender" OnRowCreated="GridView_IPS_Theatre_InfectionHistory_List_RowCreated">
                                      <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                                      <EmptyDataTemplate>
                                        <table style="width: 100%; padding: 0px; border: none;">
                                          <tr>
                                            <td style="padding: 3px; vertical-align: top; white-space: nowrap; border: none;">No Infection History Captured
                                            </td>
                                          </tr>
                                        </table>
                                      </EmptyDataTemplate>
                                      <Columns>
                                        <asp:BoundField DataField="IPS_Infection_ReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="IPS_Infection_ReportNumber" ItemStyle-Width="163px" ItemStyle-CssClass="GridView_Columns" />
                                        <asp:BoundField DataField="IPS_Infection_Site_Name" HeaderText="Site" ReadOnly="True" SortExpression="IPS_Infection_Site_Name" ItemStyle-Width="166px" ItemStyle-CssClass="GridView_Columns" />
                                        <asp:BoundField DataField="IPS_Infection_Category_Name" HeaderText="Category" ReadOnly="True" SortExpression="IPS_Infection_Category_Name" ItemStyle-Width="164px" ItemStyle-CssClass="GridView_Columns" />
                                        <asp:BoundField DataField="IPS_Infection_Type_Name" HeaderText="Type" ReadOnly="True" SortExpression="IPS_Infection_Type_Name" ItemStyle-Width="108px" ItemStyle-CssClass="GridView_Columns" />
                                        <asp:BoundField DataField="IPS_Infection_SubType_Name" HeaderText="Sub Type" ReadOnly="True" SortExpression="IPS_Infection_SubType_Name" ItemStyle-Width="106px" ItemStyle-CssClass="GridView_Columns" />
                                        <asp:BoundField DataField="IPS_Infection_SubSubType_Name" HeaderText="Sub Sub Type" ReadOnly="True" SortExpression="IPS_Infection_SubSubType_Name" ItemStyle-Width="106px" ItemStyle-CssClass="GridView_Columns" />
                                      </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource_IPS_Theatre_InfectionHistory_List" runat="server"></asp:SqlDataSource>
                                  </td>
                                </tr>
                              </table>
                            </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:ObjectDataSource ID="ObjectDataSource_IPS_Theatre" runat="server" OnSelected="ObjectDataSource_IPS_Theatre_Selected"></asp:ObjectDataSource>
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
