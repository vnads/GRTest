using System;
using System.IO;
using System.Reflection;

namespace GRTest.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
