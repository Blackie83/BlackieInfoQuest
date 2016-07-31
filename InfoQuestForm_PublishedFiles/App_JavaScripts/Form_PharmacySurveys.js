
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
function Validation_Form(Control)
{
  var FormMode;
  if (document.getElementById("FormView_PharmacySurveys_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_PharmacySurveys_Form_DropDownList_" + FormMode + "Unit").value == "")
    {
      document.getElementById("FormUnit").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUnit").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormUnit").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUnit").style.color = "#333333";
    }

    if (document.getElementById("FormView_PharmacySurveys_Form_TextBox_" + FormMode + "Designation").value == "")
    {
      document.getElementById("FormDesignation").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDesignation").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDesignation").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDesignation").style.color = "#333333";
    }

    if (Control == undefined)
    {
      var Question_Count = document.getElementById("FormView_PharmacySurveys_Form_HiddenField_EditControlCount").value;
      for (var a = 1; a < Question_Count; a++)
      {
        var RadioButtonList_SurveyQuestion = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + a);
        var RadioButtonList_SurveyQuestion_Count = RadioButtonList_SurveyQuestion.getElementsByTagName("input");
        var QuestionsAnswers_Count = RadioButtonList_SurveyQuestion_Count.length;
        var ControlComplete = false;
        for (var b = 0; b < QuestionsAnswers_Count; b++)
        {
          if (document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + a + "_" + b).checked == true || ControlComplete == true)
          {
            document.getElementById("FormView_PharmacySurveys_Form_FormSurveyQuestion_" + a).style.backgroundColor = "#77cf9c";
            document.getElementById("FormView_PharmacySurveys_Form_FormSurveyQuestion_" + a).style.color = "#333333";

            ControlComplete = true;
          }
          else
          {
            document.getElementById("FormView_PharmacySurveys_Form_FormSurveyQuestion_" + a).style.backgroundColor = "#d46e6e";
            document.getElementById("FormView_PharmacySurveys_Form_FormSurveyQuestion_" + a).style.color = "#333333";
          }
        }
      }
    }
    else
    {
      var RadioButtonList_SurveyQuestion = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + Control);
      var RadioButtonList_SurveyQuestion_Count = RadioButtonList_SurveyQuestion.getElementsByTagName("input");
      var QuestionsAnswers_Count = RadioButtonList_SurveyQuestion_Count.length;
      var ControlComplete = false;
      for (var a = 0; a < QuestionsAnswers_Count; a++)
      {
        if (document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + Control + "_" + a).checked == true || ControlComplete == true)
        {
          document.getElementById("FormView_PharmacySurveys_Form_FormSurveyQuestion_" + Control).style.backgroundColor = "#77cf9c";
          document.getElementById("FormView_PharmacySurveys_Form_FormSurveyQuestion_" + Control).style.color = "#333333";

          if (ControlComplete == false)
          {
            document.getElementById("FormView_PharmacySurveys_Form_HiddenField_" + FormMode + "ControlAnswers").value = document.getElementById("FormView_PharmacySurveys_Form_HiddenField_" + FormMode + "ControlAnswers").value + "RadioButtonList_" + FormMode + "SurveyQuestion_" + Control + "=" + document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + Control + "_" + a).value + "&";
          }

          ControlComplete = true;
        }
        else
        {
          document.getElementById("FormView_PharmacySurveys_Form_FormSurveyQuestion_" + Control).style.backgroundColor = "#d46e6e";
          document.getElementById("FormView_PharmacySurveys_Form_FormSurveyQuestion_" + Control).style.color = "#333333";
        }
      }
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form(Control)
{
  var FormMode;
  if (document.getElementById("FormView_PharmacySurveys_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "Item";
  }

  if (FormMode != "")
  {
    if (FormMode != "Item")
    {
      if (Control == undefined)
      {
        for (var a = 1; a < document.getElementById("FormView_PharmacySurveys_Form_HiddenField_" + FormMode + "ControlCount").value; a++)
        {
          var SurveyQuestionId = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + a).getAttribute("SurveyQuestionId");
          var SurveyQuestionShowHideDependencyQuestionId = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + a).getAttribute("SurveyQuestionShowHideDependencyQuestionId");
          var SurveyQuestionShowHideDependencyAnswerId = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + a).getAttribute("SurveyQuestionShowHideDependencyAnswerId");
          var Find_SurveyAnswerId = "";
          if (SurveyQuestionShowHideDependencyQuestionId != "")
          {
            for (var b = 1; b < document.getElementById("FormView_PharmacySurveys_Form_HiddenField_" + FormMode + "ControlCount").value; b++)
            {
              var Find_SurveyQuestionId = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + b).getAttribute("SurveyQuestionId");

              if (SurveyQuestionShowHideDependencyQuestionId == Find_SurveyQuestionId)
              {
                for (var c = 0; c < document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + b).getElementsByTagName("input").length; c++)
                {
                  if (document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + b + "_" + c).checked == true)
                  {
                    Find_SurveyAnswerId = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + b + "_" + c).value;
                    break;
                  }
                }
              }
            }
            

            if (SurveyQuestionShowHideDependencyAnswerId == Find_SurveyAnswerId)
            {
              Show("FormView_PharmacySurveys_Form_ShowHideSurveyQuestion_" + a);
            }
            else
            {
              Hide("FormView_PharmacySurveys_Form_ShowHideSurveyQuestion_" + a);

              for (var d = 0; d < document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + a).getElementsByTagName("input").length; d++)
              {
                document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + a + "_" + d).checked = false;
              }
            }
          }
        }
      }
      else
      {
        var SurveyQuestionId = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + Control).getAttribute("SurveyQuestionId");
        var SurveyQuestionShowHideDependencyQuestionId = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + Control).getAttribute("SurveyQuestionShowHideDependencyQuestionId");
        var SurveyQuestionShowHideDependencyAnswerId = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + Control).getAttribute("SurveyQuestionShowHideDependencyAnswerId");
        var SurveyAnswerId;
        for (var a = 0; a < document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + Control).getElementsByTagName("input").length; a++)
        {
          if (document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + Control + "_" + a).checked == true)
          {
            SurveyAnswerId = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + Control + "_" + a).value;
            break;
          }
        }

        for (var b = 1; b < document.getElementById("FormView_PharmacySurveys_Form_HiddenField_" + FormMode + "ControlCount").value; b++)
        {
          var Find_SurveyQuestionId = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + b).getAttribute("SurveyQuestionId");
          var Find_SurveyQuestionShowHideDependencyQuestionId = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + b).getAttribute("SurveyQuestionShowHideDependencyQuestionId");
          var Find_SurveyQuestionShowHideDependencyAnswerId = document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + b).getAttribute("SurveyQuestionShowHideDependencyAnswerId");

          if (SurveyQuestionId == Find_SurveyQuestionShowHideDependencyQuestionId)
          {
            if (SurveyAnswerId == Find_SurveyQuestionShowHideDependencyAnswerId)
            {
              Show("FormView_PharmacySurveys_Form_ShowHideSurveyQuestion_" + b);
            }
            else
            {
              Hide("FormView_PharmacySurveys_Form_ShowHideSurveyQuestion_" + b);

              for (var c = 0; c < document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + b).getElementsByTagName("input").length; c++)
              {
                document.getElementById("FormView_PharmacySurveys_Form_RadioButtonList_" + FormMode + "SurveyQuestion_" + b + "_" + c).checked = false;
              }
            }
          }
        }

        Validation_Form();
      }
    }

    if (FormMode == "Item")
    {
      if (Control == undefined)
      {
        for (var a = 1; a < document.getElementById("FormView_PharmacySurveys_Form_HiddenField_" + FormMode + "ControlCount").value; a++)
        {
          var SurveyQuestionId = document.getElementById("FormView_PharmacySurveys_Form_Label_" + FormMode + "SurveyQuestion_" + a).getAttribute("SurveyQuestionId");
          var SurveyQuestionShowHideDependencyQuestionId = document.getElementById("FormView_PharmacySurveys_Form_Label_" + FormMode + "SurveyQuestion_" + a).getAttribute("SurveyQuestionShowHideDependencyQuestionId");
          var SurveyQuestionShowHideDependencyAnswerId = document.getElementById("FormView_PharmacySurveys_Form_Label_" + FormMode + "SurveyQuestion_" + a).getAttribute("SurveyQuestionShowHideDependencyAnswerId");
          var SurveyAnswerId = document.getElementById("FormView_PharmacySurveys_Form_Label_" + FormMode + "SurveyQuestion_" + a).getAttribute("SurveyAnswerId");
          var Find_SurveyAnswerId = "";
          if (SurveyQuestionShowHideDependencyQuestionId != "")
          {
            for (var b = 1; b < document.getElementById("FormView_PharmacySurveys_Form_HiddenField_" + FormMode + "ControlCount").value; b++)
            {
              var Find_SurveyQuestionId = document.getElementById("FormView_PharmacySurveys_Form_Label_" + FormMode + "SurveyQuestion_" + b).getAttribute("SurveyQuestionId");

              if (SurveyQuestionShowHideDependencyQuestionId == Find_SurveyQuestionId)
              {
                Find_SurveyAnswerId = document.getElementById("FormView_PharmacySurveys_Form_Label_" + FormMode + "SurveyQuestion_" + b).getAttribute("SurveyAnswerId");
                break;
              }
            }

            if (SurveyQuestionShowHideDependencyAnswerId == Find_SurveyAnswerId)
            {
              Show("FormView_PharmacySurveys_Form_ShowHideSurveyQuestion_" + a);
            }
            else
            {
              Hide("FormView_PharmacySurveys_Form_ShowHideSurveyQuestion_" + a);
            }
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