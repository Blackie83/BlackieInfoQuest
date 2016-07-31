//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Search-----------------------------------------------------------------------------------------------------------------------------
function Validation_Search() {
  if ((document.getElementById("TextBox_CurrentDateFrom").value == "" || document.getElementById("TextBox_CurrentDateFrom").value == "yyyy/mm/dd") || (document.getElementById("TextBox_CurrentDateTo").value == "" || document.getElementById("TextBox_CurrentDateTo").value == "yyyy/mm/dd")) {
    document.getElementById("SearchCurrentDate").style.backgroundColor = "#d46e6e";
    document.getElementById("SearchCurrentDate").style.color = "#333333";
  }
  else {
    document.getElementById("SearchCurrentDate").style.backgroundColor = "#77cf9c";
    document.getElementById("SearchCurrentDate").style.color = "#333333";
  }
}