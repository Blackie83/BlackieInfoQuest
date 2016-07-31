
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_FSM_Location_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_FSM_Location_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_FSM_Location_Form_TextBox_" + FormMode + "LocationName").value == "")
    {
      document.getElementById("FormLocationName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLocationName").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormLocationName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLocationName").style.color = "#333333";
    }

    if (document.getElementById("FormView_FSM_Location_Form_TextBox_" + FormMode + "LocationAddress").value == "")
    {
      document.getElementById("FormLocationAddress").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLocationAddress").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormLocationAddress").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLocationAddress").style.color = "#333333";
    }

    if (document.getElementById("FormView_FSM_Location_Form_DropDownList_" + FormMode + "Country").value == "")
    {
      document.getElementById("FormCountry").style.backgroundColor = "#d46e6e";
      document.getElementById("FormCountry").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormCountry").style.backgroundColor = "#77cf9c";
      document.getElementById("FormCountry").style.color = "#333333";
    }

    if (document.getElementById("FormView_FSM_Location_Form_DropDownList_" + FormMode + "ProvinceKey").value == "")
    {
      document.getElementById("FormProvinceKey").style.backgroundColor = "#d46e6e";
      document.getElementById("FormProvinceKey").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormProvinceKey").style.backgroundColor = "#77cf9c";
      document.getElementById("FormProvinceKey").style.color = "#333333";
    }
  }
}