using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_DES
{
    public static class CipherSupport
    {
        public static bool isBit(char value)
        {
            if (value == '0' || value == '1')
            {
                return true;
            }
            else return false;
        }

        public static String bitXor(String str1, String str2)
        {
            char[] res = new char[str1.Length];

            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] == str2[i])
                {
                    res[i] = '0';
                }
                else
                {
                    res[i] = '1';
                }
            }

            return new String(res);
        }
    }
}
