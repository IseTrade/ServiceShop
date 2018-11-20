using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceShop.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }   //Primary key
        public string MotherBoard { get; set; }
        public double? MotherBoardPrice { get; set; }
        public string VideoCard { get; set; }
        public double? VideoCardPrice { get; set; }
        public string PowerSupply { get; set; }
        public double? PowerSupplyPrice { get; set; }
        public string Cpu { get; set; }
        public double? CpuPrice { get; set; }
        public string HardDrive { get; set; }
        public double? HardDrivePrice { get; set; }
        public string Case { get; set; }
        public double? CasePrice { get; set; }
        public string Memory { get; set; }
        public double? MemoryPrice { get; set; }
        public string Fan { get; set; }
        public double? FanPrice { get; set; }
        public string CpuCooler { get; set; }
        public double? CpuCoolerPrice { get; set; }
        public double? VirusRemoval { get; set; }
        public double? DataRecovery { get; set; }
        public double? InstallOs { get; set; }
        public double? Labor { get; set; }
        public string Comment { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public string WorkOrderStatus { get; set; }
        public string Description { get; set; }
        public string PictureUpload { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }  //foreign key
        public Customer Customer { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }  //foreign key
        public Employee Employee { get; set; }
    }
}