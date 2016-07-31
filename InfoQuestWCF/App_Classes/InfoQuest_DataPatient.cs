using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;

namespace InfoQuestWCF
{
  public static class InfoQuest_DataPatient
  {
#pragma warning disable CS0618
    //Forms: Form_InfectionPrevention
    public static DataTable DataPatient_IMeds_Information(string facilityId, string patientVisitNumber)
    {
      //--START-- --GET PATIENT INFO FROM IMEDS-- --//
      //FUNCTION
      //begin :x:=ecomm_thea.get_thea(:facility,:visitnumber); end;
      //COLUMNS
      //Visit_Number
      //Name_and_Surname
      //Age
      //Contact_Number
      //Email
      //Postal_Address
      //Postal_Area
      //Postal_City
      //Postal_Zip
      //Residential_Address
      //Residential_Area
      //Residential_City
      //Residential_Zip
      //Date_of_Admission
      //Date_of_Discharge
      //Theatre_Date
      //Theatre_Time_In
      //Theatre_Procedure
      //Ward

      using (DataTable DataTable_Patient_Information = new DataTable())
      {
        DataTable_Patient_Information.Locale = CultureInfo.CurrentCulture;
        DataTable_Patient_Information.Reset();

        FromDataBase_ImedsConnectionString FromDataBase_ImedsConnectionString_Current = GetImedsConnectionString(facilityId);
        string ImedsConnectionString = FromDataBase_ImedsConnectionString_Current.ImedsConnectionString;
        string FacilityCode = FromDataBase_ImedsConnectionString_Current.FacilityCode;

        if (string.IsNullOrEmpty(ImedsConnectionString) || string.IsNullOrEmpty(FacilityCode))
        {
          DataTable_Patient_Information.Reset();
          DataTable_Patient_Information.Columns.Add("Error", typeof(string));
          DataTable_Patient_Information.Rows.Add("A connection could not be made to IMeds, Connection or Facility Code is missing, Please try again later");
        }
        else
        {
          using (OracleConnection OracleConnection_ImedsConnectionPI = new OracleConnection(ImedsConnectionString))
          {
            try
            {
              OracleConnection_ImedsConnectionPI.Open();

              string OracleStringImedsPI = "begin :x:=ecomm_thea.get_thea(:facility,:visitnumber); end;";
              OracleCommand OracleCommand_ImedsPI = OracleConnection_ImedsConnectionPI.CreateCommand();
              OracleCommand_ImedsPI.CommandText = OracleStringImedsPI;
              OracleCommand_ImedsPI.Parameters.Add(":facility", OracleType.NVarChar).Value = FacilityCode;
              OracleCommand_ImedsPI.Parameters.Add(":visitnumber", OracleType.Number).Value = patientVisitNumber;

              OracleParameter OracleParameter_ImedsPI = OracleCommand_ImedsPI.CreateParameter() as OracleParameter;
              OracleParameter_ImedsPI.OracleType = OracleType.Cursor;
              OracleParameter_ImedsPI.ParameterName = ":x";
              OracleParameter_ImedsPI.Direction = ParameterDirection.ReturnValue;
              OracleParameter_ImedsPI.Size = 10000;

              OracleCommand_ImedsPI.Parameters.Add(OracleParameter_ImedsPI);

              using (OracleDataAdapter OracleDataAdapter_ImedsPI = new OracleDataAdapter(OracleCommand_ImedsPI))
              {
                OracleDataAdapter_ImedsPI.Fill(DataTable_Patient_Information);
              }

              if (DataTable_Patient_Information.Rows.Count > 0)
              {
                DataTable DataTable_Patient_InformationClone = DataTable_Patient_Information.Clone();
                DataTable_Patient_InformationClone.Columns["Visit_Number"].DataType = typeof(string);
                foreach (DataRow DataRow_Patient_Information in DataTable_Patient_Information.Rows)
                {
                  DataTable_Patient_InformationClone.ImportRow(DataRow_Patient_Information);
                }

                DataRow[] Patient_InformationClone_VisitNumber = DataTable_Patient_InformationClone.Select("Visit_Number = '" + patientVisitNumber + "'");
                if (Patient_InformationClone_VisitNumber.Length == 0)
                {
                  DataTable_Patient_Information.Reset();
                  DataTable_Patient_Information.Columns.Add("Error", typeof(string));
                  DataTable_Patient_Information.Rows.Add("Patient Visit Number does not Exist");
                }

                DataTable_Patient_InformationClone.Reset();
              }
              else
              {
                DataTable_Patient_Information.Reset();
                DataTable_Patient_Information.Columns.Add("Error", typeof(string));
                DataTable_Patient_Information.Rows.Add("Patient Visit Number does not Exist");
              }
            }
            catch (Exception ex)
            {
              if (!string.IsNullOrEmpty(ex.ToString()))
              {
                DataTable_Patient_Information.Reset();
                DataTable_Patient_Information.Columns.Add("Error", typeof(string));
                DataTable_Patient_Information.Rows.Add("Patient data could not be retrieved from IMeds, Please try again later");
              }
              else
              {
                throw;
              }
            }
          }
        }

        return DataTable_Patient_Information;
      }
      //---END--- --GET PATIENT INFO FROM IMEDS-- --//
    }

    //Forms: None
    public static DataTable DataPatient_IMeds_Ailments(string facilityId, string patientVisitNumber)
    {
      //--START-- --GET PATIENT AILMENTS FROM IMEDS-- --//
      //FUNCTION
      //begin :x:=ecomm_thea.get_ailment(:facility,:visitnumber); end;
      //COLUMNS
      //Visit_Number
      //Visit_Ailment

      using (DataTable DataTable_Patient_Ailments = new DataTable())
      {
        DataTable_Patient_Ailments.Locale = CultureInfo.CurrentCulture;
        DataTable_Patient_Ailments.Reset();

        FromDataBase_ImedsConnectionString FromDataBase_ImedsConnectionString_Current = GetImedsConnectionString(facilityId);
        string ImedsConnectionString = FromDataBase_ImedsConnectionString_Current.ImedsConnectionString;
        string FacilityCode = FromDataBase_ImedsConnectionString_Current.FacilityCode;

        if (string.IsNullOrEmpty(ImedsConnectionString) || string.IsNullOrEmpty(FacilityCode))
        {
          DataTable_Patient_Ailments.Reset();
          DataTable_Patient_Ailments.Columns.Add("Error", typeof(string));
          DataTable_Patient_Ailments.Rows.Add("A connection could not be made to IMeds, Connection or Facility Code is missing, Please try again later");
        }
        else
        {
          using (OracleConnection OracleConnection_ImedsConnectionAilment = new OracleConnection(ImedsConnectionString))
          {
            try
            {
              OracleConnection_ImedsConnectionAilment.Open();

              string OracleStringImedsAilment = "begin :x:=ecomm_thea.get_ailment(:facility,:visitnumber); end;";
              OracleCommand OracleCommand_ImedsAilment = OracleConnection_ImedsConnectionAilment.CreateCommand();
              OracleCommand_ImedsAilment.CommandText = OracleStringImedsAilment;
              OracleCommand_ImedsAilment.Parameters.Add(":facility", OracleType.NVarChar).Value = FacilityCode;
              OracleCommand_ImedsAilment.Parameters.Add(":visitnumber", OracleType.Number).Value = patientVisitNumber;

              OracleParameter OracleParameterImedsAilment = OracleCommand_ImedsAilment.CreateParameter() as OracleParameter;
              OracleParameterImedsAilment.OracleType = OracleType.Cursor;
              OracleParameterImedsAilment.ParameterName = ":x";
              OracleParameterImedsAilment.Direction = ParameterDirection.ReturnValue;
              OracleParameterImedsAilment.Size = 10000;

              OracleCommand_ImedsAilment.Parameters.Add(OracleParameterImedsAilment);

              using (OracleDataAdapter OracleDataAdapter_ImedsAilment = new OracleDataAdapter(OracleCommand_ImedsAilment))
              {
                OracleDataAdapter_ImedsAilment.Fill(DataTable_Patient_Ailments);
              }
            }
            catch (Exception ex)
            {
              if (!string.IsNullOrEmpty(ex.ToString()))
              {
                DataTable_Patient_Ailments.Reset();
                DataTable_Patient_Ailments.Columns.Add("Error", typeof(string));
                DataTable_Patient_Ailments.Rows.Add("Patient data could not be retrieved from IMeds, Please try again later");
              }
              else
              {
                throw;
              }
            }
          }
        }

        return DataTable_Patient_Ailments;
      }
      //---END--- --GET PATIENT AILMENTS FROM IMEDS-- --//
    }

    //Forms: None
    public static DataTable DataPatient_IMeds_Theatre(string facilityId, string patientVisitNumber)
    {
      //--START-- --GET PATIENT THEATRE INFO FROM IMEDS-- --//
      //FUNCTION
      //begin :x:=ecomm_thea.get_theatreinfo(:facility,:visitnumber); end;
      //COLUMNS
      //VisitNumber
      //TheatreInvoiceNumber
      //TheatreDate
      //TheatreTimeIn
      //TheatreProcedure
      //TheatreProcedureCode

      using (DataTable DataTable_Patient_Theatre = new DataTable())
      {
        DataTable_Patient_Theatre.Locale = CultureInfo.CurrentCulture;
        DataTable_Patient_Theatre.Reset();

        FromDataBase_ImedsConnectionString FromDataBase_ImedsConnectionString_Current = GetImedsConnectionString(facilityId);
        string ImedsConnectionString = FromDataBase_ImedsConnectionString_Current.ImedsConnectionString;
        string FacilityCode = FromDataBase_ImedsConnectionString_Current.FacilityCode;

        if (string.IsNullOrEmpty(ImedsConnectionString) || string.IsNullOrEmpty(FacilityCode))
        {
          DataTable_Patient_Theatre.Reset();
          DataTable_Patient_Theatre.Columns.Add("Error", typeof(string));
          DataTable_Patient_Theatre.Rows.Add("A connection could not be made to IMeds, Connection or Facility Code is missing, Please try again later");
        }
        else
        {
          using (OracleConnection OracleConnection_ImedsConnectionTheatre = new OracleConnection(ImedsConnectionString))
          {
            try
            {
              OracleConnection_ImedsConnectionTheatre.Open();

              string OracleStringImedsTheatre = "begin :x:=ecomm_thea.get_theatreinfo(:facility,:visitnumber); end;";
              OracleCommand OracleCommand_ImedsTheatre = OracleConnection_ImedsConnectionTheatre.CreateCommand();
              OracleCommand_ImedsTheatre.CommandText = OracleStringImedsTheatre;
              OracleCommand_ImedsTheatre.Parameters.Add(":facility", OracleType.NVarChar).Value = FacilityCode;
              OracleCommand_ImedsTheatre.Parameters.Add(":visitnumber", OracleType.Number).Value = patientVisitNumber;

              OracleParameter OracleParameterImedsTheatre = OracleCommand_ImedsTheatre.CreateParameter() as OracleParameter;
              OracleParameterImedsTheatre.OracleType = OracleType.Cursor;
              OracleParameterImedsTheatre.ParameterName = ":x";
              OracleParameterImedsTheatre.Direction = ParameterDirection.ReturnValue;
              OracleParameterImedsTheatre.Size = 10000;

              OracleCommand_ImedsTheatre.Parameters.Add(OracleParameterImedsTheatre);

              using (OracleDataAdapter OracleDataAdapter_ImedsTheatre = new OracleDataAdapter(OracleCommand_ImedsTheatre))
              {
                OracleDataAdapter_ImedsTheatre.Fill(DataTable_Patient_Theatre);
              }
            }
            catch (Exception ex)
            {
              if (!string.IsNullOrEmpty(ex.ToString()))
              {
                DataTable_Patient_Theatre.Reset();
                DataTable_Patient_Theatre.Columns.Add("Error", typeof(string));
                DataTable_Patient_Theatre.Rows.Add("Patient data could not be retrieved from IMeds, Please try again later");
              }
              else
              {
                throw;
              }
            }
          }
        }

        return DataTable_Patient_Theatre;
      }
      //---END--- --GET PATIENT THEATRE INFO FROM IMEDS-- --//
    }

    //Forms: Form_InfectionPrevention
    public static DataTable DataPatient_IMeds_InfectionPrevention_VisitDiagnosis(string facilityId, string patientVisitNumber)
    {
      //--START-- --GET PATIENT InfectionPrevention_VistDiagnosis INFO FROM IMEDS-- --//
      //COLUMNS
      //CDG_CLASS
      //CDG_CD_CODE
      //CDG_TYPE
      //CD_DESCRIPTION
      //CDG_CLASS_ORDER

      using (DataTable DataTable_InfectionPrevention_VisitDiagnosis = new DataTable())
      {
        DataTable_InfectionPrevention_VisitDiagnosis.Locale = CultureInfo.CurrentCulture;
        DataTable_InfectionPrevention_VisitDiagnosis.Reset();

        FromDataBase_ImedsConnectionString FromDataBase_ImedsConnectionString_Current = GetImedsConnectionString(facilityId);
        string ImedsConnectionString = FromDataBase_ImedsConnectionString_Current.ImedsConnectionString;
        string FacilityCode = FromDataBase_ImedsConnectionString_Current.FacilityCode;

        if (string.IsNullOrEmpty(ImedsConnectionString) || string.IsNullOrEmpty(FacilityCode))
        {
          DataTable_InfectionPrevention_VisitDiagnosis.Reset();
          DataTable_InfectionPrevention_VisitDiagnosis.Columns.Add("Error", typeof(string));
          DataTable_InfectionPrevention_VisitDiagnosis.Rows.Add("A connection could not be made to IMeds, Connection or Facility Code is missing, Please try again later");
        }
        else
        {
          using (OracleConnection OracleConnection_ImedsConnection = new OracleConnection(ImedsConnectionString))
          {
            try
            {
              OracleConnection_ImedsConnection.Open();

              string OracleStringImeds = "SELECT DISTINCT A.CDG_CLASS, A.CDG_CD_CODE, A.CDG_TYPE, B.CD_DESCRIPTION, CASE WHEN CDG_CLASS = 'C' THEN '3' WHEN CDG_CLASS = 'S' THEN '2' ELSE '1' END AS CDG_CLASS_ORDER FROM CODING A JOIN CODING_CODES B ON B.CD_CODE = A.CDG_CD_CODE AND B.CD_TYPE = SUBSTR(CDG_TYPE,1,1) WHERE A.CDG_VIS_ID = :patientVisitNumber AND CD_TYPE != 'D' ORDER BY CDG_TYPE, CDG_CLASS_ORDER";
              OracleCommand OracleCommand_Imeds = OracleConnection_ImedsConnection.CreateCommand();
              OracleCommand_Imeds.CommandText = OracleStringImeds;
              OracleCommand_Imeds.Parameters.Add(":patientVisitNumber", OracleType.Number).Value = patientVisitNumber;

              OracleParameter OracleParameterImeds = OracleCommand_Imeds.CreateParameter() as OracleParameter;

              OracleCommand_Imeds.Parameters.Add(OracleParameterImeds);

              using (OracleDataAdapter OracleDataAdapter_Imeds = new OracleDataAdapter(OracleCommand_Imeds))
              {
                OracleDataAdapter_Imeds.Fill(DataTable_InfectionPrevention_VisitDiagnosis);
              }
            }
            catch (Exception ex)
            {
              if (!string.IsNullOrEmpty(ex.ToString()))
              {
                DataTable_InfectionPrevention_VisitDiagnosis.Reset();
                DataTable_InfectionPrevention_VisitDiagnosis.Columns.Add("Error", typeof(string));
                DataTable_InfectionPrevention_VisitDiagnosis.Rows.Add("Patient data could not be retrieved from IMeds, Please try again later");
              }
              else
              {
                throw;
              }
            }
          }
        }

        return DataTable_InfectionPrevention_VisitDiagnosis;
      }
      //---END--- --GET PATIENT InfectionPrevention_VistDiagnosis INFO FROM IMEDS-- --//
    }

    //Forms: Form_InfectionPrevention
    public static DataTable DataPatient_IMeds_InfectionPrevention_Antibiotic(string facilityId, string patientVisitNumber)
    {
      //--START-- --GET PATIENT InfectionPrevention_Antibiotic INFO FROM IMEDS-- --//
      //COLUMNS
      //IM_LONG_DESCRIPTION

      using (DataTable DataTable_InfectionPrevention_Antibiotic = new DataTable())
      {
        DataTable_InfectionPrevention_Antibiotic.Locale = CultureInfo.CurrentCulture;
        DataTable_InfectionPrevention_Antibiotic.Reset();

        FromDataBase_ImedsConnectionString FromDataBase_ImedsConnectionString_Current = GetImedsConnectionString(facilityId);
        string ImedsConnectionString = FromDataBase_ImedsConnectionString_Current.ImedsConnectionString;
        string FacilityCode = FromDataBase_ImedsConnectionString_Current.FacilityCode;

        if (string.IsNullOrEmpty(ImedsConnectionString) || string.IsNullOrEmpty(FacilityCode))
        {
          DataTable_InfectionPrevention_Antibiotic.Reset();
          //DataTable_InfectionPrevention_Antibiotic.Columns.Add("Error", typeof(string));
          //DataTable_InfectionPrevention_Antibiotic.Rows.Add("A connection could not be made to IMeds, Connection or Facility Code is missing, Please try again later");
        }
        else
        {
          using (OracleConnection OracleConnection_ImedsConnection = new OracleConnection(ImedsConnectionString))
          {
            try
            {
              OracleConnection_ImedsConnection.Open();

              string OracleStringImeds = "SELECT DISTINCT I.IM_LONG_DESCRIPTION FROM VISIT_CHARGES V, ITEMS I, CODES C WHERE V.VCG_VIS_ID = :patientVisitNumber AND I.IM_CG_CHG_CODE = V.VCG_CG_CHG_CODE AND C.CO_CODE_VALUE = I.IM_ANTIBIOTIC_CODE AND C.CO_CODE_ID LIKE 'ANT_%' ";
              OracleCommand OracleCommand_Imeds = OracleConnection_ImedsConnection.CreateCommand();
              OracleCommand_Imeds.CommandText = OracleStringImeds;
              OracleCommand_Imeds.Parameters.Add(":patientVisitNumber", OracleType.Number).Value = patientVisitNumber;

              OracleParameter OracleParameterImeds = OracleCommand_Imeds.CreateParameter() as OracleParameter;

              OracleCommand_Imeds.Parameters.Add(OracleParameterImeds);

              using (OracleDataAdapter OracleDataAdapter_Imeds = new OracleDataAdapter(OracleCommand_Imeds))
              {
                OracleDataAdapter_Imeds.Fill(DataTable_InfectionPrevention_Antibiotic);
              }
            }
            catch (Exception ex)
            {
              if (!string.IsNullOrEmpty(ex.ToString()))
              {
                DataTable_InfectionPrevention_Antibiotic.Reset();
                //DataTable_InfectionPrevention_Antibiotic.Columns.Add("Error", typeof(string));
                //DataTable_InfectionPrevention_Antibiotic.Rows.Add("Patient data could not be retrieved from IMeds, Please try again later");
              }
              else
              {
                throw;
              }
            }
          }
        }

        return DataTable_InfectionPrevention_Antibiotic;
      }
      //---END--- --GET PATIENT InfectionPrevention_Antibiotic INFO FROM IMEDS-- --//
    }

    //Forms: Form_InfectionPrevention
    public static DataTable DataPatient_IMeds_InfectionPrevention_Site_LabReport(string facilityId, string patientVisitNumber)
    {
      //--START-- --GET PATIENT InfectionPrevention_Site_LabReport INFO FROM IMEDS-- --//
      //COLUMNS
      //SPEC_DATE
      //SPEC_TYPE
      //ORG_DESC
      //SR_LAB_NUM

      using (DataTable DataTable_InfectionPrevention_Site_LabReport = new DataTable())
      {
        DataTable_InfectionPrevention_Site_LabReport.Locale = CultureInfo.CurrentCulture;
        DataTable_InfectionPrevention_Site_LabReport.Reset();

        FromDataBase_ImedsConnectionString FromDataBase_ImedsConnectionString_Current = GetImedsConnectionString(facilityId);
        string ImedsConnectionString = FromDataBase_ImedsConnectionString_Current.ImedsConnectionString;
        string FacilityCode = FromDataBase_ImedsConnectionString_Current.FacilityCode;

        if (string.IsNullOrEmpty(ImedsConnectionString) || string.IsNullOrEmpty(FacilityCode))
        {
          DataTable_InfectionPrevention_Site_LabReport.Reset();
          DataTable_InfectionPrevention_Site_LabReport.Columns.Add("Error", typeof(string));
          DataTable_InfectionPrevention_Site_LabReport.Rows.Add("A connection could not be made to IMeds, Connection or Facility Code is missing, Please try again later");
        }
        else
        {
          using (OracleConnection OracleConnection_ImedsConnection = new OracleConnection(ImedsConnectionString))
          {
            try
            {
              OracleConnection_ImedsConnection.Open();

              string OracleStringImeds = "SELECT S.SPEC_DATE, S.SPEC_TYPE, O.ORG_DESC, R.SR_LAB_NUM FROM VISIT_INFECTIONS I JOIN SPECIMENS S ON S.SPEC_VIN_INFECTION_ID = I.VIN_INFECTION_ID JOIN SPECIMEN_RESULTS R ON R.SR_SPEC_ID = S.SPEC_ID JOIN ORGANISMS O ON O.ORG_CODE = R.SR_ORG_CODE WHERE I.VIN_VIS_ID = :patientVisitNumber AND I.VIN_STATUS = 'CON' ORDER BY R.SR_RESULT_DATE DESC ";
              OracleCommand OracleCommand_Imeds = OracleConnection_ImedsConnection.CreateCommand();
              OracleCommand_Imeds.CommandText = OracleStringImeds;
              OracleCommand_Imeds.Parameters.Add(":patientVisitNumber", OracleType.Number).Value = patientVisitNumber;

              OracleParameter OracleParameterImeds = OracleCommand_Imeds.CreateParameter() as OracleParameter;

              OracleCommand_Imeds.Parameters.Add(OracleParameterImeds);

              using (OracleDataAdapter OracleDataAdapter_Imeds = new OracleDataAdapter(OracleCommand_Imeds))
              {
                OracleDataAdapter_Imeds.Fill(DataTable_InfectionPrevention_Site_LabReport);
              }
            }
            catch (Exception ex)
            {
              if (!string.IsNullOrEmpty(ex.ToString()))
              {
                DataTable_InfectionPrevention_Site_LabReport.Reset();
                DataTable_InfectionPrevention_Site_LabReport.Columns.Add("Error", typeof(string));
                DataTable_InfectionPrevention_Site_LabReport.Rows.Add("Patient data could not be retrieved from IMeds, Please try again later");
              }
              else
              {
                throw;
              }
            }
          }
        }

        return DataTable_InfectionPrevention_Site_LabReport;
      }
      //---END--- --GET PATIENT InfectionPrevention_Site_LabReport INFO FROM IMEDS-- --//
    }

    //Forms: Form_InfectionPrevention
    public static DataTable DataPatient_IMeds_InfectionPrevention_Site_BedHistory(string facilityId, string patientVisitNumber)
    {
      //--START-- --GET PATIENT InfectionPrevention_Site_BedHistory INFO FROM IMEDS-- --//
      //COLUMNS
      //DEP_DESCRIPTION
      //ROOMNUM
      //BEDNUM
      //VBD_DATE

      using (DataTable DataTable_InfectionPrevention_Site_BedHistory = new DataTable())
      {
        DataTable_InfectionPrevention_Site_BedHistory.Locale = CultureInfo.CurrentCulture;
        DataTable_InfectionPrevention_Site_BedHistory.Reset();

        FromDataBase_ImedsConnectionString FromDataBase_ImedsConnectionString_Current = GetImedsConnectionString(facilityId);
        string ImedsConnectionString = FromDataBase_ImedsConnectionString_Current.ImedsConnectionString;
        string FacilityCode = FromDataBase_ImedsConnectionString_Current.FacilityCode;

        if (string.IsNullOrEmpty(ImedsConnectionString) || string.IsNullOrEmpty(FacilityCode))
        {
          DataTable_InfectionPrevention_Site_BedHistory.Reset();
          DataTable_InfectionPrevention_Site_BedHistory.Columns.Add("Error", typeof(string));
          DataTable_InfectionPrevention_Site_BedHistory.Rows.Add("A connection could not be made to IMeds, Connection or Facility Code is missing, Please try again later");
        }
        else
        {
          using (OracleConnection OracleConnection_ImedsConnection = new OracleConnection(ImedsConnectionString))
          {
            try
            {
              OracleConnection_ImedsConnection.Open();

              string OracleStringImeds = "SELECT D.DEP_DESCRIPTION, b.VBD_RB_ROOM_NUM AS ROOMNUM, b.VBD_RB_BED_NUM AS BEDNUM, VBD_DATE FROM VISIT_BEDS B JOIN ROOM_BEDS RB ON RB.RB_STATION = B.VBD_RB_STATION AND RB.RB_ROOM_NUM = B.VBD_RB_ROOM_NUM AND RB.RB_BED_NUM = B.VBD_RB_BED_NUM JOIN DEPARTMENTS D ON D.DEP_CODE = RB.RB_ORD_DEP_CODE WHERE B.VBD_VIS_ID = :patientVisitNumber and VBD_ADT_IN_OUT = 'I' ORDER BY VBD_DATE ";
              OracleCommand OracleCommand_Imeds = OracleConnection_ImedsConnection.CreateCommand();
              OracleCommand_Imeds.CommandText = OracleStringImeds;
              OracleCommand_Imeds.Parameters.Add(":patientVisitNumber", OracleType.Number).Value = patientVisitNumber;

              OracleParameter OracleParameterImeds = OracleCommand_Imeds.CreateParameter() as OracleParameter;

              OracleCommand_Imeds.Parameters.Add(OracleParameterImeds);

              using (OracleDataAdapter OracleDataAdapter_Imeds = new OracleDataAdapter(OracleCommand_Imeds))
              {
                OracleDataAdapter_Imeds.Fill(DataTable_InfectionPrevention_Site_BedHistory);
              }
            }
            catch (Exception ex)
            {
              if (!string.IsNullOrEmpty(ex.ToString()))
              {
                DataTable_InfectionPrevention_Site_BedHistory.Reset();
                DataTable_InfectionPrevention_Site_BedHistory.Columns.Add("Error", typeof(string));
                DataTable_InfectionPrevention_Site_BedHistory.Rows.Add("Patient data could not be retrieved from IMeds, Please try again later");
              }
              else
              {
                throw;
              }
            }
          }
        }

        return DataTable_InfectionPrevention_Site_BedHistory;
      }
      //---END--- --GET PATIENT InfectionPrevention_Site_BedHistory INFO FROM IMEDS-- --//
    }

    //Forms: Form_InfectionPrevention
    public static DataTable DataPatient_IMeds_InfectionPrevention_Site_Surgery(string facilityId, string patientVisitNumber)
    {
      //--START-- --GET PATIENT InfectionPrevention_Site_Surgery INFO FROM IMEDS-- --//
      //COLUMNS
      //PatientIDNumber	
      //FacilityName	
      //FacilityCode	
      //VisitNumber	
      //InvoiceNum	
      //ProcedureCode	
      //ProcedureDescription	
      //Date	
      //Duration	
      //TheatreCode	
      //TheatreDescription	
      //Surgeon	
      //AssistantSurgeon	
      //ScrubNurse	
      //Anesthetist	
      //WoundCategory	
      //DateOfAdmission	
      //DischargeDate	
      //FinalDiagnosis	
      //FinalDiagnosisDescription

      DataTable DataTable_InfectionPrevention_Site_Surgery;
      using (DataTable_InfectionPrevention_Site_Surgery = new DataTable())
      {
        DataTable_InfectionPrevention_Site_Surgery.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_InfectionPrevention_Site_Surgery.Reset();
          DataTable_InfectionPrevention_Site_Surgery.Columns.Add("Error", typeof(string));
          DataTable_InfectionPrevention_Site_Surgery.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_InfectionPrevention_Site_Surgery.Reset();
          string FacilityCode = DataPatient_EDW_FacilityCode(facilityId);

          string SQLStringInfectionPrevention_Site_Surgery = "EXECUTE InfoQuest.PatientSurgeryDetailByVisitNumberProc @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_InfectionPrevention_Site_Surgery = new SqlCommand(SQLStringInfectionPrevention_Site_Surgery))
          {
            SqlCommand_InfectionPrevention_Site_Surgery.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_InfectionPrevention_Site_Surgery.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_InfectionPrevention_Site_Surgery = DataPatient_EDW_SqlGetData(SqlCommand_InfectionPrevention_Site_Surgery).Copy();
          }
        }
      }

      return DataTable_InfectionPrevention_Site_Surgery;
      //---END--- --GET PATIENT InfectionPrevention_Site_Surgery INFO FROM IMEDS-- --//
    }

    private class FromDataBase_ImedsConnectionString
    {
      public string ImedsConnectionString { get; set; }
      public string FacilityCode { get; set; }
    }

    private static FromDataBase_ImedsConnectionString GetImedsConnectionString(string facilityId)
    {
      FromDataBase_ImedsConnectionString FromDataBase_ImedsConnectionString_New = new FromDataBase_ImedsConnectionString();

      string SQLStringFacility = "SELECT Facility_FacilityCode , Facility_IMEDS_ConnectionString FROM Administration_Facility WHERE Facility_Id = @Facility_Id";
      using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
      {
        SqlCommand_Facility.Parameters.AddWithValue("@Facility_Id", facilityId);
        DataTable DataTable_Facility;
        using (DataTable_Facility = new DataTable())
        {
          DataTable_Facility.Locale = CultureInfo.CurrentCulture;
          DataTable_Facility = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Facility).Copy();
          if (DataTable_Facility.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Facility.Rows)
            {
              FromDataBase_ImedsConnectionString_New.ImedsConnectionString = DataRow_Row["Facility_IMEDS_ConnectionString"].ToString();
              FromDataBase_ImedsConnectionString_New.FacilityCode = DataRow_Row["Facility_FacilityCode"].ToString();
            }
          }
        }
      }

      return FromDataBase_ImedsConnectionString_New;
    }
#pragma warning restore CS0618


    public static DataTable DataPatient_EDW_SqlGetData(SqlCommand sqlCommand_SqlString)
    {
      using (DataTable DataTable_GetData = new DataTable())
      {
        DataTable_GetData.Locale = CultureInfo.CurrentCulture;
        string ConnectionStringGetData = InfoQuest_Connections.Connections("PatientDetailEDW");

        if (string.IsNullOrEmpty(ConnectionStringGetData))
        {
          DataTable_GetData.Reset();
          DataTable_GetData.Columns.Add("Error", typeof(string));
          DataTable_GetData.Rows.Add("Error: No EDW Connection String");
        }
        else
        {
          DataTable_GetData.Reset();
          using (SqlConnection SQLConnection_GetData = new SqlConnection(ConnectionStringGetData))
          {
            using (SqlDataAdapter SqlDataAdapter_GetData = new SqlDataAdapter())
            {
              try
              {
                if (sqlCommand_SqlString != null)
                {
                  foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
                  {
                    if (SqlParameter_Value.Value == null)
                    {
                      SqlParameter_Value.Value = DBNull.Value;
                    }
                  }

                  sqlCommand_SqlString.CommandType = CommandType.Text;
                  sqlCommand_SqlString.Connection = SQLConnection_GetData;
                  sqlCommand_SqlString.CommandTimeout = 600;
                  SQLConnection_GetData.Open();
                  SqlDataAdapter_GetData.SelectCommand = sqlCommand_SqlString;
                  SqlDataAdapter_GetData.Fill(DataTable_GetData);
                }
              }
              catch (Exception Exception_Error)
              {
                if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                {
                  DataTable_GetData.Reset();
                  DataTable_GetData.Columns.Add("Error", typeof(string));
                  DataTable_GetData.Rows.Add("Error: Data could not be retrieved from EDW");
                }
                else
                {
                  throw;
                }
              }
            }
          }
        }

        return DataTable_GetData;
      }
    }

    public static string DataPatient_EDW_FacilityCode(string facilityId)
    {
      string FacilityCode = "";
      string SQLStringFacility = "SELECT Facility_FacilityCode FROM Administration_Facility WHERE Facility_Id = @Facility_Id";
      using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
      {
        SqlCommand_Facility.Parameters.AddWithValue("@Facility_Id", facilityId);
        DataTable DataTable_Facility;
        using (DataTable_Facility = new DataTable())
        {
          DataTable_Facility.Locale = CultureInfo.CurrentCulture;
          DataTable_Facility = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Facility).Copy();
          if (DataTable_Facility.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Facility.Rows)
            {
              FacilityCode = DataRow_Row["Facility_FacilityCode"].ToString();
            }
          }
        }
      }

      return FacilityCode;
    }

    public static string DataPatient_EDW_ImpiloCountryId(string facilityId)
    {
      string ImpiloCountryId = "";
      string SQLStringImpiloCountryId = "SELECT Facility_ImpiloCountryId FROM Administration_Facility WHERE Facility_Id = @Facility_Id";
      using (SqlCommand SqlCommand_ImpiloCountryId = new SqlCommand(SQLStringImpiloCountryId))
      {
        SqlCommand_ImpiloCountryId.Parameters.AddWithValue("@Facility_Id", facilityId);
        DataTable DataTable_ImpiloCountryId;
        using (DataTable_ImpiloCountryId = new DataTable())
        {
          DataTable_ImpiloCountryId.Locale = CultureInfo.CurrentCulture;
          DataTable_ImpiloCountryId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ImpiloCountryId).Copy();
          if (DataTable_ImpiloCountryId.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ImpiloCountryId.Rows)
            {
              ImpiloCountryId = DataRow_Row["Facility_ImpiloCountryId"].ToString();
            }
          }
        }
      }

      return ImpiloCountryId;
    }

    public static string DataPatient_EDW_PatientInformationId(string facilityId, string patientVisitNumber)
    {
      string PatientInformationId = "";

      if (string.IsNullOrEmpty(facilityId) || string.IsNullOrEmpty(patientVisitNumber))
      {
        PatientInformationId = "Error: Facility or Visit Number is Empty";
      }
      else
      {
        string LifeCountry = DataPatient_EDW_ImpiloCountryId(facilityId);

        string LifeNumber = "";
        string Name = "";
        string Surname = "";
        string IDNumber = "";
        string DateOfBirth = "";
        string Gender = "";
        string Email = "";
        string ContactNumber = "";

        DataTable DataTable_PatientInformation;
        using (DataTable_PatientInformation = new DataTable())
        {
          DataTable_PatientInformation.Locale = CultureInfo.CurrentCulture;
          DataTable_PatientInformation = InfoQuest_DataPatient.DataPatient_EDW_PatientInformation(facilityId, patientVisitNumber).Copy();
          if (DataTable_PatientInformation.Columns.Count == 1)
          {
            foreach (DataRow DataRow_Row in DataTable_PatientInformation.Rows)
            {
              PatientInformationId = DataRow_Row["Error"].ToString();
            }
          }
          else
          {
            if (DataTable_PatientInformation.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PatientInformation.Rows)
              {
                LifeNumber = DataRow_Row["LifeNumber"].ToString();
                Name = DataRow_Row["Name"].ToString();
                Surname = DataRow_Row["Surname"].ToString();
                IDNumber = DataRow_Row["IDNumber"].ToString();
                DateOfBirth = DataRow_Row["DateOfBirth"].ToString();
                Gender = DataRow_Row["Gender"].ToString();
                Email = DataRow_Row["Email"].ToString();
                ContactNumber = DataRow_Row["ContactNumber"].ToString();
              }
            }
            else
            {
              PatientInformationId = "Error: No Records returned from EDW";
            }
          }
        }

        if (string.IsNullOrEmpty(LifeNumber))
        {
          if (string.IsNullOrEmpty(PatientInformationId))
          {
            PatientInformationId = "Error: Life Number is empty";
          }
        }
        else
        {
          PatientInformationId = "";
          string SQLStringPatientInformationId = "SELECT PatientInformation_Id FROM Lookup_PatientInformation WHERE PatientInformation_LifeCountry = @PatientInformation_LifeCountry AND PatientInformation_LifeNumber = @PatientInformation_LifeNumber";
          using (SqlCommand SqlCommand_PatientInformationId = new SqlCommand(SQLStringPatientInformationId))
          {
            SqlCommand_PatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeCountry", LifeCountry);
            SqlCommand_PatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeNumber", LifeNumber);
            DataTable DataTable_PatientInformationId;
            using (DataTable_PatientInformationId = new DataTable())
            {
              DataTable_PatientInformationId.Locale = CultureInfo.CurrentCulture;
              DataTable_PatientInformationId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInformationId).Copy();
              if (DataTable_PatientInformationId.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_PatientInformationId.Rows)
                {
                  PatientInformationId = DataRow_Row["PatientInformation_Id"].ToString();
                }
              }
            }
          }

          if (string.IsNullOrEmpty(PatientInformationId))
          {
            string SQLStringInsertPatientInformationId = "INSERT INTO Lookup_PatientInformation ( PatientInformation_LifeCountry , PatientInformation_LifeNumber , PatientInformation_Name , PatientInformation_Surname , PatientInformation_IDNumber , PatientInformation_DateOfBirth , PatientInformation_Gender , PatientInformation_Email , PatientInformation_ContactNumber ) VALUES ( @PatientInformation_LifeCountry , @PatientInformation_LifeNumber , @PatientInformation_Name , @PatientInformation_Surname , @PatientInformation_IDNumber , @PatientInformation_DateOfBirth , @PatientInformation_Gender , @PatientInformation_Email , @PatientInformation_ContactNumber ); SELECT SCOPE_IDENTITY()";
            using (SqlCommand SqlCommand_InsertPatientInformationId = new SqlCommand(SQLStringInsertPatientInformationId))
            {
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeCountry", LifeCountry);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeNumber", LifeNumber);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_Name", Name);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_Surname", Surname);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_IDNumber", IDNumber);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_DateOfBirth", DateOfBirth);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_Gender", Gender);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_Email", Email);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_ContactNumber", ContactNumber);

              PatientInformationId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertPatientInformationId);
            }
          }
          else
          {
            string SQLStringUpdatePatientInformationId = "UPDATE Lookup_PatientInformation SET PatientInformation_Name = @PatientInformation_Name , PatientInformation_Surname = @PatientInformation_Surname , PatientInformation_IDNumber = @PatientInformation_IDNumber , PatientInformation_DateOfBirth = @PatientInformation_DateOfBirth , PatientInformation_Gender = @PatientInformation_Gender , PatientInformation_Email = @PatientInformation_Email , PatientInformation_ContactNumber = @PatientInformation_ContactNumber WHERE PatientInformation_LifeCountry = @PatientInformation_LifeCountry AND PatientInformation_LifeNumber = @PatientInformation_LifeNumber";
            using (SqlCommand SqlCommand_UpdatePatientInformationId = new SqlCommand(SQLStringUpdatePatientInformationId))
            {
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_Name", Name);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_Surname", Surname);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_IDNumber", IDNumber);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_DateOfBirth", DateOfBirth);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_Gender", Gender);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_Email", Email);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_ContactNumber", ContactNumber);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeCountry", LifeCountry);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeNumber", LifeNumber);

              InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdatePatientInformationId);
            }
          }
        }
      }

      return PatientInformationId;
    }

    public static DataTable DataPatient_EDW_PatientInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode = "10";
      //VisitNumber = "429374";
      //LifeNumber = "3611282";
      //Name = "KHANYISA";
      //Surname = "MALULEKE";
      //IDNumber = "8109181052086";
      //DateOfBirth = "1981-09-18 00:00:00.000";
      //Gender = "F";
      //Email = "AMALK@ZA.902UTI.COM";
      //ContactNumber = "0728231146";

      DataTable DataTable_PatientInformation;
      using (DataTable_PatientInformation = new DataTable())
      {
        DataTable_PatientInformation.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_PatientInformation.Reset();
          DataTable_PatientInformation.Columns.Add("Error", typeof(string));
          DataTable_PatientInformation.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_PatientInformation.Reset();
          string FacilityCode = DataPatient_EDW_FacilityCode(facilityId);

          string SQLStringPatientInformation = "EXECUTE InfoQuest.IPS_Patient_Information_sp @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_PatientInformation = new SqlCommand(SQLStringPatientInformation))
          {
            SqlCommand_PatientInformation.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_PatientInformation.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_PatientInformation = DataPatient_EDW_SqlGetData(SqlCommand_PatientInformation).Copy();
          }
        }
      }

      return DataTable_PatientInformation;
    }

    //Forms: Form_BundleCompliance, Form_CRM, Form_FIMFAM, Form_Incident, Form_InfectionPrevention, Form_Isidima, Form_MedicationBundleCompliance, Form_MHQ14, Form_PROMS, Form_RehabBundleCompliance, Form_VTE
    public static DataTable DataPatient_EDW_VisitInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode = "10";
      //VisitNumber = "429374";
      //Name = "KHANYISA";
      //Surname = "MALULEKE";
      //DateOfBirth = "1981-09-18 00:00:00.000";
      //PatientAge = "31";
      //ContactNumber = "0728231146";
      //Email = "AMALK@ZA.902UTI.COM";
      //DateOfAdmission = "2013-01-16 05:10:00.000";
      //DateOfDischarge = "2013-01-19 11:24:00.000";
      //Deceased = "0";
      //Ward = "MATERNITY";
      //Room = "PN3";
      //Bed = "00D";

      DataTable DataTable_VisitInformation;
      using (DataTable_VisitInformation = new DataTable())
      {
        DataTable_VisitInformation.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_VisitInformation.Reset();
          DataTable_VisitInformation.Columns.Add("Error", typeof(string));
          DataTable_VisitInformation.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_VisitInformation.Reset();
          string FacilityCode = DataPatient_EDW_FacilityCode(facilityId);

          string SQLStringVisitInformation = "EXECUTE InfoQuest.Visit_Information_sp @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_VisitInformation = new SqlCommand(SQLStringVisitInformation))
          {
            SqlCommand_VisitInformation.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_VisitInformation.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_VisitInformation = DataPatient_EDW_SqlGetData(SqlCommand_VisitInformation).Copy();
          }
        }
      }

      return DataTable_VisitInformation;
    }

    //Forms: Form_BundleCompliance
    public static DataTable DataPatient_EDW_TheatreInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode = "10";
      //VisitNumber = "429374";
      //Theatre_Date = "2013-01-16 00:00:00.000";
      //Theatre_Procedure = "59514";
      //Theatre_Procedure_Description = "CESAREAN DELIVERY ONLY;, ANESTHESIA FOR CESAREAN DELIVERY FOLLOWING NEURAXIAL LABOR ANALGESIA/ANESTHESIA ";

      DataTable DataTable_TheatreInformation;
      using (DataTable_TheatreInformation = new DataTable())
      {
        DataTable_TheatreInformation.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_TheatreInformation.Reset();
          DataTable_TheatreInformation.Columns.Add("Error", typeof(string));
          DataTable_TheatreInformation.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_TheatreInformation.Reset();
          string FacilityCode = DataPatient_EDW_FacilityCode(facilityId);

          string SQLStringTheatreInformation = "EXECUTE InfoQuest.Theatre_Information_sp @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_TheatreInformation = new SqlCommand(SQLStringTheatreInformation))
          {
            SqlCommand_TheatreInformation.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_TheatreInformation.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_TheatreInformation = DataPatient_EDW_SqlGetData(SqlCommand_TheatreInformation).Copy();
          }
        }
      }

      return DataTable_TheatreInformation;
    }

    //Forms: Form_IPS
    public static DataTable DataPatient_EDW_IPS_VisitInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode = "10";
      //VisitNumber = "429374";
      //DateOfAdmission = "2013-01-16 05:10:00.000";
      //DateOfDischarge = "2013-01-19 11:24:00.000";
      //DateOfBirth = "1981-09-18 00:00:00.000";
      //PatientAge = "31";
      //Deceased = "0";
      //Ward = "MATERNITY";
      //Room = "PN3";
      //Bed = "00D";

      DataTable DataTable_VisitInformation;
      using (DataTable_VisitInformation = new DataTable())
      {
        DataTable_VisitInformation.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_VisitInformation.Reset();
          DataTable_VisitInformation.Columns.Add("Error", typeof(string));
          DataTable_VisitInformation.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_VisitInformation.Reset();
          string FacilityCode = DataPatient_EDW_FacilityCode(facilityId);

          string SQLStringVisitInformation = "EXECUTE InfoQuest.IPS_Visit_Information_sp @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_VisitInformation = new SqlCommand(SQLStringVisitInformation))
          {
            SqlCommand_VisitInformation.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_VisitInformation.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_VisitInformation = DataPatient_EDW_SqlGetData(SqlCommand_VisitInformation).Copy();
          }
        }
      }

      return DataTable_VisitInformation;
    }

    //Forms: Form_IPS
    public static DataTable DataPatient_EDW_IPS_TheatreInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode = "10";
      //VisitNumber = "429374";
      //LifeNumber = "3611282";
      //FinalDiagnosisCode = "O80.0";
      //FinalDiagnosisDescription = "SPONTANEOUS VERTEX DELIVERY";
      //AdmissionDate = "2013-01-16 05:10:00.000";
      //DischargeDate = "2013-01-19 11:24:00.000";
      //ProcedureDate = "2013-01-16 00:00:00.000";
      //TheatreInvoice = "T0429374";
      //Theatre = "THEATRE";
      //TheatreTime = "64.000000";
      //Surgeon = "DR DUSTAN  LUMU  (1656) ";
      //Anaesthetist = "DR GODFREY MUJIZI-KANAKULYA  (2009) ";
      //Assistant = "NULL";
      //ProcedureCode = "NULL";
      //ProcedureDescription = "CESAREAN DELIVERY ONLY; , POOR PROGRESS";
      //ScrubNurse = "M MHLONGO";
      //WoundCategory = "NULL";
      //ServiceCategory = "SURGICAL";
      //IP_OP_Indicator = "I";
      //VisitType = "IN-PATIENT";


      DataTable DataTable_TheatreInformation;
      using (DataTable_TheatreInformation = new DataTable())
      {
        DataTable_TheatreInformation.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_TheatreInformation.Reset();
          DataTable_TheatreInformation.Columns.Add("Error", typeof(string));
          DataTable_TheatreInformation.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_TheatreInformation.Reset();
          string FacilityCode = DataPatient_EDW_FacilityCode(facilityId);

          string SQLStringTheatreInformation = "EXECUTE InfoQuest.IPS_Theatre_Information_sp @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_TheatreInformation = new SqlCommand(SQLStringTheatreInformation))
          {
            SqlCommand_TheatreInformation.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_TheatreInformation.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_TheatreInformation = DataPatient_EDW_SqlGetData(SqlCommand_TheatreInformation).Copy();
          }
        }
      }

      return DataTable_TheatreInformation;
    }

    //Forms: Form_IPS
    public static DataTable DataPatient_EDW_IPS_CodingInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode = "10";
      //VisitNumber = "429374";
      //CodeType = "ICD";
      //CodeClass = "P";
      //Code = "O80.0";
      //CodeDescription = "SPONTANEOUS VERTEX DELIVERY";
      //Sequence = "1";


      DataTable DataTable_VisitDiagnosis;
      using (DataTable_VisitDiagnosis = new DataTable())
      {
        DataTable_VisitDiagnosis.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_VisitDiagnosis.Reset();
          DataTable_VisitDiagnosis.Columns.Add("Error", typeof(string));
          DataTable_VisitDiagnosis.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_VisitDiagnosis.Reset();
          string FacilityCode = DataPatient_EDW_FacilityCode(facilityId);

          string SQLStringVisitDiagnosis = "EXECUTE InfoQuest.IPS_Coding_Information_sp @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_VisitDiagnosis = new SqlCommand(SQLStringVisitDiagnosis))
          {
            SqlCommand_VisitDiagnosis.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_VisitDiagnosis.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_VisitDiagnosis = DataPatient_EDW_SqlGetData(SqlCommand_VisitDiagnosis).Copy();
          }
        }
      }

      return DataTable_VisitDiagnosis;
    }

    //Forms: Form_IPS
    public static DataTable DataPatient_EDW_IPS_AccommodationInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode = "10";
      //VisitNumber = "429374";
      //Department = "MATERNITY";
      //Room = "ANC2";
      //Bed = "001";
      //Date = "2013-01-16 05:10:00.000";
      //MoveType = "admit into MATERNITY bed 001 room ANC2 on the Jan 16 2013  5:10AM";
      //Sequence = "1";
      //MovementCode = "A";
      //MovementInOutFlag = "I";


      DataTable DataTable_BedHistory;
      using (DataTable_BedHistory = new DataTable())
      {
        DataTable_BedHistory.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_BedHistory.Reset();
          DataTable_BedHistory.Columns.Add("Error", typeof(string));
          DataTable_BedHistory.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_BedHistory.Reset();
          string FacilityCode = DataPatient_EDW_FacilityCode(facilityId);

          string SQLStringBedHistory = "EXECUTE InfoQuest.IPS_Accommodation_Information_sp @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_BedHistory = new SqlCommand(SQLStringBedHistory))
          {
            SqlCommand_BedHistory.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_BedHistory.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_BedHistory = DataPatient_EDW_SqlGetData(SqlCommand_BedHistory).Copy();
          }
        }
      }

      return DataTable_BedHistory;
    }

    //Forms: Form_IPS, Form_AntimicrobialStewardshipIntervention
    public static DataTable DataPatient_EDW_IPS_AntibioticInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode = "10";
      //VisitNumber = "429374";
      //Description = "ADCO-CEFTRIAXONE 1G VIAL";

      DataTable DataTable_AntibioticPrescription;
      using (DataTable_AntibioticPrescription = new DataTable())
      {
        DataTable_AntibioticPrescription.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_AntibioticPrescription.Reset();
          DataTable_AntibioticPrescription.Columns.Add("Error", typeof(string));
          DataTable_AntibioticPrescription.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_AntibioticPrescription.Reset();
          string FacilityCode = DataPatient_EDW_FacilityCode(facilityId);

          string SQLStringAntibioticPrescription = "EXECUTE InfoQuest.IPS_Antibiotic_Information_sp @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_AntibioticPrescription = new SqlCommand(SQLStringAntibioticPrescription))
          {
            SqlCommand_AntibioticPrescription.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_AntibioticPrescription.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_AntibioticPrescription = DataPatient_EDW_SqlGetData(SqlCommand_AntibioticPrescription).Copy();
          }
        }
      }

      return DataTable_AntibioticPrescription;
    }

    //Form_FIMFAM
    public static DataTable DataPatient_EDW_ImpairmentCoding(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //HospitalCode = "11";
      //VisitNumber = "7820";
      //ImpairmentClassificationDescription = "Brain Dysfunction";

      DataTable DataTable_ImpairmentCoding;
      using (DataTable_ImpairmentCoding = new DataTable())
      {
        DataTable_ImpairmentCoding.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_ImpairmentCoding.Reset();
          DataTable_ImpairmentCoding.Columns.Add("Error", typeof(string));
          DataTable_ImpairmentCoding.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_ImpairmentCoding.Reset();
          string FacilityCode = DataPatient_EDW_FacilityCode(facilityId);

          string SQLStringImpairmentCoding = "EXECUTE InfoQuest.ImpairmentCoding_SP @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_ImpairmentCoding = new SqlCommand(SQLStringImpairmentCoding))
          {
            SqlCommand_ImpairmentCoding.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_ImpairmentCoding.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_ImpairmentCoding = InfoQuest_DataPatient.DataPatient_EDW_SqlGetData(SqlCommand_ImpairmentCoding).Copy();
          }
        }
      }

      return DataTable_ImpairmentCoding;
    }


    #region EDWStaging
    //public static DataTable DataPatient_EDWStaging_SqlGetData(SqlCommand sqlCommand_SqlString)
    //{
    //  using (DataTable DataTable_GetData = new DataTable())
    //  {
    //    DataTable_GetData.Locale = CultureInfo.CurrentCulture;
    //    String ConnectionStringGetData = InfoQuest_Connections.Connections("PatientDetailEDWStaging");

    //    if (String.IsNullOrEmpty(ConnectionStringGetData))
    //    {
    //      DataTable_GetData.Reset();
    //      DataTable_GetData.Columns.Add("Error", typeof(string));
    //      DataTable_GetData.Rows.Add("Error: No EDW Staging Connection String");
    //    }
    //    else
    //    {
    //      DataTable_GetData.Reset();
    //      using (SqlConnection SQLConnection_GetData = new SqlConnection(ConnectionStringGetData))
    //      {
    //        using (SqlDataAdapter SqlDataAdapter_GetData = new SqlDataAdapter())
    //        {
    //          try
    //          {
    //            if (sqlCommand_SqlString != null)
    //            {
    //              foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
    //              {
    //                if (SqlParameter_Value.Value == null)
    //                {
    //                  SqlParameter_Value.Value = DBNull.Value;
    //                }
    //              }

    //              sqlCommand_SqlString.CommandType = CommandType.Text;
    //              sqlCommand_SqlString.Connection = SQLConnection_GetData;
    //              sqlCommand_SqlString.CommandTimeout = 600;
    //              SQLConnection_GetData.Open();
    //              SqlDataAdapter_GetData.SelectCommand = sqlCommand_SqlString;
    //              SqlDataAdapter_GetData.Fill(DataTable_GetData);
    //            }
    //          }
    //          catch (Exception Exception_Error)
    //          {
    //            if (!String.IsNullOrEmpty(Exception_Error.ToString()))
    //            {
    //              DataTable_GetData.Reset();
    //              DataTable_GetData.Columns.Add("Error", typeof(string));
    //              DataTable_GetData.Rows.Add("Error: Data could not be retrieved from EDW Staging");
    //            }
    //            else
    //            {
    //              throw;
    //            }
    //          }
    //        }
    //      }
    //    }

    //    return DataTable_GetData;
    //  }
    //}

    //public static String DataPatient_EDWStaging_FacilityCode(String facilityId)
    //{
    //  String FacilityCode = "";
    //  String SQLStringFacility = "SELECT Facility_FacilityCode FROM Administration_Facility WHERE Facility_Id = @Facility_Id";
    //  using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
    //  {
    //    SqlCommand_Facility.Parameters.AddWithValue("@Facility_Id", facilityId);
    //    DataTable DataTable_Facility;
    //    using (DataTable_Facility = new DataTable())
    //    {
    //      DataTable_Facility.Locale = CultureInfo.CurrentCulture;
    //      DataTable_Facility = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Facility).Copy();
    //      if (DataTable_Facility.Rows.Count > 0)
    //      {
    //        foreach (DataRow DataRow_Row in DataTable_Facility.Rows)
    //        {
    //          FacilityCode = DataRow_Row["Facility_FacilityCode"].ToString();
    //        }
    //      }
    //    }
    //  }

    //  return FacilityCode;
    //}

    //public static String DataPatient_EDWStaging_ImpiloCountryId(String facilityId)
    //{
    //  String ImpiloCountryId = "";
    //  String SQLStringImpiloCountryId = "SELECT Facility_ImpiloCountryId FROM Administration_Facility WHERE Facility_Id = @Facility_Id";
    //  using (SqlCommand SqlCommand_ImpiloCountryId = new SqlCommand(SQLStringImpiloCountryId))
    //  {
    //    SqlCommand_ImpiloCountryId.Parameters.AddWithValue("@Facility_Id", facilityId);
    //    DataTable DataTable_ImpiloCountryId;
    //    using (DataTable_ImpiloCountryId = new DataTable())
    //    {
    //      DataTable_ImpiloCountryId.Locale = CultureInfo.CurrentCulture;
    //      DataTable_ImpiloCountryId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ImpiloCountryId).Copy();
    //      if (DataTable_ImpiloCountryId.Rows.Count > 0)
    //      {
    //        foreach (DataRow DataRow_Row in DataTable_ImpiloCountryId.Rows)
    //        {
    //          ImpiloCountryId = DataRow_Row["Facility_ImpiloCountryId"].ToString();
    //        }
    //      }
    //    }
    //  }

    //  return ImpiloCountryId;
    //}

    //public static String DataPatient_EDWStaging_PatientInformationId(String facilityId, String patientVisitNumber)
    //{
    //  String PatientInformationId = "";

    //  if (String.IsNullOrEmpty(facilityId) || String.IsNullOrEmpty(patientVisitNumber))
    //  {
    //    PatientInformationId = "Error: Facility or Visit Number is Empty";
    //  }
    //  else
    //  {
    //    String LifeCountry = DataPatient_EDWStaging_ImpiloCountryId(facilityId);

    //    String LifeNumber = "";
    //    String Name = "";
    //    String Surname = "";
    //    String IDNumber = "";
    //    String DateOfBirth = "";
    //    String Gender = "";
    //    String Email = "";
    //    String ContactNumber = "";

    //    DataTable DataTable_PatientInformation;
    //    using (DataTable_PatientInformation = new DataTable())
    //    {
    //      DataTable_PatientInformation.Locale = CultureInfo.CurrentCulture;
    //      DataTable_PatientInformation = InfoQuest_DataPatient.DataPatient_EDWStaging_PatientInformation(facilityId, patientVisitNumber).Copy();
    //      if (DataTable_PatientInformation.Columns.Count == 1)
    //      {
    //        foreach (DataRow DataRow_Row in DataTable_PatientInformation.Rows)
    //        {
    //          PatientInformationId = DataRow_Row["Error"].ToString();
    //        }
    //      }
    //      else
    //      {
    //        if (DataTable_PatientInformation.Rows.Count > 0)
    //        {
    //          foreach (DataRow DataRow_Row in DataTable_PatientInformation.Rows)
    //          {
    //            LifeNumber = DataRow_Row["PatientInformation_PatientLifeNumber"].ToString();
    //            Name = DataRow_Row["PatientInformation_PatientName"].ToString();
    //            Surname = DataRow_Row["PatientInformation_PatientSurname"].ToString();
    //            IDNumber = DataRow_Row["PatientInformation_PatientIDNumber"].ToString();
    //            DateOfBirth = DataRow_Row["PatientInformation_PatientDateOfBirth"].ToString();
    //            Gender = DataRow_Row["PatientInformation_PatientGender"].ToString();
    //            Email = DataRow_Row["PatientInformation_PatientEmail"].ToString();
    //            ContactNumber = DataRow_Row["PatientInformation_PatientPhoneNumber"].ToString();
    //          }
    //        }
    //        else
    //        {
    //          PatientInformationId = "Error: No Records returned from EDW Staging";
    //        }
    //      }
    //    }

    //    if (String.IsNullOrEmpty(LifeNumber) && String.IsNullOrEmpty(PatientInformationId))
    //    {
    //      if (String.IsNullOrEmpty(PatientInformationId))
    //      {
    //        PatientInformationId = "Error: Life Number is empty";
    //      }
    //    }
    //    else
    //    {
    //      PatientInformationId = "";
    //      String SQLStringPatientInformationId = "SELECT PatientInformation_Id FROM Lookup_PatientInformation WHERE PatientInformation_LifeCountry = @PatientInformation_LifeCountry AND PatientInformation_LifeNumber = @PatientInformation_LifeNumber";
    //      using (SqlCommand SqlCommand_PatientInformationId = new SqlCommand(SQLStringPatientInformationId))
    //      {
    //        SqlCommand_PatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeCountry", LifeCountry);
    //        SqlCommand_PatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeNumber", LifeNumber);
    //        DataTable DataTable_PatientInformationId;
    //        using (DataTable_PatientInformationId = new DataTable())
    //        {
    //          DataTable_PatientInformationId.Locale = CultureInfo.CurrentCulture;
    //          DataTable_PatientInformationId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInformationId).Copy();
    //          if (DataTable_PatientInformationId.Rows.Count > 0)
    //          {
    //            foreach (DataRow DataRow_Row in DataTable_PatientInformationId.Rows)
    //            {
    //              PatientInformationId = DataRow_Row["PatientInformation_Id"].ToString();
    //            }
    //          }
    //        }
    //      }

    //      if (String.IsNullOrEmpty(PatientInformationId))
    //      {
    //        String SQLStringInsertPatientInformationId = "INSERT INTO Lookup_PatientInformation ( PatientInformation_LifeCountry , PatientInformation_LifeNumber , PatientInformation_Name , PatientInformation_Surname , PatientInformation_IDNumber , PatientInformation_DateOfBirth , PatientInformation_Gender , PatientInformation_Email , PatientInformation_ContactNumber ) VALUES ( @PatientInformation_LifeCountry , @PatientInformation_LifeNumber , @PatientInformation_Name , @PatientInformation_Surname , @PatientInformation_IDNumber , @PatientInformation_DateOfBirth , @PatientInformation_Gender , @PatientInformation_Email , @PatientInformation_ContactNumber ); SELECT SCOPE_IDENTITY()";
    //        using (SqlCommand SqlCommand_InsertPatientInformationId = new SqlCommand(SQLStringInsertPatientInformationId))
    //        {
    //          SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeCountry", LifeCountry);
    //          SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeNumber", LifeNumber);
    //          SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_Name", Name);
    //          SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_Surname", Surname);
    //          SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_IDNumber", IDNumber);
    //          SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_DateOfBirth", DateOfBirth);
    //          SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_Gender", Gender);
    //          SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_Email", Email);
    //          SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_ContactNumber", ContactNumber);

    //          PatientInformationId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertPatientInformationId);
    //        }
    //      }
    //      else
    //      {
    //        String SQLStringUpdatePatientInformationId = "UPDATE Lookup_PatientInformation SET PatientInformation_Name = @PatientInformation_Name , PatientInformation_Surname = @PatientInformation_Surname , PatientInformation_IDNumber = @PatientInformation_IDNumber , PatientInformation_DateOfBirth = @PatientInformation_DateOfBirth , PatientInformation_Gender = @PatientInformation_Gender , PatientInformation_Email = @PatientInformation_Email , PatientInformation_ContactNumber = @PatientInformation_ContactNumber WHERE PatientInformation_LifeCountry = @PatientInformation_LifeCountry AND PatientInformation_LifeNumber = @PatientInformation_LifeNumber";
    //        using (SqlCommand SqlCommand_UpdatePatientInformationId = new SqlCommand(SQLStringUpdatePatientInformationId))
    //        {
    //          SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_Name", Name);
    //          SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_Surname", Surname);
    //          SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_IDNumber", IDNumber);
    //          SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_DateOfBirth", DateOfBirth);
    //          SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_Gender", Gender);
    //          SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_Email", Email);
    //          SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_ContactNumber", ContactNumber);
    //          SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeCountry", LifeCountry);
    //          SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeNumber", LifeNumber);

    //          InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdatePatientInformationId);
    //        }
    //      }
    //    }
    //  }

    //  return PatientInformationId;
    //}

    //public static DataTable DataPatient_EDWStaging_PatientInformation(String facilityId, String patientVisitNumber)
    //{
    //  //COLUMNS
    //  //PatientInformation_FacilityCode = 10
    //  //PatientInformation_PatientVisitNumber = 429374
    //  //PatientInformation_PatientLifeNumber = 3625748
    //  //PatientInformation_PatientName = Khanyisa
    //  //PatientInformation_PatientSurname = Maluleke
    //  //PatientInformation_PatientIDNumber = 8109181052086
    //  //PatientInformation_PatientDateOfBirth = 1981/09/18
    //  //PatientInformation_PatientGender = Female
    //  //PatientInformation_PatientEmail = amalk@za.902uti.com
    //  //PatientInformation_PatientPhoneNumber = 0728231146

    //  DataTable DataTable_PatientInformation;
    //  using (DataTable_PatientInformation = new DataTable())
    //  {
    //    DataTable_PatientInformation.Locale = CultureInfo.CurrentCulture;

    //    if (String.IsNullOrEmpty(facilityId) && String.IsNullOrEmpty(patientVisitNumber))
    //    {
    //      DataTable_PatientInformation.Reset();
    //      DataTable_PatientInformation.Columns.Add("Error", typeof(string));
    //      DataTable_PatientInformation.Rows.Add("Error: Facility or Visit Number is Empty");
    //    }
    //    else
    //    {
    //      DataTable_PatientInformation.Reset();
    //      String FacilityCode = DataPatient_EDWStaging_FacilityCode(facilityId);

    //      String SQLStringPatientInformation = "SELECT		DimHospital.HospitalCode AS PatientInformation_FacilityCode , " +
    //                                           "           FactAdmission.VisitNumber AS PatientInformation_PatientVisitNumber , " +
    //                                           "           DimPerson.LifeNumber AS PatientInformation_PatientLifeNumber , " +
    //                                           "           DimPerson.Name AS PatientInformation_PatientName , " +
    //                                           "           DimPerson.Surname AS PatientInformation_PatientSurname , " +
    //                                           "           DimPerson.IDNumber AS PatientInformation_PatientIDNumber , " +
    //                                           "           CONVERT(NVARCHAR(MAX), DimPerson.DateOfBirth, 111) AS PatientInformation_PatientDateOfBirth , " +
    //                                           "           DimGender.GenderDescription AS PatientInformation_PatientGender , " +
    //                                           "           DimPerson.EmailAddress AS PatientInformation_PatientEmail , " +
    //                                           "           DimPerson.PhoneNumberMobile AS PatientInformation_PatientPhoneNumber " +
    //                                           " FROM			Demographics.DimPerson " +
    //                                           "           INNER JOIN Admission.FactAdmission ON FactAdmission.PatientPersonKey = DimPerson.PersonKey " +
    //                                           "           INNER JOIN BusinessUnit.DimHospital ON DimHospital.BusinessUnitKey = FactAdmission.BusinessUnitKey " +
    //                                           "           INNER JOIN Demographics.DimGender ON DimPerson.GenderKey = DimGender.GenderKey " +
    //                                           " WHERE			DimHospital.HospitalCode = @FacilityCode " +
    //                                           "           AND FactAdmission.VisitNumber = @VisitNumber " +
    //                                           "           AND DimPerson.LifeNumber IS NOT NULL";
    //      using (SqlCommand SqlCommand_PatientInformation = new SqlCommand(SQLStringPatientInformation))
    //      {
    //        SqlCommand_PatientInformation.Parameters.AddWithValue("@FacilityCode", FacilityCode);
    //        SqlCommand_PatientInformation.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
    //        DataTable_PatientInformation = InfoQuest_DataPatient.DataPatient_EDWStaging_SqlGetData(SqlCommand_PatientInformation).Copy();
    //      }
    //    }
    //  }

    //  return DataTable_PatientInformation;
    //}

    //public static DataTable DataPatient_EDWStaging_VisitInformation(String facilityId, String patientVisitNumber)
    //{
    //  //COLUMNS
    //  //VisitInformation_FacilityCode = 10
    //  //VisitInformation_PatientVisitNumber = 429374
    //  //VisitInformation_PatientSurnameName = Maluleke,Khanyisa
    //  //VisitInformation_PatientAge = 31
    //  //VisitInformation_PatientDateOfAdmission = 2013-01-16 05:10:00.000
    //  //VisitInformation_PatientDateOfDischarge = 2013-01-19 11:24:00.000

    //  DataTable DataTable_VisitInformation;
    //  using (DataTable_VisitInformation = new DataTable())
    //  {
    //    DataTable_VisitInformation.Locale = CultureInfo.CurrentCulture;

    //    if (String.IsNullOrEmpty(facilityId) && String.IsNullOrEmpty(patientVisitNumber))
    //    {
    //      DataTable_VisitInformation.Reset();
    //      DataTable_VisitInformation.Columns.Add("Error", typeof(string));
    //      DataTable_VisitInformation.Rows.Add("Error: Facility or Visit Number is Empty");
    //    }
    //    else
    //    {
    //      DataTable_VisitInformation.Reset();
    //      String FacilityCode = DataPatient_EDWStaging_FacilityCode(facilityId);

    //      String SQLStringVisitInformation = "SELECT		DimHospital.HospitalCode AS VisitInformation_FacilityCode , " +
    //                                         "           FactAdmission.VisitNumber AS VisitInformation_PatientVisitNumber , " +
    //                                         "           DimPerson.Surname + ',' + DimPerson.Name AS VisitInformation_PatientSurnameName , " +
    //                                         "           FactAdmission.AgeAtAdmission AS VisitInformation_PatientAge , " +
    //                                         "           FactAdmission.AdmitDateTime AS VisitInformation_PatientDateOfAdmission , " +
    //                                         "           FactAdmission.DepartureDateTime AS VisitInformation_PatientDateOfDischarge " +
    //                                         " FROM			Demographics.DimPerson " +
    //                                         "           INNER JOIN Admission.FactAdmission ON FactAdmission.PatientPersonKey = DimPerson.PersonKey " +
    //                                         "           INNER JOIN BusinessUnit.DimHospital ON DimHospital.BusinessUnitKey = FactAdmission.BusinessUnitKey " +
    //                                         "           INNER JOIN Demographics.DimGender ON DimPerson.GenderKey = DimGender.GenderKey " +
    //                                         " WHERE			DimHospital.HospitalCode = @FacilityCode " +
    //                                         "           AND FactAdmission.VisitNumber = @VisitNumber " +
    //                                         "           AND DimPerson.LifeNumber IS NOT NULL";
    //      using (SqlCommand SqlCommand_VisitInformation = new SqlCommand(SQLStringVisitInformation))
    //      {
    //        SqlCommand_VisitInformation.Parameters.AddWithValue("@FacilityCode", FacilityCode);
    //        SqlCommand_VisitInformation.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
    //        DataTable_VisitInformation = InfoQuest_DataPatient.DataPatient_EDWStaging_SqlGetData(SqlCommand_VisitInformation).Copy();

    //        if (DataTable_VisitInformation.Rows.Count == 0)
    //        {
    //          DataTable_VisitInformation.Reset();
    //          DataTable_VisitInformation.Columns.Add("Error", typeof(string));
    //          DataTable_VisitInformation.Rows.Add("Error: No Records returned from EDW Staging");
    //        }
    //      }
    //    }
    //  }

    //  return DataTable_VisitInformation;
    //}
    #endregion


    public static DataTable DataPatient_ODS_SqlGetData(SqlCommand sqlCommand_SqlString)
    {
      using (DataTable DataTable_GetData = new DataTable())
      {
        DataTable_GetData.Locale = CultureInfo.CurrentCulture;
        string ConnectionStringGetData = InfoQuest_Connections.Connections("PatientDetailODS");

        if (string.IsNullOrEmpty(ConnectionStringGetData))
        {
          DataTable_GetData.Reset();
          DataTable_GetData.Columns.Add("Error", typeof(string));
          DataTable_GetData.Rows.Add("Error: No ODS Connection String");
        }
        else
        {
          DataTable_GetData.Reset();
          using (SqlConnection SQLConnection_GetData = new SqlConnection(ConnectionStringGetData))
          {
            using (SqlDataAdapter SqlDataAdapter_GetData = new SqlDataAdapter())
            {
              try
              {
                if (sqlCommand_SqlString != null)
                {
                  foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
                  {
                    if (SqlParameter_Value.Value == null)
                    {
                      SqlParameter_Value.Value = DBNull.Value;
                    }
                  }

                  sqlCommand_SqlString.CommandType = CommandType.Text;
                  sqlCommand_SqlString.Connection = SQLConnection_GetData;
                  sqlCommand_SqlString.CommandTimeout = 600;
                  SQLConnection_GetData.Open();
                  SqlDataAdapter_GetData.SelectCommand = sqlCommand_SqlString;
                  SqlDataAdapter_GetData.Fill(DataTable_GetData);
                }
              }
              catch (Exception Exception_Error)
              {
                if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                {
                  DataTable_GetData.Reset();
                  DataTable_GetData.Columns.Add("Error", typeof(string));
                  DataTable_GetData.Rows.Add("Error: Data could not be retrieved from ODS");
                }
                else
                {
                  throw;
                }
              }
            }
          }
        }

        return DataTable_GetData;
      }
    }

    public static string DataPatient_ODS_FacilityCode(string facilityId)
    {
      string FacilityCode = "";
      string SQLStringFacility = "SELECT Facility_FacilityCode FROM Administration_Facility WHERE Facility_Id = @Facility_Id";
      using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
      {
        SqlCommand_Facility.Parameters.AddWithValue("@Facility_Id", facilityId);
        DataTable DataTable_Facility;
        using (DataTable_Facility = new DataTable())
        {
          DataTable_Facility.Locale = CultureInfo.CurrentCulture;
          DataTable_Facility = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Facility).Copy();
          if (DataTable_Facility.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Facility.Rows)
            {
              FacilityCode = DataRow_Row["Facility_FacilityCode"].ToString();
            }
          }
        }
      }

      return FacilityCode;
    }

    public static string DataPatient_ODS_ImpiloUnitId(string facilityId)
    {
      string ImpiloUnitId = "";
      string SQLStringImpiloUnitId = "SELECT Facility_ImpiloUnitId FROM Administration_Facility WHERE Facility_Id = @Facility_Id";
      using (SqlCommand SqlCommand_ImpiloUnitId = new SqlCommand(SQLStringImpiloUnitId))
      {
        SqlCommand_ImpiloUnitId.Parameters.AddWithValue("@Facility_Id", facilityId);
        DataTable DataTable_ImpiloUnitId;
        using (DataTable_ImpiloUnitId = new DataTable())
        {
          DataTable_ImpiloUnitId.Locale = CultureInfo.CurrentCulture;
          DataTable_ImpiloUnitId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ImpiloUnitId).Copy();
          if (DataTable_ImpiloUnitId.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ImpiloUnitId.Rows)
            {
              ImpiloUnitId = DataRow_Row["Facility_ImpiloUnitId"].ToString();
            }
          }
        }
      }

      return ImpiloUnitId;
    }

    public static string DataPatient_ODS_ImpiloCountryId(string facilityId)
    {
      string ImpiloCountryId = "";
      string SQLStringImpiloCountryId = "SELECT Facility_ImpiloCountryId FROM Administration_Facility WHERE Facility_Id = @Facility_Id";
      using (SqlCommand SqlCommand_ImpiloCountryId = new SqlCommand(SQLStringImpiloCountryId))
      {
        SqlCommand_ImpiloCountryId.Parameters.AddWithValue("@Facility_Id", facilityId);
        DataTable DataTable_ImpiloCountryId;
        using (DataTable_ImpiloCountryId = new DataTable())
        {
          DataTable_ImpiloCountryId.Locale = CultureInfo.CurrentCulture;
          DataTable_ImpiloCountryId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ImpiloCountryId).Copy();
          if (DataTable_ImpiloCountryId.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ImpiloCountryId.Rows)
            {
              ImpiloCountryId = DataRow_Row["Facility_ImpiloCountryId"].ToString();
            }
          }
        }
      }

      return ImpiloCountryId;
    }

    public static string DataPatient_ODS_PatientInformationId(string facilityId, string patientVisitNumber)
    {
      string PatientInformationId = "";

      if (string.IsNullOrEmpty(facilityId) || string.IsNullOrEmpty(patientVisitNumber))
      {
        PatientInformationId = "Error: Facility or Visit Number is Empty";
      }
      else
      {
        string LifeCountry = DataPatient_ODS_ImpiloCountryId(facilityId);

        string LifeNumber = "";
        string Name = "";
        string Surname = "";
        string IDNumber = "";
        string DateOfBirth = "";
        string Gender = "";
        string Email = "";
        string ContactNumber = "";

        DataTable DataTable_PatientInformation;
        using (DataTable_PatientInformation = new DataTable())
        {
          DataTable_PatientInformation.Locale = CultureInfo.CurrentCulture;
          DataTable_PatientInformation = InfoQuest_DataPatient.DataPatient_ODS_PatientInformation(facilityId, patientVisitNumber).Copy();
          if (DataTable_PatientInformation.Columns.Count == 1)
          {
            foreach (DataRow DataRow_Row in DataTable_PatientInformation.Rows)
            {
              PatientInformationId = DataRow_Row["Error"].ToString();
            }
          }
          else
          {
            if (DataTable_PatientInformation.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PatientInformation.Rows)
              {
                LifeNumber = DataRow_Row["LifeNumber"].ToString();
                Name = DataRow_Row["Name"].ToString();
                Surname = DataRow_Row["Surname"].ToString();
                IDNumber = DataRow_Row["IDNumber"].ToString();
                DateOfBirth = DataRow_Row["DateOfBirth"].ToString();
                Gender = DataRow_Row["Gender"].ToString();
                Email = DataRow_Row["Email"].ToString();
                ContactNumber = DataRow_Row["ContactNumber"].ToString();
              }
            }
            else
            {
              PatientInformationId = "Error: No Records returned from ODS";
            }
          }
        }

        if (string.IsNullOrEmpty(LifeNumber))
        {
          if (string.IsNullOrEmpty(PatientInformationId))
          {
            PatientInformationId = "Error: Life Number is empty";
          }
        }
        else
        {
          PatientInformationId = "";
          string SQLStringPatientInformationId = "SELECT PatientInformation_Id FROM Lookup_PatientInformation WHERE PatientInformation_LifeCountry = @PatientInformation_LifeCountry AND PatientInformation_LifeNumber = @PatientInformation_LifeNumber";
          using (SqlCommand SqlCommand_PatientInformationId = new SqlCommand(SQLStringPatientInformationId))
          {
            SqlCommand_PatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeCountry", LifeCountry);
            SqlCommand_PatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeNumber", LifeNumber);
            DataTable DataTable_PatientInformationId;
            using (DataTable_PatientInformationId = new DataTable())
            {
              DataTable_PatientInformationId.Locale = CultureInfo.CurrentCulture;
              DataTable_PatientInformationId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInformationId).Copy();
              if (DataTable_PatientInformationId.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_PatientInformationId.Rows)
                {
                  PatientInformationId = DataRow_Row["PatientInformation_Id"].ToString();
                }
              }
            }
          }

          if (string.IsNullOrEmpty(PatientInformationId))
          {
            string SQLStringInsertPatientInformationId = "INSERT INTO Lookup_PatientInformation ( PatientInformation_LifeCountry , PatientInformation_LifeNumber , PatientInformation_Name , PatientInformation_Surname , PatientInformation_IDNumber , PatientInformation_DateOfBirth , PatientInformation_Gender , PatientInformation_Email , PatientInformation_ContactNumber ) VALUES ( @PatientInformation_LifeCountry , @PatientInformation_LifeNumber , @PatientInformation_Name , @PatientInformation_Surname , @PatientInformation_IDNumber , @PatientInformation_DateOfBirth , @PatientInformation_Gender , @PatientInformation_Email , @PatientInformation_ContactNumber ); SELECT SCOPE_IDENTITY()";
            using (SqlCommand SqlCommand_InsertPatientInformationId = new SqlCommand(SQLStringInsertPatientInformationId))
            {
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeCountry", LifeCountry);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeNumber", LifeNumber);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_Name", Name);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_Surname", Surname);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_IDNumber", IDNumber);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_DateOfBirth", DateOfBirth);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_Gender", Gender);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_Email", Email);
              SqlCommand_InsertPatientInformationId.Parameters.AddWithValue("@PatientInformation_ContactNumber", ContactNumber);

              PatientInformationId = InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertPatientInformationId);
            }
          }
          else
          {
            string SQLStringUpdatePatientInformationId = "UPDATE Lookup_PatientInformation SET PatientInformation_Name = @PatientInformation_Name , PatientInformation_Surname = @PatientInformation_Surname , PatientInformation_IDNumber = @PatientInformation_IDNumber , PatientInformation_DateOfBirth = @PatientInformation_DateOfBirth , PatientInformation_Gender = @PatientInformation_Gender , PatientInformation_Email = @PatientInformation_Email , PatientInformation_ContactNumber = @PatientInformation_ContactNumber WHERE PatientInformation_LifeCountry = @PatientInformation_LifeCountry AND PatientInformation_LifeNumber = @PatientInformation_LifeNumber";
            using (SqlCommand SqlCommand_UpdatePatientInformationId = new SqlCommand(SQLStringUpdatePatientInformationId))
            {
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_Name", Name);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_Surname", Surname);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_IDNumber", IDNumber);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_DateOfBirth", DateOfBirth);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_Gender", Gender);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_Email", Email);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_ContactNumber", ContactNumber);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeCountry", LifeCountry);
              SqlCommand_UpdatePatientInformationId.Parameters.AddWithValue("@PatientInformation_LifeNumber", LifeNumber);

              InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdatePatientInformationId);
            }
          }
        }
      }

      return PatientInformationId;
    }

    public static DataTable DataPatient_ODS_PatientInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode = "10"
      //VisitNumber = "429374"
      //LifeNumber = "3611282"
      //Name = "Khanyisa"
      //Surname = "Maluleke"
      //IDNumber = "8109181052086"
      //DateOfBirth = "1981-09-18 00:00:00.000"
      //Gender = "Female"
      //Email = "amalk@za.902uti.com"
      //ContactNumber = "0728231146"

      DataTable DataTable_Information;
      using (DataTable_Information = new DataTable())
      {
        DataTable_Information.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_Information.Reset();
          DataTable_Information.Columns.Add("Error", typeof(string));
          DataTable_Information.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_Information.Reset();
          string FacilityCode = DataPatient_ODS_FacilityCode(facilityId);

          string SQLStringInformation = "EXECUTE ODS_Reports.InfoQuest.PatientInformationSP @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_Information = new SqlCommand(SQLStringInformation))
          {
            SqlCommand_Information.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_Information.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_Information = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_Information).Copy();
          }
        }
      }

      return DataTable_Information;
    }

    public static DataTable DataPatient_ODS_VisitInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode = "10"
      //VisitNumber = "429374"
      //LifeNumber = "3611282"
      //Name = "Khanyisa"
      //Surname = "Maluleke"
      //DateOfBirth = "1981-09-18 00:00:00.000"
      //PatientAge = "31"
      //ContactNumber = "0728231146"
      //Email = "amalk@za.902uti.com"
      //DateOfAdmission = "2013-01-16 05:10:00.000"
      //DateOfDischarge = "2013-01-19 11:24:11.000"
      //Deceased = "0"
      //Ward = "Maternity"
      //Room = "PN3"
      //Bed = "00D"

      DataTable DataTable_Information;
      using (DataTable_Information = new DataTable())
      {
        DataTable_Information.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_Information.Reset();
          DataTable_Information.Columns.Add("Error", typeof(string));
          DataTable_Information.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_Information.Reset();
          string FacilityCode = DataPatient_ODS_FacilityCode(facilityId);

          string SQLStringInformation = "EXECUTE ODS_Reports.InfoQuest.VisitInformationSP @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_Information = new SqlCommand(SQLStringInformation))
          {
            SqlCommand_Information.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_Information.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_Information = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_Information).Copy();
          }
        }
      }

      return DataTable_Information;
    }

    public static DataTable DataPatient_ODS_CodingInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode  VisitNumber CodeType  Code    CodeDescription
      //10	          429374	    CPT	      01968	  Anesthesia for cesarean delivery following neuraxial labor analgesia/anesthesia (List separately in addition to code for primary procedure performed)
      //10	          429374	    CPT	      59514	  Cesarean delivery only;
      //10	          429374	    ICD	      O33.2	  Maternal care for disproportion due to inlet contraction of pelvis
      //10	          429374	    ICD	      O82.9	  Delivery by caesarean section, unspecified
      //10	          429374	    ICD	      Z38.0	  Singleton, born in hospital

      DataTable DataTable_Information;
      using (DataTable_Information = new DataTable())
      {
        DataTable_Information.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_Information.Reset();
          DataTable_Information.Columns.Add("Error", typeof(string));
          DataTable_Information.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_Information.Reset();
          string FacilityCode = DataPatient_ODS_FacilityCode(facilityId);

          string SQLStringInformation = "EXECUTE ODS_Reports.InfoQuest.CodingInformationSP @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_Information = new SqlCommand(SQLStringInformation))
          {
            SqlCommand_Information.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_Information.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_Information = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_Information).Copy();
          }
        }
      }

      return DataTable_Information;
    }

    public static DataTable DataPatient_ODS_AccommodationInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode	VisitNumber	SequenceNumber	Ward	      Room	  Bed	  Date
      //10	          429374	    1	              Maternity	  NULL	  NULL	2013-01-16 05:45:47.000
      //10	          429374	    2	              Maternity	  ANC2	  001	  2013-01-16 05:49:24.000
      //10	          429374	    3	              Maternity	  PN3	    00D	  2013-01-17 01:41:18.000

      DataTable DataTable_Information;
      using (DataTable_Information = new DataTable())
      {
        DataTable_Information.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_Information.Reset();
          DataTable_Information.Columns.Add("Error", typeof(string));
          DataTable_Information.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_Information.Reset();
          string FacilityCode = DataPatient_ODS_FacilityCode(facilityId);

          string SQLStringInformation = "EXECUTE ODS_Reports.InfoQuest.AccomodationInformationSP @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_Information = new SqlCommand(SQLStringInformation))
          {
            SqlCommand_Information.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_Information.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_Information = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_Information).Copy();
          }
        }
      }

      return DataTable_Information;
    }

    public static DataTable DataPatient_ODS_PractitionerInformation(string facilityId, string patientVisitNumber)
    {
      //COLUMNS
      //FacilityCode = "10"
      //VisitNumber = "429374"
      //Practitioner = "Dustan Joseph Kibuka Lumu"

      DataTable DataTable_Information;
      using (DataTable_Information = new DataTable())
      {
        DataTable_Information.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(facilityId) && string.IsNullOrEmpty(patientVisitNumber))
        {
          DataTable_Information.Reset();
          DataTable_Information.Columns.Add("Error", typeof(string));
          DataTable_Information.Rows.Add("Error: Facility or Visit Number is Empty");
        }
        else
        {
          DataTable_Information.Reset();
          string FacilityCode = DataPatient_ODS_FacilityCode(facilityId);

          string SQLStringInformation = "EXECUTE ODS_Reports.InfoQuest.PractitionerInformationSP @FacilityCode , @VisitNumber ";
          using (SqlCommand SqlCommand_Information = new SqlCommand(SQLStringInformation))
          {
            SqlCommand_Information.Parameters.AddWithValue("@FacilityCode", FacilityCode);
            SqlCommand_Information.Parameters.AddWithValue("@VisitNumber", patientVisitNumber);
            DataTable_Information = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_Information).Copy();
          }
        }
      }

      return DataTable_Information;
    }

    public static DataTable DataPatient_ODS_PXM_PostDischargeSurvey(DateTime startDate, DateTime endDate, string facilityId)
    {
      //COLUMNS
      //AdmissionDate = "2013-03-02 11:47:00.000"
      //DischargedDate = "2013-03-12 09:04:39.000"
      //Email = ""
      //Mobile = "0734424246"
      //EventId = "PDCH"
      //PatientFirstname = "Catharina"
      //PatientKnownAs = ""
      //PatientSurname = "Theunissen"
      //PatientTitle = "Mrs"
      //PatientHomePhoneNumber = ""
      //PatientWorkPhoneNumber = ""
      //PatientMobileNumber = "0734424246"
      //PatientDateOfBirth = "1945-01-01 00:00:00.000"
      //PatientAge = "69"
      //EmergencyContactPersonFirstname = ""
      //EmergencyContactPersonSurname = ""
      //EmergencyContactPersonMobileNumber = ""
      //EmergencyContactPersonEmail = ""
      //Relationship = ""
      //Hospital = "Anncron Clinic"
      //HospitalCode = "12"
      //HospitalTypeCode = "057"
      //HospitalType = "Private Hospitals ('A' - Status)"
      //IDNumber = "4501010086083"
      //MedicalFunder = "Polmed"
      //FunderOption = "Higher"
      //PreferredChannel = "ussd"
      //ProcedureCPTCode = "A49.8"
      //DiagnosisICDCode = ""
      //TreatingDoctor = "Charles Henri Oosthuizen"
      //CareType = "Medical"
      //VisitTypeID = "1"
      //VisitType = "In Patient"
      //Lifenumber = "2079207"
      //VisitNumber = "284861"
      //DischargeWard = "C"
      //DepartureTypeId = "1"
      //DepartureType = "Home"
      //DoNotSurvey = "N"
      //UnderAge = "N"

      DataTable DataTable_PostDischargeSurvey;
      using (DataTable_PostDischargeSurvey = new DataTable())
      {
        DataTable_PostDischargeSurvey.Locale = CultureInfo.CurrentCulture;

        if (startDate == null && endDate == null)
        {
          DataTable_PostDischargeSurvey.Reset();
          DataTable_PostDischargeSurvey.Columns.Add("Error", typeof(string));
          DataTable_PostDischargeSurvey.Rows.Add("Error: Start Date or End Date is Empty");
        }
        else
        {
          DataTable_PostDischargeSurvey.Reset();
          if (string.IsNullOrEmpty(facilityId))
          {
            string SQLStringPostDischargeSurvey = "EXECUTE ODS_Reports.InfoQuest.PXMDischargeDataSP @startDate , @endDate , @HospitalUnitId ";
            using (SqlCommand SqlCommand_PostDischargeSurvey = new SqlCommand(SQLStringPostDischargeSurvey))
            {
              SqlCommand_PostDischargeSurvey.Parameters.AddWithValue("@startDate", startDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture));
              SqlCommand_PostDischargeSurvey.Parameters.AddWithValue("@endDate", endDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture));
              SqlCommand_PostDischargeSurvey.Parameters.AddWithValue("@HospitalUnitId", DBNull.Value);
              DataTable_PostDischargeSurvey = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_PostDischargeSurvey).Copy();
            }
          }
          else
          {
            string ImpiloUnitId = DataPatient_ODS_ImpiloUnitId(facilityId);

            string SQLStringPostDischargeSurvey = "EXECUTE ODS_Reports.InfoQuest.PXMDischargeDataSP @startDate , @endDate , @HospitalUnitId ";
            using (SqlCommand SqlCommand_PostDischargeSurvey = new SqlCommand(SQLStringPostDischargeSurvey))
            {
              SqlCommand_PostDischargeSurvey.Parameters.AddWithValue("@startDate", startDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture));
              SqlCommand_PostDischargeSurvey.Parameters.AddWithValue("@endDate", endDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture));
              SqlCommand_PostDischargeSurvey.Parameters.AddWithValue("@HospitalUnitId", ImpiloUnitId);
              DataTable_PostDischargeSurvey = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_PostDischargeSurvey).Copy();
            }
          }
        }
      }

      return DataTable_PostDischargeSurvey;
    }

    public static DataTable DataPatient_ODS_PXM_Event(DateTime startDate, DateTime endDate, string hospitalUnitId, string country)
    {
      //COLUMNS
      //AdmissionDate = "2013-03-02 11:47:00.000"
      //DischargedDate = "2013-03-12 09:04:39.000"
      //Email = ""
      //Mobile = "0734424246"
      //EventId = "PDCH"
      //PatientFirstname = "Catharina"
      //PatientKnownAs = ""
      //PatientSurname = "Theunissen"
      //PatientTitle = "Mrs"
      //PatientMobileNumber = "0734424246"
      //PatientDateOfBirth = "1945-01-01 00:00:00.000"
      //PatientAge = "69"
      //EmergencyContactPersonFirstname = ""
      //EmergencyContactPersonSurname = ""
      //EmergencyContactPersonMobileNumber = ""
      //EmergencyContactPersonEmail = ""
      //Relationship = ""
      //Hospital = "Anncron Clinic"
      //HospitalCode = "12"
      //IDNumber = "4501010086083"
      //MedicalFunder = "Polmed"
      //FunderOption = "Higher"
      //PreferredChannel = "ussd"
      //TreatingDoctor = "Charles Henri Oosthuizen"
      //CareType = "Medical"
      //Lifenumber = "2079207"
      //VisitNumber = "284861"
      //DischargeWard = "C"
      //DepartureTypeId = "1"
      //DepartureType = "Home"
      //UnderAge = "N"

      DataTable DataTable_Event;
      using (DataTable_Event = new DataTable())
      {
        DataTable_Event.Locale = CultureInfo.CurrentCulture;

        if (startDate == null && endDate == null)
        {
          DataTable_Event.Reset();
          DataTable_Event.Columns.Add("Error", typeof(string));
          DataTable_Event.Rows.Add("Error: Start Date or End Date is Empty");
        }
        else
        {
          string SQLStringEvent = "";
          if (country == "ZA")
          {
            SQLStringEvent = "EXECUTE ODS_Reports.InfoQuest.PXMDischargeData_MillwardBrownSP_ZA @StartDate , @EndDate , @HospitalUnitId";
          }
          else if (country == "BW")
          {
            SQLStringEvent = "EXECUTE ODS_Reports.InfoQuest.PXMDischargeData_MillwardBrownSP_BW @StartDate , @EndDate , @HospitalUnitId";
          }

          DataTable_Event.Reset();
          if (string.IsNullOrEmpty(hospitalUnitId))
          {
            using (SqlCommand SqlCommand_Event = new SqlCommand(SQLStringEvent))
            {
              SqlCommand_Event.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture));
              SqlCommand_Event.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture));
              SqlCommand_Event.Parameters.AddWithValue("@HospitalUnitId", DBNull.Value);
              DataTable_Event = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_Event).Copy();
            }
          }
          else
          {
            using (SqlCommand SqlCommand_Event = new SqlCommand(SQLStringEvent))
            {
              SqlCommand_Event.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture));
              SqlCommand_Event.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyyMMdd", CultureInfo.CurrentCulture));
              SqlCommand_Event.Parameters.AddWithValue("@HospitalUnitId", hospitalUnitId);
              DataTable_Event = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_Event).Copy();
            }
          }
        }
      }

      return DataTable_Event;
    }

    public static DataTable DataPatient_ODS_PatientSearch(string facilityId, string patient)
    {
      //COLUMNS
      //AdmissionType = "General Admission
      //FacilityDisplayName = "Wilgeheuwel Hospital (77)
      //VisitNumber = "327891
      //LifeNumber = "2174839
      //Name = "Francois
      //Surname = "Alberts
      //Gender = "Male
      //ContactNumber = "0829690360
      //Email = "dr.franco.alberts@gmail.com
      //DateOfBirth = "1958-12-27 00:00:00.000
      //DateOfAdmission = "2013-04-10 11:12:00.000
      //DateOfDischarge = "2013-04-10 11:19:00.000

      DataTable DataTable_PatientSearch;
      using (DataTable_PatientSearch = new DataTable())
      {
        DataTable_PatientSearch.Locale = CultureInfo.CurrentCulture;

        if (string.IsNullOrEmpty(patient))
        {
          DataTable_PatientSearch.Reset();
          DataTable_PatientSearch.Columns.Add("Error", typeof(string));
          DataTable_PatientSearch.Rows.Add("Error: Patient is Empty");
        }
        else
        {
          DataTable_PatientSearch.Reset();
          if (string.IsNullOrEmpty(facilityId) || facilityId == "All")
          {
            string SQLStringPatientSearch = "EXECUTE ODS_Reports.InfoQuest.PatientSearchSP @FacilityCode , @Patient ";
            using (SqlCommand SqlCommand_PatientSearch = new SqlCommand(SQLStringPatientSearch))
            {
              SqlCommand_PatientSearch.Parameters.AddWithValue("@FacilityCode", DBNull.Value);
              SqlCommand_PatientSearch.Parameters.AddWithValue("@Patient", patient);
              DataTable_PatientSearch = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_PatientSearch).Copy();
            }
          }
          else
          {
            string FacilityCode = DataPatient_ODS_FacilityCode(facilityId);

            string SQLStringPatientSearch = "EXECUTE ODS_Reports.InfoQuest.PatientSearchSP @FacilityCode , @Patient ";
            using (SqlCommand SqlCommand_PatientSearch = new SqlCommand(SQLStringPatientSearch))
            {
              SqlCommand_PatientSearch.Parameters.AddWithValue("@FacilityCode", FacilityCode);
              SqlCommand_PatientSearch.Parameters.AddWithValue("@Patient", patient);
              DataTable_PatientSearch = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_PatientSearch).Copy();
            }
          }
        }
      }

      return DataTable_PatientSearch;
    }


    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_Hospital(string organisation, string facilityNamedropDown, string facilityNameTextBox, string type, string country, string province, string subRegion, string town)
    {
      //COLUMNS
      //Hospital_Organisation = "Government"
      //Hospital_Type = "Hospitals - Military"
      //Hospital_FacilityName = "1 Military Hospital"
      //Hospital_PhysicalAddress = "Steve Biko Street, Thaba Tshwane, 0187"
      //Hospital_PostalAddress = "Private Bag X1026, Thaba Tshwane, 0143"
      //Hospital_ContactNumber1 = "(012) 314 0999"
      //Hospital_ContactNumber2 = "(012) 314 0112"
      //Hospital_ContactNumber3 = "(012) 314 0897"
      //Hospital_ContactNumber4 = "(012) 314 0461"
      //Hospital_EmergencyContactNumber1 = ""
      //Hospital_EmergencyContactNumber2 = "(012) 314 0271"
      //Hospital_FaxNumber1 = "(012) 314 0757"
      //Hospital_FaxNumber2 = ""
      //Hospital_Email = ""
      //Hospital_URL = ""
      //Hospital_Country = "South Africa"
      //Hospital_Region = "Gauteng"
      //Hospital_SubRegion = "Pretoria and Surrounds"
      //Hospital_Town = "Centurion"
      //Hospital_Latitude = "-25.7822740"
      //Hospital_Longitude = "28.1512850"
      //Hospital_Title = "1 Military Hospital - Steve Biko Street, Thaba Tshwane, 0187"
      //Hospital_Content = "<div style="width:300px;">1 Military Hospital - Steve Biko Street, Thaba Tshwane, 0187 - <a href="" target="_blank"></a> - <a href="emailto:" target="_top"></a></div>"
      //Hospital_Icon = "App_Images/Icons/letter_g_yellow.png"
      //Hospital_IconDescription = "Government"

      DataTable DataTable_ExecutiveMarketInquiry_Hospital;
      using (DataTable_ExecutiveMarketInquiry_Hospital = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_Hospital.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_Hospital.Reset();
        string SQLStringExecutiveMarketInquiry_Hospital = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_Grid_HospitalSP @Organisation , @FacilityName_DropDown , @FacilityName_TextBox , @Type , @Country , @Region , @SubRegion , @Town ";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_Hospital = new SqlCommand(SQLStringExecutiveMarketInquiry_Hospital))
        {
          SqlCommand_ExecutiveMarketInquiry_Hospital.Parameters.AddWithValue("@Organisation", organisation);
          SqlCommand_ExecutiveMarketInquiry_Hospital.Parameters.AddWithValue("@FacilityName_DropDown", facilityNamedropDown);
          SqlCommand_ExecutiveMarketInquiry_Hospital.Parameters.AddWithValue("@FacilityName_TextBox", facilityNameTextBox);
          SqlCommand_ExecutiveMarketInquiry_Hospital.Parameters.AddWithValue("@Type", type);
          SqlCommand_ExecutiveMarketInquiry_Hospital.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_Hospital.Parameters.AddWithValue("@Region", province);
          SqlCommand_ExecutiveMarketInquiry_Hospital.Parameters.AddWithValue("@SubRegion", subRegion);
          SqlCommand_ExecutiveMarketInquiry_Hospital.Parameters.AddWithValue("@Town", town);
          DataTable_ExecutiveMarketInquiry_Hospital = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_Hospital).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_Hospital;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_Doctor(string organisation, string doctorNamedropDown, string doctorNameTextBox, string type, string country, string province, string subRegion, string town)
    {
      //COLUMNS
      //Doctor_Organisation = "Life Healthcare"
      //Doctor_Type = "General Practitioner"
      //Doctor_FacilityName = "Dr Aadil Bodhania"
      //Doctor_PhysicalAddress = "6 Hull Street, Florida, 1709"
      //Doctor_PostalAddress = "P O Box 10000, Azaadville, 1750"
      //Doctor_ContactNumber1 = "(011) 672 7111"
      //Doctor_ContactNumber2 = ""
      //Doctor_ContactNumber3 = ""
      //Doctor_ContactNumber4 = ""
      //Doctor_EmergencyContactNumber1 = "(083) 462 9000"
      //Doctor_EmergencyContactNumber2 = ""
      //Doctor_FaxNumber1 = "(011) 672 7140"
      //Doctor_FaxNumber2 = ""
      //Doctor_Email = "aadil@gpnet.net"
      //Doctor_URL = ""
      //Doctor_Country = "South Africa"
      //Doctor_Region = "Gauteng"
      //Doctor_SubRegion = "West Rand"
      //Doctor_Town = "Roodepoort"
      //Doctor_Latitude = "-26.1738600"
      //Doctor_Longitude = "27.9064900"
      //Doctor_Title = "Dr Aadil Bodhania - 6 Hull Street, Florida, 1709"
      //Doctor_Content = "<div style="width:300px;">Dr Aadil Bodhania - 6 Hull Street, Florida, 1709 - <a href="" target="_blank"></a> - <a href="emailto:aadil@gpnet.net" target="_top">aadil@gpnet.net</a></div>"
      //Doctor_Icon = "App_Images/Icons/medicine_doctor_green.png"
      //Doctor_IconDescription = "Life Healthcare"

      DataTable DataTable_ExecutiveMarketInquiry_Doctor;
      using (DataTable_ExecutiveMarketInquiry_Doctor = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_Doctor.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_Doctor.Reset();
        string SQLStringExecutiveMarketInquiry_Doctor = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_Grid_DoctorSP @Organisation , @DoctorName_DropDown , @DoctorName_TextBox , @Type , @Country , @Region , @SubRegion , @Town ";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_Doctor = new SqlCommand(SQLStringExecutiveMarketInquiry_Doctor))
        {
          SqlCommand_ExecutiveMarketInquiry_Doctor.Parameters.AddWithValue("@Organisation", organisation);
          SqlCommand_ExecutiveMarketInquiry_Doctor.Parameters.AddWithValue("@DoctorName_DropDown", doctorNamedropDown);
          SqlCommand_ExecutiveMarketInquiry_Doctor.Parameters.AddWithValue("@DoctorName_TextBox", doctorNameTextBox);
          SqlCommand_ExecutiveMarketInquiry_Doctor.Parameters.AddWithValue("@Type", type);
          SqlCommand_ExecutiveMarketInquiry_Doctor.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_Doctor.Parameters.AddWithValue("@Region", province);
          SqlCommand_ExecutiveMarketInquiry_Doctor.Parameters.AddWithValue("@SubRegion", subRegion);
          SqlCommand_ExecutiveMarketInquiry_Doctor.Parameters.AddWithValue("@Town", town);
          DataTable_ExecutiveMarketInquiry_Doctor = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_Doctor).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_Doctor;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_Population(string country, string province, string municipality)
    {
      //COLUMNS
      //Population_Name = "South Africa"
      //Population_Value = "51770561"
      //Population_Level = "1"
      //Population_Type = "Country"
      //Population_Year = "2011"
      //Population_Address = "South Africa"
      //Population_Latitude = "-30.559482"
      //Population_Longitude = "22.937506"
      //Population_Title = "South Africa - South Africa"
      //Population_Content = "<div style="width:300px;">South Africa - South Africa - Population: 51770561</div>"
      //Population_Icon = "App_Images/Icons/group-2_population_red.png"
      //Population_IconDescription = "Population"
      //Population_RadiusValue = "51770561"
      //Population_RadiusColor = "#B0262E"

      DataTable DataTable_ExecutiveMarketInquiry_Population;
      using (DataTable_ExecutiveMarketInquiry_Population = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_Population.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_Population.Reset();
        string SQLStringExecutiveMarketInquiry_Population = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_Grid_PopulationSP @Country , @Province , @Municipality ";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_Population = new SqlCommand(SQLStringExecutiveMarketInquiry_Population))
        {
          SqlCommand_ExecutiveMarketInquiry_Population.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_Population.Parameters.AddWithValue("@Province", province);
          SqlCommand_ExecutiveMarketInquiry_Population.Parameters.AddWithValue("@Municipality", municipality);
          DataTable_ExecutiveMarketInquiry_Population = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_Population).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_Population;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_MedicalScheme(string country, string province)
    {
      //COLUMNS 
      //MedicalScheme_Name = "South Africa"
      //MedicalScheme_Value = "9732000"
      //MedicalScheme_Level = "1"
      //MedicalScheme_Type = "Country"
      //MedicalScheme_Year = "2013"
      //MedicalScheme_Address = "South Africa"
      //MedicalScheme_Latitude = "-30.559482"
      //MedicalScheme_Longitude = "22.937506"
      //MedicalScheme_Title = "South Africa - South Africa"
      //MedicalScheme_Content = "<div style="width:300px;">South Africa - South Africa - Medical Scheme: 9732000</div>"
      //MedicalScheme_Icon = "App_Images/Icons/hospital-building_medicalscheme_blue.png"
      //MedicalScheme_IconDescription = "Medical Scheme"
      //MedicalScheme_RadiusValue = "9732000"
      //MedicalScheme_RadiusColor = "#003768"

      DataTable DataTable_ExecutiveMarketInquiry_MedicalScheme;
      using (DataTable_ExecutiveMarketInquiry_MedicalScheme = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_MedicalScheme.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_MedicalScheme.Reset();
        string SQLStringExecutiveMarketInquiry_MedicalScheme = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_Grid_MedicalSchemeSP @Country , @Province ";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_MedicalScheme = new SqlCommand(SQLStringExecutiveMarketInquiry_MedicalScheme))
        {
          SqlCommand_ExecutiveMarketInquiry_MedicalScheme.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_MedicalScheme.Parameters.AddWithValue("@Province", province);
          DataTable_ExecutiveMarketInquiry_MedicalScheme = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_MedicalScheme).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_MedicalScheme;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_AllMapType()
    {
      //COLUMNS 
      //MapType = "Doctor"

      DataTable DataTable_ExecutiveMarketInquiry_List_AllMapType;
      using (DataTable_ExecutiveMarketInquiry_List_AllMapType = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_AllMapType.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_AllMapType.Reset();
        string SQLStringExecutiveMarketInquiry_List_AllMapType = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_AllMapTypeSP";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_AllMapType = new SqlCommand(SQLStringExecutiveMarketInquiry_List_AllMapType))
        {
          DataTable_ExecutiveMarketInquiry_List_AllMapType = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_AllMapType).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_AllMapType;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_DoctorMapType()
    {
      //COLUMNS 
      //MapType = "Doctor"

      DataTable DataTable_ExecutiveMarketInquiry_List_DoctorMapType;
      using (DataTable_ExecutiveMarketInquiry_List_DoctorMapType = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_DoctorMapType.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_DoctorMapType.Reset();
        string SQLStringExecutiveMarketInquiry_List_DoctorMapType = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_DoctorMapTypeSP";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_DoctorMapType = new SqlCommand(SQLStringExecutiveMarketInquiry_List_DoctorMapType))
        {
          DataTable_ExecutiveMarketInquiry_List_DoctorMapType = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_DoctorMapType).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_DoctorMapType;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_HospitalMapType()
    {
      //COLUMNS 
      //MapType = "Hospital"

      DataTable DataTable_ExecutiveMarketInquiry_List_HospitalMapType;
      using (DataTable_ExecutiveMarketInquiry_List_HospitalMapType = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_HospitalMapType.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_HospitalMapType.Reset();
        string SQLStringExecutiveMarketInquiry_List_HospitalMapType = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_HospitalMapTypeSP";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_HospitalMapType = new SqlCommand(SQLStringExecutiveMarketInquiry_List_HospitalMapType))
        {
          DataTable_ExecutiveMarketInquiry_List_HospitalMapType = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_HospitalMapType).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_HospitalMapType;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_AllCountry()
    {
      //COLUMNS 
      //Country = "South Africa"

      DataTable DataTable_ExecutiveMarketInquiry_List_AllCountry;
      using (DataTable_ExecutiveMarketInquiry_List_AllCountry = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_AllCountry.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_AllCountry.Reset();
        string SQLStringExecutiveMarketInquiry_List_AllCountry = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_AllCountrySP";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_AllCountry = new SqlCommand(SQLStringExecutiveMarketInquiry_List_AllCountry))
        {
          DataTable_ExecutiveMarketInquiry_List_AllCountry = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_AllCountry).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_AllCountry;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_AllProvince(string country)
    {
      //COLUMNS 
      //Province = "Eastern Cape"

      DataTable DataTable_ExecutiveMarketInquiry_List_AllProvince;
      using (DataTable_ExecutiveMarketInquiry_List_AllProvince = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_AllProvince.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_AllProvince.Reset();
        string SQLStringExecutiveMarketInquiry_List_AllProvince = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_AllProvinceSP @Country ";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_AllProvince = new SqlCommand(SQLStringExecutiveMarketInquiry_List_AllProvince))
        {
          SqlCommand_ExecutiveMarketInquiry_List_AllProvince.Parameters.AddWithValue("@Country", country);
          DataTable_ExecutiveMarketInquiry_List_AllProvince = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_AllProvince).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_AllProvince;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_PopulationMunicipality(string country, string province)
    {
      //COLUMNS 
      //Municipality = "Alfred Nzo"

      DataTable DataTable_ExecutiveMarketInquiry_List_PopulationMunicipality;
      using (DataTable_ExecutiveMarketInquiry_List_PopulationMunicipality = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_PopulationMunicipality.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_PopulationMunicipality.Reset();
        string SQLStringExecutiveMarketInquiry_List_PopulationMunicipality = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_PopulationMunicipalitySP @Country , @Province";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_PopulationMunicipality = new SqlCommand(SQLStringExecutiveMarketInquiry_List_PopulationMunicipality))
        {
          SqlCommand_ExecutiveMarketInquiry_List_PopulationMunicipality.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_List_PopulationMunicipality.Parameters.AddWithValue("@Province", province);
          DataTable_ExecutiveMarketInquiry_List_PopulationMunicipality = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_PopulationMunicipality).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_PopulationMunicipality;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_DoctorSubRegion(string country, string province)
    {
      //COLUMNS 
      //SubRegion = "Boland"

      DataTable DataTable_ExecutiveMarketInquiry_List_DoctorSubRegion;
      using (DataTable_ExecutiveMarketInquiry_List_DoctorSubRegion = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_DoctorSubRegion.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_DoctorSubRegion.Reset();
        string SQLStringExecutiveMarketInquiry_List_DoctorSubRegion = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_DoctorSubRegionSP @Country , @Province";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_DoctorSubRegion = new SqlCommand(SQLStringExecutiveMarketInquiry_List_DoctorSubRegion))
        {
          SqlCommand_ExecutiveMarketInquiry_List_DoctorSubRegion.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorSubRegion.Parameters.AddWithValue("@Province", province);
          DataTable_ExecutiveMarketInquiry_List_DoctorSubRegion = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_DoctorSubRegion).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_DoctorSubRegion;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_DoctorTown(string country, string province, string subRegion)
    {
      //COLUMNS 
      //Town = "Abaqulusi Rural"

      DataTable DataTable_ExecutiveMarketInquiry_List_DoctorTown;
      using (DataTable_ExecutiveMarketInquiry_List_DoctorTown = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_DoctorTown.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_DoctorTown.Reset();
        string SQLStringExecutiveMarketInquiry_List_DoctorTown = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_DoctorTownSP @Country , @Province , @SubRegion";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_DoctorTown = new SqlCommand(SQLStringExecutiveMarketInquiry_List_DoctorTown))
        {
          SqlCommand_ExecutiveMarketInquiry_List_DoctorTown.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorTown.Parameters.AddWithValue("@Province", province);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorTown.Parameters.AddWithValue("@SubRegion", subRegion);
          DataTable_ExecutiveMarketInquiry_List_DoctorTown = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_DoctorTown).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_DoctorTown;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_DoctorOrganisation(string country, string province, string subRegion, string town)
    {
      //COLUMNS 
      //Organisation = "Life Healthcare"

      DataTable DataTable_ExecutiveMarketInquiry_List_DoctorOrganisation;
      using (DataTable_ExecutiveMarketInquiry_List_DoctorOrganisation = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_DoctorOrganisation.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_DoctorOrganisation.Reset();
        string SQLStringExecutiveMarketInquiry_List_DoctorOrganisation = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_DoctorOrganisationSP @Country , @Province , @SubRegion , @Town";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_DoctorOrganisation = new SqlCommand(SQLStringExecutiveMarketInquiry_List_DoctorOrganisation))
        {
          SqlCommand_ExecutiveMarketInquiry_List_DoctorOrganisation.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorOrganisation.Parameters.AddWithValue("@Province", province);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorOrganisation.Parameters.AddWithValue("@SubRegion", subRegion);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorOrganisation.Parameters.AddWithValue("@Town", town);
          DataTable_ExecutiveMarketInquiry_List_DoctorOrganisation = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_DoctorOrganisation).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_DoctorOrganisation;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_DoctorType(string country, string province, string subRegion, string town, string organisation)
    {
      //COLUMNS 
      //Type = "Allergologist"

      DataTable DataTable_ExecutiveMarketInquiry_List_DoctorType;
      using (DataTable_ExecutiveMarketInquiry_List_DoctorType = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_DoctorType.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_DoctorType.Reset();
        string SQLStringExecutiveMarketInquiry_List_DoctorType = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_DoctorTypeSP @Country , @Province , @SubRegion , @Town , @Organisation";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_DoctorType = new SqlCommand(SQLStringExecutiveMarketInquiry_List_DoctorType))
        {
          SqlCommand_ExecutiveMarketInquiry_List_DoctorType.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorType.Parameters.AddWithValue("@Province", province);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorType.Parameters.AddWithValue("@SubRegion", subRegion);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorType.Parameters.AddWithValue("@Town", town);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorType.Parameters.AddWithValue("@Organisation", organisation);
          DataTable_ExecutiveMarketInquiry_List_DoctorType = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_DoctorType).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_DoctorType;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_DoctorName(string country, string province, string subRegion, string town, string organisation, string type)
    {
      //COLUMNS 
      //Name = "Dr Aadil Bodhania"

      DataTable DataTable_ExecutiveMarketInquiry_List_DoctorName;
      using (DataTable_ExecutiveMarketInquiry_List_DoctorName = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_DoctorName.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_DoctorName.Reset();
        string SQLStringExecutiveMarketInquiry_List_DoctorName = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_DoctorNameSP @Country , @Province , @SubRegion , @Town , @Organisation , @Type";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_DoctorName = new SqlCommand(SQLStringExecutiveMarketInquiry_List_DoctorName))
        {
          SqlCommand_ExecutiveMarketInquiry_List_DoctorName.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorName.Parameters.AddWithValue("@Province", province);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorName.Parameters.AddWithValue("@SubRegion", subRegion);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorName.Parameters.AddWithValue("@Town", town);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorName.Parameters.AddWithValue("@Organisation", organisation);
          SqlCommand_ExecutiveMarketInquiry_List_DoctorName.Parameters.AddWithValue("@Type", type);
          DataTable_ExecutiveMarketInquiry_List_DoctorName = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_DoctorName).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_DoctorName;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_HospitalSubRegion(string country, string province)
    {
      //COLUMNS 
      //SubRegion = "Boland"

      DataTable DataTable_ExecutiveMarketInquiry_List_HospitalSubRegion;
      using (DataTable_ExecutiveMarketInquiry_List_HospitalSubRegion = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_HospitalSubRegion.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_HospitalSubRegion.Reset();
        string SQLStringExecutiveMarketInquiry_List_HospitalSubRegion = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_HospitalSubRegionSP @Country , @Province";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_HospitalSubRegion = new SqlCommand(SQLStringExecutiveMarketInquiry_List_HospitalSubRegion))
        {
          SqlCommand_ExecutiveMarketInquiry_List_HospitalSubRegion.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalSubRegion.Parameters.AddWithValue("@Province", province);
          DataTable_ExecutiveMarketInquiry_List_HospitalSubRegion = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_HospitalSubRegion).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_HospitalSubRegion;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_HospitalTown(string country, string province, string subRegion)
    {
      //COLUMNS 
      //Town = "Aberdeen"

      DataTable DataTable_ExecutiveMarketInquiry_List_HospitalTown;
      using (DataTable_ExecutiveMarketInquiry_List_HospitalTown = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_HospitalTown.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_HospitalTown.Reset();
        string SQLStringExecutiveMarketInquiry_List_HospitalTown = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_HospitalTownSP @Country , @Province , @SubRegion";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_HospitalTown = new SqlCommand(SQLStringExecutiveMarketInquiry_List_HospitalTown))
        {
          SqlCommand_ExecutiveMarketInquiry_List_HospitalTown.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalTown.Parameters.AddWithValue("@Province", province);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalTown.Parameters.AddWithValue("@SubRegion", subRegion);
          DataTable_ExecutiveMarketInquiry_List_HospitalTown = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_HospitalTown).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_HospitalTown;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_HospitalOrganisation(string country, string province, string subRegion, string town)
    {
      //COLUMNS 
      //Organisation = "Clinix"

      DataTable DataTable_ExecutiveMarketInquiry_List_HospitalOrganisation;
      using (DataTable_ExecutiveMarketInquiry_List_HospitalOrganisation = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_HospitalOrganisation.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_HospitalOrganisation.Reset();
        string SQLStringExecutiveMarketInquiry_List_HospitalOrganisation = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_HospitalOrganisationSP @Country , @Province , @SubRegion , @Town";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_HospitalOrganisation = new SqlCommand(SQLStringExecutiveMarketInquiry_List_HospitalOrganisation))
        {
          SqlCommand_ExecutiveMarketInquiry_List_HospitalOrganisation.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalOrganisation.Parameters.AddWithValue("@Province", province);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalOrganisation.Parameters.AddWithValue("@SubRegion", subRegion);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalOrganisation.Parameters.AddWithValue("@Town", town);
          DataTable_ExecutiveMarketInquiry_List_HospitalOrganisation = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_HospitalOrganisation).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_HospitalOrganisation;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_HospitalType(string country, string province, string subRegion, string town, string organisation)
    {
      //COLUMNS 
      //Type = "Hospitals - Military"

      DataTable DataTable_ExecutiveMarketInquiry_List_HospitalType;
      using (DataTable_ExecutiveMarketInquiry_List_HospitalType = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_HospitalType.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_HospitalType.Reset();
        string SQLStringExecutiveMarketInquiry_List_HospitalType = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_HospitalTypeSP @Country , @Province , @SubRegion , @Town , @Organisation";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_HospitalType = new SqlCommand(SQLStringExecutiveMarketInquiry_List_HospitalType))
        {
          SqlCommand_ExecutiveMarketInquiry_List_HospitalType.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalType.Parameters.AddWithValue("@Province", province);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalType.Parameters.AddWithValue("@SubRegion", subRegion);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalType.Parameters.AddWithValue("@Town", town);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalType.Parameters.AddWithValue("@Organisation", organisation);
          DataTable_ExecutiveMarketInquiry_List_HospitalType = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_HospitalType).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_HospitalType;
    }

    public static DataTable DataPatient_ODS_ExecutiveMarketInquiry_List_HospitalName(string country, string province, string subRegion, string town, string organisation, string type)
    {
      //COLUMNS 
      //Name = "1 Military Hospital"

      DataTable DataTable_ExecutiveMarketInquiry_List_HospitalName;
      using (DataTable_ExecutiveMarketInquiry_List_HospitalName = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_List_HospitalName.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_List_HospitalName.Reset();
        string SQLStringExecutiveMarketInquiry_List_HospitalName = "EXECUTE ODS_Reports.InfoQuest.ExecutiveMarketInquiry_List_HospitalNameSP @Country , @Province , @SubRegion , @Town , @Organisation , @Type";
        using (SqlCommand SqlCommand_ExecutiveMarketInquiry_List_HospitalName = new SqlCommand(SQLStringExecutiveMarketInquiry_List_HospitalName))
        {
          SqlCommand_ExecutiveMarketInquiry_List_HospitalName.Parameters.AddWithValue("@Country", country);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalName.Parameters.AddWithValue("@Province", province);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalName.Parameters.AddWithValue("@SubRegion", subRegion);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalName.Parameters.AddWithValue("@Town", town);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalName.Parameters.AddWithValue("@Organisation", organisation);
          SqlCommand_ExecutiveMarketInquiry_List_HospitalName.Parameters.AddWithValue("@Type", type);
          DataTable_ExecutiveMarketInquiry_List_HospitalName = InfoQuest_DataPatient.DataPatient_ODS_SqlGetData(SqlCommand_ExecutiveMarketInquiry_List_HospitalName).Copy();
        }
      }

      return DataTable_ExecutiveMarketInquiry_List_HospitalName;
    }


    public static DataTable DataPatient_Medoc_SqlGetData(SqlCommand sqlCommand_SqlString)
    {
      using (DataTable DataTable_GetData = new DataTable())
      {
        DataTable_GetData.Locale = CultureInfo.CurrentCulture;
        string ConnectionStringGetData = InfoQuest_Connections.Connections("PatientDetailMedoc");

        if (string.IsNullOrEmpty(ConnectionStringGetData))
        {
          DataTable_GetData.Reset();
          DataTable_GetData.Columns.Add("Error", typeof(string));
          DataTable_GetData.Rows.Add("Error: No Medoc Connection String");
        }
        else
        {
          DataTable_GetData.Reset();
          using (SqlConnection SQLConnection_GetData = new SqlConnection(ConnectionStringGetData))
          {
            using (SqlDataAdapter SqlDataAdapter_GetData = new SqlDataAdapter())
            {
              try
              {
                if (sqlCommand_SqlString != null)
                {
                  foreach (SqlParameter SqlParameter_Value in sqlCommand_SqlString.Parameters)
                  {
                    if (SqlParameter_Value.Value == null)
                    {
                      SqlParameter_Value.Value = DBNull.Value;
                    }
                  }

                  sqlCommand_SqlString.CommandType = CommandType.Text;
                  sqlCommand_SqlString.Connection = SQLConnection_GetData;
                  sqlCommand_SqlString.CommandTimeout = 600;
                  SQLConnection_GetData.Open();
                  SqlDataAdapter_GetData.SelectCommand = sqlCommand_SqlString;
                  SqlDataAdapter_GetData.Fill(DataTable_GetData);
                }
              }
              catch (Exception Exception_Error)
              {
                if (!string.IsNullOrEmpty(Exception_Error.ToString()))
                {
                  DataTable_GetData.Reset();
                  DataTable_GetData.Columns.Add("Error", typeof(string));
                  DataTable_GetData.Rows.Add("Error: Data could not be retrieved from Medoc");
                }
                else
                {
                  throw;
                }
              }
            }
          }
        }

        return DataTable_GetData;
      }
    }

    public static DataTable DataPatient_Medoc_CompanySearch(string company)
    {
      //COLUMNS 
      //Company_Id = "1"
      //Company_Code = "001"
      //Company_Description = "Boumat"
      //CompanySite_Id = "294"
      //CompanySite_Code = "G0010"
      //CompanySite_Description = "Automotive Leather Company – Rosslyn"

      DataTable DataTable_CompanySearch;
      using (DataTable_CompanySearch = new DataTable())
      {
        DataTable_CompanySearch.Locale = CultureInfo.CurrentCulture;
        DataTable_CompanySearch.Reset();
        string SQLStringCompanySearch = @"SELECT		COMPANY_TB.ID AS Company_Id ,
					                                          COMPANY_TB.COMPANY_CODE AS Company_Code ,
					                                          COMPANY_TB.COMPANY_DESC AS Company_Description ,
					                                          COMPANYSITE_TB.ID AS CompanySite_Id ,
					                                          COMPANYSITE_TB.SITE_CODE AS CompanySite_Code ,
					                                          COMPANYSITE_TB.SITE_DESC AS CompanySite_Description
                                          FROM			COMPANY_TB
					                                          LEFT JOIN COMPANYSITE_TB ON COMPANY_TB.ID = COMPANYSITE_TB.ID_COMPANY
                                          WHERE			COMPANY_TB.DELETED = 0
					                                          AND COMPANYSITE_TB.DELETED = 0
					                                          AND (
						                                          (CHARINDEX(@Company , COMPANY_TB.COMPANY_DESC) > 0)
						                                          OR (CHARINDEX(@Company , COMPANYSITE_TB.SITE_DESC) > 0)
					                                          )
                                          ORDER BY	COMPANY_TB.COMPANY_DESC ,
					                                          COMPANYSITE_TB.SITE_DESC";
        using (SqlCommand SqlCommand_CompanySearch = new SqlCommand(SQLStringCompanySearch))
        {
          SqlCommand_CompanySearch.Parameters.AddWithValue("@Company", company);
          DataTable_CompanySearch = InfoQuest_DataPatient.DataPatient_Medoc_SqlGetData(SqlCommand_CompanySearch).Copy();
        }
      }

      return DataTable_CompanySearch;
    }
  }
}