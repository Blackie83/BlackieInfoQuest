
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --FormEmail-------------------------------------------------------------------------------------------------------------------------------------
function FormEmail(EmailLink)
{
  var width = 750;
  var height = 700;
  var left = (screen.width - width) / 2;
  var top = (screen.height - height) / 2;
  window.open(EmailLink, 'Email', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=No , location=No , scrollbars=Yes , resizable=No , status=Yes , left=' + left + ' , top=' + top + ' ');
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --FormPrint-------------------------------------------------------------------------------------------------------------------------------------
function FormPrint(PrintLink)
{
  var width = 750;
  var height = 700;
  var left = (screen.width - width) / 2;
  var top = (screen.height - height) / 2;
  window.open(PrintLink, 'Print', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=No , location=No , scrollbars=Yes , resizable=No , status=Yes , left=' + left + ' , top=' + top + ' ');
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --LockedRecord----------------------------------------------------------------------------------------------------------------------------------
function LockedRecord()
{
  if (QueryStringValue("Locked") == null)
  {
    alert("Record is Locked \n\n" +
        "Click the \"Update Statistics\" button to save the changes and exit the form \n" +
        "or \n" +
        "Click the \"Back to List\" button to exit the form \n");

    window.location.href = window.location.href + "&Locked=1"
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --QueryStringValue------------------------------------------------------------------------------------------------------------------------------
function QueryStringValue(FindName)
{
  var QueryString = window.location.search.substring(1);
  var QueryStringSplit = QueryString.split("&");
  for (var a = 0; a < QueryStringSplit.length; a++)
  {
    var QueryStringValue = QueryStringSplit[a].split("=");
    if (QueryStringValue[0] == FindName)
    {
      return QueryStringValue[1];
    }
  }
  return null;
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Validation_Form-------------------------------------------------------------------------------------------------------------------------------
function Validation_Form(Control)
{
  if (Control == undefined)
  {
    if (document.getElementById("Label_GridItem"))
    {
      var TotalRecords = document.getElementById("HiddenField_TotalRecordsItem").value
      for (var a = 0; a < TotalRecords; a++)
      {
        var AVGValueLow = document.getElementById("GridView_SustainabilityManagement_Item_HiddenField_EditAVGValueLow_" + a + "").value;
        var AVGValueHigh = document.getElementById("GridView_SustainabilityManagement_Item_HiddenField_EditAVGValueHigh_" + a + "").value;
        var EditValue = "";
        if (document.getElementById("GridView_SustainabilityManagement_Item_TextBox_EditValue_" + a + ""))
        {
          var EditValue = document.getElementById("GridView_SustainabilityManagement_Item_TextBox_EditValue_" + a + "").value;
        }

        if (EditValue == "")
        {
          Hide("GridView_SustainabilityManagement_Item_Label_EditAbove_" + a + "");
          Hide("GridView_SustainabilityManagement_Item_Label_EditBelow_" + a + "");
        }
        else
        {
          if (AVGValueLow == "" && AVGValueHigh == "")
          {
            Hide("GridView_SustainabilityManagement_Item_Label_EditAbove_" + a + "");
            Hide("GridView_SustainabilityManagement_Item_Label_EditBelow_" + a + "");
          }
          else
          {
            if (Math.round(EditValue * 100) > Math.round(AVGValueLow * 100) && Math.round(EditValue * 100) < Math.round(AVGValueHigh * 100))
            {
              Hide("GridView_SustainabilityManagement_Item_Label_EditAbove_" + a + "");
              Hide("GridView_SustainabilityManagement_Item_Label_EditBelow_" + a + "");
            }
            else if (Math.round(EditValue * 100) < Math.round(AVGValueLow * 100))
            {
              Hide("GridView_SustainabilityManagement_Item_Label_EditAbove_" + a + "");
              Show("GridView_SustainabilityManagement_Item_Label_EditBelow_" + a + "");
            }
            else if (Math.round(EditValue * 100) > Math.round(AVGValueHigh * 100))
            {
              Show("GridView_SustainabilityManagement_Item_Label_EditAbove_" + a + "");
              Hide("GridView_SustainabilityManagement_Item_Label_EditBelow_" + a + "");
            }
            else
            {
              Hide("GridView_SustainabilityManagement_Item_Label_EditAbove_" + a + "");
              Hide("GridView_SustainabilityManagement_Item_Label_EditBelow_" + a + "");
            }
          }
        }
      }
    }
    else if (document.getElementById("Label_GridItemList"))
    {
      var TotalRecords = document.getElementById("HiddenField_TotalRecordsItemList").value
      for (var a = 0; a < TotalRecords; a++)
      {
        var AVGValueLow = document.getElementById("GridView_SustainabilityManagement_Item_List_HiddenField_ItemAVGValueLow_" + a + "").value;
        var AVGValueHigh = document.getElementById("GridView_SustainabilityManagement_Item_List_HiddenField_ItemAVGValueHigh_" + a + "").value;
        var ItemValue = document.getElementById("GridView_SustainabilityManagement_Item_List_HiddenField_ItemValue_" + a + "").value;

        if (ItemValue == "")
        {
          Hide("GridView_SustainabilityManagement_Item_List_Label_ItemAbove_" + a + "");
          Hide("GridView_SustainabilityManagement_Item_List_Label_ItemBelow_" + a + "");
        }
        else
        {
          if (AVGValueLow == "" && AVGValueHigh == "")
          {
            Hide("GridView_SustainabilityManagement_Item_List_Label_ItemAbove_" + a + "");
            Hide("GridView_SustainabilityManagement_Item_List_Label_ItemBelow_" + a + "");
          }
          else
          {
            if (Math.round(ItemValue * 100) > Math.round(AVGValueLow * 100) && Math.round(ItemValue * 100) < Math.round(AVGValueHigh * 100))
            {
              Hide("GridView_SustainabilityManagement_Item_List_Label_ItemAbove_" + a + "");
              Hide("GridView_SustainabilityManagement_Item_List_Label_ItemBelow_" + a + "");
            }
            else if (Math.round(ItemValue * 100) < Math.round(AVGValueLow * 100))
            {
              Hide("GridView_SustainabilityManagement_Item_List_Label_ItemAbove_" + a + "");
              Show("GridView_SustainabilityManagement_Item_List_Label_ItemBelow_" + a + "");
            }
            else if (Math.round(ItemValue * 100) > Math.round(AVGValueHigh * 100))
            {
              Show("GridView_SustainabilityManagement_Item_List_Label_ItemAbove_" + a + "");
              Hide("GridView_SustainabilityManagement_Item_List_Label_ItemBelow_" + a + "");
            }
            else
            {
              Hide("GridView_SustainabilityManagement_Item_List_Label_ItemAbove_" + a + "");
              Hide("GridView_SustainabilityManagement_Item_List_Label_ItemBelow_" + a + "");
            }
          }
        }
      }
    }
  }
  else
  {
    if (document.getElementById("Label_GridItem"))
    {
      var AVGValueLow = document.getElementById("GridView_SustainabilityManagement_Item_HiddenField_EditAVGValueLow_" + Control + "").value;
      var AVGValueHigh = document.getElementById("GridView_SustainabilityManagement_Item_HiddenField_EditAVGValueHigh_" + Control + "").value;
      var EditValue = "";
      if (document.getElementById("GridView_SustainabilityManagement_Item_TextBox_EditValue_" + Control + ""))
      {
        var EditValue = document.getElementById("GridView_SustainabilityManagement_Item_TextBox_EditValue_" + Control + "").value;
      }

      if (EditValue == "")
      {
        Hide("GridView_SustainabilityManagement_Item_Label_EditAbove_" + Control + "");
        Hide("GridView_SustainabilityManagement_Item_Label_EditBelow_" + Control + "");
      }
      else
      {
        if (AVGValueLow == "" && AVGValueHigh == "")
        {
          Hide("GridView_SustainabilityManagement_Item_Label_EditAbove_" + Control + "");
          Hide("GridView_SustainabilityManagement_Item_Label_EditBelow_" + Control + "");
        }
        else
        {
          if (Math.round(EditValue * 100) > Math.round(AVGValueLow * 100) && Math.round(EditValue * 100) < Math.round(AVGValueHigh * 100))
          {
            Hide("GridView_SustainabilityManagement_Item_Label_EditAbove_" + Control + "");
            Hide("GridView_SustainabilityManagement_Item_Label_EditBelow_" + Control + "");
          }
          else if (Math.round(EditValue * 100) < Math.round(AVGValueLow * 100))
          {
            Hide("GridView_SustainabilityManagement_Item_Label_EditAbove_" + Control + "");
            Show("GridView_SustainabilityManagement_Item_Label_EditBelow_" + Control + "");
          }
          else if (Math.round(EditValue * 100) > Math.round(AVGValueHigh * 100))
          {
            Show("GridView_SustainabilityManagement_Item_Label_EditAbove_" + Control + "");
            Hide("GridView_SustainabilityManagement_Item_Label_EditBelow_" + Control + "");
          }
          else
          {
            Hide("GridView_SustainabilityManagement_Item_Label_EditAbove_" + Control + "");
            Hide("GridView_SustainabilityManagement_Item_Label_EditBelow_" + Control + "");
          }
        }
      }
    }
  }
}

//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Show------------------------------------------------------------------------------------------------------------------------------------------
function Show(id)
{
  if (document.getElementById)
  {
    document.getElementById(id).style.display = '';
  }
}


//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --Hide------------------------------------------------------------------------------------------------------------------------------------------
function Hide(id)
{
  if (document.getElementById)
  {
    document.getElementById(id).style.display = 'none';
  }
}