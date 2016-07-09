using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTest
{
    //学生类
    public class Student
    {
        private int _StuNum;
        private string _StuName;
        private int _Chinese;
        private int _Math;
        private int _English;

        public int StuNum
        {
            get
            {
                return _StuNum;
            }

            set
            {
                _StuNum = value;
            }
        }

        public string StuName
        {
            get
            {
                return _StuName;
            }

            set
            {
                _StuName = value;
            }
        }

        public int Chinese
        {
            get
            {
                return _Chinese;
            }

            set
            {
                _Chinese = value;
            }
        }

        public int Math
        {
            get
            {
                return _Math;
            }

            set
            {
                _Math = value;
            }
        }

        public int English
        {
            get
            {
                return _English;
            }

            set
            {
                _English = value;
            }
        }


        public static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("1、录入数据");
            Console.WriteLine("2、信息查询（通过学号）");
            Console.WriteLine("3、删除记录（通过学号）");
            Console.WriteLine("4、所有记录");
            Console.WriteLine("5、退出系统");
            Console.Write("请输入您的选择:");

            string key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    AddData();
                    break;
                case "2":
                    FindInfo();
                    break;
                case "3":
                    DeleteInfo();
                    break;
                case "4":
                    ShowAllInfo();
                    break;
                case "5":
                    Environment.Exit(0); //正常退出？
                    break;
                default:
                    Console.WriteLine("您输入的数字不正确，请重新输入！");
                    break;
            }

        }

        public static void AddData()
        {
            Console.Clear();
            Console.WriteLine("请输入学生的学号，姓名，语文分数，数学分数，英语分数，以空格隔开：");
            string input = Console.ReadLine();
            string[] array = input.Split(' ');

            string stuNum = array[0];
            string stuName = array[1];
            string stuChinese = array[2];
            string stuMath = array[3];
            string stuEnglish = array[4];

            if (Student.FindInfoByStuNum(stuNum) == null)
            {
                Console.WriteLine("该学号已存在！请重新输入！");
                Student.AddData();
            }
            if (string.IsNullOrEmpty(stuName))
            {
                Console.WriteLine("姓名不能为空！请重新输入！");
                Student.AddData();
            }
            for (int i = 2; i < 5; i++)
            {
                int grades = Convert.ToInt32(array[i]);
                if (grades < 0 || grades > 100)
                {
                    Console.WriteLine("分数必须在0到100之间！请重新输入！");
                    Student.AddData();
                }
            }
        }

        public static void FindInfo()
        {
        }
        public static void DeleteInfo()
        {
        }
        public static void ShowAllInfo()
        {
        }

        public static Student FindInfoByStuNum(string stunum)
        {
            return null;
            //return new Student();
        }
    }

}
