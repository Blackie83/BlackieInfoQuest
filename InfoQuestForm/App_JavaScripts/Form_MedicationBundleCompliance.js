
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
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form(Control)
{
  var FormMode;
  if (document.getElementById("FormView_MedicationBundleCompliance_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_MedicationBundleCompliance_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_MedicationBundleCompliance_Form_DropDownList_" + FormMode + "Unit").value == "")
    {
      document.getElementById("FormUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUnit").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUnit").style.color = "#333333";
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_TextBox_" + FormMode + "Date").value == "" || document.getElementById("FormView_MedicationBundleCompliance_Form_TextBox_" + FormMode + "Date").value == "yyyy/mm/dd")
    {
      document.getElementById("FormDate").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDate").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDate").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDate").style.color = "#333333";
    }

    if ((document.getElementById("FormView_MedicationBundleCompliance_Form_DropDownList_" + FormMode + "Unit").value == "") || (document.getElementById("FormView_MedicationBundleCompliance_Form_TextBox_" + FormMode + "Date").value == "" || document.getElementById("FormView_MedicationBundleCompliance_Form_TextBox_" + FormMode + "Date").value == "yyyy/mm/dd"))
    {
      document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked = false;
      document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").disabled = true;
      document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked = false;
      document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").disabled = true;
      document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked = false;
      document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").disabled = true;
      document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked = false;
      document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").disabled = true;

      document.getElementById("FormAssessed").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormAssessed").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").disabled = false;
      document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").disabled = false;
      document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").disabled = false;
      document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").disabled = false;

      if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked == false)
      {
        document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
        document.getElementById("FormAssessed").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
        document.getElementById("FormAssessed").style.color = "#333333";
      }
    }

    Validation_LMA(FormMode, Control);
    Validation_CMA(FormMode, Control)
    Validation_RMA(FormMode, Control)
    Validation_ESM(FormMode, Control)
  }
}

function Validation_LMA(FormMode, Control)
{
  if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked == false)
  {
    document.getElementById("FormLMA").style.backgroundColor = "#f7f7f7";
    document.getElementById("FormLMA").style.color = "#333333";
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").disabled = true;
  }
  else
  {
    document.getElementById("FormLMA").style.backgroundColor = "#77cf9c";
    document.getElementById("FormLMA").style.color = "#333333";

    if (Control == undefined)
    {
      if (FormMode == "Insert")
      {
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").disabled = false;
      }
      else
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").checked == true)
        {
          document.getElementById("FormLMA").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormLMA").style.color = "#333333";

          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").disabled = true;

          if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked == false)
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    }
    else if (Control != undefined)
    {
      if (Control == "AssessedLMA")
      {
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").disabled = false;
      }

      if (Control == "LMASelectAll")
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").checked = true;
        }
      }

      if (Control == "LMA1" || Control == "LMA2" || Control == "LMA3" || Control == "LMA4")
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
        }
      }

      if (Control == "LMA1NA" || Control == "LMA2NA" || Control == "LMA3NA" || Control == "LMA4NA")
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").checked == true)
        {
          document.getElementById("FormLMA").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormLMA").style.color = "#333333";

          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMASelectAll").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").disabled = true;

          if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked == false)
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    }
  }
}

function Validation_CMA(FormMode, Control)
{
  if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked == false)
  {
    document.getElementById("FormCMA").style.backgroundColor = "#f7f7f7";
    document.getElementById("FormCMA").style.color = "#333333";
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").disabled = true;
  }
  else
  {
    document.getElementById("FormCMA").style.backgroundColor = "#77cf9c";
    document.getElementById("FormCMA").style.color = "#333333";

    if (Control == undefined)
    {
      if (FormMode == "Insert")
      {
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").disabled = false;
      }
      else
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").checked == true)
        {
          document.getElementById("FormCMA").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormCMA").style.color = "#333333";

          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").disabled = true;

          if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked == false)
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    }
    else if (Control != undefined)
    {
      if (Control == "AssessedCMA")
      {
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").disabled = false;
      }

      if (Control == "CMASelectAll")
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").checked = true;
        }
      }

      if (Control == "CMA1" || Control == "CMA2" || Control == "CMA3" || Control == "CMA4" || Control == "CMA5")
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
        }
      }

      if (Control == "CMA1NA" || Control == "CMA2NA" || Control == "CMA3NA" || Control == "CMA4NA" || Control == "CMA5NA")
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").checked == true)
        {
          document.getElementById("FormCMA").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormCMA").style.color = "#333333";

          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMASelectAll").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").disabled = true;

          if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked == false)
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    }
  }
}

function Validation_RMA(FormMode, Control)
{
  if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked == false)
  {
    document.getElementById("FormRMA").style.backgroundColor = "#f7f7f7";
    document.getElementById("FormRMA").style.color = "#333333";
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").disabled = true;
  }
  else
  {
    document.getElementById("FormRMA").style.backgroundColor = "#77cf9c";
    document.getElementById("FormRMA").style.color = "#333333";

    if (Control == undefined)
    {
      if (FormMode == "Insert")
      {
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").disabled = false;
      }
      else
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").checked == true)
        {
          document.getElementById("FormRMA").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormRMA").style.color = "#333333";

          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").disabled = true;

          if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked == false)
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    }
    else if (Control != undefined)
    {
      if (Control == "AssessedRMA")
      {
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").disabled = false;
      }

      if (Control == "RMASelectAll")
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").checked = true;
        }
      }

      if (Control == "RMA1" || Control == "RMA2" || Control == "RMA3")
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
        }
      }

      if (Control == "RMA1NA" || Control == "RMA2NA" || Control == "RMA3NA")
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").checked == true)
        {
          document.getElementById("FormRMA").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormRMA").style.color = "#333333";

          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMASelectAll").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").disabled = true;

          if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked == false)
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    }
  }
}

function Validation_ESM(FormMode, Control)
{
  if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked == false)
  {
    document.getElementById("FormESM").style.backgroundColor = "#f7f7f7";
    document.getElementById("FormESM").style.color = "#333333";
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").disabled = true;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").checked = false;
    document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").disabled = true;
  }
  else
  {
    document.getElementById("FormESM").style.backgroundColor = "#77cf9c";
    document.getElementById("FormESM").style.color = "#333333";

    if (Control == undefined)
    {
      if (FormMode == "Insert")
      {
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").disabled = false;
      }
      else
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").checked == true)
        {
          document.getElementById("FormESM").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormESM").style.color = "#333333";

          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").disabled = true;

          if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked == false)
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    }
    else if (Control != undefined)
    {
      if (Control == "AssessedESM")
      {
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").disabled = false;
        document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").disabled = false;
      }

      if (Control == "ESMSelectAll")
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").checked = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").checked = true;
        }
      }

      if (Control == "ESM1" || Control == "ESM2" || Control == "ESM3" || Control == "ESM4" || Control == "ESM5")
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").disabled = true;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
        }
      }

      if (Control == "ESM1NA" || Control == "ESM2NA" || Control == "ESM3NA" || Control == "ESM4NA" || Control == "ESM5NA")
      {
        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").checked == true)
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").disabled = true;
        }
        else
        {
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").disabled = false;
        }

        if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").checked == true && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").checked == true)
        {
          document.getElementById("FormESM").style.backgroundColor = "#f7f7f7";
          document.getElementById("FormESM").style.color = "#333333";

          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESMSelectAll").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").disabled = true;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").checked = false;
          document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").disabled = true;

          if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked == false && document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked == false)
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#d46e6e";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
          else
          {
            document.getElementById("FormAssessed").style.backgroundColor = "#77cf9c";
            document.getElementById("FormAssessed").style.color = "#333333";
          }
        }
      }
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_Form------------------------------------------------------------------------------------------------------------------------------
function Calculation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_MedicationBundleCompliance_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_MedicationBundleCompliance_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    Calculation_LMA(FormMode);
    Calculation_CMA(FormMode);
    Calculation_RMA(FormMode);
    Calculation_ESM(FormMode);
  }
}

function Calculation_LMA(FormMode)
{
  if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedLMA").checked == false)
  {
    document.getElementById("FormView_MedicationBundleCompliance_Form_Textbox_" + FormMode + "LMACal").value = "Not Assessed";
  }
  else
  {
    var LMA_Total;
    var LMA_Selected;
    LMA_Total = 0;
    LMA_Selected = 0;

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1").checked == true)
    {
      LMA_Total = LMA_Total + 1;
    }
    else
    {
      LMA_Total = LMA_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2").checked == true)
    {
      LMA_Total = LMA_Total + 1;
    }
    else
    {
      LMA_Total = LMA_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3").checked == true)
    {
      LMA_Total = LMA_Total + 1;
    }
    else
    {
      LMA_Total = LMA_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4").checked == true)
    {
      LMA_Total = LMA_Total + 1;
    } else
    {
      LMA_Total = LMA_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA1NA").checked == false)
    {
      LMA_Selected = LMA_Selected + 1;
    } else
    {
      LMA_Selected = LMA_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA2NA").checked == false)
    {
      LMA_Selected = LMA_Selected + 1;
    } else
    {
      LMA_Selected = LMA_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA3NA").checked == false)
    {
      LMA_Selected = LMA_Selected + 1;
    } else
    {
      LMA_Selected = LMA_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "LMA4NA").checked == false)
    {
      LMA_Selected = LMA_Selected + 1;
    } else
    {
      LMA_Selected = LMA_Selected + 0;
    }

    var LMA_Cal = ((LMA_Total / LMA_Selected) * 100);
    LMA_Cal = LMA_Cal.toFixed(0);
    document.getElementById("FormView_MedicationBundleCompliance_Form_Textbox_" + FormMode + "LMACal").value = LMA_Cal + " %";
  }
}

function Calculation_CMA(FormMode)
{
  if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedCMA").checked == false)
  {
    document.getElementById("FormView_MedicationBundleCompliance_Form_Textbox_" + FormMode + "CMACal").value = "Not Assessed";
  }
  else
  {
    var CMA_Total;
    var CMA_Selected;
    CMA_Total = 0;
    CMA_Selected = 0;

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1").checked == true)
    {
      CMA_Total = CMA_Total + 1;
    } else
    {
      CMA_Total = CMA_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2").checked == true)
    {
      CMA_Total = CMA_Total + 1;
    } else
    {
      CMA_Total = CMA_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3").checked == true)
    {
      CMA_Total = CMA_Total + 1;
    } else
    {
      CMA_Total = CMA_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4").checked == true)
    {
      CMA_Total = CMA_Total + 1;
    } else
    {
      CMA_Total = CMA_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5").checked == true)
    {
      CMA_Total = CMA_Total + 1;
    } else
    {
      CMA_Total = CMA_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA1NA").checked == false)
    {
      CMA_Selected = CMA_Selected + 1;
    } else
    {
      CMA_Selected = CMA_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA2NA").checked == false)
    {
      CMA_Selected = CMA_Selected + 1;
    } else
    {
      CMA_Selected = CMA_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA3NA").checked == false)
    {
      CMA_Selected = CMA_Selected + 1;
    } else
    {
      CMA_Selected = CMA_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA4NA").checked == false)
    {
      CMA_Selected = CMA_Selected + 1;
    } else
    {
      CMA_Selected = CMA_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "CMA5NA").checked == false)
    {
      CMA_Selected = CMA_Selected + 1;
    } else
    {
      CMA_Selected = CMA_Selected + 0;
    }

    var CMA_Cal = ((CMA_Total / CMA_Selected) * 100);
    CMA_Cal = CMA_Cal.toFixed(0);
    document.getElementById("FormView_MedicationBundleCompliance_Form_Textbox_" + FormMode + "CMACal").value = CMA_Cal + " %";
  }
}

function Calculation_RMA(FormMode)
{
  if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedRMA").checked == false)
  {
    document.getElementById("FormView_MedicationBundleCompliance_Form_Textbox_" + FormMode + "RMACal").value = "Not Assessed";
  }
  else
  {
    var RMA_Total;
    var RMA_Selected;
    RMA_Total = 0;
    RMA_Selected = 0;

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1").checked == true)
    {
      RMA_Total = RMA_Total + 1;
    } else
    {
      RMA_Total = RMA_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2").checked == true)
    {
      RMA_Total = RMA_Total + 1;
    } else
    {
      RMA_Total = RMA_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3").checked == true)
    {
      RMA_Total = RMA_Total + 1;
    } else
    {
      RMA_Total = RMA_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA1NA").checked == false)
    {
      RMA_Selected = RMA_Selected + 1;
    } else
    {
      RMA_Selected = RMA_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA2NA").checked == false)
    {
      RMA_Selected = RMA_Selected + 1;
    } else
    {
      RMA_Selected = RMA_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "RMA3NA").checked == false)
    {
      RMA_Selected = RMA_Selected + 1;
    } else
    {
      RMA_Selected = RMA_Selected + 0;
    }

    var RMA_Cal = ((RMA_Total / RMA_Selected) * 100);
    RMA_Cal = RMA_Cal.toFixed(0);
    document.getElementById("FormView_MedicationBundleCompliance_Form_Textbox_" + FormMode + "RMACal").value = RMA_Cal + " %";
  }
}

function Calculation_ESM(FormMode)
{
  if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "AssessedESM").checked == false)
  {
    document.getElementById("FormView_MedicationBundleCompliance_Form_Textbox_" + FormMode + "ESMCal").value = "Not Assessed";
  }
  else
  {
    var ESM_Total;
    var ESM_Selected;
    ESM_Total = 0;
    ESM_Selected = 0;

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1").checked == true)
    {
      ESM_Total = ESM_Total + 1;
    } else
    {
      ESM_Total = ESM_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2").checked == true)
    {
      ESM_Total = ESM_Total + 1;
    } else
    {
      ESM_Total = ESM_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3").checked == true)
    {
      ESM_Total = ESM_Total + 1;
    } else
    {
      ESM_Total = ESM_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4").checked == true)
    {
      ESM_Total = ESM_Total + 1;
    } else
    {
      ESM_Total = ESM_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5").checked == true)
    {
      ESM_Total = ESM_Total + 1;
    } else
    {
      ESM_Total = ESM_Total + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM1NA").checked == false)
    {
      ESM_Selected = ESM_Selected + 1;
    } else
    {
      ESM_Selected = ESM_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM2NA").checked == false)
    {
      ESM_Selected = ESM_Selected + 1;
    } else
    {
      ESM_Selected = ESM_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM3NA").checked == false)
    {
      ESM_Selected = ESM_Selected + 1;
    } else
    {
      ESM_Selected = ESM_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM4NA").checked == false)
    {
      ESM_Selected = ESM_Selected + 1;
    } else
    {
      ESM_Selected = ESM_Selected + 0;
    }

    if (document.getElementById("FormView_MedicationBundleCompliance_Form_CheckBox_" + FormMode + "ESM5NA").checked == false)
    {
      ESM_Selected = ESM_Selected + 1;
    } else
    {
      ESM_Selected = ESM_Selected + 0;
    }

    var ESM_Cal = ((ESM_Total / ESM_Selected) * 100);
    ESM_Cal = ESM_Cal.toFixed(0);
    document.getElementById("FormView_MedicationBundleCompliance_Form_Textbox_" + FormMode + "ESMCal").value = ESM_Cal + " %";
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form()
{
  var FormMode;
  if (document.getElementById("FormView_MedicationBundleCompliance_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_MedicationBundleCompliance_Form_HiddenField_Edit"))
  {
    FormMode = "Edit"
  }
  else if (document.getElementById("FormView_MedicationBundleCompliance_Form_HiddenField_Item"))
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
    }

    if (FormMode == "Item")
    {
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