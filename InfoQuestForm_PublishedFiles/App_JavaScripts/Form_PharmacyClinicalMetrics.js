
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

  if (document.getElementById("TextBox_Date").value == "" || document.getElementById("TextBox_Date").value == "yyyy/mm/dd")
  {
    document.getElementById("SearchDate").style.backgroundColor = "#d46e6e";
    document.getElementById("SearchDate").style.color = "#333333";
  }
  else
  {
    document.getElementById("SearchDate").style.backgroundColor = "#77cf9c";
    document.getElementById("SearchDate").style.color = "#333333";
  }

  if (document.getElementById("DropDownList_Intervention").value == "")
  {
    document.getElementById("SearchIntervention").style.backgroundColor = "#d46e6e";
    document.getElementById("SearchIntervention").style.color = "#333333";
  }
  else
  {
    document.getElementById("SearchIntervention").style.backgroundColor = "#77cf9c";
    document.getElementById("SearchIntervention").style.color = "#333333";
  }

  if (document.getElementById("DropDownList_Intervention").value == "6217")
  {
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
  else
  {
    document.getElementById("SearchPatientVisitNumber").style.backgroundColor = "#f7f7f7";
    document.getElementById("SearchPatientVisitNumber").style.color = "#000000";
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Search---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Search()
{
  if (document.getElementById("DropDownList_Intervention").value == "6217")
  {
    Show("PatientVisitNumber");
  }
  else
  {
    Hide("PatientVisitNumber");
    document.getElementById("TextBox_PatientVisitNumber").value = "";
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  if (document.getElementById("DropDownList_Intervention").value == "6217")
  {
    var FormMode = "";
    if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_Insert"))
    {
      FormMode = "Insert";
    }
    else if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_Edit"))
    {
      FormMode = "Edit";
    }

    if (FormMode != "")
    {
      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "InterventionBy").value == "")
      {
        document.getElementById("FormInterventionBy").style.backgroundColor = "#d46e6e";
        document.getElementById("FormInterventionBy").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormInterventionBy").style.backgroundColor = "#77cf9c";
        document.getElementById("FormInterventionBy").style.color = "#333333";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "Unit").value == "")
      {
        document.getElementById("FormUnit").style.backgroundColor = "#d46e6e";
        document.getElementById("FormUnit").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormUnit").style.backgroundColor = "#77cf9c";
        document.getElementById("FormUnit").style.color = "#333333";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "Doctor").value == "")
      {
        document.getElementById("FormDoctor").style.backgroundColor = "#d46e6e";
        document.getElementById("FormDoctor").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormDoctor").style.backgroundColor = "#77cf9c";
        document.getElementById("FormDoctor").style.color = "#333333";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "Doctor").value == "Other")
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "DoctorOther").value == "")
        {
          document.getElementById("FormDoctorOther").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDoctorOther").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormDoctorOther").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDoctorOther").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormDoctorOther").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDoctorOther").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "Time").value == "")
      {
        document.getElementById("FormTime").style.backgroundColor = "#d46e6e";
        document.getElementById("FormTime").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormTime").style.backgroundColor = "#77cf9c";
        document.getElementById("FormTime").style.color = "#333333";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "IndicationNoIndication").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "IndicationNoIndicationIRList").value == "")
        {
          document.getElementById("FormIndicationNoIndicationIRList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormIndicationNoIndicationIRList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormIndicationNoIndicationIRList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormIndicationNoIndicationIRList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "IndicationNoIndicationCSList").value == "")
        {
          document.getElementById("FormIndicationNoIndicationCSList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormIndicationNoIndicationCSList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormIndicationNoIndicationCSList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormIndicationNoIndicationCSList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "IndicationNoIndicationComment").value == "")
        {
          document.getElementById("FormIndicationNoIndicationComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormIndicationNoIndicationComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormIndicationNoIndicationComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormIndicationNoIndicationComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormIndicationNoIndicationIRList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormIndicationNoIndicationIRList").style.color = "#000000";
        document.getElementById("FormIndicationNoIndicationCSList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormIndicationNoIndicationCSList").style.color = "#000000";
        document.getElementById("FormIndicationNoIndicationComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormIndicationNoIndicationComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "IndicationDuplication").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "IndicationDuplicationIRList").value == "")
        {
          document.getElementById("FormIndicationDuplicationIRList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormIndicationDuplicationIRList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormIndicationDuplicationIRList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormIndicationDuplicationIRList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "IndicationDuplicationCSList").value == "")
        {
          document.getElementById("FormIndicationDuplicationCSList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormIndicationDuplicationCSList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormIndicationDuplicationCSList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormIndicationDuplicationCSList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "IndicationDuplicationComment").value == "")
        {
          document.getElementById("FormIndicationDuplicationComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormIndicationDuplicationComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormIndicationDuplicationComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormIndicationDuplicationComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormIndicationDuplicationIRList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormIndicationDuplicationIRList").style.color = "#000000";
        document.getElementById("FormIndicationDuplicationCSList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormIndicationDuplicationCSList").style.color = "#000000";
        document.getElementById("FormIndicationDuplicationComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormIndicationDuplicationComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "IndicationUntreated").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "IndicationUntreatedIRList").value == "")
        {
          document.getElementById("FormIndicationUntreatedIRList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormIndicationUntreatedIRList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormIndicationUntreatedIRList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormIndicationUntreatedIRList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "IndicationUntreatedCSList").value == "")
        {
          document.getElementById("FormIndicationUntreatedCSList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormIndicationUntreatedCSList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormIndicationUntreatedCSList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormIndicationUntreatedCSList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "IndicationUntreatedComment").value == "")
        {
          document.getElementById("FormIndicationUntreatedComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormIndicationUntreatedComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormIndicationUntreatedComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormIndicationUntreatedComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormIndicationUntreatedIRList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormIndicationUntreatedIRList").style.color = "#000000";
        document.getElementById("FormIndicationUntreatedCSList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormIndicationUntreatedCSList").style.color = "#000000";
        document.getElementById("FormIndicationUntreatedComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormIndicationUntreatedComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "DoseDose").checked == true)
      {
        var RadioButtonList_DoseDoseList = document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_RadioButtonList_" + FormMode + "DoseDoseList");
        var RadioButtonList_DoseDoseList_Count = RadioButtonList_DoseDoseList.getElementsByTagName("input");
        var DoseDoseList_Count = RadioButtonList_DoseDoseList_Count.length;
        var ControlComplete = false;
        for (var a = 0; a < DoseDoseList_Count; a++)
        {
          if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_RadioButtonList_" + FormMode + "DoseDoseList_" + a).checked == true || ControlComplete == true)
          {
            document.getElementById("FormDoseDoseList").style.backgroundColor = "#77cf9c";
            document.getElementById("FormDoseDoseList").style.color = "#333333";

            ControlComplete = true;
          }
          else
          {
            document.getElementById("FormDoseDoseList").style.backgroundColor = "#d46e6e";
            document.getElementById("FormDoseDoseList").style.color = "#333333";
          }
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "DoseDoseIRList").value == "")
        {
          document.getElementById("FormDoseDoseIRList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDoseDoseIRList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormDoseDoseIRList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDoseDoseIRList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "DoseDoseCSList").value == "")
        {
          document.getElementById("FormDoseDoseCSList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDoseDoseCSList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormDoseDoseCSList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDoseDoseCSList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "DoseDoseComment").value == "")
        {
          document.getElementById("FormDoseDoseComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDoseDoseComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormDoseDoseComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDoseDoseComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormDoseDoseList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDoseDoseList").style.color = "#000000";
        document.getElementById("FormDoseDoseIRList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDoseDoseIRList").style.color = "#000000";
        document.getElementById("FormDoseDoseCSList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDoseDoseCSList").style.color = "#000000";
        document.getElementById("FormDoseDoseComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDoseDoseComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "DoseInterval").checked == true)
      {
        var RadioButtonList_DoseIntervalList = document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_RadioButtonList_" + FormMode + "DoseIntervalList");
        var RadioButtonList_DoseIntervalList_Count = RadioButtonList_DoseIntervalList.getElementsByTagName("input");
        var DoseIntervalList_Count = RadioButtonList_DoseIntervalList_Count.length;
        var ControlComplete = false;
        for (var a = 0; a < DoseIntervalList_Count; a++)
        {
          if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_RadioButtonList_" + FormMode + "DoseIntervalList_" + a).checked == true || ControlComplete == true)
          {
            document.getElementById("FormDoseIntervalList").style.backgroundColor = "#77cf9c";
            document.getElementById("FormDoseIntervalList").style.color = "#333333";

            ControlComplete = true;
          }
          else
          {
            document.getElementById("FormDoseIntervalList").style.backgroundColor = "#d46e6e";
            document.getElementById("FormDoseIntervalList").style.color = "#333333";
          }
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "DoseIntervalIRList").value == "")
        {
          document.getElementById("FormDoseIntervalIRList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDoseIntervalIRList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormDoseIntervalIRList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDoseIntervalIRList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "DoseIntervalCSList").value == "")
        {
          document.getElementById("FormDoseIntervalCSList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDoseIntervalCSList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormDoseIntervalCSList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDoseIntervalCSList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "DoseIntervalComment").value == "")
        {
          document.getElementById("FormDoseIntervalComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDoseIntervalComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormDoseIntervalComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDoseIntervalComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormDoseIntervalList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDoseIntervalList").style.color = "#000000";
        document.getElementById("FormDoseIntervalIRList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDoseIntervalIRList").style.color = "#000000";
        document.getElementById("FormDoseIntervalCSList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDoseIntervalCSList").style.color = "#000000";
        document.getElementById("FormDoseIntervalComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDoseIntervalComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "EfficacyChange").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "EfficacyChangeIRList").value == "")
        {
          document.getElementById("FormEfficacyChangeIRList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormEfficacyChangeIRList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormEfficacyChangeIRList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormEfficacyChangeIRList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "EfficacyChangeCSList").value == "")
        {
          document.getElementById("FormEfficacyChangeCSList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormEfficacyChangeCSList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormEfficacyChangeCSList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormEfficacyChangeCSList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "EfficacyChangeComment").value == "")
        {
          document.getElementById("FormEfficacyChangeComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormEfficacyChangeComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormEfficacyChangeComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormEfficacyChangeComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormEfficacyChangeIRList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormEfficacyChangeIRList").style.color = "#000000";
        document.getElementById("FormEfficacyChangeCSList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormEfficacyChangeCSList").style.color = "#000000";
        document.getElementById("FormEfficacyChangeComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormEfficacyChangeComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "SafetyAllergic").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyAllergicIRList").value == "")
        {
          document.getElementById("FormSafetyAllergicIRList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyAllergicIRList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyAllergicIRList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyAllergicIRList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyAllergicCSList").value == "")
        {
          document.getElementById("FormSafetyAllergicCSList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyAllergicCSList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyAllergicCSList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyAllergicCSList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "SafetyAllergicComment").value == "")
        {
          document.getElementById("FormSafetyAllergicComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyAllergicComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyAllergicComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyAllergicComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormSafetyAllergicIRList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyAllergicIRList").style.color = "#000000";
        document.getElementById("FormSafetyAllergicCSList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyAllergicCSList").style.color = "#000000";
        document.getElementById("FormSafetyAllergicComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyAllergicComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "SafetyUnwanted").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyUnwantedIRList").value == "")
        {
          document.getElementById("FormSafetyUnwantedIRList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyUnwantedIRList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyUnwantedIRList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyUnwantedIRList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyUnwantedCSList").value == "")
        {
          document.getElementById("FormSafetyUnwantedCSList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyUnwantedCSList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyUnwantedCSList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyUnwantedCSList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "SafetyUnwantedComment").value == "")
        {
          document.getElementById("FormSafetyUnwantedComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyUnwantedComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyUnwantedComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyUnwantedComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormSafetyUnwantedIRList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyUnwantedIRList").style.color = "#000000";
        document.getElementById("FormSafetyUnwantedCSList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyUnwantedCSList").style.color = "#000000";
        document.getElementById("FormSafetyUnwantedComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyUnwantedComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "SafetyDrugDrug").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugDrugIRList").value == "")
        {
          document.getElementById("FormSafetyDrugDrugIRList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyDrugDrugIRList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyDrugDrugIRList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyDrugDrugIRList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugDrugCSList").value == "")
        {
          document.getElementById("FormSafetyDrugDrugCSList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyDrugDrugCSList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyDrugDrugCSList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyDrugDrugCSList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "SafetyDrugDrugComment").value == "")
        {
          document.getElementById("FormSafetyDrugDrugComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyDrugDrugComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyDrugDrugComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyDrugDrugComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormSafetyDrugDrugIRList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyDrugDrugIRList").style.color = "#000000";
        document.getElementById("FormSafetyDrugDrugCSList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyDrugDrugCSList").style.color = "#000000";
        document.getElementById("FormSafetyDrugDrugComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyDrugDrugComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "SafetyDrugDiluent").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugDiluentIRList").value == "")
        {
          document.getElementById("FormSafetyDrugDiluentIRList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyDrugDiluentIRList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyDrugDiluentIRList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyDrugDiluentIRList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugDiluentCSList").value == "")
        {
          document.getElementById("FormSafetyDrugDiluentCSList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyDrugDiluentCSList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyDrugDiluentCSList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyDrugDiluentCSList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "SafetyDrugDiluentComment").value == "")
        {
          document.getElementById("FormSafetyDrugDiluentComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyDrugDiluentComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyDrugDiluentComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyDrugDiluentComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormSafetyDrugDiluentIRList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyDrugDiluentIRList").style.color = "#000000";
        document.getElementById("FormSafetyDrugDiluentCSList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyDrugDiluentCSList").style.color = "#000000";
        document.getElementById("FormSafetyDrugDiluentComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyDrugDiluentComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "SafetyDrugLab").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugLabIRList").value == "")
        {
          document.getElementById("FormSafetyDrugLabIRList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyDrugLabIRList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyDrugLabIRList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyDrugLabIRList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugLabCSList").value == "")
        {
          document.getElementById("FormSafetyDrugLabCSList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyDrugLabCSList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyDrugLabCSList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyDrugLabCSList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "SafetyDrugLabComment").value == "")
        {
          document.getElementById("FormSafetyDrugLabComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyDrugLabComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyDrugLabComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyDrugLabComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormSafetyDrugLabIRList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyDrugLabIRList").style.color = "#000000";
        document.getElementById("FormSafetyDrugLabCSList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyDrugLabCSList").style.color = "#000000";
        document.getElementById("FormSafetyDrugLabComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyDrugLabComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "SafetyDrugDisease").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugDiseaseIRList").value == "")
        {
          document.getElementById("FormSafetyDrugDiseaseIRList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyDrugDiseaseIRList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyDrugDiseaseIRList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyDrugDiseaseIRList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugDiseaseCSList").value == "")
        {
          document.getElementById("FormSafetyDrugDiseaseCSList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyDrugDiseaseCSList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyDrugDiseaseCSList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyDrugDiseaseCSList").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "SafetyDrugDiseaseComment").value == "")
        {
          document.getElementById("FormSafetyDrugDiseaseComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSafetyDrugDiseaseComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSafetyDrugDiseaseComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSafetyDrugDiseaseComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormSafetyDrugDiseaseIRList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyDrugDiseaseIRList").style.color = "#000000";
        document.getElementById("FormSafetyDrugDiseaseCSList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyDrugDiseaseCSList").style.color = "#000000";
        document.getElementById("FormSafetyDrugDiseaseComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSafetyDrugDiseaseComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "MedicationErrorMissed").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "MedicationErrorMissedComment").value == "")
        {
          document.getElementById("FormMedicationErrorMissedComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormMedicationErrorMissedComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormMedicationErrorMissedComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormMedicationErrorMissedComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormMedicationErrorMissedComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormMedicationErrorMissedComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "MedicationErrorIncorrectDrug").checked == true) {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "MedicationErrorIncorrectDrugComment").value == "") {
          document.getElementById("FormMedicationErrorIncorrectDrugComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormMedicationErrorIncorrectDrugComment").style.color = "#333333";
        }
        else {
          document.getElementById("FormMedicationErrorIncorrectDrugComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormMedicationErrorIncorrectDrugComment").style.color = "#333333";
        }
      }
      else {
        document.getElementById("FormMedicationErrorIncorrectDrugComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormMedicationErrorIncorrectDrugComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "MedicationErrorIncorrect").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "MedicationErrorIncorrectComment").value == "")
        {
          document.getElementById("FormMedicationErrorIncorrectComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormMedicationErrorIncorrectComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormMedicationErrorIncorrectComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormMedicationErrorIncorrectComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormMedicationErrorIncorrectComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormMedicationErrorIncorrectComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "MedicationErrorPrescribed").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "MedicationErrorPrescribedComment").value == "")
        {
          document.getElementById("FormMedicationErrorPrescribedComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormMedicationErrorPrescribedComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormMedicationErrorPrescribedComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormMedicationErrorPrescribedComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormMedicationErrorPrescribedComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormMedicationErrorPrescribedComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "MedicationErrorAdministered").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "MedicationErrorAdministeredComment").value == "")
        {
          document.getElementById("FormMedicationErrorAdministeredComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormMedicationErrorAdministeredComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormMedicationErrorAdministeredComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormMedicationErrorAdministeredComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormMedicationErrorAdministeredComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormMedicationErrorAdministeredComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "MedicationErrorMedication").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "MedicationErrorMedicationComment").value == "")
        {
          document.getElementById("FormMedicationErrorMedicationComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormMedicationErrorMedicationComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormMedicationErrorMedicationComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormMedicationErrorMedicationComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormMedicationErrorMedicationComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormMedicationErrorMedicationComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "CostGeneric").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "CostGenericComment").value == "")
        {
          document.getElementById("FormCostGenericComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormCostGenericComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormCostGenericComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormCostGenericComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormCostGenericComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormCostGenericComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "CostSubstitution").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "CostSubstitutionComment").value == "")
        {
          document.getElementById("FormCostSubstitutionComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormCostSubstitutionComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormCostSubstitutionComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormCostSubstitutionComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormCostSubstitutionComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormCostSubstitutionComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "CostDecrease").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "CostDecreaseComment").value == "")
        {
          document.getElementById("FormCostDecreaseComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormCostDecreaseComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormCostDecreaseComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormCostDecreaseComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormCostDecreaseComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormCostDecreaseComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "CostIncrease").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "CostIncreaseComment").value == "")
        {
          document.getElementById("FormCostIncreaseComment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormCostIncreaseComment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormCostIncreaseComment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormCostIncreaseComment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormCostIncreaseComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormCostIncreaseComment").style.color = "#000000";
      }
    }
  }
  else if (document.getElementById("DropDownList_Intervention").value == "6218")
  {
    var FormMode = "";
    if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_Insert"))
    {
      FormMode = "Insert";
    }
    else if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_Edit"))
    {
      FormMode = "Edit";
    }

    if (FormMode != "")
    {
      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "InterventionBy").value == "")
      {
        document.getElementById("FormInterventionBy").style.backgroundColor = "#d46e6e";
        document.getElementById("FormInterventionBy").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormInterventionBy").style.backgroundColor = "#77cf9c";
        document.getElementById("FormInterventionBy").style.color = "#333333";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "PatientFile").checked == true || document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "PatientLabResults").checked == true || document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "PatientPrescription").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "PatientTotalTime").value == "")
        {
          document.getElementById("FormPatientTotalTime").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPatientTotalTime").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPatientTotalTime").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPatientTotalTime").style.color = "#333333";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "PatientAmount").value == "")
        {
          document.getElementById("FormPatientAmount").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPatientAmount").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPatientAmount").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPatientAmount").style.color = "#333333";
        }

        //if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "PatientComments").value == "")
        //{
        //  document.getElementById("FormPatientComments").style.backgroundColor = "#d46e6e";
        //  document.getElementById("FormPatientComments").style.color = "#333333";
        //}
        //else
        //{
        //  document.getElementById("FormPatientComments").style.backgroundColor = "#77cf9c";
        //  document.getElementById("FormPatientComments").style.color = "#333333";
        //}
      }
      else
      {
        document.getElementById("FormPatientTotalTime").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientTotalTime").style.color = "#000000";
        document.getElementById("FormPatientAmount").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientAmount").style.color = "#000000";
        //document.getElementById("FormPatientComments").style.backgroundColor = "#f7f7f7";
        //document.getElementById("FormPatientComments").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Medication").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "MedicationTime").value == "")
        {
          document.getElementById("FormMedicationTime").style.backgroundColor = "#d46e6e";
          document.getElementById("FormMedicationTime").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormMedicationTime").style.backgroundColor = "#77cf9c";
          document.getElementById("FormMedicationTime").style.color = "#333333";
        }

        //if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "MedicationComment").value == "")
        //{
        //  document.getElementById("FormMedicationComment").style.backgroundColor = "#d46e6e";
        //  document.getElementById("FormMedicationComment").style.color = "#333333";
        //}
        //else
        //{
        //  document.getElementById("FormMedicationComment").style.backgroundColor = "#77cf9c";
        //  document.getElementById("FormMedicationComment").style.color = "#333333";
        //}
      }
      else
      {
        document.getElementById("FormMedicationTime").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormMedicationTime").style.color = "#000000";
        //document.getElementById("FormMedicationComment").style.backgroundColor = "#f7f7f7";
        //document.getElementById("FormMedicationComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Research").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "ResearchTime").value == "")
        {
          document.getElementById("FormResearchTime").style.backgroundColor = "#d46e6e";
          document.getElementById("FormResearchTime").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormResearchTime").style.backgroundColor = "#77cf9c";
          document.getElementById("FormResearchTime").style.color = "#333333";
        }

        //if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "ResearchComment").value == "")
        //{
        //  document.getElementById("FormResearchComment").style.backgroundColor = "#d46e6e";
        //  document.getElementById("FormResearchComment").style.color = "#333333";
        //}
        //else
        //{
        //  document.getElementById("FormResearchComment").style.backgroundColor = "#77cf9c";
        //  document.getElementById("FormResearchComment").style.color = "#333333";
        //}
      }
      else
      {
        document.getElementById("FormResearchTime").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormResearchTime").style.color = "#000000";
        //document.getElementById("FormResearchComment").style.backgroundColor = "#f7f7f7";
        //document.getElementById("FormResearchComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Rounds").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "RoundsTime").value == "")
        {
          document.getElementById("FormRoundsTime").style.backgroundColor = "#d46e6e";
          document.getElementById("FormRoundsTime").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormRoundsTime").style.backgroundColor = "#77cf9c";
          document.getElementById("FormRoundsTime").style.color = "#333333";
        }

        //if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "RoundsComment").value == "")
        //{
        //  document.getElementById("FormRoundsComment").style.backgroundColor = "#d46e6e";
        //  document.getElementById("FormRoundsComment").style.color = "#333333";
        //}
        //else
        //{
        //  document.getElementById("FormRoundsComment").style.backgroundColor = "#77cf9c";
        //  document.getElementById("FormRoundsComment").style.color = "#333333";
        //}
      }
      else
      {
        document.getElementById("FormRoundsTime").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormRoundsTime").style.color = "#000000";
        //document.getElementById("FormRoundsComment").style.backgroundColor = "#f7f7f7";
        //document.getElementById("FormRoundsComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Counselling").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "CounsellingTime").value == "")
        {
          document.getElementById("FormCounsellingTime").style.backgroundColor = "#d46e6e";
          document.getElementById("FormCounsellingTime").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormCounsellingTime").style.backgroundColor = "#77cf9c";
          document.getElementById("FormCounsellingTime").style.color = "#333333";
        }

        //if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "CounsellingComment").value == "")
        //{
        //  document.getElementById("FormCounsellingComment").style.backgroundColor = "#d46e6e";
        //  document.getElementById("FormCounsellingComment").style.color = "#333333";
        //}
        //else
        //{
        //  document.getElementById("FormCounsellingComment").style.backgroundColor = "#77cf9c";
        //  document.getElementById("FormCounsellingComment").style.color = "#333333";
        //}
      }
      else
      {
        document.getElementById("FormCounsellingTime").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormCounsellingTime").style.color = "#000000";
        //document.getElementById("FormCounsellingComment").style.backgroundColor = "#f7f7f7";
        //document.getElementById("FormCounsellingComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Training").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "TrainingTime").value == "")
        {
          document.getElementById("FormTrainingTime").style.backgroundColor = "#d46e6e";
          document.getElementById("FormTrainingTime").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormTrainingTime").style.backgroundColor = "#77cf9c";
          document.getElementById("FormTrainingTime").style.color = "#333333";
        }

        //if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "TrainingComment").value == "")
        //{
        //  document.getElementById("FormTrainingComment").style.backgroundColor = "#d46e6e";
        //  document.getElementById("FormTrainingComment").style.color = "#333333";
        //}
        //else
        //{
        //  document.getElementById("FormTrainingComment").style.backgroundColor = "#77cf9c";
        //  document.getElementById("FormTrainingComment").style.color = "#333333";
        //}
      }
      else
      {
        document.getElementById("FormTrainingTime").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormTrainingTime").style.color = "#000000";
        //document.getElementById("FormTrainingComment").style.backgroundColor = "#f7f7f7";
        //document.getElementById("FormTrainingComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Reporting").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "ReportingTime").value == "")
        {
          document.getElementById("FormReportingTime").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportingTime").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportingTime").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportingTime").style.color = "#333333";
        }

        //if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "ReportingComment").value == "")
        //{
        //  document.getElementById("FormReportingComment").style.backgroundColor = "#d46e6e";
        //  document.getElementById("FormReportingComment").style.color = "#333333";
        //}
        //else
        //{
        //  document.getElementById("FormReportingComment").style.backgroundColor = "#77cf9c";
        //  document.getElementById("FormReportingComment").style.color = "#333333";
        //}
      }
      else
      {
        document.getElementById("FormReportingTime").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportingTime").style.color = "#000000";
        //document.getElementById("FormReportingComment").style.backgroundColor = "#f7f7f7";
        //document.getElementById("FormReportingComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Calculations").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "CalculationsTime").value == "")
        {
          document.getElementById("FormCalculationsTime").style.backgroundColor = "#d46e6e";
          document.getElementById("FormCalculationsTime").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormCalculationsTime").style.backgroundColor = "#77cf9c";
          document.getElementById("FormCalculationsTime").style.color = "#333333";
        }

        //if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "CalculationsComment").value == "")
        //{
        //  document.getElementById("FormCalculationsComment").style.backgroundColor = "#d46e6e";
        //  document.getElementById("FormCalculationsComment").style.color = "#333333";
        //}
        //else
        //{
        //  document.getElementById("FormCalculationsComment").style.backgroundColor = "#77cf9c";
        //  document.getElementById("FormCalculationsComment").style.color = "#333333";
        //}
      }
      else
      {
        document.getElementById("FormCalculationsTime").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormCalculationsTime").style.color = "#000000";
        //document.getElementById("FormCalculationsComment").style.backgroundColor = "#f7f7f7";
        //document.getElementById("FormCalculationsComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "AdviceDoctor").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "AdviceDoctorTime").value == "")
        {
          document.getElementById("FormAdviceDoctorTime").style.backgroundColor = "#d46e6e";
          document.getElementById("FormAdviceDoctorTime").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormAdviceDoctorTime").style.backgroundColor = "#77cf9c";
          document.getElementById("FormAdviceDoctorTime").style.color = "#333333";
        }

        //if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "AdviceDoctorComment").value == "")
        //{
        //  document.getElementById("FormAdviceDoctorComment").style.backgroundColor = "#d46e6e";
        //  document.getElementById("FormAdviceDoctorComment").style.color = "#333333";
        //}
        //else
        //{
        //  document.getElementById("FormAdviceDoctorComment").style.backgroundColor = "#77cf9c";
        //  document.getElementById("FormAdviceDoctorComment").style.color = "#333333";
        //}
      }
      else
      {
        document.getElementById("FormAdviceDoctorTime").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormAdviceDoctorTime").style.color = "#000000";
        //document.getElementById("FormAdviceDoctorComment").style.backgroundColor = "#f7f7f7";
        //document.getElementById("FormAdviceDoctorComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "AdviceNurse").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "AdviceNurseTime").value == "")
        {
          document.getElementById("FormAdviceNurseTime").style.backgroundColor = "#d46e6e";
          document.getElementById("FormAdviceNurseTime").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormAdviceNurseTime").style.backgroundColor = "#77cf9c";
          document.getElementById("FormAdviceNurseTime").style.color = "#333333";
        }

        //if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "AdviceNurseComment").value == "")
        //{
        //  document.getElementById("FormAdviceNurseComment").style.backgroundColor = "#d46e6e";
        //  document.getElementById("FormAdviceNurseComment").style.color = "#333333";
        //}
        //else
        //{
        //  document.getElementById("FormAdviceNurseComment").style.backgroundColor = "#77cf9c";
        //  document.getElementById("FormAdviceNurseComment").style.color = "#333333";
        //}
      }
      else
      {
        document.getElementById("FormAdviceNurseTime").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormAdviceNurseTime").style.color = "#000000";
        //document.getElementById("FormAdviceNurseComment").style.backgroundColor = "#f7f7f7";
        //document.getElementById("FormAdviceNurseComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "MedicalHistory").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "MedicalHistoryTime").value == "")
        {
          document.getElementById("FormMedicalHistoryTime").style.backgroundColor = "#d46e6e";
          document.getElementById("FormMedicalHistoryTime").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormMedicalHistoryTime").style.backgroundColor = "#77cf9c";
          document.getElementById("FormMedicalHistoryTime").style.color = "#333333";
        }

        //if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "MedicalHistoryComment").value == "")
        //{
        //  document.getElementById("FormMedicalHistoryComment").style.backgroundColor = "#d46e6e";
        //  document.getElementById("FormMedicalHistoryComment").style.color = "#333333";
        //}
        //else
        //{
        //  document.getElementById("FormMedicalHistoryComment").style.backgroundColor = "#77cf9c";
        //  document.getElementById("FormMedicalHistoryComment").style.color = "#333333";
        //}
      }
      else
      {
        document.getElementById("FormMedicalHistoryTime").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormMedicalHistoryTime").style.color = "#000000";
        //document.getElementById("FormMedicalHistoryComment").style.backgroundColor = "#f7f7f7";
        //document.getElementById("FormMedicalHistoryComment").style.color = "#000000";
      }

      if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Statistics").checked == true)
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "StatisticsTime").value == "")
        {
          document.getElementById("FormStatisticsTime").style.backgroundColor = "#d46e6e";
          document.getElementById("FormStatisticsTime").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormStatisticsTime").style.backgroundColor = "#77cf9c";
          document.getElementById("FormStatisticsTime").style.color = "#333333";
        }

        //if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "StatisticsComment").value == "")
        //{
        //  document.getElementById("FormStatisticsComment").style.backgroundColor = "#d46e6e";
        //  document.getElementById("FormStatisticsComment").style.color = "#333333";
        //}
        //else
        //{
        //  document.getElementById("FormStatisticsComment").style.backgroundColor = "#77cf9c";
        //  document.getElementById("FormStatisticsComment").style.color = "#333333";
        //}
      }
      else
      {
        document.getElementById("FormStatisticsTime").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormStatisticsTime").style.color = "#000000";
        //document.getElementById("FormStatisticsComment").style.backgroundColor = "#f7f7f7";
        //document.getElementById("FormStatisticsComment").style.color = "#000000";
      }
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form()
{
  if (document.getElementById("DropDownList_Intervention").value == "6217")
  {
    var FormMode = "";
    if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_Insert"))
    {
      FormMode = "Insert";
    }
    else if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_Edit"))
    {
      FormMode = "Edit"
    }
    else if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_Item"))
    {
      FormMode = "Item"
    }

    if (FormMode != "")
    {
      if (FormMode != "Item")
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "Doctor").value == "Other")
        {
          Show("DoctorOther");
        }
        else
        {
          Hide("DoctorOther");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "DoctorOther").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "IndicationNoIndication").checked == true)
        {
          Show("IndicationNoIndicationIRList");
          Show("IndicationNoIndicationCSList");
          Show("IndicationNoIndicationComment");
        }
        else
        {
          Hide("IndicationNoIndicationIRList");
          Hide("IndicationNoIndicationCSList");
          Hide("IndicationNoIndicationComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "IndicationNoIndicationIRList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "IndicationNoIndicationCSList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "IndicationNoIndicationComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "IndicationDuplication").checked == true)
        {
          Show("IndicationDuplicationIRList");
          Show("IndicationDuplicationCSList");
          Show("IndicationDuplicationComment");
        }
        else
        {
          Hide("IndicationDuplicationIRList");
          Hide("IndicationDuplicationCSList");
          Hide("IndicationDuplicationComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "IndicationDuplicationIRList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "IndicationDuplicationCSList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "IndicationDuplicationComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "IndicationUntreated").checked == true)
        {
          Show("IndicationUntreatedIRList");
          Show("IndicationUntreatedCSList");
          Show("IndicationUntreatedComment");
        }
        else
        {
          Hide("IndicationUntreatedIRList");
          Hide("IndicationUntreatedCSList");
          Hide("IndicationUntreatedComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "IndicationUntreatedIRList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "IndicationUntreatedCSList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "IndicationUntreatedComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "DoseDose").checked == true)
        {
          Show("DoseDoseList");
          Show("DoseDoseIRList");
          Show("DoseDoseCSList");
          Show("DoseDoseComment");
        }
        else
        {
          Hide("DoseDoseList");
          Hide("DoseDoseIRList");
          Hide("DoseDoseCSList");
          Hide("DoseDoseComment");

          var RadioButtonList_DoseDoseList = document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_RadioButtonList_" + FormMode + "DoseDoseList");
          var RadioButtonList_DoseDoseList_Count = RadioButtonList_DoseDoseList.getElementsByTagName("input");
          var DoseDoseList_Count = RadioButtonList_DoseDoseList_Count.length;
          for (var a = 0; a < DoseDoseList_Count; a++)
          {
            document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_RadioButtonList_" + FormMode + "DoseDoseList_" + a).checked = false;
          }

          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "DoseDoseIRList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "DoseDoseCSList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "DoseDoseComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "DoseInterval").checked == true)
        {
          Show("DoseIntervalList");
          Show("DoseIntervalIRList");
          Show("DoseIntervalCSList");
          Show("DoseIntervalComment");
        }
        else
        {
          Hide("DoseIntervalList");
          Hide("DoseIntervalIRList");
          Hide("DoseIntervalCSList");
          Hide("DoseIntervalComment");

          var RadioButtonList_DoseIntervalList = document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_RadioButtonList_" + FormMode + "DoseIntervalList");
          var RadioButtonList_DoseIntervalList_Count = RadioButtonList_DoseIntervalList.getElementsByTagName("input");
          var DoseIntervalList_Count = RadioButtonList_DoseIntervalList_Count.length;
          for (var a = 0; a < DoseIntervalList_Count; a++)
          {
            document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_RadioButtonList_" + FormMode + "DoseIntervalList_" + a).checked = false;
          }

          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "DoseIntervalIRList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "DoseIntervalCSList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "DoseIntervalComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "EfficacyChange").checked == true)
        {
          Show("EfficacyChangeIRList");
          Show("EfficacyChangeCSList");
          Show("EfficacyChangeComment");
        }
        else
        {
          Hide("EfficacyChangeIRList");
          Hide("EfficacyChangeCSList");
          Hide("EfficacyChangeComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "EfficacyChangeIRList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "EfficacyChangeCSList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "EfficacyChangeComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "SafetyAllergic").checked == true)
        {
          Show("SafetyAllergicURL");
          Show("SafetyAllergicIRList");
          Show("SafetyAllergicCSList");
          Show("SafetyAllergicComment");
        }
        else
        {
          Hide("SafetyAllergicURL");
          Hide("SafetyAllergicIRList");
          Hide("SafetyAllergicCSList");
          Hide("SafetyAllergicComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyAllergicIRList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyAllergicCSList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "SafetyAllergicComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "SafetyUnwanted").checked == true)
        {
          Show("SafetyUnwantedURL");
          Show("SafetyUnwantedIRList");
          Show("SafetyUnwantedCSList");
          Show("SafetyUnwantedComment");
        }
        else
        {
          Hide("SafetyUnwantedURL");
          Hide("SafetyUnwantedIRList");
          Hide("SafetyUnwantedCSList");
          Hide("SafetyUnwantedComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyUnwantedIRList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyUnwantedCSList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "SafetyUnwantedComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "SafetyDrugDrug").checked == true)
        {
          Show("SafetyDrugDrugIRList");
          Show("SafetyDrugDrugCSList");
          Show("SafetyDrugDrugComment");
        }
        else
        {
          Hide("SafetyDrugDrugIRList");
          Hide("SafetyDrugDrugCSList");
          Hide("SafetyDrugDrugComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugDrugIRList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugDrugCSList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "SafetyDrugDrugComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "SafetyDrugDiluent").checked == true)
        {
          Show("SafetyDrugDiluentIRList");
          Show("SafetyDrugDiluentCSList");
          Show("SafetyDrugDiluentComment");
        }
        else
        {
          Hide("SafetyDrugDiluentIRList");
          Hide("SafetyDrugDiluentCSList");
          Hide("SafetyDrugDiluentComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugDiluentIRList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugDiluentCSList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "SafetyDrugDiluentComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "SafetyDrugLab").checked == true)
        {
          Show("SafetyDrugLabIRList");
          Show("SafetyDrugLabCSList");
          Show("SafetyDrugLabComment");
        }
        else
        {
          Hide("SafetyDrugLabIRList");
          Hide("SafetyDrugLabCSList");
          Hide("SafetyDrugLabComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugLabIRList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugLabCSList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "SafetyDrugLabComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "SafetyDrugDisease").checked == true)
        {
          Show("SafetyDrugDiseaseIRList");
          Show("SafetyDrugDiseaseCSList");
          Show("SafetyDrugDiseaseComment");
        }
        else
        {
          Hide("SafetyDrugDiseaseIRList");
          Hide("SafetyDrugDiseaseCSList");
          Hide("SafetyDrugDiseaseComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugDiseaseIRList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DropDownList_" + FormMode + "SafetyDrugDiseaseCSList").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "SafetyDrugDiseaseComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "MedicationErrorMissed").checked == true)
        {
          Show("MedicationErrorMissedComment");
        }
        else
        {
          Hide("MedicationErrorMissedComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "MedicationErrorMissedComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "MedicationErrorIncorrectDrug").checked == true) {
          Show("MedicationErrorIncorrectDrugComment");
        }
        else {
          Hide("MedicationErrorIncorrectDrugComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "MedicationErrorIncorrectDrugComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "MedicationErrorIncorrect").checked == true)
        {
          Show("MedicationErrorIncorrectComment");
        }
        else
        {
          Hide("MedicationErrorIncorrectComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "MedicationErrorIncorrectComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "MedicationErrorPrescribed").checked == true)
        {
          Show("MedicationErrorPrescribedComment");
        }
        else
        {
          Hide("MedicationErrorPrescribedComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "MedicationErrorPrescribedComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "MedicationErrorAdministered").checked == true)
        {
          Show("MedicationErrorAdministeredComment");
        }
        else
        {
          Hide("MedicationErrorAdministeredComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "MedicationErrorAdministeredComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "MedicationErrorMedication").checked == true)
        {
          Show("MedicationErrorMedicationComment");
        }
        else
        {
          Hide("MedicationErrorMedicationComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "MedicationErrorMedicationComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "CostGeneric").checked == true)
        {
          Show("CostGenericComment");
        }
        else
        {
          Hide("CostGenericComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "CostGenericComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "CostSubstitution").checked == true)
        {
          Show("CostSubstitutionComment");
        }
        else
        {
          Hide("CostSubstitutionComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "CostSubstitutionComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "CostDecrease").checked == true)
        {
          Show("CostDecreaseComment");
        }
        else
        {
          Hide("CostDecreaseComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "CostDecreaseComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_CheckBox_" + FormMode + "CostIncrease").checked == true)
        {
          Show("CostIncreaseComment");
        }
        else
        {
          Hide("CostIncreaseComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_TextBox_" + FormMode + "CostIncreaseComment").value = "";
        }
      }

      if (FormMode == "Item")
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "Doctor").value == "Other")
        {
          Show("DoctorOther");
        }
        else
        {
          Hide("DoctorOther");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "IndicationNoIndication").value == "True")
        {
          Show("IndicationNoIndicationIRList");
          Show("IndicationNoIndicationCSList");
          Show("IndicationNoIndicationComment");
        }
        else
        {
          Hide("IndicationNoIndicationIRList");
          Hide("IndicationNoIndicationCSList");
          Hide("IndicationNoIndicationComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "IndicationDuplication").value == "True")
        {
          Show("IndicationDuplicationIRList");
          Show("IndicationDuplicationCSList");
          Show("IndicationDuplicationComment");
        }
        else
        {
          Hide("IndicationDuplicationIRList");
          Hide("IndicationDuplicationCSList");
          Hide("IndicationDuplicationComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "IndicationUntreated").value == "True")
        {
          Show("IndicationUntreatedIRList");
          Show("IndicationUntreatedCSList");
          Show("IndicationUntreatedComment");
        }
        else
        {
          Hide("IndicationUntreatedIRList");
          Hide("IndicationUntreatedCSList");
          Hide("IndicationUntreatedComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "DoseDose").value == "True")
        {
          Show("DoseDoseList");
          Show("DoseDoseIRList");
          Show("DoseDoseCSList");
          Show("DoseDoseComment");
        }
        else
        {
          Hide("DoseDoseList");
          Hide("DoseDoseIRList");
          Hide("DoseDoseCSList");
          Hide("DoseDoseComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "DoseInterval").value == "True")
        {
          Show("DoseIntervalList");
          Show("DoseIntervalIRList");
          Show("DoseIntervalCSList");
          Show("DoseIntervalComment");
        }
        else
        {
          Hide("DoseIntervalList");
          Hide("DoseIntervalIRList");
          Hide("DoseIntervalCSList");
          Hide("DoseIntervalComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "EfficacyChange").value == "True")
        {
          Show("EfficacyChangeIRList");
          Show("EfficacyChangeCSList");
          Show("EfficacyChangeComment");
        }
        else
        {
          Hide("EfficacyChangeIRList");
          Hide("EfficacyChangeCSList");
          Hide("EfficacyChangeComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "SafetyAllergic").value == "True")
        {
          Show("SafetyAllergicURL");
          Show("SafetyAllergicIRList");
          Show("SafetyAllergicCSList");
          Show("SafetyAllergicComment");
        }
        else
        {
          Hide("SafetyAllergicURL");
          Hide("SafetyAllergicIRList");
          Hide("SafetyAllergicCSList");
          Hide("SafetyAllergicComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "SafetyUnwanted").value == "True")
        {
          Show("SafetyUnwantedURL");
          Show("SafetyUnwantedIRList");
          Show("SafetyUnwantedCSList");
          Show("SafetyUnwantedComment");
        }
        else
        {
          Hide("SafetyUnwantedURL");
          Hide("SafetyUnwantedIRList");
          Hide("SafetyUnwantedCSList");
          Hide("SafetyUnwantedComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "SafetyDrugDrug").value == "True")
        {
          Show("SafetyDrugDrugIRList");
          Show("SafetyDrugDrugCSList");
          Show("SafetyDrugDrugComment");
        }
        else
        {
          Hide("SafetyDrugDrugIRList");
          Hide("SafetyDrugDrugCSList");
          Hide("SafetyDrugDrugComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "SafetyDrugDiluent").value == "True")
        {
          Show("SafetyDrugDiluentIRList");
          Show("SafetyDrugDiluentCSList");
          Show("SafetyDrugDiluentComment");
        }
        else
        {
          Hide("SafetyDrugDiluentIRList");
          Hide("SafetyDrugDiluentCSList");
          Hide("SafetyDrugDiluentComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "SafetyDrugLab").value == "True")
        {
          Show("SafetyDrugLabIRList");
          Show("SafetyDrugLabCSList");
          Show("SafetyDrugLabComment");
        }
        else
        {
          Hide("SafetyDrugLabIRList");
          Hide("SafetyDrugLabCSList");
          Hide("SafetyDrugLabComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "SafetyDrugDisease").value == "True")
        {
          Show("SafetyDrugDiseaseIRList");
          Show("SafetyDrugDiseaseCSList");
          Show("SafetyDrugDiseaseComment");
        }
        else
        {
          Hide("SafetyDrugDiseaseIRList");
          Hide("SafetyDrugDiseaseCSList");
          Hide("SafetyDrugDiseaseComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "MedicationErrorMissed").value == "True")
        {
          Show("MedicationErrorMissedComment");
        }
        else
        {
          Hide("MedicationErrorMissedComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "MedicationErrorIncorrectDrug").value == "True") {
          Show("MedicationErrorIncorrectDrugComment");
        }
        else {
          Hide("MedicationErrorIncorrectDrugComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "MedicationErrorIncorrect").value == "True")
        {
          Show("MedicationErrorIncorrectComment");
        }
        else
        {
          Hide("MedicationErrorIncorrectComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "MedicationErrorPrescribed").value == "True")
        {
          Show("MedicationErrorPrescribedComment");
        }
        else
        {
          Hide("MedicationErrorPrescribedComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "MedicationErrorAdministered").value == "True")
        {
          Show("MedicationErrorAdministeredComment");
        }
        else
        {
          Hide("MedicationErrorAdministeredComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "MedicationErrorMedication").value == "True")
        {
          Show("MedicationErrorMedicationComment");
        }
        else
        {
          Hide("MedicationErrorMedicationComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "CostGeneric").value == "True")
        {
          Show("CostGenericComment");
        }
        else
        {
          Hide("CostGenericComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "CostSubstitution").value == "True")
        {
          Show("CostSubstitutionComment");
        }
        else
        {
          Hide("CostSubstitutionComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "CostDecrease").value == "True")
        {
          Show("CostDecreaseComment");
        }
        else
        {
          Hide("CostDecreaseComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_HiddenField_" + FormMode + "CostIncrease").value == "True")
        {
          Show("CostIncreaseComment");
        }
        else
        {
          Hide("CostIncreaseComment");
        }
      }
    }
  }
  else if (document.getElementById("DropDownList_Intervention").value == "6218")
  {
    var FormMode = "";
    if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_Insert"))
    {
      FormMode = "Insert";
    }
    else if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_Edit"))
    {
      FormMode = "Edit"
    }
    else if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_Item"))
    {
      FormMode = "Item"
    }

    if (FormMode != "")
    {
      if (FormMode != "Item")
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "PatientFile").checked == true || document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "PatientLabResults").checked == true || document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "PatientPrescription").checked == true)
        {
          Show("PatientTotalTime");
          Show("PatientAmount");
          Show("PatientComments");
        }
        else
        {
          Hide("PatientTotalTime");
          Hide("PatientAmount");
          Hide("PatientComments");
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "PatientTotalTime").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "PatientAmount").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "PatientComments").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Medication").checked == true)
        {
          Show("MedicationTime");
          Show("MedicationComment");
        }
        else
        {
          Hide("MedicationTime");
          Hide("MedicationComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "MedicationTime").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "MedicationComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Research").checked == true)
        {
          Show("ResearchTime");
          Show("ResearchComment");
        }
        else
        {
          Hide("ResearchTime");
          Hide("ResearchComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "ResearchTime").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "ResearchComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Rounds").checked == true)
        {
          Show("RoundsTime");
          Show("RoundsComment");
        }
        else
        {
          Hide("RoundsTime");
          Hide("RoundsComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "RoundsTime").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "RoundsComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Counselling").checked == true)
        {
          Show("CounsellingTime");
          Show("CounsellingComment");
        }
        else
        {
          Hide("CounsellingTime");
          Hide("CounsellingComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "CounsellingTime").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "CounsellingComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Training").checked == true)
        {
          Show("TrainingTime");
          Show("TrainingComment");
        }
        else
        {
          Hide("TrainingTime");
          Hide("TrainingComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "TrainingTime").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "TrainingComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Reporting").checked == true)
        {
          Show("ReportingURL");
          Show("ReportingTime");
          Show("ReportingComment");
        }
        else
        {
          Hide("ReportingURL");
          Hide("ReportingTime");
          Hide("ReportingComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "ReportingTime").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "ReportingComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Calculations").checked == true)
        {
          Show("CalculationsTime");
          Show("CalculationsComment");
        }
        else
        {
          Hide("CalculationsTime");
          Hide("CalculationsComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "CalculationsTime").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "CalculationsComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "AdviceDoctor").checked == true)
        {
          Show("AdviceDoctorTime");
          Show("AdviceDoctorComment");
        }
        else
        {
          Hide("AdviceDoctorTime");
          Hide("AdviceDoctorComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "AdviceDoctorTime").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "AdviceDoctorComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "AdviceNurse").checked == true)
        {
          Show("AdviceNurseTime");
          Show("AdviceNurseComment");
        }
        else
        {
          Hide("AdviceNurseTime");
          Hide("AdviceNurseComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "AdviceNurseTime").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "AdviceNurseComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "MedicalHistory").checked == true)
        {
          Show("MedicalHistoryTime");
          Show("MedicalHistoryComment");
        }
        else
        {
          Hide("MedicalHistoryTime");
          Hide("MedicalHistoryComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "MedicalHistoryTime").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "MedicalHistoryComment").value = "";
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_CheckBox_" + FormMode + "Statistics").checked == true)
        {
          Show("StatisticsTime");
          Show("StatisticsComment");
        }
        else
        {
          Hide("StatisticsTime");
          Hide("StatisticsComment");
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "StatisticsTime").value = "";
          document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_TextBox_" + FormMode + "StatisticsComment").value = "";
        }
      }

      if (FormMode == "Item")
      {
        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "PatientFile").value == "True" || document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "PatientLabResults").value == "True" || document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "PatientPrescription").value == "True")
        {
          Show("PatientTotalTime");
          Show("PatientAmount");
          Show("PatientComments");
        }
        else
        {
          Hide("PatientTotalTime");
          Hide("PatientAmount");
          Hide("PatientComments");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "Medication").value == "True")
        {
          Show("MedicationTime");
          Show("MedicationComment");
        }
        else
        {
          Hide("MedicationTime");
          Hide("MedicationComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "Research").value == "True")
        {
          Show("ResearchTime");
          Show("ResearchComment");
        }
        else
        {
          Hide("ResearchTime");
          Hide("ResearchComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "Rounds").value == "True")
        {
          Show("RoundsTime");
          Show("RoundsComment");
        }
        else
        {
          Hide("RoundsTime");
          Hide("RoundsComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "Counselling").value == "True")
        {
          Show("CounsellingTime");
          Show("CounsellingComment");
        }
        else
        {
          Hide("CounsellingTime");
          Hide("CounsellingComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "Training").value == "True")
        {
          Show("TrainingTime");
          Show("TrainingComment");
        }
        else
        {
          Hide("TrainingTime");
          Hide("TrainingComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "Reporting").value == "True")
        {
          Show("ReportingURL");
          Show("ReportingTime");
          Show("ReportingComment");
        }
        else
        {
          Hide("ReportingURL");
          Hide("ReportingTime");
          Hide("ReportingComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "Calculations").value == "True")
        {
          Show("CalculationsTime");
          Show("CalculationsComment");
        }
        else
        {
          Hide("CalculationsTime");
          Hide("CalculationsComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "AdviceDoctor").value == "True")
        {
          Show("AdviceDoctorTime");
          Show("AdviceDoctorComment");
        }
        else
        {
          Hide("AdviceDoctorTime");
          Hide("AdviceDoctorComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "AdviceNurse").value == "True")
        {
          Show("AdviceNurseTime");
          Show("AdviceNurseComment");
        }
        else
        {
          Hide("AdviceNurseTime");
          Hide("AdviceNurseComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "MedicalHistory").value == "True")
        {
          Show("MedicalHistoryTime");
          Show("MedicalHistoryComment");
        }
        else
        {
          Hide("MedicalHistoryTime");
          Hide("MedicalHistoryComment");
        }

        if (document.getElementById("FormView_PharmacyClinicalMetrics_PharmacistTime_Form_HiddenField_" + FormMode + "Statistics").value == "True")
        {
          Show("StatisticsTime");
          Show("StatisticsComment");
        }
        else
        {
          Hide("StatisticsTime");
          Hide("StatisticsComment");
        }
      }
    }
  }
}