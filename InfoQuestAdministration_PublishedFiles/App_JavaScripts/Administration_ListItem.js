
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_ListItem_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_ListItem_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  } else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_ListItem_Form_DropDownList_" + FormMode + "FormId").value == "")
    {
      document.getElementById("FormFormId").style.backgroundColor = "#d46e6e";
      document.getElementById("FormFormId").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormFormId").style.backgroundColor = "#77cf9c";
      document.getElementById("FormFormId").style.color = "#333333";
    }

    if (document.getElementById("FormView_ListItem_Form_DropDownList_" + FormMode + "ListCategoryId").value == "")
    {
      document.getElementById("FormListCategoryId").style.backgroundColor = "#d46e6e";
      document.getElementById("FormListCategoryId").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormListCategoryId").style.backgroundColor = "#77cf9c";
      document.getElementById("FormListCategoryId").style.color = "#333333";
    }

    if (document.getElementById("FormView_ListItem_Form_DropDownList_" + FormMode + "Parent").value == "")
    {
      document.getElementById("FormParent").style.backgroundColor = "#d46e6e";
      document.getElementById("FormParent").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormParent").style.backgroundColor = "#77cf9c";
      document.getElementById("FormParent").style.color = "#333333";
    }

    if (document.getElementById("FormView_ListItem_Form_TextBox_" + FormMode + "Name"))
    {
      if (document.getElementById("FormView_ListItem_Form_TextBox_" + FormMode + "Name").value == "")
      {
        document.getElementById("FormName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormName").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_ListItem_Form_DropDownList_" + FormMode + "Name_Facility"))
    {
      if (document.getElementById("FormView_ListItem_Form_DropDownList_" + FormMode + "Name_Facility").value == "")
      {
        document.getElementById("FormName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormName").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_ListItem_Form_DropDownList_" + FormMode + "Name_ListCategory"))
    {
      if (document.getElementById("FormView_ListItem_Form_DropDownList_" + FormMode + "Name_ListCategory").value == "")
      {
        document.getElementById("FormName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormName").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_ListItem_Form_DropDownList_" + FormMode + "Name_Unit"))
    {
      if (document.getElementById("FormView_ListItem_Form_DropDownList_" + FormMode + "Name_Unit").value == "")
      {
        document.getElementById("FormName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormName").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormName").style.color = "#333333";
      }
    }
  }
}
