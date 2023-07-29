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
            for (int i = 0; i < database.englishData.Length; i++)
            {
                string temp = database.chineseData[i];
                bool state = lineJzCode.Contains(temp);
                if (state == true)
                {
                    temp = database.englishData[i];
                    lineJzCode = lineJzCode.Replace(database.chineseData[i], temp);
                }
            }
            return lineJzCode;
        }

        public String randomLetter(int randomNext)
        {
            Random random = new Random();
            String[] letterArrays = { "a", "b", "c", "d", "e", 
                "f", "g", "h", "i", "j", "k", "l", "m", "n" ,
                "o","p","q","r","s","t","u","v","w","x","y","z"};
            return letterArrays[randomNext];
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
                    startInfo.Arguments = "/k java " + Path + " & echo jz计算机" +
                        "软件编程语言由得快科技开发制作 & echo 若发现bug请联系QQ:2241105683";

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
