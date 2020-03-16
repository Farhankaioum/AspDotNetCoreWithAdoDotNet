using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LoginSystemWithRSA.Security
{
    public class RSAConfiguration
    {
        private static RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;

        public RSAConfiguration()
        {
            _privateKey = rsa.ExportParameters(true);
            _publicKey = rsa.ExportParameters(false);
        }


        // If want to see your public key
        public string PublicKeyString()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _publicKey);
            return sw.ToString();
        }

        public string Encrypt(string plainText)
        {
            rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(_publicKey);

            var data = Encoding.Unicode.GetBytes(plainText);
            var cypher = rsa.Encrypt(data, false);

            return Convert.ToBase64String(cypher);

        }

        public string Decrypt(string cypterText)
        {
            var dataBytes = Convert.FromBase64String(cypterText);
            rsa.ImportParameters(_privateKey);
            var plaintext = rsa.Decrypt(dataBytes, false);

            return Encoding.Unicode.GetString(plaintext);
        }
    }
}
