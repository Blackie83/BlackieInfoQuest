
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
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_Alert_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_Alert_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_Alert_Form_DropDownList_" + FormMode + "Facility"))
    {
      if (document.getElementById("FormView_Alert_Form_DropDownList_" + FormMode + "Facility").value != "")
      {
        document.getElementById("FormFacility").style.backgroundColor = "#77cf9c";
        document.getElementById("FormFacility").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_Alert_Form_DropDownList_" + FormMode + "FacilityFrom").value == "")
    {
      document.getElementById("FormFacilityFrom").style.backgroundColor = "#d46e6e";
      document.getElementById("FormFacilityFrom").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormFacilityFrom").style.backgroundColor = "#77cf9c";
      document.getElementById("FormFacilityFrom").style.color = "#333333";
    }

    if (document.getElementById("FormView_Alert_Form_TextBox_" + FormMode + "Date").value == "" || document.getElementById("FormView_Alert_Form_TextBox_" + FormMode + "Date").value == "yyyy/mm/dd")
    {
      document.getElementById("FormDate").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDate").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDate").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDate").style.color = "#333333";
    }

    if (document.getElementById("FormView_Alert_Form_TextBox_" + FormMode + "Originator").value == "")
    {
      document.getElementById("FormOriginator").style.backgroundColor = "#d46e6e";
      document.getElementById("FormOriginator").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormOriginator").style.backgroundColor = "#77cf9c";
      document.getElementById("FormOriginator").style.color = "#333333";
    }

    if (document.getElementById("FormView_Alert_Form_DropDownList_" + FormMode + "UnitFromUnit").value == "")
    {
      document.getElementById("FormUnitFromUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUnitFromUnit").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormUnitFromUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUnitFromUnit").style.color = "#333333";
    }

    if (document.getElementById("FormView_Alert_Form_DropDownList_" + FormMode + "UnitToUnit").value == "")
    {
      document.getElementById("FormUnitToUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUnitToUnit").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormUnitToUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUnitToUnit").style.color = "#333333";
    }

    if (document.getElementById("FormView_Alert_Form_TextBox_" + FormMode + "Description").value == "")
    {
      document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDescription").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDescription").style.color = "#333333";
    }

    if (document.getElementById("FormView_Alert_Form_DropDownList_" + FormMode + "Level1List").value == "")
    {
      document.getElementById("FormLevel1List").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLevel1List").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormLevel1List").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLevel1List").style.color = "#333333";
    }

    if (document.getElementById("FormView_Alert_Form_DropDownList_" + FormMode + "Level2List").value == "")
    {
      document.getElementById("FormLevel2List").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLevel2List").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormLevel2List").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLevel2List").style.color = "#333333";
    }

    if (document.getElementById("FormView_Alert_Form_TextBox_" + FormMode + "NumberOfInstances").value == "")
    {
      document.getElementById("FormNumberOfInstances").style.backgroundColor = "#d46e6e";
      document.getElementById("FormNumberOfInstances").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormNumberOfInstances").style.backgroundColor = "#77cf9c";
      document.getElementById("FormNumberOfInstances").style.color = "#333333";
    }

    if (document.getElementById("FormView_Alert_Form_DropDownList_" + FormMode + "Status"))
    {
      if (document.getElementById("FormView_Alert_Form_DropDownList_" + FormMode + "Status").value == "Rejected")
      {
        if (document.getElementById("FormView_Alert_Form_TextBox_" + FormMode + "StatusRejectedReason").value == "")
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
function ShowHide_Form()
{
  var FormMode;
  if (document.getElementById("FormView_Alert_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_Alert_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else if (document.getElementById("FormView_Alert_Form_HiddenField_Item"))
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
      if (document.getElementById("FormView_Alert_Form_DropDownList_" + FormMode + "Status"))
      {
        if (document.getElementById("FormView_Alert_Form_DropDownList_" + FormMode + "Status").value == "Rejected")
        {
          Show("AlertStatusRejectedReason");
        }
        else
        {
          Hide("AlertStatusRejectedReason");
          document.getElementById("FormView_Alert_Form_TextBox_" + FormMode + "StatusRejectedReason").value = "";
        }
      }
    }

    if (FormMode == "Item")
    {
      if (document.getElementById("FormView_Alert_Form_HiddenField_" + FormMode + "Status").value == "Rejected")
      {
        Show("AlertStatusRejectedReason");
      }
      else
      {
        Hide("AlertStatusRejectedReason");
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
