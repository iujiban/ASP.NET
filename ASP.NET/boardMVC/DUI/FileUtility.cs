using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.ComponentModel;


namespace DUI
{ // 게시판의 파일 업로드 기능을 구현할 때 중복된 파일명에 번호를 붙임
    public class FileUtility
    {
        //파일 처리 관련 기본 유틸리티
        public static string GetFileNameWithNumbering(string dir,  string name)
        { 
            //순수파일명
            string strName = Path.GetFileNameWithoutExtension(name);
            // 확장자 : .txt
            string strExt = Path.GetExtension(name);
            bool blnExists = true;
            int i = 0;
            
            while (blnExists)
            {
                //Path.Combine(경로, 파일명) = 경로 + 파일명
                //해당하는 똑같은 파일이 있으면 (1)이라는 숫자를 붙여줘라.
                if (File.Exists(Path.Combine(dir, name)))
                {
                    name = strName + "(" + ++i + ")" + strExt; 
                }
                else
                {
                    blnExists = false;
                }
            }
            return name;
        } 
    }
}
