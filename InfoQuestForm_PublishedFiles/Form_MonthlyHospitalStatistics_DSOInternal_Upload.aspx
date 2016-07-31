<%@ Page Language="C#" AutoEventWireup="true" Inherits="InfoQuestForm.Form_MonthlyHospitalStatistics_DSOInternal_Upload" CodeBehind="Form_MonthlyHospitalStatistics_DSOInternal_Upload.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="App_Controls/Controls_Header.ascx" TagName="HeaderText" TagPrefix="Header" %>
<%@ Register Src="App_Controls/Controls_Navigation.ascx" TagName="NavigationMenu" TagPrefix="Navigation" %>
<%@ Register Src="App_Controls/Controls_Footer.ascx" TagName="FooterText" TagPrefix="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>InfoQuest - Monthly Hospital Statistics DSO Internal Upload</title>
  <asp:PlaceHolder runat="server">
    <link href="App_Themes/LifeHealthcare/LifeHealthcare.css?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" rel="stylesheet" type="text/css" />
    <script src="App_Javascripts/InfoQuest_Loader.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/InfoQuest_LoaderSettings.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
    <script src="App_Javascripts/Form_MonthlyHospitalStatistics_DSOInternal_Upload.js?Version=<%=InfoQuestWCF.InfoQuest_All.All_FileVersion() %>" type="text/javascript"></script>
  </asp:PlaceHolder>
</head>
<body onload="Hide('Label_ProgressUpload');Hide('Label_ProgressDelete');Hide('Label_ProgressExtract');">
  <form id="form_MHS_DSOInternal_Upload" runat="server">
    <Header:HeaderText ID="HeaderText_Page" runat="server" />
    <Navigation:NavigationMenu ID="NavigationMenu_Page" runat="server" />
    <div>
      <Ajax:ToolkitScriptManager ID="ToolkitScriptManager_MHS_DSOInternal_Upload" runat="server" CombineScripts="false">
      </Ajax:ToolkitScriptManager>
      <asp:UpdateProgress runat="server" ID="PageUpdateProgress_MHS_DSOInternal_Upload" AssociatedUpdatePanelID="UpdatePanel_MHS_DSOInternal_Upload">
        <ProgressTemplate>
          <div class="UpdateProgressBackground">
          </div>
          <div class="UpdateProgressContent" style="position: fixed;">
            <asp:Label ID="Label_UpdateProgress" runat="server" Text=""></asp:Label>
          </div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel ID="UpdatePanel_MHS_DSOInternal_Upload" runat="server">
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
          <table id="TableUpload" class="Table" style="width: 600px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_UploadHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>When uploading a document<br />
                      Only these document formats can be uploaded: Excel (.xls / .xlsx)<br />
                      Only files smaller then 5 MB can be uploaded
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <asp:FileUpload ID="FileUpload_Upload" runat="server" CssClass="Controls_FileUpload" Width="500" AllowMultiple="true" />
                    </td>
                  </tr>
                  <tr>
                    <td style="vertical-align: middle; text-align: center;">
                      <asp:Label ID="Label_Upload" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_ProgressUpload" runat="server" Text="File Uploading Started"></asp:Label>
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
                      <asp:Button ID="Button_Upload" runat="server" Text="Upload File" CssClass="Controls_Button" OnClick="Button_Upload_Click" OnClientClick="Hide('Label_Upload');Show('Label_ProgressUpload');" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
          <table id="TableUploaded" class="Table" style="width: 600px;" runat="server">
            <tr>
              <td>
                <table class="Table_Header">
                  <tr>
                    <td>
                      <asp:Label ID="Label_UploadedHeading" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class="Table_Body">
                  <tr>
                    <td>Total Files:&nbsp;<asp:Label ID="Label_TotalFiles" runat="server" Text=""></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="Table_BodyHeaderWhite">File Name
                    </td>
                  </tr>
                  <tr>
                    <td style="padding: 0px;">
                      <asp:Label ID="Label_UploadedFiles" runat="server" Text=""></asp:Label>
                      <asp:CheckBoxList ID="CheckBoxList_UploadedFiles" CssClass="Controls_CheckBoxList" runat="server">
                      </asp:CheckBoxList>
                    </td>
                  </tr>
                  <tr>
                    <td style="vertical-align: middle; text-align: center;">
                      <asp:Label ID="Label_Delete" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_ProgressDelete" runat="server" Text="File Deletion Started"></asp:Label>
                      <asp:Label ID="Label_Extract" runat="server" Text=""></asp:Label>
                      <asp:Label ID="Label_ProgressExtract" runat="server" Text="File Extraction Started"></asp:Label>
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
                      <asp:Button ID="Button_Delete" runat="server" CssClass="Controls_Button" Text="Delete Checked Files" OnClick="Button_Delete_Click" OnClientClick="Hide('Label_Delete');Show('Label_ProgressDelete');Hide('Label_Extract');" />&nbsp;
                      <asp:Button ID="Button_DeleteAll" runat="server" CssClass="Controls_Button" Text="Delete All Files" OnClick="Button_DeleteAll_Click" OnClientClick="Hide('Label_Delete');Show('Label_ProgressDelete');Hide('Label_Extract');" />&nbsp;
                      <br />
                      <br />
                      <asp:Button ID="Button_Extract" runat="server" CssClass="Controls_Button" Text="Import Data from Checked Files" OnClick="Button_Extract_Click" OnClientClick="Hide('Label_Delete');Hide('Label_Extract');Show('Label_ProgressExtract');" />&nbsp;
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
          <div>
            &nbsp;
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    <Footer:FooterText ID="FooterText_Page" runat="server" />
  </form>
</body>
</html>
