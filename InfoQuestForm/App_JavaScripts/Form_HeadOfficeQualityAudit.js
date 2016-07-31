
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
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "Facility"))
    {
      if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "Facility").value == "")
      {
        document.getElementById("FormFacility").style.backgroundColor = "#d46e6e";
        document.getElementById("FormFacility").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormFacility").style.backgroundColor = "#77cf9c";
        document.getElementById("FormFacility").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "FunctionList"))
    {
      if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "FunctionList").value == "")
      {
        document.getElementById("FormFunctionList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormFunctionList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormFunctionList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormFunctionList").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "FindingDate"))
    {
      if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "FindingDate").value == "" || document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "FindingDate").value == "yyyy/mm/dd")
      {
        document.getElementById("FormFindingDate").style.backgroundColor = "#d46e6e";
        document.getElementById("FormFindingDate").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormFindingDate").style.backgroundColor = "#77cf9c";
        document.getElementById("FormFindingDate").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "Auditor"))
    {
      if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "Auditor").value == "")
      {
        document.getElementById("FormAuditor").style.backgroundColor = "#d46e6e";
        document.getElementById("FormAuditor").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormAuditor").style.backgroundColor = "#77cf9c";
        document.getElementById("FormAuditor").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "CriteriaList"))
    {
      if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "CriteriaList").value == "")
      {
        document.getElementById("FormCriteriaList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCriteriaList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormCriteriaList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCriteriaList").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "SubCriteriaList"))
    {
      if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "SubCriteriaList").length == 1)
      {
        document.getElementById("FormSubCriteriaList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSubCriteriaList").style.color = "#000000";
      }
      else
      {
        if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "SubCriteriaList").value == "")
        {
          document.getElementById("FormSubCriteriaList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSubCriteriaList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSubCriteriaList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSubCriteriaList").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "ClassificationList"))
    {
      if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "ClassificationList").value == "")
      {
        document.getElementById("FormClassificationList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormClassificationList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormClassificationList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormClassificationList").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "Description"))
    {
      if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "Description").value == "")
      {
        document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
        document.getElementById("FormDescription").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
        document.getElementById("FormDescription").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "TrackingList"))
    {
      if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "TrackingList").value == "5399")
      {
        if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "ImmediateAction").value == "")
        {
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormImmediateAction").style.backgroundColor = "#d46e6e";
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormImmediateAction").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormImmediateAction").style.backgroundColor = "#77cf9c";
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormImmediateAction").style.color = "#333333";
        }

        if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "RootCause").value == "")
        {
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormRootCause").style.backgroundColor = "#d46e6e";
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormRootCause").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormRootCause").style.backgroundColor = "#77cf9c";
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormRootCause").style.color = "#333333";
        }

        if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "CorrectiveAction").value == "")
        {
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormCorrectiveAction").style.backgroundColor = "#d46e6e";
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormCorrectiveAction").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormCorrectiveAction").style.backgroundColor = "#77cf9c";
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormCorrectiveAction").style.color = "#333333";
        }

        if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "Evaluation").value == "")
        {
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormEvaluation").style.backgroundColor = "#d46e6e";
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormEvaluation").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormEvaluation").style.backgroundColor = "#77cf9c";
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormEvaluation").style.color = "#333333";
        }

        if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "LateCloseOutList"))
        {
          if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "LateCloseOutList").value == "")
          {
            document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormLateCloseOutList").style.backgroundColor = "#d46e6e";
            document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormLateCloseOutList").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormLateCloseOutList").style.backgroundColor = "#77cf9c";
            document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormLateCloseOutList").style.color = "#333333";
          }

          if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "LateCloseOutList").value == "5406")
          {
            if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "LateCloseOutListOther").value == "")
            {
              document.getElementById("FormLateCloseOutListOther").style.backgroundColor = "#d46e6e";
              document.getElementById("FormLateCloseOutListOther").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormLateCloseOutListOther").style.backgroundColor = "#77cf9c";
              document.getElementById("FormLateCloseOutListOther").style.color = "#333333";
            }
          }
        }
      }
      else
      {
        document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormImmediateAction").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormImmediateAction").style.color = "#000000";
        document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormRootCause").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormRootCause").style.color = "#000000";
        document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormCorrectiveAction").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormCorrectiveAction").style.color = "#000000";
        document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormEvaluation").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormEvaluation").style.color = "#000000";

        if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "LateCloseOutList"))
        {
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormLateCloseOutList").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_FormLateCloseOutList").style.color = "#000000";
        }

        if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "LateCloseOutListOther"))
        {
          document.getElementById("FormLateCloseOutListOther").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormLateCloseOutListOther").style.color = "#000000";
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
  if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_HiddenField_Item"))
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
      if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "LateCloseOutList"))
      {
        if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_DropDownList_" + FormMode + "LateCloseOutList").value == "5406")
        {
          Show("LateCloseOutListOther");
        }
        else
        {
          Hide("LateCloseOutListOther");
          document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "LateCloseOutListOther").value = "";
        }
      }
      else
      {
        Hide("LateCloseOutListOther");
        document.getElementById("FormView_HeadOfficeQualityAudit_Form_TextBox_" + FormMode + "LateCloseOutListOther").value = "";
      }
    }

    if (FormMode == "Item")
    {
      if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_HiddenField_" + FormMode + "LateCloseOutList").value == "")
      {
        Hide("LateCloseOutList");
        Hide("LateCloseOutListOther");
      }
      else
      {
        Show("LateCloseOutList");
        if (document.getElementById("FormView_HeadOfficeQualityAudit_Form_HiddenField_" + FormMode + "LateCloseOutList").value == "5406")
        {
          Show("LateCloseOutListOther");
        }
        else
        {
          Hide("LateCloseOutListOther");
        }
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
