﻿
$(document).ready(function () {
  $("#Body_AAA_Test").ready(function () {
    //PageLoad_ShowPage();
  });
});


$(document).ready(function () {
  $("#Button_TestServerPost").click(function () {
    //PageLoad_HidePage();
  });
});


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --PageLoad_ShowPage-----------------------------------------------------------------------------------------------------------------------------
function PageLoad_ShowPage() {
  //document.getElementById("PageLoad_Loader").style.display = "none";
  //document.getElementById("PageLoad_Page").style.display = "block";
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --PageLoad_HidePage-----------------------------------------------------------------------------------------------------------------------------
function PageLoad_HidePage() {
  //document.getElementById("PageLoad_Loader").style.display = "block";
  //document.getElementById("PageLoad_Page").style.display = "none";
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Time-------------------------------------------------------------------------------------------------------------------------------
function Validation_Time() {
  if ($("#ControlsTimePicker_SelectTime_DropDownList_Hour").val() == "" || $("#ControlsTimePicker_SelectTime_DropDownList_Minute").val() == "") {
    $("#Time").css("background-color", "#d46e6e");
    $("#Time").css("color", "#333333");
  } else {
    $("#Time").css("background-color", "#77cf9c");
    $("#Time").css("color", "#333333");
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Date-------------------------------------------------------------------------------------------------------------------------------
function Validation_Date() {
  if ($("#ControlsDatePicker_SelectDate_DropDownList_Year").val() == "" || $("#ControlsDatePicker_SelectDate_DropDownList_Month").val() == "" || $("#ControlsDatePicker_SelectDate_DropDownList_Day").val() == "") {
    $("#Date").css("background-color", "#d46e6e");
    $("#Date").css("color", "#333333");
  }
  else {
    $("#Date").css("background-color", "#77cf9c");
    $("#Date").css("color", "#333333");
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_DateTime---------------------------------------------------------------------------------------------------------------------------
function Validation_DateTime() {
  if ($("#ControlsDateTimePicker_SelectDateTime_DropDownList_Year").val() == "" || $("#ControlsDateTimePicker_SelectDateTime_DropDownList_Month").val() == "" || $("#ControlsDateTimePicker_SelectDateTime_DropDownList_Day").val() == "" || $("#ControlsDateTimePicker_SelectDateTime_DropDownList_Hour").val() == "" || $("#ControlsDateTimePicker_SelectDateTime_DropDownList_Minute").val() == "") {
    $("#DateTime").css("background-color", "#d46e6e");
    $("#DateTime").css("color", "#333333");
  }
  else {
    $("#DateTime").css("background-color", "#77cf9c");
    $("#DateTime").css("color", "#333333");
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Show------------------------------------------------------------------------------------------------------------------------------------------
function Show(id) {
  $("#" + id + "").show();
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Hide------------------------------------------------------------------------------------------------------------------------------------------
function Hide(id) {
  $("#" + id + "").hide();
}
