
//----- ------------------------------------------------------------------------------------------------------------------------------------------------
//----- --FormPatientInfectionHistory-------------------------------------------------------------------------------------------------------------------
function FormPatientInfectionHistory(PatientInfectionHistoryLink)
{
  var width = 800;
  var height = 700;
  var left = (screen.width - width) / 2;
  var top = (screen.height - height) / 2;
  window.open(PatientInfectionHistoryLink, 'PatientInfectionHistory', 'width=' + width + ' , height=' + height + ' , toolbar=No , menubar=Yes , location=No , scrollbars=Yes , resizable=Yes , status=Yes , left=' + left + ' , top=' + top + ' ');
}
