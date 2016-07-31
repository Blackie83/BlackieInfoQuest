<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_Pharmacy_Supplier_Lookup.aspx.cs" Inherits="InfoQuestForm.Form_Pharmacy_Supplier_Lookup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Pharmacy - Supplier Lookup</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_Pharmacy_Supplier_Lookup.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>  
</head>
<body>
  <form id="form_Pharmacy_Supplier_Lookup" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <navigation:navigationmenu id="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_Pharmacy_Supplier_Lookup" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_Pharmacy_Supplier_Lookup" AssociatedUpdatePanelID="UpdatePanel_Pharmacy_Supplier_Lookup">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_Pharmacy_Supplier_Lookup" runat="server">
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
                    <td>Search Supplier Lookup
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>Code
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Code" runat="server" DataSourceID="SqlDataSource_Pharmacy_Supplier_Lookup_Code" AppendDataBoundItems="true" DataTextField="Pharmacy_Supplier_Lookup_Code" DataValueField="Pharmacy_Supplier_Lookup_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Code</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Pharmacy_Supplier_Lookup_Code" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Description
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_Description" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource_Pharmacy_Supplier_Lookup_Description" DataTextField="Pharmacy_Supplier_Lookup_Description" DataValueField="Pharmacy_Supplier_Lookup_Id" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Description</asp:ListItem>
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_Pharmacy_Supplier_Lookup_Description" runat="server"></asp:SqlDataSource>
                    </td>
                  </tr>
                  <tr>
                    <td>Is Active
                    </td>
                    <td>
                      <asp:DropDownList ID="DropDownList_IsActive" runat="server" CssClass="Controls_DropDownList">
                        <asp:ListItem Value="">Select Value</asp:ListItem>
                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                        <asp:ListItem Value="No">No</asp:ListItem>
                      </asp:DropDownList>
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
                      <asp:Button ID="Button_Clear" runat="server" Text="Clear" CssClass="Controls_Button" OnClick="Button_Clear_Click" />&nbsp;
                    <asp:Button ID="Button_Search" runat="server" Text="Search" CssClass="Controls_Button" OnClick="Button_Search_Click" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table>
            <tr>
              <td style="width: 480px; vertical-align: top;">
                <table class="Table">
                  <tr>
                    <td>
                      <table class="Table_Header">
                        <tr>
                          <td>List of Supplier Lookup
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
                            <asp:GridView ID="GridView_Pharmacy_Supplier_Lookup_List" Width="480px" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_Pharmacy_Supplier_Lookup_List" CssClass="GridView" AllowPaging="True" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" PageSize="20" OnPreRender="GridView_Pharmacy_Supplier_Lookup_List_PreRender" OnDataBound="GridView_Pharmacy_Supplier_Lookup_List_DataBound" OnRowCreated="GridView_Pharmacy_Supplier_Lookup_List_RowCreated">
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
                                    <%=GridView_Pharmacy_Supplier_Lookup_List.PageCount%>
                                    </td>
                                    <td>
                                      <asp:ImageButton ID="ImageButton_Next" runat="server" CommandName="Page" CommandArgument="Next" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Next.gif" />
                                      <asp:ImageButton ID="ImageButton_Last" runat="server" CommandName="Page" CommandArgument="Last" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/Last.gif" />
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
                                <asp:TemplateField HeaderText="">
                                  <ItemTemplate>
                                    <asp:HyperLink ID="Link" Text='<%# GetLink(Eval("Pharmacy_Supplier_Lookup_Id")) %>' runat="server"></asp:HyperLink>
                                  </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Pharmacy_Supplier_Lookup_Code" HeaderText="Code" ReadOnly="True" SortExpression="Pharmacy_Supplier_Lookup_Code" />
                                <asp:BoundField DataField="Pharmacy_Supplier_Lookup_Description" HeaderText="Description" ReadOnly="True" SortExpression="Pharmacy_Supplier_Lookup_Description" />
                                <asp:BoundField DataField="Pharmacy_Supplier_Lookup_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="Pharmacy_Supplier_Lookup_IsActive" />
                              </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource_Pharmacy_Supplier_Lookup_List" runat="server" OnSelected="SqlDataSource_Pharmacy_Supplier_Lookup_List_Selected"></asp:SqlDataSource>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="width: 40px;">&nbsp;
              </td>
              <td style="width: 480px; vertical-align: top;">
                <table class="Table">
                  <tr>
                    <td>
                      <table class="Table_Header">
                        <tr>
                          <td>Supplier Lookup
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:FormView ID="FormView_Pharmacy_Supplier_Lookup_Form" runat="server" DataKeyNames="Pharmacy_Supplier_Lookup_Id" CssClass="FormView" DataSourceID="SqlDataSource_Pharmacy_Supplier_Lookup_Form" OnItemInserting="FormView_Pharmacy_Supplier_Lookup_Form_ItemInserting" DefaultMode="Insert" OnItemCommand="FormView_Pharmacy_Supplier_Lookup_Form_ItemCommand" OnItemUpdating="FormView_Pharmacy_Supplier_Lookup_Form_ItemUpdating">
                        <InsertItemTemplate>
                          <table class="FormView_TableBody">
                            <tr>
                              <td colspan="2">
                                <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td style="width: 100px;">Id
                              </td>
                              <td>
                                <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                                &nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td style="width: 100px;" class="DetailsViewControlsValidation" id="FormCode">Code
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertCode" runat="server" Width="340px" Text='<%# Bind("Pharmacy_Supplier_Lookup_Code") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertCode_TextChanged"></asp:TextBox>
                                <asp:Label ID="Label_InsertCodeError" runat="server"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td style="width: 100px;" class="DetailsViewControlsValidation" id="FormDescription">Description
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_InsertDescription" runat="server" Width="340px" Text='<%# Bind("Pharmacy_Supplier_Lookup_Description") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertDescription_TextChanged"></asp:TextBox>
                                <asp:Label ID="Label_InsertDescriptionError" runat="server"></asp:Label>
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
                                <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("Pharmacy_Supplier_Lookup_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td>Created By
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("Pharmacy_Supplier_Lookup_CreatedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td>Modified Date
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("Pharmacy_Supplier_Lookup_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td>Modified By
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("Pharmacy_Supplier_Lookup_ModifiedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td>Is Active
                              </td>
                              <td>
                                <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("Pharmacy_Supplier_Lookup_IsActive") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr class="FormView_TableFooter">
                              <td colspan="2">
                                <asp:Button ID="Button_InsertClear" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                              <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add Supplier" CssClass="Controls_Button" />&nbsp;
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
                              <td style="width: 100px;">Id
                              </td>
                              <td>
                                <asp:Label ID="Label_EditId" runat="server" Text='<%# Bind("Pharmacy_Supplier_Lookup_Id") %>'></asp:Label>
                                <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                                &nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td style="width: 100px;" class="DetailsViewControlsValidation" id="FormCode">Code
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_EditCode" runat="server" Width="340px" Text='<%# Bind("Pharmacy_Supplier_Lookup_Code") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditCode_TextChanged"></asp:TextBox>
                                <asp:Label ID="Label_EditCodeError" runat="server"></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td style="width: 100px;" class="DetailsViewControlsValidation" id="FormDescription">Description
                              </td>
                              <td>
                                <asp:TextBox ID="TextBox_EditDescription" runat="server" Width="340px" Text='<%# Bind("Pharmacy_Supplier_Lookup_Description") %>' CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditDescription_TextChanged"></asp:TextBox>
                                <asp:Label ID="Label_EditDescriptionError" runat="server"></asp:Label>&nbsp;
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
                                <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("Pharmacy_Supplier_Lookup_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td>Created By
                              </td>
                              <td>
                                <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("Pharmacy_Supplier_Lookup_CreatedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td>Modified Date
                              </td>
                              <td>
                                <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("Pharmacy_Supplier_Lookup_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td>Modified By
                              </td>
                              <td>
                                <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("Pharmacy_Supplier_Lookup_ModifiedBy") %>'></asp:Label>&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td>Is Active
                              </td>
                              <td>
                                <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("Pharmacy_Supplier_Lookup_IsActive") %>' />&nbsp;
                              </td>
                            </tr>
                            <tr class="FormView_TableFooter">
                              <td colspan="2" style="text-align: right;">
                                <asp:Button ID="Button_EditClear" runat="server" CausesValidation="False" CommandName="Cancel" Text="Clear" CssClass="Controls_Button" />&nbsp;
                              <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="False" CommandName="Update" Text="Update Supplier" CssClass="Controls_Button" OnClick="Button_EditUpdate_Click" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EditItemTemplate>
                      </asp:FormView>
                      <asp:SqlDataSource ID="SqlDataSource_Pharmacy_Supplier_Lookup_Form" runat="server" OnUpdated="SqlDataSource_Pharmacy_Supplier_Lookup_Form_Updated" OnInserted="SqlDataSource_Pharmacy_Supplier_Lookup_Form_Inserted"></asp:SqlDataSource>
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
