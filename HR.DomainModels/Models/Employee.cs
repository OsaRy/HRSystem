using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.DomainModels.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(120)]
        public string Address { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        [Required, MaxLength(50)]
        public string EmailAddress { get; set; }
        [Required,MaxLength(14)]
        public string Mobile { get; set; }

        [ForeignKey("Manager")]
        public int? Manager_id { get; set; }
        public  Employee Manager { get; set; }
        public ICollection<Employee> Managed_Employees { get; set; }

		public ICollection<EmployeeLogs> EmployeeLogs { get; set; }

	}
}
