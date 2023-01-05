using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace HR.DomainModels.Models
{
    public class EmployeesUsers: IdentityUser
    {
        public int? Emp_ID { get; set; }
       
    }
}
