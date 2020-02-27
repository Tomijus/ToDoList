using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.SQL
{
    public interface IRepository<T>
    {
        T Get(int id);
        List<T> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        List<T> Find(string task);
        List<T> Sort();
    }
}
