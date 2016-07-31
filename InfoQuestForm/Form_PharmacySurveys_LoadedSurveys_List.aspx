<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_PharmacySurveys_LoadedSurveys_List.aspx.cs" Inherits="InfoQuestForm.Form_PharmacySurveys_LoadedSurveys_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Pharmacy Surveys Loaded Surveys List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_PharmacySurveys_LoadedSurveys_List.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_PharmacySurveys_LoadedSurveys_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_PharmacySurveys_LoadedSurveys_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_PharmacySurveys_LoadedSurveys_List" AssociatedUpdatePanelID="UpdatePanel_PharmacySurveys_LoadedSurveys_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_PharmacySurveys_LoadedSurveys_List" runat="server">
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
                    <td>Surveys
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_LoadedSurveys" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_LoadedSurveys" DataTextField="LoadedSurveys_Name" DataValueField="LoadedSurveys_Id">
                        <asp:ListItem Value="">Select Survey</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_LoadedSurveys" runat="server"></asp:SqlDataSource>
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
                      <asp:Button ID="Button_CreateSurvey" runat="server" Text="Create New Survey" CssClass="Controls_Button" OnClick="Button_CreateSurvey_Click" />&nbsp;
                      <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" />&nbsp;
                      <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivLoadedSurveys1" runat="server">
            &nbsp;
          </div>
          <table id="TableDuplicateSurveys" class="Table" runat="server" style="width: 1000px;">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_DuplicateSurveyHeading" runat="server" Text=""></asp:Label>
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
                      <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation" EnableViewState="false"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 170px;" id="FormDuplicateSurveysName">New Survey Name
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_DuplicateName" runat="server" CssClass="Controls_TextBox" Width="800px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 170px;" id="FormDuplicateSurveysFY">FY
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_DuplicateFY" runat="server" DataSourceID="SqlDataSource_DuplicateFY" AppendDataBoundItems="true" DataTextField="FY" DataValueField="FY" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select FY</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_DuplicateFY" runat="server"></asp:SqlDataSource>
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
                      <asp:Button ID="Button_ClearDuplicateSurvey" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_ClearDuplicateSurvey_Click" />&nbsp;
                      <asp:Button ID="Button_CreateDuplicateSurvey" runat="server" Text="Create Duplicate Survey" CssClass="Controls_Button" OnClick="Button_CreateDuplicateSurvey_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div id="DivLoadedSurveys2" runat="server">
            &nbsp;
          </div>
          <table id="TableLoadedSurveys" class="Table" runat="server" style="width: 1000px;">
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
                      <asp:GridView ID="GridView_PharmacySurveys_LoadedSurveys_List" runat="server" Width="100%" AutoGenerateColumns="False" DataSourceID="SqlDataSource_PharmacySurveys_LoadedSurveys_List" CssClass="GridView" AllowPaging="False" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_PharmacySurveys_LoadedSurveys_List_PreRender" OnDataBound="GridView_PharmacySurveys_LoadedSurveys_List_DataBound" OnRowCreated="GridView_PharmacySurveys_LoadedSurveys_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" HorizontalAlign="Left" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>Records Per Page:
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_PageSize" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_SelectedIndexChanged">
                                  <asp:ListItem Value="20">20</asp:ListItem>
                                  <asp:ListItem Value="50">50</asp:ListItem>
                                  <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />
                                <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />
                              </td>
                              <td>Page
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_Page" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_SelectedIndexChanged">
                                </asp:DropDownList>
                              </td>
                              <td>of <%=GridView_PharmacySurveys_LoadedSurveys_List.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                              <td>&nbsp;
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <RowStyle CssClass="GridView_RowStyle" HorizontalAlign="Left" />
                        <FooterStyle CssClass="GridView_FooterStyle" HorizontalAlign="Center" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
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
                              <td>&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="Survey" SortExpression="LoadedSurveys_Name">
                            <ItemTemplate>
                              <asp:HyperLink ID="HyperLink_LoadedSurveys" Text='<%# GetLink_UpdateSurvey(Eval("LoadedSurveys_Id"), Eval("LoadedSurveys_Name")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Section" SortExpression="LoadedSections_Name">
                            <ItemTemplate>
                              <asp:HyperLink ID="HyperLink_LoadedSections" Text='<%# GetLink_UpdateSections(Eval("LoadedSections_Id"), Eval("LoadedSections_Name")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Question" SortExpression="LoadedQuestions_Name">
                            <ItemTemplate>
                              <asp:HyperLink ID="HyperLink_LoadedQuestions" Text='<%# GetLink_UpdateQuestions(Eval("LoadedQuestions_Id"), Eval("LoadedQuestions_Name")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Answer" SortExpression="LoadedAnswers_Name">
                            <ItemTemplate>
                              <asp:HyperLink ID="HyperLink_LoadedAnswers" Text='<%# GetLink_UpdateAnswers(Eval("LoadedAnswers_Id"), Eval("LoadedAnswers_Name")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_PharmacySurveys_LoadedSurveys_List" runat="server" OnSelected="SqlDataSource_PharmacySurveys_LoadedSurveys_List_Selected"></asp:SqlDataSource>
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
