using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Line_by_line_sorting.Domain
{
    public static class LblSorting
    {
        //Директория где будут все файлы (split, sorted)
        private static readonly string directoryFiles = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        private static readonly string patternSearchSplit = "split*.txt";
        private static readonly string patternSearchSort = "sorted*.txt";

        public static void QSortFile(string path = "test.txt")
        {
            //Разбиваем файл на части, взависимости от длины строки
            Split(path, 100000);
            //Сортируем массивы строк в каждом разбитом файле
            SortPiece();
            //Соединяем отсортированные файлы (Массивы) в один файл
            Merge(path);
        }

        /// <summary>
        /// Метод для разделения файла на маленькие кусочки
        /// </summary>
        /// <param name="path">Путь файла</param>
        /// <param name="size">Максимальный размер одного файла</param>
        private static void Split(string path, long size)
        {
            int splitNumber = 1;
            StreamWriter sw = new StreamWriter(string.Format("split{0:d5}.txt", splitNumber));
            using (StreamReader sr = new StreamReader(path, false))
            {
                string line = null;
                while (sr.Peek() >= 0)
                {
                    if ((line = sr.ReadLine()) != string.Empty)
                        sw.WriteLine(line);

                    if (sw.BaseStream.Length > size && sr.Peek() >= 0)
                    {
                        sw.Close();
                        splitNumber++;
                        sw = new StreamWriter(string.Format("split{0:d5}.txt", splitNumber));
                    }
                }
            }
            sw.Close();
        }

        /// <summary>
        /// Метод для сортировки файлов (split*.txt)
        /// </summary>
        private static void SortPiece()
        {
            foreach (string path in Directory.GetFiles(directoryFiles, patternSearchSplit))
            {
                string[] contents = File.ReadAllLines(path);
                Array.Sort(contents);
                string newpath = path.Replace("split", "sorted");
                File.WriteAllLines(newpath, contents);
                File.Delete(path);
            }
        }

        static void Merge(string path)
        {
            string[] paths = Directory.GetFiles(directoryFiles, patternSearchSort);
            int chunks = paths.Length;
            int recordsize = 100; // размер записи
            int maxusage = 500000000; // максимальное использование памяти (около 50 мб)
            int buffersize = maxusage / chunks; // размер в байтах каждого буфера
            double recordoverhead = 7.5;
            int bufferlen = (int)(buffersize / recordsize / recordoverhead); // количество записей в каждом буфере

            // Открываем файлы
            StreamReader[] readers = new StreamReader[chunks];
            for (int i = 0; i < chunks; i++)
                readers[i] = new StreamReader(paths[i]);

            // Создаем очередь
            Queue<string>[] queues = new Queue<string>[chunks];
            for (int i = 0; i < chunks; i++)
                queues[i] = new Queue<string>(bufferlen);

            // Заполняем очередь
            for (int i = 0; i < chunks; i++)
                AddQueue(queues[i], readers[i], bufferlen);

            // Слияние файлов
            StreamWriter sw = new StreamWriter(path);

            int lowest_index, j;
            string lowest_value;
            while (true)
            {
                lowest_index = -1;
                lowest_value = string.Empty;
                //Найти часть с наименьшим значением
                for (j = 0; j < chunks; j++)
                {
                    if (queues[j] != null)
                    {
                        // Проверяем 
                        if (lowest_index < 0 || string.Compare(queues[j].Peek(), lowest_value, StringComparison.CurrentCulture) < 0)
                        {
                            lowest_index = j;
                            lowest_value = queues[j].Peek();
                        }
                    }
                }

                if (lowest_index == -1)
                    break;

                // Ввод
                sw.WriteLine(lowest_value);

                // Изъятие из очереди
                queues[lowest_index].Dequeue();

                //Если очередь закончилась
                if (queues[lowest_index].Count == 0)
                {
                    // Пополнение очереди
                    AddQueue(queues[lowest_index], readers[lowest_index], bufferlen);
                    // Есть ли еще записи
                    if (queues[lowest_index].Count == 0)
                    {
                        queues[lowest_index] = null;
                    }
                }
            }
            sw.Close();

            // Удаляем временные файлы
            for (int i = 0; i < chunks; i++)
            {
                readers[i].Close();
                File.Delete(paths[i]);
            }
        }
        /// <summary>
        /// Метод добавления в очередь
        /// </summary>
        /// <param name="queue">Очередь</param>
        /// <param name="file">файл</param>
        /// <param name="records">Количество записей в очереди</param>
        private static void AddQueue(Queue<string> queue, StreamReader file, int records)
        {
            for (int i = 0; i < records; i++)
            {
                if (file.Peek() < 0) break;
                queue.Enqueue(file.ReadLine());
            }
        }
    }
}
