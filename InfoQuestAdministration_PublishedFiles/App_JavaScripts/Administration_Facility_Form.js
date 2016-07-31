
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form() {
  if (document.getElementById("CheckBoxList_Facility")) {
    var CheckBoxList_Facility = document.getElementById("CheckBoxList_Facility");
    var InputItemArray = CheckBoxList_Facility.getElementsByTagName("input");
    var TotalItems = InputItemArray.length;

    for (var a = 0; a < InputItemArray.length; a++) {
      var InputItem = InputItemArray[a];

      if (InputItem.checked) {
        InputItem.parentElement.style.color = "#333333";
        InputItem.parentElement.style.backgroundColor = "#77cf9c";
      }
      else {
        InputItem.parentElement.style.color = "#000000";
        InputItem.parentElement.style.backgroundColor = "#f7f7f7";
      }
    }
  }
}