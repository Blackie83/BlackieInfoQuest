<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_SustainabilityManagement.aspx.cs" Inherits="InfoQuestForm.Form_SustainabilityManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Sustainability Management</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_SustainabilityManagement.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_SustainabilityManagement" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_SustainabilityManagement" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_SustainabilityManagement" AssociatedUpdatePanelID="UpdatePanel_SustainabilityManagement">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_SustainabilityManagement" runat="server">
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
          <table id="TableSustainabilityManagementInfo" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_SustainabilityManagementInfoHeading" runat="server" Text=""></asp:Label>
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
                      <strong>Facility:</strong>
                    </td>
                    <td>
                      <asp:Label ID="Label_SustainabilityManagementFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                      <strong>Month:</strong>
                    </td>
                    <td>
                      <asp:Label ID="Label_SustainabilityManagementMonth" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td>
                      <strong>FY Period:</strong>
                    </td>
                    <td>
                      <asp:Label ID="Label_SustainabilityManagementFYPeriod" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableLinks" style="width: 900px;" runat="server">
            <tr>
              <td style="text-align: center;">
                <asp:Button ID="Button_GoToList" runat="server" Text="Back To List" CssClass="Controls_Button" OnClick="Button_GoToList_Click" />
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableItem" style="width: 900px;" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridItem" runat="server" Text=""></asp:Label>
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
                      <asp:Label ID="Label_TotalRecordsItem" runat="server" Text=""></asp:Label>
                      <asp:HiddenField ID="HiddenField_TotalRecordsItem" runat="server" />
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_SustainabilityManagement_Item" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_SustainabilityManagement_Item" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_SustainabilityManagement_Item_PreRender" OnDataBound="GridView_SustainabilityManagement_Item_DataBound">
                        <HeaderStyle CssClass="GridView_HeaderStyle" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle" style="width: 100%">
                            <tr>
                              <td style="text-align:right;">
                                <asp:Button ID="Button_EditPrint" runat="server" Text="Print Statistics" CssClass="Controls_Button" OnClick="Button_EditPrint_Click" />&nbsp;
                                <asp:Button ID="Button_EditEmail" runat="server" Text="Email Link" CssClass="Controls_Button" OnClick="Button_EditEmail_Click" />&nbsp;
                                <asp:Button ID="Button_EditUpdate" runat="server" Text="Update Form" CssClass="Controls_Button" OnClick="Button_EditUpdate" />
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:right;">
                                <asp:Button ID="Button_EditGoToList" runat="server" Text="Back To List" CssClass="Controls_Button" OnClick="Button_EditGoToList_Click" />
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <RowStyle CssClass="GridView_RowStyle" />
                        <FooterStyle CssClass="GridView_FooterStyle" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Right" />
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td>No records
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>
                                <asp:Button ID="Button_EditGoToList" runat="server" Text="Back To List" CssClass="Controls_Button" OnClick="Button_EditGoToList_Click" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="Scope" HeaderText="" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="Scope" />
                          <asp:BoundField DataField="Item" HeaderText="" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="Item" />
                          <asp:TemplateField HeaderText="Prev 12 Month Avg" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" SortExpression="AVGValue">
                            <ItemTemplate>
                              <asp:Label ID="Label_EditAVGValue" runat="server" Width="100px" Text='<%# Bind("AVGValue","{0:#,##0.00}") %>'></asp:Label>&nbsp;
                              <asp:HiddenField ID="HiddenField_EditAVGPercentage" runat="server" Value='<%# Eval("AVGPercentage") %>'></asp:HiddenField>
                              <asp:HiddenField ID="HiddenField_EditAVGValueLow" runat="server" Value='<%# Eval("AVGValueLow","{0:###0.00}") %>'></asp:HiddenField>
                              <asp:HiddenField ID="HiddenField_EditAVGValueHigh" runat="server" Value='<%# Eval("AVGValueHigh","{0:###0.00}") %>'></asp:HiddenField>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" SortExpression="Value">
                            <ItemTemplate>
                              <asp:Label ID="Label_EditAbove" runat="server" Text='<%# "Above " + Eval("AVGPercentage") + " % of Avg" %>' CssClass="Controls_Error"></asp:Label>
                              <asp:Label ID="Label_EditBelow" runat="server" Text='<%# "Below " + Eval("AVGPercentage") + " % of Avg" %>' CssClass="Controls_Error"></asp:Label>
                              <asp:Label ID="Label_EditInvalidForm" runat="server" CssClass="Controls_Validation"></asp:Label>
                              <asp:TextBox ID="TextBox_EditValue" runat="server" Width="150px" Text='<%# Bind("Value","{0:###0.00}") %>' CssClass="Controls_TextBox_AlignRight"></asp:TextBox>
                              <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender_EditValue" runat="server" TargetControlID="TextBox_EditValue" FilterType="Custom,Numbers" ValidChars=".-">
                              </Ajax:FilteredTextBoxExtender>
                              <asp:Label ID="Label_EditValue" runat="server" Text='<%# Bind("Value","{0:#,##0.00}") %>'></asp:Label>
                              <asp:HiddenField ID="HiddenField_EditValue" runat="server" Value='<%# Eval("Value","{0:###0.00}") %>'></asp:HiddenField>
                              <asp:HiddenField ID="HiddenField_EditItemId" runat="server" Value='<%# Bind("SustainabilityManagement_Item_Id") %>' />
                              <asp:HiddenField ID="HiddenField_EditCapture" runat="server" Value='<%# Bind("Capture") %>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="Measurement" HeaderText="" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="Measurement" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_SustainabilityManagement_Item" runat="server" OnSelected="SqlDataSource_SustainabilityManagement_Item_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <table id="TableItemList" style="width: 900px;" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_GridItemList" runat="server" Text=""></asp:Label>
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
                      <asp:Label ID="Label_TotalRecordsItemList" runat="server" Text=""></asp:Label>
                      <asp:HiddenField ID="HiddenField_TotalRecordsItemList" runat="server" />
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_SustainabilityManagement_Item_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_SustainabilityManagement_Item_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="True" BorderWidth="0px" ShowFooter="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_SustainabilityManagement_Item_List_PreRender" OnDataBound="GridView_SustainabilityManagement_Item_List_DataBound">
                        <HeaderStyle CssClass="GridView_HeaderStyle" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="text-align:right;">
                                <asp:Button ID="Button_ItemPrint" runat="server" Text="Print Statistics" CssClass="Controls_Button" OnClick="Button_ItemPrint_Click" />&nbsp;
                                <asp:Button ID="Button_ItemEmail" runat="server" Text="Email Link" CssClass="Controls_Button" OnClick="Button_ItemEmail_Click" />
                              </td>
                            </tr>
                            <tr>
                              <td style="text-align:right;">
                                <asp:Button ID="Button_ItemGoToList" runat="server" Text="Back To List" CssClass="Controls_Button" OnClick="Button_ItemGoToList_Click" />
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <RowStyle CssClass="GridView_RowStyle" />
                        <FooterStyle CssClass="GridView_FooterStyle" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Right" />
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle">
                            <tr>
                              <td>No records
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td>
                                <asp:Button ID="Button_ItemGoToList" runat="server" Text="Back To List" CssClass="Controls_Button" OnClick="Button_ItemGoToList_Click" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="Scope" HeaderText="" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="Scope" />
                          <asp:BoundField DataField="Item" HeaderText="" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="Item" />
                          <asp:TemplateField HeaderText="Prev 12 Month Avg" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" SortExpression="AVGValue">
                            <ItemTemplate>                              
                              <asp:Label ID="Label_ItemAVGValue" runat="server" Width="100px" Text='<%# Bind("AVGValue","{0:#,##0.00}") %>'></asp:Label>&nbsp;
                              <asp:HiddenField ID="HiddenField_ItemAVGPercentage" runat="server" Value='<%# Eval("AVGPercentage") %>'></asp:HiddenField>
                              <asp:HiddenField ID="HiddenField_ItemAVGValueLow" runat="server" Value='<%# Eval("AVGValueLow","{0:###0.00}") %>'></asp:HiddenField>
                              <asp:HiddenField ID="HiddenField_ItemAVGValueHigh" runat="server" Value='<%# Eval("AVGValueHigh","{0:###0.00}") %>'></asp:HiddenField>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" SortExpression="Value">
                            <ItemTemplate>
                              <asp:Label ID="Label_ItemAbove" runat="server" Text='<%# "Above " + Eval("AVGPercentage") + "% of Avg" %>' CssClass="Controls_Error"></asp:Label>
                              <asp:Label ID="Label_ItemBelow" runat="server" Text='<%# "Below " + Eval("AVGPercentage") + "% of Avg" %>' CssClass="Controls_Error"></asp:Label>
                              <asp:Label ID="Label_ItemValue" runat="server" Width="100px" Text='<%# Bind("Value","{0:#,##0.00}") %>'></asp:Label>&nbsp;
                              <asp:HiddenField ID="HiddenField_ItemValue" runat="server" Value='<%# Bind("Value","{0:###0.00}") %>'></asp:HiddenField>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="Measurement" HeaderText="" HeaderStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="Measurement" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_SustainabilityManagement_Item_List" runat="server" OnSelected="SqlDataSource_SustainabilityManagement_Item_List_Selected"></asp:SqlDataSource>
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
