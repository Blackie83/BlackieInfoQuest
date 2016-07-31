
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form_Surveys-----------------------------------------------------------------------------------------------------------------------
function Validation_Form_Surveys()
{
  var FormMode;
  if (document.getElementById("FormView_PharmacySurveys_LoadedSurveys_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_PharmacySurveys_LoadedSurveys_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_PharmacySurveys_LoadedSurveys_Form_TextBox_" + FormMode + "Name").value == "")
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
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form_Sections----------------------------------------------------------------------------------------------------------------------
function Validation_Form_Sections()
{
  var FormMode;
  if (document.getElementById("FormView_PharmacySurveys_LoadedSections_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_PharmacySurveys_LoadedSections_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_PharmacySurveys_LoadedSections_Form_TextBox_" + FormMode + "Name").value == "")
    {
      document.getElementById("FormLoadedSectionsName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLoadedSectionsName").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormLoadedSectionsName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLoadedSectionsName").style.color = "#333333";
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form_Questions---------------------------------------------------------------------------------------------------------------------
function Validation_Form_Questions()
{
  var FormMode;
  if (document.getElementById("FormView_PharmacySurveys_LoadedQuestions_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_PharmacySurveys_LoadedQuestions_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_PharmacySurveys_LoadedQuestions_Form_TextBox_" + FormMode + "Name").value == "")
    {
      document.getElementById("FormLoadedQuestionsName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLoadedQuestionsName").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormLoadedQuestionsName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLoadedQuestionsName").style.color = "#333333";
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form_Answers-----------------------------------------------------------------------------------------------------------------------
function Validation_Form_Answers()
{
  var FormMode;
  if (document.getElementById("FormView_PharmacySurveys_LoadedAnswers_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_PharmacySurveys_LoadedAnswers_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_PharmacySurveys_LoadedAnswers_Form_TextBox_" + FormMode + "Name").value == "")
    {
      document.getElementById("FormLoadedAnswersName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLoadedAnswersName").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormLoadedAnswersName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLoadedAnswersName").style.color = "#333333";
    }

    if (document.getElementById("FormView_PharmacySurveys_LoadedAnswers_Form_TextBox_" + FormMode + "Score").value == "")
    {
      document.getElementById("FormLoadedAnswersScore").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLoadedAnswersScore").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormLoadedAnswersScore").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLoadedAnswersScore").style.color = "#333333";
    }
  }
}