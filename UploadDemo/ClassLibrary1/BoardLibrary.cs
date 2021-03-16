using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary1
{
    public class BoardLibrary
    {
        public static string DownloadType(string strFileName, string altString)
        {
            string strFileExt = Path.GetExtension(strFileName).Replace(".", "").ToLower();
            string r = "";
            switch (strFileExt)
            {
                case "xlsx":
                    r = "<img src= '/Images/ext_xls.gif' boarder ='0' alt ='" + altString + "'>"; break;
            }
            return r;
        }
        public static string ConvertToFileSize(int intByte)
        {
            int intFileSize = Convert.ToInt32(intByte);
            string strResult = "";
            if (intFileSize >= 1048576)
            {
                strResult = string.Format("{0:F} MB", (intByte / 1048576));
            }
            else
            {
                if (intFileSize >= 1024)
                {
                    strResult = string.Format("{0} KB", (intByte / 1024));
                }
                else
                {
                    strResult = intByte + "Byte(s)";
                }

            }
            return strResult;
        }
        public static string FuncFileDownSingle(int id, string strFileName, string strFileSize)
        {
            if (strFileName.Length > 0)
            {
                return "<a href=\"/Download.aspx?Id=" + id.ToString() + "\">" + DownloadType(strFileName, strFileName + "(" +
                    ConvertToFileSize(Convert.ToInt32(strFileSize)) + ")") + "</a>";

            }
            else
            {
                return "-";
            }
        }
    }
}
