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
            "#取双精度浮点输入","#求最大","#求最小",
            "#随机数","#创建随机数","#取随机数"
        };

        public static String[] specialFunctionName =
        {
            "#计次循环","#循环后判断","#值相等"
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
            "#循环后判断"
        };

        public static String getSpeciaFuncitonBlock(String Case,String CodeBlock,String CodeBlockText,int linenumber)
        {
            try
            {
                var listData = new Dictionary<string, String>();
                listData["#计次循环"] = "for(int jcxhnzwbl = 0;jcxhnzwbl <= x;jcxhnzwbl++){";
                listData["#循环后判断"] = "do{";
                listData["#值相等"] = "ax.equals(bx)";
                String temp = listData[Case];
                if (Case==("#计次循环"))
                {
                    String x = function.takeJoins(CodeBlock)[0];
                    temp = temp.Replace("x", x);
                    return temp;
                }
                else if (Case == "#循环后判断")
                {
                    String x = function.takeMiddle(linenumber, CodeBlockText);
                    MessageBox.Show(x);
                    List<string> lines =
                    x.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    x = "";
                    for(int i = 0; i < lines.Count; i++)
                    {
                       x = x+function.translateJzCode(lines[i],i,x)+"\n";
                    }
                    MessageBox.Show(x);
                    x = function.takeMiddleorMinddle(x);
                    MessageBox.Show(x);
                    String[] joins = function.takeJoins(lines[0]);
                    joins[0] = function.translateJzCode(joins[0], 0, joins[0]);
                    MessageBox.Show(temp + x + "while(" + joins[0] + ");");
                    return temp + x + "while(" + joins[0] + ");" ;
                }
                else if(Case == "#值相等")
                {
                    Match match = Regex.Match(CodeBlock, @"#值相等\([^)]+\)");
                    string a = function.takeJoins(match.Value)[0];
                    string b = function.takeJoins(match.Value)[1];
                    temp = temp.Replace("ax", a);
                    temp = temp.Replace("bx", b);
                    //#如果(#值相等("a","b"));
                    
                    
                    CodeBlock = CodeBlock.Replace(match.Value, temp);
                    
                    return CodeBlock;
                }
                return temp;
            }
            catch(KeyNotFoundException e)
            {
                MessageBox.Show("Error:SpeciaFuncitonget所需内容不存在");
                Environment.Exit(0);
                return null;
            }
        }
       
        public static String translationOfSpecialFunction(String Code, int LineNumber)
        {
            try
            {
                bool state = false;
                String CodeHead = null;
                String CodeBlock = null;
                String CodeTemp = null;
                foreach (String temp in speciaFuncitonBlock)
                {
                    state = Code.Contains(temp);
                    if (state == true)
                    {
                        CodeTemp = function.takeMiddle(LineNumber, new Form1().getTextBox_Code());
                        CodeBlock = function.takeMiddleorMinddle(CodeTemp);
                        String pattern = @"#(.*?)\(";
                        CodeHead = Regex.Match(Code, pattern).Groups[1].Value;
                        CodeHead = CodeHead.Trim();

                        return getSpeciaFuncitonBlock(temp, Code, CodeBlock,LineNumber);
                    }
                }
                return Code;
            }
            catch (Exception ex)
            {

                MessageBox.Show("error: " + ex.Message);
                return Code; 
            }
        }

        public static string translateJzCodeBlock(string CodeBlock, int LineNumber)
        {

            try
            {
                List<string> lines =
                CodeBlock.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                int count = lines.Count;
                for (int i = LineNumber; i < count; i++)
                {
                    foreach (String temp in specialdatabase.speciaFuncitonBlock)
                    {
                        if (lines[i].Contains(temp))
                        {
                            string tempBlock = null;
                            for (int j = i; j < count; j++)
                            {
                                tempBlock = tempBlock + lines[j] + "\n";
                            }

                            tempBlock = specialdatabase.getSpeciaFuncitonBlock(temp, tempBlock, function.takeMiddleorMinddle(tempBlock),LineNumber);
                            translateJzCodeBlock(tempBlock, i);
                            return tempBlock;
                        }

                    }
                }
                return CodeBlock;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
                return CodeBlock;
            }
        }



        public static String FuncitonBlockSpecia(String CodeBlock)
        {
            try
            {
                String MinddleCodeBlock = function.takeMiddleorMinddle(CodeBlock);
                String CodeBlockNameMain = function.takeMiddleMain(CodeBlock);
                String pattern = @"\((.*?)\)";
                String CodeBlockages = Regex.Match(CodeBlockNameMain, pattern).Groups[1].Value;
                String Case = CodeBlockNameMain.Replace(CodeBlockages, "");
                Case = Case.Replace("(){}", "");
                return Case;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
                return CodeBlock;
            }
        }

    
       


    }
}

