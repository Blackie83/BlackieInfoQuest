
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  if (document.getElementById("DropDownList_Facility").value == "")
  {
    document.getElementById("FormFacility").style.backgroundColor = "#d46e6e";
    document.getElementById("FormFacility").style.color = "#333333";
  }
  else
  {
    document.getElementById("FormFacility").style.backgroundColor = "#77cf9c";
    document.getElementById("FormFacility").style.color = "#333333";
  }

  if (document.getElementById("DropDownList_LoadedSurveysName").value == "")
  {
    document.getElementById("FormLoadedSurveysName").style.backgroundColor = "#d46e6e";
    document.getElementById("FormLoadedSurveysName").style.color = "#333333";
  }
  else
  {
    document.getElementById("FormLoadedSurveysName").style.backgroundColor = "#77cf9c";
    document.getElementById("FormLoadedSurveysName").style.color = "#333333";
  }
}