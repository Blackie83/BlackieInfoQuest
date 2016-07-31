<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_IPS_SpecimenAll.aspx.cs" Inherits="InfoQuestForm.Form_IPS_SpecimenAll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Infection Prevention Surveillance - Specimen All</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_IPS_SpecimenAll" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_IPS_SpecimenAll" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_IPS_SpecimenAll" AssociatedUpdatePanelID="UpdatePanel_IPS_SpecimenAll">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_IPS_SpecimenAll" runat="server">
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
          <table id="TableInfo" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_InfoHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="width: 90px">Facility:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Visit Number:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Surname, Name:
                    </td>
                    <td style="width: 150px">
                      <asp:Label ID="Label_IName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 90px">Report Number:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IInfectionReportNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Category:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IInfectionCategoryName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Type:
                    </td>
                    <td style="width: 150px">
                      <asp:Label ID="Label_IInfectionTypeName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 90px">Infection:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IInfectionCompleted" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Specimen:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_ISpecimen" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">HAI Investigation:
                    </td>
                    <td style="width: 150px">
                      <asp:Label ID="Label_IHAI" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Footer">
                  <tr>
                    <td style="text-align: center;">
                      <asp:Button ID="Button_InfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_InfectionHome_OnClick" />&nbsp;
                      <asp:Button ID="Button_SpecimenHome" runat="server" Text="Specimen Home" CssClass="Controls_Button" OnClick="Button_SpecimenHome_OnClick" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div style="height: 40px; width: 900px; text-align: center;">
            &nbsp;
          </div>
          <table id="TableSpecimenAll" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_SpecimenAllHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenSpecimenAllTotalRecords" runat="server" Text="" Visible="false" />
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_IPS_SpecimenAll_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_SpecimenAll_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_SpecimenAll_List_PreRender" OnRowCreated="GridView_IPS_SpecimenAll_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_RowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_SpecimenAllTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px;">
                                <asp:Button ID="Button_CaptureNewSpecimen" runat="server" Text="Capture New Specimen" CssClass="Controls_Button" OnClick="Button_CaptureNewSpecimen_OnClick" />
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
                              <td colspan="2">No Specimen Captured
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_SpecimenAllTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_CaptureNewSpecimen" runat="server" Text="Capture New Specimen" CssClass="Controls_Button" OnClick="Button_CaptureNewSpecimen_OnClick" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="IPS_Specimen_Id" HeaderText="SID" ReadOnly="True" SortExpression="IPS_Specimen_Id" />
                          <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetSpecimenLink(Eval("IPS_Specimen_Id"), Eval("SpecimenStatus")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="SpecimenDate" HeaderText="Date" ReadOnly="True" SortExpression="SpecimenDate" />
                          <asp:BoundField DataField="SpecimenTime" HeaderText="Time" ReadOnly="True" SortExpression="SpecimenTime" />
                          <asp:BoundField DataField="SpecimenType" HeaderText="Type" ReadOnly="True" SortExpression="SpecimenType" />
                          <asp:BoundField DataField="IPS_SpecimenResult_Id" HeaderText="SRID" ReadOnly="True" SortExpression="IPS_SpecimenResult_Id" />
                          <asp:TemplateField HeaderText="Laboratory Number">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetSpecimenResultLink(Eval("IPS_Specimen_Id"), Eval("IPS_SpecimenResult_Id"), Eval("SpecimenResultLabNumber")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="IPS_Organism_Id" HeaderText="OID" ReadOnly="True" SortExpression="IPS_Organism_Id" />
                          <asp:TemplateField HeaderText="Organism">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetOrganismLink(Eval("IPS_Specimen_Id"), Eval("IPS_SpecimenResult_Id"), Eval("IPS_Organism_Id"), Eval("Organism")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="Antibiogram" HeaderText="Antibiogram" ReadOnly="True" SortExpression="Antibiogram" />
                          <asp:BoundField DataField="AntibiogramSRI" HeaderText="SRI" ReadOnly="True" SortExpression="AntibiogramSRI" />
                          <asp:BoundField DataField="Complete" HeaderText="Complete" ReadOnly="True" SortExpression="Complete" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_SpecimenAll_List" runat="server" OnSelected="SqlDataSource_IPS_SpecimenAll_List_Selected"></asp:SqlDataSource>
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
