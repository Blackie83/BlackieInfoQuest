
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Search-----------------------------------------------------------------------------------------------------------------------------
function Validation_Search()
{
  if (document.getElementById("TextBox_SearchName"))
  {
    if (document.getElementById("TextBox_SearchName").value == "")
    {
      document.getElementById("SearchName").style.backgroundColor = "#d46e6e";
      document.getElementById("SearchName").style.color = "#333333";
    }
    else
    {
      document.getElementById("SearchName").style.backgroundColor = "#77cf9c";
      document.getElementById("SearchName").style.color = "#333333";
    }
  }
}