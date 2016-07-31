<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_Isidima_Rules.aspx.cs" Inherits="InfoQuestForm.Form_Isidima_Rules" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Isidima Rules</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Isidima_Rules" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Isidima_Rules" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Isidima_Rules" AssociatedUpdatePanelID="UpdatePanel_Isidima_Rules">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Isidima_Rules" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td>
                <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Esidimeni/85_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
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
                      <asp:Label ID="Label_SearchErrorMessage" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td>Section
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Section" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Isidima_Rules_Section" DataTextField="ListItem_Name" DataValueField="ListItem_Id">
                        <asp:ListItem Value="">Select Section</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Isidima_Rules_Section" runat="server"></asp:SqlDataSource>
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
                      <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" />&nbsp;
                    <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableRules" runat="server" class="Table">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridHeading" runat="server" Text=""></asp:Label>
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
                    <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_Isidima_Rules" runat="server" PageSize="1000" AllowPaging="true" Width="1000px" DataSourceID="SqlDataSource_Isidima_Rules" AutoGenerateColumns="false" CssClass="GridView" BorderWidth="0px" ShowFooter="True" OnPreRender="GridView_Isidima_Rules_PreRender">
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
                        <FooterStyle CssClass="GridView_RowStyle" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                        <EmptyDataRowStyle CssClass="GridView_RowStyle" />
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td colspan="7">&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <strong>Section</strong>
                              </td>
                              <td>
                                <strong>Id</strong>
                              </td>
                              <td>
                                <strong>Question</strong>
                              </td>
                              <td>
                                <strong>Yes Weight</strong>
                              </td>
                              <td>
                                <strong>No Weight</strong>
                              </td>
                              <td>
                                <strong>Is Active</strong>
                              </td>
                              <td rowspan="2">
                                <asp:Button ID="Button_Insert" runat="server" Text="Insert" CssClass="Controls_Button" OnClick="Button_Insert_Click" /><br />
                                <asp:Label ID="Label_InsertValidationMessage" runat="server" Visible="false" Text="Question, Yes Weight and No Weight is Required" CssClass="Controls_Validation" Width="135px"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <asp:Label ID="Label_InsertSectionList" runat="server" Text="" Width="150px" OnDataBinding="Label_InsertSectionList_DataBinding"></asp:Label>&nbsp;
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertQuestionId" runat="server" Text="" Width="50px" OnDataBinding="Label_InsertQuestionId_DataBinding"></asp:Label>&nbsp;
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertQuestion" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_Question") %>' CssClass="Controls_TextBox"></asp:TextBox>
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertQuestionYesWeight" runat="server" Width="50px" Text='<%# Bind("Isidima_Rules_Question_YesWeight") %>' CssClass="Controls_TextBox"></asp:TextBox>
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertQuestionNoWeight" runat="server" Width="50px" Text='<%# Bind("Isidima_Rules_Question_NoWeight") %>' CssClass="Controls_TextBox"></asp:TextBox>
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertIsActive" runat="server" Text="" Width="50px"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2" style="width: 200px;">
                                <strong>Professional Action</strong>
                              </td>
                              <td style="width: 200px;">
                                <strong>Action Taken</strong>
                              </td>
                              <td colspan="2" style="width: 200px;">
                                <strong>Category Responsible</strong>
                              </td>
                              <td style="width: 200px;">
                                <strong>Material Available</strong>
                              </td>
                              <td style="width: 200px;">
                                <strong>Risk</strong>
                              </td>
                            </tr>
                            <tr>
                              <td colspan="2">
                                <asp:TextBox ID="TextBox_InsertProfessionalAction" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_ProfessionalAction") %>' CssClass="Controls_TextBox"></asp:TextBox>
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertActionTaken" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_ActionTaken") %>' CssClass="Controls_TextBox"></asp:TextBox>
                              </td>
                              <td colspan="2">
                                <asp:TextBox ID="TextBox_InsertCategoryResponsible" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_CategoryResponsible") %>' CssClass="Controls_TextBox"></asp:TextBox>
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertMaterialAvailable" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_MaterialAvailable") %>' CssClass="Controls_TextBox"></asp:TextBox>
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertRisk" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_Risk") %>' CssClass="Controls_TextBox"></asp:TextBox>
                              </td>
                            </tr>
                            <tr class="Pager">
                              <td colspan="7">&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <table>
                                <tr>
                                  <td>
                                    <strong>Section</strong>
                                  </td>
                                  <td>
                                    <strong>Id</strong>
                                  </td>
                                  <td>
                                    <strong>Question</strong>
                                  </td>
                                  <td>
                                    <strong>Yes Weight</strong>
                                  </td>
                                  <td>
                                    <strong>No Weight</strong>
                                  </td>
                                  <td>
                                    <strong>Is Active</strong>
                                  </td>
                                  <td rowspan="2">
                                    <asp:Button ID="Button_Update" runat="server" Text="Update" CssClass="Controls_Button" OnClick="Button_Update_Click" /><br />
                                    <asp:Label ID="Label_EditValidationMessage" runat="server" Visible="false" Text="Question, Yes Weight and No Weight is Required" CssClass="Controls_Validation" Width="135px"></asp:Label>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:HiddenField ID="HiddenField_EditRulesId" runat="server" Value='<%# Bind("Isidima_Rules_Id") %>' />
                                    <asp:Label ID="Label_EditSectionList" runat="server" Text="" Width="150px" OnDataBinding="Label_EditSectionList_DataBinding"></asp:Label>&nbsp;
                                  </td>
                                  <td>
                                    <asp:Label ID="Label_EditQuestionId" runat="server" Text='<%# Bind("Isidima_Rules_QuestionId") %>' Width="50px"></asp:Label>&nbsp;
                                  </td>
                                  <td>
                                    <asp:TextBox ID="TextBox_EditQuestion" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_Question") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:TextBox ID="TextBox_EditQuestionYesWeight" runat="server" Width="50px" Text='<%# Bind("Isidima_Rules_Question_YesWeight") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:TextBox ID="TextBox_EditQuestionNoWeight" runat="server" Width="50px" Text='<%# Bind("Isidima_Rules_Question_NoWeight") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("Isidima_Rules_IsActive") %>' />
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2" style="width: 200px;">
                                    <strong>Professional Action</strong>
                                  </td>
                                  <td style="width: 200px;">
                                    <strong>Action Taken</strong>
                                  </td>
                                  <td colspan="2" style="width: 200px;">
                                    <strong>Category Responsible</strong>
                                  </td>
                                  <td style="width: 200px;">
                                    <strong>Material Available</strong>
                                  </td>
                                  <td style="width: 200px;">
                                    <strong>Risk</strong>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2">
                                    <asp:TextBox ID="TextBox_EditProfessionalAction" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_ProfessionalAction") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:TextBox ID="TextBox_EditActionTaken" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_ActionTaken") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:TextBox ID="TextBox_EditCategoryResponsible" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_CategoryResponsible") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:TextBox ID="TextBox_EditMaterialAvailable" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_MaterialAvailable") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:TextBox ID="TextBox_EditRisk" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_Risk") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                </tr>
                              </table>
                            </ItemTemplate>
                            <FooterTemplate>
                              <table>
                                <tr>
                                  <td colspan="7">&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <strong>Section</strong>
                                  </td>
                                  <td>
                                    <strong>Id</strong>
                                  </td>
                                  <td>
                                    <strong>Question</strong>
                                  </td>
                                  <td>
                                    <strong>Yes Weight</strong>
                                  </td>
                                  <td>
                                    <strong>No Weight</strong>
                                  </td>
                                  <td>
                                    <strong>Is Active</strong>
                                  </td>
                                  <td rowspan="2">
                                    <asp:Button ID="Button_Insert" runat="server" Text="Insert" CssClass="Controls_Button" OnClick="Button_Insert_Click" /><br />
                                    <asp:Label ID="Label_InsertValidationMessage" runat="server" Visible="false" Text="Question, Yes Weight and No Weight is Required" CssClass="Controls_Validation" Width="135px"></asp:Label>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <asp:Label ID="Label_InsertSectionList" runat="server" Text="" Width="150px" OnDataBinding="Label_InsertSectionList_DataBinding"></asp:Label>&nbsp;
                                  </td>
                                  <td>
                                    <asp:Label ID="Label_InsertQuestionId" runat="server" Text="" Width="50px" OnDataBinding="Label_InsertQuestionId_DataBinding"></asp:Label>&nbsp;
                                  </td>
                                  <td>
                                    <asp:TextBox ID="TextBox_InsertQuestion" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_Question") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:TextBox ID="TextBox_InsertQuestionYesWeight" runat="server" Width="50px" Text='<%# Bind("Isidima_Rules_Question_YesWeight") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:TextBox ID="TextBox_InsertQuestionNoWeight" runat="server" Width="50px" Text='<%# Bind("Isidima_Rules_Question_NoWeight") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:Label ID="Label_InsertIsActive" runat="server" Text="" Width="50px"></asp:Label>&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2" style="width: 200px;">
                                    <strong>Professional Action</strong>
                                  </td>
                                  <td style="width: 200px;">
                                    <strong>Action Taken</strong>
                                  </td>
                                  <td colspan="2" style="width: 200px;">
                                    <strong>Category Responsible</strong>
                                  </td>
                                  <td style="width: 200px;">
                                    <strong>Material Available</strong>
                                  </td>
                                  <td style="width: 200px;">
                                    <strong>Risk</strong>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2">
                                    <asp:TextBox ID="TextBox_InsertProfessionalAction" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_ProfessionalAction") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:TextBox ID="TextBox_InsertActionTaken" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_ActionTaken") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td colspan="2">
                                    <asp:TextBox ID="TextBox_InsertCategoryResponsible" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_CategoryResponsible") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:TextBox ID="TextBox_InsertMaterialAvailable" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_MaterialAvailable") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                  <td>
                                    <asp:TextBox ID="TextBox_InsertRisk" runat="server" TextMode="MultiLine" Rows="3" Width="200px" Text='<%# Bind("Isidima_Rules_Risk") %>' CssClass="Controls_TextBox"></asp:TextBox>
                                  </td>
                                </tr>
                              </table>
                            </FooterTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_Isidima_Rules" runat="server" OnSelected="SqlDataSource_Isidima_Rules_Selected"></asp:SqlDataSource>
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
