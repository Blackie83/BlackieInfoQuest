
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --FormPatientInfectionHistory-------------------------------------------------------------------------------------------------------------------
function FormPatientInfectionHistory(PatientInfectionHistoryLink)
{
  var width = 800;
  var height = 700;
  var left = (screen.width - width) / 2;
  var top = (screen.height - height) / 2;
  window.open(PatientInfectionHistoryLink, 'PatientInfectionHistory', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=Yes , location=No , scrollbars=Yes , resizable=Yes , status=Yes , left=' + left + ' , top=' + top + ' ');
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_SpecimenForm-----------------------------------------------------------------------------------------------------------------------
function Validation_SpecimenForm()
{
  var FormMode;
  if (document.getElementById("FormView_IPS_Specimen_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_IPS_Specimen_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_IPS_Specimen_Form_DropDownList_" + FormMode + "SpecimenStatusList").value == "")
    {
      document.getElementById("FormSpecimenStatusList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSpecimenStatusList").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormSpecimenStatusList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSpecimenStatusList").style.color = "#333333";
    }

    if (document.getElementById("FormView_IPS_Specimen_Form_TextBox_" + FormMode + "SpecimenDate").value == "" || document.getElementById("FormView_IPS_Specimen_Form_TextBox_" + FormMode + "SpecimenDate").value == "yyyy/mm/dd")
    {
      document.getElementById("FormSpecimenDate").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSpecimenDate").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormSpecimenDate").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSpecimenDate").style.color = "#333333";
    }

    if (document.getElementById("FormView_IPS_Specimen_Form_DropDownList_" + FormMode + "SpecimenTimeHours").value == "" || document.getElementById("FormView_IPS_Specimen_Form_DropDownList_" + FormMode + "SpecimenTimeMinutes").value == "")
    {
      document.getElementById("FormSpecimenTime").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSpecimenTime").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormSpecimenTime").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSpecimenTime").style.color = "#333333";
    }

    if (document.getElementById("FormView_IPS_Specimen_Form_DropDownList_" + FormMode + "SpecimenTypeList").value == "")
    {
      document.getElementById("FormSpecimenTypeList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSpecimenTypeList").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormSpecimenTypeList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSpecimenTypeList").style.color = "#333333";
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_SpecimenResultForm-----------------------------------------------------------------------------------------------------------------
function Validation_SpecimenResultForm()
{
  var FormMode;
  if (document.getElementById("FormView_IPS_SpecimenResult_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_IPS_SpecimenResult_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_IPS_SpecimenResult_Form_TextBox_" + FormMode + "SpecimenResultLabNumber").value == "")
    {
      document.getElementById("FormSpecimenResultLabNumber").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSpecimenResultLabNumber").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormSpecimenResultLabNumber").style.backgroundColor = "#77cf9c";
      document.getElementById("FormSpecimenResultLabNumber").style.color = "#333333";
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_OrganismForm-----------------------------------------------------------------------------------------------------------------------
function Validation_OrganismForm()
{
  var FormMode;
  if (document.getElementById("FormView_IPS_Organism_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_IPS_Organism_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if ((document.getElementById("FormView_IPS_Organism_Form_TextBox_" + FormMode + "OrganismNameLookup").value == "" || document.getElementById("FormView_IPS_Organism_Form_TextBox_" + FormMode + "OrganismNameLookup").value == "Code") || document.getElementById("FormView_IPS_Organism_Form_DropDownList_" + FormMode + "OrganismNameLookup").value == "")
    {
      document.getElementById("FormOrganismName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormOrganismName").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormOrganismName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormOrganismName").style.color = "#333333";
    }

    if (document.getElementById("FormView_IPS_Organism_Form_DropDownList_" + FormMode + "OrganismResistanceList"))
    {
      if (document.getElementById("FormView_IPS_Organism_Form_DropDownList_" + FormMode + "OrganismResistanceList").value == "")
      {
        document.getElementById("FormView_IPS_Organism_Form_FormResistance").style.backgroundColor = "#d46e6e";
        document.getElementById("FormView_IPS_Organism_Form_FormResistance").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormView_IPS_Organism_Form_FormResistance").style.backgroundColor = "#77cf9c";
        document.getElementById("FormView_IPS_Organism_Form_FormResistance").style.color = "#333333";
      }
    }

    var TotalItemsYes = parseInt(document.getElementById("FormView_IPS_Organism_Form_HiddenField_" + FormMode + "RMMechanismItemListTotal").value);
    var CompletedYes = "0";
    for (var aYes = 0; aYes < TotalItemsYes; aYes++)
    {
      if (document.getElementById("FormView_IPS_Organism_Form_CheckBoxList_" + FormMode + "RMMechanismItemList_" + aYes + "").checked == true)
      {
        CompletedYes = "1";
        document.getElementById("FormResistanceMechanism").style.backgroundColor = "#77cf9c";
        document.getElementById("FormResistanceMechanism").style.color = "#333333";
      }
      else if (document.getElementById("FormView_IPS_Organism_Form_CheckBoxList_" + FormMode + "RMMechanismItemList_" + aYes + "").checked == false && CompletedYes == "0")
      {
        document.getElementById("FormResistanceMechanism").style.backgroundColor = "#d46e6e";
        document.getElementById("FormResistanceMechanism").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_IPS_Organism_Form_CheckBox_" + FormMode + "OrganismNotifiableDepartmentOfHealth"))
    {
      if (document.getElementById("FormView_IPS_Organism_Form_CheckBox_" + FormMode + "OrganismNotifiableDepartmentOfHealth").checked == true)
      {
        if (document.getElementById("FormView_IPS_Organism_Form_TextBox_" + FormMode + "OrganismNotifiableDepartmentOfHealthDate").value == "" || document.getElementById("FormView_IPS_Organism_Form_TextBox_" + FormMode + "OrganismNotifiableDepartmentOfHealthDate").value == "yyyy/mm/dd")
        {
          document.getElementById("FormView_IPS_Organism_Form_FormDOH2").style.backgroundColor = "#d46e6e";
          document.getElementById("FormView_IPS_Organism_Form_FormDOH2").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormView_IPS_Organism_Form_FormDOH2").style.backgroundColor = "#77cf9c";
          document.getElementById("FormView_IPS_Organism_Form_FormDOH2").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormView_IPS_Organism_Form_FormDOH2").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormView_IPS_Organism_Form_FormDOH2").style.color = "#000000";
        document.getElementById("FormView_IPS_Organism_Form_FormDOH3").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormView_IPS_Organism_Form_FormDOH3").style.color = "#000000";
      }
    }
  }
}
