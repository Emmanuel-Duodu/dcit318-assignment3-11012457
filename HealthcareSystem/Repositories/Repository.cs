using System;
using System.Collections.Generic;

namespace HealthcareSystem.Repositories
{
    public class Repository<T>
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public List<T> GetAll()
        {
            return items;
        }

        // Convert Func<T,bool> to Predicate<T> for List.Find
        public T? GetById(Func<T, bool> predicate)
        {
            return items.Find(new Predicate<T>(predicate));
        }

        public bool Remove(Func<T, bool> predicate)
        {
            var item = items.Find(new Predicate<T>(predicate));
            if (item != null)
            {
                items.Remove(item);
                return true;
            }
            return false;
        }
    }
}
