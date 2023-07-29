using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace JZ计算机软件开发语言
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String lineCode,Code=null;
            List<string> lines = new List<string>
            (File.ReadAllLines(@"D:\jz.jz"));
            int line = lines.Count;
            for(int i = 0; i < line; i++)
            {
                lineCode = lines[i];
                lines[i] = database.translateJzCode(lineCode);
            }
            for(int i = 0;i < line; i++)
            {
                Code = Code + lines[i]+"\n";
            }
            File.WriteAllText(@"D:\jz.java",Code);
            cmdProcessStart(@"D:\jz.jz");
            


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
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
