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
        [Display(Name ="Mother Board: ")]
        public string MotherBoard { get; set; }
        [Display(Name = "Mother Board Price: ")]
        public double? MotherBoardPrice { get; set; }
        [Display(Name = "Video Card: ")]
        public string VideoCard { get; set; }
        [Display(Name = "Video Card Price: ")]
        public double? VideoCardPrice { get; set; }
        [Display(Name = "Power Supply: ")]
        public string PowerSupply { get; set; }
        [Display(Name = "Power Supply Price: ")]
        public double? PowerSupplyPrice { get; set; }
        [Display(Name = "CPU: ")]
        public string Cpu { get; set; }
        [Display(Name = "CPU Price: ")]
        public double? CpuPrice { get; set; }
        [Display(Name = "Hard Drive: ")]
        public string HardDrive { get; set; }
        [Display(Name = "Hard Drive Price: ")]
        public double? HardDrivePrice { get; set; }
        [Display(Name = "Case: ")]
        public string Case { get; set; }
        [Display(Name = "Case Price: ")]
        public double? CasePrice { get; set; }
        [Display(Name = "Memory: ")]
        public string Memory { get; set; }
        [Display(Name = "Memory Price: ")]
        public double? MemoryPrice { get; set; }
        [Display(Name = "Fan: ")]
        public string Fan { get; set; }
        [Display(Name = "Fan Price: ")]
        public double? FanPrice { get; set; }
        [Display(Name = "CPU Cooler: ")]
        public string CpuCooler { get; set; }
        [Display(Name = "CPU Cooler Price: ")]
        public double? CpuCoolerPrice { get; set; }
        [Display(Name = "Virus Removal: ")]
        public double? VirusRemoval { get; set; }
        [Display(Name = "Data Recovery: ")]
        public double? DataRecovery { get; set; }
        [Display(Name = "Install OS: ")]
        public double? InstallOs { get; set; }
        [Display(Name = "Labor: ")]
        public double? Labor { get; set; }
        [Display(Name = "Comment: ")]
        public string Comment { get; set; }
        [Display(Name = "Payment Status: ")]
        public string PaymentStatus { get; set; }
        [Display(Name = "Work Order Date: ")]
        public DateTime? WorkOrderDate { get; set; }
        [Display(Name = "Order Status: ")]
        public string WorkOrderStatus { get; set; }
        [Display(Name = "Description: ")]
        public string Description { get; set; }
        [Display(Name = "Picture Upload: ")]
        public string PictureUpload { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }  //foreign key
        public Customer Customer { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }  //foreign key
        public Employee Employee { get; set; }
    }

    public enum PayStat
    {
        Pending,
        Paid
    }

    public enum OrderStat
    {
        Pending,
        Completed
    }
}