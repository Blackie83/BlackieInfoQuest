<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_PharmacySurveys_LoadedSurveys.aspx.cs" Inherits="InfoQuestForm.Form_PharmacySurveys_LoadedSurveys" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Pharmacy Surveys Loaded Surveys</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_PharmacySurveys_LoadedSurveys.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_PharmacySurveys_LoadedSurveys" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_PharmacySurveys_LoadedSurveys" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_PharmacySurveys_LoadedSurveys" AssociatedUpdatePanelID="UpdatePanel_PharmacySurveys_LoadedSurveys">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_PharmacySurveys_LoadedSurveys" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td>
                <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
              </td>
              <td style="width: 25px"></td>
              <td class="Form_Header">
                <asp:Label ID="Label_Title" runat="server"></asp:Label>
              </td>
              <td style="width: 25px"></td>
              <td>&nbsp;
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableNavigation" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_HeadingNavigation" runat="server" Text="Survey Navigation"></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="width:150px;">Survey</td>
                    <td style="width:200px;">Section</td>
                    <td style="width:650px;">Question</td>
                  </tr>
                  <tr>
                    <td>
                      <asp:HyperLink ID="Hyperlink_NavigationSurveys" Text="" runat="server"></asp:HyperLink>&nbsp;
                    </td>
                    <td>
                      <asp:HyperLink ID="Hyperlink_NavigationSections" Text="" runat="server"></asp:HyperLink>&nbsp;
                    </td>
                    <td>
                      <asp:HyperLink ID="Hyperlink_NavigationQuestions" Text="" runat="server"></asp:HyperLink>&nbsp;
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="3">
                      <asp:Button ID="Button_CreateSurvey" runat="server" Text="Create New Survey" CssClass="Controls_Button" OnClick="Button_CreateSurvey_Click" />
                      <asp:Button ID="Button_AddSection" runat="server" Text="Add Section to Survey" CssClass="Controls_Button" OnClick="Button_AddSection_Click" />
                      <asp:Button ID="Button_AddQuestion" runat="server" Text="Add Question to Section" CssClass="Controls_Button" OnClick="Button_AddQuestion_Click" />
                      <asp:Button ID="Button_AddAnswer" runat="server" Text="Add Answer to Question" CssClass="Controls_Button" OnClick="Button_AddAnswer_Click" />
                      <asp:Button ID="Button_GoToLoadedSurveys" runat="server" Text="Go to Loaded Surveys" CssClass="Controls_Button" OnClick="Button_GoToLoadedSurveys_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivFormSurveys" runat="server">
            &nbsp;
          </div>
          <table id="TableFormSurveys" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_HeadingFormSurveys" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_PharmacySurveys_LoadedSurveys_Form" runat="server" DataKeyNames="LoadedSurveys_Id" CssClass="FormView" DataSourceID="SqlDataSource_PharmacySurveys_LoadedSurveys_Form" OnItemInserting="FormView_PharmacySurveys_LoadedSurveys_Form_ItemInserting" DefaultMode="Insert" OnDataBound="FormView_PharmacySurveys_LoadedSurveys_Form_DataBound" OnItemUpdating="FormView_PharmacySurveys_LoadedSurveys_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLoadedSurveysName">Name
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertName" runat="server" Width="800px" Rows="2" TextMode="MultiLine" Text='<%# Bind("LoadedSurveys_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" EnableViewState="false" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLoadedSurveysFY">FY
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_InsertFY" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_InsertFY" runat="server" />
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("LoadedSurveys_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("LoadedSurveys_CreatedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("LoadedSurveys_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("LoadedSurveys_ModifiedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("LoadedSurveys_IsActive") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertClear_Click_LoadedSurveys" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Survey" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLoadedSurveysName">Name
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditName" runat="server" Width="800px" Rows="2" TextMode="MultiLine" Text='<%# Bind("LoadedSurveys_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLoadedSurveysFY">FY
                        </td>
                        <td style="width: 830px;">
                          <asp:Label ID="Label_EditFY" runat="server" Text='<%# Bind("LoadedSurveys_FY") %>'></asp:Label>&nbsp;
                          <asp:HiddenField ID="HiddenField_EditFY" runat="server" Value='<%# Bind("LoadedSurveys_FY") %>' />
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("LoadedSurveys_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("LoadedSurveys_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("LoadedSurveys_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("LoadedSurveys_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("LoadedSurveys_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditClear_Click_LoadedSurveys" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Survey" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click_LoadedSurveys" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedSurveys_Form" runat="server" OnInserted="SqlDataSource_PharmacySurveys_LoadedSurveys_Form_Inserted" OnUpdated="SqlDataSource_PharmacySurveys_LoadedSurveys_Form_Updated"></asp:SqlDataSource>
              </td>
            </tr>
          </table>
          <div id="DivGridSections" runat="server">
            &nbsp;
          </div>
          <table id="TableGridSections" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_HeadingGridSections" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>Total Records:
                    <asp:Label ID="Label_TotalRecords_Sections" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_PharmacySurveys_LoadedSections_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_PharmacySurveys_LoadedSections_List" Width="100%" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnPreRender="GridView_PharmacySurveys_LoadedSections_List_PreRender">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <RowStyle CssClass="GridView_RowStyle" />
                        <FooterStyle CssClass="GridView_FooterStyle" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>
                                <asp:Button ID="Button_AddSections" runat="server" Text="Add Section to Survey" CssClass="Controls_Button" OnClick="Button_AddSections_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td>No records
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>&nbsp;
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="text-align:center;">
                                <asp:Button ID="Button_AddSections" runat="server" Text="Add Section to Survey" CssClass="Controls_Button" OnClick="Button_AddSections_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="" ItemStyle-Width="50px">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink_Sections(Eval("LoadedSections_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="LoadedSections_Name" HeaderText="Name" ReadOnly="True" SortExpression="LoadedSections_Name" />
                          <asp:BoundField DataField="LoadedSections_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="LoadedSections_IsActive" ItemStyle-Width="50px" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedSections_List" runat="server" OnSelected="SqlDataSource_PharmacySurveys_LoadedSections_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivFormSections" runat="server">
            &nbsp;
          </div>
          <table id="TableFormSections" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_HeadingFormSections" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_PharmacySurveys_LoadedSections_Form" runat="server" DataKeyNames="LoadedSections_Id" CssClass="FormView" DataSourceID="SqlDataSource_PharmacySurveys_LoadedSections_Form" OnItemInserting="FormView_PharmacySurveys_LoadedSections_Form_ItemInserting" DefaultMode="Insert" OnItemUpdating="FormView_PharmacySurveys_LoadedSections_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLoadedSectionsName">Name
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertName" runat="server" Width="800px" Rows="2" TextMode="MultiLine" Text='<%# Bind("LoadedSections_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" EnableViewState="false" />
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("LoadedSections_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("LoadedSections_CreatedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("LoadedSections_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("LoadedSections_ModifiedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("LoadedSections_IsActive") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertClear_Click_LoadedSections" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Section" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLoadedSectionsName">Name
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditName" runat="server" Width="800px" Rows="2" TextMode="MultiLine" Text='<%# Bind("LoadedSections_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("LoadedSections_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("LoadedSections_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("LoadedSections_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("LoadedSections_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("LoadedSections_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditClear_Click_LoadedSections" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Section" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click_LoadedSections" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedSections_Form" runat="server" OnInserted="SqlDataSource_PharmacySurveys_LoadedSections_Form_Inserted" OnUpdated="SqlDataSource_PharmacySurveys_LoadedSections_Form_Updated"></asp:SqlDataSource>
              </td>
            </tr>
          </table>
          <div id="DivGridQuestions" runat="server">
            &nbsp;
          </div>
          <table id="TableGridQuestions" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_HeadingGridQuestions" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>Total Records:
                    <asp:Label ID="Label_TotalRecords_Questions" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_PharmacySurveys_LoadedQuestions_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_PharmacySurveys_LoadedQuestions_List" Width="100%" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnPreRender="GridView_PharmacySurveys_LoadedQuestions_List_PreRender">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <RowStyle CssClass="GridView_RowStyle" />
                        <FooterStyle CssClass="GridView_FooterStyle" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>
                                <asp:Button ID="Button_AddQuestions" runat="server" Text="Add Question to Section" CssClass="Controls_Button" OnClick="Button_AddQuestions_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td>No records
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>&nbsp;
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="text-align:center;">
                                <asp:Button ID="Button_AddQuestions" runat="server" Text="Add Question to Section" CssClass="Controls_Button" OnClick="Button_AddQuestions_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="" ItemStyle-Width="50px">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink_Questions(Eval("LoadedQuestions_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="LoadedQuestions_Name" HeaderText="Name" ReadOnly="True" SortExpression="LoadedQuestions_Name" />
                          <asp:BoundField DataField="LoadedQuestions_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="LoadedQuestions_IsActive" ItemStyle-Width="50px" />
                          <asp:BoundField DataField="LoadedQuestions_Dependency_ShowHide_LoadedQuestionsName" HeaderText="Dependency Question" ReadOnly="True" SortExpression="LoadedQuestions_Dependency_ShowHide_LoadedQuestionsName" />
                          <asp:BoundField DataField="LoadedQuestions_Dependency_ShowHide_LoadedQuestionsIsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="LoadedQuestions_Dependency_ShowHide_LoadedQuestionsIsActive" />
                          <asp:BoundField DataField="LoadedQuestions_Dependency_ShowHide_LoadedAnswersName" HeaderText="Dependency Answer" ReadOnly="True" SortExpression="LoadedQuestions_Dependency_ShowHide_LoadedAnswersName" />
                          <asp:BoundField DataField="LoadedQuestions_Dependency_ShowHide_LoadedAnswersIsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="LoadedQuestions_Dependency_ShowHide_LoadedAnswersIsActive" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedQuestions_List" runat="server" OnSelected="SqlDataSource_PharmacySurveys_LoadedAnswers_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivFormQuestions" runat="server">
            &nbsp;
          </div>
          <table id="TableFormQuestions" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_HeadingFormQuestions" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_PharmacySurveys_LoadedQuestions_Form" runat="server" DataKeyNames="LoadedQuestions_Id" CssClass="FormView" DataSourceID="SqlDataSource_PharmacySurveys_LoadedQuestions_Form" OnItemInserting="FormView_PharmacySurveys_LoadedQuestions_Form_ItemInserting" DefaultMode="Insert" OnDataBound="FormView_PharmacySurveys_LoadedQuestions_Form_DataBound" OnItemUpdating="FormView_PharmacySurveys_LoadedQuestions_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLoadedQuestionsName">Name
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertName" runat="server" Width="800px" Rows="2" TextMode="MultiLine" Text='<%# Bind("LoadedQuestions_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" EnableViewState="false" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Question Dependency
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDependencySections">Section
                        </td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertDependencyShowHideLoadedAnswersId_Section" runat="server" DataSourceID="SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Section" AppendDataBoundItems="true" DataTextField="LoadedSections_Name" DataValueField="LoadedSections_Id" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertDependencyShowHideLoadedAnswersId_Section_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Section</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDependencyQuestions">Question
                        </td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertDependencyShowHideLoadedAnswersId_Question" runat="server" Width="800px" DataSourceID="SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Question" AppendDataBoundItems="true" DataTextField="LoadedQuestions_Name" DataValueField="LoadedQuestions_Id" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertDependencyShowHideLoadedAnswersId_Question_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Question</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDependencyAnswers">Answer
                        </td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_InsertDependencyShowHideLoadedAnswersId" runat="server" DataSourceID="SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId" AppendDataBoundItems="true" DataTextField="LoadedAnswers_Name" DataValueField="LoadedAnswers_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Answer</asp:ListItem>
                          </asp:DropDownList>
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("LoadedQuestions_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("LoadedQuestions_CreatedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("LoadedQuestions_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("LoadedQuestions_ModifiedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("LoadedQuestions_IsActive") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertClear_Click_LoadedQuestions" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Question" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLoadedQuestionsName">Name
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditName" runat="server" Width="800px" Rows="2" TextMode="MultiLine" Text='<%# Bind("LoadedQuestions_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Question Dependency
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDependencySections">Section
                        </td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditDependencyShowHideLoadedAnswersId_Section" runat="server" DataSourceID="SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Section" AppendDataBoundItems="true" DataTextField="LoadedSections_Name" DataValueField="LoadedSections_Id" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditDependencyShowHideLoadedAnswersId_Section_SelectedIndexChanged" OnDataBound="DropDownList_EditDependencyShowHideLoadedAnswersId_Section_DataBound">
                            <asp:ListItem Value="">Select Section</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDependencyQuestions">Question
                        </td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditDependencyShowHideLoadedAnswersId_Question" runat="server" Width="800px" DataSourceID="SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Question" AppendDataBoundItems="true" DataTextField="LoadedQuestions_Name" DataValueField="LoadedQuestions_Id" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditDependencyShowHideLoadedAnswersId_Question_SelectedIndexChanged" OnDataBound="DropDownList_EditDependencyShowHideLoadedAnswersId_Question_DataBound">
                            <asp:ListItem Value="">Select Question</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormDependencyAnswers">Answer
                        </td>
                        <td style="width: 830px;">
                          <asp:DropDownList ID="DropDownList_EditDependencyShowHideLoadedAnswersId" runat="server" DataSourceID="SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId" AppendDataBoundItems="true" DataTextField="LoadedAnswers_Name" DataValueField="LoadedAnswers_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Answer</asp:ListItem>
                          </asp:DropDownList>
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("LoadedQuestions_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("LoadedQuestions_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("LoadedQuestions_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("LoadedQuestions_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("LoadedQuestions_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditClear_Click_LoadedQuestions" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Question" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click_LoadedQuestions" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Section" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Question" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Section" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Question" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedQuestions_Form" runat="server" OnInserted="SqlDataSource_PharmacySurveys_LoadedQuestions_Form_Inserted" OnUpdated="SqlDataSource_PharmacySurveys_LoadedQuestions_Form_Updated"></asp:SqlDataSource>
              </td>
            </tr>
          </table>
          <div id="DivGridAnswers" runat="server">
            &nbsp;
          </div>
          <table id="TableGridAnswers" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_HeadingGridAnswers" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>Total Records:
                    <asp:Label ID="Label_TotalRecords_Answers" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_PharmacySurveys_LoadedAnswers_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_PharmacySurveys_LoadedAnswers_List" Width="100%" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnPreRender="GridView_PharmacySurveys_LoadedAnswers_List_PreRender">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <RowStyle CssClass="GridView_RowStyle" />
                        <FooterStyle CssClass="GridView_FooterStyle" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>
                                <asp:Button ID="Button_AddAnswers" runat="server" Text="Add Answer to Question" CssClass="Controls_Button" OnClick="Button_AddAnswers_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td>No records
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>&nbsp;
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="text-align:center;">
                                <asp:Button ID="Button_AddAnswers" runat="server" Text="Add Answer to Question" CssClass="Controls_Button" OnClick="Button_AddAnswers_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="" ItemStyle-Width="50px">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink_Answers(Eval("LoadedAnswers_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="LoadedAnswers_Name" HeaderText="Name" ReadOnly="True" SortExpression="LoadedAnswers_Name" />
                          <asp:BoundField DataField="LoadedAnswers_Score" HeaderText="Score" ReadOnly="True" SortExpression="LoadedAnswers_Score" ItemStyle-Width="50px" />
                          <asp:BoundField DataField="LoadedAnswers_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="LoadedAnswers_IsActive" ItemStyle-Width="50px" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedAnswers_List" runat="server" OnSelected="SqlDataSource_PharmacySurveys_LoadedAnswers_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivFormAnswers" runat="server">
            &nbsp;
          </div>
          <table id="TableFormAnswers" class="Table" style="width: 1000px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_HeadingFormAnswers" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_PharmacySurveys_LoadedAnswers_Form" runat="server" DataKeyNames="LoadedAnswers_Id" CssClass="FormView" DataSourceID="SqlDataSource_PharmacySurveys_LoadedAnswers_Form" OnItemInserting="FormView_PharmacySurveys_LoadedAnswers_Form_ItemInserting" DefaultMode="Insert" OnItemUpdating="FormView_PharmacySurveys_LoadedAnswers_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLoadedAnswersName">Name
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertName" runat="server" Width="800px" Rows="2" TextMode="MultiLine" Text='<%# Bind("LoadedAnswers_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" EnableViewState="false" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLoadedAnswersScore">Score
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_InsertScore" runat="server" Width="200px" Text='<%# Bind("LoadedAnswers_Score") %>' CssClass="Controls_TextBox"></asp:TextBox>
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("LoadedAnswers_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("LoadedAnswers_CreatedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("LoadedAnswers_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("LoadedAnswers_ModifiedBy") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("LoadedAnswers_IsActive") %>' EnableViewState="false"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertClear_Click_LoadedAnswers" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Answer" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLoadedAnswersName">Name
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditName" runat="server" Width="800px" Rows="2" TextMode="MultiLine" Text='<%# Bind("LoadedAnswers_Name") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLoadedAnswersScore">Score
                        </td>
                        <td style="width: 830px;">
                          <asp:TextBox ID="TextBox_EditScore" runat="server" Width="200px" Text='<%# Bind("LoadedAnswers_Score") %>' CssClass="Controls_TextBox"></asp:TextBox>
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("LoadedAnswers_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("LoadedAnswers_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("LoadedAnswers_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("LoadedAnswers_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("LoadedAnswers_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditClear_Click_LoadedAnswers" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Answer" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click_LoadedAnswers" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedAnswers_Form" runat="server" OnInserted="SqlDataSource_PharmacySurveys_LoadedAnswers_Form_Inserted" OnUpdated="SqlDataSource_PharmacySurveys_LoadedAnswers_Form_Updated"></asp:SqlDataSource>
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
