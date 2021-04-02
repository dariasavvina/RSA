using System;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace RSAv2
{
    public class PublicKey
    {
        public BigInteger E { get; private set; }

        public BigInteger N { get; private set; }

        public PublicKey(BigInteger e, BigInteger n)
        {
            this.E = e;
            this.N = n;
        }

    }
    
    public class PrivateKey
    {
        public BigInteger d { get; }

        public BigInteger n { get; }

        public PrivateKey(BigInteger d, BigInteger n)
        {
            this.d = d;
            this.n = n;
        }

    }


    public class RSA
    {
        private BigInteger p { get; }

        private BigInteger q { get; }

        private BigInteger e { get; }


        public RSA(BigInteger p, BigInteger q)
        {
            this.p = p;
            this.q = q;
            e = new BigInteger(17);
        }

        public Tuple<PublicKey, PrivateKey> GenerateKeys()
        {
            var n = p * q;
            var phi = (p - 1) * (q - 1);
            var d = new BigInteger(1);
            var compare = new BigInteger(1);
            while (((e * d) % phi) != compare)
            {
                
                d++;
                
            }
            return Tuple.Create(new PublicKey(e, n), new PrivateKey(d, n));
        }

        public BigInteger Encrypt(PublicKey key, BigInteger encryptMessage)
        {
            return encryptMessage.Pow(key.E) % key.N;
        }

        public BigInteger Decrypt(PrivateKey key, BigInteger decryptedMessage)
        {
            return decryptedMessage.Pow(key.d) % key.n;
        }

        public BigInteger[] EncryptText(PublicKey key, string text)
        {
            var bytes = text.Select(sym => (byte) sym).ToArray();
            var cryptBytes = bytes
                .Select(bt => Encrypt(key, new BigInteger(bt)))
                .ToArray();
            return cryptBytes;
        }

        public string DecryptText(PrivateKey key, BigInteger[] decryptedMessage)
        {
            var decrypt = decryptedMessage
                .Select(bg => (byte) BigInteger.ToInt32(Decrypt(key, bg)))
                .Select(bt => (char) bt)
                .ToArray();
            return string.Join("", decrypt);
        }
    }
}