
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_FSM_BusinessUnit_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_FSM_BusinessUnit_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_FSM_BusinessUnit_Form_TextBox_" + FormMode + "BusinessUnitName").value == "")
    {
      document.getElementById("FormBusinessUnitName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBusinessUnitName").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBusinessUnitName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBusinessUnitName").style.color = "#333333";
    }

    if (document.getElementById("FormView_FSM_BusinessUnit_Form_DropDownList_" + FormMode + "BusinessUnitTypeKey").value == "")
    {
      document.getElementById("FormBusinessUnitTypeKey").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBusinessUnitTypeKey").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBusinessUnitTypeKey").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBusinessUnitTypeKey").style.color = "#333333";
    }

    if (document.getElementById("FormView_FSM_BusinessUnit_Form_DropDownList_" + FormMode + "BusinessUnitTypeKey").value == "1")
    {
      if (document.getElementById("FormView_FSM_BusinessUnit_Form_TextBox_" + FormMode + "RegisteredName").value == "")
      {
        document.getElementById("FormRegisteredName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormRegisteredName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormRegisteredName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormRegisteredName").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnit_Form_TextBox_" + FormMode + "ShortName").value == "")
      {
        document.getElementById("FormShortName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormShortName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormShortName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormShortName").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnit_Form_TextBox_" + FormMode + "PracticeNumber").value == "")
      {
        document.getElementById("FormPracticeNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPracticeNumber").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPracticeNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPracticeNumber").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnit_Form_DropDownList_" + FormMode + "HospitalTypeKey").value == "")
      {
        document.getElementById("FormHospitalTypeKey").style.backgroundColor = "#d46e6e";
        document.getElementById("FormHospitalTypeKey").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormHospitalTypeKey").style.backgroundColor = "#77cf9c";
        document.getElementById("FormHospitalTypeKey").style.color = "#333333";
      }
    }
    else
    {
      document.getElementById("FormRegisteredName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormRegisteredName").style.color = "#000000";
      document.getElementById("FormShortName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormShortName").style.color = "#000000";
      document.getElementById("FormPracticeNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPracticeNumber").style.color = "#000000";
      document.getElementById("FormHospitalTypeKey").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormHospitalTypeKey").style.color = "#000000";
    }

    if (document.getElementById("FormView_FSM_BusinessUnit_Form_DropDownList_" + FormMode + "LocationKey").value == "")
    {
      document.getElementById("FormLocationKey").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLocationKey").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormLocationKey").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLocationKey").style.color = "#333333";
    }

    if (document.getElementById("FormView_FSM_BusinessUnit_Form_DropDownList_" + FormMode + "BusinessUnitReportingGroupKey").value == "")
    {
      document.getElementById("FormBusinessUnitReportingGroupKey").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBusinessUnitReportingGroupKey").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBusinessUnitReportingGroupKey").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBusinessUnitReportingGroupKey").style.color = "#333333";
    }

    if (document.getElementById("FormView_FSM_BusinessUnit_Form_TextBox_" + FormMode + "BusinessUnitDefaultEntity").value == "")
    {
      document.getElementById("FormBusinessUnitDefaultEntity").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBusinessUnitDefaultEntity").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBusinessUnitDefaultEntity").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBusinessUnitDefaultEntity").style.color = "#333333";
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form(Control)
{
  var FormMode;
  if (document.getElementById("FormView_FSM_BusinessUnit_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_FSM_BusinessUnit_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else if (document.getElementById("FormView_FSM_BusinessUnit_Form_HiddenField_Item"))
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
      if (document.getElementById("FormView_FSM_BusinessUnit_Form_DropDownList_" + FormMode + "BusinessUnitTypeKey").value == "1")
      {
        Show("BusinessUnitTypeHospital1");
        Show("BusinessUnitTypeHospital2");
        Show("BusinessUnitTypeHospital3");
        Show("BusinessUnitTypeHospital4");
        Show("BusinessUnitTypeHospital5");
        Show("BusinessUnitTypeHospital6");

        Show("MappingBusinessUnit1");
        Show("MappingBusinessUnit2");
        Show("MappingBusinessUnit3");
      }
      else
      {
        Hide("BusinessUnitTypeHospital1");
        Hide("BusinessUnitTypeHospital2");
        Hide("BusinessUnitTypeHospital3");
        Hide("BusinessUnitTypeHospital4");
        Hide("BusinessUnitTypeHospital5");
        Hide("BusinessUnitTypeHospital6");

        document.getElementById("FormView_FSM_BusinessUnit_Form_TextBox_" + FormMode + "RegisteredName").value = "";
        document.getElementById("FormView_FSM_BusinessUnit_Form_TextBox_" + FormMode + "ShortName").value = "";
        document.getElementById("FormView_FSM_BusinessUnit_Form_TextBox_" + FormMode + "PracticeNumber").value = "";
        document.getElementById("FormView_FSM_BusinessUnit_Form_DropDownList_" + FormMode + "HospitalTypeKey").value = "";

        Hide("MappingBusinessUnit1");
        Hide("MappingBusinessUnit2");
        Hide("MappingBusinessUnit3");
      }
    }

    if (FormMode == "Item")
    {
      if (document.getElementById("FormView_FSM_BusinessUnit_Form_HiddenField_" + FormMode + "BusinessUnitTypeKey").value == "1")
      {
        Show("BusinessUnitTypeHospital1");
        Show("BusinessUnitTypeHospital2");
        Show("BusinessUnitTypeHospital3");
        Show("BusinessUnitTypeHospital4");
        Show("BusinessUnitTypeHospital5");
        Show("BusinessUnitTypeHospital6");

        Show("MappingBusinessUnit1");
        Show("MappingBusinessUnit2");
        Show("MappingBusinessUnit3");
      }
      else
      {
        Hide("BusinessUnitTypeHospital1");
        Hide("BusinessUnitTypeHospital2");
        Hide("BusinessUnitTypeHospital3");
        Hide("BusinessUnitTypeHospital4");
        Hide("BusinessUnitTypeHospital5");
        Hide("BusinessUnitTypeHospital6");

        Hide("MappingBusinessUnit1");
        Hide("MappingBusinessUnit2");
        Hide("MappingBusinessUnit3");
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