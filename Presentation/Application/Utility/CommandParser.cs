using System;
using System.Text.RegularExpressions;

namespace Presentation.Application.Utility
{
    public class CommandParser
    {
        private readonly string _pattern;
        private readonly string _groupTag;

        public CommandParser(string pattern, string groupTag)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException(nameof(pattern));
            }
            if (string.IsNullOrEmpty(groupTag))
            {
                throw new ArgumentNullException(nameof(groupTag));
            }

            _pattern = pattern;
            _groupTag = groupTag;
        }

        public string ExtractCommand(string message)
        {
            string command = default;

            if(!string.IsNullOrEmpty(message))
            {
                command = new Regex(_pattern, RegexOptions.IgnoreCase)
                    .Match(message)
                    .Groups[_groupTag].Value;
            }

            return command;
        }
    }
}
