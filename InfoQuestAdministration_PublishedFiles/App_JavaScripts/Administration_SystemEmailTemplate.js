
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form() {
  var FormMode;
  if (document.getElementById("FormView_SystemEmailTemplate_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_SystemEmailTemplate_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_SystemEmailTemplate_Form_TextBox_" + FormMode + "Description").value == "")
    {
      document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDescription").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDescription").style.color = "#333333";
    }

    //    if (document.getElementById("_content_FormView_SystemEmailTemplate_Form_Editor_InsertTemplate_ctl02").value == "") {
    //      document.getElementById("FormTemplate").style.backgroundColor = "#d46e6e";
    //      document.getElementById("FormTemplate").style.color = "#333333";
    //    } else {
    //      document.getElementById("FormTemplate").style.backgroundColor = "#77cf9c";
    //      document.getElementById("FormTemplate").style.color = "#333333";
    //    }
  }
}