using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JZ计算机软件开发语言
{
    internal class database
    {
        public static String[] englishData = {"public class", "public " +
                "static void main(String[] args)","public", 
            "private","byte","char","int","long","float",
            "double","String","boolean","if","else if",
            "else","while","for","System.out.println",
            "System.out.print"
        };
        //todo "for(int i = 0;i <= x;i++)"--计次循环（满足传入x参数与可以返回当前计次个数的变量)
        //do while--循环后判断
        //需要后期重构

        public static String[] chineseData = {"#主类", "#程序入口","#公开",
            "#私有","#字节型","#字符型","#整数型","#长整数型"
                ,"#单精度小数型","#双精度小数型","#文本型",
            "#布尔型","#如果","#否则如果","#否则",
            "#判断后循环","#变量循环","#打印并换行","#打印"

        };
        
    }
}
