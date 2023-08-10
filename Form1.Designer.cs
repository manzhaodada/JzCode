namespace JZ计算机软件开发语言
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Run = new System.Windows.Forms.Button();
            this.Button_DaBao = new System.Windows.Forms.Button();
            this.button_Open = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Teaching = new System.Windows.Forms.Button();
            this.textBox_Code = new System.Windows.Forms.TextBox();
            this.openFileDialog_Open = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog_Save = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // button_Run
            // 
            this.button_Run.Location = new System.Drawing.Point(245, 1);
            this.button_Run.Name = "button_Run";
            this.button_Run.Size = new System.Drawing.Size(115, 38);
            this.button_Run.TabIndex = 0;
            this.button_Run.Text = "运行";
            this.button_Run.UseVisualStyleBackColor = true;
            this.button_Run.Click += new System.EventHandler(this.button_Run_Click);
            // 
            // Button_DaBao
            // 
            this.Button_DaBao.Location = new System.Drawing.Point(366, 1);
            this.Button_DaBao.Name = "Button_DaBao";
            this.Button_DaBao.Size = new System.Drawing.Size(115, 38);
            this.Button_DaBao.TabIndex = 1;
            this.Button_DaBao.Text = "编译";
            this.Button_DaBao.UseVisualStyleBackColor = true;
            this.Button_DaBao.Click += new System.EventHandler(this.Button_DaBao_Click);
            // 
            // button_Open
            // 
            this.button_Open.Location = new System.Drawing.Point(3, 1);
            this.button_Open.Name = "button_Open";
            this.button_Open.Size = new System.Drawing.Size(115, 38);
            this.button_Open.TabIndex = 2;
            this.button_Open.Text = "打开";
            this.button_Open.UseVisualStyleBackColor = true;
            this.button_Open.Click += new System.EventHandler(this.button_Open_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(124, 1);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(115, 38);
            this.button_Save.TabIndex = 3;
            this.button_Save.Text = "保存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Teaching
            // 
            this.button_Teaching.Location = new System.Drawing.Point(487, 1);
            this.button_Teaching.Name = "button_Teaching";
            this.button_Teaching.Size = new System.Drawing.Size(115, 38);
            this.button_Teaching.TabIndex = 4;
            this.button_Teaching.Text = "教学实例";
            this.button_Teaching.UseVisualStyleBackColor = true;
            this.button_Teaching.Click += new System.EventHandler(this.button_Teaching_Click);
            // 
            // textBox_Code
            // 
            this.textBox_Code.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Code.Location = new System.Drawing.Point(3, 45);
            this.textBox_Code.Multiline = true;
            this.textBox_Code.Name = "textBox_Code";
            this.textBox_Code.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Code.Size = new System.Drawing.Size(1255, 615);
            this.textBox_Code.TabIndex = 5;
            this.textBox_Code.Text = "#主类 jz{\r\n\r\n    #程序入口{\r\n        #打印并换行(\"Hello JZ\");\r\n    }\r\n\r\n}";
            // 
            // openFileDialog_Open
            // 
            this.openFileDialog_Open.FileName = "openFileDialog1";
            this.openFileDialog_Open.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_Open_FileOk);
            // 
            // saveFileDialog_Save
            // 
            this.saveFileDialog_Save.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_Save_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 664);
            this.Controls.Add(this.textBox_Code);
            this.Controls.Add(this.button_Teaching);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_Open);
            this.Controls.Add(this.Button_DaBao);
            this.Controls.Add(this.button_Run);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Run;
        private System.Windows.Forms.Button Button_DaBao;
        private System.Windows.Forms.Button button_Open;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Teaching;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Open;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_Save;
        public System.Windows.Forms.TextBox textBox_Code;
    }
}

