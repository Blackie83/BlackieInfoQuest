using System;
using System.Collections.Generic;

namespace InfoQuestAdministration
{
  public partial class Controls_Navigation : System.Web.UI.UserControl
  {
    private Dictionary<string, string> NavigationIdHandler = new Dictionary<string, string>();

    public Dictionary<string, string> NavigationId
    {
      get { return NavigationIdHandler; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        Label_Navigation.Text = Navigation();
      }
    }

    protected string Navigation()
    {
      string NavigationMenu = "";

      foreach (KeyValuePair<string, string> NavigationId in NavigationIdHandler)
      {
        NavigationMenu = NavigationMenu + InfoQuestWCF.InfoQuest_All.All_NavigationDescription(NavigationId.Value);
      }

      NavigationIdHandler.Clear();

      return NavigationMenu;
    }
  }
}