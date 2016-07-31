
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
  window.open(PrintLink, 'Print', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=No , location=No , scrollbars=Yes , resizable=No , status=Yes , left=' + left + ' , top=' + top + ' ');
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --LockedRecord----------------------------------------------------------------------------------------------------------------------------------
function LockedRecord()
{
  if (QueryStringValue("Locked") == null)
  {
    alert("Record is Locked \n\n" +
        "Click the \"Update Statistics\" button to save the changes and exit the form \n" +
        "or \n" +
        "Click the \"Back to List\" button to exit the form \n");

    window.location.href = window.location.href + "&Locked=1"
  }
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
//----- --Calculation_HABSI_Percentage------------------------------------------------------------------------------------------------------------------
function Calculation_HABSI_Percentage()
{
  var FormMode;
  if (document.getElementById("FormView_MHS_Form_TextBox_EditHABSI_MSRA"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "HABSI_MSRA") != null)
    {
      MSRA = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "HABSI_MSRA").value;
    }
    else
    {
      MSRA = "";
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "HABSI_MRSA") != null)
    {
      MRSA = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "HABSI_MRSA").value;
    }
    else
    {
      MRSA = "";
    }

    if ((MSRA != "") && (MRSA != ""))
    {
      if ((MSRA == "0") && (MRSA == "0"))
      {
        document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "HABSI_Percentage").value = 0;
      }
      else if ((MSRA != "0") && (MRSA == "0"))
      {
        document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "HABSI_Percentage").value = 0;
      }
      else if ((MSRA == "0") && (MRSA != "0"))
      {
        document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "HABSI_Percentage").value = 0;
      }
      else if ((MSRA != "0") && (MRSA != "0"))
      {
        var value2 = ((MRSA / MSRA) * 100);
        document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "HABSI_Percentage").value = value2.toFixed(2);
      }
    }
    else
    {
      if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "HABSI_Percentage") != null)
      {
        document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "HABSI_Percentage").value = ""
      }
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_CC_CCNReceivedPositive_Calculated-------------------------------------------------------------------------------------------------
function Calculation_CC_CCNReceivedPositive_Calculated()
{
  var FormMode;
  if (document.getElementById("FormView_MHS_Form_TextBox_EditCC_CCNReceivedPositive"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedPositive") != null)
    {
      Positive = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedPositive").value;
      if (Positive == "")
      {
        Positive = 0;
      }
    }
    else
    {
      Positive = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedPositive_Published") != null)
    {
      Positive_Published = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedPositive_Published").value;
      if (Positive_Published == "")
      {
        Positive_Published = 0;
      }
    }
    else
    {
      Positive_Published = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedPositive_Calculated") != null)
    {
      document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedPositive_Calculated").value = (parseFloat(Positive) + parseFloat(Positive_Published));
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_CC_CCNReceivedSuggestions_Calculated----------------------------------------------------------------------------------------------
function Calculation_CC_CCNReceivedSuggestions_Calculated()
{
  var FormMode;
  if (document.getElementById("FormView_MHS_Form_TextBox_EditCC_CCNReceivedPositive"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedSuggestions") != null)
    {
      Suggestions = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedSuggestions").value;
      if (Suggestions == "")
      {
        Suggestions = 0;
      }
    }
    else
    {
      Suggestions = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedSuggestions_Published") != null)
    {
      Suggestions_Published = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedSuggestions_Published").value;
      if (Suggestions_Published == "")
      {
        Suggestions_Published = 0;
      }
    }
    else
    {
      Suggestions_Published = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedSuggestions_Calculated") != null)
    {
      document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedSuggestions_Calculated").value = (parseFloat(Suggestions) + parseFloat(Suggestions_Published));
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_CCNReceivedNegative_Calculated----------------------------------------------------------------------------------------------------
function Calculation_CCNReceivedNegative_Calculated()
{
  var FormMode;
  if (document.getElementById("FormView_MHS_Form_TextBox_EditCC_CCNReceivedPositive"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedNegative") != null)
    {
      Negative = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedNegative").value;
      if (Negative == "")
      {
        Negative = 0;
      }
    }
    else
    {
      Negative = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedNegative_Published") != null)
    {
      Negative_Published = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedNegative_Published").value;
      if (Negative_Published == "")
      {
        Negative_Published = 0;
      }
    }
    else
    {
      Negative_Published = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedNegative_Calculated") != null)
    {
      document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedNegative_Calculated").value = (parseFloat(Negative) + parseFloat(Negative_Published));
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_CC_ComplaintsReceived_Calculated--------------------------------------------------------------------------------------------------
function Calculation_CC_ComplaintsReceived_Calculated()
{
  var FormMode;
  if (document.getElementById("FormView_MHS_Form_TextBox_EditCC_CCNReceivedPositive"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_ComplaintsReceived") != null)
    {
      ComplaintsReceived = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_ComplaintsReceived").value;
      if (ComplaintsReceived == "")
      {
        ComplaintsReceived = 0;
      }
    }
    else
    {
      ComplaintsReceived = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_ComplaintsReceived_Published") != null)
    {
      ComplaintsReceived_Published = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_ComplaintsReceived_Published").value;
      if (ComplaintsReceived_Published == "")
      {
        ComplaintsReceived_Published = 0;
      }
    }
    else
    {
      Negative_Published = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_ComplaintsReceived_Calculated") != null)
    {
      document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_ComplaintsReceived_Calculated").value = (parseFloat(ComplaintsReceived) + parseFloat(ComplaintsReceived_Published));
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_CC_CCNReceivedTotal---------------------------------------------------------------------------------------------------------------
function Calculate_CC_CCNReceivedTotal()
{
  var FormMode;
  if (document.getElementById("FormView_MHS_Form_TextBox_EditCC_CCNReceivedPositive"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedPositive_Calculated") != null)
    {
      Positive = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedPositive_Calculated").value;
      if (Positive == "")
      {
        Positive = 0;
      }
    }
    else
    {
      Positive = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedSuggestions_Calculated") != null)
    {
      Suggestions = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedSuggestions_Calculated").value;
      if (Suggestions == "")
      {
        Suggestions = 0;
      }
    }
    else
    {
      Suggestions = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedNegative_Calculated") != null)
    {
      Negative = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedNegative_Calculated").value;
      if (Negative == "")
      {
        Negative = 0;
      }
    }
    else
    {
      Negative = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedTotal") != null)
    {
      document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedTotal").value = (parseFloat(Positive) + parseFloat(Suggestions) + parseFloat(Negative));
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_CC_TotalComplaints----------------------------------------------------------------------------------------------------------------
function Calculation_CC_TotalComplaints()
{
  var FormMode;
  if (document.getElementById("FormView_MHS_Form_TextBox_EditCC_CCNReceivedPositive"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedNegative_Calculated") != null)
    {
      P3 = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_CCNReceivedNegative_Calculated").value;
      if (P3 == "")
      {
        P3 = 0;
      }
    }
    else
    {
      P3 = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_ComplaintsReceived_Calculated") != null)
    {
      P1P2 = document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_ComplaintsReceived_Calculated").value;
      if (P1P2 == "")
      {
        P1P2 = 0;
      }
    }
    else
    {
      P1P2 = 0;
    }

    if (document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_TotalComplaints") != null)
    {
      document.getElementById("FormView_MHS_Form_TextBox_" + FormMode + "CC_TotalComplaints").value = (parseFloat(P3) + parseFloat(P1P2));
    }
  }
}