<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_Pharmacy_NewProduct" CodeBehind="Form_Pharmacy_NewProduct.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Pharmacy - New Product Code Request</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_Pharmacy_NewProduct.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body onload="Validation_Form();ShowHide_Form();">
  <form id="form_Pharmacy_NewProduct" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Pharmacy_NewProduct" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
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
      <table id="TableForm" class="Table" style="width: 900px;" runat="server">
        <tr>
          <td>
            <table class="Table_Header">
              <tr>
                <td>
                  <asp:Label ID="Label_FormHeading" runat="server" Text=""></asp:Label>
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <asp:FormView ID="FormView_Pharmacy_NewProduct_Form" runat="server" Width="900px" DataKeyNames="NewProduct_Id" CssClass="FormView" DataSourceID="SqlDataSource_Pharmacy_NewProduct_Form" OnItemInserting="FormView_Pharmacy_NewProduct_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_Pharmacy_NewProduct_Form_ItemCommand" OnDataBound="FormView_Pharmacy_NewProduct_Form_DataBound" OnItemUpdating="FormView_Pharmacy_NewProduct_Form_ItemUpdating">
              <InsertItemTemplate>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2">
                      <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                      <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Report Number
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_InsertReportNumber" runat="server" Text='<%# Bind("NewProduct_ReportNumber") %>'></asp:Label>
                      <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                      <asp:HiddenField ID="HiddenField_InsertNewProductFileTemp" runat="server" />
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormFacility">Facility
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_InsertFacility" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_InsertFacility" AppendDataBoundItems="True" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Facility</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormDate">Date<br />
                      (yyyy/mm/dd)
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertDate" runat="server" Width="75px" Text='<%# Bind("NewProduct_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_InsertDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                    <Ajax:CalendarExtender ID="CalendarExtender_InsertDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertDate">
                    </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertDate" runat="server" TargetControlID="TextBox_InsertDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                      </Ajax:TextBoxWatermarkExtender>
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormProductClassificationList">Product Classification
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_InsertProductClassificationList" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_InsertProductClassificationList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("NewProduct_ProductClassification_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Classification</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormManufacturerList">Manufacturer / Supplier
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_InsertManufacturerList" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_InsertManufacturerList" AppendDataBoundItems="true" DataTextField="Pharmacy_Supplier_Lookup_Description" DataValueField="Pharmacy_Supplier_Lookup_Id" SelectedValue='<%# Bind("NewProduct_Manufacturer_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Supplier</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormProductDescription">Product Description
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertProductDescription" runat="server" Width="300px" Text='<%# Bind("NewProduct_ProductDescription") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductTradeName">
                    <td style="width: 175px;" id="FormTradeName">Trade Name of product
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertTradeName" runat="server" Width="300px" Text='<%# Bind("NewProduct_TradeName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductActiveIngredient">
                    <td style="width: 175px;" id="FormActiveIngredient">Active Ingredient
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertActiveIngredient" runat="server" Width="300px" Text='<%# Bind("NewProduct_ActiveIngredient") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductStrength">
                    <td style="width: 175px;" id="FormStrength">Strength
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertStrength" runat="server" Width="300px" Text='<%# Bind("NewProduct_Strength") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormPackSize">Pack Size
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertPackSize" runat="server" Width="300px" Text='<%# Bind("NewProduct_PackSize") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormNettPrice">Nett Price excluding
                    </td>
                    <td style="width: 725px;">R&nbsp;
                    <asp:TextBox ID="TextBox_InsertNettPrice" runat="server" Width="200px" Text='<%# Bind("NewProduct_NettPrice","{0:#,##0.00}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                      <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertNettPrice" runat="server" TargetControlID="TextBox_InsertNettPrice" FilterType="Custom, Numbers" ValidChars=".,">
                      </Ajax:FilteredTextBoxExtender>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductUseList">
                    <td style="width: 175px;" id="FormUseList">Single use or multiple use
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_InsertUseList" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_InsertUseList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("NewProduct_Use_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductSupplierCatalogNumber">
                    <td style="width: 175px;" id="FormSupplierCatalogNumber">Supplier Catalog Number
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertSupplierCatalogNumber" runat="server" Width="300px" Text='<%# Bind("NewProduct_SupplierCatalogNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormNappiCode">9 Digit NAPPI code<br />
                      First 6 digits are required
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertNappiCode" runat="server" Width="100px" Text='<%# Bind("NewProduct_NappiCode") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertNappiCode_TextChanged"></asp:TextBox>
                      <Ajax:MaskedEditExtender ID="MaskedEditExtender_InsertNappiCode" runat="server" TargetControlID="TextBox_InsertNappiCode" Mask="999999-999" ClearMaskOnLostFocus="false" InputDirection="LeftToRight">
                      </Ajax:MaskedEditExtender>
                      <br />
                      <asp:Label ID="Label_InsertNappiCodeError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductProductRequestList">
                    <td style="width: 175px;" id="FormProductRequestList">Reason for new Product Request
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_InsertProductRequestList" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_InsertProductRequestList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("NewProduct_ProductRequest_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductRequestByList">
                    <td style="width: 175px;" id="FormRequestByList">Requested By
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_InsertRequestByList" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_InsertRequestByList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("NewProduct_RequestBy_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFormularyProduct1">
                    <td style="width: 175px;" id="FormFormularyProduct">Is there a formulary product
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_InsertFormularyProduct" runat="server" SelectedValue='<%# Bind("NewProduct_FormularyProduct") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                        <asp:ListItem Value="True">Yes</asp:ListItem>
                        <asp:ListItem Value="False">No</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFormularyProduct2">
                    <td style="width: 175px;">Formulary Product Description / Code
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertFormularyProductDescription" runat="server" TextMode="MultiLine" Rows="4" Width="600px" Text='<%# Bind("NewProduct_FormularyProductDescription") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductComment">
                    <td style="width: 175px;">Comment
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertComment" runat="server" TextMode="MultiLine" Rows="4" Width="600px" Text='<%# Bind("NewProduct_Comment") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductComparisonHeading1">
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductComparisonHeading2">
                    <td colspan="2" class="FormView_TableBodyHeader">
                      Comparison of New Product against Current Product
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductClinicalBenefit">
                    <td style="width: 175px;" id="FormClinicalBenefit">Clinical benefit compared to current product
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertClinicalBenefit" runat="server" TextMode="MultiLine" Rows="4" Width="600px" Text='<%# Bind("NewProduct_ClinicalBenefit") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFinancialBenefit">
                    <td style="width: 175px;" id="FormFinancialBenefit">Financial benefit compared to current product
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertFinancialBenefit" runat="server" TextMode="MultiLine" Rows="4" Width="600px" Text='<%# Bind("NewProduct_FinancialBenefit") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductRequirement">
                    <td style="width: 175px;" id="FormRequirement">Additional items or equipment required for the use of this product
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertRequirement" runat="server" TextMode="MultiLine" Rows="4" Width="600px" Text='<%# Bind("NewProduct_Requirement") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile1">
                    <td colspan="2">Upload Files
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile2">
                    <td colspan="2">When uploading a document<br />
                      Only these document formats can be uploaded: Word (.doc / .docx), Excel (.xls / .xlsx), Adobe (.pdf), Fax (.tif / .tiff)<br />
                      Only files smaller then 5 MB can be uploaded<br />
                      Field is Required<br />
                      Quote the Report Number as a reference if documentation is faxed or emailed
                    <asp:HiddenField ID="HiddenField_InsertFile" runat="server" />
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile3">
                    <td colspan="2">
                      <asp:Label ID="Label_InsertMessageFile" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile4">
                    <td style="width: 175px;">File
                    </td>
                    <td style="width: 725px;">
                      <asp:FileUpload ID="FileUpload_InsertFile" runat="server" CssClass="Controls_FileUpload" Width="350px" AllowMultiple="true" />&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile5">
                    <td style="width: 175px;">Section
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_InsertField" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_File_InsertField" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Section</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile6">
                    <td style="width: 175px;">Sections missing files
                    </td>
                    <td style="width: 725px;">
                      <asp:BulletedList ID="BulletedList_InsertFieldMissing" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_File_InsertFieldMissing" DataTextField="ListItem_Name">
                      </asp:BulletedList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile7">
                    <td colspan="2" style="text-align: center;">
                      <asp:Button ID="Button_InsertUploadFile" runat="server" OnClick="Button_InsertUploadFile_OnClick" Text="Upload File" CssClass="Controls_Button" CommandArgument="FileUpload_InsertFile" />&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile8">
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile9">
                    <td colspan="2">
                      <asp:GridView ID="GridView_InsertFile" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_Pharmacy_NewProduct_File_InsertFile" CssClass="GridView" AllowPaging="True" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_InsertFile_RowCreated" OnPreRender="GridView_InsertFile_PreRender">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>
                                <asp:Button ID="Button_InsertDeleteFile" runat="server" OnClick="Button_InsertDeleteFile_OnClick" Text="Delete Checked Files" CssClass="Controls_Button" CommandArgument="GridView_InsertFile" />&nbsp;
                                <asp:Button ID="Button_InsertDeleteAllFile" runat="server" OnClick="Button_InsertDeleteAllFile_OnClick" Text="Delete All Files" CssClass="Controls_Button" CommandArgument="GridView_InsertFile" />&nbsp;
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
                              <td>No Files
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="" ItemStyle-Width="25px">
                            <ItemTemplate>
                              <asp:CheckBox ID="CheckBox_InsertFile" runat="server" CssClass='<%# Eval("NewProduct_File_Id") %>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Uploaded File">
                            <ItemTemplate>
                              <asp:LinkButton ID="LinkButton_InsertFile" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("NewProduct_File_Name")) %>' CommandArgument='<%# Eval("NewProduct_File_Id") %>'></asp:LinkButton>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="NewProduct_File_Field_Name" HeaderText="Section" ReadOnly="True" ItemStyle-Width="150px" />
                          <asp:BoundField DataField="NewProduct_File_CreatedBy" HeaderText="Created By" ReadOnly="True" ItemStyle-Width="75px" />
                          <asp:BoundField DataField="NewProduct_File_CreatedDate" HeaderText="Created Date" ReadOnly="True" ItemStyle-Width="125px" />
                        </Columns>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile10">
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">Feedback
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormFeedbackDescription">Feedback
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_InsertFeedbackDescription" runat="server" TextMode="MultiLine" Rows="4" Width="600px" Text='<%# Bind("NewProduct_Feedback_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Progress Status
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_InsertFeedbackProgressStatusList" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_InsertFeedbackProgressStatusList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("NewProduct_Feedback_ProgressStatus_List") %>' CssClass="Controls_DropDownList">
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Progress Status Date
                    </td>
                    <td>
                      <asp:Label ID="Label_InsertFeedbackDate" runat="server" Text='<%# Bind("NewProduct_Feedback_Date","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
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
                      <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("NewProduct_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Created By
                    </td>
                    <td>
                      <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("NewProduct_CreatedBy") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Modified Date
                    </td>
                    <td>
                      <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("NewProduct_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Modified By
                    </td>
                    <td>
                      <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("NewProduct_ModifiedBy") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Is Active
                    </td>
                    <td>
                      <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("NewProduct_IsActive") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2">
                      <asp:Button ID="Button_InsertClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertClear_Click" />&nbsp;
                      <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Form" CssClass="Controls_Button" />&nbsp;
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2">
                      <asp:Button ID="Button_InsertCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_InsertCancel_Click" />&nbsp;
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
                    <td style="width: 175px;">Report Number
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_EditReportNumber" runat="server" Text='<%# Bind("NewProduct_ReportNumber") %>'></asp:Label>
                      <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Facility
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_EditFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormDate">Date<br />
                      (yyyy/mm/dd)
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditDate" runat="server" Width="75px" Text='<%# Bind("NewProduct_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:ImageButton runat="Server" ID="ImageButton_EditDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                    <Ajax:CalendarExtender ID="CalendarExtender_EditDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditDate">
                    </Ajax:CalendarExtender>
                      <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditDate" runat="server" TargetControlID="TextBox_EditDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                      </Ajax:TextBoxWatermarkExtender>
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormProductClassificationList">Product Classification
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_EditProductClassificationList" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_EditProductClassificationList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("NewProduct_ProductClassification_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Classification</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormManufacturerList">Manufacturer / Supplier
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_EditManufacturerList" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_EditManufacturerList" AppendDataBoundItems="true" DataTextField="Pharmacy_Supplier_Lookup_Description" DataValueField="Pharmacy_Supplier_Lookup_Id" SelectedValue='<%# Bind("NewProduct_Manufacturer_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Supplier</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormProductDescription">Product Description
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditProductDescription" runat="server" Width="300px" Text='<%# Bind("NewProduct_ProductDescription") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductTradeName">
                    <td style="width: 175px;" id="FormTradeName">Trade Name of product
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditTradeName" runat="server" Width="300px" Text='<%# Bind("NewProduct_TradeName") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductActiveIngredient">
                    <td style="width: 175px;" id="FormActiveIngredient">Active Ingredient
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditActiveIngredient" runat="server" Width="300px" Text='<%# Bind("NewProduct_ActiveIngredient") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductStrength">
                    <td style="width: 175px;" id="FormStrength">Strength
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditStrength" runat="server" Width="300px" Text='<%# Bind("NewProduct_Strength") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormPackSize">Pack Size
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditPackSize" runat="server" Width="300px" Text='<%# Bind("NewProduct_PackSize") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormNettPrice">Nett Price excluding
                    </td>
                    <td style="width: 725px;">R&nbsp;
                    <asp:TextBox ID="TextBox_EditNettPrice" runat="server" Width="200px" Text='<%# Bind("NewProduct_NettPrice","{0:#,##0.00}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                      <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditNettPrice" runat="server" TargetControlID="TextBox_EditNettPrice" FilterType="Custom, Numbers" ValidChars=".,">
                      </Ajax:FilteredTextBoxExtender>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductUseList">
                    <td style="width: 175px;" id="FormUseList">Single use or multiple use
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_EditUseList" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_EditUseList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("NewProduct_Use_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductSupplierCatalogNumber">
                    <td style="width: 175px;" id="FormSupplierCatalogNumber">Supplier Catalog Number
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditSupplierCatalogNumber" runat="server" Width="300px" Text='<%# Bind("NewProduct_SupplierCatalogNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormNappiCode">9 Digit NAPPI code<br />
                      First 6 digits are required
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditNappiCode" runat="server" Width="100px" Text='<%# Bind("NewProduct_NappiCode") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditNappiCode_TextChanged"></asp:TextBox>
                      <Ajax:MaskedEditExtender ID="MaskedEditExtender_EditNappiCode" runat="server" TargetControlID="TextBox_EditNappiCode" Mask="999999-999" ClearMaskOnLostFocus="false" InputDirection="LeftToRight">
                      </Ajax:MaskedEditExtender>
                      <br />
                      <asp:Label ID="Label_EditNappiCodeError" runat="server" Text="" CssClass="Controls_Error"></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductProductRequestList">
                    <td style="width: 175px;" id="FormProductRequestList">Reason for new Product Request
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_EditProductRequestList" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_EditProductRequestList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("NewProduct_ProductRequest_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductRequestByList">
                    <td style="width: 175px;" id="FormRequestByList">Requested By
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_EditRequestByList" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_EditRequestByList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("NewProduct_RequestBy_List") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFormularyProduct1">
                    <td style="width: 175px;" id="FormFormularyProduct">Is there a formulary product
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_EditFormularyProduct" runat="server" SelectedValue='<%# Bind("NewProduct_FormularyProduct") %>' CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                        <asp:ListItem Value="True">Yes</asp:ListItem>
                        <asp:ListItem Value="False">No</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFormularyProduct2">
                    <td style="width: 175px;">Formulary Product Description / Code
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditFormularyProductDescription" runat="server" TextMode="MultiLine" Rows="4" Width="600px" Text='<%# Bind("NewProduct_FormularyProductDescription") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductComment">
                    <td style="width: 175px;">Comment
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditComment" runat="server" TextMode="MultiLine" Rows="4" Width="600px" Text='<%# Bind("NewProduct_Comment") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductComparisonHeading1">
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductComparisonHeading2">
                    <td colspan="2" class="FormView_TableBodyHeader">
                      Comparison of New Product against Current Product
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductClinicalBenefit">
                    <td style="width: 175px;" id="FormClinicalBenefit">Clinical benefit compared to current product
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditClinicalBenefit" runat="server" TextMode="MultiLine" Rows="4" Width="600px" Text='<%# Bind("NewProduct_ClinicalBenefit") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFinancialBenefit">
                    <td style="width: 175px;" id="FormFinancialBenefit">Financial benefit compared to current product
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditFinancialBenefit" runat="server" TextMode="MultiLine" Rows="4" Width="600px" Text='<%# Bind("NewProduct_FinancialBenefit") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductRequirement">
                    <td style="width: 175px;" id="FormRequirement">Additional items or equipment required for the use of this product
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditRequirement" runat="server" TextMode="MultiLine" Rows="4" Width="600px" Text='<%# Bind("NewProduct_Requirement") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile1">
                    <td colspan="2">Upload Files
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile2">
                    <td colspan="2">When uploading a document<br />
                      Only these document formats can be uploaded: Word (.doc / .docx), Excel (.xls / .xlsx), Adobe (.pdf), Fax (.tif / .tiff)<br />
                      Only files smaller then 5 MB can be uploaded<br />
                      Field is Required<br />
                      Quote the Report Number as a reference if documentation is faxed or emailed
                    <asp:HiddenField ID="HiddenField_EditFile" runat="server" />
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile3">
                    <td colspan="2">
                      <asp:Label ID="Label_EditMessageFile" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile4">
                    <td style="width: 175px;">File
                    </td>
                    <td style="width: 725px;">
                      <asp:FileUpload ID="FileUpload_EditFile" runat="server" CssClass="Controls_FileUpload" Width="350px" AllowMultiple="true" />&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile5">
                    <td style="width: 175px;">Section
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_EditField" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_File_EditField" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Section</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile6">
                    <td style="width: 175px;">Sections missing files
                    </td>
                    <td style="width: 725px;">
                      <asp:BulletedList ID="BulletedList_EditFieldMissing" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_File_EditFieldMissing" DataTextField="ListItem_Name">
                      </asp:BulletedList>
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile7">
                    <td colspan="2" style="text-align: center;">
                      <asp:Button ID="Button_EditUploadFile" runat="server" OnClick="Button_EditUploadFile_OnClick" Text="Upload File" CssClass="Controls_Button" CommandArgument="FileUpload_EditFile" />&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile8">
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile9">
                    <td colspan="2">
                      <asp:GridView ID="GridView_EditFile" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_Pharmacy_NewProduct_File_EditFile" CssClass="GridView" AllowPaging="True" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_EditFile_RowCreated" OnPreRender="GridView_EditFile_PreRender">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td>
                                <asp:Button ID="Button_EditDeleteFile" runat="server" OnClick="Button_EditDeleteFile_OnClick" Text="Delete Checked Files" CssClass="Controls_Button" CommandArgument="GridView_EditFile" />&nbsp;
                              <asp:Button ID="Button_EditDeleteAllFile" runat="server" OnClick="Button_EditDeleteAllFile_OnClick" Text="Delete All Files" CssClass="Controls_Button" CommandArgument="GridView_EditFile" />&nbsp;
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
                              <td>No Files
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="" ItemStyle-Width="25px">
                            <ItemTemplate>
                              <asp:CheckBox ID="CheckBox_EditFile" runat="server" CssClass='<%# Eval("NewProduct_File_Id") %>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Uploaded File">
                            <ItemTemplate>
                              <asp:LinkButton ID="LinkButton_EditFile" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("NewProduct_File_Name")) %>' CommandArgument='<%# Eval("NewProduct_File_Id") %>'></asp:LinkButton>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="NewProduct_File_Field_Name" HeaderText="Section" ReadOnly="True" ItemStyle-Width="150px" />
                          <asp:BoundField DataField="NewProduct_File_CreatedBy" HeaderText="Created By" ReadOnly="True" ItemStyle-Width="75px" />
                          <asp:BoundField DataField="NewProduct_File_CreatedDate" HeaderText="Created Date" ReadOnly="True" ItemStyle-Width="125px" />
                        </Columns>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile10">
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">
                      Feedback
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;" id="FormFeedbackDescription">Feedback
                    </td>
                    <td style="width: 725px;">
                      <asp:TextBox ID="TextBox_EditFeedbackDescription" runat="server" TextMode="MultiLine" Rows="4" Width="600px" Text='<%# Bind("NewProduct_Feedback_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                      <asp:Label ID="Label_EditFeedbackDescription" runat="server" Text='<%# Eval("NewProduct_Feedback_Description") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Progress Status
                    </td>
                    <td style="width: 725px;">
                      <asp:DropDownList ID="DropDownList_EditFeedbackProgressStatusList" runat="server" DataSourceID="SqlDataSource_Pharmacy_NewProduct_EditFeedbackProgressStatusList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("NewProduct_Feedback_ProgressStatus_List") %>' CssClass="Controls_DropDownList">
                      </asp:DropDownList>
                      <asp:Label ID="Label_EditFeedbackProgressStatusList" runat="server" Text=""></asp:Label>
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Progress Status Date
                    </td>
                    <td>
                      <asp:Label ID="Label_EditFeedbackDate" runat="server" Text='<%# Bind("NewProduct_Feedback_Date","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
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
                      <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("NewProduct_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Created By
                    </td>
                    <td>
                      <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("NewProduct_CreatedBy") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Modified Date
                    </td>
                    <td>
                      <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("NewProduct_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Modified By
                    </td>
                    <td>
                      <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("NewProduct_ModifiedBy") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Is Active
                    </td>
                    <td>
                      <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("NewProduct_IsActive") %>' />
                      <asp:Label ID="Label_EditIsActive" runat="server" Text='<%# (bool)(Eval("NewProduct_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2">
                      <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="False" CommandName="Update" Text="Print Form" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                    <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="False" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                    <asp:Button ID="Button_EditClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditClear_Click" />&nbsp;
                    <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Form" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2">
                      <asp:Button ID="Button_EditCancel" runat="server" CausesValidation="False" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_EditCancel_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
              </EditItemTemplate>
              <ItemTemplate>
                <table class="FormView_TableBody">
                  <tr>
                    <td colspan="2"></td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Report Number
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemReportNumber" runat="server" Text='<%# Bind("NewProduct_ReportNumber") %>'></asp:Label>
                      <asp:HiddenField ID="HiddenField_Item" runat="server" />
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Facility
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Date<br />
                      (yyyy/mm/dd)
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemDate" runat="server" Text='<%# Bind("NewProduct_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Product Classification
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemProductClassificationList" runat="server" Text=""></asp:Label>
                      <asp:HiddenField ID="HiddenField_ItemProductClassificationList" runat="server" Value='<%# Eval("NewProduct_ProductClassification_List") %>' />
                      &nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Manufacturer / Supplier
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemManufacturerList" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Product Description
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemProductDescription" runat="server" Text='<%# Bind("NewProduct_ProductDescription") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductTradeName">
                    <td style="width: 175px;">Trade Name of product
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemTradeName" runat="server" Text='<%# Bind("NewProduct_TradeName") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductActiveIngredient">
                    <td style="width: 175px;">Active Ingredient
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemActiveIngredient" runat="server" Text='<%# Bind("NewProduct_ActiveIngredient") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductStrength">
                    <td style="width: 175px;">Strength
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemStrength" runat="server" Text='<%# Bind("NewProduct_Strength") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Pack Size
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemPackSize" runat="server" Text='<%# Bind("NewProduct_PackSize") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Nett Price excluding
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemNettPrice" runat="server" Text='<%# Bind("NewProduct_NettPrice") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductUseList">
                    <td style="width: 175px;">Single use or multiple use
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemUseList" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductSupplierCatalogNumber">
                    <td style="width: 175px;">Supplier Catalog Number
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemSupplierCatalogNumber" runat="server" Text='<%# Bind("NewProduct_SupplierCatalogNumber") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">9 Digit Nappi code
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemNappiCode" runat="server" Text='<%# Bind("NewProduct_NappiCode") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductProductRequestList">
                    <td style="width: 175px;">Reason for new Product Request
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemProductRequestList" runat="server" Text=""></asp:Label>&nbsp;
                    <asp:HiddenField ID="HiddenField_ItemProductRequestList" runat="server" Value='<%# Eval("NewProduct_ProductRequest_List") %>' />
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductRequestByList">
                    <td style="width: 175px;">Requested By
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemRequestByList" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFormularyProduct1">
                    <td>Is there a formulary product
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemFormularyProduct" runat="server" Text='<%# string.IsNullOrEmpty(Eval("NewProduct_FormularyProduct").ToString())?"" : (bool)(Eval("NewProduct_FormularyProduct"))?"Yes":"No" %>'></asp:Label>
                      <asp:HiddenField ID="HiddenField_ItemFormularyProduct" runat="server" Value='<%# Eval("NewProduct_FormularyProduct") %>' />
                      &nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFormularyProduct2">
                    <td style="width: 175px;">Formulary Product Description / Code
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemFormularyProductDescription" runat="server" Text='<%# Bind("NewProduct_FormularyProductDescription") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductComment">
                    <td style="width: 175px;">Comment
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemComment" runat="server" Text='<%# Bind("NewProduct_Comment") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductComparisonHeading1">
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductComparisonHeading2">
                    <td colspan="2" class="FormView_TableBodyHeader">
                      Comparison of New Product against Current Product
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductClinicalBenefit">
                    <td style="width: 175px;">Clinical benefit compared to current product
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemClinicalBenefit" runat="server" Text='<%# Bind("NewProduct_ClinicalBenefit") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFinancialBenefit">
                    <td style="width: 175px;">Financial benefit compared to current product
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemFinancialBenefit" runat="server" Text='<%# Bind("NewProduct_FinancialBenefit") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductRequirement">
                    <td style="width: 175px;">Additional items or equipment required for the use of this product
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemRequirement" runat="server" Text='<%# Bind("NewProduct_Requirement") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile1">
                    <td colspan="2">Uploaded Files
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile2">
                    <td colspan="2">
                      <asp:GridView ID="GridView_ItemFile" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_Pharmacy_NewProduct_File_ItemFile" CssClass="GridView" AllowPaging="True" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemFile_RowCreated" OnPreRender="GridView_ItemFile_PreRender">
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
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td>No Files
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="Uploaded File">
                            <ItemTemplate>
                              <asp:LinkButton ID="LinkButton_ItemFile" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("NewProduct_File_Name")) %>' CommandArgument='<%# Eval("NewProduct_File_Id") %>'></asp:LinkButton>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="NewProduct_File_Field_Name" HeaderText="Section" ReadOnly="True" ItemStyle-Width="150px" />
                          <asp:BoundField DataField="NewProduct_File_CreatedBy" HeaderText="Created By" ReadOnly="True" ItemStyle-Width="75px" />
                          <asp:BoundField DataField="NewProduct_File_CreatedDate" HeaderText="Created Date" ReadOnly="True" ItemStyle-Width="125px" />
                        </Columns>
                      </asp:GridView>
                    </td>
                  </tr>
                  <tr id="PharmacyNewProductFile3">
                    <td colspan="2">&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="FormView_TableBodyHeader">
                      Feedback
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 175px;">Feedback
                    </td>
                    <td style="width: 725px;">
                      <asp:Label ID="Label_ItemFeedbackDescription" runat="server" Text='<%# Bind("NewProduct_Feedback_Description") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Progress Status
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemFeedbackProgressStatusList" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Progress Status Date
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemFeedbackDate" runat="server" Text='<%# Bind("NewProduct_Feedback_Date","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
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
                      <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("NewProduct_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Created By
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("NewProduct_CreatedBy") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Modified Date
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("NewProduct_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Modified By
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("NewProduct_ModifiedBy") %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td>Is Active
                    </td>
                    <td>
                      <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("NewProduct_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2">
                      <asp:Button ID="Button_ItemPrint" runat="server" CausesValidation="False" CommandName="Print" Text="Print Form" CssClass="Controls_Button" />&nbsp;
                    <asp:Button ID="Button_ItemEmail" runat="server" CausesValidation="False" CommandName="Email" Text="Email Link" CssClass="Controls_Button" />&nbsp;
                    <asp:Button ID="Button_ItemClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_ItemClear_Click" />&nbsp;
                    </td>
                  </tr>
                  <tr class="FormView_TableFooter">
                    <td colspan="2">
                      <asp:Button ID="Button_ItemCancel" runat="server" CausesValidation="False" Text="Go to Captured Forms" CssClass="Controls_Button" OnClick="Button_ItemCancel_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
              </ItemTemplate>
            </asp:FormView>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_InsertFacility" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_InsertProductClassificationList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_InsertManufacturerList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_InsertProductRequestList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_InsertRequestByList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_InsertUseList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_InsertFeedbackProgressStatusList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_File_InsertField" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_File_InsertFieldMissing" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_File_InsertFile" runat="server" OnSelected="SqlDataSource_Pharmacy_NewProduct_InsertFile_Selected"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_EditProductClassificationList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_EditManufacturerList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_EditProductRequestList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_EditRequestByList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_EditUseList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_EditFeedbackProgressStatusList" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_File_EditField" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_File_EditFieldMissing" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_File_EditFile" runat="server" OnSelected="SqlDataSource_Pharmacy_NewProduct_EditFile_Selected"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_File_ItemFile" runat="server" OnSelected="SqlDataSource_Pharmacy_NewProduct_ItemFile_Selected"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_NewProduct_Form" runat="server" OnInserted="SqlDataSource_Pharmacy_NewProduct_Form_Inserted" OnUpdated="SqlDataSource_Pharmacy_NewProduct_Form_Updated"></asp:SqlDataSource>
          </td>
        </tr>
      </table>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
