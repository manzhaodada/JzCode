using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace JZ计算机软件开发语言
{
    internal class specialdatabase
    {
        public static String[] englishbase =
        {
            "Scanner","new Scanner(System.in)","next",
            "nextInt","nextFloat","nextDouble","Math.max",
            "Math.min","Random","new Random()","nextInt"
        };

        public static String[] Chinesebase =
        {
            "#输入流","#创建输入流","#取文本输入",
            "#取整数输入","#取单精度浮点输入",
            "#取双精度浮点输入","求最大","求最小",
            "随机数","创建随机数","取随机数"
        };

        public static String[] specialFunctionName =
        {
            "#循环后判断"
        };

        public static String lineSpecialOrFunction(String Line)
        {
            //此函数负责对比Line内是否存在特殊函数
            int indexof = 0;
            for(int i = 0; i < specialFunctionName.Length; i++)
            {
                indexof = Line.IndexOf(specialFunctionName[i]);
                if (indexof != -1)
                {
                    return specialHandlingFunction(specialFunctionName[i],Line);
                }
            }
            return null;
        }

        public static String specialHandlingFunction(String Case,String Line)
        {
            //此方法负责处理特殊函数，传入Case跳转至对应的处理函数
            switch (Case)
            {
                case "#计次循环":
                    int i;
                    return null;
                default:
                    return null;       
            }  
        }


    }
}

