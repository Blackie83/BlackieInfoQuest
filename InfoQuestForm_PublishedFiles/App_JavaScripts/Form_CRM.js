
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
  if (document.getElementById("FormView_CRM_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_CRM_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "Facility"))
    {
      if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "Facility").value == "")
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

    if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "DateReceived").value == "" || document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "DateReceived").value == "yyyy/mm/dd")
    {
      document.getElementById("FormDateReceived").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDateReceived").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDateReceived").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDateReceived").style.color = "#333333";
    }

    if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "OriginatedAtList"))
    {
      if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "OriginatedAtList").value == "")
      {
        document.getElementById("FormOriginatedAtList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormOriginatedAtList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormOriginatedAtList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormOriginatedAtList").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "TypeList").value == "")
    {
      document.getElementById("FormTypeList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormTypeList").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormTypeList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormTypeList").style.color = "#333333";
    }

    if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedViaList").value == "")
    {
      document.getElementById("FormReceivedViaList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormReceivedViaList").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormReceivedViaList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormReceivedViaList").style.color = "#333333";
    }

    if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "")
    {
      document.getElementById("FormReceivedFromList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormReceivedFromList").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormReceivedFromList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormReceivedFromList").style.color = "#333333";
    }

    if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "4396" || document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "4415" || document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "4798" || document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "5387")
    {
      document.getElementById("FormCustomerName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCustomerName").style.color = "#000000";
      document.getElementById("FormCustomerEmail").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCustomerEmail").style.color = "#000000";
      document.getElementById("FormCustomerContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCustomerContactNumber").style.color = "#000000";
    }
    else
    {
      if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CustomerName").value == "")
      {
        document.getElementById("FormCustomerName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCustomerName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormCustomerName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCustomerName").style.color = "#333333";
      }

      if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CustomerEmail").value == "" && document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CustomerContactNumber").value == "")
      {
        document.getElementById("FormCustomerEmail").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCustomerEmail").style.color = "#333333";
        document.getElementById("FormCustomerContactNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCustomerContactNumber").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormCustomerEmail").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCustomerEmail").style.color = "#333333";
        document.getElementById("FormCustomerContactNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCustomerContactNumber").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "TypeList").value == "4395")
    {
      if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut"))
      {
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut").checked == true)
        {
          if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "4396")
          {
            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientVisitNumber").value == "")
            {
              document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#d46e6e";
              document.getElementById("FormPatientVisitNumber").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#77cf9c";
              document.getElementById("FormPatientVisitNumber").style.color = "#333333";
            }

            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientName").value == "")
            {
              document.getElementById("FormPatientName").style.backgroundColor = "#d46e6e";
              document.getElementById("FormPatientName").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormPatientName").style.backgroundColor = "#77cf9c";
              document.getElementById("FormPatientName").style.color = "#333333";
            }

            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientDateOfAdmission").value == "")
            {
              document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#d46e6e";
              document.getElementById("FormPatientDateOfAdmission").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#77cf9c";
              document.getElementById("FormPatientDateOfAdmission").style.color = "#333333";
            }

            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientEmail").value == "" && document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientContactNumber").value == "")
            {
              document.getElementById("FormPatientEmail").style.backgroundColor = "#d46e6e";
              document.getElementById("FormPatientEmail").style.color = "#333333";
              document.getElementById("FormPatientContactNumber").style.backgroundColor = "#d46e6e";
              document.getElementById("FormPatientContactNumber").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormPatientEmail").style.backgroundColor = "#77cf9c";
              document.getElementById("FormPatientEmail").style.color = "#333333";
              document.getElementById("FormPatientContactNumber").style.backgroundColor = "#77cf9c";
              document.getElementById("FormPatientContactNumber").style.color = "#333333";
            }
          }
          else
          {
            document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientVisitNumber").style.color = "#000000";
            document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientName").style.color = "#000000";
            document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
            document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientEmail").style.color = "#000000";
            document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientContactNumber").style.color = "#000000";
          }

          if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDescription").value == "")
          {
            document.getElementById("FormComplaintDescription").style.backgroundColor = "#d46e6e";
            document.getElementById("FormComplaintDescription").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormComplaintDescription").style.backgroundColor = "#77cf9c";
            document.getElementById("FormComplaintDescription").style.color = "#333333";
          }

          if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintUnitId").value == "")
          {
            document.getElementById("FormComplaintUnitId").style.backgroundColor = "#d46e6e";
            document.getElementById("FormComplaintUnitId").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormComplaintUnitId").style.backgroundColor = "#77cf9c";
            document.getElementById("FormComplaintUnitId").style.color = "#333333";
          }

          if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDateOccurred").value == "" || document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDateOccurred").value == "yyyy/mm/dd")
          {
            document.getElementById("FormComplaintDateOccurred").style.backgroundColor = "#d46e6e";
            document.getElementById("FormComplaintDateOccurred").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormComplaintDateOccurred").style.backgroundColor = "#77cf9c";
            document.getElementById("FormComplaintDateOccurred").style.color = "#333333";
          }

          if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintTimeOccuredHours").value == "" || document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintTimeOccuredMinutes").value == "")
          {
            document.getElementById("FormComplaintTimeOccured").style.backgroundColor = "#d46e6e";
            document.getElementById("FormComplaintTimeOccured").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormComplaintTimeOccured").style.backgroundColor = "#77cf9c";
            document.getElementById("FormComplaintTimeOccured").style.color = "#333333";
          }

          if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintPriorityList").value == "")
          {
            document.getElementById("FormComplaintPriorityList").style.backgroundColor = "#d46e6e";
            document.getElementById("FormComplaintPriorityList").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormComplaintPriorityList").style.backgroundColor = "#77cf9c";
            document.getElementById("FormComplaintPriorityList").style.color = "#333333";
          }

          var TotalItemsYes = parseInt(document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ComplaintCategoryItemListTotal").value);
          var CompletedYes = "0";
          for (var aYes = 0; aYes < TotalItemsYes; aYes++)
          {
            if (document.getElementById("FormView_CRM_Form_CheckBoxList_" + FormMode + "ComplaintCategoryItemList_" + aYes + "").checked == true)
            {
              CompletedYes = "1";
              document.getElementById("FormComplaintCategoryItemList").style.backgroundColor = "#77cf9c";
              document.getElementById("FormComplaintCategoryItemList").style.color = "#333333";
            }
            else if (document.getElementById("FormView_CRM_Form_CheckBoxList_" + FormMode + "ComplaintCategoryItemList_" + aYes + "").checked == false && CompletedYes == "0")
            {
              document.getElementById("FormComplaintCategoryItemList").style.backgroundColor = "#d46e6e";
              document.getElementById("FormComplaintCategoryItemList").style.color = "#333333";
            }
          }

          if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin24Hours").value == "")
          {
            document.getElementById("FormComplaintWithin24Hours").style.backgroundColor = "#d46e6e";
            document.getElementById("FormComplaintWithin24Hours").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormComplaintWithin24Hours").style.backgroundColor = "#77cf9c";
            document.getElementById("FormComplaintWithin24Hours").style.color = "#333333";
          }

          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "EscalatedForm").checked == true)
          {
            document.getElementById("FormComplaintWithin5Days").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormComplaintWithin5Days").style.color = "#000000";
            document.getElementById("FormComplaintWithin10Days").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormComplaintWithin10Days").style.color = "#000000";
            document.getElementById("FormComplaintWithin10DaysReason").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormComplaintWithin10DaysReason").style.color = "#000000";

            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin3Days").value == "")
            {
              document.getElementById("FormComplaintWithin3Days").style.backgroundColor = "#d46e6e";
              document.getElementById("FormComplaintWithin3Days").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormComplaintWithin3Days").style.backgroundColor = "#77cf9c";
              document.getElementById("FormComplaintWithin3Days").style.color = "#333333";

              if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin3Days").value == "No")
              {
                if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin3DaysReason").value == "")
                {
                  document.getElementById("FormComplaintWithin3DaysReason").style.backgroundColor = "#d46e6e";
                  document.getElementById("FormComplaintWithin3DaysReason").style.color = "#333333";
                }
                else
                {
                  document.getElementById("FormComplaintWithin3DaysReason").style.backgroundColor = "#77cf9c";
                  document.getElementById("FormComplaintWithin3DaysReason").style.color = "#333333";
                }
              }
              else
              {
                document.getElementById("FormComplaintWithin3DaysReason").style.backgroundColor = "#f7f7f7";
                document.getElementById("FormComplaintWithin3DaysReason").style.color = "#000000";
              }
            }
          }
          else
          {
            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin5Days").value == "")
            {
              document.getElementById("FormComplaintWithin5Days").style.backgroundColor = "#d46e6e";
              document.getElementById("FormComplaintWithin5Days").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormComplaintWithin5Days").style.backgroundColor = "#77cf9c";
              document.getElementById("FormComplaintWithin5Days").style.color = "#333333";
            }

            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin10Days").value == "")
            {
              document.getElementById("FormComplaintWithin10Days").style.backgroundColor = "#d46e6e";
              document.getElementById("FormComplaintWithin10Days").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormComplaintWithin10Days").style.backgroundColor = "#77cf9c";
              document.getElementById("FormComplaintWithin10Days").style.color = "#333333";

              if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin10Days").value == "No")
              {
                if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin10DaysReason").value == "")
                {
                  document.getElementById("FormComplaintWithin10DaysReason").style.backgroundColor = "#d46e6e";
                  document.getElementById("FormComplaintWithin10DaysReason").style.color = "#333333";
                }
                else
                {
                  document.getElementById("FormComplaintWithin10DaysReason").style.backgroundColor = "#77cf9c";
                  document.getElementById("FormComplaintWithin10DaysReason").style.color = "#333333";
                }
              }
              else
              {
                document.getElementById("FormComplaintWithin10DaysReason").style.backgroundColor = "#f7f7f7";
                document.getElementById("FormComplaintWithin10DaysReason").style.color = "#000000";
              }
            }

            document.getElementById("FormComplaintWithin3Days").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormComplaintWithin3Days").style.color = "#000000";
            document.getElementById("FormComplaintWithin3DaysReason").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormComplaintWithin3DaysReason").style.color = "#000000";
          }

          if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintCustomerSatisfied").value == "")
          {
            document.getElementById("FormComplaintCustomerSatisfied").style.backgroundColor = "#d46e6e";
            document.getElementById("FormComplaintCustomerSatisfied").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormComplaintCustomerSatisfied").style.backgroundColor = "#77cf9c";
            document.getElementById("FormComplaintCustomerSatisfied").style.color = "#333333";
          }

          if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintInvestigatorName").value == "")
          {
            document.getElementById("FormComplaintInvestigatorName").style.backgroundColor = "#d46e6e";
            document.getElementById("FormComplaintInvestigatorName").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormComplaintInvestigatorName").style.backgroundColor = "#77cf9c";
            document.getElementById("FormComplaintInvestigatorName").style.color = "#333333";
          }

          if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintInvestigatorDesignation").value == "")
          {
            document.getElementById("FormComplaintInvestigatorDesignation").style.backgroundColor = "#d46e6e";
            document.getElementById("FormComplaintInvestigatorDesignation").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormComplaintInvestigatorDesignation").style.backgroundColor = "#77cf9c";
            document.getElementById("FormComplaintInvestigatorDesignation").style.color = "#333333";
          }

          if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintRootCause").value == "")
          {
            document.getElementById("FormComplaintRootCause").style.backgroundColor = "#d46e6e";
            document.getElementById("FormComplaintRootCause").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormComplaintRootCause").style.backgroundColor = "#77cf9c";
            document.getElementById("FormComplaintRootCause").style.color = "#333333";
          }
        }
        else
        {
          document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientVisitNumber").style.color = "#000000";
          document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientName").style.color = "#000000";
          document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
          document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientEmail").style.color = "#000000";
          document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientContactNumber").style.color = "#000000";

          if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDescription").value == "")
          {
            document.getElementById("FormComplaintDescription").style.backgroundColor = "#d46e6e";
            document.getElementById("FormComplaintDescription").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormComplaintDescription").style.backgroundColor = "#77cf9c";
            document.getElementById("FormComplaintDescription").style.color = "#333333";
          }

          document.getElementById("FormComplaintUnitId").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintUnitId").style.color = "#000000";
          document.getElementById("FormComplaintDateOccurred").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintDateOccurred").style.color = "#000000";
          document.getElementById("FormComplaintTimeOccured").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintTimeOccured").style.color = "#000000";
          document.getElementById("FormComplaintPriorityList").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintPriorityList").style.color = "#000000";
          document.getElementById("FormComplaintCategoryItemList").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintCategoryItemList").style.color = "#000000";

          document.getElementById("FormComplaintWithin24Hours").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintWithin24Hours").style.color = "#000000";
          document.getElementById("FormComplaintWithin5Days").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintWithin5Days").style.color = "#000000";
          document.getElementById("FormComplaintWithin10Days").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintWithin10Days").style.color = "#000000";
          document.getElementById("FormComplaintWithin10DaysReason").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintWithin10DaysReason").style.color = "#000000";
          document.getElementById("FormComplaintWithin3Days").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintWithin3Days").style.color = "#000000";
          document.getElementById("FormComplaintWithin3DaysReason").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintWithin3DaysReason").style.color = "#000000";
          document.getElementById("FormComplaintCustomerSatisfied").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintCustomerSatisfied").style.color = "#000000";
          document.getElementById("FormComplaintInvestigatorName").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintInvestigatorName").style.color = "#000000";
          document.getElementById("FormComplaintInvestigatorDesignation").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintInvestigatorDesignation").style.color = "#000000";
          document.getElementById("FormComplaintRootCause").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplaintRootCause").style.color = "#000000";
        }
      }
      else
      {
        document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientVisitNumber").style.color = "#000000";
        document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientName").style.color = "#000000";
        document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
        document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientEmail").style.color = "#000000";
        document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientContactNumber").style.color = "#000000";

        if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDescription").value == "")
        {
          document.getElementById("FormComplaintDescription").style.backgroundColor = "#d46e6e";
          document.getElementById("FormComplaintDescription").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormComplaintDescription").style.backgroundColor = "#77cf9c";
          document.getElementById("FormComplaintDescription").style.color = "#333333";
        }

        document.getElementById("FormComplaintUnitId").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintUnitId").style.color = "#000000";
        document.getElementById("FormComplaintDateOccurred").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintDateOccurred").style.color = "#000000";
        document.getElementById("FormComplaintTimeOccured").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintTimeOccured").style.color = "#000000";
        document.getElementById("FormComplaintPriorityList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintPriorityList").style.color = "#000000";
        document.getElementById("FormComplaintCategoryItemList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintCategoryItemList").style.color = "#000000";

        document.getElementById("FormComplaintWithin24Hours").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintWithin24Hours").style.color = "#000000";
        document.getElementById("FormComplaintWithin5Days").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintWithin5Days").style.color = "#000000";
        document.getElementById("FormComplaintWithin10Days").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintWithin10Days").style.color = "#000000";
        document.getElementById("FormComplaintWithin10DaysReason").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintWithin10DaysReason").style.color = "#000000";
        document.getElementById("FormComplaintWithin3Days").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintWithin3Days").style.color = "#000000";
        document.getElementById("FormComplaintWithin3DaysReason").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintWithin3DaysReason").style.color = "#000000";
        document.getElementById("FormComplaintCustomerSatisfied").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintCustomerSatisfied").style.color = "#000000";
        document.getElementById("FormComplaintInvestigatorName").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintInvestigatorName").style.color = "#000000";
        document.getElementById("FormComplaintInvestigatorDesignation").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintInvestigatorDesignation").style.color = "#000000";
        document.getElementById("FormComplaintRootCause").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplaintRootCause").style.color = "#000000";
      }
    }
    else if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "TypeList").value == "4406")
    {
      if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge"))
      {
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge").checked == true)
        {
          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut").checked == true)
          {
            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "4396")
            {
              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientVisitNumber").value == "")
              {
                document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientVisitNumber").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientVisitNumber").style.color = "#333333";
              }

              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientName").value == "")
              {
                document.getElementById("FormPatientName").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientName").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientName").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientName").style.color = "#333333";
              }

              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientDateOfAdmission").value == "")
              {
                document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientDateOfAdmission").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientDateOfAdmission").style.color = "#333333";
              }

              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientEmail").value == "" && document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientContactNumber").value == "")
              {
                document.getElementById("FormPatientEmail").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientEmail").style.color = "#333333";
                document.getElementById("FormPatientContactNumber").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientContactNumber").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientEmail").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientEmail").style.color = "#333333";
                document.getElementById("FormPatientContactNumber").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientContactNumber").style.color = "#333333";
              }
            }
            else
            {
              document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientVisitNumber").style.color = "#000000";
              document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientName").style.color = "#000000";
              document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
              document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientEmail").style.color = "#000000";
              document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientContactNumber").style.color = "#000000";
            }

            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplimentDescription").value == "")
            {
              document.getElementById("FormComplimentDescription").style.backgroundColor = "#d46e6e";
              document.getElementById("FormComplimentDescription").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormComplimentDescription").style.backgroundColor = "#77cf9c";
              document.getElementById("FormComplimentDescription").style.color = "#333333";
            }

            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplimentUnitId").value == "")
            {
              document.getElementById("FormComplimentUnitId").style.backgroundColor = "#d46e6e";
              document.getElementById("FormComplimentUnitId").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormComplimentUnitId").style.backgroundColor = "#77cf9c";
              document.getElementById("FormComplimentUnitId").style.color = "#333333";
            }
          }
          else
          {
            document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientVisitNumber").style.color = "#000000";
            document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientName").style.color = "#000000";
            document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
            document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientEmail").style.color = "#000000";
            document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientContactNumber").style.color = "#000000";

            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplimentDescription").value == "")
            {
              document.getElementById("FormComplimentDescription").style.backgroundColor = "#d46e6e";
              document.getElementById("FormComplimentDescription").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormComplimentDescription").style.backgroundColor = "#77cf9c";
              document.getElementById("FormComplimentDescription").style.color = "#333333";
            }

            document.getElementById("FormComplimentUnitId").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormComplimentUnitId").style.color = "#000000";
          }
        }
        else
        {
          document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientVisitNumber").style.color = "#000000";
          document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientName").style.color = "#000000";
          document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
          document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientEmail").style.color = "#000000";
          document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientContactNumber").style.color = "#000000";

          if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplimentDescription").value == "")
          {
            document.getElementById("FormComplimentDescription").style.backgroundColor = "#d46e6e";
            document.getElementById("FormComplimentDescription").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormComplimentDescription").style.backgroundColor = "#77cf9c";
            document.getElementById("FormComplimentDescription").style.color = "#333333";
          }

          document.getElementById("FormComplimentUnitId").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormComplimentUnitId").style.color = "#000000";
        }
      }
      else
      {
        document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientVisitNumber").style.color = "#000000";
        document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientName").style.color = "#000000";
        document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
        document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientEmail").style.color = "#000000";
        document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientContactNumber").style.color = "#000000";

        if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplimentDescription").value == "")
        {
          document.getElementById("FormComplimentDescription").style.backgroundColor = "#d46e6e";
          document.getElementById("FormComplimentDescription").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormComplimentDescription").style.backgroundColor = "#77cf9c";
          document.getElementById("FormComplimentDescription").style.color = "#333333";
        }

        document.getElementById("FormComplimentUnitId").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormComplimentUnitId").style.color = "#000000";
      }
    }
    else if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "TypeList").value == "4412")
    {
      if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge"))
      {
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge").checked == true)
        {
          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut").checked == true)
          {
            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "4396")
            {
              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientVisitNumber").value == "")
              {
                document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientVisitNumber").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientVisitNumber").style.color = "#333333";
              }

              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientName").value == "")
              {
                document.getElementById("FormPatientName").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientName").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientName").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientName").style.color = "#333333";
              }

              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientDateOfAdmission").value == "")
              {
                document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientDateOfAdmission").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientDateOfAdmission").style.color = "#333333";
              }

              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientEmail").value == "" && document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientContactNumber").value == "")
              {
                document.getElementById("FormPatientEmail").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientEmail").style.color = "#333333";
                document.getElementById("FormPatientContactNumber").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientContactNumber").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientEmail").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientEmail").style.color = "#333333";
                document.getElementById("FormPatientContactNumber").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientContactNumber").style.color = "#333333";
              }
            }
            else
            {
              document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientVisitNumber").style.color = "#000000";
              document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientName").style.color = "#000000";
              document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
              document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientEmail").style.color = "#000000";
              document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientContactNumber").style.color = "#000000";
            }

            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CommentDescription").value == "")
            {
              document.getElementById("FormCommentDescription").style.backgroundColor = "#d46e6e";
              document.getElementById("FormCommentDescription").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormCommentDescription").style.backgroundColor = "#77cf9c";
              document.getElementById("FormCommentDescription").style.color = "#333333";
            }

            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentUnitId").value == "")
            {
              document.getElementById("FormCommentUnitId").style.backgroundColor = "#d46e6e";
              document.getElementById("FormCommentUnitId").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormCommentUnitId").style.backgroundColor = "#77cf9c";
              document.getElementById("FormCommentUnitId").style.color = "#333333";
            }

            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentTypeList").value == "")
            {
              document.getElementById("FormCommentTypeList").style.backgroundColor = "#d46e6e";
              document.getElementById("FormCommentTypeList").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormCommentTypeList").style.backgroundColor = "#77cf9c";
              document.getElementById("FormCommentTypeList").style.color = "#333333";
            }
          }
          else
          {
            document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientVisitNumber").style.color = "#000000";
            document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientName").style.color = "#000000";
            document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
            document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientEmail").style.color = "#000000";
            document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientContactNumber").style.color = "#000000";

            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CommentDescription").value == "")
            {
              document.getElementById("FormCommentDescription").style.backgroundColor = "#d46e6e";
              document.getElementById("FormCommentDescription").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormCommentDescription").style.backgroundColor = "#77cf9c";
              document.getElementById("FormCommentDescription").style.color = "#333333";
            }

            document.getElementById("FormCommentTypeList").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormCommentTypeList").style.color = "#000000";
            document.getElementById("FormCommentUnitId").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormCommentUnitId").style.color = "#000000";


          }
        }
        else
        {
          document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientVisitNumber").style.color = "#000000";
          document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientName").style.color = "#000000";
          document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
          document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientEmail").style.color = "#000000";
          document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientContactNumber").style.color = "#000000";

          if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CommentDescription").value == "")
          {
            document.getElementById("FormCommentDescription").style.backgroundColor = "#d46e6e";
            document.getElementById("FormCommentDescription").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormCommentDescription").style.backgroundColor = "#77cf9c";
            document.getElementById("FormCommentDescription").style.color = "#333333";
          }

          document.getElementById("FormCommentUnitId").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormCommentUnitId").style.color = "#000000";
          document.getElementById("FormCommentTypeList").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormCommentTypeList").style.color = "#000000";
        }
      }
      else
      {
        document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientVisitNumber").style.color = "#000000";
        document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientName").style.color = "#000000";
        document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
        document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientEmail").style.color = "#000000";
        document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientContactNumber").style.color = "#000000";

        if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CommentDescription").value == "")
        {
          document.getElementById("FormCommentDescription").style.backgroundColor = "#d46e6e";
          document.getElementById("FormCommentDescription").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormCommentDescription").style.backgroundColor = "#77cf9c";
          document.getElementById("FormCommentDescription").style.color = "#333333";
        }

        document.getElementById("FormCommentUnitId").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormCommentUnitId").style.color = "#000000";
        document.getElementById("FormCommentTypeList").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormCommentTypeList").style.color = "#000000";
      }
    }
    else if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "TypeList").value == "4413")
    {
      if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge"))
      {
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge").checked == true)
        {
          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut").checked == true)
          {
            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "4396")
            {
              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientVisitNumber").value == "")
              {
                document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientVisitNumber").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientVisitNumber").style.color = "#333333";
              }

              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientName").value == "")
              {
                document.getElementById("FormPatientName").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientName").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientName").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientName").style.color = "#333333";
              }

              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientDateOfAdmission").value == "")
              {
                document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientDateOfAdmission").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientDateOfAdmission").style.color = "#333333";
              }

              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientEmail").value == "" && document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientContactNumber").value == "")
              {
                document.getElementById("FormPatientEmail").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientEmail").style.color = "#333333";
                document.getElementById("FormPatientContactNumber").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientContactNumber").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientEmail").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientEmail").style.color = "#333333";
                document.getElementById("FormPatientContactNumber").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientContactNumber").style.color = "#333333";
              }
            }
            else
            {
              document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientVisitNumber").style.color = "#000000";
              document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientName").style.color = "#000000";
              document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
              document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientEmail").style.color = "#000000";
              document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientContactNumber").style.color = "#000000";
            }

            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "QueryDescription").value == "")
            {
              document.getElementById("FormQueryDescription").style.backgroundColor = "#d46e6e";
              document.getElementById("FormQueryDescription").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormQueryDescription").style.backgroundColor = "#77cf9c";
              document.getElementById("FormQueryDescription").style.color = "#333333";
            }

            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "QueryUnitId").value == "")
            {
              document.getElementById("FormQueryUnitId").style.backgroundColor = "#d46e6e";
              document.getElementById("FormQueryUnitId").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormQueryUnitId").style.backgroundColor = "#77cf9c";
              document.getElementById("FormQueryUnitId").style.color = "#333333";
            }
          }
          else
          {
            document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientVisitNumber").style.color = "#000000";
            document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientName").style.color = "#000000";
            document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
            document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientEmail").style.color = "#000000";
            document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientContactNumber").style.color = "#000000";

            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "QueryDescription").value == "")
            {
              document.getElementById("FormQueryDescription").style.backgroundColor = "#d46e6e";
              document.getElementById("FormQueryDescription").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormQueryDescription").style.backgroundColor = "#77cf9c";
              document.getElementById("FormQueryDescription").style.color = "#333333";
            }

            document.getElementById("FormQueryUnitId").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormQueryUnitId").style.color = "#000000";
          }
        }
        else
        {
          document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientVisitNumber").style.color = "#000000";
          document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientName").style.color = "#000000";
          document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
          document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientEmail").style.color = "#000000";
          document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientContactNumber").style.color = "#000000";

          if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "QueryDescription").value == "")
          {
            document.getElementById("FormQueryDescription").style.backgroundColor = "#d46e6e";
            document.getElementById("FormQueryDescription").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormQueryDescription").style.backgroundColor = "#77cf9c";
            document.getElementById("FormQueryDescription").style.color = "#333333";
          }

          document.getElementById("FormQueryUnitId").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormQueryUnitId").style.color = "#000000";
        }
      }
      else
      {
        document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientVisitNumber").style.color = "#000000";
        document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientName").style.color = "#000000";
        document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
        document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientEmail").style.color = "#000000";
        document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientContactNumber").style.color = "#000000";

        if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "QueryDescription").value == "")
        {
          document.getElementById("FormQueryDescription").style.backgroundColor = "#d46e6e";
          document.getElementById("FormQueryDescription").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormQueryDescription").style.backgroundColor = "#77cf9c";
          document.getElementById("FormQueryDescription").style.color = "#333333";
        }

        document.getElementById("FormQueryUnitId").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormQueryUnitId").style.color = "#000000";
      }
    }
    else if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "TypeList").value == "4414")
    {
      if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge"))
      {
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge").checked == true)
        {
          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut").checked == true)
          {
            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "4396")
            {
              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientVisitNumber").value == "")
              {
                document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientVisitNumber").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientVisitNumber").style.color = "#333333";
              }

              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientName").value == "")
              {
                document.getElementById("FormPatientName").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientName").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientName").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientName").style.color = "#333333";
              }

              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientDateOfAdmission").value == "")
              {
                document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientDateOfAdmission").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientDateOfAdmission").style.color = "#333333";
              }

              if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientEmail").value == "" && document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientContactNumber").value == "")
              {
                document.getElementById("FormPatientEmail").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientEmail").style.color = "#333333";
                document.getElementById("FormPatientContactNumber").style.backgroundColor = "#d46e6e";
                document.getElementById("FormPatientContactNumber").style.color = "#333333";
              }
              else
              {
                document.getElementById("FormPatientEmail").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientEmail").style.color = "#333333";
                document.getElementById("FormPatientContactNumber").style.backgroundColor = "#77cf9c";
                document.getElementById("FormPatientContactNumber").style.color = "#333333";
              }
            }
            else
            {
              document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientVisitNumber").style.color = "#000000";
              document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientName").style.color = "#000000";
              document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
              document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientEmail").style.color = "#000000";
              document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
              document.getElementById("FormPatientContactNumber").style.color = "#000000";
            }

            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "SuggestionDescription").value == "")
            {
              document.getElementById("FormSuggestionDescription").style.backgroundColor = "#d46e6e";
              document.getElementById("FormSuggestionDescription").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormSuggestionDescription").style.backgroundColor = "#77cf9c";
              document.getElementById("FormSuggestionDescription").style.color = "#333333";
            }

            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "SuggestionUnitId").value == "")
            {
              document.getElementById("FormSuggestionUnitId").style.backgroundColor = "#d46e6e";
              document.getElementById("FormSuggestionUnitId").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormSuggestionUnitId").style.backgroundColor = "#77cf9c";
              document.getElementById("FormSuggestionUnitId").style.color = "#333333";
            }
          }
          else
          {
            document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientVisitNumber").style.color = "#000000";
            document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientName").style.color = "#000000";
            document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
            document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientEmail").style.color = "#000000";
            document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormPatientContactNumber").style.color = "#000000";

            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "SuggestionDescription").value == "")
            {
              document.getElementById("FormSuggestionDescription").style.backgroundColor = "#d46e6e";
              document.getElementById("FormSuggestionDescription").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormSuggestionDescription").style.backgroundColor = "#77cf9c";
              document.getElementById("FormSuggestionDescription").style.color = "#333333";
            }

            document.getElementById("FormSuggestionUnitId").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormSuggestionUnitId").style.color = "#000000";
          }
        }
        else
        {
          document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientVisitNumber").style.color = "#000000";
          document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientName").style.color = "#000000";
          document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
          document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientEmail").style.color = "#000000";
          document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormPatientContactNumber").style.color = "#000000";

          if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "SuggestionDescription").value == "")
          {
            document.getElementById("FormSuggestionDescription").style.backgroundColor = "#d46e6e";
            document.getElementById("FormSuggestionDescription").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormSuggestionDescription").style.backgroundColor = "#77cf9c";
            document.getElementById("FormSuggestionDescription").style.color = "#333333";
          }

          document.getElementById("FormSuggestionUnitId").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormSuggestionUnitId").style.color = "#000000";
        }
      }
      else
      {
        document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientVisitNumber").style.color = "#000000";
        document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientName").style.color = "#000000";
        document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
        document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientEmail").style.color = "#000000";
        document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormPatientContactNumber").style.color = "#000000";

        if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "SuggestionDescription").value == "")
        {
          document.getElementById("FormSuggestionDescription").style.backgroundColor = "#d46e6e";
          document.getElementById("FormSuggestionDescription").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormSuggestionDescription").style.backgroundColor = "#77cf9c";
          document.getElementById("FormSuggestionDescription").style.color = "#333333";
        }

        document.getElementById("FormSuggestionUnitId").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormSuggestionUnitId").style.color = "#000000";
      }
    }
    else
    {
      document.getElementById("FormPatientVisitNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPatientVisitNumber").style.color = "#000000";
      document.getElementById("FormPatientName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPatientName").style.color = "#000000";
      document.getElementById("FormPatientDateOfAdmission").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPatientDateOfAdmission").style.color = "#000000";
      document.getElementById("FormPatientEmail").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPatientEmail").style.color = "#000000";
      document.getElementById("FormPatientContactNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormPatientContactNumber").style.color = "#000000";

      document.getElementById("FormComplaintDescription").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintDescription").style.color = "#000000";
      document.getElementById("FormComplaintUnitId").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintUnitId").style.color = "#000000";
      document.getElementById("FormComplaintDateOccurred").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintDateOccurred").style.color = "#000000";
      document.getElementById("FormComplaintTimeOccured").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintTimeOccured").style.color = "#000000";
      document.getElementById("FormComplaintPriorityList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintPriorityList").style.color = "#000000";
      document.getElementById("FormComplaintCategoryItemList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintCategoryItemList").style.color = "#000000";

      document.getElementById("FormComplaintWithin24Hours").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintWithin24Hours").style.color = "#000000";
      document.getElementById("FormComplaintWithin5Days").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintWithin5Days").style.color = "#000000";
      document.getElementById("FormComplaintWithin10Days").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintWithin10Days").style.color = "#000000";
      document.getElementById("FormComplaintWithin10DaysReason").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintWithin10DaysReason").style.color = "#000000";
      document.getElementById("FormComplaintWithin3Days").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintWithin3Days").style.color = "#000000";
      document.getElementById("FormComplaintWithin3DaysReason").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintWithin3DaysReason").style.color = "#000000";
      document.getElementById("FormComplaintCustomerSatisfied").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintCustomerSatisfied").style.color = "#000000";
      document.getElementById("FormComplaintInvestigatorName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintInvestigatorName").style.color = "#000000";
      document.getElementById("FormComplaintInvestigatorDesignation").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintInvestigatorDesignation").style.color = "#000000";
      document.getElementById("FormComplaintRootCause").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplaintRootCause").style.color = "#000000";

      document.getElementById("FormComplimentDescription").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplimentDescription").style.color = "#000000";
      document.getElementById("FormComplimentUnitId").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormComplimentUnitId").style.color = "#000000";

      document.getElementById("FormCommentDescription").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCommentDescription").style.color = "#000000";
      document.getElementById("FormCommentUnitId").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCommentUnitId").style.color = "#000000";
      document.getElementById("FormCommentTypeList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormCommentTypeList").style.color = "#000000";

      document.getElementById("FormQueryDescription").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormQueryDescription").style.color = "#000000";
      document.getElementById("FormQueryUnitId").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormQueryUnitId").style.color = "#000000";

      document.getElementById("FormSuggestionDescription").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSuggestionDescription").style.color = "#000000";
      document.getElementById("FormSuggestionUnitId").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSuggestionUnitId").style.color = "#000000";
    }

    if (FormMode == "Edit")
    {
      if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "Status").value == "Approved")
      {
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
        {
          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute").checked == true)
          {
            if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "RouteFacility").value == "")
            {
              document.getElementById("FormRouteFacility").style.backgroundColor = "#d46e6e";
              document.getElementById("FormRouteFacility").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormRouteFacility").style.backgroundColor = "#77cf9c";
              document.getElementById("FormRouteFacility").style.color = "#333333";
            }

            var TotalRadioButtonListItemsYes = parseInt(document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "RouteUnitTotal").value);
            var CompletedRadioButtonListYes = "0";
            for (var aYes = 0; aYes < TotalRadioButtonListItemsYes; aYes++)
            {
              if (document.getElementById("FormView_CRM_Form_RadioButtonList_" + FormMode + "RouteUnit_" + aYes + "").checked == true)
              {
                CompletedRadioButtonListYes = "1";
                document.getElementById("FormRouteUnit").style.backgroundColor = "#77cf9c";
                document.getElementById("FormRouteUnit").style.color = "#333333";
              }
              else if (document.getElementById("FormView_CRM_Form_RadioButtonList_" + FormMode + "RouteUnit_" + aYes + "").checked == false && CompletedRadioButtonListYes == "0")
              {
                document.getElementById("FormRouteUnit").style.backgroundColor = "#d46e6e";
                document.getElementById("FormRouteUnit").style.color = "#333333";
              }
            }

            if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "RouteComment").value == "")
            {
              document.getElementById("FormRouteComment").style.backgroundColor = "#d46e6e";
              document.getElementById("FormRouteComment").style.color = "#333333";
            }
            else
            {
              document.getElementById("FormRouteComment").style.backgroundColor = "#77cf9c";
              document.getElementById("FormRouteComment").style.color = "#333333";
            }
          }
          else
          {
            document.getElementById("FormRouteFacility").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormRouteFacility").style.color = "#000000";
            document.getElementById("FormRouteUnit").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormRouteUnit").style.color = "#000000";
            document.getElementById("FormRouteComment").style.backgroundColor = "#f7f7f7";
            document.getElementById("FormRouteComment").style.color = "#000000";
          }
        }
        else
        {
          if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "RouteComment").value == "")
          {
            document.getElementById("FormRouteComment").style.backgroundColor = "#d46e6e";
            document.getElementById("FormRouteComment").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormRouteComment").style.backgroundColor = "#77cf9c";
            document.getElementById("FormRouteComment").style.color = "#333333";
          }
        }
      }
      else
      {
        document.getElementById("FormRouteFacility").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormRouteFacility").style.color = "#000000";
        document.getElementById("FormRouteUnit").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormRouteUnit").style.color = "#000000";
        document.getElementById("FormRouteComment").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormRouteComment").style.color = "#000000";
      }
    }

    if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "Status"))
    {
      if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "Status").value == "Rejected")
      {
        if (document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "StatusRejectedReason").value == "")
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
  if (document.getElementById("FormView_CRM_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_CRM_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else if (document.getElementById("FormView_CRM_Form_HiddenField_Item"))
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
      if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "TypeList").value == "4395")
      {
        Show("Escalated1");
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "EscalatedForm").checked == true)
        {
          Show("Escalated2");
        }
        else
        {
          document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "EscalatedReportNumber").value = "";
          Hide("Escalated2");
        }

        Show("ComplaintDetail1");
        Show("ComplaintDetail2");
        Show("ComplaintDetail4");
        Show("ComplaintDetail5");
        Show("ComplaintDetail6");
        Show("ComplaintDetail7");
        Show("ComplaintDetail8");
        Show("ComplaintDetail9");
        Show("ComplaintDetail10");
        Show("ComplaintDetail11");
        Show("ComplaintDetail12");
        if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin24Hours").value == "No")
        {
          Show("ComplaintDetail13");
        }
        else
        {
          document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin24HoursReason").value = "";
          Hide("ComplaintDetail13");
        }

        Show("ComplaintDetail14");

        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "EscalatedForm").checked == true)
        {
          document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin5Days").value = "";
          document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin5DaysReason").value = "";
          document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin10Days").value = "";
          document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin10DaysReason").value = "";
          Hide("ComplaintDetail15");
          Hide("ComplaintDetail16");
          Hide("ComplaintDetail17");
          Hide("ComplaintDetail18");
          Show("ComplaintDetail19");
          if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin3Days").value == "No")
          {
            Show("ComplaintDetail20");
          }
          else
          {
            document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin3DaysReason").value = "";
            Hide("ComplaintDetail20");
          }
        }
        else
        {
          Show("ComplaintDetail15");
          if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin5Days").value == "No")
          {
            Show("ComplaintDetail16");
          }
          else
          {
            document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin5DaysReason").value = "";
            Hide("ComplaintDetail16");
          }

          Show("ComplaintDetail17");
          if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin10Days").value == "No")
          {
            Show("ComplaintDetail18");
          }
          else
          {
            document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin10DaysReason").value = "";
            Hide("ComplaintDetail18");
          }

          document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin3Days").value = "";
          document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin3DaysReason").value = "";
          Hide("ComplaintDetail19");
          Hide("ComplaintDetail20");
        }

        Show("ComplaintDetail21");
        if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintCustomerSatisfied").value == "No")
        {
          Show("ComplaintDetail22");
        }
        else
        {
          document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintCustomerSatisfiedReason").value = "";
          Hide("ComplaintDetail22");
        }

        Show("ComplaintDetail23");
        Show("ComplaintDetail24");
        Show("ComplaintDetail25");
        Show("ComplaintDetail26");
        Show("ComplaintDetail27");
        Show("ComplaintDetail28");
        Show("ComplaintDetail29");
        Show("ComplaintDetail30");

        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut"))
        {
          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut").checked == true)
          {
            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute").checked = false;
              Hide("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }
          else
          {
            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              Show("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }
        }

        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteComplete"))
        {
          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut"))
          {
            document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut").checked = false;
            Hide("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut");
          }
        }

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplimentDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplimentUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut").checked = false;
        }
        Hide("ComplimentDetail1");
        Hide("ComplimentDetail2");
        Hide("ComplimentDetail3");
        Hide("ComplimentDetail4");
        Hide("ComplimentDetail5");
        Hide("ComplimentDetail6");
        Hide("ComplimentDetail7");
        Hide("ComplimentDetail8");
        Hide("ComplimentDetail9");
        Hide("ComplimentDetail10");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CommentDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentUnitId").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentTypeList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentCategoryList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalUnitId").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalTypeList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalCategoryList").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut").checked = false;
        }
        Hide("CommentDetail1");
        Hide("CommentDetail2");
        Hide("CommentDetail3");
        Hide("CommentDetail4");
        Hide("CommentDetail5");
        Hide("CommentDetail6");
        Hide("CommentDetail7");
        Hide("CommentDetail8");
        Hide("CommentDetail9");
        Hide("CommentDetail10");
        Hide("CommentDetail11");
        if (document.getElementById("CommentDetail12"))
        {
          Hide("CommentDetail12");
        }
        Hide("CommentDetail14");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "QueryDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "QueryUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut").checked = false;
        }
        Hide("QueryDetail1");
        Hide("QueryDetail2");
        Hide("QueryDetail3");
        Hide("QueryDetail4");
        Hide("QueryDetail5");
        Hide("QueryDetail6");
        Hide("QueryDetail7");
        Hide("QueryDetail8");
        Hide("QueryDetail9");
        Hide("QueryDetail10");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "SuggestionDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "SuggestionUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut").checked = false;
        }
        Hide("SuggestionDetail1");
        Hide("SuggestionDetail2");
        Hide("SuggestionDetail3");
        Hide("SuggestionDetail4");
        Hide("SuggestionDetail5");
        Hide("SuggestionDetail6");
        Hide("SuggestionDetail7");
        Hide("SuggestionDetail8");
        Hide("SuggestionDetail9");
        Hide("SuggestionDetail10");
      }
      else if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "TypeList").value == "4406")
      {
        document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "EscalatedForm").checked = false;
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "EscalatedReportNumber").value = "";
        Hide("Escalated1");
        Hide("Escalated2");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintUnitId").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDateOccurred").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintTimeOccuredHours").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintTimeOccuredMinutes").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintPriorityList").value = "";
        var TotalItems4406 = parseInt(document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ComplaintCategoryItemListTotal").value);
        for (var a4406 = 0; a4406 < TotalItems4406; a4406++)
        {
          document.getElementById("FormView_CRM_Form_CheckBoxList_" + FormMode + "ComplaintCategoryItemList_" + a4406 + "").checked = false;
        }

        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin24Hours").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin24HoursReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin5Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin5DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin10Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin10DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin3Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin3DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintCustomerSatisfied").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintCustomerSatisfiedReason").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintInvestigatorName").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintInvestigatorDesignation").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintRootCause").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut").checked = false;
        }
        Hide("ComplaintDetail1");
        Hide("ComplaintDetail2");
        Hide("ComplaintDetail4");
        Hide("ComplaintDetail5");
        Hide("ComplaintDetail6");
        Hide("ComplaintDetail7");
        Hide("ComplaintDetail8");
        Hide("ComplaintDetail9");
        Hide("ComplaintDetail10");
        Hide("ComplaintDetail11");
        Hide("ComplaintDetail12");
        Hide("ComplaintDetail13");
        Hide("ComplaintDetail14");
        Hide("ComplaintDetail15");
        Hide("ComplaintDetail16");
        Hide("ComplaintDetail17");
        Hide("ComplaintDetail18");
        Hide("ComplaintDetail19");
        Hide("ComplaintDetail20");
        Hide("ComplaintDetail21");
        Hide("ComplaintDetail22");
        Hide("ComplaintDetail23");
        Hide("ComplaintDetail24");
        Hide("ComplaintDetail25");
        Hide("ComplaintDetail26");
        Hide("ComplaintDetail27");
        Hide("ComplaintDetail28");
        Hide("ComplaintDetail29");
        Hide("ComplaintDetail30");

        Show("ComplimentDetail1");
        Show("ComplimentDetail2");
        Show("ComplimentDetail3");
        Show("ComplimentDetail4");
        Show("ComplimentDetail5");
        Show("ComplimentDetail6");
        Show("ComplimentDetail7");
        Show("ComplimentDetail8");
        Show("ComplimentDetail9");
        Show("ComplimentDetail10");

        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge"))
        {
          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge").checked == true)
          {
            Show("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut");
            Hide("FormView_CRM_Form_Label_" + FormMode + "ComplimentCloseOut");

            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              Show("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }
          else
          {
            document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut").checked = false;
            Hide("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut");
            Show("FormView_CRM_Form_Label_" + FormMode + "ComplimentCloseOut");

            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute").checked = false;
              Hide("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }

          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut").checked == true)
          {
            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute").checked = false;
              Hide("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }
        }

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CommentDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentUnitId").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentTypeList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentCategoryList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalUnitId").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalTypeList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalCategoryList").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut").checked = false;
        }
        Hide("CommentDetail1");
        Hide("CommentDetail2");
        Hide("CommentDetail3");
        Hide("CommentDetail4");
        Hide("CommentDetail5");
        Hide("CommentDetail6");
        Hide("CommentDetail7");
        Hide("CommentDetail8");
        Hide("CommentDetail9");
        Hide("CommentDetail10");
        Hide("CommentDetail11");
        if (document.getElementById("CommentDetail12"))
        {
          Hide("CommentDetail12");
        }
        Hide("CommentDetail14");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "QueryDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "QueryUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut").checked = false;
        }
        Hide("QueryDetail1");
        Hide("QueryDetail2");
        Hide("QueryDetail3");
        Hide("QueryDetail4");
        Hide("QueryDetail5");
        Hide("QueryDetail6");
        Hide("QueryDetail7");
        Hide("QueryDetail8");
        Hide("QueryDetail9");
        Hide("QueryDetail10");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "SuggestionDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "SuggestionUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut").checked = false;
        }
        Hide("SuggestionDetail1");
        Hide("SuggestionDetail2");
        Hide("SuggestionDetail3");
        Hide("SuggestionDetail4");
        Hide("SuggestionDetail5");
        Hide("SuggestionDetail6");
        Hide("SuggestionDetail7");
        Hide("SuggestionDetail8");
        Hide("SuggestionDetail9");
        Hide("SuggestionDetail10");
      }
      else if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "TypeList").value == "4412")
      {
        document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "EscalatedForm").checked = false;
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "EscalatedReportNumber").value = "";
        Hide("Escalated1");
        Hide("Escalated2");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintUnitId").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDateOccurred").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintTimeOccuredHours").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintTimeOccuredMinutes").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintPriorityList").value = "";
        var TotalItems4412 = parseInt(document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ComplaintCategoryItemListTotal").value);
        for (var a4412 = 0; a4412 < TotalItems4412; a4412++)
        {
          document.getElementById("FormView_CRM_Form_CheckBoxList_" + FormMode + "ComplaintCategoryItemList_" + a4412 + "").checked = false;
        }

        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin24Hours").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin24HoursReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin5Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin5DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin10Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin10DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin3Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin3DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintCustomerSatisfied").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintCustomerSatisfiedReason").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintInvestigatorName").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintInvestigatorDesignation").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintRootCause").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut").checked = false;
        }
        Hide("ComplaintDetail1");
        Hide("ComplaintDetail2");
        Hide("ComplaintDetail4");
        Hide("ComplaintDetail5");
        Hide("ComplaintDetail6");
        Hide("ComplaintDetail7");
        Hide("ComplaintDetail8");
        Hide("ComplaintDetail9");
        Hide("ComplaintDetail10");
        Hide("ComplaintDetail11");
        Hide("ComplaintDetail12");
        Hide("ComplaintDetail13");
        Hide("ComplaintDetail14");
        Hide("ComplaintDetail15");
        Hide("ComplaintDetail16");
        Hide("ComplaintDetail17");
        Hide("ComplaintDetail18");
        Hide("ComplaintDetail19");
        Hide("ComplaintDetail20");
        Hide("ComplaintDetail21");
        Hide("ComplaintDetail22");
        Hide("ComplaintDetail23");
        Hide("ComplaintDetail24");
        Hide("ComplaintDetail25");
        Hide("ComplaintDetail26");
        Hide("ComplaintDetail27");
        Hide("ComplaintDetail28");
        Hide("ComplaintDetail29");
        Hide("ComplaintDetail30");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplimentDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplimentUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut").checked = false;
        }
        Hide("ComplimentDetail1");
        Hide("ComplimentDetail2");
        Hide("ComplimentDetail3");
        Hide("ComplimentDetail4");
        Hide("ComplimentDetail5");
        Hide("ComplimentDetail6");
        Hide("ComplimentDetail7");
        Hide("ComplimentDetail8");
        Hide("ComplimentDetail9");
        Hide("ComplimentDetail10");

        Show("CommentDetail1");
        Show("CommentDetail2");
        Show("CommentDetail3");
        Show("CommentDetail4");
        Show("CommentDetail5");
        Show("CommentDetail6");
        Show("CommentDetail7");
        Show("CommentDetail8");
        Show("CommentDetail9");
        Show("CommentDetail10");
        Show("CommentDetail11");
        if (document.getElementById("CommentDetail12"))
        {
          Show("CommentDetail12");
        }
        Show("CommentDetail14");

        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge"))
        {
          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge").checked == true)
          {
            Show("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut");
            Hide("FormView_CRM_Form_Label_" + FormMode + "CommentCloseOut");

            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              Show("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }
          else
          {
            document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut").checked = false;
            Hide("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut");
            Show("FormView_CRM_Form_Label_" + FormMode + "CommentCloseOut");

            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute").checked = false;
              Hide("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }

          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut").checked == true)
          {
            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute").checked = false;
              Hide("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }
        }

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "QueryDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "QueryUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut").checked = false;
        }
        Hide("QueryDetail1");
        Hide("QueryDetail2");
        Hide("QueryDetail3");
        Hide("QueryDetail4");
        Hide("QueryDetail5");
        Hide("QueryDetail6");
        Hide("QueryDetail7");
        Hide("QueryDetail8");
        Hide("QueryDetail9");
        Hide("QueryDetail10");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "SuggestionDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "SuggestionUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut").checked = false;
        }
        Hide("SuggestionDetail1");
        Hide("SuggestionDetail2");
        Hide("SuggestionDetail3");
        Hide("SuggestionDetail4");
        Hide("SuggestionDetail5");
        Hide("SuggestionDetail6");
        Hide("SuggestionDetail7");
        Hide("SuggestionDetail8");
        Hide("SuggestionDetail9");
        Hide("SuggestionDetail10");
      }
      else if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "TypeList").value == "4413")
      {
        document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "EscalatedForm").checked = false;
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "EscalatedReportNumber").value = "";
        Hide("Escalated1");
        Hide("Escalated2");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintUnitId").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDateOccurred").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintTimeOccuredHours").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintTimeOccuredMinutes").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintPriorityList").value = "";
        var TotalItems4413 = parseInt(document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ComplaintCategoryItemListTotal").value);
        for (var a4413 = 0; a4413 < TotalItems4413; a4413++)
        {
          document.getElementById("FormView_CRM_Form_CheckBoxList_" + FormMode + "ComplaintCategoryItemList_" + a4413 + "").checked = false;
        }

        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin24Hours").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin24HoursReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin5Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin5DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin10Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin10DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin3Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin3DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintCustomerSatisfied").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintCustomerSatisfiedReason").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintInvestigatorName").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintInvestigatorDesignation").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintRootCause").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut").checked = false;
        }
        Hide("ComplaintDetail1");
        Hide("ComplaintDetail2");
        Hide("ComplaintDetail4");
        Hide("ComplaintDetail5");
        Hide("ComplaintDetail6");
        Hide("ComplaintDetail7");
        Hide("ComplaintDetail8");
        Hide("ComplaintDetail9");
        Hide("ComplaintDetail10");
        Hide("ComplaintDetail11");
        Hide("ComplaintDetail12");
        Hide("ComplaintDetail13");
        Hide("ComplaintDetail14");
        Hide("ComplaintDetail15");
        Hide("ComplaintDetail16");
        Hide("ComplaintDetail17");
        Hide("ComplaintDetail18");
        Hide("ComplaintDetail19");
        Hide("ComplaintDetail20");
        Hide("ComplaintDetail21");
        Hide("ComplaintDetail22");
        Hide("ComplaintDetail23");
        Hide("ComplaintDetail24");
        Hide("ComplaintDetail25");
        Hide("ComplaintDetail26");
        Hide("ComplaintDetail27");
        Hide("ComplaintDetail28");
        Hide("ComplaintDetail29");
        Hide("ComplaintDetail30");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplimentDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplimentUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut").checked = false;
        }
        Hide("ComplimentDetail1");
        Hide("ComplimentDetail2");
        Hide("ComplimentDetail3");
        Hide("ComplimentDetail4");
        Hide("ComplimentDetail5");
        Hide("ComplimentDetail6");
        Hide("ComplimentDetail7");
        Hide("ComplimentDetail8");
        Hide("ComplimentDetail9");
        Hide("ComplimentDetail10");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CommentDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentUnitId").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentTypeList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentCategoryList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalUnitId").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalTypeList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalCategoryList").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut").checked = false;
        }
        Hide("CommentDetail1");
        Hide("CommentDetail2");
        Hide("CommentDetail3");
        Hide("CommentDetail4");
        Hide("CommentDetail5");
        Hide("CommentDetail6");
        Hide("CommentDetail7");
        Hide("CommentDetail8");
        Hide("CommentDetail9");
        Hide("CommentDetail10");
        Hide("CommentDetail11");
        if (document.getElementById("CommentDetail12"))
        {
          Hide("CommentDetail12");
        }
        Hide("CommentDetail14");

        Show("QueryDetail1");
        Show("QueryDetail2");
        Show("QueryDetail3");
        Show("QueryDetail4");
        Show("QueryDetail5");
        Show("QueryDetail6");
        Show("QueryDetail7");
        Show("QueryDetail8");
        Show("QueryDetail9");
        Show("QueryDetail10");

        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge"))
        {
          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge").checked == true)
          {
            Show("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut");
            Hide("FormView_CRM_Form_Label_" + FormMode + "QueryCloseOut");

            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              Show("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }
          else
          {
            document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut").checked = false;
            Hide("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut");
            Show("FormView_CRM_Form_Label_" + FormMode + "QueryCloseOut");

            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute").checked = false;
              Hide("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }

          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut").checked == true)
          {
            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute").checked = false;
              Hide("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }
        }

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "SuggestionDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "SuggestionUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut").checked = false;
        }
        Hide("SuggestionDetail1");
        Hide("SuggestionDetail2");
        Hide("SuggestionDetail3");
        Hide("SuggestionDetail4");
        Hide("SuggestionDetail5");
        Hide("SuggestionDetail6");
        Hide("SuggestionDetail7");
        Hide("SuggestionDetail8");
        Hide("SuggestionDetail9");
        Hide("SuggestionDetail10");
      }
      else if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "TypeList").value == "4414")
      {
        document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "EscalatedForm").checked = false;
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "EscalatedReportNumber").value = "";
        Hide("Escalated1");
        Hide("Escalated2");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintUnitId").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDateOccurred").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintTimeOccuredHours").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintTimeOccuredMinutes").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintPriorityList").value = "";
        var TotalItems4414 = parseInt(document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ComplaintCategoryItemListTotal").value);
        for (var a4414 = 0; a4414 < TotalItems4414; a4414++)
        {
          document.getElementById("FormView_CRM_Form_CheckBoxList_" + FormMode + "ComplaintCategoryItemList_" + a4414 + "").checked = false;
        }

        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin24Hours").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin24HoursReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin5Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin5DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin10Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin10DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin3Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin3DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintCustomerSatisfied").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintCustomerSatisfiedReason").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintInvestigatorName").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintInvestigatorDesignation").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintRootCause").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut").checked = false;
        }
        Hide("ComplaintDetail1");
        Hide("ComplaintDetail2");
        Hide("ComplaintDetail4");
        Hide("ComplaintDetail5");
        Hide("ComplaintDetail6");
        Hide("ComplaintDetail7");
        Hide("ComplaintDetail8");
        Hide("ComplaintDetail9");
        Hide("ComplaintDetail10");
        Hide("ComplaintDetail11");
        Hide("ComplaintDetail12");
        Hide("ComplaintDetail13");
        Hide("ComplaintDetail14");
        Hide("ComplaintDetail15");
        Hide("ComplaintDetail16");
        Hide("ComplaintDetail17");
        Hide("ComplaintDetail18");
        Hide("ComplaintDetail19");
        Hide("ComplaintDetail20");
        Hide("ComplaintDetail21");
        Hide("ComplaintDetail22");
        Hide("ComplaintDetail23");
        Hide("ComplaintDetail24");
        Hide("ComplaintDetail25");
        Hide("ComplaintDetail26");
        Hide("ComplaintDetail27");
        Hide("ComplaintDetail28");
        Hide("ComplaintDetail29");
        Hide("ComplaintDetail30");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplimentDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplimentUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut").checked = false;
        }
        Hide("ComplimentDetail1");
        Hide("ComplimentDetail2");
        Hide("ComplimentDetail3");
        Hide("ComplimentDetail4");
        Hide("ComplimentDetail5");
        Hide("ComplimentDetail6");
        Hide("ComplimentDetail7");
        Hide("ComplimentDetail8");
        Hide("ComplimentDetail9");
        Hide("ComplimentDetail10");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CommentDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentUnitId").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentTypeList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentCategoryList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalUnitId").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalTypeList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalCategoryList").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut").checked = false;
        }
        Hide("CommentDetail1");
        Hide("CommentDetail2");
        Hide("CommentDetail3");
        Hide("CommentDetail4");
        Hide("CommentDetail5");
        Hide("CommentDetail6");
        Hide("CommentDetail7");
        Hide("CommentDetail8");
        Hide("CommentDetail9");
        Hide("CommentDetail10");
        Hide("CommentDetail11");
        if (document.getElementById("CommentDetail12"))
        {
          Hide("CommentDetail12");
        }
        Hide("CommentDetail14");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "QueryDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "QueryUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut").checked = false;
        }
        Hide("QueryDetail1");
        Hide("QueryDetail2");
        Hide("QueryDetail3");
        Hide("QueryDetail4");
        Hide("QueryDetail5");
        Hide("QueryDetail6");
        Hide("QueryDetail7");
        Hide("QueryDetail8");
        Hide("QueryDetail9");
        Hide("QueryDetail10");

        Show("SuggestionDetail1");
        Show("SuggestionDetail2");
        Show("SuggestionDetail3");
        Show("SuggestionDetail4");
        Show("SuggestionDetail5");
        Show("SuggestionDetail6");
        Show("SuggestionDetail7");
        Show("SuggestionDetail8");
        Show("SuggestionDetail9");
        Show("SuggestionDetail10");

        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge"))
        {
          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge").checked == true)
          {
            Show("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut");
            Hide("FormView_CRM_Form_Label_" + FormMode + "SuggestionCloseOut");

            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              Show("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }
          else
          {
            document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut").checked = false;
            Hide("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut");
            Show("FormView_CRM_Form_Label_" + FormMode + "SuggestionCloseOut");

            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute").checked = false;
              Hide("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }

          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut").checked == true)
          {
            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
            {
              document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute").checked = false;
              Hide("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
            }
          }
        }
      }
      else
      {
        document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "EscalatedForm").checked = false;
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "EscalatedReportNumber").value = "";
        Hide("Escalated1");
        Hide("Escalated2");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintUnitId").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintDateOccurred").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintTimeOccuredHours").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintTimeOccuredMinutes").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintPriorityList").value = "";
        var TotalItemsELSE = parseInt(document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ComplaintCategoryItemListTotal").value);
        for (var aELSE = 0; aELSE < TotalItemsELSE; aELSE++)
        {
          document.getElementById("FormView_CRM_Form_CheckBoxList_" + FormMode + "ComplaintCategoryItemList_" + aELSE + "").checked = false;
        }

        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin24Hours").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin24HoursReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin5Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin5DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin10Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin10DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintWithin3Days").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintWithin3DaysReason").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplaintCustomerSatisfied").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintCustomerSatisfiedReason").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintInvestigatorName").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintInvestigatorDesignation").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplaintRootCause").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplaintCloseOut").checked = false;
        }
        Hide("ComplaintDetail1");
        Hide("ComplaintDetail2");
        Hide("ComplaintDetail4");
        Hide("ComplaintDetail5");
        Hide("ComplaintDetail6");
        Hide("ComplaintDetail7");
        Hide("ComplaintDetail8");
        Hide("ComplaintDetail9");
        Hide("ComplaintDetail10");
        Hide("ComplaintDetail11");
        Hide("ComplaintDetail12");
        Hide("ComplaintDetail13");
        Hide("ComplaintDetail14");
        Hide("ComplaintDetail15");
        Hide("ComplaintDetail16");
        Hide("ComplaintDetail17");
        Hide("ComplaintDetail18");
        Hide("ComplaintDetail19");
        Hide("ComplaintDetail20");
        Hide("ComplaintDetail21");
        Hide("ComplaintDetail22");
        Hide("ComplaintDetail23");
        Hide("ComplaintDetail24");
        Hide("ComplaintDetail25");
        Hide("ComplaintDetail26");
        Hide("ComplaintDetail27");
        Hide("ComplaintDetail28");
        Hide("ComplaintDetail29");
        Hide("ComplaintDetail30");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "ComplimentDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ComplimentUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "ComplimentCloseOut").checked = false;
        }
        Hide("ComplimentDetail1");
        Hide("ComplimentDetail2");
        Hide("ComplimentDetail3");
        Hide("ComplimentDetail4");
        Hide("ComplimentDetail5");
        Hide("ComplimentDetail6");
        Hide("ComplimentDetail7");
        Hide("ComplimentDetail8");
        Hide("ComplimentDetail9");
        Hide("ComplimentDetail10");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CommentDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentUnitId").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentTypeList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentCategoryList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalUnitId").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalTypeList").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "CommentAdditionalCategoryList").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "CommentCloseOut").checked = false;
        }
        Hide("CommentDetail1");
        Hide("CommentDetail2");
        Hide("CommentDetail3");
        Hide("CommentDetail4");
        Hide("CommentDetail5");
        Hide("CommentDetail6");
        Hide("CommentDetail7");
        Hide("CommentDetail8");
        Hide("CommentDetail9");
        Hide("CommentDetail10");
        Hide("CommentDetail11");
        if (document.getElementById("CommentDetail12"))
        {
          Hide("CommentDetail12");
        }
        Hide("CommentDetail14");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "QueryDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "QueryUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "QueryCloseOut").checked = false;
        }
        Hide("QueryDetail1");
        Hide("QueryDetail2");
        Hide("QueryDetail3");
        Hide("QueryDetail4");
        Hide("QueryDetail5");
        Hide("QueryDetail6");
        Hide("QueryDetail7");
        Hide("QueryDetail8");
        Hide("QueryDetail9");
        Hide("QueryDetail10");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "SuggestionDescription").value = "";
        document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "SuggestionUnitId").value = "";
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionAcknowledge").checked = false;
        }
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "SuggestionCloseOut").checked = false;
        }
        Hide("SuggestionDetail1");
        Hide("SuggestionDetail2");
        Hide("SuggestionDetail3");
        Hide("SuggestionDetail4");
        Hide("SuggestionDetail5");
        Hide("SuggestionDetail6");
        Hide("SuggestionDetail7");
        Hide("SuggestionDetail8");
        Hide("SuggestionDetail9");
        Hide("SuggestionDetail10");

        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
        {
          document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute").checked = false;
          Hide("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute");
        }
      }

      if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "4396")
      {
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CustomerName").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CustomerEmail").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CustomerContactNumber").value = "";
        Hide("CustomerInformation1");
        Hide("CustomerInformation2");
        Hide("CustomerInformation3");
        Hide("CustomerInformation4");
        Hide("CustomerInformation5");

        Show("PatientInformation1");
        Show("PatientInformation2");
        Show("PatientInformation3");
        Show("PatientInformation4");
        Show("PatientInformation5");
        Show("PatientInformation6");
        Show("PatientInformation7");
      }
      else if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "4415")
      {
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CustomerName").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CustomerEmail").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CustomerContactNumber").value = "";
        Hide("CustomerInformation1");
        Hide("CustomerInformation2");
        Hide("CustomerInformation3");
        Hide("CustomerInformation4");
        Hide("CustomerInformation5");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientVisitNumber").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientName").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientDateOfAdmission").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientEmail").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientContactNumber").value = "";
        Hide("PatientInformation1");
        Hide("PatientInformation2");
        Hide("PatientInformation3");
        Hide("PatientInformation4");
        Hide("PatientInformation5");
        Hide("PatientInformation6");
        Hide("PatientInformation7");
      }
      else if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "ReceivedFromList").value == "")
      {
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CustomerName").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CustomerEmail").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "CustomerContactNumber").value = "";
        Hide("CustomerInformation1");
        Hide("CustomerInformation2");
        Hide("CustomerInformation3");
        Hide("CustomerInformation4");
        Hide("CustomerInformation5");

        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientVisitNumber").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientName").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientDateOfAdmission").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientEmail").value = "";
        document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "PatientContactNumber").value = "";
        Hide("PatientInformation1");
        Hide("PatientInformation2");
        Hide("PatientInformation3");
        Hide("PatientInformation4");
        Hide("PatientInformation5");
        Hide("PatientInformation6");
        Hide("PatientInformation7");
      }
      else
      {
        Show("CustomerInformation1");
        Show("CustomerInformation2");
        Show("CustomerInformation3");
        Show("CustomerInformation4");
        Show("CustomerInformation5");

        Show("PatientInformation1");
        Show("PatientInformation2");
        Show("PatientInformation3");
        Show("PatientInformation4");
        Show("PatientInformation5");
        Show("PatientInformation6");
        Show("PatientInformation7");
      }

      if (FormMode == "Edit")
      {
        if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute"))
        {
          Hide("FormView_CRM_Form_Label_" + FormMode + "Status");

          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteRoute").checked == false)
          {
            document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "RouteFacility").value = "";

            var TotalRadioButtonListItemsYes = parseInt(document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "RouteUnitTotal").value);
            var CompletedRadioButtonListYes = "0";
            for (var aYes = 0; aYes < TotalRadioButtonListItemsYes; aYes++)
            {
              document.getElementById("FormView_CRM_Form_RadioButtonList_" + FormMode + "RouteUnit_" + aYes + "").checked = false;
            }

            document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "RouteComment").value = "";
            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteComplete"))
            {
              document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteComplete").checked = false;
            }

            if (document.getElementById("FormView_CRM_Form_RadioButtonList_" + FormMode + "RouteUnit"))
            {
              Hide("FormView_CRM_Form_RadioButtonList_" + FormMode + "RouteUnit");
            }

            Hide("Route1");
            Hide("Route2");
            Hide("Route3");
            Hide("Route4");
            Hide("Route5");
          }
          else
          {
            Show("Route1");
            Show("Route2");
            Show("Route3");
            Show("Route4");
            Show("Route5");
          }
        }
        else
        {
          if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteComplete"))
          {
            Hide("FormView_CRM_Form_DropDownList_" + FormMode + "Status");
            Show("FormView_CRM_Form_Label_" + FormMode + "Status");

            Show("Route1");
            Show("Route2");
            Show("Route3");
            Show("Route4");
            Show("Route5");
          }
          else
          {
            Hide("FormView_CRM_Form_Label_" + FormMode + "Status");

            document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "RouteFacility").value = "";

            var TotalRadioButtonListItemsYes = parseInt(document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "RouteUnitTotal").value);
            var CompletedRadioButtonListYes = "0";
            for (var aYes = 0; aYes < TotalRadioButtonListItemsYes; aYes++)
            {
              document.getElementById("FormView_CRM_Form_RadioButtonList_" + FormMode + "RouteUnit_" + aYes + "").checked = false;
            }

            document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "RouteComment").value = "";
            if (document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteComplete"))
            {
              document.getElementById("FormView_CRM_Form_CheckBox_" + FormMode + "RouteComplete").checked = false;
            }

            if (document.getElementById("FormView_CRM_Form_RadioButtonList_" + FormMode + "RouteUnit"))
            {
              Hide("FormView_CRM_Form_RadioButtonList_" + FormMode + "RouteUnit");
            }

            Hide("Route1");
            Hide("Route2");
            Hide("Route3");
            Hide("Route4");
            Hide("Route5");
          }
        }
      }

      if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "Status"))
      {
        if (document.getElementById("FormView_CRM_Form_DropDownList_" + FormMode + "Status").value == "Rejected")
        {
          Show("CRMStatusRejectedReason");
        }
        else
        {
          Hide("CRMStatusRejectedReason");
          document.getElementById("FormView_CRM_Form_TextBox_" + FormMode + "StatusRejectedReason").value = "";
        }
      }
    }

    if (FormMode == "Item")
    {
      if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "TypeList").value == "4395")
      {
        Show("Escalated1");
        if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "EscalatedForm").value == "true")
        {
          Show("Escalated2");
        }
        else
        {
          Hide("Escalated2");
        }

        Show("ComplaintDetail1");
        Show("ComplaintDetail2");
        Show("ComplaintDetail4");
        Show("ComplaintDetail5");
        Show("ComplaintDetail6");
        Show("ComplaintDetail7");
        Show("ComplaintDetail8");
        Show("ComplaintDetail9");
        Show("ComplaintDetail10");
        Show("ComplaintDetail11");
        Show("ComplaintDetail12");
        if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ComplaintWithin24Hours").value == "No")
        {
          Show("ComplaintDetail13");
        }
        else
        {
          Hide("ComplaintDetail13");
        }

        Show("ComplaintDetail14");

        if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "EscalatedForm").value == "true")
        {
          Hide("ComplaintDetail15");
          Hide("ComplaintDetail16");
          Hide("ComplaintDetail17");
          Hide("ComplaintDetail18");
          Show("ComplaintDetail19");
          if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ComplaintWithin3Days").value == "No")
          {
            Show("ComplaintDetail20");
          }
          else
          {
            Hide("ComplaintDetail20");
          }
        }
        else
        {
          Show("ComplaintDetail15");
          if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ComplaintWithin5Days").value == "No")
          {
            Show("ComplaintDetail16");
          }
          else
          {
            Hide("ComplaintDetail16");
          }

          Show("ComplaintDetail17");
          if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ComplaintWithin10Days").value == "No")
          {
            Show("ComplaintDetail18");
          }
          else
          {
            Hide("ComplaintDetail18");
          }

          Hide("ComplaintDetail19");
          Hide("ComplaintDetail20");
        }

        Show("ComplaintDetail21");
        if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ComplaintCustomerSatisfied").value == "No")
        {
          Show("ComplaintDetail22");
        }
        else
        {
          Hide("ComplaintDetail22");
        }

        Show("ComplaintDetail23");
        Show("ComplaintDetail24");
        Show("ComplaintDetail25");
        Show("ComplaintDetail26");
        Show("ComplaintDetail27");
        Show("ComplaintDetail28");
        Show("ComplaintDetail29");
        Show("ComplaintDetail30");

        Hide("ComplimentDetail1");
        Hide("ComplimentDetail2");
        Hide("ComplimentDetail3");
        Hide("ComplimentDetail4");
        Hide("ComplimentDetail5");
        Hide("ComplimentDetail6");
        Hide("ComplimentDetail7");
        Hide("ComplimentDetail8");
        Hide("ComplimentDetail9");
        Hide("ComplimentDetail10");

        Hide("CommentDetail1");
        Hide("CommentDetail2");
        Hide("CommentDetail3");
        Hide("CommentDetail4");
        Hide("CommentDetail5");
        Hide("CommentDetail6");
        Hide("CommentDetail7");
        Hide("CommentDetail8");
        Hide("CommentDetail9");
        Hide("CommentDetail10");
        Hide("CommentDetail11");
        if (document.getElementById("CommentDetail12"))
        {
          Hide("CommentDetail12");
        }
        Hide("CommentDetail14");

        Hide("QueryDetail1");
        Hide("QueryDetail2");
        Hide("QueryDetail3");
        Hide("QueryDetail4");
        Hide("QueryDetail5");
        Hide("QueryDetail6");
        Hide("QueryDetail7");
        Hide("QueryDetail8");
        Hide("QueryDetail9");
        Hide("QueryDetail10");

        Hide("SuggestionDetail1");
        Hide("SuggestionDetail2");
        Hide("SuggestionDetail3");
        Hide("SuggestionDetail4");
        Hide("SuggestionDetail5");
        Hide("SuggestionDetail6");
        Hide("SuggestionDetail7");
        Hide("SuggestionDetail8");
        Hide("SuggestionDetail9");
        Hide("SuggestionDetail10");
      }
      else if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "TypeList").value == "4406")
      {
        Hide("Escalated1");
        Hide("Escalated2");
        Hide("ComplaintDetail1");
        Hide("ComplaintDetail2");
        Hide("ComplaintDetail4");
        Hide("ComplaintDetail5");
        Hide("ComplaintDetail6");
        Hide("ComplaintDetail7");
        Hide("ComplaintDetail8");
        Hide("ComplaintDetail9");
        Hide("ComplaintDetail10");
        Hide("ComplaintDetail11");
        Hide("ComplaintDetail12");
        Hide("ComplaintDetail13");
        Hide("ComplaintDetail14");
        Hide("ComplaintDetail15");
        Hide("ComplaintDetail16");
        Hide("ComplaintDetail17");
        Hide("ComplaintDetail18");
        Hide("ComplaintDetail19");
        Hide("ComplaintDetail20");
        Hide("ComplaintDetail21");
        Hide("ComplaintDetail22");
        Hide("ComplaintDetail23");
        Hide("ComplaintDetail24");
        Hide("ComplaintDetail25");
        Hide("ComplaintDetail26");
        Hide("ComplaintDetail27");
        Hide("ComplaintDetail28");
        Hide("ComplaintDetail29");
        Hide("ComplaintDetail30");

        Show("ComplimentDetail1");
        Show("ComplimentDetail2");
        Show("ComplimentDetail3");
        Show("ComplimentDetail4");
        Show("ComplimentDetail5");
        Show("ComplimentDetail6");
        Show("ComplimentDetail7");
        Show("ComplimentDetail8");
        Show("ComplimentDetail9");
        Show("ComplimentDetail10");

        Hide("CommentDetail1");
        Hide("CommentDetail2");
        Hide("CommentDetail3");
        Hide("CommentDetail4");
        Hide("CommentDetail5");
        Hide("CommentDetail6");
        Hide("CommentDetail7");
        Hide("CommentDetail8");
        Hide("CommentDetail9");
        Hide("CommentDetail10");
        Hide("CommentDetail11");
        if (document.getElementById("CommentDetail12"))
        {
          Hide("CommentDetail12");
        }
        Hide("CommentDetail14");

        Hide("QueryDetail1");
        Hide("QueryDetail2");
        Hide("QueryDetail3");
        Hide("QueryDetail4");
        Hide("QueryDetail5");
        Hide("QueryDetail6");
        Hide("QueryDetail7");
        Hide("QueryDetail8");
        Hide("QueryDetail9");
        Hide("QueryDetail10");

        Hide("SuggestionDetail1");
        Hide("SuggestionDetail2");
        Hide("SuggestionDetail3");
        Hide("SuggestionDetail4");
        Hide("SuggestionDetail5");
        Hide("SuggestionDetail6");
        Hide("SuggestionDetail7");
        Hide("SuggestionDetail8");
        Hide("SuggestionDetail9");
        Hide("SuggestionDetail10");
      }
      else if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "TypeList").value == "4412")
      {
        Hide("Escalated1");
        Hide("Escalated2");
        Hide("ComplaintDetail1");
        Hide("ComplaintDetail2");
        Hide("ComplaintDetail4");
        Hide("ComplaintDetail5");
        Hide("ComplaintDetail6");
        Hide("ComplaintDetail7");
        Hide("ComplaintDetail8");
        Hide("ComplaintDetail9");
        Hide("ComplaintDetail10");
        Hide("ComplaintDetail11");
        Hide("ComplaintDetail12");
        Hide("ComplaintDetail13");
        Hide("ComplaintDetail14");
        Hide("ComplaintDetail15");
        Hide("ComplaintDetail16");
        Hide("ComplaintDetail17");
        Hide("ComplaintDetail18");
        Hide("ComplaintDetail19");
        Hide("ComplaintDetail20");
        Hide("ComplaintDetail21");
        Hide("ComplaintDetail22");
        Hide("ComplaintDetail23");
        Hide("ComplaintDetail24");
        Hide("ComplaintDetail25");
        Hide("ComplaintDetail26");
        Hide("ComplaintDetail27");
        Hide("ComplaintDetail28");
        Hide("ComplaintDetail29");
        Hide("ComplaintDetail30");

        Hide("ComplimentDetail1");
        Hide("ComplimentDetail2");
        Hide("ComplimentDetail3");
        Hide("ComplimentDetail4");
        Hide("ComplimentDetail5");
        Hide("ComplimentDetail6");
        Hide("ComplimentDetail7");
        Hide("ComplimentDetail8");
        Hide("ComplimentDetail9");
        Hide("ComplimentDetail10");

        Show("CommentDetail1");
        Show("CommentDetail2");
        Show("CommentDetail3");
        Show("CommentDetail4");
        Show("CommentDetail5");
        Show("CommentDetail6");
        Show("CommentDetail7");
        Show("CommentDetail8");
        Show("CommentDetail9");
        Show("CommentDetail10");
        Show("CommentDetail11");
        if (document.getElementById("CommentDetail12"))
        {
          Show("CommentDetail12");
        }
        Show("CommentDetail14");

        Hide("QueryDetail1");
        Hide("QueryDetail2");
        Hide("QueryDetail3");
        Hide("QueryDetail4");
        Hide("QueryDetail5");
        Hide("QueryDetail6");
        Hide("QueryDetail7");
        Hide("QueryDetail8");
        Hide("QueryDetail9");
        Hide("QueryDetail10");

        Hide("SuggestionDetail1");
        Hide("SuggestionDetail2");
        Hide("SuggestionDetail3");
        Hide("SuggestionDetail4");
        Hide("SuggestionDetail5");
        Hide("SuggestionDetail6");
        Hide("SuggestionDetail7");
        Hide("SuggestionDetail8");
        Hide("SuggestionDetail9");
        Hide("SuggestionDetail10");
      }
      else if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "TypeList").value == "4413")
      {
        Hide("Escalated1");
        Hide("Escalated2");
        Hide("ComplaintDetail1");
        Hide("ComplaintDetail2");
        Hide("ComplaintDetail4");
        Hide("ComplaintDetail5");
        Hide("ComplaintDetail6");
        Hide("ComplaintDetail7");
        Hide("ComplaintDetail8");
        Hide("ComplaintDetail9");
        Hide("ComplaintDetail10");
        Hide("ComplaintDetail11");
        Hide("ComplaintDetail12");
        Hide("ComplaintDetail13");
        Hide("ComplaintDetail14");
        Hide("ComplaintDetail15");
        Hide("ComplaintDetail16");
        Hide("ComplaintDetail17");
        Hide("ComplaintDetail18");
        Hide("ComplaintDetail19");
        Hide("ComplaintDetail20");
        Hide("ComplaintDetail21");
        Hide("ComplaintDetail22");
        Hide("ComplaintDetail23");
        Hide("ComplaintDetail24");
        Hide("ComplaintDetail25");
        Hide("ComplaintDetail26");
        Hide("ComplaintDetail27");
        Hide("ComplaintDetail28");
        Hide("ComplaintDetail29");
        Hide("ComplaintDetail30");

        Hide("ComplimentDetail1");
        Hide("ComplimentDetail2");
        Hide("ComplimentDetail3");
        Hide("ComplimentDetail4");
        Hide("ComplimentDetail5");
        Hide("ComplimentDetail6");
        Hide("ComplimentDetail7");
        Hide("ComplimentDetail8");
        Hide("ComplimentDetail9");
        Hide("ComplimentDetail10");

        Hide("CommentDetail1");
        Hide("CommentDetail2");
        Hide("CommentDetail3");
        Hide("CommentDetail4");
        Hide("CommentDetail5");
        Hide("CommentDetail6");
        Hide("CommentDetail7");
        Hide("CommentDetail8");
        Hide("CommentDetail9");
        Hide("CommentDetail10");
        Hide("CommentDetail11");
        if (document.getElementById("CommentDetail12"))
        {
          Hide("CommentDetail12");
        }
        Hide("CommentDetail14");

        Show("QueryDetail1");
        Show("QueryDetail2");
        Show("QueryDetail3");
        Show("QueryDetail4");
        Show("QueryDetail5");
        Show("QueryDetail6");
        Show("QueryDetail7");
        Show("QueryDetail8");
        Show("QueryDetail9");
        Show("QueryDetail10");

        Hide("SuggestionDetail1");
        Hide("SuggestionDetail2");
        Hide("SuggestionDetail3");
        Hide("SuggestionDetail4");
        Hide("SuggestionDetail5");
        Hide("SuggestionDetail6");
        Hide("SuggestionDetail7");
        Hide("SuggestionDetail8");
        Hide("SuggestionDetail9");
        Hide("SuggestionDetail10");
      }
      else if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "TypeList").value == "4414")
      {
        Hide("Escalated1");
        Hide("Escalated2");
        Hide("ComplaintDetail1");
        Hide("ComplaintDetail2");
        Hide("ComplaintDetail4");
        Hide("ComplaintDetail5");
        Hide("ComplaintDetail6");
        Hide("ComplaintDetail7");
        Hide("ComplaintDetail8");
        Hide("ComplaintDetail9");
        Hide("ComplaintDetail10");
        Hide("ComplaintDetail11");
        Hide("ComplaintDetail12");
        Hide("ComplaintDetail13");
        Hide("ComplaintDetail14");
        Hide("ComplaintDetail15");
        Hide("ComplaintDetail16");
        Hide("ComplaintDetail17");
        Hide("ComplaintDetail18");
        Hide("ComplaintDetail19");
        Hide("ComplaintDetail20");
        Hide("ComplaintDetail21");
        Hide("ComplaintDetail22");
        Hide("ComplaintDetail23");
        Hide("ComplaintDetail24");
        Hide("ComplaintDetail25");
        Hide("ComplaintDetail26");
        Hide("ComplaintDetail27");
        Hide("ComplaintDetail28");
        Hide("ComplaintDetail29");
        Hide("ComplaintDetail30");

        Hide("ComplimentDetail1");
        Hide("ComplimentDetail2");
        Hide("ComplimentDetail3");
        Hide("ComplimentDetail4");
        Hide("ComplimentDetail5");
        Hide("ComplimentDetail6");
        Hide("ComplimentDetail7");
        Hide("ComplimentDetail8");
        Hide("ComplimentDetail9");
        Hide("ComplimentDetail10");

        Hide("CommentDetail1");
        Hide("CommentDetail2");
        Hide("CommentDetail3");
        Hide("CommentDetail4");
        Hide("CommentDetail5");
        Hide("CommentDetail6");
        Hide("CommentDetail7");
        Hide("CommentDetail8");
        Hide("CommentDetail9");
        Hide("CommentDetail10");
        Hide("CommentDetail11");
        if (document.getElementById("CommentDetail12"))
        {
          Hide("CommentDetail12");
        }
        Hide("CommentDetail14");

        Hide("QueryDetail1");
        Hide("QueryDetail2");
        Hide("QueryDetail3");
        Hide("QueryDetail4");
        Hide("QueryDetail5");
        Hide("QueryDetail6");
        Hide("QueryDetail7");
        Hide("QueryDetail8");
        Hide("QueryDetail9");
        Hide("QueryDetail10");

        Show("SuggestionDetail1");
        Show("SuggestionDetail2");
        Show("SuggestionDetail3");
        Show("SuggestionDetail4");
        Show("SuggestionDetail5");
        Show("SuggestionDetail6");
        Show("SuggestionDetail7");
        Show("SuggestionDetail8");
        Show("SuggestionDetail9");
        Show("SuggestionDetail10");
      }
      else
      {
        Hide("Escalated1");
        Hide("Escalated2");
        Hide("ComplaintDetail1");
        Hide("ComplaintDetail2");
        Hide("ComplaintDetail4");
        Hide("ComplaintDetail5");
        Hide("ComplaintDetail6");
        Hide("ComplaintDetail7");
        Hide("ComplaintDetail8");
        Hide("ComplaintDetail9");
        Hide("ComplaintDetail10");
        Hide("ComplaintDetail11");
        Hide("ComplaintDetail12");
        Hide("ComplaintDetail13");
        Hide("ComplaintDetail14");
        Hide("ComplaintDetail15");
        Hide("ComplaintDetail16");
        Hide("ComplaintDetail17");
        Hide("ComplaintDetail18");
        Hide("ComplaintDetail19");
        Hide("ComplaintDetail20");
        Hide("ComplaintDetail21");
        Hide("ComplaintDetail22");
        Hide("ComplaintDetail23");
        Hide("ComplaintDetail24");
        Hide("ComplaintDetail25");
        Hide("ComplaintDetail26");
        Hide("ComplaintDetail27");
        Hide("ComplaintDetail28");
        Hide("ComplaintDetail29");
        Hide("ComplaintDetail30");

        Hide("ComplimentDetail1");
        Hide("ComplimentDetail2");
        Hide("ComplimentDetail3");
        Hide("ComplimentDetail4");
        Hide("ComplimentDetail5");
        Hide("ComplimentDetail6");
        Hide("ComplimentDetail7");
        Hide("ComplimentDetail8");
        Hide("ComplimentDetail9");
        Hide("ComplimentDetail10");

        Hide("CommentDetail1");
        Hide("CommentDetail2");
        Hide("CommentDetail3");
        Hide("CommentDetail4");
        Hide("CommentDetail5");
        Hide("CommentDetail6");
        Hide("CommentDetail7");
        Hide("CommentDetail8");
        Hide("CommentDetail9");
        Hide("CommentDetail10");
        Hide("CommentDetail11");
        if (document.getElementById("CommentDetail12"))
        {
          Hide("CommentDetail12");
        }
        Hide("CommentDetail14");

        Hide("QueryDetail1");
        Hide("QueryDetail2");
        Hide("QueryDetail3");
        Hide("QueryDetail4");
        Hide("QueryDetail5");
        Hide("QueryDetail6");
        Hide("QueryDetail7");
        Hide("QueryDetail8");
        Hide("QueryDetail9");
        Hide("QueryDetail10");

        Hide("SuggestionDetail1");
        Hide("SuggestionDetail2");
        Hide("SuggestionDetail3");
        Hide("SuggestionDetail4");
        Hide("SuggestionDetail5");
        Hide("SuggestionDetail6");
        Hide("SuggestionDetail7");
        Hide("SuggestionDetail8");
        Hide("SuggestionDetail9");
        Hide("SuggestionDetail10");
      }

      if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ReceivedFromList").value == "4396")
      {
        Hide("CustomerInformation1");
        Hide("CustomerInformation2");
        Hide("CustomerInformation3");
        Hide("CustomerInformation4");
        Hide("CustomerInformation5");

        Show("PatientInformation1");
        Show("PatientInformation2");
        Show("PatientInformation3");
        Show("PatientInformation4");
        Show("PatientInformation5");
        Show("PatientInformation6");
        Show("PatientInformation7");
      }
      else if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ReceivedFromList").value == "4415")
      {
        Hide("CustomerInformation1");
        Hide("CustomerInformation2");
        Hide("CustomerInformation3");
        Hide("CustomerInformation4");
        Hide("CustomerInformation5");

        Hide("PatientInformation1");
        Hide("PatientInformation2");
        Hide("PatientInformation3");
        Hide("PatientInformation4");
        Hide("PatientInformation5");
        Hide("PatientInformation6");
        Hide("PatientInformation7");
      }
      else if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "ReceivedFromList").value == "")
      {
        Hide("CustomerInformation1");
        Hide("CustomerInformation2");
        Hide("CustomerInformation3");
        Hide("CustomerInformation4");
        Hide("CustomerInformation5");

        Hide("PatientInformation1");
        Hide("PatientInformation2");
        Hide("PatientInformation3");
        Hide("PatientInformation4");
        Hide("PatientInformation5");
        Hide("PatientInformation6");
        Hide("PatientInformation7");
      }
      else
      {
        Show("CustomerInformation1");
        Show("CustomerInformation2");
        Show("CustomerInformation3");
        Show("CustomerInformation4");
        Show("CustomerInformation5");

        Show("PatientInformation1");
        Show("PatientInformation2");
        Show("PatientInformation3");
        Show("PatientInformation4");
        Show("PatientInformation5");
        Show("PatientInformation6");
        Show("PatientInformation7");
      }

      if (document.getElementById("FormView_CRM_Form_HiddenField_" + FormMode + "Status").value == "Rejected")
      {
        Show("CRMStatusRejectedReason");
      }
      else
      {
        Hide("CRMStatusRejectedReason");
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