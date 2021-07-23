using System;

namespace Line_by_line_sorting.Domain
{
    public static class LblSorting
    {
        public static string[] QuickSort(string[] array)
        {
            return QuickSort(array, 0, array.Length - 1);
        }

        static void Swap(ref string a, ref string b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
        //Находим опорный элемент
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
        //быстрая сортировка
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
    }
}
