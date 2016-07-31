
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
//----- --Validation_Link-------------------------------------------------------------------------------------------------------------------------------
function Validation_Link() {
  if ((QueryStringValue("s_Facility_Id") == null) && (QueryStringValue("s_Isidima_PatientVisitNumber") == null)) {

  } else {
    var FormLinkMode;
    if (document.getElementById("FormView_Isidima_AdminLink_DropDownList_InsertLinkedFacility_Id")) {
      FormLinkMode = "Insert";
    } else if (document.getElementById("FormView_Isidima_AdminLink_DropDownList_EditLinkedFacility_Id")) {
      FormLinkMode = "Edit";
    } else {
      FormLinkMode = "";
    }

    if (FormLinkMode != "") {
      if (document.getElementById("FormView_Isidima_AdminLink_DropDownList_" + FormLinkMode + "LinkedFacility_Id").value == "") {
        document.getElementById("LinkedFacility_Id").style.backgroundColor = "#d46e6e";
        document.getElementById("LinkedFacility_Id").style.color = "#333333";
      } else {
        document.getElementById("LinkedFacility_Id").style.backgroundColor = "#77cf9c";
        document.getElementById("LinkedFacility_Id").style.color = "#333333";
      }

      if (document.getElementById("FormView_Isidima_AdminLink_DropDownList_" + FormLinkMode + "LinkedPatientVisitNumber").value == "") {
        document.getElementById("LinkedPatientVisitNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("LinkedPatientVisitNumber").style.color = "#333333";
      } else {
        document.getElementById("LinkedPatientVisitNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("LinkedPatientVisitNumber").style.color = "#333333";
      }      
    }
  }
}
