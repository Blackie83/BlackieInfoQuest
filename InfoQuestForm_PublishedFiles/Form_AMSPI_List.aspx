<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_AMSPI_List" CodeBehind="Form_AMSPI_List.aspx.cs" %>

<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Antimicrobial Stewardship - Pharmacist Intervention List</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion()  %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion()  %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion()  %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_AMSPI_List" runat="server" defaultbutton="Button_Search">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
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
                <td>Facility
                </td>
                <td>
                  <asp:DropDownList ID="DropDownList_Facility" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_AMSPI_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Facility_SelectedIndexChanged">
                    <asp:ListItem Value="">Select Facility</asp:ListItem>
                  </asp:DropDownList>
                  <asp:SqlDataSource ID="SqlDataSource_AMSPI_Facility" runat="server"></asp:SqlDataSource>
                </td>
              </tr>
              <tr>
                <td>Unit
                </td>
                <td>
                  <asp:DropDownList ID="DropDownList_Unit" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_AMSPI_Unit" DataTextField="Unit_Name" DataValueField="Unit_Id">
                    <asp:ListItem Value="">Select Unit</asp:ListItem>
                  </asp:DropDownList>
                  <asp:SqlDataSource ID="SqlDataSource_AMSPI_Unit" runat="server"></asp:SqlDataSource>
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
                <td>Patient Name
                </td>
                <td>
                  <asp:TextBox ID="TextBox_PatientName" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td>Report Number
                </td>
                <td>
                  <asp:TextBox ID="TextBox_ReportNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
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
                  <asp:GridView ID="GridView_AMSPI_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_AMSPI_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_AMSPI_List_PreRender" OnDataBound="GridView_AMSPI_List_DataBound" OnRowCreated="GridView_AMSPI_List_RowCreated">
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
                          <%=GridView_AMSPI_List.PageCount%>
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
                            <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Pharmacist Intervention" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
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
                          <td style="text-align: center;">
                            <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Pharmacist Intervention" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                          </td>
                        </tr>
                      </table>
                    </EmptyDataTemplate>
                    <Columns>
                      <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                          <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("Facility_Id") , Eval("AMSPI_PI_PatientVisitNumber")) %>' runat="server"></asp:HyperLink>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="Facility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="Facility_FacilityDisplayName" />
                      <asp:BoundField DataField="AMSPI_PI_PatientVisitNumber" HeaderText="Patient Visit Number" ReadOnly="True" SortExpression="AMSPI_PI_PatientVisitNumber" />
                      <asp:BoundField DataField="AMSPI_PI_PatientName" HeaderText="Patient Name" ReadOnly="True" SortExpression="AMSPI_PI_PatientName" />
                      <asp:BoundField DataField="AMSPI_PI_PatientAge" HeaderText="Age" ReadOnly="True" SortExpression="AMSPI_PI_PatientAge" />
                      <asp:BoundField DataField="AMSPI_PI_PatientDateOfAdmission" HeaderText="Admission Date" ReadOnly="True" SortExpression="AMSPI_PI_PatientDateOfAdmission" />
                      <asp:BoundField DataField="AMSPI_PI_PatientDateofDischarge" HeaderText="Discharge Date" ReadOnly="True" SortExpression="AMSPI_PI_PatientDateofDischarge" />
                    </Columns>
                  </asp:GridView>
                  <asp:SqlDataSource ID="SqlDataSource_AMSPI_List" runat="server" OnSelected="SqlDataSource_AMSPI_List_Selected"></asp:SqlDataSource>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
