﻿
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Search-----------------------------------------------------------------------------------------------------------------------------
function Validation_Search()
{
  if (document.getElementById("TextBox_MapType").value == "")
  {
    document.getElementById("TextBox_MapType").style.backgroundColor = "#d46e6e";
    document.getElementById("TextBox_MapType").style.color = "#333333";
  }
  else
  {
    document.getElementById("TextBox_MapType").style.backgroundColor = "#77cf9c";
    document.getElementById("TextBox_MapType").style.color = "#333333";
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --CheckAllMapType--------------------------------------------------------------------------------------------------------------------------
function CheckAllMapType()
{
  var intIndex = 0;
  var rowCount = document.getElementById("CheckBoxList_MapType").getElementsByTagName("input").length;
  for (intIndex = 0; intIndex < rowCount; intIndex++)
  {
    if (document.getElementById("CheckBox_MapType_CheckAll").checked == true)
    {
      if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex))
      {
        if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex).disabled != true)
        {
          document.getElementById("CheckBoxList_MapType" + "_" + intIndex).checked = true;
        }
      }
    }
    else
    {
      if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex))
      {
        if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex).disabled != true)
        {
          document.getElementById("CheckBoxList_MapType" + "_" + intIndex).checked = false;
        }
      }
    }
  }

  var SelectedMapTypeValue = "";
  for (intIndex = 0; intIndex < rowCount; intIndex++)
  {
    if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex))
    {
      if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex).checked == true)
      {
        SelectedMapTypeValue = SelectedMapTypeValue + document.getElementById("CheckBoxList_MapType" + "_" + intIndex).value + ',';
      }
    }
  }

  if (SelectedMapTypeValue.charAt(SelectedMapTypeValue.length - 1) == ',')
  {
    SelectedMapTypeValue = SelectedMapTypeValue.substr(0, SelectedMapTypeValue.length - 1);
  }

  document.getElementById("TextBox_MapType").value = SelectedMapTypeValue;

  Validation_Search();
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ClearAllMapType--------------------------------------------------------------------------------------------------------------------------
function ClearAllMapType()
{
  var intIndex = 0;
  var flag = 0;
  var rowCount = document.getElementById("CheckBoxList_MapType").getElementsByTagName("input").length;
  for (intIndex = 0; intIndex < rowCount; intIndex++)
  {
    if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex))
    {
      if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex).checked == true)
      {
        flag = 1;
      }
      else
      {
        flag = 0;
        break;
      }
    }
  }

  if (flag == 0)
  {
    document.getElementById("CheckBox_MapType_CheckAll").checked = false;
  }
  else
  {
    document.getElementById("CheckBox_MapType_CheckAll").checked = true;
  }

  var SelectedMapTypeValue = "";
  for (intIndex = 0; intIndex < rowCount; intIndex++)
  {
    if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex))
    {
      if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex).checked == true)
      {
        SelectedMapTypeValue = SelectedMapTypeValue + document.getElementById("CheckBoxList_MapType" + "_" + intIndex).value + ',';
      }
    }
  }

  if (SelectedMapTypeValue.charAt(SelectedMapTypeValue.length - 1) == ',')
  {
    SelectedMapTypeValue = SelectedMapTypeValue.substr(0, SelectedMapTypeValue.length - 1);
  }

  document.getElementById("TextBox_MapType").value = SelectedMapTypeValue;

  Validation_Search();
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --CheckAllHospitalOrganisation------------------------------------------------------------------------------------------------------------------
function CheckAllHospitalOrganisation()
{
  var intIndex = 0;
  var rowCount = 0;
  if (document.getElementById("CheckBoxList_Hospital_Organisation"))
  {
    rowCount = document.getElementById("CheckBoxList_Hospital_Organisation").getElementsByTagName("input").length;
  }

  for (intIndex = 0; intIndex < rowCount; intIndex++)
  {
    if (document.getElementById("CheckBox_Hospital_Organisation_CheckAll").checked == true)
    {
      if (document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex))
      {
        if (document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex).disabled != true)
        {
          document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex).checked = true;
        }
      }
    }
    else
    {
      if (document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex))
      {
        if (document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex).disabled != true)
        {
          document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex).checked = false;
        }
      }
    }
  }

  var SelectedOrganisationValue = "";
  for (intIndex = 0; intIndex < rowCount; intIndex++)
  {
    if (document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex))
    {
      if (document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex).checked == true)
      {
        SelectedOrganisationValue = SelectedOrganisationValue + document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex).value + ',';
      }
    }
  }

  if (SelectedOrganisationValue.charAt(SelectedOrganisationValue.length - 1) == ',')
  {
    SelectedOrganisationValue = SelectedOrganisationValue.substr(0, SelectedOrganisationValue.length - 1);
  }

  document.getElementById("TextBox_Hospital_Organisation").value = SelectedOrganisationValue;

  if (rowCount == 0)
  {
    document.getElementById("CheckBox_Hospital_Organisation_CheckAll").checked = false;
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ClearAllHospitalOrganisation------------------------------------------------------------------------------------------------------------------
function ClearAllHospitalOrganisation()
{
  var intIndex = 0;
  var flag = 0;
  var rowCount = 0;
  if (document.getElementById("CheckBoxList_Hospital_Organisation"))
  {
    rowCount = document.getElementById("CheckBoxList_Hospital_Organisation").getElementsByTagName("input").length;
  }

  for (intIndex = 0; intIndex < rowCount; intIndex++)
  {
    if (document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex))
    {
      if (document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex).checked == true)
      {
        flag = 1;
      }
      else
      {
        flag = 0;
        break;
      }
    }
  }

  if (flag == 0)
  {
    document.getElementById("CheckBox_Hospital_Organisation_CheckAll").checked = false;
  }
  else
  {
    document.getElementById("CheckBox_Hospital_Organisation_CheckAll").checked = true;
  }

  var SelectedOrganisationValue = "";
  for (intIndex = 0; intIndex < rowCount; intIndex++)
  {
    if (document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex))
    {
      if (document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex).checked == true)
      {
        SelectedOrganisationValue = SelectedOrganisationValue + document.getElementById("CheckBoxList_Hospital_Organisation" + "_" + intIndex).value + ',';
      }
    }
  }

  if (SelectedOrganisationValue.charAt(SelectedOrganisationValue.length - 1) == ',')
  {
    SelectedOrganisationValue = SelectedOrganisationValue.substr(0, SelectedOrganisationValue.length - 1);
  }

  document.getElementById("TextBox_Hospital_Organisation").value = SelectedOrganisationValue;
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Search()
{
  var intIndex = 0;
  var ShowHospital = false;
  var ShowPopulation = false;
  var rowCount = document.getElementById("CheckBoxList_MapType").getElementsByTagName("input").length;
  for (intIndex = 0; intIndex < rowCount; intIndex++)
  {
    if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex).value == "Hospital")
    {
      if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex).checked == true)
      {
        ShowHospital = true;
        Show("Hospital1");
        Show("Hospital2");
        Show("Hospital3");
        Show("Hospital4");
      }
      else
      {
        if (ShowHospital == false)
        {
          Hide("Hospital1");
          Hide("Hospital2");
          Hide("Hospital3");
          Hide("Hospital4");
        }
      }
    }

    if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex).value == "Population")
    {
      if (document.getElementById("CheckBoxList_MapType" + "_" + intIndex).checked == true)
      {
        ShowPopulation = true;
        Show("Population1");
        Show("Population2");
      }
      else
      {
        if (ShowPopulation == false)
        {
          Hide("Population1");
          Hide("Population2");
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
