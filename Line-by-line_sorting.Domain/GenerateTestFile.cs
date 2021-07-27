using System;
using System.IO;

namespace Line_by_line_sorting.Domain
{
    public class GenerateTestFile
    {
        /// <summary>
        /// Метод создания файла с рандомными строками
        /// </summary>
        /// <param name="rowCount">Количество строк</param>
        /// <param name="rowLength">Длина строк</param>
        /// <param name="pathOut">Путь выходного файла</param>
        public static void GenerateFile(ulong rowCount, ulong rowLength, string pathOut = "test.txt")
        {
            //Из каких символов делать строки
            char[] letters = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();
            //char[] letters = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~".ToCharArray();
            Random rand = new Random();
            string line;
            using (StreamWriter file = new StreamWriter(pathOut, false))
            {
                for (ulong j = 0; j < rowCount - 1; j++)
                {
                    line = string.Empty;

                    for (ulong i = 0; i < rowLength - 1; i++)
                    {
                        line += letters[rand.Next(0, letters.Length - 1)];
                    }
                    file.WriteLine(line + "\n");
                }
                file.Close();
            }
        }
    }
}
