using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Permissions;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_ContactCentre_UserAccessRequest : InfoQuestWCF.Override_SystemWebUIPage
  {
    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    protected void Page_Load(object sender, EventArgs e)
    {
      SqlDataSourceSetup();

      if (!IsPostBack)
      {
        InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

        RequestedByInfo();

        Button_New.Visible = false;
        Button_PrintTop.Visible = false;
        Button_PrintBottom.Visible = false;
      }
    }

    protected void Page_Error(object sender, EventArgs e)
    {
      Exception Exception_Error = Server.GetLastError().GetBaseException();
      Server.ClearError();

      InfoQuestWCF.InfoQuest_Exceptions.Exceptions(Exception_Error, Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"], "");
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
      InfoQuestWCF.InfoQuest_All.All_Maintenance("20");

      Label Label_UpdateProgress = (Label)PageUpdateProgress_ContactCentre_UserAccessRequest.FindControl("Label_UpdateProgress");
      Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Facility.SelectCommand = "SELECT Facility_Id , Facility_FacilityDisplayName FROM vAdministration_Facility_Active ORDER BY Facility_FacilityDisplayName";

      SqlDataSource_ContactCentre_UserAccessRequest_SecurityRole.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ContactCentre_UserAccessRequest_SecurityRole.SelectCommand = "spForm_Get_ContactCentre_UserAccessRequest_SecurityRole";
      SqlDataSource_ContactCentre_UserAccessRequest_SecurityRole.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_ContactCentre_UserAccessRequest_SecurityRole.CancelSelectOnNullParameter = false;
      SqlDataSource_ContactCentre_UserAccessRequest_SecurityRole.SelectParameters.Clear();
      SqlDataSource_ContactCentre_UserAccessRequest_SecurityRole.SelectParameters.Add("FacilityId", TypeCode.String, "");
    }


    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    private void RequestedByInfo()
    {
      string RequestedByUserName = Request.ServerVariables["LOGON_USER"];

      DataTable DataTable_AD;
      using (DataTable_AD = new DataTable())
      {
        DataTable_AD.Locale = CultureInfo.CurrentCulture;
        DataTable_AD = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(RequestedByUserName).Copy();
        if (DataTable_AD.Columns.Count == 1)
        {
          Label_RequestedByName.Text = "";
          Label_RequestedByEmail.Text = "";
        }
        else if (DataTable_AD.Columns.Count != 1)
        {
          if (DataTable_AD.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_AD.Rows)
            {
              Label_RequestedByName.Text = DataRow_Row["DisplayName"].ToString();
              Label_RequestedByEmail.Text = DataRow_Row["Email"].ToString();
            }
          }
          else
          {
            Label_RequestedByName.Text = "";
            Label_RequestedByEmail.Text = "";
          }
        }
      }
    }

    private void RegisterPostBackControl()
    {
      ScriptManager ScriptManager_Form = ScriptManager.GetCurrent(Page);

      ScriptManager_Form.RegisterPostBackControl(DropDownList_Facility);
      ScriptManager_Form.RegisterPostBackControl(Button_New);
      ScriptManager_Form.RegisterPostBackControl(Button_PrintTop);
      ScriptManager_Form.RegisterPostBackControl(Button_PrintBottom);
    }


    protected void DropDownList_Facility_SelectedIndexChanged(object sender, EventArgs e)
    {
      string FacilityId = DropDownList_Facility.SelectedValue.ToString();
      SqlDataSource_ContactCentre_UserAccessRequest_SecurityRole.SelectParameters["FacilityId"].DefaultValue = FacilityId;
      GridView_ContactCentre_UserAccessRequest_SecurityRole.DataBind();

      if (string.IsNullOrEmpty(FacilityId))
      {
        Button_PrintTop.Visible = false;
        Button_PrintBottom.Visible = false;
      }
      else
      {
        Button_PrintTop.Visible = true;
        Button_PrintBottom.Visible = true;
      }

      RegisterPostBackControl();
    }

    protected void Button_Print_Click(object sender, EventArgs e)
    {
      Label_Facility.Text = DropDownList_Facility.SelectedItem.ToString();
      DropDownList_Facility.Visible = false;
      Label_Facility.Visible = true;

      Button_New.Visible = true;
      Button_PrintTop.Visible = false;
      Button_PrintBottom.Visible = false;
      ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'>window.print();</script>");

      RegisterPostBackControl();
    }

    protected void Button_New_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Contact Centre User Access Request", "Form_ContactCentre_UserAccessRequest.aspx");
      Response.Redirect(FinalURL, false);
    }

    protected override void Render(HtmlTextWriter writer)
    {
      string LastForm = "";
      Table Table_Grid = (Table)GridView_ContactCentre_UserAccessRequest_SecurityRole.Controls[0];
      foreach (GridViewRow GridViewRow_Row in GridView_ContactCentre_UserAccessRequest_SecurityRole.Rows)
      {
        HiddenField HiddenField_Form = (HiddenField)GridViewRow_Row.FindControl("HiddenField_Form");
        string CurrentForm = HiddenField_Form.Value;
        if (string.Compare(LastForm, CurrentForm, StringComparison.CurrentCulture) != 0)
        {
          Int32 rowIndex = Table_Grid.Rows.GetRowIndex(GridViewRow_Row);

          using (GridViewRow GridViewRow_Add = new GridViewRow(rowIndex, rowIndex, DataControlRowType.DataRow, DataControlRowState.Normal))
          {
            using (TableCell TableCell_Header = new TableCell())
            {
              TableCell_Header.ColumnSpan = GridView_ContactCentre_UserAccessRequest_SecurityRole.Columns.Count;
              TableCell_Header.Text = CurrentForm.ToString();
              TableCell_Header.Font.Bold = true;
              TableCell_Header.Font.Size = 9;
              TableCell_Header.HorizontalAlign = HorizontalAlign.Center;

              GridViewRow_Add.Cells.Add(TableCell_Header);
              Table_Grid.Controls.AddAt(rowIndex, GridViewRow_Add);
            }
          }

          LastForm = CurrentForm;
        }
      }

      base.Render(writer);
    }
  }
}