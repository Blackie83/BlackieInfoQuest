
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
//----- --NursingSelectAllYes_Form----------------------------------------------------------------------------------------------------------------------
function NursingSelectAllYes_Form()
{
  var FormMode;
  if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "ByList").value == "5390")
    {
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_0").checked = true
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_0").checked = true
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_0").checked = true
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_0").checked = true
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_0").checked = true
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_0").checked = true
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --PharmacySelectAllYes_Form---------------------------------------------------------------------------------------------------------------------
function PharmacySelectAllYes_Form()
{
  var FormMode;
  if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "ByList").value == "5389")
    {
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1_0").checked = true
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_0").checked = true
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3_0").checked = true
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4_0").checked = true
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5_0").checked = true
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_0").checked = true
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7_0").checked = true
      document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy8_0").checked = true
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_TextBox_" + FormMode + "Date").value == "" || document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_TextBox_" + FormMode + "Date").value == "yyyy/mm/dd")
    {
      document.getElementById("FormDate").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDate").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDate").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDate").style.color = "#333333";
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "ByList").value == "")
    {
      document.getElementById("FormByList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormByList").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormByList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormByList").style.color = "#333333";
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "Unit").value == "")
    {
      document.getElementById("FormUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUnit").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUnit").style.color = "#333333";
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "Doctor").value == "")
    {
      document.getElementById("FormDoctor").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDoctor").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDoctor").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDoctor").style.color = "#333333";
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "ByList").value == "5390")
    {
      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_2").checked == false)
      {
        document.getElementById("FormNursing1").style.backgroundColor = "#d46e6e";
        document.getElementById("FormNursing1").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormNursing1").style.backgroundColor = "#77cf9c";
        document.getElementById("FormNursing1").style.color = "#333333";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_1").checked == true)
      {
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1B_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1B_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1B_2").checked == false)
        {
          document.getElementById("FormNursing1B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormNursing1B").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormNursing1B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormNursing1B").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormNursing1B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormNursing1B").style.color = "#000000";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_2").checked == false)
      {
        document.getElementById("FormNursing2").style.backgroundColor = "#d46e6e";
        document.getElementById("FormNursing2").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormNursing2").style.backgroundColor = "#77cf9c";
        document.getElementById("FormNursing2").style.color = "#333333";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_1").checked == true)
      {
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2B_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2B_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2B_2").checked == false)
        {
          document.getElementById("FormNursing2B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormNursing2B").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormNursing2B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormNursing2B").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormNursing2B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormNursing2B").style.color = "#000000";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_2").checked == false)
      {
        document.getElementById("FormNursing3").style.backgroundColor = "#d46e6e";
        document.getElementById("FormNursing3").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormNursing3").style.backgroundColor = "#77cf9c";
        document.getElementById("FormNursing3").style.color = "#333333";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_1").checked == true)
      {
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3B_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3B_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3B_2").checked == false)
        {
          document.getElementById("FormNursing3B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormNursing3B").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormNursing3B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormNursing3B").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormNursing3B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormNursing3B").style.color = "#000000";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_2").checked == false)
      {
        document.getElementById("FormNursing4").style.backgroundColor = "#d46e6e";
        document.getElementById("FormNursing4").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormNursing4").style.backgroundColor = "#77cf9c";
        document.getElementById("FormNursing4").style.color = "#333333";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_2").checked == false)
      {
        document.getElementById("FormNursing5").style.backgroundColor = "#d46e6e";
        document.getElementById("FormNursing5").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormNursing5").style.backgroundColor = "#77cf9c";
        document.getElementById("FormNursing5").style.color = "#333333";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_1").checked == true)
      {
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_2").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_3").checked == false)
        {
          document.getElementById("FormNursing5B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormNursing5B").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormNursing5B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormNursing5B").style.color = "#333333";
        }

        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason"))
        {
          var CheckBoxList_Nursing5BReason = document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason");
          var CheckBoxList_Nursing5BReason_Count = CheckBoxList_Nursing5BReason.getElementsByTagName("input");

          var TotalItems = CheckBoxList_Nursing5BReason_Count.length;
          var Completed = "0";
          for (var a = 0; a < TotalItems; a++)
          {
            if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason_" + a + "").checked == true)
            {
              Completed = "1";
              document.getElementById("FormNursing5BReason").style.backgroundColor = "#77cf9c";
              document.getElementById("FormNursing5BReason").style.color = "#333333";
            }
            else if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason_" + a + "").checked == false && Completed == "0")
            {
              document.getElementById("FormNursing5BReason").style.backgroundColor = "#d46e6e";
              document.getElementById("FormNursing5BReason").style.color = "#333333";
            }
          }
        }
      }
      else
      {
        document.getElementById("FormNursing5B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormNursing5B").style.color = "#000000";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_2").checked == false)
      {
        document.getElementById("FormNursing6").style.backgroundColor = "#d46e6e";
        document.getElementById("FormNursing6").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormNursing6").style.backgroundColor = "#77cf9c";
        document.getElementById("FormNursing6").style.color = "#333333";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_1").checked == true)
      {
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6B_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6B_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6B_2").checked == false)
        {
          document.getElementById("FormNursing6B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormNursing6B").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormNursing6B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormNursing6B").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormNursing6B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormNursing6B").style.color = "#000000";
      }

      document.getElementById("FormPharmacy1").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy1").style.color = "#000000";
      document.getElementById("FormPharmacy1B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy1B").style.color = "#000000";
      document.getElementById("FormPharmacy2").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy2").style.color = "#000000";
      document.getElementById("FormPharmacy2B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy2B").style.color = "#000000";
      document.getElementById("FormPharmacy3").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy3").style.color = "#000000";
      document.getElementById("FormPharmacy3B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy3B").style.color = "#000000";
      document.getElementById("FormPharmacy4").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy4").style.color = "#000000";
      document.getElementById("FormPharmacy4B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy4B").style.color = "#000000";
      document.getElementById("FormPharmacy5").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy5").style.color = "#000000";
      document.getElementById("FormPharmacy5B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy5B").style.color = "#000000";
      document.getElementById("FormPharmacy6").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy6").style.color = "#000000";
      document.getElementById("FormPharmacy6B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy6B").style.color = "#000000";
      document.getElementById("FormPharmacy7").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy7").style.color = "#000000";
      document.getElementById("FormPharmacy7B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy7B").style.color = "#000000";
      document.getElementById("FormPharmacy8").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy8").style.color = "#000000";

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_0").checked == false ||
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_0").checked == false ||
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_0").checked == false ||
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_0").checked == false ||
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_0").checked == false ||
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_0").checked == false)
      {
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBox_" + FormMode + "NursingSelectAllYes").checked = false;
      }
      else
      {
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBox_" + FormMode + "NursingSelectAllYes").checked = true;
      }
    }
    else if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "ByList").value == "5389")
    {
      document.getElementById("FormNursing1").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing1").style.color = "#000000";
      document.getElementById("FormNursing1B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing1B").style.color = "#000000";
      document.getElementById("FormNursing2").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing2").style.color = "#000000";
      document.getElementById("FormNursing2B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing2B").style.color = "#000000";
      document.getElementById("FormNursing3").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing3").style.color = "#000000";
      document.getElementById("FormNursing3B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing3B").style.color = "#000000";
      document.getElementById("FormNursing4").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing4").style.color = "#000000";
      document.getElementById("FormNursing5").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing5").style.color = "#000000";
      document.getElementById("FormNursing5B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing5B").style.color = "#000000";
      document.getElementById("Nursing5BReason").style.backgroundColor = "#f7f7f7";
      document.getElementById("Nursing5BReason").style.color = "#000000";
      document.getElementById("FormNursing6").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing6").style.color = "#000000";
      document.getElementById("FormNursing6B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing6B").style.color = "#000000";


      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1_1").checked == true)
      {
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1B_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1B_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1B_2").checked == false)
        {
          document.getElementById("FormPharmacy1B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacy1B").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacy1B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacy1B").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormPharmacy1B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacy1B").style.color = "#000000";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_2").checked == false)
      {
        document.getElementById("FormPharmacy2").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPharmacy2").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPharmacy2").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPharmacy2").style.color = "#333333";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_1").checked == true)
      {
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2B_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2B_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2B_2").checked == false)
        {
          document.getElementById("FormPharmacy2B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacy2B").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacy2B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacy2B").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormPharmacy2B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacy2B").style.color = "#000000";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3_1").checked == false)
      {
        document.getElementById("FormPharmacy3").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPharmacy3").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPharmacy3").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPharmacy3").style.color = "#333333";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3_1").checked == true)
      {
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3B_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3B_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3B_2").checked == false)
        {
          document.getElementById("FormPharmacy3B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacy3B").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacy3B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacy3B").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormPharmacy3B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacy3B").style.color = "#000000";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4_1").checked == false)
      {
        document.getElementById("FormPharmacy4").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPharmacy4").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPharmacy4").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPharmacy4").style.color = "#333333";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4_1").checked == true)
      {
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4B_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4B_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4B_2").checked == false)
        {
          document.getElementById("FormPharmacy4B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacy4B").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacy4B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacy4B").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormPharmacy4B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacy4B").style.color = "#000000";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5_1").checked == false)
      {
        document.getElementById("FormPharmacy5").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPharmacy5").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPharmacy5").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPharmacy5").style.color = "#333333";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5_1").checked == true)
      {
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5B_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5B_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5B_2").checked == false)
        {
          document.getElementById("FormPharmacy5B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacy5B").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacy5B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacy5B").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormPharmacy5B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacy5B").style.color = "#000000";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_2").checked == false)
      {
        document.getElementById("FormPharmacy6").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPharmacy6").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPharmacy6").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPharmacy6").style.color = "#333333";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_1").checked == true)
      {
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6B_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6B_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6B_2").checked == false)
        {
          document.getElementById("FormPharmacy6B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacy6B").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacy6B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacy6B").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormPharmacy6B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacy6B").style.color = "#000000";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7_1").checked == true)
      {
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7B_0").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7B_1").checked == false && document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7B_2").checked == false)
        {
          document.getElementById("FormPharmacy7B").style.backgroundColor = "#d46e6e";
          document.getElementById("FormPharmacy7B").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormPharmacy7B").style.backgroundColor = "#77cf9c";
          document.getElementById("FormPharmacy7B").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormPharmacy7B").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPharmacy7B").style.color = "#000000";
      }

      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1_0").checked == false ||
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_0").checked == false ||
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3_0").checked == false ||
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4_0").checked == false ||
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5_0").checked == false ||
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_0").checked == false ||
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7_0").checked == false ||
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy8_0").checked == false)
      {
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBox_" + FormMode + "PharmacySelectAllYes").checked = false;
      }
      else
      {
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBox_" + FormMode + "PharmacySelectAllYes").checked = true;
      }
    }
    else
    {
      document.getElementById("FormNursing1").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing1").style.color = "#000000";
      document.getElementById("FormNursing1B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing1B").style.color = "#000000";
      document.getElementById("FormNursing2").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing2").style.color = "#000000";
      document.getElementById("FormNursing2B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing2B").style.color = "#000000";
      document.getElementById("FormNursing3").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing3").style.color = "#000000";
      document.getElementById("FormNursing3B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing3B").style.color = "#000000";
      document.getElementById("FormNursing4").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing4").style.color = "#000000";
      document.getElementById("FormNursing5").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing5").style.color = "#000000";
      document.getElementById("FormNursing5B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing5B").style.color = "#000000";
      document.getElementById("Nursing5BReason").style.backgroundColor = "#f7f7f7";
      document.getElementById("Nursing5BReason").style.color = "#000000";
      document.getElementById("FormNursing6").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing6").style.color = "#000000";
      document.getElementById("FormNursing6B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormNursing6B").style.color = "#000000";

      document.getElementById("FormPharmacy1").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy1").style.color = "#000000";
      document.getElementById("FormPharmacy1B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy1B").style.color = "#000000";
      document.getElementById("FormPharmacy2").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy2").style.color = "#000000";
      document.getElementById("FormPharmacy2B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy2B").style.color = "#000000";
      document.getElementById("FormPharmacy3").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy3").style.color = "#000000";
      document.getElementById("FormPharmacy3B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy3B").style.color = "#000000";
      document.getElementById("FormPharmacy4").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy4").style.color = "#000000";
      document.getElementById("FormPharmacy4B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy4B").style.color = "#000000";
      document.getElementById("FormPharmacy5").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy5").style.color = "#000000";
      document.getElementById("FormPharmacy5B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy5B").style.color = "#000000";
      document.getElementById("FormPharmacy6").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy6").style.color = "#000000";
      document.getElementById("FormPharmacy6B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy6B").style.color = "#000000";
      document.getElementById("FormPharmacy7").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy7").style.color = "#000000";
      document.getElementById("FormPharmacy7B").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy7B").style.color = "#000000";
      document.getElementById("FormPharmacy8").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPharmacy8").style.color = "#000000";
    }

    //var TotalRecords = document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "TotalRecords").value
    //if (TotalRecords > 0)
    //{
    //  var AntibioticValid = "No";
    //  for (var a = 0; a < TotalRecords; a++)
    //  {
    //    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_GridView_" + FormMode + "AntimicrobialStewardshipIntervention_Antibiotic_CheckBox_" + FormMode + "Antibiotic_" + a).checked == true)
    //    {
    //      AntibioticValid = "Yes";
    //    }
    //  }

    //  if (AntibioticValid == "No")
    //  {
    //    document.getElementById("FormAntibioticsPrescribed").style.backgroundColor = "#d46e6e";
    //    document.getElementById("FormAntibioticsPrescribed").style.color = "#333333";
    //  }
    //  else
    //  {
    //    document.getElementById("FormAntibioticsPrescribed").style.backgroundColor = "#77cf9c";
    //    document.getElementById("FormAntibioticsPrescribed").style.color = "#333333";
    //  }
    //}
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_Form------------------------------------------------------------------------------------------------------------------------------
function Calculation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    Calculation_Nursing(FormMode);
    Calculation_Pharmacy(FormMode);
  }
}

function Calculation_Nursing(FormMode)
{
  if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "ByList").value == "5389")
  {
    document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_TextBox_" + FormMode + "NursingScore").value = "";
  }
  else if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "ByList").value == "5390")
  {
    var Nursing_Total = 0;
    var Nursing_Selected = 0;

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_0").checked == true)
    {
      Nursing_Total = Nursing_Total + 1;
      Nursing_Selected = Nursing_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_1").checked == true)
    {
      Nursing_Selected = Nursing_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_0").checked == true)
    {
      Nursing_Total = Nursing_Total + 1;
      Nursing_Selected = Nursing_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_1").checked == true)
    {
      Nursing_Selected = Nursing_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_0").checked == true)
    {
      Nursing_Total = Nursing_Total + 1;
      Nursing_Selected = Nursing_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_1").checked == true)
    {
      Nursing_Selected = Nursing_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_0").checked == true)
    {
      Nursing_Total = Nursing_Total + 1;
      Nursing_Selected = Nursing_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_1").checked == true)
    {
      Nursing_Selected = Nursing_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_0").checked == true)
    {
      Nursing_Total = Nursing_Total + 1;
      Nursing_Selected = Nursing_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_1").checked == true)
    {
      Nursing_Selected = Nursing_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_0").checked == true)
    {
      Nursing_Total = Nursing_Total + 1;
      Nursing_Selected = Nursing_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_1").checked == true)
    {
      Nursing_Selected = Nursing_Selected + 1;
    }

    if (Nursing_Selected == 0)
    {
      var Nursing_Score = "N/A";
    }
    else
    {
      var Nursing_Score = ((Nursing_Total / Nursing_Selected) * 100);
      Nursing_Score = Nursing_Score.toFixed(0);
      Nursing_Score = Nursing_Score + " %";
    }

    document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_TextBox_" + FormMode + "NursingScore").value = Nursing_Total + " / " + Nursing_Selected + " (" + Nursing_Score + ")";
  }
}

function Calculation_Pharmacy(FormMode)
{
  if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "ByList").value == "5390")
  {
    document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_TextBox_" + FormMode + "PharmacyScore").value = "";
  }
  else if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "ByList").value == "5389")
  {
    var Pharmacy_Total = 0;
    var Pharmacy_Selected = 0;

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1_0").checked == true)
    {
      Pharmacy_Total = Pharmacy_Total + 1;
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1_1").checked == true)
    {
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_0").checked == true)
    {
      Pharmacy_Total = Pharmacy_Total + 1;
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_1").checked == true)
    {
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3_0").checked == true)
    {
      Pharmacy_Total = Pharmacy_Total + 1;
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3_1").checked == true)
    {
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4_0").checked == true)
    {
      Pharmacy_Total = Pharmacy_Total + 1;
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4_1").checked == true)
    {
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5_0").checked == true)
    {
      Pharmacy_Total = Pharmacy_Total + 1;
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5_1").checked == true)
    {
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_0").checked == true)
    {
      Pharmacy_Total = Pharmacy_Total + 1;
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_1").checked == true)
    {
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7_0").checked == true)
    {
      Pharmacy_Total = Pharmacy_Total + 1;
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7_1").checked == true)
    {
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy8_0").checked == true)
    {
      Pharmacy_Total = Pharmacy_Total + 1;
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy8_1").checked == true)
    {
      Pharmacy_Selected = Pharmacy_Selected + 1;
    }

    if (Pharmacy_Selected == 0)
    {
      var Pharmacy_Score = "N/A";
    }
    else
    {
      var Pharmacy_Score = ((Pharmacy_Total / Pharmacy_Selected) * 100);
      Pharmacy_Score = Pharmacy_Score.toFixed(0);
      Pharmacy_Score = Pharmacy_Score + " %";
    }

    document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_TextBox_" + FormMode + "PharmacyScore").value = Pharmacy_Total + " / " + Pharmacy_Selected + " (" + Pharmacy_Score + ")";
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form()
{
  var FormMode;
  if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_Edit"))
  {
    FormMode = "Edit"
  }
  else if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_Item"))
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
      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "ByList").value == "5389")
      {
        Hide("Nursing");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBox_" + FormMode + "NursingSelectAllYes").checked = false;
        Hide("NursingSelectAllYes");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_2").checked = false;
        Hide("Nursing1");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1B_2").checked = false;
        Hide("Nursing1B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_2").checked = false;
        Hide("Nursing2");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2B_2").checked = false;
        Hide("Nursing2B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_2").checked = false;
        Hide("Nursing3");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3B_2").checked = false;
        Hide("Nursing3B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_2").checked = false;
        Hide("Nursing4");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_2").checked = false;
        Hide("Nursing5");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_2").checked = false;
        Hide("Nursing5B");

        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason"))
        {
          var CheckBoxList_Nursing5BReason = document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason");
          var CheckBoxList_Nursing5BReason_Count = CheckBoxList_Nursing5BReason.getElementsByTagName("input");

          var TotalItems = CheckBoxList_Nursing5BReason_Count.length;
          var Completed = "0";
          for (var a = 0; a < TotalItems; a++)
          {
            document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason_" + a + "").checked = false;
          }
        }

        Hide("Nursing5BReason");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_2").checked = false;
        Hide("Nursing6");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6B_2").checked = false;
        Hide("Nursing6B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_TextBox_" + FormMode + "NursingScore").value = "";
        Hide("NursingScore");

        Show("Pharmacy");
        Show("PharmacySelectAllYes");
        Show("Pharmacy1");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1_1").checked == false)
        {
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1B_0").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1B_1").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1B_2").checked = false;
          Hide("Pharmacy1B");
        }
        else
        {
          Show("Pharmacy1B");
        }

        Show("Pharmacy2");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_1").checked == false)
        {
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2B_0").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2B_1").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2B_2").checked = false;
          Hide("Pharmacy2B");
        }
        else
        {
          Show("Pharmacy2B");
        }

        Show("Pharmacy3");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3_1").checked == false)
        {
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3B_0").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3B_1").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3B_2").checked = false;
          Hide("Pharmacy3B");
        }
        else
        {
          Show("Pharmacy3B");
        }

        Show("Pharmacy4");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4_1").checked == false)
        {
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4B_0").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4B_1").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4B_2").checked = false;
          Hide("Pharmacy4B");
        }
        else
        {
          Show("Pharmacy4B");
        }

        Show("Pharmacy5");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5_1").checked == false)
        {
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5B_0").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5B_1").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5B_2").checked = false;
          Hide("Pharmacy5B");
        }
        else
        {
          Show("Pharmacy5B");
        }

        Show("Pharmacy6");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_1").checked == false)
        {
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6B_0").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6B_1").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6B_2").checked = false;
          Hide("Pharmacy6B");
        }
        else
        {
          Show("Pharmacy6B");
        }

        Show("Pharmacy7");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7_1").checked == false)
        {
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7B_0").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7B_1").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7B_2").checked = false;
          Hide("Pharmacy7B");
        }
        else
        {
          Show("Pharmacy7B");
        }

        Show("Pharmacy8");
        Show("PharmacyScore");
      }
      else if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_DropDownList_" + FormMode + "ByList").value == "5390")
      {
        Show("Nursing");
        Show("NursingSelectAllYes");
        Show("Nursing1");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_1").checked == false)
        {
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1B_0").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1B_1").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1B_2").checked = false;
          Hide("Nursing1B");
        }
        else
        {
          Show("Nursing1B");
        }

        Show("Nursing2");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_1").checked == false)
        {
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2B_0").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2B_1").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2B_2").checked = false;
          Hide("Nursing2B");
        }
        else
        {
          Show("Nursing2B");
        }

        Show("Nursing3");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_1").checked == false)
        {
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3B_0").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3B_1").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3B_2").checked = false;
          Hide("Nursing3B");
        }
        else
        {
          Show("Nursing3B");
        }

        Show("Nursing4");

        Show("Nursing5");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_1").checked == false)
        {
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_0").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_1").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_2").checked = false;
          Hide("Nursing5B");

          if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason"))
          {
            var CheckBoxList_Nursing5BReason = document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason");
            var CheckBoxList_Nursing5BReason_Count = CheckBoxList_Nursing5BReason.getElementsByTagName("input");

            var TotalItems = CheckBoxList_Nursing5BReason_Count.length;
            var Completed = "0";
            for (var a = 0; a < TotalItems; a++)
            {
              document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason_" + a + "").checked = false;
            }
          }

          Hide("Nursing5BReason");
        }
        else
        {
          Show("Nursing5B");
          Show("Nursing5BReason");
        }

        Show("Nursing6");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_1").checked == false)
        {
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6B_0").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6B_1").checked = false;
          document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6B_2").checked = false;
          Hide("Nursing6B");
        }
        else
        {
          Show("Nursing6B");
        }
        Show("NursingScore");


        Hide("Pharmacy");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBox_" + FormMode + "PharmacySelectAllYes").checked = false;
        Hide("PharmacySelectAllYes");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1_2").checked = false;
        Hide("Pharmacy1");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1B_2").checked = false;
        Hide("Pharmacy1B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_2").checked = false;
        Hide("Pharmacy2");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2B_2").checked = false;
        Hide("Pharmacy2B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3_1").checked = false;
        Hide("Pharmacy3");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3B_2").checked = false;
        Hide("Pharmacy3B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4_1").checked = false;
        Hide("Pharmacy4");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4B_2").checked = false;
        Hide("Pharmacy4B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5_1").checked = false;
        Hide("Pharmacy5");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5B_2").checked = false;
        Hide("Pharmacy5B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_2").checked = false;
        Hide("Pharmacy6");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6B_2").checked = false;
        Hide("Pharmacy6B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7_2").checked = false;
        Hide("Pharmacy7");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7B_2").checked = false;
        Hide("Pharmacy7B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy8_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy8_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy8_2").checked = false;
        Hide("Pharmacy8");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_TextBox_" + FormMode + "PharmacyScore").value = "";
        Hide("PharmacyScore");
      }
      else
      {
        Hide("Nursing");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBox_" + FormMode + "NursingSelectAllYes").checked = false;
        Hide("NursingSelectAllYes");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1_2").checked = false;
        Hide("Nursing1");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing1B_2").checked = false;
        Hide("Nursing1B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2_2").checked = false;
        Hide("Nursing2");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing2B_2").checked = false;
        Hide("Nursing2B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3_2").checked = false;
        Hide("Nursing3");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing3B_2").checked = false;
        Hide("Nursing3B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing4_2").checked = false;
        Hide("Nursing4");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5_2").checked = false;
        Hide("Nursing5");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing5B_2").checked = false;
        Hide("Nursing5B");

        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason"))
        {
          var CheckBoxList_Nursing5BReason = document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason");
          var CheckBoxList_Nursing5BReason_Count = CheckBoxList_Nursing5BReason.getElementsByTagName("input");

          var TotalItems = CheckBoxList_Nursing5BReason_Count.length;
          var Completed = "0";
          for (var a = 0; a < TotalItems; a++)
          {
            document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBoxList_" + FormMode + "Nursing5BReason_" + a + "").checked = false;
          }
        }

        Hide("Nursing5BReason");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6_2").checked = false;
        Hide("Nursing6");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Nursing6B_2").checked = false;
        Hide("Nursing6B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_TextBox_" + FormMode + "NursingScore").value = "";
        Hide("NursingScore");

        Hide("Pharmacy");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_CheckBox_" + FormMode + "PharmacySelectAllYes").checked = false;
        Hide("PharmacySelectAllYes");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1_2").checked = false;
        Hide("Pharmacy1");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy1B_2").checked = false;
        Hide("Pharmacy1B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2_2").checked = false;
        Hide("Pharmacy2");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy2B_2").checked = false;
        Hide("Pharmacy2B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3_1").checked = false;
        Hide("Pharmacy3");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy3B_2").checked = false;
        Hide("Pharmacy3B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4_1").checked = false;
        Hide("Pharmacy4");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy4B_2").checked = false;
        Hide("Pharmacy4B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5_1").checked = false;
        Hide("Pharmacy5");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy5B_2").checked = false;
        Hide("Pharmacy5B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6_2").checked = false;
        Hide("Pharmacy6");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy6B_2").checked = false;
        Hide("Pharmacy6B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7_2").checked = false;
        Hide("Pharmacy7");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7B_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7B_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy7B_2").checked = false;
        Hide("Pharmacy7B");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy8_0").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy8_1").checked = false;
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_RadioButtonList_" + FormMode + "Pharmacy8_2").checked = false;
        Hide("Pharmacy8");
        document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_TextBox_" + FormMode + "PharmacyScore").value = "";
        Hide("PharmacyScore");
      }
    }

    if (FormMode == "Item")
    {
      if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "ByList").value == "5389")
      {
        Hide("Nursing");
        Hide("NursingSelectAllYes");
        Hide("Nursing1");
        Hide("Nursing1B");
        Hide("Nursing2");
        Hide("Nursing2B");
        Hide("Nursing3");
        Hide("Nursing3B");
        Hide("Nursing4");
        Hide("Nursing5");
        Hide("Nursing5B");
        Hide("Nursing5BReason");
        Hide("Nursing6");
        Hide("Nursing6B");
        Hide("NursingScore");

        Show("Pharmacy");
        Show("PharmacySelectAllYes");
        Show("Pharmacy1");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "Pharmacy1").value == "No")
        {
          Show("Pharmacy1B");
        }
        else
        {
          Hide("Pharmacy1B");
        }

        Show("Pharmacy2");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "Pharmacy2").value == "No")
        {
          Show("Pharmacy2B");
        }
        else
        {
          Hide("Pharmacy2B");
        }

        Show("Pharmacy3");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "Pharmacy3").value == "No")
        {
          Show("Pharmacy3B");
        }
        else
        {
          Hide("Pharmacy3B");
        }

        Show("Pharmacy4");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "Pharmacy4").value == "No (there is duplication)")
        {
          Show("Pharmacy4B");
        }
        else
        {
          Hide("Pharmacy4B");
        }

        Show("Pharmacy5");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "Pharmacy5").value == "No")
        {
          Show("Pharmacy5B");
        }
        else
        {
          Hide("Pharmacy5B");
        }

        Show("Pharmacy6");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "Pharmacy6").value == "No")
        {
          Show("Pharmacy6B");
        }
        else
        {
          Hide("Pharmacy6B");
        }

        Show("Pharmacy7");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "Pharmacy7").value == "No")
        {
          Show("Pharmacy7B");
        }
        else
        {
          Hide("Pharmacy7B");
        }

        Show("Pharmacy8");
        Show("PharmacyScore");
      }
      else if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "ByList").value == "5390")
      {
        Show("Nursing");
        Show("NursingSelectAllYes");
        Show("Nursing1");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "Nursing1").value == "No")
        {
          Show("Nursing1B");
        }
        else
        {
          Hide("Nursing1B");
        }

        Show("Nursing2");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "Nursing2").value == "No")
        {
          Show("Nursing2B");
        }
        else
        {
          Hide("Nursing2B");
        }

        Show("Nursing3");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "Nursing3").value == "No")
        {
          Show("Nursing3B");
        }
        else
        {
          Hide("Nursing3B");
        }

        Show("Nursing4");
        Show("Nursing5");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "Nursing5").value == "No")
        {
          Show("Nursing5B");
          Show("Nursing5BReason");
        }
        else
        {
          Hide("Nursing5B");
          Hide("Nursing5BReason");
        }

        Show("Nursing6");
        if (document.getElementById("FormView_AntimicrobialStewardshipIntervention_Form_HiddenField_" + FormMode + "Nursing6").value == "No")
        {
          Show("Nursing6B");
        }
        else
        {
          Hide("Nursing6B");
        }
        Show("NursingScore");

        Hide("Pharmacy");
        Hide("PharmacySelectAllYes");
        Hide("Pharmacy1");
        Hide("Pharmacy1B");
        Hide("Pharmacy2");
        Hide("Pharmacy2B");
        Hide("Pharmacy3");
        Hide("Pharmacy3B");
        Hide("Pharmacy4");
        Hide("Pharmacy4B");
        Hide("Pharmacy5");
        Hide("Pharmacy5B");
        Hide("Pharmacy6");
        Hide("Pharmacy6B");
        Hide("Pharmacy7");
        Hide("Pharmacy7B");
        Hide("Pharmacy8");
        Hide("PharmacyScore");
      }
      else
      {
        Hide("Nursing");
        Hide("NursingSelectAllYes");
        Hide("Nursing1");
        Hide("Nursing1B");
        Hide("Nursing2");
        Hide("Nursing2B");
        Hide("Nursing3");
        Hide("Nursing3B");
        Hide("Nursing4");
        Hide("Nursing5");
        Hide("Nursing5B");
        Hide("Nursing5BReason");
        Hide("Nursing6");
        Hide("Nursing6B");
        Hide("NursingScore");

        Hide("Pharmacy");
        Hide("PharmacySelectAllYes");
        Hide("Pharmacy1");
        Hide("Pharmacy1B");
        Hide("Pharmacy2");
        Hide("Pharmacy2B");
        Hide("Pharmacy3");
        Hide("Pharmacy3B");
        Hide("Pharmacy4");
        Hide("Pharmacy4B");
        Hide("Pharmacy5");
        Hide("Pharmacy5B");
        Hide("Pharmacy6");
        Hide("Pharmacy6B");
        Hide("Pharmacy7");
        Hide("Pharmacy7B");
        Hide("Pharmacy8");
        Hide("PharmacyScore");
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