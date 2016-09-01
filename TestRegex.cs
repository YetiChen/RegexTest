using System;
using System.Text.RegularExpressions;

namespace TestRegex
{
    public class TestRegex
    {
        public  enum RegexType
        {
            Match,
            Replace
        }

        public RegexType Type { get; set; }
        public String Data { get; set; }
        public string Pattern { get; set; }
        public string Replacement { get; set; }

        public Action<string> OutMatched;

        public Action<string> OutNormal;

        public TestRegex()
        {
            Type = RegexType.Match;
        }

        public bool SetType(string input)
        {
            var rtn = true;
            switch (input.ToLower())
            {
                case "m":
                    Type = RegexType.Match;
                    break;
                case "r":
                    Type = RegexType.Replace;
                    break;
                default:
                    rtn = false;
                    break;
            }
            return rtn;
        }

        public bool Match()
        {
            if (string.IsNullOrEmpty(Data) || string.IsNullOrEmpty(Pattern) || OutMatched == null || OutNormal == null)
            {
                return false;
            }

            var sIndex = 0;
            var ms = Regex.Matches(Data, Pattern);
            foreach (Match m in ms)
            {
                if (sIndex != m.Index)
                {
                    OutNormal(Data.Substring(sIndex, m.Index - sIndex));
                }
                OutMatched(Data.Substring(m.Index, m.Length));
                sIndex = m.Index + m.Length;
            }
            if (sIndex != Data.Length)
            {
                OutNormal(Data.Substring(sIndex, Data.Length - sIndex));
            }
            return true;
        }

        public bool Replace()
        {
            if (string.IsNullOrEmpty(Data) || string.IsNullOrEmpty(Pattern) || string.IsNullOrEmpty(Replacement) || OutMatched == null || OutNormal == null)
            {
                return false;
            }

            OutMatched(Regex.Replace(Data, Pattern, Replacement));

            return true;
        }
    }
}