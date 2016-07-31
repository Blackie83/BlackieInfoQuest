
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_EmailNotification_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_EmailNotification_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_EmailNotification_Form_TextBox_" + FormMode + "Assembly").value == "")
    {
      document.getElementById("FormAssembly").style.backgroundColor = "#d46e6e";
      document.getElementById("FormAssembly").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormAssembly").style.backgroundColor = "#77cf9c";
      document.getElementById("FormAssembly").style.color = "#333333";
    }

    if (document.getElementById("FormView_EmailNotification_Form_TextBox_" + FormMode + "Method").value == "")
    {
      document.getElementById("FormMethod").style.backgroundColor = "#d46e6e";
      document.getElementById("FormMethod").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormMethod").style.backgroundColor = "#77cf9c";
      document.getElementById("FormMethod").style.color = "#333333";
    }

    if (document.getElementById("FormView_EmailNotification_Form_TextBox_" + FormMode + "Email").value == "")
    {
      document.getElementById("FormEmail").style.backgroundColor = "#d46e6e";
      document.getElementById("FormEmail").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormEmail").style.backgroundColor = "#77cf9c";
      document.getElementById("FormEmail").style.color = "#333333";
    }
  }
}
