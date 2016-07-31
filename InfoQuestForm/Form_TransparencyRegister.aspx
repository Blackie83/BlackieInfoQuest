<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_TransparencyRegister.aspx.cs" Inherits="InfoQuestForm.Form_TransparencyRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Transparency Register</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_TransparencyRegister.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_TransparencyRegister" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_TransparencyRegister" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_TransparencyRegister" AssociatedUpdatePanelID="UpdatePanel_TransparencyRegister">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_TransparencyRegister" runat="server">
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
          <table>
            <tr>
              <td>
                This form is used to record all gifts and / or benefits that are given to Life Healthcare employees that are in excess of R750 in value.<br />
                The completed form will be sent to your line manager for approval, this is to ensure that the gift / benefit being given is in line with the Company’s Code of Ethics Policy.<br />
                Please complete all the fields.
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
                      <asp:Label ID="Label_TransparencyRegisterHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_TransparencyRegister_Form" runat="server" Width="900px" DataKeyNames="TransparencyRegister_Id" CssClass="FormView" DataSourceID="SqlDataSource_TransparencyRegister_Form" OnItemInserting="FormView_TransparencyRegister_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_TransparencyRegister_Form_ItemCommand" OnDataBound="FormView_TransparencyRegister_Form_DataBound" OnItemUpdating="FormView_TransparencyRegister_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Details of Employee receiving Gift or Hospitality
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormEmployeeNumber">Employee Number
                        </td>
                        <td style="width: 725px;">
                          <asp:TextBox ID="TextBox_InsertEmployeeNumber" runat="server" Width="300px" Text='<%# Bind("TransparencyRegister_EmployeeNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:Button ID="Button_InsertFindEmployee" runat="server" OnClick="Button_InsertFindEmployee_Click" Text="Find Employee Details" CssClass="Controls_Button" />
                          <asp:Button ID="Button_InsertClearEmployee" runat="server" OnClick="Button_InsertClearEmployee_Click" Text="Clear Employee Details" CssClass="Controls_Button" />
                          <asp:Label ID="Label_InsertEmployeeError" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          <asp:HiddenField ID="HiddenField_InsertTransparencyRegisterIdTemp" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">First Name
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_InsertFirstName" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_InsertFirstName" runat="server" Value='<%# Bind("TransparencyRegister_FirstName") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Last Name
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_InsertLastName" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_InsertLastName" runat="server" Value='<%# Bind("TransparencyRegister_LastName") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Job Title
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_InsertJobTitle" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_InsertJobTitle" runat="server" Value='<%# Bind("TransparencyRegister_JobTitle") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Department
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_InsertDepartment" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_InsertDepartment" runat="server" Value='<%# Bind("TransparencyRegister_Department") %>' />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Approving Manager
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_InsertManagerName" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_InsertManagerEmployeeNumber" runat="server" Value='<%# Bind("TransparencyRegister_ManagerEmployeeNumber") %>' />
                          <asp:HiddenField ID="HiddenField_InsertManagerFirstName" runat="server" Value='<%# Bind("TransparencyRegister_ManagerFirstName") %>' />
                          <asp:HiddenField ID="HiddenField_InsertManagerLastName" runat="server" Value='<%# Bind("TransparencyRegister_ManagerLastName") %>' />
                          <asp:HiddenField ID="HiddenField_InsertManagerUserName" runat="server" />
                          <asp:HiddenField ID="HiddenField_InsertManagerEmail" runat="server" />
                          <asp:HiddenField ID="HiddenField_InsertReportsTo" runat="server" />                          
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Description of Gift or Hospitality
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDeclarationDate">Declaration Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 725px;">
                          <asp:TextBox ID="TextBox_InsertDeclarationDate" runat="server" Width="75px" Text='<%# Bind("TransparencyRegister_DeclarationDate","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertDeclarationDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertDeclarationDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertDeclarationDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertDeclarationDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertDeclarationDate" runat="server" TargetControlID="TextBox_InsertDeclarationDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormClassification">Classification
                        </td>
                        <td style="width: 725px; padding: 0px;">
                          <asp:HiddenField ID="HiddenField_InsertTotalRecords" runat="server" />
                          <asp:GridView ID="GridView_InsertTransparencyRegister_Classification" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_TransparencyRegister_InsertClassification" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_InsertTransparencyRegister_Classification_RowCreated" OnDataBound="GridView_InsertTransparencyRegister_Classification_DataBound" OnPreRender="GridView_InsertTransparencyRegister_Classification_PreRender">
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
                              No Classification selected
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="Classification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
                                <ItemTemplate>
                                  <asp:CheckBox ID="CheckBox_InsertName" runat="server" Text='<%# Bind("ListItem_Name") %>' />
                                  <asp:HiddenField ID="HiddenField_InsertId" runat="server" Value='<%# Bind("ListItem_Id") %>' />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="50%">
                                <ItemTemplate>
                                  <asp:Label ID="Label_InsertValueCurrency" runat="server" Text="R"></asp:Label>&nbsp;
                                  <asp:TextBox ID="TextBox_InsertValue" runat="server" Width="200px" CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                                  <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_InsertValue" runat="server" TargetControlID="TextBox_InsertValue" FilterType="Custom, Numbers" ValidChars=".">
                                  </Ajax:FilteredTextBoxExtender><br />
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator_InsertValue" runat="server" Display="Dynamic" ControlToValidate="TextBox_InsertValue" ErrorMessage="Please enter only numbers like 1000 or 1000.00" CssClass="Controls_Error" ValidationExpression="^\d+(?:\.\d{0,2})?$"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormValue">Total Value
                        </td>
                        <td style="width: 725px;">R
                          <asp:TextBox ID="TextBox_InsertValue" runat="server" Width="600px" Text='<%# Bind("TransparencyRegister_Value") %>' CssClass="Controls_TextBox_Calculation"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td style="width: 725px;">
                          <asp:TextBox ID="TextBox_InsertDescription" runat="server" Width="700px" TextMode="MultiLine" Rows="5" Text='<%# Bind("TransparencyRegister_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr> 
                      <tr>
                        <td style="width: 175px;" id="FormPurpose">Purpose of the offer
                        </td>
                        <td style="width: 725px;">
                          <asp:TextBox ID="TextBox_InsertPurpose" runat="server" Width="700px" TextMode="MultiLine" Rows="5" Text='<%# Bind("TransparencyRegister_Purpose") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPersonOrganisation">Person / organisation providing the gift or hospitality
                        </td>
                        <td style="width: 725px;">
                          <asp:TextBox ID="TextBox_InsertPersonOrganisation" runat="server" Width="700px" TextMode="MultiLine" Rows="5" Text='<%# Bind("TransparencyRegister_PersonOrganisation") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRelationship">Relationship (or future relationship) to the person / organisation offering the gift or hospitality
                        </td>
                        <td style="width: 725px;">
                          <asp:TextBox ID="TextBox_InsertRelationship" runat="server" Width="700px" TextMode="MultiLine" Rows="5" Text='<%# Bind("TransparencyRegister_Relationship") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">If you do not want to upload a photo please click on the “Submit Form” button below.
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Voluntary Photo Upload
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          If you would like to upload a photo of the gift / hospitality offered please follow these instructions:<br />
                          1)	Click on "Browse..."<br />
                          2)	Select the appropriate files for upload (only the following formats are permissible: Word (.doc / .docx), Excel (.xls / .xlsx), Adobe (.pdf), Fax (.tif / .tiff), TXT (.txt), Outlook (.msg), Images (.jpeg / .jpg / .gif / .png) and only files smaller than 5 MB can be uploaded<br />
                          3)	Click on "Upload file"<br />
                          4)	Click on "Submit Form"
                        <asp:HiddenField ID="HiddenField_InsertFile" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertMessageFile" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">File
                        </td>
                        <td style="width: 725px;" colspan="3">
                          <asp:FileUpload ID="FileUpload_InsertFile" runat="server" CssClass="Controls_FileUpload" Width="350px" AllowMultiple="true" />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="text-align: center;">
                          <asp:Button ID="Button_InsertUploadFile" runat="server" OnClick="Button_InsertUploadFile_OnClick" Text="Upload File" CssClass="Controls_Button" CommandArgument="FileUpload_InsertFile" OnDataBinding="Button_InsertUploadFile_DataBinding" />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="padding: 0px; border-left-width: 0px; border-top-width: 0px;">
                          <asp:GridView ID="GridView_InsertFile" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_TransparencyRegister_File_InsertFile" CssClass="GridView" AllowPaging="True" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_InsertFile_RowCreated" OnPreRender="GridView_InsertFile_PreRender">
                            <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                            <PagerTemplate>
                              <table class="GridView_PagerStyle">
                                <tr>
                                  <td>
                                    <asp:Button ID="Button_InsertDeleteFile" runat="server" OnClick="Button_InsertDeleteFile_OnClick" Text="Delete Checked Files" CssClass="Controls_Button" CommandArgument="GridView_InsertFile" OnDataBinding="Button_InsertDeleteFile_DataBinding" />&nbsp;
                                  <asp:Button ID="Button_InsertDeleteAllFile" runat="server" OnClick="Button_InsertDeleteAllFile_OnClick" Text="Delete All Files" CssClass="Controls_Button" CommandArgument="GridView_InsertFile" OnDataBinding="Button_InsertDeleteAllFile_DataBinding" />&nbsp;
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
                                  <td>No Photos
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="" ItemStyle-Width="25px">
                                <ItemTemplate>
                                  <asp:CheckBox ID="CheckBox_InsertFile" runat="server" CssClass='<%# Eval("TransparencyRegister_File_Id") %>' />
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Uploaded Photos">
                                <ItemTemplate>
                                  <asp:LinkButton ID="LinkButton_InsertFile" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("TransparencyRegister_File_FileName")) %>' CommandArgument='<%# Eval("TransparencyRegister_File_Id") %>' OnDataBinding="LinkButton_InsertFile_DataBinding"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="TransparencyRegister_File_CreatedBy" HeaderText="Created By" ReadOnly="True" ItemStyle-Width="75px" />
                              <asp:BoundField DataField="TransparencyRegister_File_CreatedDate" HeaderText="Created Date" ReadOnly="True" ItemStyle-Width="125px" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <%--<tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Form Status
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Form Status
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_InsertStatus" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td style="width: 725px;">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Approved or Rejected By
                        </td>
                        <td style="width: 725px;">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Message
                        </td>
                        <td style="width: 725px;">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created Date
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("TransparencyRegister_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created By
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("TransparencyRegister_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified Date
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("TransparencyRegister_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified By
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("TransparencyRegister_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>--%>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_InsertClear_Click" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Submit Form" CssClass="Controls_Button" />&nbsp;
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
                        <td colspan="2" class="FormView_TableBodyHeader">Details of Employee receiving Gift or Hospitality
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormEmployeeNumber">Employee Number
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditEmployeeNumber" runat="server" Text='<%# Bind("TransparencyRegister_EmployeeNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_EditEmployeeNumber" runat="server" Value='<%# Bind("TransparencyRegister_EmployeeNumber") %>' />
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">First Name
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditFirstName" runat="server" Text='<%# Bind("TransparencyRegister_FirstName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Last Name
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditLastName" runat="server" Text='<%# Bind("TransparencyRegister_LastName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Job Title
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditJobTitle" runat="server" Text='<%# Bind("TransparencyRegister_JobTitle") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Department
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditDepartment" runat="server" Text='<%# Bind("TransparencyRegister_Department") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Approving Manager
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditManagerName" runat="server" Text='<%# Eval("TransparencyRegister_ManagerFirstName") + " " + Eval("TransparencyRegister_ManagerLastName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Description of Gift or Hospitality
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDeclarationDate">Declaration Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditDeclarationDate" runat="server" Text='<%# Bind("TransparencyRegister_DeclarationDate","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormClassification">Classification
                        </td>
                        <td style="width: 725px; padding: 0px;">
                          <asp:GridView ID="GridView_EditTransparencyRegister_Classification" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_TransparencyRegister_EditClassification" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="False" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_EditTransparencyRegister_Classification_RowCreated" OnPreRender="GridView_EditTransparencyRegister_Classification_PreRender">
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
                              No Classification selected
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="ListItem_Name" HeaderText="Reason" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" SortExpression="ListItem_Name" />
                              <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                  <asp:Label ID="Label_EditValueCurrency" runat="server" Text="R"></asp:Label>&nbsp;
                                  <asp:Label ID="Label_EditValue" runat="server" Text='<%# Convert.ToDecimal(Eval("TransparencyRegister_Classification_Value")).ToString("#,##0.00") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormValue">Total Value
                        </td>
                        <td style="width: 725px;">R
                          <asp:Label ID="Label_EditValue" runat="server" Text='<%# Convert.ToDecimal(Eval("TransparencyRegister_Value")).ToString("#,##0.00") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditDescription" runat="server" Text='<%# Bind("TransparencyRegister_Description") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPurpose">Purpose of the offer
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditPurpose" runat="server" Text='<%# Bind("TransparencyRegister_Purpose") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPersonOrganisation">Person / organisation providing the gift or hospitality
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditPersonOrganisation" runat="server" Text='<%# Bind("TransparencyRegister_PersonOrganisation") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRelationship">Relationship (or future relationship) to the person / organisation offering the gift or hospitality
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditRelationship" runat="server" Text='<%# Bind("TransparencyRegister_Relationship") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Uploaded Photos
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="padding: 0px; border-left-width: 0px; border-top-width: 0px;">
                          <asp:GridView ID="GridView_EditFile" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_TransparencyRegister_File_EditFile" CssClass="GridView" AllowPaging="True" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_EditFile_RowCreated" OnPreRender="GridView_EditFile_PreRender">
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
                                  <td>No Photos
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="Uploaded Photos">
                                <ItemTemplate>
                                  <asp:LinkButton ID="LinkButton_EditFile" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("TransparencyRegister_File_FileName")) %>' CommandArgument='<%# Eval("TransparencyRegister_File_Id") %>' OnDataBinding="LinkButton_EditFile_DataBinding"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="TransparencyRegister_File_CreatedBy" HeaderText="Created By" ReadOnly="True" ItemStyle-Width="75px" />
                              <asp:BoundField DataField="TransparencyRegister_File_CreatedDate" HeaderText="Created Date" ReadOnly="True" ItemStyle-Width="125px" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Form Status
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Form Status
                        </td>
                        <td style="width: 725px;">
                          <asp:DropDownList ID="DropDownList_EditStatus" runat="server" SelectedValue='<%# Bind("TransparencyRegister_Status") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="Pending Approval">Pending Approval</asp:ListItem>
                            <asp:ListItem Value="Approved">Approved</asp:ListItem>
                            <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditStatusDate" runat="server" Text='<%# Bind("TransparencyRegister_StatusDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Approved or Rejected By
                        </td>
                        <td style="width: 725px;">
                         <asp:Label ID="Label_EditStatusChangedBy" runat="server" Text='<%# Bind("TransparencyRegister_StatusApprovedRejectedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormStatusMessage">Message
                        </td>
                        <td style="width: 725px;">
                          <asp:TextBox ID="TextBox_EditStatusMessage" runat="server" TextMode="MultiLine" Rows="5" Width="700px" Text='<%# Bind("TransparencyRegister_StatusMessage") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created Date
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("TransparencyRegister_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("TransparencyRegister_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("TransparencyRegister_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("TransparencyRegister_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditPrint" runat="server" CausesValidation="True" CommandName="Update" Text="Print Form" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                          <asp:Button ID="Button_EditEmail" runat="server" CausesValidation="True" CommandName="Update" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                          <asp:Button ID="Button_EditClear" runat="server" CausesValidation="False" Text="Clear" CssClass="Controls_Button" OnClick="Button_EditClear_Click" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Form" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
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
                        <td colspan="2" class="FormView_TableBodyHeader">Details of Employee receiving Gift or Hospitality
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormEmployeeNumber">Employee Number
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemEmployeeNumber" runat="server" Text='<%# Bind("TransparencyRegister_EmployeeNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_ItemEmployeeNumber" runat="server" Value='<%# Bind("TransparencyRegister_EmployeeNumber") %>' />
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">First Name
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemFirstName" runat="server" Text='<%# Bind("TransparencyRegister_FirstName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Last Name
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemLastName" runat="server" Text='<%# Bind("TransparencyRegister_LastName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Job Title
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemJobTitle" runat="server" Text='<%# Bind("TransparencyRegister_JobTitle") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Department
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemDepartment" runat="server" Text='<%# Bind("TransparencyRegister_Department") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Approving Manager
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemManagerName" runat="server" Text='<%# Eval("TransparencyRegister_ManagerFirstName") + " " + Eval("TransparencyRegister_ManagerLastName") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Description of Gift or Hospitality
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDeclarationDate">Declaration Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemDeclarationDate" runat="server" Text='<%# Bind("TransparencyRegister_DeclarationDate","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormClassification">Classification
                        </td>
                        <td style="width: 725px; padding: 0px;">
                          <asp:GridView ID="GridView_ItemTransparencyRegister_Classification" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_TransparencyRegister_ItemClassification" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="False" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemTransparencyRegister_Classification_RowCreated" OnPreRender="GridView_ItemTransparencyRegister_Classification_PreRender">
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
                              No Classification selected
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="ListItem_Name" HeaderText="Reason" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" SortExpression="ListItem_Name" />
                              <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                  <asp:Label ID="Label_ItemValueCurrency" runat="server" Text="R"></asp:Label>&nbsp;
                                  <asp:Label ID="Label_ItemValue" runat="server" Text='<%# Convert.ToDecimal(Eval("TransparencyRegister_Classification_Value")).ToString("#,##0.00") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormValue">Total Value
                        </td>
                        <td style="width: 725px;">R
                          <asp:Label ID="Label_ItemValue" runat="server" Text='<%# Convert.ToDecimal(Eval("TransparencyRegister_Value")).ToString("#,##0.00") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemDescription" runat="server" Text='<%# Bind("TransparencyRegister_Description") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPurpose">Purpose of the offer
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemPurpose" runat="server" Text='<%# Bind("TransparencyRegister_Purpose") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormPersonOrganisation">Person / organisation providing the gift or hospitality
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemPersonOrganisation" runat="server" Text='<%# Bind("TransparencyRegister_PersonOrganisation") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormRelationship">Relationship (or future relationship) to the person / organisation offering the gift or hospitality
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemRelationship" runat="server" Text='<%# Bind("TransparencyRegister_Relationship") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Uploaded Photos
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="padding: 0px; border-left-width: 0px; border-top-width: 0px;">
                          <asp:GridView ID="GridView_ItemFile" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_TransparencyRegister_File_ItemFile" CssClass="GridView" AllowPaging="True" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemFile_RowCreated" OnPreRender="GridView_ItemFile_PreRender">
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
                                  <td>No Photos
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:TemplateField HeaderText="Uploaded Photos">
                                <ItemTemplate>
                                  <asp:LinkButton ID="LinkButton_ItemFile" runat="server" OnClick="RetrieveDatabaseFile" Text='<%# DatabaseFileName(Eval("TransparencyRegister_File_FileName")) %>' CommandArgument='<%# Eval("TransparencyRegister_File_Id") %>' OnDataBinding="LinkButton_ItemFile_DataBinding"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="TransparencyRegister_File_CreatedBy" HeaderText="Created By" ReadOnly="True" ItemStyle-Width="75px" />
                              <asp:BoundField DataField="TransparencyRegister_File_CreatedDate" HeaderText="Created Date" ReadOnly="True" ItemStyle-Width="125px" />
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="FormView_TableBodyHeader">Form Status
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Form Status
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemStatus" runat="server" Text='<%# Bind("TransparencyRegister_Status") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Date
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemStatusDate" runat="server" Text='<%# Bind("TransparencyRegister_StatusDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Approved or Rejected By
                        </td>
                        <td style="width: 725px;">
                         <asp:Label ID="Label_ItemStatusChangedBy" runat="server" Text='<%# Bind("TransparencyRegister_StatusApprovedRejectedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Message
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemStatusMessage" runat="server" Text='<%# Bind("TransparencyRegister_StatusMessage") %>'></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created Date
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("TransparencyRegister_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Created By
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("TransparencyRegister_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified Date
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("TransparencyRegister_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Modified By
                        </td>
                        <td style="width: 725px;">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("TransparencyRegister_ModifiedBy") %>'></asp:Label>&nbsp;
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
                <asp:SqlDataSource ID="SqlDataSource_TransparencyRegister_InsertClassification" runat="server"></asp:SqlDataSource>                
                <asp:SqlDataSource ID="SqlDataSource_TransparencyRegister_File_InsertFile" runat="server" OnSelected="SqlDataSource_TransparencyRegister_InsertFile_Selected"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_TransparencyRegister_EditClassification" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_TransparencyRegister_File_EditFile" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_TransparencyRegister_ItemClassification" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_TransparencyRegister_File_ItemFile" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_TransparencyRegister_Form" runat="server" OnInserted="SqlDataSource_TransparencyRegister_Form_Inserted" OnUpdated="SqlDataSource_TransparencyRegister_Form_Updated"></asp:SqlDataSource>
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
