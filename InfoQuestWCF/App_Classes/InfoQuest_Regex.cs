using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace InfoQuestWCF
{
  public class InfoQuest_Regex
  {
    string ValidEmailAddress = "No";

    public string Regex_ValidEmailAddress(string emailAddress)
    {
      ValidEmailAddress = "No";
      if (string.IsNullOrEmpty(emailAddress))
      {
        return "No";
      }

      emailAddress = Regex.Replace(emailAddress, @"(@)(.+)$", Regex_DomainMapper);

      if (ValidEmailAddress == "No")
      {
        return "No";
      }
      else
      {
        bool IsValidEmailAddress = Regex.IsMatch(emailAddress, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$", RegexOptions.IgnoreCase);

        if (IsValidEmailAddress == true)
        {
          return "Yes";
        }
        else
        {
          return "No";
        }
      }
    }

    private string Regex_DomainMapper(Match MatchDomain)
    {
      IdnMapping IdnMapping_Domain = new IdnMapping();

      string DomainName = MatchDomain.Groups[2].Value;
      try
      {
        DomainName = IdnMapping_Domain.GetAscii(DomainName);
        ValidEmailAddress = "Yes";
      }
      catch (ArgumentException)
      {
        ValidEmailAddress = "No";
      }

      return MatchDomain.Groups[1].Value + DomainName;
    }

    public static string Regex_Longitude(string longitude)
    {
      string ValidLongitude = "No";

      if (!string.IsNullOrEmpty(longitude))
      {
        bool IsValidLongitude = Regex.IsMatch(longitude, @"^-?([1]?[1-7][1-9]|[1]?[1-8][0]|[1-9]?[0-9])\.{1}\d{1,6}", RegexOptions.IgnoreCase);

        if (IsValidLongitude == true)
        {
          ValidLongitude = "Yes";
        }
      }

      return ValidLongitude;
    }

    public static string Regex_Latitude(string latitude)
    {
      string ValidLatitude = "No";

      if (!string.IsNullOrEmpty(latitude))
      {
        bool IsValidLatitude = Regex.IsMatch(latitude, @"^-?([1-8]?[1-9]|[1-9]0)\.{1}\d{1,6}", RegexOptions.IgnoreCase);

        if (IsValidLatitude == true)
        {
          ValidLatitude = "Yes";
        }
      }

      return ValidLatitude;
    }

    public static string Regex_Currency(string currency)
    {
      string ValidCurrency = "No";

      if (!string.IsNullOrEmpty(currency))
      {
        bool IsValidCurrency = Regex.IsMatch(currency, @"^\d+(?:\.\d{0,2})?$", RegexOptions.IgnoreCase);

        if (IsValidCurrency == true)
        {
          ValidCurrency = "Yes";
        }
      }

      return ValidCurrency;
    }
  }
}