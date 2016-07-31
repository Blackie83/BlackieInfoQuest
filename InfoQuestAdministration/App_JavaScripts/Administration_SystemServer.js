
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_SystemServer_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_SystemServer_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_SystemServer_Form_TextBox_" + FormMode + "Description").value == "")
    {
      document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDescription").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDescription").style.color = "#333333";
    }

    if (document.getElementById("FormView_SystemServer_Form_TextBox_" + FormMode + "Server").value == "")
    {
      document.getElementById("FormServer").style.backgroundColor = "#d46e6e";
      document.getElementById("FormServer").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormServer").style.backgroundColor = "#77cf9c";
      document.getElementById("FormServer").style.color = "#333333";
    }
  }
}
