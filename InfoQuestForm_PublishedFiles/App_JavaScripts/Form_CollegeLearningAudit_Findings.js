
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
  if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_DropDownList_" + FormMode + "TrackingList").value == "6134")
    {
      if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_TextBox_" + FormMode + "RootCause").value == "") {
        document.getElementById("FormRootCause").style.backgroundColor = "#d46e6e";
        document.getElementById("FormRootCause").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormRootCause").style.backgroundColor = "#77cf9c";
        document.getElementById("FormRootCause").style.color = "#333333";
      }

      if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_TextBox_" + FormMode + "Actions").value == "")
      {
        document.getElementById("FormActions").style.backgroundColor = "#d46e6e";
        document.getElementById("FormActions").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormActions").style.backgroundColor = "#77cf9c";
        document.getElementById("FormActions").style.color = "#333333";
      }

      if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_TextBox_" + FormMode + "ResponsiblePerson").value == "")
      {
        document.getElementById("FormResponsiblePerson").style.backgroundColor = "#d46e6e";
        document.getElementById("FormResponsiblePerson").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormResponsiblePerson").style.backgroundColor = "#77cf9c";
        document.getElementById("FormResponsiblePerson").style.color = "#333333";
      }

      if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_TextBox_" + FormMode + "DueDate").value == "")
      {
        document.getElementById("FormDueDate").style.backgroundColor = "#d46e6e";
        document.getElementById("FormDueDate").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormDueDate").style.backgroundColor = "#77cf9c";
        document.getElementById("FormDueDate").style.color = "#333333";
      }

      if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_DropDownList_" + FormMode + "ActionsEffective").value == "")
      {
        document.getElementById("FormActionsEffective").style.backgroundColor = "#d46e6e";
        document.getElementById("FormActionsEffective").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormActionsEffective").style.backgroundColor = "#77cf9c";
        document.getElementById("FormActionsEffective").style.color = "#333333";
      }

      if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_DropDownList_" + FormMode + "LateClosingOutCNCList"))
      {
        if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_DropDownList_" + FormMode + "LateClosingOutCNCList").value == "")
        {
          document.getElementById("FormView_CollegeLearningAudit_Findings_Form_FormLateClosingOutCNCList").style.backgroundColor = "#d46e6e";
          document.getElementById("FormView_CollegeLearningAudit_Findings_Form_FormLateClosingOutCNCList").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormView_CollegeLearningAudit_Findings_Form_FormLateClosingOutCNCList").style.backgroundColor = "#77cf9c";
          document.getElementById("FormView_CollegeLearningAudit_Findings_Form_FormLateClosingOutCNCList").style.color = "#333333";
        }

        if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_DropDownList_" + FormMode + "LateClosingOutCNCList").value == "6140")
        {
          if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_TextBox_" + FormMode + "LateClosingOutCNCListOther").value == "")
          {
            document.getElementById("FormLateClosingOutCNCListOther").style.backgroundColor = "#d46e6e";
            document.getElementById("FormLateClosingOutCNCListOther").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormLateClosingOutCNCListOther").style.backgroundColor = "#77cf9c";
            document.getElementById("FormLateClosingOutCNCListOther").style.color = "#333333";
          }
        }
      }
    }
    else
    {
      document.getElementById("FormRootCause").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormRootCause").style.color = "#000000";

      document.getElementById("FormActions").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormActions").style.color = "#000000";

      document.getElementById("FormResponsiblePerson").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormResponsiblePerson").style.color = "#000000";

      document.getElementById("FormDueDate").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormDueDate").style.color = "#000000";

      document.getElementById("FormActionsEffective").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormActionsEffective").style.color = "#000000";

      if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_DropDownList_" + FormMode + "LateClosingOutCNCList"))
      {
        document.getElementById("FormView_CollegeLearningAudit_Findings_Form_FormLateClosingOutCNCList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormView_CollegeLearningAudit_Findings_Form_FormLateClosingOutCNCList").style.color = "#000000";
      }

      if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_TextBox_" + FormMode + "LateClosingOutCNCListOther"))
      {
        document.getElementById("FormLateClosingOutCNCListOther").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormLateClosingOutCNCListOther").style.color = "#000000";
      }
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form()
{
  var FormMode;
  if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  } else if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_HiddenField_Item"))
  {
    FormMode = "Item"
  } else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (FormMode != "Item")
    {
      if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_DropDownList_" + FormMode + "LateClosingOutCNCList"))
      {
        if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_DropDownList_" + FormMode + "LateClosingOutCNCList").value == "6140")
        {
          Show("LateClosingOutCNCListOther");
        } else
        {
          Hide("LateClosingOutCNCListOther");
          document.getElementById("FormView_CollegeLearningAudit_Findings_Form_TextBox_" + FormMode + "LateClosingOutCNCListOther").value = "";
        }
      } else
      {
        Hide("LateClosingOutCNCListOther");
        document.getElementById("FormView_CollegeLearningAudit_Findings_Form_TextBox_" + FormMode + "LateClosingOutCNCListOther").value = "";
      }
    }

    if (FormMode == "Item")
    {
      if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_HiddenField_" + FormMode + "LateClosingOutCNCList").value == "")
      {
        Hide("LateClosingOutCNCList");
        Hide("LateClosingOutCNCListOther");
      } else
      {
        Show("LateClosingOutCNCList");
        if (document.getElementById("FormView_CollegeLearningAudit_Findings_Form_HiddenField_" + FormMode + "LateClosingOutCNCList").value == "6140")
        {
          Show("LateClosingOutCNCListOther");
        } else
        {
          Hide("LateClosingOutCNCListOther");
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
