
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_SecurityAccess_WCF_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_SecurityAccess_WCF_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_SecurityAccess_WCF_Form_TextBox_" + FormMode + "Method").value == "")
    {
      document.getElementById("FormMethod").style.backgroundColor = "#d46e6e";
      document.getElementById("FormMethod").style.color = "#333333";
    } else
    {
      document.getElementById("FormMethod").style.backgroundColor = "#77cf9c";
      document.getElementById("FormMethod").style.color = "#333333";
    }

    if (document.getElementById("FormView_SecurityAccess_WCF_Form_TextBox_" + FormMode + "UserName").value == "")
    {
      document.getElementById("FormUserName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUserName").style.color = "#333333";
    } else
    {
      document.getElementById("FormUserName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUserName").style.color = "#333333";
    }

    if (document.getElementById("FormView_SecurityAccess_WCF_Form_TextBox_" + FormMode + "Password").value == "")
    {
      document.getElementById("FormPassword").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPassword").style.color = "#333333";
    } else
    {
      document.getElementById("FormPassword").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPassword").style.color = "#333333";
    }
  }
}
