using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_Exceptions : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_Exceptions_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_Exceptions, GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["Exceptions_Id"] != null)
          {
            TableForm.Visible = true;

            SetFormVisibility();
          }
          else
          {
            TableForm.Visible = false;

            RedirectToList();
          }

          if (TableForm.Visible == true)
          {
            TableFormVisible();
          }
        }
      }
    }

    private string PageSecurity()
    {
      string SecurityAllow = "0";

      string SecurityAllowAdministration = "0";

      SecurityAllowAdministration = InfoQuestWCF.InfoQuest_Security.Security_Form_Administration(Request.ServerVariables["LOGON_USER"]);

      if (SecurityAllowAdministration == "1")
      {
        SecurityAllow = "1";
      }
      else
      {
        SecurityAllow = "0";
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("No Access", "InfoQuest_PageText.aspx?PageTextValue=5"), false);
        Response.End();
      }

      return SecurityAllow;
    }

    protected void Page_Error(object sender, EventArgs e)
    {
      Exception Exception_Error = Server.GetLastError().GetBaseException();
      Server.ClearError();

      InfoQuestWCF.InfoQuest_Exceptions.Exceptions(Exception_Error, Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"], "");
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
      InfoQuestWCF.InfoQuest_All.All_Maintenance("0");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Exceptions.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["Exceptions_Id"] != null)
      {
        FormView_Exceptions_Form.ChangeMode(FormViewMode.Edit);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_Exceptions_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_Exceptions_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((DropDownList)FormView_Exceptions_Form.FindControl("DropDownList_EditCompleted")).Attributes.Add("OnChange", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_ExceptionsPage"];
      string SearchField2 = Request.QueryString["Search_ExceptionsOther"];
      string SearchField3 = Request.QueryString["Search_ExceptionsCompleted"];
      string SearchField4 = Request.QueryString["Search_ExceptionsDateFrom"];
      string SearchField5 = Request.QueryString["Search_ExceptionsDateTo"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Exceptions_Page=" + Request.QueryString["Search_ExceptionsPage"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Exceptions_Other=" + Request.QueryString["Search_ExceptionsOther"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_Exceptions_Completed=" + Request.QueryString["Search_ExceptionsCompleted"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_Exceptions_DateFrom=" + Request.QueryString["Search_ExceptionsDateFrom"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_Exceptions_DateTo=" + Request.QueryString["Search_ExceptionsDateTo"] + "&";
      }

      string FinalURL = "Administration_Exceptions_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Exceptions List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_Exceptions_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDExceptionsModifiedDate"] = e.OldValues["Exceptions_ModifiedDate"];
        object OLDExceptionsModifiedDate = Session["OLDExceptionsModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDExceptionsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareExceptions = (DataView)SqlDataSource_Exceptions_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareExceptions = DataView_CompareExceptions[0];
        Session["DBExceptionsModifiedDate"] = Convert.ToString(DataRowView_CompareExceptions["Exceptions_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBExceptionsModifiedBy"] = Convert.ToString(DataRowView_CompareExceptions["Exceptions_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBExceptionsModifiedDate = Session["DBExceptionsModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBExceptionsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBExceptionsModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_Exceptions_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_Exceptions_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation();

          if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
          }
          else
          {
            e.Cancel = true;
          }

          if (e.Cancel == true)
          {
            Page.MaintainScrollPositionOnPostBack = false;
            ((Label)FormView_Exceptions_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_Exceptions_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["Exceptions_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Exceptions_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_Exceptions", "Exceptions_Id = " + Request.QueryString["Exceptions_Id"]);

            DataView DataView_Exceptions = (DataView)SqlDataSource_Exceptions_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Exceptions = DataView_Exceptions[0];
            Session["ExceptionsHistory"] = Convert.ToString(DataRowView_Exceptions["Exceptions_History"], CultureInfo.CurrentCulture);

            Session["ExceptionsHistory"] = Session["History"].ToString() + Session["ExceptionsHistory"].ToString();
            e.NewValues["Exceptions_History"] = Session["ExceptionsHistory"].ToString();

            string DBCompleted = e.OldValues["Exceptions_Completed"].ToString();
            DropDownList DropDownList_EditCompleted = (DropDownList)FormView_Exceptions_Form.FindControl("DropDownList_EditCompleted");

            if (DBCompleted != DropDownList_EditCompleted.SelectedValue)
            {
              if (DropDownList_EditCompleted.SelectedValue == "True")
              {
                e.NewValues["Exceptions_CompletedDate"] = DateTime.Now.ToString();
              }
              else
              {
                e.NewValues["Exceptions_CompletedDate"] = "";
              }
            }

            Session["ExceptionsHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDExceptionsModifiedDate"] = "";
        Session["DBExceptionsModifiedDate"] = "";
        Session["DBExceptionsModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_EditCompleted = (DropDownList)FormView_Exceptions_Form.FindControl("DropDownList_EditCompleted");
      TextBox TextBox_EditDescription = (TextBox)FormView_Exceptions_Form.FindControl("TextBox_EditDescription");

      if (InvalidForm == "No")
      {
        if (DropDownList_EditCompleted.SelectedValue == "True")
        {
          if (string.IsNullOrEmpty(TextBox_EditDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {

      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Exceptions_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;
            RedirectToList();
          }
        }
      }
    }


    protected void FormView_Exceptions_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          RedirectToList();
        }
      }
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}