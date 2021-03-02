using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUI
{
    public static class StringLibrary
    {//문자열의 길이를 잘라주는 유틸리티 메서드를 관리
        //주어진 문자열을 주어진 길이만큼만 잘라서 반환. 나머지 부분은 '...'을 붙임.
        public static string CutString(this string strCut, int intChar)
        {
            if (strCut.Length > (intChar -3))
            {
                return strCut.Substring(0, intChar - 3) + "...";
            }
            return strCut;

       }
        //유니코드 이모티콘을 포함한 문자열을 자르기
        public static string CutStringUnicode(this string str, int length)
        {
            string result = str;

            var si = new System.Globalization.StringInfo(str);
            var l = si.LengthInTextElements;

            if (l > (length -3))
            {
                result = si.SubstringByTextElements(0, length - 3) + "...";
            }
            return result;

        }
       
    }
}
