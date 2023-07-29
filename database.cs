using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JZ计算机软件开发语言
{
    internal class database
    {
        public static String getDataBase(String Case)
        {
            var listData = new Dictionary<string, String>();
            listData["#主类"] = "public class";
            listData["#程序入口"] = "public static void main(String[] args)";
            listData["#公开"] = "public";
            listData["#私有"] = "private";
            listData["#字节型"] = "byte";
            listData["#字符型"] = "char";
            listData["#整数型"] = "int";
            listData["#长整数型"] = "long";
            listData["#单精度小数型"] = "float";
            listData["#双精度小数型"] = "double";
            listData["#文本型"] = "String";
            listData["#布尔型"] = "boolean";
            listData["#如果"] = "if";
            listData["#否则如果"] = "else if";
            listData["#否则"] = "else";
            listData["#判断后循环"] = "while";
            listData["#变量循环"] = "for";
            listData["#打印并换行"] = "System.out.println";
            listData["#打印"] = "System.out.print";
            listData["静态"] = "static";
            return listData[Case];
    }

        //todo "for(int i = 0;i <= x;i++)"--计次循环（满足传入x参数与可以返回当前计次个数的变量)
        //do while--循环后判断
        //需要后期重构

        public static String[] chineseData = {"#主类", "#程序入口","#公开",
            "#私有","#字节型","#字符型","#整数型","#长整数型"
                ,"#单精度小数型","#双精度小数型","#文本型",
            "#布尔型","#如果","#否则如果","#否则",
            "#判断后循环","#变量循环","#打印并换行","#打印"

        };
    }
    }

