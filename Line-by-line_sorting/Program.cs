using Line_by_line_sorting.Domain;
using System;

namespace Line_by_line_sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("[1] - Отсортировать файл\n[2] - Сгенерировать новый файл\n([0] для выхода)");
                Console.Write("[>] Выберите действие: ");
                try
                {
                    int selection = Convert.ToInt32(Console.ReadLine());
                    switch (selection)
                    {
                        case 0:
                            Console.WriteLine("Выход из программы");
                            return;
                        case 1:
                            Sort();
                            break;
                        case 2:
                            Create();
                            break;
                        default:
                            Console.WriteLine("К сожалению нет такого действия");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Ошибка: {0}\n",ex.Message);
                }

            }
        }
        static void Create()
        {
            Console.Write("[>] Укажите количество строк: ");
            ulong rowCount = Convert.ToUInt64(Console.ReadLine());
            Console.Write("[>] Укажите максимальную длину этих строк: ");
            ulong rowLength = Convert.ToUInt64(Console.ReadLine());

            GenerateTestFile.GenerateFile(rowCount, rowLength);
            Console.WriteLine("[+] Генерация файла завершена");

        }
        static void Sort()
        {
            LblSorting.QSortFile();
            Console.WriteLine("[+] Сортирвка успешна!");
        }
    }
}
