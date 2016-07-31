
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form() {
  if (document.getElementById("DropDownList_Facility")) {
    if (document.getElementById("DropDownList_Facility").value == "") {
      document.getElementById("FormFacility").style.backgroundColor = "#d46e6e";
      document.getElementById("FormFacility").style.color = "#333333";
    } else {
      document.getElementById("FormFacility").style.backgroundColor = "#77cf9c";
      document.getElementById("FormFacility").style.color = "#333333";
    }
  }

  if (document.getElementById("CheckBoxList_SecurityRole")) {
    var CheckBoxList_SecurityRole = document.getElementById("CheckBoxList_SecurityRole");
    var CheckBoxList_SecurityRole_Count = CheckBoxList_SecurityRole.getElementsByTagName("input");

    var TotalItems = CheckBoxList_SecurityRole_Count.length;
    var Completed = "0";
    for (var a = 0; a < TotalItems; a++) {
      if (document.getElementById("CheckBoxList_SecurityRole_" + a + "").checked == true) {
        Completed = "1";
        document.getElementById("FormSecurityRole").style.backgroundColor = "#77cf9c";
        document.getElementById("FormSecurityRole").style.color = "#333333";
      } else if (document.getElementById("CheckBoxList_SecurityRole_" + a + "").checked == false && Completed == "0") {
        document.getElementById("FormSecurityRole").style.backgroundColor = "#d46e6e";
        document.getElementById("FormSecurityRole").style.color = "#333333";
      }
    }
  } else {
    if (document.getElementById("DropDownList_Facility")) {
      document.getElementById("FormSecurityRole").style.backgroundColor = "#d46e6e";
      document.getElementById("FormSecurityRole").style.color = "#333333";
    }
  }

  if (document.getElementById("CheckBoxList_SecurityRole")) {
    var CheckBoxList_SecurityRole = document.getElementById("CheckBoxList_SecurityRole");
    var InputItemArray = CheckBoxList_SecurityRole.getElementsByTagName("input");
    var TotalItems = InputItemArray.length;
    var LastForm = "";

    for (var a = 0; a < InputItemArray.length; a++) {
      var InputItem = InputItemArray[a];

      if (InputItem.checked) {
        var SecurityRole = InputItemArray[a].parentNode.getElementsByTagName('label');
        var SecurityRoleText = SecurityRole[0].innerHTML;

        var SecurityRoleSplit = SecurityRoleText.split(" -- ");
        var CurrentForm = SecurityRoleSplit[0];

        if (LastForm == CurrentForm) {
          InputItem.parentElement.style.color = "#333333";
          InputItem.parentElement.style.backgroundColor = "#d46e6e";
        } else {
          InputItem.parentElement.style.color = "#333333";
          InputItem.parentElement.style.backgroundColor = "#77cf9c";
        }

        LastForm = CurrentForm;
      }
      else {
        InputItem.parentElement.style.color = "#000000";
        InputItem.parentElement.style.backgroundColor = "#f7f7f7";
      }
    }
  }
}