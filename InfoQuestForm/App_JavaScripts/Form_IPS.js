
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --FormPatientInfectionHistory-------------------------------------------------------------------------------------------------------------------
function FormPatientInfectionHistory(PatientInfectionHistoryLink) {
  var width = 800;
  var height = 700;
  var left = (screen.width - width) / 2;
  var top = (screen.height - height) / 2;
  window.open(PatientInfectionHistoryLink, 'PatientInfectionHistory', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=Yes , location=No , scrollbars=Yes , resizable=Yes , status=Yes , left=' + left + ' , top=' + top + ' ');
}

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
  else {
    document.getElementById("SearchFacility").style.backgroundColor = "#77cf9c";
    document.getElementById("SearchFacility").style.color = "#333333";
  }

  if (document.getElementById("TextBox_PatientVisitNumber").value == "") {
    document.getElementById("SearchPatientVisitNumber").style.backgroundColor = "#d46e6e";
    document.getElementById("SearchPatientVisitNumber").style.color = "#333333";
  }
  else {
    document.getElementById("SearchPatientVisitNumber").style.backgroundColor = "#77cf9c";
    document.getElementById("SearchPatientVisitNumber").style.color = "#333333";
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form() {
  var FormMode;
  if (document.getElementById("FormView_IPS_Infection_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_IPS_Infection_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  }
  else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionCategoryList").value == "") {
      document.getElementById("FormInfectionCategoryList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormInfectionCategoryList").style.color = "#333333";
    }
    else {
      document.getElementById("FormInfectionCategoryList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormInfectionCategoryList").style.color = "#333333";
    }

    if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionCategoryList").value == "4799") {
      if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionTypeList").value == "") {
        document.getElementById("FormInfectionTypeList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormInfectionTypeList").style.color = "#333333";
      }
      else {
        document.getElementById("FormInfectionTypeList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormInfectionTypeList").style.color = "#333333";
      }

      if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSubTypeList").length == 1) {
        document.getElementById("FormInfectionSubTypeList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormInfectionSubTypeList").style.color = "#000000";
      }
      else {
        if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSubTypeList").value == "") {
          document.getElementById("FormInfectionSubTypeList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormInfectionSubTypeList").style.color = "#333333";
        }
        else {
          document.getElementById("FormInfectionSubTypeList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormInfectionSubTypeList").style.color = "#333333";
        }
      }

      if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSubSubTypeList").length == 1) {
        document.getElementById("FormInfectionSubSubTypeList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormInfectionSubSubTypeList").style.color = "#000000";
      }
      else {
        if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSubSubTypeList").value == "") {
          document.getElementById("FormInfectionSubSubTypeList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormInfectionSubSubTypeList").style.color = "#333333";
        }
        else {
          document.getElementById("FormInfectionSubSubTypeList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormInfectionSubSubTypeList").style.color = "#333333";
        }
      }

      if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSiteList").value == "") {
        document.getElementById("FormInfectionSiteList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormInfectionSiteList").style.color = "#333333";
      }
      else {
        document.getElementById("FormInfectionSiteList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormInfectionSiteList").style.color = "#333333";
      }

      if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSiteList").value == "4996" || document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSiteList").value == "") {
        document.getElementById("FormInfectionSiteInfectionId").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormInfectionSiteInfectionId").style.color = "#000000";
      }
      else {
        if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSiteInfectionId").value == "" || document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSiteInfectionId").options[document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSiteInfectionId").selectedIndex].text == "Linked Site Required") {
          document.getElementById("FormInfectionSiteInfectionId").style.backgroundColor = "#d46e6e";
          document.getElementById("FormInfectionSiteInfectionId").style.color = "#333333";
        }
        else {
          document.getElementById("FormInfectionSiteInfectionId").style.backgroundColor = "#77cf9c";
          document.getElementById("FormInfectionSiteInfectionId").style.color = "#333333";
        }
      }
    }
    else {
      document.getElementById("FormInfectionTypeList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInfectionTypeList").style.color = "#000000";
      document.getElementById("FormInfectionSubTypeList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInfectionSubTypeList").style.color = "#000000";
      document.getElementById("FormInfectionSubSubTypeList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInfectionSubSubTypeList").style.color = "#000000";
      document.getElementById("FormInfectionSiteList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInfectionSiteList").style.color = "#000000";
      document.getElementById("FormInfectionSiteInfectionId").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInfectionSiteInfectionId").style.color = "#000000";
    }

    if (document.getElementById("FormView_IPS_Infection_Form_CheckBox_" + FormMode + "InfectionScreening").checked == true) {
      var TotalItemsYes = parseInt(document.getElementById("FormView_IPS_Infection_Form_HiddenField_" + FormMode + "ScreeningTypeTypeListTotal").value);
      var CompletedYes = "0";
      for (var aYes = 0; aYes < TotalItemsYes; aYes++) {
        if (document.getElementById("FormView_IPS_Infection_Form_CheckBoxList_" + FormMode + "ScreeningTypeTypeList_" + aYes + "").checked == true) {
          CompletedYes = "1";
          document.getElementById("FormInfectionScreeningType").style.backgroundColor = "#77cf9c";
          document.getElementById("FormInfectionScreeningType").style.color = "#333333";
        }
        else if (document.getElementById("FormView_IPS_Infection_Form_CheckBoxList_" + FormMode + "ScreeningTypeTypeList_" + aYes + "").checked == false && CompletedYes == "0") {
          document.getElementById("FormInfectionScreeningType").style.backgroundColor = "#d46e6e";
          document.getElementById("FormInfectionScreeningType").style.color = "#333333";
        }
      }

      if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionScreeningReasonList").value == "") {
        document.getElementById("FormInfectionScreeningReasonList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormInfectionScreeningReasonList").style.color = "#333333";
      }
      else {
        document.getElementById("FormInfectionScreeningReasonList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormInfectionScreeningReasonList").style.color = "#333333";
      }
    }
    else {
      document.getElementById("FormInfectionScreeningType").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInfectionScreeningType").style.color = "#000000";
      document.getElementById("FormInfectionScreeningReasonList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInfectionScreeningReasonList").style.color = "#000000";
    }

    if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionCategoryList").value == "4799") {
      if (document.getElementById("FormView_IPS_Infection_Form_TextBox_" + FormMode + "InfectionSummary").value == "") {
        document.getElementById("FormInfectionSummary").style.backgroundColor = "#d46e6e";
        document.getElementById("FormInfectionSummary").style.color = "#333333";
      }
      else {
        document.getElementById("FormInfectionSummary").style.backgroundColor = "#77cf9c";
        document.getElementById("FormInfectionSummary").style.color = "#333333";
      }
    }
    else {
      document.getElementById("FormInfectionSummary").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInfectionSummary").style.color = "#000000";
    }

    if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionUnitId").value == "") {
      document.getElementById("FormInfectionUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormInfectionUnit").style.color = "#333333";
    }
    else {
      document.getElementById("FormInfectionUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormInfectionUnit").style.color = "#333333";
    }

    if (document.getElementById("FormView_IPS_Infection_Form_CheckBox_" + FormMode + "IsActive")) {
      if (document.getElementById("FormView_IPS_Infection_Form_CheckBox_" + FormMode + "IsActive").checked == false) {
        if (document.getElementById("FormView_IPS_Infection_Form_TextBox_" + FormMode + "RejectedReason").value == "") {
          document.getElementById("FormRejectedReason").style.backgroundColor = "#d46e6e";
          document.getElementById("FormRejectedReason").style.color = "#333333";
        }
        else {
          document.getElementById("FormRejectedReason").style.backgroundColor = "#77cf9c";
          document.getElementById("FormRejectedReason").style.color = "#333333";
        }
      }
      else {
        document.getElementById("FormRejectedReason").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormRejectedReason").style.color = "#000000";
      }
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form() {
  var FormMode;
  if (document.getElementById("FormView_IPS_Infection_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_IPS_Infection_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  }
  else if (document.getElementById("FormView_IPS_Infection_Form_HiddenField_Item")) {
    FormMode = "Item"
  }
  else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (FormMode != "Item") {
      if (document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionCategoryList").value == "4799") {
        Show("InfectionType");
        Show("InfectionSubType");
        Show("InfectionSubSubType");
        Show("InfectionSite");
        Show("InfectionLinkedSite");
      }
      else {
        document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionTypeList").value = "";
        document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSubTypeList").value = "";
        document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSubSubTypeList").value = "";
        document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSiteList").value = "";
        document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionSiteInfectionId").value = "";
        Hide("InfectionType");
        Hide("InfectionSubType");
        Hide("InfectionSubSubType");
        Hide("InfectionSite");
        Hide("InfectionLinkedSite");
      }

      if (document.getElementById("FormView_IPS_Infection_Form_CheckBox_" + FormMode + "InfectionScreening").checked == true) {
        Show("InfectionScreeningType");
        Show("InfectionScreeningReason");
      }
      else {
        var TotalItemsScreeningType = parseInt(document.getElementById("FormView_IPS_Infection_Form_HiddenField_" + FormMode + "ScreeningTypeTypeListTotal").value);
        for (var a = 0; a < TotalItemsScreeningType; a++) {
          document.getElementById("FormView_IPS_Infection_Form_CheckBoxList_" + FormMode + "ScreeningTypeTypeList_" + a + "").checked = false;
        }

        document.getElementById("FormView_IPS_Infection_Form_DropDownList_" + FormMode + "InfectionScreeningReasonList").value = "";

        Hide("InfectionScreeningType");
        Hide("InfectionScreeningReason");
      }

      if (document.getElementById("FormView_IPS_Infection_Form_CheckBox_" + FormMode + "IsActive")) {
        if (document.getElementById("FormView_IPS_Infection_Form_CheckBox_" + FormMode + "IsActive").checked == false) {
          Show("IPSInfectionRejectedReason");
        }
        else {
          Hide("IPSInfectionRejectedReason");
          document.getElementById("FormView_IPS_Infection_Form_TextBox_" + FormMode + "RejectedReason").value = "";
        }
      }
    }

    if (FormMode == "Item") {
      if (document.getElementById("FormView_IPS_Infection_Form_HiddenField_" + FormMode + "InfectionCategoryList").value == "4799") {
        Show("InfectionType");
        Show("InfectionSubType");
        Show("InfectionSubSubType");
        Show("InfectionSite");
        Show("InfectionLinkedSite");
      }
      else {
        Hide("InfectionType");
        Hide("InfectionSubType");
        Hide("InfectionSubSubType");
        Hide("InfectionSite");
        Hide("InfectionLinkedSite");
      }

      if (document.getElementById("FormView_IPS_Infection_Form_HiddenField_" + FormMode + "InfectionScreening").value == "True") {
        Show("InfectionScreeningType");
        Show("InfectionScreeningReason");
      }
      else {
        Hide("InfectionScreeningType");
        Hide("InfectionScreeningReason");
      }

      if (document.getElementById("FormView_IPS_Infection_Form_HiddenField_" + FormMode + "IsActive").value == "False") {
        Show("IPSInfectionRejectedReason");
      }
      else {
        Hide("IPSInfectionRejectedReason");
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