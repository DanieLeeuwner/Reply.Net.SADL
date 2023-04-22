## Drivers Licence Scanning

### Data

The barcode on the back of a South African drivers licence contains 720 bytes encoded into a PDF417 barcode format. The first 4 bytes contains a version number that should be used to decide which of the encryption keys to use. All currently valid licences use version 2 for decryption.

| Version bytes | Version   |
| ------------- | --------- |
| `01 e1 02 45` | Version 1 |
| `01 9b 09 45` | Version 2 |

Following the version number are 2 bytes containing zeros, i.e. `00 00`. The remaining 714 bytes contains the encrypted drivers licence data.

### Decryption

The data should be divided into 6 blocks. The first 5 blocks should each be 128 bytes, and the remaining block should be 74 bytes. Each of the licence data blocks should be decrypted using **RSA ENCRYPT** with the specific licence version public key. 

The licence holder details are contained within the first block.

**Version 1 - 128 bytes**

```
-----BEGIN RSA PUBLIC KEY-----
MIGXAoGBAP7S4cJ+M2MxbncxenpSxUmBOVGGvkl0dgxyUY1j4FRKSNCIszLFsMNw
x2XWXZg8H53gpCsxDMwHrncL0rYdak3M6sdXaJvcv2CEePrzEvYIfMSWw3Ys9cRl
HK7No0mfrn7bfrQOPhjrMEFw6R7VsVaqzm9DLW7KbMNYUd6MZ49nAhEAu3l//ex/
nkLJ1vebE3BZ2w==
-----END RSA PUBLIC KEY-----
```

**Version 1 - 74 bytes**

```
-----BEGIN RSA PUBLIC KEY-----
MGACSwD/POxrX0Djw2YUUbn8+u866wbcIynA5vTczJJ5cmcWzhW74F7tLFcRvPj1
tsj3J221xDv6owQNwBqxS5xNFvccDOXqlT8MdUxrFwIRANsFuoItmswz+rfY9Cf5
zmU=
-----END RSA PUBLIC KEY-----
```

**Version 2 - 128 bytes**

```
-----BEGIN RSA PUBLIC KEY-----
MIGWAoGBAMqfGO9sPz+kxaRh/qVKsZQGul7NdG1gonSS3KPXTjtcHTFfexA4MkGA
mwKeu9XeTRFgMMxX99WmyaFvNzuxSlCFI/foCkx0TZCFZjpKFHLXryxWrkG1Bl9+
+gKTvTJ4rWk1RvnxYhm3n/Rxo2NoJM/822Oo7YBZ5rmk8NuJU4HLAhAYcJLaZFTO
sYU+aRX4RmoF
-----END RSA PUBLIC KEY-----
```

**Version 2 - 74 bytes**

```
-----BEGIN RSA PUBLIC KEY-----
MF8CSwC0BKDfEdHKz/GhoEjU1XP5U6YsWD10klknVhpteh4rFAQlJq9wtVBUc5Dq
bsdI0w/bga20kODDahmGtASy9fae9dobZj5ZUJEw5wIQMJz+2XGf4qXiDJu0R2U4
Kw==
-----END RSA PUBLIC KEY-----
```

### Interpretation

Refer to this repository for interpretation information: https://github.com/ugommirikwe/sa-license-decoder

### Usage

```csharp
// Byte array contianing raw licence bytes from barcode 
var encryptedLicenceBytes = ...; 

// Decrypt and decode licence fields
var driversLicenceService = new DriversLicenceService();
var decryptedLicenceBytes = driversLicenceService.DecryptLicence(encryptedLicenceBytes);
var licence = driversLicenceService.DecodeLicence(decryptedLicenceBytes);

// Read fields from licence object
var surname = licence.Surname;
var initials = licence.Initials;
```

### Samples

**Raw barcode data (base64)**

*Licence holder: JH Schoeman*

```
AZsJRQAAZxPEXcrzenVzwtrtm630N042KTifimvzxlEEOEQH9m8yM9S6uDMFLIndFcyA+TBpH/j8x3vwSU6kCLXsBGWpuIWAyVxGzCE0n8oDoQuVd4y9lnLFPRUOSq58v5thva3NPGnxEDMjbS4SJ/afSdtAGoWnAhpUfGFrcDgzcjjTfRCGuzxzDHN/qsFkuunNrwJzkE+odXmQeHeWYPh/vwoor1Nzmsh8OXWBAP2bzDvtyHFsNSErf5Ea3WTEMoG9GOsS8bx/ASSfFOz35ujtLixm8y8HFq7yG1t/69iJRXYyyqqHi20lPV1PA43lZd9UspyLPdh3FHgMDowCexV32TdfdsLuhovydg4n3SrEsWEawvBpWITS64CwDCzVJQWj86z1iiMGpHFyUZ5n3Txawebkn4U1/sk+jZsyLvm6LMWwku3kpF+gunwBRGPoYNb68QXr7Juu5qMlXDQ6DMmXSzeafXCzRphmiFE+8iST+00a6EQ81yGCf37skziR4H9HHMLhl8EeqCIz0yxmb3CALa5x0rr87CVQ90WNQMI/DA89lFIlPZrmx5vX2Vkvp0TBw8QLlF9IaTyhtEdnWYfqUdYxvtmcY4ZbJahEb/IlmkVPNwdkbCeBLxdUEMkwyCxgwxJnVWUMXRN9zRWyhsMtzMuzB01mcpkUTfRMxOskZ16IJ4NaWB7zhHVMLAB9cg2CyJx2CMrBVDewYPLvmpdUf8o6261h2h6bDbkGH+g8EM8ybpK4t+eH745F/8BW4QClBK4JV4/c7dDTq/EobHduekv9Ark/NOkSG/ZvF6d6TbNroEAypVOJLRw/bDd75PdpY1e+uqmD6jFmWSyK/DUoKuVaumTYP3Q2qgtLWGcIVaKPLhTmey6EQR45t/4ZzWi+XKQRufYKnwM54ZrovaUCI/tJMN8LiiDtRSimASlqgUchJF2zkravqSnx1UfD
```

**Raw barcode data (base64)**

*Licence holder: DC Jansen*

```
AZsJRQAAYOmieh5HX4mvF8s6Wuhr2RFS0i+8ofRiuovTnpNBuyatguOrG05oBpJHt1KG7bZP7UdsGkRLun/+yorJ3c9imsWCqGW47v7GgvM95A5opvwIJl34atBYN221bkqdKi2ihdEfQIST+0cGq8NGYyoVa1F8h7WDcqOv2ovhHbrkqPWj0YgVWc4MaazQJncP2edcKMSr0LJ5bnDsqJTYVGvYRWvc2a8KoRL3i1ZSNHG0aAWuhEpmEIui8SooGA5d3e1IQ2I56uISxhk9NRIB5qJGE9JILdrlUr9+IXKSRmZ6nNXHMLpLgHNlhvX5CJSxAX/Op7KZzWJBombEt7KLcE3h8CZgJTWNE0aG4crCupr3TqFrlST0/KIHktJhi+7ISZ26tZb1pWUx8mRiyZba1eEnhAClJmxbWu0t08rhNYIRSL39PVZKwSfJDToSUDHLJj5jh5390Qcl3IAiWnAOXSWaWh0jQNe9LT/sybB44XJmYeLFG5RbVtdi+jBn8rkm3ScHp04VNavxp+4JsOxAaJzTs7mZXf3am18zSC8UQa+kgwSmiyS5QkZNAzTUsgwXpvCdXbEZJXIS97LUiGDiLGSSTcU8A1V2JKAgsXXvR1U5Rs4uYMPyupWGXXOy1EDC/T6zyYUER8JJyGlETbwQPYGXxVVjVebEZFW2GqCxdrzHMpC1MnJtQzm7NbFE/s9cXUxekK7yP8nJN1rnIG+epaIiVTZffgco4zYMt4ViyAWk+aK0pnTrWvMLWYuYsWInoiNQXi4BPkJilujz0f8D1lBvpDEWJDivq7xOi5Ym6IIda9Jk/501ot+Q4r4n4Cl8FioKa7yTfzSdMUE0hLUsonJVsiY6veJ5PLlGOHaF3pZid3zfaAihjDoldZ7U5qtkEPeIG2VUIb6H4/PfEABi8SroBgWco+ESSFVwgKZOTsbgpGaXyuTFxRl+Pe/c
```

**Decrypted Licence (base64)**

*Licence holder: JH Schoeman*

```
AQIDBAUCjhoDADcBGoJRQeBFQuHhU0NIT0VNQU7gSkjhWkHgWkHgMOAw4eEyMDIyMDAwNlQ2UzbgNjIwNTA0NTEzNjA4MQIZmBIHGZgSB6oAoBGWIFBCAVEBAgIBAlAaV0kEAPoAyEMcKEAAMCAAMUUOMQ+mGSMUy07AWsq8665ivyPwu692As4ev1uG20NiUk4ogWq55kKW7AgkmPkLEw7oU/fm5pAtw4Vcn8PUtrv32t+6K8cxB5wbED+RDw0S8NPhVzyuDc0Zo0PVfsmz/6wo1HkP6650njT7bZ1rkScpFECgPKvQ7uM9seGWjK0tpp3iBpx03tQnXT3D2ulICKLo5lqATYVu0/CQhCfakgzMxjkG/ZMn9PFEAI2xW8S1h55KDI/SEnz0V0ESVteL7NFtpKnuuQWTzRvsiNvbFwAwR/P0PWNIgS4042n+T2VPbdU4pZSSeWGlDhDFuyf8Oph5UlBxbhftQX6ITm86ujbfS46skC9NJ4eu4g70DY3td4wJyU6zTR7xPCnYVDJxTMuhaIvidWy9Piz27RR04A5Ah/pF6VhcVQQYxmB6hv/ToRbcQ806OMR1OcLQozHCv4+TECQ8hQhZY84GJdiDQ6l99R4gbuWRnRgYgMwglna+5p8xM/V+YlH8bdR8sqBAo6/fZBpylxYKyKE7Txa87dcQIDBAUJMQjkLNxZHKmXiPJvC6wWfEVFqS+byHubOFGYOOYoHG5XAUkqrV+jGWdbRkUBblgcD3rsAIITEHCHLJI1A1FAj/AUQoIkOkBmQQtHGaEEb3nxVWMWFh9DCGDca8OFIBo0PHAXcIoQggwKIHAO4REyxbbP4OsYW2gJaityBAYAAgd3d3PeW9Zed4zjnxQdAN0FsiIrGDSDEFXVgoI3aBXefkEGqKCKqUtAQLF4iKCqAqqioiqqqqpKigqBOEFBBQUQFCQAAAAAAAAAAA
```

**Decrypted Licence (base64)**

*Licence holder: DC Jansen*

```
AQIDBAUCSxQDADIBFoJaQuHh4UpBTlNFTuBEQ+FaQeBaQeAw4eHhNDE1NjAwMDAyNTIy4DkyMDYxODUwMzYwODgCIBEFEKqgCgEZkgYYIBEFESAWBRABV0kEAPoAyEL4KEAAMAQQQeks05IuWW8xHDBOaW7HBBGaee+5DBIKQqdmb9n/QLkkyF7XfI/LQXyTqDJCarANWRAYxn8OAJ2SI4I662lUDSFOpPm/GfE7lg9+tffbLKOEYjhGnwSdDfGWlRcoeHWnDvEXuooRCI2FWnBmRARmQvxuDJeVoPJKvZo/j2AZAyBQ/0sERkYTNV5FP9TtTA1izrGDsozB844gygQIDBAUAyJ8qAo6NzhSlJKXLuN4TxM9LsNT/yqQ2sFJuTeykvCz5K4pZrWYNH/Y+pRK27F9h1IGJamVqcgc4wFkL/pEG9codK20WCOFe6XJZwVTxfWesAhwAHkEArz+wBOkOz0+Qit4AS+eUOkLsxHhP4Hdsx217E54pXEbsnFLUV+fyucQegA+vKcNkWrxlIFQqHHdy2I7lviM9aq5p+g0lpeddkhB0mi0C2r4p04KM1Puehlz40TX/0GQdqZ4nDAjr5wMeXtSP0o24wqKyNYDCqGTOOAnjNzeWK8pvus/bVoL1Dzd+7wKS5hSvmyiHupI5q7ou+obES0OAf2s6EcJ7MjaAOhgdm1hjP9o36fMDVXZNPRYoaaWZisdV1XsoLX9V+48dDctBitB1lNjPoCuagsp+/5G0oR6RAMGVXGrNGa8G4xOpL0XC14l28ab8pMVqRwrQmp4AQPHmGFC7c6pC9+9Waxa6s+GOIEtN2MbzA2GCQ8CS6OEB47xi4ririBAYAAg44HjmiKcDh4iNVRBUzJMKA6AAyoi1rkghVaEVoArRjRQ7tkzDMSSisCIKQKqEjtBAEEBgQEFFEAZQEAAEBVUEQUUAAAAAAAAAAAA
```

**Decrypted Licence (base64)**

*Licence holder: J Sanders*

```
Am0WAwAyARaCWkLh4eFTQU5ERVJT4ErhWkHgWkHgMOHh4TYyMzQ1NjAwMDFBQuA4NjA5MTM1MTM5MDEyAiAHAhmqoQoBGYYJEyASERQgFxIUAVdJBAD6AMhDLihAAChCNFWnQrzo0jDnNeVd2987O9NM9v45zkHm/ll7Ivb5cBQx7vfrFr3e975/m97of/HDQKGCh/LRixwt8OqE5KhYXoNDmoVPDQKFWdbMIXtUo8+40zll0RmpShJw785Ys/luDajQ1gRRksEkr7FjiKHRMO4wpyFBGMJfagh7iuNxHOEMJFrGOBpRg5UETHrDTx0mx9RmWuHQjFA+Naa7abq64z0qL5B3ktR5oFuN4rtWXoZeql6MiPthbIY+vGEUN5mPczaChm0UvGwwkQckiCBP+LSAAVexEcEgAKywW4F+62PhksQeG6Zx/0fCjSQUBWD4fEwVwSnz5ARYJAVimcsOERK2EkQ0kXYSqR2NGSxZ9JY1ADb8Y2aC1hQPh/F/NXq+j/IVV3ratkIa1ETAaPu91/FhMgl1MdUWp5yn8UVVm+wFiWW1IC/UsvkUOkKHhSYCaMqt5loAOXGqumWXwtoOK4QcqHboj8RwAqatEPFrOGDu9ChjRI9HpjPpgvTsB3VGApj8Q8RXRPC8zi097mSBVqhNcGcO2vSTKjjIPC5yG+ISRwvFBBnEW9tarAR3lZ98IAMIQgu0VTRca4lsqeIB8xjXXuw1l24bRDiPYi5JEwAlWO09CP+G+6jYdgVLBYskCWGukFDX2g2NLBGjvYB47rCucJ8Yhgjdik1EAgNiKFN3KCCS6qCixBMkBqrbd0FnPWnX3eYFjOWqLbVoEAUARFpF1RRFEBRZS4YgoqogABY7UAEAFFAFVVUBBEVAVVQQAAAAAAAAAAAAAAAA
```

### Links

https://www.b4x.com/android/forum/threads/rsa-encryption-decryption.89436/

https://stackoverflow.com/questions/17549231/decode-south-african-za-drivers-license

https://github.com/ugommirikwe/sa-license-decoder

https://www.b4x.com/android/forum/threads/solved-need-assistance-with-rsa-decryption.118756/
