
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form() {
  var FormMode;
  if (document.getElementById("FormView_SecurityUser_Form_HiddenField_Insert")) {
    FormMode = "Insert";
  } else if (document.getElementById("FormView_SecurityUser_Form_HiddenField_Edit")) {
    FormMode = "Edit";
  } else {
    FormMode = "";
  }

  if (FormMode != "") {
    if (document.getElementById("FormView_SecurityUser_Form_TextBox_" + FormMode + "UserName").value == "") {
      document.getElementById("FormUserName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormUserName").style.color = "#333333";
    } else {
      var UserNameText = document.getElementById("FormView_SecurityUser_Form_TextBox_" + FormMode + "UserName").value;
      var UserNameIndex = UserNameText.indexOf("LHC\\", 0);

      if (UserNameIndex == -1) {
        document.getElementById("FormUserName").style.backgroundColor = "#d46e6e";
        document.getElementById("FormUserName").style.color = "#333333";
      } else {
        document.getElementById("FormUserName").style.backgroundColor = "#77cf9c";
        document.getElementById("FormUserName").style.color = "#333333";
      }
    }

    if (document.getElementById("FormView_SecurityUser_Form_TextBox_" + FormMode + "DisplayName").value == "") {
      document.getElementById("FormDisplayName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormDisplayName").style.color = "#333333";
    } else {
      document.getElementById("FormDisplayName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormDisplayName").style.color = "#333333";
    }

    if (document.getElementById("FormView_SecurityUser_Form_TextBox_" + FormMode + "FirstName").value == "") {
      document.getElementById("FormFirstName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormFirstName").style.color = "#333333";
    } else {
      document.getElementById("FormFirstName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormFirstName").style.color = "#333333";
    }

    if (document.getElementById("FormView_SecurityUser_Form_TextBox_" + FormMode + "LastName").value == "") {
      document.getElementById("FormLastName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormLastName").style.color = "#333333";
    } else {
      document.getElementById("FormLastName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormLastName").style.color = "#333333";
    }

    if (document.getElementById("FormView_SecurityUser_Form_TextBox_" + FormMode + "EmployeeNumber").value == "") {
      document.getElementById("FormEmployeeNumber").style.backgroundColor = "#d46e6e";
      document.getElementById("FormEmployeeNumber").style.color = "#333333";
    } else {
      document.getElementById("FormEmployeeNumber").style.backgroundColor = "#77cf9c";
      document.getElementById("FormEmployeeNumber").style.color = "#333333";
    }

    if (document.getElementById("FormView_SecurityUser_Form_TextBox_" + FormMode + "Email").value == "") {
      document.getElementById("FormEmail").style.backgroundColor = "#d46e6e";
      document.getElementById("FormEmail").style.color = "#333333";
    } else {
      document.getElementById("FormEmail").style.backgroundColor = "#77cf9c";
      document.getElementById("FormEmail").style.color = "#333333";
    }

    if (document.getElementById("FormView_SecurityUser_Form_TextBox_" + FormMode + "ManagerUserName").value == "") {
      document.getElementById("FormManagerUserName").style.backgroundColor = "#d46e6e";
      document.getElementById("FormManagerUserName").style.color = "#333333";
    } else {
      document.getElementById("FormManagerUserName").style.backgroundColor = "#77cf9c";
      document.getElementById("FormManagerUserName").style.color = "#333333";
    }
  }
}