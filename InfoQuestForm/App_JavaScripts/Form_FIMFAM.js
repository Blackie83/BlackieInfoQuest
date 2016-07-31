
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
  if (document.getElementById("FormView_FIMFAM_Form_TextBox_InsertObservationDate"))
  {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_FIMFAM_Form_TextBox_EditObservationDate"))
  {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_FIMFAM_Form_TextBox_" + FormMode + "ObservationDate").value == "" || document.getElementById("FormView_FIMFAM_Form_TextBox_" + FormMode + "ObservationDate").value == "yyyy/mm/dd")
    {
      document.getElementById("FormObservationDate").style.backgroundColor = "#d46e6e";
      document.getElementById("FormObservationDate").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormObservationDate").style.backgroundColor = "#77cf9c";
      document.getElementById("FormObservationDate").style.color = "#333333";
    }

    if (document.getElementById("FormView_FIMFAM_Form_TextBox_" + FormMode + "OnsetDate"))
    {
      if (document.getElementById("FormView_FIMFAM_Form_TextBox_" + FormMode + "OnsetDate").value == "" || document.getElementById("FormView_FIMFAM_Form_TextBox_" + FormMode + "OnsetDate").value == "yyyy/mm/dd")
      {
        document.getElementById("FormOnsetDate").style.backgroundColor = "#d46e6e";
        document.getElementById("FormOnsetDate").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormOnsetDate").style.backgroundColor = "#77cf9c";
        document.getElementById("FormOnsetDate").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareEating_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareEating_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareEating_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareEating_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareEating_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareEating_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareEating_6").checked == true)
    {
      document.getElementById("FormSelfcareEating").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSelfcareEating").style.color = "#333333";
      document.getElementById("FormSelfcareEating").style.height = "24px";
    } else {
      document.getElementById("FormSelfcareEating").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSelfcareEating").style.color = "#333333";
      document.getElementById("FormSelfcareEating").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareGrooming_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareGrooming_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareGrooming_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareGrooming_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareGrooming_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareGrooming_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareGrooming_6").checked == true)
    {
      document.getElementById("FormSelfcareGrooming").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSelfcareGrooming").style.color = "#333333";
      document.getElementById("FormSelfcareGrooming").style.height = "24px";
    } else {
      document.getElementById("FormSelfcareGrooming").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSelfcareGrooming").style.color = "#333333";
      document.getElementById("FormSelfcareGrooming").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareBathing_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareBathing_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareBathing_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareBathing_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareBathing_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareBathing_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareBathing_6").checked == true)
    {
      document.getElementById("FormSelfcareBathing").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSelfcareBathing").style.color = "#333333";
      document.getElementById("FormSelfcareBathing").style.height = "24px";
    } else {
      document.getElementById("FormSelfcareBathing").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSelfcareBathing").style.color = "#333333";
      document.getElementById("FormSelfcareBathing").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingUpper_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingUpper_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingUpper_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingUpper_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingUpper_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingUpper_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingUpper_6").checked == true)
    {
      document.getElementById("FormSelfcareDressingUpper").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSelfcareDressingUpper").style.color = "#333333";
      document.getElementById("FormSelfcareDressingUpper").style.height = "24px";
    } else {
      document.getElementById("FormSelfcareDressingUpper").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSelfcareDressingUpper").style.color = "#333333";
      document.getElementById("FormSelfcareDressingUpper").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingLower_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingLower_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingLower_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingLower_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingLower_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingLower_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingLower_6").checked == true)
    {
      document.getElementById("FormSelfcareDressingLower").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSelfcareDressingLower").style.color = "#333333";
      document.getElementById("FormSelfcareDressingLower").style.height = "24px";
    } else {
      document.getElementById("FormSelfcareDressingLower").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSelfcareDressingLower").style.color = "#333333";
      document.getElementById("FormSelfcareDressingLower").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareToileting_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareToileting_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareToileting_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareToileting_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareToileting_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareToileting_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareToileting_6").checked == true)
    {
      document.getElementById("FormSelfcareToileting").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSelfcareToileting").style.color = "#333333";
      document.getElementById("FormSelfcareToileting").style.height = "24px";
    } else {
      document.getElementById("FormSelfcareToileting").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSelfcareToileting").style.color = "#333333";
      document.getElementById("FormSelfcareToileting").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareSwallowing_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareSwallowing_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareSwallowing_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareSwallowing_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareSwallowing_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareSwallowing_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareSwallowing_6").checked == true)
    {
      document.getElementById("FormSelfcareSwallowing").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSelfcareSwallowing").style.color = "#333333";
      document.getElementById("FormSelfcareSwallowing").style.height = "24px";
    } else {
      document.getElementById("FormSelfcareSwallowing").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSelfcareSwallowing").style.color = "#333333";
      document.getElementById("FormSelfcareSwallowing").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder1_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder1_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder1_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder1_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder1_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder1_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder1_6").checked == true)
    {
      document.getElementById("FormSphincterBladder1").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSphincterBladder1").style.color = "#333333";
      document.getElementById("FormSphincterBladder1").style.height = "24px";
    } else {
      document.getElementById("FormSphincterBladder1").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSphincterBladder1").style.color = "#333333";
      document.getElementById("FormSphincterBladder1").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder2_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder2_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder2_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder2_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder2_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder2_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder2_6").checked == true)
    {
      document.getElementById("FormSphincterBladder2").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSphincterBladder2").style.color = "#333333";
      document.getElementById("FormSphincterBladder2").style.height = "24px";
    } else {
      document.getElementById("FormSphincterBladder2").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSphincterBladder2").style.color = "#333333";
      document.getElementById("FormSphincterBladder2").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel1_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel1_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel1_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel1_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel1_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel1_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel1_6").checked == true)
    {
      document.getElementById("FormSphincterBowel1").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSphincterBowel1").style.color = "#333333";
      document.getElementById("FormSphincterBowel1").style.height = "24px";
    } else {
      document.getElementById("FormSphincterBowel1").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSphincterBowel1").style.color = "#333333";
      document.getElementById("FormSphincterBowel1").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel2_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel2_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel2_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel2_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel2_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel2_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel2_6").checked == true)
    {
      document.getElementById("FormSphincterBowel2").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSphincterBowel2").style.color = "#333333";
      document.getElementById("FormSphincterBowel2").style.height = "24px";
    } else {
      document.getElementById("FormSphincterBowel2").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSphincterBowel2").style.color = "#333333";
      document.getElementById("FormSphincterBowel2").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferBCW_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferBCW_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferBCW_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferBCW_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferBCW_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferBCW_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferBCW_6").checked == true)
    {
      document.getElementById("FormTransferBCW").style.backgroundColor = "#77cf9c";
      document.getElementById("FormTransferBCW").style.color = "#333333";
      document.getElementById("FormTransferBCW").style.height = "24px";
    } else {
      document.getElementById("FormTransferBCW").style.backgroundColor = "#d46e6e";
      document.getElementById("FormTransferBCW").style.color = "#333333";
      document.getElementById("FormTransferBCW").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferToilet_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferToilet_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferToilet_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferToilet_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferToilet_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferToilet_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferToilet_6").checked == true)
    {
      document.getElementById("FormTransferToilet").style.backgroundColor = "#77cf9c";
      document.getElementById("FormTransferToilet").style.color = "#333333";
      document.getElementById("FormTransferToilet").style.height = "24px";
    } else {
      document.getElementById("FormTransferToilet").style.backgroundColor = "#d46e6e";
      document.getElementById("FormTransferToilet").style.color = "#333333";
      document.getElementById("FormTransferToilet").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferTS_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferTS_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferTS_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferTS_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferTS_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferTS_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferTS_6").checked == true)
    {
      document.getElementById("FormTransferTS").style.backgroundColor = "#77cf9c";
      document.getElementById("FormTransferTS").style.color = "#333333";
      document.getElementById("FormTransferTS").style.height = "24px";
    } else {
      document.getElementById("FormTransferTS").style.backgroundColor = "#d46e6e";
      document.getElementById("FormTransferTS").style.color = "#333333";
      document.getElementById("FormTransferTS").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferCarTransfer_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferCarTransfer_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferCarTransfer_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferCarTransfer_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferCarTransfer_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferCarTransfer_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferCarTransfer_6").checked == true)
    {
      document.getElementById("FormTransferCarTransfer").style.backgroundColor = "#77cf9c";
      document.getElementById("FormTransferCarTransfer").style.color = "#333333";
      document.getElementById("FormTransferCarTransfer").style.height = "24px";
    } else {
      document.getElementById("FormTransferCarTransfer").style.backgroundColor = "#d46e6e";
      document.getElementById("FormTransferCarTransfer").style.color = "#333333";
      document.getElementById("FormTransferCarTransfer").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionWW_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionWW_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionWW_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionWW_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionWW_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionWW_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionWW_6").checked == true)
    {
      document.getElementById("FormLocomotionWW").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLocomotionWW").style.color = "#333333";
      document.getElementById("FormLocomotionWW").style.height = "24px";
    } else {
      document.getElementById("FormLocomotionWW").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLocomotionWW").style.color = "#333333";
      document.getElementById("FormLocomotionWW").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionStairs_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionStairs_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionStairs_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionStairs_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionStairs_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionStairs_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionStairs_6").checked == true)
    {
      document.getElementById("FormLocomotionStairs").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLocomotionStairs").style.color = "#333333";
      document.getElementById("FormLocomotionStairs").style.height = "24px";
    } else {
      document.getElementById("FormLocomotionStairs").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLocomotionStairs").style.color = "#333333";
      document.getElementById("FormLocomotionStairs").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionCommunityAccess_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionCommunityAccess_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionCommunityAccess_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionCommunityAccess_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionCommunityAccess_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionCommunityAccess_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionCommunityAccess_6").checked == true)
    {
      document.getElementById("FormLocomotionCommunityAccess").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLocomotionCommunityAccess").style.color = "#333333";
      document.getElementById("FormLocomotionCommunityAccess").style.height = "24px";
    } else {
      document.getElementById("FormLocomotionCommunityAccess").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLocomotionCommunityAccess").style.color = "#333333";
      document.getElementById("FormLocomotionCommunityAccess").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationAV_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationAV_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationAV_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationAV_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationAV_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationAV_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationAV_6").checked == true)
    {
      document.getElementById("FormCommunicationAV").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCommunicationAV").style.color = "#333333";
      document.getElementById("FormCommunicationAV").style.height = "24px";
    } else {
      document.getElementById("FormCommunicationAV").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCommunicationAV").style.color = "#333333";
      document.getElementById("FormCommunicationAV").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationVN_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationVN_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationVN_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationVN_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationVN_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationVN_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationVN_6").checked == true)
    {
      document.getElementById("FormCommunicationVN").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCommunicationVN").style.color = "#333333";
      document.getElementById("FormCommunicationVN").style.height = "24px";
    } else {
      document.getElementById("FormCommunicationVN").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCommunicationVN").style.color = "#333333";
      document.getElementById("FormCommunicationVN").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationReading_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationReading_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationReading_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationReading_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationReading_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationReading_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationReading_6").checked == true)
    {
      document.getElementById("FormCommunicationReading").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCommunicationReading").style.color = "#333333";
      document.getElementById("FormCommunicationReading").style.height = "24px";
    } else {
      document.getElementById("FormCommunicationReading").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCommunicationReading").style.color = "#333333";
      document.getElementById("FormCommunicationReading").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationWriting_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationWriting_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationWriting_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationWriting_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationWriting_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationWriting_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationWriting_6").checked == true)
    {
      document.getElementById("FormCommunicationWriting").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCommunicationWriting").style.color = "#333333";
      document.getElementById("FormCommunicationWriting").style.height = "24px";
    } else {
      document.getElementById("FormCommunicationWriting").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCommunicationWriting").style.color = "#333333";
      document.getElementById("FormCommunicationWriting").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationSpeech_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationSpeech_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationSpeech_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationSpeech_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationSpeech_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationSpeech_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationSpeech_6").checked == true)
    {
      document.getElementById("FormCommunicationSpeech").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCommunicationSpeech").style.color = "#333333";
      document.getElementById("FormCommunicationSpeech").style.height = "24px";
    } else {
      document.getElementById("FormCommunicationSpeech").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCommunicationSpeech").style.color = "#333333";
      document.getElementById("FormCommunicationSpeech").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustSocialInteraction_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustSocialInteraction_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustSocialInteraction_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustSocialInteraction_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustSocialInteraction_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustSocialInteraction_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustSocialInteraction_6").checked == true)
    {
      document.getElementById("FormPSAdjustSocialInteraction").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPSAdjustSocialInteraction").style.color = "#333333";
      document.getElementById("FormPSAdjustSocialInteraction").style.height = "24px";
    } else {
      document.getElementById("FormPSAdjustSocialInteraction").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPSAdjustSocialInteraction").style.color = "#333333";
      document.getElementById("FormPSAdjustSocialInteraction").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmotionalStatus_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmotionalStatus_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmotionalStatus_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmotionalStatus_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmotionalStatus_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmotionalStatus_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmotionalStatus_6").checked == true)
    {
      document.getElementById("FormPSAdjustEmotionalStatus").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPSAdjustEmotionalStatus").style.color = "#333333";
      document.getElementById("FormPSAdjustEmotionalStatus").style.height = "24px";
    } else {
      document.getElementById("FormPSAdjustEmotionalStatus").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPSAdjustEmotionalStatus").style.color = "#333333";
      document.getElementById("FormPSAdjustEmotionalStatus").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustAdjustment_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustAdjustment_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustAdjustment_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustAdjustment_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustAdjustment_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustAdjustment_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustAdjustment_6").checked == true)
    {
      document.getElementById("FormPSAdjustAdjustment").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPSAdjustAdjustment").style.color = "#333333";
      document.getElementById("FormPSAdjustAdjustment").style.height = "24px";
    } else {
      document.getElementById("FormPSAdjustAdjustment").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPSAdjustAdjustment").style.color = "#333333";
      document.getElementById("FormPSAdjustAdjustment").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmployability_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmployability_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmployability_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmployability_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmployability_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmployability_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmployability_6").checked == true)
    {
      document.getElementById("FormPSAdjustEmployability").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPSAdjustEmployability").style.color = "#333333";
      document.getElementById("FormPSAdjustEmployability").style.height = "24px";
    } else {
      document.getElementById("FormPSAdjustEmployability").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPSAdjustEmployability").style.color = "#333333";
      document.getElementById("FormPSAdjustEmployability").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionProblemSolving_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionProblemSolving_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionProblemSolving_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionProblemSolving_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionProblemSolving_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionProblemSolving_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionProblemSolving_6").checked == true)
    {
      document.getElementById("FormCognitiveFunctionProblemSolving").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCognitiveFunctionProblemSolving").style.color = "#333333";
      document.getElementById("FormCognitiveFunctionProblemSolving").style.height = "24px";
    } else {
      document.getElementById("FormCognitiveFunctionProblemSolving").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCognitiveFunctionProblemSolving").style.color = "#333333";
      document.getElementById("FormCognitiveFunctionProblemSolving").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionMemory_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionMemory_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionMemory_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionMemory_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionMemory_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionMemory_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionMemory_6").checked == true)
    {
      document.getElementById("FormCognitiveFunctionMemory").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCognitiveFunctionMemory").style.color = "#333333";
      document.getElementById("FormCognitiveFunctionMemory").style.height = "24px";
    } else {
      document.getElementById("FormCognitiveFunctionMemory").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCognitiveFunctionMemory").style.color = "#333333";
      document.getElementById("FormCognitiveFunctionMemory").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionOrientation_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionOrientation_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionOrientation_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionOrientation_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionOrientation_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionOrientation_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionOrientation_6").checked == true)
    {
      document.getElementById("FormCognitiveFunctionOrientation").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCognitiveFunctionOrientation").style.color = "#333333";
      document.getElementById("FormCognitiveFunctionOrientation").style.height = "24px";
    } else {
      document.getElementById("FormCognitiveFunctionOrientation").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCognitiveFunctionOrientation").style.color = "#333333";
      document.getElementById("FormCognitiveFunctionOrientation").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionAttention_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionAttention_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionAttention_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionAttention_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionAttention_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionAttention_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionAttention_6").checked == true)
    {
      document.getElementById("FormCognitiveFunctionAttention").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCognitiveFunctionAttention").style.color = "#333333";
      document.getElementById("FormCognitiveFunctionAttention").style.height = "24px";
    } else {
      document.getElementById("FormCognitiveFunctionAttention").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCognitiveFunctionAttention").style.color = "#333333";
      document.getElementById("FormCognitiveFunctionAttention").style.height = "24px";
    }

    if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionSafetyJudgement_0").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionSafetyJudgement_1").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionSafetyJudgement_2").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionSafetyJudgement_3").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionSafetyJudgement_4").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionSafetyJudgement_5").checked == true || document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionSafetyJudgement_6").checked == true)
    {
      document.getElementById("FormCognitiveFunctionSafetyJudgement").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCognitiveFunctionSafetyJudgement").style.color = "#333333";
      document.getElementById("FormCognitiveFunctionSafetyJudgement").style.height = "24px";
    } else {
      document.getElementById("FormCognitiveFunctionSafetyJudgement").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCognitiveFunctionSafetyJudgement").style.color = "#333333";
      document.getElementById("FormCognitiveFunctionSafetyJudgement").style.height = "24px";
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_Form------------------------------------------------------------------------------------------------------------------------------
function Calculation_Form() {
  var FormMode;
  if (document.getElementById("FormView_FIMFAM_Form_TextBox_InsertObservationDate"))
  {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_FIMFAM_Form_TextBox_EditObservationDate"))
  {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    var Calculation_MotorSubScore = 0;
    var Calculation_CognitiveSubScore = 0;
    var Calculation_Total = 0;
    var a;

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareEating_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareEating_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareEating_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareGrooming_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareGrooming_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareGrooming_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareBathing_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareBathing_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareBathing_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingUpper_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingUpper_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingUpper_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingLower_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingLower_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareDressingLower_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareToileting_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareToileting_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareToileting_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareSwallowing_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareSwallowing_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SelfcareSwallowing_" + a).value);
      }
    }

    var Bladder1 = 0;
    var Bladder2 = 0;

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder1_" + a).checked == true)
      {
        Bladder1 = parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder1_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder2_" + a).checked == true)
      {
        Bladder2 = parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBladder2_" + a).value);
      }
    }

    if (Bladder1 <= Bladder2) {
      Calculation_MotorSubScore = Calculation_MotorSubScore + Bladder1;
      Calculation_Total = Calculation_Total + Bladder1;
    } else {
      Calculation_MotorSubScore = Calculation_MotorSubScore + Bladder2;
      Calculation_Total = Calculation_Total + Bladder2;
    }

    var Bowel1 = 0;
    var Bowel2 = 0;

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel1_" + a).checked == true)
      {
        Bowel1 = parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel1_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel2_" + a).checked == true)
      {
        Bowel2 = parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "SphincterBowel2_" + a).value);
      }
    }

    if (Bowel1 <= Bowel2) {
      Calculation_MotorSubScore = Calculation_MotorSubScore + Bowel1;
      Calculation_Total = Calculation_Total + Bowel1;
    } else {
      Calculation_MotorSubScore = Calculation_MotorSubScore + Bowel2;
      Calculation_Total = Calculation_Total + Bowel2;
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferBCW_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferBCW_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferBCW_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferToilet_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferToilet_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferToilet_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferTS_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferTS_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferTS_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferCarTransfer_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferCarTransfer_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "TransferCarTransfer_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionWW_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionWW_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionWW_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionStairs_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionStairs_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionStairs_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionCommunityAccess_" + a).checked == true)
      {
        Calculation_MotorSubScore = Calculation_MotorSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionCommunityAccess_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "LocomotionCommunityAccess_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationAV_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationAV_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationAV_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationVN_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationVN_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationVN_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationReading_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationReading_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationReading_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationWriting_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationWriting_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationWriting_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationSpeech_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationSpeech_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CommunicationSpeech_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustSocialInteraction_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustSocialInteraction_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustSocialInteraction_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmotionalStatus_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmotionalStatus_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmotionalStatus_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustAdjustment_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustAdjustment_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustAdjustment_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmployability_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmployability_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "PSAdjustEmployability_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionProblemSolving_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionProblemSolving_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionProblemSolving_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionMemory_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionMemory_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionMemory_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionOrientation_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionOrientation_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionOrientation_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionAttention_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionAttention_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionAttention_" + a).value);
      }
    }

    for (a = 0; a < 7; a++) {
      if (document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionSafetyJudgement_" + a).checked == true)
      {
        Calculation_CognitiveSubScore = Calculation_CognitiveSubScore + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionSafetyJudgement_" + a).value);
        Calculation_Total = Calculation_Total + parseInt(document.getElementById("FormView_FIMFAM_Form_RadioButtonList_" + FormMode + "CognitiveFunctionSafetyJudgement_" + a).value);
      }
    }

    document.getElementById("FormView_FIMFAM_Form_Textbox_" + FormMode + "MotorSubScore").value = Calculation_MotorSubScore;
    document.getElementById("FormView_FIMFAM_Form_Textbox_" + FormMode + "CognitiveSubScore").value = Calculation_CognitiveSubScore;
    document.getElementById("FormView_FIMFAM_Form_Textbox_" + FormMode + "Total").value = Calculation_Total;

  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------