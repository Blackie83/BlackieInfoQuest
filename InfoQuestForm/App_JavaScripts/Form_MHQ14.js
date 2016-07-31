
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
  if (document.getElementById("FormView_MHQ14_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_MHQ14_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_MHQ14_Form_TextBox_" + FormMode + "ADMSDate").value == "" || document.getElementById("FormView_MHQ14_Form_TextBox_" + FormMode + "ADMSDate").value == "yyyy/mm/dd") {
      document.getElementById("FormADMSDate").style.backgroundColor = "#d46e6e";
      document.getElementById("FormADMSDate").style.color = "#333333";
    } else {
      document.getElementById("FormADMSDate").style.backgroundColor = "#77cf9c";
      document.getElementById("FormADMSDate").style.color = "#333333";
    }

    var Completed = "0";
    var a = 0;
    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSDiagnosisQ1")) {
      Completed = "0";
      for (a = 0; a <= 1; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSDiagnosisQ1_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSDiagnosisQ1").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSDiagnosisQ1").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSDiagnosisQ1_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSDiagnosisQ1").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSDiagnosisQ1").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSDiagnosisQ2")) {
      Completed = "0";
      for (a = 0; a <= 1; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSDiagnosisQ2_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSDiagnosisQ2").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSDiagnosisQ2").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSDiagnosisQ2_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSDiagnosisQ2").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSDiagnosisQ2").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSDiagnosisQ3")) {
      Completed = "0";
      for (a = 0; a <= 1; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSDiagnosisQ3_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSDiagnosisQ3").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSDiagnosisQ3").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSDiagnosisQ3_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSDiagnosisQ3").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSDiagnosisQ3").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSDiagnosisQ4List")) {
      Completed = "0";
      for (a = 0; a <= document.getElementsByName("FormView_MHQ14_Form$RadioButtonList_" + FormMode + "ADMSDiagnosisQ4List").length - 1; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSDiagnosisQ4List_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSDiagnosisQ4List").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSDiagnosisQ4List").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSDiagnosisQ4List_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSDiagnosisQ4List").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSDiagnosisQ4List").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1A")) {
      Completed = "0";
      for (a = 0; a <= 6; a++) {        
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1A_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ1A").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ1A").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1A_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ1A").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ1A").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1B")) {
      Completed = "0";
      for (a = 0; a <= 6; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1B_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ1B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ1B").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1B_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ1B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ1B").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1C")) {
      Completed = "0";
      for (a = 0; a <= 6; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1C_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ1C").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ1C").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1C_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ1C").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ1C").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1D")) {
      Completed = "0";
      for (a = 0; a <= 6; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1D_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ1D").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ1D").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1D_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ1D").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ1D").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1E")) {
      Completed = "0";
      for (a = 0; a <= 6; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1E_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ1E").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ1E").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1E_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ1E").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ1E").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2A")) {
      Completed = "0";
      for (a = 0; a <= 6; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2A_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ2A").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ2A").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2A_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ2A").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ2A").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2B")) {
      Completed = "0";
      for (a = 0; a <= 6; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2B_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ2B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ2B").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2B_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ2B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ2B").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2C")) {
      Completed = "0";
      for (a = 0; a <= 6; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2C_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ2C").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ2C").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2C_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ2C").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ2C").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2D")) {
      Completed = "0";
      for (a = 0; a <= 6; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2D_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ2D").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ2D").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2D_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ2D").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ2D").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ3A")) {
      Completed = "0";
      for (a = 0; a <= 6; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ3A_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ3A").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ3A").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ3A_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ3A").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ3A").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ3B")) {
      Completed = "0";
      for (a = 0; a <= 5; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ3B_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ3B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ3B").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ3B_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ3B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ3B").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4A")) {
      Completed = "0";
      for (a = 0; a <= 2; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4A_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ4A").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ4A").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4A_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ4A").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ4A").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4B")) {
      Completed = "0";
      for (a = 0; a <= 2; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4B_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ4B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ4B").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4B_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ4B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ4B").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4C")) {
      Completed = "0";
      for (a = 0; a <= 2; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4C_" + a + "").checked == true) {
          Completed = "1";
          document.getElementById("FormADMSQ4C").style.backgroundColor = "#77cf9c";
          document.getElementById("FormADMSQ4C").style.color = "#333333";
        } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4C_" + a + "").checked == false && Completed == "0") {
          document.getElementById("FormADMSQ4C").style.backgroundColor = "#d46e6e";
          document.getElementById("FormADMSQ4C").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_DropDownList_" + FormMode + "DISCHCompleteDischarge")) {
      if (document.getElementById("FormView_MHQ14_Form_DropDownList_" + FormMode + "DISCHCompleteDischarge").value == "") {
        document.getElementById("FormDISCHDate").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHDate").style.color = "#333333";
        document.getElementById("FormDISCHNoDischargeReasonList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHNoDischargeReasonList").style.color = "#333333";
        document.getElementById("FormDISCHDiagnosisQ1").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHDiagnosisQ1").style.color = "#333333";
        document.getElementById("FormDISCHDiagnosisQ2").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHDiagnosisQ2").style.color = "#333333";
        document.getElementById("FormDISCHDiagnosisQ3").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHDiagnosisQ3").style.color = "#333333";
        document.getElementById("FormDISCHDiagnosisQ4List").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHDiagnosisQ4List").style.color = "#333333";
        document.getElementById("FormDISCHQ1A").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ1A").style.color = "#333333";
        document.getElementById("FormDISCHQ1B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ1B").style.color = "#333333";
        document.getElementById("FormDISCHQ1C").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ1C").style.color = "#333333";
        document.getElementById("FormDISCHQ1D").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ1D").style.color = "#333333";
        document.getElementById("FormDISCHQ1E").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ1E").style.color = "#333333";
        document.getElementById("FormDISCHQ2A").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ2A").style.color = "#333333";
        document.getElementById("FormDISCHQ2B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ2B").style.color = "#333333";
        document.getElementById("FormDISCHQ2C").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ2C").style.color = "#333333";
        document.getElementById("FormDISCHQ2D").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ2D").style.color = "#333333";
        document.getElementById("FormDISCHQ3A").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ3A").style.color = "#333333";
        document.getElementById("FormDISCHQ3B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ3B").style.color = "#333333";
        document.getElementById("FormDISCHQ4A").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ4A").style.color = "#333333";
        document.getElementById("FormDISCHQ4B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ4B").style.color = "#333333";
        document.getElementById("FormDISCHQ4C").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ4C").style.color = "#333333";
      } else if (document.getElementById("FormView_MHQ14_Form_DropDownList_" + FormMode + "DISCHCompleteDischarge").value == "No") {
        if (document.getElementById("FormView_MHQ14_Form_TextBox_" + FormMode + "DISCHDate").value == "" || document.getElementById("FormView_MHQ14_Form_TextBox_" + FormMode + "DISCHDate").value == "yyyy/mm/dd") {
          document.getElementById("FormDISCHDate").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDISCHDate").style.color = "#333333";
        } else {
          document.getElementById("FormDISCHDate").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDISCHDate").style.color = "#333333";
        }

        if (document.getElementById("FormView_MHQ14_Form_DropDownList_" + FormMode + "DISCHNoDischargeReasonList").value == "") {
          document.getElementById("FormDISCHNoDischargeReasonList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDISCHNoDischargeReasonList").style.color = "#333333";
        } else {
          document.getElementById("FormDISCHNoDischargeReasonList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDISCHNoDischargeReasonList").style.color = "#333333";
        }

        document.getElementById("FormDISCHDiagnosisQ1").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHDiagnosisQ1").style.color = "#333333";
        document.getElementById("FormDISCHDiagnosisQ2").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHDiagnosisQ2").style.color = "#333333";
        document.getElementById("FormDISCHDiagnosisQ3").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHDiagnosisQ3").style.color = "#333333";
        document.getElementById("FormDISCHDiagnosisQ4List").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHDiagnosisQ4List").style.color = "#333333";
        document.getElementById("FormDISCHQ1A").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ1A").style.color = "#333333";
        document.getElementById("FormDISCHQ1B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ1B").style.color = "#333333";
        document.getElementById("FormDISCHQ1C").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ1C").style.color = "#333333";
        document.getElementById("FormDISCHQ1D").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ1D").style.color = "#333333";
        document.getElementById("FormDISCHQ1E").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ1E").style.color = "#333333";
        document.getElementById("FormDISCHQ2A").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ2A").style.color = "#333333";
        document.getElementById("FormDISCHQ2B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ2B").style.color = "#333333";
        document.getElementById("FormDISCHQ2C").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ2C").style.color = "#333333";
        document.getElementById("FormDISCHQ2D").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ2D").style.color = "#333333";
        document.getElementById("FormDISCHQ3A").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ3A").style.color = "#333333";
        document.getElementById("FormDISCHQ3B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ3B").style.color = "#333333";
        document.getElementById("FormDISCHQ4A").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ4A").style.color = "#333333";
        document.getElementById("FormDISCHQ4B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ4B").style.color = "#333333";
        document.getElementById("FormDISCHQ4C").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHQ4C").style.color = "#333333";
      } else if (document.getElementById("FormView_MHQ14_Form_DropDownList_" + FormMode + "DISCHCompleteDischarge").value == "Yes") {
        if (document.getElementById("FormView_MHQ14_Form_TextBox_" + FormMode + "DISCHDate").value == "" || document.getElementById("FormView_MHQ14_Form_TextBox_" + FormMode + "DISCHDate").value == "yyyy/mm/dd") {
          document.getElementById("FormDISCHDate").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDISCHDate").style.color = "#333333";
        } else {
          document.getElementById("FormDISCHDate").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDISCHDate").style.color = "#333333";
        }

        document.getElementById("FormDISCHNoDischargeReasonList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDISCHNoDischargeReasonList").style.color = "#333333";

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ1")) {
          Completed = "0";
          for (a = 0; a <= 1; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ1_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHDiagnosisQ1").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHDiagnosisQ1").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ1_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHDiagnosisQ1").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHDiagnosisQ1").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ2")) {
          Completed = "0";
          for (a = 0; a <= 1; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ2_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHDiagnosisQ2").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHDiagnosisQ2").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ2_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHDiagnosisQ2").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHDiagnosisQ2").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ3")) {
          Completed = "0";
          for (a = 0; a <= 1; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ3_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHDiagnosisQ3").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHDiagnosisQ3").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ3_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHDiagnosisQ3").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHDiagnosisQ3").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List")) {
          Completed = "0";
          for (a = 0; a <= document.getElementsByName("FormView_MHQ14_Form$RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List").length - 1; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHDiagnosisQ4List").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHDiagnosisQ4List").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHDiagnosisQ4List").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHDiagnosisQ4List").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A")) {
          Completed = "0";
          for (a = 0; a <= 6; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ1A").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ1A").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ1A").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ1A").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B")) {
          Completed = "0";
          for (a = 0; a <= 6; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ1B").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ1B").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ1B").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ1B").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C")) {
          Completed = "0";
          for (a = 0; a <= 6; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ1C").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ1C").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ1C").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ1C").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D")) {
          Completed = "0";
          for (a = 0; a <= 6; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ1D").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ1D").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ1D").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ1D").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E")) {
          Completed = "0";
          for (a = 0; a <= 6; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ1E").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ1E").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ1E").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ1E").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A")) {
          Completed = "0";
          for (a = 0; a <= 6; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ2A").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ2A").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ2A").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ2A").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B")) {
          Completed = "0";
          for (a = 0; a <= 6; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ2B").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ2B").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ2B").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ2B").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C")) {
          Completed = "0";
          for (a = 0; a <= 6; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ2C").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ2C").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ2C").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ2C").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D")) {
          Completed = "0";
          for (a = 0; a <= 6; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ2D").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ2D").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ2D").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ2D").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A")) {
          Completed = "0";
          for (a = 0; a <= 6; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ3A").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ3A").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ3A").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ3A").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B")) {
          Completed = "0";
          for (a = 0; a <= 5; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ3B").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ3B").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ3B").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ3B").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A")) {
          Completed = "0";
          for (a = 0; a <= 2; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ4A").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ4A").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ4A").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ4A").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B")) {
          Completed = "0";
          for (a = 0; a <= 2; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ4B").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ4B").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ4B").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ4B").style.color = "#333333";
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C")) {
          Completed = "0";
          for (a = 0; a <= 2; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C_" + a + "").checked == true) {
              Completed = "1";
              document.getElementById("FormDISCHQ4C").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDISCHQ4C").style.color = "#333333";
            } else if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C_" + a + "").checked == false && Completed == "0") {
              document.getElementById("FormDISCHQ4C").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDISCHQ4C").style.color = "#333333";
            }
          }
        }
      }
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_Form------------------------------------------------------------------------------------------------------------------------------
function Calculation_Form() {
  var FormMode;
  if (document.getElementById("FormView_MHQ14_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_MHQ14_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    var a = 0;

    var ADMSSection1Score = 0;
    var ADMSSection1Numerator = 0;
    var ADMSSection1Denominator = 0;

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1A")) {
      for (a = 0; a <= 5; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1A_" + a + "").checked == true) {
          ADMSSection1Numerator = ADMSSection1Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1A_" + a + "").value);
          ADMSSection1Denominator = ADMSSection1Denominator + 1;
        } 
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1B")) {
      for (a = 0; a <= 5; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1B_" + a + "").checked == true) {
          ADMSSection1Numerator = ADMSSection1Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1B_" + a + "").value);
          ADMSSection1Denominator = ADMSSection1Denominator + 1;
        } 
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1C")) {
      for (a = 0; a <= 5; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1C_" + a + "").checked == true) {
          ADMSSection1Numerator = ADMSSection1Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1C_" + a + "").value);
          ADMSSection1Denominator = ADMSSection1Denominator + 1;
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1D")) {
      for (a = 0; a <= 5; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1D_" + a + "").checked == true) {
          ADMSSection1Numerator = ADMSSection1Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1D_" + a + "").value);
          ADMSSection1Denominator = ADMSSection1Denominator + 1;
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1E")) {
      for (a = 0; a <= 5; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1E_" + a + "").checked == true) {
          ADMSSection1Numerator = ADMSSection1Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ1E_" + a + "").value);
          ADMSSection1Denominator = ADMSSection1Denominator + 1;
        }
      }
    }

    if (ADMSSection1Denominator == 0) {
      ADMSSection1Score = 0;
    } else {
      ADMSSection1Score = ADMSSection1Numerator / ADMSSection1Denominator;
    }

    if (document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "ADMSSection1Score")) {
      document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "ADMSSection1Score").value = ADMSSection1Score.toFixed(2) + "";
    }

    var ADMSSection2Score = 0;
    var ADMSSection2Numerator = 0;
    var ADMSSection2Denominator = 0;

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2A")) {
      for (a = 0; a <= 5; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2A_" + a + "").checked == true) {
          ADMSSection2Numerator = ADMSSection2Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2A_" + a + "").value);
          ADMSSection2Denominator = ADMSSection2Denominator + 1;
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2B")) {
      for (a = 0; a <= 5; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2B_" + a + "").checked == true) {
          ADMSSection2Numerator = ADMSSection2Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2B_" + a + "").value);
          ADMSSection2Denominator = ADMSSection2Denominator + 1;
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2C")) {
      for (a = 0; a <= 5; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2C_" + a + "").checked == true) {
          ADMSSection2Numerator = ADMSSection2Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2C_" + a + "").value);
          ADMSSection2Denominator = ADMSSection2Denominator + 1;
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2D")) {
      for (a = 0; a <= 5; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2D_" + a + "").checked == true) {
          ADMSSection2Numerator = ADMSSection2Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ2D_" + a + "").value);
          ADMSSection2Denominator = ADMSSection2Denominator + 1;
        }
      }
    }

    if (ADMSSection2Denominator == 0) {
      ADMSSection2Score = 0;
    } else {
      ADMSSection2Score = ADMSSection2Numerator / ADMSSection2Denominator;
    }

    if (document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "ADMSSection2Score")) {
      document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "ADMSSection2Score").value = ADMSSection2Score.toFixed(2) + "";
    }


    var ADMSSection3Score = 0;
    var ADMSSection3Numerator = 0;
    var ADMSSection3Denominator = 0;

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ3A")) {
      for (a = 0; a <= 5; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ3A_" + a + "").checked == true) {
          ADMSSection3Numerator = ADMSSection3Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ3A_" + a + "").value);
          ADMSSection3Denominator = ADMSSection3Denominator + 1;
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ3B")) {
      for (a = 0; a <= 4; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ3B_" + a + "").checked == true) {
          ADMSSection3Numerator = ADMSSection3Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ3B_" + a + "").value);
          ADMSSection3Denominator = ADMSSection3Denominator + 1;
        }
      }
    }

    if (ADMSSection3Denominator == 0) {
      ADMSSection3Score = 0;
    } else {
      ADMSSection3Score = ADMSSection3Numerator / ADMSSection3Denominator;
    }

    if (document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "ADMSSection3Score")) {
      document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "ADMSSection3Score").value = ADMSSection3Score.toFixed(2) + "";
    }


    var ADMSSection4Score = 0;
    var ADMSSection4Numerator = 0;
    var ADMSSection4Denominator = 0;

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4A")) {
      for (a = 0; a <= 1; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4A_" + a + "").checked == true) {
          ADMSSection4Numerator = ADMSSection4Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4A_" + a + "").value);
          ADMSSection4Denominator = ADMSSection4Denominator + 1;
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4B")) {
      for (a = 0; a <= 1; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4B_" + a + "").checked == true) {
          ADMSSection4Numerator = ADMSSection4Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4B_" + a + "").value);
          ADMSSection4Denominator = ADMSSection4Denominator + 1;
        }
      }
    }

    if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4C")) {
      for (a = 0; a <= 1; a++) {
        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4C_" + a + "").checked == true) {
          ADMSSection4Numerator = ADMSSection4Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "ADMSQ4C_" + a + "").value);
          ADMSSection4Denominator = ADMSSection4Denominator + 1;
        }
      }
    }

    if (ADMSSection4Denominator == 0) {
      ADMSSection4Score = 0;
    } else {
      ADMSSection4Score = ADMSSection4Numerator / ADMSSection4Denominator;
    }

    if (document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "ADMSSection4Score")) {
      document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "ADMSSection4Score").value = ADMSSection4Score.toFixed(2) + "";
    }

    var ADMSScore = ((parseFloat(ADMSSection1Score.toFixed(2)) + parseFloat(ADMSSection2Score.toFixed(2)) + parseFloat(ADMSSection3Score.toFixed(2)) + parseFloat(ADMSSection4Score.toFixed(2))) / 4);

    if (document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "ADMSScore")) {
      document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "ADMSScore").value = ADMSScore.toFixed(2) + "";
    }


    if (document.getElementById("FormView_MHQ14_Form_DropDownList_" + FormMode + "DISCHCompleteDischarge")) {
      if (document.getElementById("FormView_MHQ14_Form_DropDownList_" + FormMode + "DISCHCompleteDischarge").value == "Yes") {
        var b = 0;

        var DISCHSection1Score = 0;
        var DISCHSection1Numerator = 0;
        var DISCHSection1Denominator = 0;

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A")) {
          for (a = 0; a <= 5; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A_" + a + "").checked == true) {
              DISCHSection1Numerator = DISCHSection1Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A_" + a + "").value);
              DISCHSection1Denominator = DISCHSection1Denominator + 1;
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B")) {
          for (a = 0; a <= 5; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B_" + a + "").checked == true) {
              DISCHSection1Numerator = DISCHSection1Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B_" + a + "").value);
              DISCHSection1Denominator = DISCHSection1Denominator + 1;
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C")) {
          for (a = 0; a <= 5; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C_" + a + "").checked == true) {
              DISCHSection1Numerator = DISCHSection1Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C_" + a + "").value);
              DISCHSection1Denominator = DISCHSection1Denominator + 1;
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D")) {
          for (a = 0; a <= 5; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D_" + a + "").checked == true) {
              DISCHSection1Numerator = DISCHSection1Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D_" + a + "").value);
              DISCHSection1Denominator = DISCHSection1Denominator + 1;
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E")) {
          for (a = 0; a <= 5; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E_" + a + "").checked == true) {
              DISCHSection1Numerator = DISCHSection1Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E_" + a + "").value);
              DISCHSection1Denominator = DISCHSection1Denominator + 1;
            }
          }
        }

        if (DISCHSection1Denominator == 0) {
          DISCHSection1Score = 0;
        } else {
          DISCHSection1Score = DISCHSection1Numerator / DISCHSection1Denominator;
        }

        if (document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection1Score")) {
          document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection1Score").value = DISCHSection1Score.toFixed(2) + "";
        }

        var DISCHSection2Score = 0;
        var DISCHSection2Numerator = 0;
        var DISCHSection2Denominator = 0;

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A")) {
          for (a = 0; a <= 5; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A_" + a + "").checked == true) {
              DISCHSection2Numerator = DISCHSection2Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A_" + a + "").value);
              DISCHSection2Denominator = DISCHSection2Denominator + 1;
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B")) {
          for (a = 0; a <= 5; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B_" + a + "").checked == true) {
              DISCHSection2Numerator = DISCHSection2Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B_" + a + "").value);
              DISCHSection2Denominator = DISCHSection2Denominator + 1;
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C")) {
          for (a = 0; a <= 5; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C_" + a + "").checked == true) {
              DISCHSection2Numerator = DISCHSection2Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C_" + a + "").value);
              DISCHSection2Denominator = DISCHSection2Denominator + 1;
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D")) {
          for (a = 0; a <= 5; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D_" + a + "").checked == true) {
              DISCHSection2Numerator = DISCHSection2Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D_" + a + "").value);
              DISCHSection2Denominator = DISCHSection2Denominator + 1;
            }
          }
        }

        if (DISCHSection2Denominator == 0) {
          DISCHSection2Score = 0;
        } else {
          DISCHSection2Score = DISCHSection2Numerator / DISCHSection2Denominator;
        }

        if (document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection2Score")) {
          document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection2Score").value = DISCHSection2Score.toFixed(2) + "";
        }


        var DISCHSection3Score = 0;
        var DISCHSection3Numerator = 0;
        var DISCHSection3Denominator = 0;

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A")) {
          for (a = 0; a <= 5; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A_" + a + "").checked == true) {
              DISCHSection3Numerator = DISCHSection3Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A_" + a + "").value);
              DISCHSection3Denominator = DISCHSection3Denominator + 1;
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B")) {
          for (a = 0; a <= 4; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B_" + a + "").checked == true) {
              DISCHSection3Numerator = DISCHSection3Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B_" + a + "").value);
              DISCHSection3Denominator = DISCHSection3Denominator + 1;
            }
          }
        }

        if (DISCHSection3Denominator == 0) {
          DISCHSection3Score = 0;
        } else {
          DISCHSection3Score = DISCHSection3Numerator / DISCHSection3Denominator;
        }

        if (document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection3Score")) {
          document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection3Score").value = DISCHSection3Score.toFixed(2) + "";
        }


        var DISCHSection4Score = 0;
        var DISCHSection4Numerator = 0;
        var DISCHSection4Denominator = 0;

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A")) {
          for (a = 0; a <= 1; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A_" + a + "").checked == true) {
              DISCHSection4Numerator = DISCHSection4Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A_" + a + "").value);
              DISCHSection4Denominator = DISCHSection4Denominator + 1;
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B")) {
          for (a = 0; a <= 1; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B_" + a + "").checked == true) {
              DISCHSection4Numerator = DISCHSection4Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B_" + a + "").value);
              DISCHSection4Denominator = DISCHSection4Denominator + 1;
            }
          }
        }

        if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C")) {
          for (a = 0; a <= 1; a++) {
            if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C_" + a + "").checked == true) {
              DISCHSection4Numerator = DISCHSection4Numerator + parseInt(document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C_" + a + "").value);
              DISCHSection4Denominator = DISCHSection4Denominator + 1;
            }
          }
        }

        if (DISCHSection4Denominator == 0) {
          DISCHSection4Score = 0;
        } else {
          DISCHSection4Score = DISCHSection4Numerator / DISCHSection4Denominator;
        }

        if (document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection4Score")) {
          document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection4Score").value = DISCHSection4Score.toFixed(2) + "";
        }


        var DISCHScore = ((parseFloat(DISCHSection1Score.toFixed(2)) + parseFloat(DISCHSection2Score.toFixed(2)) + parseFloat(DISCHSection3Score.toFixed(2)) + parseFloat(DISCHSection4Score.toFixed(2))) / 4);

        if (document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHScore")) {
          document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHScore").value = DISCHScore.toFixed(2) + "";
        }


        var DISCHDifference = (parseFloat(DISCHScore.toFixed(2)) - parseFloat(ADMSScore.toFixed(2)));

        if (document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHDifference")) {
          document.getElementById("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHDifference").value = DISCHDifference.toFixed(2) + "";
        }
      }
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form() {
  var FormMode;
  if (document.getElementById("FormView_MHQ14_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else if (document.getElementById("FormView_MHQ14_Form_HiddenField_Item")) {
    FormMode = "Item"
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (FormMode != "Item") {
      if (document.getElementById("FormView_MHQ14_Form_DropDownList_" + FormMode + "DISCHCompleteDischarge").value == "") {
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDateText");
        Hide("FormView_MHQ14_Form_TextBox_" + FormMode + "DISCHDate");
        Hide("FormView_MHQ14_Form_ImageButton_" + FormMode + "DISCHDate");

        if (document.getElementById("FormView_MHQ14_Form_TextBox_" + FormMode + "DISCHDate").value != "yyyy/mm/dd") {
          document.getElementById("FormView_MHQ14_Form_TextBox_" + FormMode + "DISCHDate").value = "";
        }

        Hide("MHQ14QuesionnaireDISCHNoDischargeReasonList1");
        Hide("MHQ14QuesionnaireDISCHNoDischargeReasonList2");
        document.getElementById("FormView_MHQ14_Form_DropDownList_" + FormMode + "DISCHNoDischargeReasonList").value = "";
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ1Text");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ1");

        for (a = 0; a <= 1; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ1_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ1_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ2Text");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ2");

        for (a = 0; a <= 1; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ2_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ2_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ3Text");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ3");

        for (a = 0; a <= 1; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ3_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ3_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ4ListText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List");

        for (a = 0; a <= document.getElementsByName("FormView_MHQ14_Form$RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List").length - 1; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1AText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1BText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1CText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1DText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1EText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection1ScoreText");
        Hide("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection1Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2AText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2BText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2CText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2DText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection2ScoreText");
        Hide("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection2Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3AText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3BText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B");

        for (a = 0; a <= 5; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection3ScoreText");
        Hide("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection3Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4AText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A");

        for (a = 0; a <= 2; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4BText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B");

        for (a = 0; a <= 2; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4CText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C");

        for (a = 0; a <= 2; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection4ScoreText");
        Hide("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection4Score");


        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHScoreText");
        Hide("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHScore");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDifferenceText");
        Hide("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHDifference");
      } else if (document.getElementById("FormView_MHQ14_Form_DropDownList_" + FormMode + "DISCHCompleteDischarge").value == "No") {
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDateText");
        Show("FormView_MHQ14_Form_TextBox_" + FormMode + "DISCHDate");
        Show("FormView_MHQ14_Form_ImageButton_" + FormMode + "DISCHDate");
        Show("MHQ14QuesionnaireDISCHNoDischargeReasonList1");
        Show("MHQ14QuesionnaireDISCHNoDischargeReasonList2");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ1Text");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ1");

        for (a = 0; a <= 1; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ1_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ1_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ2Text");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ2");

        for (a = 0; a <= 1; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ2_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ2_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ3Text");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ3");

        for (a = 0; a <= 1; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ3_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ3_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ4ListText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List");

        for (a = 0; a <= document.getElementsByName("FormView_MHQ14_Form$RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List").length - 1; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1AText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1BText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1CText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1DText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1EText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection1ScoreText");
        Hide("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection1Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2AText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2BText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2CText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2DText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection2ScoreText");
        Hide("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection2Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3AText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A");

        for (a = 0; a <= 6; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3BText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B");

        for (a = 0; a <= 5; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection3ScoreText");
        Hide("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection3Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4AText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A");

        for (a = 0; a <= 2; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4BText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B");

        for (a = 0; a <= 2; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4CText");
        Hide("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C");

        for (a = 0; a <= 2; a++) {
          if (document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C_" + a + "").checked == true) {
            document.getElementById("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C_" + a + "").checked = false;
          }
        }

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection4ScoreText");
        Hide("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection4Score");

        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHScoreText");
        Hide("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHScore");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDifferenceText");
        Hide("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHDifference");
      } else if (document.getElementById("FormView_MHQ14_Form_DropDownList_" + FormMode + "DISCHCompleteDischarge").value == "Yes") {
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDateText");
        Show("FormView_MHQ14_Form_TextBox_" + FormMode + "DISCHDate");
        Show("FormView_MHQ14_Form_ImageButton_" + FormMode + "DISCHDate");
        Hide("MHQ14QuesionnaireDISCHNoDischargeReasonList1");
        Hide("MHQ14QuesionnaireDISCHNoDischargeReasonList2");
        document.getElementById("FormView_MHQ14_Form_DropDownList_" + FormMode + "DISCHNoDischargeReasonList").value = "";
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ1Text");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ1");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ2Text");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ2");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ3Text");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ3");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ4ListText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHDiagnosisQ4List");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1Text");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1AText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1A");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1BText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1B");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1CText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1C");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1DText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1D");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1EText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ1E");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection1ScoreText");
        Show("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection1Score");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2Text");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2AText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2A");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2BText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2B");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2CText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2C");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2DText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ2D");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection2ScoreText");
        Show("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection2Score");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3AText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3A");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3BText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ3B");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection3ScoreText");
        Show("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection3Score");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4Text");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4AText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4A");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4BText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4B");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4CText");
        Show("FormView_MHQ14_Form_RadioButtonList_" + FormMode + "DISCHQ4C");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection4ScoreText");
        Show("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHSection4Score");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHScoreText");
        Show("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHScore");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDifferenceText");
        Show("FormView_MHQ14_Form_Textbox_" + FormMode + "DISCHDifference");
      }
    }

    if (FormMode == "Item") {
      if (document.getElementById("FormView_MHQ14_Form_HiddenField_" + FormMode + "DISCHCompleteDischarge").value == "") {
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDateText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDate");
        Hide("MHQ14QuesionnaireDISCHNoDischargeReasonList1");
        Hide("MHQ14QuesionnaireDISCHNoDischargeReasonList2");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ1Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ1");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ2Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ2");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ3Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ3");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ4ListText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ4List");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1AText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1A");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1BText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1B");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1CText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1C");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1DText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1D");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1EText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1E");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection1ScoreText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection1Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2AText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2A");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2BText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2B");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2CText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2C");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2DText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2D");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection2ScoreText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection2Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3AText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3A");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3BText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3B");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection3ScoreText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection3Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4AText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4A");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4BText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4B");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4CText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4C");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection4ScoreText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection4Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHScoreText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHScore");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDifferenceText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDifference");
      } else if (document.getElementById("FormView_MHQ14_Form_HiddenField_" + FormMode + "DISCHCompleteDischarge").value == "No") {
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDateText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDate");
        Show("MHQ14QuesionnaireDISCHNoDischargeReasonList1");
        Show("MHQ14QuesionnaireDISCHNoDischargeReasonList2");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ1Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ1");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ2Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ2");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ3Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ3");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ4ListText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ4List");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1AText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1A");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1BText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1B");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1CText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1C");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1DText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1D");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1EText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1E");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection1ScoreText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection1Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2AText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2A");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2BText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2B");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2CText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2C");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2DText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2D");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection2ScoreText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection2Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3AText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3A");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3BText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3B");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection3ScoreText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection3Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4Text");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4AText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4A");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4BText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4B");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4CText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4C");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection4ScoreText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection4Score");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHScoreText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHScore");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDifferenceText");
        Hide("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDifference");
      } else if (document.getElementById("FormView_MHQ14_Form_HiddenField_" + FormMode + "DISCHCompleteDischarge").value == "Yes") {
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDateText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDate");
        Hide("MHQ14QuesionnaireDISCHNoDischargeReasonList1");
        Hide("MHQ14QuesionnaireDISCHNoDischargeReasonList2");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ1Text");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ1");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ2Text");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ2");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ3Text");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ3");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ4ListText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDiagnosisQ4List");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1Text");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1AText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1A");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1BText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1B");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1CText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1C");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1DText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1D");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1EText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ1E");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection1ScoreText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection1Score");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2Text");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2AText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2A");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2BText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2B");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2CText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2C");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2DText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ2D");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection2ScoreText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection2Score");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3AText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3A");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3BText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ3B");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection3ScoreText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection3Score");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4Text");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4AText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4A");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4BText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4B");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4CText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHQ4C");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection4ScoreText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHSection4Score");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHScoreText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHScore");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDifferenceText");
        Show("FormView_MHQ14_Form_Label_" + FormMode + "DISCHDifference");
      }
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