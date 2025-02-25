using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Lab1.Interfaces
{
    public interface IGenericRepositories<T> where T : class
    {
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        T GetById(int id);
        T GetById(string id);
        IEnumerable<T> GetAll();
    }
}
