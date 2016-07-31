<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_CollegeLearningAudit_Findings.aspx.cs" Inherits="InfoQuestForm.Form_CollegeLearningAudit_Findings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - College Learning Audit Findings</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_CollegeLearningAudit_Findings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_CollegeLearningAudit_Findings" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_CollegeLearningAudit_Findings" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_CollegeLearningAudit_Findings" AssociatedUpdatePanelID="UpdatePanel_CollegeLearningAudit_Findings">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_CollegeLearningAudit_Findings" runat="server">
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
          <table id="TableFindingsInfo" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_FindingsInfoHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="width: 115px">Facility
                    </td>
                    <td>
                      <asp:Label ID="Label_Facility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px;">Audit Date
                    </td>
                    <td>
                      <asp:Label ID="Label_Date" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 115px;">Completed
                    </td>
                    <td>
                      <asp:Label ID="Label_Completed" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableFindingsForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_FindingsHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_CollegeLearningAudit_Findings_Form" runat="server" Width="600px" DataKeyNames="CLA_Findings_Id" CssClass="FormView" DataSourceID="SqlDataSource_CollegeLearningAudit_Findings_Form" DefaultMode="Edit" OnItemCommand="FormView_CollegeLearningAudit_Findings_Form_ItemCommand" OnDataBound="FormView_CollegeLearningAudit_Findings_Form_DataBound" OnItemUpdating="FormView_CollegeLearningAudit_Findings_Form_ItemUpdating">
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">System / Process
                        </td>
                        <td>
                          <asp:Label ID="Label_EditSystem" runat="server" Text='<%# Bind("CLA_Findings_System") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Element
                        </td>
                        <td>
                          <asp:Label ID="Label_EditElement" runat="server" Text='<%# Bind("CLA_Findings_Element") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Criteria No
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCriteriaNo" runat="server" Text='<%# Bind("CLA_Findings_CriteriaNo") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Measurement Criteria
                        </td>
                        <td>
                          <asp:Label ID="Label_EditMeasurementCriteria" runat="server" Text='<%# Bind("CLA_Findings_MeasurementCriteria") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Category
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCategory" runat="server" Text='<%# Bind("CLA_Findings_Category") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_EditCategory" runat="server" Value='<%# Eval("CLA_Findings_Category") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Comments
                        </td>
                        <td>
                          <asp:Label ID="Label_EditComments" runat="server" Text='<%# Bind("CLA_Findings_Comments") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <%--<tr>
                        <td style="width: 175px;">Score
                        </td>
                        <td>
                          <asp:Label ID="Label_EditScore" runat="server" Text='<%# Bind("CLA_Findings_Score") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Core Standard
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCoreStandard" runat="server" Text='<%# Bind("CLA_Findings_CoreStandard") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>--%>
                      <tr>
                        <td id="FormRootCause" style="width: 175px;">Root Cause
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditRootCause" runat="server" Text='<%# Bind("CLA_Findings_RootCause") %>' CssClass="Controls_TextBox" Rows="3" Width="400px" TextMode="MultiLine"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormActions" style="width: 175px;">Actions
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditActions" runat="server" Text='<%# Bind("CLA_Findings_Actions") %>' CssClass="Controls_TextBox" Rows="3" Width="400px" TextMode="MultiLine"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormResponsiblePerson" style="width: 175px;">Responsible Person / s
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditResponsiblePerson" runat="server" Text='<%# Bind("CLA_Findings_ResponsiblePerson") %>' CssClass="Controls_TextBox" Rows="3" Width="400px" TextMode="MultiLine"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormDueDate" style="width: 175px;">Due Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditDueDate" runat="server" Width="75px" Text='<%# Bind("CLA_Findings_DueDate","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditDueDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                    <Ajax:CalendarExtender ID="CalendarExtender_EditDueDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDueDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDueDate">
                    </Ajax:CalendarExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td id="FormActionsEffective" style="width: 175px;">Actions Effective
                        </td>
                        <td>
                          <asp:DropDownList ID="DropDownList_EditActionsEffective" runat="server" CssClass="Controls_DropDownList" SelectedValue='<%# Bind("CLA_Findings_ActionsEffective") %>'>
                            <asp:ListItem Value="">Select Value</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Tracking
                        </td>
                        <td>
                          <asp:DropDownList ID="DropDownList_EditTrackingList" runat="server" DataSourceID="SqlDataSource_CollegeLearningAudit_Findings_EditTrackingList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CLA_Findings_Tracking_List") %>' CssClass="Controls_DropDownList" OnDataBound="DropDownList_EditTrackingList_DataBound" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditTrackingList_SelectedIndexChanged">
                          </asp:DropDownList>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Tracking Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditTrackingDate" runat="server" Text='<%# Bind("CLA_Findings_TrackingDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_EditTrackingDate" runat="server" Value='<%# Eval("CLA_Findings_TrackingDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="LateClosingOutCNCList" runat="server">
                        <td id="FormLateClosingOutCNCList" style="width: 175px;">Reason for late Closing Out of CNC
                        </td>
                        <td>
                          <asp:DropDownList ID="DropDownList_EditLateClosingOutCNCList" runat="server" AppendDataBoundItems="true" DataSourceID="SqlDataSource_CollegeLearningAudit_Findings_EditLateClosingOutCNCList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CLA_Findings_LateClosingOutCNC_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Reason</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="LateClosingOutCNCListOther">
                        <td id="FormLateClosingOutCNCListOther" style="width: 175px;">Other Reason
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditLateClosingOutCNCListOther" runat="server" Text='<%# Bind("CLA_Findings_LateClosingOutCNC_List_Other") %>' CssClass="Controls_TextBox" Rows="3" Width="400px" TextMode="MultiLine"></asp:TextBox>&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("CLA_Findings_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_EditCreatedDate" runat="server" Value='<%# Eval("CLA_Findings_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("CLA_Findings_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("CLA_Findings_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("CLA_Findings_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditPrint" runat="server" CommandName="Update" Text="Print Finding" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CommandName="Update" Text="Update Finding" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditCancel" runat="server" CommandName="Cancel" Text="Back to Findings List" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">System / Process
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemSystem" runat="server" Text='<%# Bind("CLA_Findings_System") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Element
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemElement" runat="server" Text='<%# Bind("CLA_Findings_Element") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Criteria No
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCriteriaNo" runat="server" Text='<%# Bind("CLA_Findings_CriteriaNo") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Measurement Criteria
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemMeasurementCriteria" runat="server" Text='<%# Bind("CLA_Findings_MeasurementCriteria") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Category
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCategory" runat="server" Text='<%# Bind("CLA_Findings_Category") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Comments
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemComments" runat="server" Text='<%# Bind("CLA_Findings_Comments") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <%--<tr>
                        <td style="width: 175px;">Score
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemScore" runat="server" Text='<%# Bind("CLA_Findings_Score") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Core Standard
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCoreStandard" runat="server" Text='<%# Bind("CLA_Findings_CoreStandard") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>--%>
                      <tr>
                        <td style="width: 175px;">Root Cause
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemRootCause" runat="server" Text='<%# Bind("CLA_Findings_RootCause") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Actions
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemActions" runat="server" Text='<%# Bind("CLA_Findings_Actions") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Responsible Person / s
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemResponsiblePerson" runat="server" Text='<%# Bind("CLA_Findings_ResponsiblePerson") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Due Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDueDate" runat="server" Text='<%# Bind("CLA_Findings_DueDate","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Actions Effective
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemActionsEffective" runat="server" Text='<%# Bind("CLA_Findings_ActionsEffective") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Tracking
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemTrackingList" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Tracking Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemTrackingDate" runat="server" Text='<%# Bind("CLA_Findings_TrackingDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="LateClosingOutCNCList">
                        <td style="width: 175px;">Reason for late Closing Out of CNC
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemLateClosingOutCNCList" runat="server" Text=""></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemLateClosingOutCNCList" runat="server" Value='<%# Eval("CLA_Findings_LateClosingOutCNC_List") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="LateClosingOutCNCListOther">
                        <td style="width: 175px;">Other Reason
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemLateClosingOutCNCListOther" runat="server" Text='<%# Bind("CLA_Findings_LateClosingOutCNC_List_Other") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemLateClosingOutCNCListOther" runat="server" Value='<%# Eval("CLA_Findings_LateClosingOutCNC_List_Other") %>' />
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
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("CLA_Findings_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("CLA_Findings_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("CLA_Findings_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("CLA_Findings_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2" style="text-align: right;">
                          <asp:Button ID="Button_ItemPrint" runat="server" CommandName="Print" Text="Print Finding" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail" runat="server" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2" style="text-align: right;">
                          <asp:Button ID="Button_ItemCancel" runat="server" CommandName="Cancel" Text="Back to Findings List" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_CollegeLearningAudit_Findings_EditTrackingList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CollegeLearningAudit_Findings_EditLateClosingOutCNCList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_CollegeLearningAudit_Findings_Form" runat="server" OnUpdated="SqlDataSource_CollegeLearningAudit_Findings_Form_Updated"></asp:SqlDataSource>
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
