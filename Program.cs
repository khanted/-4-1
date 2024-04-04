using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // Поддержка Unicode в консоли.

        // Предлагаем пользователю выбрать тип данных массива.
        Console.WriteLine("Выберите номер типа данных для массива:");
        Console.WriteLine("1. int");
        Console.WriteLine("2. double");
        Console.WriteLine("3. string");
        Console.WriteLine("4. bool");
        Console.Write("Ваш выбор: ");
        string typeChoice = Console.ReadLine(); // Чтение выбора пользователя.

        // Запрашиваем у пользователя размер массива.
        Console.Write("Укажите размер массива (по умолчанию 4): ");
        int size = Console.ReadLine().ToLower() == "да" ? AskForArraySize() : 4; // Определение размера массива.

        // Обработка выбора типа данных массива.
        switch (typeChoice)
        {
            case "1":
                ProcessArray<int>(size); // Работа с массивом int.
                break;
            case "2":
                ProcessArray<double>(size); // Работа с массивом double.
                break;
            case "3":
                ProcessArray<string>(size); // Работа с массивом string.
                break;
            case "4":
                ProcessArray<bool>(size); // Работа с массивом bool.
                break;
            default:
                Console.WriteLine("Неверный выбор. Выберите номер от 1 до 4."); // Сообщение об ошибке при неправильном выборе.
                break;
        }
    }

    static int AskForArraySize()
    {
        Console.Write("Введите размер массива: ");
        if (int.TryParse(Console.ReadLine(), out int size)) // Попытка конвертации ввода в int.
        {
            return size; // Возврат размера.
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Используется размер по умолчанию: 4."); // Сообщение об ошибке.
            return 4; // Возврат значения по умолчанию.
        }
    }

    static void ProcessArray<T>(int size) where T : IComparable<T>
    {
         static void PrintItem<T>(T item)
        {
            Console.WriteLine(item);
        }

        static void PrintItemProjection<T>(T item)
        {
            ((string)(object)item).Length;
        }

        static bool FuncForCountIf<T>(T x)
        {
            return Convert.ToDouble(x) > 0;
        }
        // Создание экземпляра кастомного массива с указанным размером
        var customArray = new CustomArray<T>(size);

        // Заполнение массива элементами, введенными пользователем
        for (int i = 0; i < size; i++)
        {
            Console.Write($"Введите элемент {i + 1} из {size} (тип {typeof(T).Name}): ");
            T value = ReadValue<T>();
            customArray.Add(value);
        }

        // Демонстрация базовых операций с массивом
        Console.WriteLine("\nМассив успешно создан.");
        Console.WriteLine("Элементы массива:");
        customArray.ForEach(PrintItem);

        // Демонстрация операций поиска минимального и максимального элементов
        Console.WriteLine($"Минимальный элемент: {customArray.Min()}");
        Console.WriteLine($"Максимальный элемент: {customArray.Max()}");

        // Пример использования CountIf
        Console.WriteLine("\nПример использования CountIf (элементы больше нуля):");
        Console.WriteLine(customArray.CountIf(FuncForCountIf)); // Примечание: это условие работает только для числовых типов

        // Демонстрация базовых операций:
        Console.WriteLine("\nТестирование дополнительных операций:");

        // Тест Contains
        Console.WriteLine("Введите элемент для проверки его наличия в массиве:");
        T searchElement = ReadValue<T>();
        bool contains = customArray.Contains(searchElement);
        Console.WriteLine($"Элемент {(contains ? "найден" : "не найден")} в массиве.");

        // Тест Remove
        Console.WriteLine("Введите элемент для удаления из массива:");
        T removeElement = ReadValue<T>();
        bool removed = customArray.Remove(removeElement);
        Console.WriteLine($"Элемент {(removed ? "был удален" : "не найден для удаления")} из массива.");
        customArray.ForEach(PrintItem); // Повторный вывод массива для проверки

        if (typeof(T) == typeof(string))
        {
            Console.WriteLine("Минимальная длина строки в массиве: " + customArray.MinByProjection(PrintItemProjection));
            Console.WriteLine("Максимальная длина строки в массиве: " + customArray.MaxByProjection(PrintItemProjection));
        }

        // Сортировка и вывод отсортированного массива
        customArray.Sort();
        Console.WriteLine("\nОтсортированный массив:");
        customArray.ForEach(PrintItem);

        // Переворот и вывод перевернутого массива
        customArray.Reverse();
        Console.WriteLine("\nПеревернутый массив:");
        customArray.ForEach(PrintItem);
    }
    
    // Метод для чтения и конвертации введенного значения в тип T
    static T ReadValue<T>()
    {
        while (true)
        {
            string input = Console.ReadLine(); // Чтение строки из консоли.
            try
            {
                return (T)Convert.ChangeType(input, typeof(T)); // Попытка конвертации ввода в нужный тип.
            }
            catch
            {
                Console.Write("Некорректный ввод. Пожалуйста, введите значение еще раз: "); // Сообщение об ошибке.
            }
        }
    }
}