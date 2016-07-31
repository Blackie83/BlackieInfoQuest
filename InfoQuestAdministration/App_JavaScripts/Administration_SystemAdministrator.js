
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_SystemAdministrator_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_SystemAdministrator_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_SystemAdministrator_Form_TextBox_" + FormMode + "Description").value == "")
    {
      document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDescription").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDescription").style.color = "#333333";
    }

    if (document.getElementById("FormView_SystemAdministrator_Form_TextBox_" + FormMode + "Domain").value == "")
    {
      document.getElementById("FormDomain").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDomain").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDomain").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDomain").style.color = "#333333";
    }

    if (document.getElementById("FormView_SystemAdministrator_Form_TextBox_" + FormMode + "UserName").value == "")
    {
      document.getElementById("FormUserName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUserName").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormUserName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUserName").style.color = "#333333";
    }
  }
}
