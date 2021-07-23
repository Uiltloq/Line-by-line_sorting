using System;
using System.Collections.Generic;
using System.IO;

namespace Line_by_line_sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            var lines = new List<string>();
            using(StreamReader file = new StreamReader(@"test.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
                file.Close();
            }

        }

        static void SortLine(string line)
        {

        }
    }
}
