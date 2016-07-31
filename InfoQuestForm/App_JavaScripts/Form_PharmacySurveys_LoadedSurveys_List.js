
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form_DuplicateSurveys()
{
  if (document.getElementById("TextBox_DuplicateName"))
  {
    if (document.getElementById("TextBox_DuplicateName").value == "")
    {
      document.getElementById("FormDuplicateSurveysName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDuplicateSurveysName").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDuplicateSurveysName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDuplicateSurveysName").style.color = "#333333";
    }
  }

  if (document.getElementById("DropDownList_DuplicateFY"))
  {
    if (document.getElementById("DropDownList_DuplicateFY").value == "")
    {
      document.getElementById("FormDuplicateSurveysFY").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDuplicateSurveysFY").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDuplicateSurveysFY").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDuplicateSurveysFY").style.color = "#333333";
    }
  }
}
