
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Search-----------------------------------------------------------------------------------------------------------------------------
function Validation_Search()
{
  if (document.getElementById("DropDownList_SearchFacilityId"))
  {
    if (document.getElementById("DropDownList_SearchFacilityId").value == "")
    {
      document.getElementById("SearchFacilityId").style.backgroundColor = "#d46e6e";
      document.getElementById("SearchFacilityId").style.color = "#333333";
    }
    else
    {
      document.getElementById("SearchFacilityId").style.backgroundColor = "#77cf9c";
      document.getElementById("SearchFacilityId").style.color = "#333333";
    }
  }

  if (document.getElementById("TextBox_SearchPatient"))
  {
    if (document.getElementById("TextBox_SearchPatient").value == "")
    {
      document.getElementById("SearchPatient").style.backgroundColor = "#d46e6e";
      document.getElementById("SearchPatient").style.color = "#333333";
    }
    else
    {
      document.getElementById("SearchPatient").style.backgroundColor = "#77cf9c";
      document.getElementById("SearchPatient").style.color = "#333333";
    }
  }
}