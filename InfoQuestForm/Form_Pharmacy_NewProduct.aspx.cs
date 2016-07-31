using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_Pharmacy_NewProduct : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSourceSetup();

        HiddenField HiddenField_InsertNewProductFileTemp;
        using (HiddenField_InsertNewProductFileTemp = new HiddenField())
        {
          if (Request.QueryString["NewProduct_Id"] == null)
          {
            HiddenField_InsertNewProductFileTemp = (HiddenField)FormView_Pharmacy_NewProduct_Form.FindControl("HiddenField_InsertNewProductFileTemp");
            if (string.IsNullOrEmpty(HiddenField_InsertNewProductFileTemp.Value))
            {
              HiddenField_InsertNewProductFileTemp.Value = "TEMP_ID:USER:" + Request.ServerVariables["LOGON_USER"].ToUpper(CultureInfo.CurrentCulture) + ":DATE:" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss", CultureInfo.CurrentCulture).ToUpper(CultureInfo.CurrentCulture) + "";
            }
          }
        }

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          SqlDataSource_Pharmacy_NewProduct_File_InsertFieldMissing.SelectParameters["NewProduct_File_Temp_NewProduct_Id"].DefaultValue = HiddenField_InsertNewProductFileTemp.Value;
          SqlDataSource_Pharmacy_NewProduct_File_InsertFile.SelectParameters["NewProduct_File_Temp_NewProduct_Id"].DefaultValue = HiddenField_InsertNewProductFileTemp.Value;

          Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("33")).ToString();
          Label_FormHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("33")).ToString();

          if (Request.QueryString["NewProduct_Id"] != null)
          {
            SqlDataSource_Pharmacy_NewProduct_InsertFacility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
            SqlDataSource_Pharmacy_NewProduct_InsertFacility.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_Pharmacy_NewProduct";
            SqlDataSource_Pharmacy_NewProduct_InsertFacility.SelectParameters["TableWHERE"].DefaultValue = "NewProduct_Id = " + Request.QueryString["NewProduct_Id"] + "";
          }

          TableForm.Visible = true;

          SetFormVisibility();

          TableFormVisible();

          NewProductFileCleanUp();
        }
      }
    }

    private string PageSecurity()
    {
      string SecurityAllow = "0";

      string SecurityAllowAdministration = "0";

      SecurityAllowAdministration = InfoQuestWCF.InfoQuest_Security.Security_Form_Administration(Request.ServerVariables["LOGON_USER"]);

      if (SecurityAllowAdministration == "1")
      {
        SecurityAllow = "1";
      }
      else
      {
        string SecurityAllowForm = "0";

        string SQLStringSecurity = "";
        if (Request.QueryString["NewProduct_Id"] == null)
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('33'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('33')) AND (Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_Pharmacy_NewProduct WHERE NewProduct_Id = @NewProduct_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@NewProduct_Id", Request.QueryString["NewProduct_Id"]);

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          SecurityAllow = "0";
          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("No Access", "InfoQuest_PageText.aspx?PageTextValue=5"), false);
          Response.End();
        }
      }

      return SecurityAllow;
    }

    protected void Page_Error(object sender, EventArgs e)
    {
      Exception Exception_Error = Server.GetLastError().GetBaseException();
      Server.ClearError();

      InfoQuestWCF.InfoQuest_Exceptions.Exceptions(Exception_Error, Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"], "");
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Pharmacy: New Product Code Request", "6");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Pharmacy_NewProduct_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertCommand="INSERT INTO [InfoQuest_Form_Pharmacy_NewProduct] (Facility_Id, NewProduct_ReportNumber, NewProduct_Date, NewProduct_ProductClassification_List, NewProduct_Manufacturer_List, NewProduct_ProductDescription, NewProduct_TradeName ,	NewProduct_ActiveIngredient ,	NewProduct_Strength , NewProduct_PackSize, NewProduct_NettPrice, NewProduct_Use_List, NewProduct_SupplierCatalogNumber, NewProduct_NappiCode, NewProduct_ProductRequest_List, NewProduct_RequestBy_List, NewProduct_FormularyProduct, NewProduct_FormularyProductDescription, NewProduct_Comment, NewProduct_ClinicalBenefit, NewProduct_FinancialBenefit, NewProduct_Requirement, NewProduct_Feedback_Description, NewProduct_Feedback_ProgressStatus_List, NewProduct_Feedback_Date, NewProduct_CreatedDate, NewProduct_CreatedBy, NewProduct_ModifiedDate, NewProduct_ModifiedBy, NewProduct_History, NewProduct_IsActive, Form_Emailed) VALUES (@Facility_Id, @NewProduct_ReportNumber, @NewProduct_Date, @NewProduct_ProductClassification_List, @NewProduct_Manufacturer_List, @NewProduct_ProductDescription, @NewProduct_TradeName ,	@NewProduct_ActiveIngredient ,	@NewProduct_Strength , @NewProduct_PackSize, @NewProduct_NettPrice, @NewProduct_Use_List, @NewProduct_SupplierCatalogNumber, @NewProduct_NappiCode, @NewProduct_ProductRequest_List, @NewProduct_RequestBy_List, @NewProduct_FormularyProduct, @NewProduct_FormularyProductDescription, @NewProduct_Comment, @NewProduct_ClinicalBenefit, @NewProduct_FinancialBenefit, @NewProduct_Requirement, @NewProduct_Feedback_Description, @NewProduct_Feedback_ProgressStatus_List, @NewProduct_Feedback_Date, @NewProduct_CreatedDate, @NewProduct_CreatedBy, @NewProduct_ModifiedDate, @NewProduct_ModifiedBy, @NewProduct_History, @NewProduct_IsActive, @Form_Emailed); SELECT @NewProduct_Id = SCOPE_IDENTITY()";
      SqlDataSource_Pharmacy_NewProduct_Form.SelectCommand="SELECT * FROM [InfoQuest_Form_Pharmacy_NewProduct] WHERE ([NewProduct_Id] = @NewProduct_Id)";
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateCommand="UPDATE [InfoQuest_Form_Pharmacy_NewProduct] SET NewProduct_Date = @NewProduct_Date, NewProduct_ProductClassification_List = @NewProduct_ProductClassification_List, NewProduct_Manufacturer_List = @NewProduct_Manufacturer_List, NewProduct_ProductDescription = @NewProduct_ProductDescription, NewProduct_TradeName = @NewProduct_TradeName,	NewProduct_ActiveIngredient = @NewProduct_ActiveIngredient,	NewProduct_Strength = @NewProduct_Strength, NewProduct_PackSize = @NewProduct_PackSize, NewProduct_NettPrice = @NewProduct_NettPrice, NewProduct_Use_List = @NewProduct_Use_List, NewProduct_SupplierCatalogNumber = @NewProduct_SupplierCatalogNumber, NewProduct_NappiCode = @NewProduct_NappiCode, NewProduct_ProductRequest_List = @NewProduct_ProductRequest_List, NewProduct_RequestBy_List = @NewProduct_RequestBy_List, NewProduct_FormularyProduct = @NewProduct_FormularyProduct, NewProduct_FormularyProductDescription = @NewProduct_FormularyProductDescription, NewProduct_Comment = @NewProduct_Comment, NewProduct_ClinicalBenefit = @NewProduct_ClinicalBenefit, NewProduct_FinancialBenefit = @NewProduct_FinancialBenefit, NewProduct_Requirement = @NewProduct_Requirement, NewProduct_Feedback_Description = @NewProduct_Feedback_Description, NewProduct_Feedback_ProgressStatus_List = @NewProduct_Feedback_ProgressStatus_List, NewProduct_Feedback_Date = @NewProduct_Feedback_Date, NewProduct_ModifiedDate = @NewProduct_ModifiedDate, NewProduct_ModifiedBy = @NewProduct_ModifiedBy, NewProduct_History = @NewProduct_History, NewProduct_IsActive = @NewProduct_IsActive WHERE [NewProduct_Id] = @NewProduct_Id";
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_Id", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("Facility_Id", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_ReportNumber", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_Date", TypeCode.DateTime, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_ProductClassification_List", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_Manufacturer_List", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_ProductDescription", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_TradeName", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_ActiveIngredient", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_Strength", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_PackSize", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_NettPrice", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_Use_List", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_SupplierCatalogNumber", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_NappiCode", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_ProductRequest_List", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_RequestBy_List", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_FormularyProduct", TypeCode.Boolean, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_FormularyProductDescription", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_Comment", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_ClinicalBenefit", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_FinancialBenefit", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_Requirement", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_Feedback_Description", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_Feedback_ProgressStatus_List", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_Feedback_Date", TypeCode.DateTime, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_CreatedBy", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_History", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("NewProduct_IsActive", TypeCode.Boolean, "");
      SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters.Add("Form_Emailed", TypeCode.Boolean, "");
      SqlDataSource_Pharmacy_NewProduct_Form.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_Form.SelectParameters.Add("NewProduct_Id", TypeCode.Int32, Request.QueryString["NewProduct_Id"]);
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_Date", TypeCode.DateTime, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_ProductClassification_List", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_Manufacturer_List", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_ProductDescription", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_TradeName", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_ActiveIngredient", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_Strength", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_PackSize", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_NettPrice", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_Use_List", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_SupplierCatalogNumber", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_NappiCode", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_ProductRequest_List", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_RequestBy_List", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_FormularyProduct", TypeCode.Boolean, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_FormularyProductDescription", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_Comment", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_ClinicalBenefit", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_FinancialBenefit", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_Requirement", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_Feedback_Description", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_Feedback_ProgressStatus_List", TypeCode.Int32, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_Feedback_Date", TypeCode.DateTime, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_History", TypeCode.String, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_IsActive", TypeCode.Boolean, "");
      SqlDataSource_Pharmacy_NewProduct_Form.UpdateParameters.Add("NewProduct_Id", TypeCode.Int32, "");

      SqlDataSource_Pharmacy_NewProduct_InsertFacility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_InsertFacility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Pharmacy_NewProduct_InsertFacility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Pharmacy_NewProduct_InsertFacility.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_InsertFacility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Pharmacy_NewProduct_InsertFacility.SelectParameters.Add("Form_Id", TypeCode.String, "33");
      SqlDataSource_Pharmacy_NewProduct_InsertFacility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Pharmacy_NewProduct_InsertFacility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Pharmacy_NewProduct_InsertFacility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Pharmacy_NewProduct_InsertFacility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Pharmacy_NewProduct_InsertProductClassificationList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_InsertProductClassificationList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 98 AND ListItem_Parent = -1 ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_Pharmacy_NewProduct_InsertProductClassificationList.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_NewProduct_InsertManufacturerList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_InsertManufacturerList.SelectCommand = "SELECT * FROM ( SELECT Pharmacy_Supplier_Lookup_Id , Pharmacy_Supplier_Lookup_Description + ' (' + ISNULL(Pharmacy_Supplier_Lookup_Code,'') + ')' AS Pharmacy_Supplier_Lookup_Description FROM Form_Pharmacy_Supplier_Lookup WHERE Pharmacy_Supplier_Lookup_IsActive = 1 ) AS TempTableAll ORDER BY TempTableAll.Pharmacy_Supplier_Lookup_Description";
      SqlDataSource_Pharmacy_NewProduct_InsertManufacturerList.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_NewProduct_InsertProductRequestList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_InsertProductRequestList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 101 AND ListItem_Parent = -1 ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_Pharmacy_NewProduct_InsertProductRequestList.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_NewProduct_InsertRequestByList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_InsertRequestByList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 102 AND ListItem_Parent = -1 ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_Pharmacy_NewProduct_InsertRequestByList.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_NewProduct_InsertUseList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_InsertUseList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 100 AND ListItem_Parent = -1 ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_Pharmacy_NewProduct_InsertUseList.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_NewProduct_InsertFeedbackProgressStatusList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_InsertFeedbackProgressStatusList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 99 AND ListItem_Parent = -1 ) AS TempTableAll ORDER BY CASE WHEN LEFT(ListItem_Name,8) = 'Captured' THEN '000' + ListItem_Name ELSE ListItem_Name END";
      SqlDataSource_Pharmacy_NewProduct_InsertFeedbackProgressStatusList.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_NewProduct_File_InsertField.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_File_InsertField.SelectCommand = "SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 103 AND ListItem_Parent = -1 ORDER BY ListItem_Name";
      SqlDataSource_Pharmacy_NewProduct_File_InsertField.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_NewProduct_File_InsertFieldMissing.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_File_InsertFieldMissing.SelectCommand = "SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 103 AND ListItem_Parent = -1 AND ListItem_Id NOT IN (SELECT NewProduct_File_Field_List FROM InfoQuest_Form_Pharmacy_NewProduct_File WHERE NewProduct_File_CreatedBy = @NewProduct_File_CreatedBy AND NewProduct_File_Temp_NewProduct_Id = @NewProduct_File_Temp_NewProduct_Id) ORDER BY ListItem_Name";
      SqlDataSource_Pharmacy_NewProduct_File_InsertFieldMissing.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Pharmacy_NewProduct_File_InsertFieldMissing.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_File_InsertFieldMissing.SelectParameters.Add("NewProduct_File_CreatedBy", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Pharmacy_NewProduct_File_InsertFieldMissing.SelectParameters.Add("NewProduct_File_Temp_NewProduct_Id", TypeCode.String, "");

      SqlDataSource_Pharmacy_NewProduct_File_InsertFile.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_File_InsertFile.SelectCommand = "SELECT NewProduct_File_Id , NewProduct_File_Field_Name , NewProduct_File_Name , NewProduct_File_CreatedDate , NewProduct_File_CreatedBy FROM vForm_Pharmacy_NewProduct_File WHERE NewProduct_File_CreatedBy = @NewProduct_File_CreatedBy AND NewProduct_File_Temp_NewProduct_Id = @NewProduct_File_Temp_NewProduct_Id ORDER BY NewProduct_File_Field_Name";
      SqlDataSource_Pharmacy_NewProduct_File_InsertFile.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Pharmacy_NewProduct_File_InsertFile.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_File_InsertFile.SelectParameters.Add("NewProduct_File_CreatedBy", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Pharmacy_NewProduct_File_InsertFile.SelectParameters.Add("NewProduct_File_Temp_NewProduct_Id", TypeCode.String, "");

      SqlDataSource_Pharmacy_NewProduct_EditProductClassificationList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_EditProductClassificationList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 98 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_Pharmacy_NewProduct.NewProduct_ProductClassification_List,Administration_ListItem.ListItem_Name FROM InfoQuest_Form_Pharmacy_NewProduct , Administration_ListItem WHERE InfoQuest_Form_Pharmacy_NewProduct.NewProduct_ProductClassification_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Id = @NewProduct_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_Pharmacy_NewProduct_EditProductClassificationList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Pharmacy_NewProduct_EditProductClassificationList.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_EditProductClassificationList.SelectParameters.Add("NewProduct_Id", TypeCode.String, Request.QueryString["NewProduct_Id"]);

      SqlDataSource_Pharmacy_NewProduct_EditManufacturerList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_EditManufacturerList.SelectCommand = "SELECT * FROM ( SELECT Pharmacy_Supplier_Lookup_Id , Pharmacy_Supplier_Lookup_Description + ' (' + ISNULL(Pharmacy_Supplier_Lookup_Code,'') + ')' AS Pharmacy_Supplier_Lookup_Description FROM Form_Pharmacy_Supplier_Lookup WHERE Pharmacy_Supplier_Lookup_IsActive = 1 UNION SELECT InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Manufacturer_List , Pharmacy_Supplier_Lookup_Description + ' (' + ISNULL(Pharmacy_Supplier_Lookup_Code,'') + ')' AS Pharmacy_Supplier_Lookup_Description FROM InfoQuest_Form_Pharmacy_NewProduct LEFT JOIN Form_Pharmacy_Supplier_Lookup ON InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Manufacturer_List = Form_Pharmacy_Supplier_Lookup.Pharmacy_Supplier_Lookup_Id WHERE InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Id = @NewProduct_Id ) AS TempTableAll ORDER BY TempTableAll.Pharmacy_Supplier_Lookup_Description";
      SqlDataSource_Pharmacy_NewProduct_EditManufacturerList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Pharmacy_NewProduct_EditManufacturerList.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_EditManufacturerList.SelectParameters.Add("NewProduct_Id", TypeCode.String, Request.QueryString["NewProduct_Id"]);

      SqlDataSource_Pharmacy_NewProduct_EditProductRequestList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_EditProductRequestList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 101 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_Pharmacy_NewProduct.NewProduct_ProductRequest_List,Administration_ListItem.ListItem_Name FROM InfoQuest_Form_Pharmacy_NewProduct , Administration_ListItem WHERE InfoQuest_Form_Pharmacy_NewProduct.NewProduct_ProductRequest_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Id = @NewProduct_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_Pharmacy_NewProduct_EditProductRequestList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Pharmacy_NewProduct_EditProductRequestList.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_EditProductRequestList.SelectParameters.Add("NewProduct_Id", TypeCode.String, Request.QueryString["NewProduct_Id"]);

      SqlDataSource_Pharmacy_NewProduct_EditRequestByList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_EditRequestByList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 102 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_Pharmacy_NewProduct.NewProduct_RequestBy_List,Administration_ListItem.ListItem_Name FROM InfoQuest_Form_Pharmacy_NewProduct , Administration_ListItem WHERE InfoQuest_Form_Pharmacy_NewProduct.NewProduct_RequestBy_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Id = @NewProduct_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_Pharmacy_NewProduct_EditRequestByList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Pharmacy_NewProduct_EditRequestByList.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_EditRequestByList.SelectParameters.Add("NewProduct_Id", TypeCode.String, Request.QueryString["NewProduct_Id"]);

      SqlDataSource_Pharmacy_NewProduct_EditUseList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_EditUseList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 100 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Use_List,Administration_ListItem.ListItem_Name FROM InfoQuest_Form_Pharmacy_NewProduct , Administration_ListItem WHERE InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Use_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Id = @NewProduct_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_Pharmacy_NewProduct_EditUseList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Pharmacy_NewProduct_EditUseList.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_EditUseList.SelectParameters.Add("NewProduct_Id", TypeCode.String, Request.QueryString["NewProduct_Id"]);

      SqlDataSource_Pharmacy_NewProduct_EditFeedbackProgressStatusList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_EditFeedbackProgressStatusList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 99 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Feedback_ProgressStatus_List,Administration_ListItem.ListItem_Name FROM InfoQuest_Form_Pharmacy_NewProduct , Administration_ListItem WHERE InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Feedback_ProgressStatus_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Id = @NewProduct_Id ) AS TempTableAll ORDER BY CASE WHEN LEFT(ListItem_Name,8) = 'Captured' THEN '000' + ListItem_Name ELSE ListItem_Name END";
      SqlDataSource_Pharmacy_NewProduct_EditFeedbackProgressStatusList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Pharmacy_NewProduct_EditFeedbackProgressStatusList.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_EditFeedbackProgressStatusList.SelectParameters.Add("NewProduct_Id", TypeCode.String, Request.QueryString["NewProduct_Id"]);

      SqlDataSource_Pharmacy_NewProduct_File_EditField.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_File_EditField.SelectCommand = "SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 103 AND ListItem_Parent = -1 ORDER BY ListItem_Name";
      SqlDataSource_Pharmacy_NewProduct_File_EditField.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_NewProduct_File_EditFieldMissing.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_File_EditFieldMissing.SelectCommand = "SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 103 AND ListItem_Parent = -1 AND ListItem_Id NOT IN (SELECT NewProduct_File_Field_List FROM InfoQuest_Form_Pharmacy_NewProduct_File WHERE NewProduct_Id = @NewProduct_Id) ORDER BY ListItem_Name";
      SqlDataSource_Pharmacy_NewProduct_File_EditFieldMissing.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Pharmacy_NewProduct_File_EditFieldMissing.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_File_EditFieldMissing.SelectParameters.Add("NewProduct_Id", TypeCode.String, Request.QueryString["NewProduct_Id"]);

      SqlDataSource_Pharmacy_NewProduct_File_EditFile.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_File_EditFile.SelectCommand = "SELECT NewProduct_File_Id , NewProduct_File_Field_Name , NewProduct_File_Name , NewProduct_File_CreatedDate , NewProduct_File_CreatedBy FROM vForm_Pharmacy_NewProduct_File WHERE NewProduct_Id = @NewProduct_Id ORDER BY NewProduct_File_Field_Name";
      SqlDataSource_Pharmacy_NewProduct_File_EditFile.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Pharmacy_NewProduct_File_EditFile.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_File_EditFile.SelectParameters.Add("NewProduct_Id", TypeCode.String, Request.QueryString["NewProduct_Id"]);

      SqlDataSource_Pharmacy_NewProduct_File_ItemFile.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_File_ItemFile.SelectCommand = "SELECT NewProduct_File_Id , NewProduct_File_Field_Name , NewProduct_File_Name , NewProduct_File_CreatedDate , NewProduct_File_CreatedBy FROM vForm_Pharmacy_NewProduct_File WHERE NewProduct_Id = @NewProduct_Id ORDER BY NewProduct_File_Field_Name";
      SqlDataSource_Pharmacy_NewProduct_File_ItemFile.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Pharmacy_NewProduct_File_ItemFile.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_File_ItemFile.SelectParameters.Add("NewProduct_Id", TypeCode.String, Request.QueryString["NewProduct_Id"]);
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["NewProduct_Id"] == null)
      {
        FormView_Pharmacy_NewProduct_Form.ChangeMode(FormViewMode.Insert);
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('33')) AND (Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_Pharmacy_NewProduct WHERE NewProduct_Id = @NewProduct_Id) OR SecurityRole_Rank = 1)";
        using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
        {
          SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_FormMode.Parameters.AddWithValue("@NewProduct_Id", Request.QueryString["NewProduct_Id"]);

          DataTable DataTable_FormMode;
          using (DataTable_FormMode = new DataTable())
          {
            DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
            DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
            if (DataTable_FormMode.Rows.Count > 0)
            {
              DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
              DataRow[] SecurityFormAdministrator = DataTable_FormMode.Select("SecurityRole_Id = '135'");
              DataRow[] SecurityFormCoordinator = DataTable_FormMode.Select("SecurityRole_Id = '136'");
              DataRow[] SecurityFormAssistant = DataTable_FormMode.Select("SecurityRole_Id = '137'");

              Session["NewProductId"] = "";
              string SQLStringPharmacyNewProduct = "SELECT NewProduct_Id FROM InfoQuest_Form_Pharmacy_NewProduct WHERE NewProduct_Feedback_ProgressStatus_List = '4409' AND NewProduct_IsActive = 1 AND NewProduct_Id = @NewProduct_Id";
              using (SqlCommand SqlCommand_PharmacyNewProduct = new SqlCommand(SQLStringPharmacyNewProduct))
              {
                SqlCommand_PharmacyNewProduct.Parameters.AddWithValue("@NewProduct_Id", Request.QueryString["NewProduct_Id"]);
                DataTable DataTable_PharmacyNewProduct;
                using (DataTable_PharmacyNewProduct = new DataTable())
                {
                  DataTable_PharmacyNewProduct.Locale = CultureInfo.CurrentCulture;
                  DataTable_PharmacyNewProduct = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PharmacyNewProduct).Copy();
                  if (DataTable_PharmacyNewProduct.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_PharmacyNewProduct.Rows)
                    {
                      Session["NewProductId"] = DataRow_Row["NewProduct_Id"];
                    }
                  }
                  else
                  {
                    Session["NewProductId"] = "";
                  }
                }
              }

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && SecurityAdmin.Length > 0)
              {
                Session["Security"] = "0";
                FormView_Pharmacy_NewProduct_Form.ChangeMode(FormViewMode.Edit);
              }

              if (Session["Security"].ToString() == "1" && SecurityFormAdministrator.Length > 0)
              {
                Session["Security"] = "0";
                FormView_Pharmacy_NewProduct_Form.ChangeMode(FormViewMode.Edit);
              }

              if (Session["Security"].ToString() == "1" && SecurityFormCoordinator.Length > 0)
              {
                Session["Security"] = "0";
                if (string.IsNullOrEmpty(Session["NewProductId"].ToString()))
                {
                  FormView_Pharmacy_NewProduct_Form.ChangeMode(FormViewMode.ReadOnly);
                }
                else
                {
                  FormView_Pharmacy_NewProduct_Form.ChangeMode(FormViewMode.Edit);
                }

                Session["NewProductId"] = "";
              }

              if (Session["Security"].ToString() == "1" && SecurityFormAssistant.Length > 0)
              {
                Session["Security"] = "0";
                if (string.IsNullOrEmpty(Session["NewProductId"].ToString()))
                {
                  FormView_Pharmacy_NewProduct_Form.ChangeMode(FormViewMode.ReadOnly);
                }
                else
                {
                  FormView_Pharmacy_NewProduct_Form.ChangeMode(FormViewMode.Edit);
                }

                Session["NewProductId"] = "";
              }

              Session["NewProductId"] = "";
            }
            else
            {
              FormView_Pharmacy_NewProduct_Form.ChangeMode(FormViewMode.ReadOnly);
            }
          }
        }
      }
    }

    private void TableFormVisible()
    {
      if (FormView_Pharmacy_NewProduct_Form.CurrentMode == FormViewMode.Insert)
      {
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertFacility")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertProductClassificationList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertManufacturerList")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertProductDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertProductDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertTradeName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertTradeName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertActiveIngredient")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertActiveIngredient")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertStrength")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertStrength")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertPackSize")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertPackSize")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertNettPrice")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertNettPrice")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertUseList")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertSupplierCatalogNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertSupplierCatalogNumber")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertNappiCode")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertNappiCode")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertProductRequestList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertRequestByList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertFormularyProduct")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertFormularyProductDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertFormularyProductDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertClinicalBenefit")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertClinicalBenefit")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertFinancialBenefit")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertFinancialBenefit")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertRequirement")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertRequirement")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertFeedbackDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertFeedbackDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertFeedbackProgressStatusList")).Attributes.Add("OnChange", "Validation_Form();");

        TextBox TextBox_InsertDate = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertDate");
        if (string.IsNullOrEmpty(TextBox_InsertDate.Text))
        {
          TextBox_InsertDate.Text = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
        }
      }

      if (FormView_Pharmacy_NewProduct_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditProductClassificationList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditManufacturerList")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditProductDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditProductDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditTradeName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditTradeName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditActiveIngredient")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditActiveIngredient")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditStrength")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditStrength")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditPackSize")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditPackSize")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditNettPrice")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditNettPrice")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditUseList")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditSupplierCatalogNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditSupplierCatalogNumber")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditNappiCode")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditNappiCode")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditProductRequestList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditRequestByList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditFormularyProduct")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditFormularyProductDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditFormularyProductDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditClinicalBenefit")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditClinicalBenefit")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditFinancialBenefit")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditFinancialBenefit")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditRequirement")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditRequirement")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditFeedbackDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditFeedbackDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditFeedbackProgressStatusList")).Attributes.Add("OnChange", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_NewProductReportNumber"];
      string SearchField3 = Request.QueryString["Search_NewProductManufacturerList"];
      string SearchField4 = Request.QueryString["Search_NewProductCreatedBy"];
      string SearchField5 = Request.QueryString["Search_NewProductModifiedBy"];
      string SearchField6 = Request.QueryString["Search_NewProductIsActive"];
      string SearchField7 = Request.QueryString["Search_NewProductFeedbackProgressStatusList"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_NewProduct_ReportNumber=" + Request.QueryString["Search_NewProductReportNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_NewProduct_Manufacturer_List=" + Request.QueryString["Search_NewProductManufacturerList"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_NewProduct_CreatedBy=" + Request.QueryString["Search_NewProductCreatedBy"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_NewProduct_ModifiedBy=" + Request.QueryString["Search_NewProductModifiedBy"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "s_NewProduct_IsActive=" + Request.QueryString["Search_NewProductIsActive"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField7))
      {
        SearchField7 = "s_NewProduct_Feedback_ProgressStatus_List=" + Request.QueryString["Search_NewProductFeedbackProgressStatusList"] + "&";
      }

      string FinalURL = "Form_Pharmacy_NewProduct_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("New Product Code Request Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    private void NewProductFileCleanUp()
    {
      if (Request.QueryString["NewProduct_Id"] == null)
      {
        HiddenField HiddenField_InsertNewProductFileTemp = (HiddenField)FormView_Pharmacy_NewProduct_Form.FindControl("HiddenField_InsertNewProductFileTemp");
        //String NewProductFileTempId = HiddenField_InsertNewProductFileTemp.Value;

        string SQLStringNewProductFile = "DELETE FROM InfoQuest_Form_Pharmacy_NewProduct_File WHERE NewProduct_Id IS NULL AND NewProduct_File_CreatedBy = @NewProduct_File_CreatedBy AND	NewProduct_File_Temp_NewProduct_Id <> @NewProduct_File_Temp_NewProduct_Id";
        using (SqlCommand SqlCommand_NewProductFile = new SqlCommand(SQLStringNewProductFile))
        {
          SqlCommand_NewProductFile.Parameters.AddWithValue("@NewProduct_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_NewProductFile.Parameters.AddWithValue("@NewProduct_File_Temp_NewProduct_Id", HiddenField_InsertNewProductFileTemp.Value);

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_NewProductFile);
        }
      }
    }


    //--START-- --TableForm--//
    protected void FormView_Pharmacy_NewProduct_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation();

        if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;
        }
        else
        {
          e.Cancel = true;
        }

        if (e.Cancel == true)
        {
          Page.MaintainScrollPositionOnPostBack = false;
          ((Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          DropDownList DropDownList_InsertFacility = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertFacility");

          Session["NewProduct_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], DropDownList_InsertFacility.SelectedValue.ToString(), "33");

          SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["Facility_Id"].DefaultValue = DropDownList_InsertFacility.SelectedValue.ToString();
          SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_ReportNumber"].DefaultValue = Session["NewProduct_ReportNumber"].ToString();
          SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_History"].DefaultValue = "";
          SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_IsActive"].DefaultValue = "true";
          SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["Form_Emailed"].DefaultValue = "false";

          string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('33'))";
          using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
          {
            SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
            DataTable DataTable_FormMode;
            using (DataTable_FormMode = new DataTable())
            {
              DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
              DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
              if (DataTable_FormMode.Rows.Count > 0)
              {
                //DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
                //DataRow[] SecurityFormAdministrator = DataTable_FormMode.Select("SecurityRole_Id = '135'");
                DataRow[] SecurityFormCoordinator = DataTable_FormMode.Select("SecurityRole_Id = '136'");
                DataRow[] SecurityFormAssistant = DataTable_FormMode.Select("SecurityRole_Id = '137'");

                Session["Security"] = "1";
                if (Session["Security"].ToString() == "1" && SecurityFormCoordinator.Length > 0)
                {
                  Session["Security"] = "0";
                  SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_Feedback_ProgressStatus_List"].DefaultValue = "4411";
                }

                if (Session["Security"].ToString() == "1" && SecurityFormAssistant.Length > 0)
                {
                  Session["Security"] = "0";
                  SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_Feedback_ProgressStatus_List"].DefaultValue = "4411";
                }
                Session["Security"] = "1";
              }
              else
              {
                SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_Feedback_ProgressStatus_List"].DefaultValue = "4411";
              }
            }
          }

          DropDownList DropDownList_InsertFeedbackProgressStatusList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertFeedbackProgressStatusList");

          if (DropDownList_InsertFeedbackProgressStatusList.SelectedValue.ToString() != "4332" && DropDownList_InsertFeedbackProgressStatusList.SelectedValue.ToString() != "4333")
          {
            SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_Feedback_Date"].DefaultValue = DBNull.Value.ToString();
          }
          else
          {
            SqlDataSource_Pharmacy_NewProduct_Form.InsertParameters["NewProduct_Feedback_Date"].DefaultValue = DateTime.Now.ToString();
          }

          Session["NewProduct_ReportNumber"] = "";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_InsertFacility = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertFacility");
      TextBox TextBox_InsertDate = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertDate");
      DropDownList DropDownList_InsertProductClassificationList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertProductClassificationList");
      TextBox TextBox_InsertNappiCode = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertNappiCode");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_InsertFacility.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertDate.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertProductClassificationList.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (DropDownList_InsertProductClassificationList.SelectedValue.ToString() == "4329")
        {
          InvalidForm = InsertRequiredFields_ProductClassification_Ethical(InvalidForm);
        }
        else if (DropDownList_InsertProductClassificationList.SelectedValue.ToString() == "4330")
        {
          InvalidForm = InsertRequiredFields_ProductClassification_Prosthesis(InvalidForm);
        }
        else
        {
          InvalidForm = InsertRequiredFields_ProductClassification_Other(InvalidForm);
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string DateToValidateDate = TextBox_InsertDate.Text.ToString();
        DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

        if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
          }
        }

        if (TextBox_InsertNappiCode.Text.ToString() != "______-___")
        {
          string NappiCode = TextBox_InsertNappiCode.Text.ToString();
          Int32 NappiCodeValid = NappiCode.IndexOf("_", StringComparison.CurrentCulture);

          if (NappiCodeValid >= 0 && NappiCodeValid <= 5)
          {
            InvalidFormMessage = InvalidFormMessage + "9 Digit NAPPI code is not in the correct format, first 6 digits are required";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected string InsertRequiredFields_ProductClassification_Ethical(string invalidForm)
    {
      string InvalidForm = invalidForm;

      DropDownList DropDownList_InsertManufacturerList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertManufacturerList");
      TextBox TextBox_InsertProductDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertProductDescription");
      TextBox TextBox_InsertTradeName = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertTradeName");
      TextBox TextBox_InsertActiveIngredient = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertActiveIngredient");
      TextBox TextBox_InsertStrength = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertStrength");
      TextBox TextBox_InsertPackSize = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertPackSize");
      TextBox TextBox_InsertNettPrice = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertNettPrice");
      TextBox TextBox_InsertNappiCode = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertNappiCode");
      DropDownList DropDownList_InsertFormularyProduct = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertFormularyProduct");
      TextBox TextBox_InsertFeedbackDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertFeedbackDescription");
      DropDownList DropDownList_InsertFeedbackProgressStatusList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertFeedbackProgressStatusList");

      if (string.IsNullOrEmpty(DropDownList_InsertManufacturerList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertProductDescription.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertTradeName.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertActiveIngredient.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertStrength.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertPackSize.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertNettPrice.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertNappiCode.Text) || TextBox_InsertNappiCode.Text == "______-___")
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_InsertFormularyProduct.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (DropDownList_InsertFeedbackProgressStatusList.Visible == true)
      {
        if (DropDownList_InsertFeedbackProgressStatusList.SelectedValue.ToString() == "4333")
        {
          if (string.IsNullOrEmpty(TextBox_InsertFeedbackDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      return InvalidForm;
    }

    protected string InsertRequiredFields_ProductClassification_Prosthesis(string invalidForm)
    {
      string InvalidForm = invalidForm;

      DropDownList DropDownList_InsertManufacturerList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertManufacturerList");
      TextBox TextBox_InsertProductDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertProductDescription");
      TextBox TextBox_InsertPackSize = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertPackSize");
      TextBox TextBox_InsertNettPrice = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertNettPrice");
      DropDownList DropDownList_InsertUseList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertUseList");
      TextBox TextBox_InsertSupplierCatalogNumber = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertSupplierCatalogNumber");
      TextBox TextBox_InsertNappiCode = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertNappiCode");
      DropDownList DropDownList_InsertProductRequestList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertProductRequestList");
      DropDownList DropDownList_InsertRequestByList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertRequestByList");

      if (string.IsNullOrEmpty(DropDownList_InsertManufacturerList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertProductDescription.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertPackSize.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertNettPrice.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_InsertUseList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertSupplierCatalogNumber.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertNappiCode.Text) || TextBox_InsertNappiCode.Text == "______-___")
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_InsertProductRequestList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_InsertRequestByList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string InsertRequiredFields_ProductClassification_Other(string invalidForm)
    {
      string InvalidForm = invalidForm;

      DropDownList DropDownList_InsertManufacturerList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertManufacturerList");
      TextBox TextBox_InsertProductDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertProductDescription");
      TextBox TextBox_InsertPackSize = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertPackSize");
      TextBox TextBox_InsertNettPrice = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertNettPrice");
      DropDownList DropDownList_InsertUseList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertUseList");
      TextBox TextBox_InsertSupplierCatalogNumber = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertSupplierCatalogNumber");
      TextBox TextBox_InsertNappiCode = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertNappiCode");
      DropDownList DropDownList_InsertProductRequestList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertProductRequestList");
      DropDownList DropDownList_InsertRequestByList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertRequestByList");
      DropDownList DropDownList_InsertFormularyProduct = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertFormularyProduct");
      TextBox TextBox_InsertClinicalBenefit = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertClinicalBenefit");
      TextBox TextBox_InsertFinancialBenefit = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertFinancialBenefit");
      TextBox TextBox_InsertRequirement = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertRequirement");
      DropDownList DropDownList_InsertFeedbackProgressStatusList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertFeedbackProgressStatusList");
      TextBox TextBox_InsertFeedbackDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertFeedbackDescription");

      if (string.IsNullOrEmpty(DropDownList_InsertManufacturerList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertProductDescription.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertPackSize.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertNettPrice.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_InsertUseList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertSupplierCatalogNumber.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertNappiCode.Text) || TextBox_InsertNappiCode.Text == "______-___")
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_InsertProductRequestList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_InsertRequestByList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (DropDownList_InsertProductRequestList.SelectedValue.ToString() != "4348")
      {
        if (string.IsNullOrEmpty(DropDownList_InsertFormularyProduct.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertClinicalBenefit.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertFinancialBenefit.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertRequirement.Text))
        {
          InvalidForm = "Yes";
        }
      }

      if (DropDownList_InsertFeedbackProgressStatusList.Visible == true)
      {
        if (DropDownList_InsertFeedbackProgressStatusList.SelectedValue.ToString() == "4333")
        {
          if (string.IsNullOrEmpty(TextBox_InsertFeedbackDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      return InvalidForm;
    }

    protected void SqlDataSource_Pharmacy_NewProduct_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["NewProduct_Id"] = e.Command.Parameters["@NewProduct_Id"].Value;
        Session["NewProduct_ReportNumber"] = e.Command.Parameters["@NewProduct_ReportNumber"].Value;

        string SQLStringInsertFeedbackTracking = "INSERT INTO Form_Pharmacy_NewProduct_FeedbackTracking ( NewProduct_Id , NewProduct_FeedbackTracking_ProgressStatus_List , NewProduct_FeedbackTracking_CreatedDate , NewProduct_FeedbackTracking_CreatedBy ) SELECT NewProduct_Id , NewProduct_Feedback_ProgressStatus_List , NewProduct_ModifiedDate , NewProduct_ModifiedBy FROM InfoQuest_Form_Pharmacy_NewProduct WHERE NewProduct_Id = @NewProduct_Id";
        using (SqlCommand SqlCommand_InsertFeedbackTracking = new SqlCommand(SQLStringInsertFeedbackTracking))
        {
          SqlCommand_InsertFeedbackTracking.Parameters.AddWithValue("@NewProduct_Id", Session["NewProduct_Id"].ToString());

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertFeedbackTracking);
        }

        HiddenField HiddenField_InsertNewProductFileTemp = (HiddenField)FormView_Pharmacy_NewProduct_Form.FindControl("HiddenField_InsertNewProductFileTemp");

        string SQLStringUpdateFile = "UPDATE InfoQuest_Form_Pharmacy_NewProduct_File SET NewProduct_Id = @NewProduct_Id, NewProduct_File_Temp_NewProduct_Id = NULL WHERE NewProduct_File_Temp_NewProduct_Id = @NewProduct_File_Temp_NewProduct_Id";
        using (SqlCommand SqlCommand_UpdateFile = new SqlCommand(SQLStringUpdateFile))
        {
          SqlCommand_UpdateFile.Parameters.AddWithValue("@NewProduct_Id", Session["NewProduct_Id"].ToString());
          SqlCommand_UpdateFile.Parameters.AddWithValue("@NewProduct_File_Temp_NewProduct_Id", HiddenField_InsertNewProductFileTemp.Value.ToString());

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateFile);
        }

        //if (e.Command.Parameters["@NewProduct_ProductRequest_List"].Value.ToString() == "4348" || e.Command.Parameters["@NewProduct_ProductClassification_List"].Value.ToString() == "4329")
        //{
        //  string SQLStringRemoveFile = "DELETE FROM InfoQuest_Form_Pharmacy_NewProduct_File WHERE NewProduct_Id = @NewProduct_Id";
        //  using (SqlCommand SqlCommand_RemoveFile = new SqlCommand(SQLStringRemoveFile))
        //  {
        //    SqlCommand_RemoveFile.Parameters.AddWithValue("@NewProduct_Id", Session["NewProduct_Id"].ToString());

        //    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_RemoveFile);
        //  }
        //}

        if (!string.IsNullOrEmpty(Session["NewProduct_Id"].ToString()))
        {
          InsertedSendEmail(Session["NewProduct_Id"].ToString(), e.Command.Parameters["@NewProduct_ProductClassification_List"].Value.ToString());          
        }

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_Pharmacy_NewProduct&ReportNumber=" + Session["NewProduct_ReportNumber"].ToString() + ""), false);
      }
    }


    protected static void InsertedSendEmail(string currentProductId, string currentProductProductClassificationList)
    {
      string ProductClassificationEmailAddress = "";

      if (currentProductProductClassificationList == "4329") //Ethical
      {
        string SQLStringFormEmail = "SELECT ListItem_Name FROM Administration_ListItem WHERE ListItem_Id = 4930 AND ListItem_Name != 'No'";
        using (SqlCommand SqlCommand_FormEmail = new SqlCommand(SQLStringFormEmail))
        {
          DataTable DataTable_FormEmail;
          using (DataTable_FormEmail = new DataTable())
          {
            DataTable_FormEmail.Locale = CultureInfo.CurrentCulture;
            DataTable_FormEmail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormEmail).Copy();
            if (DataTable_FormEmail.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FormEmail.Rows)
              {
                ProductClassificationEmailAddress = DataRow_Row["ListItem_Name"].ToString();
              }
            }
          }
        }
      }

      if (currentProductProductClassificationList == "4330") //Prosthesis
      {
        string SQLStringFormEmail = "SELECT ListItem_Name FROM Administration_ListItem WHERE ListItem_Id = 4472 AND ListItem_Name != 'No'";
        using (SqlCommand SqlCommand_FormEmail = new SqlCommand(SQLStringFormEmail))
        {
          DataTable DataTable_FormEmail;
          using (DataTable_FormEmail = new DataTable())
          {
            DataTable_FormEmail.Locale = CultureInfo.CurrentCulture;
            DataTable_FormEmail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormEmail).Copy();
            if (DataTable_FormEmail.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FormEmail.Rows)
              {
                ProductClassificationEmailAddress = DataRow_Row["ListItem_Name"].ToString();
              }
            }
          }
        }
      }

      if (currentProductProductClassificationList == "4331") //Surgical
      {
        string SQLStringFormEmail = "SELECT ListItem_Name FROM Administration_ListItem WHERE ListItem_Id = 4931 AND ListItem_Name != 'No'";
        using (SqlCommand SqlCommand_FormEmail = new SqlCommand(SQLStringFormEmail))
        {
          DataTable DataTable_FormEmail;
          using (DataTable_FormEmail = new DataTable())
          {
            DataTable_FormEmail.Locale = CultureInfo.CurrentCulture;
            DataTable_FormEmail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormEmail).Copy();
            if (DataTable_FormEmail.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FormEmail.Rows)
              {
                ProductClassificationEmailAddress = DataRow_Row["ListItem_Name"].ToString();
              }
            }
          }
        }
      }


      if (!string.IsNullOrEmpty(ProductClassificationEmailAddress))
      {
        string NewProductId = "";
        string FacilityFacilityDisplayName = "";
        string NewProductReportNumber = "";
        string SQLStringFacilityDisplayName = "SELECT NewProduct_Id, Facility_FacilityDisplayName, NewProduct_ReportNumber FROM vForm_Pharmacy_NewProduct WHERE NewProduct_Id = @NewProduct_Id";
        using (SqlCommand SqlCommand_FacilityDisplayName = new SqlCommand(SQLStringFacilityDisplayName))
        {
          SqlCommand_FacilityDisplayName.Parameters.AddWithValue("@NewProduct_Id", currentProductId);
          DataTable DataTable_FacilityDisplayName;
          using (DataTable_FacilityDisplayName = new DataTable())
          {
            DataTable_FacilityDisplayName.Locale = CultureInfo.CurrentCulture;
            DataTable_FacilityDisplayName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityDisplayName).Copy();
            if (DataTable_FacilityDisplayName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FacilityDisplayName.Rows)
              {
                NewProductId = DataRow_Row["NewProduct_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                NewProductReportNumber = DataRow_Row["NewProduct_ReportNumber"].ToString();
              }
            }
          }
        }

        InsertedSendEmail_ToAccount(NewProductId, FacilityFacilityDisplayName, NewProductReportNumber, ProductClassificationEmailAddress);
      }
    }

    protected static void InsertedSendEmail_ToAccount(string currentProductId, string currentFacilityFacilityDisplayName, string currentProductReportNumber, string productClassificationEmailAddress)
    {
      string FormsEmailAccount = productClassificationEmailAddress;

      string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("20");
      string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
      string FormsName = InfoQuestWCF.InfoQuest_All.All_FormName("33");
      string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
      string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();

      string BodyString = EmailTemplate;

      BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + FormsEmailAccount + "");
      BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormsName + "");
      BodyString = BodyString.Replace(";replace;FacilityDisplayName;replace;", "" + currentFacilityFacilityDisplayName + "");
      BodyString = BodyString.Replace(";replace;NewProductId;replace;", "" + currentProductId + "");
      BodyString = BodyString.Replace(";replace;NewProductReportNumber;replace;", "" + currentProductReportNumber + "");
      BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");

      string EmailBody = HeaderString + BodyString + FooterString;

      string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", FormsEmailAccount, FormsName, EmailBody);

      if (!string.IsNullOrEmpty(EmailSend))
      {
        EmailSend = "";
      }

      EmailBody = "";
      EmailTemplate = "";
      URLAuthority = "";
      FormsName = "";
    }


    protected void FormView_Pharmacy_NewProduct_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDNewProductModifiedDate"] = e.OldValues["NewProduct_ModifiedDate"];
        object OLDNewProductModifiedDate = Session["OLDNewProductModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDNewProductModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareNewProduct = (DataView)SqlDataSource_Pharmacy_NewProduct_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareNewProduct = DataView_CompareNewProduct[0];
        Session["DBNewProductModifiedDate"] = Convert.ToString(DataRowView_CompareNewProduct["NewProduct_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBNewProductModifiedBy"] = Convert.ToString(DataRowView_CompareNewProduct["NewProduct_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBNewProductModifiedDate = Session["DBNewProductModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBNewProductModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          Page.MaintainScrollPositionOnPostBack = false;

          string Label_EditConcurrencyUpdateMessage = "" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBNewProductModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>";

          ((Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Convert.ToString(Label_EditConcurrencyUpdateMessage, CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation();

          if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
          }
          else
          {
            e.Cancel = true;
          }

          if (e.Cancel == true)
          {
            Page.MaintainScrollPositionOnPostBack = false;
            ((Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["NewProduct_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["NewProduct_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Pharmacy_NewProduct", "NewProduct_Id = " + Request.QueryString["NewProduct_Id"]);

            DataView DataView_NewProduct = (DataView)SqlDataSource_Pharmacy_NewProduct_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_NewProduct = DataView_NewProduct[0];
            Session["NewProductHistory"] = Convert.ToString(DataRowView_NewProduct["NewProduct_History"], CultureInfo.CurrentCulture);

            Session["NewProductHistory"] = Session["History"].ToString() + Session["NewProductHistory"].ToString();
            e.NewValues["NewProduct_History"] = Session["NewProductHistory"].ToString();

            Session["NewProductHistory"] = "";
            Session["History"] = "";


            TextBox TextBox_EditFeedbackDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditFeedbackDescription");
            DropDownList DropDownList_EditFeedbackProgressStatusList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditFeedbackProgressStatusList");

            Session["NewProductCompleted"] = "0";
            Session["NewProductInformationRequired"] = "0";
            Session["NewProductInformationRequiredCompleted"] = "0";
            if (DropDownList_EditFeedbackProgressStatusList.Visible == false)
            {
              Session["NewProductCompleted"] = "0";
              Session["NewProductInformationRequired"] = "0";
              Session["NewProductInformationRequiredCompleted"] = "1";

              e.NewValues["NewProduct_Feedback_Description"] = e.OldValues["NewProduct_Feedback_Description"];
              e.NewValues["NewProduct_Feedback_ProgressStatus_List"] = e.OldValues["NewProduct_Feedback_ProgressStatus_List"];
              e.NewValues["NewProduct_Feedback_Date"] = e.OldValues["NewProduct_Feedback_Date"];
              e.NewValues["NewProduct_IsActive"] = e.OldValues["NewProduct_IsActive"];
            }
            else
            {
              Session["NewProductInformationRequiredCompleted"] = "0";

              if (e.OldValues["NewProduct_Feedback_ProgressStatus_List"].ToString() == "4334")
              {
                if (DropDownList_EditFeedbackProgressStatusList.SelectedValue == "4334")
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DBNull.Value.ToString();
                  Session["NewProductCompleted"] = "1";
                  Session["NewProductInformationRequired"] = "0";
                }
                else if (DropDownList_EditFeedbackProgressStatusList.SelectedValue == "4411")
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DBNull.Value.ToString();
                  Session["NewProductCompleted"] = "0";
                  Session["NewProductInformationRequired"] = "0";
                }
                else if (DropDownList_EditFeedbackProgressStatusList.SelectedValue == "4409")
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DBNull.Value.ToString();
                  Session["NewProductCompleted"] = "0";
                  Session["NewProductInformationRequired"] = "1";
                }
                else
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DateTime.Now.ToString();
                  Session["NewProductCompleted"] = "1";
                  Session["NewProductInformationRequired"] = "0";
                }
              }
              else if (e.OldValues["NewProduct_Feedback_ProgressStatus_List"].ToString() == "4411")
              {
                if (DropDownList_EditFeedbackProgressStatusList.SelectedValue == "4334")
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DBNull.Value.ToString();
                  Session["NewProductCompleted"] = "1";
                  Session["NewProductInformationRequired"] = "0";
                }
                else if (DropDownList_EditFeedbackProgressStatusList.SelectedValue == "4411")
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DBNull.Value.ToString();
                  Session["NewProductCompleted"] = "0";
                  Session["NewProductInformationRequired"] = "0";
                }
                else if (DropDownList_EditFeedbackProgressStatusList.SelectedValue == "4409")
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DBNull.Value.ToString();
                  Session["NewProductCompleted"] = "0";
                  Session["NewProductInformationRequired"] = "1";
                }
                else
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DateTime.Now.ToString();
                  Session["NewProductCompleted"] = "1";
                  Session["NewProductInformationRequired"] = "0";
                }
              }
              else if (e.OldValues["NewProduct_Feedback_ProgressStatus_List"].ToString() == "4409")
              {
                if (DropDownList_EditFeedbackProgressStatusList.SelectedValue == "4334")
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DBNull.Value.ToString();
                  Session["NewProductCompleted"] = "1";
                  Session["NewProductInformationRequired"] = "0";
                }
                else if (DropDownList_EditFeedbackProgressStatusList.SelectedValue == "4411")
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DBNull.Value.ToString();
                  Session["NewProductCompleted"] = "0";
                  Session["NewProductInformationRequired"] = "0";
                }
                else if (DropDownList_EditFeedbackProgressStatusList.SelectedValue == "4409")
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DBNull.Value.ToString();
                  Session["NewProductCompleted"] = "0";
                  Session["NewProductInformationRequired"] = "0";
                }
                else
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DateTime.Now.ToString();
                  Session["NewProductCompleted"] = "1";
                  Session["NewProductInformationRequired"] = "0";
                }
              }
              else
              {
                if (DropDownList_EditFeedbackProgressStatusList.SelectedValue == "4334")
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DBNull.Value.ToString();
                  Session["NewProductCompleted"] = "1";
                  Session["NewProductInformationRequired"] = "0";
                }
                else if (DropDownList_EditFeedbackProgressStatusList.SelectedValue == "4411")
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DBNull.Value.ToString();
                  Session["NewProductCompleted"] = "0";
                  Session["NewProductInformationRequired"] = "0";
                }
                else if (DropDownList_EditFeedbackProgressStatusList.SelectedValue == "4409")
                {
                  e.NewValues["NewProduct_Feedback_Date"] = DBNull.Value.ToString();
                  Session["NewProductCompleted"] = "0";
                  Session["NewProductInformationRequired"] = "1";
                }
                else
                {
                  if (e.OldValues["NewProduct_Feedback_Description"].ToString() != TextBox_EditFeedbackDescription.Text.ToString())
                  {
                    Session["NewProductCompleted"] = "1";
                    Session["NewProductInformationRequired"] = "0";
                  }
                  else
                  {
                    Session["NewProductCompleted"] = "0";
                    Session["NewProductInformationRequired"] = "0";
                  }
                }
              }
            }
          }
        }
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditDate = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditDate");
      DropDownList DropDownList_EditProductClassificationList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditProductClassificationList");
      TextBox TextBox_EditNappiCode = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditNappiCode");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditDate.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditProductClassificationList.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (DropDownList_EditProductClassificationList.SelectedValue.ToString() == "4329")
        {
          InvalidForm = EditRequiredFields_ProductClassification_Ethical(InvalidForm);
        }
        else if (DropDownList_EditProductClassificationList.SelectedValue.ToString() == "4330")
        {
          InvalidForm = EditRequiredFields_ProductClassification_Prosthesis(InvalidForm);
        }
        else
        {
          InvalidForm = EditRequiredFields_ProductClassification_Other(InvalidForm);
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string DateToValidateDate = TextBox_EditDate.Text.ToString();
        DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

        if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
          }
        }

        if (TextBox_EditNappiCode.Text.ToString() != "______-___")
        {
          string NappiCode = TextBox_EditNappiCode.Text.ToString();
          Int32 NappiCodeValid = NappiCode.IndexOf("_", StringComparison.CurrentCulture);

          if (NappiCodeValid >= 0 && NappiCodeValid <= 5)
          {
            InvalidFormMessage = InvalidFormMessage + "9 Digit NAPPI code is not in the correct format, first 6 digits are required";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected string EditRequiredFields_ProductClassification_Ethical(string invalidForm)
    {
      string InvalidForm = invalidForm;

      DropDownList DropDownList_EditManufacturerList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditManufacturerList");
      TextBox TextBox_EditProductDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditProductDescription");
      TextBox TextBox_EditTradeName = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditTradeName");
      TextBox TextBox_EditActiveIngredient = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditActiveIngredient");
      TextBox TextBox_EditStrength = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditStrength");
      TextBox TextBox_EditPackSize = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditPackSize");
      TextBox TextBox_EditNettPrice = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditNettPrice");
      TextBox TextBox_EditNappiCode = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditNappiCode");
      DropDownList DropDownList_EditFormularyProduct = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditFormularyProduct");
      TextBox TextBox_EditFeedbackDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditFeedbackDescription");
      DropDownList DropDownList_EditFeedbackProgressStatusList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditFeedbackProgressStatusList");

      if (string.IsNullOrEmpty(DropDownList_EditManufacturerList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditProductDescription.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditTradeName.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditActiveIngredient.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditStrength.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditPackSize.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditNettPrice.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditNappiCode.Text) || TextBox_EditNappiCode.Text == "______-___")
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_EditFormularyProduct.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (DropDownList_EditFeedbackProgressStatusList.Visible == true)
      {
        if (DropDownList_EditFeedbackProgressStatusList.SelectedValue.ToString() == "4333")
        {
          if (string.IsNullOrEmpty(TextBox_EditFeedbackDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      return InvalidForm;
    }

    protected string EditRequiredFields_ProductClassification_Prosthesis(string invalidForm)
    {
      string InvalidForm = invalidForm;

      DropDownList DropDownList_EditManufacturerList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditManufacturerList");
      TextBox TextBox_EditProductDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditProductDescription");
      TextBox TextBox_EditPackSize = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditPackSize");
      TextBox TextBox_EditNettPrice = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditNettPrice");
      DropDownList DropDownList_EditUseList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditUseList");
      TextBox TextBox_EditSupplierCatalogNumber = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditSupplierCatalogNumber");
      TextBox TextBox_EditNappiCode = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditNappiCode");
      DropDownList DropDownList_EditProductRequestList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditProductRequestList");
      DropDownList DropDownList_EditRequestByList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditRequestByList");

      if (string.IsNullOrEmpty(DropDownList_EditManufacturerList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditProductDescription.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditPackSize.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditNettPrice.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_EditUseList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditSupplierCatalogNumber.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditNappiCode.Text) || TextBox_EditNappiCode.Text == "______-___")
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_EditProductRequestList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_EditRequestByList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string EditRequiredFields_ProductClassification_Other(string invalidForm)
    {
      string InvalidForm = invalidForm;

      DropDownList DropDownList_EditManufacturerList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditManufacturerList");
      TextBox TextBox_EditProductDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditProductDescription");
      TextBox TextBox_EditPackSize = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditPackSize");
      TextBox TextBox_EditNettPrice = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditNettPrice");
      DropDownList DropDownList_EditUseList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditUseList");
      TextBox TextBox_EditSupplierCatalogNumber = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditSupplierCatalogNumber");
      TextBox TextBox_EditNappiCode = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditNappiCode");
      DropDownList DropDownList_EditProductRequestList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditProductRequestList");
      DropDownList DropDownList_EditRequestByList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditRequestByList");
      DropDownList DropDownList_EditFormularyProduct = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditFormularyProduct");
      TextBox TextBox_EditClinicalBenefit = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditClinicalBenefit");
      TextBox TextBox_EditFinancialBenefit = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditFinancialBenefit");
      TextBox TextBox_EditRequirement = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditRequirement");
      TextBox TextBox_EditFeedbackDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditFeedbackDescription");
      DropDownList DropDownList_EditFeedbackProgressStatusList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditFeedbackProgressStatusList");

      if (string.IsNullOrEmpty(DropDownList_EditManufacturerList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditProductDescription.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditPackSize.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditNettPrice.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_EditUseList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditSupplierCatalogNumber.Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_EditNappiCode.Text) || TextBox_EditNappiCode.Text == "______-___")
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_EditProductRequestList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_EditRequestByList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (DropDownList_EditProductRequestList.SelectedValue.ToString() != "4348")
      {
        if (string.IsNullOrEmpty(DropDownList_EditFormularyProduct.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditClinicalBenefit.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditFinancialBenefit.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditRequirement.Text))
        {
          InvalidForm = "Yes";
        }
      }

      if (DropDownList_EditFeedbackProgressStatusList.Visible == true)
      {
        if (DropDownList_EditFeedbackProgressStatusList.SelectedValue.ToString() == "4333")
        {
          if (string.IsNullOrEmpty(TextBox_EditFeedbackDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      return InvalidForm;
    }

    protected void SqlDataSource_Pharmacy_NewProduct_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            string SQLStringInsertFeedbackTracking = "INSERT INTO Form_Pharmacy_NewProduct_FeedbackTracking ( NewProduct_Id , NewProduct_FeedbackTracking_ProgressStatus_List , NewProduct_FeedbackTracking_CreatedDate , NewProduct_FeedbackTracking_CreatedBy ) SELECT NewProduct_Id , NewProduct_Feedback_ProgressStatus_List , NewProduct_ModifiedDate , NewProduct_ModifiedBy FROM InfoQuest_Form_Pharmacy_NewProduct WHERE NewProduct_Id = @NewProduct_Id";
            using (SqlCommand SqlCommand_InsertFeedbackTracking = new SqlCommand(SQLStringInsertFeedbackTracking))
            {
              SqlCommand_InsertFeedbackTracking.Parameters.AddWithValue("@NewProduct_Id", Request.QueryString["NewProduct_Id"]);

              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertFeedbackTracking);
            }

            //if (e.Command.Parameters["@NewProduct_ProductRequest_List"].Value.ToString() == "4348" || e.Command.Parameters["@NewProduct_ProductClassification_List"].Value.ToString() == "4329")
            //{
            //  string SQLStringRemoveFile = "DELETE FROM InfoQuest_Form_Pharmacy_NewProduct_File WHERE NewProduct_Id = @NewProduct_Id";
            //  using (SqlCommand SqlCommand_RemoveFile = new SqlCommand(SQLStringRemoveFile))
            //  {
            //    SqlCommand_RemoveFile.Parameters.AddWithValue("@NewProduct_Id", Request.QueryString["NewProduct_Id"]);

            //    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_RemoveFile);
            //  }
            //}

            UpdatedSendEmail();

            RedirectToList();
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;

            ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'>FormPrint('InfoQuest_Print.aspx?PrintPage=Form_Pharmacy_NewProduct&PrintValue=" + Request.QueryString["NewProduct_Id"] + "')</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "Reload", "<script language='javascript'>window.location.href='" + Request.Url.AbsoluteUri + "'</script>");
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;

            ClientScript.RegisterStartupScript(this.GetType(), "Email", "<script language='javascript'>FormEmail('InfoQuest_Email.aspx?EmailPage=Form_Pharmacy_NewProduct&EmailValue=" + Request.QueryString["NewProduct_Id"] + "')</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "Reload", "<script language='javascript'>window.location.href='" + Request.Url.AbsoluteUri + "'</script>");
          }
        }
      }
    }


    protected void UpdatedSendEmail()
    {
      if (Session["NewProductInformationRequired"].ToString() == "1")
      {
        Session["NewProductInformationRequired"] = "0";

        UpdatedSendEmail_NewProductInformationRequired();
      }

      if (Session["NewProductInformationRequiredCompleted"].ToString() == "1")
      {
        Session["NewProductInformationRequiredCompleted"] = "0";

        UpdatedSendEmail_NewProductInformationRequiredCompleted();
      }

      if (Session["NewProductCompleted"].ToString() == "1")
      {
        Session["NewProductCompleted"] = "0";

        UpdatedSendEmail_NewProductCompleted();
      }
    }

    protected void UpdatedSendEmail_NewProductInformationRequired()
    {
      string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("4");
      string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
      string FormsName = InfoQuestWCF.InfoQuest_All.All_FormName("33");

      string NewProductId = "";
      string FacilityFacilityDisplayName = "";
      string NewProductReportNumber = "";
      string NewProductFeedbackDescription = "";
      string NewProductCreatedBy = "";
      string SQLStringFacilityDisplayName = "SELECT NewProduct_Id, Facility_FacilityDisplayName, NewProduct_ReportNumber , NewProduct_Feedback_Description , NewProduct_CreatedBy FROM vForm_Pharmacy_NewProduct WHERE NewProduct_Id = @NewProduct_Id";
      using (SqlCommand SqlCommand_FacilityDisplayName = new SqlCommand(SQLStringFacilityDisplayName))
      {
        SqlCommand_FacilityDisplayName.Parameters.AddWithValue("@NewProduct_Id", Request.QueryString["NewProduct_Id"]);
        DataTable DataTable_FacilityDisplayName;
        using (DataTable_FacilityDisplayName = new DataTable())
        {
          DataTable_FacilityDisplayName.Locale = CultureInfo.CurrentCulture;
          DataTable_FacilityDisplayName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityDisplayName).Copy();
          if (DataTable_FacilityDisplayName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FacilityDisplayName.Rows)
            {
              NewProductId = DataRow_Row["NewProduct_Id"].ToString();
              FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              NewProductReportNumber = DataRow_Row["NewProduct_ReportNumber"].ToString();
              NewProductFeedbackDescription = DataRow_Row["NewProduct_Feedback_Description"].ToString();
              NewProductCreatedBy = DataRow_Row["NewProduct_CreatedBy"].ToString();
            }
          }
        }
      }

      string FirstNameSendTo = "";
      string LastNameSendTo = "";
      string EmailSendTo = "";
      DataTable DataTable_EmailTo;
      using (DataTable_EmailTo = new DataTable())
      {
        DataTable_EmailTo.Locale = CultureInfo.CurrentCulture;
        DataTable_EmailTo = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(NewProductCreatedBy).Copy();
        if (DataTable_EmailTo.Columns.Count == 1)
        {
          string Error = "";
          foreach (DataRow DataRow_Row in DataTable_EmailTo.Rows)
          {
            Error = DataRow_Row["Error"].ToString();
          }

          if (!string.IsNullOrEmpty(Error))
          {
            FirstNameSendTo = "";
            LastNameSendTo = "";
            EmailSendTo = "";
          }
          Error = "";
        }
        else if (DataTable_EmailTo.Columns.Count != 1)
        {
          if (DataTable_EmailTo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EmailTo.Rows)
            {
              FirstNameSendTo = DataRow_Row["FirstName"].ToString();
              LastNameSendTo = DataRow_Row["LastName"].ToString();
              EmailSendTo = DataRow_Row["Email"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(EmailSendTo))
      {
        string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
        string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();

        string BodyString = EmailTemplate;

        BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + FirstNameSendTo + " " + LastNameSendTo + "");
        BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormsName + "");
        BodyString = BodyString.Replace(";replace;FacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
        BodyString = BodyString.Replace(";replace;NewProductId;replace;", "" + NewProductId + "");
        BodyString = BodyString.Replace(";replace;NewProductReportNumber;replace;", "" + NewProductReportNumber + "");
        BodyString = BodyString.Replace(";replace;NewProductFeedbackDescription;replace;", "" + NewProductFeedbackDescription + "");
        BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");

        string EmailBody = HeaderString + BodyString + FooterString;

        string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", EmailSendTo, FormsName, EmailBody);

        if (!string.IsNullOrEmpty(EmailSend))
        {
          EmailSend = "";
        }

        EmailBody = "";
      }

      EmailTemplate = "";
      URLAuthority = "";
      FormsName = "";
      NewProductId = "";
      FacilityFacilityDisplayName = "";
      NewProductReportNumber = "";
      NewProductFeedbackDescription = "";
      NewProductCreatedBy = "";
      FirstNameSendTo = "";
      LastNameSendTo = "";
      EmailSendTo = "";
    }

    protected void UpdatedSendEmail_NewProductInformationRequiredCompleted()
    {
      string ProductInformationRequiredCompletedEmailAddress = "";
      string SQLStringFormEmail = "SELECT ListItem_Name FROM Administration_ListItem WHERE ListItem_Id = 4939 AND ListItem_Name != 'No'";
      using (SqlCommand SqlCommand_FormEmail = new SqlCommand(SQLStringFormEmail))
      {
        DataTable DataTable_FormEmail;
        using (DataTable_FormEmail = new DataTable())
        {
          DataTable_FormEmail.Locale = CultureInfo.CurrentCulture;
          DataTable_FormEmail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormEmail).Copy();
          if (DataTable_FormEmail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FormEmail.Rows)
            {
              ProductInformationRequiredCompletedEmailAddress = DataRow_Row["ListItem_Name"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(ProductInformationRequiredCompletedEmailAddress))
      {
        string NewProductId = "";
        string FacilityFacilityDisplayName = "";
        string NewProductReportNumber = "";
        string SQLStringFacilityDisplayName = "SELECT NewProduct_Id, Facility_FacilityDisplayName, NewProduct_ReportNumber FROM vForm_Pharmacy_NewProduct WHERE NewProduct_Id = @NewProduct_Id";
        using (SqlCommand SqlCommand_FacilityDisplayName = new SqlCommand(SQLStringFacilityDisplayName))
        {
          SqlCommand_FacilityDisplayName.Parameters.AddWithValue("@NewProduct_Id", Request.QueryString["NewProduct_Id"]);
          DataTable DataTable_FacilityDisplayName;
          using (DataTable_FacilityDisplayName = new DataTable())
          {
            DataTable_FacilityDisplayName.Locale = CultureInfo.CurrentCulture;
            DataTable_FacilityDisplayName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityDisplayName).Copy();
            if (DataTable_FacilityDisplayName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FacilityDisplayName.Rows)
              {
                NewProductId = DataRow_Row["NewProduct_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                NewProductReportNumber = DataRow_Row["NewProduct_ReportNumber"].ToString();
              }
            }
          }
        }

        UpdatedSendEmail_NewProductInformationRequiredCompleted_ToAccount(NewProductId, FacilityFacilityDisplayName, NewProductReportNumber, ProductInformationRequiredCompletedEmailAddress);
      }
    }

    protected static void UpdatedSendEmail_NewProductInformationRequiredCompleted_ToAccount(string currentProductId, string currentFacilityFacilityDisplayName, string currentProductReportNumber, string productInformationRequiredCompletedEmailAddress)
    {
      string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("3");
      string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
      string FormsName = InfoQuestWCF.InfoQuest_All.All_FormName("33");
      string BodyString = EmailTemplate;

      BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + productInformationRequiredCompletedEmailAddress + "");
      BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormsName + "");
      BodyString = BodyString.Replace(";replace;FacilityDisplayName;replace;", "" + currentFacilityFacilityDisplayName + "");
      BodyString = BodyString.Replace(";replace;NewProductId;replace;", "" + currentProductId + "");
      BodyString = BodyString.Replace(";replace;NewProductReportNumber;replace;", "" + currentProductReportNumber + "");
      BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");

      string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
      string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
      string EmailBody = HeaderString + BodyString + FooterString;

      string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", productInformationRequiredCompletedEmailAddress, FormsName, EmailBody);

      if (!string.IsNullOrEmpty(EmailSend))
      {
        EmailSend = "";
      }

      EmailBody = "";
      EmailTemplate = "";
      URLAuthority = "";
      FormsName = "";
    }

    private class FromDataBase_ProgressStatusEmailAddress
    {
      public string ProgressStatusEmailAddress { get; set; }
    }

    private static FromDataBase_ProgressStatusEmailAddress GetProgressStatusEmailAddress(string id)
    {
      FromDataBase_ProgressStatusEmailAddress DataBase_ProgressStatusEmailAddress_New = new FromDataBase_ProgressStatusEmailAddress();

      string SQLStringProgressStatusEmailAddress = "SELECT ListItem_Name FROM Administration_ListItem WHERE ListItem_Id = @ListItem_Id AND ListItem_Name != 'No'";
      using (SqlCommand SqlCommand_ProgressStatusEmailAddress = new SqlCommand(SQLStringProgressStatusEmailAddress))
      {
        SqlCommand_ProgressStatusEmailAddress.Parameters.AddWithValue("@ListItem_Id", id);
        DataTable DataTable_ProgressStatusEmailAddress;
        using (DataTable_ProgressStatusEmailAddress = new DataTable())
        {
          DataTable_ProgressStatusEmailAddress.Locale = CultureInfo.CurrentCulture;
          DataTable_ProgressStatusEmailAddress = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ProgressStatusEmailAddress).Copy();
          if (DataTable_ProgressStatusEmailAddress.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ProgressStatusEmailAddress.Rows)
            {
              DataBase_ProgressStatusEmailAddress_New.ProgressStatusEmailAddress = DataRow_Row["ListItem_Name"].ToString();
            }
          }
        }
      }

      return DataBase_ProgressStatusEmailAddress_New;
    }

    protected void UpdatedSendEmail_NewProductCompleted()
    {
      string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("10");
      string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
      string FormsName = InfoQuestWCF.InfoQuest_All.All_FormName("33");

      string NewProductId = "";
      string FacilityFacilityDisplayName = "";
      string NewProductReportNumber = "";
      string NewProductFeedbackDescription = "";
      string NewProductFeedbackProgressStatusList = "";
      string NewProductCreatedBy = "";
      string SQLStringFacilityDisplayName = "SELECT NewProduct_Id, Facility_FacilityDisplayName, NewProduct_ReportNumber , NewProduct_Feedback_Description , NewProduct_Feedback_ProgressStatus_List , NewProduct_CreatedBy FROM vForm_Pharmacy_NewProduct WHERE NewProduct_Id = @NewProduct_Id";
      using (SqlCommand SqlCommand_FacilityDisplayName = new SqlCommand(SQLStringFacilityDisplayName))
      {
        SqlCommand_FacilityDisplayName.Parameters.AddWithValue("@NewProduct_Id", Request.QueryString["NewProduct_Id"]);
        DataTable DataTable_FacilityDisplayName;
        using (DataTable_FacilityDisplayName = new DataTable())
        {
          DataTable_FacilityDisplayName.Locale = CultureInfo.CurrentCulture;
          DataTable_FacilityDisplayName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityDisplayName).Copy();
          if (DataTable_FacilityDisplayName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FacilityDisplayName.Rows)
            {
              NewProductId = DataRow_Row["NewProduct_Id"].ToString();
              FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              NewProductReportNumber = DataRow_Row["NewProduct_ReportNumber"].ToString();
              NewProductFeedbackDescription = DataRow_Row["NewProduct_Feedback_Description"].ToString();
              NewProductFeedbackProgressStatusList = DataRow_Row["NewProduct_Feedback_ProgressStatus_List"].ToString();
              NewProductCreatedBy = DataRow_Row["NewProduct_CreatedBy"].ToString();
            }
          }
        }
      }

      string NewProductFeedbackProgressStatusListEmailAddress = "";

      if (NewProductFeedbackProgressStatusList == "4332") //Approved
      {
        FromDataBase_ProgressStatusEmailAddress FromDataBase_ProgressStatusEmailAddress_Current = GetProgressStatusEmailAddress("4933");
        NewProductFeedbackProgressStatusListEmailAddress = FromDataBase_ProgressStatusEmailAddress_Current.ProgressStatusEmailAddress;
      }

      if (NewProductFeedbackProgressStatusList == "4333") //Declined
      {
        FromDataBase_ProgressStatusEmailAddress FromDataBase_ProgressStatusEmailAddress_Current = GetProgressStatusEmailAddress("4934");
        NewProductFeedbackProgressStatusListEmailAddress = FromDataBase_ProgressStatusEmailAddress_Current.ProgressStatusEmailAddress;
      }

      if (NewProductFeedbackProgressStatusList == "4334") //Pending
      {
        FromDataBase_ProgressStatusEmailAddress FromDataBase_ProgressStatusEmailAddress_Current = GetProgressStatusEmailAddress("4936");
        NewProductFeedbackProgressStatusListEmailAddress = FromDataBase_ProgressStatusEmailAddress_Current.ProgressStatusEmailAddress;
      }

      if (NewProductFeedbackProgressStatusList == "4938") //Completed
      {
        FromDataBase_ProgressStatusEmailAddress FromDataBase_ProgressStatusEmailAddress_Current = GetProgressStatusEmailAddress("4937");
        NewProductFeedbackProgressStatusListEmailAddress = FromDataBase_ProgressStatusEmailAddress_Current.ProgressStatusEmailAddress;
      }

      if (!string.IsNullOrEmpty(NewProductFeedbackProgressStatusListEmailAddress))
      {
        string FirstNameSendTo = "";
        string LastNameSendTo = "";
        string EmailSendTo = "";
        DataTable DataTable_EmailTo;
        using (DataTable_EmailTo = new DataTable())
        {
          DataTable_EmailTo.Locale = CultureInfo.CurrentCulture;
          DataTable_EmailTo = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(NewProductCreatedBy).Copy();
          if (DataTable_EmailTo.Columns.Count != 1)
          {
            if (DataTable_EmailTo.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_EmailTo.Rows)
              {
                FirstNameSendTo = DataRow_Row["FirstName"].ToString();
                LastNameSendTo = DataRow_Row["LastName"].ToString();
                EmailSendTo = DataRow_Row["Email"].ToString();
              }
            }
          }
        }

        string NewEmailSendTo = NewProductFeedbackProgressStatusListEmailAddress;

        if (!string.IsNullOrEmpty(EmailSendTo))
        {
          NewEmailSendTo = NewEmailSendTo.Replace("No,", "");
          NewEmailSendTo = NewEmailSendTo.Replace(",No", "");
          NewEmailSendTo = NewEmailSendTo.Replace("No", "");
          NewEmailSendTo = NewEmailSendTo.Replace("User,", EmailSendTo + ",");
          NewEmailSendTo = NewEmailSendTo.Replace(",User", "," + EmailSendTo);
          NewEmailSendTo = NewEmailSendTo.Replace("User", EmailSendTo);
        }
        else
        {
          NewEmailSendTo = NewEmailSendTo.Replace("No,", "");
          NewEmailSendTo = NewEmailSendTo.Replace(",No", "");
          NewEmailSendTo = NewEmailSendTo.Replace("No", "");
          NewEmailSendTo = NewEmailSendTo.Replace("User,", "");
          NewEmailSendTo = NewEmailSendTo.Replace(",User", "");
          NewEmailSendTo = NewEmailSendTo.Replace("User", "");
        }


        if (!string.IsNullOrEmpty(NewEmailSendTo))
        {
          string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
          string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();

          string BodyString = EmailTemplate;

          BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + FirstNameSendTo + " " + LastNameSendTo + "");
          BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormsName + "");
          BodyString = BodyString.Replace(";replace;FacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
          BodyString = BodyString.Replace(";replace;NewProductId;replace;", "" + NewProductId + "");
          BodyString = BodyString.Replace(";replace;NewProductReportNumber;replace;", "" + NewProductReportNumber + "");
          BodyString = BodyString.Replace(";replace;NewProductFeedbackDescription;replace;", "" + NewProductFeedbackDescription + "");
          BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");

          string EmailBody = HeaderString + BodyString + FooterString;

          string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", NewEmailSendTo, FormsName, EmailBody);

          if (!string.IsNullOrEmpty(EmailSend))
          {
            EmailSend = "";
          }

          EmailBody = "";
        }

        EmailTemplate = "";
        URLAuthority = "";
        FormsName = "";
        NewProductId = "";
        FacilityFacilityDisplayName = "";
        NewProductReportNumber = "";
        NewProductFeedbackDescription = "";
        NewProductCreatedBy = "";
        FirstNameSendTo = "";
        LastNameSendTo = "";
        EmailSendTo = "";
        NewEmailSendTo = "";
      }
    }


    protected void FormView_Pharmacy_NewProduct_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["NewProduct_Id"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("New Product Code Request Form", "Form_Pharmacy_NewProduct.aspx?NewProduct_Id=" + Request.QueryString["NewProduct_Id"] + ""), false);
          }
        }
      }
    }

    protected void FormView_Pharmacy_NewProduct_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_Pharmacy_NewProduct_Form.CurrentMode == FormViewMode.Insert)
      {
        InsertDataBound();
      }

      if (FormView_Pharmacy_NewProduct_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_Pharmacy_NewProduct_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected void InsertDataBound()
    {
      TextBox TextBox_InsertFeedbackDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertFeedbackDescription");
      DropDownList DropDownList_InsertFeedbackProgressStatusList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertFeedbackProgressStatusList");

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('33'))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdministrator = DataTable_FormMode.Select("SecurityRole_Id = '135'");
            DataRow[] SecurityFormCoordinator = DataTable_FormMode.Select("SecurityRole_Id = '136'");
            DataRow[] SecurityFormAssistant = DataTable_FormMode.Select("SecurityRole_Id = '137'");

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && SecurityAdmin.Length > 0)
            {
              Session["Security"] = "0";
              TextBox_InsertFeedbackDescription.Visible = true;
              DropDownList_InsertFeedbackProgressStatusList.Visible = true;
            }

            if (Session["Security"].ToString() == "1" && SecurityFormAdministrator.Length > 0)
            {
              Session["Security"] = "0";
              TextBox_InsertFeedbackDescription.Visible = true;
              DropDownList_InsertFeedbackProgressStatusList.Visible = true;
            }

            if (Session["Security"].ToString() == "1" && SecurityFormCoordinator.Length > 0)
            {
              Session["Security"] = "0";
              TextBox_InsertFeedbackDescription.Visible = false;
              DropDownList_InsertFeedbackProgressStatusList.Visible = false;
            }

            if (Session["Security"].ToString() == "1" && SecurityFormAssistant.Length > 0)
            {
              Session["Security"] = "0";
              TextBox_InsertFeedbackDescription.Visible = false;
              DropDownList_InsertFeedbackProgressStatusList.Visible = false;
            }
            Session["Security"] = "1";
          }
          else
          {
            TextBox_InsertFeedbackDescription.Visible = false;
            DropDownList_InsertFeedbackProgressStatusList.Visible = false;
          }
        }
      }
    }

    protected void EditDataBound()
    {
      TextBox TextBox_EditFeedbackDescription = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditFeedbackDescription");
      Label Label_EditFeedbackDescription = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditFeedbackDescription");
      DropDownList DropDownList_EditFeedbackProgressStatusList = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditFeedbackProgressStatusList");
      Label Label_EditFeedbackProgressStatusList1 = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditFeedbackProgressStatusList");
      CheckBox CheckBox_EditIsActive = (CheckBox)FormView_Pharmacy_NewProduct_Form.FindControl("CheckBox_EditIsActive");
      Label Label_EditIsActive = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditIsActive");

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('33'))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdministrator = DataTable_FormMode.Select("SecurityRole_Id = '135'");
            DataRow[] SecurityFormCoordinator = DataTable_FormMode.Select("SecurityRole_Id = '136'");
            DataRow[] SecurityFormAssistant = DataTable_FormMode.Select("SecurityRole_Id = '137'");

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && SecurityAdmin.Length > 0)
            {
              Session["Security"] = "0";
              TextBox_EditFeedbackDescription.Visible = true;
              Label_EditFeedbackDescription.Visible = false;
              DropDownList_EditFeedbackProgressStatusList.Visible = true;
              Label_EditFeedbackProgressStatusList1.Visible = false;
              CheckBox_EditIsActive.Visible = true;
              Label_EditIsActive.Visible = false;
            }

            if (Session["Security"].ToString() == "1" && SecurityFormAdministrator.Length > 0)
            {
              Session["Security"] = "0";
              TextBox_EditFeedbackDescription.Visible = true;
              Label_EditFeedbackDescription.Visible = false;
              DropDownList_EditFeedbackProgressStatusList.Visible = true;
              Label_EditFeedbackProgressStatusList1.Visible = false;
              CheckBox_EditIsActive.Visible = true;
              Label_EditIsActive.Visible = false;
            }

            if (Session["Security"].ToString() == "1" && SecurityFormCoordinator.Length > 0)
            {
              Session["Security"] = "0";
              TextBox_EditFeedbackDescription.Visible = false;
              Label_EditFeedbackDescription.Visible = true;
              DropDownList_EditFeedbackProgressStatusList.Visible = false;
              Label_EditFeedbackProgressStatusList1.Visible = true;
              CheckBox_EditIsActive.Visible = false;
              Label_EditIsActive.Visible = true;
            }

            if (Session["Security"].ToString() == "1" && SecurityFormAssistant.Length > 0)
            {
              Session["Security"] = "0";
              TextBox_EditFeedbackDescription.Visible = false;
              Label_EditFeedbackDescription.Visible = true;
              DropDownList_EditFeedbackProgressStatusList.Visible = false;
              Label_EditFeedbackProgressStatusList1.Visible = true;
              CheckBox_EditIsActive.Visible = false;
              Label_EditIsActive.Visible = true;
            }
            Session["Security"] = "1";
          }
          else
          {
            TextBox_EditFeedbackDescription.Visible = false;
            Label_EditFeedbackDescription.Visible = true;
            DropDownList_EditFeedbackProgressStatusList.Visible = false;
            Label_EditFeedbackProgressStatusList1.Visible = true;
            CheckBox_EditIsActive.Visible = false;
            Label_EditIsActive.Visible = true;
          }
        }
      }

      Session["FacilityFacilityDisplayName"] = "";
      Session["NewProductFeedbackProgressStatusName"] = "";
      string SQLStringPharmacyNewProduct = "SELECT Facility_FacilityDisplayName , NewProduct_Feedback_ProgressStatus_Name FROM vForm_Pharmacy_NewProduct WHERE NewProduct_Id = @NewProduct_Id";
      using (SqlCommand SqlCommand_PharmacyNewProduct = new SqlCommand(SQLStringPharmacyNewProduct))
      {
        SqlCommand_PharmacyNewProduct.Parameters.AddWithValue("@NewProduct_Id", Request.QueryString["NewProduct_Id"]);
        DataTable DataTable_PharmacyNewProduct;
        using (DataTable_PharmacyNewProduct = new DataTable())
        {
          DataTable_PharmacyNewProduct.Locale = CultureInfo.CurrentCulture;
          DataTable_PharmacyNewProduct = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PharmacyNewProduct).Copy();
          if (DataTable_PharmacyNewProduct.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PharmacyNewProduct.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["NewProductFeedbackProgressStatusName"] = DataRow_Row["NewProduct_Feedback_ProgressStatus_Name"];
            }
          }
        }
      }

      Label Label_EditFacility = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditFacility");
      Label_EditFacility.Text = Session["FacilityFacilityDisplayName"].ToString();

      Label Label_EditFeedbackProgressStatusList = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditFeedbackProgressStatusList");
      Label_EditFeedbackProgressStatusList.Text = Session["NewProductFeedbackProgressStatusName"].ToString();

      Session["FacilityFacilityDisplayName"] = "";
      Session["NewProductFeedbackProgressStatusName"] = "";


      if (Request.QueryString["NewProduct_Id"] != null)
      {
        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 33";
        using (SqlCommand SqlCommand_Email = new SqlCommand(SQLStringEmail))
        {
          DataTable DataTable_Email;
          using (DataTable_Email = new DataTable())
          {
            DataTable_Email.Locale = CultureInfo.CurrentCulture;
            DataTable_Email = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Email).Copy();
            if (DataTable_Email.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Email.Rows)
              {
                Email = DataRow_Row["Form_Email"].ToString();
                Print = DataRow_Row["Form_Print"].ToString();
              }
            }
          }
        }

        if (Print == "False")
        {
          ((Button)FormView_Pharmacy_NewProduct_Form.FindControl("Button_EditPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_Pharmacy_NewProduct_Form.FindControl("Button_EditPrint")).Visible = true;
        }

        if (Email == "False")
        {
          ((Button)FormView_Pharmacy_NewProduct_Form.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_Pharmacy_NewProduct_Form.FindControl("Button_EditEmail")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (Request.QueryString["NewProduct_Id"] != null)
      {
        Session["FacilityFacilityDisplayName"] = "";
        Session["NewProductProductClassificationName"] = "";
        Session["NewProductManufacturerName"] = "";
        Session["NewProductManufacturerCode"] = "";
        Session["NewProductUseName"] = "";
        Session["NewProductProductRequestName"] = "";
        Session["NewProductRequestByName"] = "";
        Session["NewProductFeedbackProgressStatusName"] = "";
        string SQLStringPharmacyNewProduct = "SELECT Facility_FacilityDisplayName , NewProduct_ProductClassification_Name , NewProduct_Manufacturer_Name , NewProduct_Manufacturer_Code , NewProduct_Use_Name , NewProduct_ProductRequest_Name , NewProduct_RequestBy_Name , NewProduct_Feedback_ProgressStatus_Name FROM vForm_Pharmacy_NewProduct WHERE NewProduct_Id = @NewProduct_Id";
        using (SqlCommand SqlCommand_PharmacyNewProduct = new SqlCommand(SQLStringPharmacyNewProduct))
        {
          SqlCommand_PharmacyNewProduct.Parameters.AddWithValue("@NewProduct_Id", Request.QueryString["NewProduct_Id"]);
          DataTable DataTable_PharmacyNewProduct;
          using (DataTable_PharmacyNewProduct = new DataTable())
          {
            DataTable_PharmacyNewProduct.Locale = CultureInfo.CurrentCulture;
            DataTable_PharmacyNewProduct = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PharmacyNewProduct).Copy();
            if (DataTable_PharmacyNewProduct.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PharmacyNewProduct.Rows)
              {
                Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
                Session["NewProductProductClassificationName"] = DataRow_Row["NewProduct_ProductClassification_Name"];
                Session["NewProductManufacturerName"] = DataRow_Row["NewProduct_Manufacturer_Name"];
                Session["NewProductManufacturerCode"] = DataRow_Row["NewProduct_Manufacturer_Code"];
                Session["NewProductUseName"] = DataRow_Row["NewProduct_Use_Name"];
                Session["NewProductProductRequestName"] = DataRow_Row["NewProduct_ProductRequest_Name"];
                Session["NewProductRequestByName"] = DataRow_Row["NewProduct_RequestBy_Name"];
                Session["NewProductFeedbackProgressStatusName"] = DataRow_Row["NewProduct_Feedback_ProgressStatus_Name"];
              }
            }
          }
        }


        Label Label_ItemFacility = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_ItemFacility");
        Label_ItemFacility.Text = Session["FacilityFacilityDisplayName"].ToString();

        Label Label_ItemProductClassificationList = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_ItemProductClassificationList");
        Label_ItemProductClassificationList.Text = Session["NewProductProductClassificationName"].ToString();

        Label Label_ItemManufacturerList = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_ItemManufacturerList");
        Label_ItemManufacturerList.Text = Convert.ToString(Session["NewProductManufacturerName"].ToString() + " (" + Session["NewProductManufacturerCode"].ToString() + ")", CultureInfo.CurrentCulture);

        Label Label_ItemUseList = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_ItemUseList");
        Label_ItemUseList.Text = Session["NewProductUseName"].ToString();

        Label Label_ItemProductRequestList = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_ItemProductRequestList");
        Label_ItemProductRequestList.Text = Session["NewProductProductRequestName"].ToString();

        Label Label_ItemRequestByList = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_ItemRequestByList");
        Label_ItemRequestByList.Text = Session["NewProductRequestByName"].ToString();

        Label Label_ItemFeedbackProgressStatusList = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_ItemFeedbackProgressStatusList");
        Label_ItemFeedbackProgressStatusList.Text = Session["NewProductFeedbackProgressStatusName"].ToString();

        Session["FacilityFacilityDisplayName"] = "";
        Session["NewProductProductClassificationName"] = "";
        Session["NewProductManufacturerName"] = "";
        Session["NewProductManufacturerCode"] = "";
        Session["NewProductUseName"] = "";
        Session["NewProductProductRequestName"] = "";
        Session["NewProductRequestByName"] = "";
        Session["NewProductFeedbackProgressStatusName"] = "";

        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 33";
        using (SqlCommand SqlCommand_Email = new SqlCommand(SQLStringEmail))
        {
          DataTable DataTable_Email;
          using (DataTable_Email = new DataTable())
          {
            DataTable_Email.Locale = CultureInfo.CurrentCulture;
            DataTable_Email = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Email).Copy();
            if (DataTable_Email.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Email.Rows)
              {
                Email = DataRow_Row["Form_Email"].ToString();
                Print = DataRow_Row["Form_Print"].ToString();
              }
            }
          }
        }

        if (Print == "False")
        {
          ((Button)FormView_Pharmacy_NewProduct_Form.FindControl("Button_ItemPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_Pharmacy_NewProduct_Form.FindControl("Button_ItemPrint")).Visible = true;
          ((Button)FormView_Pharmacy_NewProduct_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('InfoQuest_Print.aspx?PrintPage=Form_Pharmacy_NewProduct&PrintValue=" + Request.QueryString["NewProduct_Id"] + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_Pharmacy_NewProduct_Form.FindControl("Button_ItemEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_Pharmacy_NewProduct_Form.FindControl("Button_ItemEmail")).Visible = true;
          ((Button)FormView_Pharmacy_NewProduct_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('InfoQuest_Email.aspx?EmailPage=Form_Pharmacy_NewProduct&EmailValue=" + Request.QueryString["NewProduct_Id"] + "')";
        }

        Email = "";
        Print = "";
      }
    }


    public static string DatabaseFileName(object newProduct_File_Name)
    {
      string DataBaseFileName = "";

      if (newProduct_File_Name != null)
      {
        DataBaseFileName = "" + newProduct_File_Name.ToString() + "";
      }

      return DataBaseFileName;
    }

    protected void RetrieveDatabaseFile(object sender, EventArgs e)
    {
      LinkButton LinkButton_NewProductFile = (LinkButton)sender;
      string FileId = LinkButton_NewProductFile.CommandArgument.ToString();

      Session["NewProductFileName"] = "";
      Session["NewProductFileContentType"] = "";
      Session["NewProductFileData"] = "";
      string SQLStringNewProductFile = "SELECT NewProduct_File_Name ,NewProduct_File_ContentType ,NewProduct_File_Data FROM InfoQuest_Form_Pharmacy_NewProduct_File WHERE NewProduct_File_Id = @NewProduct_File_Id";
      using (SqlCommand SqlCommand_NewProductFile = new SqlCommand(SQLStringNewProductFile))
      {
        SqlCommand_NewProductFile.Parameters.AddWithValue("@NewProduct_File_Id", FileId);
        DataTable DataTable_NewProductFile;
        using (DataTable_NewProductFile = new DataTable())
        {
          DataTable_NewProductFile.Locale = CultureInfo.CurrentCulture;
          DataTable_NewProductFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_NewProductFile).Copy();
          if (DataTable_NewProductFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_NewProductFile.Rows)
            {
              Session["NewProductFileName"] = DataRow_Row["NewProduct_File_Name"];
              Session["NewProductFileContentType"] = DataRow_Row["NewProduct_File_ContentType"];
              Session["NewProductFileData"] = DataRow_Row["NewProduct_File_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["NewProductFileData"].ToString()))
      {
        Byte[] Byte_FileData = (Byte[])Session["NewProductFileData"];
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = Session["NewProductFileContentType"].ToString();
        Response.AddHeader("content-disposition", "attachment; filename=" + Session["NewProductFileName"].ToString() + "");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      Session["NewProductFileName"] = "";
      Session["NewProductFileContentType"] = "";
      Session["NewProductFileData"] = "";
    }

    protected static string FileUploadExtension(string fileExtension)
    {
      string FileContentType = string.Empty;

      switch (fileExtension)
      {
        case ".doc":
          FileContentType = "application/vnd.ms-word";
          break;
        case ".docx":
          FileContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
          break;
        case ".xls":
          FileContentType = "application/vnd.ms-excel";
          break;
        case ".xlsx":
          FileContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
          break;
        case ".pdf":
          FileContentType = "application/pdf";
          break;
        case ".tif":
          FileContentType = "image/tiff";
          break;
        case ".tiff":
          FileContentType = "image/tiff";
          break;
      }

      return FileContentType;
    }


    protected void SqlDataSource_Pharmacy_NewProduct_InsertFile_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;
        HiddenField HiddenField_InsertFile = (HiddenField)FormView_Pharmacy_NewProduct_Form.FindControl("HiddenField_InsertFile");
        HiddenField_InsertFile.Value = TotalRecords.ToString(CultureInfo.CurrentCulture);

        GridView GridView_InsertFile = (GridView)FormView_Pharmacy_NewProduct_Form.FindControl("GridView_InsertFile");

        if (TotalRecords > 0)
        {
          GridView_InsertFile.Visible = true;
        }
        else
        {
          GridView_InsertFile.Visible = false;
        }
      }
    }

    protected void GridView_InsertFile_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_InsertFile_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          int m = e.Row.Cells.Count;

          for (int i = m - 1; i >= 1; i += -1)
          {
            e.Row.Cells.RemoveAt(i);
          }

          e.Row.Cells[0].ColumnSpan = m;
          e.Row.Cells[0].Text = Convert.ToString("&nbsp;", CultureInfo.CurrentCulture);
        }
      }
    }

    protected void Button_InsertUploadFile_OnClick(object sender, EventArgs e)
    {
      string UploadMessage = "";

      Label Label_InsertMessageFile = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_InsertMessageFile");
      FileUpload FileUpload_InsertFile = (FileUpload)FormView_Pharmacy_NewProduct_Form.FindControl("FileUpload_InsertFile");
      DropDownList DropDownList_InsertField = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_InsertField");
      //Button Button_InsertUploadFile = (Button)sender;

      if (string.IsNullOrEmpty(DropDownList_InsertField.SelectedValue))
      {
        UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>Field not selected</span>";
      }
      else
      {
        if (!FileUpload_InsertFile.HasFiles)
        {
          UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>No file chosen</span>";
        }
        else
        {
          foreach (HttpPostedFile HttpPostedFile_File in FileUpload_InsertFile.PostedFiles)
          {
            string FileName = Path.GetFileName(HttpPostedFile_File.FileName);
            string FileExtension = System.IO.Path.GetExtension(FileName);
            FileExtension = FileExtension.ToLower(CultureInfo.CurrentCulture);
            string FileContentType = string.Empty;
            decimal FileSize = HttpPostedFile_File.ContentLength;
            decimal FileSizeMB = (FileSize / 1024 / 1024);
            string FileSizeMBString = FileSizeMB.ToString("N2", CultureInfo.CurrentCulture);

            FileContentType = FileUploadExtension(FileExtension);

            if ((!string.IsNullOrEmpty(FileContentType)) && (FileSize < 5242880))
            {
              HiddenField HiddenField_InsertNewProductFileTemp = (HiddenField)FormView_Pharmacy_NewProduct_Form.FindControl("HiddenField_InsertNewProductFileTemp");

              Session["NewProductFileId"] = "";
              string SQLStringExistingFile = "SELECT NewProduct_File_Id FROM InfoQuest_Form_Pharmacy_NewProduct_File WHERE NewProduct_File_Field_List = @NewProduct_File_Field_List AND NewProduct_File_CreatedBy = @NewProduct_File_CreatedBy AND NewProduct_File_Temp_NewProduct_Id = @NewProduct_File_Temp_NewProduct_Id AND NewProduct_File_Name = @NewProduct_File_Name";
              using (SqlCommand SqlCommand_ExistingFile = new SqlCommand(SQLStringExistingFile))
              {
                SqlCommand_ExistingFile.Parameters.AddWithValue("@NewProduct_File_Field_List", DropDownList_InsertField.SelectedValue.ToString());
                SqlCommand_ExistingFile.Parameters.AddWithValue("@NewProduct_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);
                SqlCommand_ExistingFile.Parameters.AddWithValue("@NewProduct_File_Temp_NewProduct_Id", HiddenField_InsertNewProductFileTemp.Value);
                SqlCommand_ExistingFile.Parameters.AddWithValue("@NewProduct_File_Name", FileName);
                DataTable DataTable_ExistingFile;
                using (DataTable_ExistingFile = new DataTable())
                {
                  DataTable_ExistingFile.Locale = CultureInfo.CurrentCulture;
                  DataTable_ExistingFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ExistingFile).Copy();
                  if (DataTable_ExistingFile.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_ExistingFile.Rows)
                    {
                      Session["NewProductFileId"] = DataRow_Row["NewProduct_File_Id"];
                    }
                  }
                  else
                  {
                    Session["NewProductFileId"] = "";
                  }
                }
              }

              if (!string.IsNullOrEmpty(Session["NewProductFileId"].ToString()))
              {
                UploadMessage = "<span style='color:#d46e6e;'>File Uploading Failed<br/>File already uploaded<br/>File Name: " + FileName + "</span>";
              }
              else
              {
                Stream Stream_File = HttpPostedFile_File.InputStream;
                BinaryReader BinaryReader_File = new BinaryReader(Stream_File);
                Byte[] Byte_File = BinaryReader_File.ReadBytes((Int32)Stream_File.Length);

                string SQLStringInsertFile = "INSERT INTO InfoQuest_Form_Pharmacy_NewProduct_File ( NewProduct_Id ,NewProduct_File_Temp_NewProduct_Id ,NewProduct_File_Field_List ,NewProduct_File_Name ,NewProduct_File_ContentType ,NewProduct_File_Data ,NewProduct_File_CreatedDate ,NewProduct_File_CreatedBy ) VALUES ( @NewProduct_Id ,@NewProduct_File_Temp_NewProduct_Id ,@NewProduct_File_Field_List ,@NewProduct_File_Name ,@NewProduct_File_ContentType ,@NewProduct_File_Data ,@NewProduct_File_CreatedDate ,@NewProduct_File_CreatedBy )";
                using (SqlCommand SqlCommand_InsertFile = new SqlCommand(SQLStringInsertFile))
                {
                  SqlCommand_InsertFile.Parameters.AddWithValue("@NewProduct_Id", DBNull.Value);
                  SqlCommand_InsertFile.Parameters.AddWithValue("@NewProduct_File_Temp_NewProduct_Id", HiddenField_InsertNewProductFileTemp.Value);
                  SqlCommand_InsertFile.Parameters.AddWithValue("@NewProduct_File_Field_List", DropDownList_InsertField.SelectedValue.ToString());
                  SqlCommand_InsertFile.Parameters.AddWithValue("@NewProduct_File_Name", FileName);
                  SqlCommand_InsertFile.Parameters.AddWithValue("@NewProduct_File_ContentType", FileContentType);
                  SqlCommand_InsertFile.Parameters.AddWithValue("@NewProduct_File_Data", Byte_File);
                  SqlCommand_InsertFile.Parameters.AddWithValue("@NewProduct_File_CreatedDate", DateTime.Now);
                  SqlCommand_InsertFile.Parameters.AddWithValue("@NewProduct_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertFile);
                }

                GridView GridView_InsertFile = (GridView)FormView_Pharmacy_NewProduct_Form.FindControl("GridView_InsertFile");
                SqlDataSource_Pharmacy_NewProduct_File_InsertFile.SelectParameters["NewProduct_File_Temp_NewProduct_Id"].DefaultValue = HiddenField_InsertNewProductFileTemp.Value;
                GridView_InsertFile.DataBind();

                BulletedList BulletedList_InsertFieldMissing = (BulletedList)FormView_Pharmacy_NewProduct_Form.FindControl("BulletedList_InsertFieldMissing");
                SqlDataSource_Pharmacy_NewProduct_File_InsertFieldMissing.SelectParameters["NewProduct_File_Temp_NewProduct_Id"].DefaultValue = HiddenField_InsertNewProductFileTemp.Value;
                BulletedList_InsertFieldMissing.DataBind();
              }
            }
            else
            {
              if (FileExtension != "xls" && FileExtension != "xlsx" && FileExtension != "doc" && FileExtension != "docx" && FileExtension != "pdf" && FileExtension != "tif" && FileExtension != "tiff")
              {
                UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>Only doc, docx, xls, xlsx, pdf, tif and tiff files can be uploaded<br/>File Name: " + FileName + "</span>";
              }

              if (FileSize > 5242880)
              {
                UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>Only files smaller then 5 MB can be uploaded<br/>File Name: " + FileName + "<br/>File Size: " + FileSizeMBString + " MB</span>";
              }
            }
          }
        }
      }

      Label_InsertMessageFile.Text = Convert.ToString(UploadMessage, CultureInfo.CurrentCulture);
      DropDownList_InsertField.SelectedValue = "";
    }

    protected void Button_InsertDeleteFile_OnClick(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      //Button Button_InsertDeleteFile = (Button)sender;
      GridView GridView_InsertFile = (GridView)FormView_Pharmacy_NewProduct_Form.FindControl("GridView_InsertFile");
      Label Label_InsertMessageFile = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_InsertMessageFile");
      BulletedList BulletedList_InsertFieldMissing = (BulletedList)FormView_Pharmacy_NewProduct_Form.FindControl("BulletedList_InsertFieldMissing");

      for (int i = 0; i < GridView_InsertFile.Rows.Count; i++)
      {
        CheckBox CheckBox_InsertFile = (CheckBox)GridView_InsertFile.Rows[i].Cells[0].FindControl("CheckBox_InsertFile");
        Int32 FileId = 0;

        if (CheckBox_InsertFile.Checked == true)
        {
          FileId = Convert.ToInt32(CheckBox_InsertFile.CssClass, CultureInfo.CurrentCulture);

          string SQLStringNewProductFile = "DELETE FROM InfoQuest_Form_Pharmacy_NewProduct_File WHERE NewProduct_File_Id = @NewProduct_File_Id";
          using (SqlCommand SqlCommand_NewProductFile = new SqlCommand(SQLStringNewProductFile))
          {
            SqlCommand_NewProductFile.Parameters.AddWithValue("@NewProduct_File_Id", FileId);

            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_NewProductFile);

            DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
          }
        }
      }

      Label_InsertMessageFile.Text = DeleteMessage;
      GridView_InsertFile.DataBind();
      BulletedList_InsertFieldMissing.DataBind();
    }

    protected void Button_InsertDeleteAllFile_OnClick(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      //Button Button_InsertDeleteAllFile = (Button)sender;
      GridView GridView_InsertFile = (GridView)FormView_Pharmacy_NewProduct_Form.FindControl("GridView_InsertFile");
      Label Label_InsertMessageFile = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_InsertMessageFile");
      BulletedList BulletedList_InsertFieldMissing = (BulletedList)FormView_Pharmacy_NewProduct_Form.FindControl("BulletedList_InsertFieldMissing");

      for (int i = 0; i < GridView_InsertFile.Rows.Count; i++)
      {
        CheckBox CheckBox_InsertFile = (CheckBox)GridView_InsertFile.Rows[i].Cells[0].FindControl("CheckBox_InsertFile");
        Int32 FileId = 0;

        FileId = Convert.ToInt32(CheckBox_InsertFile.CssClass, CultureInfo.CurrentCulture);

        string SQLStringNewProductFile = "DELETE FROM InfoQuest_Form_Pharmacy_NewProduct_File WHERE NewProduct_File_Id = @NewProduct_File_Id";
        using (SqlCommand SqlCommand_NewProductFile = new SqlCommand(SQLStringNewProductFile))
        {
          SqlCommand_NewProductFile.Parameters.AddWithValue("@NewProduct_File_Id", FileId);

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_NewProductFile);

          DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
        }
      }

      Label_InsertMessageFile.Text = DeleteMessage;
      GridView_InsertFile.DataBind();
      BulletedList_InsertFieldMissing.DataBind();
    }


    protected void SqlDataSource_Pharmacy_NewProduct_EditFile_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;
        HiddenField HiddenField_EditFile = (HiddenField)FormView_Pharmacy_NewProduct_Form.FindControl("HiddenField_EditFile");
        HiddenField_EditFile.Value = TotalRecords.ToString(CultureInfo.CurrentCulture);

        GridView GridView_EditFile = (GridView)FormView_Pharmacy_NewProduct_Form.FindControl("GridView_EditFile");

        if (TotalRecords > 0)
        {
          GridView_EditFile.Visible = true;
        }
        else
        {
          GridView_EditFile.Visible = false;
        }
      }
    }

    protected void GridView_EditFile_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_EditFile_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          int m = e.Row.Cells.Count;

          for (int i = m - 1; i >= 1; i += -1)
          {
            e.Row.Cells.RemoveAt(i);
          }

          e.Row.Cells[0].ColumnSpan = m;
          e.Row.Cells[0].Text = Convert.ToString("&nbsp;", CultureInfo.CurrentCulture);
        }
      }
    }

    protected void Button_EditUploadFile_OnClick(object sender, EventArgs e)
    {
      string UploadMessage = "";

      Label Label_EditMessageFile = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditMessageFile");
      FileUpload FileUpload_EditFile = (FileUpload)FormView_Pharmacy_NewProduct_Form.FindControl("FileUpload_EditFile");
      DropDownList DropDownList_EditField = (DropDownList)FormView_Pharmacy_NewProduct_Form.FindControl("DropDownList_EditField");
      //Button Button_EditUploadFile = (Button)sender;

      if (string.IsNullOrEmpty(DropDownList_EditField.SelectedValue))
      {
        UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>Field not selected</span>";
      }
      else
      {
        if (!FileUpload_EditFile.HasFiles)
        {
          UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>No file chosen</span>";
        }
        else
        {
          foreach (HttpPostedFile HttpPostedFile_File in FileUpload_EditFile.PostedFiles)
          {
            string FileName = Path.GetFileName(HttpPostedFile_File.FileName);
            string FileExtension = System.IO.Path.GetExtension(FileName);
            FileExtension = FileExtension.ToLower(CultureInfo.CurrentCulture);
            string FileContentType = string.Empty;
            decimal FileSize = HttpPostedFile_File.ContentLength;
            decimal FileSizeMB = (FileSize / 1024 / 1024);
            string FileSizeMBString = FileSizeMB.ToString("N2", CultureInfo.CurrentCulture);

            FileContentType = FileUploadExtension(FileExtension);

            if ((!string.IsNullOrEmpty(FileContentType)) && (FileSize < 5242880))
            {
              Session["NewProductFileId"] = "";
              string SQLStringExistingFile = "SELECT NewProduct_File_Id FROM InfoQuest_Form_Pharmacy_NewProduct_File WHERE NewProduct_File_Field_List = @NewProduct_File_Field_List AND NewProduct_File_CreatedBy = @NewProduct_File_CreatedBy AND NewProduct_Id = @NewProduct_Id AND NewProduct_File_Name = @NewProduct_File_Name";
              using (SqlCommand SqlCommand_ExistingFile = new SqlCommand(SQLStringExistingFile))
              {
                SqlCommand_ExistingFile.Parameters.AddWithValue("@NewProduct_File_Field_List", DropDownList_EditField.SelectedValue.ToString());
                SqlCommand_ExistingFile.Parameters.AddWithValue("@NewProduct_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);
                SqlCommand_ExistingFile.Parameters.AddWithValue("@NewProduct_Id", Request.QueryString["NewProduct_Id"]);
                SqlCommand_ExistingFile.Parameters.AddWithValue("@NewProduct_File_Name", FileName);
                DataTable DataTable_ExistingFile;
                using (DataTable_ExistingFile = new DataTable())
                {
                  DataTable_ExistingFile.Locale = CultureInfo.CurrentCulture;
                  DataTable_ExistingFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ExistingFile).Copy();
                  if (DataTable_ExistingFile.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_ExistingFile.Rows)
                    {
                      Session["NewProductFileId"] = DataRow_Row["NewProduct_File_Id"];
                    }
                  }
                  else
                  {
                    Session["NewProductFileId"] = "";
                  }
                }
              }

              if (!string.IsNullOrEmpty(Session["NewProductFileId"].ToString()))
              {
                UploadMessage = "<span style='color:#d46e6e;'>File Uploading Failed<br/>File already uploaded<br/>File Name: " + FileName + "</span>";
              }
              else
              {
                Stream Stream_File = HttpPostedFile_File.InputStream;
                BinaryReader BinaryReader_File = new BinaryReader(Stream_File);
                Byte[] Byte_File = BinaryReader_File.ReadBytes((Int32)Stream_File.Length);

                string SQLStringEditFile = "INSERT INTO InfoQuest_Form_Pharmacy_NewProduct_File ( NewProduct_Id ,NewProduct_File_Temp_NewProduct_Id ,NewProduct_File_Field_List ,NewProduct_File_Name ,NewProduct_File_ContentType ,NewProduct_File_Data ,NewProduct_File_CreatedDate ,NewProduct_File_CreatedBy ) VALUES ( @NewProduct_Id ,@NewProduct_File_Temp_NewProduct_Id ,@NewProduct_File_Field_List ,@NewProduct_File_Name ,@NewProduct_File_ContentType ,@NewProduct_File_Data ,@NewProduct_File_CreatedDate ,@NewProduct_File_CreatedBy )";
                using (SqlCommand SqlCommand_EditFile = new SqlCommand(SQLStringEditFile))
                {
                  SqlCommand_EditFile.Parameters.AddWithValue("@NewProduct_Id", Request.QueryString["NewProduct_Id"]);
                  SqlCommand_EditFile.Parameters.AddWithValue("@NewProduct_File_Temp_NewProduct_Id", DBNull.Value);
                  SqlCommand_EditFile.Parameters.AddWithValue("@NewProduct_File_Field_List", DropDownList_EditField.SelectedValue.ToString());
                  SqlCommand_EditFile.Parameters.AddWithValue("@NewProduct_File_Name", FileName);
                  SqlCommand_EditFile.Parameters.AddWithValue("@NewProduct_File_ContentType", FileContentType);
                  SqlCommand_EditFile.Parameters.AddWithValue("@NewProduct_File_Data", Byte_File);
                  SqlCommand_EditFile.Parameters.AddWithValue("@NewProduct_File_CreatedDate", DateTime.Now);
                  SqlCommand_EditFile.Parameters.AddWithValue("@NewProduct_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditFile);
                }

                GridView GridView_EditNewProductFile = (GridView)FormView_Pharmacy_NewProduct_Form.FindControl("GridView_EditFile");
                GridView_EditNewProductFile.DataBind();

                BulletedList BulletedList_EditFieldMissing = (BulletedList)FormView_Pharmacy_NewProduct_Form.FindControl("BulletedList_EditFieldMissing");
                BulletedList_EditFieldMissing.DataBind();
              }
            }
            else
            {
              if (FileExtension != "xls" && FileExtension != "xlsx" && FileExtension != "doc" && FileExtension != "docx" && FileExtension != "pdf" && FileExtension != "tif" && FileExtension != "tiff")
              {
                UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>Only doc, docx, xls, xlsx, pdf, tif and tiff files can be uploaded<br/>File Name: " + FileName + "</span>";
              }

              if (FileSize > 5242880)
              {
                UploadMessage = UploadMessage + "<span style='color:#d46e6e;'>File Uploading Failed<br/>Only files smaller then 5 MB can be uploaded<br/>File Name: " + FileName + "<br/>File Size: " + FileSizeMBString + " MB</span>";
              }
            }
          }
        }
      }

      Label_EditMessageFile.Text = Convert.ToString(UploadMessage, CultureInfo.CurrentCulture);
      DropDownList_EditField.SelectedValue = "";
    }

    protected void Button_EditDeleteFile_OnClick(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      //Button Button_EditDeleteFile = (Button)sender;
      GridView GridView_EditFile = (GridView)FormView_Pharmacy_NewProduct_Form.FindControl("GridView_EditFile");
      Label Label_EditMessageFile = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditMessageFile");
      BulletedList BulletedList_EditFieldMissing = (BulletedList)FormView_Pharmacy_NewProduct_Form.FindControl("BulletedList_EditFieldMissing");

      for (int i = 0; i < GridView_EditFile.Rows.Count; i++)
      {
        CheckBox CheckBox_EditFile = (CheckBox)GridView_EditFile.Rows[i].Cells[0].FindControl("CheckBox_EditFile");
        Int32 FileId = 0;

        if (CheckBox_EditFile.Checked == true)
        {
          FileId = Convert.ToInt32(CheckBox_EditFile.CssClass, CultureInfo.CurrentCulture);

          string SQLStringNewProductFile = "DELETE FROM InfoQuest_Form_Pharmacy_NewProduct_File WHERE NewProduct_File_Id = @NewProduct_File_Id";
          using (SqlCommand SqlCommand_NewProductFile = new SqlCommand(SQLStringNewProductFile))
          {
            SqlCommand_NewProductFile.Parameters.AddWithValue("@NewProduct_File_Id", FileId);

            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_NewProductFile);

            DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
          }
        }
      }

      Label_EditMessageFile.Text = DeleteMessage;
      GridView_EditFile.DataBind();
      BulletedList_EditFieldMissing.DataBind();
    }

    protected void Button_EditDeleteAllFile_OnClick(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      //Button Button_EditDeleteAllFile = (Button)sender;
      GridView GridView_EditFile = (GridView)FormView_Pharmacy_NewProduct_Form.FindControl("GridView_EditFile");
      Label Label_EditMessageFile = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditMessageFile");
      BulletedList BulletedList_EditFieldMissing = (BulletedList)FormView_Pharmacy_NewProduct_Form.FindControl("BulletedList_EditFieldMissing");

      for (int i = 0; i < GridView_EditFile.Rows.Count; i++)
      {
        CheckBox CheckBox_EditFile = (CheckBox)GridView_EditFile.Rows[i].Cells[0].FindControl("CheckBox_EditFile");
        Int32 FileId = 0;

        FileId = Convert.ToInt32(CheckBox_EditFile.CssClass, CultureInfo.CurrentCulture);

        string SQLStringNewProductFile = "DELETE FROM InfoQuest_Form_Pharmacy_NewProduct_File WHERE NewProduct_File_Id = @NewProduct_File_Id";
        using (SqlCommand SqlCommand_NewProductFile = new SqlCommand(SQLStringNewProductFile))
        {
          SqlCommand_NewProductFile.Parameters.AddWithValue("@NewProduct_File_Id", FileId);

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_NewProductFile);

          DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
        }
      }

      Label_EditMessageFile.Text = DeleteMessage;
      GridView_EditFile.DataBind();
      BulletedList_EditFieldMissing.DataBind();
    }


    protected void SqlDataSource_Pharmacy_NewProduct_ItemFile_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;
        GridView GridView_ItemFile = (GridView)FormView_Pharmacy_NewProduct_Form.FindControl("GridView_ItemFile");

        if (TotalRecords > 0)
        {
          GridView_ItemFile.Visible = true;
        }
        else
        {
          GridView_ItemFile.Visible = false;
        }
      }
    }

    protected void GridView_ItemFile_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_ItemFile_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          int m = e.Row.Cells.Count;

          for (int i = m - 1; i >= 1; i += -1)
          {
            e.Row.Cells.RemoveAt(i);
          }

          e.Row.Cells[0].ColumnSpan = m;
          e.Row.Cells[0].Text = Convert.ToString("&nbsp;", CultureInfo.CurrentCulture);
        }
      }
    }


    protected void TextBox_InsertNappiCode_TextChanged(object sender, EventArgs e)
    {
      Label Label_InsertNappiCodeError = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_InsertNappiCodeError");
      TextBox TextBox_InsertNappiCode = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_InsertNappiCode");

      if (TextBox_InsertNappiCode.Text.ToString() != "______-___")
      {
        string NappiCode = TextBox_InsertNappiCode.Text.ToString();
        Int32 NappiCodeValid = NappiCode.IndexOf("_", StringComparison.CurrentCulture);

        if (NappiCodeValid >= 0 && NappiCodeValid <= 5)
        {
          Label_InsertNappiCodeError.Text = Convert.ToString("9 Digit NAPPI code is not in the correct format, first 6 digits are required", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_InsertNappiCodeError.Text = "";
        }
      }
    }

    protected void TextBox_EditNappiCode_TextChanged(object sender, EventArgs e)
    {
      Label Label_EditNappiCodeError = (Label)FormView_Pharmacy_NewProduct_Form.FindControl("Label_EditNappiCodeError");
      TextBox TextBox_EditNappiCode = (TextBox)FormView_Pharmacy_NewProduct_Form.FindControl("TextBox_EditNappiCode");

      if (TextBox_EditNappiCode.Text.ToString() != "______-___")
      {
        string NappiCode = TextBox_EditNappiCode.Text.ToString();
        Int32 NappiCodeValid = NappiCode.IndexOf("_", StringComparison.CurrentCulture);

        if (NappiCodeValid >= 0 && NappiCodeValid <= 5)
        {
          Label_EditNappiCodeError.Text = Convert.ToString("9 Digit NAPPI code is not in the correct format, first 6 digits are required", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_EditNappiCodeError.Text = "";
        }
      }
    }


    protected void Button_InsertCancel_Click(object sender, EventArgs e)
    {
      NewProductFileCleanUp();
      RedirectToList();
    }

    protected void Button_InsertClear_Click(object sender, EventArgs e)
    {
      NewProductFileCleanUp();
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("New Product Code Request Form", "Form_Pharmacy_NewProduct.aspx"), false);
    }

    protected void Button_EditCancel_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_EditClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("New Product Code Request Form", "Form_Pharmacy_NewProduct.aspx"), false);
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }

    protected void Button_EditPrint_Click(object sender, EventArgs e)
    {
      Button_EditPrintClicked = true;
    }

    protected void Button_EditEmail_Click(object sender, EventArgs e)
    {
      Button_EditEmailClicked = true;
    }

    protected void Button_ItemCancel_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_ItemClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("New Product Code Request Form", "Form_Pharmacy_NewProduct.aspx"), false);
    }
    //---END--- --TableForm--// 
  }
}