
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --FormEmail-------------------------------------------------------------------------------------------------------------------------------------
function FormEmail(EmailLink) {
  var width = 750;
  var height = 700;
  var left = (screen.width - width) / 2;
  var top = (screen.height - height) / 2;
  window.open(EmailLink, 'Email', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=No , location=No , scrollbars=Yes , resizable=No , status=Yes , left=' + left + ' , top=' + top + ' ');
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --FormPrint-------------------------------------------------------------------------------------------------------------------------------------
function FormPrint(PrintLink) {
  var width = 750;
  var height = 700;
  var left = (screen.width - width) / 2;
  var top = (screen.height - height) / 2;
  window.open(PrintLink, 'Email', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=No , location=No , scrollbars=Yes , resizable=No , status=Yes , left=' + left + ' , top=' + top + ' ');
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form() {
  var FormMode;
  if (document.getElementById("FormView_Pharmacy_NewProduct_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_Pharmacy_NewProduct_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "Facility")) {
      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "Facility").value == "") {
        document.getElementById("FormFacility").style.backgroundColor = "#d46e6e";
        document.getElementById("FormFacility").style.color = "#333333";
      } else {
        document.getElementById("FormFacility").style.backgroundColor = "#77cf9c";
        document.getElementById("FormFacility").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "Date").value == "" || document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "Date").value == "yyyy/mm/dd") {
      document.getElementById("FormDate").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDate").style.color = "#333333";
    } else {
      document.getElementById("FormDate").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDate").style.color = "#333333";
    }

    if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "ProductClassificationList").value == "") {
      document.getElementById("FormProductClassificationList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormProductClassificationList").style.color = "#333333";
    } else {
      document.getElementById("FormProductClassificationList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormProductClassificationList").style.color = "#333333";
    }

    if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "ProductDescription").value == "") {
      document.getElementById("FormProductDescription").style.backgroundColor = "#d46e6e";
      document.getElementById("FormProductDescription").style.color = "#333333";
    } else {
      document.getElementById("FormProductDescription").style.backgroundColor = "#77cf9c";
      document.getElementById("FormProductDescription").style.color = "#333333";
    }

    if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "PackSize").value == "") {
      document.getElementById("FormPackSize").style.backgroundColor = "#d46e6e";
      document.getElementById("FormPackSize").style.color = "#333333";
    } else {
      document.getElementById("FormPackSize").style.backgroundColor = "#77cf9c";
      document.getElementById("FormPackSize").style.color = "#333333";
    }

    if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "ManufacturerList").value == "") {
      document.getElementById("FormManufacturerList").style.backgroundColor = "#d46e6e";
      document.getElementById("FormManufacturerList").style.color = "#333333";
    } else {
      document.getElementById("FormManufacturerList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormManufacturerList").style.color = "#333333";
    }

    if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "NappiCode").value == "" || document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "NappiCode").value == "______-___") {
      document.getElementById("FormNappiCode").style.backgroundColor = "#d46e6e";
      document.getElementById("FormNappiCode").style.color = "#333333";
    } else {
      document.getElementById("FormNappiCode").style.backgroundColor = "#77cf9c";
      document.getElementById("FormNappiCode").style.color = "#333333";
    }

    if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "NettPrice").value == "") {
      document.getElementById("FormNettPrice").style.backgroundColor = "#d46e6e";
      document.getElementById("FormNettPrice").style.color = "#333333";
    } else {
      document.getElementById("FormNettPrice").style.backgroundColor = "#77cf9c";
      document.getElementById("FormNettPrice").style.color = "#333333";
    }

    if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "FeedbackProgressStatusList")) {
      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "FeedbackProgressStatusList").value != "4333") {
        document.getElementById("FormFeedbackDescription").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormFeedbackDescription").style.color = "#333333";
      } else {
        if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "FeedbackDescription").value == "") {
          document.getElementById("FormFeedbackDescription").style.backgroundColor = "#d46e6e";
          document.getElementById("FormFeedbackDescription").style.color = "#333333";
        } else {
          document.getElementById("FormFeedbackDescription").style.backgroundColor = "#77cf9c";
          document.getElementById("FormFeedbackDescription").style.color = "#333333";
        }
      }
    }

    if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "ProductClassificationList").value == "4329") {
      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "TradeName").value == "") {
        document.getElementById("FormTradeName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormTradeName").style.color = "#333333";
      } else {
        document.getElementById("FormTradeName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormTradeName").style.color = "#333333";
      }

      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "ActiveIngredient").value == "") {
        document.getElementById("FormActiveIngredient").style.backgroundColor = "#d46e6e";
        document.getElementById("FormActiveIngredient").style.color = "#333333";
      } else {
        document.getElementById("FormActiveIngredient").style.backgroundColor = "#77cf9c";
        document.getElementById("FormActiveIngredient").style.color = "#333333";
      }

      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "Strength").value == "") {
        document.getElementById("FormStrength").style.backgroundColor = "#d46e6e";
        document.getElementById("FormStrength").style.color = "#333333";
      } else {
        document.getElementById("FormStrength").style.backgroundColor = "#77cf9c";
        document.getElementById("FormStrength").style.color = "#333333";
      }

      document.getElementById("FormSupplierCatalogNumber").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormSupplierCatalogNumber").style.color = "#333333";

      document.getElementById("FormProductRequestList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormProductRequestList").style.color = "#333333";

      document.getElementById("FormRequestByList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormRequestByList").style.color = "#333333";

      document.getElementById("FormUseList").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormUseList").style.color = "#333333";

      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "FormularyProduct").value == "") {
        document.getElementById("FormFormularyProduct").style.backgroundColor = "#d46e6e";
        document.getElementById("FormFormularyProduct").style.color = "#333333";
      } else {
        document.getElementById("FormFormularyProduct").style.backgroundColor = "#77cf9c";
        document.getElementById("FormFormularyProduct").style.color = "#333333";
      }

      document.getElementById("FormClinicalBenefit").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormClinicalBenefit").style.color = "#333333";

      document.getElementById("FormFinancialBenefit").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormFinancialBenefit").style.color = "#333333";
      
      document.getElementById("FormRequirement").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormRequirement").style.color = "#333333";
    } else if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "ProductClassificationList").value == "4330") {
      document.getElementById("FormTradeName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormTradeName").style.color = "#333333";

      document.getElementById("FormActiveIngredient").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormActiveIngredient").style.color = "#333333";

      document.getElementById("FormStrength").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormStrength").style.color = "#333333";

      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "SupplierCatalogNumber").value == "") {
        document.getElementById("FormSupplierCatalogNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormSupplierCatalogNumber").style.color = "#333333";
      } else {
        document.getElementById("FormSupplierCatalogNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormSupplierCatalogNumber").style.color = "#333333";
      }

      document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "ProductRequestList").value = "4338";
      document.getElementById("FormProductRequestList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormProductRequestList").style.color = "#333333";

      document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "RequestByList").value = "4340";
      document.getElementById("FormRequestByList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormRequestByList").style.color = "#333333";

      document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "UseList").value = "4336";
      document.getElementById("FormUseList").style.backgroundColor = "#77cf9c";
      document.getElementById("FormUseList").style.color = "#333333";

      document.getElementById("FormFormularyProduct").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormFormularyProduct").style.color = "#333333";

      document.getElementById("FormClinicalBenefit").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormClinicalBenefit").style.color = "#333333";

      document.getElementById("FormFinancialBenefit").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormFinancialBenefit").style.color = "#333333";

      document.getElementById("FormRequirement").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormRequirement").style.color = "#333333";      
    } else {
      document.getElementById("FormTradeName").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormTradeName").style.color = "#333333";

      document.getElementById("FormActiveIngredient").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormActiveIngredient").style.color = "#333333";

      document.getElementById("FormStrength").style.backgroundColor = "#f7f7f7";
      document.getElementById("FormStrength").style.color = "#333333";

      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "SupplierCatalogNumber").value == "") {
        document.getElementById("FormSupplierCatalogNumber").style.backgroundColor = "#d46e6e";
        document.getElementById("FormSupplierCatalogNumber").style.color = "#333333";
      } else {
        document.getElementById("FormSupplierCatalogNumber").style.backgroundColor = "#77cf9c";
        document.getElementById("FormSupplierCatalogNumber").style.color = "#333333";
      }

      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "ProductRequestList").value == "") {
        document.getElementById("FormProductRequestList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormProductRequestList").style.color = "#333333";
      } else {
        document.getElementById("FormProductRequestList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormProductRequestList").style.color = "#333333";
      }

      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "RequestByList").value == "") {
        document.getElementById("FormRequestByList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormRequestByList").style.color = "#333333";
      } else {
        document.getElementById("FormRequestByList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormRequestByList").style.color = "#333333";
      }

      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "UseList").value == "") {
        document.getElementById("FormUseList").style.backgroundColor = "#d46e6e";
        document.getElementById("FormUseList").style.color = "#333333";
      } else {
        document.getElementById("FormUseList").style.backgroundColor = "#77cf9c";
        document.getElementById("FormUseList").style.color = "#333333";
      }

      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "ProductRequestList").value == "4348") {
        document.getElementById("FormFormularyProduct").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormFormularyProduct").style.color = "#333333";

        document.getElementById("FormClinicalBenefit").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormClinicalBenefit").style.color = "#333333";

        document.getElementById("FormFinancialBenefit").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormFinancialBenefit").style.color = "#333333";

        document.getElementById("FormRequirement").style.backgroundColor = "#f7f7f7";
        document.getElementById("FormRequirement").style.color = "#333333";
      } else {
        if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "FormularyProduct").value == "") {
          document.getElementById("FormFormularyProduct").style.backgroundColor = "#d46e6e";
          document.getElementById("FormFormularyProduct").style.color = "#333333";
        } else {
          document.getElementById("FormFormularyProduct").style.backgroundColor = "#77cf9c";
          document.getElementById("FormFormularyProduct").style.color = "#333333";
        }

        if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "ClinicalBenefit").value == "") {
          document.getElementById("FormClinicalBenefit").style.backgroundColor = "#d46e6e";
          document.getElementById("FormClinicalBenefit").style.color = "#333333";
        } else {
          document.getElementById("FormClinicalBenefit").style.backgroundColor = "#77cf9c";
          document.getElementById("FormClinicalBenefit").style.color = "#333333";
        }

        if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "FinancialBenefit").value == "") {
          document.getElementById("FormFinancialBenefit").style.backgroundColor = "#d46e6e";
          document.getElementById("FormFinancialBenefit").style.color = "#333333";
        } else {
          document.getElementById("FormFinancialBenefit").style.backgroundColor = "#77cf9c";
          document.getElementById("FormFinancialBenefit").style.color = "#333333";
        }

        if (document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "Requirement").value == "") {
          document.getElementById("FormRequirement").style.backgroundColor = "#d46e6e";
          document.getElementById("FormRequirement").style.color = "#333333";
        } else {
          document.getElementById("FormRequirement").style.backgroundColor = "#77cf9c";
          document.getElementById("FormRequirement").style.color = "#333333";
        }
      }
    }    
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --ShowHide_Form---------------------------------------------------------------------------------------------------------------------------------
function ShowHide_Form() {
  var FormMode;
  if (document.getElementById("FormView_Pharmacy_NewProduct_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_Pharmacy_NewProduct_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else if (document.getElementById("FormView_Pharmacy_NewProduct_Form_HiddenField_Item")) {
    FormMode = "Item"
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (FormMode != "Item") {
      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "ProductClassificationList").value == "4329") {
        Show("PharmacyNewProductTradeName");
        Show("PharmacyNewProductActiveIngredient");
        Show("PharmacyNewProductStrength");

        Hide("PharmacyNewProductSupplierCatalogNumber");
        Hide("PharmacyNewProductProductRequestList");
        Hide("PharmacyNewProductRequestByList");
        Hide("PharmacyNewProductUseList");
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "SupplierCatalogNumber").value = "";
        document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "ProductRequestList").value = "";
        document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "RequestByList").value = "";
        document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "UseList").value = "";

        Show("PharmacyNewProductFormularyProduct1");
        if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "FormularyProduct").value == "True") {
          Show("PharmacyNewProductFormularyProduct2");
        } else {
          Hide("PharmacyNewProductFormularyProduct2");
          document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "FormularyProductDescription").value = "";
        }

        Show("PharmacyNewProductComment");

        Hide("PharmacyNewProductComparisonHeading1");
        Hide("PharmacyNewProductComparisonHeading2");
        Hide("PharmacyNewProductClinicalBenefit");
        Hide("PharmacyNewProductFinancialBenefit");
        Hide("PharmacyNewProductRequirement");
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "ClinicalBenefit").value = "";
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "FinancialBenefit").value = "";
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "Requirement").value = "";
      } else if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "ProductClassificationList").value == "4330") {
        Hide("PharmacyNewProductTradeName");
        Hide("PharmacyNewProductActiveIngredient");
        Hide("PharmacyNewProductStrength");
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "TradeName").value = "";
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "ActiveIngredient").value = "";
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "Strength").value = "";

        Show("PharmacyNewProductSupplierCatalogNumber");
        Show("PharmacyNewProductProductRequestList");
        Show("PharmacyNewProductRequestByList");
        Show("PharmacyNewProductUseList");

        Hide("PharmacyNewProductFormularyProduct1");
        Hide("PharmacyNewProductFormularyProduct2");
        document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "FormularyProduct").value = "";
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "FormularyProductDescription").value = "";

        Hide("PharmacyNewProductComment");
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "Comment").value = "";

        Hide("PharmacyNewProductComparisonHeading1");
        Hide("PharmacyNewProductComparisonHeading2");
        Hide("PharmacyNewProductClinicalBenefit");
        Hide("PharmacyNewProductFinancialBenefit");
        Hide("PharmacyNewProductRequirement");
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "ClinicalBenefit").value = "";
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "FinancialBenefit").value = "";
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "Requirement").value = "";
      } else {
        Hide("PharmacyNewProductTradeName");
        Hide("PharmacyNewProductActiveIngredient");
        Hide("PharmacyNewProductStrength");
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "TradeName").value = "";
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "ActiveIngredient").value = "";
        document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "Strength").value = "";

        Show("PharmacyNewProductSupplierCatalogNumber");
        Show("PharmacyNewProductProductRequestList");
        Show("PharmacyNewProductRequestByList");
        Show("PharmacyNewProductUseList");

        Show("PharmacyNewProductFormularyProduct1");
        if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "FormularyProduct").value == "True") {
          Show("PharmacyNewProductFormularyProduct2");
        } else {
          Hide("PharmacyNewProductFormularyProduct2");
          document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "FormularyProductDescription").value = "";
        }

        Show("PharmacyNewProductComment");

        Show("PharmacyNewProductClinicalBenefit");
        Show("PharmacyNewProductFinancialBenefit");
        Show("PharmacyNewProductRequirement");

        if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "ProductRequestList").value == "4348") {
          Hide("PharmacyNewProductFormularyProduct1");
          Hide("PharmacyNewProductFormularyProduct2");
          document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "FormularyProduct").value = "";
          document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "FormularyProductDescription").value = "";

          Hide("PharmacyNewProductComparisonHeading1");
          Hide("PharmacyNewProductComparisonHeading2");
          Hide("PharmacyNewProductClinicalBenefit");
          Hide("PharmacyNewProductFinancialBenefit");
          Hide("PharmacyNewProductRequirement");
          document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "ClinicalBenefit").value = "";
          document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "FinancialBenefit").value = "";
          document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "Requirement").value = "";

          Hide("PharmacyNewProductFile1");
          Hide("PharmacyNewProductFile2");
          Hide("PharmacyNewProductFile3");
          Hide("PharmacyNewProductFile4");
          Hide("PharmacyNewProductFile5");
          Hide("PharmacyNewProductFile6");
          Hide("PharmacyNewProductFile7");
          Hide("PharmacyNewProductFile8");
          Hide("PharmacyNewProductFile9");
          Hide("PharmacyNewProductFile10");
        } else {
          Show("PharmacyNewProductFormularyProduct1");
          if (document.getElementById("FormView_Pharmacy_NewProduct_Form_DropDownList_" + FormMode + "FormularyProduct").value == "True") {
            Show("PharmacyNewProductFormularyProduct2");
          } else {
            Hide("PharmacyNewProductFormularyProduct2");
            document.getElementById("FormView_Pharmacy_NewProduct_Form_TextBox_" + FormMode + "FormularyProductDescription").value = "";
          }

          Show("PharmacyNewProductComparisonHeading1");
          Show("PharmacyNewProductComparisonHeading2");
          Show("PharmacyNewProductClinicalBenefit");
          Show("PharmacyNewProductFinancialBenefit");
          Show("PharmacyNewProductRequirement");

          Show("PharmacyNewProductFile1");
          Show("PharmacyNewProductFile2");
          Show("PharmacyNewProductFile3");
          Show("PharmacyNewProductFile4");
          Show("PharmacyNewProductFile5");
          Show("PharmacyNewProductFile6");
          Show("PharmacyNewProductFile7");
          Show("PharmacyNewProductFile8");
          Show("PharmacyNewProductFile9");
          Show("PharmacyNewProductFile10");
        }
      }
    }

    if (FormMode == "Item") {
      if (document.getElementById("FormView_Pharmacy_NewProduct_Form_HiddenField_" + FormMode + "ProductClassificationList").value == "4329") {
        Show("PharmacyNewProductTradeName");
        Show("PharmacyNewProductActiveIngredient");
        Show("PharmacyNewProductStrength");

        Hide("PharmacyNewProductSupplierCatalogNumber");
        Hide("PharmacyNewProductProductRequestList");
        Hide("PharmacyNewProductRequestByList");
        Hide("PharmacyNewProductUseList");

        Show("PharmacyNewProductFormularyProduct1");
        if (document.getElementById("FormView_Pharmacy_NewProduct_Form_HiddenField_" + FormMode + "FormularyProduct").value == "True") {
          Show("PharmacyNewProductFormularyProduct2");
        } else {
          Hide("PharmacyNewProductFormularyProduct2");
        }

        Show("PharmacyNewProductComment");

        Hide("PharmacyNewProductComparisonHeading1");
        Hide("PharmacyNewProductComparisonHeading2");
        Hide("PharmacyNewProductClinicalBenefit");
        Hide("PharmacyNewProductFinancialBenefit");
        Hide("PharmacyNewProductRequirement");
      } else if (document.getElementById("FormView_Pharmacy_NewProduct_Form_HiddenField_" + FormMode + "ProductClassificationList").value == "4330") {
        Hide("PharmacyNewProductTradeName");
        Hide("PharmacyNewProductActiveIngredient");
        Hide("PharmacyNewProductStrength");

        Show("PharmacyNewProductSupplierCatalogNumber");
        Show("PharmacyNewProductProductRequestList");
        Show("PharmacyNewProductRequestByList");
        Show("PharmacyNewProductUseList");

        Hide("PharmacyNewProductFormularyProduct1");
        Hide("PharmacyNewProductFormularyProduct2");

        Hide("PharmacyNewProductComment");

        Hide("PharmacyNewProductComparisonHeading1");
        Hide("PharmacyNewProductComparisonHeading2");
        Hide("PharmacyNewProductClinicalBenefit");
        Hide("PharmacyNewProductFinancialBenefit");
        Hide("PharmacyNewProductRequirement");
      } else {
        Hide("PharmacyNewProductTradeName");
        Hide("PharmacyNewProductActiveIngredient");
        Hide("PharmacyNewProductStrength");

        Show("PharmacyNewProductSupplierCatalogNumber");
        Show("PharmacyNewProductProductRequestList");
        Show("PharmacyNewProductRequestByList");
        Show("PharmacyNewProductUseList");

        Show("PharmacyNewProductFormularyProduct1");
        if (document.getElementById("FormView_Pharmacy_NewProduct_Form_HiddenField_" + FormMode + "FormularyProduct").value == "True") {
          Show("PharmacyNewProductFormularyProduct2");
        } else {
          Hide("PharmacyNewProductFormularyProduct2");
        }

        Show("PharmacyNewProductComment");

        Show("PharmacyNewProductClinicalBenefit");
        Show("PharmacyNewProductFinancialBenefit");
        Show("PharmacyNewProductRequirement");

        if (document.getElementById("FormView_Pharmacy_NewProduct_Form_HiddenField_" + FormMode + "ProductRequestList").value == "4348") {
          Hide("PharmacyNewProductFormularyProduct1");
          Hide("PharmacyNewProductFormularyProduct2");

          Hide("PharmacyNewProductComparisonHeading1");
          Hide("PharmacyNewProductComparisonHeading2");
          Hide("PharmacyNewProductClinicalBenefit");
          Hide("PharmacyNewProductFinancialBenefit");
          Hide("PharmacyNewProductRequirement");

          Hide("PharmacyNewProductFile1");
          Hide("PharmacyNewProductFile2");
          Hide("PharmacyNewProductFile3");
          Hide("PharmacyNewProductFile4");
          Hide("PharmacyNewProductFile5");
          Hide("PharmacyNewProductFile6");
          Hide("PharmacyNewProductFile7");
          Hide("PharmacyNewProductFile8");
          Hide("PharmacyNewProductFile9");
          Hide("PharmacyNewProductFile10");
        } else {
          Show("PharmacyNewProductFormularyProduct1");
          if (document.getElementById("FormView_Pharmacy_NewProduct_Form_HiddenField_" + FormMode + "FormularyProduct").value == "True") {
            Show("PharmacyNewProductFormularyProduct2");
          } else {
            Hide("PharmacyNewProductFormularyProduct2");
          }

          Show("PharmacyNewProductComparisonHeading1");
          Show("PharmacyNewProductComparisonHeading2");
          Show("PharmacyNewProductClinicalBenefit");
          Show("PharmacyNewProductFinancialBenefit");
          Show("PharmacyNewProductRequirement");

          Show("PharmacyNewProductFile1");
          Show("PharmacyNewProductFile2");
          Show("PharmacyNewProductFile3");
          Show("PharmacyNewProductFile4");
          Show("PharmacyNewProductFile5");
          Show("PharmacyNewProductFile6");
          Show("PharmacyNewProductFile7");
          Show("PharmacyNewProductFile8");
          Show("PharmacyNewProductFile9");
          Show("PharmacyNewProductFile10");
        }
      }
    }
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Show------------------------------------------------------------------------------------------------------------------------------------------
function Show(id) {
  if (document.getElementById) {
    document.getElementById(id).style.display = '';
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Hide------------------------------------------------------------------------------------------------------------------------------------------
function Hide(id) {
  if (document.getElementById) {
    document.getElementById(id).style.display = 'none';
  }
}