using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Line_by_line_sorting.Domain
{
    public static class LblSorting
    {
        public static void QSortFile() { }

        private static void Split(string path)
        {
            int splitNumber = 1;
            StreamWriter sw = new StreamWriter(string.Format("split{0:d5}.txt",splitNumber)); //TODO: Проверить
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    sw.WriteLine(sr.ReadLine());

                    if (sw.BaseStream.Length > 100000000 && sr.Peek() >= 0)
                    {
                        sw.Close();
                        splitNumber++;
                        sw = new StreamWriter(string.Format("test{0:d5}.txt", splitNumber));
                    }
                }
            }
            sw.Close();
        }

        private static void SortPiece()
        {
            foreach (string path in Directory.GetFiles(@"", "split*.txt"))
            {
                string[] contents = File.ReadAllLines(path);
                Array.Sort(contents);
                string newpath = path.Replace("split", "sorted");
                File.WriteAllLines(newpath, contents);
                File.Delete(path);
                contents = null;
                GC.Collect();
            }
        }

        private static void Merge()
        {

        }







        /*
        /// <summary>
        /// Метод для создания отсортированного файла
        /// </summary>
        /// <param name="path">Путь не отсортированного файла</param>
        /// <param name="pathOut">Путь выходного файла</param>
        public static void QSortFile(string path = "test.txt", string pathOut = "out.txt")
        {
            
                var lines = new List<string>();
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException();
                }
                using (StreamReader file = new StreamReader(path))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                    file.Close();
                }

                var a = QuickSort(lines.ToArray());
                File.WriteAllLines(pathOut, a.Select(i => i.ToString()).ToArray());
            
        }
        /// <summary>
        /// Метод для получения массива быстрой сортировки
        /// </summary>
        /// <param name="array">Массив строк</param>
        /// <returns>Отсортированный массив строк</returns>
        public static string[] QuickSort(string[] array)
        {
            return QuickSort(array, 0, array.Length - 1);
        }
        /// <summary>
        /// Метод перемещения
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        static void Swap(ref string a, ref string b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
        /// <summary>
        /// Метод для поиска опоры
        /// </summary>
        /// <param name="array">Массив строк</param>
        /// <param name="minIndex">Минимальный индекс массива</param>
        /// <param name="maxIndex">Максимальный индекс массива</param>
        /// <returns>Индекс опоры</returns>
        static int Partition(string[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (string.Compare(array[i], array[maxIndex], StringComparison.Ordinal) < 0)
                {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }
        /// <summary>
        /// Быстрая сортировка
        /// </summary>
        /// <param name="array"></param>
        /// <param name="minIndex"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        static string[] QuickSort(string[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }
    */
    }
}
