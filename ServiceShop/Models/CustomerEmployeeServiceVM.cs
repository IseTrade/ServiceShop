using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceShop.Models
{
    public class CustomerEmployeeServiceVM
    {
        public Employee employee { get; set; }
        public Customer customer { get; set; }
        public Service service { get; set; }
    }
}