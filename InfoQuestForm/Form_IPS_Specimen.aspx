<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_IPS_Specimen.aspx.cs" Inherits="InfoQuestForm.Form_IPS_Specimen" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Infection Prevention Surveillance - Specimen</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_IPS_Specimen.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body>
  <form id="form_IPS_Specimen" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_IPS_Specimen" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_IPS_Specimen" AssociatedUpdatePanelID="UpdatePanel_IPS_Specimen">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_IPS_Specimen" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td>
                <asp:ImageButton runat="server" ID="ImageButton_Logo" ImageUrl="App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg" AlternateText="" BorderWidth="0px" Height="75px" CausesValidation="false" CssClass="Controls_ImageButton_NoHand" />
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
          <table id="TableInfo" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_InfoHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="width: 90px">Facility:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IFacility" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Visit Number:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IVisitNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Surname, Name:
                    </td>
                    <td style="width: 150px">
                      <asp:Label ID="Label_IName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 90px">Report Number:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IInfectionReportNumber" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Category:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IInfectionCategoryName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Type:
                    </td>
                    <td style="width: 150px">
                      <asp:Label ID="Label_IInfectionTypeName" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 90px">Infection:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_IInfectionCompleted" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">Specimen:
                    </td>
                    <td style="width: 140px">
                      <asp:Label ID="Label_ISpecimen" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 90px">HAI Investigation:
                    </td>
                    <td style="width: 150px">
                      <asp:Label ID="Label_IHAI" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Footer">
                  <tr>
                    <td style="text-align: center;">
                      <asp:Button ID="Button_InfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_InfectionHome_OnClick" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div style="height: 40px; width: 900px; text-align: center;">
            &nbsp;
          </div>
          <table id="TableSpecimen" class="Table" style="width: 905px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_SpecimenHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenSpecimenTotalRecords" runat="server" Text="" Visible="false" />
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #ffcc66; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_IPS_Specimen_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_Specimen_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="True" BorderWidth="0px" ShowFooter="False" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_Specimen_List_PreRender" OnRowCreated="GridView_IPS_Specimen_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_SpecimenTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px;">
                                <asp:Button ID="Button_CaptureNewSpecimen" runat="server" Text="Capture New Specimen" CssClass="Controls_Button" OnClick="Button_CaptureNewSpecimen_OnClick" />
                                <asp:Button ID="Button_SpecimenAll" runat="server" Text="View All Captured Specimens" CssClass="Controls_Button" OnClick="Button_SpecimenAll_Click" />
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
                              <td colspan="2">No Specimen Captured
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_SpecimenTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_CaptureNewSpecimen" runat="server" Text="Capture New Specimen" CssClass="Controls_Button" OnClick="Button_CaptureNewSpecimen_OnClick" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="" ItemStyle-Width="50px">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetSpecimenLink(Eval("IPS_Specimen_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="IPS_Specimen_Status_Name" HeaderText="Status" ReadOnly="True" SortExpression="IPS_Specimen_Status_Name" />
                          <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                              <asp:Label ID="Label_SpecimenDate" runat="server" Text='<%# Bind("IPS_Specimen_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="IPS_Specimen_Time" HeaderText="Time" ReadOnly="True" SortExpression="IPS_Specimen_Time" />
                          <asp:BoundField DataField="IPS_Specimen_Type_Name" HeaderText="Type" ReadOnly="True" SortExpression="IPS_Specimen_Type_Name" />
                          <asp:BoundField DataField="IPS_Specimen_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="IPS_Specimen_IsActive" ItemStyle-Width="60px" />
                          <asp:BoundField DataField="Specimen" HeaderText="Specimen" ReadOnly="True" SortExpression="Specimen" ItemStyle-Width="120px" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_Specimen_List" runat="server" OnSelected="SqlDataSource_IPS_Specimen_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #ffcc66; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <a id="CurrentSpecimen"></a>
          <div>
            &nbsp;
          </div>
          <asp:LinkButton ID="LinkButton_CurrentSpecimen" runat="server"></asp:LinkButton>
          <table id="TableCurrentSpecimen" class="Table" style="width: 905px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_CurrentSpecimenHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #ffcc66; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_IPS_Specimen_Form" runat="server" DataKeyNames="IPS_Specimen_Id" CssClass="FormView" DataSourceID="SqlDataSource_IPS_Specimen_Form" OnItemInserting="FormView_IPS_Specimen_Form_ItemInserting" DefaultMode="Insert" OnDataBound="FormView_IPS_Specimen_Form_DataBound" OnItemUpdating="FormView_IPS_Specimen_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormSpecimenStatusList">Status
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertSpecimenStatusList" runat="server" DataSourceID="SqlDataSource_IPS_InsertSpecimenStatusList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Specimen_Status_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Status</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormSpecimenDate">Specimen Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertSpecimenDate" runat="server" Width="75px" Text='<%# Bind("IPS_Specimen_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertSpecimenDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertSpecimenDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertSpecimenDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertSpecimenDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertSpecimenDate" runat="server" TargetControlID="TextBox_InsertSpecimenDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormSpecimenTime">Specimen Time<br />
                          (Hour: 00-23) (Minute: 00-59)
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertSpecimenTimeHours" runat="server" SelectedValue='<%# Bind("IPS_Specimen_TimeHours") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Hour</asp:ListItem>
                            <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;:&nbsp;
                          <asp:DropDownList ID="DropDownList_InsertSpecimenTimeMinutes" runat="server" SelectedValue='<%# Bind("IPS_Specimen_TimeMinutes") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Minute</asp:ListItem>
                            <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                            <asp:ListItem Value="24">24</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="26">26</asp:ListItem>
                            <asp:ListItem Value="27">27</asp:ListItem>
                            <asp:ListItem Value="28">28</asp:ListItem>
                            <asp:ListItem Value="29">29</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="31">31</asp:ListItem>
                            <asp:ListItem Value="32">32</asp:ListItem>
                            <asp:ListItem Value="33">33</asp:ListItem>
                            <asp:ListItem Value="34">34</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="36">36</asp:ListItem>
                            <asp:ListItem Value="37">37</asp:ListItem>
                            <asp:ListItem Value="38">38</asp:ListItem>
                            <asp:ListItem Value="39">39</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="41">41</asp:ListItem>
                            <asp:ListItem Value="42">42</asp:ListItem>
                            <asp:ListItem Value="43">43</asp:ListItem>
                            <asp:ListItem Value="44">44</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="46">46</asp:ListItem>
                            <asp:ListItem Value="47">47</asp:ListItem>
                            <asp:ListItem Value="48">48</asp:ListItem>
                            <asp:ListItem Value="49">49</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="51">51</asp:ListItem>
                            <asp:ListItem Value="52">52</asp:ListItem>
                            <asp:ListItem Value="53">53</asp:ListItem>
                            <asp:ListItem Value="54">54</asp:ListItem>
                            <asp:ListItem Value="55">55</asp:ListItem>
                            <asp:ListItem Value="56">56</asp:ListItem>
                            <asp:ListItem Value="57">57</asp:ListItem>
                            <asp:ListItem Value="58">58</asp:ListItem>
                            <asp:ListItem Value="59">59</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormSpecimenTypeList">Type
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertSpecimenTypeList" runat="server" DataSourceID="SqlDataSource_IPS_InsertSpecimenTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Specimen_Type_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Specimen taken from which bed
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertSpecimenBedHistory" runat="server" DataSourceID="SqlDataSource_IPS_InsertSpecimenBedHistory" AppendDataBoundItems="true" DataTextField="BedHistory" DataValueField="IPS_BedHistory_Id" SelectedValue='<%# Bind("IPS_Specimen_BedHistory") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Bed History</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("IPS_Specimen_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Created Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("IPS_Specimen_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Created By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("IPS_Specimen_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Modified Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("IPS_Specimen_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Modified By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("IPS_Specimen_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_InsertInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_InsertSpecimenInfectionHome_OnClick" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Specimen" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormSpecimenStatusList">Status
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditSpecimenStatusList" runat="server" DataSourceID="SqlDataSource_IPS_EditSpecimenStatusList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Specimen_Status_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Status</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormSpecimenDate">Specimen Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditSpecimenDate" runat="server" Width="75px" Text='<%# Bind("IPS_Specimen_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_EditSpecimenDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_EditSpecimenDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_EditSpecimenDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_EditSpecimenDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditSpecimenDate" runat="server" TargetControlID="TextBox_EditSpecimenDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormSpecimenTime">Specimen Time<br />
                          (Hour: 00-23) (Minute: 00-59)
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditSpecimenTimeHours" runat="server" SelectedValue='<%# Bind("IPS_Specimen_TimeHours") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Hour</asp:ListItem>
                            <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;:&nbsp;
                          <asp:DropDownList ID="DropDownList_EditSpecimenTimeMinutes" runat="server" SelectedValue='<%# Bind("IPS_Specimen_TimeMinutes") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Minute</asp:ListItem>
                            <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                            <asp:ListItem Value="24">24</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="26">26</asp:ListItem>
                            <asp:ListItem Value="27">27</asp:ListItem>
                            <asp:ListItem Value="28">28</asp:ListItem>
                            <asp:ListItem Value="29">29</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="31">31</asp:ListItem>
                            <asp:ListItem Value="32">32</asp:ListItem>
                            <asp:ListItem Value="33">33</asp:ListItem>
                            <asp:ListItem Value="34">34</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="36">36</asp:ListItem>
                            <asp:ListItem Value="37">37</asp:ListItem>
                            <asp:ListItem Value="38">38</asp:ListItem>
                            <asp:ListItem Value="39">39</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="41">41</asp:ListItem>
                            <asp:ListItem Value="42">42</asp:ListItem>
                            <asp:ListItem Value="43">43</asp:ListItem>
                            <asp:ListItem Value="44">44</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="46">46</asp:ListItem>
                            <asp:ListItem Value="47">47</asp:ListItem>
                            <asp:ListItem Value="48">48</asp:ListItem>
                            <asp:ListItem Value="49">49</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="51">51</asp:ListItem>
                            <asp:ListItem Value="52">52</asp:ListItem>
                            <asp:ListItem Value="53">53</asp:ListItem>
                            <asp:ListItem Value="54">54</asp:ListItem>
                            <asp:ListItem Value="55">55</asp:ListItem>
                            <asp:ListItem Value="56">56</asp:ListItem>
                            <asp:ListItem Value="57">57</asp:ListItem>
                            <asp:ListItem Value="58">58</asp:ListItem>
                            <asp:ListItem Value="59">59</asp:ListItem>
                          </asp:DropDownList>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormSpecimenTypeList">Type
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditSpecimenTypeList" runat="server" DataSourceID="SqlDataSource_IPS_EditSpecimenTypeList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Specimen_Type_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Type</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Specimen taken from which bed
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditSpecimenBedHistory" runat="server" DataSourceID="SqlDataSource_IPS_EditSpecimenBedHistory" AppendDataBoundItems="true" DataTextField="BedHistory" DataValueField="IPS_BedHistory_Id" SelectedValue='<%# Bind("IPS_Specimen_BedHistory") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Bed History</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("IPS_Specimen_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Created Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("IPS_Specimen_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Created By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("IPS_Specimen_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Modified Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("IPS_Specimen_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Modified By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("IPS_Specimen_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_EditInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_EditSpecimenInfectionHome_OnClick" />&nbsp;
                          <asp:Button ID="Button_EditNew" runat="server" CausesValidation="False" Text="Capture New Specimen" CssClass="Controls_Button" OnClick="Button_EditSpecimenNew_OnClick" />&nbsp;                          
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Specimen" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4"></td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Status
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemSpecimenStatusList" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Specimen Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemSpecimenDate" runat="server" Text='<%# Bind("IPS_Specimen_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Specimen Time<br />
                          (Hour: 00-23) (Minute: 00-59)
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemSpecimenTimeHours" runat="server" Text='<%# Bind("IPS_Specimen_TimeHours") %>'></asp:Label>&nbsp;:&nbsp;<asp:Label ID="Label_ItemSpecimenTimeMinutes" runat="server" Text='<%# Bind("IPS_Specimen_TimeMinutes") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Type
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemSpecimenTypeList" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Specimen taken from which bed
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemSpecimenBedHistory" runat="server"></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("IPS_Specimen_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Created Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("IPS_Specimen_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Created By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("IPS_Specimen_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Modified Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("IPS_Specimen_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Modified By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("IPS_Specimen_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_ItemInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_ItemSpecimenInfectionHome_OnClick" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertSpecimenStatusList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertSpecimenTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertSpecimenBedHistory" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditSpecimenStatusList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditSpecimenTypeList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditSpecimenBedHistory" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_Specimen_Form" runat="server" OnInserted="SqlDataSource_IPS_Specimen_Form_Inserted" OnUpdated="SqlDataSource_IPS_Specimen_Form_Updated"></asp:SqlDataSource>
              </td>
              <td style="background-color: #ffcc66; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div id="DivSpecimenResult" runat="server" style="height: 40px; width: 900px; text-align: center;">
            &nbsp;<hr style="height: 5px; width: 80%; background-color: #b0262e; border: none;" />
          </div>
          <table id="TableSpecimenResult" class="Table" style="width: 905px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_SpecimenResultHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenSpecimenResultTotalRecords" runat="server" Text="" Visible="false" />
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #68c0ff; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_IPS_SpecimenResult_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_SpecimenResult_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_SpecimenResult_List_PreRender" OnRowCreated="GridView_IPS_SpecimenResult_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_SpecimenResultTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px;">
                                <asp:Button ID="Button_CaptureSpecimenResult" runat="server" Text="Capture New Laboratory Result" CssClass="Controls_Button" OnClick="Button_CaptureSpecimenResult_OnClick" />
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
                              <td colspan="2">No Laboratory Result Captured
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_SpecimenResultTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_CaptureSpecimenResult" runat="server" Text="Capture New Laboratory Result" CssClass="Controls_Button" OnClick="Button_CaptureSpecimenResult_OnClick" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="" ItemStyle-Width="50px">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetSpecimenResultLink(Eval("IPS_SpecimenResult_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="IPS_SpecimenResult_LabNumber" HeaderText="Laboratory Number" ReadOnly="True" SortExpression="IPS_SpecimenResult_LabNumber" />
                          <asp:BoundField DataField="IPS_SpecimenResult_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="IPS_SpecimenResult_IsActive" ItemStyle-Width="60px" />
                          <asp:BoundField DataField="Organism" HeaderText="Organism" ReadOnly="True" SortExpression="Organism" ItemStyle-Width="120px" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_SpecimenResult_List" runat="server" OnSelected="SqlDataSource_IPS_SpecimenResult_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #68c0ff; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <a id="CurrentSpecimenResult"></a>
          <div id="DivCurrentSpecimenResult" runat="server">
            &nbsp;
          </div>
          <asp:LinkButton ID="LinkButton_CurrentSpecimenResult" runat="server"></asp:LinkButton>
          <table id="TableCurrentSpecimenResult" class="Table" style="width: 905px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_CurrentSpecimenResultHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #68c0ff; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_IPS_SpecimenResult_Form" runat="server" DataKeyNames="IPS_SpecimenResult_Id" CssClass="FormView" DataSourceID="SqlDataSource_IPS_SpecimenResult_Form" OnItemInserting="FormView_IPS_SpecimenResult_Form_ItemInserting" DefaultMode="Insert" OnItemUpdating="FormView_IPS_SpecimenResult_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormSpecimenResultLabNumber">Laboratory Number
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertSpecimenResultLabNumber" runat="server" Width="500px" Text='<%# Bind("IPS_SpecimenResult_LabNumber") %>' CssClass="Controls_TextBox" AutoPostBack="False" OnTextChanged="TextBox_InsertSpecimenResultLabNumber_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_InsertSpecimenResultLabNumberError" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("IPS_SpecimenResult_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Created Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("IPS_SpecimenResult_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Created By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("IPS_SpecimenResult_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Modified Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("IPS_SpecimenResult_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Modified By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("IPS_SpecimenResult_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_InsertInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_InsertSpecimenResultInfectionHome_OnClick" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Laboratory Result" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormSpecimenResultLabNumber">Laboratory Number
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditSpecimenResultLabNumber" runat="server" Width="500px" Text='<%# Bind("IPS_SpecimenResult_LabNumber") %>' CssClass="Controls_TextBox" AutoPostBack="False" OnTextChanged="TextBox_EditSpecimenResultLabNumber_TextChanged"></asp:TextBox>
                          <asp:Label ID="Label_EditSpecimenResultLabNumberError" runat="server"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("IPS_SpecimenResult_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Created Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("IPS_SpecimenResult_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Created By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("IPS_SpecimenResult_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Modified Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("IPS_SpecimenResult_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Modified By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("IPS_SpecimenResult_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_EditInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_EditSpecimenResultInfectionHome_OnClick" />&nbsp;
                          <asp:Button ID="Button_EditNewSpecimen" runat="server" CausesValidation="False" Text="Capture New Specimen" CssClass="Controls_Button" OnClick="Button_EditSpecimenResultNewSpecimen_OnClick" />&nbsp;
                          <asp:Button ID="Button_EditNew" runat="server" CausesValidation="False" Text="Capture New Laboratory Result" CssClass="Controls_Button" OnClick="Button_EditSpecimenResultNew_OnClick" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Laboratory Result" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4"></td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Laboratory Number
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemSpecimenResultLabNumber" runat="server" Text='<%# Bind("IPS_SpecimenResult_LabNumber") %>'></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("IPS_SpecimenResult_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Created Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("IPS_SpecimenResult_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Created By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("IPS_SpecimenResult_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Modified Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("IPS_SpecimenResult_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Modified By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("IPS_SpecimenResult_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_ItemInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_ItemSpecimenResultInfectionHome_OnClick" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_IPS_SpecimenResult_Form" runat="server" OnInserted="SqlDataSource_IPS_SpecimenResult_Form_Inserted" OnUpdated="SqlDataSource_IPS_SpecimenResult_Form_Updated"></asp:SqlDataSource>
              </td>
              <td style="background-color: #68c0ff; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div id="DivOrganism" runat="server" style="height: 40px; width: 900px; text-align: center;">
            &nbsp;<hr style="height: 5px; width: 80%; background-color: #b0262e; border: none;" />
          </div>
          <table id="TableOrganism" style="width: 905px;" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_OrganismHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenOrganismTotalRecords" runat="server" Text="" Visible="false" />
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_IPS_Organism_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_Organism_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_Organism_List_PreRender" OnRowCreated="GridView_IPS_Organism_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_OrganismTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px;">
                                <asp:Button ID="Button_CaptureOrganism" runat="server" Text="Capture New Organism" CssClass="Controls_Button" OnClick="Button_CaptureOrganism_OnClick" />
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
                              <td colspan="2">No Organism Captured
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_OrganismTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_CaptureOrganism" runat="server" Text="Capture New Organism" CssClass="Controls_Button" OnClick="Button_CaptureOrganism_OnClick" />
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="" ItemStyle-Width="50px">
                            <ItemTemplate>
                              <asp:HyperLink ID="Link" Text='<%# GetOrganismLink(Eval("IPS_Organism_Id")) %>' runat="server"></asp:HyperLink>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="IPS_Organism_Lookup_Code" HeaderText="Code" ReadOnly="True" SortExpression="IPS_Organism_Lookup_Code" />
                          <asp:BoundField DataField="IPS_Organism_Lookup_Description" HeaderText="Description" ReadOnly="True" SortExpression="IPS_Organism_Lookup_Description" />
                          <asp:BoundField DataField="IPS_Organism_AntibiogramNotRequired" HeaderText="Antibiogram Not Required" ReadOnly="True" SortExpression="IPS_Organism_AntibiogramNotRequired" />
                          <asp:BoundField DataField="IPS_Organism_Lookup_Resistance" HeaderText="Resistance" ReadOnly="True" SortExpression="IPS_Organism_Lookup_Resistance" />
                          <asp:BoundField DataField="IPS_Organism_Resistance_Name" HeaderText="Resistance Name" ReadOnly="True" SortExpression="IPS_Organism_Resistance_Name" />
                          <asp:BoundField DataField="IPS_Organism_Lookup_Notifiable" HeaderText="Notifiable" ReadOnly="True" SortExpression="IPS_Organism_Lookup_Notifiable" />
                          <asp:BoundField DataField="IPS_Organism_Notifiable_DepartmentOfHealth" HeaderText="DOH" ReadOnly="True" SortExpression="IPS_Organism_Notifiable_DepartmentOfHealth" />
                          <asp:BoundField DataField="IPS_Organism_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="IPS_Organism_IsActive" ItemStyle-Width="60px" />
                          <asp:BoundField DataField="Antibiogram" HeaderText="Antibiogram" ReadOnly="True" SortExpression="Antibiogram" ItemStyle-Width="120px" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_Organism_List" runat="server" OnSelected="SqlDataSource_IPS_Organism_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <a id="CurrentOrganism"></a>
          <div id="DivCurrentOrganism" runat="server">
            &nbsp;
          </div>
          <asp:LinkButton ID="LinkButton_CurrentOrganism" runat="server"></asp:LinkButton>
          <table id="TableCurrentOrganism" class="Table" style="width: 905px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_CurrentOrganismHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <asp:FormView ID="FormView_IPS_Organism_Form" runat="server" DataKeyNames="IPS_Organism_Id" CssClass="FormView" DataSourceID="SqlDataSource_IPS_Organism_Form" OnItemInserting="FormView_IPS_Organism_Form_ItemInserting" DefaultMode="Insert" OnDataBound="FormView_IPS_Organism_Form_DataBound" OnItemUpdating="FormView_IPS_Organism_Form_ItemUpdating">
                  <InsertItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_InsertInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_InsertConcurrencyInsertMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormOrganismName">Organism
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertOrganismNameLookup" runat="server" Width="100px" Text="" CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_InsertOrganismNameLookup_TextChanged"></asp:TextBox>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertOrganismNameLookup" runat="server" TargetControlID="TextBox_InsertOrganismNameLookup" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;&nbsp;or&nbsp;&nbsp;
                          <asp:DropDownList ID="DropDownList_InsertOrganismNameLookup" runat="server" DataSourceID="SqlDataSource_IPS_InsertOrganismNameLookup" AppendDataBoundItems="true" DataTextField="IPS_Organism_Lookup" DataValueField="IPS_Organism_Lookup_Id" SelectedValue='<%# Bind("IPS_Organism_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_InsertOrganismNameLookup_SelectedIndexChanged">
                            <asp:ListItem Value="">Select Organism</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_Insert" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Antibiogram Not Required</td>
                        <td style="width: 720px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertAntibiogramNotRequired" runat="server" Checked='<%# Bind("IPS_Organism_AntibiogramNotRequired") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideResistance" runat="server" visible="false">
                        <td style="width: 180px;" id="FormResistance">Resistance</td>
                        <td style="width: 720px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_InsertOrganismResistanceList" runat="server" DataSourceID="SqlDataSource_IPS_InsertOrganismResistanceList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Organism_Resistance_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Resistance</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormResistanceMechanism">Resistance Mechanism
                        </td>
                        <td style="width: 720px; padding: 0px; border-left-width: 0px; border-top-width: 1px; border-bottom-width: 0px;" colspan="3">
                          <div id="InsertRMMechanismItemList" style="max-height: 250px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                            <asp:CheckBoxList ID="CheckBoxList_InsertRMMechanismItemList" runat="server" AppendDataBoundItems="true" CssClass="Controls_CheckBoxListWithScrollbars" DataSourceID="SqlDataSource_IPS_InsertOrganismResistanceMechanismItemList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="3" RepeatLayout="Table" Width="690px">
                              <asp:ListItem Value="5018">None</asp:ListItem>
                            </asp:CheckBoxList>
                          </div>
                          <asp:HiddenField ID="HiddenField_InsertRMMechanismItemListTotal" runat="server" OnDataBinding="HiddenField_InsertRMMechanismItemListTotal_DataBinding" />
                        </td>
                      </tr>
                      <tr id="ShowHideDOH1" runat="server" visible="false">
                        <td style="width: 180px;" id="FormDOH1">Department of Health
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_InsertOrganismNotifiableDepartmentOfHealth" runat="server" Checked='<%# Bind("IPS_Organism_Notifiable_DepartmentOfHealth") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideDOH2" runat="server" visible="false">
                        <td style="width: 180px;" id="FormDOH2">Department of Health Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertOrganismNotifiableDepartmentOfHealthDate" runat="server" Width="75px" Text='<%# Bind("IPS_Organism_Notifiable_DepartmentOfHealth_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                          <asp:ImageButton runat="Server" ID="ImageButton_InsertOrganismNotifiableDepartmentOfHealthDate" ImageUrl="App_Themes/LifeHealthcare/Images/Blue/DatePicker.gif" AlternateText="Click here to display calendar" />&nbsp;
                          <Ajax:CalendarExtender ID="CalendarExtender_InsertOrganismNotifiableDepartmentOfHealthDate" runat="server" CssClass="Calendar" TargetControlID="TextBox_InsertOrganismNotifiableDepartmentOfHealthDate" Format="yyyy/MM/dd" PopupButtonID="ImageButton_InsertOrganismNotifiableDepartmentOfHealthDate">
                          </Ajax:CalendarExtender>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertOrganismNotifiableDepartmentOfHealthDate" runat="server" TargetControlID="TextBox_InsertOrganismNotifiableDepartmentOfHealthDate" WatermarkText="yyyy/mm/dd" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                        </td>
                      </tr>
                      <tr id="ShowHideDOH3" runat="server" visible="false">
                        <td style="width: 180px;" id="FormDOH3">Department of Health Reference Number
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_InsertOrganismNotifiableDepartmentOfHealthReferenceNumber" runat="server" Width="500px" Text='<%# Bind("IPS_Organism_Notifiable_DepartmentOfHealth_ReferenceNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_InsertIsActive" runat="server" Text='<%# Bind("IPS_Organism_IsActive") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Created Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_InsertCreatedDate" runat="server" Text='<%# Bind("IPS_Organism_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Created By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_InsertCreatedBy" runat="server" Text='<%# Bind("IPS_Organism_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Modified Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_InsertModifiedDate" runat="server" Text='<%# Bind("IPS_Organism_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Modified By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_InsertModifiedBy" runat="server" Text='<%# Bind("IPS_Organism_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_InsertInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_InsertOrganismInfectionHome_OnClick" />&nbsp;
                          <asp:Button ID="Button_InsertAdd" runat="server" CausesValidation="True" CommandName="Insert" Text="Add Organism" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </InsertItemTemplate>
                  <EditItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4">
                          <asp:Label ID="Label_EditInvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                          <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormOrganismName">Organism
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditOrganismNameLookup" runat="server" Width="100px" Text="" CssClass="Controls_TextBox" AutoPostBack="True" OnTextChanged="TextBox_EditOrganismNameLookup_TextChanged"></asp:TextBox>
                          <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditOrganismNameLookup" runat="server" TargetControlID="TextBox_EditOrganismNameLookup" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                          </Ajax:TextBoxWatermarkExtender>
                          &nbsp;&nbsp;or&nbsp;&nbsp;
                          <asp:DropDownList ID="DropDownList_EditOrganismNameLookup" runat="server" DataSourceID="SqlDataSource_IPS_EditOrganismNameLookup" AppendDataBoundItems="true" DataTextField="IPS_Organism_Lookup" DataValueField="IPS_Organism_Lookup_Id" SelectedValue='<%# Bind("IPS_Organism_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_EditOrganismNameLookup_SelectedIndexChanged" OnDataBound="DropDownList_EditOrganismNameLookup_DataBound">
                            <asp:ListItem Value="">Select Organism</asp:ListItem>
                          </asp:DropDownList>
                          <asp:HiddenField ID="HiddenField_Edit" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Antibiogram Not Required</td>
                        <td style="width: 720px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditAntibiogramNotRequired" runat="server" Checked='<%# Bind("IPS_Organism_AntibiogramNotRequired") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideResistance" runat="server" visible="false">
                        <td style="width: 180px;" id="FormResistance">Resistance</td>
                        <td style="width: 720px;" colspan="3">
                          <asp:DropDownList ID="DropDownList_EditOrganismResistanceList" runat="server" DataSourceID="SqlDataSource_IPS_EditOrganismResistanceList" AppendDataBoundItems="true" DataTextField="ListItem_Name" DataValueField="ListItem_Id" SelectedValue='<%# Bind("IPS_Organism_Resistance_List") %>' CssClass="Controls_DropDownList">
                            <asp:ListItem Value="">Select Resistance</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;" id="FormResistanceMechanism">Resistance Mechanism
                        </td>
                        <td style="width: 720px; padding: 0px; border-left-width: 0px; border-top-width: 1px; border-bottom-width: 0px;" colspan="3">
                          <div id="EditRMMechanismItemList" style="max-height: 250px; height: auto; overflow: auto; border-width: 0px; border-color: #dfdfdf; border-style: solid; vertical-align: top;">
                            <asp:CheckBoxList ID="CheckBoxList_EditRMMechanismItemList" runat="server" AppendDataBoundItems="true" CssClass="Controls_CheckBoxListWithScrollbars" DataSourceID="SqlDataSource_IPS_EditOrganismResistanceMechanismItemList" DataTextField="ListItem_Name" DataValueField="ListItem_Id" CellPadding="0" CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="3" RepeatLayout="Table" OnDataBound="CheckBoxList_EditRMMechanismItemList_DataBound" Width="690px">
                              <asp:ListItem Value="5018">None</asp:ListItem>
                            </asp:CheckBoxList>
                          </div>
                          <asp:HiddenField ID="HiddenField_EditRMMechanismItemListTotal" runat="server" OnDataBinding="HiddenField_EditRMMechanismItemListTotal_DataBinding" />
                        </td>
                      </tr>
                      <tr id="ShowHideDOH1" runat="server" visible="false">
                        <td style="width: 180px;" id="FormDOH1">Department of Health
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:CheckBox ID="CheckBox_EditOrganismNotifiableDepartmentOfHealth" runat="server" Checked='<%# Bind("IPS_Organism_Notifiable_DepartmentOfHealth") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideDOH2" runat="server" visible="false">
                        <td style="width: 180px;" id="FormDOH2">Department of Health Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditOrganismNotifiableDepartmentOfHealthDate" runat="server" Width="500px" Text='<%# Bind("IPS_Organism_Notifiable_DepartmentOfHealth_Date","{0:yyyy/MM/dd}") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr id="ShowHideDOH3" runat="server" visible="false">
                        <td style="width: 180px;" id="FormDOH3">Department of Health Reference Number
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:TextBox ID="TextBox_EditOrganismNotifiableDepartmentOfHealthReferenceNumber" runat="server" Width="500px" Text='<%# Bind("IPS_Organism_Notifiable_DepartmentOfHealth_ReferenceNumber") %>' CssClass="Controls_TextBox"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:CheckBox ID="CheckBox_EditIsActive" runat="server" Checked='<%# Bind("IPS_Organism_IsActive") %>' />&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Created Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditCreatedDate" runat="server" Text='<%# Bind("IPS_Organism_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Created By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditCreatedBy" runat="server" Text='<%# Bind("IPS_Organism_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Modified Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditModifiedDate" runat="server" Text='<%# Bind("IPS_Organism_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Modified By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_EditModifiedBy" runat="server" Text='<%# Bind("IPS_Organism_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_EditInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_EditOrganismInfectionHome_OnClick" />&nbsp;
                          <asp:Button ID="Button_EditNewSpecimenResult" runat="server" CausesValidation="False" Text="Capture New Laboratory Result" CssClass="Controls_Button" OnClick="Button_EditOrganismNewSpecimenResult_OnClick" />&nbsp;
                          <asp:Button ID="Button_EditNew" runat="server" CausesValidation="False" Text="Capture New Organism" CssClass="Controls_Button" OnClick="Button_EditOrganismNew_OnClick" />&nbsp;
                          <asp:Button ID="Button_EditUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update Organism" CssClass="Controls_Button" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </EditItemTemplate>
                  <ItemTemplate>
                    <table class="FormView_TableBody">
                      <tr>
                        <td colspan="4"></td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Organism
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemOrganismNameLookup" runat="server" OnDataBinding="Label_ItemOrganismNameLookup_DataBinding"></asp:Label>
                          <asp:HiddenField ID="HiddenField_Item" runat="server" />
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Antibiogram Not Required</td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemAntibiogramNotRequired" runat="server" Text='<%# (bool)(Eval("IPS_Organism_AntibiogramNotRequired"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideResistance" runat="server" visible="false">
                        <td style="width: 180px;">Resistance</td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemOrganismResistanceList" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Resistance Mechanism
                        </td>
                        <td style="width: 720px; padding: 0px; border-left-width: 1px; border-top-width: 1px;" colspan="3">
                          <asp:GridView ID="GridView_ItemOrganismResistanceMechanism" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="SqlDataSource_IPS_ItemOrganismResistanceMechanism" CssClass="GridView" AllowPaging="False" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="False" ShowHeaderWhenEmpty="True" OnRowCreated="GridView_ItemOrganismResistanceMechanism_RowCreated" OnPreRender="GridView_ItemOrganismResistanceMechanism_PreRender">
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
                                  <td>No Resistance Mechanism
                                  </td>
                                </tr>
                              </table>
                            </EmptyDataTemplate>
                            <Columns>
                              <asp:BoundField DataField="IPS_RM_Mechanism_Item_Name" ReadOnly="True" />
                            </Columns>
                          </asp:GridView>
                          &nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideDOH1" runat="server" visible="false">
                        <td style="width: 180px;">Department of Health
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemOrganismNotifiableDepartmentOfHealth" runat="server" Text='<%# (bool)(Eval("IPS_Organism_Notifiable_DepartmentOfHealth"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideDOH2" runat="server" visible="false">
                        <td style="width: 180px;">Department of Health Date<br />
                          (yyyy/mm/dd)
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemOrganismNotifiableDepartmentOfHealthDate" runat="server" Text='<%# Bind("IPS_Organism_Notifiable_DepartmentOfHealth_Date","{0:yyyy/MM/dd}") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr id="ShowHideDOH3" runat="server" visible="false">
                        <td style="width: 180px;">Department of Health Reference Number
                        </td>
                        <td style="width: 720px;" colspan="3">
                          <asp:Label ID="Label_ItemOrganismNotifiableDepartmentOfHealthReferenceNumber" runat="server" Text='<%# Bind("IPS_Organism_Notifiable_DepartmentOfHealth_ReferenceNumber") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td colspan="4">&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>Is Active
                        </td>
                        <td colspan="3">
                          <asp:Label ID="Label_ItemIsActive" runat="server" Text='<%# (bool)(Eval("IPS_Organism_IsActive"))?"Yes":"No" %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Created Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemCreatedDate" runat="server" Text='<%# Bind("IPS_Organism_CreatedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Created By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemCreatedBy" runat="server" Text='<%# Bind("IPS_Organism_CreatedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 180px;">Modified Date
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemModifiedDate" runat="server" Text='<%# Bind("IPS_Organism_ModifiedDate","{0:yyyy/MM/dd hh:mm:ss tt}") %>'></asp:Label>&nbsp;
                        </td>
                        <td style="width: 180px;">Modified By
                        </td>
                        <td style="width: 270px;">
                          <asp:Label ID="Label_ItemModifiedBy" runat="server" Text='<%# Bind("IPS_Organism_ModifiedBy") %>'></asp:Label>&nbsp;
                        </td>
                      </tr>
                      <tr class="FormView_TableFooter">
                        <td colspan="4">
                          <asp:Button ID="Button_ItemInfectionHome" runat="server" CausesValidation="False" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_ItemOrganismInfectionHome_OnClick" />&nbsp;
                        </td>
                      </tr>
                    </table>
                  </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertOrganismNameLookup" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertOrganismResistanceList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertOrganismResistanceMechanismItemList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditOrganismNameLookup" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditOrganismResistanceList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_EditOrganismResistanceMechanismItemList" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_ItemOrganismResistanceMechanism" runat="server" OnSelected="SqlDataSource_IPS_ItemOrganismResistanceMechanism_Selected"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_IPS_Organism_Form" runat="server" OnInserted="SqlDataSource_IPS_Organism_Form_Inserted" OnUpdated="SqlDataSource_IPS_Organism_Form_Updated"></asp:SqlDataSource>
              </td>
              <td style="background-color: #c3c3c3; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div id="DivAntibiogram" runat="server" style="height: 40px; width: 900px; text-align: center;">
            &nbsp;<hr style="height: 5px; width: 80%; background-color: #b0262e; border: none;" />
          </div>
          <a id="CurrentAntibiogram"></a>
          <table id="TableAntibiogram" style="width: 905px;" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_AntibiogramHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenAntibiogramTotalRecords" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #77cf9c; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_IPS_Antibiogram" runat="server" AllowPaging="True" PageSize="1000" DataSourceID="SqlDataSource_IPS_Antibiogram" AutoGenerateColumns="false" CssClass="GridView" BorderWidth="0px" ShowFooter="True" OnPreRender="GridView_IPS_Antibiogram_PreRender" OnDataBound="GridView_IPS_Antibiogram_DataBound" OnRowCreated="GridView_IPS_Antibiogram_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle_TemplateField" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_AntibiogramTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px;">
                                <asp:Button ID="Button_AntibiogramInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_AntibiogramInfectionHome_OnClick" />&nbsp;
                                <asp:Button ID="Button_AntibiogramCompleteInfection" runat="server" Text="Complete Infection" CssClass="Controls_Button" OnClick="Button_AntibiogramCompleteInfection_OnClick" />
                              </td>
                            </tr>
                          </table>
                        </PagerTemplate>
                        <RowStyle CssClass="GridView_RowStyle_TemplateField" />
                        <FooterStyle CssClass="GridView_RowStyle_TemplateField" />
                        <PagerStyle CssClass="GridView_PagerStyle" HorizontalAlign="Center" />
                        <EmptyDataRowStyle CssClass="GridView_EmptyDataStyle_TemplateField" />
                        <EmptyDataTemplate>
                          <table class="GridView_EmptyDataStyle_TemplateField">
                            <tr>
                              <td class="Table_TemplateField" colspan="4">
                                <asp:Label ID="Label_InsertValidationMessage_1" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramId_1" runat="server" Value="" />
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_1" runat="server" Value="" />
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>Antibiogram:&nbsp;</strong>
                                <asp:TextBox ID="TextBox_InsertAntibiogramCode_1" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_1" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_1" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                </Ajax:TextBoxWatermarkExtender>
                                <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_1" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_1" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                  <asp:ListItem Value="">Select Description</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_1" runat="server"></asp:SqlDataSource>
                              </td>
                              <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>SRI:&nbsp;</strong>
                                <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                  <asp:ListItem Value="4973">S</asp:ListItem>
                                  <asp:ListItem Value="4974">R</asp:ListItem>
                                  <asp:ListItem Value="4975">I</asp:ListItem>
                                </asp:RadioButtonList>
                              </td>
                              <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                              </td>
                              <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" colspan="4">
                                <asp:Label ID="Label_InsertValidationMessage_2" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramId_2" runat="server" Value="" />
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_2" runat="server" Value="" />
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>Antibiogram:&nbsp;</strong>
                                <asp:TextBox ID="TextBox_InsertAntibiogramCode_2" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_2" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_2" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                </Ajax:TextBoxWatermarkExtender>
                                <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_2" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_2" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                  <asp:ListItem Value="">Select Description</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_2" runat="server"></asp:SqlDataSource>
                              </td>
                              <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>SRI:&nbsp;</strong>
                                <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                  <asp:ListItem Value="4973">S</asp:ListItem>
                                  <asp:ListItem Value="4974">R</asp:ListItem>
                                  <asp:ListItem Value="4975">I</asp:ListItem>
                                </asp:RadioButtonList>
                              </td>
                              <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                              </td>
                              <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" colspan="4">
                                <asp:Label ID="Label_InsertValidationMessage_3" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramId_3" runat="server" Value="" />
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_3" runat="server" Value="" />
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>Antibiogram:&nbsp;</strong>
                                <asp:TextBox ID="TextBox_InsertAntibiogramCode_3" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_3" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_3" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                </Ajax:TextBoxWatermarkExtender>
                                <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_3" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_3" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                  <asp:ListItem Value="">Select Description</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_3" runat="server"></asp:SqlDataSource>
                              </td>
                              <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>SRI:&nbsp;</strong>
                                <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_3" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                  <asp:ListItem Value="4973">S</asp:ListItem>
                                  <asp:ListItem Value="4974">R</asp:ListItem>
                                  <asp:ListItem Value="4975">I</asp:ListItem>
                                </asp:RadioButtonList>
                              </td>
                              <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                              </td>
                              <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" colspan="4">
                                <asp:Label ID="Label_InsertValidationMessage_4" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramId_4" runat="server" Value="" />
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_4" runat="server" Value="" />
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>Antibiogram:&nbsp;</strong>
                                <asp:TextBox ID="TextBox_InsertAntibiogramCode_4" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_4" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_4" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                </Ajax:TextBoxWatermarkExtender>
                                <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_4" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_4" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                  <asp:ListItem Value="">Select Description</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_4" runat="server"></asp:SqlDataSource>
                              </td>
                              <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>SRI:&nbsp;</strong>
                                <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_4" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                  <asp:ListItem Value="4973">S</asp:ListItem>
                                  <asp:ListItem Value="4974">R</asp:ListItem>
                                  <asp:ListItem Value="4975">I</asp:ListItem>
                                </asp:RadioButtonList>
                              </td>
                              <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                              </td>
                              <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" colspan="4">
                                <asp:Label ID="Label_InsertValidationMessage_5" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramId_5" runat="server" Value="" />
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_5" runat="server" Value="" />
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>Antibiogram:&nbsp;</strong>
                                <asp:TextBox ID="TextBox_InsertAntibiogramCode_5" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_5" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_5" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                </Ajax:TextBoxWatermarkExtender>
                                <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_5" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_5" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                  <asp:ListItem Value="">Select Description</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_5" runat="server"></asp:SqlDataSource>
                              </td>
                              <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>SRI:&nbsp;</strong>
                                <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_5" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                  <asp:ListItem Value="4973">S</asp:ListItem>
                                  <asp:ListItem Value="4974">R</asp:ListItem>
                                  <asp:ListItem Value="4975">I</asp:ListItem>
                                </asp:RadioButtonList>
                              </td>
                              <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                              </td>
                              <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" colspan="4">
                                <asp:Label ID="Label_InsertValidationMessage_6" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramId_6" runat="server" Value="" />
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_6" runat="server" Value="" />
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>Antibiogram:&nbsp;</strong>
                                <asp:TextBox ID="TextBox_InsertAntibiogramCode_6" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_6" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_6" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                </Ajax:TextBoxWatermarkExtender>
                                <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_6" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_6" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                  <asp:ListItem Value="">Select Description</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_6" runat="server"></asp:SqlDataSource>
                              </td>
                              <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>SRI:&nbsp;</strong>
                                <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_6" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                  <asp:ListItem Value="4973">S</asp:ListItem>
                                  <asp:ListItem Value="4974">R</asp:ListItem>
                                  <asp:ListItem Value="4975">I</asp:ListItem>
                                </asp:RadioButtonList>
                              </td>
                              <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                              </td>
                              <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" colspan="4">
                                <asp:Label ID="Label_InsertValidationMessage_7" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramId_7" runat="server" Value="" />
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_7" runat="server" Value="" />
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>Antibiogram:&nbsp;</strong>
                                <asp:TextBox ID="TextBox_InsertAntibiogramCode_7" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_7" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_7" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                </Ajax:TextBoxWatermarkExtender>
                                <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_7" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_7" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                  <asp:ListItem Value="">Select Description</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_7" runat="server"></asp:SqlDataSource>
                              </td>
                              <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>SRI:&nbsp;</strong>
                                <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_7" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                  <asp:ListItem Value="4973">S</asp:ListItem>
                                  <asp:ListItem Value="4974">R</asp:ListItem>
                                  <asp:ListItem Value="4975">I</asp:ListItem>
                                </asp:RadioButtonList>
                              </td>
                              <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                              </td>
                              <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" colspan="4">
                                <asp:Label ID="Label_InsertValidationMessage_8" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramId_8" runat="server" Value="" />
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_8" runat="server" Value="" />
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>Antibiogram:&nbsp;</strong>
                                <asp:TextBox ID="TextBox_InsertAntibiogramCode_8" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_8" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_8" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                </Ajax:TextBoxWatermarkExtender>
                                <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_8" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_8" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                  <asp:ListItem Value="">Select Description</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_8" runat="server"></asp:SqlDataSource>
                              </td>
                              <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>SRI:&nbsp;</strong>
                                <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_8" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                  <asp:ListItem Value="4973">S</asp:ListItem>
                                  <asp:ListItem Value="4974">R</asp:ListItem>
                                  <asp:ListItem Value="4975">I</asp:ListItem>
                                </asp:RadioButtonList>
                              </td>
                              <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                              </td>
                              <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" colspan="4">
                                <asp:Label ID="Label_InsertValidationMessage_9" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramId_9" runat="server" Value="" />
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_9" runat="server" Value="" />
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>Antibiogram:&nbsp;</strong>
                                <asp:TextBox ID="TextBox_InsertAntibiogramCode_9" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_9" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_9" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                </Ajax:TextBoxWatermarkExtender>
                                <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_9" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_9" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                  <asp:ListItem Value="">Select Description</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_9" runat="server"></asp:SqlDataSource>
                              </td>
                              <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>SRI:&nbsp;</strong>
                                <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_9" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                  <asp:ListItem Value="4973">S</asp:ListItem>
                                  <asp:ListItem Value="4974">R</asp:ListItem>
                                  <asp:ListItem Value="4975">I</asp:ListItem>
                                </asp:RadioButtonList>
                              </td>
                              <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                              </td>
                              <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" colspan="4">
                                <asp:Label ID="Label_InsertValidationMessage_10" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramId_10" runat="server" Value="" />
                                <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_10" runat="server" Value="" />
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>Antibiogram:&nbsp;</strong>
                                <asp:TextBox ID="TextBox_InsertAntibiogramCode_10" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_10" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_10" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                </Ajax:TextBoxWatermarkExtender>
                                <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_10" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_10" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                  <asp:ListItem Value="">Select Description</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_10" runat="server"></asp:SqlDataSource>
                              </td>
                              <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                <strong>SRI:&nbsp;</strong>
                                <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                  <asp:ListItem Value="4973">S</asp:ListItem>
                                  <asp:ListItem Value="4974">R</asp:ListItem>
                                  <asp:ListItem Value="4975">I</asp:ListItem>
                                </asp:RadioButtonList>
                              </td>
                              <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                              </td>
                              <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td class="Table_TemplateField" style="text-align: center;" colspan="4">Up to 10 Antibiograms can be inserted at the same time<br />
                                Please click on the Insert button to insert the Antibiograms, where after 10 more rows will be made available for capture
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td class="Table_TemplateField" style="width: 556px;">&nbsp;</td>
                              <td class="Table_TemplateField" style="width: 140px;">&nbsp;</td>
                              <td class="Table_TemplateField" style="width: 96px;">&nbsp;</td>
                              <td class="Table_TemplateField" style="width: 73px; text-align: left;">
                                <asp:Button ID="Button_InsertAntibiogram" runat="server" Text="Insert" CssClass="Controls_Button" OnClick="Button_InsertAntibiogram_OnClick" />&nbsp;
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td colspan="4">
                                <table class="Table_Body">
                                  <tr>
                                    <td style="width: 100px; text-align: left; vertical-align: middle; padding: 5px;" class="Table_TemplateField">Total Records:
                                      <asp:Label ID="Label_AntibiogramTotalRecords" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="width: 800px; text-align: center; vertical-align: middle; padding: 5px;">
                                      <asp:Button ID="Button_AntibiogramInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_AntibiogramInfectionHome_OnClick" />&nbsp;
                                      <asp:Button ID="Button_AntibiogramCompleteInfection" runat="server" Text="Complete Infection" CssClass="Controls_Button" OnClick="Button_AntibiogramCompleteInfection_OnClick" />
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                              <table class="GridView_RowStyle_TemplateField">
                                <tr>
                                  <td class="Table_TemplateField" colspan="4">
                                    <asp:Label ID="Label_EditInvalidFormMessage" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                    <asp:Label ID="Label_EditConcurrencyUpdateMessage" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                    <asp:HiddenField ID="HiddenField_EditAntibiogramId" runat="server" Value='<%# Bind("IPS_Antibiogram_Id") %>' />
                                    <asp:HiddenField ID="HiddenField_EditAntibiogramModifiedDate" runat="server" Value='<%# Bind("IPS_Antibiogram_ModifiedDate") %>' />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 556px;">
                                    <strong>Antibiogram:&nbsp;</strong>
                                    <asp:TextBox ID="TextBox_EditAntibiogramCode" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_EditAntibiogramCode_TextChanged" OnDataBinding="TextBox_EditAntibiogramCode_DataBinding"></asp:TextBox>
                                    <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_EditAntibiogramCode" runat="server" TargetControlID="TextBox_EditAntibiogramCode" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                    </Ajax:TextBoxWatermarkExtender>
                                    <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                    <asp:DropDownList ID="DropDownList_EditAntibiogramNameLookup" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_EditAntibiogramNameLookup" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_EditAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_EditAntibiogramNameLookup_DataBinding">
                                      <asp:ListItem Value="">Select Description</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource_IPS_EditAntibiogramNameLookup" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 140px;">
                                    <strong>SRI:&nbsp;</strong>
                                    <asp:RadioButtonList ID="RadioButtonList_EditAntibiogramSRI" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                      <asp:ListItem Value="4973">S</asp:ListItem>
                                      <asp:ListItem Value="4974">R</asp:ListItem>
                                      <asp:ListItem Value="4975">I</asp:ListItem>
                                    </asp:RadioButtonList>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 96px; vertical-align: middle;">
                                    <strong>Is Active:&nbsp;</strong>
                                    <asp:CheckBox ID="CheckBox_EditAntibiogramIsActive" runat="server" Checked='<%# Bind("IPS_Antibiogram_IsActive") %>' />
                                  </td>
                                  <td class="Table_TemplateField" style="width: 73px;">
                                    <asp:Button ID="Button_EditAntibiogram" runat="server" Text="Update" CssClass="Controls_Button" OnClick="Button_EditAntibiogram_OnClick" />
                                  </td>
                                </tr>
                              </table>
                            </ItemTemplate>
                            <FooterTemplate>
                              <table class="GridView_RowStyle_TemplateField">
                                <tr>
                                  <td class="Table_TemplateField" colspan="4">
                                    <asp:Label ID="Label_InsertValidationMessage_1" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramId_1" runat="server" Value="" />
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_1" runat="server" Value="" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>Antibiogram:&nbsp;</strong>
                                    <asp:TextBox ID="TextBox_InsertAntibiogramCode_1" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                    <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_1" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_1" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                    </Ajax:TextBoxWatermarkExtender>
                                    <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                    <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_1" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_1" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                      <asp:ListItem Value="">Select Description</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_1" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>SRI:&nbsp;</strong>
                                    <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                      <asp:ListItem Value="4973">S</asp:ListItem>
                                      <asp:ListItem Value="4974">R</asp:ListItem>
                                      <asp:ListItem Value="4975">I</asp:ListItem>
                                    </asp:RadioButtonList>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4">
                                    <asp:Label ID="Label_InsertValidationMessage_2" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramId_2" runat="server" Value="" />
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_2" runat="server" Value="" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>Antibiogram:&nbsp;</strong>
                                    <asp:TextBox ID="TextBox_InsertAntibiogramCode_2" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                    <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_2" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_2" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                    </Ajax:TextBoxWatermarkExtender>
                                    <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                    <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_2" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_2" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                      <asp:ListItem Value="">Select Description</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_2" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>SRI:&nbsp;</strong>
                                    <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                      <asp:ListItem Value="4973">S</asp:ListItem>
                                      <asp:ListItem Value="4974">R</asp:ListItem>
                                      <asp:ListItem Value="4975">I</asp:ListItem>
                                    </asp:RadioButtonList>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4">
                                    <asp:Label ID="Label_InsertValidationMessage_3" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramId_3" runat="server" Value="" />
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_3" runat="server" Value="" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>Antibiogram:&nbsp;</strong>
                                    <asp:TextBox ID="TextBox_InsertAntibiogramCode_3" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                    <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_3" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_3" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                    </Ajax:TextBoxWatermarkExtender>
                                    <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                    <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_3" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_3" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                      <asp:ListItem Value="">Select Description</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_3" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>SRI:&nbsp;</strong>
                                    <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_3" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                      <asp:ListItem Value="4973">S</asp:ListItem>
                                      <asp:ListItem Value="4974">R</asp:ListItem>
                                      <asp:ListItem Value="4975">I</asp:ListItem>
                                    </asp:RadioButtonList>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4">
                                    <asp:Label ID="Label_InsertValidationMessage_4" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramId_4" runat="server" Value="" />
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_4" runat="server" Value="" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>Antibiogram:&nbsp;</strong>
                                    <asp:TextBox ID="TextBox_InsertAntibiogramCode_4" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                    <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_4" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_4" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                    </Ajax:TextBoxWatermarkExtender>
                                    <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                    <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_4" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_4" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                      <asp:ListItem Value="">Select Description</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_4" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>SRI:&nbsp;</strong>
                                    <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_4" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                      <asp:ListItem Value="4973">S</asp:ListItem>
                                      <asp:ListItem Value="4974">R</asp:ListItem>
                                      <asp:ListItem Value="4975">I</asp:ListItem>
                                    </asp:RadioButtonList>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4">
                                    <asp:Label ID="Label_InsertValidationMessage_5" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramId_5" runat="server" Value="" />
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_5" runat="server" Value="" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>Antibiogram:&nbsp;</strong>
                                    <asp:TextBox ID="TextBox_InsertAntibiogramCode_5" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                    <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_5" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_5" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                    </Ajax:TextBoxWatermarkExtender>
                                    <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                    <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_5" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_5" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                      <asp:ListItem Value="">Select Description</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_5" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>SRI:&nbsp;</strong>
                                    <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_5" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                      <asp:ListItem Value="4973">S</asp:ListItem>
                                      <asp:ListItem Value="4974">R</asp:ListItem>
                                      <asp:ListItem Value="4975">I</asp:ListItem>
                                    </asp:RadioButtonList>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4">
                                    <asp:Label ID="Label_InsertValidationMessage_6" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramId_6" runat="server" Value="" />
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_6" runat="server" Value="" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>Antibiogram:&nbsp;</strong>
                                    <asp:TextBox ID="TextBox_InsertAntibiogramCode_6" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                    <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_6" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_6" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                    </Ajax:TextBoxWatermarkExtender>
                                    <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                    <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_6" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_6" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                      <asp:ListItem Value="">Select Description</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_6" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>SRI:&nbsp;</strong>
                                    <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_6" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                      <asp:ListItem Value="4973">S</asp:ListItem>
                                      <asp:ListItem Value="4974">R</asp:ListItem>
                                      <asp:ListItem Value="4975">I</asp:ListItem>
                                    </asp:RadioButtonList>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4">
                                    <asp:Label ID="Label_InsertValidationMessage_7" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramId_7" runat="server" Value="" />
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_7" runat="server" Value="" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>Antibiogram:&nbsp;</strong>
                                    <asp:TextBox ID="TextBox_InsertAntibiogramCode_7" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                    <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_7" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_7" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                    </Ajax:TextBoxWatermarkExtender>
                                    <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                    <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_7" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_7" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                      <asp:ListItem Value="">Select Description</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_7" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>SRI:&nbsp;</strong>
                                    <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_7" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                      <asp:ListItem Value="4973">S</asp:ListItem>
                                      <asp:ListItem Value="4974">R</asp:ListItem>
                                      <asp:ListItem Value="4975">I</asp:ListItem>
                                    </asp:RadioButtonList>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4">
                                    <asp:Label ID="Label_InsertValidationMessage_8" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramId_8" runat="server" Value="" />
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_8" runat="server" Value="" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>Antibiogram:&nbsp;</strong>
                                    <asp:TextBox ID="TextBox_InsertAntibiogramCode_8" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                    <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_8" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_8" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                    </Ajax:TextBoxWatermarkExtender>
                                    <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                    <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_8" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_8" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                      <asp:ListItem Value="">Select Description</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_8" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>SRI:&nbsp;</strong>
                                    <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_8" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                      <asp:ListItem Value="4973">S</asp:ListItem>
                                      <asp:ListItem Value="4974">R</asp:ListItem>
                                      <asp:ListItem Value="4975">I</asp:ListItem>
                                    </asp:RadioButtonList>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4">
                                    <asp:Label ID="Label_InsertValidationMessage_9" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramId_9" runat="server" Value="" />
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_9" runat="server" Value="" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>Antibiogram:&nbsp;</strong>
                                    <asp:TextBox ID="TextBox_InsertAntibiogramCode_9" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                    <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_9" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_9" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                    </Ajax:TextBoxWatermarkExtender>
                                    <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                    <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_9" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_9" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                      <asp:ListItem Value="">Select Description</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_9" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>SRI:&nbsp;</strong>
                                    <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_9" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                      <asp:ListItem Value="4973">S</asp:ListItem>
                                      <asp:ListItem Value="4974">R</asp:ListItem>
                                      <asp:ListItem Value="4975">I</asp:ListItem>
                                    </asp:RadioButtonList>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" colspan="4">
                                    <asp:Label ID="Label_InsertValidationMessage_10" runat="server" Text="" CssClass="Controls_Validation"></asp:Label>
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramId_10" runat="server" Value="" />
                                    <asp:HiddenField ID="HiddenField_InsertAntibiogramInserted_10" runat="server" Value="" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="width: 556px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>Antibiogram:&nbsp;</strong>
                                    <asp:TextBox ID="TextBox_InsertAntibiogramCode_10" runat="server" Width="75px" Text="" CssClass="Controls_TextBox" AutoPostBack="true" OnTextChanged="TextBox_InsertAntibiogramCode_TextChanged"></asp:TextBox>
                                    <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_InsertAntibiogramCode_10" runat="server" TargetControlID="TextBox_InsertAntibiogramCode_10" WatermarkText="Code" WatermarkCssClass="Controls_Watermark">
                                    </Ajax:TextBoxWatermarkExtender>
                                    <strong>&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;</strong>
                                    <asp:DropDownList ID="DropDownList_InsertAntibiogramNameLookup_10" runat="server" Width="350px" DataSourceID="SqlDataSource_IPS_InsertAntibiogramNameLookup_10" AppendDataBoundItems="true" DataTextField="IPS_Antibiogram_Lookup" DataValueField="IPS_Antibiogram_Lookup_Id" SelectedValue='<%# Bind("IPS_Antibiogram_Name_Lookup") %>' CssClass="Controls_DropDownList" AutoPostBack="false" OnSelectedIndexChanged="DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged" OnDataBinding="DropDownList_InsertAntibiogramNameLookup_DataBinding">
                                      <asp:ListItem Value="">Select Description</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource_IPS_InsertAntibiogramNameLookup_10" runat="server"></asp:SqlDataSource>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 140px; border-bottom-color: #003768; border-bottom-width: 1px; border-bottom-color: #003768; border-bottom-width: 1px;">
                                    <strong>SRI:&nbsp;</strong>
                                    <asp:RadioButtonList ID="RadioButtonList_InsertAntibiogramSRI_10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left" SelectedValue='<%# Bind("IPS_Antibiogram_SRI_List")%>'>
                                      <asp:ListItem Value="4973">S</asp:ListItem>
                                      <asp:ListItem Value="4974">R</asp:ListItem>
                                      <asp:ListItem Value="4975">I</asp:ListItem>
                                    </asp:RadioButtonList>
                                  </td>
                                  <td class="Table_TemplateField" style="width: 96px; border-bottom-color: #003768; border-bottom-width: 1px; vertical-align: middle;">&nbsp;
                                  </td>
                                  <td class="Table_TemplateField" style="width: 73px; border-bottom-color: #003768; border-bottom-width: 1px;">&nbsp;
                                  </td>
                                </tr>
                                <tr>
                                  <td class="Table_TemplateField" style="text-align: center;" colspan="4">Up to 10 Antibiograms can be inserted at the same time<br />
                                    Please click on the Insert button to insert the Antibiograms, where after 10 more rows will be made available for capture
                                  </td>
                                </tr>
                                <tr class="GridView_EmptyDataStyle_FooterStyle">
                                  <td class="Table_TemplateField" style="width: 556px;">&nbsp;</td>
                                  <td class="Table_TemplateField" style="width: 140px;">&nbsp;</td>
                                  <td class="Table_TemplateField" style="width: 96px;">&nbsp;</td>
                                  <td class="Table_TemplateField" style="width: 73px; text-align: left;">
                                    <asp:Button ID="Button_InsertAntibiogram" runat="server" Text="Insert" CssClass="Controls_Button" OnClick="Button_InsertAntibiogram_OnClick" />&nbsp;
                                  </td>
                                </tr>
                              </table>
                            </FooterTemplate>
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_Antibiogram" runat="server" OnSelected="SqlDataSource_IPS_Antibiogram_Selected" OnUpdated="SqlDataSource_IPS_Antibiogram_Updated"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #77cf9c; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <table id="TableAntibiogramList" style="width: 905px;" class="Table" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_AntibiogramListHeading" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_HiddenAntibiogramListTotalRecords" runat="server" Text="" Visible="false" />
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #77cf9c; width: 5px;">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td style="padding: 0px;">
                      <asp:GridView ID="GridView_IPS_Antibiogram_List" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_IPS_Antibiogram_List" CssClass="GridView" AllowPaging="True" PageSize="1000" AllowSorting="False" BorderWidth="0px" ShowFooter="False" ShowHeader="True" ShowHeaderWhenEmpty="True" OnPreRender="GridView_IPS_Antibiogram_List_PreRender" OnRowCreated="GridView_IPS_Antibiogram_List_RowCreated">
                        <HeaderStyle CssClass="GridView_HeaderStyle" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <PagerTemplate>
                          <table class="GridView_PagerStyle">
                            <tr>
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_AntibiogramListTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px;">
                                <asp:Button ID="Button_AntibiogramInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_AntibiogramInfectionHome_OnClick" />&nbsp;
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
                              <td colspan="2">No Antibiogram Captured
                              </td>
                            </tr>
                            <tr class="GridView_EmptyDataStyle_FooterStyle">
                              <td style="width: 100px; text-align: left;">Total Records:
                                <asp:Label ID="Label_AntibiogramListTotalRecords" runat="server" Text=""></asp:Label></td>
                              <td style="width: 800px; text-align: center;">
                                <asp:Button ID="Button_AntibiogramInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_AntibiogramInfectionHome_OnClick" />&nbsp;
                              </td>
                            </tr>
                          </table>
                        </EmptyDataTemplate>
                        <Columns>
                          <asp:BoundField DataField="IPS_Antibiogram_Lookup_Code" HeaderText="Code" ReadOnly="True" SortExpression="IPS_Antibiogram_Lookup_Code" />
                          <asp:BoundField DataField="IPS_Antibiogram_Lookup_Description" HeaderText="Description" ReadOnly="True" SortExpression="IPS_Antibiogram_Lookup_Description" />
                          <asp:BoundField DataField="IPS_Antibiogram_SRI_Name" HeaderText="SRI" ReadOnly="True" SortExpression="IPS_Antibiogram_SRI_Name" />
                          <asp:BoundField DataField="IPS_Antibiogram_IsActive" HeaderText="Is Active" ReadOnly="True" SortExpression="IPS_Antibiogram_IsActive" ItemStyle-Width="60px" />
                        </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSource_IPS_Antibiogram_List" runat="server" OnSelected="SqlDataSource_IPS_Antibiogram_List_Selected"></asp:SqlDataSource>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="background-color: #77cf9c; width: 5px;">&nbsp;</td>
            </tr>
          </table>
          <div id="DivCurrentInfectionComplete" runat="server" style="height: 40px; width: 900px; text-align: center;">
            &nbsp;<hr style="height: 5px; width: 80%; background-color: #b0262e; border: none;" />
          </div>
          <asp:LinkButton ID="LinkButton_CurrentInfectionComplete" runat="server"></asp:LinkButton>
          <table id="TableCurrentInfectionComplete" class="Table" style="width: 900px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_CurrentInfectionCompleteHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Footer">
                  <tr>
                    <td>&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td colspan="8">
                      <asp:Label ID="Label_InvalidFormMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                      <asp:Label ID="Label_ConcurrencyUpdateMessage" runat="server" CssClass="Controls_Validation"></asp:Label>
                      <asp:HiddenField ID="HiddenField_CurrentInfectionComplete_ModifiedDate" runat="server" />
                      <asp:HiddenField ID="HiddenField_CurrentInfectionComplete_ModifiedBy" runat="server" />
                      <asp:HiddenField ID="HiddenField_CurrentInfectionComplete_History" runat="server" />
                      <asp:HiddenField ID="HiddenField_CurrentInfectionComplete_HAIModifiedDate" runat="server" />
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 75px"><strong>Infection:</strong>
                    </td>
                    <td style="width: 100px" id="CurrentInfectionCompleteInfection" runat="server">
                      <asp:Label ID="Label_CurrentInfectionCompleteInfection" runat="server" Text=""></asp:Label>&nbsp;
                    </td>
                    <td style="width: 75px"><strong>Specimen:</strong>
                    </td>
                    <td style="width: 100px" id="CurrentInfectionCompleteSpecimen" runat="server">
                      <asp:Label ID="Label_CurrentInfectionCompleteSpecimen" runat="server" Text="" Visible="false"></asp:Label>
                      <asp:HyperLink ID="Hyperlink_CurrentInfectionCompleteSpecimen" Text="" runat="server"></asp:HyperLink>&nbsp;
                    </td>
                    <td style="width: 125px"><strong>HAI Investigation:</strong>
                    </td>
                    <td style="width: 100px" id="CurrentInfectionCompleteHAIInvestigation" runat="server">
                      <asp:Label ID="Label_CurrentInfectionCompleteHAIInvestigation" runat="server" Text="" Visible="false"></asp:Label>
                      <asp:HyperLink ID="Hyperlink_CurrentInfectionCompleteHAIInvestigation" Text="" runat="server"></asp:HyperLink>&nbsp;
                      <asp:HiddenField ID="HiddenField_CurrentInfectionCompleteHAIId" runat="server" />
                    </td>
                    <td style="width: 75px"><strong>Is Active:</strong>
                    </td>
                    <td style="width: 100px" id="CurrentInfectionCompleteIsActive" runat="server">
                      <asp:Label ID="Label_CurrentInfectionCompleteIsActive" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Footer">
                  <tr>
                    <td style="width: 100px; text-align: left;">&nbsp;</td>
                    <td style="width: 800px; text-align: center;">
                      <asp:Button ID="Button_InfectionInfectionHome" runat="server" Text="Infection Home" CssClass="Controls_Button" OnClick="Button_InfectionInfectionHome_OnClick" />&nbsp;
                      <asp:Button ID="Button_HAIYes_LinkedSiteRequired" runat="server" Text="Linked Site Required" CssClass="Controls_Button" OnClick="Button_HAIYes_LinkedSiteRequired_OnClick" Enabled="false" />
                      <asp:Button ID="Button_HAIYes_SpecimenIncomplete" runat="server" Text="Specimen Incomplete" CssClass="Controls_Button" OnClick="Button_HAIYes_SpecimenIncomplete_OnClick" Enabled="false" />
                      <asp:Button ID="Button_HAIYes_InfectionCanceled" runat="server" Text="Infection Cancelled" CssClass="Controls_Button" OnClick="Button_HAIYes_InfectionCanceled_OnClick" Enabled="false" />
                      <asp:Button ID="Button_HAIYes_CompleteInfectionGoToHAIInvestigation" runat="server" Text="Complete Infection and Go to HAI Investigation" CssClass="Controls_Button" OnClick="Button_HAIYes_CompleteInfectionGoToHAIInvestigation_OnClick" />
                      <asp:Button ID="Button_HAIYes_OpenInfection" runat="server" Text="Open Infection" CssClass="Controls_Button" OnClick="Button_HAIYes_OpenInfection_OnClick" />&nbsp;
                      <asp:Button ID="Button_HAIYes_GoToHAIInvestigation" runat="server" Text="Go to HAI Investigation" CssClass="Controls_Button" OnClick="Button_HAIYes_GoToHAIInvestigation_OnClick" />
                      <asp:Button ID="Button_HAIYes_OpenHAIInvestigation" runat="server" Text="Open HAI Investigation" CssClass="Controls_Button" OnClick="Button_HAIYes_OpenHAIInvestigation_OnClick" />&nbsp;
                      <asp:Button ID="Button_HAIYes_CaptureNewInfection" runat="server" Text="Capture New Infection" CssClass="Controls_Button" OnClick="Button_HAIYes_CaptureNewInfection_OnClick" />
                      <asp:Button ID="Button_HAINo_SpecimenIncomplete" runat="server" Text="Specimen Incomplete" CssClass="Controls_Button" OnClick="Button_HAINo_SpecimenIncomplete_OnClick" Enabled="false" />
                      <asp:Button ID="Button_HAINo_InfectionCanceled" runat="server" Text="Infection Cancelled" CssClass="Controls_Button" OnClick="Button_HAINo_InfectionCanceled_OnClick" Enabled="false" />
                      <asp:Button ID="Button_HAINo_CompleteInfection" runat="server" Text="Complete Infection" CssClass="Controls_Button" OnClick="Button_HAINo_CompleteInfection_OnClick" />
                      <asp:Button ID="Button_HAINo_OpenInfection" runat="server" Text="Open Infection" CssClass="Controls_Button" OnClick="Button_HAINo_OpenInfection_OnClick" />&nbsp;
                      <asp:Button ID="Button_HAINo_CaptureNewInfection" runat="server" Text="Capture New Infection" CssClass="Controls_Button" OnClick="Button_HAINo_CaptureNewInfection_OnClick" />
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
    <div style="height: 1000px;">
      &nbsp;
    </div>
  </form>
</body>
</html>
