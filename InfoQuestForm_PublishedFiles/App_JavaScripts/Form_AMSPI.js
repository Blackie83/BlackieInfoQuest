
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --FormEmail-------------------------------------------------------------------------------------------------------------------------------------
function FormEmail(EmailLink) {
  var width = 750;
  var height = 700;
  var left = (screen.width - width) / 2;
  var top = (screen.height - height) / 2;
  window.open(EmailLink, 'Email', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=No , location=No , scrollbars=Yes , resizable=No , status=Yes , left=' + left + ' , top=' + top + ' ');
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --FormPrint-------------------------------------------------------------------------------------------------------------------------------------
function FormPrint(PrintLink) {
  var width = 750;
  var height = 700;
  var left = (screen.width - width) / 2;
  var top = (screen.height - height) / 2;
  window.open(PrintLink, 'Email', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=No , location=No , scrollbars=Yes , resizable=No , status=Yes , left=' + left + ' , top=' + top + ' ');
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --QueryStringValue------------------------------------------------------------------------------------------------------------------------------
function QueryStringValue(FindName) {
  var QueryString = window.location.search.substring(1);
  var QueryStringSplit = QueryString.split("&");
  for (var a = 0; a < QueryStringSplit.length; a++) {
    var QueryStringValue = QueryStringSplit[a].split("=");
    if (QueryStringValue[0] == FindName) {
      return QueryStringValue[1];
    }
  }
  return null;
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Search-----------------------------------------------------------------------------------------------------------------------------
function Validation_Search() {
  if (document.getElementById("DropDownList_Facility").value == "") {
    document.getElementById("SearchFacility").style.backgroundColor = "#d46e6e";
    document.getElementById("SearchFacility").style.color = "#333333";
  } else {
    document.getElementById("SearchFacility").style.backgroundColor = "#77cf9c";
    document.getElementById("SearchFacility").style.color = "#333333";
  }

  if (document.getElementById("TextBox_PatientVisitNumber").value == "") {
    document.getElementById("SearchPatientVisitNumber").style.backgroundColor = "#d46e6e";
    document.getElementById("SearchPatientVisitNumber").style.color = "#333333";
  } else {
    document.getElementById("SearchPatientVisitNumber").style.backgroundColor = "#77cf9c";
    document.getElementById("SearchPatientVisitNumber").style.color = "#333333";
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form() {
  var FormMode;
  if (document.getElementById("FormView_AMSPI_Form_TextBox_InsertDate")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_AMSPI_Form_TextBox_EditDate")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_AMSPI_Form_TextBox_" + FormMode + "Date").value == "") {
      document.getElementById("FormDate").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDate").style.color = "#333333";
    } else {
      document.getElementById("FormDate").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDate").style.color = "#333333";
    }

    if (document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Unit").value == "") {
      document.getElementById("FormUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUnit").style.color = "#333333";
      document.getElementById("FormUnit").style.height = "24px";
    } else {
      document.getElementById("FormUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUnit").style.color = "#333333";
      document.getElementById("FormUnit").style.height = "24px";
    }

    if (document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "CommunicationList").value == "") {
      document.getElementById("FormCommunicationList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCommunicationList").style.color = "#333333";
      document.getElementById("FormCommunicationList").style.height = "24px";
    } else {
      document.getElementById("FormCommunicationList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCommunicationList").style.color = "#333333";
      document.getElementById("FormCommunicationList").style.height = "24px";
    }

    if (document.getElementById("FormView_AMSPI_Form_TextBox_" + FormMode + "Time").value == "") {
      document.getElementById("FormTime").style.backgroundColor = "#d46e6e";
      document.getElementById("FormTime").style.color = "#333333";
      document.getElementById("FormTime").style.height = "24px";
    } else {
      document.getElementById("FormTime").style.backgroundColor = "#77cf9c";
      document.getElementById("FormTime").style.color = "#333333";
      document.getElementById("FormTime").style.height = "24px";
    }

    if (document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "InterventionList").value == "") {
      document.getElementById("FormInterventionList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormInterventionList").style.color = "#333333";
      document.getElementById("FormInterventionList").style.height = "24px";
    } else {
      document.getElementById("FormInterventionList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormInterventionList").style.color = "#333333";
      document.getElementById("FormInterventionList").style.height = "24px";
    }

    if (document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "InterventionInList").value == "") {
      document.getElementById("FormInterventionInList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormInterventionInList").style.color = "#333333";
      document.getElementById("FormInterventionInList").style.height = "24px";
    } else {
      document.getElementById("FormInterventionInList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormInterventionInList").style.color = "#333333";
      document.getElementById("FormInterventionInList").style.height = "24px";
    }

    var TypeTotal = 0;
    for (a = 1; a <= 11; a++) {
      if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type" + a + "").checked == true) {
        TypeTotal = TypeTotal + 1;
      }
    }

    if (TypeTotal > 0) {
      document.getElementById("FormType").style.backgroundColor = "#77cf9c";
      document.getElementById("FormType").style.color = "#333333";
      document.getElementById("FormType").style.height = "24px";
    } else {
      document.getElementById("FormType").style.backgroundColor = "#d46e6e";
      document.getElementById("FormType").style.color = "#333333";
      document.getElementById("FormType").style.height = "24px";
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type1").checked == true) {
      if (document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type1List").value == "") {
        document.getElementById("FormType1").style.backgroundColor = "#d46e6e";
        document.getElementById("FormType1").style.color = "#333333";
        document.getElementById("FormType1").style.height = "24px";
      } else {
        document.getElementById("FormType1").style.backgroundColor = "#77cf9c";
        document.getElementById("FormType1").style.color = "#333333";
        document.getElementById("FormType1").style.height = "24px";
      }
    } else {
      document.getElementById("FormType1").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormType1").style.color = "#000000";
      document.getElementById("FormType1").style.height = "24px";
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type2").checked == true) {
      if (document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type2List").value == "") {
        document.getElementById("FormType2").style.backgroundColor = "#d46e6e";
        document.getElementById("FormType2").style.color = "#333333";
        document.getElementById("FormType2").style.height = "24px";
      } else {
        document.getElementById("FormType2").style.backgroundColor = "#77cf9c";
        document.getElementById("FormType2").style.color = "#333333";
        document.getElementById("FormType2").style.height = "24px";
      }
    } else {
      document.getElementById("FormType2").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormType2").style.color = "#000000";
      document.getElementById("FormType2").style.height = "24px";
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type3").checked == true) {
      if (document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type3List").value == "") {
        document.getElementById("FormType3").style.backgroundColor = "#d46e6e";
        document.getElementById("FormType3").style.color = "#333333";
        document.getElementById("FormType3").style.height = "24px";
      } else {
        document.getElementById("FormType3").style.backgroundColor = "#77cf9c";
        document.getElementById("FormType3").style.color = "#333333";
        document.getElementById("FormType3").style.height = "24px";
      }
    } else {
      document.getElementById("FormType3").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormType3").style.color = "#000000";
      document.getElementById("FormType3").style.height = "24px";
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type4").checked == true) {
      if (document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type4List").value == "") {
        document.getElementById("FormType4").style.backgroundColor = "#d46e6e";
        document.getElementById("FormType4").style.color = "#333333";
        document.getElementById("FormType4").style.height = "24px";
      } else {
        document.getElementById("FormType4").style.backgroundColor = "#77cf9c";
        document.getElementById("FormType4").style.color = "#333333";
        document.getElementById("FormType4").style.height = "24px";
      }
    } else {
      document.getElementById("FormType4").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormType4").style.color = "#000000";
      document.getElementById("FormType4").style.height = "24px";
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type7").checked == true) {
      if (document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type7List").value == "") {
        document.getElementById("FormType7").style.backgroundColor = "#d46e6e";
        document.getElementById("FormType7").style.color = "#333333";
        document.getElementById("FormType7").style.height = "24px";
      } else {
        document.getElementById("FormType7").style.backgroundColor = "#77cf9c";
        document.getElementById("FormType7").style.color = "#333333";
        document.getElementById("FormType7").style.height = "24px";
      }
    } else {
      document.getElementById("FormType7").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormType7").style.color = "#000000";
      document.getElementById("FormType7").style.height = "24px";
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type9").checked == true) {
      if (document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type9List").value == "") {
        document.getElementById("FormType9").style.backgroundColor = "#d46e6e";
        document.getElementById("FormType9").style.color = "#333333";
        document.getElementById("FormType9").style.height = "24px";
      } else {
        document.getElementById("FormType9").style.backgroundColor = "#77cf9c";
        document.getElementById("FormType9").style.color = "#333333";
        document.getElementById("FormType9").style.height = "24px";
      }
    } else {
      document.getElementById("FormType9").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormType9").style.color = "#000000";
      document.getElementById("FormType9").style.height = "24px";
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type10").checked == true) {
      if (document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type10List").value == "") {
        document.getElementById("FormType10").style.backgroundColor = "#d46e6e";
        document.getElementById("FormType10").style.color = "#333333";
        document.getElementById("FormType10").style.height = "24px";
      } else {
        document.getElementById("FormType10").style.backgroundColor = "#77cf9c";
        document.getElementById("FormType10").style.color = "#333333";
        document.getElementById("FormType10").style.height = "24px";
      }
    } else {
      document.getElementById("FormType10").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormType10").style.color = "#000000";
      document.getElementById("FormType10").style.height = "24px";
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type11").checked == true) {
      if (document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type11List").value == "") {
        document.getElementById("FormType11").style.backgroundColor = "#d46e6e";
        document.getElementById("FormType11").style.color = "#333333";
        document.getElementById("FormType11").style.height = "24px";
      } else {
        document.getElementById("FormType11").style.backgroundColor = "#77cf9c";
        document.getElementById("FormType11").style.color = "#333333";
        document.getElementById("FormType11").style.height = "24px";
      }
    } else {
      document.getElementById("FormType11").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormType11").style.color = "#000000";
      document.getElementById("FormType11").style.height = "24px";
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_Form------------------------------------------------------------------------------------------------------------------------------
function Calculation_Form() {
  var FormMode;
  if (document.getElementById("FormView_AMSPI_Form_TextBox_InsertDate")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_AMSPI_Form_TextBox_EditDate")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    var TypeTotal = 0;

    for (a = 1; a <= 11; a++) {
      if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type" + a + "").checked == true) {
        TypeTotal = TypeTotal + 1;
      }
    }

    document.getElementById("FormView_AMSPI_Form_Textbox_" + FormMode + "Total").value = TypeTotal;
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form() {
  var FormMode;
  if (document.getElementById("FormView_AMSPI_Form_TextBox_InsertDate")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_AMSPI_Form_TextBox_EditDate")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type1").checked == true) {
      Show("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type1List");
    } else {
      document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type1List").value = "";
      Hide("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type1List");
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type2").checked == true) {
      Show("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type2List");
    } else {
      document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type2List").value = "";
      Hide("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type2List");
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type3").checked == true) {
      Show("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type3List");
    } else {
      document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type3List").value = "";
      Hide("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type3List");
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type4").checked == true) {
      Show("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type4List");
    } else {
      document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type4List").value = "";
      Hide("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type4List");
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type7").checked == true) {
      Show("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type7List");
    } else {
      document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type7List").value = "";
      Hide("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type7List");
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type9").checked == true) {
      Show("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type9List");
    } else {
      document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type9List").value = "";
      Hide("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type9List");
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type10").checked == true) {
      Show("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type10List");
    } else {
      document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type10List").value = "";
      Hide("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type10List");
    }

    if (document.getElementById("FormView_AMSPI_Form_CheckBox_" + FormMode + "Type11").checked == true) {
      Show("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type11List");
    } else {
      document.getElementById("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type11List").value = "";
      Hide("FormView_AMSPI_Form_DropDownList_" + FormMode + "Type11List");      
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Show------------------------------------------------------------------------------------------------------------------------------------------
function Show(id) {
  if (document.getElementById) {
    document.getElementById(id).style.display = '';
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Hide------------------------------------------------------------------------------------------------------------------------------------------
function Hide(id) {
  if (document.getElementById) {
    document.getElementById(id).style.display = 'none';
  }
}