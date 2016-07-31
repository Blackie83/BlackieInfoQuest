
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --FormEmail-------------------------------------------------------------------------------------------------------------------------------------
function FormEmail(EmailLink)
{
  var width = 750;
  var height = 700;
  var left = (screen.width - width) / 2;
  var top = (screen.height - height) / 2;
  window.open(EmailLink, 'Email', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=No , location=No , scrollbars=Yes , resizable=No , status=Yes , left=' + left + ' , top=' + top + ' ');
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --FormPrint-------------------------------------------------------------------------------------------------------------------------------------
function FormPrint(PrintLink)
{
  var width = 750;
  var height = 700;
  var left = (screen.width - width) / 2;
  var top = (screen.height - height) / 2;
  window.open(PrintLink, 'Email', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=No , location=No , scrollbars=Yes , resizable=No , status=Yes , left=' + left + ' , top=' + top + ' ');
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --QueryStringValue------------------------------------------------------------------------------------------------------------------------------
function QueryStringValue(FindName)
{
  var QueryString = window.location.search.substring(1);
  var QueryStringSplit = QueryString.split("&");
  for (var a = 0; a < QueryStringSplit.length; a++)
  {
    var QueryStringValue = QueryStringSplit[a].split("=");
    if (QueryStringValue[0] == FindName)
    {
      return QueryStringValue[1];
    }
  }
  return null;
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Search-----------------------------------------------------------------------------------------------------------------------------
function Validation_Search()
{
  if (document.getElementById("DropDownList_Facility").value == "")
  {
    document.getElementById("SearchFacility").style.backgroundColor = "#d46e6e";
    document.getElementById("SearchFacility").style.color = "#333333";
  }
  else
  {
    document.getElementById("SearchFacility").style.backgroundColor = "#77cf9c";
    document.getElementById("SearchFacility").style.color = "#333333";
  }

  if (document.getElementById("TextBox_PatientVisitNumber").value == "")
  {
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
function Validation_Form(Control)
{
  var FormMode;
  if (document.getElementById("FormView_VTE_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_VTE_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "Date").value == "" || document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "Date").value == "yyyy/mm/dd")
    {
      document.getElementById("FormDate").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDate").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDate").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDate").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "By").value == "")
    {
      document.getElementById("FormBy").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBy").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBy").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBy").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_DropDownList_" + FormMode + "Unit").value == "")
    {
      document.getElementById("FormUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUnit").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUnit").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "Weight").value == "")
    {
      document.getElementById("FormWeight").style.backgroundColor = "#d46e6e";
      document.getElementById("FormWeight").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormWeight").style.backgroundColor = "#77cf9c";
      document.getElementById("FormWeight").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "Height").value == "")
    {
      document.getElementById("FormHeight").style.backgroundColor = "#d46e6e";
      document.getElementById("FormHeight").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormHeight").style.backgroundColor = "#77cf9c";
      document.getElementById("FormHeight").style.color = "#333333";
    }


    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalStroke_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalStroke_1").checked == true)
    {
      document.getElementById("FormBRFMedicalStroke").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFMedicalStroke").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFMedicalStroke").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFMedicalStroke").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalHeartAttack_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalHeartAttack_1").checked == true)
    {
      document.getElementById("FormBRFMedicalHeartAttack").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFMedicalHeartAttack").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFMedicalHeartAttack").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFMedicalHeartAttack").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalHeartFailure_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalHeartFailure_1").checked == true)
    {
      document.getElementById("FormBRFMedicalHeartFailure").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFMedicalHeartFailure").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFMedicalHeartFailure").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFMedicalHeartFailure").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalInfection_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalInfection_1").checked == true)
    {
      document.getElementById("FormBRFMedicalInfection").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFMedicalInfection").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFMedicalInfection").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFMedicalInfection").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalThrombolytic_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalThrombolytic_1").checked == true)
    {
      document.getElementById("FormBRFMedicalThrombolytic").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFMedicalThrombolytic").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFMedicalThrombolytic").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFMedicalThrombolytic").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalCVA_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalCVA_1").checked == true)
    {
      document.getElementById("FormBRFMedicalCVA").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFMedicalCVA").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFMedicalCVA").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFMedicalCVA").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryOfPelvis_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryOfPelvis_1").checked == true)
    {
      document.getElementById("FormBRFSurgicalSurgeryOfPelvis").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFSurgicalSurgeryOfPelvis").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFSurgicalSurgeryOfPelvis").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFSurgicalSurgeryOfPelvis").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalFractureOfPelvis_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalFractureOfPelvis_1").checked == true)
    {
      document.getElementById("FormBRFSurgicalFractureOfPelvis").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFSurgicalFractureOfPelvis").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFSurgicalFractureOfPelvis").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFSurgicalFractureOfPelvis").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalMultipleTrauma_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalMultipleTrauma_1").checked == true)
    {
      document.getElementById("FormBRFSurgicalMultipleTrauma").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFSurgicalMultipleTrauma").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFSurgicalMultipleTrauma").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFSurgicalMultipleTrauma").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSpinalCordInjury_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSpinalCordInjury_1").checked == true)
    {
      document.getElementById("FormBRFSurgicalSpinalCordInjury").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFSurgicalSpinalCordInjury").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFSurgicalSpinalCordInjury").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFSurgicalSpinalCordInjury").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalPlaster_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalPlaster_1").checked == true)
    {
      document.getElementById("FormBRFSurgicalPlaster").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFSurgicalPlaster").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFSurgicalPlaster").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFSurgicalPlaster").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryAbove45Min_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryAbove45Min_1").checked == true)
    {
      document.getElementById("FormBRFSurgicalSurgeryAbove45Min").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFSurgicalSurgeryAbove45Min").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFSurgicalSurgeryAbove45Min").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFSurgicalSurgeryAbove45Min").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryBelow45Min_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryBelow45Min_1").checked == true)
    {
      document.getElementById("FormBRFSurgicalSurgeryBelow45Min").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFSurgicalSurgeryBelow45Min").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFSurgicalSurgeryBelow45Min").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFSurgicalSurgeryBelow45Min").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFBothPatientInBed_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFBothPatientInBed_1").checked == true)
    {
      document.getElementById("FormBRFBothPatientInBed").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBRFBothPatientInBed").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBRFBothPatientInBed").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBRFBothPatientInBed").style.color = "#333333";
    }


    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesHistory_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesHistory_1").checked == true)
    {
      document.getElementById("FormPRFMorbiditiesHistory").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPRFMorbiditiesHistory").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormPRFMorbiditiesHistory").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPRFMorbiditiesHistory").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesCancer_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesCancer_1").checked == true)
    {
      document.getElementById("FormPRFMorbiditiesCancer").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPRFMorbiditiesCancer").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormPRFMorbiditiesCancer").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPRFMorbiditiesCancer").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesVaricoseVeins_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesVaricoseVeins_1").checked == true)
    {
      document.getElementById("FormPRFMorbiditiesVaricoseVeins").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPRFMorbiditiesVaricoseVeins").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormPRFMorbiditiesVaricoseVeins").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPRFMorbiditiesVaricoseVeins").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesInflammatoryBowel_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesInflammatoryBowel_1").checked == true)
    {
      document.getElementById("FormPRFMorbiditiesInflammatoryBowel").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPRFMorbiditiesInflammatoryBowel").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormPRFMorbiditiesInflammatoryBowel").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPRFMorbiditiesInflammatoryBowel").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderOralContraceptive_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderOralContraceptive_1").checked == true)
    {
      document.getElementById("FormPRFGenderOralContraceptive").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPRFGenderOralContraceptive").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormPRFGenderOralContraceptive").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPRFGenderOralContraceptive").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderHormoneReplacementTherapy_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderHormoneReplacementTherapy_1").checked == true)
    {
      document.getElementById("FormPRFGenderHormoneReplacementTherapy").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPRFGenderHormoneReplacementTherapy").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormPRFGenderHormoneReplacementTherapy").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPRFGenderHormoneReplacementTherapy").style.color = "#333333";
    }

    if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderPregnancy_0").checked == true || document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderPregnancy_1").checked == true)
    {
      document.getElementById("FormPRFGenderPregnancy").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPRFGenderPregnancy").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormPRFGenderPregnancy").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPRFGenderPregnancy").style.color = "#333333";
    }


    Calculation_BRF(FormMode);
    Calculation_PRF(FormMode);
    Calculation_RFS(FormMode);

    var RFSScore = parseInt(document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "RFSScore").value);

    if (RFSScore > 2)
    {
      if (document.getElementById("FormView_VTE_Form_DropDownList_" + FormMode + "DoctorDoctorNotified").value == "")
      {
        document.getElementById("FormDoctorDoctorNotified").style.backgroundColor = "#d46e6e";
        document.getElementById("FormDoctorDoctorNotified").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormDoctorDoctorNotified").style.backgroundColor = "#77cf9c";
        document.getElementById("FormDoctorDoctorNotified").style.color = "#333333";
      }

      if (document.getElementById("FormView_VTE_Form_DropDownList_" + FormMode + "DoctorDoctorNotified").value == "No")
      {
        if (document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "DoctorReasonNotNotified").value == "")
        {
          document.getElementById("FormDoctorReasonNotNotified").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDoctorReasonNotNotified").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormDoctorReasonNotNotified").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDoctorReasonNotNotified").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormDoctorReasonNotNotified").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDoctorReasonNotNotified").style.color = "#000000";
      }


      if (document.getElementById("FormView_VTE_Form_DropDownList_" + FormMode + "DoctorTreatmentInitiated").value == "")
      {
        document.getElementById("FormDoctorTreatmentInitiated").style.backgroundColor = "#d46e6e";
        document.getElementById("FormDoctorTreatmentInitiated").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormDoctorTreatmentInitiated").style.backgroundColor = "#77cf9c";
        document.getElementById("FormDoctorTreatmentInitiated").style.color = "#333333";
      }

      if (document.getElementById("FormView_VTE_Form_DropDownList_" + FormMode + "DoctorTreatmentInitiated").value == "No")
      {
        if (document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "DoctorReasonNotInitiated").value == "")
        {
          document.getElementById("FormDoctorReasonNotInitiated").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDoctorReasonNotInitiated").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormDoctorReasonNotInitiated").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDoctorReasonNotInitiated").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormDoctorReasonNotInitiated").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDoctorReasonNotInitiated").style.color = "#000000";
      }
    }
    else
    {
      document.getElementById("FormDoctorDoctorNotified").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormDoctorDoctorNotified").style.color = "#000000";
      document.getElementById("FormDoctorReasonNotNotified").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormDoctorReasonNotNotified").style.color = "#000000";
      document.getElementById("FormDoctorTreatmentInitiated").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormDoctorTreatmentInitiated").style.color = "#000000";
      document.getElementById("FormDoctorReasonNotInitiated").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormDoctorReasonNotInitiated").style.color = "#000000";
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_Form------------------------------------------------------------------------------------------------------------------------------
function Calculation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_VTE_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_VTE_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    Calculation_Age(FormMode);
    Calculation_BMI(FormMode);
    Calculation_BRF(FormMode);
    Calculation_PRF(FormMode);
    Calculation_RFS(FormMode);
  }
}

function Calculation_Age(FormMode)
{
  if (document.getElementById("HiddenField_VIAge").value > 75)
  {
    document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "PRFAgeYears").value = "> 75";
    document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFAgeYears").value = "> 75 (3)";
  }
  else if (document.getElementById("HiddenField_VIAge").value >= 60 && document.getElementById("HiddenField_VIAge").value <= 75)
  {
    document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "PRFAgeYears").value = "60-75";
    document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFAgeYears").value = "60-75 (2)";
  }
  else if (document.getElementById("HiddenField_VIAge").value >= 41 && document.getElementById("HiddenField_VIAge").value <= 59)
  {
    document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "PRFAgeYears").value = "41-59";
    document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFAgeYears").value = "41-59 (1)";
  }
  else if (document.getElementById("HiddenField_VIAge").value < 41)
  {
    document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "PRFAgeYears").value = "< 41";
    document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFAgeYears").value = "< 41 (0)";
  }
}

function Calculation_BMI(FormMode)
{
  if (document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "Weight").value == "" || document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "Height").value == "")
  {
    document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "BMI").value = "";
    document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "PRFMorbiditiesObesity").value = "No";
    document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFMorbiditiesObesity").value = "No (0)";
  }
  else
  {
    var Weight = document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "Weight").value;
    var Height = document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "Height").value;
    var BMI = (Weight / (Height * Height));
    BMI = BMI.toFixed(2);
    document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "BMI").value = BMI;

    if (BMI >= 30)
    {
      document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "PRFMorbiditiesObesity").value = "Yes";
      document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFMorbiditiesObesity").value = "Yes (1)";
    }
    else
    {
      document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "PRFMorbiditiesObesity").value = "No";
      document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFMorbiditiesObesity").value = "No (0)";
    }
  }
}

function Calculation_BRF(FormMode)
{
  var BRFScore = 0;
  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalStroke_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalStroke_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalStroke_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalStroke_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalHeartAttack_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalHeartAttack_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalHeartAttack_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalHeartAttack_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalHeartFailure_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalHeartFailure_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalHeartFailure_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalHeartFailure_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalInfection_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalInfection_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalInfection_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalInfection_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalThrombolytic_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalThrombolytic_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalThrombolytic_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalThrombolytic_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalCVA_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalCVA_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalCVA_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFMedicalCVA_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryOfPelvis_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryOfPelvis_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryOfPelvis_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryOfPelvis_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalFractureOfPelvis_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalFractureOfPelvis_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalFractureOfPelvis_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalFractureOfPelvis_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalMultipleTrauma_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalMultipleTrauma_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalMultipleTrauma_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalMultipleTrauma_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSpinalCordInjury_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSpinalCordInjury_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSpinalCordInjury_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSpinalCordInjury_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalPlaster_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalPlaster_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalPlaster_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalPlaster_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryAbove45Min_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryAbove45Min_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryAbove45Min_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryAbove45Min_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryBelow45Min_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryBelow45Min_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryBelow45Min_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFSurgicalSurgeryBelow45Min_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFBothPatientInBed_0").checked == true)
  {
    BRFScore = parseInt(BRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFBothPatientInBed_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFBothPatientInBed_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "BRFBothPatientInBed_0").value.indexOf(")")));
  }

  document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "BRFScore").value = BRFScore;
}

function Calculation_PRF(FormMode)
{
  var PRFScore = 0;
  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesHistory_0").checked == true)
  {
    PRFScore = parseInt(PRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesHistory_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesHistory_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesHistory_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesCancer_0").checked == true)
  {
    PRFScore = parseInt(PRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesCancer_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesCancer_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesCancer_0").value.indexOf(")")));
  }

  PRFScore = parseInt(PRFScore) + parseInt(document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFMorbiditiesObesity").value.substring(document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFMorbiditiesObesity").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFMorbiditiesObesity").value.indexOf(")")));

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesVaricoseVeins_0").checked == true)
  {
    PRFScore = parseInt(PRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesVaricoseVeins_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesVaricoseVeins_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesVaricoseVeins_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesInflammatoryBowel_0").checked == true)
  {
    PRFScore = parseInt(PRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesInflammatoryBowel_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesInflammatoryBowel_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFMorbiditiesInflammatoryBowel_0").value.indexOf(")")));
  }

  PRFScore = parseInt(PRFScore) + parseInt(document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFAgeYears").value.substring(document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFAgeYears").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "PRFAgeYears").value.indexOf(")")));

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderOralContraceptive_0").checked == true)
  {
    PRFScore = parseInt(PRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderOralContraceptive_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderOralContraceptive_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderOralContraceptive_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderHormoneReplacementTherapy_0").checked == true)
  {
    PRFScore = parseInt(PRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderHormoneReplacementTherapy_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderHormoneReplacementTherapy_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderHormoneReplacementTherapy_0").value.indexOf(")")));
  }

  if (document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderPregnancy_0").checked == true)
  {
    PRFScore = parseInt(PRFScore) + parseInt(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderPregnancy_0").value.substring(document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderPregnancy_0").value.indexOf("(") + 1, document.getElementById("FormView_VTE_Form_RadioButtonList_" + FormMode + "PRFGenderPregnancy_0").value.indexOf(")")));
  }


  document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "PRFScore").value = PRFScore;
}

function Calculation_RFS(FormMode)
{
  var RFSScore = parseInt(document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "BRFScore").value) + parseInt(document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "PRFScore").value);
  document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "RFSScore").value = RFSScore;

  if (RFSScore == 0 || RFSScore == 1)
  {
    document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "RFSLevel").value = "Low";
  }
  else if (RFSScore == 2)
  {
    document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "RFSLevel").value = "Moderate";
  }
  else if (RFSScore == 3 || RFSScore == 4)
  {
    document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "RFSLevel").value = "High";
  }
  else if (RFSScore >= 5)
  {
    document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "RFSLevel").value = "Extremely High";
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form()
{
  var FormMode;
  if (document.getElementById("FormView_VTE_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_VTE_Form_HiddenField_Edit"))
  {
    FormMode = "Edit"
  }
  else if (document.getElementById("FormView_VTE_Form_HiddenField_Item"))
  {
    FormMode = "Item"
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (FormMode != "Item")
    {
      var RFSScore = parseInt(document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "RFSScore").value);

      if (RFSScore > 2)
      {
        Show("RiskFactorLevel1");
        Show("RiskFactorLevel2");
        Show("RiskFactorLevel3");
        Show("RiskFactorLevel4");
        Show("RiskFactorLevel5");
        Show("RiskFactorLevel6");
        Show("RiskFactorLevel7");
        Show("RiskFactorLevel8");
        Show("RiskFactorLevel9");
        Show("RiskFactorLevel10");

        if (document.getElementById("FormView_VTE_Form_DropDownList_" + FormMode + "DoctorDoctorNotified").value == "No")
        {
          Show("RiskFactorLevel11");
        }
        else
        {
          document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "DoctorReasonNotNotified").value = "";

          Hide("RiskFactorLevel11");
        }

        Show("RiskFactorLevel12");

        if (document.getElementById("FormView_VTE_Form_DropDownList_" + FormMode + "DoctorTreatmentInitiated").value == "No")
        {
          Show("RiskFactorLevel13");
        }
        else
        {
          document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "DoctorReasonNotInitiated").value = "";

          Hide("RiskFactorLevel13");
        }
      }
      else
      {
        document.getElementById("FormView_VTE_Form_CheckBox_" + FormMode + "RelativeSystolicHypertension").checked = false;
        document.getElementById("FormView_VTE_Form_CheckBox_" + FormMode + "RelativeAntiCoagulants").checked = false;
        document.getElementById("FormView_VTE_Form_CheckBox_" + FormMode + "RelativeHaemorrhagicStroke").checked = false;
        document.getElementById("FormView_VTE_Form_CheckBox_" + FormMode + "RelativeLowPlatelets").checked = false;
        document.getElementById("FormView_VTE_Form_CheckBox_" + FormMode + "RelativeActiveBleeding").checked = false;
        document.getElementById("FormView_VTE_Form_CheckBox_" + FormMode + "RelativeBleedingDisorder").checked = false;
        document.getElementById("FormView_VTE_Form_DropDownList_" + FormMode + "DoctorDoctorNotified").value = "";
        document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "DoctorReasonNotNotified").value = "";
        document.getElementById("FormView_VTE_Form_DropDownList_" + FormMode + "DoctorTreatmentInitiated").value = "";
        document.getElementById("FormView_VTE_Form_TextBox_" + FormMode + "DoctorReasonNotInitiated").value = "";

        Hide("RiskFactorLevel1");
        Hide("RiskFactorLevel2");
        Hide("RiskFactorLevel3");
        Hide("RiskFactorLevel4");
        Hide("RiskFactorLevel5");
        Hide("RiskFactorLevel6");
        Hide("RiskFactorLevel7");
        Hide("RiskFactorLevel8");
        Hide("RiskFactorLevel9");
        Hide("RiskFactorLevel10");
        Hide("RiskFactorLevel11");
        Hide("RiskFactorLevel12");
        Hide("RiskFactorLevel13");
      }
    }

    if (FormMode == "Item")
    {
      var RFSScore = parseInt(document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "RFSScore").value);

      if (RFSScore > 2)
      {
        Show("RiskFactorLevel1");
        Show("RiskFactorLevel2");
        Show("RiskFactorLevel3");
        Show("RiskFactorLevel4");
        Show("RiskFactorLevel5");
        Show("RiskFactorLevel6");
        Show("RiskFactorLevel7");
        Show("RiskFactorLevel8");
        Show("RiskFactorLevel9");
        Show("RiskFactorLevel10");

        if (document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "DoctorDoctorNotified").value == "No")
        {
          Show("RiskFactorLevel11");
        }
        else
        {
          Hide("RiskFactorLevel11");
        }

        Show("RiskFactorLevel12");

        if (document.getElementById("FormView_VTE_Form_HiddenField_" + FormMode + "DoctorTreatmentInitiated").value == "No")
        {
          Show("RiskFactorLevel13");
        }
        else
        {
          Hide("RiskFactorLevel13");
        }
      }
      else
      {
        Hide("RiskFactorLevel1");
        Hide("RiskFactorLevel2");
        Hide("RiskFactorLevel3");
        Hide("RiskFactorLevel4");
        Hide("RiskFactorLevel5");
        Hide("RiskFactorLevel6");
        Hide("RiskFactorLevel7");
        Hide("RiskFactorLevel8");
        Hide("RiskFactorLevel9");
        Hide("RiskFactorLevel10");
        Hide("RiskFactorLevel11");
        Hide("RiskFactorLevel12");
        Hide("RiskFactorLevel13");
      }
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Show------------------------------------------------------------------------------------------------------------------------------------------
function Show(id)
{
  if (document.getElementById)
  {
    document.getElementById(id).style.display = '';
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Hide------------------------------------------------------------------------------------------------------------------------------------------
function Hide(id)
{
  if (document.getElementById)
  {
    document.getElementById(id).style.display = 'none';
  }
}