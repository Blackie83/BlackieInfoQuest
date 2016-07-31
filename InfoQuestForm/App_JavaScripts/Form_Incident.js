
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
//----- --Search_EEmployeeName--------------------------------------------------------------------------------------------------------------------------
function Search_EEmployeeName()
{
  var FormMode;
  if (document.getElementById("FormView_Incident_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_Incident_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  } else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeNumber").value != "")
    {
      document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeName").value = "Please Wait, Searching...";
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Search_PName----------------------------------------------------------------------------------------------------------------------------------
function Search_PName()
{
  var FormMode;
  if (document.getElementById("FormView_Incident_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_Incident_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  } else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PVisitNumber").value != "")
    {
      document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PName").value = "Please Wait, Searching...";
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Search_PatientDetailName----------------------------------------------------------------------------------------------------------------------
function Search_PatientDetailName()
{
  var FormMode;
  if (document.getElementById("FormView_Incident_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_Incident_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  } else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PatientDetailVisitNumber").value != "")
    {
      document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PatientDetailName").value = "Please Wait, Searching...";
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Search_ReportableCEOEmployeeName--------------------------------------------------------------------------------------------------------------
function Search_ReportableCEOEmployeeName()
{
  var FormMode;
  if (document.getElementById("FormView_Incident_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_Incident_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  } else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOEmployeeNumber").value != "")
    {
      document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOEmployeeName").value = "Please Wait, Searching...";
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_Incident_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_Incident_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "Facility"))
    {
      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "Facility").value != "")
      {
        document.getElementById("FormFacility").style.backgroundColor = "#77cf9c";
        document.getElementById("FormFacility").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "FacilityFrom").value == "")
    {
      document.getElementById("FormFacilityFrom").style.backgroundColor = "#d46e6e";
      document.getElementById("FormFacilityFrom").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormFacilityFrom").style.backgroundColor = "#77cf9c";
      document.getElementById("FormFacilityFrom").style.color = "#333333";
    }

    if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "Date").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "Date").value == "yyyy/mm/dd")
    {
      document.getElementById("FormDate").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDate").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDate").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDate").style.color = "#333333";
    }

    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "TimeHours").value == "" || document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "TimeMin").value == "")
    {
      document.getElementById("FormTime").style.backgroundColor = "#d46e6e";
      document.getElementById("FormTime").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormTime").style.backgroundColor = "#77cf9c";
      document.getElementById("FormTime").style.color = "#333333";
    }

    if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportingPerson").value.trim() == "")
    {
      document.getElementById("FormReportingPerson").style.backgroundColor = "#d46e6e";
      document.getElementById("FormReportingPerson").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormReportingPerson").style.backgroundColor = "#77cf9c";
      document.getElementById("FormReportingPerson").style.color = "#333333";
    }

    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "UnitFromUnit").value == "")
    {
      document.getElementById("FormUnitFromUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUnitFromUnit").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormUnitFromUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUnitFromUnit").style.color = "#333333";
    }

    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "UnitToUnit").value == "")
    {
      document.getElementById("FormUnitToUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUnitToUnit").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormUnitToUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUnitToUnit").style.color = "#333333";
    }

    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "DisciplineList").value == "")
    {
      document.getElementById("FormDisciplineList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDisciplineList").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDisciplineList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDisciplineList").style.color = "#333333";
    }

    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "")
    {
      document.getElementById("FormIncidentCategoryList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormIncidentCategoryList").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormIncidentCategoryList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormIncidentCategoryList").style.color = "#333333";
    }

    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "266")
    {
      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRName").value.trim() == "")
      {
        document.getElementById("FormCVRName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCVRName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormCVRName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCVRName").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRContactNumber").value.trim() == "")
      {
        document.getElementById("FormCVRContactNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCVRContactNumber").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormCVRContactNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCVRContactNumber").style.color = "#333333";
      }

      document.getElementById("FormEEmployeeNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeNumber").style.color = "#000000";
      document.getElementById("FormEEmployeeName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeName").style.color = "#000000";
      document.getElementById("FormEEmployeeUnitUnit").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeUnitUnit").style.color = "#000000";
      document.getElementById("FormEEmployeeStatusList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeStatusList").style.color = "#000000";
      document.getElementById("FormEStaffCategoryList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEStaffCategoryList").style.color = "#000000";
      document.getElementById("FormEBodyPartAffectedList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEBodyPartAffectedList").style.color = "#000000";
      document.getElementById("FormETreatmentRequiredList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormETreatmentRequiredList").style.color = "#000000";
      document.getElementById("FormEDaysOff").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEDaysOff").style.color = "#000000";
      document.getElementById("FormEMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEMainContributorList").style.color = "#000000";
      document.getElementById("FormEMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEMainContributorStaffList").style.color = "#000000";
      document.getElementById("FormMMDTName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTName").style.color = "#000000";
      document.getElementById("FormMMDTContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTContactNumber").style.color = "#000000";
      document.getElementById("FormMMDTDisciplineList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTDisciplineList").style.color = "#000000";
      document.getElementById("FormPVisitNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPVisitNumber").style.color = "#000000";
      document.getElementById("FormPName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPName").style.color = "#000000";
      document.getElementById("FormPMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPMainContributorList").style.color = "#000000";
      document.getElementById("FormPMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPMainContributorStaffList").style.color = "#000000";
      document.getElementById("FormPropMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPropMainContributorList").style.color = "#000000";
      document.getElementById("FormPropMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPropMainContributorStaffList").style.color = "#000000";
      document.getElementById("FormSSName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSSName").style.color = "#000000";
      document.getElementById("FormSSContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSSContactNumber").style.color = "#000000";
    }
    else if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "267")
    {
      document.getElementById("FormCVRName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCVRName").style.color = "#000000";
      document.getElementById("FormCVRContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCVRContactNumber").style.color = "#000000";

      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeNumber").value.trim() == "")
      {
        document.getElementById("FormEEmployeeNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEEmployeeNumber").style.color = "#333333";

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeName").value = "";
      }
      else
      {
        document.getElementById("FormEEmployeeNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEEmployeeNumber").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeName").value.trim() == "")
      {
        document.getElementById("FormEEmployeeName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEEmployeeName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEEmployeeName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEEmployeeName").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeUnitUnit").value == "")
      {
        document.getElementById("FormEEmployeeUnitUnit").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEEmployeeUnitUnit").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEEmployeeUnitUnit").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEEmployeeUnitUnit").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeStatusList").value == "")
      {
        document.getElementById("FormEEmployeeStatusList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEEmployeeStatusList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEEmployeeStatusList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEEmployeeStatusList").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EStaffCategoryList").value == "")
      {
        document.getElementById("FormEStaffCategoryList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEStaffCategoryList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEStaffCategoryList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEStaffCategoryList").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EBodyPartAffectedList").value == "")
      {
        document.getElementById("FormEBodyPartAffectedList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEBodyPartAffectedList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEBodyPartAffectedList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEBodyPartAffectedList").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ETreatmentRequiredList").value == "")
      {
        document.getElementById("FormETreatmentRequiredList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormETreatmentRequiredList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormETreatmentRequiredList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormETreatmentRequiredList").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EDaysOff").value.trim() == "")
      {
        document.getElementById("FormEDaysOff").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEDaysOff").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEDaysOff").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEDaysOff").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorList").value == "")
      {
        document.getElementById("FormEMainContributorList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEMainContributorList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEMainContributorList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEMainContributorList").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorList").value == "6261")
      {
        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorStaffList").value == "")
        {
          document.getElementById("FormEMainContributorStaffList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormEMainContributorStaffList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormEMainContributorStaffList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormEMainContributorStaffList").style.color = "#333333";
        }
      }

      document.getElementById("FormMMDTName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTName").style.color = "#000000";
      document.getElementById("FormMMDTContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTContactNumber").style.color = "#000000";
      document.getElementById("FormMMDTDisciplineList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTDisciplineList").style.color = "#000000";
      document.getElementById("FormPVisitNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPVisitNumber").style.color = "#000000";
      document.getElementById("FormPName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPName").style.color = "#000000";
      document.getElementById("FormPMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPMainContributorList").style.color = "#000000";
      document.getElementById("FormPMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPMainContributorStaffList").style.color = "#000000";
      document.getElementById("FormPropMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPropMainContributorList").style.color = "#000000";
      document.getElementById("FormPropMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPropMainContributorStaffList").style.color = "#000000";
      document.getElementById("FormSSName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSSName").style.color = "#000000";
      document.getElementById("FormSSContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSSContactNumber").style.color = "#000000";
    }
    else if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "268")
    {
      document.getElementById("FormCVRName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCVRName").style.color = "#000000";
      document.getElementById("FormCVRContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCVRContactNumber").style.color = "#000000";
      document.getElementById("FormEEmployeeNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeNumber").style.color = "#000000";
      document.getElementById("FormEEmployeeName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeName").style.color = "#000000";
      document.getElementById("FormEEmployeeUnitUnit").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeUnitUnit").style.color = "#000000";
      document.getElementById("FormEEmployeeStatusList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeStatusList").style.color = "#000000";
      document.getElementById("FormEStaffCategoryList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEStaffCategoryList").style.color = "#000000";
      document.getElementById("FormEBodyPartAffectedList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEBodyPartAffectedList").style.color = "#000000";
      document.getElementById("FormETreatmentRequiredList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormETreatmentRequiredList").style.color = "#000000";
      document.getElementById("FormEDaysOff").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEDaysOff").style.color = "#000000";
      document.getElementById("FormEMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEMainContributorList").style.color = "#000000";
      document.getElementById("FormEMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEMainContributorStaffList").style.color = "#000000";

      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTName").value.trim() == "")
      {
        document.getElementById("FormMMDTName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormMMDTName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormMMDTName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormMMDTName").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTContactNumber").value.trim() == "")
      {
        document.getElementById("FormMMDTContactNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormMMDTContactNumber").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormMMDTContactNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormMMDTContactNumber").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "MMDTDisciplineList").value == "")
      {
        document.getElementById("FormMMDTDisciplineList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormMMDTDisciplineList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormMMDTDisciplineList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormMMDTDisciplineList").style.color = "#333333";
      }

      document.getElementById("FormPVisitNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPVisitNumber").style.color = "#000000";
      document.getElementById("FormPName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPName").style.color = "#000000";
      document.getElementById("FormPMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPMainContributorList").style.color = "#000000";
      document.getElementById("FormPMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPMainContributorStaffList").style.color = "#000000";
      document.getElementById("FormPropMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPropMainContributorList").style.color = "#000000";
      document.getElementById("FormPropMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPropMainContributorStaffList").style.color = "#000000";
      document.getElementById("FormSSName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSSName").style.color = "#000000";
      document.getElementById("FormSSContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSSContactNumber").style.color = "#000000";
    }
    else if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "269")
    {
      document.getElementById("FormCVRName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCVRName").style.color = "#000000";
      document.getElementById("FormCVRContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCVRContactNumber").style.color = "#000000";
      document.getElementById("FormEEmployeeNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeNumber").style.color = "#000000";
      document.getElementById("FormEEmployeeName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeName").style.color = "#000000";
      document.getElementById("FormEEmployeeUnitUnit").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeUnitUnit").style.color = "#000000";
      document.getElementById("FormEEmployeeStatusList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeStatusList").style.color = "#000000";
      document.getElementById("FormEStaffCategoryList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEStaffCategoryList").style.color = "#000000";
      document.getElementById("FormEBodyPartAffectedList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEBodyPartAffectedList").style.color = "#000000";
      document.getElementById("FormETreatmentRequiredList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormETreatmentRequiredList").style.color = "#000000";
      document.getElementById("FormEDaysOff").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEDaysOff").style.color = "#000000";
      document.getElementById("FormEMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEMainContributorList").style.color = "#000000";
      document.getElementById("FormEMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEMainContributorStaffList").style.color = "#000000";
      document.getElementById("FormMMDTName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTName").style.color = "#000000";
      document.getElementById("FormMMDTContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTContactNumber").style.color = "#000000";
      document.getElementById("FormMMDTDisciplineList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTDisciplineList").style.color = "#000000";

      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PVisitNumber").value.trim() == "")
      {
        document.getElementById("FormPVisitNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPVisitNumber").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPVisitNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPVisitNumber").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PName").value.trim() == "")
      {
        document.getElementById("FormPName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPName").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorList").value == "")
      {
        document.getElementById("FormPMainContributorList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPMainContributorList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPMainContributorList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPMainContributorList").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorList").value == "6261")
      {
        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorStaffList").value == "")
        {
          document.getElementById("FormPMainContributorStaffList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPMainContributorStaffList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPMainContributorStaffList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPMainContributorStaffList").style.color = "#333333";
        }
      }

      document.getElementById("FormPropMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPropMainContributorList").style.color = "#000000";
      document.getElementById("FormPropMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPropMainContributorStaffList").style.color = "#000000";
      document.getElementById("FormSSName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSSName").style.color = "#000000";
      document.getElementById("FormSSContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSSContactNumber").style.color = "#000000";

    }
    else if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "270")
    {
      document.getElementById("FormCVRName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCVRName").style.color = "#000000";
      document.getElementById("FormCVRContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCVRContactNumber").style.color = "#000000";
      document.getElementById("FormEEmployeeNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeNumber").style.color = "#000000";
      document.getElementById("FormEEmployeeName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeName").style.color = "#000000";
      document.getElementById("FormEEmployeeUnitUnit").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeUnitUnit").style.color = "#000000";
      document.getElementById("FormEEmployeeStatusList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeStatusList").style.color = "#000000";
      document.getElementById("FormEStaffCategoryList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEStaffCategoryList").style.color = "#000000";
      document.getElementById("FormEBodyPartAffectedList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEBodyPartAffectedList").style.color = "#000000";
      document.getElementById("FormETreatmentRequiredList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormETreatmentRequiredList").style.color = "#000000";
      document.getElementById("FormEDaysOff").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEDaysOff").style.color = "#000000";
      document.getElementById("FormEMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEMainContributorList").style.color = "#000000";
      document.getElementById("FormEMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEMainContributorStaffList").style.color = "#000000";
      document.getElementById("FormMMDTName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTName").style.color = "#000000";
      document.getElementById("FormMMDTContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTContactNumber").style.color = "#000000";
      document.getElementById("FormMMDTDisciplineList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTDisciplineList").style.color = "#000000";
      document.getElementById("FormPVisitNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPVisitNumber").style.color = "#000000";
      document.getElementById("FormPName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPName").style.color = "#000000";
      document.getElementById("FormPMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPMainContributorList").style.color = "#000000";
      document.getElementById("FormPMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPMainContributorStaffList").style.color = "#000000";

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorList").value == "")
      {
        document.getElementById("FormPropMainContributorList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPropMainContributorList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPropMainContributorList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPropMainContributorList").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorList").value == "6261")
      {
        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorStaffList").value == "")
        {
          document.getElementById("FormPropMainContributorStaffList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPropMainContributorStaffList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPropMainContributorStaffList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPropMainContributorStaffList").style.color = "#333333";
        }
      }

      document.getElementById("FormSSName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSSName").style.color = "#000000";
      document.getElementById("FormSSContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSSContactNumber").style.color = "#000000";
    }
    else if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "271")
    {
      document.getElementById("FormCVRName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCVRName").style.color = "#000000";
      document.getElementById("FormCVRContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCVRContactNumber").style.color = "#000000";
      document.getElementById("FormEEmployeeNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeNumber").style.color = "#000000";
      document.getElementById("FormEEmployeeName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeName").style.color = "#000000";
      document.getElementById("FormEEmployeeUnitUnit").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeUnitUnit").style.color = "#000000";
      document.getElementById("FormEEmployeeStatusList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEEmployeeStatusList").style.color = "#000000";
      document.getElementById("FormEStaffCategoryList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEStaffCategoryList").style.color = "#000000";
      document.getElementById("FormEBodyPartAffectedList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEBodyPartAffectedList").style.color = "#000000";
      document.getElementById("FormETreatmentRequiredList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormETreatmentRequiredList").style.color = "#000000";
      document.getElementById("FormEDaysOff").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEDaysOff").style.color = "#000000";
      document.getElementById("FormEMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEMainContributorList").style.color = "#000000";
      document.getElementById("FormEMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormEMainContributorStaffList").style.color = "#000000";
      document.getElementById("FormMMDTName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTName").style.color = "#000000";
      document.getElementById("FormMMDTContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTContactNumber").style.color = "#000000";
      document.getElementById("FormMMDTDisciplineList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormMMDTDisciplineList").style.color = "#000000";
      document.getElementById("FormPVisitNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPVisitNumber").style.color = "#000000";
      document.getElementById("FormPName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPName").style.color = "#000000";
      document.getElementById("FormPMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPMainContributorList").style.color = "#000000";
      document.getElementById("FormPMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPMainContributorStaffList").style.color = "#000000";
      document.getElementById("FormPropMainContributorList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPropMainContributorList").style.color = "#000000";
      document.getElementById("FormPropMainContributorStaffList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPropMainContributorStaffList").style.color = "#000000";

      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSName").value.trim() == "")
      {
        document.getElementById("FormSSName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormSSName").style.color = "#333333";
      } else
      {
        document.getElementById("FormSSName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormSSName").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSContactNumber").value.trim() == "")
      {
        document.getElementById("FormSSContactNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormSSContactNumber").style.color = "#333333";
      } else
      {
        document.getElementById("FormSSContactNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormSSContactNumber").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "Description").value.trim() == "")
    {
      document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDescription").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDescription").style.color = "#333333";
    }

    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "Level1List").value == "")
    {
      document.getElementById("FormLevel1List").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLevel1List").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormLevel1List").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLevel1List").style.color = "#333333";
    }

    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "Level2List").value == "")
    {
      document.getElementById("FormLevel2List").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLevel2List").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormLevel2List").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLevel2List").style.color = "#333333";
    }

    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "Level3List").length == 1)
    {
      document.getElementById("FormLevel3List").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormLevel3List").style.color = "#000000";
    }
    else
    {
      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "Level3List").value == "")
      {
        document.getElementById("FormLevel3List").style.backgroundColor = "#d46e6e";
        document.getElementById("FormLevel3List").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormLevel3List").style.backgroundColor = "#77cf9c";
        document.getElementById("FormLevel3List").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "SeverityList").value == "")
    {
      document.getElementById("FormSeverityList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSeverityList").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormSeverityList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSeverityList").style.color = "#333333";
    }


    if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked == true)
    {
      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCOID").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCOIDDate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCOIDDate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCOIDNumber").value.trim() == "")
        {
          document.getElementById("FormReportCOID").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportCOID").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportCOID").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportCOID").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportCOID").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportCOID").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDEAT").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDEATDate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDEATDate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDEATNumber").value.trim() == "")
        {
          document.getElementById("FormReportDEAT").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportDEAT").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportDEAT").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportDEAT").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportDEAT").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportDEAT").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfHealth").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfHealthDate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfHealthDate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfHealthNumber").value.trim() == "")
        {
          document.getElementById("FormReportDepartmentOfHealth").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportDepartmentOfHealth").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportDepartmentOfHealth").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportDepartmentOfHealth").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportDepartmentOfHealth").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportDepartmentOfHealth").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfLabour").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfLabourDate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfLabourDate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfLabourNumber").value.trim() == "")
        {
          document.getElementById("FormReportDepartmentOfLabour").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportDepartmentOfLabour").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportDepartmentOfLabour").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportDepartmentOfLabour").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportDepartmentOfLabour").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportDepartmentOfLabour").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHospitalManager").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHospitalManagerDate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHospitalManagerDate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHospitalManagerNumber").value.trim() == "")
        {
          document.getElementById("FormReportHospitalManager").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportHospitalManager").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportHospitalManager").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportHospitalManager").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportHospitalManager").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportHospitalManager").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHPCSA").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHPCSADate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHPCSADate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHPCSANumber").value.trim() == "")
        {
          document.getElementById("FormReportHPCSA").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportHPCSA").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportHPCSA").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportHPCSA").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportHPCSA").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportHPCSA").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportLegalDepartment").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportLegalDepartmentDate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportLegalDepartmentDate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportLegalDepartmentNumber").value.trim() == "")
        {
          document.getElementById("FormReportLegalDepartment").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportLegalDepartment").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportLegalDepartment").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportLegalDepartment").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportLegalDepartment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportLegalDepartment").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCEO").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCEODate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCEODate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCEONumber").value.trim() == "")
        {
          document.getElementById("FormReportCEO").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportCEO").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportCEO").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportCEO").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportCEO").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportCEO").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportPharmacyCouncil").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportPharmacyCouncilDate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportPharmacyCouncilDate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportPharmacyCouncilNumber").value.trim() == "")
        {
          document.getElementById("FormReportPharmacyCouncil").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportPharmacyCouncil").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportPharmacyCouncil").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportPharmacyCouncil").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportPharmacyCouncil").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportPharmacyCouncil").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportQuality").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportQualityDate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportQualityDate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportQualityNumber").value.trim() == "")
        {
          document.getElementById("FormReportQuality").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportQuality").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportQuality").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportQuality").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportQuality").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportQuality").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportRM").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportRMDate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportRMDate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportRMNumber").value.trim() == "")
        {
          document.getElementById("FormReportRM").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportRM").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportRM").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportRM").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportRM").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportRM").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSANC").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSANCDate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSANCDate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSANCNumber").value.trim() == "")
        {
          document.getElementById("FormReportSANC").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportSANC").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportSANC").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportSANC").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportSANC").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportSANC").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSAPSDate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSAPSDate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSAPSNumber").value.trim() == "")
        {
          document.getElementById("FormReportSAPS").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportSAPS").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportSAPS").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportSAPS").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportSAPS").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportSAPS").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportInternalAudit").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportInternalAuditDate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportInternalAuditDate").value == "yyyy/mm/dd" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportInternalAuditNumber").value.trim() == "")
        {
          document.getElementById("FormReportInternalAudit").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportInternalAudit").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportInternalAudit").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportInternalAudit").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportInternalAudit").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportInternalAudit").style.color = "#000000";
      }
    }
    else
    {
      document.getElementById("FormReportCOID").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportCOID").style.color = "#000000";
      document.getElementById("FormReportDEAT").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportDEAT").style.color = "#000000";
      document.getElementById("FormReportDepartmentOfHealth").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportDepartmentOfHealth").style.color = "#000000";
      document.getElementById("FormReportDepartmentOfLabour").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportDepartmentOfLabour").style.color = "#000000";
      document.getElementById("FormReportHospitalManager").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportHospitalManager").style.color = "#000000";
      document.getElementById("FormReportHPCSA").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportHPCSA").style.color = "#000000";
      document.getElementById("FormReportLegalDepartment").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportLegalDepartment").style.color = "#000000";
      document.getElementById("FormReportCEO").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportCEO").style.color = "#000000";
      document.getElementById("FormReportPharmacyCouncil").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportPharmacyCouncil").style.color = "#000000";
      document.getElementById("FormReportQuality").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportQuality").style.color = "#000000";
      document.getElementById("FormReportRM").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportRM").style.color = "#000000";
      document.getElementById("FormReportSANC").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportSANC").style.color = "#000000";
      document.getElementById("FormReportSAPS").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportSAPS").style.color = "#000000";
      document.getElementById("FormReportInternalAudit").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormReportInternalAudit").style.color = "#000000";
    }


    if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "PatientFalling_TriggerLevel").value != "")
    {
      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PatientFallingWhereFallOccurList").value == "")
      {
        document.getElementById("FormPatientFallingWhereFallOccurList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPatientFallingWhereFallOccurList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPatientFallingWhereFallOccurList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPatientFallingWhereFallOccurList").style.color = "#333333";
      }
    }
    else
    {
      document.getElementById("FormPatientFallingWhereFallOccurList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPatientFallingWhereFallOccurList").style.color = "#000000";
    }


    if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "PatientDetail_TriggerLevel").value != "")
    {
      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PatientDetailVisitNumber").value.trim() == "")
      {
        document.getElementById("FormPatientDetailVisitNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPatientDetailVisitNumber").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPatientDetailVisitNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPatientDetailVisitNumber").style.color = "#333333";
      }

      if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PatientDetailName").value.trim() == "")
      {
        document.getElementById("FormPatientDetailName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPatientDetailName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPatientDetailName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPatientDetailName").style.color = "#333333";
      }
    }
    else
    {
      document.getElementById("FormPatientDetailVisitNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPatientDetailVisitNumber").style.color = "#000000";
      document.getElementById("FormPatientDetailName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPatientDetailName").style.color = "#000000";
    }


    if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "Pharmacy_TriggerLevel").value != "")
    {
      if (FormMode == "Edit")
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyInitials").value.trim() == "")
        {
          document.getElementById("FormPharmacyInitials").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyInitials").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyInitials").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyInitials").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyStaffInvolvedList").value == "")
        {
          document.getElementById("FormPharmacyStaffInvolvedList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyStaffInvolvedList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyStaffInvolvedList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyStaffInvolvedList").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyCheckingList").value == "")
        {
          document.getElementById("FormPharmacyCheckingList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyCheckingList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyCheckingList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyCheckingList").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyLocumOrPermanent").value == "")
        {
          document.getElementById("FormPharmacyLocumOrPermanent").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyLocumOrPermanent").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyLocumOrPermanent").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyLocumOrPermanent").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyStaffOnDutyList").value == "")
        {
          document.getElementById("FormPharmacyStaffOnDutyList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyStaffOnDutyList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyStaffOnDutyList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyStaffOnDutyList").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyChangeInWorkProcedure").value == "")
        {
          document.getElementById("FormPharmacyChangeInWorkProcedure").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyChangeInWorkProcedure").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyChangeInWorkProcedure").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyChangeInWorkProcedure").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyTypeOfPrescriptionList").value == "")
        {
          document.getElementById("FormPharmacyTypeOfPrescriptionList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyTypeOfPrescriptionList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyTypeOfPrescriptionList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyTypeOfPrescriptionList").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyNumberOfRxOnDay").value.trim() == "")
        {
          document.getElementById("FormPharmacyNumberOfRxOnDay").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyNumberOfRxOnDay").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyNumberOfRxOnDay").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyNumberOfRxOnDay").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyNumberOfItemsDispensedOnDay").value.trim() == "")
        {
          document.getElementById("FormPharmacyNumberOfItemsDispensedOnDay").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyNumberOfItemsDispensedOnDay").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyNumberOfItemsDispensedOnDay").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyNumberOfItemsDispensedOnDay").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyNumberOfRxDayBefore").value.trim() == "")
        {
          document.getElementById("FormPharmacyNumberOfRxDayBefore").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyNumberOfRxDayBefore").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyNumberOfRxDayBefore").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyNumberOfRxDayBefore").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyNumberOfItemsDispensedDayBefore").value.trim() == "")
        {
          document.getElementById("FormPharmacyNumberOfItemsDispensedDayBefore").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyNumberOfItemsDispensedDayBefore").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyNumberOfItemsDispensedDayBefore").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyNumberOfItemsDispensedDayBefore").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyDrugPrescribed").value.trim() == "")
        {
          document.getElementById("FormPharmacyDrugPrescribed").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyDrugPrescribed").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyDrugPrescribed").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyDrugPrescribed").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyDrugDispensed").value.trim() == "")
        {
          document.getElementById("FormPharmacyDrugDispensed").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyDrugDispensed").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyDrugDispensed").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyDrugDispensed").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyDrugPacked").value.trim() == "")
        {
          document.getElementById("FormPharmacyDrugPacked").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyDrugPacked").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyDrugPacked").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyDrugPacked").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyStrengthDrugPrescribed").value.trim() == "")
        {
          document.getElementById("FormPharmacyStrengthDrugPrescribed").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyStrengthDrugPrescribed").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyStrengthDrugPrescribed").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyStrengthDrugPrescribed").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyStrengthDrugDispensed").value.trim() == "")
        {
          document.getElementById("FormPharmacyStrengthDrugDispensed").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyStrengthDrugDispensed").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyStrengthDrugDispensed").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyStrengthDrugDispensed").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyDrugPrescribedNewOnMarket").value == "")
        {
          document.getElementById("FormPharmacyDrugPrescribedNewOnMarket").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyDrugPrescribedNewOnMarket").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyDrugPrescribedNewOnMarket").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyDrugPrescribedNewOnMarket").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyLegislativeInformationOnPrescription").value == "")
        {
          document.getElementById("FormPharmacyLegislativeInformationOnPrescription").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyLegislativeInformationOnPrescription").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyLegislativeInformationOnPrescription").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyLegislativeInformationOnPrescription").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyLegislativeInformationOnPrescription").value == "No")
        {
          if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyLegislativeInformationNotOnPrescription").value.trim() == "")
          {
            document.getElementById("FormPharmacyLegislativeInformationNotOnPrescription").style.backgroundColor = "#d46e6e";
            document.getElementById("FormPharmacyLegislativeInformationNotOnPrescription").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormPharmacyLegislativeInformationNotOnPrescription").style.backgroundColor = "#77cf9c";
            document.getElementById("FormPharmacyLegislativeInformationNotOnPrescription").style.color = "#333333";
          }
        }
        else
        {
          document.getElementById("FormPharmacyLegislativeInformationNotOnPrescription").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPharmacyLegislativeInformationNotOnPrescription").style.color = "#000000";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyDoctorName").value == "")
        {
          document.getElementById("FormPharmacyDoctorName").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyDoctorName").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyDoctorName").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyDoctorName").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyFactorsList").value == "")
        {
          document.getElementById("FormPharmacyFactorsList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyFactorsList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyFactorsList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyFactorsList").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacySystemRelatedIssuesList").value == "")
        {
          document.getElementById("FormPharmacySystemRelatedIssuesList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacySystemRelatedIssuesList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacySystemRelatedIssuesList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacySystemRelatedIssuesList").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyErgonomicProblemsList").value == "")
        {
          document.getElementById("FormPharmacyErgonomicProblemsList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyErgonomicProblemsList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyErgonomicProblemsList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyErgonomicProblemsList").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyTypeOfPrescriptionList").value != "6094")
        {
          if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyPatientCounselled").value == "")
          {
            document.getElementById("FormPharmacyPatientCounselled").style.backgroundColor = "#d46e6e";
            document.getElementById("FormPharmacyPatientCounselled").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormPharmacyPatientCounselled").style.backgroundColor = "#77cf9c";
            document.getElementById("FormPharmacyPatientCounselled").style.color = "#333333";
          }
        }
        else
        {
          document.getElementById("FormPharmacyPatientCounselled").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPharmacyPatientCounselled").style.color = "#000000";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacySimilarIncident").value == "")
        {
          document.getElementById("FormPharmacySimilarIncident").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacySimilarIncident").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacySimilarIncident").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacySimilarIncident").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyLocationList").value == "")
        {
          document.getElementById("FormPharmacyLocationList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyLocationList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyLocationList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyLocationList").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyPatientOutcomeAffected").value == "")
        {
          document.getElementById("FormPharmacyPatientOutcomeAffected").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacyPatientOutcomeAffected").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacyPatientOutcomeAffected").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacyPatientOutcomeAffected").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormPharmacyInitials").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyInitials").style.color = "#000000";
        document.getElementById("FormPharmacyStaffInvolvedList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyStaffInvolvedList").style.color = "#000000";
        document.getElementById("FormPharmacyCheckingList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyCheckingList").style.color = "#000000";
        document.getElementById("FormPharmacyLocumOrPermanent").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyLocumOrPermanent").style.color = "#000000";
        document.getElementById("FormPharmacyStaffOnDutyList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyStaffOnDutyList").style.color = "#000000";
        document.getElementById("FormPharmacyChangeInWorkProcedure").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyChangeInWorkProcedure").style.color = "#000000";
        document.getElementById("FormPharmacyTypeOfPrescriptionList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyTypeOfPrescriptionList").style.color = "#000000";
        document.getElementById("FormPharmacyNumberOfRxOnDay").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyNumberOfRxOnDay").style.color = "#000000";
        document.getElementById("FormPharmacyNumberOfItemsDispensedOnDay").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyNumberOfItemsDispensedOnDay").style.color = "#000000";
        document.getElementById("FormPharmacyNumberOfRxDayBefore").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyNumberOfRxDayBefore").style.color = "#000000";
        document.getElementById("FormPharmacyNumberOfItemsDispensedDayBefore").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyNumberOfItemsDispensedDayBefore").style.color = "#000000";
        document.getElementById("FormPharmacyDrugPrescribed").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyDrugPrescribed").style.color = "#000000";
        document.getElementById("FormPharmacyDrugDispensed").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyDrugDispensed").style.color = "#000000";
        document.getElementById("FormPharmacyDrugPacked").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyDrugPacked").style.color = "#000000";
        document.getElementById("FormPharmacyStrengthDrugPrescribed").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyStrengthDrugPrescribed").style.color = "#000000";
        document.getElementById("FormPharmacyStrengthDrugDispensed").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyStrengthDrugDispensed").style.color = "#000000";
        document.getElementById("FormPharmacyDrugPrescribedNewOnMarket").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyDrugPrescribedNewOnMarket").style.color = "#000000";
        document.getElementById("FormPharmacyLegislativeInformationOnPrescription").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyLegislativeInformationOnPrescription").style.color = "#000000";
        document.getElementById("FormPharmacyLegislativeInformationNotOnPrescription").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyLegislativeInformationNotOnPrescription").style.color = "#000000";
        document.getElementById("FormPharmacyDoctorName").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyDoctorName").style.color = "#000000";
        document.getElementById("FormPharmacyFactorsList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyFactorsList").style.color = "#000000";
        document.getElementById("FormPharmacySystemRelatedIssuesList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacySystemRelatedIssuesList").style.color = "#000000";
        document.getElementById("FormPharmacyErgonomicProblemsList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyErgonomicProblemsList").style.color = "#000000";
        document.getElementById("FormPharmacyPatientCounselled").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyPatientCounselled").style.color = "#000000";
        document.getElementById("FormPharmacySimilarIncident").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacySimilarIncident").style.color = "#000000";
        document.getElementById("FormPharmacyLocationList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyLocationList").style.color = "#000000";
        document.getElementById("FormPharmacyPatientOutcomeAffected").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacyPatientOutcomeAffected").style.color = "#000000";
      }
    }
    else
    {
      document.getElementById("FormPharmacyInitials").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyInitials").style.color = "#000000";
      document.getElementById("FormPharmacyStaffInvolvedList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyStaffInvolvedList").style.color = "#000000";
      document.getElementById("FormPharmacyCheckingList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyCheckingList").style.color = "#000000";
      document.getElementById("FormPharmacyLocumOrPermanent").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyLocumOrPermanent").style.color = "#000000";
      document.getElementById("FormPharmacyStaffOnDutyList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyStaffOnDutyList").style.color = "#000000";
      document.getElementById("FormPharmacyChangeInWorkProcedure").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyChangeInWorkProcedure").style.color = "#000000";
      document.getElementById("FormPharmacyTypeOfPrescriptionList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyTypeOfPrescriptionList").style.color = "#000000";
      document.getElementById("FormPharmacyNumberOfRxOnDay").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyNumberOfRxOnDay").style.color = "#000000";
      document.getElementById("FormPharmacyNumberOfItemsDispensedOnDay").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyNumberOfItemsDispensedOnDay").style.color = "#000000";
      document.getElementById("FormPharmacyNumberOfRxDayBefore").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyNumberOfRxDayBefore").style.color = "#000000";
      document.getElementById("FormPharmacyNumberOfItemsDispensedDayBefore").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyNumberOfItemsDispensedDayBefore").style.color = "#000000";
      document.getElementById("FormPharmacyDrugPrescribed").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyDrugPrescribed").style.color = "#000000";
      document.getElementById("FormPharmacyDrugDispensed").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyDrugDispensed").style.color = "#000000";
      document.getElementById("FormPharmacyDrugPacked").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyDrugPacked").style.color = "#000000";
      document.getElementById("FormPharmacyStrengthDrugPrescribed").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyStrengthDrugPrescribed").style.color = "#000000";
      document.getElementById("FormPharmacyStrengthDrugDispensed").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyStrengthDrugDispensed").style.color = "#000000";
      document.getElementById("FormPharmacyDrugPrescribedNewOnMarket").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyDrugPrescribedNewOnMarket").style.color = "#000000";
      document.getElementById("FormPharmacyLegislativeInformationOnPrescription").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyLegislativeInformationOnPrescription").style.color = "#000000";
      document.getElementById("FormPharmacyLegislativeInformationNotOnPrescription").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyLegislativeInformationNotOnPrescription").style.color = "#000000";
      document.getElementById("FormPharmacyDoctorName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyDoctorName").style.color = "#000000";
      document.getElementById("FormPharmacyFactorsList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyFactorsList").style.color = "#000000";
      document.getElementById("FormPharmacySystemRelatedIssuesList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacySystemRelatedIssuesList").style.color = "#000000";
      document.getElementById("FormPharmacyErgonomicProblemsList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyErgonomicProblemsList").style.color = "#000000";
      document.getElementById("FormPharmacyPatientCounselled").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyPatientCounselled").style.color = "#000000";
      document.getElementById("FormPharmacySimilarIncident").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacySimilarIncident").style.color = "#000000";
      document.getElementById("FormPharmacyLocationList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyLocationList").style.color = "#000000";
      document.getElementById("FormPharmacyPatientOutcomeAffected").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacyPatientOutcomeAffected").style.color = "#000000";
    }

    if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked == true)
    {
      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCEO").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEODoctorRelated").value == "")
        {
          document.getElementById("FormReportableCEODoctorRelated").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportableCEODoctorRelated").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportableCEODoctorRelated").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportableCEODoctorRelated").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "InvestigationCompleted"))
        {
          if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "InvestigationCompleted").checked == true)
          {
            if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOAcknowledgedHM").value == "")
            {
              document.getElementById("FormReportableCEOAcknowledgedHM").style.backgroundColor = "#d46e6e";
              document.getElementById("FormReportableCEOAcknowledgedHM").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormReportableCEOAcknowledgedHM").style.backgroundColor = "#77cf9c";
              document.getElementById("FormReportableCEOAcknowledgedHM").style.color = "#333333";
            }

            if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOCEONotifiedWithin24Hours").value == "")
            {
              document.getElementById("FormReportableCEOCEONotifiedWithin24Hours").style.backgroundColor = "#d46e6e";
              document.getElementById("FormReportableCEOCEONotifiedWithin24Hours").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormReportableCEOCEONotifiedWithin24Hours").style.backgroundColor = "#77cf9c";
              document.getElementById("FormReportableCEOCEONotifiedWithin24Hours").style.color = "#333333";
            }

            if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOProgressUpdateSent").value == "")
            {
              document.getElementById("FormReportableCEOProgressUpdateSent").style.backgroundColor = "#d46e6e";
              document.getElementById("FormReportableCEOProgressUpdateSent").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormReportableCEOProgressUpdateSent").style.backgroundColor = "#77cf9c";
              document.getElementById("FormReportableCEOProgressUpdateSent").style.color = "#333333";
            }

            if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOActionsTakenHM").value.trim() == "")
            {
              document.getElementById("FormReportableCEOActionsTakenHM").style.backgroundColor = "#d46e6e";
              document.getElementById("FormReportableCEOActionsTakenHM").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormReportableCEOActionsTakenHM").style.backgroundColor = "#77cf9c";
              document.getElementById("FormReportableCEOActionsTakenHM").style.color = "#333333";
            }

            if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEODate").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEODate").value == "yyyy/mm/dd")
            {
              document.getElementById("FormReportableCEODate").style.backgroundColor = "#d46e6e";
              document.getElementById("FormReportableCEODate").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormReportableCEODate").style.backgroundColor = "#77cf9c";
              document.getElementById("FormReportableCEODate").style.color = "#333333";
            }

            if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportableCEOActionsAgainstEmployee").checked == true)
            {
              if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOEmployeeNumber").value.trim() == "")
              {
                document.getElementById("FormReportableCEOEmployeeNumber").style.backgroundColor = "#d46e6e";
                document.getElementById("FormReportableCEOEmployeeNumber").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormReportableCEOEmployeeNumber").style.backgroundColor = "#77cf9c";
                document.getElementById("FormReportableCEOEmployeeNumber").style.color = "#333333";
              }

              if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOEmployeeName").value.trim() == "")
              {
                document.getElementById("FormReportableCEOEmployeeName").style.backgroundColor = "#d46e6e";
                document.getElementById("FormReportableCEOEmployeeName").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormReportableCEOEmployeeName").style.backgroundColor = "#77cf9c";
                document.getElementById("FormReportableCEOEmployeeName").style.color = "#333333";
              }
            }

            if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOOutcome").value.trim() == "")
            {
              document.getElementById("FormReportableCEOOutcome").style.backgroundColor = "#d46e6e";
              document.getElementById("FormReportableCEOOutcome").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormReportableCEOOutcome").style.backgroundColor = "#77cf9c";
              document.getElementById("FormReportableCEOOutcome").style.color = "#333333";
            }

            if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOCloseOffHM").value == "")
            {
              document.getElementById("FormReportableCEOCloseOffHM").style.backgroundColor = "#d46e6e";
              document.getElementById("FormReportableCEOCloseOffHM").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormReportableCEOCloseOffHM").style.backgroundColor = "#77cf9c";
              document.getElementById("FormReportableCEOCloseOffHM").style.color = "#333333";
            }

            if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOCloseOutEmailSend").value == "")
            {
              document.getElementById("FormReportableCEOCloseOutEmailSend").style.backgroundColor = "#d46e6e";
              document.getElementById("FormReportableCEOCloseOutEmailSend").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormReportableCEOCloseOutEmailSend").style.backgroundColor = "#77cf9c";
              document.getElementById("FormReportableCEOCloseOutEmailSend").style.color = "#333333";
            }
          }
          else
          {
            document.getElementById("FormReportableCEOAcknowledgedHM").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormReportableCEOAcknowledgedHM").style.color = "#000000";
            document.getElementById("FormReportableCEOCEONotifiedWithin24Hours").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormReportableCEOCEONotifiedWithin24Hours").style.color = "#000000";
            document.getElementById("FormReportableCEOProgressUpdateSent").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormReportableCEOProgressUpdateSent").style.color = "#000000";
            document.getElementById("FormReportableCEOActionsTakenHM").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormReportableCEOActionsTakenHM").style.color = "#000000";
            document.getElementById("FormReportableCEODate").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormReportableCEODate").style.color = "#000000";
            document.getElementById("FormReportableCEOEmployeeNumber").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormReportableCEOEmployeeNumber").style.color = "#000000";
            document.getElementById("FormReportableCEOEmployeeName").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormReportableCEOEmployeeName").style.color = "#000000";
            document.getElementById("FormReportableCEOOutcome").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormReportableCEOOutcome").style.color = "#000000";
            document.getElementById("FormReportableCEOCloseOffHM").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormReportableCEOCloseOffHM").style.color = "#000000";
            document.getElementById("FormReportableCEOCloseOutEmailSend").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormReportableCEOCloseOutEmailSend").style.color = "#000000";
          }
        }
        else
        {
          document.getElementById("FormReportableCEOAcknowledgedHM").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormReportableCEOAcknowledgedHM").style.color = "#000000";
          document.getElementById("FormReportableCEOCEONotifiedWithin24Hours").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormReportableCEOCEONotifiedWithin24Hours").style.color = "#000000";
          document.getElementById("FormReportableCEOProgressUpdateSent").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormReportableCEOProgressUpdateSent").style.color = "#000000";
          document.getElementById("FormReportableCEOActionsTakenHM").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormReportableCEOActionsTakenHM").style.color = "#000000";
          document.getElementById("FormReportableCEODate").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormReportableCEODate").style.color = "#000000";
          document.getElementById("FormReportableCEOEmployeeNumber").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormReportableCEOEmployeeNumber").style.color = "#000000";
          document.getElementById("FormReportableCEOEmployeeName").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormReportableCEOEmployeeName").style.color = "#000000";
          document.getElementById("FormReportableCEOOutcome").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormReportableCEOOutcome").style.color = "#000000";
          document.getElementById("FormReportableCEOCloseOffHM").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormReportableCEOCloseOffHM").style.color = "#000000";
          document.getElementById("FormReportableCEOCloseOutEmailSend").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormReportableCEOCloseOutEmailSend").style.color = "#000000";
        }
      }
      else
      {
        document.getElementById("FormReportableCEOAcknowledgedHM").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableCEOAcknowledgedHM").style.color = "#000000";
        document.getElementById("FormReportableCEODoctorRelated").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableCEODoctorRelated").style.color = "#000000";
        document.getElementById("FormReportableCEOCEONotifiedWithin24Hours").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableCEOCEONotifiedWithin24Hours").style.color = "#000000";
        document.getElementById("FormReportableCEOProgressUpdateSent").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableCEOProgressUpdateSent").style.color = "#000000";
        document.getElementById("FormReportableCEOActionsTakenHM").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableCEOActionsTakenHM").style.color = "#000000";
        document.getElementById("FormReportableCEODate").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableCEODate").style.color = "#000000";
        document.getElementById("FormReportableCEOEmployeeNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableCEOEmployeeNumber").style.color = "#000000";
        document.getElementById("FormReportableCEOEmployeeName").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableCEOEmployeeName").style.color = "#000000";
        document.getElementById("FormReportableCEOOutcome").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableCEOOutcome").style.color = "#000000";
        document.getElementById("FormReportableCEOCloseOffHM").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableCEOCloseOffHM").style.color = "#000000";
        document.getElementById("FormReportableCEOCloseOutEmailSend").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableCEOCloseOutEmailSend").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableSAPSPoliceStation").value.trim() == "")
        {
          document.getElementById("FormReportableSAPSPoliceStation").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportableSAPSPoliceStation").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportableSAPSPoliceStation").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportableSAPSPoliceStation").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableSAPSCaseNumber").value.trim() == "")
        {
          document.getElementById("FormReportableSAPSCaseNumber").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportableSAPSCaseNumber").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportableSAPSCaseNumber").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportableSAPSCaseNumber").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormReportableSAPSPoliceStation").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableSAPSPoliceStation").style.color = "#000000";
        document.getElementById("FormReportableSAPSCaseNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableSAPSCaseNumber").style.color = "#000000";
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportInternalAudit").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditDateDetected").value.trim() == "" || document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditDateDetected").value == "yyyy/mm/dd")
        {
          document.getElementById("FormReportableInternalAuditDateDetected").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportableInternalAuditDateDetected").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportableInternalAuditDateDetected").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportableInternalAuditDateDetected").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditByWhom").value.trim() == "")
        {
          document.getElementById("FormReportableInternalAuditByWhom").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportableInternalAuditByWhom").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportableInternalAuditByWhom").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportableInternalAuditByWhom").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditRecoveryPlan").value.trim() == "")
        {
          document.getElementById("FormReportableInternalAuditRecoveryPlan").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportableInternalAuditRecoveryPlan").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportableInternalAuditRecoveryPlan").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportableInternalAuditRecoveryPlan").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditStatusOfInvestigation").value.trim() == "")
        {
          document.getElementById("FormReportableInternalAuditStatusOfInvestigation").style.backgroundColor = "#d46e6e";
          document.getElementById("FormReportableInternalAuditStatusOfInvestigation").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormReportableInternalAuditStatusOfInvestigation").style.backgroundColor = "#77cf9c";
          document.getElementById("FormReportableInternalAuditStatusOfInvestigation").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked == false)
        {
          if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditSAPSNotReported").value.trim() == "")
          {
            document.getElementById("FormReportableInternalAuditSAPSNotReported").style.backgroundColor = "#d46e6e";
            document.getElementById("FormReportableInternalAuditSAPSNotReported").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormReportableInternalAuditSAPSNotReported").style.backgroundColor = "#77cf9c";
            document.getElementById("FormReportableInternalAuditSAPSNotReported").style.color = "#333333";
          }
        }
      }
      else
      {
        document.getElementById("FormReportableInternalAuditDateDetected").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableInternalAuditDateDetected").style.color = "#000000";
        document.getElementById("FormReportableInternalAuditByWhom").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableInternalAuditByWhom").style.color = "#000000";
        document.getElementById("FormReportableInternalAuditRecoveryPlan").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableInternalAuditRecoveryPlan").style.color = "#000000";
        document.getElementById("FormReportableInternalAuditStatusOfInvestigation").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormReportableInternalAuditStatusOfInvestigation").style.color = "#000000";
      }
    }

    if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "InvestigationCompleted"))
    {
      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "InvestigationCompleted").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_RadioButtonList_" + FormMode + "DegreeOfHarmList"))
        {
          var TotalRadioButtonListItemsYes = parseInt(document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "DegreeOfHarmListTotal").value);
          var CompletedRadioButtonListYes = "0";
          for (var aYes = 0; aYes < TotalRadioButtonListItemsYes; aYes++)
          {
            if (document.getElementById("FormView_Incident_Form_RadioButtonList_" + FormMode + "DegreeOfHarmList_" + aYes + "").checked == true)
            {
              CompletedRadioButtonListYes = "1";
              document.getElementById("FormDegreeOfHarmList").style.backgroundColor = "#77cf9c";
              document.getElementById("FormDegreeOfHarmList").style.color = "#333333";
            }
            else if (document.getElementById("FormView_Incident_Form_RadioButtonList_" + FormMode + "DegreeOfHarmList_" + aYes + "").checked == false && CompletedRadioButtonListYes == "0")
            {
              document.getElementById("FormDegreeOfHarmList").style.backgroundColor = "#d46e6e";
              document.getElementById("FormDegreeOfHarmList").style.color = "#333333";
            }
          }
        }

        var TotalItemsYes = parseInt(document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "DegreeOfHarmImpactImpactListTotal").value);
        var CompletedYes = "0";
        for (var aYes = 0; aYes < TotalItemsYes; aYes++)
        {
          if (document.getElementById("FormView_Incident_Form_CheckBoxList_" + FormMode + "DegreeOfHarmImpactImpactList_" + aYes + "").checked == true)
          {
            CompletedYes = "1";
            document.getElementById("FormDegreeOfHarmImpact").style.backgroundColor = "#77cf9c";
            document.getElementById("FormDegreeOfHarmImpact").style.color = "#333333";
          }
          else if (document.getElementById("FormView_Incident_Form_CheckBoxList_" + FormMode + "DegreeOfHarmImpactImpactList_" + aYes + "").checked == false && CompletedYes == "0")
          {
            document.getElementById("FormDegreeOfHarmImpact").style.backgroundColor = "#d46e6e";
            document.getElementById("FormDegreeOfHarmImpact").style.color = "#333333";
          }
        }
      }
      else
      {
        document.getElementById("FormDegreeOfHarmList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDegreeOfHarmList").style.color = "#000000";

        document.getElementById("FormDegreeOfHarmImpact").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormDegreeOfHarmImpact").style.color = "#000000";
      }
    }


    if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "InvestigationCompleted"))
    {
      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "InvestigationCompleted").checked == true)
      {
        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "RootCategoryList").value == "")
        {
          document.getElementById("FormRootCategoryList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormRootCategoryList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormRootCategoryList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormRootCategoryList").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "RootDescription").value.trim() == "")
        {
          document.getElementById("FormRootDescription").style.backgroundColor = "#d46e6e";
          document.getElementById("FormRootDescription").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormRootDescription").style.backgroundColor = "#77cf9c";
          document.getElementById("FormRootDescription").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "Investigator").value.trim() == "")
        {
          document.getElementById("FormInvestigator").style.backgroundColor = "#d46e6e";
          document.getElementById("FormInvestigator").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormInvestigator").style.backgroundColor = "#77cf9c";
          document.getElementById("FormInvestigator").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "InvestigatorContactNumber").value.trim() == "")
        {
          document.getElementById("FormInvestigatorContactNumber").style.backgroundColor = "#d46e6e";
          document.getElementById("FormInvestigatorContactNumber").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormInvestigatorContactNumber").style.backgroundColor = "#77cf9c";
          document.getElementById("FormInvestigatorContactNumber").style.color = "#333333";
        }

        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "InvestigatorDesignation").value.trim() == "")
        {
          document.getElementById("FormInvestigatorDesignation").style.backgroundColor = "#d46e6e";
          document.getElementById("FormInvestigatorDesignation").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormInvestigatorDesignation").style.backgroundColor = "#77cf9c";
          document.getElementById("FormInvestigatorDesignation").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormRootCategoryList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormRootCategoryList").style.color = "#000000";

        document.getElementById("FormRootDescription").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormRootDescription").style.color = "#000000";

        document.getElementById("FormInvestigator").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormInvestigator").style.color = "#000000";

        document.getElementById("FormInvestigatorContactNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormInvestigatorContactNumber").style.color = "#000000";

        document.getElementById("FormInvestigatorDesignation").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormInvestigatorDesignation").style.color = "#000000";
      }
    }

    if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "Status"))
    {
      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "Status").value == "Rejected")
      {
        if (document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "StatusRejectedReason").value.trim() == "")
        {
          document.getElementById("FormStatusRejectedReason").style.backgroundColor = "#d46e6e";
          document.getElementById("FormStatusRejectedReason").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormStatusRejectedReason").style.backgroundColor = "#77cf9c";
          document.getElementById("FormStatusRejectedReason").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormStatusRejectedReason").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormStatusRejectedReason").style.color = "#000000";
      }
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form(Control)
{
  var FormMode;
  if (document.getElementById("FormView_Incident_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_Incident_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else if (document.getElementById("FormView_Incident_Form_HiddenField_Item"))
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
      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "")
      {
        Hide("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeUnitUnit").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeStatusList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EStaffCategoryList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EBodyPartAffectedList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ETreatmentRequiredList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EDaysOff").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "MMDTDisciplineList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PVisitNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSContactNumber").value = "";
      }
      else if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "266")
      {
        Show("IncidentCategoryListSpace");
        Show("IncidentCategoryCVR1");
        Show("IncidentCategoryCVR2");
        Show("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeUnitUnit").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeStatusList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EStaffCategoryList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EBodyPartAffectedList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ETreatmentRequiredList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EDaysOff").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "MMDTDisciplineList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PVisitNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSContactNumber").value = "";
      }
      else if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "267")
      {
        Show("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Show("IncidentCategoryEmployee1");
        Show("IncidentCategoryEmployee2");
        Show("IncidentCategoryEmployee3");
        Show("IncidentCategoryEmployee4");
        Show("IncidentCategoryEmployee5");
        Show("IncidentCategoryEmployee6");
        Show("IncidentCategoryEmployee7");
        Show("IncidentCategoryEmployee8");
        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ETreatmentRequiredList").value == "6058")
        {
          Show("IncidentCategoryEmployee9");
        }
        else
        {
          Hide("IncidentCategoryEmployee9");
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EDaysOff").value = "";
        }

        Show("IncidentCategoryEmployee10");
        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorList").value == "6261")
        {
          Show("IncidentCategoryEmployee11");
        }
        else
        {
          Hide("IncidentCategoryEmployee11");
          document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorStaffList").value = "";
        }

        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "MMDTDisciplineList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PVisitNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSContactNumber").value = "";
      }
      else if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "268")
      {
        Show("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Show("IncidentCategoryMMDT1");
        Show("IncidentCategoryMMDT2");
        Show("IncidentCategoryMMDT3");
        Show("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeUnitUnit").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeStatusList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EStaffCategoryList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EBodyPartAffectedList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ETreatmentRequiredList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EDaysOff").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PVisitNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSContactNumber").value = "";
      }
      else if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "269")
      {
        Show("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Show("IncidentCategoryPatient1");
        Show("IncidentCategoryPatient2");
        Show("IncidentCategoryPatient3");
        Show("IncidentCategoryPatient4");
        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorList").value == "6261")
        {
          Show("IncidentCategoryPatient5");
        }
        else
        {
          Hide("IncidentCategoryPatient5");
          document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorStaffList").value = "";
        }
        
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeUnitUnit").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeStatusList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EStaffCategoryList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EBodyPartAffectedList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ETreatmentRequiredList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EDaysOff").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "MMDTDisciplineList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSContactNumber").value = "";
      }
      else if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "270")
      {
        Hide("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Show("IncidentCategoryProperty1");
        Show("IncidentCategoryProperty2");
        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorList").value == "6261")
        {
          Show("IncidentCategoryProperty3");
        }
        else
        {
          Hide("IncidentCategoryProperty3");
          document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorStaffList").value = "";
        }
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeUnitUnit").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeStatusList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EStaffCategoryList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EBodyPartAffectedList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ETreatmentRequiredList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EDaysOff").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "MMDTDisciplineList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PVisitNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSContactNumber").value = "";
      }
      else if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "IncidentCategoryList").value == "271")
      {
        Show("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Show("IncidentCategorySS1");
        Show("IncidentCategorySS2");
        Show("IncidentCategorySS3");

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeUnitUnit").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeStatusList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EStaffCategoryList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EBodyPartAffectedList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ETreatmentRequiredList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EDaysOff").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "MMDTDisciplineList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PVisitNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorStaffList").value = "";
      }
      else
      {
        Hide("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "CVRContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EEmployeeName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeUnitUnit").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EEmployeeStatusList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EStaffCategoryList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EBodyPartAffectedList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ETreatmentRequiredList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "EDaysOff").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "EMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "MMDTContactNumber").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "MMDTDisciplineList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PVisitNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PropMainContributorStaffList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "SSContactNumber").value = "";
      }


      var IncidentReportable_TriggerLevel_Value = document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "IncidentReportable_TriggerLevel").value;
      var IncidentReportable_TriggerLevel_Split = IncidentReportable_TriggerLevel_Value.split(":");
      for (var a = 0; a < IncidentReportable_TriggerLevel_Split.length; a++)
      {
        if (IncidentReportable_TriggerLevel_Split[a] == "5207;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5207;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCOID").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportCOIDCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCOID").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportCOIDCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportCOID")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCOID").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportCOIDCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCOID").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportCOIDCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCOID").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCOID").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCOID").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCOIDDate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCOIDNumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5208;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5208;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDEAT").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportDEATCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDEAT").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportDEATCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportDEAT")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDEAT").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportDEATCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDEAT").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportDEATCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDEAT").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDEAT").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDEAT").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDEATDate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDEATNumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5209;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5209;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfHealth").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportDepartmentOfHealthCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfHealth").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportDepartmentOfHealthCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportDepartmentOfHealth")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfHealth").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportDepartmentOfHealthCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfHealth").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportDepartmentOfHealthCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfHealth").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfHealth").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfHealth").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfHealthDate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfHealthNumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5210;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5210;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfLabour").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportDepartmentOfLabourCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfLabour").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportDepartmentOfLabourCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportDepartmentOfLabour")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfLabour").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportDepartmentOfLabourCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfLabour").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportDepartmentOfLabourCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfLabour").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfLabour").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfLabour").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfLabourDate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfLabourNumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5211;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5211;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHospitalManager").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportHospitalManagerCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHospitalManager").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportHospitalManagerCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportHospitalManager")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHospitalManager").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportHospitalManagerCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHospitalManager").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportHospitalManagerCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHospitalManager").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHospitalManager").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHospitalManager").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHospitalManagerDate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHospitalManagerNumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5213;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5213;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHPCSA").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportHPCSACompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHPCSA").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportHPCSACompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportHPCSA")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHPCSA").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportHPCSACompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHPCSA").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportHPCSACompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHPCSA").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHPCSA").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHPCSA").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHPCSADate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHPCSANumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5215;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5215;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportLegalDepartment").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportLegalDepartmentCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportLegalDepartment").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportLegalDepartmentCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportLegalDepartment")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportLegalDepartment").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportLegalDepartmentCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportLegalDepartment").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportLegalDepartmentCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportLegalDepartment").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportLegalDepartment").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportLegalDepartment").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportLegalDepartmentDate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportLegalDepartmentNumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5212;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5212;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCEO").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportCEOCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCEO").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportCEOCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportCEO")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCEO").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportCEOCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCEO").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportCEOCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCEO").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCEO").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCEO").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCEODate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCEONumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5216;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5216;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportPharmacyCouncil").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportPharmacyCouncilCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportPharmacyCouncil").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportPharmacyCouncilCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportPharmacyCouncil")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportPharmacyCouncil").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportPharmacyCouncilCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportPharmacyCouncil").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportPharmacyCouncilCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportPharmacyCouncil").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportPharmacyCouncil").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportPharmacyCouncil").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportPharmacyCouncilDate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportPharmacyCouncilNumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5217;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5217;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportQuality").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportQualityCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportQuality").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportQualityCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportQuality")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportQuality").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportQualityCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportQuality").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportQualityCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportQuality").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportQuality").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportQuality").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportQualityDate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportQualityNumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5218;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5218;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportRM").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportRMCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportRM").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportRMCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportRM")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportRM").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportRMCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportRM").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportRMCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportRM").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportRM").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportRM").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportRMDate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportRMNumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5219;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5219;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSANC").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportSANCCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSANC").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportSANCCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportSANC")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSANC").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportSANCCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSANC").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportSANCCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSANC").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSANC").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSANC").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSANCDate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSANCNumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5220;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5220;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportSAPSCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportSAPSCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportSAPS")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportSAPSCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportSAPSCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSAPSDate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSAPSNumber").value = "";
                  }
                }
              }
            }
          }
        }

        if (IncidentReportable_TriggerLevel_Split[a] == "5214;Yes" || IncidentReportable_TriggerLevel_Split[a] == "5214;No")
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked = true;

          if (Control == undefined)
          {
            if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
            {
              document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportInternalAudit").checked = true;
            }
            else
            {
              if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportInternalAuditCompulsory").value == "No")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportInternalAudit").checked = true;
                document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportInternalAuditCompulsory").value = "Yes";
              }
            }
          }
          else
          {
            if (Control == "ReportInternalAudit")
            {
              if (IncidentReportable_TriggerLevel_CompulsoryValue(IncidentReportable_TriggerLevel_Split[a]) == "Yes")
              {
                document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportInternalAudit").checked = true;
              }
              else
              {
                if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportInternalAuditCompulsory").value == "No")
                {
                  document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportInternalAudit").checked = true;
                  document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportInternalAuditCompulsory").value = "Yes";
                }
                else
                {
                  if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportInternalAudit").checked == true)
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportInternalAudit").checked = true;
                  }
                  else
                  {
                    document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportInternalAudit").checked = false;
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportInternalAuditDate").value = "";
                    document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportInternalAuditNumber").value = "";
                  }
                }
              }
            }
          }
        }
      }

      if (IncidentReportable_TriggerLevel_Value != "")
      {
        Validation_Form();
      }


      if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "PatientFalling_TriggerLevel").value == "")
      {
        Hide("PatientFallingSpace");
        Hide("PatientFallingHeading");
        Hide("PatientFalling1");

        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PatientFallingWhereFallOccurList").value = "";
      }
      else
      {
        Show("PatientFallingSpace");
        Show("PatientFallingHeading");
        Show("PatientFalling1");
      }


      if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "PatientDetail_TriggerLevel").value == "")
      {
        Hide("PatientDetailSpace");
        Hide("PatientDetailHeading");
        Hide("PatientDetail1");
        Hide("PatientDetail2");

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PatientDetailVisitNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PatientDetailName").value = "";
      }
      else
      {
        Show("PatientDetailSpace");
        Show("PatientDetailHeading");
        Show("PatientDetail1");
        Show("PatientDetail2");
      }


      if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "Pharmacy_TriggerLevel").value == "")
      {
        Hide("PharmacySpace");
        Hide("PharmacyHeading");
        Hide("Pharmacy1");
        Hide("Pharmacy2");
        Hide("Pharmacy3");
        Hide("Pharmacy4");
        Hide("Pharmacy5");
        Hide("Pharmacy6");
        Hide("Pharmacy7");
        Hide("Pharmacy8");
        Hide("Pharmacy9");
        Hide("Pharmacy10");
        Hide("Pharmacy11");
        Hide("Pharmacy12");
        Hide("Pharmacy13");
        Hide("Pharmacy14");
        Hide("Pharmacy15");
        Hide("Pharmacy16");
        Hide("Pharmacy17");
        Hide("Pharmacy18");
        Hide("Pharmacy19");
        Hide("Pharmacy20");
        Hide("Pharmacy21");
        Hide("Pharmacy22");
        Hide("Pharmacy23");
        Hide("Pharmacy24");
        Hide("Pharmacy25");
        Hide("Pharmacy26");
        Hide("Pharmacy27");

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyInitials").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyStaffInvolvedList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyCheckingList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyLocumOrPermanent").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyStaffOnDutyList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyChangeInWorkProcedure").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyTypeOfPrescriptionList").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyNumberOfRxOnDay").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyNumberOfItemsDispensedOnDay").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyNumberOfRxDayBefore").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyNumberOfItemsDispensedDayBefore").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyDrugPrescribed").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyDrugDispensed").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyDrugPacked").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyStrengthDrugPrescribed").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyStrengthDrugDispensed").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyDrugPrescribedNewOnMarket").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyLegislativeInformationOnPrescription").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyLegislativeInformationNotOnPrescription").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyDoctorName").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyFactorsList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacySystemRelatedIssuesList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyErgonomicProblemsList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyPatientCounselled").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacySimilarIncident").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyLocationList").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyPatientOutcomeAffected").value = "";
      }
      else
      {
        Show("PharmacySpace");
        Show("PharmacyHeading");
        Show("Pharmacy1");
        Show("Pharmacy2");
        Show("Pharmacy3");
        Show("Pharmacy4");
        Show("Pharmacy5");
        Show("Pharmacy6");
        Show("Pharmacy7");
        Show("Pharmacy8");
        Show("Pharmacy9");
        Show("Pharmacy10");
        Show("Pharmacy11");
        Show("Pharmacy12");
        Show("Pharmacy13");
        Show("Pharmacy14");
        Show("Pharmacy15");
        Show("Pharmacy16");
        Show("Pharmacy17");
        Show("Pharmacy18");

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyLegislativeInformationOnPrescription").value == "No")
        {
          Show("Pharmacy19");
        }
        else
        {
          Hide("Pharmacy19");

          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "PharmacyLegislativeInformationNotOnPrescription").value = "";
        }
        
        Show("Pharmacy20");
        Show("Pharmacy21");
        Show("Pharmacy22");
        Show("Pharmacy23");

        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyTypeOfPrescriptionList").value != "6094")
        {
          Show("Pharmacy24");
        }
        else
        {
          Hide("Pharmacy24");

          document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "PharmacyPatientCounselled").value = "";
        }

        Show("Pharmacy25");
        Show("Pharmacy26");
        Show("Pharmacy27");
      }

      if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "Reportable").checked == true)
      {
        Show("IncidentReportable1");
        Show("IncidentReportable2");
        Show("IncidentReportable3");
        Show("IncidentReportable4");
        Show("IncidentReportable5");
        Show("IncidentReportable6");
        Show("IncidentReportable7");
        Show("IncidentReportable8");
        Show("IncidentReportable9");
        Show("IncidentReportable10");
        Show("IncidentReportable11");
        Show("IncidentReportable12");
        Show("IncidentReportable13");
        Show("IncidentReportable14");
        Show("IncidentReportable15");

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCOID").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCOID").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCOIDDate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCOIDNumber").value = "";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDEAT").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDEAT").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDEATDate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDEATNumber").value = "";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfHealth").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfHealth").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfHealthDate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfHealthNumber").value = "";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfLabour").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfLabour").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfLabourDate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfLabourNumber").value = "";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHospitalManager").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHospitalManager").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHospitalManagerDate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHospitalManagerNumber").value = "";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHPCSA").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHPCSA").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHPCSADate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHPCSANumber").value = "";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportLegalDepartment").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportLegalDepartment").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportLegalDepartmentDate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportLegalDepartmentNumber").value = "";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCEO").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCEO").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCEODate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCEONumber").value = "";

          Hide("IncidentReportableCEOSpace");
          Hide("IncidentReportableCEOHeading");
          Hide("IncidentReportableCEO1");
          Hide("IncidentReportableCEO13");
          Hide("IncidentReportableCEO2");
          Hide("IncidentReportableCEO3");
          Hide("IncidentReportableCEO4");
          Hide("IncidentReportableCEO5");
          Hide("IncidentReportableCEO6");
          Hide("IncidentReportableCEO7");
          Hide("IncidentReportableCEO8");
          Hide("IncidentReportableCEO9");
          Hide("IncidentReportableCEO10");
          Hide("IncidentReportableCEO11");
          Hide("IncidentReportableCEO12");

          document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOAcknowledgedHM").value = "";
          document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEODoctorRelated").value = "";
          document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOCEONotifiedWithin24Hours").value = "";
          document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOProgressUpdateSent").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOActionsTakenHM").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEODate").value = "";
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportableCEOActionsAgainstEmployee").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOEmployeeNumber").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOEmployeeName").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOOutcome").value = "";
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportableCEOFileScanned").checked = false;
          document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOCloseOffHM").value = "";
          document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOCloseOutEmailSend").value = "";
        }
        else
        {
          Show("IncidentReportableCEOSpace");
          Show("IncidentReportableCEOHeading");
          Show("IncidentReportableCEO1");
          Show("IncidentReportableCEO13");
          Show("IncidentReportableCEO2");
          Show("IncidentReportableCEO3");
          Show("IncidentReportableCEO4");
          Show("IncidentReportableCEO5");
          Show("IncidentReportableCEO6");

          if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportableCEOActionsAgainstEmployee").checked == true)
          {
            Show("IncidentReportableCEO7");
            Show("IncidentReportableCEO8");
          }
          else
          {
            Hide("IncidentReportableCEO7");
            Hide("IncidentReportableCEO8");

            document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOEmployeeNumber").value = "";
            document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOEmployeeName").value = "";
          }

          Show("IncidentReportableCEO9");
          Show("IncidentReportableCEO10");
          Show("IncidentReportableCEO11");
          Show("IncidentReportableCEO12");

        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportPharmacyCouncil").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportPharmacyCouncil").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportPharmacyCouncilDate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportPharmacyCouncilNumber").value = "";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportQuality").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportQuality").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportQualityDate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportQualityNumber").value = "";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportRM").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportRM").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportRMDate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportRMNumber").value = "";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSANC").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSANC").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSANCDate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSANCNumber").value = "";
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSAPSDate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSAPSNumber").value = "";

          Hide("IncidentReportableSAPSSpace");
          Hide("IncidentReportableSAPSHeading");
          Hide("IncidentReportableSAPS1");
          Hide("IncidentReportableSAPS2");
          Hide("IncidentReportableSAPS3");
          Hide("IncidentReportableSAPS4");

          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableSAPSPoliceStation").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableSAPSInvestigationOfficersName").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableSAPSTelephoneNumber").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableSAPSCaseNumber").value = "";
        }
        else
        {
          Show("IncidentReportableSAPSSpace");
          Show("IncidentReportableSAPSHeading");
          Show("IncidentReportableSAPS1");
          Show("IncidentReportableSAPS2");
          Show("IncidentReportableSAPS3");
          Show("IncidentReportableSAPS4");
        }

        if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportInternalAudit").checked == false)
        {
          document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportInternalAudit").checked = false;
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportInternalAuditDate").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportInternalAuditNumber").value = "";

          Hide("IncidentReportableInternalAuditSpace");
          Hide("IncidentReportableInternalAuditHeading");
          Hide("IncidentReportableInternalAudit1");
          Hide("IncidentReportableInternalAudit2");
          Hide("IncidentReportableInternalAudit3");
          Hide("IncidentReportableInternalAudit4");
          Hide("IncidentReportableInternalAudit5");
          Hide("IncidentReportableInternalAudit6");
          Hide("IncidentReportableInternalAudit7");

          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditDateDetected").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditByWhom").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditTotalLossValue").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditTotalRecovery").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditRecoveryPlan").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditStatusOfInvestigation").value = "";
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditSAPSNotReported").value = "";
        }
        else
        {
          Show("IncidentReportableInternalAuditSpace");
          Show("IncidentReportableInternalAuditHeading");
          Show("IncidentReportableInternalAudit1");
          Show("IncidentReportableInternalAudit2");
          Show("IncidentReportableInternalAudit3");
          Show("IncidentReportableInternalAudit4");
          Show("IncidentReportableInternalAudit5");
          Show("IncidentReportableInternalAudit6");

          if (document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked == true)
          {
            Hide("IncidentReportableInternalAudit7");

            document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditSAPSNotReported").value = "";
          }
          else
          {
            Show("IncidentReportableInternalAudit7");
          }
        }
      }
      else
      {
        Hide("IncidentReportable1");
        Hide("IncidentReportable2");
        Hide("IncidentReportable3");
        Hide("IncidentReportable4");
        Hide("IncidentReportable5");
        Hide("IncidentReportable6");
        Hide("IncidentReportable7");
        Hide("IncidentReportable8");
        Hide("IncidentReportable9");
        Hide("IncidentReportable10");
        Hide("IncidentReportable11");
        Hide("IncidentReportable12");
        Hide("IncidentReportable13");
        Hide("IncidentReportable14");
        Hide("IncidentReportable15");

        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCOID").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCOIDDate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCOIDNumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDEAT").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDEATDate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDEATNumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfHealth").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfHealthDate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfHealthNumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportDepartmentOfLabour").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfLabourDate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportDepartmentOfLabourNumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHospitalManager").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHospitalManagerDate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHospitalManagerNumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportHPCSA").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHPCSADate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportHPCSANumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportLegalDepartment").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportLegalDepartmentDate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportLegalDepartmentNumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportCEO").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCEODate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportCEONumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportPharmacyCouncil").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportPharmacyCouncilDate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportPharmacyCouncilNumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportQuality").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportQualityDate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportQualityNumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportRM").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportRMDate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportRMNumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSANC").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSANCDate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSANCNumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportSAPS").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSAPSDate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportSAPSNumber").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportInternalAudit").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportInternalAuditDate").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportInternalAuditNumber").value = "";

        Hide("IncidentReportableCEOSpace");
        Hide("IncidentReportableCEOHeading");
        Hide("IncidentReportableCEO1");
        Hide("IncidentReportableCEO13");
        Hide("IncidentReportableCEO2");
        Hide("IncidentReportableCEO3");
        Hide("IncidentReportableCEO4");
        Hide("IncidentReportableCEO5");
        Hide("IncidentReportableCEO6");
        Hide("IncidentReportableCEO7");
        Hide("IncidentReportableCEO8");
        Hide("IncidentReportableCEO9");
        Hide("IncidentReportableCEO10");
        Hide("IncidentReportableCEO11");
        Hide("IncidentReportableCEO12");

        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOAcknowledgedHM").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEODoctorRelated").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOCEONotifiedWithin24Hours").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOProgressUpdateSent").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOActionsTakenHM").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEODate").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportableCEOActionsAgainstEmployee").checked = false;
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOEmployeeNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOEmployeeName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableCEOOutcome").value = "";
        document.getElementById("FormView_Incident_Form_CheckBox_" + FormMode + "ReportableCEOFileScanned").checked = false;
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOCloseOffHM").value = "";
        document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "ReportableCEOCloseOutEmailSend").value = "";

        Hide("IncidentReportableSAPSSpace");
        Hide("IncidentReportableSAPSHeading");
        Hide("IncidentReportableSAPS1");
        Hide("IncidentReportableSAPS2");
        Hide("IncidentReportableSAPS3");
        Hide("IncidentReportableSAPS4");

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableSAPSPoliceStation").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableSAPSInvestigationOfficersName").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableSAPSTelephoneNumber").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableSAPSCaseNumber").value = "";

        Hide("IncidentReportableInternalAuditSpace");
        Hide("IncidentReportableInternalAuditHeading");
        Hide("IncidentReportableInternalAudit1");
        Hide("IncidentReportableInternalAudit2");
        Hide("IncidentReportableInternalAudit3");
        Hide("IncidentReportableInternalAudit4");
        Hide("IncidentReportableInternalAudit5");
        Hide("IncidentReportableInternalAudit6");
        Hide("IncidentReportableInternalAudit7");

        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditDateDetected").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditByWhom").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditTotalLossValue").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditTotalRecovery").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditRecoveryPlan").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditStatusOfInvestigation").value = "";
        document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "ReportableInternalAuditSAPSNotReported").value = "";
      }

      if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "Status"))
      {
        if (document.getElementById("FormView_Incident_Form_DropDownList_" + FormMode + "Status").value == "Rejected")
        {
          Show("IncidentStatusRejectedReason");
        }
        else
        {
          Hide("IncidentStatusRejectedReason");
          document.getElementById("FormView_Incident_Form_TextBox_" + FormMode + "StatusRejectedReason").value = "";
        }
      }
    }

    if (FormMode == "Item")
    {
      if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "IncidentCategoryList").value == "")
      {
        Hide("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");
      }
      else if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "IncidentCategoryList").value == "266")
      {
        Show("IncidentCategoryListSpace");
        Show("IncidentCategoryCVR1");
        Show("IncidentCategoryCVR2");
        Show("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");
      }
      else if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "IncidentCategoryList").value == "267")
      {
        Show("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Show("IncidentCategoryEmployee1");
        Show("IncidentCategoryEmployee2");
        Show("IncidentCategoryEmployee3");
        Show("IncidentCategoryEmployee4");
        Show("IncidentCategoryEmployee5");
        Show("IncidentCategoryEmployee6");
        Show("IncidentCategoryEmployee7");
        Show("IncidentCategoryEmployee8");
        if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ETreatmentRequiredList").value == "6058")
        {
          Show("IncidentCategoryEmployee9");
        }
        else
        {
          Hide("IncidentCategoryEmployee9");
        }

        Show("IncidentCategoryEmployee10");
        if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "EMainContributorList").value == "6261")
        {
          Show("IncidentCategoryEmployee11");
        }
        else
        {
          Hide("IncidentCategoryEmployee11");
        }

        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");
      }
      else if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "IncidentCategoryList").value == "268")
      {
        Show("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Show("IncidentCategoryMMDT1");
        Show("IncidentCategoryMMDT2");
        Show("IncidentCategoryMMDT3");
        Show("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");
      }
      else if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "IncidentCategoryList").value == "269")
      {
        Show("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Show("IncidentCategoryPatient1");
        Show("IncidentCategoryPatient2");
        Show("IncidentCategoryPatient3");
        Show("IncidentCategoryPatient4");
        if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "PMainContributorList").value == "6261")
        {
          Show("IncidentCategoryPatient5");
        }
        else
        {
          Hide("IncidentCategoryPatient5");
        }

        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");
      }
      else if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "IncidentCategoryList").value == "270")
      {
        Hide("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Show("IncidentCategoryProperty1");
        Show("IncidentCategoryProperty2");
        if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "PropMainContributorList").value == "6261")
        {
          Show("IncidentCategoryProperty3");
        }
        else
        {
          Hide("IncidentCategoryProperty3");
        }

        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");
      }
      else if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "IncidentCategoryList").value == "271")
      {
        Show("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Show("IncidentCategorySS1");
        Show("IncidentCategorySS2");
        Show("IncidentCategorySS3");
      }
      else
      {
        Hide("IncidentCategoryListSpace");
        Hide("IncidentCategoryCVR1");
        Hide("IncidentCategoryCVR2");
        Hide("IncidentCategoryCVR3");
        Hide("IncidentCategoryEmployee1");
        Hide("IncidentCategoryEmployee2");
        Hide("IncidentCategoryEmployee3");
        Hide("IncidentCategoryEmployee4");
        Hide("IncidentCategoryEmployee5");
        Hide("IncidentCategoryEmployee6");
        Hide("IncidentCategoryEmployee7");
        Hide("IncidentCategoryEmployee8");
        Hide("IncidentCategoryEmployee9");
        Hide("IncidentCategoryEmployee10");
        Hide("IncidentCategoryEmployee11");
        Hide("IncidentCategoryMMDT1");
        Hide("IncidentCategoryMMDT2");
        Hide("IncidentCategoryMMDT3");
        Hide("IncidentCategoryMMDT4");
        Hide("IncidentCategoryPatient1");
        Hide("IncidentCategoryPatient2");
        Hide("IncidentCategoryPatient3");
        Hide("IncidentCategoryPatient4");
        Hide("IncidentCategoryPatient5");
        Hide("IncidentCategoryProperty1");
        Hide("IncidentCategoryProperty2");
        Hide("IncidentCategoryProperty3");
        Hide("IncidentCategorySS1");
        Hide("IncidentCategorySS2");
        Hide("IncidentCategorySS3");
      }


      if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "PatientFalling_TriggerLevel").value == "")
      {
        Hide("PatientFallingSpace");
        Hide("PatientFallingHeading");
        Hide("PatientFalling1");
      }
      else
      {
        Show("PatientFallingSpace");
        Show("PatientFallingHeading");
        Show("PatientFalling1");
      }


      if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "PatientDetailVisitNumber").value == "")
      {
        Hide("PatientDetailSpace");
        Hide("PatientDetailHeading");
        Hide("PatientDetail1");
        Hide("PatientDetail2");
      }
      else
      {
        Show("PatientDetailSpace");
        Show("PatientDetailHeading");
        Show("PatientDetail1");
        Show("PatientDetail2");
      }


      if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "PharmacyInitials").value == "")
      {
        Hide("PharmacySpace");
        Hide("PharmacyHeading");
        Hide("Pharmacy1");
        Hide("Pharmacy2");
        Hide("Pharmacy3");
        Hide("Pharmacy4");
        Hide("Pharmacy5");
        Hide("Pharmacy6");
        Hide("Pharmacy7");
        Hide("Pharmacy8");
        Hide("Pharmacy9");
        Hide("Pharmacy10");
        Hide("Pharmacy11");
        Hide("Pharmacy12");
        Hide("Pharmacy13");
        Hide("Pharmacy14");
        Hide("Pharmacy15");
        Hide("Pharmacy16");
        Hide("Pharmacy17");
        Hide("Pharmacy18");
        Hide("Pharmacy19");
        Hide("Pharmacy20");
        Hide("Pharmacy21");
        Hide("Pharmacy22");
        Hide("Pharmacy23");
        Hide("Pharmacy24");
        Hide("Pharmacy25");
        Hide("Pharmacy26");
        Hide("Pharmacy27");
      }
      else
      {
        Show("PharmacySpace");
        Show("PharmacyHeading");
        Show("Pharmacy1");
        Show("Pharmacy2");
        Show("Pharmacy3");
        Show("Pharmacy4");
        Show("Pharmacy5");
        Show("Pharmacy6");
        Show("Pharmacy7");
        Show("Pharmacy8");
        Show("Pharmacy9");
        Show("Pharmacy10");
        Show("Pharmacy11");
        Show("Pharmacy12");
        Show("Pharmacy13");
        Show("Pharmacy14");
        Show("Pharmacy15");
        Show("Pharmacy16");
        Show("Pharmacy17");
        Show("Pharmacy18");

        if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "PharmacyLegislativeInformationOnPrescription").value == "No")
        {
          Show("Pharmacy19");
        }
        else
        {
          Hide("Pharmacy19");
        }

        Show("Pharmacy20");
        Show("Pharmacy21");
        Show("Pharmacy22");
        Show("Pharmacy23");

        if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "PharmacyTypeOfPrescriptionList").value != "6094")
        {
          Show("Pharmacy24");
        }
        else
        {
          Hide("Pharmacy24");
        }

        Show("Pharmacy25");
        Show("Pharmacy26");
        Show("Pharmacy27");
      }

      if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "Reportable").value == "True")
      {
        Show("IncidentReportable1");
        Show("IncidentReportable2");
        Show("IncidentReportable3");
        Show("IncidentReportable4");
        Show("IncidentReportable5");
        Show("IncidentReportable6");
        Show("IncidentReportable7");
        Show("IncidentReportable8");
        Show("IncidentReportable9");
        Show("IncidentReportable10");
        Show("IncidentReportable11");
        Show("IncidentReportable12");
        Show("IncidentReportable13");
        Show("IncidentReportable14");
        Show("IncidentReportable15");


        if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportCEO").value == "True")
        {
          Show("IncidentReportableCEOSpace");
          Show("IncidentReportableCEOHeading");
          Show("IncidentReportableCEO1");
          Show("IncidentReportableCEO13");
          Show("IncidentReportableCEO2");
          Show("IncidentReportableCEO3");
          Show("IncidentReportableCEO4");
          Show("IncidentReportableCEO5");
          Show("IncidentReportableCEO6");
          Show("IncidentReportableCEO7");
          Show("IncidentReportableCEO8");
          Show("IncidentReportableCEO9");
          Show("IncidentReportableCEO10");
          Show("IncidentReportableCEO11");
          Show("IncidentReportableCEO12");
        }
        else
        {
          Hide("IncidentReportableCEOSpace");
          Hide("IncidentReportableCEOHeading");
          Hide("IncidentReportableCEO1");
          Hide("IncidentReportableCEO13");
          Hide("IncidentReportableCEO2");
          Hide("IncidentReportableCEO3");
          Hide("IncidentReportableCEO4");
          Hide("IncidentReportableCEO5");
          Hide("IncidentReportableCEO6");
          Hide("IncidentReportableCEO7");
          Hide("IncidentReportableCEO8");
          Hide("IncidentReportableCEO9");
          Hide("IncidentReportableCEO10");
          Hide("IncidentReportableCEO11");
          Hide("IncidentReportableCEO12");
        }

        if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportSAPS").value == "True")
        {
          Show("IncidentReportableSAPSSpace");
          Show("IncidentReportableSAPSHeading");
          Show("IncidentReportableSAPS1");
          Show("IncidentReportableSAPS2");
          Show("IncidentReportableSAPS3");
          Show("IncidentReportableSAPS4");
        }
        else
        {
          Hide("IncidentReportableSAPSSpace");
          Hide("IncidentReportableSAPSHeading");
          Hide("IncidentReportableSAPS1");
          Hide("IncidentReportableSAPS2");
          Hide("IncidentReportableSAPS3");
          Hide("IncidentReportableSAPS4");
        }


        if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportInternalAudit").value == "True")
        {
          Show("IncidentReportableInternalAuditSpace");
          Show("IncidentReportableInternalAuditHeading");
          Show("IncidentReportableInternalAudit1");
          Show("IncidentReportableInternalAudit2");
          Show("IncidentReportableInternalAudit3");
          Show("IncidentReportableInternalAudit4");
          Show("IncidentReportableInternalAudit5");
          Show("IncidentReportableInternalAudit6");

          if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "ReportSAPS").value == "True")
          {
            Hide("IncidentReportableInternalAudit7");
          }
          else
          {
            Show("IncidentReportableInternalAudit7");
          }
        }
        else
        {
          Hide("IncidentReportableInternalAuditSpace");
          Hide("IncidentReportableInternalAuditHeading");
          Hide("IncidentReportableInternalAudit1");
          Hide("IncidentReportableInternalAudit2");
          Hide("IncidentReportableInternalAudit3");
          Hide("IncidentReportableInternalAudit4");
          Hide("IncidentReportableInternalAudit5");
          Hide("IncidentReportableInternalAudit6");
          Hide("IncidentReportableInternalAudit7");
        }
      }
      else
      {
        Hide("IncidentReportable1");
        Hide("IncidentReportable2");
        Hide("IncidentReportable3");
        Hide("IncidentReportable4");
        Hide("IncidentReportable5");
        Hide("IncidentReportable6");
        Hide("IncidentReportable7");
        Hide("IncidentReportable8");
        Hide("IncidentReportable9");
        Hide("IncidentReportable10");
        Hide("IncidentReportable11");
        Hide("IncidentReportable12");
        Hide("IncidentReportable13");
        Hide("IncidentReportable14");
        Hide("IncidentReportable15");
        Hide("IncidentReportableCEOSpace");
        Hide("IncidentReportableCEOHeading");
        Hide("IncidentReportableCEO1");
        Hide("IncidentReportableCEO13");
        Hide("IncidentReportableCEO2");
        Hide("IncidentReportableCEO3");
        Hide("IncidentReportableCEO4");
        Hide("IncidentReportableCEO5");
        Hide("IncidentReportableCEO6");
        Hide("IncidentReportableCEO7");
        Hide("IncidentReportableCEO8");
        Hide("IncidentReportableCEO9");
        Hide("IncidentReportableCEO10");
        Hide("IncidentReportableCEO11");
        Hide("IncidentReportableCEO12");
        Hide("IncidentReportableSAPSSpace");
        Hide("IncidentReportableSAPSHeading");
        Hide("IncidentReportableSAPS1");
        Hide("IncidentReportableSAPS2");
        Hide("IncidentReportableSAPS3");
        Hide("IncidentReportableSAPS4");
        Hide("IncidentReportableInternalAuditSpace");
        Hide("IncidentReportableInternalAuditHeading");
        Hide("IncidentReportableInternalAudit1");
        Hide("IncidentReportableInternalAudit2");
        Hide("IncidentReportableInternalAudit3");
        Hide("IncidentReportableInternalAudit4");
        Hide("IncidentReportableInternalAudit5");
        Hide("IncidentReportableInternalAudit6");
        Hide("IncidentReportableInternalAudit7");
      }


      if (document.getElementById("FormView_Incident_Form_HiddenField_" + FormMode + "Status").value == "Rejected")
      {
        Show("IncidentStatusRejectedReason");
      }
      else
      {
        Hide("IncidentStatusRejectedReason");
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


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --IncidentReportable_TriggerLevel_CompulsoryValue-----------------------------------------------------------------------------------------------
function IncidentReportable_TriggerLevel_CompulsoryValue(Value)
{
  var IncidentReportable_TriggerLevel_CompulsoryValue = Value;
  var IncidentReportable_TriggerLevel_CompulsorySplit = IncidentReportable_TriggerLevel_CompulsoryValue.split(";");
  for (var b = 1; b < IncidentReportable_TriggerLevel_CompulsorySplit.length; b++)
  {
    return IncidentReportable_TriggerLevel_CompulsorySplit[1];
  }
}