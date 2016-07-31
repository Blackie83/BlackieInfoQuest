<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_IPS_HAI.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="InfoQuestForm.Form_IPS_HAI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Infection Prevention Surveillance - HAI</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_IPS_HAI.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_IPS_HAI" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_IPS_HAI" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_IPS_HAI" AssociatedUpdatePanelID="UpdatePanel_IPS_HAI">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_IPS_HAI" runat="server">
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
                  <tr id="RowInfoVisible" runat="server">
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
          <a id="CurrentHAI"></a>
          <asp:LinkButton ID="LinkButton_CurrentHAI" runat="server"></asp:LinkButton>
          <table id="TableCurrentHAI" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_CurrentHAIHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_IPS_HAI_Form" runat="server" DataKeyNames="IPS_HAI_Id" CssClass="FormView" DataSourceID="SqlDataSource_IPS_HAI_Form" DefaultMode="Edit" OnDataBound="FormView_IPS_HAI_Form_DataBound" OnItemUpdating="FormView_IPS_HAI_Form_ItemUpdating">
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormInfectionUnit">Unit
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditInfectionUnitId" runat="server" DataSourceID="SqlDataSource_IPS_EditInfectionUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormInfectionSummary">Infection Summary
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInfectionSummary" runat="server" TextMode="MultiLine" Rows="4" Width="700px" Text="" CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditInfectionSummary" runat="server" TargetControlID="TextBox_EditInfectionSummary" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Predisposing Conditions and Contributing Factors
                          <asp:HiddenField ID="HiddenField_EditPredisposingConditionTotalRecords" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" style="padding: 0px;">
                          <asp:GridView ID="GridView_IPS_HAI_EditPredisposingConditionList" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_HAI_EditPredisposingConditionList" CssClass="GridView" AllowPaging="False" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_HAI_EditPredisposingConditionList_PreRender" OnRowDataBound="GridView_IPS_HAI_EditPredisposingConditionList_RowDataBound">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>&nbsp;
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
                                  <td colspan="2">No Predisposing Conditions and Contributing Factors
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="" ItemStyle-Width="40px">
                                <ItemTemplate>
                                  <asp:CheckBox ID="CheckBox_Selected" runat="server" />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Condition" ItemStyle-Width="300px">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ConditionName" runat="server" Text='<%# Bind("ListItem_Name") %>' Width="280px"></asp:Label>
                                  <asp:HiddenField ID="HiddenField_ConditionList" runat="server" Value='<%# Bind("ListItem_Id") %>' />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Description" ItemStyle-Width="560px">
                                <ItemTemplate>
                                  <asp:TextBox ID="TextBox_Description" runat="server" Width="510px" CssClass="Controls_TextBox"></asp:TextBox>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Laboratory Results
                          <asp:HiddenField ID="HiddenField_EditLabReportsTotalRecords" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" style="padding: 0px;">
                          <asp:GridView ID="GridView_IPS_HAI_EditLabReportsList" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_HAI_EditLabReportsList" CssClass="GridView" AllowPaging="False" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_HAI_EditLabReportsList_PreRender" OnRowDataBound="GridView_IPS_HAI_EditLabReportsList_RowDataBound">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>&nbsp;
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
                                  <td colspan="2">No Laboratory Results
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="" ItemStyle-Width="40px">
                                <ItemTemplate>
                                  <asp:CheckBox ID="CheckBox_Selected" runat="server" />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                  <asp:Label ID="Label_SpecimenStatus" runat="server" Text='<%# Bind("SpecimenStatus") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                  <asp:Label ID="Label_SpecimenDate" runat="server" Text='<%# Bind("SpecimenDate") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Time">
                                <ItemTemplate>
                                  <asp:Label ID="Label_SpecimenTime" runat="server" Text='<%# Bind("SpecimenTime") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                  <asp:Label ID="Label_SpecimenType" runat="server" Text='<%# Bind("SpecimenType") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Laboratory Number">
                                <ItemTemplate>
                                  <asp:Label ID="Label_SpecimenResultLabNumber" runat="server" Text='<%# Bind("SpecimenResultLabNumber") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Organism">
                                <ItemTemplate>
                                  <asp:Label ID="Label_Organism" runat="server" Text='<%# Bind("Organism") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Resistance">
                                <ItemTemplate>
                                  <asp:Label ID="Label_OrganismResistance" runat="server" Text='<%# Bind("OrganismResistance") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Organism Resistance">
                                <ItemTemplate>
                                  <asp:Label ID="Label_OrganismResistanceName" runat="server" Text='<%# Bind("OrganismResistanceName") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Bundle Compliance
                        </td>
                      </tr>
                      <tr id="BundleComplianceQA1" runat="server">
                        <td style="width: 180px;" id="FormBundleComplianceDays">Number of
                          <asp:Label ID="Label_EditInfectionTypeName" runat="server"></asp:Label>
                          Days
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditBundleComplianceDays" runat="server" Width="100px" Text='<%# Bind("IPS_HAI_BundleCompliance_Days") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditBundleComplianceDays" runat="server" TargetControlID="TextBox_EditBundleComplianceDays" FilterType="Numbers">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr id="BundleComplianceQA2" runat="server">
                        <td colspan="4"><strong>Bundles</strong>
                        </td>
                      </tr>
                      <tr id="BundleComplianceQA3" runat="server">
                        <td colspan="4" style="padding: 0px;">
                          <asp:PlaceHolder ID="PlaceHolder_EditBundleComplianceQA" runat="server" Visible="false"></asp:PlaceHolder>
                        </td>
                      </tr>
                      <tr id="BundleComplianceQA4" runat="server">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4"><strong>Related High Risk Procedures</strong>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">TPN
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditBundleComplianceTPN" runat="server" Checked='<%# Bind("IPS_HAI_BundleCompliance_TPN") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Enteral Feeding
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditBundleComplianceEnteralFeeding" runat="server" Checked='<%# Bind("IPS_HAI_BundleCompliance_EnteralFeeding") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Investigation
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormInvestigationDate">Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigationDate" runat="server" Width="75px" Text='<%# Bind("IPS_HAI_Investigation_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditInvestigationDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditInvestigationDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditInvestigationDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditInvestigationDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditInvestigationDate" runat="server" TargetControlID="TextBox_EditInvestigationDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormInvestigationInvestigatorName">Investigator Name
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigationInvestigatorName" runat="server" Width="700px" Text='<%# Bind("IPS_HAI_Investigation_InvestigatorName") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormInvestigationInvestigatorDesignation">Investigator Designation
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigationInvestigatorDesignation" runat="server" Width="700px" Text='<%# Bind("IPS_HAI_Investigation_InvestigatorDesignation") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormInvestigationIPCName">IPC / S Name
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigationIPCName" runat="server" Width="700px" Text='<%# Bind("IPS_HAI_Investigation_IPCName") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormInvestigationTeamMembers">Team Members
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInvestigationTeamMembers" runat="server" TextMode="MultiLine" Rows="4" Width="700px" Text='<%# Bind("IPS_HAI_Investigation_TeamMembers") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Precautionary / Isolation Measures
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" style="padding: 0px;">
                          <asp:CheckBoxList ID="CheckBoxList_EditMeasureList" runat="server" AppendDataBoundItems="true" CssClass="Controls_CheckBoxList" DataSourceID="SqlDataSource_IPS_HAI_EditMeasureList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" OnDataBound="CheckBoxList_EditMeasureList_DataBound">
                          </asp:CheckBoxList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Investigation Completed
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormInvestigationCompleted">Completed
                        </td>
                        <td style="width: 270px;">
                          <asp:CheckBox ID="CheckBox_EditInvestigationCompleted" runat="server" Checked='<%# Bind("IPS_HAI_Investigation_Completed") %>' />
                        </td>
                        <td style="width: 180px;" id="FormInvestigationCompletedDate">Completed Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditInvestigationCompletedDate" runat="server" Text='<%# Bind("IPS_HAI_Investigation_CompletedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Created Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("IPS_HAI_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Created By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("IPS_HAI_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Modified Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("IPS_HAI_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Modified By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("IPS_HAI_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_EditInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_EditHAIInfectionHome_OnClick" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update HAI Investigation" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Unit
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionUnit" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Infection Summary
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionSummary" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Predisposing Conditions and Contributing Factors
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" style="padding: 0px;">
                          <asp:GridView ID="GridView_IPS_HAI_ItemPredisposingConditionList" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_HAI_ItemPredisposingConditionList" CssClass="GridView" AllowPaging="False" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_HAI_ItemPredisposingConditionList_PreRender">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </PagerTemplate>
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              No Predisposing Conditions and Contributing Factors
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="ListItem_Name" HeaderText="Condition" ReadOnly="True" SortExpression="ListItem_Name" ItemStyle-Width="300px" />
                              <asp:BoundField DataField="IPS_HAI_PredisposingCondition_Description" HeaderText="Description" ReadOnly="True" SortExpression="IPS_HAI_PredisposingCondition_Description" ItemStyle-Width="600px" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Laboratory Results
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" style="padding: 0px;">
                          <asp:GridView ID="GridView_IPS_HAI_ItemLabReportsList" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_HAI_ItemLabReportsList" CssClass="GridView" AllowPaging="False" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_HAI_ItemLabReportsList_PreRender">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </PagerTemplate>
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              No Laboratory Results
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="IPS_HAI_LabReports_SpecimenStatus" HeaderText="Status" ReadOnly="True" SortExpression="IPS_HAI_LabReports_SpecimenStatus" />
                              <asp:BoundField DataField="IPS_HAI_LabReports_SpecimenDate" HeaderText="Date" ReadOnly="True" SortExpression="IPS_HAI_LabReports_SpecimenDate" />
                              <asp:BoundField DataField="IPS_HAI_LabReports_SpecimenTime" HeaderText="Time" ReadOnly="True" SortExpression="IPS_HAI_LabReports_SpecimenTime" />
                              <asp:BoundField DataField="IPS_HAI_LabReports_SpecimenType" HeaderText="Type" ReadOnly="True" SortExpression="IPS_HAI_LabReports_SpecimenType" />
                              <asp:BoundField DataField="IPS_HAI_LabReports_SpecimenResultLabNumber" HeaderText="Laboratory Number" ReadOnly="True" SortExpression="IPS_HAI_LabReports_SpecimenResultLabNumber" />
                              <asp:BoundField DataField="IPS_HAI_LabReports_Organism" HeaderText="Organism" ReadOnly="True" SortExpression="IPS_HAI_LabReports_Organism" />
                              <asp:BoundField DataField="IPS_HAI_LabReports_OrganismResistance" HeaderText="Resistance" ReadOnly="True" SortExpression="IPS_HAI_LabReports_OrganismResistance" />
                              <asp:BoundField DataField="IPS_HAI_LabReports_OrganismResistanceName" HeaderText="Organism Resistance" ReadOnly="True" SortExpression="IPS_HAI_LabReports_OrganismResistanceName" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Bundle Compliance
                        </td>
                      </tr>
                      <tr id="BundleComplianceQA1" runat="server">
                        <td style="width: 180px;">Number of
                          <asp:Label ID="Label_ItemInfectionTypeName" runat="server"></asp:Label>
                          Days
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemBundleComplianceDays" runat="server" Text='<%# Bind("IPS_HAI_BundleCompliance_Days") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="BundleComplianceQA2" runat="server">
                        <td colspan="4"><strong>Bundles</strong>
                        </td>
                      </tr>
                      <tr id="BundleComplianceQA3" runat="server">
                        <td colspan="4" style="padding: 0px; border: none;">
                          <asp:GridView ID="GridView_IPS_HAI_ItemBundleComplianceQAList" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_HAI_ItemBundleComplianceQAList" CssClass="GridView" AllowPaging="False" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="False" ShowHeader="False" OnPreRender="GridView_IPS_HAI_ItemBundleComplianceQAList_PreRender">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </PagerTemplate>
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              No Bundle Compliance
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="Answer" HeaderText="" ReadOnly="True" SortExpression="Answer" ItemStyle-Width="30px" />
                              <asp:BoundField DataField="Question" HeaderText="" ReadOnly="True" SortExpression="Question" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr id="BundleComplianceQA4" runat="server">
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4"><strong>Related High Risk Procedures</strong>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">TPN
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemBundleComplianceTPN" runat="server" Text='<%# (bool)(Eval("IPS_HAI_BundleCompliance_TPN"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Enteral Feeding
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemBundleComplianceEnteralFeeding" runat="server" Text='<%# (bool)(Eval("IPS_HAI_BundleCompliance_EnteralFeeding"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Investigation
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationDate" runat="server" Text='<%# Bind("IPS_HAI_Investigation_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Investigator Name
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationInvestigatorName" runat="server" Text='<%# Bind("IPS_HAI_Investigation_InvestigatorName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Investigator Designation
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationInvestigatorDesignation" runat="server" Text='<%# Bind("IPS_HAI_Investigation_InvestigatorDesignation") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">IPC / S Name
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationIPCName" runat="server" Text='<%# Bind("IPS_HAI_Investigation_IPCName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Team Members
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemInvestigationTeamMembers" runat="server" Text='<%# Bind("IPS_HAI_Investigation_TeamMembers") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Precautionary / Isolation Measures
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" style="padding: 0px;">
                          <asp:GridView ID="GridView_IPS_HAI_ItemMeasureList" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_HAI_ItemMeasureList" CssClass="GridView" AllowPaging="False" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_HAI_ItemMeasureList_PreRender">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </PagerTemplate>
                            <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              No Precautionary / Isolation Measures
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="ListItem_Name" HeaderText="Measures" ReadOnly="True" SortExpression="ListItem_Name" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4" class="FormView_TableBodyHeader">Investigation Completed
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormInvestigationCompleted">Completed
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemInvestigationCompleted" runat="server" Text='<%# (bool)(Eval("IPS_HAI_Investigation_Completed"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;" id="FormInvestigationCompletedDate">Completed Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemInvestigationCompletedDate" runat="server" Text='<%# Bind("IPS_HAI_Investigation_CompletedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Created Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("IPS_HAI_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Created By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("IPS_HAI_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Modified Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("IPS_HAI_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Modified By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("IPS_HAI_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_ItemInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_ItemHAIInfectionHome_OnClick" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditInfectionUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_HAI_EditPredisposingConditionList" runat="server" OnSelected="SqlDataSource_IPS_HAI_EditPredisposingConditionList_Selected"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_HAI_EditLabReportsList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_HAI_EditMeasureList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_HAI_ItemPredisposingConditionList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_HAI_ItemLabReportsList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_HAI_ItemBundleComplianceQAList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_HAI_ItemMeasureList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_HAI_Form" runat="server" OnUpdated="SqlDataSource_IPS_HAI_Form_Updated"></asp:SqlDataSource>
              </td>
            </tr>
          </table>
          <div id="DivCurrentInfectionComplete" runat="server" style="height: 40px; width: 900px; text-align: center;">
            &nbsp;<hr style="height: 5px; width: 80%; background-color: #b0262e; border: none;" />
          </div>
          <asp:LinkButton ID="LinkButton_CurrentInfectionComplete" runat="server"></asp:LinkButton>
          <table id="TableCurrentInfectionComplete" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_CurrentInfectionCompleteHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Footer">
                  <tr>
                    <td>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td colspan="8">
                      <asp:Label ID="Label_InvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                      <asp:Label ID="Label_ConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                      <asp:HiddenField ID="HiddenField_CurrentInfectionComplete_ModifiedDate" runat="server" />
                      <asp:HiddenField ID="HiddenField_CurrentInfectionComplete_ModifiedBy" runat="server" />
                      <asp:HiddenField ID="HiddenField_CurrentInfectionComplete_History" runat="server" />
                      <asp:HiddenField ID="HiddenField_CurrentInfectionComplete_HAIModifiedDate" runat="server" />
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 75px"><strong>Infection:</strong>
                    </td>
                    <td style="width: 100px" id="CurrentInfectionCompleteInfection" runat="server">
                      <asp:Label ID="Label_CurrentInfectionCompleteInfection" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 75px"><strong>Specimen:</strong>
                    </td>
                    <td style="width: 100px" id="CurrentInfectionCompleteSpecimen" runat="server">
                      <asp:Label ID="Label_CurrentInfectionCompleteSpecimen" runat="server" Text="" Visible="false"></asp:Label>
                      <asp:HyperLink ID="Hyperlink_CurrentInfectionCompleteSpecimen" Text="" runat="server"></asp:HyperLink>&nbsp;
                    </td>
                    <td style="width: 125px"><strong>HAI Investigation:</strong>
                    </td>
                    <td style="width: 100px" id="CurrentInfectionCompleteHAIInvestigation" runat="server">
                      <asp:Label ID="Label_CurrentInfectionCompleteHAIInvestigation" runat="server" Text="" Visible="false"></asp:Label>
                      <asp:HyperLink ID="Hyperlink_CurrentInfectionCompleteHAIInvestigation" Text="" runat="server"></asp:HyperLink>&nbsp;
                      <asp:HiddenField ID="HiddenField_CurrentInfectionCompleteHAIId" runat="server" />
                    </td>
                    <td style="width: 75px"><strong>Is Active:</strong>
                    </td>
                    <td style="width: 100px" id="CurrentInfectionCompleteIsActive" runat="server">
                      <asp:Label ID="Label_CurrentInfectionCompleteIsActive" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Footer">
                  <tr>
                    <td style="width: 100px; text-align: left;">&nbsp;</td>
                    <td style="width: 800px; text-align: center;">
                      <asp:Button ID="Button_InfectionInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_InfectionInfectionHome_OnClick" />&nbsp;
                      <asp:Button ID="Button_HAIYes_LinkedSiteRequired" runat="server" Text="Linked Site Required" CssClass="Controls_Button" OnClick="Button_HAIYes_LinkedSiteRequired_OnClick" Enabled="false" />
                      <asp:Button ID="Button_HAIYes_SpecimenIncomplete" runat="server" Text="Specimen Incomplete" CssClass="Controls_Button" OnClick="Button_HAIYes_SpecimenIncomplete_OnClick" Enabled="false" />
                      <asp:Button ID="Button_HAIYes_InfectionCanceled" runat="server" Text="Infection Cancelled" CssClass="Controls_Button" OnClick="Button_HAIYes_InfectionCanceled_OnClick" Enabled="false" />
                      <asp:Button ID="Button_HAIYes_CompleteInfectionGoToHAIInvestigation" runat="server" Text="Complete Infection and Go to HAI Investigation" CssClass="Controls_Button" OnClick="Button_HAIYes_CompleteInfectionGoToHAIInvestigation_OnClick" />
                      <asp:Button ID="Button_HAIYes_OpenInfection" runat="server" Text="Open Infection" CssClass="Controls_Button" OnClick="Button_HAIYes_OpenInfection_OnClick" />&nbsp;
                      <asp:Button ID="Button_HAIYes_CompleteHAIInvestigation" runat="server" Text="Complete HAI Investigation" CssClass="Controls_Button" OnClick="Button_HAIYes_CompleteHAIInvestigation_OnClick" />
                      <asp:Button ID="Button_HAIYes_OpenHAIInvestigation" runat="server" Text="Open HAI Investigation" CssClass="Controls_Button" OnClick="Button_HAIYes_OpenHAIInvestigation_OnClick" />&nbsp;
                      <asp:Button ID="Button_HAIYes_CaptureNewInfection" runat="server" Text="Capture New Infection" CssClass="Controls_Button" OnClick="Button_HAIYes_CaptureNewInfection_OnClick" />
                      <asp:Button ID="Button_HAINo_SpecimenIncomplete" runat="server" Text="Specimen Incomplete" CssClass="Controls_Button" OnClick="Button_HAINo_SpecimenIncomplete_OnClick" Enabled="false" />
                      <asp:Button ID="Button_HAINo_InfectionCanceled" runat="server" Text="Infection Cancelled" CssClass="Controls_Button" OnClick="Button_HAINo_InfectionCanceled_OnClick" Enabled="false" />
                      <asp:Button ID="Button_HAINo_CompleteInfection" runat="server" Text="Complete Infection" CssClass="Controls_Button" OnClick="Button_HAINo_CompleteInfection_OnClick" />
                      <asp:Button ID="Button_HAINo_OpenInfection" runat="server" Text="Open Infection" CssClass="Controls_Button" OnClick="Button_HAINo_OpenInfection_OnClick" />&nbsp;
                      <asp:Button ID="Button_HAINo_CaptureNewInfection" runat="server" Text="Capture New Infection" CssClass="Controls_Button" OnClick="Button_HAINo_CaptureNewInfection_OnClick" />
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivFile" runat="server" style="height: 40px; width: 900px; text-align: center;">
            &nbsp;
          </div>
          <table id="TableFile" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_FileHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td colspan="2">Upload Files
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">When uploading a document<br />
                      Only these document formats can be uploaded: Word (.doc / .docx), Excel (.xls / .xlsx), Adobe (.pdf), Fax (.tif / .tiff), TXT (.txt), Outlook (.msg), Images (.jpeg / .jpg / .gif / .png)<br />
                      Only files smaller then 5 MB can be uploaded
                    <asp:HiddenField ID="HiddenField_File" runat="server" />
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <asp:Label ID="Label_MessageFile" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">File
                    </td>
                    <td style="width: 725px;">
                      <asp:FileUpload ID="FileUpload_File" runat="server" CssClass="Controls_FileUpload" Width="350px" AllowMultiple="true" />&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="text-align: center;">
                      <asp:Button ID="Button_UploadFile" runat="server" OnClick="Button_UploadFile_OnClick" Text="Upload File" CssClass="Controls_Button" CommandArgument="FileUpload_File" OnDataBinding="Button_UploadFile_DataBinding" />&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" style="padding: 0px;">
                      <asp:GridView ID="GridView_IPS_File" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_File" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_IPS_File_RowCreated" OnPreRender="GridView_IPS_File_PreRender">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>
                                <asp:Button ID="Button_DeleteFile" runat="server" OnClick="Button_DeleteFile_OnClick" Text="Delete Checked Files" CssClass="Controls_Button" CommandArgument="GridView_File" OnDataBinding="Button_DeleteFile_DataBinding" />&nbsp;
                              <asp:Button ID="Button_DeleteAllFile" runat="server" OnClick="Button_DeleteAllFile_OnClick" Text="Delete All Files" CssClass="Controls_Button" CommandArgument="GridView_File" OnDataBinding="Button_DeleteAllFile_DataBinding" />&nbsp;
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
                              <td>No Files
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="" ItemStyle-Width="25px">
                            <ItemTemplate>
                              <asp:CheckBox ID="CheckBox_File" runat="server" CssClass='<%# Eval("IPS_File_Id") %>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Uploaded File">
                            <ItemTemplate>
                              <asp:LinkButton ID="LinkButton_File" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("IPS_File_Name")) %>' CommandArgument='<%# Eval("IPS_File_Id") %>' OnDataBinding="LinkButton_File_DataBinding"></asp:LinkButton>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="IPS_File_CreatedBy" HeaderText="Created By" ReadOnly="True" ItemStyle-Width="75px" />
                          <asp:BoundField DataField="IPS_File_CreatedDate" HeaderText="Created Date" ReadOnly="True" ItemStyle-Width="125px" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_File" runat="server" OnSelected="SqlDataSource_IPS_File_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <table id="TableFileList" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_FileListHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenFileListTotalRecords" runat="server" Text="" Visible="false" />
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
                      <asp:GridView ID="GridView_IPS_File_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_File_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_File_List_PreRender" OnRowCreated="GridView_IPS_File_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_FileListTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px;">&nbsp;
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
                              <td colspan="2">No Files Captured
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_FileListTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="Uploaded File">
                            <ItemTemplate>
                              <asp:LinkButton ID="LinkButton_File" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("IPS_File_Name")) %>' CommandArgument='<%# Eval("IPS_File_Id") %>' OnDataBinding="LinkButton_File_DataBinding"></asp:LinkButton>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="IPS_File_CreatedBy" HeaderText="Created By" ReadOnly="True" ItemStyle-Width="75px" />
                          <asp:BoundField DataField="IPS_File_CreatedDate" HeaderText="Created Date" ReadOnly="True" ItemStyle-Width="125px" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_File_List" runat="server" OnSelected="SqlDataSource_IPS_File_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <asp:LinkButton ID="LinkButton_CurrentFile" runat="server"></asp:LinkButton>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
    <div style="height: 1000px;">
      &nbsp;
    </div>
  </form>
</body>
</html>
