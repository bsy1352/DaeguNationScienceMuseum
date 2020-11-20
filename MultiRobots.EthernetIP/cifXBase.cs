using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiRobots.EthernetIP
{
    public class cifXBase
    {
        private cifXUser cifXUser = new cifXUser();

        public string SetLastError(UInt32 lError)
        {
            string sError = null;

            if (lError == 0)
            {
                sError = string.Format("0x{0:X8}", lError);
                return sError;
            }
            else
            {
                byte[] szBuffer = new byte[1024];
                UInt32 ulSize = 1024;
                UInt32 lret = 0;
                sError = string.Format("0x{0:X8}", lError);

                lret = cifXUser.xDriverGetErrorDescription(lError, ref szBuffer, ulSize);
                sError += "\r\n" + ByteArrayToString(szBuffer);
                return sError;
            }
        }

        public string ByteArrayToString(byte[] arr)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(arr);
        }

        public byte[] CreateOutputData(string sTemp, bool bAutoInc)
        {
            //delete all existing blanks
            sTemp = sTemp.Replace(" ", "");

            int iLen = sTemp.Length;
            if (iLen > 0)
            {
                //insert new blanks
                for (int i = 2; i <= iLen; i += 3)
                {
                    sTemp = sTemp.Insert(i, " ");
                    iLen++;
                }

                //split the string into an array of string
                string[] arTemp = sTemp.Split(new Char[] { ' ' });
                //create a new array for the byte data
                byte[] pvData = new byte[arTemp.Length - 1];

                //convert each value of the textfield to a corresponding hexadecimal value
                int iIndex = 0;
                foreach (string s in arTemp)
                {
                    pvData[iIndex] = Convert.ToByte(int.Parse(s, System.Globalization.NumberStyles.AllowHexSpecifier));
                    if (bAutoInc == true)
                        pvData[iIndex]++;
                    iIndex++;
                    if (iIndex == pvData.Length)
                        break;
                }
                sTemp = "";

                return pvData;
            }
            byte[] pvNullData = new byte[0];
            return pvNullData;
        }
    }
}
