<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_ExecutiveMarketInquiry_Map.aspx.cs" Inherits="InfoQuestForm.Form_ExecutiveMarketInquiry_Map" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Executive Market Inquiry - Map</title>
  <asp:Literal ID="Literal_JavaScript" runat="server"></asp:Literal>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_ExecutiveMarketInquiry_Map.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>"></script>
    <script src="App_Javascripts/GoogleMapAddon/oms.min.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>"></script>
    <script src="App_Javascripts/GoogleMapAddon/ContextMenu.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>"></script>
  </asp:PlaceHolder>
  <style type="text/css">
    .context_menu {
      background-color: white;
      border: 1px solid gray;
    }

    .context_menu_item {
      padding: 3px 6px;
    }

      .context_menu_item:hover {
        background-color: #CCCCCC;
      }

    .context_menu_separator {
      background-color: gray;
      height: 1px;
      margin: 0;
      padding: 0;
    }

    #clearDirectionsItem, #getDirectionsItem {
      display: none;
    }
  </style>
</head>
<body>
  <form id="form_ExecutiveMarketInquiry_Map" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_ExecutiveMarketInquiry_Map" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_ExecutiveMarketInquiry_Map" AssociatedUpdatePanelID="UpdatePanel_ExecutiveMarketInquiry_Map">
        <ProgressTemplate>
          <div class="UpdateProgressBackground"></div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_ExecutiveMarketInquiry_Map" runat="server">
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
          <a id="MapSearch"></a>
          <div>
            &nbsp;
          </div>
          <table class="Table" style="width: 1000px;">
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
                    <td>
                      <asp:Label ID="Label_InvalidSearchMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td id="SearchMaps">Maps:&nbsp;
                      <asp:TextBox ID="TextBox_MapType" runat="server" ReadOnly="True" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
                      <Ajax:PopupControlExtender ID="PopupControlExtender_MapType" runat="server" Enabled="True" TargetControlID="TextBox_MapType" PopupControlID="Panel_MapType" OffsetY="20">
                      </Ajax:PopupControlExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_MapType" runat="server" TargetControlID="TextBox_MapType" WatermarkText="Select Map Type" WatermarkCssClass="Controls_Watermark"></Ajax:TextBoxWatermarkExtender>
                      <asp:Panel ID="Panel_MapType" runat="server" Height="300px" Width="300px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#003768" Direction="LeftToRight" ScrollBars="Auto" BackColor="#f7f7f7" Style="display: none;">
                        <asp:CheckBox ID="CheckBox_MapType_CheckAll" runat="server" Text="Select All" />
                        <asp:CheckBoxList ID="CheckBoxList_MapType" runat="server" Width="100%" DataSourceID="ObjectDataSource_MapType" AppendDataBoundItems="true" DataTextField="MapType" DataValueField="MapType" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList_MapType_SelectedIndexChanged" OnDataBound="CheckBoxList_MapType_DataBound"></asp:CheckBoxList>
                        <asp:ObjectDataSource ID="ObjectDataSource_MapType" runat="server"></asp:ObjectDataSource>
                      </asp:Panel>
                      &nbsp;&nbsp;&nbsp;
                      Country:&nbsp;
                      <asp:DropDownList ID="DropDownList_Country" runat="server" DataSourceID="ObjectDataSource_Country" AppendDataBoundItems="true" DataTextField="Country" DataValueField="Country" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Country_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Country</asp:ListItem>
                      </asp:DropDownList>
                      <asp:ObjectDataSource ID="ObjectDataSource_Country" runat="server"></asp:ObjectDataSource>
                      &nbsp;&nbsp;&nbsp;
                      Province:&nbsp;
                      <asp:DropDownList ID="DropDownList_Province" runat="server" DataSourceID="ObjectDataSource_Province" AppendDataBoundItems="true" DataTextField="Province" DataValueField="Province" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Province_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Province</asp:ListItem>
                      </asp:DropDownList>
                      <asp:ObjectDataSource ID="ObjectDataSource_Province" runat="server"></asp:ObjectDataSource>
                      &nbsp;&nbsp;&nbsp;
                    </td>
                  </tr>
                  <tr id="HospitalDoctor1">
                    <td class="Table_BodyHeaderBlue">Doctor & Hospital
                    </td>
                  </tr>
                  <tr id="HospitalDoctor2">
                    <td>Sub Region:&nbsp;
                      <asp:DropDownList ID="DropDownList_HospitalDoctor_SubRegion" runat="server" DataSourceID="" AppendDataBoundItems="True" DataTextField="SubRegion" DataValueField="SubRegion" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_HospitalDoctor_SubRegion_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Sub Region</asp:ListItem>
                      </asp:DropDownList>
                      <asp:ObjectDataSource ID="ObjectDataSource_HospitalDoctor_SubRegion" runat="server"></asp:ObjectDataSource>
                      &nbsp;&nbsp;&nbsp;
                      Town:&nbsp;
                      <asp:DropDownList ID="DropDownList_HospitalDoctor_Town" runat="server" DataSourceID="" AppendDataBoundItems="True" DataTextField="Town" DataValueField="Town" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_HospitalDoctor_Town_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Town</asp:ListItem>
                      </asp:DropDownList>
                      <asp:ObjectDataSource ID="ObjectDataSource_HospitalDoctor_Town" runat="server"></asp:ObjectDataSource>
                    </td>
                  </tr>
                  <tr id="HospitalDoctor3">
                    <td>Hospital Organisation:&nbsp;
                      <asp:TextBox ID="TextBox_Hospital_Organisation" runat="server" ReadOnly="True" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
                      <Ajax:PopupControlExtender ID="PopupControlExtender_Hospital_Organisation" runat="server" Enabled="True" TargetControlID="TextBox_Hospital_Organisation" PopupControlID="Panel_Hospital_Organisation" OffsetY="20">
                      </Ajax:PopupControlExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_Hospital_Organisation" runat="server" TargetControlID="TextBox_Hospital_Organisation" WatermarkText="Select Organisation" WatermarkCssClass="Controls_Watermark"></Ajax:TextBoxWatermarkExtender>
                      <asp:Panel ID="Panel_Hospital_Organisation" runat="server" Height="300px" Width="300px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#003768" Direction="LeftToRight" ScrollBars="Auto" BackColor="#f7f7f7" Style="display: none">
                        <asp:CheckBox ID="CheckBox_Hospital_Organisation_CheckAll" runat="server" Text="Select All" />
                        <asp:CheckBoxList ID="CheckBoxList_Hospital_Organisation" runat="server" Width="100%" DataSourceID="ObjectDataSource_Hospital_Organisation" AppendDataBoundItems="true" DataTextField="Organisation" DataValueField="Organisation" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList_Hospital_Organisation_SelectedIndexChanged" OnDataBound="CheckBoxList_Hospital_Organisation_DataBound"></asp:CheckBoxList>
                        <asp:ObjectDataSource ID="ObjectDataSource_Hospital_Organisation" runat="server"></asp:ObjectDataSource>
                      </asp:Panel>
                      &nbsp;&nbsp;&nbsp;
                      Hospital Type:&nbsp;
                      <asp:DropDownList ID="DropDownList_Hospital_Type" runat="server" DataSourceID="ObjectDataSource_Hospital_Type" AppendDataBoundItems="True" DataTextField="Type" DataValueField="Type" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Hospital_Type_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Type</asp:ListItem>
                      </asp:DropDownList>
                      <asp:ObjectDataSource ID="ObjectDataSource_Hospital_Type" runat="server"></asp:ObjectDataSource>
                    </td>
                  </tr>
                  <tr id="HospitalDoctor4">
                    <td>Hospital:&nbsp;
                      <asp:DropDownList ID="DropDownList_Hospital_NamedropDown" runat="server" DataSourceID="" AppendDataBoundItems="True" DataTextField="Name" DataValueField="Name" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Hospital</asp:ListItem>
                      </asp:DropDownList>
                      <asp:ObjectDataSource ID="ObjectDataSource_Hospital_NamedropDown" runat="server"></asp:ObjectDataSource>
                      &nbsp;OR&nbsp;
                      <asp:TextBox ID="TextBox_Hospital_NameTextBox" runat="server" CssClass="Controls_TextBox" Width="300px"></asp:TextBox>
                    </td>
                  </tr>
                  <tr id="HospitalDoctor5">
                    <td>Doctor Organisation:&nbsp;
                      <asp:TextBox ID="TextBox_Doctor_Organisation" runat="server" ReadOnly="True" Width="300px" CssClass="Controls_TextBox"></asp:TextBox>
                      <Ajax:PopupControlExtender ID="PopupControlExtender_Doctor_Organisation" runat="server" Enabled="True" TargetControlID="TextBox_Doctor_Organisation" PopupControlID="Panel_Doctor_Organisation" OffsetY="20">
                      </Ajax:PopupControlExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_Doctor_Organisation" runat="server" TargetControlID="TextBox_Doctor_Organisation" WatermarkText="Select Organisation" WatermarkCssClass="Controls_Watermark"></Ajax:TextBoxWatermarkExtender>
                      <asp:Panel ID="Panel_Doctor_Organisation" runat="server" Height="300px" Width="300px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#003768" Direction="LeftToRight" ScrollBars="Auto" BackColor="#f7f7f7" Style="display: none">
                        <asp:CheckBox ID="CheckBox_Doctor_Organisation_CheckAll" runat="server" Text="Select All" />
                        <asp:CheckBoxList ID="CheckBoxList_Doctor_Organisation" runat="server" Width="100%" DataSourceID="ObjectDataSource_Doctor_Organisation" AppendDataBoundItems="true" DataTextField="Organisation" DataValueField="Organisation" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList_Doctor_Organisation_SelectedIndexChanged" OnDataBound="CheckBoxList_Doctor_Organisation_DataBound"></asp:CheckBoxList>
                        <asp:ObjectDataSource ID="ObjectDataSource_Doctor_Organisation" runat="server"></asp:ObjectDataSource>
                      </asp:Panel>
                      &nbsp;&nbsp;&nbsp;
                      Doctor Type:&nbsp;
                      <asp:DropDownList ID="DropDownList_Doctor_Type" runat="server" DataSourceID="ObjectDataSource_Doctor_Type" AppendDataBoundItems="True" DataTextField="Type" DataValueField="Type" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Doctor_Type_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Type</asp:ListItem>
                      </asp:DropDownList>
                      <asp:ObjectDataSource ID="ObjectDataSource_Doctor_Type" runat="server"></asp:ObjectDataSource>
                    </td>
                  </tr>
                  <tr id="HospitalDoctor6">
                    <td>Doctor:&nbsp;
                      <asp:DropDownList ID="DropDownList_Doctor_NamedropDown" runat="server" DataSourceID="" AppendDataBoundItems="True" DataTextField="Name" DataValueField="Name" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Doctor</asp:ListItem>
                      </asp:DropDownList>
                      <asp:ObjectDataSource ID="ObjectDataSource_Doctor_NamedropDown" runat="server"></asp:ObjectDataSource>
                      &nbsp;OR&nbsp;
                      <asp:TextBox ID="TextBox_Doctor_NameTextBox" runat="server" CssClass="Controls_TextBox" Width="300px"></asp:TextBox>
                    </td>
                  </tr>
                  <tr id="PopulationMedicalScheme1">
                    <td class="Table_BodyHeaderBlue">Population
                    </td>
                  </tr>
                  <tr id="PopulationMedicalScheme2">
                    <td>Municipality:&nbsp;
                      <asp:DropDownList ID="DropDownList_Population_Municipality" runat="server" DataSourceID="" AppendDataBoundItems="true" DataTextField="Municipality" DataValueField="Municipality" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Municipality</asp:ListItem>
                      </asp:DropDownList>
                      <asp:ObjectDataSource ID="ObjectDataSource_Population_Municipality" runat="server"></asp:ObjectDataSource>
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
                      <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_OnClick" />&nbsp;
                      <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_OnClick" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </ContentTemplate>
      </asp:UpdatePanel>
      <div>
        &nbsp;
      </div>
      <table class="Table" style="width: 100%; z-index: 0;">
        <tr>
          <td>
            <table class="Table_Header">
              <tr>
                <td>
                  <asp:Label ID="Label_MapHeading" runat="server" Text=""></asp:Label>
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <table class="Table_Body">
              <tr>
                <td>Total Hospitals:&nbsp;<asp:Label ID="Label_TotalHospital_Map" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  Total Doctors:&nbsp;<asp:Label ID="Label_TotalDoctor_Map" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  Total Population Circles:&nbsp;<asp:Label ID="Label_TotalPopulation_Map" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  Total Medical Scheme Beneficiaries Circles:&nbsp;<asp:Label ID="Label_TotalMedicalScheme_Map" runat="server" Text=""></asp:Label>
                </td>
              </tr>
              <tr>
                <td style="padding: 0px;">
                  <asp:DataList ID="DataList_ExecutiveMarketInquiry_MapLegend_List" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                    <ItemTemplate>
                      <table>
                        <tr>
                          <td style="border: none;">
                            <a id="Link_Icon" onmouseover="GoogleMarkerBounce('<%# Eval("Icon") %>')" onmouseout="GoogleMarkerBounce('')">
                              <asp:Image ID="Icon" runat="server" ImageUrl='<%# Eval("Icon") %>' Height="25px" /></a>
                          </td>
                          <td style="border: none; vertical-align: middle;">
                            <asp:Label ID="IconDescription" runat="server" Text='<%# Eval("IconDescription") %>'></asp:Label>
                            (<asp:Label ID="IconCount" runat="server" Text='<%# Eval("IconCount") %>'></asp:Label>)
                          </td>
                        </tr>
                      </table>
                    </ItemTemplate>
                  </asp:DataList>
                </td>
              </tr>
              <tr>
                <td style="padding: 0px;">
                  <div id="googleMap" style="width: 100%; height: 600px;"></div>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <div id="DivHospital" runat="server" style="height: 40px;">
        &nbsp;
      </div>
      <table id="TableHospital" runat="server" class="Table" style="width: 1000px;">
        <tr>
          <td>
            <table class="Table_Header">
              <tr>
                <td>
                  <asp:Label ID="Label_GridHeading_Hospital" runat="server" Text=""></asp:Label>
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <table class="Table_Body">
              <tr>
                <td>Total Hospitals:
                    <asp:Label ID="Label_TotalHospital_List" runat="server" Text=""></asp:Label>
                </td>
              </tr>
              <tr>
                <td style="padding: 0px;">
                  <asp:GridView ID="GridView_ExecutiveMarketInquiry_Hospital_List" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource_ExecutiveMarketInquiry_Hospital_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_ExecutiveMarketInquiry_Hospital_List_PreRender" OnDataBound="GridView_ExecutiveMarketInquiry_Hospital_List_DataBound" OnRowCreated="GridView_ExecutiveMarketInquiry_Hospital_List_RowCreated">
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
                            <asp:DropDownList ID="DropDownList_PageSize_Hospital" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_Hospital_SelectedIndexChanged">
                              <asp:ListItem Value="20">20</asp:ListItem>
                              <asp:ListItem Value="50">50</asp:ListItem>
                              <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList>
                          </td>
                          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          </td>
                          <td>
                            <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />&nbsp;
                                <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />&nbsp;
                          </td>
                          <td>Page
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_Page_Hospital" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_Hospital_SelectedIndexChanged">
                            </asp:DropDownList>
                          </td>
                          <td>of
                          <%=GridView_ExecutiveMarketInquiry_Hospital_List.PageCount%>
                          </td>
                          <td>
                            <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />&nbsp;
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />&nbsp;
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
                      <asp:BoundField DataField="Hospital_Organisation" HeaderText="Organisation" ReadOnly="True" SortExpression="Hospital_Organisation" />
                      <asp:BoundField DataField="Hospital_Type" HeaderText="Type" ReadOnly="True" SortExpression="Hospital_Type" />
                      <asp:BoundField DataField="Hospital_FacilityName" HeaderText="Name" ReadOnly="True" SortExpression="Hospital_FacilityName" />
                      <asp:BoundField DataField="Hospital_PhysicalAddress" HeaderText="Physical" ReadOnly="True" SortExpression="Hospital_PhysicalAddress" />
                      <asp:BoundField DataField="Hospital_PostalAddress" HeaderText="Postal" ReadOnly="True" SortExpression="Hospital_PostalAddress" />
                      <%--<asp:BoundField DataField="Hospital_ContactNumber1" HeaderText="Contact 1" ReadOnly="True" SortExpression="Hospital_ContactNumber1" />
                          <asp:BoundField DataField="Hospital_ContactNumber2" HeaderText="Contact 2" ReadOnly="True" SortExpression="Hospital_ContactNumber2" />
                          <asp:BoundField DataField="Hospital_ContactNumber3" HeaderText="Contact 3" ReadOnly="True" SortExpression="Hospital_ContactNumber3" />
                          <asp:BoundField DataField="Hospital_ContactNumber4" HeaderText="Contact 4" ReadOnly="True" SortExpression="Hospital_ContactNumber4" />
                          <asp:BoundField DataField="EHospital_mergencyContactNumber1" HeaderText="Emergency 1" ReadOnly="True" SortExpression="Hospital_EmergencyContactNumber1" />
                          <asp:BoundField DataField="Hospital_EmergencyContactNumber2" HeaderText="Emergency 2" ReadOnly="True" SortExpression="Hospital_EmergencyContactNumber2" />
                          <asp:BoundField DataField="Hospital_FaxNumber1" HeaderText="Fax 1" ReadOnly="True" SortExpression="Hospital_FaxNumber1" />
                          <asp:BoundField DataField="Hospital_FaxNumber2" HeaderText="Fax 2" ReadOnly="True" SortExpression="Hospital_FaxNumber2" />
                          <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# Eval("Hospital_Email") %>' NavigateUrl='<%# Eval("Hospital_Email", "mailto:{0}") %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="URL">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# Eval("Hospital_URL") %>' NavigateUrl='<%# Eval("Hospital_URL") %>' Target="_blank" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>--%>
                      <asp:BoundField DataField="Hospital_Country" HeaderText="Country" ReadOnly="True" SortExpression="Hospital_Country" />
                      <asp:BoundField DataField="Hospital_Region" HeaderText="Region" ReadOnly="True" SortExpression="Hospital_Region" />
                      <asp:BoundField DataField="Hospital_SubRegion" HeaderText="Sub Region" ReadOnly="True" SortExpression="Hospital_SubRegion" />
                      <asp:BoundField DataField="Hospital_Town" HeaderText="Town" ReadOnly="True" SortExpression="Hospital_Town" />
                      <asp:BoundField DataField="Hospital_Latitude" HeaderText="Latitude" ReadOnly="True" SortExpression="Hospital_Latitude" ItemStyle-Width="70px" />
                      <asp:BoundField DataField="Hospital_Longitude" HeaderText="Longitude" ReadOnly="True" SortExpression="Hospital_Longitude" ItemStyle-Width="70px" />
                    </Columns>
                  </asp:GridView>
                  <asp:ObjectDataSource ID="ObjectDataSource_ExecutiveMarketInquiry_Hospital_List" runat="server" OnSelected="ObjectDataSource_ExecutiveMarketInquiry_Hospital_List_Selected"></asp:ObjectDataSource>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <div id="DivDoctor" runat="server" style="height: 40px;">
        &nbsp;
      </div>
      <table id="TableDoctor" runat="server" class="Table" style="width: 1000px;">
        <tr>
          <td>
            <table class="Table_Header">
              <tr>
                <td>
                  <asp:Label ID="Label_GridHeading_Doctor" runat="server" Text=""></asp:Label>
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <table class="Table_Body">
              <tr>
                <td>Total Doctors:
                    <asp:Label ID="Label_TotalDoctor_List" runat="server" Text=""></asp:Label>
                </td>
              </tr>
              <tr>
                <td style="padding: 0px;">
                  <asp:GridView ID="GridView_ExecutiveMarketInquiry_Doctor_List" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource_ExecutiveMarketInquiry_Doctor_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_ExecutiveMarketInquiry_Doctor_List_PreRender" OnDataBound="GridView_ExecutiveMarketInquiry_Doctor_List_DataBound" OnRowCreated="GridView_ExecutiveMarketInquiry_Doctor_List_RowCreated">
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
                            <asp:DropDownList ID="DropDownList_PageSize_Doctor" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_Doctor_SelectedIndexChanged">
                              <asp:ListItem Value="20">20</asp:ListItem>
                              <asp:ListItem Value="50">50</asp:ListItem>
                              <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList>
                          </td>
                          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          </td>
                          <td>
                            <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />&nbsp;
                                <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />&nbsp;
                          </td>
                          <td>Page
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_Page_Doctor" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_Doctor_SelectedIndexChanged">
                            </asp:DropDownList>
                          </td>
                          <td>of
                          <%=GridView_ExecutiveMarketInquiry_Doctor_List.PageCount%>
                          </td>
                          <td>
                            <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />&nbsp;
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />&nbsp;
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
                      <asp:BoundField DataField="Doctor_Organisation" HeaderText="Organisation" ReadOnly="True" SortExpression="Doctor_Organisation" />
                      <asp:BoundField DataField="Doctor_Type" HeaderText="Type" ReadOnly="True" SortExpression="Doctor_Type" />
                      <asp:BoundField DataField="Doctor_Name" HeaderText="Name" ReadOnly="True" SortExpression="Doctor_Name" />
                      <asp:BoundField DataField="Doctor_PhysicalAddress" HeaderText="Physical" ReadOnly="True" SortExpression="Doctor_PhysicalAddress" />
                      <asp:BoundField DataField="Doctor_PostalAddress" HeaderText="Postal" ReadOnly="True" SortExpression="Doctor_PostalAddress" />
                      <%--<asp:BoundField DataField="Doctor_ContactNumber1" HeaderText="Contact 1" ReadOnly="True" SortExpression="Doctor_ContactNumber1" />
                          <asp:BoundField DataField="Doctor_ContactNumber2" HeaderText="Contact 2" ReadOnly="True" SortExpression="Doctor_ContactNumber2" />
                          <asp:BoundField DataField="Doctor_ContactNumber3" HeaderText="Contact 3" ReadOnly="True" SortExpression="Doctor_ContactNumber3" />
                          <asp:BoundField DataField="Doctor_ContactNumber4" HeaderText="Contact 4" ReadOnly="True" SortExpression="Doctor_ContactNumber4" />
                          <asp:BoundField DataField="EDoctor_mergencyContactNumber1" HeaderText="Emergency 1" ReadOnly="True" SortExpression="Doctor_EmergencyContactNumber1" />
                          <asp:BoundField DataField="Doctor_EmergencyContactNumber2" HeaderText="Emergency 2" ReadOnly="True" SortExpression="Doctor_EmergencyContactNumber2" />
                          <asp:BoundField DataField="Doctor_FaxNumber1" HeaderText="Fax 1" ReadOnly="True" SortExpression="Doctor_FaxNumber1" />
                          <asp:BoundField DataField="Doctor_FaxNumber2" HeaderText="Fax 2" ReadOnly="True" SortExpression="Doctor_FaxNumber2" />
                          <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# Eval("Doctor_Email") %>' NavigateUrl='<%# Eval("Doctor_Email", "mailto:{0}") %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="URL">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# Eval("Doctor_URL") %>' NavigateUrl='<%# Eval("Doctor_URL") %>' Target="_blank" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>--%>
                      <asp:BoundField DataField="Doctor_Country" HeaderText="Country" ReadOnly="True" SortExpression="Doctor_Country" />
                      <asp:BoundField DataField="Doctor_Region" HeaderText="Region" ReadOnly="True" SortExpression="Doctor_Region" />
                      <asp:BoundField DataField="Doctor_SubRegion" HeaderText="Sub Region" ReadOnly="True" SortExpression="Doctor_SubRegion" />
                      <asp:BoundField DataField="Doctor_Town" HeaderText="Town" ReadOnly="True" SortExpression="Doctor_Town" />
                      <asp:BoundField DataField="Doctor_Latitude" HeaderText="Latitude" ReadOnly="True" SortExpression="Doctor_Latitude" ItemStyle-Width="70px" />
                      <asp:BoundField DataField="Doctor_Longitude" HeaderText="Longitude" ReadOnly="True" SortExpression="Doctor_Longitude" ItemStyle-Width="70px" />
                    </Columns>
                  </asp:GridView>
                  <asp:ObjectDataSource ID="ObjectDataSource_ExecutiveMarketInquiry_Doctor_List" runat="server" OnSelected="ObjectDataSource_ExecutiveMarketInquiry_Doctor_List_Selected"></asp:ObjectDataSource>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <div id="DivPopulation" runat="server" style="height: 40px;">
        &nbsp;
      </div>
      <table id="TablePopulation" runat="server" class="Table" style="width: 1000px;">
        <tr>
          <td>
            <table class="Table_Header">
              <tr>
                <td>
                  <asp:Label ID="Label_GridHeading_Population" runat="server" Text=""></asp:Label>
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <table class="Table_Body">
              <tr>
                <td>Total Population:
                    <asp:Label ID="Label_TotalPopulation_List" runat="server" Text=""></asp:Label>
                </td>
              </tr>
              <tr>
                <td style="padding: 0px;">
                  <asp:GridView ID="GridView_ExecutiveMarketInquiry_Population_List" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource_ExecutiveMarketInquiry_Population_List" CssClass="GridView" AllowPaging="False" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnPreRender="GridView_ExecutiveMarketInquiry_Population_List_PreRender" OnDataBound="GridView_ExecutiveMarketInquiry_Population_List_DataBound">
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
                            <asp:DropDownList ID="DropDownList_PageSize_Population" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_Population_SelectedIndexChanged">
                              <asp:ListItem Value="20">20</asp:ListItem>
                              <asp:ListItem Value="50">50</asp:ListItem>
                              <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList>
                          </td>
                          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          </td>
                          <td>
                            <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />&nbsp;
                                <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />&nbsp;
                          </td>
                          <td>Page
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_Page_Population" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_Population_SelectedIndexChanged">
                            </asp:DropDownList>
                          </td>
                          <td>of
                          <%=GridView_ExecutiveMarketInquiry_Population_List.PageCount%>
                          </td>
                          <td>
                            <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />&nbsp;
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />&nbsp;
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
                      <asp:BoundField DataField="Population_Name" HeaderText="Name" ReadOnly="True" SortExpression="Population_Name" />
                      <asp:TemplateField HeaderText="Population" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" SortExpression="Population_Value" ItemStyle-Width="100px">
                        <ItemTemplate>
                          <asp:Label ID="Label_Population_Value" runat="server" Text='<%# Bind("Population_Value","{0:#,##0}") %>'></asp:Label>&nbsp;                          
                          <asp:HiddenField ID="HiddenField_Population_Level" runat="server" Value='<%# Bind("Population_Level") %>' />
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="Population_Type" HeaderText="Type" ReadOnly="True" SortExpression="Population_Type" />
                      <asp:BoundField DataField="Population_Year" HeaderText="Year" ReadOnly="True" SortExpression="Population_Year" />
                      <asp:BoundField DataField="Population_Address" HeaderText="Address" ReadOnly="True" SortExpression="Population_Address" />
                      <asp:BoundField DataField="Population_Latitude" HeaderText="Latitude" ReadOnly="True" SortExpression="Population_Latitude" ItemStyle-Width="70px" />
                      <asp:BoundField DataField="Population_Longitude" HeaderText="Longitude" ReadOnly="True" SortExpression="Population_Longitude" ItemStyle-Width="70px" />
                    </Columns>
                  </asp:GridView>
                  <asp:ObjectDataSource ID="ObjectDataSource_ExecutiveMarketInquiry_Population_List" runat="server" OnSelected="ObjectDataSource_ExecutiveMarketInquiry_Population_List_Selected"></asp:ObjectDataSource>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <div id="DivMedicalScheme" runat="server" style="height: 40px;">
        &nbsp;
      </div>
      <table id="TableMedicalScheme" runat="server" class="Table" style="width: 1000px;">
        <tr>
          <td>
            <table class="Table_Header">
              <tr>
                <td>
                  <asp:Label ID="Label_GridHeading_MedicalScheme" runat="server" Text=""></asp:Label>
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <table class="Table_Body">
              <tr>
                <td>Total Medical Scheme Beneficiaries:
                    <asp:Label ID="Label_TotalMedicalScheme_List" runat="server" Text=""></asp:Label>
                </td>
              </tr>
              <tr>
                <td style="padding: 0px;">
                  <asp:GridView ID="GridView_ExecutiveMarketInquiry_MedicalScheme_List" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource_ExecutiveMarketInquiry_MedicalScheme_List" CssClass="GridView" AllowPaging="False" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="1000" OnPreRender="GridView_ExecutiveMarketInquiry_MedicalScheme_List_PreRender" OnDataBound="GridView_ExecutiveMarketInquiry_MedicalScheme_List_DataBound">
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
                            <asp:DropDownList ID="DropDownList_PageSize_MedicalScheme" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_MedicalScheme_SelectedIndexChanged">
                              <asp:ListItem Value="20">20</asp:ListItem>
                              <asp:ListItem Value="50">50</asp:ListItem>
                              <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList>
                          </td>
                          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          </td>
                          <td>
                            <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />&nbsp;
                                <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />&nbsp;
                          </td>
                          <td>Page
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_Page_MedicalScheme" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_MedicalScheme_SelectedIndexChanged">
                            </asp:DropDownList>
                          </td>
                          <td>of
                          <%=GridView_ExecutiveMarketInquiry_MedicalScheme_List.PageCount%>
                          </td>
                          <td>
                            <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />&nbsp;
                                <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />&nbsp;
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
                      <asp:BoundField DataField="MedicalScheme_Name" HeaderText="Name" ReadOnly="True" SortExpression="MedicalScheme_Name" />
                      <asp:TemplateField HeaderText="Medical Scheme Beneficiaries" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" SortExpression="MedicalScheme_Value" ItemStyle-Width="100px">
                        <ItemTemplate>
                          <asp:Label ID="Label_MedicalScheme_Value" runat="server" Text='<%# Bind("MedicalScheme_Value","{0:#,##0}") %>'></asp:Label>&nbsp;
                          <asp:HiddenField ID="HiddenField_MedicalScheme_Level" runat="server" Value='<%# Bind("MedicalScheme_Level") %>' />
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="MedicalScheme_Type" HeaderText="Type" ReadOnly="True" SortExpression="MedicalScheme_Type" />
                      <asp:BoundField DataField="MedicalScheme_Year" HeaderText="Year" ReadOnly="True" SortExpression="MedicalScheme_Year" />
                      <asp:BoundField DataField="MedicalScheme_Address" HeaderText="Address" ReadOnly="True" SortExpression="MedicalScheme_Address" />
                      <asp:BoundField DataField="MedicalScheme_Latitude" HeaderText="Latitude" ReadOnly="True" SortExpression="MedicalScheme_Latitude" ItemStyle-Width="70px" />
                      <asp:BoundField DataField="MedicalScheme_Longitude" HeaderText="Longitude" ReadOnly="True" SortExpression="MedicalScheme_Longitude" ItemStyle-Width="70px" />
                    </Columns>
                  </asp:GridView>
                  <asp:ObjectDataSource ID="ObjectDataSource_ExecutiveMarketInquiry_MedicalScheme_List" runat="server" OnSelected="ObjectDataSource_ExecutiveMarketInquiry_MedicalScheme_List_Selected"></asp:ObjectDataSource>
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
