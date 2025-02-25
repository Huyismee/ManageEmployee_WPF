using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobileLibrary.Repositories.Common;
using AutomobileLibrary.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PRN221_Lab1.DTO;
using PRN221_Lab1.Interfaces;
using PRN221_Lab1.Models;
using AutoMapper;

namespace AutomobileLibrary.Repositories
{
    public class NorthwindRepository: GenericRepositories<Order>, INorthwindRepository
    {
        private readonly PRN221Context _dbContext;

        public NorthwindRepository(PRN221Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }

        public IEnumerable<Order> GetOrdersByEmployee(Employee employee)
        {
            List<Order> orders = new List<Order>();
            if (employee != null)
            {
                orders = _dbContext.Orders.Include(e => e.Employee).Include(e => e.Customer)
                    .Include(e => e.OrderDetails).Where(e => e.Employee == employee).ToList();
                Debug.Print(employee.LastName);
            }
            else
            {
                orders = _dbContext.Orders.Include(e => e.Employee).Include(e => e.Customer)
                    .Include(e => e.OrderDetails).ToList();
                Debug.Print("What the fuck?");
            }

            return orders;
        }
        public IEnumerable<Order> GetAllOrders()
        {
           
            List<Order> orders = new List<Order>();
            orders = _dbContext.Orders.Include(e => e.Employee)
                .Include(e => e.Customer).Include(e => e.OrderDetails).ToList();
            
            return orders;
        }

        public IEnumerable<Employee> GetOrderEmployees()
        {
            List<Employee> employees = new List<Employee>();
            employees = _dbContext.Employees.Where(e => e.Orders != null).ToList();
            return employees;
        }
        public IEnumerable<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers = _dbContext.Customers.ToList();
            return customers = _dbContext.Customers.ToList();
            ;
        }

        public List<OrderDetail> GetOrderDetail(int orderId)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            orderDetails = _dbContext.OrderDetails.Where(e => e.OrderId == orderId).ToList();
            return orderDetails;
        }

        public Decimal TotalPrice(List<OrderDetail> orderDetails)
        {
            Decimal total = 0;
            foreach (var obj in orderDetails)
            {
                total += (obj.UnitPrice * obj.Quantity);
            }
            return total;
        }

        public Order GetOrderById(int orderId)
        {
            Order ord = _dbContext.Orders.FirstOrDefault(e => e.OrderId == orderId);
            return ord;
        }

        public void DeleteOrd(int orderId)
        {
            Order ord = _dbContext.Orders.FirstOrDefault(e => e.OrderId == orderId);
            _dbContext.OrderDetails.RemoveRange(ord.OrderDetails);
            _dbContext.Orders.Remove(ord);
        }

        public void SaveChange()
        {
            _dbContext.SaveChanges();
        }
    }

    public class CustomerRepository : GenericRepositories<Customer>, ICustomerRepository
    {
        public CustomerRepository(PRN221Context dbcontext) : base(dbcontext)
        {
        }
    }
    public class EmployeeRepository : GenericRepositories<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(PRN221Context dbcontext) : base(dbcontext)
        {
        }
    }
}
