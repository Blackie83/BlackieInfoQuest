
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form()
{
  var FormMode;
  if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "BusinessUnitComponentName").value == "")
    {
      document.getElementById("FormBusinessUnitComponentName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBusinessUnitComponentName").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBusinessUnitComponentName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBusinessUnitComponentName").style.color = "#333333";
    }

    if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitKey").value == "")
    {
      document.getElementById("FormBusinessUnitKey").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBusinessUnitKey").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBusinessUnitKey").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBusinessUnitKey").style.color = "#333333";
    }

    if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "")
    {
      document.getElementById("FormBusinessUnitComponentTypeKey").style.backgroundColor = "#d46e6e";
      document.getElementById("FormBusinessUnitComponentTypeKey").style.color = "#333333";
    }
    else
    {
      document.getElementById("FormBusinessUnitComponentTypeKey").style.backgroundColor = "#77cf9c";
      document.getElementById("FormBusinessUnitComponentTypeKey").style.color = "#333333";
    }

    if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "ParentBusinessUnitComponentKey").length == 1)
    {
      document.getElementById("FormParentBusinessUnitComponentKey").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormParentBusinessUnitComponentKey").style.color = "#000000";
    }
    else
    {
      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "ParentBusinessUnitComponentKey").value == "")
      {
        document.getElementById("FormParentBusinessUnitComponentKey").style.backgroundColor = "#d46e6e";
        document.getElementById("FormParentBusinessUnitComponentKey").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormParentBusinessUnitComponentKey").style.backgroundColor = "#77cf9c";
        document.getElementById("FormParentBusinessUnitComponentKey").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "1")
    {
      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "WardTypeKey").value == "")
      {
        document.getElementById("FormWardTypeKey").style.backgroundColor = "#d46e6e";
        document.getElementById("FormWardTypeKey").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormWardTypeKey").style.backgroundColor = "#77cf9c";
        document.getElementById("FormWardTypeKey").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_Ward").value == "")
      {
        document.getElementById("FormNursingDisciplineKey_Ward").style.backgroundColor = "#d46e6e";
        document.getElementById("FormNursingDisciplineKey_Ward").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormNursingDisciplineKey_Ward").style.backgroundColor = "#77cf9c";
        document.getElementById("FormNursingDisciplineKey_Ward").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "Entity").value == "")
      {
        document.getElementById("FormEntity").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEntity").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEntity").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEntity").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "CostCentre").value == "")
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
    }
    else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "7")
    {
      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "TheatreComplexTypeKey").value == "")
      {
        document.getElementById("FormTheatreComplexTypeKey").style.backgroundColor = "#d46e6e";
        document.getElementById("FormTheatreComplexTypeKey").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormTheatreComplexTypeKey").style.backgroundColor = "#77cf9c";
        document.getElementById("FormTheatreComplexTypeKey").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_TheatreComplex").value == "")
      {
        document.getElementById("FormNursingDisciplineKey_TheatreComplex").style.backgroundColor = "#d46e6e";
        document.getElementById("FormNursingDisciplineKey_TheatreComplex").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormNursingDisciplineKey_TheatreComplex").style.backgroundColor = "#77cf9c";
        document.getElementById("FormNursingDisciplineKey_TheatreComplex").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "Entity").value == "")
      {
        document.getElementById("FormEntity").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEntity").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEntity").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEntity").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "CostCentre").value == "")
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
    }
    else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "10")
    {
      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "OperatingTheatreTypeKey").value == "")
      {
        document.getElementById("FormOperatingTheatreTypeKey").style.backgroundColor = "#d46e6e";
        document.getElementById("FormOperatingTheatreTypeKey").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormOperatingTheatreTypeKey").style.backgroundColor = "#77cf9c";
        document.getElementById("FormOperatingTheatreTypeKey").style.color = "#333333";
      }
    }
    else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "4")
    {
      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "StockroomTypeKey").value == "")
      {
        document.getElementById("FormStockroomTypeKey").style.backgroundColor = "#d46e6e";
        document.getElementById("FormStockroomTypeKey").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormStockroomTypeKey").style.backgroundColor = "#77cf9c";
        document.getElementById("FormStockroomTypeKey").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "Entity").value == "")
      {
        document.getElementById("FormEntity").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEntity").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEntity").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEntity").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "CostCentre").value == "")
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
    }
    else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "6")
    {
      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "RetailPharmacyPracticeNumber").value == "")
      {
        document.getElementById("FormRetailPharmacyPracticeNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormRetailPharmacyPracticeNumber").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormRetailPharmacyPracticeNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormRetailPharmacyPracticeNumber").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "Entity").value == "")
      {
        document.getElementById("FormEntity").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEntity").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEntity").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEntity").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "CostCentre").value == "")
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
    }
    else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "11")
    {
      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "Entity").value == "")
      {
        document.getElementById("FormEntity").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEntity").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEntity").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEntity").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "CostCentre").value == "")
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
    }
    else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "5")
    {
      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "Entity").value == "")
      {
        document.getElementById("FormEntity").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEntity").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEntity").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEntity").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "CostCentre").value == "")
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
    }
    else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "12")
    {
      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "SupportFunctionTypeKey").value == "")
      {
        document.getElementById("FormSupportFunctionTypeKey").style.backgroundColor = "#d46e6e";
        document.getElementById("FormSupportFunctionTypeKey").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormSupportFunctionTypeKey").style.backgroundColor = "#77cf9c";
        document.getElementById("FormSupportFunctionTypeKey").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "Entity").value == "")
      {
        document.getElementById("FormEntity").style.backgroundColor = "#d46e6e";
        document.getElementById("FormEntity").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormEntity").style.backgroundColor = "#77cf9c";
        document.getElementById("FormEntity").style.color = "#333333";
      }

      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "CostCentre").value == "")
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#d46e6e";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
      else
      {
        document.getElementById("FormCostCentre").style.backgroundColor = "#77cf9c";
        document.getElementById("FormCostCentre").style.color = "#333333";
      }
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form(Control)
{
  var FormMode;
  if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_Insert"))
  {
    FormMode = "Insert";
  }
  else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_Edit"))
  {
    FormMode = "Edit";
  }
  else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_Item"))
  {
    FormMode = "Item"
  }
  else
  {
    FormMode = "";
  }

  if (FormMode != "")
  {
    if (FormMode != "Item")
    {
      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "1")
      {
        Show("BusinessUnitComponentTypeWard1");
        Show("BusinessUnitComponentTypeWard2");
        Show("BusinessUnitComponentTypeWard3");
        Show("BusinessUnitComponentTypeWard4");

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "TheatreComplexTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_TheatreComplex").value = "";

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "OperatingTheatreTypeKey").value = "";

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "StockroomTypeKey").value = "";

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "RetailPharmacyPracticeNumber").value = "";

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "SupportFunctionTypeKey").value = "";

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "7")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "WardTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_Ward").value = "";

        Show("BusinessUnitComponentTypeTheatreComplex1");
        Show("BusinessUnitComponentTypeTheatreComplex2");
        Show("BusinessUnitComponentTypeTheatreComplex3");
        Show("BusinessUnitComponentTypeTheatreComplex4");

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "OperatingTheatreTypeKey").value = "";

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "StockroomTypeKey").value = "";

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "RetailPharmacyPracticeNumber").value = "";

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "SupportFunctionTypeKey").value = "";

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "10")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "WardTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_Ward").value = "";

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "TheatreComplexTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_TheatreComplex").value = "";

        Show("BusinessUnitComponentTypeOperatingTheatre1");
        Show("BusinessUnitComponentTypeOperatingTheatre2");
        Show("BusinessUnitComponentTypeOperatingTheatre3");

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "StockroomTypeKey").value = "";

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "RetailPharmacyPracticeNumber").value = "";

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "SupportFunctionTypeKey").value = "";

        Hide("BusinessUnitComponentTypeOther1");
        Hide("BusinessUnitComponentTypeOther2");
        Hide("BusinessUnitComponentTypeOther3");
        Hide("BusinessUnitComponentTypeOther4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "CostCentre").value = "";
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "4")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "WardTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_Ward").value = "";

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "TheatreComplexTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_TheatreComplex").value = "";

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "OperatingTheatreTypeKey").value = "";

        Show("BusinessUnitComponentTypeStockroom1");
        Show("BusinessUnitComponentTypeStockroom2");
        Show("BusinessUnitComponentTypeStockroom3");

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "RetailPharmacyPracticeNumber").value = "";

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "SupportFunctionTypeKey").value = "";

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "6")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "WardTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_Ward").value = "";

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "TheatreComplexTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_TheatreComplex").value = "";

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "OperatingTheatreTypeKey").value = "";

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "StockroomTypeKey").value = "";

        Show("BusinessUnitComponentTypeRetailPharmacy1");
        Show("BusinessUnitComponentTypeRetailPharmacy2");
        Show("BusinessUnitComponentTypeRetailPharmacy3");

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "SupportFunctionTypeKey").value = "";

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "5")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "WardTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_Ward").value = "";

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "TheatreComplexTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_TheatreComplex").value = "";

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "OperatingTheatreTypeKey").value = "";

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "StockroomTypeKey").value = "";

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "RetailPharmacyPracticeNumber").value = "";

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "SupportFunctionTypeKey").value = "";

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "11")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "WardTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_Ward").value = "";

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "TheatreComplexTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_TheatreComplex").value = "";

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "OperatingTheatreTypeKey").value = "";

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "StockroomTypeKey").value = "";

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "RetailPharmacyPracticeNumber").value = "";

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "SupportFunctionTypeKey").value = "";

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "BusinessUnitComponentTypeKey").value == "12")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "WardTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_Ward").value = "";

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "TheatreComplexTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_TheatreComplex").value = "";

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "OperatingTheatreTypeKey").value = "";

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "StockroomTypeKey").value = "";

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "RetailPharmacyPracticeNumber").value = "";

        Show("BusinessUnitComponentTypeSupportFunction1");
        Show("BusinessUnitComponentTypeSupportFunction2");
        Show("BusinessUnitComponentTypeSupportFunction3");

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "WardTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_Ward").value = "";

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "TheatreComplexTypeKey").value = "";
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "NursingDisciplineKey_TheatreComplex").value = "";

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "OperatingTheatreTypeKey").value = "";

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "StockroomTypeKey").value = "";

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "RetailPharmacyPracticeNumber").value = "";

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_DropDownList_" + FormMode + "SupportFunctionTypeKey").value = "";

        Hide("BusinessUnitComponentTypeOther1");
        Hide("BusinessUnitComponentTypeOther2");
        Hide("BusinessUnitComponentTypeOther3");
        Hide("BusinessUnitComponentTypeOther4");
        document.getElementById("FormView_FSM_BusinessUnitComponent_Form_TextBox_" + FormMode + "CostCentre").value = "";
      }
    }

    if (FormMode == "Item")
    {
      if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_" + FormMode + "BusinessUnitComponentTypeKey").value == "1")
      {
        Show("BusinessUnitComponentTypeWard1");
        Show("BusinessUnitComponentTypeWard2");
        Show("BusinessUnitComponentTypeWard3");
        Show("BusinessUnitComponentTypeWard4");

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_" + FormMode + "BusinessUnitComponentTypeKey").value == "7")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");

        Show("BusinessUnitComponentTypeTheatreComplex1");
        Show("BusinessUnitComponentTypeTheatreComplex2");
        Show("BusinessUnitComponentTypeTheatreComplex3");
        Show("BusinessUnitComponentTypeTheatreComplex4");

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_" + FormMode + "BusinessUnitComponentTypeKey").value == "10")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");

        Show("BusinessUnitComponentTypeOperatingTheatre1");
        Show("BusinessUnitComponentTypeOperatingTheatre2");
        Show("BusinessUnitComponentTypeOperatingTheatre3");

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");

        Hide("BusinessUnitComponentTypeOther1");
        Hide("BusinessUnitComponentTypeOther2");
        Hide("BusinessUnitComponentTypeOther3");
        Hide("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_" + FormMode + "BusinessUnitComponentTypeKey").value == "4")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");

        Show("BusinessUnitComponentTypeStockroom1");
        Show("BusinessUnitComponentTypeStockroom2");
        Show("BusinessUnitComponentTypeStockroom3");

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_" + FormMode + "BusinessUnitComponentTypeKey").value == "6")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");

        Show("BusinessUnitComponentTypeRetailPharmacy1");
        Show("BusinessUnitComponentTypeRetailPharmacy2");
        Show("BusinessUnitComponentTypeRetailPharmacy3");

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_" + FormMode + "BusinessUnitComponentTypeKey").value == "5")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_" + FormMode + "BusinessUnitComponentTypeKey").value == "11")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else if (document.getElementById("FormView_FSM_BusinessUnitComponent_Form_HiddenField_" + FormMode + "BusinessUnitComponentTypeKey").value == "12")
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");

        Show("BusinessUnitComponentTypeSupportFunction1");
        Show("BusinessUnitComponentTypeSupportFunction2");
        Show("BusinessUnitComponentTypeSupportFunction3");

        Show("BusinessUnitComponentTypeOther1");
        Show("BusinessUnitComponentTypeOther2");
        Show("BusinessUnitComponentTypeOther3");
        Show("BusinessUnitComponentTypeOther4");
      }
      else
      {
        Hide("BusinessUnitComponentTypeWard1");
        Hide("BusinessUnitComponentTypeWard2");
        Hide("BusinessUnitComponentTypeWard3");
        Hide("BusinessUnitComponentTypeWard4");

        Hide("BusinessUnitComponentTypeTheatreComplex1");
        Hide("BusinessUnitComponentTypeTheatreComplex2");
        Hide("BusinessUnitComponentTypeTheatreComplex3");
        Hide("BusinessUnitComponentTypeTheatreComplex4");

        Hide("BusinessUnitComponentTypeOperatingTheatre1");
        Hide("BusinessUnitComponentTypeOperatingTheatre2");
        Hide("BusinessUnitComponentTypeOperatingTheatre3");

        Hide("BusinessUnitComponentTypeStockroom1");
        Hide("BusinessUnitComponentTypeStockroom2");
        Hide("BusinessUnitComponentTypeStockroom3");

        Hide("BusinessUnitComponentTypeRetailPharmacy1");
        Hide("BusinessUnitComponentTypeRetailPharmacy2");
        Hide("BusinessUnitComponentTypeRetailPharmacy3");

        Hide("BusinessUnitComponentTypeSupportFunction1");
        Hide("BusinessUnitComponentTypeSupportFunction2");
        Hide("BusinessUnitComponentTypeSupportFunction3");

        Hide("BusinessUnitComponentTypeOther1");
        Hide("BusinessUnitComponentTypeOther2");
        Hide("BusinessUnitComponentTypeOther3");
        Hide("BusinessUnitComponentTypeOther4");
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

