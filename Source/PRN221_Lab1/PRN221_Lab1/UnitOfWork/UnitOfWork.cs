using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobileLibrary.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PRN221_Lab1.Interfaces;
using PRN221_Lab1.Models;

namespace AutomobileLibrary.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PRN221Context _dbContext;

        public UnitOfWork(PRN221Context dbContext)
        {
            _dbContext = dbContext;
            NorthwindRepository = new NorthwindRepository(_dbContext);
            CustomerRepository = new CustomerRepository(_dbContext);
            EmployeeRepository = new EmployeeRepository(_dbContext);
        }

        public INorthwindRepository NorthwindRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
