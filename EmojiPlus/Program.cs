using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmojiPlus
{
    class Program
    {
        static Dictionary<string, int> integers = new Dictionary<string, int>();
        static Dictionary<string, string> strings = new Dictionary<string, string>();
        static Dictionary<byte, int> pointers = new Dictionary<byte, int>();

        private static string arg = "0";
        static void Main(string[] args)
        {
            arg = args[0];
        begin:
            string input = "";
            if (args[0] == "0")
            {
                while (true)
                {
                    input = Console.ReadLine();
                    string[] temp1 = input.Split();
                    char[] temp2 = input.ToCharArray();
                    mode(temp1[0], temp1, temp2);
                }
            }
            else if (args[0] == "1")
            {
                const string f = "C:\\test.emp";
                List<string> lines = new List<string>();

                using (StreamReader r = new StreamReader(f))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
                foreach (string s in lines)
                {
                    input = s.ToString();
                    string[] temp1 = input.Split();
                    char[] temp2 = input.ToCharArray();
                    mode(temp1[0], temp1, temp2);
                }
            }
            else
            {
                if (args[0] == "0")
                {
                    Console.WriteLine("Invalid Command");
                    goto begin;
                }
                else
                {
                    Console.WriteLine("Invalid Command");
                    uhoh();
                }
            }

        }

        public static void mode(string command, string[] input1, char[] input2)
        {
            switch (command)
            {
                case ":o":
                    print(input2);
                    break;
                case ":)":
                    readL(input1);
                    break;
                case ":p":
                    //comments
                    break;
                case "#":
                    readN(input1);
                    break;
                case "$":
                    int tmp = 0;
                    int.TryParse(input1[3], out tmp);
                    integers.Add(input1[1], tmp);
                    //Console.WriteLine(integers[input1[1]]);
                    break;
                case "w":
                    strings.Add(input1[1], input1[3]);
                    //Console.WriteLine(strings[input1[1]]);
                    break;
                case "math":
                    math(input1);
                    //Console.WriteLine(math(input1));
                    break;
                case "@":
                    //looping
                    break;
                case "":

                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    uhoh();
                    break;
            }
        }

        public static int math(string[] input)
        {
            int temp = 0;
            int num1 = 0;
            int num2 = 0;

            string stemp = "";
            string str1 = "";
            string str2 = "";

            if (integers.ContainsKey(input[3]) && integers.ContainsKey(input[5]))
            {
                num1 = integers[input[3]];
                num2 = integers[input[5]];

                switch (strings[input[4]])
                {
                    case "+":
                        temp = num1 + num2;
                        break;
                    case "-":
                        temp = num1 - num2;
                        break;
                    case "*":
                        temp = num1 * num2;
                        break;
                    case "/":
                        temp = num1 / num2;
                        break;
                    default:
                        Console.WriteLine("Invalid Command");
                        uhoh();
                        break;
                }

                if (integers.ContainsKey(input[1]))
                {
                    integers.Remove(input[1]);
                    integers.Add(input[1], temp);
                }
                else
                {
                    integers.Add(input[1], temp);
                }
            }
            else if (strings.ContainsKey(input[3]) && strings.ContainsKey(input[5]))
            {
                str1 = strings[input[3]];
                str2 = strings[input[5]];

                switch (input[4])
                {
                    case "+":
                        stemp = str1 + str2;
                        break;
                    default:
                        Console.WriteLine("Invalid Command");
                        uhoh();
                        break;
                }

                if (strings.ContainsKey(input[1]))
                {
                    strings.Remove(input[1]);
                    strings.Add(input[1], stemp);
                }
                else
                {
                    strings.Add(input[1], stemp);
                }
            }

            return temp;
        }

        public static string readL(string[] input)
        {
            string temp = Console.ReadLine();
            if (strings.ContainsKey(input[1]))
            {
                strings.Remove(input[1]);
                strings.Add(input[1], temp);
            }
            else
            {
                strings.Add(input[1], temp);
            }
            return temp;
        }

        public static int readN(string[] input)
        {
            string temp = Console.ReadLine();
            int num = 0;
            int.TryParse(temp, out num);
            if (integers.ContainsKey(input[1]))
            {
                integers.Remove(input[1]);
                integers.Add(input[1], num);
            }
            else
            {
                integers.Add(input[1], num);
            }
            return num;
        }

        public static string print(char[] input)
        {
            bool flag = false;
            bool flag2 = false;
            StringBuilder builder = new StringBuilder();
            StringBuilder temp = new StringBuilder();
            for (int i = 3; i < input.Length; i++)
            {
                if (input[i] == '"' && flag == true)
                {
                    flag = false;
                }
                else if (input[i] == '"')
                {
                    flag = true;
                }
                else if (input[i] == '(' && flag == false)
                {
                    flag2 = true;
                }
                else if (input[i] == ')' && flag2 == true)
                {
                    flag2 = false;
                    if (integers.ContainsKey(temp.ToString()))
                    {
                        builder.Append(integers[temp.ToString()]);
                    }
                    else if (strings.ContainsKey(temp.ToString()))
                    {
                        builder.Append(strings[temp.ToString()]);
                    }
                    temp.Clear();
                }
                else if (flag == true)
                {
                    builder.Append(input[i]);
                }
                else if (flag2 == true)
                {
                    temp.Append(input[i]);
                }
            }

            string line = builder.ToString();
            if (line == "")
            {
                Console.WriteLine("Invalid Input");
                uhoh();
                return null;
            }
            else
            {
                Console.WriteLine(line);
            }

            return line;
        }

        public static void uhoh()
        {
            if (arg == "1")
            {
                Console.WriteLine("Program has exited due to errors. ");
                Environment.Exit(117);
            }
            else
            {
                //Console.WriteLine("Try again!");
            }
        }
    }
}
