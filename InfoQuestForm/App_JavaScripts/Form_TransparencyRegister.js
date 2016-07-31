
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
  if (document.getElementById("FormView_TransparencyRegister_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "EmployeeNumber"))
    {
      if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "EmployeeNumber").value == "")
      {
        document.getElementById("FormEmployeeNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEmployeeNumber").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEmployeeNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEmployeeNumber").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "DeclarationDate"))
    {
      if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "DeclarationDate").value == "" || document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "DeclarationDate").value == "yyyy/mm/dd")
      {
        document.getElementById("FormDeclarationDate").style.backgroundColor = "#d46e6e";
        document.getElementById("FormDeclarationDate").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormDeclarationDate").style.backgroundColor = "#77cf9c";
        document.getElementById("FormDeclarationDate").style.color = "#333333";
      }
    }


    var TotalRecords = document.getElementById("FormView_TransparencyRegister_Form_HiddenField_" + FormMode + "TotalRecords").value
    if (TotalRecords > 0)
    {
      var ClassificationValid;
      for (var a = 0; a < TotalRecords; a++)
      {
        if (document.getElementById("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_CheckBox_" + FormMode + "Name_" + a).checked == true && document.getElementById("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_TextBox_" + FormMode + "Value_" + a).value != "")
        {
          if (ClassificationValid != "No")
          {
            ClassificationValid = "Yes";
          }
        }
        else if (document.getElementById("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_CheckBox_" + FormMode + "Name_" + a).checked == true && document.getElementById("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_TextBox_" + FormMode + "Value_" + a).value == "")
        {
          ClassificationValid = "No";
        }
      }

      if (ClassificationValid == "Yes")
      {
        document.getElementById("FormClassification").style.backgroundColor = "#77cf9c";
        document.getElementById("FormClassification").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormClassification").style.backgroundColor = "#d46e6e";
        document.getElementById("FormClassification").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "Description"))
    {
      if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "Description").value == "")
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

    //if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "Value"))
    //{
    //  if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "Value").value == "")
    //  {
    //    document.getElementById("FormValue").style.backgroundColor = "#d46e6e";
    //    document.getElementById("FormValue").style.color = "#333333";
    //  }
    //  else
    //  {
    //    document.getElementById("FormValue").style.backgroundColor = "#77cf9c";
    //    document.getElementById("FormValue").style.color = "#333333";
    //  }
    //}

    if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "Purpose"))
    {
      if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "Purpose").value == "")
      {
        document.getElementById("FormPurpose").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPurpose").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPurpose").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPurpose").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "PersonOrganisation"))
    {
      if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "PersonOrganisation").value == "")
      {
        document.getElementById("FormPersonOrganisation").style.backgroundColor = "#d46e6e";
        document.getElementById("FormPersonOrganisation").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormPersonOrganisation").style.backgroundColor = "#77cf9c";
        document.getElementById("FormPersonOrganisation").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "Relationship"))
    {
      if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "Relationship").value == "")
      {
        document.getElementById("FormRelationship").style.backgroundColor = "#d46e6e";
        document.getElementById("FormRelationship").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormRelationship").style.backgroundColor = "#77cf9c";
        document.getElementById("FormRelationship").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_TransparencyRegister_Form_DropDownList_" + FormMode + "Status"))
    {
      if (document.getElementById("FormView_TransparencyRegister_Form_DropDownList_" + FormMode + "Status").value == "Rejected")
      {
        if (document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "StatusMessage").value == "")
        {
          document.getElementById("FormStatusMessage").style.backgroundColor = "#d46e6e";
          document.getElementById("FormStatusMessage").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormStatusMessage").style.backgroundColor = "#77cf9c";
          document.getElementById("FormStatusMessage").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormStatusMessage").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormStatusMessage").style.color = "#000000";
      }
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Calculation_Form------------------------------------------------------------------------------------------------------------------------------
function Calculation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_TransparencyRegister_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    var TotalValue = 0;
    var TotalRecords = document.getElementById("FormView_TransparencyRegister_Form_HiddenField_" + FormMode + "TotalRecords").value;
    if (TotalRecords > 0)
    {
      for (var a = 0; a < TotalRecords; a++)
      {
        if (document.getElementById("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_CheckBox_" + FormMode + "Name_" + a).checked == true && document.getElementById("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_TextBox_" + FormMode + "Value_" + a).value != "")
        {
          TotalValue = TotalValue + parseFloat(document.getElementById("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_TextBox_" + FormMode + "Value_" + a).value);
        }
      }      
    }

    document.getElementById("FormView_TransparencyRegister_Form_TextBox_" + FormMode + "Value").value = TotalValue.toFixed(2);
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form()
{
  var FormMode;
  if (document.getElementById("FormView_TransparencyRegister_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (FormMode != "Item")
    {
      var TotalRecords = document.getElementById("FormView_TransparencyRegister_Form_HiddenField_" + FormMode + "TotalRecords").value
      if (TotalRecords > 0)
      {
        for (var a = 0; a < TotalRecords; a++)
        {
          if (document.getElementById("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_CheckBox_" + FormMode + "Name_" + a).checked == true)
          {
            Show("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_TextBox_" + FormMode + "Value_" + a);
            Show("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_Label_" + FormMode + "ValueCurrency_" + a);
          }
          else
          {
            document.getElementById("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_TextBox_" + FormMode + "Value_" + a).value = "";
            Hide("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_TextBox_" + FormMode + "Value_" + a);
            Hide("FormView_TransparencyRegister_Form_GridView_" + FormMode + "TransparencyRegister_Classification_Label_" + FormMode + "ValueCurrency_" + a);
          }
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