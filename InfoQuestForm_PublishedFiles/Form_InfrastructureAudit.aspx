<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_InfrastructureAudit.aspx.cs" Inherits="InfoQuestForm.Form_InfrastructureAudit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Infrastructure Audit</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_InfrastructureAudit" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_InfrastructureAudit" runat="server">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_InfrastructureAudit" AssociatedUpdatePanelID="UpdatePanel_InfrastructureAudit">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_InfrastructureAudit" runat="server">
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
          <table id="TableReviewForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_ReviewHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_InfrastructureAudit_Form" runat="server" Width="600px" DataKeyNames="InfrastructureAudit_Id" CssClass="FormView" DataSourceID="SqlDataSource_InfrastructureAudit_Form" DefaultMode="Edit" OnItemCommand="FormView_InfrastructureAudit_Form_ItemCommand" OnDataBound="FormView_InfrastructureAudit_Form_DataBound" OnItemUpdating="FormView_InfrastructureAudit_Form_ItemUpdating">
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Facility
                        </td>
                        <td>
                          <asp:Label ID="Label_EditFacility" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Audit Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditDate" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Audit
                        </td>
                        <td>
                          <asp:LinkButton ID="LinkButton_EditFile" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("InfrastructureAudit_ZipFileName")) %>' CommandArgument='<%# Eval("InfrastructureAudit_Id") %>' OnDataBinding="LinkButton_EditFile_DataBinding"></asp:LinkButton>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Completed
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditCompleted" runat="server" Checked='<%# Bind("InfrastructureAudit_Completed") %>' />&nbsp;
                          <asp:Label ID="Label_EditCompleted" runat="server" Text='<%# (bool)(Eval("InfrastructureAudit_Completed"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Completed Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCompletedDate" runat="server" Text='<%# Bind("InfrastructureAudit_CompletedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("InfrastructureAudit_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("InfrastructureAudit_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("InfrastructureAudit_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("InfrastructureAudit_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("InfrastructureAudit_IsActive") %>' />
                          <asp:Label ID="Label_EditIsActive" runat="server" Text='<%# (bool)(Eval("InfrastructureAudit_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditUpdate" runat="server" CommandName="Update" Text="Update Audit" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditCancel" runat="server" CommandName="Cancel" Text="Back to Audit List" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Facility
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemFacility" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Audit Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemDate" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Audit
                        </td>
                        <td>
                          <asp:LinkButton ID="LinkButton_ItemFile" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("InfrastructureAudit_ZipFileName")) %>' CommandArgument='<%# Eval("InfrastructureAudit_Id") %>' OnDataBinding="LinkButton_ItemFile_DataBinding"></asp:LinkButton>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Completed
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCompleted" runat="server" Text='<%# (bool)(Eval("InfrastructureAudit_Completed"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Completed Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCompletedDate" runat="server" Text='<%# Bind("InfrastructureAudit_CompletedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
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
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("InfrastructureAudit_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("InfrastructureAudit_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("InfrastructureAudit_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("InfrastructureAudit_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("InfrastructureAudit_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_ItemCancel" runat="server" CommandName="Cancel" Text="Back to Audit List" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_InfrastructureAudit_Form" runat="server" OnUpdated="SqlDataSource_InfrastructureAudit_Form_Updated"></asp:SqlDataSource>
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
