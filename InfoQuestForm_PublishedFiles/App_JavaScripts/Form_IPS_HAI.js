
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
//----- --Validation_HAIForm----------------------------------------------------------------------------------------------------------------------------
function Validation_HAIForm()
{
  var FormMode;
  if (document.getElementById("FormView_IPS_HAI_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_IPS_HAI_Form_DropDownList_" + FormMode + "InfectionUnitId").value == "")
    {
      document.getElementById("FormInfectionUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormInfectionUnit").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormInfectionUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormInfectionUnit").style.color = "#333333";
    }

    if (document.getElementById("FormView_IPS_HAI_Form_TextBox_" + FormMode + "InfectionSummary").value == "")
    {
      document.getElementById("FormInfectionSummary").style.backgroundColor = "#d46e6e";
      document.getElementById("FormInfectionSummary").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormInfectionSummary").style.backgroundColor = "#77cf9c";
      document.getElementById("FormInfectionSummary").style.color = "#333333";
    }

    if (document.getElementById("FormView_IPS_HAI_Form_CheckBox_" + FormMode + "InvestigationCompleted").checked == true)
    {
      //var PredisposingConditionTotalItemsYes = parseInt(document.getElementById("FormView_IPS_HAI_Form_HiddenField_" + FormMode + "PredisposingConditionTotalRecords").value);
      //for (var Items = 0; Items < PredisposingConditionTotalItemsYes; Items++)
      //{
      //  if (document.getElementById("FormView_IPS_HAI_Form_GridView_IPS_HAI_" + FormMode + "PredisposingConditionList_CheckBox_Selected_" + Items + "").checked == true)
      //  {
      //    if (document.getElementById("FormView_IPS_HAI_Form_GridView_IPS_HAI_" + FormMode + "PredisposingConditionList_TextBox_Description_" + Items + "").value == "")
      //    {
      //      document.getElementById("FormView_IPS_HAI_Form_GridView_IPS_HAI_" + FormMode + "PredisposingConditionList_Label_ConditionName_" + Items + "").parentElement.style.backgroundColor = '#d46e6e';
      //      document.getElementById("FormView_IPS_HAI_Form_GridView_IPS_HAI_" + FormMode + "PredisposingConditionList_Label_ConditionName_" + Items + "").parentElement.style.color = '#333333';
      //    }
      //    else
      //    {
      //      document.getElementById("FormView_IPS_HAI_Form_GridView_IPS_HAI_" + FormMode + "PredisposingConditionList_Label_ConditionName_" + Items + "").parentElement.style.backgroundColor = '#77cf9c';
      //      document.getElementById("FormView_IPS_HAI_Form_GridView_IPS_HAI_" + FormMode + "PredisposingConditionList_Label_ConditionName_" + Items + "").parentElement.style.color = '#333333';
      //    }
      //  }
      //  else if (document.getElementById("FormView_IPS_HAI_Form_GridView_IPS_HAI_" + FormMode + "PredisposingConditionList_CheckBox_Selected_" + Items + "").checked == false)
      //  {
      //    document.getElementById("FormView_IPS_HAI_Form_GridView_IPS_HAI_" + FormMode + "PredisposingConditionList_Label_ConditionName_" + Items + "").parentElement.style.backgroundColor = '#f7f7f7';
      //    document.getElementById("FormView_IPS_HAI_Form_GridView_IPS_HAI_" + FormMode + "PredisposingConditionList_Label_ConditionName_" + Items + "").parentElement.style.color = '#000000';
      //  }
      //}

      if (document.getElementById("FormView_IPS_HAI_Form_TextBox_" + FormMode + "BundleComplianceDays"))
      {
        if (document.getElementById("FormView_IPS_HAI_Form_TextBox_" + FormMode + "BundleComplianceDays").value == "")
        {
          document.getElementById("FormView_IPS_HAI_Form_FormBundleComplianceDays").style.backgroundColor = "#d46e6e";
          document.getElementById("FormView_IPS_HAI_Form_FormBundleComplianceDays").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormView_IPS_HAI_Form_FormBundleComplianceDays").style.backgroundColor = "#77cf9c";
          document.getElementById("FormView_IPS_HAI_Form_FormBundleComplianceDays").style.color = "#333333";
        }
      }

      if (document.getElementById("FormView_IPS_HAI_Form_TextBox_" + FormMode + "InvestigationDate").value == "" || document.getElementById("FormView_IPS_HAI_Form_TextBox_" + FormMode + "InvestigationDate").value == "yyyy/mm/dd")
      {
        document.getElementById("FormInvestigationDate").style.backgroundColor = "#d46e6e";
        document.getElementById("FormInvestigationDate").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormInvestigationDate").style.backgroundColor = "#77cf9c";
        document.getElementById("FormInvestigationDate").style.color = "#333333";
      }
            
      if (document.getElementById("FormView_IPS_HAI_Form_TextBox_" + FormMode + "InvestigationInvestigatorName").value == "")
      {
        document.getElementById("FormInvestigationInvestigatorName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormInvestigationInvestigatorName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormInvestigationInvestigatorName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormInvestigationInvestigatorName").style.color = "#333333";
      }

      if (document.getElementById("FormView_IPS_HAI_Form_TextBox_" + FormMode + "InvestigationInvestigatorDesignation").value == "")
      {
        document.getElementById("FormInvestigationInvestigatorDesignation").style.backgroundColor = "#d46e6e";
        document.getElementById("FormInvestigationInvestigatorDesignation").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormInvestigationInvestigatorDesignation").style.backgroundColor = "#77cf9c";
        document.getElementById("FormInvestigationInvestigatorDesignation").style.color = "#333333";
      }

      if (document.getElementById("FormView_IPS_HAI_Form_TextBox_" + FormMode + "InvestigationIPCName").value == "")
      {
        document.getElementById("FormInvestigationIPCName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormInvestigationIPCName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormInvestigationIPCName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormInvestigationIPCName").style.color = "#333333";
      }

      if (document.getElementById("FormView_IPS_HAI_Form_TextBox_" + FormMode + "InvestigationTeamMembers").value == "")
      {
        document.getElementById("FormInvestigationTeamMembers").style.backgroundColor = "#d46e6e";
        document.getElementById("FormInvestigationTeamMembers").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormInvestigationTeamMembers").style.backgroundColor = "#77cf9c";
        document.getElementById("FormInvestigationTeamMembers").style.color = "#333333";
      }
    }
    else
    {
      var PredisposingConditionTotalItemsYes = parseInt(document.getElementById("FormView_IPS_HAI_Form_HiddenField_" + FormMode + "PredisposingConditionTotalRecords").value);
      for (var Items = 0; Items < PredisposingConditionTotalItemsYes; Items++)
      {
        document.getElementById("FormView_IPS_HAI_Form_GridView_IPS_HAI_" + FormMode + "PredisposingConditionList_Label_ConditionName_" + Items + "").parentElement.style.backgroundColor = '#f7f7f7';
        document.getElementById("FormView_IPS_HAI_Form_GridView_IPS_HAI_" + FormMode + "PredisposingConditionList_Label_ConditionName_" + Items + "").parentElement.style.color = '#000000';
      }

      if (document.getElementById("FormView_IPS_HAI_Form_TextBox_" + FormMode + "BundleComplianceDays"))
      {
        document.getElementById("FormView_IPS_HAI_Form_FormBundleComplianceDays").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormView_IPS_HAI_Form_FormBundleComplianceDays").style.color = "#000000";
      }

      document.getElementById("FormInvestigationDate").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInvestigationDate").style.color = "#000000";
      document.getElementById("FormInvestigationInvestigatorName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInvestigationInvestigatorName").style.color = "#000000";
      document.getElementById("FormInvestigationInvestigatorDesignation").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInvestigationInvestigatorDesignation").style.color = "#000000";
      document.getElementById("FormInvestigationIPCName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInvestigationIPCName").style.color = "#000000";
      document.getElementById("FormInvestigationTeamMembers").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormInvestigationTeamMembers").style.color = "#000000";
    }    
  }
}