using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EmployeeBenefits.Type.Data;

namespace BenefitCostCalculator.Test
{
    public class MemoryRepository<T> : IRepository<T>
    {
        private IDictionary<string, T> dataTable = new Dictionary<string, T>();

        public bool Contains(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            return dataTable.ContainsKey(id);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return dataTable.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dataTable.GetEnumerator();
        }

        public void Save(string id, T obj)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            dataTable[id] = obj;
        }

        public T Get(string id)
        {
            return Get(id, default(T));
        }

        public T Get(string id, T defaultValue)
        {
            if (id == null)
            {
                return defaultValue;
            }

            T dataValue;
            return dataTable.TryGetValue(id, out dataValue)
                ? dataValue
                : defaultValue;
        }

        public void Delete(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            dataTable.Remove(id);
        }
    }
}