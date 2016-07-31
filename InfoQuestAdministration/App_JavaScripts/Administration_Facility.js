
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form() {
  var FormMode;
  if (document.getElementById("FormView_Facility_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_Facility_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_Facility_Form_TextBox_" + FormMode + "FacilityName").value == "") {
      document.getElementById("FormFacilityName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormFacilityName").style.color = "#333333";
    } else {
      document.getElementById("FormFacilityName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormFacilityName").style.color = "#333333";
    }

    if (document.getElementById("FormView_Facility_Form_TextBox_" + FormMode + "FacilityCode").value == "") {
      document.getElementById("FormFacilityCode").style.backgroundColor = "#d46e6e";
      document.getElementById("FormFacilityCode").style.color = "#333333";
    } else {
      document.getElementById("FormFacilityCode").style.backgroundColor = "#77cf9c";
      document.getElementById("FormFacilityCode").style.color = "#333333";
    }

    if (document.getElementById("FormView_Facility_Form_DropDownList_" + FormMode + "FacilityType").value == "")
    {
      document.getElementById("FormFacilityType").style.backgroundColor = "#d46e6e";
      document.getElementById("FormFacilityType").style.color = "#333333";
    } else
    {
      document.getElementById("FormFacilityType").style.backgroundColor = "#77cf9c";
      document.getElementById("FormFacilityType").style.color = "#333333";
    }

    //if (document.getElementById("FormView_Facility_Form_TextBox_" + FormMode + "ImpiloUnitId").value == "")
    //{
    //  document.getElementById("FormImpiloUnitId").style.backgroundColor = "#d46e6e";
    //  document.getElementById("FormImpiloUnitId").style.color = "#333333";
    //} else
    //{
    //  document.getElementById("FormImpiloUnitId").style.backgroundColor = "#77cf9c";
    //  document.getElementById("FormImpiloUnitId").style.color = "#333333";
    //}

    if (document.getElementById("FormView_Facility_Form_TextBox_" + FormMode + "ImpiloCountryId").value == "")
    {
      document.getElementById("FormImpiloCountryId").style.backgroundColor = "#d46e6e";
      document.getElementById("FormImpiloCountryId").style.color = "#333333";
    } else
    {
      document.getElementById("FormImpiloCountryId").style.backgroundColor = "#77cf9c";
      document.getElementById("FormImpiloCountryId").style.color = "#333333";
    }

    //if (document.getElementById("FormView_Facility_Form_TextBox_" + FormMode + "IMEDSConnectionString").value == "") {
    //  document.getElementById("FormIMEDSConnectionString").style.backgroundColor = "#d46e6e";
    //  document.getElementById("FormIMEDSConnectionString").style.color = "#333333";
    //} else {
    //  document.getElementById("FormIMEDSConnectionString").style.backgroundColor = "#77cf9c";
    //  document.getElementById("FormIMEDSConnectionString").style.color = "#333333";
    //}
  }
}
