
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form() {
  var FormMode;
  if (document.getElementById("FormView_Exceptions_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_Exceptions_Form_DropDownList_" + FormMode + "Completed").value == "True") {
      if (document.getElementById("FormView_Exceptions_Form_TextBox_" + FormMode + "Description").value == "") {
        document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
        document.getElementById("FormDescription").style.color = "#333333";
      } else {
        document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
        document.getElementById("FormDescription").style.color = "#333333";
      }
    } else {
      document.getElementById("FormDescription").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormDescription").style.color = "#000000";
    }
  }
}