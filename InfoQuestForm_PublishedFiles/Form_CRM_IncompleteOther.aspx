<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_CRM_IncompleteOther" CodeBehind="Form_CRM_IncompleteOther.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Customer Relationship Management Bulk</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_CRM_IncompleteOther" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_CRM_IncompleteOther" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_CRM_IncompleteOther" AssociatedUpdatePanelID="UpdatePanel_CRM_IncompleteOther">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_CRM_IncompleteOther" runat="server">
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
                    <td colspan="4">
                      <asp:Label ID="Label_SearchErrorMessage" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      Facility Type
                    </td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_FacilityType" runat="server" CssClass="Controls_DropDownList" AppendDataBoundItems="True" DataSourceID="SqlDataSource_CRM_FacilityType" DataTextField="Facility_Type_Lookup_Name" DataValueField="Facility_Type_Lookup_Id" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_FacilityType_SelectedIndexChanged">
                        <asp:ListItem Value="">Select Facility Type</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_FacilityType" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Facility
                    </td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_Facility" runat="server" AppendDataBoundItems="True" CssClass="Controls_DropDownList" DataSourceID="SqlDataSource_CRM_Facility" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_Facility" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Report Number
                    </td>
                    <td colspan="3">
                      <asp:TextBox ID="TextBox_ReportNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td>Type
                    </td>
                    <td colspan="3">
                      <asp:DropDownList ID="DropDownList_Type" runat="server" AppendDataBoundItems="true" CssClass="Controls_DropDownList" DataSourceID="SqlDataSource_CRM_Type" DataTextField="ListItem_Name" DataValueField="ListItem_Id" OnDataBound="DropDownList_Type_DataBound">
                        <asp:ListItem Value="">Select Type</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_Type" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Patient Visit Number
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_PatientVisitNumber" runat="server" CssClass="Controls_TextBox"></asp:TextBox>&nbsp;&nbsp;
                    </td>
                    <td>Patient or Customer Name
                    </td>
                    <td>
                      <asp:TextBox ID="TextBox_Name" runat="server" CssClass="Controls_TextBox"></asp:TextBox>&nbsp;&nbsp;
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
                      <asp:Button ID="Button_Clear" runat="server" CssClass="Controls_Button" OnClick="Button_Clear_Click" Text="Clear" />
                      &nbsp;
                    <asp:Button ID="Button_Search" runat="server" CssClass="Controls_Button" OnClick="Button_Search_Click" Text="Search" />
                      &nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <div>
            <asp:ImageButton runat="server" ID="ImageButton_IncompleteOther" AlternateText="" BorderWidth="0px" Height="0px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
          </div>
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
                      <asp:GridView ID="GridView_CRM_IncompleteOther" runat="server" AllowPaging="True" Width="1000px" DataSourceID="SqlDataSource_CRM_IncompleteOther" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="100" OnPreRender="GridView_CRM_IncompleteOther_PreRender" OnDataBound="GridView_CRM_IncompleteOther_DataBound" OnRowCreated="GridView_CRM_IncompleteOther_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
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
                              <%=GridView_CRM_IncompleteOther.PageCount%>
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
                                <asp:Button ID="Button_AcknowledgeAll" runat="server" Text="Acknowledge All" CssClass="Controls_Button" OnClick="Button_AcknowledgeAll_Click" OnDataBinding="Button_AcknowledgeAll_DataBinding" />&nbsp;
                              <asp:Button ID="Button_AcknowledgeCloseoutAll" runat="server" Text="Acknowledge & Close Out All" CssClass="Controls_Button" OnClick="Button_AcknowledgeCloseoutAll_Click" OnDataBinding="Button_AcknowledgeCloseoutAll_DataBinding" />&nbsp;
                              <asp:Button ID="Button_Update" runat="server" Text="Update" CssClass="Controls_Button" OnClick="Button_Update_Click" OnDataBinding="Button_Update_DataBinding" />&nbsp;
                              <asp:Button ID="Button_Cancel" runat="server" Text="Cancel" CssClass="Controls_Button" OnClick="Button_Cancel_Click" OnDataBinding="Button_Cancel_DataBinding" />&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td colspan="10">
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Form" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <RowStyle CssClass="GridView_RowStyle_TemplateField" />
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
                                <asp:Button ID="Button_CaptureNew" runat="server" Text="Capture New Form" CssClass="Controls_Button" OnClick="Button_CaptureNew_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <table>
                                <tr>
                                  <td class="Table_TemplateField" colspan="6">
                                    <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label><asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label><asp:HiddenField ID="HiddenField_EditModifiedDate" runat="server" Value='<%# Bind("CRM_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 275px;"><strong>Facility</strong></td>
                                  <td class="Table_TemplateField" style="width: 125px;"><strong>Report Number</strong></td>
                                  <td class="Table_TemplateField" style="width: 150px;"><strong>Type</strong></td>
                                  <td class="Table_TemplateField" style="width: 150px;"><strong>Customer Name</strong></td>
                                  <td class="Table_TemplateField" style="width: 150px;"><strong>Patient Visit Number</strong></td>
                                  <td class="Table_TemplateField" style="width: 150px;"><strong>Patient Name</strong></td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 275px;">
                                    <asp:Label ID="Label_EditFacilityDisplayName" runat="server" Text='<%# Bind("Facility_FacilityDisplayName") %>' Width="260px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 125px;">
                                    <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("CRM_ReportNumber") %>' Width="110px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditTypeName" runat="server" Text='<%# Bind("CRM_Type_Name") %>' Width="135px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditCustomerName" runat="server" Text='<%# Bind("CRM_CustomerName") %>' Width="135px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditPatientVisitNumber" runat="server" Text='<%# Bind("CRM_PatientVisitNumber") %>' Width="135px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditPatientName" runat="server" Text='<%# Bind("CRM_PatientName") %>' Width="135px"></asp:Label></td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="2" style="width: 400px;"><strong>Description</strong></td>
                                  <td class="Table_TemplateField" style="width: 150px;"><strong>Comment Type</strong></td>
                                  <td class="Table_TemplateField" style="width: 150px;"><strong>Unit</strong></td>
                                  <td class="Table_TemplateField" style="width: 150px;"><strong>Comment Category</strong></td>
                                  <td class="Table_TemplateField" style="width: 150px;"><strong>Acknowledge</strong></td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="2" rowspan="3" style="width: 400px;">
                                    <asp:Label ID="Label_EditDescription" runat="server" Text='<%# Bind("CRM_Description") %>' Width="385px"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:LinkButton ID="LinkButton_EditCommentPXMPDCHResults" runat="server" Text="Survey Results" OnClick="LinkButton_EditCommentPXMPDCHResults_OnClick"></asp:LinkButton>
                                    <asp:HiddenField ID="HiddenField_EditCRMId" runat="server" Value='<%# Bind("CRM_Id") %>' />
                                    <asp:HiddenField ID="HiddenField_EditFacilityId" runat="server" Value='<%# Bind("Facility_Id") %>' />
                                    <asp:HiddenField ID="HiddenField_EditCRMTypeList" runat="server" Value='<%# Bind("CRM_Type_List") %>' />
                                    <asp:HiddenField ID="HiddenField_EditCRMUploadedFrom" runat="server" Value='<%# Bind("CRM_UploadedFrom") %>' />
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditCommentTypeList" runat="server" Text="Not Required" Width="135px"></asp:Label>
                                    <asp:DropDownList ID="DropDownList_EditCommentTypeList" runat="server" Width="125px" DataSourceID="SqlDataSource_CRM_EditCommentTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Comment_Type_List") %>' CssClass="Controls_DropDownList" OnDataBinding="DropDownList_EditCommentTypeList_DataBinding">
                                      <asp:ListItem Value="">Select Type</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label_EditCommentTypeName" runat="server" Text='<%# Bind("CRM_Comment_Type_Name") %>' Width="135px"></asp:Label>
                                    <asp:SqlDataSource ID="SqlDataSource_CRM_EditCommentTypeList" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:DropDownList ID="DropDownList_EditUnitId" runat="server" Width="125px" DataSourceID="SqlDataSource_CRM_EditUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("CRM_Unit_Id") %>' CssClass="Controls_DropDownList" OnDataBinding="DropDownList_EditUnitId_DataBinding">
                                      <asp:ListItem Value="">Select Unit</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label_EditUnitName" runat="server" Text='<%# Bind("CRM_Unit_Name") %>' Width="135px"></asp:Label>
                                    <asp:SqlDataSource ID="SqlDataSource_CRM_EditUnitId" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditCommentCategoryList" runat="server" Text="Not Required" Width="135px"></asp:Label>
                                    <asp:DropDownList ID="DropDownList_EditCommentCategoryList" runat="server" Width="125px" DataSourceID="SqlDataSource_CRM_EditCommentCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Comment_Category_List") %>' CssClass="Controls_DropDownList" OnDataBinding="DropDownList_EditCommentCategoryList_DataBinding">
                                      <asp:ListItem Value="">Select Category</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label_EditCommentCategoryName" runat="server" Text='<%# Bind("CRM_Comment_Category_Name") %>' Width="135px"></asp:Label>
                                    <asp:SqlDataSource ID="SqlDataSource_CRM_EditCommentCategoryList" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:CheckBox ID="CheckBox_EditAcknowledge" runat="server" Checked='<%# Bind("CRM_Acknowledge") %>' OnCheckedChanged="CheckBox_EditAcknowledge_CheckedChanged" AutoPostBack="true" /><asp:Label ID="Label_EditAcknowledge" runat="server" Text='<%# (bool)(Eval("CRM_Acknowledge"))?"Yes":"No" %>'></asp:Label><asp:HiddenField ID="HiddenField_EditAcknowledge" runat="server" Value='<%# Bind("CRM_Acknowledge") %>' />
                                    <asp:HiddenField ID="HiddenField_EditAcknowledgeDate" runat="server" Value='<%# Bind("CRM_AcknowledgeDate") %>' />
                                    <asp:HiddenField ID="HiddenField_EditAcknowledgeBy" runat="server" Value='<%# Bind("CRM_AcknowledgeBy") %>' />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 150px;"><strong>Comment Type 2</strong></td>
                                  <td class="Table_TemplateField" style="width: 150px;"><strong>Unit 2</strong></td>
                                  <td class="Table_TemplateField" style="width: 150px;"><strong>Comment Category 2</strong></td>
                                  <td class="Table_TemplateField" style="width: 150px;"><strong>Close Out</strong></td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditCommentAdditionalTypeList" runat="server" Text="Not Required" Width="135px"></asp:Label>
                                    <asp:DropDownList ID="DropDownList_EditCommentAdditionalTypeList" runat="server" Width="125px" DataSourceID="SqlDataSource_CRM_EditCommentAdditionalTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Comment_AdditionalType_List") %>' CssClass="Controls_DropDownList" OnDataBinding="DropDownList_EditCommentAdditionalTypeList_DataBinding">
                                      <asp:ListItem Value="">Select Type</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label_EditCommentAdditionalTypeName" runat="server" Text='<%# Bind("CRM_Comment_AdditionalType_Name") %>' Width="135px"></asp:Label>
                                    <asp:SqlDataSource ID="SqlDataSource_CRM_EditCommentAdditionalTypeList" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditCommentAdditionalUnitId" runat="server" Text="Not Required" Width="135px"></asp:Label>
                                    <asp:DropDownList ID="DropDownList_EditCommentAdditionalUnitId" runat="server" Width="125px" DataSourceID="SqlDataSource_CRM_EditCommentAdditionalUnitId" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# Bind("CRM_Comment_AdditionalUnit_Id") %>' CssClass="Controls_DropDownList" OnDataBinding="DropDownList_EditCommentAdditionalUnitId_DataBinding">
                                      <asp:ListItem Value="">Select Unit</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label_EditCommentAdditionalUnitName" runat="server" Text='<%# Bind("CRM_Comment_AdditionalUnit_Name") %>' Width="135px"></asp:Label>
                                    <asp:SqlDataSource ID="SqlDataSource_CRM_EditCommentAdditionalUnitId" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditCommentAdditionalCategoryList" runat="server" Text="Not Required" Width="135px"></asp:Label>
                                    <asp:DropDownList ID="DropDownList_EditCommentAdditionalCategoryList" runat="server" Width="125px" DataSourceID="SqlDataSource_CRM_EditCommentAdditionalCategoryList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("CRM_Comment_AdditionalCategory_List") %>' CssClass="Controls_DropDownList" OnDataBinding="DropDownList_EditCommentAdditionalCategoryList_DataBinding">
                                      <asp:ListItem Value="">Select Category</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label_EditCommentAdditionalCategoryName" runat="server" Text='<%# Bind("CRM_Comment_AdditionalCategory_Name") %>' Width="135px"></asp:Label>
                                    <asp:SqlDataSource ID="SqlDataSource_CRM_EditCommentAdditionalCategoryList" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:CheckBox ID="CheckBox_EditCloseout" runat="server" Checked='<%# Bind("CRM_CloseOut") %>' OnCheckedChanged="CheckBox_EditCloseout_CheckedChanged" AutoPostBack="true" OnDataBinding="CheckBox_EditCloseout_DataBinding" /><asp:Label ID="Label_EditCloseout" runat="server" Text='<%# (bool)(Eval("CRM_CloseOut"))?"Yes":"No" %>'></asp:Label><asp:HiddenField ID="HiddenField_EditCloseout" runat="server" Value='<%# Bind("CRM_CloseOut") %>' />
                                    <asp:HiddenField ID="HiddenField_EditCloseoutDate" runat="server" Value='<%# Bind("CRM_CloseOutDate") %>' />
                                    <asp:HiddenField ID="HiddenField_EditCloseoutBy" runat="server" Value='<%# Bind("CRM_CloseOutBy") %>' />
                                  </td>
                                </tr>
                              </table>
                            </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_IncompleteOther" runat="server" OnSelected="SqlDataSource_CRM_IncompleteOther_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <div>
            <asp:ImageButton runat="server" ID="ImageButton_BulkApproval" AlternateText="" BorderWidth="0px" Height="0px" CausesValidation="false" EnableViewState="false" CssClass="Controls_ImageButton_NoHand" />
          </div>
          <div>
            &nbsp;
          </div>
          <table id="TableBulkApproval" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_BulkApprovalHeading" runat="server" Text=""></asp:Label>
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
                    <asp:Label ID="Label_TotalRecords_BulkApproval" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_CRM_IncompleteOtherBulkApproval" runat="server" Width="1000px" AllowPaging="True" DataSourceID="SqlDataSource_CRM_IncompleteOtherBulkApproval" AutoGenerateColumns="false" CssClass="GridView" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="100" OnPreRender="GridView_CRM_IncompleteOtherBulkApproval_PreRender" OnDataBound="GridView_CRM_IncompleteOtherBulkApproval_DataBound" OnRowCreated="GridView_CRM_IncompleteOtherBulkApproval_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              </td>
                              <td>Records Per Page:
                              </td>
                              <td>
                                <asp:DropDownList ID="DropDownList_PageSize" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PageSize_SelectedIndexChanged_BulkApproval">
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
                                <asp:DropDownList ID="DropDownList_Page" CssClass="Controls_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_Page_SelectedIndexChanged_BulkApproval">
                                </asp:DropDownList>
                              </td>
                              <td>of
                              <%=GridView_CRM_IncompleteOtherBulkApproval.PageCount%>
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
                                <asp:Button ID="Button_ApproveAll_BulkApproval" runat="server" Text="Approve All Forms" CssClass="Controls_Button" OnClick="Button_ApproveAll_BulkApproval_Click" OnDataBinding="Button_ApproveAll_BulkApproval_DataBinding" />&nbsp;
                              <asp:Button ID="Button_Update_BulkApproval" runat="server" Text="Update Forms" CssClass="Controls_Button" OnClick="Button_Update_BulkApproval_Click" OnDataBinding="Button_Update_BulkApproval_DataBinding" />&nbsp;
                              <asp:Button ID="Button_Cancel_BulkApproval" runat="server" Text="Cancel" CssClass="Controls_Button" OnClick="Button_Cancel_BulkApproval_Click" OnDataBinding="Button_Cancel_BulkApproval_DataBinding" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <RowStyle CssClass="GridView_RowStyle_TemplateField" />
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
                              <table>
                                <tr>
                                  <td class="Table_TemplateField" colspan="9">
                                    <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label><asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label><asp:HiddenField ID="HiddenField_EditModifiedDate" runat="server" Value='<%# Bind("CRM_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>' />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" rowspan="4" style="width: 50px;">
                                    <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("CRM_Id"), Eval("ViewUpdate")) %>' runat="server"></asp:HyperLink></td>
                                  <td class="Table_TemplateField" style="width: 200px;"><strong>Facility</strong></td>
                                  <td class="Table_TemplateField" style="width: 125px;"><strong>Report Number</strong></td>
                                  <td class="Table_TemplateField" style="width: 75px;"><strong>Type</strong></td>
                                  <td class="Table_TemplateField" style="width: 75px;"><strong>Originated At</strong></td>
                                  <td class="Table_TemplateField" style="width: 75px;"><strong>Received From</strong></td>
                                  <td class="Table_TemplateField" style="width: 100px;"><strong>Customer Name</strong></td>
                                  <td class="Table_TemplateField" style="width: 100px;"><strong>Patient Visit Number</strong></td>
                                  <td class="Table_TemplateField" style="width: 100px;"><strong>Patient Name</strong></td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 200px;">
                                    <asp:Label ID="Label_EditFacilityDisplayName" runat="server" Text='<%# Bind("Facility_FacilityDisplayName") %>' Width="185px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 125px;">
                                    <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("CRM_ReportNumber") %>' Width="110px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 75px;">
                                    <asp:Label ID="Label_EditTypeName" runat="server" Text='<%# Bind("CRM_Type_Name") %>' Width="60px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 75px;">
                                    <asp:Label ID="Label_EditOriginatedAtName" runat="server" Text='<%# Bind("CRM_OriginatedAt_Name") %>' Width="60px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 75px;">
                                    <asp:Label ID="Label_EditReceivedFromName" runat="server" Text='<%# Bind("CRM_ReceivedFrom_Name") %>' Width="60px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditCustomerName" runat="server" Text='<%# Bind("CRM_CustomerName") %>' Width="135px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 100px;">
                                    <asp:Label ID="Label_EditPatientVisitNumber" runat="server" Text='<%# Bind("CRM_PatientVisitNumber") %>' Width="85px"></asp:Label></td>
                                  <td class="Table_TemplateField" style="width: 150px;">
                                    <asp:Label ID="Label_EditPatientName" runat="server" Text='<%# Bind("CRM_PatientName") %>' Width="135px"></asp:Label></td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4" style="width: 475px;"><strong>Description</strong></td>
                                  <td class="Table_TemplateField" colspan="2" style="width: 225px;"><strong>Form Status</strong></td>
                                  <td class="Table_TemplateField" colspan="2" style="width: 250px;"><strong>
                                    <asp:Label ID="Label_UpdateStatusRejectedLabel" runat="server" Text="Rejection Reason:" Visible="false"></asp:Label></strong></td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4" style="width: 475px;">
                                    <asp:Label ID="Label_EditDescription" runat="server" Text='<%# Bind("CRM_Description") %>' Width="455px"></asp:Label><asp:HiddenField ID="HiddenField_EditCRMId" runat="server" Value='<%# Bind("CRM_Id") %>' />
                                  </td>
                                  <td class="Table_TemplateField" colspan="2" style="width: 225px;">
                                    <asp:DropDownList ID="DropDownList_EditStatus" runat="server" SelectedValue='<%# Bind("CRM_Status") %>' Width="200px" CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditStatus_SelectedIndexChanged">
                                      <asp:ListItem Value="Pending Approval">Pending Approval</asp:ListItem>
                                      <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                      <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                    </asp:DropDownList><asp:HiddenField ID="HiddenField_EditStatus" runat="server" Value='<%# Bind("CRM_Status") %>' />
                                    <asp:HiddenField ID="HiddenField_EditStatusDate" runat="server" Value='<%# Bind("CRM_StatusDate") %>' />
                                  </td>
                                  <td class="Table_TemplateField" colspan="2" style="width: 250px;">
                                    <asp:TextBox ID="TextBox_EditStatusRejectedReason" runat="server" Visible="false" TextMode="MultiLine" Rows="3" Width="220px" Text='<%# Bind("CRM_StatusRejectedReason") %>' CssClass="Controls_TextBox"></asp:TextBox></td>
                                </tr>
                              </table>
                            </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_CRM_IncompleteOtherBulkApproval" runat="server" OnSelected="SqlDataSource_CRM_IncompleteOtherBulkApproval_Selected"></asp:SqlDataSource>
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
