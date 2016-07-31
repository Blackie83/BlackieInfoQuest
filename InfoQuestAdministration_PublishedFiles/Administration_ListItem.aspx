<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_ListItem" CodeBehind="Administration_ListItem.aspx.cs" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - List Item</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_ListItem.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_ListItem" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_ListItem" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_ListItem" AssociatedUpdatePanelID="UpdatePanel_ListItem">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_ListItem" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>List Item
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_ListItem_Form" runat="server" DataKeyNames="ListItem_Id" CssClass="FormView" DataSourceID="SqlDataSource_ListItem_Form" OnItemInserting="FormView_ListItem_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_ListItem_Form_ItemCommand" OnItemUpdating="FormView_ListItem_Form_ItemUpdating" OnDataBound="FormView_ListItem_Form_DataBound">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="2">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;">Id
                        </td>
                        <td>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFormId">Form
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_InsertFormId" runat="server" DataSourceID="SqlDataSource_ListItem_InsertFormId" AppendDataBoundItems="true" DataTextField="Form_Name" DataValueField="Form_Id" CssClass="Controls_DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_InsertFormId_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Form</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormListCategoryId">List Category
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_InsertListCategoryId" runat="server" DataSourceID="SqlDataSource_ListItem_InsertListCategoryId" AppendDataBoundItems="true" DataTextField="ListCategory_Name" DataValueField="ListCategory_Id" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListCategory_Id") %>' CssClass="Controls_DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_InsertListCategoryId_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormParent">Parent
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_InsertParent" runat="server" DataSourceID="SqlDataSource_ListItem_InsertParent" AppendDataBoundItems="true" DataTextField="ListItem_ParentName" DataValueField="ListItem_Parent" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListItem_Parent") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Parent</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormName">Name
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertName" runat="server" Width="500px" Text='<%# Bind("ListItem_Name") %>' CssClass="Controls_TextBox" AutoPostBack="False" OnTextChanged="TextBox_InsertName_TextChanged"></asp:TextBox>
                          <asp:DropDownList ID="DropDownList_InsertName_Facility" runat="server" DataSourceID="SqlDataSource_ListItem_InsertName_Facility" AppendDataBoundItems="true" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListItem_Name") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Facility</asp:ListItem>
                          </asp:DropDownList>
                          <asp:DropDownList ID="DropDownList_InsertName_ListCategory" runat="server" DataSourceID="SqlDataSource_ListItem_InsertName_ListCategory" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListItem_Name") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Name</asp:ListItem>
                          </asp:DropDownList>
                          <asp:DropDownList ID="DropDownList_InsertName_Unit" runat="server" DataSourceID="SqlDataSource_ListItem_InsertName_Unit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListItem_Name") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          <asp:Label ID="Label_InsertNameError" runat="server"></asp:Label>&nbsp;
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("ListItem_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("ListItem_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("ListItem_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("ListItem_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("ListItem_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add List Item" CssClass="Controls_Button" />&nbsp;
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
                        <td style="width: 175px;">Id
                        </td>
                        <td>
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("ListItem_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFormId">Form
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_EditFormId" runat="server" DataSourceID="SqlDataSource_ListItem_EditFormId" AppendDataBoundItems="true" DataTextField="Form_Name" DataValueField="Form_Id" CssClass="Controls_DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_EditFormId_SelectedIndexChanged" OnDataBound="DropDownList_EditFormId_DataBound">
                            <asp:ListItem Value="">Select Form</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormListCategoryId">List Category
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_EditListCategoryId" runat="server" DataSourceID="SqlDataSource_ListItem_EditListCategoryId" AppendDataBoundItems="true" DataTextField="ListCategory_Name" DataValueField="ListCategory_Id" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListCategory_Id") %>' CssClass="Controls_DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_EditListCategoryId_SelectedIndexChanged" OnDataBound="DropDownList_EditListCategoryId_DataBound">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormParent">Parent
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_EditParent" runat="server" DataSourceID="SqlDataSource_ListItem_EditParent" AppendDataBoundItems="true" DataTextField="ListItem_ParentName" DataValueField="ListItem_Parent" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListItem_Parent") %>' CssClass="Controls_DropDownList" OnDataBound="DropDownList_EditParent_DataBound">
                            <asp:ListItem Value="">Select Parent</asp:ListItem>
                            <asp:ListItem Value="-1">No Parent</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormName">Name
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditName" runat="server" Width="500px" Text='<%# Bind("ListItem_Name") %>' CssClass="Controls_TextBox" AutoPostBack="False" OnTextChanged="TextBox_EditName_TextChanged"></asp:TextBox>
                          <asp:DropDownList ID="DropDownList_EditName_Facility" runat="server" DataSourceID="SqlDataSource_ListItem_EditName_Facility" AppendDataBoundItems="true" DataTextField="Facility_FacilityDisplayName" DataValueField="Facility_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Facility</asp:ListItem>
                          </asp:DropDownList>
                          <asp:DropDownList ID="DropDownList_EditName_ListCategory" runat="server" DataSourceID="SqlDataSource_ListItem_EditName_ListCategory" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Name</asp:ListItem>
                          </asp:DropDownList>
                          <asp:DropDownList ID="DropDownList_EditName_Unit" runat="server" DataSourceID="SqlDataSource_ListItem_EditName_Unit" AppendDataBoundItems="true" DataTextField="Unit_Name" DataValueField="Unit_Id" CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Unit</asp:ListItem>
                          </asp:DropDownList>
                          <asp:Label ID="Label_EditNameError" runat="server"></asp:Label>&nbsp;
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("ListItem_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("ListItem_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("ListItem_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("ListItem_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("ListItem_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update List Item" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_InsertFormId" runat="server" SelectCommand="SELECT DISTINCT Form_Id , Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')' AS Form_Name FROM Administration_Form ORDER BY Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_InsertListCategoryId" runat="server" SelectCommand="SELECT DISTINCT ListCategory_Id , ListCategory_Name + ' (' + CASE WHEN ListCategory_IsActive = 1 THEN 'Yes' WHEN ListCategory_IsActive = 0 THEN 'No' END + ')' AS ListCategory_Name FROM vAdministration_ListCategory_All WHERE (Form_Id = @Form_Id OR @Form_Id = '') ORDER BY ListCategory_Name + ' (' + CASE WHEN ListCategory_IsActive = 1 THEN 'Yes' WHEN ListCategory_IsActive = 0 THEN 'No' END + ')'">
                  <SelectParameters>
                    <asp:Parameter Name="Form_Id" Type="String" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_InsertParent" runat="server" SelectCommand="SELECT A.ListItem_Id AS ListItem_Parent, CASE WHEN LEN(A.ListItem_Name) > 100 THEN LEFT(A.ListItem_Name,100) + '.....' + ' (' + CASE WHEN A.ListItem_IsActive = 1 THEN 'Yes' WHEN A.ListItem_IsActive = 0 THEN 'No' END + ') (' + CAST(A.ListItem_Id AS NVARCHAR(MAX)) + ')' ELSE A.ListItem_Name + ' (' + CASE WHEN A.ListItem_IsActive = 1 THEN 'Yes' WHEN A.ListItem_IsActive = 0 THEN 'No' END + ') (' + CAST(A.ListItem_Id AS NVARCHAR(MAX)) + ')' END AS ListItem_ParentName FROM Administration_ListItem AS A LEFT OUTER JOIN Administration_ListItem AS B ON A.ListItem_Parent = B.ListItem_Id WHERE A.ListCategory_Id IN ( SELECT ListCategory_Parent FROM Administration_ListCategory WHERE	Form_Id = @Form_Id AND ListCategory_Id = @ListCategory_Id AND ListCategory_Parent != -1 ) ORDER BY CASE WHEN LEN(A.ListItem_Name) > 100 THEN LEFT(A.ListItem_Name,100) + '.....' + ' (' + CASE WHEN A.ListItem_IsActive = 1 THEN 'Yes' WHEN A.ListItem_IsActive = 0 THEN 'No' END + ') (' + CAST(A.ListItem_Id AS NVARCHAR(MAX)) + ')' ELSE A.ListItem_Name + ' (' + CASE WHEN A.ListItem_IsActive = 1 THEN 'Yes' WHEN A.ListItem_IsActive = 0 THEN 'No' END + ') (' + CAST(A.ListItem_Id AS NVARCHAR(MAX)) + ')' END">
                  <SelectParameters>
                    <asp:Parameter Name="Form_Id" Type="String" />
                    <asp:Parameter Name="ListCategory_Id" Type="String" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_InsertName_Facility" runat="server" SelectCommand="SELECT Facility_Id , Facility_FacilityDisplayName + ' (' + CASE Facility_IsActive WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END + ')' AS Facility_FacilityDisplayName FROM vAdministration_Facility_All ORDER BY Facility_FacilityDisplayName"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_InsertName_ListCategory" runat="server" SelectCommand="SELECT ListItem_Id , ListItem_Name + ' (' + CAST(ListItem_Id AS NVARCHAR(MAX)) + ')' AS ListItem_Name FROM Administration_ListItem WHERE ListCategory_Id IN ( SELECT ListCategory_LinkedCategory_List_ListCategory_Child FROM Administration_ListCategory WHERE ListCategory_Id = @ListCategory_Id AND ListCategory_LinkedCategory = 1 AND ListCategory_LinkedCategory_List = 'List Category' ) ORDER BY ListItem_Name">
                  <SelectParameters>
                    <asp:Parameter Name="ListCategory_Id" Type="String" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_InsertName_Unit" runat="server" SelectCommand="SELECT Unit_Id , Unit_Name + ' (' + CASE Unit_IsActive WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END + ')' AS Unit_Name FROM Administration_Unit ORDER BY Unit_Name"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_EditFormId" runat="server" SelectCommand="SELECT DISTINCT Form_Id , Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')' AS Form_Name FROM Administration_Form ORDER BY Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_EditListCategoryId" runat="server" SelectCommand="SELECT DISTINCT ListCategory_Id , ListCategory_Name + ' (' + CASE WHEN ListCategory_IsActive = 1 THEN 'Yes' WHEN ListCategory_IsActive = 0 THEN 'No' END + ')' AS ListCategory_Name FROM vAdministration_ListCategory_All WHERE (Form_Id = @Form_Id OR @Form_Id = '') ORDER BY ListCategory_Name + ' (' + CASE WHEN ListCategory_IsActive = 1 THEN 'Yes' WHEN ListCategory_IsActive = 0 THEN 'No' END + ')'">
                  <SelectParameters>
                    <asp:Parameter Name="Form_Id" Type="String" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_EditParent" runat="server" SelectCommand="SELECT A.ListItem_Id AS ListItem_Parent, CASE WHEN LEN(A.ListItem_Name) > 100 THEN LEFT(A.ListItem_Name,100) + '.....' + ' (' + CASE WHEN A.ListItem_IsActive = 1 THEN 'Yes' WHEN A.ListItem_IsActive = 0 THEN 'No' END + ') (' + CAST(A.ListItem_Id AS NVARCHAR(MAX)) + ')' ELSE A.ListItem_Name + ' (' + CASE WHEN A.ListItem_IsActive = 1 THEN 'Yes' WHEN A.ListItem_IsActive = 0 THEN 'No' END + ') (' + CAST(A.ListItem_Id AS NVARCHAR(MAX)) + ')' END AS ListItem_ParentName FROM Administration_ListItem AS A LEFT OUTER JOIN Administration_ListItem AS B ON A.ListItem_Parent = B.ListItem_Id WHERE A.ListCategory_Id IN ( SELECT ListCategory_Parent FROM Administration_ListCategory WHERE	Form_Id = @Form_Id AND ListCategory_Id = @ListCategory_Id AND ListCategory_Parent != -1 ) ORDER BY CASE WHEN LEN(A.ListItem_Name) > 100 THEN LEFT(A.ListItem_Name,100) + '.....' + ' (' + CASE WHEN A.ListItem_IsActive = 1 THEN 'Yes' WHEN A.ListItem_IsActive = 0 THEN 'No' END + ') (' + CAST(A.ListItem_Id AS NVARCHAR(MAX)) + ')' ELSE A.ListItem_Name + ' (' + CASE WHEN A.ListItem_IsActive = 1 THEN 'Yes' WHEN A.ListItem_IsActive = 0 THEN 'No' END + ') (' + CAST(A.ListItem_Id AS NVARCHAR(MAX)) + ')' END">
                  <SelectParameters>
                    <asp:Parameter Name="Form_Id" Type="String" />
                    <asp:Parameter Name="ListCategory_Id" Type="String" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_EditName_Facility" runat="server" SelectCommand="SELECT Facility_Id , Facility_FacilityDisplayName + ' (' + CASE Facility_IsActive WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END + ')' AS Facility_FacilityDisplayName FROM vAdministration_Facility_All ORDER BY Facility_FacilityDisplayName"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_EditName_ListCategory" runat="server" SelectCommand="SELECT ListItem_Id , ListItem_Name + ' (' + CAST(ListItem_Id AS NVARCHAR(MAX)) + ')' AS ListItem_Name FROM Administration_ListItem WHERE ListCategory_Id IN ( SELECT ListCategory_LinkedCategory_List_ListCategory_Child FROM Administration_ListCategory WHERE ListCategory_Id = @ListCategory_Id AND ListCategory_LinkedCategory = 1 AND ListCategory_LinkedCategory_List = 'List Category' ) ORDER BY ListItem_Name">
                  <SelectParameters>
                    <asp:Parameter Name="ListCategory_Id" Type="String" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_EditName_Unit" runat="server" SelectCommand="SELECT Unit_Id , Unit_Name + ' (' + CASE Unit_IsActive WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END + ')' AS Unit_Name FROM Administration_Unit ORDER BY Unit_Name"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListItem_Form" runat="server" InsertCommand="INSERT INTO Administration_ListItem (ListCategory_Id ,ListItem_Parent ,ListItem_Name ,ListItem_CreatedDate ,ListItem_CreatedBy ,ListItem_ModifiedDate ,ListItem_ModifiedBy ,ListItem_History ,ListItem_IsActive) VALUES (@ListCategory_Id ,@ListItem_Parent ,@ListItem_Name ,@ListItem_CreatedDate ,@ListItem_CreatedBy ,@ListItem_ModifiedDate ,@ListItem_ModifiedBy ,@ListItem_History ,@ListItem_IsActive); SELECT @ListItem_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_ListItem WHERE (ListItem_Id = @ListItem_Id)" UpdateCommand="UPDATE Administration_ListItem SET ListCategory_Id = @ListCategory_Id ,ListItem_Parent = @ListItem_Parent ,ListItem_Name = @ListItem_Name ,ListItem_ModifiedDate = @ListItem_ModifiedDate ,ListItem_ModifiedBy = @ListItem_ModifiedBy ,ListItem_History = @ListItem_History ,ListItem_IsActive = @ListItem_IsActive WHERE ListItem_Id = @ListItem_Id" OnUpdated="SqlDataSource_ListItem_Form_Updated" OnInserted="SqlDataSource_ListItem_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="ListItem_Id" Type="Int32" />
                    <asp:Parameter Name="ListCategory_Id" Type="Int32" />
                    <asp:Parameter Name="ListItem_Parent" Type="Int32" />
                    <asp:Parameter Name="ListItem_Name" Type="String" />
                    <asp:Parameter Name="ListItem_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="ListItem_CreatedBy" Type="String" />
                    <asp:Parameter Name="ListItem_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="ListItem_ModifiedBy" Type="String" />
                    <asp:Parameter Name="ListItem_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="ListItem_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="ListItem_Id" QueryStringField="ListItem_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="ListCategory_Id" Type="Int32" />
                    <asp:Parameter Name="ListItem_Parent" Type="Int32" />
                    <asp:Parameter Name="ListItem_Name" Type="String" />
                    <asp:Parameter Name="ListItem_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="ListItem_ModifiedBy" Type="String" />
                    <asp:Parameter Name="ListItem_History" Type="String" />
                    <asp:Parameter Name="ListItem_IsActive" Type="Boolean" />
                    <asp:Parameter Name="ListItem_Id" Type="Int32" />
                  </UpdateParameters>
                </asp:SqlDataSource>
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
