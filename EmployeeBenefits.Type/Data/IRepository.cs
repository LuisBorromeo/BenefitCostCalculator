using System.Collections.Generic;

namespace EmployeeBenefits.Type.Data
{
    public interface IRepository<T> : IEnumerable<T>
    {
        void Save(string id, T obj);
        T Get(string id);
        void Delete(string id);
    }
}