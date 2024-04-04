using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; 

public class CustomArray<T> : IEnumerable<T> where T : IComparable<T>
{
    private T[] _items;
    private int _count = 0;

    public CustomArray() : this(4) { }

    public CustomArray(int capacity)
    {
        _items = new T[capacity];
    }

    public void Add(T item)
    {
        if (_count == _items.Length)
        {
            IncreaseCapacity();
        }
        _items[_count++] = item;
    }

    // Увеличивает вместимость массива, когда необходимо добавить новый элемент
    private void IncreaseCapacity()
    {
        T[] newArray = new T[_items.Length * 2 + 1]; // Создаем новый массив с удвоенной вместимостью
        _items.CopyTo(newArray, 0); // Копируем элементы в новый массив
        _items = newArray; // Обновляем ссылку на массив
    }

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

    // Проверяет, содержит ли массив указанный элемент
    public bool Contains(T item)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_items[i].Equals(item))
            {
                return true; 
            }
        }
        return false; 
    }

    public T Find(Func<T, bool> predicate)
    {
        foreach (T item in _items)
        {
            if (predicate(item))
            {
                return item;
            }
        }
        return default(T);
    }

    // Применяет указанное действие к каждому элементу массива
    public void ForEach(Action<T> action)
    {
        for (int i = 0; i < _count; i++)
        {
            action(_items[i]); // Выполняем действие для каждого элемента
        }
    }

    // Удаляет первое вхождение указанного элемента из массива
    public bool Remove(T item)
    {
        int index = Array.IndexOf(_items, item, 0, _count); // Находим индекс элемента
        if (index < 0) return false; // Если элемент не найден, возвращаем false
        Array.Copy(_items, index + 1, _items, index, _count - index - 1); // Сдвигаем элементы
        _items[--_count] = default(T); // Обнуляем последний элемент
        return true; // Возвращаем true, указывая, что элемент был удален
    }

    // Сортирует элементы в массиве
    public void Sort()
    {
        Array.Sort(_items, 0, _count); 
    }

    // Получение количества элементов в массиве
    public int Count()
    {
        return _count; 
    }

    public void Reverse()
    {
        Array.Reverse(_items, 0, _count);
    }

    // Возвращает минимальный элемент
    public T Min()
    {
        if (_count == 0)
        {
            throw new InvalidOperationException("Массив пуст."); 
        }
        return _items.Take(_count).Min(); 
    }

    // Возвращает максимальный элемент
    public T Max()
    {
        if (_count == 0)
        {
            throw new InvalidOperationException("Массив пуст."); 
        }
        return _items.Take(_count).Max(); 
    }

    public TResult MinByProjection<TResult>(Func<T, TResult> projection) where TResult : IComparable<TResult>
    {
        if (_count == 0) throw new InvalidOperationException("The array is empty.");
        return _items.Take(_count).Select(projection).Min();
    }

    public TResult MaxByProjection<TResult>(Func<T, TResult> projection) where TResult : IComparable<TResult>
    {
        if (_count == 0) throw new InvalidOperationException("The array is empty.");
        return _items.Take(_count).Select(projection).Max();
    }

    public IEnumerable<TResult> Project<TResult>(Func<T, TResult> projection)
    {
        return _items.Take(_count).Select(projection);
    }

    public IEnumerable<T> GetRange(int index, int count)
    {
        if (index < 0 || index >= _count) throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative.");
        int actualCount = Math.Min(count, _count - index);
        return _items.Skip(index).Take(actualCount);
    }

    // Возвращает перечислитель, выполняющий итерацию по коллекции
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[i]; 
        }
    }

    // Возвращает перечислитель, который итерирует по коллекции
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator(); 
    }
}