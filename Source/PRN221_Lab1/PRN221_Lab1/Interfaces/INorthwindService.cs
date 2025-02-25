using PRN221_Lab1.DTO;
using PRN221_Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Lab1.Interfaces
{
    public interface INorthwindService
    {
        public IEnumerable<OrdersDto> GetOrdersByEmployee(Employee employee);
        public IEnumerable<Order> GetAllOrders();
        public IEnumerable<OrdersDto> GetAllOrdersDto();
        public IEnumerable<Employee> GetOrderEmployees();
        public IEnumerable<Customer> GetCustomers();
        public Decimal TotalPrice(int orderId);
        public void SaveChange();
        public Order MapOrder(OrdersDto ordersDto);
        public Customer GetCustomerById(string id);
        public Employee GetEmployeeById(int id);
        public IEnumerable<EmployeeResponseDto> GetEmployeeTotal();
        public Order GetOrderById(int id);
        public void DeleteOrd(int orderId);

        public void DeleteOrderById(int id);

    }
}
