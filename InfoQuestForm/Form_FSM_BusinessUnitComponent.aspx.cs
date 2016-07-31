using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_FSM_BusinessUnitComponent : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_FSM_BusinessUnitComponent, this.GetType(), "UpdateProgress_Start", "Validation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          PageTitle();

          SetBusinessUnitComponentVisibility();

          TableBusinessUnitComponentVisible();
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('41'))";
        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("41");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_FSM_BusinessUnitComponent.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Facility Structure Maintenance", "15");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_FSM_BusinessUnitComponent_InsertBusinessUnitKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_InsertBusinessUnitKey.SelectCommand = "SELECT BusinessUnitKey , BusinessUnitName FROM BusinessUnit.BusinessUnit WHERE IsActive = 1 ORDER BY BusinessUnitName";

      SqlDataSource_FSM_BusinessUnitComponent_InsertBusinessUnitComponentTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_InsertBusinessUnitComponentTypeKey.SelectCommand = "SELECT BusinessUnitComponentTypeKey , BusinessUnitComponentTypeName FROM BusinessUnit.BusinessUnitComponentType ORDER BY BusinessUnitComponentTypeName";

      SqlDataSource_FSM_BusinessUnitComponent_InsertWardTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_InsertWardTypeKey.SelectCommand = "SELECT WardTypeKey , WardTypeName FROM FinanceMapping.ValidCombinations INNER JOIN BusinessUnit.BusinessUnit ON ValidCombinations.Entity = BusinessUnit.BusinessUnitDefaultEntity INNER JOIN Hospital.WardType ON ValidCombinations.CostCentre = WardType.Centre WHERE BusinessUnitKey = @BusinessUnitKey ORDER BY WardTypeName";
      SqlDataSource_FSM_BusinessUnitComponent_InsertWardTypeKey.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_InsertWardTypeKey.SelectParameters.Add("BusinessUnitKey", TypeCode.String, "");

      SqlDataSource_FSM_BusinessUnitComponent_InsertNursingDisciplineKey_Ward.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_InsertNursingDisciplineKey_Ward.SelectCommand = "SELECT NursingDisciplineKey , NursingDisciplineName FROM Hospital.NursingDiscipline WHERE IsActive = 1 ORDER BY NursingDisciplineName";

      SqlDataSource_FSM_BusinessUnitComponent_InsertTheatreComplexTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_InsertTheatreComplexTypeKey.SelectCommand = "SELECT TheatreComplexTypeKey , TheatreComplexTypeName FROM FinanceMapping.ValidCombinations INNER JOIN BusinessUnit.BusinessUnit ON ValidCombinations.Entity = BusinessUnit.BusinessUnitDefaultEntity INNER JOIN Hospital.TheatreComplexType ON ValidCombinations.CostCentre = TheatreComplexType.Centre WHERE BusinessUnitKey = @BusinessUnitKey ORDER BY TheatreComplexTypeName";
      SqlDataSource_FSM_BusinessUnitComponent_InsertTheatreComplexTypeKey.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_InsertTheatreComplexTypeKey.SelectParameters.Add("BusinessUnitKey", TypeCode.String, "");

      SqlDataSource_FSM_BusinessUnitComponent_InsertNursingDisciplineKey_TheatreComplex.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_InsertNursingDisciplineKey_TheatreComplex.SelectCommand = "SELECT NursingDisciplineKey , NursingDisciplineName FROM Hospital.NursingDiscipline WHERE IsActive = 1 ORDER BY NursingDisciplineName";

      SqlDataSource_FSM_BusinessUnitComponent_InsertOperatingTheatreTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_InsertOperatingTheatreTypeKey.SelectCommand = "SELECT OperatingTheatreTypeKey , OperatingTheatreTypeName FROM Hospital.OperatingTheatreType ORDER BY OperatingTheatreTypeName";

      SqlDataSource_FSM_BusinessUnitComponent_InsertStockroomTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_InsertStockroomTypeKey.SelectCommand = "SELECT StockRoomTypeKey , StockRoomTypeName FROM BusinessUnit.StockRoomType ORDER BY StockRoomTypeName";

      SqlDataSource_FSM_BusinessUnitComponent_InsertSupportFunctionTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_InsertSupportFunctionTypeKey.SelectCommand = "SELECT SupportFunctionTypeKey , SupportFunctionTypeName FROM FinanceMapping.ValidCombinations INNER JOIN BusinessUnit.BusinessUnit ON ValidCombinations.Entity = BusinessUnit.BusinessUnitDefaultEntity INNER JOIN BusinessUnit.SupportFunctionType ON ValidCombinations.CostCentre = SupportFunctionType.Centre WHERE BusinessUnitKey = @BusinessUnitKey ORDER BY SupportFunctionTypeName";
      SqlDataSource_FSM_BusinessUnitComponent_InsertSupportFunctionTypeKey.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_InsertSupportFunctionTypeKey.SelectParameters.Add("BusinessUnitKey", TypeCode.String, "");

      SqlDataSource_FSM_BusinessUnitComponent_EditBusinessUnitKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_EditBusinessUnitKey.SelectCommand = "SELECT BusinessUnitKey , BusinessUnitName FROM BusinessUnit.BusinessUnit WHERE IsActive = 1 UNION SELECT BusinessUnitComponent.BusinessUnitKey , BusinessUnit.BusinessUnitName FROM BusinessUnit.BusinessUnitComponent LEFT JOIN BusinessUnit.BusinessUnit ON BusinessUnitComponent.BusinessUnitKey = BusinessUnit.BusinessUnitKey WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey ORDER BY BusinessUnitName";
      SqlDataSource_FSM_BusinessUnitComponent_EditBusinessUnitKey.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_EditBusinessUnitKey.SelectParameters.Add("BusinessUnitComponentKey", TypeCode.String, Request.QueryString["BusinessUnitComponentKey"]);

      SqlDataSource_FSM_BusinessUnitComponent_EditBusinessUnitComponentTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_EditBusinessUnitComponentTypeKey.SelectCommand = "SELECT BusinessUnitComponentTypeKey , BusinessUnitComponentTypeName FROM BusinessUnit.BusinessUnitComponentType ORDER BY BusinessUnitComponentTypeName";

      SqlDataSource_FSM_BusinessUnitComponent_EditWardTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_EditWardTypeKey.SelectCommand = "SELECT WardTypeKey , WardTypeName FROM FinanceMapping.ValidCombinations INNER JOIN BusinessUnit.BusinessUnit ON ValidCombinations.Entity = BusinessUnit.BusinessUnitDefaultEntity INNER JOIN Hospital.WardType ON ValidCombinations.CostCentre = WardType.Centre WHERE BusinessUnitKey = @BusinessUnitKey ORDER BY WardTypeName";
      SqlDataSource_FSM_BusinessUnitComponent_EditWardTypeKey.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_EditWardTypeKey.SelectParameters.Add("BusinessUnitKey", TypeCode.String, "");

      SqlDataSource_FSM_BusinessUnitComponent_EditNursingDisciplineKey_Ward.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_EditNursingDisciplineKey_Ward.SelectCommand = "SELECT NursingDisciplineKey , NursingDisciplineName FROM Hospital.NursingDiscipline WHERE IsActive = 1 UNION SELECT Ward.NursingDisciplineKey , NursingDiscipline.NursingDisciplineName FROM Hospital.Ward LEFT JOIN Hospital.NursingDiscipline ON Ward.NursingDisciplineKey = NursingDiscipline.NursingDisciplineKey WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey ORDER BY NursingDisciplineName";
      SqlDataSource_FSM_BusinessUnitComponent_EditNursingDisciplineKey_Ward.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_EditNursingDisciplineKey_Ward.SelectParameters.Add("BusinessUnitComponentKey", TypeCode.String, Request.QueryString["BusinessUnitComponentKey"]);

      SqlDataSource_FSM_BusinessUnitComponent_EditTheatreComplexTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_EditTheatreComplexTypeKey.SelectCommand = "SELECT TheatreComplexTypeKey , TheatreComplexTypeName FROM FinanceMapping.ValidCombinations INNER JOIN BusinessUnit.BusinessUnit ON ValidCombinations.Entity = BusinessUnit.BusinessUnitDefaultEntity INNER JOIN Hospital.TheatreComplexType ON ValidCombinations.CostCentre = TheatreComplexType.Centre WHERE BusinessUnitKey = @BusinessUnitKey ORDER BY TheatreComplexTypeName";
      SqlDataSource_FSM_BusinessUnitComponent_EditTheatreComplexTypeKey.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_EditTheatreComplexTypeKey.SelectParameters.Add("BusinessUnitKey", TypeCode.String, "");

      SqlDataSource_FSM_BusinessUnitComponent_EditNursingDisciplineKey_TheatreComplex.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_EditNursingDisciplineKey_TheatreComplex.SelectCommand = "SELECT NursingDisciplineKey , NursingDisciplineName FROM Hospital.NursingDiscipline WHERE IsActive = 1 UNION SELECT TheatreComplex.NursingDisciplineKey , NursingDiscipline.NursingDisciplineName FROM Hospital.TheatreComplex LEFT JOIN Hospital.NursingDiscipline ON TheatreComplex.NursingDisciplineKey = NursingDiscipline.NursingDisciplineKey WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey ORDER BY NursingDisciplineName";
      SqlDataSource_FSM_BusinessUnitComponent_EditNursingDisciplineKey_TheatreComplex.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_EditNursingDisciplineKey_TheatreComplex.SelectParameters.Add("BusinessUnitComponentKey", TypeCode.String, Request.QueryString["BusinessUnitComponentKey"]);

      SqlDataSource_FSM_BusinessUnitComponent_EditOperatingTheatreTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_EditOperatingTheatreTypeKey.SelectCommand = "SELECT OperatingTheatreTypeKey , OperatingTheatreTypeName FROM Hospital.OperatingTheatreType ORDER BY OperatingTheatreTypeName";

      SqlDataSource_FSM_BusinessUnitComponent_EditStockroomTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_EditStockroomTypeKey.SelectCommand = "SELECT StockRoomTypeKey , StockRoomTypeName FROM BusinessUnit.StockRoomType ORDER BY StockRoomTypeName";

      SqlDataSource_FSM_BusinessUnitComponent_EditSupportFunctionTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_EditSupportFunctionTypeKey.SelectCommand = "SELECT SupportFunctionTypeKey , SupportFunctionTypeName FROM FinanceMapping.ValidCombinations INNER JOIN BusinessUnit.BusinessUnit ON ValidCombinations.Entity = BusinessUnit.BusinessUnitDefaultEntity INNER JOIN BusinessUnit.SupportFunctionType ON ValidCombinations.CostCentre = SupportFunctionType.Centre WHERE BusinessUnitKey = @BusinessUnitKey ORDER BY SupportFunctionTypeName";
      SqlDataSource_FSM_BusinessUnitComponent_EditSupportFunctionTypeKey.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_EditSupportFunctionTypeKey.SelectParameters.Add("BusinessUnitKey", TypeCode.String, "");

      SqlDataSource_FSM_BusinessUnitComponent_ItemMappingBusinessUnitComponent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_ItemMappingBusinessUnitComponent.SelectCommand = "SELECT SourceSystem.SourceSystemName , BusinessUnitComponent.SourceSystemValue FROM Mapping.BusinessUnitComponent LEFT JOIN Mapping.SourceSystem ON BusinessUnitComponent.SourceSystemKey = SourceSystem.SourceSystemKey WHERE BusinessUnitComponent.BusinessUnitComponentKey = @BusinessUnitComponentKey ORDER BY SourceSystem.SourceSystemName";
      SqlDataSource_FSM_BusinessUnitComponent_ItemMappingBusinessUnitComponent.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_ItemMappingBusinessUnitComponent.SelectParameters.Add("BusinessUnitComponentKey", TypeCode.String, Request.QueryString["BusinessUnitComponentKey"]);

      SqlDataSource_FSM_BusinessUnitComponent_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertCommand = @" INSERT INTO BusinessUnit.BusinessUnitComponent ( BusinessUnitComponentName , BusinessUnitKey , BusinessUnitComponentTypeKey , ParentBusinessUnitComponentKey , CreatedDate , CreatedBy , ModifiedDate , ModifiedBy , IsActive ) VALUES ( @BusinessUnitComponentName , @BusinessUnitKey , @BusinessUnitComponentTypeKey , @ParentBusinessUnitComponentKey , @CreatedDate , @CreatedBy , @ModifiedDate , @ModifiedBy , @IsActive );
                                                                      SELECT @BusinessUnitComponentKey = SCOPE_IDENTITY();
                                                                      INSERT INTO Hospital.Ward ( BusinessUnitComponentKey , WardTypeKey , NursingDisciplineKey ) SELECT @BusinessUnitComponentKey , @WardTypeKey , @NursingDisciplineKey_Ward WHERE @BusinessUnitComponentTypeKey = 1;
                                                                      INSERT INTO Hospital.TheatreComplex ( BusinessUnitComponentKey , TheatreComplexTypeKey , NursingDisciplineKey ) SELECT @BusinessUnitComponentKey , @TheatreComplexTypeKey , @NursingDisciplineKey_TheatreComplex WHERE @BusinessUnitComponentTypeKey = 7;
                                                                      INSERT INTO Hospital.OperatingTheatre ( BusinessUnitComponentKey , OperatingTheatreTypeKey ) SELECT @BusinessUnitComponentKey , @OperatingTheatreTypeKey WHERE @BusinessUnitComponentTypeKey = 10;
                                                                      INSERT INTO BusinessUnit.StockRoom ( BusinessUnitComponentKey , StockRoomTypeKey ) SELECT @BusinessUnitComponentKey , @StockRoomTypeKey WHERE @BusinessUnitComponentTypeKey = 4;
                                                                      INSERT INTO Hospital.RetailPharmacy ( BusinessUnitComponentKey , RetailPharmacyPracticeNumber ) SELECT @BusinessUnitComponentKey , @RetailPharmacyPracticeNumber WHERE @BusinessUnitComponentTypeKey = 6;
                                                                      INSERT INTO BusinessUnit.SupportFunction ( BusinessUnitComponentKey , SupportFunctionTypeKey ) SELECT @BusinessUnitComponentKey , @SupportFunctionTypeKey WHERE @BusinessUnitComponentTypeKey = 12;
                                                                      INSERT INTO FinanceMapping.Exceptions ( BusinessUnitComponentKey , Entity , CostCentre ) SELECT @BusinessUnitComponentKey , @Entity , @CostCentre WHERE @BusinessUnitComponentTypeKey IN (1,7,5,11,4,6,12) AND (@DefaultEntity != @Entity OR @DefaultCostCentre != @CostCentre)";
      SqlDataSource_FSM_BusinessUnitComponent_Form.SelectCommand = @" SELECT * , Ward.NursingDisciplineKey AS NursingDisciplineKey_Ward , TheatreComplex.NursingDisciplineKey AS NursingDisciplineKey_TheatreComplex FROM BusinessUnit.BusinessUnitComponent LEFT JOIN Hospital.Ward ON BusinessUnitComponent.BusinessUnitComponentKey = Ward.BusinessUnitComponentKey LEFT JOIN Hospital.TheatreComplex ON BusinessUnitComponent.BusinessUnitComponentKey = TheatreComplex.BusinessUnitComponentKey LEFT JOIN Hospital.OperatingTheatre ON BusinessUnitComponent.BusinessUnitComponentKey = OperatingTheatre.BusinessUnitComponentKey LEFT JOIN BusinessUnit.StockRoom ON BusinessUnitComponent.BusinessUnitComponentKey = StockRoom.BusinessUnitComponentKey LEFT JOIN Hospital.RetailPharmacy ON BusinessUnitComponent.BusinessUnitComponentKey = RetailPharmacy.BusinessUnitComponentKey LEFT JOIN FinanceMapping.Exceptions ON BusinessUnitComponent.BusinessUnitComponentKey = Exceptions.BusinessUnitComponentKey LEFT JOIN BusinessUnit.SupportFunction ON BusinessUnitComponent.BusinessUnitComponentKey = SupportFunction.BusinessUnitComponentKey WHERE BusinessUnitComponent.BusinessUnitComponentKey = @BusinessUnitComponentKey";
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateCommand = @" UPDATE BusinessUnit.BusinessUnitComponent SET BusinessUnitComponentName = @BusinessUnitComponentName , BusinessUnitKey = @BusinessUnitKey , BusinessUnitComponentTypeKey = @BusinessUnitComponentTypeKey , ParentBusinessUnitComponentKey = @ParentBusinessUnitComponentKey , ModifiedDate = @ModifiedDate , ModifiedBy = @ModifiedBy , IsActive = @IsActive WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey;

                                                                      MERGE Hospital.Ward AS [Target]
                                                                      USING (SELECT BusinessUnitComponentKey , BusinessUnitComponentTypeKey FROM BusinessUnit.BusinessUnitComponent WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey) AS [Source] (BusinessUnitComponentKey , BusinessUnitComponentTypeKey)
                                                                      ON ([Target].BusinessUnitComponentKey = [Source].BusinessUnitComponentKey)
                                                                      WHEN NOT MATCHED AND BusinessUnitComponentTypeKey = 1
	                                                                      THEN INSERT ( BusinessUnitComponentKey , WardTypeKey , NursingDisciplineKey ) VALUES ( @BusinessUnitComponentKey , @WardTypeKey , @NursingDisciplineKey_Ward )
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey = 1
	                                                                      THEN UPDATE SET [Target].BusinessUnitComponentKey = @BusinessUnitComponentKey , [Target].WardTypeKey = @WardTypeKey , [Target].NursingDisciplineKey = @NursingDisciplineKey_Ward
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey != 1
                                                                        THEN DELETE ;
  
                                                                      MERGE Hospital.TheatreComplex AS [Target]
                                                                      USING (SELECT BusinessUnitComponentKey , BusinessUnitComponentTypeKey FROM BusinessUnit.BusinessUnitComponent WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey) AS [Source] (BusinessUnitComponentKey , BusinessUnitComponentTypeKey)
                                                                      ON ([Target].BusinessUnitComponentKey = [Source].BusinessUnitComponentKey)
                                                                      WHEN NOT MATCHED AND BusinessUnitComponentTypeKey = 7
	                                                                      THEN INSERT ( BusinessUnitComponentKey , TheatreComplexTypeKey , NursingDisciplineKey ) VALUES ( @BusinessUnitComponentKey , @TheatreComplexTypeKey , @NursingDisciplineKey_TheatreComplex )
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey = 7
	                                                                      THEN UPDATE SET [Target].BusinessUnitComponentKey = @BusinessUnitComponentKey , [Target].TheatreComplexTypeKey = @TheatreComplexTypeKey , [Target].NursingDisciplineKey = @NursingDisciplineKey_TheatreComplex
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey != 1
                                                                        THEN DELETE ; 
   
                                                                      MERGE Hospital.OperatingTheatre AS [Target]
                                                                      USING (SELECT BusinessUnitComponentKey , BusinessUnitComponentTypeKey FROM BusinessUnit.BusinessUnitComponent WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey) AS [Source] (BusinessUnitComponentKey , BusinessUnitComponentTypeKey)
                                                                      ON ([Target].BusinessUnitComponentKey = [Source].BusinessUnitComponentKey)
                                                                      WHEN NOT MATCHED AND BusinessUnitComponentTypeKey = 10
                                                                        THEN INSERT ( BusinessUnitComponentKey , OperatingTheatreTypeKey ) VALUES ( @BusinessUnitComponentKey , @OperatingTheatreTypeKey )
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey = 10
	                                                                      THEN UPDATE SET [Target].BusinessUnitComponentKey = @BusinessUnitComponentKey , [Target].OperatingTheatreTypeKey = @OperatingTheatreTypeKey
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey != 10
                                                                        THEN DELETE ;

                                                                      MERGE BusinessUnit.StockRoom AS [Target]
                                                                      USING (SELECT BusinessUnitComponentKey , BusinessUnitComponentTypeKey FROM BusinessUnit.BusinessUnitComponent WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey) AS [Source] (BusinessUnitComponentKey , BusinessUnitComponentTypeKey)
                                                                      ON ([Target].BusinessUnitComponentKey = [Source].BusinessUnitComponentKey)
                                                                      WHEN NOT MATCHED AND BusinessUnitComponentTypeKey = 4
                                                                        THEN INSERT ( BusinessUnitComponentKey , StockRoomTypeKey ) VALUES ( @BusinessUnitComponentKey , @StockRoomTypeKey )
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey = 4
	                                                                      THEN UPDATE SET [Target].BusinessUnitComponentKey = @BusinessUnitComponentKey , [Target].StockRoomTypeKey = @StockRoomTypeKey
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey != 4
                                                                        THEN DELETE ;

                                                                      MERGE BusinessUnit.SupportFunction AS [Target]
                                                                      USING (SELECT BusinessUnitComponentKey , BusinessUnitComponentTypeKey FROM BusinessUnit.BusinessUnitComponent WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey) AS [Source] (BusinessUnitComponentKey , BusinessUnitComponentTypeKey)
                                                                      ON ([Target].BusinessUnitComponentKey = [Source].BusinessUnitComponentKey)
                                                                      WHEN NOT MATCHED AND BusinessUnitComponentTypeKey = 12
                                                                        THEN INSERT ( BusinessUnitComponentKey , SupportFunctionTypeKey ) VALUES ( @BusinessUnitComponentKey , @SupportFunctionTypeKey )
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey = 12
	                                                                      THEN UPDATE SET [Target].BusinessUnitComponentKey = @BusinessUnitComponentKey , [Target].SupportFunctionTypeKey = @SupportFunctionTypeKey
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey != 12
                                                                        THEN DELETE ;

                                                                      MERGE Hospital.RetailPharmacy AS [Target]
                                                                      USING (SELECT BusinessUnitComponentKey , BusinessUnitComponentTypeKey FROM BusinessUnit.BusinessUnitComponent WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey) AS [Source] (BusinessUnitComponentKey , BusinessUnitComponentTypeKey)
                                                                      ON ([Target].BusinessUnitComponentKey = [Source].BusinessUnitComponentKey)
                                                                      WHEN NOT MATCHED AND BusinessUnitComponentTypeKey = 6
                                                                        THEN INSERT ( BusinessUnitComponentKey , RetailPharmacyPracticeNumber ) VALUES ( @BusinessUnitComponentKey , @RetailPharmacyPracticeNumber )
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey = 6
	                                                                      THEN UPDATE SET [Target].BusinessUnitComponentKey = @BusinessUnitComponentKey , [Target].RetailPharmacyPracticeNumber = @RetailPharmacyPracticeNumber
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey != 6
                                                                        THEN DELETE ;

                                                                      MERGE FinanceMapping.Exceptions AS [Target]
                                                                      USING (SELECT BusinessUnitComponentKey , BusinessUnitComponentTypeKey FROM BusinessUnit.BusinessUnitComponent WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey) AS [Source] (BusinessUnitComponentKey , BusinessUnitComponentTypeKey)
                                                                      ON ([Target].BusinessUnitComponentKey = [Source].BusinessUnitComponentKey)
                                                                      WHEN NOT MATCHED AND BusinessUnitComponentTypeKey IN (1,7,5,11,4,6) AND (@DefaultEntity != @Entity OR @DefaultCostCentre != @CostCentre)
                                                                        THEN INSERT ( BusinessUnitComponentKey , Entity , CostCentre ) VALUES ( @BusinessUnitComponentKey , @Entity , @CostCentre )  
                                                                      WHEN MATCHED AND BusinessUnitComponentTypeKey IN (1,7,5,11,4,6) AND (@DefaultEntity != @Entity OR @DefaultCostCentre != @CostCentre)
                                                                        THEN UPDATE SET [Target].BusinessUnitComponentKey = @BusinessUnitComponentKey , [Target].Entity = @Entity , [Target].CostCentre = @CostCentre  
                                                                      WHEN MATCHED AND (BusinessUnitComponentTypeKey NOT IN (1,7,5,11,4,6)) OR (BusinessUnitComponentTypeKey IN (1,7,5,11,4,6) AND (@DefaultEntity = @Entity AND @DefaultCostCentre = @CostCentre))
                                                                        THEN DELETE ;";
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("BusinessUnitComponentKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["BusinessUnitComponentKey"].Direction = ParameterDirection.Output;
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("BusinessUnitComponentName", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("BusinessUnitKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("BusinessUnitComponentTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("ParentBusinessUnitComponentKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("CreatedBy", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("ModifiedBy", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("IsActive", TypeCode.Boolean, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("WardTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("NursingDisciplineKey_Ward", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("TheatreComplexTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("NursingDisciplineKey_TheatreComplex", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("OperatingTheatreTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("StockRoomTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("RetailPharmacyPracticeNumber", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("SupportFunctionTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("Entity", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("CostCentre", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("DefaultEntity", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters.Add("DefaultCostCentre", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_Form.SelectParameters.Add("BusinessUnitComponentKey", TypeCode.Int32, Request.QueryString["BusinessUnitComponentKey"]);
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("BusinessUnitComponentName", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("BusinessUnitKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("BusinessUnitComponentTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("ParentBusinessUnitComponentKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("ModifiedBy", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("IsActive", TypeCode.Boolean, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("BusinessUnitComponentKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("WardTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("NursingDisciplineKey_Ward", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("TheatreComplexTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("NursingDisciplineKey_TheatreComplex", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("OperatingTheatreTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("StockRoomTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("SupportFunctionTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("RetailPharmacyPracticeNumber", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("Entity", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("CostCentre", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("DefaultEntity", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnitComponent_Form.UpdateParameters.Add("DefaultCostCentre", TypeCode.String, "");
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("41").Replace(" Form", "")).ToString() + " : Business Unit Component", CultureInfo.CurrentCulture);
      Label_BusinessUnitComponentHeading.Text = Convert.ToString("Business Unit Component", CultureInfo.CurrentCulture);
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_BusinessUnitName"];
      string SearchField2 = Request.QueryString["Search_BusinessUnitComponent"];
      string SearchField3 = Request.QueryString["Search_BusinessUnitComponentType"];
      string SearchField4 = Request.QueryString["Search_BusinessUnitComponentDescription"];
      string SearchField5 = Request.QueryString["Search_Entity"];
      string SearchField6 = Request.QueryString["Search_CostCentre"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_BusinessUnit=" + Request.QueryString["Search_BusinessUnitName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_BusinessUnitComponent=" + Request.QueryString["Search_BusinessUnitComponent"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_BusinessUnitComponentType=" + Request.QueryString["Search_BusinessUnitComponentType"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_BusinessUnitComponentDescription=" + Request.QueryString["Search_BusinessUnitComponentDescription"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_Entity=" + Request.QueryString["Search_Entity"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "s_CostCentre=" + Request.QueryString["Search_CostCentre"] + "&";
      }

      string FinalURL = "Form_FSM_BusinessUnitComponent_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - Captured Business Unit Components", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    private class FromDataBase_SecurityRole
    {
      public DataRow[] SecurityAdmin { get; set; }
      public DataRow[] SecurityFormAdminUpdate { get; set; }
      public DataRow[] SecurityFormAdminView { get; set; }
    }

    private FromDataBase_SecurityRole GetSecurityRole()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_New = new FromDataBase_SecurityRole();

      string SQLStringSecurityRole = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('41'))";
      using (SqlCommand SqlCommand_SecurityRole = new SqlCommand(SQLStringSecurityRole))
      {
        SqlCommand_SecurityRole.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);

        DataTable DataTable_SecurityRole;
        using (DataTable_SecurityRole = new DataTable())
        {
          DataTable_SecurityRole.Locale = CultureInfo.CurrentCulture;
          DataTable_SecurityRole = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityRole).Copy();

          if (DataTable_SecurityRole.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_SecurityRole.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_SecurityRole.Select("SecurityRole_Id = '170'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_SecurityRole.Select("SecurityRole_Id = '171'");
          }
        }
      }

      return FromDataBase_SecurityRole_New;
    }


    //--START-- --TableBusinessUnitComponent--//
    protected void SetBusinessUnitComponentVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["BusinessUnitComponentKey"]))
      {
        FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
        DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
        DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
        DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;

        string Security = "1";
        if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
        {
          Security = "0";
          FormView_FSM_BusinessUnitComponent_Form.ChangeMode(FormViewMode.Insert);
        }

        if (Security == "1" && (SecurityFormAdminView.Length > 0))
        {
          Security = "0";
          FormView_FSM_BusinessUnitComponent_Form.ChangeMode(FormViewMode.ReadOnly);
        }

        if (Security == "1")
        {
          Security = "0";
          FormView_FSM_BusinessUnitComponent_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }
      else
      {
        FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
        DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
        DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
        DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;

        string Security = "1";
        if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
        {
          Security = "0";
          FormView_FSM_BusinessUnitComponent_Form.ChangeMode(FormViewMode.Edit);
        }

        if (Security == "1" && (SecurityFormAdminView.Length > 0))
        {
          Security = "0";
          FormView_FSM_BusinessUnitComponent_Form.ChangeMode(FormViewMode.ReadOnly);
        }

        if (Security == "1")
        {
          Security = "0";
          FormView_FSM_BusinessUnitComponent_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }
    }

    protected void TableBusinessUnitComponentVisible()
    {
      if (FormView_FSM_BusinessUnitComponent_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertBusinessUnitComponentName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertBusinessUnitComponentName")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertParentBusinessUnitComponentKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertWardTypeKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertNursingDisciplineKey_Ward")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertTheatreComplexTypeKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertNursingDisciplineKey_TheatreComplex")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertOperatingTheatreTypeKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertStockroomTypeKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertRetailPharmacyPracticeNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertRetailPharmacyPracticeNumber")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertSupportFunctionTypeKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertEntity")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertEntity")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertCostCentre")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertCostCentre")).Attributes.Add("OnInput", "Validation_Form();");
      }

      if (FormView_FSM_BusinessUnitComponent_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditBusinessUnitComponentName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditBusinessUnitComponentName")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditParentBusinessUnitComponentKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditWardTypeKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditNursingDisciplineKey_Ward")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditTheatreComplexTypeKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditNursingDisciplineKey_TheatreComplex")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditOperatingTheatreTypeKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditStockroomTypeKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditRetailPharmacyPracticeNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditRetailPharmacyPracticeNumber")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditSupportFunctionTypeKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditEntity")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditEntity")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditCostCentre")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditCostCentre")).Attributes.Add("OnInput", "Validation_Form();");
      }
    }


    //--START-- --Insert--//
    protected void FormView_FSM_BusinessUnitComponent_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ToolkitScriptManager_FSM_BusinessUnitComponent.SetFocus(UpdatePanel_FSM_BusinessUnitComponent);
          ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["IsActive"].DefaultValue = "true";

          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["BusinessUnitKey"].DefaultValue = ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitKey")).SelectedValue;
          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["BusinessUnitComponentTypeKey"].DefaultValue = ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue;
          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["ParentBusinessUnitComponentKey"].DefaultValue = ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertParentBusinessUnitComponentKey")).SelectedValue;

          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["WardTypeKey"].DefaultValue = ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertWardTypeKey")).SelectedValue;
          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["TheatreComplexTypeKey"].DefaultValue = ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertTheatreComplexTypeKey")).SelectedValue;
          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["SupportFunctionTypeKey"].DefaultValue = ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertSupportFunctionTypeKey")).SelectedValue;

          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["Entity"].DefaultValue = ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertEntity")).Text;
          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["CostCentre"].DefaultValue = ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertCostCentre")).Text;
          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["DefaultEntity"].DefaultValue = ((HiddenField)FormView_FSM_BusinessUnitComponent_Form.FindControl("HiddenField_InsertEntity")).Value;
          SqlDataSource_FSM_BusinessUnitComponent_Form.InsertParameters["DefaultCostCentre"].DefaultValue = ((HiddenField)FormView_FSM_BusinessUnitComponent_Form.FindControl("HiddenField_InsertCostCentre")).Value;
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertBusinessUnitComponentName")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertParentBusinessUnitComponentKey")).Items.Count > 1)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertParentBusinessUnitComponentKey")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        InvalidForm = InsertValidation_BusinessUnitComponentTypeKey(InvalidForm);
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = InsertFieldValidation(InvalidFormMessage);
      }

      return InvalidFormMessage;
    }

    protected string InsertValidation_BusinessUnitComponentTypeKey(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "1")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertWardTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertNursingDisciplineKey_Ward")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "7")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertTheatreComplexTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertNursingDisciplineKey_TheatreComplex")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "10")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertOperatingTheatreTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "4")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertStockroomTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "6")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertRetailPharmacyPracticeNumber")).Text))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "12")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertSupportFunctionTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }

      if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "1" || ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "7" || ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "5" || ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "11" || ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "4" || ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "6" || ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue == "12")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertEntity")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertCostCentre")).Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertFieldValidation(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      //String BusinessUnitName = "";
      //String BusinessUnitComponentName = "";
      //String SQLStringName = "SELECT BusinessUnit.BusinessUnitName , BusinessUnitComponent.BusinessUnitComponentName FROM BusinessUnit.BusinessUnit LEFT JOIN BusinessUnit.BusinessUnitComponent ON BusinessUnit.BusinessUnitKey = BusinessUnitComponent.BusinessUnitKey WHERE BusinessUnitComponent.BusinessUnitKey = @BusinessUnitKey AND BusinessUnitComponent.BusinessUnitComponentName = @BusinessUnitComponentName";
      //using (SqlCommand SqlCommand_Name = new SqlCommand(SQLStringName))
      //{
      //  SqlCommand_Name.Parameters.AddWithValue("@BusinessUnitKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitKey")).Text.ToString());
      //  SqlCommand_Name.Parameters.AddWithValue("@BusinessUnitComponentName", ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertBusinessUnitComponentName")).Text.ToString());
      //  DataTable DataTable_Name;
      //  using (DataTable_Name = new DataTable())
      //  {
      //    DataTable_Name.Locale = CultureInfo.CurrentCulture;
      //    DataTable_Name = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_Name, "PatientDetailFacilityStructure").Copy();
      //    if (DataTable_Name.Rows.Count > 0)
      //    {
      //      foreach (DataRow DataRow_Row in DataTable_Name.Rows)
      //      {
      //        BusinessUnitName = DataRow_Row["BusinessUnitName"].ToString();
      //        BusinessUnitComponentName = DataRow_Row["BusinessUnitComponentName"].ToString();
      //      }
      //    }
      //  }
      //}

      //if (!String.IsNullOrEmpty(BusinessUnitComponentName))
      //{
      //  InvalidFormMessage = InvalidFormMessage + "A Business Unit Component with the Name '" + BusinessUnitComponentName + "' for Business Unit '" + BusinessUnitName + "' already exists<br />";
      //}

      //BusinessUnitName = "";
      //BusinessUnitComponentName = "";


      string ValidEntityCostCentre = "";
      string SQLStringEntityCostCentre = "SELECT BusinessUnit.ValidateEntityCostCentreFn ( @EntityCode , @CostCentre ) AS ValidEntityCostCentre";
      using (SqlCommand SqlCommand_EntityCostCentre = new SqlCommand(SQLStringEntityCostCentre))
      {
        SqlCommand_EntityCostCentre.Parameters.AddWithValue("@EntityCode", ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertEntity")).Text.ToString());
        SqlCommand_EntityCostCentre.Parameters.AddWithValue("@CostCentre", ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertCostCentre")).Text.ToString());
        DataTable DataTable_EntityCostCentre;
        using (DataTable_EntityCostCentre = new DataTable())
        {
          DataTable_EntityCostCentre.Locale = CultureInfo.CurrentCulture;
          DataTable_EntityCostCentre = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_EntityCostCentre, "PatientDetailFacilityStructure").Copy();
          if (DataTable_EntityCostCentre.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EntityCostCentre.Rows)
            {
              ValidEntityCostCentre = DataRow_Row["ValidEntityCostCentre"].ToString();
            }
          }
        }
      }

      if (ValidEntityCostCentre == "False")
      {
        InvalidFormMessage = InvalidFormMessage + "Entity Code or Cost Centre Code is not valid<br />";
      }

      ValidEntityCostCentre = "";


      return InvalidFormMessage;
    }

    protected void SqlDataSource_FSM_BusinessUnitComponent_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        string BusinessUnitComponentKey = e.Command.Parameters["@BusinessUnitComponentKey"].Value.ToString();

        if (!string.IsNullOrEmpty(BusinessUnitComponentKey))
        {
          if (((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_InsertMappingBusinessUnitComponent")).Rows.Count > 0)
          {
            foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_InsertMappingBusinessUnitComponent")).Rows)
            {
              HiddenField HiddenField_InsertSourceSystemKey = (HiddenField)GridViewRow_Row.FindControl("HiddenField_InsertSourceSystemKey");
              TextBox TextBox_InsertSourceSystemValue = (TextBox)GridViewRow_Row.FindControl("TextBox_InsertSourceSystemValue");

              if (!string.IsNullOrEmpty(TextBox_InsertSourceSystemValue.Text))
              {
                string SQLStringInsertSourceSystemMapping = "INSERT INTO Mapping.BusinessUnitComponent ( BusinessUnitComponentKey , SourceSystemKey , SourceSystemValue ) VALUES ( @BusinessUnitComponentKey , @SourceSystemKey , @SourceSystemValue )";
                using (SqlCommand SqlCommand_InsertSourceSystemMapping = new SqlCommand(SQLStringInsertSourceSystemMapping))
                {
                  SqlCommand_InsertSourceSystemMapping.Parameters.AddWithValue("@BusinessUnitComponentKey", BusinessUnitComponentKey);
                  SqlCommand_InsertSourceSystemMapping.Parameters.AddWithValue("@SourceSystemKey", HiddenField_InsertSourceSystemKey.Value);
                  SqlCommand_InsertSourceSystemMapping.Parameters.AddWithValue("@SourceSystemValue", TextBox_InsertSourceSystemValue.Text);

                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_Other(SqlCommand_InsertSourceSystemMapping, "PatientDetailFacilityStructure");
                }
              }
            }
          }
        }

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Captured", "InfoQuest_Captured.aspx?CapturedPage=Form_FSM_BusinessUnitComponent&CapturedNumber=" + BusinessUnitComponentKey + ""), false);
      }
    }
    //---END--- --Insert--//


    //--START-- --Edit--//
    protected void FormView_FSM_BusinessUnitComponent_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDModifiedDate"] = e.OldValues["ModifiedDate"];
        object OLDModifiedDate = Session["OLDModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareFSM_BusinessUnitComponent = (DataView)SqlDataSource_FSM_BusinessUnitComponent_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareFSM_BusinessUnitComponent = DataView_CompareFSM_BusinessUnitComponent[0];
        Session["DBModifiedDate"] = Convert.ToString(DataRowView_CompareFSM_BusinessUnitComponent["ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBModifiedBy"] = Convert.ToString(DataRowView_CompareFSM_BusinessUnitComponent["ModifiedBy"], CultureInfo.CurrentCulture);
        object DBModifiedDate = Session["DBModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_FSM_BusinessUnitComponent.SetFocus(UpdatePanel_FSM_BusinessUnitComponent);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ToolkitScriptManager_FSM_BusinessUnitComponent.SetFocus(UpdatePanel_FSM_BusinessUnitComponent);
            ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            e.NewValues["BusinessUnitKey"] = ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitKey")).SelectedValue;
            e.NewValues["BusinessUnitComponentTypeKey"] = ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue;
            e.NewValues["ParentBusinessUnitComponentKey"] = ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditParentBusinessUnitComponentKey")).SelectedValue;

            e.NewValues["WardTypeKey"] = ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditWardTypeKey")).SelectedValue;
            e.NewValues["TheatreComplexTypeKey"] = ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditTheatreComplexTypeKey")).SelectedValue;
            e.NewValues["SupportFunctionTypeKey"] = ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditSupportFunctionTypeKey")).SelectedValue;

            e.NewValues["Entity"] = ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditEntity")).Text;
            e.NewValues["CostCentre"] = ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditCostCentre")).Text;
            e.NewValues["DefaultEntity"] = ((HiddenField)FormView_FSM_BusinessUnitComponent_Form.FindControl("HiddenField_EditEntity")).Value;
            e.NewValues["DefaultCostCentre"] = ((HiddenField)FormView_FSM_BusinessUnitComponent_Form.FindControl("HiddenField_EditCostCentre")).Value;
          }
        }

        Session["OLDModifiedDate"] = "";
        Session["DBModifiedDate"] = "";
        Session["DBModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditBusinessUnitComponentName")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditParentBusinessUnitComponentKey")).Items.Count > 1)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditParentBusinessUnitComponentKey")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        InvalidForm = EditValidation_BusinessUnitComponentTypeKey(InvalidForm);
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = EditFieldValidation(InvalidFormMessage);
      }

      return InvalidFormMessage;
    }

    protected string EditValidation_BusinessUnitComponentTypeKey(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "1")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditWardTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditNursingDisciplineKey_Ward")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "7")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditTheatreComplexTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditNursingDisciplineKey_TheatreComplex")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "10")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditOperatingTheatreTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "4")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditStockroomTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "6")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditRetailPharmacyPracticeNumber")).Text))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "12")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditSupportFunctionTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }

      if (((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "1" || ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "7" || ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "5" || ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "11" || ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "4" || ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "6" || ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue == "12")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditEntity")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditCostCentre")).Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditFieldValidation(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      //String BusinessUnitName = "";
      //String BusinessUnitComponentKey = "";
      //String BusinessUnitComponentName = "";
      //String SQLStringName = "SELECT BusinessUnit.BusinessUnitName , BusinessUnitComponent.BusinessUnitComponentKey , BusinessUnitComponent.BusinessUnitComponentName FROM BusinessUnit.BusinessUnit LEFT JOIN BusinessUnit.BusinessUnitComponent ON BusinessUnit.BusinessUnitKey = BusinessUnitComponent.BusinessUnitKey WHERE BusinessUnitComponent.BusinessUnitKey = @BusinessUnitKey AND BusinessUnitComponent.BusinessUnitComponentName = @BusinessUnitComponentName";
      //using (SqlCommand SqlCommand_Name = new SqlCommand(SQLStringName))
      //{
      //  SqlCommand_Name.Parameters.AddWithValue("@BusinessUnitKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitKey")).Text.ToString());
      //  SqlCommand_Name.Parameters.AddWithValue("@BusinessUnitComponentName", ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditBusinessUnitComponentName")).Text.ToString());
      //  DataTable DataTable_Name;
      //  using (DataTable_Name = new DataTable())
      //  {
      //    DataTable_Name.Locale = CultureInfo.CurrentCulture;
      //    DataTable_Name = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_Name, "PatientDetailFacilityStructure").Copy();
      //    if (DataTable_Name.Rows.Count > 0)
      //    {
      //      foreach (DataRow DataRow_Row in DataTable_Name.Rows)
      //      {
      //        BusinessUnitName = DataRow_Row["BusinessUnitName"].ToString();
      //        BusinessUnitComponentKey = DataRow_Row["BusinessUnitComponentKey"].ToString();
      //        BusinessUnitComponentName = DataRow_Row["BusinessUnitComponentName"].ToString();
      //      }
      //    }
      //  }
      //}

      //if (!String.IsNullOrEmpty(BusinessUnitComponentName))
      //{
      //  if (BusinessUnitComponentKey != Request.QueryString["BusinessUnitComponentKey"])
      //  {
      //    InvalidFormMessage = InvalidFormMessage + "A Business Unit Component with the Name '" + BusinessUnitComponentName + "' for Business Unit '" + BusinessUnitName + "' already exists<br />";
      //  }
      //}

      //BusinessUnitName = "";
      //BusinessUnitComponentKey = "";
      //BusinessUnitComponentName = "";


      string ValidEntityCostCentre = "";
      string SQLStringEntityCostCentre = "SELECT BusinessUnit.ValidateEntityCostCentreFn ( @EntityCode , @CostCentre ) AS ValidEntityCostCentre";
      using (SqlCommand SqlCommand_EntityCostCentre = new SqlCommand(SQLStringEntityCostCentre))
      {
        SqlCommand_EntityCostCentre.Parameters.AddWithValue("@EntityCode", ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditEntity")).Text.ToString());
        SqlCommand_EntityCostCentre.Parameters.AddWithValue("@CostCentre", ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditCostCentre")).Text.ToString());
        DataTable DataTable_EntityCostCentre;
        using (DataTable_EntityCostCentre = new DataTable())
        {
          DataTable_EntityCostCentre.Locale = CultureInfo.CurrentCulture;
          DataTable_EntityCostCentre = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_EntityCostCentre, "PatientDetailFacilityStructure").Copy();
          if (DataTable_EntityCostCentre.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EntityCostCentre.Rows)
            {
              ValidEntityCostCentre = DataRow_Row["ValidEntityCostCentre"].ToString();
            }
          }
        }
      }

      if (ValidEntityCostCentre == "False")
      {
        InvalidFormMessage = InvalidFormMessage + "Entity Code or Cost Centre Code is not valid<br />";
      }

      ValidEntityCostCentre = "";


      return InvalidFormMessage;
    }

    protected void SqlDataSource_FSM_BusinessUnitComponent_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows >= 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            if (!string.IsNullOrEmpty(Request.QueryString["BusinessUnitComponentKey"]))
            {
              if (((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_EditMappingBusinessUnitComponent")).Rows.Count > 0)
              {
                string SQLStringDeleteSourceSystemMapping = "DELETE FROM Mapping.BusinessUnitComponent WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey";
                using (SqlCommand SqlCommand_DeleteSourceSystemMapping = new SqlCommand(SQLStringDeleteSourceSystemMapping))
                {
                  SqlCommand_DeleteSourceSystemMapping.Parameters.AddWithValue("@BusinessUnitComponentKey", Request.QueryString["BusinessUnitComponentKey"]);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_Other(SqlCommand_DeleteSourceSystemMapping, "PatientDetailFacilityStructure");
                }

                foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_EditMappingBusinessUnitComponent")).Rows)
                {
                  HiddenField HiddenField_EditSourceSystemKey = (HiddenField)GridViewRow_Row.FindControl("HiddenField_EditSourceSystemKey");
                  TextBox TextBox_EditSourceSystemValue = (TextBox)GridViewRow_Row.FindControl("TextBox_EditSourceSystemValue");

                  if (!string.IsNullOrEmpty(TextBox_EditSourceSystemValue.Text))
                  {
                    string SQLStringInsertSourceSystemMapping = "INSERT INTO Mapping.BusinessUnitComponent ( BusinessUnitComponentKey , SourceSystemKey , SourceSystemValue ) VALUES ( @BusinessUnitComponentKey , @SourceSystemKey , @SourceSystemValue )";
                    using (SqlCommand SqlCommand_InsertSourceSystemMapping = new SqlCommand(SQLStringInsertSourceSystemMapping))
                    {
                      SqlCommand_InsertSourceSystemMapping.Parameters.AddWithValue("@BusinessUnitComponentKey", Request.QueryString["BusinessUnitComponentKey"]);
                      SqlCommand_InsertSourceSystemMapping.Parameters.AddWithValue("@SourceSystemKey", HiddenField_EditSourceSystemKey.Value);
                      SqlCommand_InsertSourceSystemMapping.Parameters.AddWithValue("@SourceSystemValue", TextBox_EditSourceSystemValue.Text);

                      InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_Other(SqlCommand_InsertSourceSystemMapping, "PatientDetailFacilityStructure");
                    }
                  }
                }
              }
            }

            RedirectToList();
          }
        }
      }
    }
    //---END--- --Edit--//


    protected void FormView_FSM_BusinessUnitComponent_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["BusinessUnitComponentKey"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - New Business Unit Component", "Form_FSM_BusinessUnitComponent.aspx"), false);
          }
        }
      }
    }

    protected void FormView_FSM_BusinessUnitComponent_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_FSM_BusinessUnitComponent_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_FSM_BusinessUnitComponent_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected void EditDataBound()
    {
      if (!string.IsNullOrEmpty(Request.QueryString["BusinessUnitComponentKey"]))
      {
        DataView DataView_FSM_BusinessUnitComponent = (DataView)SqlDataSource_FSM_BusinessUnitComponent_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_FSM_BusinessUnitComponent = DataView_FSM_BusinessUnitComponent[0];
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditWardTypeKey")).SelectedValue = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["WardTypeKey"], CultureInfo.CurrentCulture);
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditTheatreComplexTypeKey")).SelectedValue = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["TheatreComplexTypeKey"], CultureInfo.CurrentCulture);
        ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditSupportFunctionTypeKey")).SelectedValue = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["SupportFunctionTypeKey"], CultureInfo.CurrentCulture);

        string ExceptionEntity = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["Entity"], CultureInfo.CurrentCulture);
        string ExceptionCostCentre = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["CostCentre"], CultureInfo.CurrentCulture);

        string DefaultEntity = "";
        string DefaultCostCentre = "";
        string SQLStringDefaultEntityCostCentre = "SELECT Entity , CostCentre FROM BusinessUnit.DefaultFinancialMapping ( @BusinessUnitComponentKey )";
        using (SqlCommand SqlCommand_DefaultEntityCostCentre = new SqlCommand(SQLStringDefaultEntityCostCentre))
        {
          SqlCommand_DefaultEntityCostCentre.Parameters.AddWithValue("@BusinessUnitComponentKey", Request.QueryString["BusinessUnitComponentKey"]);
          DataTable DataTable_DefaultEntityCostCentre;
          using (DataTable_DefaultEntityCostCentre = new DataTable())
          {
            DataTable_DefaultEntityCostCentre.Locale = CultureInfo.CurrentCulture;
            DataTable_DefaultEntityCostCentre = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_DefaultEntityCostCentre, "PatientDetailFacilityStructure").Copy();
            if (DataTable_DefaultEntityCostCentre.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_DefaultEntityCostCentre.Rows)
              {
                DefaultEntity = DataRow_Row["Entity"].ToString();
                DefaultCostCentre = DataRow_Row["CostCentre"].ToString();
              }
            }
          }
        }

        ((HiddenField)FormView_FSM_BusinessUnitComponent_Form.FindControl("HiddenField_EditEntity")).Value = DefaultEntity;
        ((HiddenField)FormView_FSM_BusinessUnitComponent_Form.FindControl("HiddenField_EditCostCentre")).Value = DefaultCostCentre;

        if (string.IsNullOrEmpty(ExceptionEntity) && string.IsNullOrEmpty(ExceptionCostCentre))
        {
          ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditEntity")).Text = DefaultEntity;
          ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditCostCentre")).Text = DefaultCostCentre;
        }
        else
        {
          ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditEntity")).Text = ExceptionEntity;
          ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditCostCentre")).Text = ExceptionCostCentre;
        }

        Edit_GetEntityDescription();
        Edit_GetCostCentreDescription();
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (!string.IsNullOrEmpty(Request.QueryString["BusinessUnitComponentKey"]))
      {
        string BusinessUnitName = "";
        string BusinessUnitComponentTypeName = "";
        string BusinessUnitComponentName = "";
        string WardTypeName = "";
        string NursingDisciplineName_Ward = "";
        string TheatreComplexTypeName = "";
        string NursingDisciplineName_TheatreComplex = "";
        string OperatingTheatreTypeName = "";
        string StockRoomTypeName = "";
        string SupportFunctionTypeName = "";
        string SQLStringFSMBusinessUnitComponent = "SELECT BusinessUnit.BusinessUnitName , BusinessUnitComponentType.BusinessUnitComponentTypeName , BusinessUnitComponentParent.BusinessUnitComponentName , WardType.WardTypeName , NursingDiscipline_Ward.NursingDisciplineName AS NursingDisciplineName_Ward , TheatreComplexType.TheatreComplexTypeName , NursingDiscipline_TheatreComplex.NursingDisciplineName AS NursingDisciplineName_TheatreComplex , OperatingTheatreType.OperatingTheatreTypeName , StockRoomType.StockRoomTypeName , SupportFunctionType.SupportFunctionTypeName FROM BusinessUnit.BusinessUnitComponent LEFT JOIN BusinessUnit.BusinessUnit ON BusinessUnitComponent.BusinessUnitKey = BusinessUnit.BusinessUnitKey LEFT JOIN BusinessUnit.BusinessUnitComponentType ON BusinessUnitComponent.BusinessUnitComponentTypeKey = BusinessUnitComponentType.BusinessUnitComponentTypeKey LEFT JOIN BusinessUnit.BusinessUnitComponent AS BusinessUnitComponentParent ON BusinessUnitComponent.ParentBusinessUnitComponentKey = BusinessUnitComponentParent.BusinessUnitComponentKey LEFT JOIN Hospital.Ward ON BusinessUnitComponent.BusinessUnitComponentKey = Ward.BusinessUnitComponentKey LEFT JOIN Hospital.WardType ON Ward.WardTypeKey = WardType.WardTypeKey LEFT JOIN Hospital.NursingDiscipline AS NursingDiscipline_Ward ON Ward.NursingDisciplineKey = NursingDiscipline_Ward.NursingDisciplineKey LEFT JOIN Hospital.TheatreComplex ON BusinessUnitComponent.BusinessUnitComponentKey = TheatreComplex.BusinessUnitComponentKey LEFT JOIN Hospital.TheatreComplexType ON TheatreComplex.TheatreComplexKey = TheatreComplexType.TheatreComplexTypeKey LEFT JOIN Hospital.NursingDiscipline AS NursingDiscipline_TheatreComplex ON TheatreComplex.NursingDisciplineKey = NursingDiscipline_TheatreComplex.NursingDisciplineKey LEFT JOIN Hospital.OperatingTheatre ON BusinessUnitComponent.BusinessUnitComponentKey = OperatingTheatre.BusinessUnitComponentKey LEFT JOIN Hospital.OperatingTheatreType ON OperatingTheatre.OperatingTheatreKey = OperatingTheatreType.OperatingTheatreTypeKey LEFT JOIN BusinessUnit.StockRoom ON BusinessUnitComponent.BusinessUnitComponentKey = StockRoom.BusinessUnitComponentKey LEFT JOIN BusinessUnit.StockRoomType ON StockRoom.StockRoomTypeKey = StockRoomType.StockRoomTypeKey LEFT JOIN BusinessUnit.SupportFunction  ON BusinessUnitComponent.BusinessUnitComponentKey = SupportFunction.BusinessUnitComponentKey LEFT JOIN BusinessUnit.SupportFunctionType ON SupportFunction.SupportFunctionTypeKey = SupportFunctionType.SupportFunctionTypeKey WHERE BusinessUnitComponent.BusinessUnitComponentKey = @BusinessUnitComponentKey";
        using (SqlCommand SqlCommand_FSMBusinessUnitComponent = new SqlCommand(SQLStringFSMBusinessUnitComponent))
        {
          SqlCommand_FSMBusinessUnitComponent.Parameters.AddWithValue("@BusinessUnitComponentKey", Request.QueryString["BusinessUnitComponentKey"]);
          DataTable DataTable_FSMBusinessUnitComponent;
          using (DataTable_FSMBusinessUnitComponent = new DataTable())
          {
            DataTable_FSMBusinessUnitComponent.Locale = CultureInfo.CurrentCulture;
            DataTable_FSMBusinessUnitComponent = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_FSMBusinessUnitComponent, "PatientDetailFacilityStructure").Copy();
            if (DataTable_FSMBusinessUnitComponent.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FSMBusinessUnitComponent.Rows)
              {
                BusinessUnitName = DataRow_Row["BusinessUnitName"].ToString();
                BusinessUnitComponentTypeName = DataRow_Row["BusinessUnitComponentTypeName"].ToString();
                BusinessUnitComponentName = DataRow_Row["BusinessUnitComponentName"].ToString();
                WardTypeName = DataRow_Row["WardTypeName"].ToString();
                NursingDisciplineName_Ward = DataRow_Row["NursingDisciplineName_Ward"].ToString();
                TheatreComplexTypeName = DataRow_Row["TheatreComplexTypeName"].ToString();
                NursingDisciplineName_TheatreComplex = DataRow_Row["NursingDisciplineName_TheatreComplex"].ToString();
                OperatingTheatreTypeName = DataRow_Row["OperatingTheatreTypeName"].ToString();
                StockRoomTypeName = DataRow_Row["StockRoomTypeName"].ToString();
                SupportFunctionTypeName = DataRow_Row["SupportFunctionTypeName"].ToString();
              }
            }
          }
        }

        ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemBusinessUnitKey")).Text = BusinessUnitName;
        ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemBusinessUnitComponentTypeKey")).Text = BusinessUnitComponentTypeName;
        ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemParentBusinessUnitComponentKey")).Text = BusinessUnitComponentName;
        ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemWardTypeKey")).Text = WardTypeName;
        ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemNursingDisciplineKey_Ward")).Text = NursingDisciplineName_Ward;
        ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemTheatreComplexTypeKey")).Text = TheatreComplexTypeName;
        ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemNursingDisciplineKey_TheatreComplex")).Text = NursingDisciplineName_TheatreComplex;
        ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemOperatingTheatreTypeKey")).Text = OperatingTheatreTypeName;
        ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemStockroomTypeKey")).Text = StockRoomTypeName;
        ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemSupportFunctionTypeKey")).Text = SupportFunctionTypeName;

        BusinessUnitName = "";
        BusinessUnitComponentTypeName = "";
        BusinessUnitComponentName = "";
        WardTypeName = "";
        NursingDisciplineName_Ward = "";
        TheatreComplexTypeName = "";
        NursingDisciplineName_TheatreComplex = "";
        OperatingTheatreTypeName = "";
        StockRoomTypeName = "";
        SupportFunctionTypeName = "";


        DataView DataView_FSM_BusinessUnitComponent = (DataView)SqlDataSource_FSM_BusinessUnitComponent_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_FSM_BusinessUnitComponent = DataView_FSM_BusinessUnitComponent[0];

        string ExceptionEntity = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["Entity"], CultureInfo.CurrentCulture);
        string ExceptionCostCentre = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["CostCentre"], CultureInfo.CurrentCulture);

        string DefaultEntity = "";
        string DefaultCostCentre = "";
        string SQLStringDefaultEntityCostCentre = "SELECT Entity , CostCentre FROM BusinessUnit.DefaultFinancialMapping ( @BusinessUnitComponentKey )";
        using (SqlCommand SqlCommand_DefaultEntityCostCentre = new SqlCommand(SQLStringDefaultEntityCostCentre))
        {
          SqlCommand_DefaultEntityCostCentre.Parameters.AddWithValue("@BusinessUnitComponentKey", Request.QueryString["BusinessUnitComponentKey"]);
          DataTable DataTable_DefaultEntityCostCentre;
          using (DataTable_DefaultEntityCostCentre = new DataTable())
          {
            DataTable_DefaultEntityCostCentre.Locale = CultureInfo.CurrentCulture;
            DataTable_DefaultEntityCostCentre = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_DefaultEntityCostCentre, "PatientDetailFacilityStructure").Copy();
            if (DataTable_DefaultEntityCostCentre.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_DefaultEntityCostCentre.Rows)
              {
                DefaultEntity = DataRow_Row["Entity"].ToString();
                DefaultCostCentre = DataRow_Row["CostCentre"].ToString();
              }
            }
          }
        }

        if (string.IsNullOrEmpty(ExceptionEntity) && string.IsNullOrEmpty(ExceptionCostCentre))
        {
          ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemEntity")).Text = DefaultEntity;
          ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemCostCentre")).Text = DefaultCostCentre;
        }
        else
        {
          ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemEntity")).Text = ExceptionEntity;
          ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemCostCentre")).Text = ExceptionCostCentre;
        }

        Item_GetEntityDescription();
        Item_GetCostCentreDescription();
      }
    }


    //--START-- --Insert Controls--//
    private void Insert_GetBusinessUnitComponentParent()
    {
      string SQLStringParent = "SELECT BusinessUnitComponentKey , BusinessUnitComponentName FROM BusinessUnit.FindAvailableParentsSP ( @BusinessUnitKey , @BusinessUnitComponentTypeKey )";
      using (SqlCommand SqlCommand_Parent = new SqlCommand(SQLStringParent))
      {
        SqlCommand_Parent.Parameters.AddWithValue("@BusinessUnitKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitKey")).SelectedValue);
        SqlCommand_Parent.Parameters.AddWithValue("@BusinessUnitComponentTypeKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue);
        DataTable DataTable_Parent;
        using (DataTable_Parent = new DataTable())
        {
          DataTable_Parent.Locale = CultureInfo.CurrentCulture;
          DataTable_Parent = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_Parent, "PatientDetailFacilityStructure").Copy();

          ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertParentBusinessUnitComponentKey")).Items.Clear();
          ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertParentBusinessUnitComponentKey")).Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));

          if (DataTable_Parent.Rows.Count > 0)
          {
            ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertParentBusinessUnitComponentKey")).DataSource = DataTable_Parent;
            ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertParentBusinessUnitComponentKey")).DataBind();
          }
          else
          {
            ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertParentBusinessUnitComponentKey")).DataSource = null;
            ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertParentBusinessUnitComponentKey")).DataBind();
          }
        }
      }
    }

    private void Insert_GetEntityCostCentre()
    {
      string Entity = "";
      string CostCentre = "";
      string SQLStringInsertEntityCostCentre = "SELECT Entity , CostCentre FROM BusinessUnit.InsertFinancialMapping ( @BusinessUnitKey , @BusinessUnitComponentTypeKey , @ParentBusinessUnitComponentKey , @BusinessUnitComponentSubTypeKey )";
      using (SqlCommand SqlCommand_InsertEntityCostCentre = new SqlCommand(SQLStringInsertEntityCostCentre))
      {
        SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@BusinessUnitKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitKey")).SelectedValue);
        SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@BusinessUnitComponentTypeKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertBusinessUnitComponentTypeKey")).SelectedValue);
        SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@ParentBusinessUnitComponentKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertParentBusinessUnitComponentKey")).SelectedValue);

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertWardTypeKey")).SelectedValue) && string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertTheatreComplexTypeKey")).SelectedValue) && string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertSupportFunctionTypeKey")).SelectedValue))
        {
          SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@BusinessUnitComponentSubTypeKey", "");
        }

        if (!string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertWardTypeKey")).SelectedValue))
        {
          SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@BusinessUnitComponentSubTypeKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertWardTypeKey")).SelectedValue);
        }

        if (!string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertTheatreComplexTypeKey")).SelectedValue))
        {
          SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@BusinessUnitComponentSubTypeKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertTheatreComplexTypeKey")).SelectedValue);
        }

        if (!string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertSupportFunctionTypeKey")).SelectedValue))
        {
          SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@BusinessUnitComponentSubTypeKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertSupportFunctionTypeKey")).SelectedValue);
        }

        DataTable DataTable_InsertEntityCostCentre;
        using (DataTable_InsertEntityCostCentre = new DataTable())
        {
          DataTable_InsertEntityCostCentre.Locale = CultureInfo.CurrentCulture;
          DataTable_InsertEntityCostCentre = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_InsertEntityCostCentre, "PatientDetailFacilityStructure").Copy();
          if (DataTable_InsertEntityCostCentre.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InsertEntityCostCentre.Rows)
            {
              Entity = DataRow_Row["Entity"].ToString();
              CostCentre = DataRow_Row["CostCentre"].ToString();
            }
          }
        }
      }

      ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertEntity")).Text = Entity;
      ((HiddenField)FormView_FSM_BusinessUnitComponent_Form.FindControl("HiddenField_InsertEntity")).Value = Entity;
      ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertCostCentre")).Text = CostCentre;
      ((HiddenField)FormView_FSM_BusinessUnitComponent_Form.FindControl("HiddenField_InsertCostCentre")).Value = CostCentre;

      Insert_GetEntityDescription();
      Insert_GetCostCentreDescription();
    }

    private void Insert_GetEntityDescription()
    {
      string Description = "No Description";
      string SQLStringEntityLookup = "SELECT Description FROM BusinessUnit.EntityLookup WHERE Entity = @Entity";
      using (SqlCommand SqlCommand_EntityLookup = new SqlCommand(SQLStringEntityLookup))
      {
        SqlCommand_EntityLookup.Parameters.AddWithValue("@Entity", ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertEntity")).Text);
        DataTable DataTable_EntityLookup;
        using (DataTable_EntityLookup = new DataTable())
        {
          DataTable_EntityLookup.Locale = CultureInfo.CurrentCulture;
          DataTable_EntityLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_EntityLookup, "PatientDetailFacilityStructure").Copy();
          if (DataTable_EntityLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EntityLookup.Rows)
            {
              Description = DataRow_Row["Description"].ToString();
            }
          }
        }
      }

      ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_InsertEntityLookup")).Text = Convert.ToString(Description, CultureInfo.CurrentCulture);

      Description = "";
    }

    private void Insert_GetCostCentreDescription()
    {
      string Description = "No Description";
      string SQLStringCostCentreLookup = "SELECT Description FROM BusinessUnit.CostCentreLookup WHERE CostCentre = @CostCentre";
      using (SqlCommand SqlCommand_CostCentreLookup = new SqlCommand(SQLStringCostCentreLookup))
      {
        SqlCommand_CostCentreLookup.Parameters.AddWithValue("@CostCentre", ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_InsertCostCentre")).Text);
        DataTable DataTable_CostCentreLookup;
        using (DataTable_CostCentreLookup = new DataTable())
        {
          DataTable_CostCentreLookup.Locale = CultureInfo.CurrentCulture;
          DataTable_CostCentreLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_CostCentreLookup, "PatientDetailFacilityStructure").Copy();
          if (DataTable_CostCentreLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CostCentreLookup.Rows)
            {
              Description = DataRow_Row["Description"].ToString();
            }
          }
        }
      }

      ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_InsertCostCentreLookup")).Text = Convert.ToString(Description, CultureInfo.CurrentCulture);

      Description = "";
    }

    protected void DropDownList_InsertBusinessUnitKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      Insert_GetBusinessUnitComponentParent();

      Insert_GetEntityCostCentre();

      string BusinessUnitKey = ((DropDownList)sender).SelectedValue;

      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertWardTypeKey")).Items.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_InsertWardTypeKey.SelectParameters["BusinessUnitKey"].DefaultValue = BusinessUnitKey;
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertWardTypeKey")).Items.Insert(0, new ListItem(Convert.ToString("Select Type", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertWardTypeKey")).DataBind();

      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertTheatreComplexTypeKey")).Items.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_InsertTheatreComplexTypeKey.SelectParameters["BusinessUnitKey"].DefaultValue = BusinessUnitKey;
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertTheatreComplexTypeKey")).Items.Insert(0, new ListItem(Convert.ToString("Select Type", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertTheatreComplexTypeKey")).DataBind();

      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertSupportFunctionTypeKey")).Items.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_InsertSupportFunctionTypeKey.SelectParameters["BusinessUnitKey"].DefaultValue = BusinessUnitKey;
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertSupportFunctionTypeKey")).Items.Insert(0, new ListItem(Convert.ToString("Select Type", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_InsertSupportFunctionTypeKey")).DataBind();
    }

    protected void DropDownList_InsertBusinessUnitComponentTypeKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      Insert_GetBusinessUnitComponentParent();

      Insert_GetEntityCostCentre();

      string SQLStringSourceSystem = "SELECT SourceSystemKey , SourceSystemName FROM Mapping.SourceSystem WHERE MappingTypeKey IN ( SELECT MappingTypeKey FROM Mapping.MappingType WHERE LookupTable = 'BusinessUnit.BusinessUnitComponentType' ) AND MappingTypeKey IN ( SELECT MappingTypeKey FROM Mapping.MappingType WHERE LookupTableKeyValue = @LookupTableKeyValue ) ORDER BY SourceSystemName";
      using (SqlCommand SqlCommand_SourceSystem = new SqlCommand(SQLStringSourceSystem))
      {
        SqlCommand_SourceSystem.Parameters.AddWithValue("@LookupTableKeyValue", ((DropDownList)sender).SelectedValue);
        DataTable DataTable_SourceSystem;
        using (DataTable_SourceSystem = new DataTable())
        {
          DataTable_SourceSystem.Locale = CultureInfo.CurrentCulture;
          DataTable_SourceSystem = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_SourceSystem, "PatientDetailFacilityStructure").Copy();
          if (DataTable_SourceSystem.Rows.Count > 0)
          {
            ((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_InsertMappingBusinessUnitComponent")).DataSource = DataTable_SourceSystem;
            ((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_InsertMappingBusinessUnitComponent")).DataBind();
          }
          else
          {
            ((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_InsertMappingBusinessUnitComponent")).DataSource = null;
            ((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_InsertMappingBusinessUnitComponent")).DataBind();
          }
        }
      }
    }

    protected void DropDownList_InsertParentBusinessUnitComponentKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      Insert_GetEntityCostCentre();
    }

    protected void DropDownList_InsertWardTypeKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      Insert_GetEntityCostCentre();
    }

    protected void DropDownList_InsertTheatreComplexTypeKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      Insert_GetEntityCostCentre();
    }

    protected void DropDownList_InsertSupportFunctionTypeKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      Insert_GetEntityCostCentre();
    }

    protected void TextBox_InsertEntity_TextChanged(object sender, EventArgs e)
    {
      Insert_GetEntityDescription();
    }

    protected void TextBox_InsertCostCentre_TextChanged(object sender, EventArgs e)
    {
      Insert_GetCostCentreDescription();
    }

    protected void GridView_InsertMappingBusinessUnitComponent_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_InsertMappingBusinessUnitComponent_DataBound(object sender, EventArgs e)
    {
      GridView GridView_InsertMappingBusinessUnitComponent = (GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_InsertMappingBusinessUnitComponent");

      ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_InsertTotalRecords")).Text = GridView_InsertMappingBusinessUnitComponent.Rows.Count.ToString(CultureInfo.CurrentCulture);
      ((HiddenField)FormView_FSM_BusinessUnitComponent_Form.FindControl("HiddenField_InsertTotalRecords")).Value = GridView_InsertMappingBusinessUnitComponent.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void GridView_InsertMappingBusinessUnitComponent_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_InsertGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_InsertCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_FSM_BusinessUnitComponent.aspx", false);
    }
    //---END--- --Insert Controls--//


    //--START-- --Edit Controls--//
    private void Edit_GetBusinessUnitComponentParent()
    {
      string SQLStringParent = "SELECT BusinessUnitComponentKey , BusinessUnitComponentName FROM BusinessUnit.FindAvailableParentsSP ( @BusinessUnitKey , @BusinessUnitComponentTypeKey )";
      using (SqlCommand SqlCommand_Parent = new SqlCommand(SQLStringParent))
      {
        SqlCommand_Parent.Parameters.AddWithValue("@BusinessUnitKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitKey")).SelectedValue);
        SqlCommand_Parent.Parameters.AddWithValue("@BusinessUnitComponentTypeKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue);
        DataTable DataTable_Parent;
        using (DataTable_Parent = new DataTable())
        {
          DataTable_Parent.Locale = CultureInfo.CurrentCulture;
          DataTable_Parent = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_Parent, "PatientDetailFacilityStructure").Copy();

          ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditParentBusinessUnitComponentKey")).Items.Clear();
          ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditParentBusinessUnitComponentKey")).Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));

          if (DataTable_Parent.Rows.Count > 0)
          {
            ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditParentBusinessUnitComponentKey")).DataSource = DataTable_Parent;
            ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditParentBusinessUnitComponentKey")).DataBind();
          }
          else
          {
            ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditParentBusinessUnitComponentKey")).DataSource = null;
            ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditParentBusinessUnitComponentKey")).DataBind();
          }
        }
      }
    }

    private void Edit_GetEntityCostCentre()
    {
      string Entity = "";
      string CostCentre = "";
      string SQLStringInsertEntityCostCentre = "SELECT Entity , CostCentre FROM BusinessUnit.InsertFinancialMapping ( @BusinessUnitKey , @BusinessUnitComponentTypeKey , @ParentBusinessUnitComponentKey , @BusinessUnitComponentSubTypeKey )";
      using (SqlCommand SqlCommand_InsertEntityCostCentre = new SqlCommand(SQLStringInsertEntityCostCentre))
      {
        SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@BusinessUnitKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitKey")).SelectedValue);
        SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@BusinessUnitComponentTypeKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue);
        SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@ParentBusinessUnitComponentKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditParentBusinessUnitComponentKey")).SelectedValue);

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditWardTypeKey")).SelectedValue) && string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditTheatreComplexTypeKey")).SelectedValue) && string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditSupportFunctionTypeKey")).SelectedValue))
        {
          SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@BusinessUnitComponentSubTypeKey", "");
        }

        if (!string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditWardTypeKey")).SelectedValue))
        {
          SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@BusinessUnitComponentSubTypeKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditWardTypeKey")).SelectedValue);
        }

        if (!string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditTheatreComplexTypeKey")).SelectedValue))
        {
          SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@BusinessUnitComponentSubTypeKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditTheatreComplexTypeKey")).SelectedValue);
        }

        if (!string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditSupportFunctionTypeKey")).SelectedValue))
        {
          SqlCommand_InsertEntityCostCentre.Parameters.AddWithValue("@BusinessUnitComponentSubTypeKey", ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditSupportFunctionTypeKey")).SelectedValue);
        }

        DataTable DataTable_InsertEntityCostCentre;
        using (DataTable_InsertEntityCostCentre = new DataTable())
        {
          DataTable_InsertEntityCostCentre.Locale = CultureInfo.CurrentCulture;
          DataTable_InsertEntityCostCentre = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_InsertEntityCostCentre, "PatientDetailFacilityStructure").Copy();
          if (DataTable_InsertEntityCostCentre.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InsertEntityCostCentre.Rows)
            {
              Entity = DataRow_Row["Entity"].ToString();
              CostCentre = DataRow_Row["CostCentre"].ToString();
            }
          }
        }
      }

      ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditEntity")).Text = Entity;
      ((HiddenField)FormView_FSM_BusinessUnitComponent_Form.FindControl("HiddenField_EditEntity")).Value = Entity;
      ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditCostCentre")).Text = CostCentre;
      ((HiddenField)FormView_FSM_BusinessUnitComponent_Form.FindControl("HiddenField_EditCostCentre")).Value = CostCentre;

      Edit_GetEntityDescription();
      Edit_GetCostCentreDescription();
    }

    private void Edit_GetEntityDescription()
    {
      string Description = "No Description";
      string SQLStringEntityLookup = "SELECT Description FROM BusinessUnit.EntityLookup WHERE Entity = @Entity";
      using (SqlCommand SqlCommand_EntityLookup = new SqlCommand(SQLStringEntityLookup))
      {
        SqlCommand_EntityLookup.Parameters.AddWithValue("@Entity", ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditEntity")).Text);
        DataTable DataTable_EntityLookup;
        using (DataTable_EntityLookup = new DataTable())
        {
          DataTable_EntityLookup.Locale = CultureInfo.CurrentCulture;
          DataTable_EntityLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_EntityLookup, "PatientDetailFacilityStructure").Copy();
          if (DataTable_EntityLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EntityLookup.Rows)
            {
              Description = DataRow_Row["Description"].ToString();
            }
          }
        }
      }

      ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_EditEntityLookup")).Text = Convert.ToString(Description, CultureInfo.CurrentCulture);

      Description = "";
    }

    private void Edit_GetCostCentreDescription()
    {
      string Description = "No Description";
      string SQLStringCostCentreLookup = "SELECT Description FROM BusinessUnit.CostCentreLookup WHERE CostCentre = @CostCentre";
      using (SqlCommand SqlCommand_CostCentreLookup = new SqlCommand(SQLStringCostCentreLookup))
      {
        SqlCommand_CostCentreLookup.Parameters.AddWithValue("@CostCentre", ((TextBox)FormView_FSM_BusinessUnitComponent_Form.FindControl("TextBox_EditCostCentre")).Text);
        DataTable DataTable_CostCentreLookup;
        using (DataTable_CostCentreLookup = new DataTable())
        {
          DataTable_CostCentreLookup.Locale = CultureInfo.CurrentCulture;
          DataTable_CostCentreLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_CostCentreLookup, "PatientDetailFacilityStructure").Copy();
          if (DataTable_CostCentreLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CostCentreLookup.Rows)
            {
              Description = DataRow_Row["Description"].ToString();
            }
          }
        }
      }

      ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_EditCostCentreLookup")).Text = Convert.ToString(Description, CultureInfo.CurrentCulture);

      Description = "";
    }

    protected void DropDownList_EditBusinessUnitKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      Edit_GetBusinessUnitComponentParent();

      Edit_GetEntityCostCentre();

      string BusinessUnitKey = ((DropDownList)sender).SelectedValue;

      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditWardTypeKey")).Items.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_EditWardTypeKey.SelectParameters["BusinessUnitKey"].DefaultValue = BusinessUnitKey;
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditWardTypeKey")).Items.Insert(0, new ListItem(Convert.ToString("Select Type", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditWardTypeKey")).DataBind();

      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditTheatreComplexTypeKey")).Items.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_EditTheatreComplexTypeKey.SelectParameters["BusinessUnitKey"].DefaultValue = BusinessUnitKey;
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditTheatreComplexTypeKey")).Items.Insert(0, new ListItem(Convert.ToString("Select Type", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditTheatreComplexTypeKey")).DataBind();

      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditSupportFunctionTypeKey")).Items.Clear();
      SqlDataSource_FSM_BusinessUnitComponent_EditSupportFunctionTypeKey.SelectParameters["BusinessUnitKey"].DefaultValue = BusinessUnitKey;
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditSupportFunctionTypeKey")).Items.Insert(0, new ListItem(Convert.ToString("Select Type", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditSupportFunctionTypeKey")).DataBind();
    }

    protected void DropDownList_EditBusinessUnitKey_DataBound(object sender, EventArgs e)
    {
      DataView DataView_FSM_BusinessUnitComponent = (DataView)SqlDataSource_FSM_BusinessUnitComponent_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_FSM_BusinessUnitComponent = DataView_FSM_BusinessUnitComponent[0];

      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitKey")).SelectedValue = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["BusinessUnitKey"], CultureInfo.CurrentCulture);

      string BusinessUnitKey = ((DropDownList)sender).SelectedValue;

      SqlDataSource_FSM_BusinessUnitComponent_EditWardTypeKey.SelectParameters["BusinessUnitKey"].DefaultValue = BusinessUnitKey;
      SqlDataSource_FSM_BusinessUnitComponent_EditTheatreComplexTypeKey.SelectParameters["BusinessUnitKey"].DefaultValue = BusinessUnitKey;
      SqlDataSource_FSM_BusinessUnitComponent_EditSupportFunctionTypeKey.SelectParameters["BusinessUnitKey"].DefaultValue = BusinessUnitKey;

      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditWardTypeKey")).SelectedValue = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["WardTypeKey"], CultureInfo.CurrentCulture);
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditTheatreComplexTypeKey")).SelectedValue = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["TheatreComplexTypeKey"], CultureInfo.CurrentCulture);
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditSupportFunctionTypeKey")).SelectedValue = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["SupportFunctionTypeKey"], CultureInfo.CurrentCulture);
    }

    protected void DropDownList_EditBusinessUnitComponentTypeKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      Edit_GetBusinessUnitComponentParent();

      Edit_GetEntityCostCentre();

      string SQLStringSourceSystem = "SELECT SourceSystemKey , SourceSystemName FROM Mapping.SourceSystem WHERE MappingTypeKey IN ( SELECT MappingTypeKey FROM Mapping.MappingType WHERE LookupTable = 'BusinessUnit.BusinessUnitComponentType' ) AND MappingTypeKey IN ( SELECT MappingTypeKey FROM Mapping.MappingType WHERE LookupTableKeyValue = @LookupTableKeyValue ) ORDER BY SourceSystemName";
      using (SqlCommand SqlCommand_SourceSystem = new SqlCommand(SQLStringSourceSystem))
      {
        SqlCommand_SourceSystem.Parameters.AddWithValue("@LookupTableKeyValue", ((DropDownList)sender).SelectedValue);
        DataTable DataTable_SourceSystem;
        using (DataTable_SourceSystem = new DataTable())
        {
          DataTable_SourceSystem.Locale = CultureInfo.CurrentCulture;
          DataTable_SourceSystem = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_SourceSystem, "PatientDetailFacilityStructure").Copy();
          if (DataTable_SourceSystem.Rows.Count > 0)
          {
            ((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_EditMappingBusinessUnitComponent")).DataSource = DataTable_SourceSystem;
            ((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_EditMappingBusinessUnitComponent")).DataBind();
          }
          else
          {
            ((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_EditMappingBusinessUnitComponent")).DataSource = null;
            ((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_EditMappingBusinessUnitComponent")).DataBind();
          }
        }
      }
    }

    protected void DropDownList_EditBusinessUnitComponentTypeKey_DataBound(object sender, EventArgs e)
    {
      DataView DataView_FSM_BusinessUnitComponent = (DataView)SqlDataSource_FSM_BusinessUnitComponent_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_FSM_BusinessUnitComponent = DataView_FSM_BusinessUnitComponent[0];
      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditBusinessUnitComponentTypeKey")).SelectedValue = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["BusinessUnitComponentTypeKey"], CultureInfo.CurrentCulture);

      Edit_GetBusinessUnitComponentParent();

      ((DropDownList)FormView_FSM_BusinessUnitComponent_Form.FindControl("DropDownList_EditParentBusinessUnitComponentKey")).SelectedValue = Convert.ToString(DataRowView_FSM_BusinessUnitComponent["ParentBusinessUnitComponentKey"], CultureInfo.CurrentCulture);

      string SQLStringMappingSourceSystem = @"SELECT BusinessUnitComponent.SourceSystemKey , SourceSystem.SourceSystemName FROM Mapping.BusinessUnitComponent LEFT JOIN Mapping.SourceSystem ON BusinessUnitComponent.SourceSystemKey = SourceSystem.SourceSystemKey WHERE BusinessUnitComponent.BusinessUnitComponentKey = @BusinessUnitComponentKey";
      using (SqlCommand SqlCommand_MappingSourceSystem = new SqlCommand(SQLStringMappingSourceSystem))
      {
        SqlCommand_MappingSourceSystem.Parameters.AddWithValue("@BusinessUnitComponentKey", Request.QueryString["BusinessUnitComponentKey"]);
        DataTable DataTable_MappingSourceSystem;
        using (DataTable_MappingSourceSystem = new DataTable())
        {
          DataTable_MappingSourceSystem.Locale = CultureInfo.CurrentCulture;
          DataTable_MappingSourceSystem.Merge(InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_MappingSourceSystem, "PatientDetailFacilityStructure"));

          string SQLStringSourceSystem = "SELECT SourceSystemKey , SourceSystemName FROM Mapping.SourceSystem WHERE MappingTypeKey IN ( SELECT MappingTypeKey FROM Mapping.MappingType WHERE LookupTable = 'BusinessUnit.BusinessUnitComponentType' ) AND MappingTypeKey IN ( SELECT MappingTypeKey FROM Mapping.MappingType WHERE LookupTableKeyValue = @LookupTableKeyValue ) ORDER BY SourceSystemName";
          using (SqlCommand SqlCommand_SourceSystem = new SqlCommand(SQLStringSourceSystem))
          {
            SqlCommand_SourceSystem.Parameters.AddWithValue("@LookupTableKeyValue", ((DropDownList)sender).SelectedValue);
            DataTable DataTable_SourceSystem;
            using (DataTable_SourceSystem = new DataTable())
            {
              DataTable_SourceSystem.Locale = CultureInfo.CurrentCulture;
              DataTable_SourceSystem = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_SourceSystem, "PatientDetailFacilityStructure").Copy();

              if (DataTable_SourceSystem.Rows.Count > 0)
              {
                DataTable_MappingSourceSystem.Merge(DataTable_SourceSystem);
              }
            }
          }

          DataTable_MappingSourceSystem.DefaultView.Sort = "SourceSystemName ASC";

          ((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_EditMappingBusinessUnitComponent")).DataSource = DataTable_MappingSourceSystem.DefaultView.ToTable(true, "SourceSystemKey", "SourceSystemName");
          ((GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_EditMappingBusinessUnitComponent")).DataBind();
        }
      }
    }

    protected void DropDownList_EditParentBusinessUnitComponentKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      Edit_GetEntityCostCentre();
    }

    protected void DropDownList_EditWardTypeKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      Edit_GetEntityCostCentre();
    }

    protected void DropDownList_EditTheatreComplexTypeKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      Edit_GetEntityCostCentre();
    }

    protected void DropDownList_EditSupportFunctionTypeKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      Edit_GetEntityCostCentre();
    }

    protected void TextBox_EditEntity_TextChanged(object sender, EventArgs e)
    {
      Edit_GetEntityDescription();
    }

    protected void TextBox_EditCostCentre_TextChanged(object sender, EventArgs e)
    {
      Edit_GetCostCentreDescription();
    }

    protected void GridView_EditMappingBusinessUnitComponent_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_EditMappingBusinessUnitComponent_DataBound(object sender, EventArgs e)
    {
      GridView GridView_EditMappingBusinessUnitComponent = (GridView)FormView_FSM_BusinessUnitComponent_Form.FindControl("GridView_EditMappingBusinessUnitComponent");

      ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_EditTotalRecords")).Text = GridView_EditMappingBusinessUnitComponent.Rows.Count.ToString(CultureInfo.CurrentCulture);
      ((HiddenField)FormView_FSM_BusinessUnitComponent_Form.FindControl("HiddenField_EditTotalRecords")).Value = GridView_EditMappingBusinessUnitComponent.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void GridView_EditMappingBusinessUnitComponent_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_EditMappingBusinessUnitComponent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          HiddenField HiddenField_EditSourceSystemKey = (HiddenField)e.Row.FindControl("HiddenField_EditSourceSystemKey");

          string SourceSystemKey = "";
          string SourceSystemValue = "";
          string SQLStringSourceSystem = "SELECT SourceSystemKey , SourceSystemValue FROM Mapping.BusinessUnitComponent WHERE BusinessUnitComponentKey = @BusinessUnitComponentKey AND SourceSystemKey = @SourceSystemKey";
          using (SqlCommand SqlCommand_SourceSystem = new SqlCommand(SQLStringSourceSystem))
          {
            SqlCommand_SourceSystem.Parameters.AddWithValue("@BusinessUnitComponentKey", Request.QueryString["BusinessUnitComponentKey"]);
            SqlCommand_SourceSystem.Parameters.AddWithValue("@SourceSystemKey", HiddenField_EditSourceSystemKey.Value);
            DataTable DataTable_SourceSystem;
            using (DataTable_SourceSystem = new DataTable())
            {
              DataTable_SourceSystem.Locale = CultureInfo.CurrentCulture;
              DataTable_SourceSystem = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_SourceSystem, "PatientDetailFacilityStructure").Copy();
              if (DataTable_SourceSystem.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_SourceSystem.Rows)
                {
                  SourceSystemKey = DataRow_Row["SourceSystemKey"].ToString();
                  SourceSystemValue = DataRow_Row["SourceSystemValue"].ToString();
                }
              }
            }
          }

          TextBox TextBox_EditSourceSystemValue = (TextBox)e.Row.FindControl("TextBox_EditSourceSystemValue");

          if (!string.IsNullOrEmpty(SourceSystemKey))
          {
            TextBox_EditSourceSystemValue.Text = SourceSystemValue;
          }
          else
          {
            TextBox_EditSourceSystemValue.Text = "";
          }

          SourceSystemKey = "";
          SourceSystemValue = "";
        }
      }
    }

    protected void Button_EditGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_EditCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_FSM_BusinessUnitComponent.aspx", false);
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Edit Controls--//


    //--START-- --Item Controls--//
    private void Item_GetEntityDescription()
    {
      string Description = "No Description";
      string SQLStringEntityLookup = "SELECT Description FROM BusinessUnit.EntityLookup WHERE Entity = @Entity";
      using (SqlCommand SqlCommand_EntityLookup = new SqlCommand(SQLStringEntityLookup))
      {
        SqlCommand_EntityLookup.Parameters.AddWithValue("@Entity", ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemEntity")).Text);
        DataTable DataTable_EntityLookup;
        using (DataTable_EntityLookup = new DataTable())
        {
          DataTable_EntityLookup.Locale = CultureInfo.CurrentCulture;
          DataTable_EntityLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_EntityLookup, "PatientDetailFacilityStructure").Copy();
          if (DataTable_EntityLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EntityLookup.Rows)
            {
              Description = DataRow_Row["Description"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemEntity")).Text))
      {
        Description = " - " + Description;
      }

      ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemEntityLookup")).Text = Convert.ToString(Description, CultureInfo.CurrentCulture);

      Description = "";
    }

    private void Item_GetCostCentreDescription()
    {
      string Description = "No Description";
      string SQLStringCostCentreLookup = "SELECT Description FROM BusinessUnit.CostCentreLookup WHERE CostCentre = @CostCentre";
      using (SqlCommand SqlCommand_CostCentreLookup = new SqlCommand(SQLStringCostCentreLookup))
      {
        SqlCommand_CostCentreLookup.Parameters.AddWithValue("@CostCentre", ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemCostCentre")).Text);
        DataTable DataTable_CostCentreLookup;
        using (DataTable_CostCentreLookup = new DataTable())
        {
          DataTable_CostCentreLookup.Locale = CultureInfo.CurrentCulture;
          DataTable_CostCentreLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_CostCentreLookup, "PatientDetailFacilityStructure").Copy();
          if (DataTable_CostCentreLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CostCentreLookup.Rows)
            {
              Description = DataRow_Row["Description"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemCostCentre")).Text))
      {
        Description = " - " + Description;
      }

      ((Label)FormView_FSM_BusinessUnitComponent_Form.FindControl("Label_ItemCostCentreLookup")).Text = Convert.ToString(Description, CultureInfo.CurrentCulture);

      Description = "";
    }

    protected void GridView_ItemMappingBusinessUnitComponent_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_ItemMappingBusinessUnitComponent_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_ItemGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_ItemCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_FSM_BusinessUnitComponent.aspx", false);
    }
    //---END--- --Item Controls--//
    //---END--- --TableBusinessUnitComponent--//
  }
}