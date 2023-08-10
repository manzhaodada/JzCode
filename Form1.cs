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
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Contexts;

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
        private static String improt = "import java.util.*;\nimport java.math.*;\nimport java.io.*;\n";

        private void button_Run_Click(object sender, EventArgs e)
        {

            String filePath = @"C:\JzTemp\";

            // 检查文件路径是否存在
            if (Directory.Exists(filePath) == true)
            {
                // 如果文件路径存在，则先删除该路径下的所有文件和文件夹
                Directory.Delete(filePath, true);

                // 创建文件路径
                Directory.CreateDirectory(filePath);

                // 生成随机文件名
                Random random = new Random();
                for (int i = 0; i <= 5; i++)
                {
                    String temp = new function().randomLetter(random.Next(26));
                    filePath = filePath + temp;
                }

                // 将文件名扩展为".jz"
                filePath = filePath + ".jz";

                // 将文本框中的代码写入文件
                File.WriteAllText(filePath, this.textBox_Code.Text, Encoding.UTF8);
            }
            else
            {
                // 如果文件路径不存在，则创建文件路径
                Directory.CreateDirectory(filePath);

                // 生成随机文件名
                Random random = new Random();
                for (int i = 0; i <= 5; i++)
                {
                    int temp = random.Next(26);
                    filePath = filePath + temp.ToString();
                }

                // 将文件名扩展为".jz"
                filePath = filePath + ".jz";

                // 将文本框中的代码写入文件
                File.WriteAllText(filePath, this.textBox_Code.Text);
            }

            String lineCode, Code = null;

            // 读取文件的所有行
            List<string> lines = new List<string>(File.ReadAllLines(filePath, Encoding.UTF8));
            int line = lines.Count;

            // 对每一行代码进行翻译
            for (int i = 0; i < line; i++)
            {
                lineCode = lines[i];
                lines[i] = function.translateJzCode(lineCode,i);
            }

            // 将翻译为源文件后的代码拼接成String
            for (int i = 0; i < line; i++)
            {
                Code = Code + lines[i] + "\n";
            }

            // 拼接improt引用库和代码部分
            String codeTemp = improt + Code;

            File.WriteAllText(@"D:\jz.java", codeTemp);

            // 启动cmd进程执行代码文件
            new function().cmdProcessStart(@"D:\jz.jz");
        }

        private void Button_DaBao_Click(object sender, EventArgs e)
        {
            String lineCode, Code = null;
            lineCode = File.ReadAllText(@"D:\jz.jz");
            List<string> lines =
            lineCode.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            int line = lines.Count;
            int on, off = 0;
            bool bracesEqualsState = false;
            for (int i = 0; i < line; i++)
            {
                lineCode = lines[i];
                lines[i] = function.translateJzCode(lineCode,i);
            }
            for (int i = 0; i < line; i++)
            {
                Code = Code + lines[i] + "\n";
            }
            for (int i = 0; i < line; i++)
            {

            }
            //已知一个文本被以行分割装在了lines的一个list内，一个int变量linenum控制当前为第多少行，现在需要完成从该行开始，寻找
            String codeTemp = improt + Code;
            File.WriteAllText(@"D:\jz.java", codeTemp);
            new function().cmdProcessStart(@"D:\jz.jz");
        }
        private void button_Open_Click(object sender, EventArgs e)
        {
            this.openFileDialog_Open.Filter = "Jz Code (*.jz)|*.jz";
            this.openFileDialog_Open.FileName = null;
            this.openFileDialog_Open.ShowDialog();
        }

        private void openFileDialog_Open_FileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog openFileDialog = sender as OpenFileDialog;
            String filePath = openFileDialog.FileName;
            this.textBox_Code.Text = null;
            this.textBox_Code.Text = File.ReadAllText(filePath, Encoding.UTF8);
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            String mainClassName = function.getMainClassName(this.textBox_Code.Text);
            if (mainClassName==("error: 主类名称未设置") ||
                mainClassName==("error:代码内未找到主类"))
            {
                MessageBox.Show(mainClassName);
            }
            else
            {
                this.saveFileDialog_Save.Filter = "Jz Code (*.jz)|*.jz";
                this.saveFileDialog_Save.FileName = mainClassName+".jz";
                this.saveFileDialog_Save.ShowDialog();
            }

        }

        private void saveFileDialog_Save_FileOk(object sender, CancelEventArgs e)
        {
            SaveFileDialog saveFileDialog = sender as SaveFileDialog;
            String filePath = saveFileDialog.FileName;
            File.WriteAllText(filePath, getTextBox_Code());
        }

        public bool setTextBox_Code(String Content)
        {
            if (Content==(this.textBox_Code.Text))
            {
                String temp = this.textBox_Code.Text;
                this.textBox_Code.Text = Content;
                if (temp!=(Content))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public String getTextBox_Code()
        {
            this.textBox_Code.Update();
            return this.textBox_Code.Text.ToString();
        }

        private void button_Teaching_Click(object sender, EventArgs e)
        {
            //String temp = "";
            //temp = specialdatabase.translationOfSpecialFunction(getTextBox_Code(), 0);
            //MessageBox.Show(temp);
            function.test();

        }


        public Form1 getForm1()
        {
            Form1 tempForm = this;
            return tempForm;
            
        }
    }
}
