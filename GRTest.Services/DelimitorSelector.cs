using System;

namespace GRTest.Services
{
    public class DelimitorSelector
    {
        private static readonly char[] Delimiters = {',', '|', ' ' }; //space needs to be last in case there are spaces between fields with other delimiters


        public char GetDelimiter(string data)
        {
            foreach (var delimiter in Delimiters)
            {
                if(data.IndexOf(delimiter) >= 0)
                    return delimiter;
                
            }
            throw new InvalidOperationException("No valid delimiters found");
        }
    }
}
