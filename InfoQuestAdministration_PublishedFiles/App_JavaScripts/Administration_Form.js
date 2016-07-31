
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form() {
  var FormMode;
  if (document.getElementById("FormView_Form_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_Form_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_Form_Form_TextBox_" + FormMode + "Name").value == "") {
      document.getElementById("FormName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormName").style.color = "#333333";
    } else {
      document.getElementById("FormName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormName").style.color = "#333333";
    }

    if (document.getElementById("FormView_Form_Form_TextBox_" + FormMode + "Description").value == "") {
      document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDescription").style.color = "#333333";
    } else {
      document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDescription").style.color = "#333333";
    }

    if (document.getElementById("FormView_Form_Form_TextBox_" + FormMode + "ReportNumberIdentifier").value == "") {
      document.getElementById("FormReportNumberIdentifier").style.backgroundColor = "#d46e6e";
      document.getElementById("FormReportNumberIdentifier").style.color = "#333333";
    } else {
      document.getElementById("FormReportNumberIdentifier").style.backgroundColor = "#77cf9c";
      document.getElementById("FormReportNumberIdentifier").style.color = "#333333";
    }
  }
}
