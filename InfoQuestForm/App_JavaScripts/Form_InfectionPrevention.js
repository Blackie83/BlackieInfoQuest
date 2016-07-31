
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
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form(Control)
{
  var FormMode;
  if (document.getElementById("FormView_InfectionPrevention_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_InfectionPrevention_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "Facility"))
    {
      if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "Facility").value == "")
      {
        document.getElementById("FormFacility").style.backgroundColor = "#d46e6e";
        document.getElementById("FormFacility").style.color = "#333333";
      } else
      {
        document.getElementById("FormFacility").style.backgroundColor = "#77cf9c";
        document.getElementById("FormFacility").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_InfectionPrevention_Form_TextBox_" + FormMode + "PatientVisitNumber"))
    {
      if (document.getElementById("FormView_InfectionPrevention_Form_TextBox_" + FormMode + "PatientVisitNumber").value == "")
      {
        document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPatientVisitNumber").style.color = "#333333";
      } else
      {
        document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPatientVisitNumber").style.color = "#333333";
      }
    }

    if ((QueryStringValue("s_FacilityId") != null) && (QueryStringValue("s_PatientVisitNumber") != null))
    {
      if (document.getElementById("FormView_InfectionPrevention_Form_HiddenField_" + FormMode + "ValidIMedsData").value == "Yes")
      {
        if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "UnitId").value == "")
        {
          document.getElementById("FormUnitId").style.backgroundColor = "#d46e6e";
          document.getElementById("FormUnitId").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormUnitId").style.backgroundColor = "#77cf9c";
          document.getElementById("FormUnitId").style.color = "#333333";
        }

        if (document.getElementById("FormView_InfectionPrevention_Form_TextBox_" + FormMode + "DateInfectionReported").value == "" || document.getElementById("FormView_InfectionPrevention_Form_TextBox_" + FormMode + "DateInfectionReported").value == "yyyy/mm/dd")
        {
          document.getElementById("FormDateInfectionReported").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDateInfectionReported").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormDateInfectionReported").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDateInfectionReported").style.color = "#333333";
        }

        if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "")
        {
          document.getElementById("FormInfectionType").style.backgroundColor = "#d46e6e";
          document.getElementById("FormInfectionType").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormInfectionType").style.backgroundColor = "#77cf9c";
          document.getElementById("FormInfectionType").style.color = "#333333";
        }

        if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1131")
        {
          if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "SSISubType").value == "")
          {
            document.getElementById("FormSSISubType").style.backgroundColor = "#d46e6e";
            document.getElementById("FormSSISubType").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormSSISubType").style.backgroundColor = "#77cf9c";
            document.getElementById("FormSSISubType").style.color = "#333333";
          }
        }
        else
        {
          document.getElementById("FormSSISubType").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormSSISubType").style.color = "#000000";
        }

        if (document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Severity_0").checked == false && document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Severity_1").checked == false)
        {
          document.getElementById("FormSeverity").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSeverity").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSeverity").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSeverity").style.color = "#333333";
        }

        if (document.getElementById("FormView_InfectionPrevention_Form_TextBox_" + FormMode + "Description").value == "")
        {
          document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
          document.getElementById("FormDescription").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
          document.getElementById("FormDescription").style.color = "#333333";
        }

        if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1131" || document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1135" || document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1137" || document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1139")
        {
          if (document.getElementById("FormView_InfectionPrevention_Form_TextBox_" + FormMode + "Days").value == "")
          {
            document.getElementById("FormDays").style.backgroundColor = "#d46e6e";
            document.getElementById("FormDays").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormDays").style.backgroundColor = "#77cf9c";
            document.getElementById("FormDays").style.color = "#333333";
          }
        }
        else
        {
          document.getElementById("FormSSISubType").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormSSISubType").style.color = "#000000";
        }

        if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1131")
        {
          if (Control == undefined)
          {
            if (document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI1").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI2").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI3").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI4").checked == true)
            {
              document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_0").checked = true;
            }
            else
            {
              document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_1").checked = true;
            }
          }
          else
          {
            if (Control == "Compliance")
            {
              if (document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_0").checked == true)
              {
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI1").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI2").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI3").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI4").checked = true;
              }
              else if (document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_1").checked == true)
              {
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI1").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI2").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI3").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI4").checked = false;
              }
            }
          }
        }

        if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1135")
        {
          if (Control == undefined)
          {
            if (document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP1").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP2").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP3").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP4").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP5").checked == true)
            {
              document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_0").checked = true;
            }
            else
            {
              document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_1").checked = true;
            }
          }
          else
          {
            if (Control == "Compliance")
            {
              if (document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_0").checked == true)
              {
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP1").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP2").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP3").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP4").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP5").checked = true;
              }
              else if (document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_1").checked == true)
              {
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP1").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP2").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP3").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP4").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP5").checked = false;
              }
            }
          }
        }

        if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1137")
        {
          if (Control == undefined)
          {
            if (document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI1").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI2").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI3").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI4").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI5").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI6").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI7").checked == true)
            {
              document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_0").checked = true;
            }
            else
            {
              document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_1").checked = true;
            }
          }
          else
          {
            if (Control == "Compliance")
            {
              if (document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_0").checked == true)
              {
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI1").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI2").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI3").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI4").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI5").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI6").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI7").checked = true;
              }
              else if (document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_1").checked == true)
              {
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI1").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI2").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI3").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI4").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI5").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI6").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI7").checked = false;
              }
            }
          }
        }

        if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1139")
        {
          if (Control == undefined)
          {
            if (document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI1").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI2").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI3").checked == true && document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI4").checked == true)
            {
              document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_0").checked = true;
            }
            else
            {
              document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_1").checked = true;
            }
          }
          else
          {
            if (Control == "Compliance")
            {
              if (document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_0").checked == true)
              {
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI1").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI2").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI3").checked = true;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI4").checked = true;
              }
              else if (document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_1").checked == true)
              {
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI1").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI2").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI3").checked = false;
                document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI4").checked = false;
              }
            }
          }
        }

        if (document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "InvestigationCompleted").checked == true)
        {
          if (document.getElementById("FormView_InfectionPrevention_Form_TextBox_" + FormMode + "InvestigationDate").value == "")
          {
            document.getElementById("FormInvestigationDate").style.backgroundColor = "#d46e6e";
            document.getElementById("FormInvestigationDate").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormInvestigationDate").style.backgroundColor = "#77cf9c";
            document.getElementById("FormInvestigationDate").style.color = "#333333";
          }

          if (document.getElementById("FormView_InfectionPrevention_Form_TextBox_" + FormMode + "InvestigationName").value == "")
          {
            document.getElementById("FormInvestigationName").style.backgroundColor = "#d46e6e";
            document.getElementById("FormInvestigationName").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormInvestigationName").style.backgroundColor = "#77cf9c";
            document.getElementById("FormInvestigationName").style.color = "#333333";
          }

          if (document.getElementById("FormView_InfectionPrevention_Form_TextBox_" + FormMode + "InvestigationDesignation").value == "")
          {
            document.getElementById("FormInvestigationDesignation").style.backgroundColor = "#d46e6e";
            document.getElementById("FormInvestigationDesignation").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormInvestigationDesignation").style.backgroundColor = "#77cf9c";
            document.getElementById("FormInvestigationDesignation").style.color = "#333333";
          }

          if (document.getElementById("FormView_InfectionPrevention_Form_TextBox_" + FormMode + "InvestigationIPCSName").value == "")
          {
            document.getElementById("FormInvestigationIPCSName").style.backgroundColor = "#d46e6e";
            document.getElementById("FormInvestigationIPCSName").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormInvestigationIPCSName").style.backgroundColor = "#77cf9c";
            document.getElementById("FormInvestigationIPCSName").style.color = "#333333";
          }

          if (document.getElementById("FormView_InfectionPrevention_Form_TextBox_" + FormMode + "InvestigationTeamMembers").value == "")
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
          document.getElementById("FormInvestigationDate").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormInvestigationDate").style.color = "#000000";
          document.getElementById("FormInvestigationName").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormInvestigationName").style.color = "#000000";
          document.getElementById("FormInvestigationDesignation").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormInvestigationDesignation").style.color = "#000000";
          document.getElementById("FormInvestigationIPCSName").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormInvestigationIPCSName").style.color = "#000000";
          document.getElementById("FormInvestigationTeamMembers").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormInvestigationTeamMembers").style.color = "#000000";
        }
      }
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form()
{
  var FormMode;
  if (document.getElementById("FormView_InfectionPrevention_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_InfectionPrevention_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else if (document.getElementById("FormView_InfectionPrevention_Form_HiddenField_Item"))
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
      if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1131")
      {
        Show("ShowHideSSISubType");
      }
      else
      {
        Hide("ShowHideSSISubType");
      }

      if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1131" || document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1135" || document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1137" || document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1139")
      {
        Show("ShowHideDays");
        Show("ShowHideCompliance");
      }
      else
      {
        Hide("ShowHideDays");
        Hide("ShowHideCompliance");

        document.getElementById("FormView_InfectionPrevention_Form_TextBox_" + FormMode + "Days").value = "";
        document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_0").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_RadioButtonList_" + FormMode + "Compliance_1").checked = false;
      }

      if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1131")
      {
        Show("ShowHideTypeSSI1");
        Show("ShowHideTypeSSI2");
        Show("ShowHideTypeSSI3");
        Show("ShowHideTypeSSI4");
      }
      else
      {
        Hide("ShowHideTypeSSI1");
        Hide("ShowHideTypeSSI2");
        Hide("ShowHideTypeSSI3");
        Hide("ShowHideTypeSSI4");

        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI1").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI2").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI3").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeSSI4").checked = false;
      }
      
      if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1135")
      {
        Show("ShowHideTypeVAP1");
        Show("ShowHideTypeVAP2");
        Show("ShowHideTypeVAP3");
        Show("ShowHideTypeVAP4");
        Show("ShowHideTypeVAP5");
      }
      else
      {
        Hide("ShowHideTypeVAP1");
        Hide("ShowHideTypeVAP2");
        Hide("ShowHideTypeVAP3");
        Hide("ShowHideTypeVAP4");
        Hide("ShowHideTypeVAP5");

        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP1").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP2").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP3").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP4").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeVAP5").checked = false;
      }
      
      if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1137")
      {
        Show("ShowHideTypeCLABSI1");
        Show("ShowHideTypeCLABSI2");
        Show("ShowHideTypeCLABSI3");
        Show("ShowHideTypeCLABSI4");
        Show("ShowHideTypeCLABSI5");
        Show("ShowHideTypeCLABSI6");
        Show("ShowHideTypeCLABSI7");
      }
      else
      {
        Hide("ShowHideTypeCLABSI1");
        Hide("ShowHideTypeCLABSI2");
        Hide("ShowHideTypeCLABSI3");
        Hide("ShowHideTypeCLABSI4");
        Hide("ShowHideTypeCLABSI5");
        Hide("ShowHideTypeCLABSI6");
        Hide("ShowHideTypeCLABSI7");

        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI1").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI2").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI3").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI4").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI5").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI6").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCLABSI7").checked = false;
      }
      
      if (document.getElementById("FormView_InfectionPrevention_Form_DropDownList_" + FormMode + "InfectionType").value == "1139")
      {
        Show("ShowHideTypeCAUTI1");
        Show("ShowHideTypeCAUTI2");
        Show("ShowHideTypeCAUTI3");
        Show("ShowHideTypeCAUTI4");
      }
      else
      {
        Hide("ShowHideTypeCAUTI1");
        Hide("ShowHideTypeCAUTI2");
        Hide("ShowHideTypeCAUTI3");
        Hide("ShowHideTypeCAUTI4");

        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI1").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI2").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI3").checked = false;
        document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "TypeCAUTI4").checked = false;
      }

      if (document.getElementById("FormView_InfectionPrevention_Form_CheckBox_" + FormMode + "InvestigationCompleted").checked == true)
      {
        Show("ShowHideInvestigationCompletedDate");
      }
      else
      {
        Hide("ShowHideInvestigationCompletedDate");
      }
    }

    if (FormMode == "Item")
    {
      if (document.getElementById("FormView_InfectionPrevention_Form_HiddenField_" + FormMode + "InfectionType").value == "1131")
      {
        Show("ShowHideSSISubType");
      }
      else
      {
        Hide("ShowHideSSISubType");
      }

      if (document.getElementById("FormView_InfectionPrevention_Form_HiddenField_" + FormMode + "InfectionType").value == "1131" || document.getElementById("FormView_InfectionPrevention_Form_HiddenField_" + FormMode + "InfectionType").value == "1135" || document.getElementById("FormView_InfectionPrevention_Form_HiddenField_" + FormMode + "InfectionType").value == "1137" || document.getElementById("FormView_InfectionPrevention_Form_HiddenField_" + FormMode + "InfectionType").value == "1139")
      {
        Show("ShowHideDays");
        Show("ShowHideCompliance");
        Show("ShowHideBundleType");
      }
      else
      {
        Hide("ShowHideDays");
        Hide("ShowHideCompliance");
        Hide("ShowHideBundleType");
      }

      if (document.getElementById("FormView_InfectionPrevention_Form_HiddenField_" + FormMode + "InvestigationCompleted").value == "Yes")
      {
        Show("ShowHideInvestigationCompletedDate");
      }
      else
      {
        Hide("ShowHideInvestigationCompletedDate");
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