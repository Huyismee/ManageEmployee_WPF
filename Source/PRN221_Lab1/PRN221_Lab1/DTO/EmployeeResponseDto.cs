using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN221_Lab1.Models;

namespace PRN221_Lab1.DTO
{
    public class EmployeeResponseDto
    {
        public Employee Employee { get; set; }
        public Decimal? Total { get; set; }
    }
}
