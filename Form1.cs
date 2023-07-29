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
using JzCode;

namespace JZ计算机软件开发语言
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_Run_Click(object sender, EventArgs e)
        {
            String filePath = @"C:\JzTemp\";
            if(Directory.Exists(filePath) == true)
            {
                Directory.Delete(filePath,true);
                Directory.CreateDirectory(filePath);
                Random random = new Random();
                for(int i = 0; i <= 5; i++)
                {
                    String temp = new function().randomLetter(random.Next(26));
                    filePath = filePath + temp;
                }
                filePath = filePath + ".jz";
                File.WriteAllText(filePath,new Form1().textBox_Code.Text);
            }
            else
            {
                Directory.CreateDirectory(filePath);
                Random random = new Random();
                for (int i = 0; i <= 5; i++)
                {
                    int temp = random.Next(26);
                    filePath = filePath + temp.ToString();
                }
                filePath = filePath + ".jz";
                File.WriteAllText(filePath, new Form1().textBox_Code.Text);
            }
            String lineCode, Code = null;
            List<string> lines = new List<string>
            (File.ReadAllLines(filePath));
            int line = lines.Count;
            for (int i = 0; i < line; i++)
            {
                lineCode = lines[i];
                lines[i] = function.translateJzCode(lineCode);
            }
            for (int i = 0; i < line; i++)
            {
                Code = Code + lines[i] + "\n";
            }
            File.WriteAllText(@"D:\jz.java", Code);
            new function().cmdProcessStart(@"D:\jz.jz");
        }

        private void Button_DaBao_Click(object sender, EventArgs e)
        {
            String lineCode, Code = null;
            List<string> lines = new List<string>
            (File.ReadAllLines(@"D:\jz.jz"));
            int line = lines.Count;
            for (int i = 0; i < line; i++)
            {
                lineCode = lines[i];
                lines[i] = function.translateJzCode(lineCode);
            }
            for (int i = 0; i < line; i++)
            {
                Code = Code + lines[i] + "\n";
            }
            File.WriteAllText(@"D:\jz.java", Code);
            new function().cmdProcessStart(@"D:\jz.jz");
        }
    }
}
