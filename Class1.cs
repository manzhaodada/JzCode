using JZ计算机软件开发语言;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JzCode
{
    internal class Class1
    {
        private Form1 form1;

        public Class1(Form1 form1)
        {
            this.form1 = form1;
        }

        public Class1()
        {

        }

        public string getTextBox_Code()
        {
            if (form1 != null)
            {
                return form1.getTextBox_Code();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
