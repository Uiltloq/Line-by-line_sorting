using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public static void GenerateFile(int rowCount, int rowLength, string pathOut = "test.txt")
        {
            File.WriteAllLines(pathOut, GenerateList(rowCount, rowLength).Select(i => i.ToString()).ToArray());
        }
        /// <summary>
        /// Метод создания листа с рандомными символами
        /// </summary>
        /// <param name="rowCount">Количество строк</param>
        /// <param name="rowLength">Длина строк</param>
        /// <returns>Лист рандомных символов</returns>
        private static List<string> GenerateList(int rowCount, int rowLength)
        {
            List<string> randomStrings = new List<string>();
            char[] letters = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~".ToCharArray();
            Random rand = new Random();
            string line;
            for (int j = 0; j < rowCount - 1; j++)
            {
                line = string.Empty;
                for (int i = 0; i < rowLength - 1; i++)
                {
                    line += letters[rand.Next(0, letters.Length - 1)];
                }
                randomStrings.Add(line);
            }

            return randomStrings;
        }
    }
}
