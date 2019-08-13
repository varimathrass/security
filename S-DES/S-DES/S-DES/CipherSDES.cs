using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_DES
{
    class CipherSDES
    {
        private String text;
        private String key1;
        private String key2;
        private String action;
        private String IPtext;
        private String mainLeftPart;
        private String mainRightPart;
        private String leftPart;
        private String rightPart;
        private String finalLeftPart;
        private String finalRightPart;
        private String resText;

        private String[,] S0 = new String[4, 4] { {"01", "00", "11", "10" },
                                                  {"11", "10", "01", "00" },
                                                  {"00", "10", "01", "11" },
                                                  {"11", "01", "11", "10" }};

        private String[,] S1 = new String[4, 4] { {"00", "01", "10", "11" },
                                                  {"10", "00", "01", "11" },
                                                  {"11", "00", "01", "00" },
                                                  {"10", "01", "00", "11" }};


        public CipherSDES(String text, String key1, String key2)
        {
            this.text = text;
            this.key1 = key1;
            this.key2 = key2;
        }

        public String makeAction(String action)
        {
            this.action = action;

            //Round 1
            IPtext = IP();
            startRound(1);
            //Round2
            mainLeftPart = String.Copy(finalRightPart);
            mainRightPart = String.Copy(finalLeftPart);

            startRound(2);

            resText = String.Concat(finalLeftPart, finalRightPart);
            resText = inverseIP();

            return resText;
        }      

        private void startRound(int round)
        {
            String val1 = "";
            String val2 = "";

            if (action == "Encrypt")
            {
                val1 = key1;
                val2 = key2;
            }
            else
            if (action == "Decrypt")
            {
                val1 = key2; 
                val2 = key1; 
            }

            if (round == 1)
            {
                mainLeftPart = IPtext.Substring(0, 4);
                mainRightPart = IPtext.Substring(4, 4);
            }

            finalRightPart = mainRightPart;

            mainRightPart = EP();
                   
            if (round == 1)
            {
                mainRightPart = CipherSupport.bitXor(mainRightPart, val1);
            }
            else 
            if (round == 2)
            {
                mainRightPart = CipherSupport.bitXor(mainRightPart, val2);
            }

            leftPart = mainRightPart.Substring(0, 4);
            rightPart = mainRightPart.Substring(4, 4);

            Dictionary<String, int> bitConvert = new Dictionary<String, int>();
            bitConvert["00"] = 0;
            bitConvert["01"] = 1;
            bitConvert["10"] = 2;
            bitConvert["11"] = 3;

            int S0_ROW = bitConvert[leftPart[0].ToString() + leftPart[3].ToString()];
            int S0_COL = bitConvert[leftPart[1].ToString() + leftPart[2].ToString()];
            int S1_ROW = bitConvert[rightPart[0].ToString() + rightPart[3].ToString()];
            int S1_COL = bitConvert[rightPart[1].ToString() + rightPart[2].ToString()];

            leftPart = S0[S0_ROW, S0_COL];
            rightPart = S1[S1_ROW, S1_COL];

            mainRightPart = String.Concat(leftPart, rightPart);
            mainRightPart = P4();

            finalLeftPart = CipherSupport.bitXor(mainLeftPart, mainRightPart);
        }       

        private String IP()
        {
            char[] bitArr = text.ToCharArray();
            char[] tempBitArr = new char[bitArr.Length];
            Array.Copy(bitArr, tempBitArr, bitArr.Length);

            bitArr[0] = tempBitArr[1];
            bitArr[1] = tempBitArr[5];
            bitArr[2] = tempBitArr[2];
            bitArr[3] = tempBitArr[0];
            bitArr[4] = tempBitArr[3];
            bitArr[5] = tempBitArr[7];
            bitArr[6] = tempBitArr[4];
            bitArr[7] = tempBitArr[6];

            return new String(bitArr);
        }

        private String EP()
        {
            char[] bitArr = new char[8];
            char[] tempBitArr = mainRightPart.ToCharArray();

            bitArr[0] = tempBitArr[3];
            bitArr[1] = tempBitArr[0];
            bitArr[2] = tempBitArr[1];
            bitArr[3] = tempBitArr[2];
            bitArr[4] = tempBitArr[1];
            bitArr[5] = tempBitArr[2];
            bitArr[6] = tempBitArr[3];
            bitArr[7] = tempBitArr[0];

            return new String(bitArr);
        }

        private String P4()
        {
            char[] bitArr = mainRightPart.ToCharArray();
            char[] tempBitArr = new char[bitArr.Length];
            Array.Copy(bitArr, tempBitArr, bitArr.Length);

            bitArr[0] = tempBitArr[1];
            bitArr[1] = tempBitArr[3];
            bitArr[2] = tempBitArr[2];
            bitArr[3] = tempBitArr[0];

            return new String(bitArr);
        }

        private String inverseIP()
        {
            char[] bitArray = resText.ToCharArray();
            char[] tempBitArray = new char[bitArray.Length];
            Array.Copy(bitArray, tempBitArray, bitArray.Length);

            bitArray[0] = tempBitArray[3];
            bitArray[1] = tempBitArray[0];
            bitArray[2] = tempBitArray[2];
            bitArray[3] = tempBitArray[4];
            bitArray[4] = tempBitArray[6];
            bitArray[5] = tempBitArray[1];
            bitArray[6] = tempBitArray[7];
            bitArray[7] = tempBitArray[5];

            return new String(bitArray);
        }
    }
}
