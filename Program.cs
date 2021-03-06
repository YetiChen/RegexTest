﻿using System;
using System.Collections.Generic;
using System.Text;
using static TestRegex.TestRegex;

namespace TestRegexConsoleApplication
{
    public class Program
    {
        private static List<ConsoleColor> _matchColors = new List<ConsoleColor>() { ConsoleColor.Red, ConsoleColor.Cyan };
        private static int _matchColorIndex = 0;

        public static void Main(string[] args)
        {
            var test = new TestRegex.TestRegex();
            test.OutNormal = OutNormal;
            test.OutMatched = OutMatched;
            test.OutGroup = OutGroup;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("pass h show help");
                ShowConfigure(test);
                Console.Write("command:");
                switch (Console.ReadLine().ToLower())
                {
                    case "t":
                        Console.Clear();
                        Console.WriteLine("math:m or replace:r");
                        while (!test.SetType(Console.ReadLine()))
                        {
                            Console.Clear();
                            Console.WriteLine("math:m or replace:r");
                        }
                        break;
                    case "r":
                        test.Pattern = Console.ReadLine();
                        break;
                    case "d":
                        test.Data = SetData();
                        break;
                    case "p":
                        test.Replacement = SetData();
                        break;
                    case "c":
                        ShowConfigure(test);
                        break;
                    case "run":
                        Console.Clear();
                        ShowConfigure(test);
                        Console.WriteLine("result:");
                        var r = true;
                        switch (test.Type)
                        {
                            case RegexType.Match:
                                r = r && test.Match();
                                Console.WriteLine();
                                Console.Write("group:");
                                r = r && test.Group();
                                break;
                            case RegexType.Replace:
                                Console.WriteLine("match:");
                                r = r && test.Match();
                                Console.WriteLine();
                                Console.Write("group:");
                                r = r && test.Group();
                                Console.WriteLine();
                                Console.WriteLine("replace:");
                                r = r && test.Replace();
                                Console.WriteLine();
                                break;
                            default:
                                r = false;
                                break;
                        }
                        if (!r)
                        {
                            Console.WriteLine("error");
                        }
                        Console.WriteLine();
                        Console.Write("pass any key to continue");
                        Console.ReadKey();
                        break;
                    case "h":
                        Console.Clear();
                        GetHelp();
                        Console.Write("pass any key to continue");
                        Console.ReadKey();
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

        private static void ShowConfigure(TestRegex.TestRegex test)
        {
            var bc = Console.BackgroundColor;
            var fc = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("type:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(test.Type);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("data:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(test.Data);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("regex:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(test.Pattern);
            if (test.Type == RegexType.Replace)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("replace:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(test.Replacement);
            }

            Console.BackgroundColor = bc;
            Console.ForegroundColor = fc;
        }

        private static void OutNormal(string data)
        {
            var bc = Console.BackgroundColor;
            var fc = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(data);

            Console.BackgroundColor = bc;
            Console.ForegroundColor = fc;
        }

        private static void OutMatched(string data)
        {
            var bc = Console.BackgroundColor;
            var fc = Console.ForegroundColor;

            Console.ForegroundColor = _matchColors[_matchColorIndex];
            if (_matchColorIndex < _matchColors.Count - 1)
            {
                _matchColorIndex++;
            }
            else
            {
                _matchColorIndex = 0;
            }
            Console.Write(data);

            Console.BackgroundColor = bc;
            Console.ForegroundColor = fc;
        }

        private static void OutGroup(int index, string data)
        {
            var bc = Console.BackgroundColor;
            var fc = Console.ForegroundColor;

            if (index == 0)
            {
                Console.WriteLine();
            }
            else
            {
                Console.Write("\t");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"${index}:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(data);

            Console.BackgroundColor = bc;
            Console.ForegroundColor = fc;
        }
    }
}
