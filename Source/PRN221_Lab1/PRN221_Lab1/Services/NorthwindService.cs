using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PRN221_Lab1.DTO;
using PRN221_Lab1.Interfaces;
using PRN221_Lab1.Models;

namespace PRN221_Lab1.Services
{
    public class NorthwindService : INorthwindService
    {
        private IUnitOfWork _unitOfWork;
        private INorthwindRepository _northwindRepository;
        private readonly IMapper _mapper;

        public NorthwindService(IUnitOfWork unitOfWork, INorthwindRepository northwindRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _northwindRepository = northwindRepository;
            _mapper = mapper;
        }

        public IEnumerable<OrdersDto> GetOrdersByEmployee(Employee employee)
        {
            List<OrdersDto> ordersDto = new List<OrdersDto>();
            IEnumerable<Order> orders = _northwindRepository.GetOrdersByEmployee(employee);
            foreach (var order in orders)
            {
                var data = _mapper.Map<OrdersDto>(order);
                data.TotalPrice = TotalPrice(order.OrderId);
                ordersDto.Add(data);
            }
            return ordersDto;
        }

        public IEnumerable<OrdersDto> GetAllOrdersDto()
        {
            List<OrdersDto> ordersDtos = new List<OrdersDto>();
            IEnumerable<Order> orders = _unitOfWork.NorthwindRepository.GetAll();
            foreach (var order in orders)
            {
                var data = _mapper.Map<OrdersDto>(order);
                ordersDtos.Add(data);
            }

            return ordersDtos;
        }

        public IEnumerable<Employee> GetOrderEmployees()
        {
            IEnumerable<Employee> employees = _northwindRepository.GetOrderEmployees();
            return employees;
        }
        public IEnumerable<Customer> GetCustomers()
        {
            IEnumerable<Customer> customers = _northwindRepository.GetCustomers();
            return customers;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            IEnumerable<Order> orders = _northwindRepository.GetAllOrders();

            return orders;
        }

        public Decimal TotalPrice(int orderId)
        {
            decimal totalPrice;
            List<OrderDetail> orderDetails = _northwindRepository.GetOrderDetail(orderId);
            totalPrice = _northwindRepository.TotalPrice(orderDetails);
            return totalPrice;
        }

        public void SaveChange()
        {
            _northwindRepository.SaveChange();
        }

        public Order MapOrder(OrdersDto ordersDto)
        {
            Order order = _unitOfWork.NorthwindRepository.GetById(ordersDto.OrderId);
            _unitOfWork.NorthwindRepository.Update(order);
            _northwindRepository.SaveChange();
            return order;
        }

        public Customer GetCustomerById(string id)
        {
            Customer cus = _unitOfWork.CustomerRepository.GetById(id);
            return cus;
        }
        public Employee GetEmployeeById(int id)
        {
            Employee emp = _unitOfWork.EmployeeRepository.GetById(id);
            return emp;
        }

        public IEnumerable<EmployeeResponseDto> GetEmployeeTotal()
        {
            List<EmployeeResponseDto> empList = new List<EmployeeResponseDto>();
            IEnumerable<OrdersDto> ordersDtos = GetOrdersByEmployee(null);
            IEnumerable<Employee> employees = GetOrderEmployees();

            foreach (var emp in employees)
            {
                EmployeeResponseDto empResponseDto = new EmployeeResponseDto();
                empResponseDto.Total = 0;
                empResponseDto.Employee = emp;
                foreach (var ord in ordersDtos)
                {
                    if (ord.Employee == emp)
                        empResponseDto.Total += ord.TotalPrice;
                }
                empList.Add(empResponseDto);
            }
            return empList;
        }

        public Order GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrd(int orderId)
        {
            _northwindRepository.DeleteOrd(orderId);
            _unitOfWork.NorthwindRepository.SaveChange();
        }

        public void DeleteOrderById(int id)
        {
            Order ord = _unitOfWork.NorthwindRepository.GetById(id); 
            _unitOfWork.NorthwindRepository.Delete(ord);
            _unitOfWork.NorthwindRepository.SaveChange();
        }
    }
}
