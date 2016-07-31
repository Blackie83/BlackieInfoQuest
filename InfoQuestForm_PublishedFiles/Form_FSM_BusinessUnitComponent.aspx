<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_FSM_BusinessUnitComponent.aspx.cs" Inherits="InfoQuestForm.Form_FSM_BusinessUnitComponent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Facility Structure Maintenance - Business Unit Component</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_FSM_BusinessUnitComponent.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_FSM_BusinessUnitComponent" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_FSM_BusinessUnitComponent" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_FSM_BusinessUnitComponent" AssociatedUpdatePanelID="UpdatePanel_FSM_BusinessUnitComponent">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_FSM_BusinessUnitComponent" runat="server">
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
          <table id="TableBusinessUnitComponent" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_BusinessUnitComponentHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_FSM_BusinessUnitComponent_Form" runat="server" DataKeyNames="BusinessUnitComponentKey" CssClass="FormView" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_Form" OnItemInserting="FormView_FSM_BusinessUnitComponent_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_FSM_BusinessUnitComponent_Form_ItemCommand" OnDataBound="FormView_FSM_BusinessUnitComponent_Form_DataBound" OnItemUpdating="FormView_FSM_BusinessUnitComponent_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitComponentName">Name
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_InsertBusinessUnitComponentName" runat="server" Text='<%# Bind("BusinessUnitComponentName") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitKey">Business Unit
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertBusinessUnitKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_InsertBusinessUnitKey" AppendDataBoundItems="true" DataTextField="BusinessUnitName" DataValueField="BusinessUnitKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertBusinessUnitKey_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Business Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitComponentTypeKey">Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertBusinessUnitComponentTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_InsertBusinessUnitComponentTypeKey" AppendDataBoundItems="true" DataTextField="BusinessUnitComponentTypeName" DataValueField="BusinessUnitComponentTypeKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertBusinessUnitComponentTypeKey_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormParentBusinessUnitComponentKey">Parent
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertParentBusinessUnitComponentKey" runat="server" AppendDataBoundItems="true" DataTextField="BusinessUnitComponentName" DataValueField="BusinessUnitComponentKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertParentBusinessUnitComponentKey_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Parent</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeWard1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeWard2">
                        <td colspan="2" class="FormView_TableBodyHeader">Ward
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeWard3">
                        <td style="width: 170px;" id="FormWardTypeKey">Ward Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertWardTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_InsertWardTypeKey" AppendDataBoundItems="true" DataTextField="WardTypeName" DataValueField="WardTypeKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertWardTypeKey_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeWard4">
                        <td style="width: 170px;" id="FormNursingDisciplineKey_Ward">Nursing Discipline
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertNursingDisciplineKey_Ward" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_InsertNursingDisciplineKey_Ward" AppendDataBoundItems="true" DataTextField="NursingDisciplineName" DataValueField="NursingDisciplineKey" SelectedValue='<%# Bind("NursingDisciplineKey_Ward") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Discipline</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeTheatreComplex1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeTheatreComplex2">
                        <td colspan="2" class="FormView_TableBodyHeader">Theatre Complex
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeTheatreComplex3">
                        <td style="width: 170px;" id="FormTheatreComplexTypeKey">Theatre Complex Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertTheatreComplexTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_InsertTheatreComplexTypeKey" AppendDataBoundItems="true" DataTextField="TheatreComplexTypeName" DataValueField="TheatreComplexTypeKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertTheatreComplexTypeKey_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeTheatreComplex4">
                        <td style="width: 170px;" id="FormNursingDisciplineKey_TheatreComplex">Nursing Discipline
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertNursingDisciplineKey_TheatreComplex" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_InsertNursingDisciplineKey_TheatreComplex" AppendDataBoundItems="true" DataTextField="NursingDisciplineName" DataValueField="NursingDisciplineKey" SelectedValue='<%# Bind("NursingDisciplineKey_TheatreComplex") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Discipline</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOperatingTheatre1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOperatingTheatre2">
                        <td colspan="2" class="FormView_TableBodyHeader">Operating Theatre
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOperatingTheatre3">
                        <td style="width: 170px;" id="FormOperatingTheatreTypeKey">Operating Theatre Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertOperatingTheatreTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_InsertOperatingTheatreTypeKey" AppendDataBoundItems="true" DataTextField="OperatingTheatreTypeName" DataValueField="OperatingTheatreTypeKey" SelectedValue='<%# Bind("OperatingTheatreTypeKey") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Operating Theatre</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeStockroom1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeStockroom2">
                        <td colspan="2" class="FormView_TableBodyHeader">Stock Room
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeStockroom3">
                        <td style="width: 170px;" id="FormStockroomTypeKey">Stock Room Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertStockroomTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_InsertStockroomTypeKey" AppendDataBoundItems="true" DataTextField="StockroomTypeName" DataValueField="StockroomTypeKey" SelectedValue='<%# Bind("StockRoomTypeKey") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Stock Room</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeRetailPharmacy1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeRetailPharmacy2">
                        <td colspan="2" class="FormView_TableBodyHeader">Retail Pharmacy
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeRetailPharmacy3">
                        <td style="width: 170px;" id="FormRetailPharmacyPracticeNumber">Practice Number
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_InsertRetailPharmacyPracticeNumber" runat="server" Text='<%# Bind("RetailPharmacyPracticeNumber") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeSupportFunction1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeSupportFunction2">
                        <td colspan="2" class="FormView_TableBodyHeader">Support Function
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeSupportFunction3">
                        <td style="width: 170px;" id="FormSupportFunctionTypeKey">Support Function Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertSupportFunctionTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_InsertSupportFunctionTypeKey" AppendDataBoundItems="true" DataTextField="SupportFunctionTypeName" DataValueField="SupportFunctionTypeKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertSupportFunctionTypeKey_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOther1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOther2">
                        <td colspan="2" class="FormView_TableBodyHeader">Ward / Theatre Complex / Pharmacy / Purchasing Site / Stock Room / Retail Pharmacy / Support Function
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOther3">
                        <td style="width: 170px;" id="FormEntity">Entity Code
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_InsertEntity" runat="server" CssClass="Controls_TextBox" Width="100px" MaxLength="5" AutoPostBack="true" OnTextChanged="TextBox_InsertEntity_TextChanged"></asp:TextBox>&nbsp;
                          <asp:Label ID="Label_InsertEntityLookup" runat="server"></asp:Label>&nbsp;
                          <asp:HiddenField ID="HiddenField_InsertEntity" runat="server" />
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOther4">
                        <td style="width: 170px;" id="FormCostCentre">Cost Centre Code
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_InsertCostCentre" runat="server" CssClass="Controls_TextBox" Width="100px" MaxLength="4" AutoPostBack="true" OnTextChanged="TextBox_InsertCostCentre_TextChanged"></asp:TextBox>&nbsp;
                          <asp:Label ID="Label_InsertCostCentreLookup" runat="server"></asp:Label>&nbsp;
                          <asp:HiddenField ID="HiddenField_InsertCostCentre" runat="server" />
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnitComponent1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnitComponent2">
                        <td colspan="2" class="FormView_TableBodyHeader">Business Unit Component Mapping
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnitComponent3">
                        <td colspan="2">
                          <table class="Table_Body">
                            <tr>
                              <td>Total Records:
                                <asp:Label ID="Label_InsertTotalRecords" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="HiddenField_InsertTotalRecords" runat="server" />
                              </td>
                            </tr>
                            <tr>
                              <td style="padding: 0px;">
                                <asp:GridView ID="GridView_InsertMappingBusinessUnitComponent" runat="server" AllowPaging="True" PageSize="1000" AutoGenerateColumns="false" CssClass="GridView" GridLines="None" BorderWidth="0px" ShowFooter="True" OnPreRender="GridView_InsertMappingBusinessUnitComponent_PreRender" OnDataBound="GridView_InsertMappingBusinessUnitComponent_DataBound" OnRowCreated="GridView_InsertMappingBusinessUnitComponent_RowCreated">
                                  <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                                  <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                                  <PagerTemplate>
                                    <table class="GridView_PagerStyle">
                                      <tr>
                                        <td>&nbsp;</td>
                                      </tr>
                                    </table>
                                  </PagerTemplate>
                                  <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                                  <FooterStyle CssClass="GridView_RowStyle_TemplateField" />
                                  <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                                  <EmptyDataRowStyle CssClass="GridView_EmptyDataStyle_TemplateField" />
                                  <EmptyDataTemplate>
                                    <table class="GridView_EmptyDataStyle">
                                      <tr>
                                        <td>No Business Unit Component Mapping
                                        </td>
                                      </tr>
                                      <tr class="GridView_EmptyDataStyle_FooterStyle">
                                        <td>&nbsp;</td>
                                      </tr>
                                    </table>
                                  </EmptyDataTemplate>
                                  <Columns>
                                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="GridView_RowStyle_TemplateField_td">
                                      <ItemTemplate>
                                        <table class="Table_Body">
                                          <tr>
                                            <td class="Table_TemplateField" style="width: 200px;">
                                              <asp:Label ID="Label_InsertSourceSystemName" runat="server" Text='<%# Bind("SourceSystemName") %>' Width="200px"></asp:Label>
                                              <asp:HiddenField ID="HiddenField_InsertSourceSystemKey" runat="server" Value='<%# Eval("SourceSystemKey") %>' />
                                            </td>
                                            <td class="Table_TemplateField" style="width: 700px;">
                                              <asp:TextBox ID="TextBox_InsertSourceSystemValue" runat="server" Width="200px" CssClass="Controls_TextBox"></asp:TextBox>
                                            </td>
                                          </tr>
                                        </table>
                                      </ItemTemplate>
                                    </asp:TemplateField>
                                  </Columns>
                                </asp:GridView>
                              </td>
                            </tr>
                          </table>
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertGoToList" runat="server" Text="Go To List" CssClass="Controls_Button" OnClick="Button_InsertGoToList_Click" CausesValidation="False" />&nbsp;
                          <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertCancel_Click" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="false" CommandName="Insert" Text="Add Business Unit Component" CssClass="Controls_Button" />&nbsp;
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
                        <td style="width: 170px;" id="FormBusinessUnitComponentName">Name
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditBusinessUnitComponentName" runat="server" Text='<%# Bind("BusinessUnitComponentName") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitKey">Business Unit
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditBusinessUnitKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_EditBusinessUnitKey" AppendDataBoundItems="true" DataTextField="BusinessUnitName" DataValueField="BusinessUnitKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditBusinessUnitKey_SelectedIndexChanged" OnDataBound="DropDownList_EditBusinessUnitKey_DataBound">
                            <asp:ListItem Value="">Select Business Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitComponentTypeKey">Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditBusinessUnitComponentTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_EditBusinessUnitComponentTypeKey" AppendDataBoundItems="true" DataTextField="BusinessUnitComponentTypeName" DataValueField="BusinessUnitComponentTypeKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditBusinessUnitComponentTypeKey_SelectedIndexChanged" OnDataBound="DropDownList_EditBusinessUnitComponentTypeKey_DataBound">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormParentBusinessUnitComponentKey">Parent
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditParentBusinessUnitComponentKey" runat="server" AppendDataBoundItems="true" DataTextField="BusinessUnitComponentName" DataValueField="BusinessUnitComponentKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditParentBusinessUnitComponentKey_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Parent</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeWard1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeWard2">
                        <td colspan="2" class="FormView_TableBodyHeader">Ward
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeWard3">
                        <td style="width: 170px;" id="FormWardTypeKey">Ward Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditWardTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_EditWardTypeKey" AppendDataBoundItems="true" DataTextField="WardTypeName" DataValueField="WardTypeKey" SelectedValue='<%# Bind("WardTypeKey") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditWardTypeKey_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeWard4">
                        <td style="width: 170px;" id="FormNursingDisciplineKey_Ward">Nursing Discipline
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditNursingDisciplineKey_Ward" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_EditNursingDisciplineKey_Ward" AppendDataBoundItems="true" DataTextField="NursingDisciplineName" DataValueField="NursingDisciplineKey" SelectedValue='<%# Bind("NursingDisciplineKey_Ward") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Discipline</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeTheatreComplex1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeTheatreComplex2">
                        <td colspan="2" class="FormView_TableBodyHeader">Theatre Complex
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeTheatreComplex3">
                        <td style="width: 170px;" id="FormTheatreComplexTypeKey">Theatre Complex Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditTheatreComplexTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_EditTheatreComplexTypeKey" AppendDataBoundItems="true" DataTextField="TheatreComplexTypeName" DataValueField="TheatreComplexTypeKey" SelectedValue='<%# Bind("TheatreComplexTypeKey") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditTheatreComplexTypeKey_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeTheatreComplex4">
                        <td style="width: 170px;" id="FormNursingDisciplineKey_TheatreComplex">Nursing Discipline
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditNursingDisciplineKey_TheatreComplex" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_EditNursingDisciplineKey_TheatreComplex" AppendDataBoundItems="true" DataTextField="NursingDisciplineName" DataValueField="NursingDisciplineKey" SelectedValue='<%# Bind("NursingDisciplineKey_TheatreComplex") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Discipline</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOperatingTheatre1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOperatingTheatre2">
                        <td colspan="2" class="FormView_TableBodyHeader">Operating Theatre
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOperatingTheatre3">
                        <td style="width: 170px;" id="FormOperatingTheatreTypeKey">Operating Theatre Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditOperatingTheatreTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_EditOperatingTheatreTypeKey" AppendDataBoundItems="true" DataTextField="OperatingTheatreTypeName" DataValueField="OperatingTheatreTypeKey" SelectedValue='<%# Bind("OperatingTheatreTypeKey") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Operating Theatre</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeStockroom1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeStockroom2">
                        <td colspan="2" class="FormView_TableBodyHeader">Stock Room
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeStockroom3">
                        <td style="width: 170px;" id="FormStockroomTypeKey">Stock Room Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditStockroomTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_EditStockroomTypeKey" AppendDataBoundItems="true" DataTextField="StockroomTypeName" DataValueField="StockroomTypeKey" SelectedValue='<%# Bind("StockRoomTypeKey") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Stock Room</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeRetailPharmacy1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeRetailPharmacy2">
                        <td colspan="2" class="FormView_TableBodyHeader">Retail Pharmacy
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeRetailPharmacy3">
                        <td style="width: 170px;" id="FormRetailPharmacyPracticeNumber">Practice Number
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditRetailPharmacyPracticeNumber" runat="server" Text='<%# Bind("RetailPharmacyPracticeNumber") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeSupportFunction1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeSupportFunction2">
                        <td colspan="2" class="FormView_TableBodyHeader">Support Function
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeSupportFunction3">
                        <td style="width: 170px;" id="FormSupportFunctionTypeKey">Support Function Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditSupportFunctionTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_EditSupportFunctionTypeKey" AppendDataBoundItems="true" DataTextField="SupportFunctionTypeName" DataValueField="SupportFunctionTypeKey" SelectedValue='<%# Bind("SupportFunctionTypeKey") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditSupportFunctionTypeKey_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOther1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOther2">
                        <td colspan="2" class="FormView_TableBodyHeader">Ward / Theatre Complex / Pharmacy / Purchasing Site / Stock Room / Retail Pharmacy / Support Function
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOther3">
                        <td style="width: 170px;" id="FormEntity">Entity Code
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditEntity" runat="server" CssClass="Controls_TextBox" Width="100px" MaxLength="5" AutoPostBack="true" OnTextChanged="TextBox_EditEntity_TextChanged"></asp:TextBox>&nbsp;
                          <asp:Label ID="Label_EditEntityLookup" runat="server"></asp:Label>&nbsp;
                          <asp:HiddenField ID="HiddenField_EditEntity" runat="server" />
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOther4">
                        <td style="width: 170px;" id="FormCostCentre">Cost Centre Code
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditCostCentre" runat="server" CssClass="Controls_TextBox" Width="100px" MaxLength="4" AutoPostBack="true" OnTextChanged="TextBox_EditCostCentre_TextChanged"></asp:TextBox>&nbsp;
                          <asp:Label ID="Label_EditCostCentreLookup" runat="server"></asp:Label>&nbsp;
                          <asp:HiddenField ID="HiddenField_EditCostCentre" runat="server" />
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnitComponent1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnitComponent2">
                        <td colspan="2" class="FormView_TableBodyHeader">Business Unit Component Mapping
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnitComponent3">
                        <td colspan="2">
                          <table class="Table_Body">
                            <tr>
                              <td>Total Records:
                                <asp:Label ID="Label_EditTotalRecords" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="HiddenField_EditTotalRecords" runat="server" />
                              </td>
                            </tr>
                            <tr>
                              <td style="padding: 0px;">
                                <asp:GridView ID="GridView_EditMappingBusinessUnitComponent" runat="server" AllowPaging="True" PageSize="1000" AutoGenerateColumns="false" CssClass="GridView" BorderWidth="0px" ShowFooter="True" OnPreRender="GridView_EditMappingBusinessUnitComponent_PreRender" OnDataBound="GridView_EditMappingBusinessUnitComponent_DataBound" OnRowCreated="GridView_EditMappingBusinessUnitComponent_RowCreated" OnRowDataBound="GridView_EditMappingBusinessUnitComponent_RowDataBound">
                                  <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                                  <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                                  <PagerTemplate>
                                    <table class="GridView_PagerStyle">
                                      <tr>
                                        <td>&nbsp;</td>
                                      </tr>
                                    </table>
                                  </PagerTemplate>
                                  <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                                  <FooterStyle CssClass="GridView_RowStyle_TemplateField" />
                                  <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                                  <EmptyDataRowStyle CssClass="GridView_EmptyDataStyle_TemplateField" />
                                  <EmptyDataTemplate>
                                    <table class="GridView_EmptyDataStyle">
                                      <tr>
                                        <td>No Business Unit Component Mapping
                                        </td>
                                      </tr>
                                      <tr class="GridView_EmptyDataStyle_FooterStyle">
                                        <td>&nbsp;</td>
                                      </tr>
                                    </table>
                                  </EmptyDataTemplate>
                                  <Columns>
                                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="GridView_RowStyle_TemplateField_td">
                                      <ItemTemplate>
                                        <table>
                                          <tr>
                                            <td class="Table_TemplateField_1" style="width: 200px;">
                                              <asp:Label ID="Label_EditSourceSystemName" runat="server" Text='<%# Bind("SourceSystemName") %>' Width="200px"></asp:Label>
                                              <asp:HiddenField ID="HiddenField_EditSourceSystemKey" runat="server" Value='<%# Eval("SourceSystemKey") %>' />
                                            </td>
                                            <td class="Table_TemplateField_1" style="width: 700px;">
                                              <asp:TextBox ID="TextBox_EditSourceSystemValue" runat="server" Width="200px" CssClass="Controls_TextBox"></asp:TextBox>
                                            </td>
                                          </tr>
                                        </table>
                                      </ItemTemplate>
                                    </asp:TemplateField>
                                  </Columns>
                                </asp:GridView>
                              </td>
                            </tr>
                          </table>
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditGoToList" runat="server" Text="Go To List" CssClass="Controls_Button" OnClick="Button_EditGoToList_Click" CausesValidation="False" />&nbsp;
                          <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditCancel_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Business Unit Component" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="3"></td>
                      </tr>
                      <tr>
                        <td style="width: 170px;">Name
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:Label ID="Label_ItemBusinessUnitComponentName" runat="server" Text='<%# Bind("BusinessUnitComponentName") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitKey">Business Unit
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemBusinessUnitKey" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitComponentTypeKey">Type
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemBusinessUnitComponentTypeKey" runat="server"></asp:Label>                          
                          <asp:HiddenField ID="HiddenField_ItemBusinessUnitComponentTypeKey" runat="server" Value='<%# Eval("BusinessUnitComponentTypeKey") %>' />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormParentBusinessUnitComponentKey">Parent
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemParentBusinessUnitComponentKey" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeWard1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeWard2">
                        <td colspan="2" class="FormView_TableBodyHeader">Ward
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeWard3">
                        <td style="width: 170px;" id="FormWardTypeKey">Ward Type
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemWardTypeKey" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeWard4">
                        <td style="width: 170px;" id="FormNursingDisciplineKey_Ward">Nursing Discipline
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursingDisciplineKey_Ward" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeTheatreComplex1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeTheatreComplex2">
                        <td colspan="2" class="FormView_TableBodyHeader">Theatre Complex
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeTheatreComplex3">
                        <td style="width: 170px;" id="FormTheatreComplexTypeKey">Theatre Complex Type
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemTheatreComplexTypeKey" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeTheatreComplex4">
                        <td style="width: 170px;" id="FormNursingDisciplineKey_TheatreComplex">Nursing Discipline
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemNursingDisciplineKey_TheatreComplex" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOperatingTheatre1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOperatingTheatre2">
                        <td colspan="2" class="FormView_TableBodyHeader">Operating Theatre
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOperatingTheatre3">
                        <td style="width: 170px;" id="FormOperatingTheatreTypeKey">Operating Theatre Type
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemOperatingTheatreTypeKey" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeStockroom1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeStockroom2">
                        <td colspan="2" class="FormView_TableBodyHeader">Stock Room
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeStockroom3">
                        <td style="width: 170px;" id="FormStockroomTypeKey">Stock Room Type
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemStockroomTypeKey" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeRetailPharmacy1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeRetailPharmacy2">
                        <td colspan="2" class="FormView_TableBodyHeader">Retail Pharmacy
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeRetailPharmacy3">
                        <td style="width: 170px;" id="FormRetailPharmacyPracticeNumber">Practice Number
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemRetailPharmacyPracticeNumber" runat="server" Text='<%# Bind("RetailPharmacyPracticeNumber") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeSupportFunction1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeSupportFunction2">
                        <td colspan="2" class="FormView_TableBodyHeader">Support Function
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeSupportFunction3">
                        <td style="width: 170px;" id="FormSupportFunctionTypeKey">Support Function Type
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemSupportFunctionTypeKey" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOther1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOther2">
                        <td colspan="2" class="FormView_TableBodyHeader">Ward / Theatre Complex / Pharmacy / Purchasing Site / Stock Room / Retail Pharmacy / Support Function
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOther3">
                        <td style="width: 170px;" id="FormEntity">Entity Code
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemEntity" runat="server"></asp:Label>&nbsp;
                          <asp:Label ID="Label_ItemEntityLookup" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitComponentTypeOther4">
                        <td style="width: 170px;" id="FormCostCentre">Cost Centre Code
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemCostCentre" runat="server"></asp:Label>&nbsp;
                          <asp:Label ID="Label_ItemCostCentreLookup" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnitComponent1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnitComponent2">
                        <td colspan="2" class="FormView_TableBodyHeader">Business Unit Component Mapping
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnitComponent3">
                        <td colspan="2" style="padding: 0px;">
                          <asp:GridView ID="GridView_ItemMappingBusinessUnitComponent" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_FSM_BusinessUnitComponent_ItemMappingBusinessUnitComponent" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="True" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemMappingBusinessUnitComponent_RowCreated" OnPreRender="GridView_ItemMappingBusinessUnitComponent_PreRender">
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
                            <FooterStyle CssClass="GridView_FooterStyle" />
                            <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                              No Business Unit Component Mapping
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="SourceSystemName" HeaderText="Source System" ItemStyle-Width="200px" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" SortExpression="SourceSystemName" />
                              <asp:BoundField DataField="SourceSystemValue" HeaderText="Value" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" SortExpression="SourceSystemValue" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="3">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="2">
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="3">
                          <asp:Button ID="Button_ItemGoToList" runat="server" Text="Go To List" CssClass="Controls_Button" OnClick="Button_ItemGoToList_Click" CausesValidation="False" />&nbsp;
                          <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" OnClick="Button_ItemCancel_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_InsertBusinessUnitKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_InsertBusinessUnitComponentTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_InsertWardTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_InsertNursingDisciplineKey_Ward" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_InsertTheatreComplexTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_InsertNursingDisciplineKey_TheatreComplex" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_InsertOperatingTheatreTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_InsertStockroomTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_InsertSupportFunctionTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_EditBusinessUnitKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_EditBusinessUnitComponentTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_EditWardTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_EditNursingDisciplineKey_Ward" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_EditTheatreComplexTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_EditNursingDisciplineKey_TheatreComplex" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_EditOperatingTheatreTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_EditStockroomTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_EditSupportFunctionTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_ItemMappingBusinessUnitComponent" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnitComponent_Form" runat="server" OnInserted="SqlDataSource_FSM_BusinessUnitComponent_Form_Inserted" OnUpdated="SqlDataSource_FSM_BusinessUnitComponent_Form_Updated"></asp:SqlDataSource>
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
