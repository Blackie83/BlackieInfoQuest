<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_IPS" MaintainScrollPositionOnPostback="true" CodeBehind="Form_IPS.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Infection Prevention Surveillance</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_IPS.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_IPS" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_IPS" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_IPS" AssociatedUpdatePanelID="UpdatePanel_IPS">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_IPS" runat="server">
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
                      <asp:Label ID="Label_InvalidSearchMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td id="SearchFacility" style="width: 150px">Facility
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_IPS_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td id="SearchPatientVisitNumber" style="width: 150px">Patient Visit Number
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_PatientVisitNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                      <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_PatientVisitNumber" runat="server" TargetControlID="TextBox_PatientVisitNumber" FilterType="Numbers">
                      </Ajax:FilteredTextBoxExtender>
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
                      <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" CausesValidation="False" />&nbsp;
                    <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_Click" CausesValidation="False" />&nbsp;
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
                      <asp:Button ID="Button_GoToList" runat="server" Text="Go To List" CssClass="Controls_Button" OnClick="Button_GoToList_Click" CausesValidation="False" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div style="height: 40px; width: 900px; text-align: center;">
            &nbsp;
          </div>
          <table id="TableVisitInfo" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_VisitInfoHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="width: 115px">Facility:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Date of Birth:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIDateOfBirth" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Visit Number:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Date of Admission:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIDateAdmission" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Surname, Name:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Date of Discharge:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIDateDischarge" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Gender:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIGender" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Deceased:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIDeceased" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px">Age:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIAge" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 115px">Ward:
                    </td>
                    <td style="width: 335px">
                      <asp:Label ID="Label_VIWard" runat="server" Text=""></asp:Label>&nbsp;
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
                    <td style="text-align: center;">&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div style="height: 40px; width: 900px; text-align: center;">
            &nbsp;
          </div>
          <table id="TableInfection" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_InfectionHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenInfectionTotalRecords" runat="server" Text="" Visible="false" />
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
                      <asp:GridView ID="GridView_IPS_Infection_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_Infection_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_Infection_List_PreRender" OnRowCreated="GridView_IPS_Infection_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_InfectionTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_CaptureNewInfection" runat="server" Text="Capture New Infection" CssClass="Controls_Button" OnClick="Button_CaptureNewInfection_Click" />
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
                              <td colspan="2">No Infection Captured
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_InfectionTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_CaptureNewInfection" runat="server" Text="Capture New Infection" CssClass="Controls_Button" OnClick="Button_CaptureNewInfection_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <table class="Table_Body">
                                <tr>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <strong>Report Number</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <strong>Infection</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <strong>Specimen</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <strong>HAI Investigation</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <strong>Is Active</strong>
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <asp:Label ID="Label_ReportNumber" runat="server" Text='<%# Bind("IPS_Infection_ReportNumber") %>' Width="150px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" id="InfectionCompleted" runat="server" style="width: 180px;">
                                    <asp:Label ID="Label_Completed" runat="server" Text='<%# Bind("IPS_Infection_Completed") %>' Visible="false" Width="140px"></asp:Label>
                                    <asp:HyperLink ID="HyperLink_Completed" Text='<%# GetInfectionLink(Eval("IPS_Infection_Id"), Eval("IPS_Infection_Completed")) %>' runat="server"></asp:HyperLink>
                                  </td>
                                  <td class="Table_TemplateField" id="InfectionSpecimen" runat="server" style="width: 180px;">
                                    <asp:Label ID="Label_Specimen" runat="server" Text='<%# Bind("Specimen") %>' Visible="false"></asp:Label>
                                    <asp:HyperLink ID="HyperLink_Specimen" Text='<%# GetSpecimenLink(Eval("IPS_Infection_Id"), Eval("Specimen")) %>' runat="server"></asp:HyperLink>
                                  </td>
                                  <td class="Table_TemplateField" id="InfectionHAI" runat="server" style="width: 180px;">
                                    <asp:Label ID="Label_HAI" runat="server" Text='<%# Bind("HAI") %>' Visible="false"></asp:Label>
                                    <asp:HyperLink ID="HyperLink_HAI" Text='<%# GetHAILink(Eval("IPS_Infection_Id"), Eval("IPS_Infection_Completed"), Eval("HAI"), Eval("IPS_HAI_Id")) %>' runat="server"></asp:HyperLink>
                                  </td>
                                  <td class="Table_TemplateField" id="InfectionIsActive" runat="server" style="width: 180px;">
                                    <asp:Label ID="Label_IsActive" runat="server" Text='<%# Bind("IPS_Infection_IsActive") %>' Width="160px"></asp:Label>&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <strong>Category</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <strong>Type</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <strong>Sub Type</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <strong>Sub Sub Type</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <strong>Site</strong>
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <asp:Label ID="Label_Category" runat="server" Text='<%# Bind("IPS_Infection_Category_Name") %>' Width="160px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <asp:Label ID="Label_Type" runat="server" Text='<%# Bind("IPS_Infection_Type_Name") %>' Width="160px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <asp:Label ID="Label_SubType" runat="server" Text='<%# Bind("IPS_Infection_SubType_Name") %>' Width="160px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <asp:Label ID="Label_SubSubType" runat="server" Text='<%# Bind("IPS_Infection_SubSubType_Name") %>' Width="160px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" id="InfectionHAISite" runat="server" style="width: 180px;">
                                    <asp:Label ID="Label_Site" runat="server" Text='<%# Bind("IPS_Infection_Site_Name") %>' Width="160px"></asp:Label>&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <strong>Unit</strong>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <asp:Label ID="Label_Unit" runat="server" Text='<%# Bind("Unit_Name") %>' Width="160px"></asp:Label>&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 180px;">
                                    <strong>Summary</strong>
                                  </td>
                                  <td class="Table_TemplateField" colspan="2" style="width: 360px;">
                                    <asp:Label ID="Label_Summary" runat="server" Text='<%# Bind("IPS_Infection_Summary") %>' Width="340px"></asp:Label>&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_Infection_List" runat="server" OnSelected="SqlDataSource_IPS_Infection_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivCurrentInfection" runat="server" style="height: 40px; width: 900px; text-align: center;">
            &nbsp;
          </div>
          <a id="CurrentInfection"></a>
          <asp:LinkButton ID="LinkButton_CurrentInfection" runat="server"></asp:LinkButton>
          <table id="TableCurrentInfection" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_CurrentInfectionHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_IPS_Infection_Form" runat="server" DataKeyNames="IPS_Infection_Id" CssClass="FormView" DataSourceID="SqlDataSource_IPS_Infection_Form" OnItemInserting="FormView_IPS_Infection_Form_ItemInserting" DefaultMode="Insert" OnDataBound="FormView_IPS_Infection_Form_DataBound" OnItemUpdating="FormView_IPS_Infection_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Report Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("IPS_Infection_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInfectionCategoryList">Category
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertInfectionCategoryList" runat="server" DataSourceID="SqlDataSource_IPS_InsertInfectionCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Infection_Category_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="InfectionType">
                        <td style="width: 175px;" id="FormInfectionTypeList">Type
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertInfectionTypeList" runat="server" DataSourceID="SqlDataSource_IPS_InsertInfectionTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Infection_Type_List") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertInfectionTypeList_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="InfectionSubType">
                        <td style="width: 175px;" id="FormInfectionSubTypeList">Sub Type
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertInfectionSubTypeList" runat="server" DataSourceID="SqlDataSource_IPS_InsertInfectionSubTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertInfectionSubTypeList_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Sub Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="InfectionSubSubType">
                        <td style="width: 175px;" id="FormInfectionSubSubTypeList">Sub Sub Type
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertInfectionSubSubTypeList" runat="server" DataSourceID="SqlDataSource_IPS_InsertInfectionSubSubTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Sub Sub Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="InfectionSite">
                        <td style="width: 175px;" id="FormInfectionSiteList">Site
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertInfectionSiteList" runat="server" DataSourceID="SqlDataSource_IPS_InsertInfectionSiteList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Infection_Site_List") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertInfectionSiteList_SelectedIndexChanged" OnDataBound="DropDownList_InsertInfectionSiteList_DataBound">
                            <asp:ListItem Value="">Select Site</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="InfectionLinkedSite">
                        <td style="width: 175px;" id="FormInfectionSiteInfectionId">Linked Primary or Secondary Site
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertInfectionSiteInfectionId" runat="server" DataSourceID="SqlDataSource_IPS_InsertInfectionSiteInfectionId" AppendDataBoundItems="true" DataTextField="IPS_Infection_ReportNumber" DataValueField="IPS_Infection_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Linked Site</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Screening
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertInfectionScreening" runat="server" Checked='<%# Bind("IPS_Infection_Screening") %>' />
                        </td>
                      </tr>
                      <tr id="InfectionScreeningType">
                        <td style="width: 175px;" id="FormInfectionScreeningType">Screening Type
                        </td>
                        <td style="width: 725px; padding: 0px; border-left-width: 0px; border-top-width: 1px; border-bottom-width: 0px;" colspan="3">
                          <div id="InsertScreeningTypeTypeList" style="max-height: 250px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                            <asp:CheckBoxList ID="CheckBoxList_InsertScreeningTypeTypeList" runat="server" AppendDataBoundItems="true" CssClass="Controls_CheckBoxListWithScrollbars" DataSourceID="SqlDataSource_IPS_InsertInfectionScreeningTypeTypeList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="3" RepeatLayout="Table" Width="690px">
                            </asp:CheckBoxList>
                          </div>
                          <asp:HiddenField ID="HiddenField_InsertScreeningTypeTypeListTotal" runat="server" OnDataBinding="HiddenField_InsertScreeningTypeTypeListTotal_DataBinding" />
                        </td>
                      </tr>
                      <tr id="InfectionScreeningReason">
                        <td style="width: 175px;" id="FormInfectionScreeningReasonList">Screening Reason
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertInfectionScreeningReasonList" runat="server" DataSourceID="SqlDataSource_IPS_InsertInfectionScreeningReasonList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Infection_ScreeningReason_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Reason</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInfectionUnit">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertInfectionUnitId" runat="server" DataSourceID="SqlDataSource_IPS_InsertInfectionUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInfectionSummary">Infection Summary
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertInfectionSummary" runat="server" TextMode="MultiLine" Rows="4" Width="700px" Text='<%# Bind("IPS_Infection_Summary") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertInfectionSummary" runat="server" TargetControlID="TextBox_InsertInfectionSummary" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("IPS_Infection_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created Date
                        </td>
                        <td style="width: 225px;">
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("IPS_Infection_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 175px;">Created By
                        </td>
                        <td style="width: 225px;">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("IPS_Infection_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified Date
                        </td>
                        <td style="width: 225px;">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("IPS_Infection_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 175px;">Modified By
                        </td>
                        <td style="width: 225px;">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("IPS_Infection_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_InsertInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_InsertInfectionHome_Click" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Infection" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Report Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("IPS_Infection_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          <asp:HiddenField ID="HiddenField_EditModifiedDate" runat="server" Value='<%# Eval("IPS_Infection_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:HiddenField>
                          <asp:HiddenField ID="HiddenField_EditModifiedBy" runat="server" Value='<%# Eval("IPS_Infection_ModifiedBy") %>'></asp:HiddenField>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInfectionCategoryList">Category
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditInfectionCategoryList" runat="server" DataSourceID="SqlDataSource_IPS_EditInfectionCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Infection_Category_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_EditInfectionCategoryList" runat="server" Value='<%# Eval("IPS_Infection_Category_List") %>'></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="InfectionType">
                        <td style="width: 175px;" id="FormInfectionTypeList">Type
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditInfectionTypeList" runat="server" DataSourceID="SqlDataSource_IPS_EditInfectionTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Infection_Type_List") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditInfectionTypeList_SelectedIndexChanged" OnDataBound="DropDownList_EditInfectionTypeList_DataBound">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_EditInfectionTypeList" runat="server" Value='<%# Eval("IPS_Infection_Type_List") %>'></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="InfectionSubType">
                        <td style="width: 175px;" id="FormInfectionSubTypeList">Sub Type
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditInfectionSubTypeList" runat="server" DataSourceID="SqlDataSource_IPS_EditInfectionSubTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditInfectionSubTypeList_SelectedIndexChanged" OnDataBound="DropDownList_EditInfectionSubTypeList_DataBound">
                            <asp:ListItem Value="">Select Sub Type</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_EditInfectionSubTypeList" runat="server" Value='<%# Eval("IPS_Infection_SubType_List") %>'></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="InfectionSubSubType">
                        <td style="width: 175px;" id="FormInfectionSubSubTypeList">Sub Sub Type
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditInfectionSubSubTypeList" runat="server" DataSourceID="SqlDataSource_IPS_EditInfectionSubSubTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Sub Sub Type</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_EditInfectionSubSubTypeList" runat="server" Value='<%# Eval("IPS_Infection_SubSubType_List") %>'></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="InfectionSite">
                        <td style="width: 175px;" id="FormInfectionSiteList">Site
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditInfectionSiteList" runat="server" DataSourceID="SqlDataSource_IPS_EditInfectionSiteList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Infection_Site_List") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditInfectionSiteList_SelectedIndexChanged" OnDataBound="DropDownList_EditInfectionSiteList_DataBound">
                            <asp:ListItem Value="">Select Site</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_EditInfectionSiteList" runat="server" Value='<%# Eval("IPS_Infection_Site_List") %>'></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="InfectionLinkedSite">
                        <td style="width: 175px;" id="FormInfectionSiteInfectionId">Linked Primary or Secondary Site
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditInfectionSiteInfectionId" runat="server" DataSourceID="SqlDataSource_IPS_EditInfectionSiteInfectionId" AppendDataBoundItems="true" DataTextField="IPS_Infection_ReportNumber" DataValueField="IPS_Infection_Id" CssClass="Controls_DropDownList" OnDataBound="DropDownList_EditInfectionSiteInfectionId_DataBound">
                            <asp:ListItem Value="">Select Linked Site</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_EditInfectionSiteInfectionId" runat="server" Value='<%# Eval("IPS_Infection_Site_Infection_Id") %>'></asp:HiddenField>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Screening
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditInfectionScreening" runat="server" Checked='<%# Bind("IPS_Infection_Screening") %>' />
                          <asp:HiddenField ID="HiddenField_EditInfectionScreening" runat="server" Value='<%# Eval("IPS_Infection_Screening") %>'></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="InfectionScreeningType">
                        <td style="width: 175px;" id="FormInfectionScreeningType">Screening Type
                        </td>
                        <td style="width: 725px; padding: 0px; border-left-width: 0px; border-top-width: 1px; border-bottom-width: 0px;" colspan="3">
                          <div id="EditScreeningTypeTypeList" style="max-height: 250px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                            <asp:CheckBoxList ID="CheckBoxList_EditScreeningTypeTypeList" runat="server" AppendDataBoundItems="true" CssClass="Controls_CheckBoxListWithScrollbars" DataSourceID="SqlDataSource_IPS_EditInfectionScreeningTypeTypeList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="3" RepeatLayout="Table" OnDataBound="CheckBoxList_EditScreeningTypeTypeList_DataBound" Width="690px">
                            </asp:CheckBoxList>
                          </div>
                          <asp:HiddenField ID="HiddenField_EditScreeningTypeTypeListTotal" runat="server" OnDataBinding="HiddenField_EditScreeningTypeTypeListTotal_DataBinding" />
                        </td>
                      </tr>
                      <tr id="InfectionScreeningReason">
                        <td style="width: 175px;" id="FormInfectionScreeningReasonList">Screening Reason
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditInfectionScreeningReasonList" runat="server" DataSourceID="SqlDataSource_IPS_EditInfectionScreeningReasonList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Infection_ScreeningReason_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Reason</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInfectionUnit">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditInfectionUnitId" runat="server" DataSourceID="SqlDataSource_IPS_EditInfectionUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_EditInfectionUnitId" runat="server" Value='<%# Eval("Unit_id") %>'></asp:HiddenField>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInfectionSummary">Infection Summary
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditInfectionSummary" runat="server" TextMode="MultiLine" Rows="4" Width="700px" Text='<%# Bind("IPS_Infection_Summary") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditInfectionSummary" runat="server" TargetControlID="TextBox_EditInfectionSummary" FilterMode="InvalidChars" FilterType="Custom" InvalidChars="<#">
                          </Ajax:FilteredTextBoxExtender>
                          <asp:HiddenField ID="HiddenField_EditInfectionSummary" runat="server" Value='<%# Eval("IPS_Infection_Summary") %>'></asp:HiddenField>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Is Active
                        </td>
                        <td colspan="3">
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("IPS_Infection_IsActive") %>' />
                          <asp:HiddenField ID="HiddenField_EditIsActive" runat="server" Value='<%# Eval("IPS_Infection_IsActive") %>'></asp:HiddenField>
                        </td>
                      </tr>
                      <tr id="IPSInfectionRejectedReason">
                        <td style="width: 175px;" id="FormRejectedReason">Rejected Reason</td>
                        <td colspan="3"><asp:TextBox ID="TextBox_EditRejectedReason" runat="server" TextMode="MultiLine" Rows="4" Width="700px" Text='<%# Bind("IPS_Infection_RejectedReason") %>' CssClass="Controls_TextBox"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created Date
                        </td>
                        <td style="width: 225px;">
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("IPS_Infection_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 175px;">Created By
                        </td>
                        <td style="width: 225px;">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("IPS_Infection_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified Date
                        </td>
                        <td style="width: 225px;">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("IPS_Infection_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 175px;">Modified By
                        </td>
                        <td style="width: 225px;">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("IPS_Infection_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_EditInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_EditInfectionHome_Click" />&nbsp;
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="False" CommandName="Update" Text="Print Form" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Infection" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4"></td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Report Number
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("IPS_Infection_ReportNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Category
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionCategoryList" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemInfectionCategoryList" runat="server" Value='<%# Eval("IPS_Infection_Category_List") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="InfectionType">
                        <td style="width: 175px;">Type
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionTypeList" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="InfectionSubType">
                        <td style="width: 175px;">Sub Type
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionSubTypeList" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="InfectionSubSubType">
                        <td style="width: 175px;">Sub Sub Type
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionSubSubTypeList" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="InfectionSite">
                        <td style="width: 175px;">Site
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionSiteList" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="InfectionLinkedSite">
                        <td style="width: 175px;">Linked Primary or Secondary Site
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionSiteInfectionId" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Screening
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionScreening" runat="server" Text='<%# (bool)(Eval("IPS_Infection_Screening"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemInfectionScreening" runat="server" Value='<%# Eval("IPS_Infection_Screening") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="InfectionScreeningType">
                        <td style="width: 175px;" id="FormInfectionScreeningType">Screening Type
                        </td>
                        <td style="width: 725px; padding: 0px; border-left-width: 1px; border-top-width: 1px;" colspan="3">
                          <asp:GridView ID="GridView_ItemInfectionScreeningType" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_IPS_ItemInfectionScreeningType" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="False" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemInfectionScreeningType_RowCreated" OnPreRender="GridView_ItemInfectionScreeningType_PreRender">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>&nbsp;
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
                                  <td>No Screening Type
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="IPS_ScreeningType_Type_Name" ReadOnly="True" />
                            </Columns>
                          </asp:GridView>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="InfectionScreeningReason">
                        <td style="width: 175px;" id="FormInfectionScreeningReasonList">Screening Reason
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionScreeningReasonList" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInfectionUnit">Unit
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionUnit" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormInfectionSummary">Infection Summary
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:Label ID="Label_ItemInfectionSummary" runat="server" Text='<%# Bind("IPS_Infection_Summary") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("IPS_Infection_IsActive"))?"Yes":"No" %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemIsActive" runat="server" Value='<%# Bind("IPS_Infection_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="IPSInfectionRejectedReason">
                        <td style="width: 175px;">Rejected Reason
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemRejectedReason" runat="server" Text='<%# Bind("IPS_Infection_RejectedReason") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created Date
                        </td>
                        <td style="width: 225px;">
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("IPS_Infection_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 175px;">Created By
                        </td>
                        <td style="width: 225px;">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("IPS_Infection_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified Date
                        </td>
                        <td style="width: 225px;">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("IPS_Infection_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 175px;">Modified By
                        </td>
                        <td style="width: 225px;">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("IPS_Infection_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_ItemInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_ItemInfectionHome_Click" />&nbsp;
                          <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Form" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertInfectionCategoryList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertInfectionTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertInfectionSubTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertInfectionSubSubTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertInfectionSiteList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertInfectionSiteInfectionId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertInfectionScreeningTypeTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertInfectionScreeningReasonList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertInfectionUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditInfectionCategoryList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditInfectionTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditInfectionSubTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditInfectionSubSubTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditInfectionSiteList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditInfectionSiteInfectionId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditInfectionScreeningTypeTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditInfectionScreeningReasonList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditInfectionUnitId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_ItemInfectionScreeningType" runat="server" OnSelected="SqlDataSource_IPS_ItemInfectionScreeningType_Selected"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_Infection_Form" runat="server" OnInserted="SqlDataSource_IPS_Infection_Form_Inserted" OnUpdated="SqlDataSource_IPS_Infection_Form_Updated"></asp:SqlDataSource>
              </td>
            </tr>
          </table>
          <div id="DivTheatre" runat="server" style="height: 40px; width: 900px; text-align: center;">
            &nbsp;
          </div>
          <a id="Theatre"></a>
          <table id="TableTheatre" style="width: 900px;" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_TheatreHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenTheatreTotalRecords" runat="server" Text="" Visible="false" />
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
                      <asp:GridView ID="GridView_IPS_Theatre_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_Theatre_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_Theatre_List_PreRender" OnRowCreated="GridView_IPS_Theatre_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_TheatreTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_TheatreInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_TheatreInfectionHome_Click" />&nbsp;
                                <asp:Button ID="Button_TheatreSelect" runat="server" Text="Select Visit History" CssClass="Controls_Button" OnClick="Button_TheatreSelect_Click" />
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
                              <td colspan="2">No Visit History Selected
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_TheatreTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_TheatreInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_TheatreInfectionHome_Click" />&nbsp;
                                <asp:Button ID="Button_TheatreSelect" runat="server" Text="Select Visit History" CssClass="Controls_Button" OnClick="Button_TheatreSelect_Click" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <table class="Table_Body">
                                <tr>
                                  <td style="width: 180px;" class="Table_TemplateField"><strong>Facility</strong></td>
                                  <td style="width: 90px;" class="Table_TemplateField"><strong>Visit Number</strong></td>
                                  <td style="width: 90px;" class="Table_TemplateField"><strong>Type</strong></td>
                                  <td style="width: 180px;" class="Table_TemplateField"><strong>Admission Date</strong></td>
                                  <td style="width: 180px;" class="Table_TemplateField"><strong>Discharge Date</strong></td>
                                  <td style="width: 180px;" class="Table_TemplateField"><strong>Final Diagnosis</strong></td>
                                </tr>
                                <tr>
                                  <td style="width: 180px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_Facility" runat="server" Text='<%# Bind("IPS_Theatre_FacilityDisplayName") %>' Width="170px"></asp:Label></td>
                                  <td style="width: 90px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_VisitNumber" runat="server" Text='<%# Bind("IPS_Theatre_VisitNumber") %>' Width="80px"></asp:Label></td>
                                  <td style="width: 90px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_Type" runat="server" Text='<%# Bind("IPS_Theatre_ServiceCategory") %>' Width="80px"></asp:Label></td>
                                  <td style="width: 180px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_DateOfAdmission" runat="server" Text='<%# Bind("IPS_Theatre_DateOfAdmission") %>' Width="170px"></asp:Label></td>
                                  <td style="width: 180px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_DateOfDischarge" runat="server" Text='<%# Bind("IPS_Theatre_DateOfDischarge") %>' Width="170px"></asp:Label></td>
                                  <td style="width: 180px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_FinalDiagnosis" runat="server" Text='<%# GetFinalDiagnosis(Eval("IPS_Theatre_FinalDiagnosisCode"), Eval("IPS_Theatre_FinalDiagnosisDescription")) %>' Width="170px"></asp:Label></td>
                                </tr>
                                <tr id="SurgicalRow1" runat="server">
                                  <td style="width: 180px;" class="Table_TemplateField"><strong>Theatre</strong></td>
                                  <td style="width: 180px;" class="Table_TemplateField" colspan="2"><strong>Theatre Time</strong></td>
                                  <td style="width: 180px;" class="Table_TemplateField"><strong>Procedure Date</strong></td>
                                  <td style="width: 180px;" class="Table_TemplateField"><strong>Procedure</strong></td>
                                  <td style="width: 180px;" class="Table_TemplateField"><strong>Theatre Invoice</strong></td>
                                </tr>
                                <tr id="SurgicalRow2" runat="server">
                                  <td style="width: 180px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_Theatre" runat="server" Text='<%# Bind("IPS_Theatre_Theatre") %>' Width="170px"></asp:Label></td>
                                  <td style="width: 180px;" class="Table_TemplateField" colspan="2">
                                    <asp:Label ID="Label_TheatreTime" runat="server" Text='<%# Bind("IPS_Theatre_TheatreTime") %>' Width="170px"></asp:Label></td>
                                  <td style="width: 180px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_ProcedureDate" runat="server" Text='<%# Bind("IPS_Theatre_ProcedureDate") %>' Width="170px"></asp:Label></td>
                                  <td style="width: 180px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_Procedure" runat="server" Text='<%# GetProcedure(Eval("IPS_Theatre_ProcedureCode"), Eval("IPS_Theatre_ProcedureDescription")) %>' Width="170px"></asp:Label></td>
                                  <td style="width: 180px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_TheatreInvoice" runat="server" Text='<%# Bind("IPS_Theatre_TheatreInvoice") %>' Width="170px"></asp:Label></td>
                                </tr>
                                <tr id="SurgicalRow3" runat="server">
                                  <td style="width: 180px;" class="Table_TemplateField"><strong>Surgeon</strong></td>
                                  <td style="width: 180px;" class="Table_TemplateField" colspan="2"><strong>Anaesthetist</strong></td>
                                  <td style="width: 180px;" class="Table_TemplateField"><strong>Assistant</strong></td>
                                  <td style="width: 180px;" class="Table_TemplateField"><strong>Wound Category</strong></td>
                                  <td style="width: 180px;" class="Table_TemplateField"><strong>Scrub Nurse</strong></td>
                                </tr>
                                <tr id="SurgicalRow4" runat="server">
                                  <td style="width: 180px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_Surgeon" runat="server" Text='<%# Bind("IPS_Theatre_Surgeon") %>' Width="170px"></asp:Label></td>
                                  <td style="width: 180px;" class="Table_TemplateField" colspan="2">
                                    <asp:Label ID="Label_Anaesthetist" runat="server" Text='<%# Bind("IPS_Theatre_Anaesthetist") %>' Width="170px"></asp:Label></td>
                                  <td style="width: 180px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_Assistant" runat="server" Text='<%# Bind("IPS_Theatre_Assistant") %>' Width="170px"></asp:Label></td>
                                  <td style="width: 180px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_WoundCategory" runat="server" Text='<%# Bind("IPS_Theatre_WoundCategory") %>' Width="170px"></asp:Label></td>
                                  <td style="width: 180px;" class="Table_TemplateField">
                                    <asp:Label ID="Label_ScrubNurse" runat="server" Text='<%# Bind("IPS_Theatre_ScrubNurse") %>' Width="170px"></asp:Label></td>
                                </tr>
                              </table>
                            </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_Theatre_List" runat="server" OnSelected="SqlDataSource_IPS_Theatre_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivVisitDiagnosis" runat="server" style="height: 40px; width: 900px; text-align: center;">
            &nbsp;
          </div>
          <a id="VisitDiagnosis"></a>
          <table id="TableVisitDiagnosis" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_VisitDiagnosisHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenVisitDiagnosisTotalRecords" runat="server" Text="" Visible="false" />
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
                      <asp:GridView ID="GridView_IPS_VisitDiagnosis_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_VisitDiagnosis_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_VisitDiagnosis_List_PreRender" OnRowCreated="GridView_IPS_VisitDiagnosis_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_VisitDiagnosisTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_VisitDiagnosisInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_VisitDiagnosisInfectionHome_Click" />&nbsp;
                                <asp:Button ID="Button_VisitDiagnosisSelect" runat="server" Text="Select Visit Diagnosis" CssClass="Controls_Button" OnClick="Button_VisitDiagnosisSelect_Click" />
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
                              <td colspan="2">No Visit Diagnosis Selected
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_VisitDiagnosisTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_VisitDiagnosisInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_VisitDiagnosisInfectionHome_Click" />&nbsp;
                                <asp:Button ID="Button_VisitDiagnosisSelect" runat="server" Text="Select Visit Diagnosis" CssClass="Controls_Button" OnClick="Button_VisitDiagnosisSelect_Click" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="IPS_VisitDiagnosis_CodeType" HeaderText="Type" ReadOnly="True" SortExpression="IPS_VisitDiagnosis_CodeType" />
                          <asp:BoundField DataField="IPS_VisitDiagnosis_Code" HeaderText="Code" ReadOnly="True" SortExpression="IPS_VisitDiagnosis_Code" />
                          <asp:BoundField DataField="IPS_VisitDiagnosis_Description" HeaderText="Description" ReadOnly="True" SortExpression="IPS_VisitDiagnosis_Description" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_VisitDiagnosis_List" runat="server" OnSelected="SqlDataSource_IPS_VisitDiagnosis_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivBedHistory" runat="server" style="height: 40px; width: 900px; text-align: center;">
            &nbsp;
          </div>
          <a id="BedHistory"></a>
          <table id="TableBedHistory" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_BedHistoryHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenBedHistoryTotalRecords" runat="server" Text="" Visible="false" />
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
                      <asp:GridView ID="GridView_IPS_BedHistory_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_BedHistory_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_BedHistory_List_PreRender" OnRowCreated="GridView_IPS_BedHistory_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_BedHistoryTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_BedHistoryInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_BedHistoryInfectionHome_Click" />&nbsp;
                                <asp:Button ID="Button_BedHistorySelect" runat="server" Text="Select Bed History" CssClass="Controls_Button" OnClick="Button_BedHistorySelect_Click" />
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
                              <td colspan="2">No Bed History Selected
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_BedHistoryTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_BedHistoryInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_BedHistoryInfectionHome_Click" />&nbsp;
                                <asp:Button ID="Button_BedHistorySelect" runat="server" Text="Select Bed History" CssClass="Controls_Button" OnClick="Button_BedHistorySelect_Click" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="IPS_BedHistory_Department" HeaderText="Department" ReadOnly="True" SortExpression="IPS_BedHistory_Department" />
                          <asp:BoundField DataField="IPS_BedHistory_Room" HeaderText="Room" ReadOnly="True" SortExpression="IPS_BedHistory_Room" />
                          <asp:BoundField DataField="IPS_BedHistory_Bed" HeaderText="Bed" ReadOnly="True" SortExpression="IPS_BedHistory_Bed" />
                          <asp:BoundField DataField="IPS_BedHistory_Date" HeaderText="Date" ReadOnly="True" SortExpression="IPS_BedHistory_Date" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_BedHistory_List" runat="server" OnSelected="SqlDataSource_IPS_BedHistory_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivSpecimen" runat="server" style="height: 40px; width: 900px; text-align: center;">
            &nbsp;
          </div>
          <a id="Specimen"></a>
          <table id="TableSpecimen" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_SpecimenHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenSpecimenTotalRecords" runat="server" Text="" Visible="false" />
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
                      <asp:GridView ID="GridView_IPS_Specimen_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_Specimen_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_Specimen_List_PreRender" OnRowCreated="GridView_IPS_Specimen_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_SpecimenTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_SpecimenInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_SpecimenInfectionHome_Click" />&nbsp;
                                <asp:Button ID="Button_SpecimenCapture" runat="server" Text="Capture New Specimen" CssClass="Controls_Button" OnClick="Button_SpecimenCapture_Click" />
                                <asp:Button ID="Button_SpecimenAll" runat="server" Text="View All Captured Specimens" CssClass="Controls_Button" OnClick="Button_SpecimenAll_Click" />
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
                              <td colspan="2">No Specimen Captured
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_SpecimenTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_SpecimenInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_SpecimenInfectionHome_Click" />&nbsp;
                                <asp:Button ID="Button_SpecimenCapture" runat="server" Text="Capture New Specimen" CssClass="Controls_Button" OnClick="Button_SpecimenCapture_Click" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="" ItemStyle-Width="50px">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetSpecimenLink(Eval("IPS_Specimen_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="IPS_Specimen_Status_Name" HeaderText="Status" ReadOnly="True" SortExpression="IPS_Specimen_Status_Name" />
                          <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                              <asp:Label ID="Label_SpecimenDate" runat="server" Text='<%# Bind("IPS_Specimen_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="IPS_Specimen_Time" HeaderText="Time" ReadOnly="True" SortExpression="IPS_Specimen_Time" />
                          <asp:BoundField DataField="IPS_Specimen_Type_Name" HeaderText="Type" ReadOnly="True" SortExpression="IPS_Specimen_Type_Name" />
                          <asp:BoundField DataField="IPS_Specimen_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="IPS_Specimen_IsActive" ItemStyle-Width="60px" />
                          <asp:BoundField DataField="Specimen" HeaderText="Specimen" ReadOnly="True" SortExpression="Specimen" ItemStyle-Width="120px" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_Specimen_List" runat="server" OnSelected="SqlDataSource_IPS_Specimen_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivCurrentInfectionComplete" runat="server" style="height: 40px; width: 900px; text-align: center;">
            &nbsp;
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
                      <asp:Button ID="Button_InfectionInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_InfectionInfectionHome_Click" />&nbsp;
                      <asp:Button ID="Button_HAIYes_LinkedSiteRequired" runat="server" Text="Linked Site Required" CssClass="Controls_Button" OnClick="Button_HAIYes_LinkedSiteRequired_Click" Enabled="false" />
                      <asp:Button ID="Button_HAIYes_SpecimenIncomplete" runat="server" Text="Specimen Incomplete" CssClass="Controls_Button" OnClick="Button_HAIYes_SpecimenIncomplete_Click" Enabled="false" />
                      <asp:Button ID="Button_HAIYes_InfectionCanceled" runat="server" Text="Infection Cancelled" CssClass="Controls_Button" OnClick="Button_HAIYes_InfectionCanceled_Click" Enabled="false" />
                      <asp:Button ID="Button_HAIYes_CompleteInfectionGoToHAIInvestigation" runat="server" Text="Complete Infection and Go to HAI Investigation" CssClass="Controls_Button" OnClick="Button_HAIYes_CompleteInfectionGoToHAIInvestigation_Click" />
                      <asp:Button ID="Button_HAIYes_OpenInfection" runat="server" Text="Open Infection" CssClass="Controls_Button" OnClick="Button_HAIYes_OpenInfection_Click" />&nbsp;
                      <asp:Button ID="Button_HAIYes_GoToHAIInvestigation" runat="server" Text="Go to HAI Investigation" CssClass="Controls_Button" OnClick="Button_HAIYes_GoToHAIInvestigation_Click" />
                      <asp:Button ID="Button_HAIYes_OpenHAIInvestigation" runat="server" Text="Open HAI Investigation" CssClass="Controls_Button" OnClick="Button_HAIYes_OpenHAIInvestigation_Click" />&nbsp;
                      <asp:Button ID="Button_HAIYes_CaptureNewInfection" runat="server" Text="Capture New Infection" CssClass="Controls_Button" OnClick="Button_HAIYes_CaptureNewInfection_Click" />
                      <asp:Button ID="Button_HAINo_SpecimenIncomplete" runat="server" Text="Specimen Incomplete" CssClass="Controls_Button" OnClick="Button_HAINo_SpecimenIncomplete_Click" Enabled="false" />
                      <asp:Button ID="Button_HAINo_InfectionCanceled" runat="server" Text="Infection Cancelled" CssClass="Controls_Button" OnClick="Button_HAINo_InfectionCanceled_Click" Enabled="false" />
                      <asp:Button ID="Button_HAINo_CompleteInfection" runat="server" Text="Complete Infection" CssClass="Controls_Button" OnClick="Button_HAINo_CompleteInfection_Click" />
                      <asp:Button ID="Button_HAINo_OpenInfection" runat="server" Text="Open Infection" CssClass="Controls_Button" OnClick="Button_HAINo_OpenInfection_Click" />&nbsp;
                      <asp:Button ID="Button_HAINo_CaptureNewInfection" runat="server" Text="Capture New Infection" CssClass="Controls_Button" OnClick="Button_HAINo_CaptureNewInfection_Click" />
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
                      <asp:Button ID="Button_UploadFile" runat="server" OnClick="Button_UploadFile_Click" Text="Upload File" CssClass="Controls_Button" CommandArgument="FileUpload_File" OnDataBinding="Button_UploadFile_DataBinding" />&nbsp;
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
                                <asp:Button ID="Button_DeleteFile" runat="server" OnClick="Button_DeleteFile_Click" Text="Delete Checked Files" CssClass="Controls_Button" CommandArgument="GridView_File" OnDataBinding="Button_DeleteFile_DataBinding" />&nbsp;
                              <asp:Button ID="Button_DeleteAllFile" runat="server" OnClick="Button_DeleteAllFile_Click" Text="Delete All Files" CssClass="Controls_Button" CommandArgument="GridView_File" OnDataBinding="Button_DeleteAllFile_DataBinding" />&nbsp;
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
