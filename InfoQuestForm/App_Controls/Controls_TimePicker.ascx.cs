using System;

namespace InfoQuestForm
{
  public partial class Controls_TimePicker : System.Web.UI.UserControl
  {
    public string SetGetHour
    {
      get { return DropDownList_Hour.SelectedValue; }
      set { DropDownList_Hour.SelectedValue = value; }
    }

    public string SetGetMinute
    {
      get { return DropDownList_Minute.SelectedValue; }
      set { DropDownList_Minute.SelectedValue = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
  }
}