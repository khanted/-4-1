using System;

class Program
{
    // Главный метод программы
    static void Main()
    {
        // Установка кодировки консоли для поддержки Unicode
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Предложение пользователю выбрать тип данных для массива
        Console.WriteLine("Выберите номер типа данных, который нужно применить для массива:");
        Console.WriteLine("1. int");
        Console.WriteLine("2. double");
        Console.WriteLine("3. string");
        Console.WriteLine("4. bool");
        Console.Write("Ваш выбор: ");
        string typeChoice = Console.ReadLine(); // Чтение выбора пользователя

        // Запрос размера массива от пользователя
        Console.Write("Хотите указать размер массива? (да/нет): ");
        int size = Console.ReadLine().ToLower() == "да" ? AskForArraySize() : 4; // Получение размера массива

        // Обработка выбора пользователя и создание массива соответствующего типа
        switch (typeChoice)
        {
            case "1":
                ProcessArray<int>(size);
                break;
            case "2":
                ProcessArray<double>(size);
                break;
            case "3":
                ProcessArray<string>(size);
                break;
            case "4":
                ProcessArray<bool>(size);
                break;
            default:
                Console.WriteLine("Неверный выбор. Попробуйте еще раз, выбрав номер от 1 до 4.");
                break; // Прекращение выполнения программы при неверном выборе
        }
    }

    // Метод для заполнения и обработки массива
    static void ProcessArray<T>(int size) where T : IComparable<T>
    {
        var array = new CustomArray<T>(size); // Создание экземпляра массива

        // Заполнение массива значениями, введенными пользователем
        Console.WriteLine($"Введите {size} элементов для заполнения массива:");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"Элемент [{i}]: ");
            array.Add(ReadValue<T>());
        }

        ShowArrayOperations(array); // Демонстрация операций с массивом
    }

    // Метод для запроса размера массива у пользователя
    static int AskForArraySize()
    {
        Console.Write("Введите размер массива: ");
        return int.Parse(Console.ReadLine()); // Возврат введенного размера
    }

    // Метод для чтения и преобразования введенного значения в тип T
    static T ReadValue<T>() where T : IComparable<T>
    {
        while (true) // Бесконечный цикл для обработки ввода
        {
            var input = Console.ReadLine();
            try
            {
                // Попытка преобразования ввода в тип T
                if (typeof(T) == typeof(bool) && bool.TryParse(input, out bool boolValue))
                {
                    return (T)(object)boolValue; // Для bool используется TryParse
                }
                else
                {
                    return (T)Convert.ChangeType(input, typeof(T)); // Для остальных типов используется Convert.ChangeType
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный формат. Пожалуйста, введите значение ещё раз:");
            }
        }
    }

    // Метод для демонстрации операций с массивом
    static void ShowArrayOperations<T>(CustomArray<T> array) where T : IComparable<T>
    {
        // Вывод исходного содержимого массива
        Console.WriteLine("\nИсходное содержимое массива:");
        array.ForEach(item => Console.WriteLine(item));

        // Сортировка и вывод отсортированного массива
        array.Sort();
        Console.WriteLine("\nСодержимое массива после сортировки:");
        array.ForEach(item => Console.WriteLine(item));

        // Вывод минимального и максимального элементов
        Console.WriteLine($"\nМинимальный элемент: {array.Min()}");
        Console.WriteLine($"Максимальный элемент: {array.Max()}");

        // Переворот и вывод перевернутого массива
        array.Reverse();
        Console.WriteLine("\nМассив после переворота:");
        array.ForEach(item => Console.WriteLine(item));
    }
}