using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN221_Lab1.Interfaces;
using PRN221_Lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobileLibrary.Repositories.Common
{
    public class GenericRepositories<T> : IGenericRepositories<T> where T : class
    {
        private readonly PRN221Context _dbContext;

        public GenericRepositories(PRN221Context dbcontext)
        {
            _dbContext = dbcontext;
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public T GetById(string id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking()
                .ToList();
        }
    }
}
