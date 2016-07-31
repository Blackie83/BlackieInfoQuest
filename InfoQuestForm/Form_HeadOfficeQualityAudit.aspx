<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_HeadOfficeQualityAudit.aspx.cs" Inherits="InfoQuestForm.Form_HeadOfficeQualityAudit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Head Office Quality Audit Finding</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_HeadOfficeQualityAudit.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_HeadOfficeQualityAudit" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_HeadOfficeQualityAudit" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_HeadOfficeQualityAudit" AssociatedUpdatePanelID="UpdatePanel_HeadOfficeQualityAudit">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_HeadOfficeQualityAudit" runat="server">
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
          <table id="TableForm" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_FormHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table id="TableFormNoAccess" runat="server" class="FormView_TableBody">
                  <tr>
                    <td>
                      <asp:Label ID="Label_NoAccess" runat="server" CssClass="Controls_Validation" Text="Only Head Office form Administrators can capture new Findings"></asp:Label>
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td>
                      <asp:Button ID="Button_NoAccessCaptured" runat="server" Text="Go to Captured Findings" CssClass="Controls_Button" OnClick="Button_NoAccessCaptured_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
                <asp:FormView ID="FormView_HeadOfficeQualityAudit_Form" runat="server" Width="1000px" DataKeyNames="HQA_Finding_Id" CssClass="FormView" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_Form" OnItemInserting="FormView_HeadOfficeQualityAudit_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_HeadOfficeQualityAudit_Form_ItemCommand" OnDataBound="FormView_HeadOfficeQualityAudit_Form_DataBound" OnItemUpdating="FormView_HeadOfficeQualityAudit_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFacility">Facility
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_InsertFacility" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_InsertFacility" AppendDataBoundItems="True" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" SelectedValue='<%# Bind("Facility_Id") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Facility</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFunctionList">Function
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_InsertFunctionList" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_InsertFunctionList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("HQA_Finding_Function_List") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertFunctionList_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Function</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFindingDate">Finding Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_InsertFindingDate" runat="server" Width="75px" Text='<%# Bind("HQA_Finding_FindingDate","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertFindingDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertFindingDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertFindingDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertFindingDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertFindingDate" runat="server" TargetControlID="TextBox_InsertFindingDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormAuditor">Auditor
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_InsertAuditor" runat="server" Width="800px" Text='<%# Bind("HQA_Finding_Auditor") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormCriteriaList">Criteria No
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_InsertCriteriaList" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertCriteriaList_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Criteria</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormSubCriteriaList">Sub Criteria No
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_InsertSubCriteriaList" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Sub Criteria</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormClassificationList">Classification of Finding
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_InsertClassificationList" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_InsertClassificationList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("HQA_Finding_Classification_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Classification</asp:ListItem>
                          </asp:DropDownList>                          
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Description and Details of Finding
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description of Finding
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_InsertDescription" runat="server" TextMode="MultiLine" Rows="8" Width="800px" Text='<%# Bind("HQA_Finding_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="ImmediateAction" runat="server">
                        <td style="width: 175px;" id="FormImmediateAction">Immediate Action Taken
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_InsertImmediateAction" runat="server" TextMode="MultiLine" Rows="8" Width="800px" Text='<%# Bind("HQA_Finding_ImmediateAction") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="RootCause" runat="server">
                        <td style="width: 175px;" id="FormRootCause">Root Cause/s Identified
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_InsertRootCause" runat="server" TextMode="MultiLine" Rows="8" Width="800px" Text='<%# Bind("HQA_Finding_RootCause") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="CorrectiveAction" runat="server">
                        <td style="width: 175px;" id="FormCorrectiveAction">Corrective action to address root cause/s identified
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_InsertCorrectiveAction" runat="server" TextMode="MultiLine" Rows="8" Width="800px" Text='<%# Bind("HQA_Finding_CorrectiveAction") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Evaluation" runat="server">
                        <td style="width: 175px;" id="FormEvaluation">Evaluation of Effectiveness of Action
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_InsertEvaluation" runat="server" TextMode="MultiLine" Rows="8" Width="800px" Text='<%# Bind("HQA_Finding_Evaluation") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Tracking
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_InsertTrackingList" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_InsertTrackingList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("HQA_Finding_Tracking_List") %>' CssClass="Controls_DropDownList">
                          </asp:DropDownList>
                          <asp:Label ID="Label_InsertTrackingList" runat="server" Text="Not Yet Actioned"></asp:Label>
                          &nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertClear_Click" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Finding" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertCaptured" runat="server" CausesValidation="False" CommandName="Cancel" Text="Go to Captured Findings" CssClass="Controls_Button" OnClick="Button_InsertCaptured_Click" />&nbsp;
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
                        <td style="width: 175px;">Facility
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_EditFacility" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFunctionList">Function
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_EditFunctionList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_EditFunctionList" runat="server" Value='<%# Eval("HQA_Finding_Function_List") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Finding No
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_EditFindingNo" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFindingDate">Finding Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditFindingDate" runat="server" Width="75px" Text='<%# Bind("HQA_Finding_FindingDate","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditFindingDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />
                          <Ajax:CalendarExtender ID="CalendarExtender_EditFindingDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditFindingDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditFindingDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditFindingDate" runat="server" TargetControlID="TextBox_EditFindingDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          <asp:Label ID="Label_EditFindingDate" runat="server" Text='<%# Eval("HQA_Finding_FindingDate","{0:yyyy/MM/dd}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Financial Year
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_EditFinancialYear" runat="server" Text='<%# Bind("HQA_Finding_FinancialYear") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormAuditor">Auditor
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditAuditor" runat="server" Width="800px" Text='<%# Bind("HQA_Finding_Auditor") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:Label ID="Label_EditAuditor" runat="server" Text='<%# Eval("HQA_Finding_Auditor") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormCriteriaList">Criteria No
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_EditCriteriaList" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList" OnDataBound="DropDownList_EditCriteriaList_DataBound" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditCriteriaList_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Criteria</asp:ListItem>
                          </asp:DropDownList>
                          <asp:Label ID="Label_EditCriteriaList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_EditCriteriaList" runat="server" Value='<%# Bind("HQA_Finding_Criteria_List") %>' />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormSubCriteriaList">Sub Criteria No
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_EditSubCriteriaList" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Sub Criteria</asp:ListItem>
                          </asp:DropDownList>
                          <asp:Label ID="Label_EditSubCriteriaList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_EditSubCriteriaList" runat="server" Value='<%# Bind("HQA_Finding_SubCriteria_List") %>' />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormClassificationList">Classification of Finding
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_EditClassificationList" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_EditClassificationList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("HQA_Finding_Classification_List") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditClassificationList_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Classification</asp:ListItem>
                          </asp:DropDownList>
                          <asp:Label ID="Label_EditClassificationList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Description and Details of Finding
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description of Finding
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditDescription" runat="server" TextMode="MultiLine" Rows="8" Width="800px" Text='<%# Bind("HQA_Finding_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:Label ID="Label_EditDescription" runat="server" Text='<%# Eval("HQA_Finding_Description") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="ImmediateAction" runat="server">
                        <td style="width: 175px;" id="FormImmediateAction">Immediate Action Taken
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditImmediateAction" runat="server" TextMode="MultiLine" Rows="8" Width="800px" Text='<%# Bind("HQA_Finding_ImmediateAction") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="RootCause" runat="server">
                        <td style="width: 175px;" id="FormRootCause">Root Cause/s Identified
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditRootCause" runat="server" TextMode="MultiLine" Rows="8" Width="800px" Text='<%# Bind("HQA_Finding_RootCause") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="CorrectiveAction" runat="server">
                        <td style="width: 175px;" id="FormCorrectiveAction">Corrective action to address root cause/s identified
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditCorrectiveAction" runat="server" TextMode="MultiLine" Rows="8" Width="800px" Text='<%# Bind("HQA_Finding_CorrectiveAction") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="Evaluation" runat="server">
                        <td style="width: 175px;" id="FormEvaluation">Evaluation of Effectiveness of Action
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditEvaluation" runat="server" TextMode="MultiLine" Rows="8" Width="800px" Text='<%# Bind("HQA_Finding_Evaluation") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Tracking
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_EditTrackingList" runat="server" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_EditTrackingList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("HQA_Finding_Tracking_List") %>' CssClass="Controls_DropDownList" OnDataBound="DropDownList_EditTrackingList_DataBound" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditTrackingList_SelectedIndexChanged">
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Tracking Date
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_EditTrackingDate" runat="server" Text='<%# Bind("HQA_Finding_TrackingDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_EditTrackingDate" runat="server" Value='<%# Eval("HQA_Finding_TrackingDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="LateCloseOutList" runat="server">
                        <td style="width: 175px;" id="FormLateCloseOutList">Reason for late closing out
                        </td>
                        <td style="width: 825px;">
                          <asp:DropDownList ID="DropDownList_EditLateCloseOutList" runat="server" AppendDataBoundItems="true" DataSourceID="SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("HQA_Finding_LateCloseOut_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Reason</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="LateCloseOutListOther">
                        <td style="width: 175px;" id="FormLateCloseOutListOther">Other Reason
                        </td>
                        <td style="width: 825px;">
                          <asp:TextBox ID="TextBox_EditLateCloseOutListOther" runat="server" Text='<%# Bind("HQA_Finding_LateCloseOut_List_Other") %>' CssClass="Controls_TextBox" Rows="3" Width="800px" TextMode="MultiLine"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("HQA_Finding_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_EditCreatedDate" runat="server" Value='<%# Eval("HQA_Finding_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("HQA_Finding_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("HQA_Finding_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("HQA_Finding_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("HQA_Finding_IsActive") %>' />
                          <asp:Label ID="Label_EditIsActive" runat="server" Text='<%# (bool)(Eval("HQA_Finding_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="False" CommandName="Update" Text="Print Form" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditClear_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Finding" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditCaptured" runat="server" CausesValidation="False" Text="Go to Captured Findings" CssClass="Controls_Button" OnClick="Button_EditCaptured_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td style="width: 175px;">Facility
                        </td>
                        <td style="width: 825px;" colspan="3">
                          <asp:Label ID="Label_ItemFacility" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Function
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemFunctionList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Finding No
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemFindingNo" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Finding Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemFindingDate" runat="server" Text='<%# Eval("HQA_Finding_FindingDate","{0:yyyy/MM/dd}") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Financial Year
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemFinancialYear" runat="server" Text='<%# Bind("HQA_Finding_FinancialYear") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormAuditor">Auditor
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemAuditor" runat="server" Text='<%# Eval("HQA_Finding_Auditor") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormCriteriaList">Criteria No
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemCriteriaList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormSubCriteriaList">Sub Criteria No
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemSubCriteriaList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormClassificationList">Classification of Finding
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemClassificationList" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Description and Details of Finding
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description of Finding
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemDescription" runat="server" Text='<%# Eval("HQA_Finding_Description") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormImmediateAction">Immediate Action Taken
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemImmediateAction" runat="server" Text='<%# Eval("HQA_Finding_ImmediateAction") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRootCause">Root Cause/s Identified
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemRootCause" runat="server" Text='<%# Eval("HQA_Finding_RootCause") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormCorrectiveAction">Corrective action to address root cause/s identified
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemCorrectiveAction" runat="server" Text='<%# Eval("HQA_Finding_CorrectiveAction") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormEvaluation">Evaluation of Effectiveness of Action
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemEvaluation" runat="server" Text='<%# Eval("HQA_Finding_Evaluation") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Tracking
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemTrackingList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Tracking Date
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemTrackingDate" runat="server" Text='<%# Bind("HQA_Finding_TrackingDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="LateCloseOutList">
                        <td style="width: 175px;">Reason for late closing out
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemLateCloseOutList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemLateCloseOutList" runat="server" Value='<%# Eval("HQA_Finding_LateCloseOut_List") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="LateCloseOutListOther">
                        <td style="width: 175px;">Other Reason
                        </td>
                        <td style="width: 825px;">
                          <asp:Label ID="Label_ItemLateCloseOutListOther" runat="server" Text='<%# Bind("HQA_Finding_LateCloseOut_List_Other") %>'></asp:Label>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("HQA_Finding_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("HQA_Finding_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("HQA_Finding_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("HQA_Finding_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("HQA_Finding_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Form" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_ItemClear_Click" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                        <asp:Button ID="Button_ItemCaptured" runat="server" CausesValidation="False" Text="Go to Captured Findings" CssClass="Controls_Button" OnClick="Button_ItemCaptured_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_InsertFacility" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_InsertFunctionList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_InsertClassificationList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_InsertTrackingList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_EditClassificationList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_EditTrackingList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_HeadOfficeQualityAudit_Form" runat="server" OnInserted="SqlDataSource_HeadOfficeQualityAudit_Form_Inserted" OnUpdated="SqlDataSource_HeadOfficeQualityAudit_Form_Updated"></asp:SqlDataSource>
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
