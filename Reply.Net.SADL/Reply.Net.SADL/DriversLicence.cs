using System;
using System.Collections.Generic;

namespace Reply.Net.SADL
{
  public class DriversLicence
  {
    /// <summary>
    /// Surname
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Initials
    /// </summary>
    public string Initials { get; set; }

    /// <summary>
    /// Identity number
    /// </summary>
    public string IdentityNumber { get; set; }

    /// <summary>
    /// Date of birth
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Gender
    /// 01 = male, 02 = female
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// Licence codes
    /// </summary>
    public List<string> LicenceCodes { get; set; }

    /// <summary>
    /// Licence number
    /// </summary>
    public string LicenceNumber { get; set; }

    /// <summary>
    /// Identity country of issue
    /// </summary>
    public string IdentityCountryOfIssue { get; set; }

    /// <summary>
    /// Licence country of issue
    /// </summary>
    public string LicenceCountryOfIssue { get; set; }

    /// <summary>
    /// Vehicle restrictions
    /// Up to 4 restrictions
    /// </summary>
    public List<string> VehicleRestrictions { get; set; }

    /// <summary>
    /// Identity number type
    /// 02 = South African
    /// </summary>
    public string IdentityNumberType { get; set; }

    /// <summary>
    /// Licence code issue dates
    /// Up to 4 dates
    /// </summary>
    public List<DateTime> LicenceCodeIssueDates { get; set; }

    /// <summary>
    /// Driver restriction codes, formatted as XX
    /// 0 = none, 1 = glasses, 2 = artificial limb
    /// </summary>
    public string DriverRestrictionCodes { get; set; }

    /// <summary>
    /// Professional driving permit expiry date
    /// </summary>
    public DateTime? ProfessionalDrivingPermitExpiryDate { get; set; }

    /// <summary>
    /// Licence issue number
    /// </summary>
    public string LicenceIssueNumber { get; set; }

    /// <summary>
    /// Drivers licence image
    /// </summary>
    public DriversLicenceImage DriversLicenceImage { get; set; }

    /// <summary>
    /// Licence issue date
    /// </summary>
    public DateTime? LicenceIssueDate { get; set; }

    /// <summary>
    /// Licence expiry date
    /// </summary>
    public DateTime? LicenceExpiryDate { get; set; }
  }
}
