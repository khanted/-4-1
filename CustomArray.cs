using System;
using System.Collections;
using System.Collections.Generic;

// Определение обобщенного класса CustomArray<T>, который поддерживает итерацию через IEnumerable<T>
public class CustomArray<T> : IEnumerable<T>
{
    private T[] _items; // Внутреннее хранилище элементов
    private int _count = 0; // Количество элементов в массиве

    // Конструктор по умолчанию задает начальную емкость массива равной 4
    public CustomArray() : this(4) { }

    // Конструктор с параметром для задания начальной емкости массива
    public CustomArray(int capacity)
    {
        _items = new T[capacity];
    }

    // Добавление элемента в массив
    public void Add(T item)
    {
        // Если достигнута текущая емкость массива, увеличиваем емкость
        if (_count == _items.Length)
        {
            IncreaseCapacity();
        }
        // Добавление нового элемента и увеличение счетчика элементов
        _items[_count++] = item;
    }

    // Внутренний метод для увеличения емкости массива
    private void IncreaseCapacity()
    {
        // Новая емкость определяется как 2n + 1
        int newCapacity = _items.Length * 2 + 1;
        // Создание нового массива с увеличенной емкостью
        T[] newArray = new T[newCapacity];
        // Копирование элементов из старого массива в новый
        Array.Copy(_items, newArray, _count);
        // Обновление ссылки на внутренний массив
        _items = newArray;
    }

    // Удаление элемента из массива
    public bool Remove(T item)
    {
        // Поиск элемента для удаления
        int index = Array.IndexOf(_items, item, 0, _count);
        // Если элемент не найден, возвращаем false
        if (index < 0) return false;
        // Сдвигаем элементы для заполнения пробела
        Array.Copy(_items, index + 1, _items, index, _count - index - 1);
        // Очищаем последний элемент и уменьшаем счетчик
        _items[--_count] = default(T);
        return true;
    }

    // Сортировка элементов массива
    public void Sort()
    {
        Array.Sort(_items, 0, _count);
    }

    // Получение количества элементов в массиве
    public int Count()
    {
        return _count; // Возвращаем количество элементов, а не длину массива
    }
    // Метод для подсчета элементов, удовлетворяющих условию
    public int CountIf(Func<T, bool> predicate)
    {
        int count = 0;
        foreach (T item in _items)
        {
            if (predicate(item))
            {
                count++;
            }
        }
        return count;
    }
    // Метод для проверки, удовлетворяет ли хотя бы один элемент условию
    public bool Any(Func<T, bool> predicate)
    {
        foreach (T item in _items)
        {
            if (predicate(item))
            {
                return true;
            }
        }
        return false;
    }
    // Метод для проверки, удовлетворяют ли все элементы условию
    public bool All(Func<T, bool> predicate)
    {
        foreach (T item in _items)
        {
            if (!predicate(item))
            {
                return false;
            }
        }
        return true;
    }
    // Метод для проверки наличия элемента в массиве
    public bool Contains(T item)
    {
        foreach (T currentItem in _items)
        {
            if (currentItem.Equals(item))
            {
                return true;
            }
        }
        return false;
    }

    // Метод для получения первого элемента, удовлетворяющего условию
    public T Find(Func<T, bool> predicate)
    {
        foreach (T item in _items)
        {
            if (predicate(item))
            {
                return item;
            }
        }
        return default(T); // Возвращаем значение по умолчанию, если ничего не найдено
    }

    // Метод для применения действия ко всем элементам массива
    public void ForEach(Action<T> action)
    {
        foreach (T item in _items)
        {
            action(item);
        }
    }

    // Переворот массива
    public void Reverse()
    {
        Array.Reverse(_items, 0, _count);
    }

    // Получение минимального элемента массива
    public T Min()
    {
        if (_count == 0) throw new InvalidOperationException("The array is empty.");
        T min = _items[0];
        for (int i = 1; i < _count; i++)
        {
            if (((IComparable<T>)_items[i]).CompareTo(min) < 0) min = _items[i];
        }
        return min;
    }

    // Получение максимального элемента массива
    public T Max()
    {
        if (_count == 0) throw new InvalidOperationException("The array is empty.");
        T max = _items[0];
        for (int i = 1; i < _count; i++)
        {
            if (((IComparable<T>)_items[i]).CompareTo(max) > 0) max = _items[i];
        }
        return max;
    }

    // Реализация GetEnumerator
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[i];
        }
    }

    // Явная реализация интерфейса IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}