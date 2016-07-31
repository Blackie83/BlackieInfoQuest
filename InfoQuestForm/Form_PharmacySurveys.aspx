<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_PharmacySurveys.aspx.cs" Inherits="InfoQuestForm.Form_PharmacySurveys" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Pharmacy Surveys</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_PharmacySurveys.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_PharmacySurveys" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_PharmacySurveys" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_PharmacySurveys" AssociatedUpdatePanelID="UpdatePanel_PharmacySurveys">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_PharmacySurveys" runat="server">
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
          <table id="TableForm" class="Table" style="width: 900px;" runat="server">
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
                <asp:FormView ID="FormView_PharmacySurveys_Form" runat="server" DataKeyNames="CreatedSurveys_Id" CssClass="FormView" DataSourceID="SqlDataSource_PharmacySurveys_Form" DefaultMode="Edit" OnItemCommand="FormView_PharmacySurveys_Form_ItemCommand" OnDataBound="FormView_PharmacySurveys_Form_DataBound" OnItemUpdating="FormView_PharmacySurveys_Form_ItemUpdating">
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Facility
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_EditFacility" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          <asp:HiddenField ID="HiddenField_EditControlCount" runat="server" />
                          <asp:HiddenField ID="HiddenField_EditControlAnswers" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Survey
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_EditSurvey" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">FY
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_EditFY" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Name
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_EditName" runat="server" Text='<%# Bind("CreatedSurveys_Name") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormUnit">Unit
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditUnit" runat="server" DataSourceID="SqlDataSource_PharmacySurveys_EditUnit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("Unit_Id") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDesignation">Designation
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditDesignation" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CreatedSurveys_Designation") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="text-align: center;">For each item identified below, select the number to the right that best fits your judgment of its quality.<br />
                          Use the rating scale to select the quality number.
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="padding: 0px; border-top-width: 2px; border-top-color: #f7f7f7; border-bottom-width: 2px; border-bottom-color: #f7f7f7;">
                          <asp:PlaceHolder ID="PlaceHolder_EditPharmacySurveys" runat="server"></asp:PlaceHolder>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormComments">Comments
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditComments" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CreatedSurveys_Comments") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormCompliment">Would like to compliment someone at the pharmacy
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditCompliment" runat="server" TextMode="MultiLine" Rows="3" Width="700px" Text='<%# Bind("CreatedSurveys_Compliment") %>' CssClass="Controls_TextBox"></asp:TextBox>
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("CreatedSurveys_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("CreatedSurveys_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("CreatedSurveys_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("CreatedSurveys_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditCancel_Click" />&nbsp;
                          <asp:Button ID="Button_EditGoToList" runat="server" CausesValidation="False" CommandName="GoTo" Text="Go To List" CssClass="Controls_Button" OnClick="Button_EditGoToList_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Save Survey" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
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
                        <td style="width: 170px;">Facility
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemFacility" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          <asp:HiddenField ID="HiddenField_ItemControlCount" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Survey
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemSurvey" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">FY
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemFY" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Name
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemName" runat="server" Text='<%# Bind("CreatedSurveys_Name") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Unit
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemUnit" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Designation
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemDesignation" runat="server" Text='<%# Bind("CreatedSurveys_Designation") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="padding: 0px; border-top-width: 2px; border-top-color: #f7f7f7; border-bottom-width: 2px; border-bottom-color: #f7f7f7;">
                          <asp:PlaceHolder ID="PlaceHolder_ItemPharmacySurveys" runat="server"></asp:PlaceHolder>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Comments
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemComments" runat="server" Text='<%# Bind("CreatedSurveys_Comments") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Would like to compliment someone at the pharmacy
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemCompliment" runat="server" Text='<%# Bind("CreatedSurveys_Compliment") %>'></asp:Label>
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
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("CreatedSurveys_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("CreatedSurveys_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("CreatedSurveys_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("CreatedSurveys_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Survey" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_ItemGoToList" runat="server" CausesValidation="False" CommandName="GoTo" Text="Go To List" CssClass="Controls_Button" OnClick="Button_ItemGoToList_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_EditUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_Form" runat="server" OnUpdated="SqlDataSource_PharmacySurveys_Form_Updated"></asp:SqlDataSource>
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
