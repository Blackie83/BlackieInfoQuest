using System;
using System.IO;
using System.Web.Caching;
using System.Configuration;
using System.Web.UI;
using System.Globalization;
using System.Web.UI.WebControls;

namespace InfoQuestWCF
{
  public class Override_SystemWebUIPage : Page
  {
    protected override void SavePageStateToPersistenceMedium(object state)
    {
      string Key = "";

      switch (ViewState_Type())
      {
        //----------------------------------------------------------------------------------------------------
        case "Default":
          base.SavePageStateToPersistenceMedium(state);

          break;
        //----------------------------------------------------------------------------------------------------
        case "File":
          Key = ViewState_Key(Request.Form["__VIEWSTATE_KEY"], Request.ServerVariables["LOGON_USER"], Page.Title);
          ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", Key);
          string FileName = Path.Combine(Server.MapPath("App_Files/InfoQuest_ViewState/") + Key + ".txt");
          FileStream FileStream_FileName;
          using (FileStream_FileName = new FileStream(FileName, FileMode.Create))
          {
            LosFormatter LosFormatter_FileName = new LosFormatter();
            LosFormatter_FileName.Serialize(FileStream_FileName, state);
            FileStream_FileName.Flush();
          }

          break;
        //----------------------------------------------------------------------------------------------------
        case "Cache":
          Key = ViewState_Key(Request.Form["__VIEWSTATE_KEY"], Request.ServerVariables["LOGON_USER"], Page.Title);
          ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", Key);
          Cache.Remove(Key);
          Cache.Add(Key, state, null, DateTime.Now.AddMinutes(Session.Timeout), TimeSpan.Zero, CacheItemPriority.Default, null);

          break;
        //----------------------------------------------------------------------------------------------------
        case "Session":
          Key = ViewState_Key(Request.Form["__VIEWSTATE_KEY"], Request.ServerVariables["LOGON_USER"], Page.Title);
          ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", Key);
          Session["_ViewState_" + Key + ""] = state;

          break;
        //----------------------------------------------------------------------------------------------------
      }
    }

    protected override Object LoadPageStateFromPersistenceMedium()
    {
      Object ReturnViewState = null;
      string Key = "";

      switch (ViewState_Type())
      {
        //----------------------------------------------------------------------------------------------------
        case "Default":
          ReturnViewState = base.LoadPageStateFromPersistenceMedium();

          break;
        //----------------------------------------------------------------------------------------------------
        case "File":
          Key = ViewState_Key(Request.Form["__VIEWSTATE_KEY"], Request.ServerVariables["LOGON_USER"], Page.Title);
          ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", Key);
          string FileName = Path.Combine(Server.MapPath("App_Files/InfoQuest_ViewState/") + Key + ".txt");
          Object State = null;
          using (StreamReader StreamReader_FileName = new StreamReader(FileName))
          {
            LosFormatter LosFormatter_FileName = new LosFormatter();
            State = LosFormatter_FileName.Deserialize(StreamReader_FileName);
          }
          ReturnViewState = State;

          break;
        //----------------------------------------------------------------------------------------------------
        case "Cache":
          Key = ViewState_Key(Request.Form["__VIEWSTATE_KEY"], Request.ServerVariables["LOGON_USER"], Page.Title);
          ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", Key);
          ReturnViewState = Cache[Key];

          break;
        //----------------------------------------------------------------------------------------------------
        case "Session":
          Key = ViewState_Key(Request.Form["__VIEWSTATE_KEY"], Request.ServerVariables["LOGON_USER"], Page.Title);
          ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", Key);
          ReturnViewState = Session["_ViewState_" + Key + ""];

          break;
        //----------------------------------------------------------------------------------------------------
      }

      return ReturnViewState;
    }

    public static string ViewState_Type()
    {
      string Type = "";
      if (ConfigurationManager.AppSettings["ViewStateServerSide"] != null)
      {
        if (ConfigurationManager.AppSettings["ViewStateServerSide"].ToString() == "Yes")
        {
          if (ConfigurationManager.AppSettings["ViewStateType"] != null)
          {
            Type = ConfigurationManager.AppSettings["ViewStateType"].ToString();
          }
          else
          {
            Type = "Default";
          }
        }
        else
        {
          Type = "Default";
        }
      }
      else
      {
        Type = "Default";
      }

      return Type;
    }

    public static string ViewState_Key(string state_Key, string userName, string title)
    {
      string Key = "";
      if (state_Key == null)
      {
        Key = "VIEWSTATE_" + title + "_" + userName + "_" + DateTime.Now.Ticks.ToString(CultureInfo.CurrentCulture);
        Key = Key.Replace("\\", "");
        Key = Key.Replace(" ", "");
        Key = Key.Replace("-", "");
        Key = Key.ToString().ToUpper(CultureInfo.CurrentCulture);
      }
      else
      {
        Key = state_Key;
      }

      return Key;
    }
  }

  public class Override_AjaxControlToolkitHtmlEditorEditor : AjaxControlToolkit.HTMLEditor.Editor
  {
    protected override void FillTopToolbar()
    {
      //TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Bold());
      //TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Italic());
    }

    protected override void FillBottomToolbar()
    {
      //BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.DesignMode());
      BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.HtmlMode());
      BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.PreviewMode());
    }
  }

  public class Override_CheckBoxList : CheckBoxList    
  {
    protected override object SaveViewState()
    {
      // create object array for Item count + 1
      object[] allStates = new object[this.Items.Count + 1];

      // the +1 is to hold the base info
      object baseState = base.SaveViewState();
      allStates[0] = baseState;

      Int32 i = 1;
      // now loop through and save each Style attribute for the List
      foreach (ListItem li in this.Items)
      {
        Int32 j = 0;
        string[][] attributes = new string[li.Attributes.Count][];
        foreach (string attribute in li.Attributes.Keys)
        {
          attributes[j++] = new string[] { attribute, li.Attributes[attribute] };
        }
        allStates[i++] = attributes;
      }
      return allStates;
    }

    protected override void LoadViewState(object savedState)
    {
      if (savedState != null)
      {
        object[] myState = (object[])savedState;

        // restore base first
        if (myState[0] != null)
          base.LoadViewState(myState[0]);

        Int32 i = 1;
        foreach (ListItem li in this.Items)
        {
          // loop through and restore each style attribute
          foreach (string[] attribute in (string[][])myState[i++])
          {
            li.Attributes[attribute[0]] = attribute[1];
          }
        }
      }
    }
  }
}