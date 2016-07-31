<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_FSM_BusinessUnit.aspx.cs" Inherits="InfoQuestForm.Form_FSM_BusinessUnit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Facility Structure Maintenance - Business Unit</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_FSM_BusinessUnit.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_FSM_BusinessUnit" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_FSM_BusinessUnit" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_FSM_BusinessUnit" AssociatedUpdatePanelID="UpdatePanel_FSM_BusinessUnit">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_FSM_BusinessUnit" runat="server">
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
          <table id="TableBusinessUnit" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_BusinessUnitHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_FSM_BusinessUnit_Form" runat="server" DataKeyNames="BusinessUnitKey" CssClass="FormView" DataSourceID="SqlDataSource_FSM_BusinessUnit_Form" OnItemInserting="FormView_FSM_BusinessUnit_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_FSM_BusinessUnit_Form_ItemCommand" OnDataBound="FormView_FSM_BusinessUnit_Form_DataBound" OnItemUpdating="FormView_FSM_BusinessUnit_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitName">Name
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_InsertBusinessUnitName" runat="server" Text='<%# Bind("BusinessUnitName") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitTypeKey">Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertBusinessUnitTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnit_InsertBusinessUnitTypeKey" AppendDataBoundItems="true" DataTextField="BusinessUnitTypeName" DataValueField="BusinessUnitTypeKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertBusinessUnitTypeKey_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLocationKey">Location
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertLocationKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnit_InsertLocationKey" AppendDataBoundItems="true" DataTextField="LocationName" DataValueField="LocationKey" SelectedValue='<%# Bind("LocationKey") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Location</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitReportingGroupKey">Reporting Group
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:DropDownList ID="DropDownList_InsertBusinessUnitReportingGroupKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnit_InsertBusinessUnitReportingGroupKey" AppendDataBoundItems="true" DataTextField="BusinessUnitReportingGroupName" DataValueField="BusinessUnitReportingGroupKey" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Reporting Group</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitDefaultEntity">Entity Code
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_InsertBusinessUnitDefaultEntity" runat="server" Text='<%# Bind("BusinessUnitDefaultEntity") %>' CssClass="Controls_TextBox" Width="100px" MaxLength="5" AutoPostBack="true" OnTextChanged="TextBox_InsertBusinessUnitDefaultEntity_TextChanged"></asp:TextBox>&nbsp;
                          <asp:Label ID="Label_InsertBusinessUnitDefaultEntityLookup" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital2">
                        <td colspan="2" class="FormView_TableBodyHeader">Hospital
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital3">
                        <td style="width: 170px;" id="FormRegisteredName">Registered Name
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_InsertRegisteredName" runat="server" Text='<%# Bind("RegisteredName") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital4">
                        <td style="width: 170px;" id="FormShortName">Short Name
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_InsertShortName" runat="server" Text='<%# Bind("ShortName") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital5">
                        <td style="width: 170px;" id="FormPracticeNumber">Practice Number
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_InsertPracticeNumber" runat="server" Text='<%# Bind("PracticeNumber") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital6">
                        <td style="width: 170px;" id="FormHospitalTypeKey">Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_InsertHospitalTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnit_InsertHospitalTypeKey" AppendDataBoundItems="true" DataTextField="HospitalTypeName" DataValueField="HospitalTypeKey" SelectedValue='<%# Bind("HospitalTypeKey") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnit1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnit2">
                        <td colspan="2" class="FormView_TableBodyHeader">Business Unit Mapping
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnit3">
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
                                <asp:GridView ID="GridView_InsertMappingBusinessUnit" runat="server" AllowPaging="True" PageSize="1000" AutoGenerateColumns="false" CssClass="GridView" BorderWidth="0px" ShowFooter="True" OnPreRender="GridView_InsertMappingBusinessUnit_PreRender" OnDataBound="GridView_InsertMappingBusinessUnit_DataBound" OnRowCreated="GridView_InsertMappingBusinessUnit_RowCreated">
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
                                        <td>No Business Unit Mapping
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
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="false" CommandName="Insert" Text="Add Business Unit" CssClass="Controls_Button" />&nbsp;
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
                        <td style="width: 170px;" id="FormBusinessUnitName">Name
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditBusinessUnitName" runat="server" Text='<%# Bind("BusinessUnitName") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;                    
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitTypeKey">Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditBusinessUnitTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnit_EditBusinessUnitTypeKey" AppendDataBoundItems="true" DataTextField="BusinessUnitTypeName" DataValueField="BusinessUnitTypeKey" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditBusinessUnitTypeKey_SelectedIndexChanged" OnDataBound="DropDownList_EditBusinessUnitTypeKey_DataBound">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLocationKey">Location
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditLocationKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnit_EditLocationKey" AppendDataBoundItems="true" DataTextField="LocationName" DataValueField="LocationKey" SelectedValue='<%# Bind("LocationKey") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Location</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitReportingGroupKey">Reporting Group
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:DropDownList ID="DropDownList_EditBusinessUnitReportingGroupKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnit_EditBusinessUnitReportingGroupKey" AppendDataBoundItems="true" DataTextField="BusinessUnitReportingGroupName" DataValueField="BusinessUnitReportingGroupKey" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Reporting Group</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitDefaultEntity">Entity Code
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditBusinessUnitDefaultEntity" runat="server" Text='<%# Bind("BusinessUnitDefaultEntity") %>' CssClass="Controls_TextBox" Width="100px" MaxLength="5" AutoPostBack="true" OnTextChanged="TextBox_EditBusinessUnitDefaultEntity_TextChanged" OnDataBinding="TextBox_EditBusinessUnitDefaultEntity_DataBinding"></asp:TextBox>&nbsp;
                          <asp:Label ID="Label_EditBusinessUnitDefaultEntityLookup" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital2">
                        <td colspan="2" class="FormView_TableBodyHeader">Hospital
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital3">
                        <td style="width: 170px;" id="FormRegisteredName">Registered Name
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditRegisteredName" runat="server" Text='<%# Bind("RegisteredName") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital4">
                        <td style="width: 170px;" id="FormShortName">Short Name
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditShortName" runat="server" Text='<%# Bind("ShortName") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital5">
                        <td style="width: 170px;" id="FormPracticeNumber">Practice Number
                        </td>
                        <td style="width: 730px;">
                          <asp:TextBox ID="TextBox_EditPracticeNumber" runat="server" Text='<%# Bind("PracticeNumber") %>' CssClass="Controls_TextBox" Width="700px"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital6">
                        <td style="width: 170px;" id="FormHospitalTypeKey">Type
                        </td>
                        <td style="width: 730px;">
                          <asp:DropDownList ID="DropDownList_EditHospitalTypeKey" runat="server" DataSourceID="SqlDataSource_FSM_BusinessUnit_EditHospitalTypeKey" AppendDataBoundItems="true" DataTextField="HospitalTypeName" DataValueField="HospitalTypeKey" SelectedValue='<%# Bind("HospitalTypeKey") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnit1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnit2">
                        <td colspan="2" class="FormView_TableBodyHeader">Business Unit Mapping
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnit3">
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
                                <asp:GridView ID="GridView_EditMappingBusinessUnit" runat="server" AllowPaging="True" PageSize="1000" AutoGenerateColumns="false" CssClass="GridView" BorderWidth="0px" ShowFooter="True" OnPreRender="GridView_EditMappingBusinessUnit_PreRender" OnDataBound="GridView_EditMappingBusinessUnit_DataBound" OnRowCreated="GridView_EditMappingBusinessUnit_RowCreated" OnRowDataBound="GridView_EditMappingBusinessUnit_RowDataBound">
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
                                        <td>No Business Unit Mapping
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
                                              <asp:Label ID="Label_EditSourceSystemName" runat="server" Text='<%# Bind("SourceSystemName") %>' Width="200px"></asp:Label>
                                              <asp:HiddenField ID="HiddenField_EditSourceSystemKey" runat="server" Value='<%# Eval("SourceSystemKey") %>' />
                                            </td>
                                            <td class="Table_TemplateField" style="width: 700px;">
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
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Business Unit" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
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
                          <asp:Label ID="Label_ItemBusinessUnitName" runat="server" Text='<%# Bind("BusinessUnitName") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitTypeKey">Type
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemBusinessUnitTypeKey" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemBusinessUnitTypeKey" runat="server" Value='<%# Eval("BusinessUnitTypeKey") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormLocationKey">Location
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemLocationKey" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitReportingGroupKey">Reporting Group
                        </td>
                        <td style="width: 730px;" colspan="2">
                          <asp:Label ID="Label_ItemBusinessUnitReportingGroupKey" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 170px;" id="FormBusinessUnitDefaultEntity">Entity Code
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemBusinessUnitDefaultEntity" runat="server" Text='<%# Bind("BusinessUnitDefaultEntity") %>' OnDataBinding="Label_ItemBusinessUnitDefaultEntity_DataBinding"></asp:Label>&nbsp;
                          <asp:Label ID="Label_ItemBusinessUnitDefaultEntityLookup" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital2">
                        <td colspan="2" class="FormView_TableBodyHeader">Hospital
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital3">
                        <td style="width: 170px;" id="FormRegisteredName">Registered Name
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemRegisteredName" runat="server" Text='<%# Bind("RegisteredName") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital4">
                        <td style="width: 170px;" id="FormShortName">Short Name
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemShortName" runat="server" Text='<%# Bind("ShortName") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital5">
                        <td style="width: 170px;" id="FormPracticeNumber">Practice Number
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemPracticeNumber" runat="server" Text='<%# Bind("PracticeNumber") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr id="BusinessUnitTypeHospital6">
                        <td style="width: 170px;" id="FormHospitalTypeKey">Type
                        </td>
                        <td style="width: 730px;">
                          <asp:Label ID="Label_ItemHospitalTypeKey" runat="server"></asp:Label>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnit1">
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnit2">
                        <td colspan="2" class="FormView_TableBodyHeader">Business Unit Mapping
                        </td>
                      </tr>
                      <tr id="MappingBusinessUnit3">
                        <td colspan="2" style="padding: 0px;">
                          <asp:GridView ID="GridView_ItemMappingBusinessUnit" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_FSM_BusinessUnit_ItemMappingBusinessUnit" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="True" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemMappingBusinessUnit_RowCreated" OnPreRender="GridView_ItemMappingBusinessUnit_PreRender">
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
                              No Business Unit Mapping
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
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnit_InsertBusinessUnitTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnit_InsertLocationKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnit_InsertBusinessUnitReportingGroupKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnit_InsertHospitalTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnit_EditBusinessUnitTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnit_EditLocationKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnit_EditBusinessUnitReportingGroupKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnit_EditHospitalTypeKey" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnit_ItemMappingBusinessUnit" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_FSM_BusinessUnit_Form" runat="server" OnInserted="SqlDataSource_FSM_BusinessUnit_Form_Inserted" OnUpdated="SqlDataSource_FSM_BusinessUnit_Form_Updated"></asp:SqlDataSource>
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
