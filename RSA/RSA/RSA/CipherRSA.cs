using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSA
{
    class CipherRSA
    {
        private long p;
        private long q;
        private String message;

        public CipherRSA() { }
        public CipherRSA(long p, long q, String message)
        {
            this.p = p;
            this.q = q;
            this.message = message;
        }

        public void Encrypt(StreamWriter encrMessageFile)
        {
            long n = p * q;
            long F = (p - 1) * (q - 1);
            long e = 0;
            bool res = false;

            for (int i = 2; i < F; i++)
            {
                for (int j = 2; j <= i; j++)
                {
                    if (i % j == 0 && F % j == 0)
                    {
                        res = false;
                        break;
                    }
                    else
                    {
                        res = true;
                    }
                }

                if (res)
                {
                    e = i;
                    break;
                }
            }

            long k = 1;
            double d;

            while (true)
            {
                d = ((double)k * F + 1) / e;

                if (d % 1 == 0)
                {
                    break;
                }

                k++;
            }

            double sign = Math.Pow(double.Parse(message), d) % n;


            encrMessageFile.Write(n + "," + e + "," + message + "," + sign);
        }

        public bool checkEncryptedMessage(long n, long e, long message, long sign)
        {
            return (Math.Pow(sign, e) % n) == message;
        }
    }
}
