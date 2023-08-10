using JZ计算机软件开发语言;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Diagnostics.PerformanceData;

namespace JzCode
{
    internal class function
    {
        public static string translateJzCode(string lineJzCode,int lineNumber)
        {
            //翻译模块，将中文代码翻译为代码源文件
            String tempCode = lineJzCode;
            string ReturnTranslationOfSpecialFunction = "";
            for (int i = 0; i < database.chineseData.Length; i++)
                //翻译基础语句
            {
                string temp = database.chineseData[i];
                bool state = lineJzCode.Contains(temp);
                if (state == true)
                {
                    temp = database.getDataBase(temp);
                    lineJzCode = lineJzCode.Replace(database.chineseData[i], temp);
                }
            }

            foreach (String i in specialdatabase.speciaFuncitonBlock)
            {
                bool state = lineJzCode.Contains(i);
                if (state == true)
                {
                    ReturnTranslationOfSpecialFunction = specialdatabase.getSpeciaFuncitonBlock(i, lineJzCode, "");
                    return ReturnTranslationOfSpecialFunction;
                }
            }

            for (int i = 0; i < specialdatabase.englishbase.Length; i++)
            {
                //翻译特殊语句
                string temp = specialdatabase.Chinesebase[i];
                bool state = lineJzCode.Contains(temp);
                if (state == true)
                {
                    temp = specialdatabase.englishbase[i];
                    lineJzCode = lineJzCode.Replace(specialdatabase.Chinesebase[i], temp);
                }
            }

            ReturnTranslationOfSpecialFunction = lineJzCode;
            if (tempCode==lineJzCode)
            {
                return lineJzCode;
            }
            else
            {
                return lineJzCode;
            }
            //todo 后续改为跳转至特殊函数处理 (***已处理***)
        }

        public static String translation(String Code)
        {
            // 循环遍历特殊函数名称数组
            foreach (var functionName in specialdatabase.specialFunctionName)
            {
                // 当代码中包含特殊函数时执行循环
                while (Code.Contains(functionName))
                {
                    // 获取特殊函数的起始索引和结束索引
                    int startIndex = Code.IndexOf(functionName);
                    int endIndex = Code.IndexOf("}", startIndex);

                    // 计算特殊函数代码块的长度，并提取代码块
                    int blockLength = endIndex - startIndex + 1;
                    String block = Code.Substring(startIndex, blockLength);

                    // 提取特殊函数的参数值和内部代码块
                    String caseValue = block.Substring(0, block.IndexOf("("));
                    String innerCode = block.Substring(block.IndexOf("{") + 1, block.Length - block.IndexOf("{") - 2);

                    // 初始化中间代码块为空字符串以备后用
                    String codeBlockMiddle = "";
                    int innerStartIndex = 0;
                    int innerEndIndex = 0;

                    // 如果内部代码块包含特殊函数，则提取中间代码块
                    if (innerCode.Contains(functionName))
                    {
                        innerStartIndex = innerCode.IndexOf(functionName);
                        innerEndIndex = innerCode.IndexOf("}", innerStartIndex);
                        int innerBlockLength = innerEndIndex - innerStartIndex + 1;
                        codeBlockMiddle = innerCode.Substring(innerStartIndex, innerBlockLength);
                    }

                    // 调用特殊函数处理函数，并替换原始代码中的特殊函数代码块
                    String translatedBlock = specialdatabase.getSpeciaFuncitonBlock(caseValue, innerCode, codeBlockMiddle);
                    Code = Code.Replace(block, translatedBlock);
                }
            }

            // 返回翻译后的代码
            return Code;
        }
        public static String takeMiddleMain(String CodeBlock)
        {
            //取出指定代码块的函数名与参数
            String temp = null;
            MatchCollection matches = Regex.Matches(CodeBlock, @"\{([^}]+)\}");
            foreach (Match match in matches)
            {
                temp += match.Groups[1].Value;
            }
            temp = takeMiddleorMinddle(CodeBlock);
            temp = CodeBlock.Replace(temp, "");
            temp = temp.Replace("{", "").Replace("}", "");
            temp = temp.Trim();
            return temp;
        }


        public static String[] takeJoins(String Code)
        {
            // 取参数 takeMiddleMain处理后的数据由takeJoins取出参数，并将每个参数存入一个String[]并返回

            // 计算参数个数
            int count = takeJoinsNumber(Code);

            // 创建参数数组
            String[] Joins = new String[count];

            // 提取参数内容
            int startIndex = Code.IndexOf('(') + 1;
            int endIndex = Code.IndexOf(')');
            String content = Code.Substring(startIndex, endIndex - startIndex);

            int StartIndex = 0;
            int EndIndex = 0;
            int numbers = 0;
            while (EndIndex < content.Length)
            {
                // 判断是否遇到参数分组符——逗号
                if (content[EndIndex] == ',')
                {
                    // 提取参数值并存入数组
                    String value = content.Substring(StartIndex, EndIndex - StartIndex);
                    Joins[numbers] = value;
                    numbers++;
                    StartIndex = EndIndex + 1;
                }

                EndIndex++;
            }

            // 添加最后一个值
            String lastValue = content.Substring(StartIndex, EndIndex - StartIndex);
            Joins[numbers] = lastValue;
            numbers++;

            return Joins;
        }

        public static int takeJoinsNumber(string Code)
        {
            //分隔参数内的,返回逗号分隔符的个数
            int count = 0;
            int index = Code.IndexOf(",");

            while (index != -1)
            {
                count++;
                index = Code.IndexOf(",", index + 1);
            }

            return count + 1; 
            // 加上最后一个值
        }

        public static String takeMiddleorMinddle(String CodeBlock)
        {
            String tempCodeBlock = null;
            //取出指定代码块内{}内的内容
            MatchCollection matches = Regex.Matches(CodeBlock, @"\{([^}]+)\}");
            foreach (Match match in matches)
            {
                tempCodeBlock += match.Groups[1].Value;
            }
            tempCodeBlock += "}";
            return tempCodeBlock;

        }

        public static String takeMiddle(int Linenum, String CodeText)
        //Linenum 从第X行开始读取花括号 CodeText整体代码文本
        //此功能目前在开发过程内存在争议，部分支持直接return花括号内的内容，部分支持放回整个函数组，目前开发以返回整个函数块为目标开发
        {
            //todo: 此处存在一个BUG，若CodeText内存在有空白行，空白行不会被加入进Lines内，可能会存在当前的line与实际需要读取的line行数
            //并不相同的bug,后续将lines的读取增加一个判定，为其添加一个判断当前行是否为空白行的功能。

            //将代码文本按照换行符分割成一个字符串列表
            List<string> lines = CodeText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //用来记录当前的花括号层数
            int braceLevel = 0;
            //定义字符串列表用来存储提取出的代码块
            List<string> block = new List<string>();
            //从指定的行号遍历字符串列表
            for (int i = Linenum; i < lines.Count; i++)
            {
                string line = lines[i];
                // 判断是否遇到左花括号
                if (line.Contains("{"))
                {
                    //将花括号层数加一
                    braceLevel++;
                }
                if (line.Contains("}"))
                {
                    //将花括号层数减一
                    braceLevel--;
                }
                block.Add(line);
                // 判断是否已经结束当前的代码块
                if (braceLevel <= 0)
                {
                    break;
                }
            }
            //将新的字符串列表连接成一个字符串，并返回
            return string.Join("\n", block.ToArray());

        }

        public static bool equaldBraces(String Line)
        {
            // 检查参数Line是否和特殊函数名称匹配的上

            // 遍历特殊函数名称数组
            for (int i = 0; i < specialdatabase.specialFunctionName.Length; i++)
            {
                // 检查行是否与特殊函数名称匹配
                if (Line == specialdatabase.specialFunctionName[i])
                {
                    // 匹配成功，返回true
                    return true;
                }
            }

            // 未找到匹配的特殊函数名称，返回false
            return false;
        }

        public string randomLetter(int randomNext)
        {
            Random random = new Random();
            String[] letterArrays = { "a", "b", "c", "d", "e", 
                "f", "g", "h", "i", "j", "k", "l", "m", "n" ,
                "o","p","q","r","s","t","u","v","w","x","y","z"};
            return letterArrays[randomNext];
        }

        public static String getMainClassName(String CodeText)
        {
            //todo 存在bug——如果#主类后没有{可能也会get (***已修复***)
            String codeTemp = CodeText;
            List<String> lines = new List<String>(codeTemp.Split('\n'));
            int start,end = 0;
            for(int i = 0; i < lines.Count; i++)
            {
                if (lines[i].IndexOf("#主类 ") != -1)
                {
                    start = lines[i].IndexOf("#主类 ");
                    start += "#主类 ".Length;
                    if (lines[i].IndexOf("{",start) != -1)
                    {
                        end = lines[i].IndexOf("{", start);
                        String temp = lines[i].Substring(start,end-start);
                        if(temp==(" ") || temp==(null))
                        {
                            return "error:主类名称未设置";
                        }
                        else
                        {
                            return temp;
                        }
                    }
                }
            }
            return "error:代码内未找到主类";
        }
        public static void test()
        {
        }

        public void cmdProcessStart(String Path)
        {
            // 处理cmd进程启动

            // 将路径中的文件扩展名从".jz"替换为".java"
            Path = Path.Replace(".jz", ".java");

            // 检查路径是否包含"jz"
            if (Path.Contains("jz") == true)
            {
                // 检查文件是否存在
                if (File.Exists(Path) == true)
                {
                    // 创建cmd进程
                    Process cmdProcess = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();

                    // 设置cmd进程的启动信息
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/k " + "echo jz计算机软件编程语言由得快科技开发" +
                        "制作 & echo 若发现bug请联系QQ:2241105683" + "& java -Dfile.encoding=UTF-8 " + Path;

                    startInfo.RedirectStandardOutput = false;
                    startInfo.UseShellExecute = true;
                    startInfo.CreateNoWindow = false;

                    cmdProcess.StartInfo = startInfo;
                    cmdProcess.Start();
                }
                else
                {
                    MessageBox.Show("您选择的代码文件不存在");
                }
            }
            else
            {
                Path = @"C:\temp\jz\jz.jz";
                MessageBox.Show("未能正确识别的jz文件\n请检查文件后缀名是否正确");
            }
        }
    }

}
