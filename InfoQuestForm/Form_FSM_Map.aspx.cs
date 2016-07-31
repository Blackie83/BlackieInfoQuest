using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_FSM_Map : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("41").Replace(" Form", "")).ToString() + " : Map", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search Map", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("Map", CultureInfo.CurrentCulture);

          SetFormQueryString();

          RegisterPostBackControl();

          LoadMap();
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
        ((Label)PageUpdateProgress_FSM_Map.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Facility Structure Maintenance", "15");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_FSM_Location_Name.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_Location_Name.SelectCommand = "SELECT LocationKey , LocationName FROM BusinessUnit.Location ORDER BY LocationName";

      SqlDataSource_FSM_Location_Country.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_Location_Country.SelectCommand = "SELECT CountryKey , CountryName FROM Geographic.Country ORDER BY CountryName";

      SqlDataSource_FSM_Location_Province.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_Location_Province.SelectCommand = "SELECT ProvinceKey , ProvinceName FROM Geographic.Province WHERE CountryKey = @CountryKey ORDER BY ProvinceName";
      SqlDataSource_FSM_Location_Province.SelectParameters.Clear();
      SqlDataSource_FSM_Location_Province.SelectParameters.Add("CountryKey", TypeCode.String, "");
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Name.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Location"] == null)
        {
          DropDownList_Name.SelectedValue = "";
        }
        else
        {
          DropDownList_Name.SelectedValue = Request.QueryString["s_Location"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Country.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Country"] == null)
        {
          DropDownList_Country.SelectedValue = "";
        }
        else
        {
          DropDownList_Country.SelectedValue = Request.QueryString["s_Country"];

          DropDownList_Province.Items.Clear();
          SqlDataSource_FSM_Location_Province.SelectParameters["CountryKey"].DefaultValue = Request.QueryString["s_Country"];
          DropDownList_Province.Items.Insert(0, new ListItem(Convert.ToString("Select Province", CultureInfo.CurrentCulture), ""));
          DropDownList_Province.DataBind();
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Province.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Province"] == null)
        {
          DropDownList_Province.SelectedValue = "";
        }
        else
        {
          DropDownList_Province.SelectedValue = Request.QueryString["s_Province"];
        }
      }
    }

    private void LoadMap()
    {
      string Locations = "";
      Int32 LocationCount = 0;
      string Title = "";
      string Content = "";

      string LocationName = "";
      string LocationAddress = "";
      string Latitude = "";
      string Longitude = "";
      string UseAddress = "";
      string SQLStringLocation = @"SELECT REPLACE(LocationName, '''' , '') AS LocationName , REPLACE(LocationAddress, '''' , '') AS LocationAddress , Latitude , Longitude , CASE WHEN Latitude IS NULL OR Longitude IS NULL THEN 'Yes' ELSE 'No' END AS UseAddress
                                   FROM BusinessUnit.Location LEFT JOIN Geographic.Province ON Location.ProvinceKey = Province.ProvinceKey LEFT JOIN Geographic.Country ON Province.CountryKey = Country.CountryKey 
                                   WHERE IsActive = 1 AND LocationAddress IS NOT NULL AND Latitude IS NOT NULL AND Longitude IS NOT NULL AND (Location.LocationKey = @Location OR @Location IS NULL) AND (Country.CountryKey = @Country OR @Country IS NULL) AND (Province.ProvinceKey = @Province OR @Province IS NULL)";
      using (SqlCommand SqlCommand_Location = new SqlCommand(SQLStringLocation))
      {
        SqlCommand_Location.Parameters.AddWithValue("@Location", Request.QueryString["s_Location"]);
        SqlCommand_Location.Parameters.AddWithValue("@Country", Request.QueryString["s_Country"]);
        SqlCommand_Location.Parameters.AddWithValue("@Province", Request.QueryString["s_Province"]);
        DataTable DataTable_Location;
        using (DataTable_Location = new DataTable())
        {
          DataTable_Location.Locale = CultureInfo.CurrentCulture;
          DataTable_Location = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_Location, "PatientDetailFacilityStructure").Copy();
          if (DataTable_Location.Rows.Count > 0)
          {
            LocationCount = DataTable_Location.Rows.Count;

            foreach (DataRow DataRow_Row in DataTable_Location.Rows)
            {
              LocationName = DataRow_Row["LocationName"].ToString();
              LocationAddress = DataRow_Row["LocationAddress"].ToString();
              Latitude = DataRow_Row["Latitude"].ToString();
              Longitude = DataRow_Row["Longitude"].ToString();
              UseAddress = DataRow_Row["UseAddress"].ToString();

              Title = "" + LocationName + " - " + LocationAddress + "";
              Content = "<div style=\"width:200px;\">" + LocationName + " - " + LocationAddress + " - <a href=\"http://www.google.com\" target=\"_blank\">Google</a></div>";

              Locations += Environment.NewLine + @"[" + Latitude + " , " + Longitude + " , '" + LocationAddress + "' , '" + Title + "' , '" + Content + "' , '" + UseAddress + "'],";
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Locations))
      {
        Locations = Locations.Remove(Locations.Length - 1, 1);
      }

      Literal_JavaScript.Text = Convert.ToString(@"
        <script src='http://maps.googleapis.com/maps/api/js?key=AIzaSyDX4jsTkgDQ3Cl-xIDga768eOTFyd73MzM&sensor=false'></script>

        <script>
          var geocoder;
          var map;
          var locationdata = [
            " + Locations + @"
          ];

          function initialize()
          {
            geocoder = new google.maps.Geocoder();

            var mapoptions = {
              panControl:true,
              zoomControl:true,
              mapTypeControl:true,
              scaleControl:true,
              streetViewControl:true,
              overviewMapControl:true,
              rotateControl:true,
              mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            map = new google.maps.Map(document.getElementById('googleMap'), mapoptions);
            var bounds = new google.maps.LatLngBounds();
            var infowindow = new google.maps.InfoWindow();
            var yesaddressexist = 'No';

            for (var i in locationdata)
            {
              var location = locationdata[i];

              if (location[5] == 'Yes')
              {
                yesaddressexist = 'Yes';
              }
            }

            for (var i in locationdata)
            {
              var location = locationdata[i];

              if (location[5] == 'No')
              {
                var latlng = new google.maps.LatLng(location[0], location[1]);
                var nobounds = new google.maps.LatLngBounds();
                nobounds.extend(latlng);
                bounds.union(nobounds);

                var marker = new google.maps.Marker({
                    position: latlng,
                    map: map,
                    title: location[3],
                    content: location[4]
                });

                google.maps.event.addListener(marker, 'click', function ()
                {
                  infowindow.setContent(this.content);
                  infowindow.open(map, this);
                });
              }
              else
              {
                geocoder.geocode( { 'address': location[2]}, function(results, status)
                {
                  if (status == google.maps.GeocoderStatus.OK)
                  {
                    var latlng = new google.maps.LatLng(results[0].geometry.location.lat(), results[0].geometry.location.lng());
                    var yesbounds = new google.maps.LatLngBounds();
                    yesbounds.extend(latlng);
                    bounds.union(yesbounds);

                    var marker = new google.maps.Marker({
                        position: latlng,
                        map: map,
                        title: location[3],
                        content: location[4]
                    });

                    google.maps.event.addListener(marker, 'click', function ()
                    {
                      infowindow.setContent(this.content);
                      infowindow.open(map, this);
                    });
                  }

                  var locationcount = " + LocationCount + @";

                  if (locationcount >= 2)
                  {
                    map.fitBounds(bounds);
                  }
                  else
                  {
                    map.setCenter(bounds.getCenter());
                    map.setZoom(14);
                  }
                });
              }
            }

            if (yesaddressexist == 'No')
            {
              var locationcount = " + LocationCount + @";

              if (locationcount >= 2)
              {
                map.fitBounds(bounds);
              }
              else
              {
                map.setCenter(bounds.getCenter());
                map.setZoom(14);
              }
            }
          }

          google.maps.event.addDomListener(window, 'load', initialize);
        </script>", CultureInfo.CurrentCulture);
    }


    //--START-- --Search--//
    protected void RegisterPostBackControl()
    {
      ScriptManager ScriptManager_Search = ScriptManager.GetCurrent(Page);

      ScriptManager_Search.RegisterPostBackControl(DropDownList_Country);
    }

    protected void DropDownList_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_Province.Items.Clear();
      SqlDataSource_FSM_Location_Province.SelectParameters["CountryKey"].DefaultValue = DropDownList_Country.SelectedValue;
      DropDownList_Province.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Province", CultureInfo.CurrentCulture), ""));
      DropDownList_Province.DataBind();

      RegisterPostBackControl();
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Name.SelectedValue;
      string SearchField2 = DropDownList_Country.SelectedValue;
      string SearchField3 = DropDownList_Province.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Location=" + DropDownList_Name.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Country=" + DropDownList_Country.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_Province=" + DropDownList_Province.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Form_FSM_Map.aspx?" + SearchField1 + SearchField2 + SearchField3;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - Map", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - Map", "Form_FSM_Map.aspx"), false);
    }
    //---END--- --Search--//
  }
}