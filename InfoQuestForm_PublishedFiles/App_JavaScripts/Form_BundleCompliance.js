
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
function Validation_Form(Control) {
  var FormMode;
  if (document.getElementById("FormView_BundleCompliance_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_BundleCompliance_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "Unit").value == "") {
      document.getElementById("FormUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUnit").style.color = "#333333";
    } else {
      document.getElementById("FormUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUnit").style.color = "#333333";
    }

    if (document.getElementById("FormView_BundleCompliance_Form_TextBox_" + FormMode + "Date").value == "" || document.getElementById("FormView_BundleCompliance_Form_TextBox_" + FormMode + "Date").value == "yyyy/mm/dd") {
      document.getElementById("FormDate").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDate").style.color = "#333333";
    } else {
      document.getElementById("FormDate").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDate").style.color = "#333333";
    }

    if ((document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "Unit").value == "") || (document.getElementById("FormView_BundleCompliance_Form_TextBox_" + FormMode + "Date").value == "" || document.getElementById("FormView_BundleCompliance_Form_TextBox_" + FormMode + "Date").value == "yyyy/mm/dd")) {
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").disabled = true;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").disabled = true;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").disabled = true;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").disabled = true;

      document.getElementById("FormAssessed").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormAssessed").style.color = "#333333";
    } else {
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").disabled = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").disabled = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").disabled = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").disabled = false;

      if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked == false) {
        document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
        document.getElementById("FormAssessed").style.color = "#333333";
      } else {
        document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
        document.getElementById("FormAssessed").style.color = "#333333";
      }
    }

    Validation_SSI(FormMode, Control);
    Validation_CLABSI(FormMode, Control)
    Validation_VAP(FormMode, Control)
    Validation_CAUTI(FormMode, Control)
  }
}


function Validation_SSI(FormMode, Control) {
  if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked == false) {
    document.getElementById("FormSSI").style.backgroundColor = "#f7f7f7";
    document.getElementById("FormSSI").style.color = "#333333";

    document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").value = "";
    document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_Label_" + FormMode + "SSITheatreProcedureError").style.display = 'none';
    document.getElementById("FormSSITheatreProcedure").style.backgroundColor = "#f7f7f7";
    document.getElementById("FormSSITheatreProcedure").style.color = "#333333";

    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").disabled = true;
  }
  else
  {
    document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").disabled = false;

    var TheatreProcedureValue = document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure");
    var TheatreProcedureText = TheatreProcedureValue[TheatreProcedureValue.selectedIndex].innerText;
    var TheatreProcedureIndex = TheatreProcedureText.indexOf("NO : ", 0);

    if (document.getElementById("FormView_BundleCompliance_Form_HiddenField_" + FormMode + "SSITheatreProcedure")) {
      if (document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").value == document.getElementById("FormView_BundleCompliance_Form_HiddenField_" + FormMode + "SSITheatreProcedure").value) {
        TheatreProcedureIndex = 1;
      } 
    }

    if (document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").value == "") {
      document.getElementById("FormSSITheatreProcedure").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSSITheatreProcedure").style.color = "#333333";
      document.getElementById("FormView_BundleCompliance_Form_Label_" + FormMode + "SSITheatreProcedureError").style.display = 'none';
      document.getElementById("FormSSI").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSSI").style.color = "#333333";
    }
    else
    {
      if (TheatreProcedureIndex == -1) {
        document.getElementById("FormSSITheatreProcedure").style.backgroundColor = "#d46e6e";
        document.getElementById("FormSSITheatreProcedure").style.color = "#333333";

        document.getElementById("FormSSI").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSSI").style.color = "#333333";

        if (document.getElementById("FormView_BundleCompliance_Form_HiddenField_" + FormMode + "SSITheatreProcedure")) {
          if (document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").value != document.getElementById("FormView_BundleCompliance_Form_HiddenField_" + FormMode + "SSITheatreProcedure").value) {
            document.getElementById("FormView_BundleCompliance_Form_Label_" + FormMode + "SSITheatreProcedureError").style.display = '';
          }
          else
          {
            document.getElementById("FormView_BundleCompliance_Form_Label_" + FormMode + "SSITheatreProcedureError").style.display = 'none';
          }
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_Label_" + FormMode + "SSITheatreProcedureError").style.display = '';
        }
      }
      else
      {
        document.getElementById("FormSSITheatreProcedure").style.backgroundColor = "#77cf9c";
        document.getElementById("FormSSITheatreProcedure").style.color = "#333333";

        document.getElementById("FormSSI").style.backgroundColor = "#77cf9c";
        document.getElementById("FormSSI").style.color = "#333333";

        document.getElementById("FormView_BundleCompliance_Form_Label_" + FormMode + "SSITheatreProcedureError").style.display = 'none';
      }      
    }

    if (document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").value == "") {
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").disabled = true;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").disabled = true;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").disabled = true;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").disabled = true;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked = false;
      document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").disabled = true;
    }
    else
    {
      if (TheatreProcedureIndex == -1) {
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").disabled = true;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").disabled = true;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").disabled = true;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").disabled = true;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").disabled = true;
      }
      else
      {
        if (Control == undefined) {
          if (FormMode == "Insert") {
            document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").disabled = false;
            document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = false;
            document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").disabled = false;
            document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = false;
            document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").disabled = false;
            document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = false;
            document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").disabled = false;
            document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = false;
            document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").disabled = false;
          }
          else
          {
            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = false;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = false;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = false;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = false;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked == true) {
              document.getElementById("FormSSI").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormSSI").style.color = "#333333";
              document.getElementById("FormSSITheatreProcedure").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormSSITheatreProcedure").style.color = "#333333";

              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").value = "";
              document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").disabled = true;

              if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked == false) {
                document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
                document.getElementById("FormAssessed").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
                document.getElementById("FormAssessed").style.color = "#333333";
              }
            }
          }
        }
        else if (Control != undefined)
        {
          if (FormMode == "Insert") {
            if (Control == "SSISelectAll") {
            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = false;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = false;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = true;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = true;
            }
          }

          if (Control == "SSI1" || Control == "SSI2" || Control == "SSI3" || Control == "SSI4") {
            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
            }
          }

            if (Control == "SSI1NA" || Control == "SSI2NA" || Control == "SSI3NA" || Control == "SSI4NA") {
            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = false;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = false;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = false;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked == true) {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
            }
            else
            {
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = false;
            }

            if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked == true) {
              document.getElementById("FormSSI").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormSSI").style.color = "#333333";
              document.getElementById("FormSSITheatreProcedure").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormSSITheatreProcedure").style.color = "#333333";

              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").value = "";
              document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked = false;
              document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").disabled = true;

              if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked == false) {
                document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
                document.getElementById("FormAssessed").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
                document.getElementById("FormAssessed").style.color = "#333333";
              }
            }
          }
          }
          else
          {
            if (Control == "SSITheatreProcedure") {
              if (TheatreProcedureIndex == -1) {
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").disabled = true;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").disabled = true;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").disabled = true;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").disabled = true;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").disabled = true;
              }
              else if (TheatreProcedureIndex == 0)
              {
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").disabled = false;
              }
              else if (TheatreProcedureIndex == 1)
              {
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked = false;
                document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").disabled = false;
              }
            }
            else
            {
              if (Control == "SSISelectAll") {
                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
                }
                else
                {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = true;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = false;
                }

                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
                }
                else
                {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = true;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = false;
                }

                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
                }
                else
                {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = true;
                }

                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
                }
                else
                {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = true;
                }
              }

              if (Control == "SSI1" || Control == "SSI2" || Control == "SSI3" || Control == "SSI4") {
                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
                }

                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
                }

                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
                }

                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
                }

                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = true;
                }
                else
                {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                }
              }

              if (Control == "SSI1NA" || Control == "SSI2NA" || Control == "SSI3NA" || Control == "SSI4NA") {
                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
                }
                else
                {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = false;
                }

                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
                }
                else
                {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = false;
                }

                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
                }
                else
                {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = false;
                }

                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked == true) {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
                }
                else
                {
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = false;
                }

                if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked == true) {
                  document.getElementById("FormSSI").style.backgroundColor = "#f7f7f7";
                  document.getElementById("FormSSI").style.color = "#333333";
                  document.getElementById("FormSSITheatreProcedure").style.backgroundColor = "#f7f7f7";
                  document.getElementById("FormSSITheatreProcedure").style.color = "#333333";

                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").value = "";
                  document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").disabled = true;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSISelectAll").disabled = true;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").disabled = true;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").disabled = true;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").disabled = true;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").disabled = true;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").disabled = true;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").disabled = true;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").disabled = true;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked = false;
                  document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").disabled = true;

                  if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked == false) {
                    document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
                    document.getElementById("FormAssessed").style.color = "#333333";
                  }
                  else
                  {
                    document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
                    document.getElementById("FormAssessed").style.color = "#333333";
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}


function Validation_CLABSI(FormMode, Control) {
  if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked == false) {
    document.getElementById("FormCLABSI").style.backgroundColor = "#f7f7f7";
    document.getElementById("FormCLABSI").style.color = "#333333";
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").disabled = true;
  }
  else
  {
    document.getElementById("FormCLABSI").style.backgroundColor = "#77cf9c";
    document.getElementById("FormCLABSI").style.color = "#333333";

    if (Control == undefined) {
      if (FormMode == "Insert") {
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").disabled = false;
      }
      else
      {
        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").checked == true) {
          document.getElementById("FormCLABSI").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormCLABSI").style.color = "#333333";

          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").disabled = true;

          if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked == false) {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    }
    else if (Control != undefined)
    {
      if (Control == "AssessedCLABSI") {
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").disabled = false;
      }

      if (Control == "CLABSISelectAll") {
        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").checked = true;
        }
      }

      if (Control == "CLABSI1" || Control == "CLABSI2" || Control == "CLABSI3" || Control == "CLABSI4" || Control == "CLABSI5" || Control == "CLABSI6" || Control == "CLABSI7") {
        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
        }
      }

      if (Control == "CLABSI1NA" || Control == "CLABSI2NA" || Control == "CLABSI3NA" || Control == "CLABSI4NA" || Control == "CLABSI5NA" || Control == "CLABSI6NA" || Control == "CLABSI7NA") {
        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").disabled = true;
        }
        else
        {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").checked == true) {
          document.getElementById("FormCLABSI").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormCLABSI").style.color = "#333333";

          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSISelectAll").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").disabled = true;

          if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked == false) {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    }
  }
}


function Validation_VAP(FormMode, Control) {
  if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked == false) {
    document.getElementById("FormVAP").style.backgroundColor = "#f7f7f7";
    document.getElementById("FormVAP").style.color = "#333333";
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").disabled = true;
  } else {
    document.getElementById("FormVAP").style.backgroundColor = "#77cf9c";
    document.getElementById("FormVAP").style.color = "#333333";

    if (Control == undefined) {
      if (FormMode == "Insert") {
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").disabled = false;
      } else {
        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").checked == true) {
          document.getElementById("FormVAP").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormVAP").style.color = "#333333";

          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").disabled = true;

          if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked == false) {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          } else {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    } else if (Control != undefined) {
      if (Control == "AssessedVAP") {
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").disabled = false;
      }

      if (Control == "VAPSelectAll") {
        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").checked = true;
        }
      }

      if (Control == "VAP1" || Control == "VAP2" || Control == "VAP3" || Control == "VAP4" || Control == "VAP5") {
        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
        }
      }

      if (Control == "VAP1NA" || Control == "VAP2NA" || Control == "VAP3NA" || Control == "VAP4NA" || Control == "VAP5NA") {
        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").checked == true) {
          document.getElementById("FormVAP").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormVAP").style.color = "#333333";

          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAPSelectAll").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").disabled = true;

          if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked == false) {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          } else {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    }
  }
}


function Validation_CAUTI(FormMode, Control) {
  if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked == false) {
    document.getElementById("FormCAUTI").style.backgroundColor = "#f7f7f7";
    document.getElementById("FormCAUTI").style.color = "#333333";
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").disabled = true;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").checked = false;
    document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").disabled = true;
  } else {
    document.getElementById("FormCAUTI").style.backgroundColor = "#77cf9c";
    document.getElementById("FormCAUTI").style.color = "#333333";

    if (Control == undefined) {
      if (FormMode == "Insert") {
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").disabled = false;
      } else {
        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").checked == true) {
          document.getElementById("FormCAUTI").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormCAUTI").style.color = "#333333";

          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").disabled = true;

          if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked == false) {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          } else {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    } else if (Control != undefined) {
      if (Control == "AssessedCAUTI") {
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").disabled = false;
        document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").disabled = false;
      }

      if (Control == "CAUTISelectAll") {
        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").checked = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").checked = true;
        }
      }

      if (Control == "CAUTI1" || Control == "CAUTI2" || Control == "CAUTI3" || Control == "CAUTI4") {
        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").disabled = true;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
        }
      }

      if (Control == "CAUTI1NA" || Control == "CAUTI2NA" || Control == "CAUTI3NA" || Control == "CAUTI4NA") {
        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").checked == true) {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").disabled = true;
        } else {
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").disabled = false;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").checked == true && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").checked == true) {
          document.getElementById("FormCAUTI").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormCAUTI").style.color = "#333333";

          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTISelectAll").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").disabled = true;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").checked = false;
          document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").disabled = true;

          if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked == false && document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked == false) {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          } else {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
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
  if (document.getElementById("FormView_BundleCompliance_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_BundleCompliance_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    Calculation_SSI(FormMode);
    Calculation_CLABSI(FormMode);
    Calculation_VAP(FormMode);
    Calculation_CAUTI(FormMode);
  }
}


function Calculation_SSI(FormMode) {
  if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedSSI").checked == false) {
    document.getElementById("FormView_BundleCompliance_Form_Textbox_" + FormMode + "SSICal").value = "Not Assessed";
  }
  else
  {
    if (document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").value == "") {
      document.getElementById("FormView_BundleCompliance_Form_Textbox_" + FormMode + "SSICal").value = "Not Assessed";
    } else {
      var TheatreProcedureValue = document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure");
      var TheatreProcedureText = TheatreProcedureValue[TheatreProcedureValue.selectedIndex].innerText;
      var TheatreProcedureIndex = TheatreProcedureText.indexOf("NO : ", 0);

      if (document.getElementById("FormView_BundleCompliance_Form_HiddenField_" + FormMode + "SSITheatreProcedure")) {
        if (document.getElementById("FormView_BundleCompliance_Form_DropDownList_" + FormMode + "SSITheatreProcedure").value == document.getElementById("FormView_BundleCompliance_Form_HiddenField_" + FormMode + "SSITheatreProcedure").value) {
          TheatreProcedureIndex = 0;
        }
      }

      if (TheatreProcedureIndex == -1) {
        document.getElementById("FormView_BundleCompliance_Form_Textbox_" + FormMode + "SSICal").value = "Not Assessed";
      } else {
        var SSI_Total;
        var SSI_Selected;
        SSI_Total = 0;
        SSI_Selected = 0;

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1").checked == true) {
          SSI_Total = SSI_Total + 1;
        } else {
          SSI_Total = SSI_Total + 0;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2").checked == true) {
          SSI_Total = SSI_Total + 1;
        } else {
          SSI_Total = SSI_Total + 0;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3").checked == true) {
          SSI_Total = SSI_Total + 1;
        } else {
          SSI_Total = SSI_Total + 0;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4").checked == true) {
          SSI_Total = SSI_Total + 1;
        } else {
          SSI_Total = SSI_Total + 0;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI1NA").checked == false) {
          SSI_Selected = SSI_Selected + 1;
        } else {
          SSI_Selected = SSI_Selected + 0;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI2NA").checked == false) {
          SSI_Selected = SSI_Selected + 1;
        } else {
          SSI_Selected = SSI_Selected + 0;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI3NA").checked == false) {
          SSI_Selected = SSI_Selected + 1;
        } else {
          SSI_Selected = SSI_Selected + 0;
        }

        if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "SSI4NA").checked == false) {
          SSI_Selected = SSI_Selected + 1;
        } else {
          SSI_Selected = SSI_Selected + 0;
        }

        var SSI_Cal = ((SSI_Total / SSI_Selected) * 100);
        SSI_Cal = SSI_Cal.toFixed(0);
        document.getElementById("FormView_BundleCompliance_Form_Textbox_" + FormMode + "SSICal").value = SSI_Cal + " %";
      }
    }
  }
}


function Calculation_CLABSI(FormMode) {
  if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCLABSI").checked == false) {
    document.getElementById("FormView_BundleCompliance_Form_Textbox_" + FormMode + "CLABSICal").value = "Not Assessed";
  } else {
    var CLABSI_Total;
    var CLABSI_Selected;
    CLABSI_Total = 0;
    CLABSI_Selected = 0;

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1").checked == true) {
      CLABSI_Total = CLABSI_Total + 1;
    } else {
      CLABSI_Total = CLABSI_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2").checked == true) {
      CLABSI_Total = CLABSI_Total + 1;
    } else {
      CLABSI_Total = CLABSI_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3").checked == true) {
      CLABSI_Total = CLABSI_Total + 1;
    } else {
      CLABSI_Total = CLABSI_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4").checked == true) {
      CLABSI_Total = CLABSI_Total + 1;
    } else {
      CLABSI_Total = CLABSI_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5").checked == true) {
      CLABSI_Total = CLABSI_Total + 1;
    } else {
      CLABSI_Total = CLABSI_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6").checked == true) {
      CLABSI_Total = CLABSI_Total + 1;
    } else {
      CLABSI_Total = CLABSI_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7").checked == true) {
      CLABSI_Total = CLABSI_Total + 1;
    } else {
      CLABSI_Total = CLABSI_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI1NA").checked == false) {
      CLABSI_Selected = CLABSI_Selected + 1;
    } else {
      CLABSI_Selected = CLABSI_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI2NA").checked == false) {
      CLABSI_Selected = CLABSI_Selected + 1;
    } else {
      CLABSI_Selected = CLABSI_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI3NA").checked == false) {
      CLABSI_Selected = CLABSI_Selected + 1;
    } else {
      CLABSI_Selected = CLABSI_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI4NA").checked == false) {
      CLABSI_Selected = CLABSI_Selected + 1;
    } else {
      CLABSI_Selected = CLABSI_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI5NA").checked == false) {
      CLABSI_Selected = CLABSI_Selected + 1;
    } else {
      CLABSI_Selected = CLABSI_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI6NA").checked == false) {
      CLABSI_Selected = CLABSI_Selected + 1;
    } else {
      CLABSI_Selected = CLABSI_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CLABSI7NA").checked == false) {
      CLABSI_Selected = CLABSI_Selected + 1;
    } else {
      CLABSI_Selected = CLABSI_Selected + 0;
    }

    var CLABSI_Cal = ((CLABSI_Total / CLABSI_Selected) * 100);
    CLABSI_Cal = CLABSI_Cal.toFixed(0);
    document.getElementById("FormView_BundleCompliance_Form_Textbox_" + FormMode + "CLABSICal").value = CLABSI_Cal + " %";
  }
}


function Calculation_VAP(FormMode) {
  if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedVAP").checked == false) {
    document.getElementById("FormView_BundleCompliance_Form_Textbox_" + FormMode + "VAPCal").value = "Not Assessed";
  } else {
    var VAP_Total;
    var VAP_Selected;
    VAP_Total = 0;
    VAP_Selected = 0;

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1").checked == true) {
      VAP_Total = VAP_Total + 1;
    } else {
      VAP_Total = VAP_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2").checked == true) {
      VAP_Total = VAP_Total + 1;
    } else {
      VAP_Total = VAP_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3").checked == true) {
      VAP_Total = VAP_Total + 1;
    } else {
      VAP_Total = VAP_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4").checked == true) {
      VAP_Total = VAP_Total + 1;
    } else {
      VAP_Total = VAP_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5").checked == true) {
      VAP_Total = VAP_Total + 1;
    } else {
      VAP_Total = VAP_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP1NA").checked == false) {
      VAP_Selected = VAP_Selected + 1;
    } else {
      VAP_Selected = VAP_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP2NA").checked == false) {
      VAP_Selected = VAP_Selected + 1;
    } else {
      VAP_Selected = VAP_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP3NA").checked == false) {
      VAP_Selected = VAP_Selected + 1;
    } else {
      VAP_Selected = VAP_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP4NA").checked == false) {
      VAP_Selected = VAP_Selected + 1;
    } else {
      VAP_Selected = VAP_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "VAP5NA").checked == false) {
      VAP_Selected = VAP_Selected + 1;
    } else {
      VAP_Selected = VAP_Selected + 0;
    }

    var VAP_Cal = ((VAP_Total / VAP_Selected) * 100);
    VAP_Cal = VAP_Cal.toFixed(0);
    document.getElementById("FormView_BundleCompliance_Form_Textbox_" + FormMode + "VAPCal").value = VAP_Cal + " %";
  }
}


function Calculation_CAUTI(FormMode) {
  if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "AssessedCAUTI").checked == false) {
    document.getElementById("FormView_BundleCompliance_Form_Textbox_" + FormMode + "CAUTICal").value = "Not Assessed";
  } else {
    var CAUTI_Total;
    var CAUTI_Selected;
    CAUTI_Total = 0;
    CAUTI_Selected = 0;

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1").checked == true) {
      CAUTI_Total = CAUTI_Total + 1;
    } else {
      CAUTI_Total = CAUTI_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2").checked == true) {
      CAUTI_Total = CAUTI_Total + 1;
    } else {
      CAUTI_Total = CAUTI_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3").checked == true) {
      CAUTI_Total = CAUTI_Total + 1;
    } else {
      CAUTI_Total = CAUTI_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4").checked == true) {
      CAUTI_Total = CAUTI_Total + 1;
    } else {
      CAUTI_Total = CAUTI_Total + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI1NA").checked == false) {
      CAUTI_Selected = CAUTI_Selected + 1;
    } else {
      CAUTI_Selected = CAUTI_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI2NA").checked == false) {
      CAUTI_Selected = CAUTI_Selected + 1;
    } else {
      CAUTI_Selected = CAUTI_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI3NA").checked == false) {
      CAUTI_Selected = CAUTI_Selected + 1;
    } else {
      CAUTI_Selected = CAUTI_Selected + 0;
    }

    if (document.getElementById("FormView_BundleCompliance_Form_CheckBox_" + FormMode + "CAUTI4NA").checked == false) {
      CAUTI_Selected = CAUTI_Selected + 1;
    } else {
      CAUTI_Selected = CAUTI_Selected + 0;
    }

    var CAUTI_Cal = ((CAUTI_Total / CAUTI_Selected) * 100);
    CAUTI_Cal = CAUTI_Cal.toFixed(0);
    document.getElementById("FormView_BundleCompliance_Form_Textbox_" + FormMode + "CAUTICal").value = CAUTI_Cal + " %";
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form() {
  var FormMode;
  if (document.getElementById("FormView_BundleCompliance_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_BundleCompliance_Form_HiddenField_Edit")) {
    FormMode = "Edit"
  } else if (document.getElementById("FormView_BundleCompliance_Form_HiddenField_Item")) {
    FormMode = "Item"
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (FormMode != "Item") {
    }

    if (FormMode == "Item") {
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