using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication
{

    public enum RegexType
    {
        Match,
        Replace
    }
    public class Program
    {
        private static RegexType _type = RegexType.Match;
        private static String _data;
        private static string _regex;
        private static string _replace;

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("pass h show help");

            while (true)
            {
                ShowConfigure();
                Console.Write("command:");
                var input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "t":
                        SetType();
                        Console.Clear();
                        break;
                    case "r":
                        SetRegex();
                        Console.Clear();
                        break;
                    case "d":
                        _data = SetData();
                        Console.Clear();
                        break;
                    case "p":
                        _replace = SetData();
                        Console.Clear();
                        break;
                    case "c":
                        ShowConfigure();
                        Console.Clear();
                        break;
                    case "run":
                        Console.Clear();
                        ShowConfigure();
                        Run();
                        Console.WriteLine();
                        Console.Write("pass any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "h":
                        Console.Clear();
                        GetHelp();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void GetHelp()
        {
            Console.WriteLine("pass t change type");
            Console.WriteLine("pass r change regex");
            Console.WriteLine("pass d change data");
            Console.WriteLine("pass p change replace");
            Console.WriteLine("pass run start run");
            Console.WriteLine("pass c show configure");
            Console.WriteLine("pass h show help");
        }

        private static void SetType()
        {        
            Console.WriteLine("math:m or replace:r");
            var input = Console.ReadLine().ToLower();
            switch (input)
            {
                case "m":
                    _type = RegexType.Match;
                    break;
                case "r":
                    _type = RegexType.Replace;
                    break;
                default:
                    SetType();
                    break;
            }
        }

        private static string SetData()
        {
            Console.WriteLine("double empty line to end");
            var ls = new List<string>();
            string lastLine = null;
            while (true)
            {
                var input = Console.ReadLine();
                if (lastLine == string.Empty && input == string.Empty)
                {
                    break;
                }
                ls.Add(input);
                lastLine = input;
            }
            var sb = new StringBuilder();
            for (int i = 0; i < ls.Count - 1; i++)
            {
                if (i > 0)
                {
                    sb.AppendLine();
                }
                sb.Append(ls[i]);
            }
            return sb.ToString();
        }

        private static void SetRegex()
        {
            var input = Console.ReadLine();
            _regex = input;
        }

        private static void Run()
        {
            Console.WriteLine("result:");
            switch (_type)
            {
                case RegexType.Match:
                    if (string.IsNullOrEmpty(_data) || string.IsNullOrEmpty(_regex))
                    {
                        Console.WriteLine("error");
                        return;
                    }

                    Match();
                    break;
                case RegexType.Replace:
                    if (string.IsNullOrEmpty(_data) || string.IsNullOrEmpty(_regex) || string.IsNullOrEmpty(_replace))
                    {
                        Console.WriteLine("error");
                        return;
                    }
                    Replace();
                    break;
                default:
                    break;
            }
        }

        private static void ShowConfigure()
        {
            var bc = Console.BackgroundColor;
            var fc = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("type:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(_type);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("data:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(_data);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("regex:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(_regex);
            if (_type == RegexType.Replace)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("replace:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(_replace);
            }

            Console.BackgroundColor = bc;
            Console.ForegroundColor = fc;
        }

        private static void Match()
        {
            var bc = Console.BackgroundColor;
            var fc = Console.ForegroundColor;

            var sIndex = 0;
            var ms = Regex.Matches(_data, _regex);
            foreach (Match m in ms)
            {
                if (sIndex != m.Index)
                {
                    OutNormal(_data.Substring(sIndex, m.Index - sIndex));
                }
                OutMatched(_data.Substring(m.Index, m.Length));
                sIndex = m.Index + m.Length;
            }
            if (sIndex != _data.Length)
            {
                OutNormal(_data.Substring(sIndex, _data.Length - sIndex));
            }

            Console.BackgroundColor = bc;
            Console.ForegroundColor = fc;
        }

        private static void Replace()
        {
            var bc = Console.BackgroundColor;
            var fc = Console.ForegroundColor;

            Console.WriteLine("match:");
            Match();
            Console.WriteLine();

            Console.WriteLine("replace:");
            OutMatched(Regex.Replace(_data, _regex, _replace));
            Console.WriteLine();

            Console.BackgroundColor = bc;
            Console.ForegroundColor = fc;
        }

        private static void OutNormal(string data)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(data);
        }

        private static void OutMatched(string data)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(data);
        }
    }
}
