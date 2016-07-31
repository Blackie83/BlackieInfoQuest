
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form() {
  var FormMode;
  if (document.getElementById("FormView_IPS_Organism_Lookup_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_IPS_Organism_Lookup_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_IPS_Organism_Lookup_Form_TextBox_" + FormMode + "Code").value == "") {
      document.getElementById("FormCode").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCode").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormCode").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCode").style.color = "#333333";
    }

    if (document.getElementById("FormView_IPS_Organism_Lookup_Form_TextBox_" + FormMode + "Description").value == "") {
      document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDescription").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDescription").style.color = "#333333";
    }

    if (document.getElementById("FormView_IPS_Organism_Lookup_Form_DropDownList_" + FormMode + "TypeList").value == "")
    {
      document.getElementById("FormTypeList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormTypeList").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormTypeList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormTypeList").style.color = "#333333";
    }
  }
}
