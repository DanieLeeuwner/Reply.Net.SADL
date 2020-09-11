using NUnit.Framework;
using System;
using System.Linq;

namespace Reply.Net.SADL.Tests
{
  public class Tests
  {
    /// <summary>
    /// Raw licence data for JH SCHOEMAN
    /// </summary>
    private const string licenceRawHex_SCHOEMAN = "019b094500006713c45dcaf37a7573c2daed9badf4374e3629389f8a6bf3c65104384407f66f3233d4bab833052c89dd15cc80f930691ff8fcc77bf0494ea408b5ec0465a9b88580c95c46cc21349fca03a10b95778cbd9672c53d150e4aae7cbf9b61bdadcd3c69f11033236d2e1227f69f49db401a85a7021a547c616b7038337238d37d1086bb3c730c737faac164bae9cdaf0273904fa875799078779660f87fbf0a28af53739ac87c39758100fd9bcc3bedc8716c35212b7f911add64c43281bd18eb12f1bc7f01249f14ecf7e6e8ed2e2c66f32f0716aef21b5b7febd889457632caaa878b6d253d5d4f038de565df54b29c8b3dd87714780c0e8c027b1577d9375f76c2ee868bf2760e27dd2ac4b1611ac2f0695884d2eb80b00c2cd52505a3f3acf58a2306a47172519e67dd3c5ac1e6e49f8535fec93e8d9b322ef9ba2cc5b092ede4a45fa0ba7c014463e860d6faf105ebec9baee6a3255c343a0cc9974b379a7d70b346986688513ef22493fb4d1ae8443cd721827f7eec933891e07f471cc2e197c11ea82233d32c666f70802dae71d2bafcec2550f7458d40c23f0c0f3d9452253d9ae6c79bd7d9592fa744c1c3c40b945f48693ca1b447675987ea51d631bed99c63865b25a8446ff2259a454f3707646c27812f175410c930c82c60c3126755650c5d137dcd15b286c32dcccbb3074d667299144df44cc4eb24675e8827835a581ef384754c2c007d720d82c89c7608cac15437b060f2ef9a97547fca3adbad61da1e9b0db9061fe83c10cf326e92b8b7e787ef8e45ffc056e100a504ae09578fdcedd0d3abf1286c776e7a4bfd02b93f34e9121bf66f17a77a4db36ba04032a553892d1c3f6c377be4f7696357bebaa983ea3166592c8afc35282ae55aba64d83f7436aa0b4b58670855a28f2e14e67b2e84411e39b7fe19cd68be5ca411b9f60a9f0339e19ae8bda50223fb4930df0b8a20ed4528a601296a814721245db392b6afa929f1d547c3";

    /// <summary>
    /// Raw licence data for DC JANSEN
    /// </summary>
    private const string licenceRawHex_JANSEN = "019b0945000060e9a27a1e475f89af17cb3a5ae86bd91152d22fbca1f462ba8bd39e9341bb26ad82e3ab1b4e68069247b75286edb64fed476c1a444bba7ffeca8ac9ddcf629ac582a865b8eefec682f33de40e68a6fc08265df86ad058376db56e4a9d2a2da285d11f408493fb4706abc346632a156b517c87b58372a3afda8be11dbae4a8f5a3d1881559ce0c69acd026770fd9e75c28c4abd0b2796e70eca894d8546bd8456bdcd9af0aa112f78b56523471b46805ae844a66108ba2f12a28180e5ddded48436239eae212c6193d351201e6a24613d2482ddae552bf7e21729246667a9cd5c730ba4b80736586f5f90894b1017fcea7b299cd6241a266c4b7b28b704de1f0266025358d134686e1cac2ba9af74ea16b9524f4fca20792d2618beec8499dbab596f5a56531f26462c996dad5e1278400a5266c5b5aed2dd3cae135821148bdfd3d564ac127c90d3a125031cb263e63879dfdd10725dc80225a700e5d259a5a1d2340d7bd2d3fecc9b078e1726661e2c51b945b56d762fa3067f2b926dd2707a74e1535abf1a7ee09b0ec40689cd3b3b9995dfdda9b5f33482f1441afa48304a68b24b942464d0334d4b20c17a6f09d5db119257212f7b2d48860e22c64924dc53c03557624a020b175ef47553946ce2e60c3f2ba95865d73b2d440c2fd3eb3c9850447c249c869444dbc103d8197c5556355e6c46455b61aa0b176bcc73290b532726d4339bb35b144fecf5c5d4c5e90aef23fc9c9375ae7206f9ea5a22255365f7e0728e3360cb78562c805a4f9a2b4a674eb5af30b598b98b16227a223505e2e013e426296e8f3d1ff03d6506fa431162438afabbc4e8b9626e8821d6bd264ff9d35a2df90e2be27e0297c162a0a6bbc937f349d31413484b52ca27255b2263abde2793cb946387685de9662777cdf6808a18c3a25759ed4e6ab6410f7881b655421be87e3f3df100062f12ae806059ca3e11248557080a64e4ec6e0a46697cae4c5c5197e3defdc";

    private byte[] HexToByteArray(string hex)
    {
      return Enumerable.Range(0, hex.Length)
                       .Where(x => x % 2 == 0)
                       .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                       .ToArray();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestLicenceDecryption_SCHOEMAN()
    {
      var driversLicenceService = new DriversLicenceService();

      var encryptedLicenceBytes = HexToByteArray(licenceRawHex_SCHOEMAN);

      Assert.AreEqual(720, encryptedLicenceBytes.Length, "Licence length is expected to be 720 bytes");

      var decryptedLicenceBytes = driversLicenceService.DecryptLicence(encryptedLicenceBytes);

      var licence = driversLicenceService.DecodeLicence(decryptedLicenceBytes);

      Assert.AreEqual("SCHOEMAN", licence.Surname);
      Assert.AreEqual("JH", licence.Initials);

      Assert.AreEqual("20220006T6S6", licence.LicenceNumber);

      Assert.AreEqual(new DateTime(2020, 10, 25), licence.LicenceExpiryDate);
    }
  }
}