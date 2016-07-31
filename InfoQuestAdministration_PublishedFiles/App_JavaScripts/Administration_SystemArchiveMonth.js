
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_SystemArchiveMonth_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_SystemArchiveMonth_Form_TextBox_" + FormMode + "Months").value == "")
    {
      document.getElementById("FormMonths").style.backgroundColor = "#d46e6e";
      document.getElementById("FormMonths").style.color = "#333333";
    } else
    {
      document.getElementById("FormMonths").style.backgroundColor = "#77cf9c";
      document.getElementById("FormMonths").style.color = "#333333";
    }
  }
}
