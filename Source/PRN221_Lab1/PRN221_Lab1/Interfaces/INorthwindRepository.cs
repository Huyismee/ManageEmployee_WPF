using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN221_Lab1.DTO;
using PRN221_Lab1.Models;

namespace PRN221_Lab1.Interfaces
{
    public interface INorthwindRepository: IGenericRepositories<Order>
    {
        public IEnumerable<Order> GetOrdersByEmployee(Employee employee);
        public IEnumerable<Order> GetAllOrders();
        public IEnumerable<Employee> GetOrderEmployees();
        public IEnumerable<Customer> GetCustomers();
        public List<OrderDetail> GetOrderDetail(int orderId);
        public Decimal TotalPrice(List<OrderDetail> orderDetails);
        public void DeleteOrd(int orderId);
        public Order GetOrderById(int orderId);
        public void SaveChange();

    }

    public interface ICustomerRepository: IGenericRepositories<Customer>{}
    public interface IEmployeeRepository: IGenericRepositories<Employee>{}
}
