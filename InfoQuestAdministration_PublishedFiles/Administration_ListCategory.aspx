<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestAdministration.Administration_ListCategory" CodeBehind="Administration_ListCategory.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Administration - List Category</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Administration_ListCategory.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_ListCategory" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div style="max-width: 1000px;">
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_ListCategory" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_ListCategory" AssociatedUpdatePanelID="UpdatePanel_ListCategory">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_ListCategory" runat="server">
        <ContentTemplate>
          <div>
            &nbsp;
          </div>
          <table id="TableForm" class="Table" runat="server">
            <tr>
              <td style="vertical-align: top;">
                <table class="Table_Header">
                  <tr>
                    <td>List Category
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_ListCategory_Form" runat="server" DataKeyNames="ListCategory_Id" CssClass="FormView" DataSourceID="SqlDataSource_ListCategory_Form" OnItemInserting="FormView_ListCategory_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_ListCategory_Form_ItemCommand" OnItemUpdating="FormView_ListCategory_Form_ItemUpdating">
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
                          <asp:DropDownList ID="DropDownList_InsertFormId" runat="server" DataSourceID="SqlDataSource_ListCategory_InsertFormId" AppendDataBoundItems="true" DataTextField="Form_Name" DataValueField="Form_Id" SelectedValue='<%# Bind("Form_Id") %>' CssClass="Controls_DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_InsertFormId_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Form</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormParent">Parent
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_InsertParent" runat="server" DataSourceID="SqlDataSource_ListCategory_InsertParent" AppendDataBoundItems="true" DataTextField="ListCategory_Name" DataValueField="ListCategory_Id" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListCategory_Parent") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Parent</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormName">Name
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertName" runat="server" Width="500px" Text='<%# Bind("ListCategory_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_InsertDescription" runat="server" Width="500px" Text='<%# Bind("ListCategory_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Linked Category
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_InsertLinkedCategory" runat="server" Checked='<%# Bind("ListCategory_LinkedCategory") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideLinkedCategoryList">
                        <td style="width: 175px;" id="FormLinkedCategoryList">Linked Category List
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_InsertLinkedCategoryList" runat="server" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListCategory_LinkedCategory_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                            <asp:ListItem Value="Facility">Facility</asp:ListItem>
                            <asp:ListItem Value="List Category">List Category</asp:ListItem>
                            <asp:ListItem Value="Unit">Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="ShowHideLinkedCategoryListCategory1">
                        <td style="width: 175px;" id="FormLinkedCategoryListListCategoryParent">Linked Category Parent
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_InsertLinkedCategoryListListCategoryParent" runat="server" DataSourceID="SqlDataSource_ListCategory_InsertLinkedCategoryListListCategoryParent" AppendDataBoundItems="true" DataTextField="ListCategory_Name" DataValueField="ListCategory_Id" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListCategory_LinkedCategory_List_ListCategory_Parent") %>' CssClass="Controls_DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_InsertLinkedCategoryListListCategoryParent_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="ShowHideLinkedCategoryListCategory2">
                        <td style="width: 175px;" id="FormLinkedCategoryListListCategoryChild">Linked Category Child
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_InsertLinkedCategoryListListCategoryChild" runat="server" DataSourceID="SqlDataSource_ListCategory_InsertLinkedCategoryListListCategoryChild" AppendDataBoundItems="true" DataTextField="ListCategory_Name" DataValueField="ListCategory_Id" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListCategory_LinkedCategory_List_ListCategory_Child") %>' CssClass="Controls_DropDownList" AutoPostBack="True">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
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
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("ListCategory_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("ListCategory_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("ListCategory_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("ListCategory_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("ListCategory_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_InsertBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add List Category" CssClass="Controls_Button" />&nbsp;
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
                          <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("ListCategory_Id") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormFormId">Form
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_EditFormId" runat="server" DataSourceID="SqlDataSource_ListCategory_EditFormId" AppendDataBoundItems="true" DataTextField="Form_Name" DataValueField="Form_Id" SelectedValue='<%# Bind("Form_Id") %>' CssClass="Controls_DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_EditFormId_SelectedIndexChanged" OnDataBound="DropDownList_EditFormId_DataBound">
                            <asp:ListItem Value="">Select Form</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormParent">Parent
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_EditParent" runat="server" DataSourceID="SqlDataSource_ListCategory_EditParent" AppendDataBoundItems="true" DataTextField="ListCategory_Name" DataValueField="ListCategory_Id" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListCategory_Parent") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Parent</asp:ListItem>
                            <asp:ListItem Value="-1">No Parent</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormName">Name
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditName" runat="server" Width="500px" Text='<%# Bind("ListCategory_Name") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditName_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditNameError" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 175px;" id="FormDescription">Description
                        </td>
                        <td>
                          <asp:TextBox ID="TextBox_EditDescription" runat="server" Width="500px" Text='<%# Bind("ListCategory_Description") %>' CssClass="Controls_TextBox"></asp:TextBox>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Linked Category
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditLinkedCategory" runat="server" Checked='<%# Bind("ListCategory_LinkedCategory") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideLinkedCategoryList">
                        <td style="width: 175px;" id="FormLinkedCategoryList">Linked Category List
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_EditLinkedCategoryList" runat="server" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListCategory_LinkedCategory_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                            <asp:ListItem Value="Facility">Facility</asp:ListItem>
                            <asp:ListItem Value="List Category">List Category</asp:ListItem>
                            <asp:ListItem Value="Unit">Unit</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="ShowHideLinkedCategoryListCategory1">
                        <td style="width: 175px;" id="FormLinkedCategoryListListCategoryParent">Linked Category Parent
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_EditLinkedCategoryListListCategoryParent" runat="server" DataSourceID="SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryParent" AppendDataBoundItems="true" DataTextField="ListCategory_Name" DataValueField="ListCategory_Id" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListCategory_LinkedCategory_List_ListCategory_Parent") %>' CssClass="Controls_DropDownList" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_EditLinkedCategoryListListCategoryParent_SelectedIndexChanged" OnDataBound="DropDownList_EditLinkedCategoryListListCategoryParent_DataBound">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr id="ShowHideLinkedCategoryListCategory2">
                        <td style="width: 175px;" id="FormLinkedCategoryListListCategoryChild">Linked Category Child
                        </td>
                        <td style="width: 525px;">
                          <asp:DropDownList ID="DropDownList_EditLinkedCategoryListListCategoryChild" runat="server" DataSourceID="SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryChild" AppendDataBoundItems="true" DataTextField="ListCategory_Name" DataValueField="ListCategory_Id" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"ListCategory_LinkedCategory_List_ListCategory_Child") %>' CssClass="Controls_DropDownList" AutoPostBack="True">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                          </asp:DropDownList>
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
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("ListCategory_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Created By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("ListCategory_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified Date
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("ListCategory_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Modified By
                        </td>
                        <td>
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("ListCategory_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td>
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("ListCategory_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="2">
                          <asp:Button ID="Button_EditBackToList" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back To List" CssClass="Controls_Button" />&nbsp;
                        <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update List Category" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_ListCategory_InsertFormId" runat="server" SelectCommand="SELECT DISTINCT Form_Id , Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')' AS Form_Name FROM Administration_Form ORDER BY Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListCategory_InsertParent" runat="server" SelectCommand="SELECT DISTINCT ListCategory_Id , ListCategory_Name + ' (' + CASE WHEN ListCategory_IsActive = 1 THEN 'Yes' WHEN ListCategory_IsActive = 0 THEN 'No' END + ')' AS ListCategory_Name FROM Administration_ListCategory WHERE Form_Id = @Form_Id ORDER BY ListCategory_Name">
                  <SelectParameters>
                    <asp:Parameter Name="Form_Id" Type="String" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListCategory_InsertLinkedCategoryListListCategoryParent" runat="server" SelectCommand="SELECT ListCategory_Id , ListCategory_Name FROM Administration_ListCategory WHERE Form_Id = @Form_Id ORDER BY ListCategory_Name">
                  <SelectParameters>
                    <asp:Parameter Name="Form_Id" Type="String" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListCategory_InsertLinkedCategoryListListCategoryChild" runat="server" SelectCommand="SELECT ListCategory_Id , ListCategory_Name FROM Administration_ListCategory WHERE Form_Id = @Form_Id AND ListCategory_Id NOT IN (@ListCategory_Id) ORDER BY ListCategory_Name">
                  <SelectParameters>
                    <asp:Parameter Name="Form_Id" Type="String" />
                    <asp:Parameter Name="ListCategory_Id" Type="Int32" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListCategory_EditFormId" runat="server" SelectCommand="SELECT DISTINCT Form_Id , Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')' AS Form_Name FROM Administration_Form ORDER BY Form_Name + ' (' + CASE WHEN Form_IsActive = 1 THEN 'Yes' WHEN Form_IsActive = 0 THEN 'No' END + ')'"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListCategory_EditParent" runat="server" SelectCommand="SELECT DISTINCT ListCategory_Id , ListCategory_Name + ' (' + CASE WHEN ListCategory_IsActive = 1 THEN 'Yes' WHEN ListCategory_IsActive = 0 THEN 'No' END + ')' AS ListCategory_Name FROM Administration_ListCategory WHERE Form_Id = @Form_Id ORDER BY ListCategory_Name">
                  <SelectParameters>
                    <asp:Parameter Name="Form_Id" Type="String" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryParent" runat="server" SelectCommand="SELECT ListCategory_Id , ListCategory_Name FROM Administration_ListCategory WHERE Form_Id = @Form_Id ORDER BY ListCategory_Name">
                  <SelectParameters>
                    <asp:Parameter Name="Form_Id" Type="String" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListCategory_EditLinkedCategoryListListCategoryChild" runat="server" SelectCommand="SELECT ListCategory_Id , ListCategory_Name FROM Administration_ListCategory WHERE Form_Id = @Form_Id AND ListCategory_Id NOT IN (@ListCategory_Id) ORDER BY ListCategory_Name">
                  <SelectParameters>
                    <asp:Parameter Name="Form_Id" Type="String" />
                    <asp:Parameter Name="ListCategory_Id" Type="Int32" />
                  </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_ListCategory_Form" runat="server" InsertCommand="INSERT INTO Administration_ListCategory (Form_Id ,ListCategory_Parent ,ListCategory_Name ,ListCategory_Description , ListCategory_LinkedCategory , ListCategory_LinkedCategory_List , ListCategory_LinkedCategory_List_ListCategory_Parent , ListCategory_LinkedCategory_List_ListCategory_Child , ListCategory_CreatedDate ,ListCategory_CreatedBy ,ListCategory_ModifiedDate ,ListCategory_ModifiedBy ,ListCategory_History ,ListCategory_IsActive) VALUES ( @Form_Id ,@ListCategory_Parent ,@ListCategory_Name , @ListCategory_Description , @ListCategory_LinkedCategory , @ListCategory_LinkedCategory_List , @ListCategory_LinkedCategory_List_ListCategory_Parent , @ListCategory_LinkedCategory_List_ListCategory_Child , @ListCategory_CreatedDate , @ListCategory_CreatedBy ,@ListCategory_ModifiedDate ,@ListCategory_ModifiedBy ,@ListCategory_History ,@ListCategory_IsActive); SELECT @ListCategory_Id = SCOPE_IDENTITY()" SelectCommand="SELECT * FROM Administration_ListCategory WHERE (ListCategory_Id = @ListCategory_Id)" UpdateCommand="UPDATE Administration_ListCategory SET Form_Id = @Form_Id ,ListCategory_Parent = @ListCategory_Parent ,ListCategory_Name = @ListCategory_Name , ListCategory_Description = @ListCategory_Description , ListCategory_LinkedCategory = @ListCategory_LinkedCategory , ListCategory_LinkedCategory_List = @ListCategory_LinkedCategory_List , ListCategory_LinkedCategory_List_ListCategory_Parent = @ListCategory_LinkedCategory_List_ListCategory_Parent , ListCategory_LinkedCategory_List_ListCategory_Child = @ListCategory_LinkedCategory_List_ListCategory_Child , ListCategory_ModifiedDate = @ListCategory_ModifiedDate ,ListCategory_ModifiedBy = @ListCategory_ModifiedBy ,ListCategory_History = @ListCategory_History ,ListCategory_IsActive = @ListCategory_IsActive WHERE ListCategory_Id = @ListCategory_Id" OnUpdated="SqlDataSource_ListCategory_Form_Updated" OnInserted="SqlDataSource_ListCategory_Form_Inserted">
                  <InsertParameters>
                    <asp:Parameter Direction="Output" Name="ListCategory_Id" Type="Int32" />
                    <asp:Parameter Name="Form_Id" Type="Int32" />
                    <asp:Parameter Name="ListCategory_Parent" Type="Int32" />
                    <asp:Parameter Name="ListCategory_Name" Type="String" />
                    <asp:Parameter Name="ListCategory_Description" Type="String" />
                    <asp:Parameter Name="ListCategory_LinkedCategory" Type="Boolean" />
                    <asp:Parameter Name="ListCategory_LinkedCategory_List" Type="String" />
                    <asp:Parameter Name="ListCategory_LinkedCategory_List_ListCategory_Parent" Type="Int32" />
                    <asp:Parameter Name="ListCategory_LinkedCategory_List_ListCategory_Child" Type="Int32" />
                    <asp:Parameter Name="ListCategory_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="ListCategory_CreatedBy" Type="String" />
                    <asp:Parameter Name="ListCategory_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="ListCategory_ModifiedBy" Type="String" />
                    <asp:Parameter Name="ListCategory_History" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Name="ListCategory_IsActive" Type="Boolean" />
                  </InsertParameters>
                  <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="ListCategory_Id" QueryStringField="ListCategory_Id" Type="Int32" />
                  </SelectParameters>
                  <UpdateParameters>
                    <asp:Parameter Name="Form_Id" Type="Int32" />
                    <asp:Parameter Name="ListCategory_Parent" Type="Int32" />
                    <asp:Parameter Name="ListCategory_Name" Type="String" />
                    <asp:Parameter Name="ListCategory_Description" Type="String" />
                    <asp:Parameter Name="ListCategory_LinkedCategory" Type="Boolean" />
                    <asp:Parameter Name="ListCategory_LinkedCategory_List" Type="String" />
                    <asp:Parameter Name="ListCategory_LinkedCategory_List_ListCategory_Parent" Type="Int32" />
                    <asp:Parameter Name="ListCategory_LinkedCategory_List_ListCategory_Child" Type="Int32" />
                    <asp:Parameter Name="ListCategory_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="ListCategory_ModifiedBy" Type="String" />
                    <asp:Parameter Name="ListCategory_History" Type="String" />
                    <asp:Parameter Name="ListCategory_IsActive" Type="Boolean" />
                    <asp:Parameter Name="ListCategory_Id" Type="Int32" />
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
