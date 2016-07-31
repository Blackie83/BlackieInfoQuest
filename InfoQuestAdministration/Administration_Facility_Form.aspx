<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_Facility_Form" CodeBehind="Administration_Facility_Form.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - Facility Form</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_Facility_Form.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_Facility_Form" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Facility_Form" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Facility_Form" AssociatedUpdatePanelID="UpdatePanel_Facility_Form">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Facility_Form" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table>
            <tr>
              <td style="vertical-align: top;">
                <table class="Table">
                  <tr>
                    <td style="vertical-align: top;">
                      <table class="Table_Header">
                        <tr>
                          <td>Search Forms</td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <table class="Table_Body">
                        <tr>
                          <td>Form Name
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_FormId" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Form_Id" DataTextField="Form_Name" DataValueField="Form_Id">
                              <asp:ListItem Value="">Select Form</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource_Form_Id" runat="server" SelectCommand="SELECT DISTINCT Form_Id , Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')' AS Form_Name FROM Administration_Form ORDER BY Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
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
                <table class="Table">
                  <tr>
                    <td style="vertical-align: top;">
                      <table class="Table_Header">
                        <tr>
                          <td>List of Forms</td>
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
                            <asp:GridView ID="GridView_Form_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_Form_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_Form_List_PreRender" OnDataBound="GridView_Form_List_DataBound" OnRowCreated="GridView_Form_List_RowCreated">
                              <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                              <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                              <PagerTemplate>
                                <table class="GridView_PagerStyle">
                                  <tr>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
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
                                    <td>of
                                    <%=GridView_Form_List.PageCount%>
                                    </td>
                                    <td>
                                      <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                      <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                                    </td>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <td>No records
                                    </td>
                                  </tr>
                                  <tr class="GridView_EmptyDataStyle_FooterStyle">
                                    <td>&nbsp;
                                    </td>
                                  </tr>
                                  <tr class="GridView_EmptyDataStyle_FooterStyle">
                                    <td style="text-align: center;">&nbsp;
                                    </td>
                                  </tr>
                                </table>
                              </EmptyDataTemplate>
                              <Columns>
                                <asp:TemplateField HeaderText="">
                                  <ItemTemplate>
                                    <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("Form_Id")) %>' runat="server"></asp:HyperLink>
                                  </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Form_Name" HeaderText="Form" ReadOnly="True" SortExpression="Form_Name" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Form_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="Form_IsActive" />
                              </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource_Form_List" runat="server" SelectCommand="SELECT DISTINCT Form_Id , Form_Name , CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END AS Form_IsActive FROM Administration_Form WHERE (Form_Id = @FormId OR @FormId = 0) ORDER BY Form_Name" OnSelected="SqlDataSource_Form_List_Selected">
                              <SelectParameters>
                                <asp:QueryStringParameter Name="FormId" QueryStringField="s_Form_Id" Type="String" DefaultValue="" />
                              </SelectParameters>
                            </asp:SqlDataSource>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="width: 10px;">&nbsp;
              </td>
              <td style="width: 5px; background-color: #b0262e;">&nbsp;
              </td>
              <td style="width: 5px; background-color: #003768;">&nbsp;
              </td>
              <td style="width: 10px;">&nbsp;
              </td>
              <td style="vertical-align: top;">
                <table id="TableFacilityForm" runat="server" class="Table">
                  <tr>
                    <td style="vertical-align: top;">
                      <table class="Table_Header">
                        <tr>
                          <td>Form Facilities</td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <table class="Table_Body">
                        <tr>
                          <td style="width: 75px;">Form Name
                          </td>
                          <td>
                            <asp:Label ID="Label_FormName" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                        </tr>
                        <tr>
                          <td style="width: 75px;">Facility Name
                          </td>
                          <td style="padding: 0px; border-left-width: 0px; border-top-width: 1px;">
                            <div style="max-height: 580px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                              <asp:CheckBoxList ID="CheckBoxList_Facility" runat="server" AppendDataBoundItems="true" CssClass="Controls_CheckBoxListWithScrollbars" DataSourceID="SqlDataSource_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="2" RepeatLayout="Table" OnDataBound="CheckBoxList_Facility_DataBound">
                              </asp:CheckBoxList>
                              <asp:SqlDataSource ID="SqlDataSource_Facility" runat="server" SelectCommand="SELECT DISTINCT Facility_Id , Facility_FacilityDisplayName + ' (' + CASE WHEN Facility_IsActive = 1 THEN 'Yes' WHEN Facility_IsActive = 0 THEN 'No' END + ')' AS Facility_FacilityDisplayName FROM vAdministration_Facility_All ORDER BY Facility_FacilityDisplayName + ' (' + CASE WHEN Facility_IsActive = 1 THEN 'Yes' WHEN Facility_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
                            </div>
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
                            <asp:Button ID="Button_FacilityFormClear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_FacilityFormClear_Click" />&nbsp;
                          <asp:Button ID="Button_FacilityFormUpdate" runat="server" Text="Update" CssClass="Controls_Button" OnClick="Button_FacilityFormUpdate_Click" />&nbsp;
                          </td>
                        </tr>
                      </table>
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
