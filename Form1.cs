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
using System.Security.Principal;
using System.Threading;

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
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            bool isAdminSystem = principal.IsInRole(WindowsBuiltInRole.Administrator);
            //if (isAdminSystem == false)
            //{
            //    MessageBox.Show("JZ全中文编程辅助工具V1.0当前为非管理员权限运行,稍后程序将尝试以管理员身份启动JZ全中文编程辅助工具V1.0,若启动失败\n请右击JZ全中文编程辅助工具V1.0软件图标，使用管理员身份运行");
            //    string filepath = System.IO.Directory.GetCurrentDirectory();
            //    filepath += @"\JZ全中文编程辅助工具V1.0.exe";
            //    ProcessStartInfo psi = new ProcessStartInfo(filepath);
            //    psi.Verb = "runas";
            //    try
            //    {
            //        Process.Start(psi);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("无法获取管理员权限：" + ex.Message);
            //        Process process = Process.GetCurrentProcess();
            //        process.Kill();
            //    }
            //    finally
            //    {
            //        Process process = Process.GetCurrentProcess();
            //        process.Kill();
            //    }
            //}


        }
        private static String improt = "import java.util.*;\nimport java.math.*;\nimport java.io.*;\n";
        private void skipnull()
        {
            List<string> lines =
            getTextBox_Code().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var skipnull = lines.Where(line => !IsNullOrWhiteSpace(line));
            setTextBox_Code(string.Join(Environment.NewLine, skipnull.ToArray()));
        }
        private static bool IsNullOrWhiteSpace(string value)
        {
            return value == null || value.Trim() == string.Empty;
        }

        private void button_Run_Click(object sender, EventArgs e)
        {
            skipnull();
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
                String codetemp = getTextBox_Code();
                codetemp = codetemp.Replace("（","(");
                codetemp = codetemp.Replace("）", ")");
                codetemp = codetemp.Replace("；", ";");
                setTextBox_Code(codetemp);
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
                lines[i] = function.translateJzCode(lineCode, i, getTextBox_Code());
            }


            // 将翻译为源文件后的代码拼接成String
            for (int i = 0; i < line; i++)
            {
                Code = Code + lines[i] + "\n";
            }

            // 拼接improt引用库和代码部分
            String codeTemp = improt + Code;

            File.WriteAllText(@"C:\JzTemp\run.java", codeTemp);

            // 启动cmd进程执行代码文件
            new function().cmdProcessStart(@"C:\JzTemp\run.java");
        }

        private void Button_DaBao_Click(object sender, EventArgs e)
        {
            string classname = function.getMainClassName(getTextBox_Code());
            if (Directory.Exists(@"C:\JzTemp") == true)
            {
                if (File.Exists(@"C:\JzTemp\run.java") == true)
                {
                    try
                    {
                        File.Move(@"C:\JzTemp\run.java", @"C:\JzTemp\" + classname + ".java");
                    }
                    catch(IOException ex)
                    {
                        MessageBox.Show("编译文件失败\n" + ex.Message);
                        if (Directory.Exists(@"C:\JzTemp"))
                        {
                            try
                            {
                                Directory.Delete(@"C:\JzTemp");
                            }
                            catch (IOException ioex)
                            {
                                MessageBox.Show("编译过程出现错误\n" + ioex.Message);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("编译出现错误\n" + ex.Message);
                    }
                    Thread thread = new Thread(threadCMD);
                    thread.Start(classname);
                    File.WriteAllText(@"C:\JzTemp\release.config", classname);
                }
                else
                {
                    MessageBox.Show("编译文件不存在，请运行一次后再次尝试编译");
                }
            }
            else
            {
                MessageBox.Show("Cache文件不存在，请运行一次后再次尝试编译");
            }
        }

        private void threadCMD(object join)
        {
            try
            {
                string classname = join.ToString();
                Process cmdProcess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();

                string filepath = System.IO.Directory.GetCurrentDirectory();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/k " + filepath + @"\sdk\bin\javac.exe" + " -encoding UTF-8 " + @"C:\JzTemp\" + classname + ".java";
                //startInfo.Arguments = "/k " +  "javac" + " -encoding UTF-8 " + @"C:\JzTemp\" + classname + ".java";
                startInfo.RedirectStandardOutput = false;
                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = false;

                cmdProcess.StartInfo = startInfo;
                cmdProcess.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            String codetemp = getTextBox_Code();
            codetemp = codetemp.Replace("（", "(");
            codetemp = codetemp.Replace("）", ")");
            codetemp = codetemp.Replace("；", ";");
            setTextBox_Code(codetemp);
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            skipnull();
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
            String codetemp = getTextBox_Code();
            codetemp = codetemp.Replace("（", "(");
            codetemp = codetemp.Replace("）", ")");
            codetemp = codetemp.Replace("；", ";");
            setTextBox_Code(codetemp);
            File.WriteAllText(filePath, codetemp);
        }

        public bool setTextBox_Code(String Content)
        {
            if (Content != this.textBox_Code.Text)
            {
                String temp = this.textBox_Code.Text;
                this.textBox_Code.Text = Content;
                if (temp!=Content)
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


        public Form1 getForm1()
        {
            Form1 tempForm = this;
            return tempForm;
            
        }

        private void button_Release_Click(object sender, EventArgs e)
        {
            skipnull();
            new Form_Release(getTextBox_Code()).Show();
        }

        private void textBox_Code_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
