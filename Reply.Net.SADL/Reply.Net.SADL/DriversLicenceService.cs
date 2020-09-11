using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Reply.Net.SADL
{
  public class DriversLicenceService
  {
    private byte[] _version1Bytes = new byte[] { 0x01, 0xe1, 0x02, 0x45 };
    private byte[] _version2Bytes = new byte[] { 0x01, 0x9b, 0x09, 0x45 };

    private string _version1LicenceKey128 = @"-----BEGIN RSA PUBLIC KEY-----
MIGXAoGBAP7S4cJ+M2MxbncxenpSxUmBOVGGvkl0dgxyUY1j4FRKSNCIszLFsMNw
x2XWXZg8H53gpCsxDMwHrncL0rYdak3M6sdXaJvcv2CEePrzEvYIfMSWw3Ys9cRl
HK7No0mfrn7bfrQOPhjrMEFw6R7VsVaqzm9DLW7KbMNYUd6MZ49nAhEAu3l//ex/
nkLJ1vebE3BZ2w==
-----END RSA PUBLIC KEY-----";

    private string _version1LicenceKey74 = @"-----BEGIN RSA PUBLIC KEY-----
MGACSwD/POxrX0Djw2YUUbn8+u866wbcIynA5vTczJJ5cmcWzhW74F7tLFcRvPj1
tsj3J221xDv6owQNwBqxS5xNFvccDOXqlT8MdUxrFwIRANsFuoItmswz+rfY9Cf5
zmU=
-----END RSA PUBLIC KEY-----";

    private string _version2LicenceKey128 = @"-----BEGIN RSA PUBLIC KEY-----
MIGWAoGBAMqfGO9sPz+kxaRh/qVKsZQGul7NdG1gonSS3KPXTjtcHTFfexA4MkGA
mwKeu9XeTRFgMMxX99WmyaFvNzuxSlCFI/foCkx0TZCFZjpKFHLXryxWrkG1Bl9+
+gKTvTJ4rWk1RvnxYhm3n/Rxo2NoJM/822Oo7YBZ5rmk8NuJU4HLAhAYcJLaZFTO
sYU+aRX4RmoF
-----END RSA PUBLIC KEY-----";

    private string _version2LicenceKey74 = @"-----BEGIN RSA PUBLIC KEY-----
MF8CSwC0BKDfEdHKz/GhoEjU1XP5U6YsWD10klknVhpteh4rFAQlJq9wtVBUc5Dq
bsdI0w/bga20kODDahmGtASy9fae9dobZj5ZUJEw5wIQMJz+2XGf4qXiDJu0R2U4
Kw==
-----END RSA PUBLIC KEY-----";

    private byte[] EncryptValue(byte[] rgb, BigInteger e, BigInteger n, int size)
    {
      BigInteger input = new BigInteger(rgb);
      BigInteger output = input.ModPow(e, n);

      var result = output.ToByteArrayUnsigned();
      var padded = new byte[size];

      Buffer.BlockCopy(result, 0, padded, (size - result.Length), result.Length);

      return padded;
    }

    private RsaKeyParameters GetRsaKeyParameters(string key)
    {
      using (var reader = new StringReader(key))
      {
        var pem = new PemReader(reader);
        return (RsaKeyParameters)pem.ReadObject();
      }
    }

    /// <summary>
    /// Decrypt drivers licence
    /// </summary>
    /// <param name="data">Encrypted licence data</param>
    public byte[] DecryptLicence(byte[] licenceBytes)
    {
      // Validate licence length
      if (licenceBytes.Length != 720)
      {
        throw new Exception("Invalid licence data");
      }

      using (var memoryStream = new MemoryStream(licenceBytes))
      using (var binaryReader = new BinaryReader(memoryStream))
      {
        // Read version bytes from licence
        var versionBytes = binaryReader.ReadBytes(4);

        // Read empty bytes
        binaryReader.ReadBytes(2);

        RsaKeyParameters rsaKeyParameters128;
        RsaKeyParameters rsaKeyParameters74;

        if (versionBytes.SequenceEqual(_version1Bytes))
        {
          // Interpret licence as version 1
          rsaKeyParameters128 = GetRsaKeyParameters(_version1LicenceKey128);
          rsaKeyParameters74 = GetRsaKeyParameters(_version1LicenceKey74);
        }
        else if (versionBytes.SequenceEqual(_version2Bytes))
        {
          // Interpret licence as version 2
          rsaKeyParameters128 = GetRsaKeyParameters(_version2LicenceKey128);
          rsaKeyParameters74 = GetRsaKeyParameters(_version2LicenceKey74);
        }
        else
        {
          throw new Exception("Invalid licence version");
        }

        var decryptedBytes = new byte[720];

        for (var i = 0; i < 6; i++)
        {
          var blockData = binaryReader.ReadBytes(128);

          byte[] decryptedBlockData = null;

          if (blockData.Length == 128)
          {
            // Decrypt using 128 byte key
            decryptedBlockData = EncryptValue(blockData, rsaKeyParameters128.Exponent, rsaKeyParameters128.Modulus, 128);
          }
          else
          {
            // Decrypt using 74 byte key
            decryptedBlockData = EncryptValue(blockData, rsaKeyParameters74.Exponent, rsaKeyParameters74.Modulus, 74);
          }

          Buffer.BlockCopy(decryptedBlockData, 0, decryptedBytes, i * 128, decryptedBlockData.Length);
        }

        return decryptedBytes;
      }
    }

    /// <summary>
    /// Decode fields from drivers licence
    /// </summary>
    /// <param name="data">Decrypted licence data</param>
    public DriversLicence DecodeLicence(byte[] data)
    {
      var driversLicence = new DriversLicence();

      using (var memoryStream = new MemoryStream(data))
      using (var binaryReader = new BinaryReader(memoryStream))
      {
        while (binaryReader.ReadByte() != 0x82)
        {
          // Dump bytes until byte with value 0x82 is read
        }

        // Dump bytes 0x5a
        binaryReader.ReadByte();

        // Read licence codes
        driversLicence.LicenceCodes = ReadStringList(binaryReader, 4);

        // Read surname
        driversLicence.Surname = ReadString(binaryReader);

        // Read initials
        driversLicence.Initials = ReadString(binaryReader);

        // Read ID country of issue
        driversLicence.IdentityCountryOfIssue = ReadString(binaryReader);

        // Read licence country of issue
        driversLicence.LicenceCountryOfIssue = ReadString(binaryReader);

        // Read vehicle restriction numbers
        driversLicence.VehicleRestrictions = ReadStringList(binaryReader, 4);

        // Read licence number
        driversLicence.LicenceNumber = ReadString(binaryReader);

        // Read identity number
        var identityNumber = String.Empty;
        for (var i = 0; i < 13; i++)
        {
          identityNumber += (char)binaryReader.ReadByte();
        }

        driversLicence.IdentityNumber = identityNumber;

        // Read identity number type
        driversLicence.IdentityNumberType = binaryReader.ReadByte().ToString("x2");

        // Nibble processing

        // Read data into nibbles until 0x57
        var nibbleQueue = new Queue<Nibble>();
        while (true)
        {
          var currentByte = binaryReader.ReadByte();
          if (currentByte == 0x57)
          {
            break;
          }

          var nibbles = ByteToNibbles(currentByte);

          nibbleQueue.Enqueue(nibbles[0]);
          nibbleQueue.Enqueue(nibbles[1]);
        }

        // Read licence code issue dates
        //var licenceCodeIssueDates = ReadNibbleDateStringList(nibbleQueue, 4);
        driversLicence.LicenceCodeIssueDates = ReadNibbleDateList(nibbleQueue, 4);


        // Read restriction codes
        // 0 = none, 1 = glasses, 2 = artificial limb
        driversLicence.DriverRestrictionCodes = $"{nibbleQueue.Dequeue()}{nibbleQueue.Dequeue()}";

        // Read professional driving permit expiry date
        driversLicence.ProfessionalDrivingPermitExpiryDate = ReadNibbleDate(nibbleQueue);

        // Read licence issue number
        driversLicence.LicenceIssueNumber = $"{nibbleQueue.Dequeue()}{nibbleQueue.Dequeue()}";

        // Read date of birth
        driversLicence.DateOfBirth = ReadNibbleDate(nibbleQueue);

        // Read licence valid from date
        driversLicence.LicenceIssueDate = ReadNibbleDate(nibbleQueue);

        // Read licence valid to date
        driversLicence.LicenceExpiryDate = ReadNibbleDate(nibbleQueue);

        // Read gender
        // 01 = male, 02 = female
        driversLicence.Gender = $"{nibbleQueue.Dequeue()}{nibbleQueue.Dequeue()}";

        // Read image
        var driversLicenceImage = new DriversLicenceImage();
        driversLicence.DriversLicenceImage = driversLicenceImage;

        // Dump 2 bytes
        binaryReader.ReadBytes(2);

        // Dump byte 0x00
        binaryReader.ReadByte();

        // Read image width
        var imageWidth = (int)binaryReader.ReadByte();
        driversLicenceImage.Width = imageWidth;

        // Dump byte 0x00
        binaryReader.ReadByte();

        // Read image height
        var imageHeight = (int)binaryReader.ReadByte();
        driversLicenceImage.Height = imageHeight;

        // TODO: complete implementation of image decoding
      }

      return driversLicence;
    }

    private string ReadString(BinaryReader binaryReader)
    {
      var fieldDeliminators = new byte[] { 0xe0, 0xe1 };

      var value = String.Empty;

      while (true)
      {
        var currentByte = binaryReader.ReadByte();
        if (fieldDeliminators.Contains(currentByte))
        {
          break;
        }

        value += (char)currentByte;
      }

      return value;
    }

    private List<string> ReadStringList(BinaryReader binaryReader, int length)
    {
      var valueList = new List<string>();

      var skippedCharacter = false;

      for (var i = 0; i < length; i++)
      {
        var value = String.Empty;

        while (true)
        {
          var currentByte = binaryReader.ReadByte();
          if (currentByte == 0xe0)
          {
            // Continue to read next string
            break;
          }
          else if (currentByte == 0xe1)
          {
            // Offset field count
            if (skippedCharacter == false)
            {
              i++;
              skippedCharacter = true;
            }
            // Continue to read next string
            break;
          }

          value += (char)currentByte;
        }

        if (String.IsNullOrEmpty(value) == false)
        {
          valueList.Add(value);
        }
      }

      return valueList;
    }

    private Nibble[] ByteToNibbles(byte value)
    {
      var nibbles = new Nibble[2];

      nibbles[0] = new Nibble(value >> 4);
      nibbles[1] = new Nibble(value);

      return nibbles;
    }

    private DateTime? ReadNibbleDate(Queue<Nibble> nibbleQueue)
    {
      var dateString = ReadNibbleDateString(nibbleQueue);
      if (String.IsNullOrEmpty(dateString))
      {
        return null;
      }

      return DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    private string ReadNibbleDateString(Queue<Nibble> nibbleQueue)
    {
      var m = nibbleQueue.Dequeue();
      if (m.Value == 10)
      {
        return String.Empty;
      }

      var c = nibbleQueue.Dequeue();
      var d = nibbleQueue.Dequeue();
      var y = nibbleQueue.Dequeue();

      var m1 = nibbleQueue.Dequeue();
      var m2 = nibbleQueue.Dequeue();

      var d1 = nibbleQueue.Dequeue();
      var d2 = nibbleQueue.Dequeue();

      return $"{m}{c}{d}{y}-{m1}{m2}-{d1}{d2}";
    }

    private List<DateTime> ReadNibbleDateList(Queue<Nibble> nibbleQueue, int count)
    {
      var dateList = new List<DateTime>();

      for (var i = 0; i < count; i++)
      {
        var dateString = ReadNibbleDateString(nibbleQueue);
        if (String.IsNullOrEmpty(dateString) == false)
        {
          var date = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
          dateList.Add(date);
        }
      }

      return dateList;
    }

    private List<string> ReadNibbleDateStringList(Queue<Nibble> nibbleQueue, int count)
    {
      var dateList = new List<string>();

      for (var i = 0; i < count; i++)
      {
        var dateString = ReadNibbleDateString(nibbleQueue);
        if (String.IsNullOrEmpty(dateString) == false)
        {
          dateList.Add(dateString);
        }
      }

      return dateList;
    }
  }
}
