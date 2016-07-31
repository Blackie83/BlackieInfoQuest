
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form() {
  var FormMode;
  if (document.getElementById("FormView_SecurityRole_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_SecurityRole_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_SecurityRole_Form_DropDownList_" + FormMode + "FormId").value == "") {
      document.getElementById("FormFormId").style.backgroundColor = "#d46e6e";
      document.getElementById("FormFormId").style.color = "#333333";
    } else {
      document.getElementById("FormFormId").style.backgroundColor = "#77cf9c";
      document.getElementById("FormFormId").style.color = "#333333";
    }

    if (document.getElementById("FormView_SecurityRole_Form_TextBox_" + FormMode + "Name").value == "") {
      document.getElementById("FormName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormName").style.color = "#333333";
    } else {
      document.getElementById("FormName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormName").style.color = "#333333";
    }

    if (document.getElementById("FormView_SecurityRole_Form_TextBox_" + FormMode + "Description").value == "") {
      document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDescription").style.color = "#333333";
    } else {
      document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDescription").style.color = "#333333";
    }

    if (document.getElementById("FormView_SecurityRole_Form_TextBox_" + FormMode + "Rank").value == "") {
      document.getElementById("FormRank").style.backgroundColor = "#d46e6e";
      document.getElementById("FormRank").style.color = "#333333";
    } else {
      document.getElementById("FormRank").style.backgroundColor = "#77cf9c";
      document.getElementById("FormRank").style.color = "#333333";
    }
  }
}