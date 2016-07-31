<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_InfectionPrevention_List.aspx.cs" Inherits="InfoQuestForm.Form_InfectionPrevention_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Infection Prevention List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_InfectionPrevention_List" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_InfectionPrevention_List" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_InfectionPrevention_List" AssociatedUpdatePanelID="UpdatePanel_InfectionPrevention_List">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_InfectionPrevention_List" runat="server">
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
                      <asp:Label ID="Label_SearchErrorMessage" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td>Facility
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_InfectionPrevention_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_Facility" runat="server" SelectCommand="spAdministration_Execute_Facility_Form" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                          <asp:Parameter Name="SecurityUser_UserName" Type="String" />
                          <asp:Parameter Name="Form_Id" Type="String" DefaultValue="37" />
                          <asp:Parameter Name="Facility_Type" Type="String" DefaultValue="0" />
                          <asp:Parameter Name="TableSELECT" Type="String" DefaultValue="0" />
                          <asp:Parameter Name="TableFROM" Type="String" DefaultValue="0" />
                          <asp:Parameter Name="TableWHERE" Type="String" DefaultValue="0" />
                        </SelectParameters>
                      </asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Report Number
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_ReportNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Patient Name
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_PatientName" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Patient Visit Number
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_PatientVisitNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Infection Type
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_InfectionType" runat="server" DataSourceID="SqlDataSource_InfectionPrevention_InfectionType" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Type</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_InfectionType" runat="server" SelectCommand="spAdministration_Execute_List" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                          <asp:Parameter Name="Form_Id" Type="String" DefaultValue="4" />
                          <asp:Parameter Name="ListCategory_Id" Type="Int32" DefaultValue="13" />
                          <asp:Parameter Name="ListItem_Parent" Type="Int32" DefaultValue="-1" />
                          <asp:Parameter Name="TableSELECT" Type="String" DefaultValue="0" />
                          <asp:Parameter Name="TableFROM" Type="String" DefaultValue="0" />
                          <asp:Parameter Name="TableWHERE" Type="String" DefaultValue="0" />
                        </SelectParameters>
                      </asp:SqlDataSource>
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
                      <asp:GridView ID="GridView_InfectionPrevention_List" runat="server" Width="1000px" AutoGenerateColumns="False" DataSourceID="SqlDataSource_InfectionPrevention_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_InfectionPrevention_List_PreRender" OnDataBound="GridView_InfectionPrevention_List_DataBound" OnRowCreated="GridView_InfectionPrevention_List_RowCreated">
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
                              <%=GridView_InfectionPrevention_List.PageCount%>
                              </td>
                              <td>
                                <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
                              </td>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10">
                                <%--<asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Form" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />--%>
                                &nbsp;
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
                              <td></td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="text-align: center;">
                                <%--<asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Form" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />--%>
                                &nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("pkiInfectionFormID"), Eval("fkiFacilityID"), Eval("sPatientVisitNumber"), Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                          <asp:BoundField DataField="sReportNumber" HeaderText="Report Number" ReadOnly="True" SortExpression="sReportNumber" />
                          <asp:BoundField DataField="sPatientVisitNumber" HeaderText="Visit Number" ReadOnly="True" SortExpression="sPatientVisitNumber" />
                          <asp:BoundField DataField="sPatientName" HeaderText="Name" ReadOnly="True" SortExpression="sPatientName" />
                          <asp:BoundField DataField="sFormStatus" HeaderText="Form Status" ReadOnly="True" SortExpression="sFormStatus" />
                          <asp:BoundField DataField="ListItem_Name" HeaderText="Infection Type" ReadOnly="True" SortExpression="ListItem_Name" />
                          <asp:BoundField DataField="bInvestigationCompleted" HeaderText="Investigation Status" ReadOnly="True" SortExpression="bInvestigationCompleted" />
                          <asp:BoundField DataField="dtInvestigationCompleted" HeaderText="Investigation Date" ReadOnly="True" SortExpression="dtInvestigationCompleted" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_InfectionPrevention_List" runat="server" SelectCommand="spForm_Get_InfectionPrevention_List" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnSelected="SqlDataSource_InfectionPrevention_List_Selected">
                        <SelectParameters>
                          <asp:Parameter Name="SecurityUser" Type="String" />
                          <asp:QueryStringParameter Name="FacilityId" QueryStringField="s_Facility" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="ReportNumber" QueryStringField="s_ReportNumber" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="PatientName" QueryStringField="s_PatientName" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="PatientVisitNumber" QueryStringField="s_PatientVisitNumber" Type="String" DefaultValue="" />
                          <asp:QueryStringParameter Name="InfectionType" QueryStringField="s_InfectionType" Type="String" DefaultValue="" />
                        </SelectParameters>
                      </asp:SqlDataSource>
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
