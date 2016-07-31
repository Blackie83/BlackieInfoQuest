<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_Isidima_Admin" CodeBehind="Form_Isidima_Admin.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Isidima Administration</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_Isidima_Admin.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body onload="Validation_Link();">
  <form id="form_Isidima_Admin" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Isidima_Admin" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <table cellspacing="0" cellpadding="0" border="0">
        <tr>
          <td>
            <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Esidimeni/85_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
          </td>
          <td style="width: 25px"></td>
          <td style="color: #003768; font-size: 18px; font-weight: bold; padding-top: 18px; padding-bottom: 7px">
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
      <table cellspacing="0" cellpadding="0" width="700px" border="0">
        <tr>
          <td style="text-align: center;">
            <asp:Button ID="Button_GoToForm" runat="server" Text="Go Back To Form" CssClass="Controls_Button" OnClick="Button_GoToForm_Click" />&nbsp;
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <table id="TableForm" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0" width="100%">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Info" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
            <table id="TableInformationShowHide" class="Grid" cellspacing="0" cellpadding="0" width="100%">
              <tr class="Row">
                <td style="width: 150px;">Facility
                </td>
                <td>
                  <asp:Label ID="Label_Facility" runat="server" Text=""></asp:Label>&nbsp;
                </td>
              </tr>
              <tr class="Row">
                <td style="width: 150px;">Patient Visit Number
                </td>
                <td>
                  <asp:Label ID="Label_VisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                </td>
              </tr>
              <tr class="Row">
                <td style="width: 150px;">Patient Name
                </td>
                <td>
                  <asp:Label ID="Label_PatientName" runat="server" Text=""></asp:Label>&nbsp;
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <table id="TableSplit1" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <hr style="height: 10px; width: 90%; color: #b02623" />
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <a id="link"></a>
      <table id="TableLink" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0" width="100%">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Link" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
            <table id="TableLinkShowHide" cellspacing="0" cellpadding="0" border="0" width="100%">
              <tr>
                <td>
                  <asp:FormView runat="server" ID="FormView_Isidima_AdminLink" Width="700px" DataKeyNames="Isidima_AdminLink_Id" CssClass="Record" DataSourceID="SqlDataSource_Isidima_AdminLink" OnItemInserting="FormView_Isidima_AdminLink_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_Isidima_AdminLink_ItemCommand" OnItemUpdating="FormView_Isidima_AdminLink_ItemUpdating">
                    <InsertItemTemplate>
                      <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr class="Row">
                          <td colspan="5">
                            <asp:ValidationSummary ID="ValidationSummary_AdminLink" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_AdminLink" CssClass="Controls_Validation" />
                          </td>
                        </tr>
                        <tr class="Row">
                          <td colspan="2">
                            <strong>Current Visit</strong>
                          </td>
                          <td rowspan="8">&nbsp;
                          </td>
                          <td colspan="2">
                            <strong>Link to Visit</strong>
                          </td>
                        </tr>
                        <tr class="Row">
                          <td>Facility
                          </td>
                          <td>
                            <asp:Label ID="Label_PIFacility" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                          <td id="LinkedFacility_Id">Facility
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_InsertLinkedFacility_Id" runat="server" DataSourceID="SqlDataSource_Isidima_AdminLink_InsertLinkedFacility_Id" AppendDataBoundItems="true" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" CssClass="Controls_DropDownList" AutoPostBack="true" SelectedValue='<%# Bind("Isidima_AdminLink_LinkedFacility_Id") %>' OnSelectedIndexChanged="DropDownList_InsertLinkedFacility_Id_SelectedIndexChanged">
                              <asp:ListItem Value="">Select Facility</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertLinkedFacility_Id" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertLinkedFacility_Id" ValidationGroup="Isidima_AdminLink"></asp:RequiredFieldValidator>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td>Visit Number
                          </td>
                          <td>
                            <asp:Label ID="Label_PIVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                          <td id="LinkedPatientVisitNumber">Visit Number
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_InsertLinkedPatientVisitNumber" runat="server" AppendDataBoundItems="false" DataTextField="Isidima_PI_PatientVisitNumber" DataValueField="Isidima_PI_PatientVisitNumber" CssClass="Controls_DropDownList" AutoPostBack="true" SelectedValue='<%# Bind("Isidima_AdminLink_LinkedPatientVisitNumber") %>' OnSelectedIndexChanged="DropDownList_InsertLinkedPatientVisitNumber_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_InsertLinkedPatientVisitNumber" runat="server" ErrorMessage="" ControlToValidate="DropDownList_InsertLinkedPatientVisitNumber" ValidationGroup="Isidima_AdminLink"></asp:RequiredFieldValidator>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td>Surname, Name
                          </td>
                          <td>
                            <asp:Label ID="Label_PIName" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                          <td>Surname, Name
                          </td>
                          <td>
                            <asp:Label ID="Label_LinkedPIName" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td>Age
                          </td>
                          <td>
                            <asp:Label ID="Label_PIAge" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                          <td>Age
                          </td>
                          <td>
                            <asp:Label ID="Label_LinkedPIAge" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td>Date of Admission
                          </td>
                          <td>
                            <asp:Label ID="Label_PIDateAdmission" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                          <td>Date of Admission
                          </td>
                          <td>
                            <asp:Label ID="Label_LinkedPIDateAdmission" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td>Date of Discharge
                          </td>
                          <td>
                            <asp:Label ID="Label_PIDateDischarge" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                          <td>Date of Discharge
                          </td>
                          <td>
                            <asp:Label ID="Label_LinkedPIDateDischarge" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td colspan="2">&nbsp;
                          </td>
                          <td>Is Active
                          </td>
                          <td>&nbsp;
                          </td>
                        </tr>
                        <tr class="Bottom">
                          <td colspan="5" style="text-align: right;">
                            <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Link" CssClass="Controls_Button" ValidationGroup="Isidima_AdminLink" />&nbsp;
                          </td>
                        </tr>
                      </table>
                    </InsertItemTemplate>
                    <EditItemTemplate>
                      <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr class="Row">
                          <td colspan="5">
                            <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                            <asp:ValidationSummary ID="ValidationSummary_AdminLink" DisplayMode="SingleParagraph" runat="server" HeaderText="All red fields are required" ShowSummary="True" ForeColor="#B0262E" ValidationGroup="Isidima_AdminLink" CssClass="Controls_Validation" />
                            <asp:HiddenField ID="HiddenField_Isidima_AdminLink_ModifiedDate" runat="server" Value='<%# Bind("Isidima_AdminLink_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' />
                          </td>
                        </tr>
                        <tr class="Row">
                          <td colspan="2">
                            <strong>Current Visit</strong>
                          </td>
                          <td rowspan="8">&nbsp;
                          </td>
                          <td colspan="2">
                            <strong>Link to Visit</strong>
                          </td>
                        </tr>
                        <tr class="Row">
                          <td>Facility
                          </td>
                          <td>
                            <asp:Label ID="Label_PIFacility" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                          <td id="LinkedFacility_Id">Facility
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_EditLinkedFacility_Id" runat="server" DataSourceID="SqlDataSource_Isidima_AdminLink_EditLinkedFacility_Id" AppendDataBoundItems="true" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" CssClass="Controls_DropDownList" AutoPostBack="true" SelectedValue='<%# Bind("Isidima_AdminLink_LinkedFacility_Id") %>' OnSelectedIndexChanged="DropDownList_EditLinkedFacility_Id_SelectedIndexChanged">
                              <asp:ListItem Value="">Select Facility</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditLinkedFacility_Id" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditLinkedFacility_Id" ValidationGroup="Isidima_AdminLink"></asp:RequiredFieldValidator>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td>Visit Number
                          </td>
                          <td>
                            <asp:Label ID="Label_PIVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                          <td id="LinkedPatientVisitNumber">Visit Number
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_EditLinkedPatientVisitNumber" runat="server" DataSourceID="SqlDataSource_Isidima_AdminLink_EditLinkedPatientVisitNumber" AppendDataBoundItems="false" DataTextField="Isidima_Category_PatientVisitNumber" DataValueField="Isidima_Category_PatientVisitNumber" CssClass="Controls_DropDownList" AutoPostBack="true" SelectedValue='<%# Bind("Isidima_AdminLink_LinkedPatientVisitNumber") %>' OnSelectedIndexChanged="DropDownList_EditLinkedPatientVisitNumber_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_EditLinkedPatientVisitNumber" runat="server" ErrorMessage="" ControlToValidate="DropDownList_EditLinkedPatientVisitNumber" ValidationGroup="Isidima_AdminLink"></asp:RequiredFieldValidator>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td>Surname, Name
                          </td>
                          <td>
                            <asp:Label ID="Label_PIName" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                          <td>Surname, Name
                          </td>
                          <td>
                            <asp:Label ID="Label_LinkedPIName" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td>Age
                          </td>
                          <td>
                            <asp:Label ID="Label_PIAge" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                          <td>Age
                          </td>
                          <td>
                            <asp:Label ID="Label_LinkedPIAge" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td>Date of Admission
                          </td>
                          <td>
                            <asp:Label ID="Label_PIDateAdmission" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                          <td>Date of Admission
                          </td>
                          <td>
                            <asp:Label ID="Label_LinkedPIDateAdmission" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td>Date of Discharge
                          </td>
                          <td>
                            <asp:Label ID="Label_PIDateDischarge" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                          <td>Date of Discharge
                          </td>
                          <td>
                            <asp:Label ID="Label_LinkedPIDateDischarge" runat="server" Text=""></asp:Label>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td colspan="2">&nbsp;
                          </td>
                          <td>Is Active
                          </td>
                          <td>
                            <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("Isidima_AdminLink_IsActive") %>' />&nbsp;
                          </td>
                        </tr>
                        <tr class="Bottom">
                          <td colspan="5" style="text-align: right;">
                            <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Link" CssClass="Controls_Button" ValidationGroup="Isidima_AdminLink" />&nbsp;
                          </td>
                        </tr>
                      </table>
                    </EditItemTemplate>
                  </asp:FormView>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_AdminLink_InsertLinkedFacility_Id" runat="server"></asp:SqlDataSource>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_AdminLink_EditLinkedFacility_Id" runat="server"></asp:SqlDataSource>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_AdminLink_EditLinkedPatientVisitNumber" runat="server"></asp:SqlDataSource>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_AdminLink" runat="server" OnInserted="SqlDataSource_Isidima_AdminLink_Inserted" OnUpdated="SqlDataSource_Isidima_AdminLink_Updated"></asp:SqlDataSource>
                  <div>
                    &nbsp;
                  </div>
                  <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr>
                      <td style="vertical-align: top;">
                        <table class="Header" cellspacing="0" cellpadding="0" border="0">
                          <tr>
                            <td class="HeaderLeft">
                              <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                            </td>
                            <td class="Headerth" style="text-align: center; font-weight: bold;">
                              <asp:Label ID="Label_LinkGrid" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="HeaderRight">
                              <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <table class="Record" cellspacing="0" cellpadding="0">
                          <tr class="Row">
                            <td>Total Records:
                            <asp:Label ID="Label_TotalRecords" runat="server" Text=""></asp:Label>
                            </td>
                          </tr>
                          <tr>
                            <td>
                              <asp:GridView ID="GridView_Isidima_AdminLink_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_Isidima_AdminLink_List" CssClass="Grid" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_Isidima_AdminLink_List_PreRender" OnDataBound="GridView_Isidima_AdminLink_List_DataBound" OnRowCreated="GridView_Isidima_AdminLink_List_RowCreated">
                                <HeaderStyle CssClass="Caption" HorizontalAlign="Left" />
                                <AlternatingRowStyle CssClass="AltRow" />
                                <PagerTemplate>
                                  <table cellpadding="0" cellspacing="0">
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
                                        <asp:ImageButton ID="ImageButton_First" runat="server" CommandName="Page" CommandArgument="First" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/First.gif" />&nbsp;
                                      <asp:ImageButton ID="ImageButton_Prev" runat="server" CommandName="Page" CommandArgument="Prev" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Prev.gif" />&nbsp;
                                      </td>
                                      <td>Page
                                      </td>
                                      <td>
                                        <asp:DropDownList ID="DropDownList_Page" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_SelectedIndexChanged">
                                        </asp:DropDownList>
                                      </td>
                                      <td>of
                                      <%=GridView_Isidima_AdminLink_List.PageCount%>
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
                                <RowStyle CssClass="Row" />
                                <FooterStyle CssClass="Footer" />
                                <PagerStyle CssClass="Pager" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                  <table class="GridNoRecords" cellspacing="0" cellpadding="0">
                                    <tr class="NoRecords">
                                      <td>No records
                                      </td>
                                    </tr>
                                    <tr class="Footer">
                                      <td>&nbsp;
                                      </td>
                                    </tr>
                                    <tr class="Footer">
                                      <td>&nbsp;
                                      </td>
                                    </tr>
                                  </table>
                                </EmptyDataTemplate>
                                <Columns>
                                  <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                      <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("Isidima_AdminLink_Id")) %>' runat="server"></asp:HyperLink>
                                    </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="LinkedFacility_FacilityDisplayName" HeaderText="Facility" ReadOnly="True" SortExpression="LinkedFacility_FacilityDisplayName" />
                                  <asp:BoundField DataField="Isidima_AdminLink_LinkedPatientVisitNumber" HeaderText="Visit Number" ReadOnly="True" SortExpression="Isidima_AdminLink_LinkedPatientVisitNumber" />
                                  <asp:BoundField DataField="Isidima_AdminLink_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="Isidima_AdminLink_IsActive" />
                                </Columns>
                              </asp:GridView>
                              <asp:SqlDataSource ID="SqlDataSource_Isidima_AdminLink_List" runat="server" OnSelected="SqlDataSource_Isidima_AdminLink_List_Selected"></asp:SqlDataSource>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <table id="TableSplit2" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <hr style="height: 10px; width: 90%; color: #b02623" />
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <a id="Discharge"></a>
      <table id="TableDischarge" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0" width="100%">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Discharge" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
            <table id="TableDischargeShowHide" cellspacing="0" cellpadding="0" border="0" width="100%">
              <tr>
                <td>
                  <asp:FormView runat="server" ID="FormView_Isidima_AdminDischarge" Width="700px" DataKeyNames="Facility_Id,Isidima_AdminDischarge_PatientVisitNumber" CssClass="Record" DataSourceID="SqlDataSource_Isidima_AdminDischarge" DefaultMode="Edit" OnItemUpdating="FormView_Isidima_AdminDischarge_ItemUpdating">
                    <EditItemTemplate>
                      <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr class="Row">
                          <td colspan="2">
                            <asp:Label ID="Label_ValidCapture" runat="server" CssClass="Controls_Validation"></asp:Label>
                            <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                          </td>
                        </tr>
                        <tr class="Row">
                          <td style="width: 175px;">Discharge Patient
                          </td>
                          <td style="width: 525px;">
                            <asp:CheckBox ID="CheckBox_EditAdminDischarge_Patient" runat="server" Checked='<%# Bind("Isidima_AdminDischarge_Patient") %>' />
                            <asp:HiddenField ID="HiddenField_Isidima_AdminDischarge_ModifiedDate" runat="server" Value='<%# Bind("Isidima_AdminDischarge_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' />
                            &nbsp;
                          </td>
                        </tr>
                        <tr class="Bottom">
                          <td colspan="2" style="text-align: right;">
                            <asp:Button ID="Button_EditUpdate" runat="server" CommandName="Update" Text="Update" CssClass="Controls_Button" />&nbsp;
                          </td>
                        </tr>
                      </table>
                    </EditItemTemplate>
                  </asp:FormView>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_AdminDischarge" runat="server" OnUpdated="SqlDataSource_Isidima_AdminDischarge_Updated"></asp:SqlDataSource>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <table id="TableSplit3" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <hr style="height: 10px; width: 90%; color: #b02623" />
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <a id="Period"></a>
      <table id="TablePeriod" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <table class="Header" cellspacing="0" cellpadding="0" border="0" width="100%">
              <tr>
                <td class="HeaderLeft">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
                <td class="Headerth" style="text-align: center; font-weight: bold;">
                  <asp:Label ID="Label_Period" runat="server" Text=""></asp:Label>
                </td>
                <td class="HeaderRight">
                  <img alt="" src="App_Themes/LifeHealthcare/Images/Blue/Spacer.gif" style="border: 0px" />
                </td>
              </tr>
            </table>
            <table id="TablePeriodShowHide" cellspacing="0" cellpadding="0" border="0" width="100%">
              <tr>
                <td>
                  <asp:FormView runat="server" ID="FormView_Isidima_AdminAssessmentPeriod" Width="700px" DataKeyNames="Facility_Id,Isidima_AdminAssessmentPeriod_PatientVisitNumber" CssClass="Record" DataSourceID="SqlDataSource_Isidima_AdminAssessmentPeriod" DefaultMode="Edit" OnItemUpdating="FormView_Isidima_AdminAssessmentPeriod_ItemUpdating">
                    <EditItemTemplate>
                      <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr class="Row">
                          <td colspan="2">
                            <asp:Label ID="Label_ValidCapture" runat="server" CssClass="Controls_Validation"></asp:Label>
                            <asp:Label ID="Label_ConcurrencyUpdate" runat="server" CssClass="Controls_Validation"></asp:Label>
                          </td>
                        </tr>
                        <tr class="Row">
                          <td style="width: 250px;">Number of Assessments done on visit
                          </td>
                          <td>&nbsp;<strong><asp:Label ID="Label_EditNumberOfAssessments" runat="server"></asp:Label></strong>&nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td style="width: 250px;">1st Assessment Period in Months
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_EditAdminAssessmentPeriod_Period1" runat="server" DataSourceID="SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period1" DataTextField="ListItem_Name" DataValueField="ListItem_Name" SelectedValue='<%# Bind("Isidima_AdminAssessmentPeriod_Period1") %>' CssClass="Controls_DropDownList">
                            </asp:DropDownList>
                            &nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td style="width: 250px;">2nd Assessment Period in Months
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_EditAdminAssessmentPeriod_Period2" runat="server" DataSourceID="SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period2" DataTextField="ListItem_Name" DataValueField="ListItem_Name" SelectedValue='<%# Bind("Isidima_AdminAssessmentPeriod_Period2") %>' CssClass="Controls_DropDownList">
                            </asp:DropDownList>
                            &nbsp;
                          </td>
                        </tr>
                        <tr class="Row">
                          <td style="width: 250px;">3rd and any future Assessment Periods in Months
                          </td>
                          <td>
                            <asp:DropDownList ID="DropDownList_EditAdminAssessmentPeriod_Period3" runat="server" DataSourceID="SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period3" DataTextField="ListItem_Name" DataValueField="ListItem_Name" SelectedValue='<%# Bind("Isidima_AdminAssessmentPeriod_Period3") %>' CssClass="Controls_DropDownList">
                            </asp:DropDownList>
                            <asp:HiddenField ID="HiddenField_Isidima_AdminAssessmentPeriod_ModifiedDate" runat="server" Value='<%# Bind("Isidima_AdminAssessmentPeriod_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' />
                            &nbsp;
                          </td>
                        </tr>
                        <tr class="Bottom">
                          <td colspan="2" style="text-align: right;">
                            <asp:Button ID="Button_EditUpdate" runat="server" CommandName="Update" Text="Update" CssClass="Controls_Button" />&nbsp;
                          </td>
                        </tr>
                      </table>
                    </EditItemTemplate>
                  </asp:FormView>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period1" runat="server"></asp:SqlDataSource>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period2" runat="server"></asp:SqlDataSource>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period3" runat="server"></asp:SqlDataSource>
                  <asp:SqlDataSource ID="SqlDataSource_Isidima_AdminAssessmentPeriod" runat="server" OnUpdated="SqlDataSource_Isidima_AdminAssessmentPeriod_Updated"></asp:SqlDataSource>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <table id="TableSplit4" cellspacing="0" cellpadding="0" width="700px" border="0" runat="server">
        <tr>
          <td style="vertical-align: top;">
            <hr style="height: 10px; width: 90%; color: #b02623;" />
          </td>
        </tr>
      </table>
      <div>
        &nbsp;
      </div>
      <table cellspacing="0" cellpadding="0" width="700px" border="0">
        <tr>
          <td style="text-align: center;">
            <asp:Button ID="Button1" runat="server" Text="Go Back To Form" CssClass="Controls_Button" OnClick="Button_GoToForm_Click" />&nbsp;
          </td>
        </tr>
      </table>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
