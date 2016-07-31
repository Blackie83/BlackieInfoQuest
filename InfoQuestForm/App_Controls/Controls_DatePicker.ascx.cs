using System;

namespace InfoQuestForm
{
  public partial class Controls_DatePicker : System.Web.UI.UserControl
  {
    public string SetGetYear
    {
      get { return DropDownList_Year.SelectedValue; }
      set { DropDownList_Year.SelectedValue = value; }
    }

    public string SetGetMonth
    {
      get { return DropDownList_Month.SelectedValue; }
      set { DropDownList_Month.SelectedValue = value; }
    }

    public string SetGetDay
    {
      get { return DropDownList_Day.SelectedValue; }
      set { DropDownList_Day.SelectedValue = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
  }
}