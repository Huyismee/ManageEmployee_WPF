using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using PRN221_Lab1.DTO;
using PRN221_Lab1.Interfaces;
using PRN221_Lab1.Models;
using PRN221_Lab1.Services;
using LiveCharts;
using LiveCharts.Wpf;

namespace PRN221_Lab1.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private IEnumerable<OrdersDto> listOrders;
        private IEnumerable<Employee> listEmployees;
        private IEnumerable<Customer> listCustomer;
        private OrdersDto curOrder;
        private Order selectedOrder;
        private Employee curEmployee;
        private string curCustomerId;
        private int curEmployeeId;
        private IEnumerable<EmployeeResponseDto> _employeeResponseDtos;
        private INorthwindService _northwindService;
        private List<string> labels = new List<string>();
        private SeriesCollection seriesCollection;
        private RelayCommand _deleteOrdCommand;

        public RelayCommand DeleteOrdCommand
        {
            get => _deleteOrdCommand; set => _deleteOrdCommand = value;
        }

        public SeriesCollection SeriesCollection
        {
            get
            {
                return seriesCollection;
            }
            set
            {
                seriesCollection = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SeriesCollection"));
                    Debug.Print("Chart updated!");
                }
            }
        }

        public List<string> Labels
        {
            get
            {
                return labels;
            }
            set
            {
                labels = value;
            }
        }

        public Func<double, string> Formatter { get; set; }

        public IEnumerable<EmployeeResponseDto> EmployeeResponseDtos
        {
            get
            {
                return _employeeResponseDtos;
            }
            set
            {
                _employeeResponseDtos = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("EmployeeResponseDtos"));
                    Debug.Print("Chart!!");
                }
            }
        }

        public Order SelectedOrder
        {
            get
            {
                return selectedOrder;
            }
            set
            {
                selectedOrder = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedOrder"));
                    _northwindService.SaveChange();
                }
            }
        }


        public IEnumerable<OrdersDto> Orders
        {
            get
            {
                return listOrders;
            }
            set
            {
                listOrders = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Orders"));
                }
            }
        }
        public IEnumerable<Employee> Employees
        {
            get
            {
                return listEmployees;
            }
            set
            {
                listEmployees = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Employees"));
                }
            }
        }
        public IEnumerable<Customer> Customers
        {
            get
            {
                return listCustomer;
            }
            set
            {
                listCustomer = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Customers"));
                }
            }
        }

        public OrdersDto CurOrder
        {
            get { return curOrder; }
            set
            {
                curOrder = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CurOrder"));
                    if (curOrder != null)
                    {
                        SelectedOrder = _northwindService.MapOrder(CurOrder);
                        SelectedOrder.RequiredDate = CurOrder.RequiredDate;
                    }
                   
                    _northwindService.SaveChange();
                }
            }
        }
        public Employee CurEmployee
        {
            get { return curEmployee; }
            set
            {
                curEmployee = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CurEmployee"));
                    Orders = _northwindService.GetOrdersByEmployee(CurEmployee);
                }
            }
        }
        public string CurCustomerId
        {
            get { return curCustomerId; }
            set
            {
                curCustomerId = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CurCustomerId"));
                    if (CurCustomerId != null)
                    {
                        SelectedOrder = _northwindService.MapOrder(CurOrder);
                        Customer cus = _northwindService.GetCustomerById(curCustomerId);
                        SelectedOrder.Customer = cus;
                        _northwindService.SaveChange();
                    }
                }
            }
        }
        public int CurEmployeeId
        {
            get { return curEmployeeId; }
            set
            {
                curEmployeeId = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CurCustomerId"));
                    if (CurEmployeeId != null)
                    {
                        SelectedOrder = _northwindService.MapOrder(CurOrder);
                        Employee emp = _northwindService.GetEmployeeById(CurEmployeeId);
                        SelectedOrder.Employee = emp;
                        _northwindService.SaveChange();
                    }
                    chartDetails();
                }
            }
        }

    

        public OrderViewModel(INorthwindService northwindService)
        {
            _northwindService = northwindService;
            Orders = _northwindService.GetOrdersByEmployee(CurEmployee);
            Employees = _northwindService.GetOrderEmployees();
            Customers = _northwindService.GetCustomers();
            DeleteOrdCommand = new RelayCommand(OnDelete);
            chartDetails();
        }

        public void chartDetails()
        {
            EmployeeResponseDtos = _northwindService.GetEmployeeTotal();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries()
                {
                    Title = "Total",
                    Values = new ChartValues<Decimal>()
                },
            };
            foreach (var emp in EmployeeResponseDtos)
            {
                SeriesCollection[0].Values.Add(emp.Total);
                Labels.Add(emp.Employee.LastName);
            }
            Formatter = value => value.ToString("N") + "$";
        }
        private void OnDelete()
        {
            if (CurOrder != null)
            {
                Delete(CurOrder.OrderId);
                Orders = _northwindService.GetOrdersByEmployee(CurEmployee);
            }
            
        }
        private void Delete(int id)
        {
            _northwindService.DeleteOrd(id);
        }


    }






}
