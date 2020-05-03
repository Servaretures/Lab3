using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose Task \n1)Delete words with loud letters \n2)Nums arrays changer \n3)Delete Lathin letter(Work with files)");
            int rs = Convert.ToInt16(Console.ReadLine());
            if(rs == 1)
            {
                Task1();
            }
            if(rs == 2)
            {
                Task2();
            }
            if (rs == 3)
            {
                Task3();
            }
        }
        static void Task1()
        {
            Console.WriteLine("Enter string (Only English)");
            string Text = Console.ReadLine();
            int StartWord = 0;
            int EndWord = 0;
            bool word = false;
            char[] txt = Text.ToCharArray();
            List<char> Holosny = new List<char>() { 'A', 'E', 'I', 'O', 'U', 'Y', 'a', 'e', 'i', 'o', 'u', 'y' };
            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] != ' ' && !word)
                {
                    StartWord = i;
                    word = true;
                }
                else if (txt[i] == ' ' && word)
                {
                    EndWord = i - 1;
                    word = false;
                    if (Holosny.Contains(txt[StartWord]) && StartWord != EndWord)
                    {
                        for (int b = StartWord; b <= EndWord; b++)
                        {
                            txt[b] = ' ';
                        }
                    }
                }
            }
            Console.WriteLine("Result - " + new string(txt));
            Console.ReadKey();
        }
        static void Task2()
        {
            Console.WriteLine("Enter way to file like C:/Users/Макс/Desktop/Exepmle.txt");
            string way = Console.ReadLine();
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(way))
                {
                    text +=  "\n" +sr.ReadToEnd();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            char[] txt = text.ToCharArray();
            int buffer = 0;
            List<int> nums = new List<int>();
            for(int i = 0;i < txt.Length; i++)
            {
                if (Char.IsDigit(txt[i]))
                {
                    buffer = buffer * 10 + Convert.ToInt32(new string(txt[i], 1)); 
                }
                else if(txt[i] == ' ' || i + 1 == txt.Length)
                {
                    nums.Add(buffer);
                    buffer = 0;
                }
            }
            string numms = "";
            for(int x = 0;x < nums.Count; x++)
            {
                if (nums[x] % 2 == 0 && nums[x] >= 0)
                {
                    nums[x] *= 2;
                }
                else if (nums[x] % 2 != 0 && nums[x] >= 0)
                {
                    nums[x] += 1;
                }
                else
                {
                    nums[x] = Math.Abs(nums[x]);
                }
                numms += nums[x] + " ";
            }
            char[] wway = way.ToCharArray();
            string newway = "";
            bool start = false;
            for(int i = wway.Length-1;i >= 0; i--)
            {
                if(wway[i] == '/')
                {
                    start = true;
                }
                if (start)
                {
                    newway = wway[i] + newway;
                }
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(newway + "OutPut.txt", false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(numms);
                }
                Console.WriteLine("Записано у файл, мiсцезнаходження файлу таке ж яке i у файла вводу");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.ReadKey();
        }
        static void Task3()
        {
            Console.WriteLine("Enter way to file like C:/Users/Макс/Desktop/Exepmle.txt");
            string way = Console.ReadLine();
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(way))
                {
                    text += "\n" + sr.ReadToEnd();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            char[] spearator = { ',', ' ' };
            char[] sp = {'/'};
            string[] nvway = way.Split(sp);
            string newway = "";
            for (int i = 0;i < nvway.Length - 1; i++)
            {
                newway += nvway[i]+ "/";
            }
            string[] texts = text.Split(spearator);
            List<string> Answ = texts.ToList();
            string Save = "";
            for (int i = 0; i < Answ.Count; i++)
            {
                if (!Regex.IsMatch(Answ[i], @"[A-Za-z]", RegexOptions.None) || Regex.IsMatch(Answ[i], @"[ЁёА-я]"))
                {
                    Save += Answ[i] + " ";
                }
            }
            using (StreamWriter sw = new StreamWriter(newway + "/new.txt"))
            {
                sw.WriteLine(Save);
            }
            Console.WriteLine("Записано");
            Console.ReadKey();
        }
    }
}
