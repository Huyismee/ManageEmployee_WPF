using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace PRN221_Lab1.Interfaces
{
    public interface IUnitOfWork
    {
        INorthwindRepository NorthwindRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        int Complete();
    }
}
