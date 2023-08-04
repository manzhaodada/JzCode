using JzCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
                    return null;//specialHandlingFunction(specialFunctionName[i],Line);
                }
            }
            return null;
        }

        public static String[] speciaFuncitonBlock =
        {
            "#计次循环"
        };

        public static String getSpeciaFuncitonBlock(String Case)
        {
            try
            {
                var listData = new Dictionary<string, String>();
                listData["#计次循环"] = "for(int i = 0;i < x;i++)";
                return listData[Case];
            }
            catch(KeyNotFoundException e)
            {
                MessageBox.Show("Error:SpeciaFuncitonget所需内容不存在");
                Environment.Exit(0);
                return null;
            }
        }

        public static String FuncitonBlockSpecia(String CodeBlock)
        {
            String MinddleCodeBlock = function.takeMiddleorMinddle(CodeBlock);
            String CodeBlockNameMain = function.takeMiddleMain(CodeBlock);
            String pattern = @"\((.*?)\)";
            String CodeBlockages = Regex.Match(CodeBlockNameMain, pattern).Groups[1].Value;
            String Case = CodeBlockNameMain.Replace(CodeBlockages,"");
            Case = Case.Replace("(){}", "");
            return Case;
        }

    
       


    }
}

