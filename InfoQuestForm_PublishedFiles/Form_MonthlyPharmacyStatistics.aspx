<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_MonthlyPharmacyStatistics" CodeBehind="Form_MonthlyPharmacyStatistics.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Monthly Pharmacy Statistics</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_MonthlyPharmacyStatistics.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_MPS" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_MPS" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_MPS" AssociatedUpdatePanelID="UpdatePanel_MPS">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_MPS" runat="server">
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
          <table id="TableMPSInfo" class="Table" style="width: 800px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_MPSInfoHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>
                      <strong>Facility:</strong>
                    </td>
                    <td>
                      <asp:Label ID="Label_MPSFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                      <strong>Month:</strong>
                    </td>
                    <td>
                      <asp:Label ID="Label_MPSMonth" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                      <strong>FY Period:</strong>
                    </td>
                    <td>
                      <asp:Label ID="Label_MPSFYPeriod" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableLinks" style="width: 800px;" runat="server">
            <tr>
              <td style="text-align: center;">
                <asp:Button ID="Button_GoToList" runat="server" Text="Back To List" CssClass="Controls_Button" OnClick="Button_GoToList_Click" />
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" style="width: 800px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_MPSHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_MPS_Form" runat="server" DataKeyNames="MPS_Id" CssClass="FormView" DataSourceID="SqlDataSource_MPS_Form" DefaultMode="Edit" OnItemCommand="FormView_MPS_Form_ItemCommand" OnDataBound="FormView_MPS_Form_DataBound" OnItemUpdating="FormView_MPS_Form_ItemUpdating">
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="3">
                          <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Revenue Actual
                        </td>
                        <td style="width: 485px; text-align: right;">R&nbsp;<asp:Label ID="Label_EditPharmacy_RevenueActual" runat="server" Text='<%# Bind("MPS_Pharmacy_RevenueActual","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_PharmacyRevenueActualInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Revenue Budget
                        </td>
                        <td style="width: 485px; text-align: right;">R&nbsp;<asp:Label ID="Label_EditPharmacy_RevenueBudget" runat="server" Text='<%# Bind("MPS_Pharmacy_RevenueBudget","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_PharmacyRevenueBudgetInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">COS Drugs Ethical
                        </td>
                        <td style="width: 485px; text-align: right;">R&nbsp;<asp:Label ID="Label_EditPharmacy_COSDrugsEthical" runat="server" Text='<%# Bind("MPS_Pharmacy_COSDrugsEthical","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_PharmacyCOSDrugsEthicalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">COS Drugs Surgical
                        </td>
                        <td style="width: 485px; text-align: right;">R&nbsp;<asp:Label ID="Label_EditPharmacy_COSDrugsSurgical" runat="server" Text='<%# Bind("MPS_Pharmacy_COSDrugsSurgical","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_PharmacyCOSDrugsSurgicalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Negative Stock
                        </td>
                        <td style="width: 485px; text-align: right;">R&nbsp;
                          <asp:TextBox ID="TextBox_EditPharmacy_NegativeStock" runat="server" Width="150px" Text='<%# Bind("MPS_Pharmacy_NegativeStock","{0:###0.00}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPharmacy_NegativeStock" runat="server" TargetControlID="TextBox_EditPharmacy_NegativeStock" FilterType="Custom, Numbers" ValidChars=".-">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditPharmacy_NegativeStock" runat="server" Text='<%# Bind("MPS_Pharmacy_NegativeStock","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_PharmacyNegativeStockInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Cost Reduction Opportunities Realized
                        </td>
                        <td style="width: 485px; text-align: right;">R&nbsp;
                          <asp:TextBox ID="TextBox_EditPharmacy_CostReductionOpportunitiesRealized" runat="server" Width="150px" Text='<%# Bind("MPS_Pharmacy_CostReductionOpportunitiesRealized","{0:###0.00}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditPharmacy_CostReductionOpportunitiesRealized" runat="server" TargetControlID="TextBox_EditPharmacy_CostReductionOpportunitiesRealized" FilterType="Custom, Numbers" ValidChars=".-">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:Label ID="Label_EditPharmacy_CostReductionOpportunitiesRealized" runat="server" Text='<%# Bind("MPS_Pharmacy_CostReductionOpportunitiesRealized","{0:#,##0.00}") %>'></asp:Label>
                        </td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_PharmacyCostReductionOpportunitiesRealizedInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">Form Information
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("MPS_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("MPS_ModifiedBy") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditBeingModified" runat="server" Text='<%# (bool)(Eval("MPS_BeingModified"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditBeingModifiedBy" runat="server" Text='<%# Bind("MPS_BeingModifiedBy") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_EditBeingModifiedDate" runat="server" Text='<%# Bind("MPS_BeingModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr class="Bottom">
                        <td colspan="3" style="text-align: right;">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="True" CommandName="Update" Text="Print Statistics" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="True" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Statistics" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                      <tr class="Bottom">
                        <td colspan="3" style="text-align: right;">
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back to List" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td style="width: 300px;">Revenue Actual</td>
                        <td style="width: 485px; text-align: right;">R&nbsp;<asp:Label ID="Label_ItemPharmacy_RevenueActual" runat="server" Text='<%# Bind("MPS_Pharmacy_RevenueActual","{0:#,##0.00}") %>'></asp:Label></td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_PharmacyRevenueActualInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Revenue Budget</td>
                        <td style="width: 485px; text-align: right;">R&nbsp;<asp:Label ID="Label_ItemPharmacy_RevenueBudget" runat="server" Text='<%# Bind("MPS_Pharmacy_RevenueBudget","{0:#,##0.00}") %>'></asp:Label></td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_PharmacyRevenueBudgetInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">COS Drugs Ethical</td>
                        <td style="width: 485px; text-align: right;">R&nbsp;<asp:Label ID="Label_ItemPharmacy_COSDrugsEthical" runat="server" Text='<%# Bind("MPS_Pharmacy_COSDrugsEthical","{0:#,##0.00}") %>'></asp:Label></td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_PharmacyCOSDrugsEthicalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">COS Drugs Surgical</td>
                        <td style="width: 485px; text-align: right;">R&nbsp;<asp:Label ID="Label_ItemPharmacy_COSDrugsSurgical" runat="server" Text='<%# Bind("MPS_Pharmacy_COSDrugsSurgical","{0:#,##0.00}") %>'></asp:Label></td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_PharmacyCOSDrugsSurgicalInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr>
                        <td style="width: 300px;">Negative Stock</td>
                        <td style="width: 485px; text-align: right;">R&nbsp;<asp:Label ID="Label_ItemPharmacy_NegativeStock" runat="server" Text='<%# Bind("MPS_Pharmacy_NegativeStock","{0:#,##0.00}") %>'></asp:Label></td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_PharmacyNegativeStockInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr class="FormView_AlternatingRowStyle">
                        <td style="width: 300px;">Cost Reduction Opportunities Realized</td>
                        <td style="width: 485px; text-align: right;">R&nbsp;<asp:Label ID="Label_ItemPharmacy_CostReductionOpportunitiesRealized" runat="server" Text='<%# Bind("MPS_Pharmacy_CostReductionOpportunitiesRealized","{0:#,##0.00}") %>'></asp:Label></td>
                        <td style="width: 15px;"><a href="#" class="tt">
                          <img height="11" alt="" src="App_Images/Information_16x16.png" style="border: 0px"><span class="tooltip"><span class="middle"><asp:Label ID="Label_PharmacyCostReductionOpportunitiesRealizedInfo" runat="server" Text="" CssClass="middle"></asp:Label></span></span></a></td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3" class="FormView_TableBodyHeader">Form Information
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("MPS_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("MPS_ModifiedBy") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemBeingModified" runat="server" Text='<%# (bool)(Eval("MPS_BeingModified"))?"Yes":"No" %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemBeingModifiedBy" runat="server" Text='<%# Bind("MPS_BeingModifiedBy") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td>Being Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemBeingModifiedDate" runat="server" Text='<%# Bind("MPS_BeingModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr class="Bottom">
                        <td colspan="3" style="text-align: right;">
                          <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Statistics" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr class="Bottom">
                        <td colspan="3" style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back to List" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_MPS_Form" runat="server" OnUpdated="SqlDataSource_MPS_Form_Updated"></asp:SqlDataSource>
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
