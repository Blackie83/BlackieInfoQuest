
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
  window.open(PrintLink, 'Print', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=No , location=No , scrollbars=Yes , resizable=No , status=Yes , left=' + left + ' , top=' + top + ' ');
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --FormAdmin-------------------------------------------------------------------------------------------------------------------------------------
function FormAdmin(AdminLink) {
  var width = 750;
  var height = 700;
  var left = (screen.width - width) / 2;
  var top = (screen.height - height) / 2;
  window.open(AdminLink, 'Admin', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=No , location=No , scrollbars=Yes , resizable=No , status=Yes , left=' + left + ' , top=' + top + ' ');
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
  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form0--------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "0") {
    var Form0Mode;
    if (document.getElementById("DetailsView_Isidima_Form0_DropDownList_InsertPatientCategory")) {
      Form0Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form0_DropDownList_EditPatientCategory")) {
      Form0Mode = "Edit";
    } else {
      Form0Mode = "";
    }

    if (Form0Mode != "") {
      if (document.getElementById("DetailsView_Isidima_Form0_DropDownList_" + Form0Mode + "PatientCategory").value == "") {
        document.getElementById("Form0PatientCategory").style.backgroundColor = "#d46e6e";
        document.getElementById("Form0PatientCategory").style.color = "#333333";
        document.getElementById("Form0PatientCategory").style.height = "24px";
      } else {
        document.getElementById("Form0PatientCategory").style.backgroundColor = "#77cf9c";
        document.getElementById("Form0PatientCategory").style.color = "#333333";
        document.getElementById("Form0PatientCategory").style.height = "24px";
      }

      if (document.getElementById("DetailsView_Isidima_Form0_TextBox_" + Form0Mode + "Date").value == "") {
        document.getElementById("Form0Date").style.backgroundColor = "#d46e6e";
        document.getElementById("Form0Date").style.color = "#333333";
        document.getElementById("Form0Date").style.height = "25px";
      } else {
        document.getElementById("Form0Date").style.backgroundColor = "#77cf9c";
        document.getElementById("Form0Date").style.color = "#333333";
        document.getElementById("Form0Date").style.height = "25px";
      }
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form1--------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "1") {
    var Form1Mode;
    if (document.getElementById("DetailsView_Isidima_Form1_RadioButtonList_InsertMHA_A01")) {
      Form1Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form1_RadioButtonList_EditMHA_A01")) {
      Form1Mode = "Edit";
    } else {
      Form1Mode = "";
    }

    if (Form1Mode != "") {
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form1_HiddenField_MHA_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form1_RadioButtonList_" + Form1Mode + "MHA_A0" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form1_RadioButtonList_" + Form1Mode + "MHA_A0" + a + "_1").checked == true) {
            document.getElementById("Form1MHA_A0" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form1MHA_A0" + a + "").style.color = "#333333";
            document.getElementById("MHA_A0" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form1MHA_A0" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form1MHA_A0" + a + "").style.color = "#333333";
            document.getElementById("MHA_A0" + a + "").style.height = "24px";
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form1_RadioButtonList_" + Form1Mode + "MHA_A" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form1_RadioButtonList_" + Form1Mode + "MHA_A" + a + "_1").checked == true) {
            document.getElementById("Form1MHA_A" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form1MHA_A" + a + "").style.color = "#333333";
            document.getElementById("MHA_A" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form1MHA_A" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form1MHA_A" + a + "").style.color = "#333333";
            document.getElementById("MHA_A" + a + "").style.height = "24px";
          }
        }
      }
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form2--------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "2") {
    var Form2Mode;
    if (document.getElementById("DetailsView_Isidima_Form2_RadioButtonList_InsertVPA_A01")) {
      Form2Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form2_RadioButtonList_EditVPA_A01")) {
      Form2Mode = "Edit";
    } else {
      Form2Mode = "";
    }

    if (Form2Mode != "") {
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form2_HiddenField_VPA_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form2_RadioButtonList_" + Form2Mode + "VPA_A0" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form2_RadioButtonList_" + Form2Mode + "VPA_A0" + a + "_1").checked == true) {
            document.getElementById("Form2VPA_A0" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form2VPA_A0" + a + "").style.color = "#333333";
            document.getElementById("VPA_A0" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form2VPA_A0" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form2VPA_A0" + a + "").style.color = "#333333";
            document.getElementById("VPA_A0" + a + "").style.height = "24px";
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form2_RadioButtonList_" + Form2Mode + "VPA_A" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form2_RadioButtonList_" + Form2Mode + "VPA_A" + a + "_1").checked == true) {
            document.getElementById("Form2VPA_A" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form2VPA_A" + a + "").style.color = "#333333";
            document.getElementById("VPA_A" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form2VPA_A" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form2VPA_A" + a + "").style.color = "#333333";
            document.getElementById("VPA_A" + a + "").style.height = "24px";
          }
        }
      }
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form3--------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "3") {
    var Form3Mode;
    if (document.getElementById("DetailsView_Isidima_Form3_RadioButtonList_InsertJ_J01")) {
      Form3Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form3_RadioButtonList_EditJ_J01")) {
      Form3Mode = "Edit";
    } else {
      Form3Mode = "";
    }

    if (Form3Mode != "") {
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form3_HiddenField_J_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form3_RadioButtonList_" + Form3Mode + "J_J0" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form3_RadioButtonList_" + Form3Mode + "J_J0" + a + "_1").checked == true) {
            document.getElementById("Form3J_J0" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form3J_J0" + a + "").style.color = "#333333";
            document.getElementById("J_J0" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form3J_J0" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form3J_J0" + a + "").style.color = "#333333";
            document.getElementById("J_J0" + a + "").style.height = "24px";
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form3_RadioButtonList_" + Form3Mode + "J_J" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form3_RadioButtonList_" + Form3Mode + "J_J" + a + "_1").checked == true) {
            document.getElementById("Form3J_J" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form3J_J" + a + "").style.color = "#333333";
            document.getElementById("J_J" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form3J_J" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form3J_J" + a + "").style.color = "#333333";
            document.getElementById("J_J" + a + "").style.height = "24px";
          }
        }
      }
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form4--------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "4") {
    var Form4Mode;
    if (document.getElementById("DetailsView_Isidima_Form4_RadioButtonList_InsertDMH_S01")) {
      Form4Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form4_RadioButtonList_EditDMH_S01")) {
      Form4Mode = "Edit";
    } else {
      Form4Mode = "";
    }

    if (Form4Mode != "") {
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form4_HiddenField_DMH_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form4_RadioButtonList_" + Form4Mode + "DMH_S0" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form4_RadioButtonList_" + Form4Mode + "DMH_S0" + a + "_1").checked == true) {
            document.getElementById("Form4DMH_S0" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form4DMH_S0" + a + "").style.color = "#333333";
            document.getElementById("DMH_S0" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form4DMH_S0" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form4DMH_S0" + a + "").style.color = "#333333";
            document.getElementById("DMH_S0" + a + "").style.height = "24px";
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form4_RadioButtonList_" + Form4Mode + "DMH_S" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form4_RadioButtonList_" + Form4Mode + "DMH_S" + a + "_1").checked == true) {
            document.getElementById("Form4DMH_S" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form4DMH_S" + a + "").style.color = "#333333";
            document.getElementById("DMH_S" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form4DMH_S" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form4DMH_S" + a + "").style.color = "#333333";
            document.getElementById("DMH_S" + a + "").style.height = "24px";
          }
        }
      }
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form5--------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "5") {
    var Form5Mode;
    if (document.getElementById("DetailsView_Isidima_Form5_RadioButtonList_InsertF_F01")) {
      Form5Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form5_RadioButtonList_EditF_F01")) {
      Form5Mode = "Edit";
    } else {
      Form5Mode = "";
    }

    if (Form5Mode != "") {
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form5_HiddenField_F_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form5_RadioButtonList_" + Form5Mode + "F_F0" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form5_RadioButtonList_" + Form5Mode + "F_F0" + a + "_1").checked == true) {
            document.getElementById("Form5F_F0" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form5F_F0" + a + "").style.color = "#333333";
            document.getElementById("F_F0" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form5F_F0" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form5F_F0" + a + "").style.color = "#333333";
            document.getElementById("F_F0" + a + "").style.height = "24px";
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form5_RadioButtonList_" + Form5Mode + "F_F" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form5_RadioButtonList_" + Form5Mode + "F_F" + a + "_1").checked == true) {
            document.getElementById("Form5F_F" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form5F_F" + a + "").style.color = "#333333";
            document.getElementById("F_F" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form5F_F" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form5F_F" + a + "").style.color = "#333333";
            document.getElementById("F_F" + a + "").style.height = "24px";
          }
        }
      }
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form6--------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "6") {
    var Form6Mode;
    if (document.getElementById("DetailsView_Isidima_Form6_RadioButtonList_InsertI_I01")) {
      Form6Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form6_RadioButtonList_EditI_I01")) {
      Form6Mode = "Edit";
    } else {
      Form6Mode = "";
    }

    if (Form6Mode != "") {
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form6_HiddenField_I_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form6_RadioButtonList_" + Form6Mode + "I_I0" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form6_RadioButtonList_" + Form6Mode + "I_I0" + a + "_1").checked == true) {
            document.getElementById("Form6I_I0" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form6I_I0" + a + "").style.color = "#333333";
            document.getElementById("I_I0" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form6I_I0" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form6I_I0" + a + "").style.color = "#333333";
            document.getElementById("I_I0" + a + "").style.height = "24px";
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form6_RadioButtonList_" + Form6Mode + "I_I" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form6_RadioButtonList_" + Form6Mode + "I_I" + a + "_1").checked == true) {
            document.getElementById("Form6I_I" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form6I_I" + a + "").style.color = "#333333";
            document.getElementById("I_I" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form6I_I" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form6I_I" + a + "").style.color = "#333333";
            document.getElementById("I_I" + a + "").style.height = "24px";
          }
        }
      }
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form7--------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "7") {
    var Form7Mode;
    if (document.getElementById("DetailsView_Isidima_Form7_RadioButtonList_InsertPSY_C01")) {
      Form7Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form7_RadioButtonList_EditPSY_C01")) {
      Form7Mode = "Edit";
    } else {
      Form7Mode = "";
    }

    if (Form7Mode != "") {
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form7_HiddenField_PSY_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form7_RadioButtonList_" + Form7Mode + "PSY_C0" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form7_RadioButtonList_" + Form7Mode + "PSY_C0" + a + "_1").checked == true) {
            document.getElementById("Form7PSY_C0" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form7PSY_C0" + a + "").style.color = "#333333";
            document.getElementById("PSY_C0" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form7PSY_C0" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form7PSY_C0" + a + "").style.color = "#333333";
            document.getElementById("PSY_C0" + a + "").style.height = "24px";
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form7_RadioButtonList_" + Form7Mode + "PSY_C" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form7_RadioButtonList_" + Form7Mode + "PSY_C" + a + "_1").checked == true) {
            document.getElementById("Form7PSY_C" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form7PSY_C" + a + "").style.color = "#333333";
            document.getElementById("PSY_C" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form7PSY_C" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form7PSY_C" + a + "").style.color = "#333333";
            document.getElementById("PSY_C" + a + "").style.height = "24px";
          }
        }
      }
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form8--------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "8") {
    var Form8Mode;
    if (document.getElementById("DetailsView_Isidima_Form8_RadioButtonList_InsertT_T01")) {
      Form8Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form8_RadioButtonList_EditT_T01")) {
      Form8Mode = "Edit";
    } else {
      Form8Mode = "";
    }

    if (Form8Mode != "") {
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form8_HiddenField_T_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form8_RadioButtonList_" + Form8Mode + "T_T0" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form8_RadioButtonList_" + Form8Mode + "T_T0" + a + "_1").checked == true) {
            document.getElementById("Form8T_T0" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form8T_T0" + a + "").style.color = "#333333";
            document.getElementById("T_T0" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form8T_T0" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form8T_T0" + a + "").style.color = "#333333";
            document.getElementById("T_T0" + a + "").style.height = "24px";
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form8_RadioButtonList_" + Form8Mode + "T_T" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form8_RadioButtonList_" + Form8Mode + "T_T" + a + "_1").checked == true) {
            document.getElementById("Form8T_T" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form8T_T" + a + "").style.color = "#333333";
            document.getElementById("T_T" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form8T_T" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form8T_T" + a + "").style.color = "#333333";
            document.getElementById("T_T" + a + "").style.height = "24px";
          }
        }
      }
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form9--------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "9") {
    var Form9Mode;
    if (document.getElementById("DetailsView_Isidima_Form9_RadioButtonList_InsertB_B01")) {
      Form9Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form9_RadioButtonList_EditB_B01")) {
      Form9Mode = "Edit";
    } else {
      Form9Mode = "";
    }

    if (Form9Mode != "") {
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form9_HiddenField_B_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form9_RadioButtonList_" + Form9Mode + "B_B0" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form9_RadioButtonList_" + Form9Mode + "B_B0" + a + "_1").checked == true) {
            document.getElementById("Form9B_B0" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form9B_B0" + a + "").style.color = "#333333";
            document.getElementById("B_B0" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form9B_B0" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form9B_B0" + a + "").style.color = "#333333";
            document.getElementById("B_B0" + a + "").style.height = "24px";
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form9_RadioButtonList_" + Form9Mode + "B_B" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form9_RadioButtonList_" + Form9Mode + "B_B" + a + "_1").checked == true) {
            document.getElementById("Form9B_B" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form9B_B" + a + "").style.color = "#333333";
            document.getElementById("B_B" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form9B_B" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form9B_B" + a + "").style.color = "#333333";
            document.getElementById("B_B" + a + "").style.height = "24px";
          }
        }
      }
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form10-------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "10") {
    var Form10Mode;
    if (document.getElementById("DetailsView_Isidima_Form10_RadioButtonList_InsertR_R01")) {
      Form10Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form10_RadioButtonList_EditR_R01")) {
      Form10Mode = "Edit";
    } else {
      Form10Mode = "";
    }

    if (Form10Mode != "") {
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form10_HiddenField_R_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form10_RadioButtonList_" + Form10Mode + "R_R0" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form10_RadioButtonList_" + Form10Mode + "R_R0" + a + "_1").checked == true) {
            document.getElementById("Form10R_R0" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form10R_R0" + a + "").style.color = "#333333";
            document.getElementById("R_R0" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form10R_R0" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form10R_R0" + a + "").style.color = "#333333";
            document.getElementById("R_R0" + a + "").style.height = "24px";
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form10_RadioButtonList_" + Form10Mode + "R_R" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form10_RadioButtonList_" + Form10Mode + "R_R" + a + "_1").checked == true) {
            document.getElementById("Form10R_R" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form10R_R" + a + "").style.color = "#333333";
            document.getElementById("R_R" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form10R_R" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form10R_R" + a + "").style.color = "#333333";
            document.getElementById("R_R" + a + "").style.height = "24px";
          }
        }
      }
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form11-------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "11") {
    var Form11Mode;
    if (document.getElementById("DetailsView_Isidima_Form11_RadioButtonList_InsertS_S01")) {
      Form11Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form11_RadioButtonList_EditS_S01")) {
      Form11Mode = "Edit";
    } else {
      Form11Mode = "";
    }

    if (Form11Mode != "") {
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form11_HiddenField_S_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form11_RadioButtonList_" + Form11Mode + "S_S0" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form11_RadioButtonList_" + Form11Mode + "S_S0" + a + "_1").checked == true) {
            document.getElementById("Form11S_S0" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form11S_S0" + a + "").style.color = "#333333";
            document.getElementById("S_S0" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form11S_S0" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form11S_S0" + a + "").style.color = "#333333";
            document.getElementById("S_S0" + a + "").style.height = "24px";
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form11_RadioButtonList_" + Form11Mode + "S_S" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form11_RadioButtonList_" + Form11Mode + "S_S" + a + "_1").checked == true) {
            document.getElementById("Form11S_S" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form11S_S" + a + "").style.color = "#333333";
            document.getElementById("S_S" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form11S_S" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form11S_S" + a + "").style.color = "#333333";
            document.getElementById("S_S" + a + "").style.height = "24px";
          }
        }
      }
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Validation_Form12-------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "12") {
    var Form12Mode;
    if (document.getElementById("DetailsView_Isidima_Form12_RadioButtonList_InsertV_V01")) {
      Form12Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form12_RadioButtonList_EditV_V01")) {
      Form12Mode = "Edit";
    } else {
      Form12Mode = "";
    }

    if (Form12Mode != "") {
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form12_HiddenField_V_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form12_RadioButtonList_" + Form12Mode + "V_V0" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form12_RadioButtonList_" + Form12Mode + "V_V0" + a + "_1").checked == true) {
            document.getElementById("Form12V_V0" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form12V_V0" + a + "").style.color = "#333333";
            document.getElementById("V_V0" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form12V_V0" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form12V_V0" + a + "").style.color = "#333333";
            document.getElementById("V_V0" + a + "").style.height = "24px";
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form12_RadioButtonList_" + Form12Mode + "V_V" + a + "_0").checked == true || document.getElementById("DetailsView_Isidima_Form12_RadioButtonList_" + Form12Mode + "V_V" + a + "_1").checked == true) {
            document.getElementById("Form12V_V" + a + "").style.backgroundColor = "#77cf9c";
            document.getElementById("Form12V_V" + a + "").style.color = "#333333";
            document.getElementById("V_V" + a + "").style.height = "24px";
          } else {
            document.getElementById("Form12V_V" + a + "").style.backgroundColor = "#d46e6e";
            document.getElementById("Form12V_V" + a + "").style.color = "#333333";
            document.getElementById("V_V" + a + "").style.height = "24px";
          }
        }
      }
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_Form------------------------------------------------------------------------------------------------------------------------------
function Calculation_Form() {
  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Calculation_Form1-------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "1") {
    var Form1Mode;
    if (document.getElementById("DetailsView_Isidima_Form1_RadioButtonList_InsertMHA_A01")) {
      Form1Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form1_RadioButtonList_EditMHA_A01")) {
      Form1Mode = "Edit";
    } else {
      Form1Mode = "";
    }

    if (Form1Mode != "") {
      var Form1Total = 0;
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form1_HiddenField_MHA_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form1_RadioButtonList_" + Form1Mode + "MHA_A0" + a + "_0").checked == true) {
            Form1Total = Form1Total + parseInt(document.getElementById("DetailsView_Isidima_Form1_HiddenField_MHA_A0" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form1_RadioButtonList_" + Form1Mode + "MHA_A0" + a + "_1").checked == true) {
            Form1Total = Form1Total + parseInt(document.getElementById("DetailsView_Isidima_Form1_HiddenField_MHA_A0" + a + "No").value);
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form1_RadioButtonList_" + Form1Mode + "MHA_A" + a + "_0").checked == true) {
            Form1Total = Form1Total + parseInt(document.getElementById("DetailsView_Isidima_Form1_HiddenField_MHA_A" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form1_RadioButtonList_" + Form1Mode + "MHA_A" + a + "_1").checked == true) {
            Form1Total = Form1Total + parseInt(document.getElementById("DetailsView_Isidima_Form1_HiddenField_MHA_A" + a + "No").value);
          }
        }
      }

      document.getElementById("DetailsView_Isidima_Form1_Textbox_" + Form1Mode + "Total").value = Form1Total;
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Calculation_Form2-------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "2") {
    var Form2Mode;
    if (document.getElementById("DetailsView_Isidima_Form2_RadioButtonList_InsertVPA_A01")) {
      Form2Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form2_RadioButtonList_EditVPA_A01")) {
      Form2Mode = "Edit";
    } else {
      Form2Mode = "";
    }

    if (Form2Mode != "") {
      var Form2Total = 0;
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form2_HiddenField_VPA_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form2_RadioButtonList_" + Form2Mode + "VPA_A0" + a + "_0").checked == true) {
            Form2Total = Form2Total + parseInt(document.getElementById("DetailsView_Isidima_Form2_HiddenField_VPA_A0" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form2_RadioButtonList_" + Form2Mode + "VPA_A0" + a + "_1").checked == true) {
            Form2Total = Form2Total + parseInt(document.getElementById("DetailsView_Isidima_Form2_HiddenField_VPA_A0" + a + "No").value);
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form2_RadioButtonList_" + Form2Mode + "VPA_A" + a + "_0").checked == true) {
            Form2Total = Form2Total + parseInt(document.getElementById("DetailsView_Isidima_Form2_HiddenField_VPA_A" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form2_RadioButtonList_" + Form2Mode + "VPA_A" + a + "_1").checked == true) {
            Form2Total = Form2Total + parseInt(document.getElementById("DetailsView_Isidima_Form2_HiddenField_VPA_A" + a + "No").value);
          }
        }
      }

      document.getElementById("DetailsView_Isidima_Form2_Textbox_" + Form2Mode + "Total").value = Form2Total;
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Calculation_Form3-------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "3") {
    var Form3Mode;
    if (document.getElementById("DetailsView_Isidima_Form3_RadioButtonList_InsertJ_J01")) {
      Form3Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form3_RadioButtonList_EditJ_J01")) {
      Form3Mode = "Edit";
    } else {
      Form3Mode = "";
    }

    if (Form3Mode != "") {
      var Form3Total = 0;
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form3_HiddenField_J_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form3_RadioButtonList_" + Form3Mode + "J_J0" + a + "_0").checked == true) {
            Form3Total = Form3Total + parseInt(document.getElementById("DetailsView_Isidima_Form3_HiddenField_J_J0" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form3_RadioButtonList_" + Form3Mode + "J_J0" + a + "_1").checked == true) {
            Form3Total = Form3Total + parseInt(document.getElementById("DetailsView_Isidima_Form3_HiddenField_J_J0" + a + "No").value);
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form3_RadioButtonList_" + Form3Mode + "J_J" + a + "_0").checked == true) {
            Form3Total = Form3Total + parseInt(document.getElementById("DetailsView_Isidima_Form3_HiddenField_J_J" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form3_RadioButtonList_" + Form3Mode + "J_J" + a + "_1").checked == true) {
            Form3Total = Form3Total + parseInt(document.getElementById("DetailsView_Isidima_Form3_HiddenField_J_J" + a + "No").value);
          }
        }
      }

      document.getElementById("DetailsView_Isidima_Form3_Textbox_" + Form3Mode + "Total").value = Form3Total;
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Calculation_Form4-------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "4") {
    var Form4Mode;
    if (document.getElementById("DetailsView_Isidima_Form4_RadioButtonList_InsertDMH_S01")) {
      Form4Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form4_RadioButtonList_EditDMH_S01")) {
      Form4Mode = "Edit";
    } else {
      Form4Mode = "";
    }

    if (Form4Mode != "") {
      var Form4Total = 0;
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form4_HiddenField_DMH_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form4_RadioButtonList_" + Form4Mode + "DMH_S0" + a + "_0").checked == true) {
            Form4Total = Form4Total + parseInt(document.getElementById("DetailsView_Isidima_Form4_HiddenField_DMH_S0" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form4_RadioButtonList_" + Form4Mode + "DMH_S0" + a + "_1").checked == true) {
            Form4Total = Form4Total + parseInt(document.getElementById("DetailsView_Isidima_Form4_HiddenField_DMH_S0" + a + "No").value);
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form4_RadioButtonList_" + Form4Mode + "DMH_S" + a + "_0").checked == true) {
            Form4Total = Form4Total + parseInt(document.getElementById("DetailsView_Isidima_Form4_HiddenField_DMH_S" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form4_RadioButtonList_" + Form4Mode + "DMH_S" + a + "_1").checked == true) {
            Form4Total = Form4Total + parseInt(document.getElementById("DetailsView_Isidima_Form4_HiddenField_DMH_S" + a + "No").value);
          }
        }
      }

      document.getElementById("DetailsView_Isidima_Form4_Textbox_" + Form4Mode + "Total").value = Form4Total;
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Calculation_Form5-------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "5") {
    var Form5Mode;
    if (document.getElementById("DetailsView_Isidima_Form5_RadioButtonList_InsertF_F01")) {
      Form5Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form5_RadioButtonList_EditF_F01")) {
      Form5Mode = "Edit";
    } else {
      Form5Mode = "";
    }

    if (Form5Mode != "") {
      var Form5Total = 0;
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form5_HiddenField_F_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form5_RadioButtonList_" + Form5Mode + "F_F0" + a + "_0").checked == true) {
            Form5Total = Form5Total + parseInt(document.getElementById("DetailsView_Isidima_Form5_HiddenField_F_F0" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form5_RadioButtonList_" + Form5Mode + "F_F0" + a + "_1").checked == true) {
            Form5Total = Form5Total + parseInt(document.getElementById("DetailsView_Isidima_Form5_HiddenField_F_F0" + a + "No").value);
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form5_RadioButtonList_" + Form5Mode + "F_F" + a + "_0").checked == true) {
            Form5Total = Form5Total + parseInt(document.getElementById("DetailsView_Isidima_Form5_HiddenField_F_F" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form5_RadioButtonList_" + Form5Mode + "F_F" + a + "_1").checked == true) {
            Form5Total = Form5Total + parseInt(document.getElementById("DetailsView_Isidima_Form5_HiddenField_F_F" + a + "No").value);
          }
        }
      }

      document.getElementById("DetailsView_Isidima_Form5_Textbox_" + Form5Mode + "Total").value = Form5Total;
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Calculation_Form6-------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "6") {
    var Form6Mode;
    if (document.getElementById("DetailsView_Isidima_Form6_RadioButtonList_InsertI_I01")) {
      Form6Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form6_RadioButtonList_EditI_I01")) {
      Form6Mode = "Edit";
    } else {
      Form6Mode = "";
    }

    if (Form6Mode != "") {
      var Form6Total = 0;
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form6_HiddenField_I_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form6_RadioButtonList_" + Form6Mode + "I_I0" + a + "_0").checked == true) {
            Form6Total = Form6Total + parseInt(document.getElementById("DetailsView_Isidima_Form6_HiddenField_I_I0" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form6_RadioButtonList_" + Form6Mode + "I_I0" + a + "_1").checked == true) {
            Form6Total = Form6Total + parseInt(document.getElementById("DetailsView_Isidima_Form6_HiddenField_I_I0" + a + "No").value);
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form6_RadioButtonList_" + Form6Mode + "I_I" + a + "_0").checked == true) {
            Form6Total = Form6Total + parseInt(document.getElementById("DetailsView_Isidima_Form6_HiddenField_I_I" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form6_RadioButtonList_" + Form6Mode + "I_I" + a + "_1").checked == true) {
            Form6Total = Form6Total + parseInt(document.getElementById("DetailsView_Isidima_Form6_HiddenField_I_I" + a + "No").value);
          }
        }
      }

      document.getElementById("DetailsView_Isidima_Form6_Textbox_" + Form6Mode + "Total").value = Form6Total;
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Calculation_Form7-------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "7") {
    var Form7Mode;
    if (document.getElementById("DetailsView_Isidima_Form7_RadioButtonList_InsertPSY_C01")) {
      Form7Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form7_RadioButtonList_EditPSY_C01")) {
      Form7Mode = "Edit";
    } else {
      Form7Mode = "";
    }

    if (Form7Mode != "") {
      var Form7Total = 0;
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form7_HiddenField_PSY_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form7_RadioButtonList_" + Form7Mode + "PSY_C0" + a + "_0").checked == true) {
            Form7Total = Form7Total + parseInt(document.getElementById("DetailsView_Isidima_Form7_HiddenField_PSY_C0" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form7_RadioButtonList_" + Form7Mode + "PSY_C0" + a + "_1").checked == true) {
            Form7Total = Form7Total + parseInt(document.getElementById("DetailsView_Isidima_Form7_HiddenField_PSY_C0" + a + "No").value);
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form7_RadioButtonList_" + Form7Mode + "PSY_C" + a + "_0").checked == true) {
            Form7Total = Form7Total + parseInt(document.getElementById("DetailsView_Isidima_Form7_HiddenField_PSY_C" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form7_RadioButtonList_" + Form7Mode + "PSY_C" + a + "_1").checked == true) {
            Form7Total = Form7Total + parseInt(document.getElementById("DetailsView_Isidima_Form7_HiddenField_PSY_C" + a + "No").value);
          }
        }
      }

      document.getElementById("DetailsView_Isidima_Form7_Textbox_" + Form7Mode + "Total").value = Form7Total;
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Calculation_Form8-------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "8") {
    var Form8Mode;
    if (document.getElementById("DetailsView_Isidima_Form8_RadioButtonList_InsertT_T01")) {
      Form8Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form8_RadioButtonList_EditT_T01")) {
      Form8Mode = "Edit";
    } else {
      Form8Mode = "";
    }

    if (Form8Mode != "") {
      var Form8Total = 0;
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form8_HiddenField_T_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form8_RadioButtonList_" + Form8Mode + "T_T0" + a + "_0").checked == true) {
            Form8Total = Form8Total + parseInt(document.getElementById("DetailsView_Isidima_Form8_HiddenField_T_T0" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form8_RadioButtonList_" + Form8Mode + "T_T0" + a + "_1").checked == true) {
            Form8Total = Form8Total + parseInt(document.getElementById("DetailsView_Isidima_Form8_HiddenField_T_T0" + a + "No").value);
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form8_RadioButtonList_" + Form8Mode + "T_T" + a + "_0").checked == true) {
            Form8Total = Form8Total + parseInt(document.getElementById("DetailsView_Isidima_Form8_HiddenField_T_T" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form8_RadioButtonList_" + Form8Mode + "T_T" + a + "_1").checked == true) {
            Form8Total = Form8Total + parseInt(document.getElementById("DetailsView_Isidima_Form8_HiddenField_T_T" + a + "No").value);
          }
        }
      }

      document.getElementById("DetailsView_Isidima_Form8_Textbox_" + Form8Mode + "Total").value = Form8Total;
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Calculation_Form9-------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "9") {
    var Form9Mode;
    if (document.getElementById("DetailsView_Isidima_Form9_RadioButtonList_InsertB_B01")) {
      Form9Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form9_RadioButtonList_EditB_B01")) {
      Form9Mode = "Edit";
    } else {
      Form9Mode = "";
    }

    if (Form9Mode != "") {
      var Form9Total = 0;
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form9_HiddenField_B_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form9_RadioButtonList_" + Form9Mode + "B_B0" + a + "_0").checked == true) {
            Form9Total = Form9Total + parseInt(document.getElementById("DetailsView_Isidima_Form9_HiddenField_B_B0" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form9_RadioButtonList_" + Form9Mode + "B_B0" + a + "_1").checked == true) {
            Form9Total = Form9Total + parseInt(document.getElementById("DetailsView_Isidima_Form9_HiddenField_B_B0" + a + "No").value);
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form9_RadioButtonList_" + Form9Mode + "B_B" + a + "_0").checked == true) {
            Form9Total = Form9Total + parseInt(document.getElementById("DetailsView_Isidima_Form9_HiddenField_B_B" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form9_RadioButtonList_" + Form9Mode + "B_B" + a + "_1").checked == true) {
            Form9Total = Form9Total + parseInt(document.getElementById("DetailsView_Isidima_Form9_HiddenField_B_B" + a + "No").value);
          }
        }
      }

      document.getElementById("DetailsView_Isidima_Form9_Textbox_" + Form9Mode + "Total").value = Form9Total;
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Calculation_Form10------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "10") {
    var Form10Mode;
    if (document.getElementById("DetailsView_Isidima_Form10_RadioButtonList_InsertR_R01")) {
      Form10Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form10_RadioButtonList_EditR_R01")) {
      Form10Mode = "Edit";
    } else {
      Form10Mode = "";
    }

    if (Form10Mode != "") {
      var Form10Total = 0;
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form10_HiddenField_R_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form10_RadioButtonList_" + Form10Mode + "R_R0" + a + "_0").checked == true) {
            Form10Total = Form10Total + parseInt(document.getElementById("DetailsView_Isidima_Form10_HiddenField_R_R0" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form10_RadioButtonList_" + Form10Mode + "R_R0" + a + "_1").checked == true) {
            Form10Total = Form10Total + parseInt(document.getElementById("DetailsView_Isidima_Form10_HiddenField_R_R0" + a + "No").value);
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form10_RadioButtonList_" + Form10Mode + "R_R" + a + "_0").checked == true) {
            Form10Total = Form10Total + parseInt(document.getElementById("DetailsView_Isidima_Form10_HiddenField_R_R" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form10_RadioButtonList_" + Form10Mode + "R_R" + a + "_1").checked == true) {
            Form10Total = Form10Total + parseInt(document.getElementById("DetailsView_Isidima_Form10_HiddenField_R_R" + a + "No").value);
          }
        }
      }

      document.getElementById("DetailsView_Isidima_Form10_Textbox_" + Form10Mode + "Total").value = Form10Total;
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Calculation_Form11------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "11") {
    var Form11Mode;
    if (document.getElementById("DetailsView_Isidima_Form11_RadioButtonList_InsertS_S01")) {
      Form11Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form11_RadioButtonList_EditS_S01")) {
      Form11Mode = "Edit";
    } else {
      Form11Mode = "";
    }

    if (Form11Mode != "") {
      var Form11Total = 0;
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form11_HiddenField_S_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form11_RadioButtonList_" + Form11Mode + "S_S0" + a + "_0").checked == true) {
            Form11Total = Form11Total + parseInt(document.getElementById("DetailsView_Isidima_Form11_HiddenField_S_S0" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form11_RadioButtonList_" + Form11Mode + "S_S0" + a + "_1").checked == true) {
            Form11Total = Form11Total + parseInt(document.getElementById("DetailsView_Isidima_Form11_HiddenField_S_S0" + a + "No").value);
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form11_RadioButtonList_" + Form11Mode + "S_S" + a + "_0").checked == true) {
            Form11Total = Form11Total + parseInt(document.getElementById("DetailsView_Isidima_Form11_HiddenField_S_S" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form11_RadioButtonList_" + Form11Mode + "S_S" + a + "_1").checked == true) {
            Form11Total = Form11Total + parseInt(document.getElementById("DetailsView_Isidima_Form11_HiddenField_S_S" + a + "No").value);
          }
        }
      }

      document.getElementById("DetailsView_Isidima_Form11_Textbox_" + Form11Mode + "Total").value = Form11Total;
    }
  }

  //----- --------------------------------------------------------------------------------------------------------------------------------------------
  //----- --Calculation_Form12------------------------------------------------------------------------------------------------------------------------
  if (QueryStringValue("Form") == "12") {
    var Form12Mode;
    if (document.getElementById("DetailsView_Isidima_Form12_RadioButtonList_InsertV_V01")) {
      Form12Mode = "Insert";
    } else if (document.getElementById("DetailsView_Isidima_Form12_RadioButtonList_EditV_V01")) {
      Form12Mode = "Edit";
    } else {
      Form12Mode = "";
    }

    if (Form12Mode != "") {
      var Form12Total = 0;
      var TotalQuestions = parseInt(document.getElementById("DetailsView_Isidima_Form12_HiddenField_V_TotalQuestions").value);
      for (var a = 1; a <= TotalQuestions; a++) {
        if (a < 10) {
          if (document.getElementById("DetailsView_Isidima_Form12_RadioButtonList_" + Form12Mode + "V_V0" + a + "_0").checked == true) {
            Form12Total = Form12Total + parseInt(document.getElementById("DetailsView_Isidima_Form12_HiddenField_V_V0" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form12_RadioButtonList_" + Form12Mode + "V_V0" + a + "_1").checked == true) {
            Form12Total = Form12Total + parseInt(document.getElementById("DetailsView_Isidima_Form12_HiddenField_V_V0" + a + "No").value);
          }
        } else {
          if (document.getElementById("DetailsView_Isidima_Form12_RadioButtonList_" + Form12Mode + "V_V" + a + "_0").checked == true) {
            Form12Total = Form12Total + parseInt(document.getElementById("DetailsView_Isidima_Form12_HiddenField_V_V" + a + "Yes").value);
          } else if (document.getElementById("DetailsView_Isidima_Form12_RadioButtonList_" + Form12Mode + "V_V" + a + "_1").checked == true) {
            Form12Total = Form12Total + parseInt(document.getElementById("DetailsView_Isidima_Form12_HiddenField_V_V" + a + "No").value);
          }
        }
      }

      document.getElementById("DetailsView_Isidima_Form12_Textbox_" + Form12Mode + "Total").value = Form12Total;
    }
  }
}