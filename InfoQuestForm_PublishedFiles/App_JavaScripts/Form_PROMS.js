
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
//----- --GotoBookmark----------------------------------------------------------------------------------------------------------------------------------
function GotoBookmark() {
  if (QueryStringValue("PROMS_Questionnaire_Id") != null) {
    window.location.hash = "LinkPatientInfo";
  }
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
//----- --Validation_Form0------------------------------------------------------------------------------------------------------------------------------
function Validation_Form0() {
  var FormMode;
  if (document.getElementById("FormView_PROMS_Questionnaire_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_PROMS_Questionnaire_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_PROMS_Questionnaire_Form_DropDownList_" + FormMode + "QuestionnaireList").value == "") {
      document.getElementById("Form0Questionnaire").style.backgroundColor = "#d46e6e";
      document.getElementById("Form0Questionnaire").style.color = "#333333";
    } else {
      document.getElementById("Form0Questionnaire").style.backgroundColor = "#77cf9c";
      document.getElementById("Form0Questionnaire").style.color = "#333333";
    }

    if (document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "Q1")) {
      var TotalQuestions = parseInt(document.getElementById("FormView_PROMS_Questionnaire_Form_HiddenField_" + FormMode + "QuestionnaireTotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        var Completed = "0";
        for (var b = 0; b <= 4; b++) {
          if (document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "Q" + a + "_" + b + "").checked == true) {
            Completed = "1";
            document.getElementById("Form0Q" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form0Q" + a + "").style.color = "#333333";
          } else if (document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "Q" + a + "_" + b + "").checked == false && Completed == "0") {
            document.getElementById("Form0Q" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form0Q" + a + "").style.color = "#333333";
          }
        }
      }
    }

    if (document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "ContactPatient_0").checked == false && document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "ContactPatient_1").checked == false) {
      document.getElementById("Form0ContactPatient").style.backgroundColor = "#d46e6e";
      document.getElementById("Form0ContactPatient").style.color = "#333333";
    } else {
      document.getElementById("Form0ContactPatient").style.backgroundColor = "#77cf9c";
      document.getElementById("Form0ContactPatient").style.color = "#333333";
    }

    if (document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "ContactPatient_0").checked == true) {
      if (document.getElementById("FormView_PROMS_Questionnaire_Form_TextBox_" + FormMode + "ContactNumber").value == "") {
        document.getElementById("Form0ContactNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("Form0ContactNumber").style.color = "#333333";
      } else {
        document.getElementById("Form0ContactNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("Form0ContactNumber").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_PROMS_Questionnaire_Form_DropDownList_" + FormMode + "LanguageList").value == "") {
      document.getElementById("Form0Language").style.backgroundColor = "#d46e6e";
      document.getElementById("Form0Language").style.color = "#333333";
    } else {
      document.getElementById("Form0Language").style.backgroundColor = "#77cf9c";
      document.getElementById("Form0Language").style.color = "#333333";
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form1------------------------------------------------------------------------------------------------------------------------------
function Validation_Form1() {
  var FormMode;
  if (document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_PROMS_FollowUp_Form_CheckBox_" + FormMode + "Cancelled").checked == true) {
      if (document.getElementById("FormView_PROMS_FollowUp_Form_DropDownList_" + FormMode + "CancelledList").value == "") {
        document.getElementById("Form1CancelledList").style.backgroundColor = "#d46e6e";
        document.getElementById("Form1CancelledList").style.color = "#333333";
      } else {
        document.getElementById("Form1CancelledList").style.backgroundColor = "#77cf9c";
        document.getElementById("Form1CancelledList").style.color = "#333333";
      }

      var TotalQuestions = parseInt(document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_" + FormMode + "FollowUpTotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        document.getElementById("Form1Q" + a + "").style.backgroundColor = "#f7f7f7";
        document.getElementById("Form1Q" + a + "").style.color = "#000000";
      }
    } else {
      if (document.getElementById("FormView_PROMS_FollowUp_Form_RadioButtonList_" + FormMode + "Q1")) {
        TotalQuestions = parseInt(document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_" + FormMode + "FollowUpTotalQuestions").value);
        for (a = 1; a <= TotalQuestions; a++) {
          var Completed = "0";
          for (var b = 0; b <= 4; b++) {
            if (document.getElementById("FormView_PROMS_FollowUp_Form_RadioButtonList_" + FormMode + "Q" + a + "_" + b + "").checked == true) {
              Completed = "1";
              document.getElementById("Form1Q" + a + "").style.backgroundColor = "#77cf9c";
              document.getElementById("Form1Q" + a + "").style.color = "#333333";
            } else if (document.getElementById("FormView_PROMS_FollowUp_Form_RadioButtonList_" + FormMode + "Q" + a + "_" + b + "").checked == false && Completed == "0") {
              document.getElementById("Form1Q" + a + "").style.backgroundColor = "#d46e6e";
              document.getElementById("Form1Q" + a + "").style.color = "#333333";
            }
          }
        }
      }

      document.getElementById("Form1CancelledList").style.backgroundColor = "#f7f7f7";
      document.getElementById("Form1CancelledList").style.color = "#000000";
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_Form0-----------------------------------------------------------------------------------------------------------------------------
function Calculation_Form0() {
  var FormMode;
  if (document.getElementById("FormView_PROMS_Questionnaire_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_PROMS_Questionnaire_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    var QuestionnaireScore = 0;

    if (document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "Q1")) {
      var TotalQuestions = parseInt(document.getElementById("FormView_PROMS_Questionnaire_Form_HiddenField_" + FormMode + "QuestionnaireTotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        var Completed = "0";
        for (var b = 0; b <= 4; b++) {
          if (document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "Q" + a + "_" + b + "").checked == true) {
            Completed = "1";
            var SelectedIndex = b;
            var SelectedValue = 4 - SelectedIndex;
            QuestionnaireScore = QuestionnaireScore + SelectedValue;
          } else if (document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "Q" + a + "_" + b + "").checked == false && Completed == "0") {
            QuestionnaireScore = QuestionnaireScore
          }
        }
      }

      document.getElementById("FormView_PROMS_Questionnaire_Form_Textbox_" + FormMode + "Score").value = QuestionnaireScore;
    } else {
      document.getElementById("FormView_PROMS_Questionnaire_Form_Textbox_" + FormMode + "Score").value = "";
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_Form1-----------------------------------------------------------------------------------------------------------------------------
function Calculation_Form1() {
  var FormMode;
  if (document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    var FollowUpScore = 0;

    if (document.getElementById("FormView_PROMS_FollowUp_Form_RadioButtonList_" + FormMode + "Q1")) {
      var TotalQuestions = parseInt(document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_" + FormMode + "FollowUpTotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        var Completed = "0";
        for (var b = 0; b <= 4; b++) {
          if (document.getElementById("FormView_PROMS_FollowUp_Form_RadioButtonList_" + FormMode + "Q" + a + "_" + b + "").checked == true) {
            Completed = "1";
            var SelectedIndex = b;
            var SelectedValue = 4 - SelectedIndex;
            FollowUpScore = FollowUpScore + SelectedValue;
          } else if (document.getElementById("FormView_PROMS_FollowUp_Form_RadioButtonList_" + FormMode + "Q" + a + "_" + b + "").checked == false && Completed == "0") {
            FollowUpScore = FollowUpScore
          }
        }
      }

      document.getElementById("FormView_PROMS_FollowUp_Form_Textbox_" + FormMode + "Score").value = FollowUpScore;
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form0--------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form0() {
  var FormMode;
  if (document.getElementById("FormView_PROMS_Questionnaire_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_PROMS_Questionnaire_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_PROMS_Questionnaire_Form_DropDownList_" + FormMode + "QuestionnaireList").value == "") {
      Hide("Form0ShowHideAdmissionDate");
      Hide("Form0ShowHideQ1");
      Hide("Form0ShowHideQ2");
      Hide("Form0ShowHideQ3");
      Hide("Form0ShowHideQ4");
      Hide("Form0ShowHideQ5");
      Hide("Form0ShowHideQ6");
      Hide("Form0ShowHideQ7");
      Hide("Form0ShowHideQ8");
      Hide("Form0ShowHideQ9");
      Hide("Form0ShowHideQ10");
      Hide("Form0ShowHideQ11");
      Hide("Form0ShowHideQ12");
      Hide("Form0ShowHideScore");
      Hide("Form0ShowHideContactPatient");

      document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "ContactPatient_0").checked = false;
      document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "ContactPatient_1").checked = false;
      document.getElementById("FormView_PROMS_Questionnaire_Form_TextBox_" + FormMode + "ContactNumber").value = "";

      Hide("Form0ShowHideContactNumber");
      Hide("Form0ShowHideLanguage");

      document.getElementById("FormView_PROMS_Questionnaire_Form_DropDownList_" + FormMode + "LanguageList").value = "";

      Hide("Form0ShowHideEmailSend");
      Hide("Form0ShowHideEmailDate");
    } else {
      if (document.getElementById("FormView_PROMS_Questionnaire_Form_Textbox_" + FormMode + "Score").value != "") {
        Show("Form0ShowHideAdmissionDate");
        Show("Form0ShowHideQ1");
        Show("Form0ShowHideQ2");
        Show("Form0ShowHideQ3");
        Show("Form0ShowHideQ4");
        Show("Form0ShowHideQ5");
        Show("Form0ShowHideQ6");
        Show("Form0ShowHideQ7");
        Show("Form0ShowHideQ8");
        Show("Form0ShowHideQ9");
        Show("Form0ShowHideQ10");
        Show("Form0ShowHideQ11");
        Show("Form0ShowHideQ12");
        Show("Form0ShowHideScore");
        Show("Form0ShowHideContactPatient");

        if (document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "ContactPatient_0").checked == false && document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "ContactPatient_1").checked == false) {
          document.getElementById("FormView_PROMS_Questionnaire_Form_TextBox_" + FormMode + "ContactNumber").value = "";
          Hide("Form0ShowHideContactNumber");
        } else if (document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "ContactPatient_0").checked == true) {
          Show("Form0ShowHideContactNumber");
        } else if (document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "ContactPatient_1").checked == true) {
          document.getElementById("FormView_PROMS_Questionnaire_Form_TextBox_" + FormMode + "ContactNumber").value = "";
          Hide("Form0ShowHideContactNumber");
        }

        Show("Form0ShowHideLanguage");
        Show("Form0ShowHideEmailSend");
        Show("Form0ShowHideEmailDate");
      }
      else 
      {
        Hide("Form0ShowHideAdmissionDate");
        Hide("Form0ShowHideScore");
        Hide("Form0ShowHideContactPatient");

        document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "ContactPatient_0").checked = false;
        document.getElementById("FormView_PROMS_Questionnaire_Form_RadioButtonList_" + FormMode + "ContactPatient_1").checked = false;
        document.getElementById("FormView_PROMS_Questionnaire_Form_TextBox_" + FormMode + "ContactNumber").value = "";

        Hide("Form0ShowHideContactNumber");
        Hide("Form0ShowHideLanguage");

        document.getElementById("FormView_PROMS_Questionnaire_Form_DropDownList_" + FormMode + "LanguageList").value = "";

        Hide("Form0ShowHideEmailSend");
        Hide("Form0ShowHideEmailDate");
      }
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form1--------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form1() {
  var FormMode;
  if (document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_PROMS_FollowUp_Form_CheckBox_" + FormMode + "Cancelled").checked == true) {
      var TotalQuestions = parseInt(document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_" + FormMode + "FollowUpTotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        for (var b = 0; b <= 4; b++) {
          document.getElementById("FormView_PROMS_FollowUp_Form_RadioButtonList_" + FormMode + "Q" + a + "_" + b + "").checked = false;
        }
        Hide("Form1ShowHideQ" + a + "");
      }

      Hide("Form1ShowHideScore");
      Show("Form1ShowHideCancelledList");
      GotoBookmark();
    } else {
      TotalQuestions = parseInt(document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_" + FormMode + "FollowUpTotalQuestions").value);
      for (a = 1; a <= TotalQuestions; a++) {
        Show("Form1ShowHideQ" + a + "");
      }

      document.getElementById("FormView_PROMS_FollowUp_Form_DropDownList_" + FormMode + "CancelledList").value = "";

      Show("Form1ShowHideScore");
      Hide("Form1ShowHideCancelledList");
      GotoBookmark();
    }
  } else {
    if (document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_ItemFollowUpTotalQuestions")) {
      if (document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_ItemCancelled").value == "True") {
        TotalQuestions = parseInt(document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_ItemFollowUpTotalQuestions").value);
        for (a = 1; a <= TotalQuestions; a++) {
          Hide("Form1ShowHideQ" + a + "");
        }

        Hide("Form1ShowHideScore");
        Show("Form1ShowHideCancelledList");
        GotoBookmark();
      } else {
        TotalQuestions = parseInt(document.getElementById("FormView_PROMS_FollowUp_Form_HiddenField_ItemFollowUpTotalQuestions").value);
        for (a = 1; a <= TotalQuestions; a++) {
          Show("Form1ShowHideQ" + a + "");
        }

        Show("Form1ShowHideScore");
        Hide("Form1ShowHideCancelledList");
        GotoBookmark();
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