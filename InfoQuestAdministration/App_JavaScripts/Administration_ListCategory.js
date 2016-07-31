
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_ListCategory_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_ListCategory_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_ListCategory_Form_DropDownList_" + FormMode + "FormId").value == "")
    {
      document.getElementById("FormFormId").style.backgroundColor = "#d46e6e";
      document.getElementById("FormFormId").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormFormId").style.backgroundColor = "#77cf9c";
      document.getElementById("FormFormId").style.color = "#333333";
    }

    if (document.getElementById("FormView_ListCategory_Form_DropDownList_" + FormMode + "Parent").value == "")
    {
      document.getElementById("FormParent").style.backgroundColor = "#d46e6e";
      document.getElementById("FormParent").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormParent").style.backgroundColor = "#77cf9c";
      document.getElementById("FormParent").style.color = "#333333";
    }

    if (document.getElementById("FormView_ListCategory_Form_TextBox_" + FormMode + "Name").value == "")
    {
      document.getElementById("FormName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormName").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormName").style.color = "#333333";
    }

    if (document.getElementById("FormView_ListCategory_Form_TextBox_" + FormMode + "Description").value == "")
    {
      document.getElementById("FormDescription").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDescription").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormDescription").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDescription").style.color = "#333333";
    }

    if (document.getElementById("FormView_ListCategory_Form_CheckBox_" + FormMode + "LinkedCategory").checked == true)
    {
      if (document.getElementById("FormView_ListCategory_Form_DropDownList_" + FormMode + "LinkedCategoryList").value == "")
      {
        document.getElementById("FormLinkedCategoryList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormLinkedCategoryList").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormLinkedCategoryList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormLinkedCategoryList").style.color = "#333333";
      }

      if (document.getElementById("FormView_ListCategory_Form_DropDownList_" + FormMode + "LinkedCategoryList").value == "List Category")
      {
        if (document.getElementById("FormView_ListCategory_Form_DropDownList_" + FormMode + "LinkedCategoryListListCategoryParent").value == "")
        {
          document.getElementById("FormLinkedCategoryListListCategoryParent").style.backgroundColor = "#d46e6e";
          document.getElementById("FormLinkedCategoryListListCategoryParent").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormLinkedCategoryListListCategoryParent").style.backgroundColor = "#77cf9c";
          document.getElementById("FormLinkedCategoryListListCategoryParent").style.color = "#333333";
        }

        if (document.getElementById("FormView_ListCategory_Form_DropDownList_" + FormMode + "LinkedCategoryListListCategoryChild").value == "")
        {
          document.getElementById("FormLinkedCategoryListListCategoryChild").style.backgroundColor = "#d46e6e";
          document.getElementById("FormLinkedCategoryListListCategoryChild").style.color = "#333333";
        }
        else
        {
          document.getElementById("FormLinkedCategoryListListCategoryChild").style.backgroundColor = "#77cf9c";
          document.getElementById("FormLinkedCategoryListListCategoryChild").style.color = "#333333";
        }
      }
      else
      {
        document.getElementById("FormLinkedCategoryListListCategoryParent").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormLinkedCategoryListListCategoryParent").style.color = "#000000";
        document.getElementById("FormLinkedCategoryListListCategoryChild").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormLinkedCategoryListListCategoryChild").style.color = "#000000";
      }
    }
    else
    {
      document.getElementById("FormLinkedCategoryList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormLinkedCategoryList").style.color = "#000000";
      document.getElementById("FormLinkedCategoryListListCategoryParent").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormLinkedCategoryListListCategoryParent").style.color = "#000000";
      document.getElementById("FormLinkedCategoryListListCategoryChild").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormLinkedCategoryListListCategoryChild").style.color = "#000000";
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form()
{
  var FormMode;
  if (document.getElementById("FormView_ListCategory_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_ListCategory_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (FormMode != "Item")
    {
      if (document.getElementById("FormView_ListCategory_Form_CheckBox_" + FormMode + "LinkedCategory").checked == true)
      {
        Show("ShowHideLinkedCategoryList");

        if (document.getElementById("FormView_ListCategory_Form_DropDownList_" + FormMode + "LinkedCategoryList").value == "List Category")
        {
          Show("ShowHideLinkedCategoryListCategory1");
          Show("ShowHideLinkedCategoryListCategory2");
        }
        else
        {
          document.getElementById("FormView_ListCategory_Form_DropDownList_" + FormMode + "LinkedCategoryListListCategoryParent").value = "";
          document.getElementById("FormView_ListCategory_Form_DropDownList_" + FormMode + "LinkedCategoryListListCategoryChild").value = "";
          Hide("ShowHideLinkedCategoryListCategory1");
          Hide("ShowHideLinkedCategoryListCategory2");
        }        
      }
      else
      {
        document.getElementById("FormView_ListCategory_Form_DropDownList_" + FormMode + "LinkedCategoryList").value = "";
        Hide("ShowHideLinkedCategoryList");

        document.getElementById("FormView_ListCategory_Form_DropDownList_" + FormMode + "LinkedCategoryListListCategoryParent").value = "";
        document.getElementById("FormView_ListCategory_Form_DropDownList_" + FormMode + "LinkedCategoryListListCategoryChild").value = "";
        Hide("ShowHideLinkedCategoryListCategory1");
        Hide("ShowHideLinkedCategoryListCategory2");
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