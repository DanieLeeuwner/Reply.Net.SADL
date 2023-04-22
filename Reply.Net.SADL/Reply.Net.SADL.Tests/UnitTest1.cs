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

        /// <summary>
        /// Raw licence data for B WHITE
        /// </summary>
        private const string licenceRawHex_WHITE = "019b09450000a2abb033685dcb5ff6166a28b5eb036166b2676362b8cb57cbe7f4d3b8075468db5633247f5e565ec1e774e746a4adc3e0a573a28c0d74f69b77c3027d62b3e256dcbe68d25e3c339da35b0290d89a559efcbc1a68be6a1fd5dfa7daebfe8d1ce60b11bd1bb82aa1c900e975dfd3ea056b77d35be221c307acc44c3148347ec180c65020f41d6348993f9ccebde625fec13c83a67c87222613c6167a559eacebddef6a93a70eba7b7073ce460313e8a814bbca5bdd4e65ec5a7a99da44964c48b151d1c8616f0ba2e7e8c11ab2ce9023339e1c0b00aca0bccc0be1471a77182893ae5318bf6bd3820d70561c438137adcf1fc3e4e9b623d1aabacc7a0a0128c98b5540ff346c51799de9ae6416bcc76aafeed683f10cb0e70ccaab2c676a107606e65fc00beea17a4eb6f12d2ba8af5050b70f6e0481533c4d06ada62cd84624db40cd4e3dd7451aefd7cfbed8d4f69d671b71833b4823c11d9e26aa4538b6ccb80c9a3200e0c73bd9e7841dd7dd9af9ca452ead93bcd88ff577170a6f063edf3d8918c11ebf24534b7f8328bbd2b174dcd70b1eecac2cc8da15710881f95e6a8ea22a7de7aeb07b82cbdd6c630fc76e3c1d94a419182558ae0428c57dbc8d52192683c1deda6b6bff76d792b0dc86e8582acc078c70ec2fa331ed90533cddf29c6d572ff79175ffb16eb5a2f00761d79b6acc226effe42f4a7c373a87065cac26e2631fb3b78a8a338de553f8a9fb954c7bf864ff45c5a239625fbf74ce0db2ab2c3a171c6ce17e7e2f0e5b37ef95cc3861a524f2bb12d46e09e0feb7a18a2e236cc162d455e8775e72a2f3f655acfd57f2b31fe0ddc182520aeab060459d41e4da7f0e4ed468cf3e96b41bfc83f8ee956626dde22c4c5fb26c91351b73dd35afcb1a963872cb187ff2ec3cc653cfbeb97a738be31d40f094eec1b1805812cdd19d1cb36704105ba1804497fc7df8de66323dc275a5265008a33a2d53ab23c9fb55e9fb34f8621932b4";

        /// <summary>
        /// Raw licence data for JE KITLEY
        /// </summary>
        private const string licenceRawHex_KITLEY = "019b09450000c07d3c8821b740587748fcbe6e6a28140f1d14cefd32d4817c1018d814e2a8244b579ca6f686ce6bb8280c5dadc9eea2621fdb51f64938f66ca2f2a75256d9ecaf14b4b8cef7290289dae91734ba70016fabf013d9f10fda610a0797b753db2e7906cc7417ffbfc45b90b6cf5d35b3ccb117a34af670a11eb3f7a8016f741405b2b3e942961132a728ab8d692acb2bdaddebda9987de7d50f8528df90f1061e5ba550605b560b7121a710f31c4b5447520bfa2b5fb6c146099ab7bc2b7a2a676a609eeee12711586df182133aef77bfe1c415cb74271dec882e002f7ac257744300a172579544940b39e7d39e21412bbf9da3490217661ad191d64337f4c2525681042b86a9bd268b85fdce8a9670f6d69772e04d06fcaf7cc9eefacd8903ac4c9681bde496c042bb0454cf24bb052961d5edd677116329ad51621cb81183b95a6af6aae00c38372c2dd7197e14b41292acd9d2fe4cfcb44127908c6f7fb89f2fe6d5419c5daae25435f2471e61d982bb1e51b2736f0c7070a81fc554f50aaf0703a3e2769946f7a40097b3847e6d55ce9ffd2d0f7db3a256b76eff48f3a75414a880d8978ed80bf48ef508e6acb8cb2e59c1a8d446494b330c8ce9aedfc99d9f527ac5704f7634a09621ec4576e5d24ea3f9169336c747af52106ea5acfeecc0b6d7f8d74e31ddea20c18f830529586d4015f41e380277f69e9c79c4ec9e62a4135c3eb401595aca79040760133d3698ddfa9c24dd519527caca9ef268d0029ef423cf6512306c419fce0e23f4bfec2b77154ef481f94fb6efbd28837c593cd5b818884eb83cf57628b958d88659e8145a8c51e0c582df8a3a999cf8d93f15421603f23103909984452e75ac19c31c07e6b2c70d56c4024dcfa5b8e67cd01db2dc8415930ca193a2dc02746e5a7693fcc1676b7177f859032bd79cbe985161199283688e5a63bfab628b52701fcf714b397253a5dde3a3e152814d4dda4b708c15af6b1747b0235d225";

        /// <summary>
        /// Raw licence data for JH SCHOEMAN
        /// </summary>
        private const string licenceRawHex_SCHOEMAN_OLD = "019b094500007cdcb8576ac0f92810ba68bce85d9b5cf4b76af287fe720bf1da17319046c9f844bd13bcdbb748e7b80891540bdfbbcb0b7f77aee2791edd331e831e45a6061986ed68cfa1110383a58e41a663ee202253a9c4c9b5ff369e2ed5c45e43250f97326bb3a399f8e581c407413fa48cb6b0b9d793410be17b6c9f19b9771022acda24be7b46e959f6e76ca8a845ecfee5da4abdb4852daf5ca12e8323d7299c204279dfa0e6b524441c1717407f7e91c0a7de349b9083c979b657983ae0f3b858fdd195068b2bd5f888bd955a0b617edc50e947b301a47465a6d9a5efde9c0dc0c05e8c6ecf84cf577754209d433e572f9a5cc32485f3f7aa529c576cd2371beef401d91734ecd50ac2b35f736c9652b0c53c758c48f685b360ff3892582ee148dda4e86221aac59ed267c43ac6ca3732e345382f3860ed117900519f698b68d394056c210c95ed38b3d1acab8b543a5751bc78b2f9840b69fe4e7baf823238683a92d789834838cbb7522a128bd386e3905b58a5dde243eaa5a8ded4881a8c923d51b102c55e3ca784afdff0d389e318d495f770505f74b911de71e3e920ff8d59082879c2f1f0c0de3ab9416d0c1805febe5f5bd46f99b6762654f6a3d1343d08dadfc879b221693d0cb5dd770df71a04b91c5c3f8268239e785c5018d4a87d6b6307b60cbed43c01dcfd15f0608da1b9f35aa2d02be07b75976efced38946abd8488c1f46e7c54e440cf561740afbd920b193ab633c21510f949ea905b3f42c47f905fe7caa246aa6b9ac14530b3a72d54ba7a4ad2973fac82019a361e1105ec5b261ca497c053266878d6fe14a56da5178840a3f11e46ba03469e4dd5cf280c956eb2156162a4ae7fc797bef428bb45693ea1a5885727adb2d237390cc8e8c63c74fa2c89c7212c692e72a7e0be0562463d07001ae8345db510256d9011951fd8d66cb4e0c2d228f323ca6afba185f7e454dc8d3621aa992a73e061c90c2b2be426d5a19c1205b53b41";

        /// <summary>
        /// Raw licence data for ZA KRIGE
        /// </summary>
        private const string licenceRawHex_KRIGE = "019b094500004a4ed27390d1ff21376edcb5c897ea27df8d37a2ffe1c0fc6b2d53631ca7730634ff1bb52336981eca4ebf1367b5eaf0577f067357128d9ae02763757a5e829cbf3bfdd0572d6e0b1585defdf6eee6347759295e1dfeab22ed32ecc62c903c507f384c4bf907e081bd9ae446602432ffcd7c42ee9498c274105db739a96f836e6b9471a933e7580dd79ffc1f013c92aa0b967f21a4261bb700e2ed7d631c7a7d1b5b1171403e38d1e01674a8073b0e99e50379a260e3521dc5c245cba11020d564e1de4bd22f0d5ca9899af02641ec6361e889fcb805c43c3a11bc9d14229b4382c3d5bfd044aa2f0ef59b4cae5ae6c41adacde6d1fc1060a6226d4c4ddd19196d729141646ba8b116ec99173a66f0b721ba83ae0e62f30438a1795b8914a3252e0233fdb398d7d03e20dcaa093287b877e4241c1cb9f27a2bc6ceae0a50033092966282a3b41a0a465e18791b1bbed823e920537912fa6bdbab75f4484579b5fe9370d1c8c4403c7710e65f1bccd7692922f3eababc9ff03fda72a586ac67f14af6be9b40eed0e1079314093816480c60804a6094b664f79cf37150c7b875a2676734f58cfa560ccc7337edb5c986e48656684ca301b8674c908fe8b61b35e406317d26c57dbf51ecfcef82ddb72bf91bcfe5ad877a56c44390f3c125a8a73887aadc2d4a58bfda069f6c156a70c618ad3cd8b623e55435fd4b90a744de844e15753b851eae20c513f7a64c636756f11648824c0d47f178ca59ca67ee1dbecbb9fc59539ff05021314b23d2027c4a7d76a905b3b05ad3d208544218b36583c92e747ebc03f1c0f1957e68a97cfc07b7baff7d60bd7db36b0d7e404bb11683cbcd331eb794d0d7a3ab5e2966f6a79109f3f1a5c178b74faf2a551dcbb570b068442afafbe9ff620dac66c74f81b84bee0cdfd78ee1b242358adb6b045d0dda06b13fda74cce0eccaa7e2233a14ad39a91420d30455d6ebdb0dd218f802ae8b45c5ef69827c8ef71071b7";


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

        private DriversLicence DecryptDriversLicence(String input)
        {
            var driversLicenceService = new DriversLicenceService();

            var encryptedLicenceBytes = HexToByteArray(input);

            Assert.AreEqual(720, encryptedLicenceBytes.Length, "Licence length is expected to be 720 bytes");

            var decryptedLicenceBytes = driversLicenceService.DecryptLicence(encryptedLicenceBytes);

            var licence = driversLicenceService.DecodeLicence(decryptedLicenceBytes);

            return licence;
        }

        [Test]
        public void TestLicenceDecryption_JANSEN()
        {
            var licence = DecryptDriversLicence(licenceRawHex_JANSEN);

            Assert.AreEqual("JANSEN", licence.Surname);
            Assert.AreEqual("DC", licence.Initials);

            Assert.AreEqual("415600002522", licence.LicenceNumber);

            Assert.AreEqual(new DateTime(2016, 05, 10), licence.LicenceExpiryDate);
        }

        [Test]
        public void TestLicenceDecryption_SCHOEMAN()
        {
            var licence = DecryptDriversLicence(licenceRawHex_SCHOEMAN);

            Assert.AreEqual("SCHOEMAN", licence.Surname);
            Assert.AreEqual("JH", licence.Initials);

            Assert.AreEqual("20220006T6S6", licence.LicenceNumber);

            Assert.AreEqual(new DateTime(2020, 10, 25), licence.LicenceExpiryDate);
        }

        [Test]
        public void TestLicenceDecryption_WHITE()
        {
            var licence = DecryptDriversLicence(licenceRawHex_WHITE);

            Assert.AreEqual("WHITE", licence.Surname);
            Assert.AreEqual("B", licence.Initials);

            Assert.AreEqual("20550003DDZM", licence.LicenceNumber);

            Assert.AreEqual(new DateTime(2023, 04, 13), licence.LicenceExpiryDate);
        }

        [Test]
        public void TestLicenceDecryption_SCHOEMAN_OLD()
        {
            var licence = DecryptDriversLicence(licenceRawHex_SCHOEMAN_OLD);

            Assert.AreEqual("SCHOEMAN", licence.Surname);
            Assert.AreEqual("JH", licence.Initials);

            Assert.AreEqual("20610009T951", licence.LicenceNumber);

            Assert.AreEqual(new DateTime(2015, 10, 25), licence.LicenceExpiryDate);
        }

        [Test]
        public void TestLicenceDecryption_KITLEY()
        {
            var licence = DecryptDriversLicence(licenceRawHex_KITLEY);

            Assert.AreEqual("KITLEY", licence.Surname);
            Assert.AreEqual("JE", licence.Initials);

            Assert.AreEqual("604600029DPG", licence.LicenceNumber);

            Assert.AreEqual(new DateTime(2014, 02, 10), licence.LicenceExpiryDate);
        }

        [Test]
        public void TestLicenceDecryption_KRIGE()
        {
            var licence = DecryptDriversLicence(licenceRawHex_KRIGE);

            Assert.AreEqual("KRIGE", licence.Surname);
            Assert.AreEqual("ZA", licence.Initials);

            Assert.AreEqual("6039000267CS", licence.LicenceNumber);

            Assert.AreEqual(new DateTime(2017, 10, 11), licence.LicenceExpiryDate);
        }
    }
}