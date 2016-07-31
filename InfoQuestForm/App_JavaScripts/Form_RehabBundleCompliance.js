
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
  }
  else
  {
    document.getElementById("SearchFacility").style.backgroundColor = "#77cf9c";
    document.getElementById("SearchFacility").style.color = "#333333";
  }

  if (document.getElementById("TextBox_PatientVisitNumber").value == "") {
    document.getElementById("SearchPatientVisitNumber").style.backgroundColor = "#d46e6e";
    document.getElementById("SearchPatientVisitNumber").style.color = "#333333";
  }
  else
  {
    document.getElementById("SearchPatientVisitNumber").style.backgroundColor = "#77cf9c";
    document.getElementById("SearchPatientVisitNumber").style.color = "#333333";
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form(Control) {
  var FormMode;
  if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "Unit").value == "") {
      document.getElementById("FormUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUnit").style.color = "#333333";
    } else {
      document.getElementById("FormUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUnit").style.color = "#333333";
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_TextBox_" + FormMode + "Date").value == "" || document.getElementById("FormView_RehabBundleCompliance_Form_TextBox_" + FormMode + "Date").value == "yyyy/mm/dd") {
      document.getElementById("FormDate").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDate").style.color = "#333333";
    } else {
      document.getElementById("FormDate").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDate").style.color = "#333333";
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "") {
      document.getElementById("FormAssessedList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormAssessedList").style.color = "#333333";
    } else {
      document.getElementById("FormAssessedList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormAssessedList").style.color = "#333333";
    }

    Validation_IUC(FormMode, Control);
    Validation_SPC(FormMode, Control)
    Validation_ICC(FormMode, Control)

    if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4383") {
      if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").checked == true) {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "IUC5CatheterList").value == "") {
          document.getElementById("FormIUC5Catheter").style.backgroundColor = "#d46e6e";
          document.getElementById("FormIUC5Catheter").style.color = "#333333";
        } else {
          document.getElementById("FormIUC5Catheter").style.backgroundColor = "#77cf9c";
          document.getElementById("FormIUC5Catheter").style.color = "#333333";
        }
      } else {
        document.getElementById("FormIUC5Catheter").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormIUC5Catheter").style.color = "#333333";
        document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "IUC5CatheterList").value = "";
      }
    } else {
      document.getElementById("FormIUC5Catheter").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormIUC5Catheter").style.color = "#333333";
      document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "IUC5CatheterList").value = "";
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4384") {
      if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").checked == true) {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "SPC5CatheterList").value == "") {
          document.getElementById("FormSPC5Catheter").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSPC5Catheter").style.color = "#333333";
        } else {
          document.getElementById("FormSPC5Catheter").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSPC5Catheter").style.color = "#333333";
        }
      } else {
        document.getElementById("FormSPC5Catheter").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSPC5Catheter").style.color = "#333333";
        document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "SPC5CatheterList").value = "";
      }
    } else {
      document.getElementById("FormSPC5Catheter").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSPC5Catheter").style.color = "#333333";
      document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "SPC5CatheterList").value = "";
    }
  }
}


function Validation_IUC(FormMode, Control) {
  if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value != "4383") {
    document.getElementById("FormIUC").style.backgroundColor = "#f7f7f7";
    document.getElementById("FormIUC").style.color = "#333333";
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").disabled = true;
  } else {
    document.getElementById("FormIUC").style.backgroundColor = "#77cf9c";
    document.getElementById("FormIUC").style.color = "#333333";

    if (Control == undefined) {
      if (FormMode == "Insert") {
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").disabled = false;
      } else {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").checked == true) {
          document.getElementById("FormIUC").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormIUC").style.color = "#333333";

          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "AssessedIUC").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").disabled = true;

          if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "") {
            document.getElementById("FormAssessedList").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessedList").style.color = "#333333";
          } else {
            document.getElementById("FormAssessedList").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessedList").style.color = "#333333";
          }
        }
      }
    } else if (Control != undefined) {
      if (Control == "AssessedList") {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4383") {        
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").disabled = false;
        }
      }

      if (Control == "IUCSelectAll") {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").checked = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").checked = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").checked = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").checked = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").checked = true;
        }
      }

      if (Control == "IUC1" || Control == "IUC2" || Control == "IUC3" || Control == "IUC4" || Control == "IUC5") {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
        }
      }

      if (Control == "IUC1NA" || Control == "IUC2NA" || Control == "IUC3NA" || Control == "IUC4NA" || Control == "IUC5NA") {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").checked == true) {
          document.getElementById("FormIUC").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormIUC").style.color = "#333333";

          document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value = ""
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUCSelectAll").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").disabled = true;

          if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "") {
            document.getElementById("FormAssessedList").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessedList").style.color = "#333333";
          } else {
            document.getElementById("FormAssessedList").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessedList").style.color = "#333333";
          }
        }
      }
    }
  }
}


function Validation_SPC(FormMode, Control) {
  if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value != "4384") {
    document.getElementById("FormSPC").style.backgroundColor = "#f7f7f7";
    document.getElementById("FormSPC").style.color = "#333333";
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").disabled = true;
  } else {
    document.getElementById("FormSPC").style.backgroundColor = "#77cf9c";
    document.getElementById("FormSPC").style.color = "#333333";

    if (Control == undefined) {
      if (FormMode == "Insert") {
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").disabled = false;
      } else {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").checked == true) {
          document.getElementById("FormSPC").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormSPC").style.color = "#333333";

          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "AssessedSPC").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").disabled = true;

          if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "") {
            document.getElementById("FormAssessedList").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessedList").style.color = "#333333";
          } else {
            document.getElementById("FormAssessedList").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessedList").style.color = "#333333";
          }
        }
      }
    } else if (Control != undefined) {
      if (Control == "AssessedList") {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4384") {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").disabled = false;
        }
      }

      if (Control == "SPCSelectAll") {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").checked = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").checked = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").checked = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").checked = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").checked = true;
        }
      }

      if (Control == "SPC1" || Control == "SPC2" || Control == "SPC3" || Control == "SPC4" || Control == "SPC5") {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
        }
      }

      if (Control == "SPC1NA" || Control == "SPC2NA" || Control == "SPC3NA" || Control == "SPC4NA" || Control == "SPC5NA") {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").checked == true) {
          document.getElementById("FormSPC").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormSPC").style.color = "#333333";

          document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value = ""
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPCSelectAll").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").disabled = true;

          if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "") {
            document.getElementById("FormAssessedList").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessedList").style.color = "#333333";
          } else {
            document.getElementById("FormAssessedList").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessedList").style.color = "#333333";
          }
        }
      }
    }
  }
}


function Validation_ICC(FormMode, Control) {
  if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value != "4385" && document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value != "4394") {
    document.getElementById("FormICC").style.backgroundColor = "#f7f7f7";
    document.getElementById("FormICC").style.color = "#333333";
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").disabled = true;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").checked = false;
    document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").disabled = true;
  } else {
    document.getElementById("FormICC").style.backgroundColor = "#77cf9c";
    document.getElementById("FormICC").style.color = "#333333";

    if (Control == undefined) {
      if (FormMode == "Insert") {
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").disabled = false;
        document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").disabled = false;
      } else {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").checked == true) {
          document.getElementById("FormICC").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormICC").style.color = "#333333";

          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "AssessedICC").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").disabled = true;

          if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "") {
            document.getElementById("FormAssessedList").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessedList").style.color = "#333333";
          } else {
            document.getElementById("FormAssessedList").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessedList").style.color = "#333333";
          }
        }
      }
    } else if (Control != undefined) {
      if (Control == "AssessedList") {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4385" || document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4394") {
          if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "AssessedList").value != "") {
            if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "AssessedList").value != document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value) {
              document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
              document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").checked = false;
              document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").checked = false;
              document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").checked = false;
              document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").checked = false;
              document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").checked = false;
              document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").checked = false;
              document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").checked = false;
              document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").checked = false;
            }
          }

          document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "AssessedList").value = document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value;

          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").disabled = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").disabled = false;
        }
      }

      if (Control == "ICCSelectAll") {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").checked = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").checked = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").checked = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").checked = true;
        }
      }

      if (Control == "ICC1" || Control == "ICC2" || Control == "ICC3" || Control == "ICC4") {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").disabled = true;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
        }
      }

      if (Control == "ICC1NA" || Control == "ICC2NA" || Control == "ICC3NA" || Control == "ICC4NA") {
        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").checked == true) {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").disabled = true;
        } else {
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").disabled = false;
        }

        if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").checked == true && document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").checked == true) {
          document.getElementById("FormICC").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormICC").style.color = "#333333";

          document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value = ""
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICCSelectAll").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").disabled = true;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").checked = false;
          document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").disabled = true;

          if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "") {
            document.getElementById("FormAssessedList").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessedList").style.color = "#333333";
          } else {
            document.getElementById("FormAssessedList").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessedList").style.color = "#333333";
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
  if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    Calculation_IUC(FormMode);
    Calculation_SPC(FormMode)
    Calculation_ICC(FormMode)
  }
}


function Calculation_IUC(FormMode) {
  if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4383") {
    var IUC_Total;
    var IUC_Selected;
    IUC_Total = 0;
    IUC_Selected = 0;

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1").checked == true) {
      IUC_Total = IUC_Total + 1;
    } else {
      IUC_Total = IUC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2").checked == true) {
      IUC_Total = IUC_Total + 1;
    } else {
      IUC_Total = IUC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3").checked == true) {
      IUC_Total = IUC_Total + 1;
    } else {
      IUC_Total = IUC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4").checked == true) {
      IUC_Total = IUC_Total + 1;
    } else {
      IUC_Total = IUC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").checked == true) {
      IUC_Total = IUC_Total + 1;
    } else {
      IUC_Total = IUC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC1NA").checked == false) {
      IUC_Selected = IUC_Selected + 1;
    } else {
      IUC_Selected = IUC_Selected + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC2NA").checked == false) {
      IUC_Selected = IUC_Selected + 1;
    } else {
      IUC_Selected = IUC_Selected + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC3NA").checked == false) {
      IUC_Selected = IUC_Selected + 1;
    } else {
      IUC_Selected = IUC_Selected + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC4NA").checked == false) {
      IUC_Selected = IUC_Selected + 1;
    } else {
      IUC_Selected = IUC_Selected + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5NA").checked == false) {
      IUC_Selected = IUC_Selected + 1;
    } else {
      IUC_Selected = IUC_Selected + 0;
    }

    var IUC_Cal = ((IUC_Total / IUC_Selected) * 100);
    IUC_Cal = IUC_Cal.toFixed(0);
    document.getElementById("FormView_RehabBundleCompliance_Form_Textbox_" + FormMode + "IUCCal").value = IUC_Cal + " %";
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4384") {
    document.getElementById("FormView_RehabBundleCompliance_Form_Textbox_" + FormMode + "IUCCal").value = "Not Assessed";
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4385" || document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4394") {
    document.getElementById("FormView_RehabBundleCompliance_Form_Textbox_" + FormMode + "IUCCal").value = "Not Assessed";
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "") {
    document.getElementById("FormView_RehabBundleCompliance_Form_Textbox_" + FormMode + "IUCCal").value = "Not Assessed";
  }
}


function Calculation_SPC(FormMode) {
  if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4384") {
    var SPC_Total;
    var SPC_Selected;
    SPC_Total = 0;
    SPC_Selected = 0;

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1").checked == true) {
      SPC_Total = SPC_Total + 1;
    } else {
      SPC_Total = SPC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2").checked == true) {
      SPC_Total = SPC_Total + 1;
    } else {
      SPC_Total = SPC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3").checked == true) {
      SPC_Total = SPC_Total + 1;
    } else {
      SPC_Total = SPC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4").checked == true) {
      SPC_Total = SPC_Total + 1;
    } else {
      SPC_Total = SPC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").checked == true) {
      SPC_Total = SPC_Total + 1;
    } else {
      SPC_Total = SPC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC1NA").checked == false) {
      SPC_Selected = SPC_Selected + 1;
    } else {
      SPC_Selected = SPC_Selected + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC2NA").checked == false) {
      SPC_Selected = SPC_Selected + 1;
    } else {
      SPC_Selected = SPC_Selected + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC3NA").checked == false) {
      SPC_Selected = SPC_Selected + 1;
    } else {
      SPC_Selected = SPC_Selected + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC4NA").checked == false) {
      SPC_Selected = SPC_Selected + 1;
    } else {
      SPC_Selected = SPC_Selected + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5NA").checked == false) {
      SPC_Selected = SPC_Selected + 1;
    } else {
      SPC_Selected = SPC_Selected + 0;
    }

    var SPC_Cal = ((SPC_Total / SPC_Selected) * 100);
    SPC_Cal = SPC_Cal.toFixed(0);
    document.getElementById("FormView_RehabBundleCompliance_Form_Textbox_" + FormMode + "SPCCal").value = SPC_Cal + " %";
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4383") {
    document.getElementById("FormView_RehabBundleCompliance_Form_Textbox_" + FormMode + "SPCCal").value = "Not Assessed";
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4385" || document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4394") {
    document.getElementById("FormView_RehabBundleCompliance_Form_Textbox_" + FormMode + "SPCCal").value = "Not Assessed";
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "") {
    document.getElementById("FormView_RehabBundleCompliance_Form_Textbox_" + FormMode + "SPCCal").value = "Not Assessed";
  }
}


function Calculation_ICC(FormMode) {
  if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4385" || document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4394") {
    var ICC_Total;
    var ICC_Selected;
    ICC_Total = 0;
    ICC_Selected = 0;

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1").checked == true) {
      ICC_Total = ICC_Total + 1;
    } else {
      ICC_Total = ICC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2").checked == true) {
      ICC_Total = ICC_Total + 1;
    } else {
      ICC_Total = ICC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3").checked == true) {
      ICC_Total = ICC_Total + 1;
    } else {
      ICC_Total = ICC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4").checked == true) {
      ICC_Total = ICC_Total + 1;
    } else {
      ICC_Total = ICC_Total + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC1NA").checked == false) {
      ICC_Selected = ICC_Selected + 1;
    } else {
      ICC_Selected = ICC_Selected + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC2NA").checked == false) {
      ICC_Selected = ICC_Selected + 1;
    } else {
      ICC_Selected = ICC_Selected + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC3NA").checked == false) {
      ICC_Selected = ICC_Selected + 1;
    } else {
      ICC_Selected = ICC_Selected + 0;
    }

    if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "ICC4NA").checked == false) {
      ICC_Selected = ICC_Selected + 1;
    } else {
      ICC_Selected = ICC_Selected + 0;
    }

    var ICC_Cal = ((ICC_Total / ICC_Selected) * 100);
    ICC_Cal = ICC_Cal.toFixed(0);
    document.getElementById("FormView_RehabBundleCompliance_Form_Textbox_" + FormMode + "ICCCal").value = ICC_Cal + " %";
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4383") {
    document.getElementById("FormView_RehabBundleCompliance_Form_Textbox_" + FormMode + "ICCCal").value = "Not Assessed";
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4384") {
    document.getElementById("FormView_RehabBundleCompliance_Form_Textbox_" + FormMode + "ICCCal").value = "Not Assessed";
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "") {
    document.getElementById("FormView_RehabBundleCompliance_Form_Textbox_" + FormMode + "ICCCal").value = "Not Assessed";
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form() {
  var FormMode;
  if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_Edit")) {
    FormMode = "Edit"
  } else if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_Item")) {
    FormMode = "Item"
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (FormMode != "Item") {
      if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "") {
        Hide("FormIUCSelectAll");
        Hide("FormIUC1");
        Hide("FormIUC2");
        Hide("FormIUC3");
        Hide("FormIUC4");
        Hide("FormIUC5");
        Hide("FormIUC5CatheterList");
        Hide("FormIUCCAL");

        Hide("FormSPCSelectAll");
        Hide("FormSPC1");
        Hide("FormSPC2");
        Hide("FormSPC3");
        Hide("FormSPC4");
        Hide("FormSPC5");
        Hide("FormSPC5CatheterList");
        Hide("FormSPCCAL");

        Hide("FormICCSelectAll");
        Hide("FormICC1");
        Hide("FormICC2");
        Hide("FormICC3");
        Hide("FormICC4");
        Hide("FormICCCAL");
      } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4383") {
        Show("FormIUCSelectAll");
        Show("FormIUC1");
        Show("FormIUC2");
        Show("FormIUC3");
        Show("FormIUC4");
        Show("FormIUC5");
        Show("FormIUCCAL");

        if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4383") {
          if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "IUC5").checked == true) {
            Show("FormIUC5CatheterList");
          } else {
            Hide("FormIUC5CatheterList");
          }
        } else {
          Hide("FormIUC5CatheterList");
        }

        Hide("FormSPCSelectAll");
        Hide("FormSPC1");
        Hide("FormSPC2");
        Hide("FormSPC3");
        Hide("FormSPC4");
        Hide("FormSPC5");
        Hide("FormSPC5CatheterList");
        Hide("FormSPCCAL");

        Hide("FormICCSelectAll");
        Hide("FormICC1");
        Hide("FormICC2");
        Hide("FormICC3");
        Hide("FormICC4");
        Hide("FormICCCAL");
      } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4384") {
        Hide("FormIUCSelectAll");
        Hide("FormIUC1");
        Hide("FormIUC2");
        Hide("FormIUC3");
        Hide("FormIUC4");
        Hide("FormIUC5");
        Hide("FormIUC5CatheterList");
        Hide("FormIUCCAL");

        Show("FormSPCSelectAll");
        Show("FormSPC1");
        Show("FormSPC2");
        Show("FormSPC3");
        Show("FormSPC4");
        Show("FormSPC5");
        Show("FormSPCCAL");

        if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4384") {
          if (document.getElementById("FormView_RehabBundleCompliance_Form_CheckBox_" + FormMode + "SPC5").checked == true) {
            Show("FormSPC5CatheterList");
          } else {
            Hide("FormSPC5CatheterList");
          }
        } else {
          Hide("FormSPC5CatheterList");
        }

        Hide("FormICCSelectAll");
        Hide("FormICC1");
        Hide("FormICC2");
        Hide("FormICC3");
        Hide("FormICC4");
        Hide("FormICCCAL");
      } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4385" || document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4394") {
        Hide("FormIUCSelectAll");
        Hide("FormIUC1");
        Hide("FormIUC2");
        Hide("FormIUC3");
        Hide("FormIUC4");
        Hide("FormIUC5");
        Hide("FormIUC5CatheterList");
        Hide("FormIUCCAL");

        Hide("FormSPCSelectAll");
        Hide("FormSPC1");
        Hide("FormSPC2");
        Hide("FormSPC3");
        Hide("FormSPC4");
        Hide("FormSPC5");
        Hide("FormSPC5CatheterList");
        Hide("FormSPCCAL");

        Show("FormICCSelectAll");
        Show("FormICC1");
        Show("FormICC2");
        Show("FormICC3");
        Show("FormICC4");
        Show("FormICCCAL");

        if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4385") {
          Show("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC1_TextRehabilitationStaff");
          Hide("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC1_TextPatientPrivateCaregiver");
          Show("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC3_TextRehabilitationStaff");
          Hide("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC3_TextPatientPrivateCaregiver");
        } else if (document.getElementById("FormView_RehabBundleCompliance_Form_DropDownList_" + FormMode + "AssessedList").value == "4394") {
          Hide("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC1_TextRehabilitationStaff");
          Show("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC1_TextPatientPrivateCaregiver");
          Hide("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC3_TextRehabilitationStaff");
          Show("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC3_TextPatientPrivateCaregiver");
        }
      }
    }

    if (FormMode == "Item") {
      if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "AssessedList").value == "") {
        Hide("FormIUCSelectAll");
        Hide("FormIUC1");
        Hide("FormIUC2");
        Hide("FormIUC3");
        Hide("FormIUC4");
        Hide("FormIUC5");
        Hide("FormIUC5CatheterList");
        Hide("FormIUCCAL");

        Hide("FormSPCSelectAll");
        Hide("FormSPC1");
        Hide("FormSPC2");
        Hide("FormSPC3");
        Hide("FormSPC4");
        Hide("FormSPC5");
        Hide("FormSPC5CatheterList");
        Hide("FormSPCCAL");

        Hide("FormICCSelectAll");
        Hide("FormICC1");
        Hide("FormICC2");
        Hide("FormICC3");
        Hide("FormICC4");
        Hide("FormICCCAL");
      } else if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "AssessedList").value == "4383") {
        Show("FormIUCSelectAll");
        Show("FormIUC1");
        Show("FormIUC2");
        Show("FormIUC3");
        Show("FormIUC4");
        Show("FormIUC5");
        Show("FormIUCCAL");

        if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "AssessedList").value == "4383") {
          if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "IUC5").value == "True") {
            Show("FormIUC5CatheterList");
          } else {
            Hide("FormIUC5CatheterList");
          }
        } else {
          Hide("FormIUC5CatheterList");
        }

        Hide("FormSPCSelectAll");
        Hide("FormSPC1");
        Hide("FormSPC2");
        Hide("FormSPC3");
        Hide("FormSPC4");
        Hide("FormSPC5");
        Hide("FormSPC5CatheterList");
        Hide("FormSPCCAL");

        Hide("FormICCSelectAll");
        Hide("FormICC1");
        Hide("FormICC2");
        Hide("FormICC3");
        Hide("FormICC4");
        Hide("FormICCCAL");
      } else if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "AssessedList").value == "4384") {
        Hide("FormIUCSelectAll");
        Hide("FormIUC1");
        Hide("FormIUC2");
        Hide("FormIUC3");
        Hide("FormIUC4");
        Hide("FormIUC5");
        Hide("FormIUC5CatheterList");
        Hide("FormIUCCAL");

        Show("FormSPCSelectAll");
        Show("FormSPC1");
        Show("FormSPC2");
        Show("FormSPC3");
        Show("FormSPC4");
        Show("FormSPC5");
        Show("FormSPCCAL");

        if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "AssessedList").value == "4384") {
          if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "SPC5").value == "True") {
            Show("FormSPC5CatheterList");
          } else {
            Hide("FormSPC5CatheterList");
          }
        } else {
          Hide("FormSPC5CatheterList");
        }

        Hide("FormICCSelectAll");
        Hide("FormICC1");
        Hide("FormICC2");
        Hide("FormICC3");
        Hide("FormICC4");
        Hide("FormICCCAL");
      } else if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "AssessedList").value == "4385" || document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "AssessedList").value == "4394") {
        Hide("FormIUCSelectAll");
        Hide("FormIUC1");
        Hide("FormIUC2");
        Hide("FormIUC3");
        Hide("FormIUC4");
        Hide("FormIUC5");
        Hide("FormIUC5CatheterList");
        Hide("FormIUCCAL");

        Hide("FormSPCSelectAll");
        Hide("FormSPC1");
        Hide("FormSPC2");
        Hide("FormSPC3");
        Hide("FormSPC4");
        Hide("FormSPC5");
        Hide("FormSPC5CatheterList");
        Hide("FormSPCCAL");

        Show("FormICCSelectAll");
        Show("FormICC1");
        Show("FormICC2");
        Show("FormICC3");
        Show("FormICC4");
        Show("FormICCCAL");

        if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "AssessedList").value == "4385") {
          Show("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC1_TextRehabilitationStaff");
          Hide("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC1_TextPatientPrivateCaregiver");
          Show("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC3_TextRehabilitationStaff");
          Hide("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC3_TextPatientPrivateCaregiver");
        } else if (document.getElementById("FormView_RehabBundleCompliance_Form_HiddenField_" + FormMode + "AssessedList").value == "4394") {
          Hide("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC1_TextRehabilitationStaff");
          Show("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC1_TextPatientPrivateCaregiver");
          Hide("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC3_TextRehabilitationStaff");
          Show("FormView_RehabBundleCompliance_Form_Label_" + FormMode + "ICC3_TextPatientPrivateCaregiver");
        }
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
