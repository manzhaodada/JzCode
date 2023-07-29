using JZ计算机软件开发语言;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace JzCode
{
    internal class function
    {
        public static string translateJzCode(string lineJzCode)
        {
            for (int i = 0; i < database.chineseData.Length; i++)
            {
                string temp = database.chineseData[i];
                bool state = lineJzCode.Contains(temp);
                if (state == true)
                {
                    temp = database.getDataBase(temp);
                    lineJzCode = lineJzCode.Replace(database.chineseData[i], temp);
                }
            }
            for (int i = 0; i < specialdatabase.englishbase.Length; i++)
            {
                string temp = specialdatabase.Chinesebase[i];
                bool state = lineJzCode.Contains(temp);
                if (state == true)
                {
                    temp = specialdatabase.englishbase[i];
                    lineJzCode = lineJzCode.Replace(specialdatabase.Chinesebase[i], temp);
                }
            }
            return lineJzCode;
            //todo 后续改为跳转至特殊函数处理 (***已处理***)
        }

        public String randomLetter(int randomNext)
        {
            Random random = new Random();
            String[] letterArrays = { "a", "b", "c", "d", "e", 
                "f", "g", "h", "i", "j", "k", "l", "m", "n" ,
                "o","p","q","r","s","t","u","v","w","x","y","z"};
            return letterArrays[randomNext];
        }

        public static String getMainClassName()
        {
            //todo 存在bug——如果#主类后没有{可能也会get (***已修复***)
            String codeTemp = new Form1().getTextBox_Code();
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
                        if(temp.Equals(" ") || temp.Equals(null))
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

        public void cmdProcessStart(String Path)
        {
            Path = Path.Replace(".jz", ".java");
            if (Path.Contains("jz") == true)
            {

                if (File.Exists(Path) == true)
                {
                    Process cmdProcess = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();

                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/k "+  "echo jz计算机软件编程语言由得快科技开发" +
                        "制作 & echo 若发现bug请联系QQ:2241105683"+ "& java -Dfile.encoding=UTF-8 " + Path ;

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
