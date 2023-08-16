using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace JzCode
{
    public partial class Form_Release : Form
    {
        public Form_Release()
        {
            InitializeComponent();
        }
        private string codetext = "";
        public Form_Release(string Code)
        {
            this.codetext = Code;
            InitializeComponent();
        }

        private void Form_Release_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Filter = "EXE可执行程序 (*.exe)|*.exe";
            this.saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            SaveFileDialog saveFileDialog = sender as SaveFileDialog;
            String filePath = saveFileDialog.FileName;
            this.textBox1.Text = filePath;
        }

        string systemPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+@"\RunMain.exe";

        private void button2_Click(object sender, EventArgs e)
        {
            fileHandle();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string textBoxValue = (string)this.Invoke((Func<string>)(() => textBox1.Text));

            Thread thread = new Thread(() => button3ThreadStart(textBoxValue));
            thread.Start();
        }

        private void fileHandle()
        {
            string filePath = Path.GetDirectoryName(this.textBox1.Text);
            string fileName = this.textBox1.Text.Replace(filePath, "");
            string classname = File.ReadAllText(@"C:\JzTemp\release.config");
            Directory.CreateDirectory(filePath);
            try
            {
                File.Copy(systemPath, filePath + fileName,true);
            }
            catch (IOException ex)
            {
                MessageBox.Show("编译器出错\n" + ex.Message);
            }
            File.WriteAllText(filePath + @"\release.config", classname);
            File.Copy(@"C:\JzTemp\" + classname + ".class",filePath+@"\"+classname+".class",true);
        }

        private void button3ThreadStart(string textBoxValue)
        {
            fileHandle();
            string filePath = Path.GetDirectoryName(textBoxValue);
            CopyDirectory(systemPath.Replace(@"\RunMain.exe", "") + @"\sdk", filePath);
        }
        private static void CopyDirectory(string sourceDir, string destDir)
        {
            try
            {
                Directory.CreateDirectory(destDir);

                string[] files = Directory.GetFiles(sourceDir);
                string[] directories = Directory.GetDirectories(sourceDir);

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string destFile = Path.Combine(destDir, fileName);
                    File.Copy(file, destFile, true);
                }

                foreach (string directory in directories)
                {
                    string dirName = Path.GetFileName(directory);
                    string destSubDir = Path.Combine(destDir, dirName);
                    CopyDirectory(directory, destSubDir);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
