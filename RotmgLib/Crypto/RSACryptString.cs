using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;

namespace RotmgLib.Crypto
{
    class RSACryptString
    {
        public static string Crypt(string input)
        {
            RsaKeyParameters parameters;
            AsymmetricCipherKeyPair keyPair;

            using (var reader = File.OpenText(@"publickey.pem")) // file containing RSA PKCS1 private key
                parameters = (RsaKeyParameters)new PemReader(reader).ReadObject();

            keyPair = new AsymmetricCipherKeyPair(parameters, new AsymmetricKeyParameter(true));

            var encryptEngine = new Pkcs1Encoding(new RsaEngine());
            encryptEngine.Init(true, keyPair.Public);

            byte[] utf = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(encryptEngine.ProcessBlock(utf, 0, utf.Length));
        }
    }
}
