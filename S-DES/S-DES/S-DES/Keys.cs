using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_DES
{
    public class Keys
    {
        private String mainKey;
        private String leftPart;
        private String rightPart;
        private String key1;
        private String key2;

        public Keys(String mainKey)
        {
            this.mainKey = mainKey;
        }

        public String Key1
        {
            get => key1;
        }
        public String Key2
        {
            get => key2;
        }

        public void generateRoundKeys()
        {
            P_10();

            leftPart = mainKey.Substring(0, 5);
            rightPart = mainKey.Substring(5, 5);

            leftPart = LS('L',1);
            rightPart = LS('R',1);
            key1 = P_8();

            leftPart = LS('L', 2);
            rightPart = LS('R', 2);
            key2 = P_8();
        }
   
        private void P_10()
        {
            char[] bitArr = mainKey.ToCharArray();
            char[] tempBitArr = new char[bitArr.Length];
            Array.Copy(bitArr, tempBitArr, bitArr.Length);

            bitArr[0] = tempBitArr[2];
            bitArr[1] = tempBitArr[4];
            bitArr[2] = tempBitArr[1];
            bitArr[3] = tempBitArr[6];
            bitArr[4] = tempBitArr[3];
            bitArr[5] = tempBitArr[9];
            bitArr[6] = tempBitArr[0];
            bitArr[7] = tempBitArr[8];
            bitArr[8] = tempBitArr[7];
            bitArr[9] = tempBitArr[5];

            mainKey = new String(bitArr);
        }

        private String LS(char part, int shift)
        {
            char[] bitArr = new char[5];

            if (part == 'L')
            {
                bitArr = leftPart.ToCharArray();

            }
            else
            if (part == 'R')
            {
                bitArr = rightPart.ToCharArray();
            }

            char[] tempBitArr = new char[bitArr.Length];
            Array.Copy(bitArr, tempBitArr, bitArr.Length);

            if (shift == 1)
            {                
                for (uint i = 0; i < bitArr.Length - 1; i++)
                {
                    bitArr[i] = tempBitArr[i + 1];
                }

                bitArr[4] = tempBitArr[0];                                              
            }
            else if (shift == 2)
            {
                for (uint i = 0; i < bitArr.Length - 2; i++)
                {
                    bitArr[i] = tempBitArr[i + 2];
                }

                bitArr[3] = tempBitArr[0];
                bitArr[4] = tempBitArr[1];
            }

            return new String(bitArr);
        }

        private String P_8()
        {
                String roundKey = String.Concat(leftPart, rightPart);

                char[] bitArr = roundKey.ToCharArray();
                char[] tempBitArr = new char[bitArr.Length];
                Array.Copy(bitArr, tempBitArr, bitArr.Length);

                bitArr[0] = tempBitArr[5];
                bitArr[1] = tempBitArr[2];
                bitArr[2] = tempBitArr[6];
                bitArr[3] = tempBitArr[3];
                bitArr[4] = tempBitArr[7];
                bitArr[5] = tempBitArr[4];
                bitArr[6] = tempBitArr[9];
                bitArr[7] = tempBitArr[8];
                bitArr[8] = '\0';
                bitArr[9] = '\0';

                return new String(bitArr);
        }
    }
}
