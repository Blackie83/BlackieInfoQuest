
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_SystemPageText_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_SystemPageText_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_SystemPageText_Form_TextBox_" + FormMode + "Description").value == "")
    {
      document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDescription").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDescription").style.color = "#333333";
    }

    //    if (document.getElementById("_content_FormView_SystemPageText_Form_Editor_InsertText_ctl02").value == "") {
    //      document.getElementById("FormText").style.backgroundColor = "#d46e6e";
    //      document.getElementById("FormText").style.color = "#333333";
    //    } else {
    //      document.getElementById("FormText").style.backgroundColor = "#77cf9c";
    //      document.getElementById("FormText").style.color = "#333333";
    //    }
  }
}