using Line_by_line_sorting.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Line_by_line_sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("[1] - Отсортировать файл\n[2] - Сгенерировать новый файл");
            int selection = Convert.ToInt32(Console.ReadLine());
            switch (selection)
            {
                case 1:
                    Sort();
                    break;
                case 2:
                    Create();
                    break;
                default:
                    break;
            }
            
        }
        static void Create()
        {
            Console.Write("[>] Укажите количество строк: ");
            int rowCount = Convert.ToInt32(Console.ReadLine());
            Console.Write("[>] Укажите максимальную длину этих строк: ");
            int rowLength = Convert.ToInt32(Console.ReadLine());

            List<string> randomStrings = new List<string>();
            Random rand = new Random();
            string line;
            for(int j = 0; j < rowCount - 1; j++)
            {
                line = string.Empty;
                for(int i = 0; i < rowLength - 1; i++)
                {
                    line += rand.Next(0x0410, 0x44F);
                }
                randomStrings.Add(line);
            }
            File.WriteAllLines("test.txt", randomStrings.Select(i => i.ToString()).ToArray());
            Console.WriteLine("[+] Генерация файла завершена");

        }
        static void Sort()
        {
            var lines = new List<string>();
            //Читаем файл
            using (StreamReader file = new StreamReader(@"test.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                file.Close();
            }
            //Выводим
            var a = LblSorting.QuickSort(lines.ToArray());

            File.WriteAllLines("out.txt", a.Select(i => i.ToString()).ToArray());
            Console.WriteLine("[+] Сортирвка успешна!");
        }
    }
}
